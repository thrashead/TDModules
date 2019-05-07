using System;
using System.Collections.Generic;
using Models.TbBlokMailModel;
using Models.TbAdresDefterModel;
using TDFramework.Common.Attributes;
using TDFramework.Common.TDModel;

namespace Models.TbMailGrupModel
{
	[DBTable(Name = "TbMailGrup")]
    public class TbMailGrup : ITDModel
	{
		public TbMailGrup()
		{
			this.TbBlokMailList = new List<TbBlokMail>();
			this.TbAdresDefterList = new List<TbAdresDefter>();
		}

		[PKey]
		[IDColumn]
		public int MailGrupID { get; set; }
		public string MailGrupAd { get; set; }
        [AggregateColumn]
        public dynamic AggColumn { get; set; }

		[RTable]
		public List<TbBlokMail> TbBlokMailList { get; set; }
		[RTable]
		public List<TbAdresDefter> TbAdresDefterList { get; set; }
	}

	public enum TbMailGrupColumns
	{
		MailGrupID,
		MailGrupAd
	}
}
