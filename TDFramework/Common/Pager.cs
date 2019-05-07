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
    public sealed class Pager
    {
        static Pager()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Pager(int pageCountOrFirstRecord, int itemsPerPageOrLastRecord, PagerTypes pagerType = PagerTypes.PAGE)
        {
            switch (pagerType)
            {
                case PagerTypes.PAGE:
                    FirstRecord = ((pageCountOrFirstRecord * itemsPerPageOrLastRecord) - (itemsPerPageOrLastRecord - 1)).ToString();
                    LastRecord = (pageCountOrFirstRecord * itemsPerPageOrLastRecord).ToString();
                    break;
                case PagerTypes.INTERVAL:
                    FirstRecord = pageCountOrFirstRecord.ToString();
                    LastRecord = itemsPerPageOrLastRecord.ToString();
                    break;
                default:
                    FirstRecord = ((pageCountOrFirstRecord * itemsPerPageOrLastRecord) - (itemsPerPageOrLastRecord - 1)).ToString();
                    LastRecord = (pageCountOrFirstRecord * itemsPerPageOrLastRecord).ToString();
                    break;
            }
        }

        internal string FirstRecord { get; set; }
        internal string LastRecord { get; set; }
    }

    public enum PagerTypes
    {
        PAGE,
        INTERVAL
    }
}
