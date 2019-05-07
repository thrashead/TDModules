using System;
using Models.ContentsModel;
using System.Collections.Generic;
using TDFramework.Common.Attributes;
using TDFramework.Common.TDModel;

namespace Models.CategoriesModel
{
    public class Categories : ITDModel
	{
        public Categories()
        {
            this.ContentsList = new List<Contents>();
        }

		[PKey]
		[IDColumn]
		public int ID { get; set; }
		public string CategoryName { get; set; }
		public bool? Active { get; set; }

		[AggregateColumn]
        public dynamic AggColumn { get; set; }

        [RTable]
        public List<Contents> ContentsList { get; set; } 
	}

    public enum CategoriesColumns
	{
		ID,
		CategoryName,
		Active
	}
}
