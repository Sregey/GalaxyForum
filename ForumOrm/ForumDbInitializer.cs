﻿using System;
using System.Data.Entity;
using ForumOrm.Models;

namespace ForumOrm
{
    public class ForumDbInitializer : //DropCreateDatabaseIfModelChanges<ForumDbContext>
        //DropCreateDatabaseAlways<ForumDbContext>
        CreateDatabaseIfNotExists<ForumDbContext>
    {
        protected override void Seed(ForumDbContext context)
        {
            InitializeRoles(context);
            InitializeStatuses(context);
            InitializeSections(context);
            InitializeUsers(context);
            InitializeTopics(context);
            InitializeComments(context);

            base.Seed(context);
        }

        private void InitializeRoles(ForumDbContext context)
        {
            context.Roles.Add(new Role { Id = 1, Name = "Admin" });
            context.Roles.Add(new Role { Id = 2, Name = "Moderator" });
            context.Roles.Add(new Role { Id = 3, Name = "User" });
        }

        private void InitializeStatuses(ForumDbContext context)
        {
            context.Statuses.Add(new Status { Id = 1, Name = "Raw" });
            context.Statuses.Add(new Status { Id = 2, Name = "Processed" });
            context.Statuses.Add(new Status { Id = 3, Name = "Accepted" });
            context.Statuses.Add(new Status { Id = 4, Name = "Rejected" });
        }

        private void InitializeSections(ForumDbContext context)
        {
            context.Sections.Add(new Section { Id = 1, Name = "Cars" });
            context.Sections.Add(new Section { Id = 2, Name = "Cinema" });
            context.Sections.Add(new Section { Id = 3, Name = "Cooking" });
            context.Sections.Add(new Section { Id = 4, Name = "Programming" });
            context.Sections.Add(new Section { Id = 5, Name = "Tourism" });
        }

        private void InitializeUsers(ForumDbContext context)
        {
            context.Users.Add(new User()
            {
                Id = 1,
                Login = "User_1",
                Email = "jhon@mail.ru",
                Password = "12345678",
                Name = "Jhon",
                LastName = "Black",
                Profession = "Programmer",
                ExtraInfo = "The best java programmer.",
                RegisrationDate = DateTime.Now,
                RoleId = 3
            });
            context.Users.Add(new User()
            {
                Id = 2,
                Login = "User_2",
                Email = "tom@mail.ru",
                Password = "12345678",
                Name = "Tom",
                LastName = "Brown",
                Profession = "Cook",
                ExtraInfo = "My expireiens is years.",
                RegisrationDate = DateTime.Now,
                RoleId = 3
            });
            context.Users.Add(new User()
            {
                Id = 3,
                Login = "User_3",
                Email = "vasya@gmail.com",
                Password = "12345678",
                Name = "Vasiliy",
                LastName = "Pupkin",
                FatherName = "Michilovich",
                Profession = "Unemployed",
                RegisrationDate = DateTime.Now,
                RoleId = 3
            });
            context.Users.Add(new User()
            {
                Id = 4,
                Login = "Moderator_1",
                Email = "andrew@mail.ru",
                Password = "12345678",
                Name = "Andrew",
                LastName = "Arnold",
                Profession = "Moderator",
                RegisrationDate = DateTime.Now,
                RoleId = 2
            });
            context.Users.Add(new User()
            {
                Id = 5,
                Login = "Moderator_2",
                Email = "mail@Mail.ru",
                Password = "12345678",
                Name = "Emma",
                LastName = "Ford",
                Profession = "Plumber",
                RegisrationDate = DateTime.Now,
                RoleId = 2
            });
            context.Users.Add(new User()
            {
                Id = 6,
                Login = "MainAdmin",
                Email = "main.admin@Mail.ru",
                Password = "12345678",
                Name = "Jacob",
                LastName = "Kirk",
                Profession = "God",
                RegisrationDate = DateTime.Now,
                RoleId = 1
            });
        }

        private void InitializeTopics(ForumDbContext context)
        {
            context.Topics.Add(new Topic()
            {
                Id = 1,
                Title = "How to cook scrambled eggs?",
                Text = "Please help me. It's very important for me.",
                Date = DateTime.Now,
                SectionId = 3,
                AuthorId = 1,
                StatusId = 3
            });
            context.Topics.Add(new Topic()
            {
                Id = 2,
                Title = "Where is pedals in my car?",
                Text = "I paid 10000$ for my car. But I cann't find pedals there!",
                Date = DateTime.Now,
                SectionId = 1,
                AuthorId = 6,
                StatusId = 1
            });
        }

        private void InitializeComments(ForumDbContext context)
        {
            context.Comments.Add(new Comment()
            {
                Id = 1,
                Text = "It's very diffcult. You should have a pan.",
                Date = DateTime.Now,
                TopicId = 1,
                SenderId = 4,
                StatusId = 3
            });
            context.Comments.Add(new Comment()
            {
                Id = 2,
                Text = "You should oil pan and scramd eggs on it.",
                Date = DateTime.Now,
                TopicId = 1,
                SenderId = 2,
                StatusId = 3,
                IsAnswer = true
            });
            context.Comments.Add(new Comment()
            {
                Id = 3,
                Text = "You are jackass!",
                Date = DateTime.Now,
                TopicId = 2,
                SenderId = 4,
                StatusId = 4
            });
            context.Comments.Add(new Comment()
            {
                Id = 4,
                Text = "What is a brand of your car?",
                Date = DateTime.Now,
                TopicId = 2,
                SenderId = 1,
                StatusId = 3
            });
            context.Comments.Add(new Comment()
            {
                Id = 5,
                Text = "BMW",
                Date = DateTime.Now,
                TopicId = 2,
                SenderId = 6,
                StatusId = 1
            });
        }

    }
}