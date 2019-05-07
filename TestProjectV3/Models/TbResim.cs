using System;
using TDFramework.Common.Attributes;
using TDFramework.Common.TDModel;

namespace Models.TbResimModel
{
	[DBTable(Name = "TbResim")]
    public class TbResim : ITDModel
	{
		[PKey]
		[IDColumn]
		public int ResimID { get; set; }
		public string ResimYol { get; set; }
        [AggregateColumn]
        public dynamic AggColumn { get; set; }
    }

	public enum TbResimColumns
	{
		ResimID,
		ResimYol
	}
}
