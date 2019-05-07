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

namespace TDFramework.Common
{
    internal sealed class Guider
    {
        static Guider()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        internal static string GetGuid(bool showMinus = false)
        {
            if (showMinus == true)
            {
                return Guid.NewGuid().ToString();
            }
            else
            {
                return Guid.NewGuid().ToString().Replace("-", "");
            }
        }

        internal static string GetGuid(int lettercount, bool showMinus = false)
        {
            string s = "";

            for (int i = 0; i < 10; i++)
            {
                if (showMinus == true)
                {
                    s = s + Guid.NewGuid().ToString() + "-";
                }
                else
                {
                    s = s + Guid.NewGuid().ToString().Replace("-", "");
                }
            }

            if (lettercount < 1)
            {
                return "";
            }
            else
            {
                return s.Remove(lettercount).Trim('-');
            }
        }
    }
}
