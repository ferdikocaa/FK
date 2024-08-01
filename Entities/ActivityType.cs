using Core.Entities;
using Core.Entities.Concrete;

namespace Entities
{
    public class ActivityType : EntityMain,IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
