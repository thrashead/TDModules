// ================================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v2.2.0.2
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 22.02.2016
// SPECIAL NOTES    : Güzel Program
// ================================

using System;

namespace TDFramework.Common
{
    public sealed class SelectClause
    {
        public dynamic OrderColumn { get; set; }
        public OrderDirections? OrderDirection { get; set; }

        public SelectClause()
		{
			this.OrderColumn = null;
			this.OrderDirection = null;
        }

        public SelectClause(dynamic orderColumn = null, OrderDirections? orderDirection = null)
        {
            this.OrderColumn = orderColumn;
            this.OrderDirection = orderDirection;
        }
	}
}

