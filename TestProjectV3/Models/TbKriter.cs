using System;
using System.Collections.Generic;
using Models.TbLoglarModel;
using TDFramework.Common.Attributes;
using TDFramework.Common.TDModel;

namespace Models.TbKriterModel
{
	[DBTable(Name = "TbKriter")]
    public class TbKriter : ITDModel
	{
		public TbKriter()
		{
			this.TbLoglarList = new List<TbLoglar>();
		}

		[PKey]
		[IDColumn]
		public int KriterID { get; set; }
		public string KriterUlke { get; set; }
		public string KriterAcenta { get; set; }
		public string KriterDogumTarih { get; set; }
		public string KriterEvlilikTarih { get; set; }
		public string KriterMeslek { get; set; }
		public string KriterCinsiyet { get; set; }
		public string KriterMedeniHal { get; set; }
		public string KriterTarih { get; set; }
		public int? KriterKacGunOnce { get; set; }
		public string KriterMailListe { get; set; }
		public int KriterKaynakID { get; set; }
		public string KriterAdi { get; set; }
		public bool? KriterSonuc { get; set; }
		public bool KriterAktif { get; set; }
		public string KriterKullanici { get; set; }
        [AggregateColumn]
        public dynamic AggColumn { get; set; }

		[RTable]
		public List<TbLoglar> TbLoglarList { get; set; }
	}

	public enum TbKriterColumns
	{
		KriterID,
		KriterUlke,
		KriterAcenta,
		KriterDogumTarih,
		KriterEvlilikTarih,
		KriterMeslek,
		KriterCinsiyet,
		KriterMedeniHal,
		KriterTarih,
		KriterKacGunOnce,
		KriterMailListe,
		KriterKaynakID,
		KriterAdi,
		KriterSonuc,
		KriterAktif,
		KriterKullanici
	}
}
