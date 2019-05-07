using System;
using TDFramework.Common.Attributes;
using TDFramework.Common.TDModel;

namespace Models.TbBilgiModel
{
	[DBTable(Name = "TbBilgi")]
    public class TbBilgi : ITDModel
	{
		[PKey]
		[IDColumn]
		public int BilgiID { get; set; }
		public string BilgiImza { get; set; }
        [AggregateColumn]
        public dynamic AggColumn { get; set; }
    }

	public enum TbBilgiColumns
	{
		BilgiID,
		BilgiImza
	}
}
