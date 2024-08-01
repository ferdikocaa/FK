using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
	public class EntityMain
	{
		public int? CreatedBy { get; set; }
		public int? UpdatedBy { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Include)]
		[DataType(DataType.DateTime)]
		public DateTime? CreatedDate { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Include)]
		[DataType(DataType.DateTime)]
		public DateTime? UpdatedDate { get; set; }

		[DefaultValue(false)]
		public bool? IsDeleted { get; set; }

		[ForeignKey(nameof(CreatedBy))]
		public virtual User CreatedUser { get; set; }

		[ForeignKey(nameof(UpdatedBy))]
		public virtual User UpdatedUser { get; set; }
	}
}
