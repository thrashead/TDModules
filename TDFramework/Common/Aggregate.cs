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
using System.Collections.Generic;

namespace TDFramework.Common
{
    public sealed class Aggregate
    {
        static Aggregate()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Aggregate(dynamic groupColumns)
        {
            Column = null;
            GroupColumns = groupColumns;
            GroupBy = GroupColumns != null;
            Having = null;
        }

        public Aggregate(dynamic groupColumns, Having having)
        {
            Column = null;
            GroupColumns = groupColumns;
            GroupBy = GroupColumns != null;
            Having = new List<Having>() { having };
        }

        public Aggregate(dynamic groupColumns, List<Having> havingList)
        {
            Column = null;
            GroupColumns = groupColumns;
            GroupBy = GroupColumns != null;
            Having = havingList;
        }

        public Aggregate(dynamic column, Aggregates aggregate)
        {
            Agregate = aggregate;
            Column = column;
            GroupColumns = null;
            GroupBy = false;
            Having = null;
        }

        public Aggregate(dynamic column, Aggregates aggregate, dynamic groupColumns)
        {
            Agregate = aggregate;
            Column = column;
            GroupColumns = groupColumns;
            GroupBy = GroupColumns != null;
            Having = null;
        }

        public Aggregate(dynamic column, Aggregates aggregate, dynamic groupColumns, Having having)
        {
            Agregate = aggregate;
            Column = column;
            GroupColumns = groupColumns;
            GroupBy = GroupColumns != null;
            Having = new List<Having>() { having };
        }

        public Aggregate(dynamic column, Aggregates aggregate, dynamic groupColumns, List<Having> havingList)
        {
            Agregate = aggregate;
            Column = column;
            GroupColumns = groupColumns;
            GroupBy = GroupColumns != null;
            Having = havingList;
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
