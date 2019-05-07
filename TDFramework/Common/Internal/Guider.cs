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
