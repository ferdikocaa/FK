using Core.Entities;
using Core.Entities.Concrete;

namespace Entities
{
    public class SuccessLog : EntityMain,IEntity
    {
        public int Id { get; set; }
        public string? MethodName { get; set; }
        public string? DeclaringType { get; set; }
        public string? TargetType { get; set; }
        public string? TargetTypeFullName { get; set; }
    }
}
