using System.Collections.Generic;
using System.Linq;

namespace TDFactory
{
    public class AdminMenu
    {
        public AdminMenu()
        {
            SubMenu = new List<AdminMenu>();
        }

        public string Title { get; set; }
        
        public List<AdminMenu> SubMenu { get; set; }

        public static List<AdminMenu> FillMenu(List<string> tables, ConnectionInfo conInfo)
        {
            List<AdminMenu> adminMenu = new List<AdminMenu>();

            foreach (string Table in tables)
            {
                adminMenu.Add(new AdminMenu() { Title = Table });
            }

            foreach (string Table in tables)
            {
                Table table = new Table(Table, conInfo);

                if (table.FkcList.Count > 0)
                {
                    foreach (ForeignKeyChecker fkc in table.FkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                    {
                        foreach (ForeignKeyChecker fkc2 in table.FkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                        {
                            string PrimaryTableName = fkc.PrimaryTableName;
                            string ForeignTableName = fkc2.ForeignTableName;

                            bool submenu = false;

                            if (adminMenu.Where(a => a.Title == ForeignTableName).ToList().Count > 0)
                            {
                                if (adminMenu.Where(a => a.Title == ForeignTableName).FirstOrDefault().SubMenu.Count <= 0)
                                {
                                    adminMenu.Remove(adminMenu.Where(a => a.Title == ForeignTableName).FirstOrDefault());
                                }
                                else
                                {
                                    submenu = true;
                                }
                            }

                            if (adminMenu.Where(a => a.Title == PrimaryTableName).ToList().Count > 0)
                            {
                                if (submenu)
                                {
                                    adminMenu.Where(a => a.Title == PrimaryTableName).FirstOrDefault().SubMenu.Add(adminMenu.Where(a => a.Title == ForeignTableName).FirstOrDefault());
                                }
                                else
                                {
                                    adminMenu.Where(a => a.Title == PrimaryTableName).FirstOrDefault().SubMenu.Add(new AdminMenu() { Title = ForeignTableName });
                                }
                            }
                        }
                    }
                }
            }

            return adminMenu;
        }
    }
}
