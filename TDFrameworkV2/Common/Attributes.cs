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

    public class DBColumnAttribute : Attribute
    {
        public string Name { get; set; }
    }

    public class NotDBColumnAttribute : Attribute
    {
    }
}
