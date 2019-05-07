// ================================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v2.2.0.2
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 22.02.2016
// SPECIAL NOTES    : Güzel Program
// ================================

using System.Data.SqlClient;
using System.Configuration;
using System;

namespace TDFramework.Common
{
    public sealed class TDConnection
    {
        internal static SqlConnection SqlConnection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["TDConnection"].ConnectionString);
            }
        }
    }
}
