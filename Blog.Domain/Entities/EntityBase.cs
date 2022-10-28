namespace Blog.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid ID { get; set; }

        protected EntityBase()
        {

        }
    }
}
