namespace ForumDal.Interface.Models
{
    public interface IDalEntityVisitor
    {
        void Visit(DalUser dalUser);

        void Visit(DalTopic dalTopic);

        void Visit(DalComment dalComment);

        void Visit(DalRole dalRole);

        void Visit(DalSection dalSection);

        void Visit(DalStatus dalStatus);

        void Visit(DalImage dalImage);
    }
}
