// ==============================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v3.2.2.3
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 07.05.2019
// SPECIAL NOTES    : Thrashead
// ==============================

using System;

namespace TDFramework.Common
{
    public sealed class Select
    {
        public dynamic OrderColumn { get; set; }
        public OrderBy? OrderBy { get; set; }
        public int? Top { get; set; }
        public bool Distinct { get; set; }
        public Aggregate Aggregate { get; set; }
        public Pager Pager { get; set; }

        static Select()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Select()
        {
            OrderColumn = null;
            OrderBy = null;
            Top = null;
            Distinct = false;
            Aggregate = null;
            Pager = null;
        }

        public Select(int top)
        {
            OrderColumn = null;
            OrderBy = null;
            Top = top;
            Distinct = false;
            Aggregate = null;
            Pager = null;
        }

        public Select(bool distinct)
        {
            OrderColumn = null;
            OrderBy = null;
            Top = null;
            Distinct = distinct;
            Aggregate = null;
            Pager = null;
        }

        public Select(Aggregate aggregate)
        {
            OrderColumn = null;
            OrderBy = null;
            Top = null;
            Distinct = false;
            Aggregate = aggregate;
            Pager = null;
        }

        public Select(Pager pager)
        {
            OrderColumn = null;
            OrderBy = null;
            Top = null;
            Distinct = false;
            Aggregate = null;
            Pager = pager;
        }

        public Select(dynamic orderColumn = null, OrderBy? orderBy = null, int? top = null, bool distinct = false, Aggregate aggregate = null, Pager pager = null)
        {
            OrderColumn = orderColumn;
            OrderBy = orderBy;
            Top = top;
            Distinct = distinct;
            Aggregate = aggregate;
            Pager = pager;
        }
    }

    public enum OrderBy
    {
        ASC,
        DESC
    }

    public enum SelectColumns
    {
        NONE
    }
}

