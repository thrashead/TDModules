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

namespace TDFramework.Common
{
    public sealed class Error
    {
        static Error()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Error()
        {
            Message = null;
            Layer = null;
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
