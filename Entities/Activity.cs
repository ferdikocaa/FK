using Core.Entities;
using Core.Entities.Concrete;

namespace Entities
{
    public class Activity : EntityMain,IEntity
    {
        public int Id { get; set; } 
        public int? ActivityTypeId { get; set; }
        public string? Description { get; set; }
        public ActivityType ActivityType { get; set; }
    }
}
