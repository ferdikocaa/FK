using Core.Entities;
using Core.Entities.Concrete;

namespace Entities
{
    public class ExceptionLog : EntityMain, IEntity
    {
        public int Id { get; set; }
        public string? Class { get; set; }
        public string? Method { get; set; }
        public string? Message { get; set; }
    }
}
