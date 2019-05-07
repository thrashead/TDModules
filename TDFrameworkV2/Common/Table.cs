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
    public sealed class Table
    {
        static Table()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Table()
        {
            this.Select = new Select();
            this.WhereList = new List<Where>();
        }

        internal string Name { get; set; }

        public string Alias { get; set; }
        public dynamic RelatedColumn { get; set; }
        public dynamic SelectColumns { get; set; }
        public Select Select { get; set; }
        public List<Where> WhereList { get; set; }
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
