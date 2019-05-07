using System;
using System.Collections.Generic;
using TDFramework.Common.TDModel;
using TDFramework.Common.Attributes;
using Models.TbKaynakModel;

namespace Models.TbKategoriModel
{
	[DBTable(Name = "TbKategori")]
    public class TbKategori : ITDModel
	{
		public TbKategori()
		{
			this.TbKaynakList = new List<Kaynak>();
		}

		[PKey]
		[IDColumn]
		public int KategoriID { get; set; }
		public string KategoriAdi { get; set; }
        public string KategoriKullanici { get; set; }
        [AggregateColumn]
        public dynamic AggColumn { get; set; }

		[RTable]
		public List<Kaynak> TbKaynakList { get; set; }
	}

	public enum TbKategoriColumns
	{
		KategoriID,
		KategoriAdi,
        KategoriKullanici
	}
}
