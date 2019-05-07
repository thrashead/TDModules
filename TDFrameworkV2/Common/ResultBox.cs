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
using System.Data;
using System.Data.SqlClient;

namespace TDFramework.Common
{
    public sealed class ResultBox
    {
        public bool Result { get; internal set; }
        public dynamic Data { get; internal set; }
        public int DataCount { get; internal set; }
        public bool HasData
        {
            get
            {
                if (this.Result == true && this.DataCount > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string QueryString { get; internal set; }
        public List<SqlParameter> Parameters { get; internal set; }
        public string ErrorMessage { get; internal set; }
        public ErrorLayers? ErrorLayer { get; internal set; }

        static ResultBox()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public ResultBox()
        {
            this.Result = false;
            this.Data = null;
            this.DataCount = 0;
            this.QueryString = null;
            this.Parameters = new List<SqlParameter>();
            this.ErrorMessage = null;
            this.ErrorLayer = null;
        }
    }

    public enum ErrorLayers
    {
        COMMON,
        TDHELPER,
        DATAHELPER
    }
}
