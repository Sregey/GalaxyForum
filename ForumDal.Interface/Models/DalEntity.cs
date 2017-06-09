namespace ForumDal.Interface.Models
{
    public abstract class DalEntity
    {
        public int Id { get; set; }

        public virtual void Accept(IDalEntityVisitor entityVisitor)
        {
            entityVisitor.Visit((dynamic)this);
        }
    }
}
