using System;
using System.Collections.Generic;
using Models.TbKriterModel;
using Models.TbTestLogModel;
using TDFramework.Common.Attributes;
using TDFramework.Common.TDModel;

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

        [TableColumn(Name = "KaynakMetin")]
        public string KaynakMetinBlaBla { get; set; }

		public string KaynakKonu { get; set; }

		public int KaynakKategoriID { get; set; }

        public string KaynakKullanici { get; set; }

        [NotTableColumn]
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
