// ==============================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v2.9.2.1
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 03.08.2016
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
        public Pager PageInfo { get; set; }

        static Select()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Select()
        {
            this.OrderColumn = null;
            this.OrderBy = null;
            this.Top = null;
            this.Distinct = false;
            this.Aggregate = null;
            this.PageInfo = null;
        }

        public Select(int top)
        {
            this.OrderColumn = null;
            this.OrderBy = null;
            this.Top = top;
            this.Distinct = false;
            this.Aggregate = null;
            this.PageInfo = null;
        }

        public Select(bool distinct)
        {
            this.OrderColumn = null;
            this.OrderBy = null;
            this.Top = null;
            this.Distinct = distinct;
            this.Aggregate = null;
            this.PageInfo = null;
        }

        public Select(Aggregate aggregate)
        {
            this.OrderColumn = null;
            this.OrderBy = null;
            this.Top = null;
            this.Distinct = false;
            this.Aggregate = aggregate;
            this.PageInfo = null;
        }

        public Select(Pager pageInfo)
        {
            this.OrderColumn = null;
            this.OrderBy = null;
            this.Top = null;
            this.Distinct = false;
            this.Aggregate = null;
            this.PageInfo = pageInfo;
        }

        public Select(dynamic orderColumn = null, OrderBy? orderBy = null, int? top = null, bool distinct = false, Aggregate aggregate = null, Pager pageInfo = null)
        {
            this.OrderColumn = orderColumn;
            this.OrderBy = orderBy;
            this.Top = top;
            this.Distinct = distinct;
            this.Aggregate = aggregate;
            this.PageInfo = pageInfo;
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

