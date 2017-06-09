namespace ForumOrm.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public virtual void Accept(IEntityVisitor entityVisitor)
        {
            entityVisitor.Visit((dynamic)this);
        }
    }
}
