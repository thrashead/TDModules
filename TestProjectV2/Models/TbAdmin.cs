using System;
using TDFramework.Common.TDModel;
using TDFramework.Common.Attributes;

namespace Models.TbAdminModel
{
	[DBTable(Name = "TbAdmin")]
	public class TbAdmin : ITDModel
	{
		[PKey]
		[IDColumn]
		public int AdminID { get; set; }
		public string AdminAd { get; set; }
		public string AdminSifre { get; set; }
		public int AdminHak { get; set; }
		public string AdminMail { get; set; }
        [AggregateColumn]
        public dynamic AggColumn { get; set; }
    }

	public enum TbAdminColumns
	{
		AdminID,
		AdminAd,
		AdminSifre,
		AdminHak,
		AdminMail
	}
}
