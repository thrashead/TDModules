using System;
using TDFramework.Common.TDModel;
using TDFramework.Common.Attributes;

namespace Models.TbLoglarModel
{
	[DBTable(Name = "TbLoglar")]
    public class TbLoglar : ITDModel
	{
		[PKey]
		[IDColumn]
		public int LogID { get; set; }
		public string LogKullaniciMail { get; set; }
		public string LogKullaniciAdi { get; set; }
		public string LogTarih { get; set; }
		public string LogSaat { get; set; }
		public string LogSonuc { get; set; }
		public int LogPlanID { get; set; }
		public bool? LogOkundu { get; set; }
		public string LogKod { get; set; }
        [AggregateColumn]
        public dynamic AggColumn { get; set; }
    }

	public enum TbLoglarColumns
	{
		LogID,
		LogKullaniciMail,
		LogKullaniciAdi,
		LogTarih,
		LogSaat,
		LogSonuc,
		LogPlanID,
		LogOkundu,
		LogKod
	}
}
