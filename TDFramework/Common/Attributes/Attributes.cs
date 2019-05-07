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

namespace TDFramework.Common.Attributes
{
    public class DBTableAttribute : Attribute
    {
        public string Name { get; set; }
    }

    public class RTableAttribute : Attribute
    {
    }

    public class AggregateColumnAttribute : Attribute
    {
    }

    public class PKeyAttribute : Attribute
    {
    }

    public class IDColumnAttribute : Attribute
    {
    }

    public class TableColumnAttribute : Attribute
    {
        public string Name { get; set; }
    }

    public class NotTableColumnAttribute : Attribute
    {
    }
}
