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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TDFramework.Common
{
    public sealed class Error
    {
        static Error()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Error()
        {
            this.Message = null;
            this.Layer = null;
        }

        public string Message { get; internal set; }
        public ErrorLayers? Layer { get; internal set; }
	}

    public enum ErrorLayers
	{
		COMMON,
		TABLE,
		DATA
	}
}
