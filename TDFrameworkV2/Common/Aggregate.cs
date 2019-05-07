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
using System.Collections.Generic;

namespace TDFramework.Common
{
    public sealed class Aggregate
    {
        static Aggregate()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Aggregate(dynamic groupColumns)
        {
            this.Column = null;
            this.GroupColumns = groupColumns;
            this.GroupBy = this.GroupColumns == null ? false : true;
            this.Having = null;
        }

        public Aggregate(dynamic groupColumns, Having having)
        {
            this.Column = null;
            this.GroupColumns = groupColumns;
            this.GroupBy = this.GroupColumns == null ? false : true;
            this.Having = new List<Having>() { having };
        }

        public Aggregate(dynamic groupColumns, List<Having> havingList)
        {
            this.Column = null;
            this.GroupColumns = groupColumns;
            this.GroupBy = this.GroupColumns == null ? false : true;
            this.Having = havingList;
        }

        public Aggregate(dynamic column, Aggregates aggregate)
        {
            this.Agregate = aggregate;
            this.Column = column;
            this.GroupColumns = null;
            this.GroupBy = false;
            this.Having = null;
        }

        public Aggregate(dynamic column, Aggregates aggregate, dynamic groupColumns)
        {
            this.Agregate = aggregate;
            this.Column = column;
            this.GroupColumns = groupColumns;
            this.GroupBy = this.GroupColumns == null ? false : true;
            this.Having = null;
        }

        public Aggregate(dynamic column, Aggregates aggregate, dynamic groupColumns, Having having)
        {
            this.Agregate = aggregate;
            this.Column = column;
            this.GroupColumns = groupColumns;
            this.GroupBy = this.GroupColumns == null ? false : true;
            this.Having = new List<Having>() { having };
        }

        public Aggregate(dynamic column, Aggregates aggregate, dynamic groupColumns, List<Having> havingList)
        {
            this.Agregate = aggregate;
            this.Column = column;
            this.GroupColumns = groupColumns;
            this.GroupBy = this.GroupColumns == null ? false : true;
            this.Having = havingList;
        }

        internal Aggregates Agregate { get; set; }
        internal List<Having> Having { get; set; }
        internal dynamic Column { get; set; }
        internal dynamic GroupColumns { get; set; }
        internal bool GroupBy { get; set; }
    }

    public enum Aggregates
    {
        COUNT,
        SUMMARY,
        MINIMUM,
        MAXIMUM,
        AVERAGE
    }
}
