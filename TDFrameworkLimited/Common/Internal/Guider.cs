// ================================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v2.2.0.2
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 22.02.2016
// SPECIAL NOTES    : Güzel Program
// ================================

using System;

namespace TDFramework.Common
{
    internal sealed class Guider
    {
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
