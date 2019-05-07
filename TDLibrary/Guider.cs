using System;

namespace TDLibrary
{
    public class Guider
    {
        public static string GetGuid(bool showMinus = false)
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

        public static string GetGuid(int lettercount, bool showMinus = false)
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
