// ==============================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v3.2.2.2
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 03.07.2018
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
            this.Pager = null;
        }

        public Select(int top)
        {
            this.OrderColumn = null;
            this.OrderBy = null;
            this.Top = top;
            this.Distinct = false;
            this.Aggregate = null;
            this.Pager = null;
        }

        public Select(bool distinct)
        {
            this.OrderColumn = null;
            this.OrderBy = null;
            this.Top = null;
            this.Distinct = distinct;
            this.Aggregate = null;
            this.Pager = null;
        }

        public Select(Aggregate aggregate)
        {
            this.OrderColumn = null;
            this.OrderBy = null;
            this.Top = null;
            this.Distinct = false;
            this.Aggregate = aggregate;
            this.Pager = null;
        }

        public Select(Pager pager)
        {
            this.OrderColumn = null;
            this.OrderBy = null;
            this.Top = null;
            this.Distinct = false;
            this.Aggregate = null;
            this.Pager = pager;
        }

        public Select(dynamic orderColumn = null, OrderBy? orderBy = null, int? top = null, bool distinct = false, Aggregate aggregate = null, Pager pager = null)
        {
            this.OrderColumn = orderColumn;
            this.OrderBy = orderBy;
            this.Top = top;
            this.Distinct = distinct;
            this.Aggregate = aggregate;
            this.Pager = pager;
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

