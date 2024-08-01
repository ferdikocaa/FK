using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
	public class OrderByEnums
	{
		[JsonConverter(typeof(StringEnumConverter))]
		[DefaultValue(CreatedDate)]
		public enum DefaultOrderBy
		{
			CreatedDate,
			UpdatedDate,
			Id,
		}

		[JsonConverter(typeof(StringEnumConverter))]
		[DefaultValue(CreatedDate)]
		public enum TireStockOrderBy
		{
			CreatedDate,
			Id,
			InstallationDate
		}
	}
}
