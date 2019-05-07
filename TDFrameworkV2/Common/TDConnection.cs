// ==============================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v2.9.2.1
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 03.08.2016
// SPECIAL NOTES    : Thrashead
// ==============================

using System.Data.SqlClient;
using System.Configuration;
using System;

namespace TDFramework.Common
{
    public sealed class TDConnection
    {
        static TDConnection()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public static string ConnectionStringForOnce { get; set; }
        internal static SqlConnection SqlConnection
        {
            get
            {
                if (String.IsNullOrEmpty(ConnectionStringForOnce))
                {
                    return new SqlConnection(ConfigurationManager.ConnectionStrings["TDConnection"].ConnectionString);
                }
                else
                {
                    return new SqlConnection(ConnectionStringForOnce);
                }
            }
        }
    }
}
