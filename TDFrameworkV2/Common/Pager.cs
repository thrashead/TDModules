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
    public sealed class Pager
    {
        static Pager()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Pager(int pageCountOrFirstRecord, int itemsPerPageOrLastRecord, PagerTypes pagerType = PagerTypes.PAGE)
        {
            switch (pagerType)
            {
                case PagerTypes.PAGE:
                    this.FirstRecord = ((pageCountOrFirstRecord * itemsPerPageOrLastRecord) - (itemsPerPageOrLastRecord - 1)).ToString();
                    this.LastRecord = (pageCountOrFirstRecord * itemsPerPageOrLastRecord).ToString();
                    break;
                case PagerTypes.INTERVAL:
                    this.FirstRecord = pageCountOrFirstRecord.ToString();
                    this.LastRecord = itemsPerPageOrLastRecord.ToString();
                    break;
                default:
                    this.FirstRecord = ((pageCountOrFirstRecord * itemsPerPageOrLastRecord) - (itemsPerPageOrLastRecord - 1)).ToString();
                    this.LastRecord = (pageCountOrFirstRecord * itemsPerPageOrLastRecord).ToString();
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
