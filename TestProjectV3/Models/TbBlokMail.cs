using System;
using TDFramework.Common.Attributes;
using TDFramework.Common.TDModel;

namespace Models.TbBlokMailModel
{
	[DBTable(Name = "TbBlokMail")]
    public class TbBlokMail : ITDModel
	{
		[PKey]
		[IDColumn]
		public int BlokMailID { get; set; }
		public string BlokMailAd { get; set; }
		public string BlokMailAdres { get; set; }
		public string BlokMailKod { get; set; }
		public string BlokMailTelefon { get; set; }
		public int? BlokMailGrupID { get; set; }
        [AggregateColumn]
        public dynamic AggColumn { get; set; }
    }

	public enum TbBlokMailColumns
	{
		BlokMailID,
		BlokMailAd,
		BlokMailAdres,
		BlokMailKod,
		BlokMailTelefon,
		BlokMailGrupID
	}
}
