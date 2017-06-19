using ForumBll.Interface.Models;
using ForumBll.Interface.Services;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ForumPlConsole
{
    class ForumConsoleApi : IDisposable
    {
        private const string LoginCommand = "login";
        private const string SectionsCommand = "sections";
        private const string SectionCommand = "section";
        private const string TopicCommand = "topic";
        private const string CommentCommand = "comment";
        private const string MyInfoCommand = "myInfo";
        private const string MyTopicsCommand = "myTopics";
        private const string AddTopicCommand = "addTopic";
        private const string AllTopicsCommand = "allTopics";
        private const string ExitCommand = "exit";

        private const string InvalidCommandError = "Invalid command";
        private const string NotEnoughParametersError = "Not enough parameters";
        private const string UnexpectedError = "Unexpected error";
        private const string NotAutorizedError = "You are not autorized";
        private const string HasLowPermissionError = "You do not have permission";
        private const string InvalidOperationError = "Some parameters of your request aren't correct";

        private readonly Dictionary<string, Action<string[]>> actions;
        private BllUser me;

        private readonly IUserService userService;
        private readonly IAccountService accountService;
        private readonly ITopicService topicService;
        private readonly ICommentService commentService;
        private readonly ISectionService sectionService;
        private readonly IRoleService roleService;

        private ForumConsoleApi()
        {
            actions = new Dictionary<string, Action<string[]>>(new ActionNameComparer())
            {
                { LoginCommand, Login },
                { ExitCommand, Exit },
                { SectionsCommand, Sections },
                { SectionCommand, Section },
                { TopicCommand, Topic },
                { CommentCommand, Comment },
                { MyInfoCommand, MyInfo },
                { MyTopicsCommand, MyTopics },
                { AddTopicCommand, AddTopic },
                { AllTopicsCommand, AllTopics },
            };
        }

        public ForumConsoleApi(IUserService userService,
            IAccountService accountService,
            ITopicService topicService,
            ICommentService commentService,
            ISectionService sectionService,
            IRoleService roleService)
            : this()
        {
            this.userService = userService;
            this.accountService = accountService;
            this.topicService = topicService;
            this.commentService = commentService;
            this.sectionService = sectionService;
            this.roleService = roleService;
        }

        public void Run()
        {
            //PrintHelp();

            while (true)
            {
                Console.Write("Forum> ");
                string command = Console.ReadLine();
                string[] commandArgs = command.Split(' ');

                Action<string[]> action;
                if (actions.TryGetValue(commandArgs[0], out action))
                {
                    try
                    {
                        action(commandArgs);
                    }
                    catch (InvalidOperationException)
                    {
                        PrintError(InvalidOperationError);
                    }
                    catch (Exception)
                    {
                        PrintError(UnexpectedError);
                    }
                }
                else
                    PrintError(InvalidCommandError);
            }
        }

        private void Login(string[] args)
        {
            /*example: login jhon@mail.ru 12345678*/

            if (args.Length < 3)
            {
                PrintError(NotEnoughParametersError);
                return;
            }
            //throw new ArgumentException(NotEnoughParametersMessage);

            me = accountService.Login(args[1], args[2]);

            Console.CursorTop--;
            ClearLine(Console.CursorTop);
            if (me != null)
                Console.WriteLine("Hello {0}", me.Login);
            else
                Console.WriteLine("There is no user with this email and password.");
        }

        private void Sections(string[] args)
        {
            var sections = sectionService.GetAllSections();
            foreach (var section in sections)
            {
                Console.WriteLine($"{section.Id}. {section.Name}");
            }
        }

        private void Section(string[] args)
        {
            if (args.Length < 2)
            {
                PrintError(NotEnoughParametersError);
                return;
            }

            int sectionId;
            if (Int32.TryParse(args[1], out sectionId))
            {
                var section = sectionService.GetSection(sectionId);
                foreach (var topic in section.Topics)
                {
                    Console.WriteLine($"{topic.Id}. {topic.Title}");
                }
            }
        }

        private void Topic(string[] args)
        {
            if (args.Length < 2)
            {
                PrintError(NotEnoughParametersError);
                return;
            }

            int topicId;
            if (Int32.TryParse(args[1], out topicId))
            {
                var topic = topicService.GetTopic(topicId);

                PrintlnFeild("Title", topic.Title);
                PrintlnFeild("Text", topic.Text);
                PrintFeilds("Comments", topic.Comments
                    .Select(c => $"({c.Sender?.Login}) {c.Text}"));
            }
        }

        private void AllTopics(string[] args)
        {
            if (!IsAutorize("Admin,Moderator"))
            {
                PrintError(HasLowPermissionError);
                return;
            }

            var topics = topicService.GetTopics();
            foreach (var topic in topics)
            {
                Console.WriteLine($"{topic.Id}. {topic.Title} ({topic.Status.Name})");
            }


            int topicId;
            if (Int32.TryParse(args[1], out topicId))
            {
                var topic = topicService.GetTopic(topicId);

                PrintlnFeild("Title", topic.Title);
                PrintlnFeild("Text", topic.Text);
                PrintFeilds("Comments", topic.Comments
                    .Select(c => $"({c.Sender?.Login}) {c.Text}"));
            }
        }

        private void AddTopic(string[] args)
        {
            if (!IsAutorize())
            {
                PrintError(NotAutorizedError);
                return;
            }

            Sections(null);

            var topic = new BllTopic
            {
                Section = new BllSection { Id = ReadInt("Section") },
                Title = ReadString("Title"),
                Text = ReadString("Text"),
                Author = me,
            };

            topicService.AddTopic(topic);

            MyTopics(null);
        }

        private void MyTopics(string[] args)
        {
            if (!IsAutorize())
            {
                PrintError(NotAutorizedError);
                return;
            }

            me = userService.GetUser(me.Id);
            foreach (var topic in me.Topics)
            {
                Console.WriteLine($"{topic.Id}. {topic.Title}");
            }
        }

        private void Comment(string[] args)
        {
            if (args.Length < 2)
            {
                PrintError(NotEnoughParametersError);
                return;
            }
            if (!IsAutorize())
            {
                PrintError(NotAutorizedError);
                return;
            }

            int topicId;
            if (Int32.TryParse(args[1], out topicId))
            {
                var comment = new BllComment
                {
                    Text = ReadString("Text"),
                    Sender = me,
                    Topic = new BllTopic { Id = topicId },
                };
                commentService.AddComment(comment);

                Topic(new string[] { null, topicId.ToString() });
            }
        }

        private void MyInfo(string[] args)
        {
            if (!IsAutorize())
            {
                PrintError(NotAutorizedError);
                return;
            }
            PrintFeild("Login", me.Login);
            PrintFeild("Email", me.Email);
            PrintFeild("Name", me.Name);
            PrintFeild("LastName", me.LastName);
            PrintFeild("FatherName", me.FatherName);
            PrintFeild("Profession", me.Profession);
            PrintlnFeild("ExtraInfo", me.ExtraInfo);
        }

        private void Exit(string[] args)
        {
            Environment.Exit(0);
        }

        private bool IsAutorize(string roles = null)
        {
            bool result = me != null;
            if (result && (roles != null))
            {
                result = roles.Split(',').Contains(me.Role.Name);
            }
            return result;
        }

        private void PrintError(string errorMessage)
        {
            Console.WriteLine("Error: {0}", errorMessage);
        }

        private void PrintFeild(string title, string text)
        {
            if (text != null)
                Console.WriteLine($"{title}: {text}");
        }

        private void PrintlnFeild(string title, string text)
        {
            if (text != null)
                Console.WriteLine($"{title}:\n\t{text}");
        }

        private void PrintFeilds(string title, IEnumerable<string> items)
        {
            Console.WriteLine($"{title}:");
            foreach (var item in items)
                Console.WriteLine($"\t{item}");
        }

        private string ReadString(string title)
        {
            string value;
            do
            {
                Console.Write($"{title}: ");
                value = Console.ReadLine();
            } while (String.IsNullOrEmpty(value));
            return value;
        }

        private int ReadInt(string title)
        {
            int value;
            do
            {
                Console.Write($"{title}: ");
            } while (!Int32.TryParse(Console.ReadLine(), out value));
            return value;
        }

        private void ClearLine(int line)
        {
            Console.MoveBufferArea(
                0, 
                line, 
                Console.BufferWidth, 
                1, 
                Console.BufferWidth, 
                line, 
                ' ', 
                Console.ForegroundColor, 
                Console.BackgroundColor);
        }

        public void Dispose()
        {
            userService.Dispose();
            accountService.Dispose();
            topicService.Dispose();
            commentService.Dispose();
            sectionService.Dispose();
            roleService.Dispose();
        }
    }
}
