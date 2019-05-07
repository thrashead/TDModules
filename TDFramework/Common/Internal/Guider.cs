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

namespace TDFramework.Common.Internal
{
    internal sealed class Guider
    {
        static Guider()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        internal static string GetGuid(int letterCount, bool showMinus = false)
        {
            string s = "";

            for (int i = 0; i < 10; i++)
            {
                if (showMinus)
                {
                    s = s + Guid.NewGuid().ToString() + "-";
                }
                else
                {
                    s = s + Guid.NewGuid().ToString().Replace("-", "");
                }
            }

            if (letterCount < 1)
            {
                return "";
            }
            else
            {
                return s.Remove(letterCount).Trim('-');
            }
        }
    }
}
