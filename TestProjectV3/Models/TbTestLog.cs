using System;
using TDFramework.Common.TDModel;
using TDFramework.Common.Attributes;

namespace Models.TbTestLogModel
{
	[DBTable(Name = "TbTestLog")]
    public class TbTestLog : ITDModel
	{
		[PKey]
		[IDColumn]
		public int TestID { get; set; }
		public string TestGonderen { get; set; }
		public string TestTarih { get; set; }
		public string TestSaat { get; set; }
		public string TestSonuc { get; set; }
		public int TestSablonID { get; set; }
		public bool TestOkundu { get; set; }
		public string TestKod { get; set; }
		public string TestKullanici { get; set; }
        [AggregateColumn]
        public dynamic AggColumn { get; set; }
    }

	public enum TbTestLogColumns
	{
		TestID,
		TestGonderen,
		TestTarih,
		TestSaat,
		TestSonuc,
		TestSablonID,
		TestOkundu,
		TestKod,
		TestKullanici
	}
}
