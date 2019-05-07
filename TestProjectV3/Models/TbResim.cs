using System;
using TDFramework.Common.TDModel;
using TDFramework.Common.Attributes;

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
