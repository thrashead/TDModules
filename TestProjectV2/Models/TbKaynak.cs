using System;
using System.Collections.Generic;
using TDFramework.Common.TDModel;
using TDFramework.Common.Attributes;
using Models.TbKriterModel;
using Models.TbTestLogModel;

namespace Models.TbKaynakModel
{
	[DBTable(Name = "TbKaynak")]
    public class Kaynak : ITDModel
	{
		public Kaynak()
		{
			this.TbKriterList = new List<TbKriter>();
			this.TbTestLogList = new List<TbTestLog>();
		}

		[PKey]
		[IDColumn]
		public int KaynakID { get; set; }

		public string KaynakAdi { get; set; }

        [DBColumn(Name = "KaynakMetin")]
        public string KaynakMetinBlaBla { get; set; }

		public string KaynakKonu { get; set; }

		public int KaynakKategoriID { get; set; }

        public string KaynakKullanici { get; set; }

        [NotDBColumn]
        public string Hede { get; set; }

        [AggregateColumn]
        public dynamic AggColumn { get; set; }

		[RTable]
		public List<TbKriter> TbKriterList { get; set; }

		[RTable]
		public List<TbTestLog> TbTestLogList { get; set; }
    }

	public enum KaynakColumns
	{
		KaynakID,
		KaynakAdi,
		KaynakMetinBlaBla,
		KaynakKonu,
		KaynakKategoriID,
		KaynakKullanici
	}
}
