using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TDFactory
{
    internal class Language
    {
        public string TR { get; set; }
        public string EN { get; set; }

        public static List<Language> List(ListBox listBox)
        {
            List<Language> returnList = new List<Language>();

            foreach (var item in listBox.Items)
            {
                returnList.Add(new Language()
                {
                    EN = item.ToString().Split('-')[0],
                    TR = item.ToString().Split('-')[1]
                });
            }

            return returnList;
        }
    }
}
