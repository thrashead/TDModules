using System;
using TDFramework.Common.Attributes;
using TDFramework.Common.TDModel;

namespace Models.TbAdresDefterModel
{
	[DBTable(Name = "TbAdresDefter")]
    public class TbAdresDefter : ITDModel
	{
		[PKey]
		[IDColumn]
		public int AdresDefterID { get; set; }
		public string AdresDefterAd { get; set; }
		public string AdresDefterMail { get; set; }
		public string AdresDefterKod { get; set; }
		public string AdresDefterTelefon { get; set; }
		public int? AdresDefterGrupID { get; set; }
        [AggregateColumn]
        public dynamic AggColumn { get; set; }
    }

	public enum TbAdresDefterColumns
	{
		AdresDefterID,
		AdresDefterAd,
		AdresDefterMail,
		AdresDefterKod,
		AdresDefterTelefon,
		AdresDefterGrupID
	}
}
