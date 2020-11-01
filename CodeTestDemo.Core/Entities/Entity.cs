using CodeTestDemo.Core.Interfaces;

namespace CodeTestDemo.Core.Entities
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
