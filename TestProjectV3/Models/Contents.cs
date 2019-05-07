using System;
using TDFramework.Common.Attributes;
using TDFramework.Common.TDModel;

namespace Models.ContentsModel
{
    public class Contents : ITDModel
	{
        [PKey]
        [IDColumn]
		public int ID { get; set; }
        public string ContentName { get; set; }
		public bool Active { get; set; }
        public int CategoryID { get; set; }

        [AggregateColumn]
        public dynamic AggColumn { get; set; }
	}

	public enum ContentsColumns
	{
		ID,
        ContentName,
		Active,
        CategoryID,
        Zaman
	}
}
