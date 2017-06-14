namespace ForumOrm.Models
{
    public interface IEntityVisitor
    {
        void Visit(User user);

        void Visit(Topic topic);

        void Visit(Comment comment);

        void Visit(Role role);

        void Visit(Section section);

        void Visit(Status status);

        void Visit(Image image);
    }
}
