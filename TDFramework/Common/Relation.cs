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
using TDFramework.Common.TDModel;

namespace TDFramework.Common
{
    public sealed class Relation<T1, T2>
        where T1 : ITDModel
        where T2 : ITDModel
    {
        static Relation()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Relation(dynamic firstRelatedColumn, dynamic secondRelatedColumn, JoinTypes joinType = JoinTypes.INNER)
        {
            this.JoinType = joinType;
            this.FirstRelatedColumn = firstRelatedColumn;
            this.SecondRelatedColumn = secondRelatedColumn;
        }

        internal JoinTypes JoinType { get; set; }
        internal dynamic FirstRelatedColumn { get; set; }
        internal dynamic SecondRelatedColumn { get; set; }

        internal string QueryString(Table<T1> firstTable, Table<T2> secondTable, string knot = "On")
        {
            string queryString = "";

            if (this.JoinType != JoinTypes.CROSS)
            {
                queryString += knot + " " + firstTable.Alias + "." + this.FirstRelatedColumn + " = " + secondTable.Alias + "." + this.SecondRelatedColumn + " ";
            }

            return queryString;
        }
    }

    public enum JoinTypes
    {
        INNER,
        LEFT,
        RIGHT,
        FULL,
        CROSS
    }
}
