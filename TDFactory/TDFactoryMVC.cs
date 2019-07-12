﻿using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TDFactory.Helper;

namespace TDFactory
{
    public partial class TDFactory : Form
    {
        #region MVC

        void CreateMVC()
        {
            CreateDirectories();

            if (chkMVCHepsi.Checked == true)
            {
                CreateModelLayer();
                CreateViewLayer();
                CreateControllerLayer();
                CreateWebConfig();
                CreateWcfServis();
                CreateStilScript();

                CreateStoredProcedure();
            }
            else
            {
                if (chkMVCModel.Checked == true)
                {
                    CreateModelLayer();
                }

                if (chkMVCView.Checked == true)
                {
                    CreateViewLayer();
                }

                if (chkMVCController.Checked == true)
                {
                    CreateControllerLayer();
                }

                if (chkMVCWebConfig.Checked == true)
                {
                    CreateWebConfig();
                }

                if (chkMVCWcfServis.Checked == true)
                {
                    CreateWcfServis();
                }

                if (chkMVCStoredProc.Checked == true)
                {
                    CreateStoredProcedure();
                }

                if (chkMVCStilScript.Checked == true)
                {
                    CreateStilScript();
                }
            }

            CreateRegistrar();
        }

        void CreateDirectories(string _tableName = null)
        {
            projectFolder = projectName;

            if (!Directory.Exists(PathAddress + "\\" + projectFolder))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder);
            }

            if (_tableName == null)
            {
                if (chkMVCHepsi.Checked)
                {
                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\App_Start"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\App_Start");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Models"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Models");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Controllers"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Controllers");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Shared"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Shared");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Home"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Home");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Models"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Models");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Home"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Home");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Controllers"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Controllers");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\css"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\css");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\img"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\img");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\js"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\js");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Servis"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Servis");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Servis\\Condition"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Servis\\Condition");
                    }
                }
                else
                {
                    if (chkMVCModel.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Models"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Models");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Models"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Models");
                        }
                    }

                    if (chkMVCView.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\App_Start"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\App_Start");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Shared"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Shared");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Home"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Home");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Home"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Home");
                        }
                    }

                    if (chkMVCController.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\App_Start"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\App_Start");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Controllers"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Controllers");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Controllers"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Controllers");
                        }
                    }

                    if (chkMVCStilScript.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\css"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\css");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\img"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\img");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\js"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\js");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js");
                        }
                    }

                    if (chkMVCWcfServis.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Servis"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Servis");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Servis\\Condition"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Servis\\Condition");
                        }
                    }
                }
            }
            else
            {
                if (chkMVCHepsi.Checked || chkMVCWebConfig.Checked || chkMVCView.Checked)
                {
                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\" + _tableName))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\" + _tableName);
                    }
                }
            }
        }

        void CreateRegistrar()
        {
            if (Directory.Exists(PathAddress + "\\" + projectFolder + "\\App_Start"))
            {
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\App_Start\\RouteConfig.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using System.Linq;");
                        yaz.WriteLine("using System.Web;");
                        yaz.WriteLine("using System.Web.Mvc;");
                        yaz.WriteLine("using System.Web.Routing;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + "");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class RouteConfig");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\tpublic static void RegisterRoutes(RouteCollection routes)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\troutes.IgnoreRoute(\"{resource}.axd/{*pathInfo}\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\troutes.MapRoute(");
                        yaz.WriteLine("\t\t\t\tname: \"Default\",");
                        yaz.WriteLine("\t\t\t\turl: \"{controller}/{action}/{id}\",");
                        yaz.WriteLine("\t\t\t\tdefaults: new { controller = \"Home\", action = \"Index\", id = UrlParameter.Optional },");
                        yaz.WriteLine("\t\t\t\tnamespaces: new[] { \"" + projectName + ".Controllers\" }");
                        yaz.WriteLine("\t\t\t);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }

            if (Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin"))
            {
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\AdminAreaRegistration.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System.Web.Mvc;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + ".Areas.Admin");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class AdminAreaRegistration : AreaRegistration");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\tpublic override string AreaName");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tget");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\treturn \"Admin\";");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tpublic override void RegisterArea(AreaRegistrationContext context)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tcontext.MapRoute(");
                        yaz.WriteLine("\t\t\t\t\"Admin_default\",");
                        yaz.WriteLine("\t\t\t\t\"Admin/{controller}/{action}/{id}\",");
                        yaz.WriteLine("\t\t\t\tnew { controller = \"Home\", action = \"Index\", id = UrlParameter.Optional },");
                        yaz.WriteLine("\t\t\t\tnamespaces: new[] { \"" + projectName + ".Areas.Admin.Controllers\" }");
                        yaz.WriteLine("\t\t\t);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }
            }
        }

        void CreateModelLayer()
        {
            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));
                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                CreateDirectories(Table);

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Models\\" + Table + ".cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        List<TableColumnNames> columnNames = tableColumnNames.Where(a => a.TableName == Table).ToList();

                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using System.Web.Mvc;");

                        List<TableColumnNames> tempcolumns = columnNames.Where(a => a.TypeName != null).ToList();

                        if (tempcolumns.Count > 0)
                        {
                            List<TableColumnNames> subTempcolumns = tempcolumns.Where(a => a.TypeName.Name == "String").ToList();

                            if (subTempcolumns.Count > 0)
                            {
                                List<TableColumnNames> lastColumns = subTempcolumns.Where(a => String.IsNullOrEmpty(a.CharLength)).ToList();

                                if (lastColumns.Count > 0)
                                {
                                    yaz.WriteLine("using System.ComponentModel.DataAnnotations;");
                                }
                            }
                        }
                        yaz.WriteLine("");

                        yaz.WriteLine("namespace Models");
                        yaz.WriteLine("{");

                        yaz.WriteLine("\tpublic class " + Table);
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\tpublic " + Table + "()");
                        yaz.WriteLine("\t\t{");

                        if (chkRTables.Checked == true)
                        {
                            if (fkcList.Count > 0)
                            {

                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    yaz.WriteLine("\t\t\t" + ForeignTableName + "List = new List<" + ForeignTableName + ">();");

                                }
                            }

                            if (fkcListForeign.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    yaz.WriteLine("\t\t\t" + PrimaryTableName + "List = new List<SelectListItem>();");
                                }
                            }
                        }
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        int counter = columnNames.Count;
                        int i = 1;

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                if (column.TypeName.Name == "String")
                                {
                                    if (String.IsNullOrEmpty(column.CharLength))
                                    {
                                        yaz.WriteLine("\t\t[DataType(DataType.MultilineText)]");
                                    }
                                }

                                if (column.IsNullable)
                                {
                                    switch (column.TypeName.Name)
                                    {
                                        case "Int16": yaz.WriteLine("\t\tpublic int? " + column.ColumnName + " { get; set; }"); break;
                                        case "Int32": yaz.WriteLine("\t\tpublic int? " + column.ColumnName + " { get; set; }"); break;
                                        case "Int64": yaz.WriteLine("\t\tpublic Int64? " + column.ColumnName + " { get; set; }"); break;
                                        case "Decimal": yaz.WriteLine("\t\tpublic decimal? " + column.ColumnName + " { get; set; }"); break;
                                        case "Double": yaz.WriteLine("\t\tpublic double? " + column.ColumnName + " { get; set; }"); break;
                                        case "Char": yaz.WriteLine("\t\tpublic char " + column.ColumnName + " { get; set; }"); break;
                                        case "Chars": yaz.WriteLine("\t\tpublic char[] " + column.ColumnName + " { get; set; }"); break;
                                        case "String": yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                        case "Byte": yaz.WriteLine("\t\tpublic byte " + column.ColumnName + " { get; set; }"); break;
                                        case "Bytes": yaz.WriteLine("\t\tpublic byte[] " + column.ColumnName + " { get; set; }"); break;
                                        case "Boolean": yaz.WriteLine("\t\tpublic bool? " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTime": yaz.WriteLine("\t\tpublic DateTime? " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTimeOffset": yaz.WriteLine("\t\tpublic DateTimeOffset? " + column.ColumnName + " { get; set; }"); break;
                                        case "TimeSpan": yaz.WriteLine("\t\tpublic TimeSpan? " + column.ColumnName + " { get; set; }"); break;
                                        case "Single": yaz.WriteLine("\t\tpublic Single? " + column.ColumnName + " { get; set; }"); break;
                                        case "Object": yaz.WriteLine("\t\tpublic object " + column.ColumnName + " { get; set; }"); break;
                                        case "Guid": yaz.WriteLine("\t\tpublic Guid? " + column.ColumnName + " { get; set; }"); break;
                                        default: yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                    }
                                }
                                else
                                {
                                    switch (column.TypeName.Name)
                                    {
                                        case "Int16": yaz.WriteLine("\t\tpublic int " + column.ColumnName + " { get; set; }"); break;
                                        case "Int32": yaz.WriteLine("\t\tpublic int " + column.ColumnName + " { get; set; }"); break;
                                        case "Int64": yaz.WriteLine("\t\tpublic Int64 " + column.ColumnName + " { get; set; }"); break;
                                        case "Decimal": yaz.WriteLine("\t\tpublic decimal " + column.ColumnName + " { get; set; }"); break;
                                        case "Double": yaz.WriteLine("\t\tpublic double " + column.ColumnName + " { get; set; }"); break;
                                        case "Char": yaz.WriteLine("\t\tpublic char " + column.ColumnName + " { get; set; }"); break;
                                        case "Chars": yaz.WriteLine("\t\tpublic char[] " + column.ColumnName + " { get; set; }"); break;
                                        case "String": yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                        case "Byte": yaz.WriteLine("\t\tpublic byte " + column.ColumnName + " { get; set; }"); break;
                                        case "Bytes": yaz.WriteLine("\t\tpublic byte[] " + column.ColumnName + " { get; set; }"); break;
                                        case "Boolean": yaz.WriteLine("\t\tpublic bool " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTime": yaz.WriteLine("\t\tpublic DateTime " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTimeOffset": yaz.WriteLine("\t\tpublic DateTimeOffset " + column.ColumnName + " { get; set; }"); break;
                                        case "TimeSpan": yaz.WriteLine("\t\tpublic TimeSpan " + column.ColumnName + " { get; set; }"); break;
                                        case "Single": yaz.WriteLine("\t\tpublic Single " + column.ColumnName + " { get; set; }"); break;
                                        case "Object": yaz.WriteLine("\t\tpublic object " + column.ColumnName + " { get; set; }"); break;
                                        case "Guid": yaz.WriteLine("\t\tpublic Guid " + column.ColumnName + " { get; set; }"); break;
                                        default: yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                    }
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }

                            i++;
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tpublic string Mesaj { get; set; }");


                        if (chkRTables.Checked == true)
                        {
                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("");

                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    yaz.WriteLine("\t\tpublic List<" + ForeignTableName + "> " + ForeignTableName + "List { get; set; }");
                                }
                            }

                            if (fkcListForeign.Count > 0)
                            {
                                yaz.WriteLine("");

                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    yaz.WriteLine("\t\tpublic List<SelectListItem> " + PrimaryTableName + "List { get; set; }");
                                }
                            }
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }
            }
        }

        void CreateLayout()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\_Layout.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("<!DOCTYPE html>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<html>");
                    yaz.WriteLine("<head>");
                    yaz.WriteLine("\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, user-scalable=1\" />");
                    yaz.WriteLine("\t<meta name=\"apple-mobile-web-app-capable\" content=\"yes\" />");
                    yaz.WriteLine("\t<meta name=\"apple-mobile-web-app-status-bar-style\" content=\"black\" />");
                    yaz.WriteLine("\t<title>@ViewBag.Title</title>");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Views/Shared/Controls/_Scripts.cshtml\"); }");
                    yaz.WriteLine("</head>");
                    yaz.WriteLine("<body>");
                    yaz.WriteLine("\t<div>");
                    yaz.WriteLine("\t\t@RenderBody()");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</body>");
                    yaz.WriteLine("</html>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\_Layout.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("<!DOCTYPE html>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<html>");
                    yaz.WriteLine("<head>");
                    yaz.WriteLine("\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, user-scalable=1\" />");
                    yaz.WriteLine("\t<meta name=\"apple-mobile-web-app-capable\" content=\"yes\" />");
                    yaz.WriteLine("\t<meta name=\"apple-mobile-web-app-status-bar-style\" content=\"black\" />");
                    yaz.WriteLine("\t<title>@ViewBag.Title</title>");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Areas/Admin/Views/Shared/Controls/_Scripts.cshtml\"); }");
                    yaz.WriteLine("</head>");
                    yaz.WriteLine("<body>");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Areas/Admin/Views/Shared/Controls/_Menu.cshtml\"); }");
                    yaz.WriteLine("\t<div class=\"clear\"></div>");
                    yaz.WriteLine("\t<div class=\"content\">");
                    yaz.WriteLine("\t\t@RenderBody()");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</body>");
                    yaz.WriteLine("</html>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls\\_Scripts.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("<link rel=\"shortcut icon\" href=\"@AppMgr.MainPath/favicon.ico\" type=\"image/x-icon\">");
                    yaz.WriteLine("<link rel=\"icon\" href=\"@AppMgr.MainPath/favicon.ico\" type=\"image/x-icon\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/jquery/jquery.min.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/jquery/json2.js\"></script>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"@AppMgr.StylePath/main.css\" />");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/pathscript.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/main.js\"></script>");

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls\\_Scriptler.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("<link rel=\"shortcut icon\" href=\"@AppMgr.MainPath/favicon.ico\" type=\"image/x-icon\">");
                    yaz.WriteLine("<link rel=\"icon\" href=\"@AppMgr.MainPath/favicon.ico\" type=\"image/x-icon\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/jquery/jquery.min.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/jquery/json2.js\"></script>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"@AppMgr.AdminStylePath/main.css\" />");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/pathscript.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.AdminScriptPath/main.js\"></script>");

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls\\_Menu.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");

                    yaz.WriteLine("<ul class=\"topmenu\">");
                    yaz.WriteLine("\t<li><a href=\"@AppMgr.AdminPath\">Ana Sayfa</a></li>");

                    foreach (string table in selectedTables)
                    {
                        yaz.WriteLine("\t<li><a href=\"@AppMgr.AdminPath/" + table + "\">" + table + "</a></li>");
                    }

                    yaz.WriteLine("\t<li><a class=\"website\" target=\"_blank\" href=\"@AppMgr.MainPath\">Web Sitesine Git</a></li>");
                    yaz.WriteLine("</ul>");

                    yaz.Close();
                }
            }
        }

        void CreateViewLayer()
        {
            int i = 0;

            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));
                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                CreateDirectories(Table);

                if (i <= 0)
                {
                    CreateLayout();
                    CreateHomePage();

                    i++;
                }

                //Index
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\" + Table + "\\Index.cshtml", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        int tablewidth = 120;
                        int colcount = tableColumnNames.Where(a => a.TableName == Table).ToList().Count;

                        switch (colcount)
                        {
                            case 1:
                                tablewidth = tablewidth + 50;
                                break;
                            case 2:
                                tablewidth = tablewidth + 50 + 230;
                                break;
                            case 3:
                                tablewidth = tablewidth + 50 + 230 + 240;
                                break;
                            case 4:
                                tablewidth = tablewidth + 50 + 230 + 240 + 138;
                                break;
                            default:
                                tablewidth = tablewidth + 50 + 230 + 240 + 138;
                                tablewidth = tablewidth + ((colcount - 4) * 138);
                                break;
                        }

                        yaz.WriteLine("@using TDLibrary");
                        yaz.WriteLine("");
                        yaz.WriteLine("@{");
                        yaz.WriteLine("\tViewBag.Title = \"" + Table + "\";");
                        yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_Layout.cshtml\";");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("<div id=\"tablo" + Table + "\">");
                        yaz.WriteLine("</div>");
                        yaz.WriteLine("");
                        yaz.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"@AppMgr.ScriptPath/tdTable/tdTable.css\" />");
                        yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/tdTable/tdTable.js\"></script>");
                        yaz.WriteLine("");
                        yaz.WriteLine("<script>");
                        yaz.WriteLine("\t$(\"#tablo" + Table + "\").tdTable(");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\trootfolder: \"@AppMgr.ScriptPath/tdTable\",");
                        yaz.WriteLine("\t\ttitle: \"" + Table + "\",");
                        yaz.WriteLine("\t\tlistmethod: \"@AppMgr.AdminPath/" + Table + "/" + Table + "Index\",");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\tdeletemethod: \"@AppMgr.AdminPath/" + Table + "/Sil\",");
                        }

                        yaz.WriteLine("\t\titemperpage: 10,");
                        yaz.WriteLine("\t\ttablewidth: " + tablewidth + ",");
                        yaz.WriteLine("\t\tenablesorting: true,");
                        yaz.WriteLine("\t\tshowfooter: true,");
                        yaz.WriteLine("\t\tshowcommands: true,");
                        yaz.WriteLine("\t\ttheme: \"blue\",");
                        yaz.WriteLine("\t\tconditions: {");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\tOrderBy: \"" + id + "\",");
                        }

                        yaz.WriteLine("\t\t\tFields: {");

                        int cssSayac = 1;
                        string cssClass = "first";

                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                        {
                            switch (cssSayac)
                            {
                                case 1:
                                    cssClass = "first";
                                    break;
                                case 2:
                                    cssClass = "second";
                                    break;
                                case 3:
                                    cssClass = "third";
                                    break;
                                case 4:
                                    cssClass = "fourth";
                                    break;
                                default:
                                    cssClass = "fourth";
                                    break;
                            }
                            List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                            if (foreLst.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t\t" + foreLst.FirstOrDefault().PrimaryTableName + "Adi: {");
                                yaz.WriteLine("\t\t\t\t\tTitle: \"" + foreLst.FirstOrDefault().PrimaryTableName + "Adi\",");
                            }
                            else
                            {
                                yaz.WriteLine("\t\t\t\t" + column.ColumnName + ": {");
                                yaz.WriteLine("\t\t\t\t\tTitle: \"" + column.ColumnName + "\",");
                            }
                            yaz.WriteLine("\t\t\t\t\tCssClass: \"" + cssClass + "\"");

                            yaz.WriteLine("\t\t\t\t},");

                            cssSayac++;
                        }

                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t},");
                        yaz.WriteLine("\t\tcommands: {");
                        yaz.WriteLine("\t\t\tAddText: \"Ekle\",");
                        yaz.WriteLine("\t\t\tAddLink: \"" + Table + "/Ekle\",");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\tDetailLink: \"" + Table + "/Detay/{" + id + "}\",");
                            yaz.WriteLine("\t\t\tUpdateLink: \"" + Table + "/Duzenle/{" + id + "}\",");
                            yaz.WriteLine("\t\t\tDeleteLink: \"javascript:;\"");
                        }
                        else
                        {
                            yaz.WriteLine("\t\t\tShowDetailLink: false,");
                            yaz.WriteLine("\t\t\tShowUpdateLink: false,");
                            yaz.WriteLine("\t\t\tShowDeleteLink: false");
                        }

                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("</script>");

                        yaz.Close();
                    }
                }

                //Ekle
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\" + Table + "\\Ekle.cshtml", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("@model " + projectName + "." + Table);
                        yaz.WriteLine("");
                        yaz.WriteLine("@{");
                        yaz.WriteLine("\tViewBag.Title = \"" + Table + " Ekle\";");
                        yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_Layout.cshtml\";");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("@using (Html.BeginForm()) {");
                        yaz.WriteLine("\t@Html.ValidationSummary(true)");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t<fieldset>");
                        yaz.WriteLine("\t\t<legend>" + Table + " Ekle</legend>");
                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                        {
                            if (!identityColumns.Contains(column.ColumnName))
                            {
                                List<ForeignKeyChecker> frchkLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                if (frchkLst.Count > 0)
                                {
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t" + frchkLst.FirstOrDefault().PrimaryTableName + "Adi");
                                    yaz.WriteLine("\t\t</div>");
                                    yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("\t\t<div class=\"editor-field\">");
                                    yaz.WriteLine("\t\t\t@Html.DropDownListFor(model => model." + column.ColumnName + ", (List<SelectListItem>)Model." + frchkLst.FirstOrDefault().PrimaryTableName + "List)");
                                    yaz.WriteLine("\t\t</div>");
                                }
                                else
                                {
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t@Html.LabelFor(model => model." + column.ColumnName + ")");
                                    yaz.WriteLine("\t\t</div>");
                                    yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("\t\t<div class=\"editor-field\">");
                                    yaz.WriteLine("\t\t\t@Html.EditorFor(model => model." + column.ColumnName + ")");
                                    yaz.WriteLine("\t\t\t@Html.ValidationMessageFor(model => model." + column.ColumnName + ")");
                                    yaz.WriteLine("\t\t</div>");
                                }
                                yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t<p>");
                        yaz.WriteLine("\t\t\t<input type=\"submit\" value=\"Kaydet\" />");
                        yaz.WriteLine("\t\t</p>");
                        yaz.WriteLine("\t</fieldset>");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("<div class=\"pagelinks\">");
                        yaz.WriteLine("\t@Html.ActionLink(\"Listeye Git\", \"Index\", \"" + Table + "\")");
                        yaz.WriteLine("</div>");

                        yaz.Close();
                    }
                }

                if (identityColumns.Count > 0)
                {
                    //Duzenle
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\" + Table + "\\Duzenle.cshtml", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                        {
                            yaz.WriteLine("@model " + projectName + "." + Table);
                            yaz.WriteLine("");
                            yaz.WriteLine("@{");
                            yaz.WriteLine("\tViewBag.Title = \"" + Table + " Düzenle\";");
                            yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_Layout.cshtml\";");
                            yaz.WriteLine("}");
                            yaz.WriteLine("");

                            yaz.WriteLine("@using (Html.BeginForm()) {");
                            yaz.WriteLine("\t@Html.ValidationSummary(true)");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t<fieldset>");
                            yaz.WriteLine("\t\t<legend>" + Table + " Düzenle</legend>");
                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                if (identityColumns.Contains(column.ColumnName))
                                {
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t@Html.HiddenFor(model => model." + column.ColumnName + ")");
                                    yaz.WriteLine("\t\t</div>");
                                }
                                else
                                {
                                    List<ForeignKeyChecker> frchkLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    if (frchkLst.Count > 0)
                                    {
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t<div class=\"editor-label\">");
                                        yaz.WriteLine("\t\t\t" + frchkLst.FirstOrDefault().PrimaryTableName + "Adi");
                                        yaz.WriteLine("\t\t</div>");
                                        yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t<div class=\"editor-field\">");
                                        yaz.WriteLine("\t\t\t@Html.DropDownListFor(model => model." + column.ColumnName + ", (List<SelectListItem>)Model." + frchkLst.FirstOrDefault().PrimaryTableName + "List)");
                                        yaz.WriteLine("\t\t</div>");
                                    }
                                    else
                                    {
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t<div class=\"editor-label\">");
                                        yaz.WriteLine("\t\t\t@Html.LabelFor(model => model." + column.ColumnName + ")");
                                        yaz.WriteLine("\t\t</div>");
                                        yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t<div class=\"editor-field\">");
                                        yaz.WriteLine("\t\t\t@Html.EditorFor(model => model." + column.ColumnName + ")");
                                        yaz.WriteLine("\t\t\t@Html.ValidationMessageFor(model => model." + column.ColumnName + ")");
                                        yaz.WriteLine("\t\t</div>");
                                    }
                                    yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                                }
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t<p>");
                            yaz.WriteLine("\t\t\t<input type=\"submit\" value=\"Kaydet\" />");
                            yaz.WriteLine("\t\t</p>");
                            yaz.WriteLine("\t</fieldset>");
                            yaz.WriteLine("}");
                            yaz.WriteLine("");
                            yaz.WriteLine("<div class=\"pagelinks\">");
                            yaz.WriteLine("\t@Html.ActionLink(\"Listeye Git\", \"Index\", \"" + Table + "\")");
                            yaz.WriteLine("</div>");

                            yaz.Close();
                        }
                    }
                }
            }
        }

        void CreateControllerLayer()
        {
            int i = 0;

            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                identityColumns = identityColumns.IdentityCheck(lstSeciliKolonlar);

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                CreateDirectories(Table);

                if (i <= 0)
                {
                    CreateHomeController();

                    i++;
                }

                StreamWriter yaz = File.CreateText(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Controllers\\" + Table + "Controller.cs");

                yaz.WriteLine("using System;");
                yaz.WriteLine("using System.Linq;");
                yaz.WriteLine("using System.Web.Mvc;");
                yaz.WriteLine("using System.Collections.Generic;");
                yaz.WriteLine("using TDLibrary;");
                yaz.WriteLine("using Models;");

                yaz.WriteLine("");
                yaz.WriteLine("namespace " + projectName + ".Areas.Admin.Controllers");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic class " + Table + "Controller : Controller");
                yaz.WriteLine("\t{");
                yaz.WriteLine("\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                yaz.WriteLine("");

                // Index
                string searchText = GetColumnText(tableColumnNames.Where(a => a.TableName == Table).ToList());

                yaz.WriteLine("\t\tpublic ViewResult Index(string conditions)");
                yaz.WriteLine("\t\t{");
                yaz.WriteLine("\t\t\treturn View();");
                yaz.WriteLine("\t\t}");
                yaz.WriteLine("");

                if (identityColumns.Count > 0)
                {
                    string columntype = tableColumnNames.Where(a => a.ColumnName == id && a.TableName == Table).FirstOrDefault().TypeName.Name.ToString();

                    // Ekle
                    yaz.WriteLine("\t\tpublic ActionResult Ekle()");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\t" + Table + " table = new " + Table + "();");
                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;

                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                    yaz.WriteLine("\t\tpublic ActionResult Ekle(" + Table + " table)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tentity." + Table + ".Add(table);");
                    yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;

                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(\"Ekle\", table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    //Duzenle
                    yaz.WriteLine("\t\t[HttpGet]");
                    yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + columntype.ReturnCSharpType() + " id)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(id);");
                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t\telse");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                    yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + Table + " table)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\t" + Table + " _table = entity." + Table + ".Find(table." + id + ");");
                    yaz.WriteLine("");

                    foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                    {
                        yaz.WriteLine("\t\t\t\t_table." + column.ColumnName + " = table." + column.ColumnName + ";");
                    }
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                    yaz.WriteLine("\t\t\t}");

                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t\telse");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(\"Duzenle\", table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    //Sil
                    yaz.WriteLine("\t\tpublic JsonResult Sil(" + columntype.ReturnCSharpType() + " conditions)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(conditions);");
                    yaz.WriteLine("\t\t\tentity." + Table + ".Remove(table);");
                    yaz.WriteLine("\t\t\tentity.SaveChanges();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\treturn Json(true);");
                    yaz.WriteLine("\t\t}");
                }

                yaz.WriteLine("\t}");
                yaz.WriteLine("}");

                yaz.Close();
            }
        }

        void CreateHomePage()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Views\\Home\\Index.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("@{");
                    yaz.WriteLine("\tViewBag.Title = \"" + projectName + " Ana Sayfa\";");
                    yaz.WriteLine("\tLayout = \"~/Views/Shared/_Layout.cshtml\";");
                    yaz.WriteLine("}");
                    yaz.WriteLine("");
                    yaz.WriteLine("<h2>" + projectName + " Ana Sayfa</h2>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Home\\Index.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("@{");
                    yaz.WriteLine("\tViewBag.Title = \"" + projectName + " Admin Ana Sayfa\";");
                    yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_Layout.cshtml\";");
                    yaz.WriteLine("}");
                    yaz.WriteLine("");
                    yaz.WriteLine("<h2>" + projectName + " Admin Ana Sayfa</h2>");
                    yaz.WriteLine("");

                    yaz.WriteLine("<ul class=\"mainmenu\">");

                    foreach (string table in selectedTables)
                    {
                        yaz.WriteLine("\t<li><a href=\"@AppMgr.AdminPath/" + table + "\">" + table + "</a></li>");
                    }

                    yaz.WriteLine("</ul>");
                    yaz.Close();
                }
            }
        }

        void CreateHomeController()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Controllers\\HomeController.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using System;");
                    yaz.WriteLine("using System.Collections.Generic;");
                    yaz.WriteLine("using System.Linq;");
                    yaz.WriteLine("using System.Web;");
                    yaz.WriteLine("using System.Web.Mvc;");
                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName + ".Controllers");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic class HomeController : Controller");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpublic ActionResult Index()");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\treturn View();");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Controllers\\HomeController.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using System;");
                    yaz.WriteLine("using System.Collections.Generic;");
                    yaz.WriteLine("using System.Linq;");
                    yaz.WriteLine("using System.Web;");
                    yaz.WriteLine("using System.Web.Mvc;");
                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName + ".Areas.Admin.Controllers");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic class HomeController : Controller");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpublic ActionResult Index()");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\treturn View();");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }
        }

        void CreateWcfServis()
        {
            CreateDirectories();

            //SELECT
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Servis\\Condition\\SELECT.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using System.Runtime.Serialization;");
                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName + ".Servis.Condition");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\t[DataContract]");
                    yaz.WriteLine("\tpublic class SELECT");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpublic SELECT()");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\ttop = 0;");
                    yaz.WriteLine("\t\t\torderBy = \"Asc\";");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[DataMember]");
                    yaz.WriteLine("\t\tpublic int? top { get; set; }");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[DataMember]");
                    yaz.WriteLine("\t\tpublic string orderColumn { get; set; }");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[DataMember]");
                    yaz.WriteLine("\t\tpublic string orderBy { get; set; }");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");

                    yaz.Close();
                }
            }

            //WHERE
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Servis\\Condition\\WHERE.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using System.Collections.Generic;");
                    yaz.WriteLine("using System.Runtime.Serialization;");
                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName + ".Servis.Condition");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\t[DataContract]");
                    yaz.WriteLine("\tpublic class WHERE");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpublic WHERE()");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\toperators = \"Equal\";");
                    yaz.WriteLine("\t\t\tknots = \"And\";");
                    yaz.WriteLine("\t\t\topenPharanthesis = \"\";");
                    yaz.WriteLine("\t\t\tclosePharanthesis = \"\";");
                    yaz.WriteLine("\t\t\tvalues = new List<string>();");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[DataMember]");
                    yaz.WriteLine("\t\tpublic string column { get; set; }");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[DataMember]");
                    yaz.WriteLine("\t\tpublic List<string> values { get; set; }");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[DataMember]");
                    yaz.WriteLine("\t\tpublic string openPharanthesis { get; set; }");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[DataMember]");
                    yaz.WriteLine("\t\tpublic string closePharanthesis { get; set; }");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[DataMember]");
                    yaz.WriteLine("\t\tpublic string operators { get; set; }");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[DataMember]");
                    yaz.WriteLine("\t\tpublic string knots { get; set; }");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");

                    yaz.Close();
                }
            }

            //ConditionTools
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Servis\\Condition\\ConditionTools.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using TDLibrary;");
                    yaz.WriteLine("using TDFramework.Common;");
                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName + ".Servis.Condition");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic class ConditionTools");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpublic static Knots ReturnKnot(string knot)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tswitch (knot)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tcase \"And\": return Knots.AND;");
                    yaz.WriteLine("\t\t\t\tcase \"Or\": return Knots.OR;");
                    yaz.WriteLine("\t\t\t\tdefault: return Knots.AND;");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tpublic static OrderBy ReturnOrderBy(string knot)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tswitch (knot)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tcase \"Asc\": return OrderBy.ASC;");
                    yaz.WriteLine("\t\t\t\tcase \"Desc\": return OrderBy.DESC;");
                    yaz.WriteLine("\t\t\t\tdefault: return OrderBy.ASC;");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tpublic static Operators ReturnOperator(string operators)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tswitch (operators)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tcase \"Equal\": return Operators.EQUAL;");
                    yaz.WriteLine("\t\t\t\tcase \"Greater\": return Operators.GREATER;");
                    yaz.WriteLine("\t\t\t\tcase \"GreaterEqual\": return Operators.GREATEREQUAL;");
                    yaz.WriteLine("\t\t\t\tcase \"Smaller\": return Operators.SMALLER;");
                    yaz.WriteLine("\t\t\t\tcase \"SmallerEqual\": return Operators.SMALLEREQUAL;");
                    yaz.WriteLine("\t\t\t\tcase \"Like\": return Operators.LIKE;");
                    yaz.WriteLine("\t\t\t\tcase \"StartLike\": return Operators.STARTLIKE;");
                    yaz.WriteLine("\t\t\t\tcase \"EndLike\": return Operators.ENDLIKE;");
                    yaz.WriteLine("\t\t\t\tcase \"ExactLike\": return Operators.EXACTLIKE;");
                    yaz.WriteLine("\t\t\t\tcase \"Between\": return Operators.BETWEEN;");
                    yaz.WriteLine("\t\t\t\tcase \"In\": return Operators.IN;");
                    yaz.WriteLine("\t\t\t\tdefault: return Operators.EQUAL;");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tpublic static Parantheses ReturnParantheses(string openParanthesis, string closeParanthesis)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tParantheses parantheses = new Parantheses();");
                    yaz.WriteLine("\t\t\tparantheses.OpenCount = openParanthesis.LetterCount('(');");
                    yaz.WriteLine("\t\t\tparantheses.ClosedCount = openParanthesis.LetterCount(')');");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\treturn parantheses;");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");

                    yaz.Close();
                }
            }

            /* Service */
            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                List<TableColumnNames> columnNames = tableColumnNames.Where(a => a.TableName == Table).ToList();

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Servis\\I" + Table + "Service.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using System.Runtime.Serialization;");
                        yaz.WriteLine("using System.ServiceModel;");
                        yaz.WriteLine("using System.ServiceModel.Web;");
                        yaz.WriteLine("using " + projectName + ".Servis.Condition;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + ".Servis");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\t[ServiceContract]");
                        yaz.WriteLine("\tpublic interface I" + Table + "Service");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\t[OperationContract]");
                        yaz.WriteLine("\t\t[WebInvoke(UriTemplate = \"/Select/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                        yaz.WriteLine("\t\tList<" + Table + "Data> Select(SELECT select, List<WHERE> whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[OperationContract]");
                        yaz.WriteLine("\t\t[WebInvoke(UriTemplate = \"/SelectSingle/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                        yaz.WriteLine("\t\t" + Table + "Data SelectSingle(SELECT select, List<WHERE> whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[OperationContract]");
                        yaz.WriteLine("\t\t[WebInvoke(UriTemplate = \"/Insert/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                        yaz.WriteLine("\t\tbool Insert(" + Table + "Data " + Table + "Data);");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebInvoke(UriTemplate = \"/Update/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\tbool Update(" + Table + "Data " + Table + "Data, SELECT select, List<WHERE> whereList);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebInvoke(UriTemplate = \"/Delete/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\tbool Delete(List<WHERE> whereList);");
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t[DataContract]");
                        yaz.WriteLine("\tpublic class " + Table + "Data");
                        yaz.WriteLine("\t{");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                yaz.WriteLine("\t\t[DataMember]");
                                yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }");
                            }
                            else
                            {
                                yaz.WriteLine("\t\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Servis\\" + Table + "Service.svc", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("<%@ ServiceHost Language=\"C#\" Debug=\"true\" Service=\"" + projectName + ".Servis." + Table + "Service\" CodeBehind=\"" + Table + "Service.svc.cs\" %>");

                        yaz.Close();
                    }
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Servis\\" + Table + "Service.svc.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Linq;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using TDLibrary;");
                        yaz.WriteLine("using " + projectName + ".Servis.Condition;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + ".Servis");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class " + Table + "Service : I" + Table + "Service");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");

                        //Select
                        yaz.WriteLine("\t\tpublic List<" + Table + "Data> Select(SELECT select, List<WHERE> whereList)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tList<" + Table + "> table = entity." + Table + ".ToList();");
                        yaz.WriteLine("\t\t\tSelect tdSelect = ReturnSelect(select);");
                        yaz.WriteLine("\t\t\tList<Where> tdWhereList = ReturnWhereList(whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (tdSelect != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\ttable.SelectSettings = tdSelect;");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (tdWhereList != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tif (tdWhereList.Count > 0)");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\ttable.WhereList.AddRange(tdWhereList);");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (select != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tif (select.columns != null)");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\tif (select.columns.Length > 0)");
                        yaz.WriteLine("\t\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\t\ttable.Columns = ReturnColumns(select.columns);");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\ttable.Select();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tList<" + Table + "> " + Table + "List = new List<" + Table + ">();");
                        yaz.WriteLine("\t\t\tList<" + Table + "Data> " + Table + "DataList = new List<" + Table + "Data>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (table.HasData)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\t" + Table + "List = table.Data as List<" + Table + ">;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tforeach (" + Table + " item in " + Table + "List)");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\t" + Table + "DataList.Add(new " + Table + "Data()");
                        yaz.WriteLine("\t\t\t\t\t{");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                yaz.WriteLine("\t\t\t\t\t\t" + column.ColumnName + " = item." + column.ColumnName + " != null ? item." + column.ColumnName + ".ToString() : null,");
                            }
                        }

                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn " + Table + "DataList;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        //SelectSingle
                        yaz.WriteLine("\t\tpublic " + Table + "Data SelectSingle(SELECT select, List<WHERE> whereList)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tTable<" + Table + "> table = new Table<" + Table + ">();");
                        yaz.WriteLine("\t\t\tSelect tdSelectSettings = ReturnSelect(select);");
                        yaz.WriteLine("\t\t\tList<Where> tdWhereList = ReturnWhereList(whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (tdSelectSettings != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\ttable.SelectSettings = tdSelectSettings;");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (tdWhereList.Count > 0)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\ttable.WhereList.AddRange(tdWhereList);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (select != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tif (select.columns != null)");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\tif (select.columns.Length > 0)");
                        yaz.WriteLine("\t\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\t\ttable.Columns = ReturnColumns(select.columns);");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\ttable.SelectSettings.Top = 1;");
                        yaz.WriteLine("\t\t\ttable.Select();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t" + Table + " " + Table + " = new " + Table + "();");
                        yaz.WriteLine("\t\t\t" + Table + "Data " + Table + "Data = new " + Table + "Data();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (table.HasData)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\t" + Table + " = (table.Data as List<" + Table + ">).FirstOrDefault();");
                        yaz.WriteLine("");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                yaz.WriteLine("\t\t\t\t" + Table + "Data." + column.ColumnName + " = " + Table + "Data." + column.ColumnName + " != null ? " + Table + "." + column.ColumnName + ".ToString() : null;");
                            }
                        }

                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn " + Table + "Data;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        //Insert
                        yaz.WriteLine("\t\tpublic bool Insert(" + Table + "Data " + Table + "Data)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tif (" + Table + "Data != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tTable<" + Table + "> table = new Table<" + Table + ">();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t" + Table + " " + Table + " = ReturnData(" + Table + "Data);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\ttable.Values = " + Table + ";");
                        yaz.WriteLine("\t\t\t\ttable.Insert();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tif (table.Error == null)");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\treturn true;");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn false;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        if (identityColumns.Count > 0)
                        {
                            //Update
                            yaz.WriteLine("\t\tpublic bool Update(" + Table + "Data " + Table + "Data, SELECT select, List<WHERE> whereList)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (" + Table + "Data != null)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tTable<" + Table + "> table = new Table<" + Table + ">();");
                            yaz.WriteLine("\t\t\t\tList<Where> tdWhereList = ReturnWhereList(whereList);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\tif (select != null)");
                            yaz.WriteLine("\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\tif (select.columns != null)");
                            yaz.WriteLine("\t\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\t\tif (select.columns.Length > 0)");
                            yaz.WriteLine("\t\t\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\t\t\ttable.Columns = ReturnColumns(select.columns);");
                            yaz.WriteLine("\t\t\t\t\t\t}");
                            yaz.WriteLine("\t\t\t\t\t}");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\tif (tdWhereList.Count > 0)");
                            yaz.WriteLine("\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\ttable.WhereList.AddRange(tdWhereList);");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t" + Table + " " + Table + " = ReturnData(" + Table + "Data, table.Columns);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\ttable.Values = " + Table + ";");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\ttable.Update();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\tif (table.Error == null)");
                            yaz.WriteLine("\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\treturn true;");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn false;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Delete
                            yaz.WriteLine("\t\tpublic bool Delete(List<WHERE> whereList)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tTable<" + Table + "> table = new Table<" + Table + ">();");
                            yaz.WriteLine("\t\t\tList<Where> tdWhereList = ReturnWhereList(whereList);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (tdWhereList.Count > 0)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\ttable.WhereList.AddRange(tdWhereList);");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\ttable.Delete();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (table.Error == null)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\treturn true;");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn false;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        //ReturnSelect
                        yaz.WriteLine("\t\tprivate Select ReturnSelect(SELECT select)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tSelect returnSelect = null;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (select != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tif (select != null)");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\treturnSelect = new Select();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tif (select.top > 0)");
                        yaz.WriteLine("\t\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\t\treturnSelect.Top = select.top;");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tif (select.orderColumn != null)");
                        yaz.WriteLine("\t\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\t\treturnSelect.OrderColumn = ReturnOrderColumnByName(select.orderColumn);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t\treturnSelect.OrderBy = ConditionTools.ReturnOrderBy(select.orderBy);");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn returnSelect;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        //ReturnWhereList
                        yaz.WriteLine("\t\tprivate List<Where> ReturnWhereList(List<WHERE> whereList)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tList<Where> returnList = null;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (whereList != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\treturnList = new List<Where>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tforeach (WHERE item in whereList)");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\tWhere tdwhere = new Where();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\ttdwhere.Knot = ConditionTools.ReturnKnot(item.knots);");
                        yaz.WriteLine("\t\t\t\t\ttdwhere.Operator = ConditionTools.ReturnOperator(item.operators);");
                        yaz.WriteLine("\t\t\t\t\ttdwhere.Parantheses = ConditionTools.ReturnParantheses(item.openPharanthesis, item.closePharanthesis);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tif (tdwhere.Operator != Operators.BETWEEN && tdwhere.Operator != Operators.IN)");
                        yaz.WriteLine("\t\t\t\t\t{");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                yaz.WriteLine("\t\t\t\t\t\tif (item.column == \"" + column.ColumnName + "\")");
                                yaz.WriteLine("\t\t\t\t\t\t{");
                                yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Column = " + Table + "Columns." + column.ColumnName + ";");

                                switch (column.TypeName.Name)
                                {
                                    case "Int16": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Int16.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "Int32": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Int32.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "Int64": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Int64.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "Decimal": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Decimal.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "Double": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Double.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "Char": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Char.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "Chars": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Char.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "String": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { item.values.FirstOrDefault() };"); break;
                                    case "Byte": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Byte.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "Bytes": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Byte.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "Boolean": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Boolean.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "DateTime": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { DateTime.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "DateTimeOffset": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { DateTimeOffset.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "TimeSpan": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { TimeSpan.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "Single": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Single.Parse(item.values.FirstOrDefault()) };"); break;
                                    case "Object": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { item.values.FirstOrDefault() };"); break;
                                    case "Guid": yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { Guid.Parse(item.values.FirstOrDefault()) };"); break;
                                    default: yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = new List<dynamic> { item.values.FirstOrDefault() };"); break;
                                }

                                yaz.WriteLine("\t\t\t\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }


                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\telse");
                        yaz.WriteLine("\t\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\t\tList<dynamic> listCon = new List<dynamic>();");
                        yaz.WriteLine("");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                yaz.WriteLine("\t\t\t\t\t\tif (item.column == \"" + column.ColumnName + "\")");
                                yaz.WriteLine("\t\t\t\t\t\t{");
                                yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Column = " + Table + "Columns." + column.ColumnName + ";");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\t\t\tforeach (string s in item.values)");
                                yaz.WriteLine("\t\t\t\t\t\t\t{");

                                switch (column.TypeName.Name)
                                {
                                    case "Int16": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Int16.Parse(s));"); break;
                                    case "Int32": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Int32.Parse(s));"); break;
                                    case "Int64": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Int64.Parse(s));"); break;
                                    case "Decimal": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Decimal.Parse(s));"); break;
                                    case "Double": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Double.Parse(s));"); break;
                                    case "Char": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Char.Parse(s));"); break;
                                    case "Chars": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Char.Parse(s));"); break;
                                    case "String": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(s);"); break;
                                    case "Byte": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Byte.Parse(s));"); break;
                                    case "Bytes": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Byte.Parse(s));"); break;
                                    case "Boolean": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Boolean.Parse(s));"); break;
                                    case "DateTime": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(DateTime.Parse(s));"); break;
                                    case "DateTimeOffset": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(DateTimeOffset.Parse(s));"); break;
                                    case "TimeSpan": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(TimeSpan.Parse(s));"); break;
                                    case "Single": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Single.Parse(s));"); break;
                                    case "Object": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(s);"); break;
                                    case "Guid": yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(Guid.Parse(s));"); break;
                                    default: yaz.WriteLine("\t\t\t\t\t\t\t\tlistCon.Add(s);"); break;
                                }

                                yaz.WriteLine("\t\t\t\t\t\t\t}");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\t\t\ttdwhere.Values = listCon;");
                                yaz.WriteLine("\t\t\t\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }

                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t\t\t\t\treturnList.Add(tdwhere);");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn returnList;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        //ReturnColumns
                        yaz.WriteLine("\t\tprivate List<" + Table + "Columns> ReturnColumns(string[] columns)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tList<" + Table + "Columns> returnColumns = new List<" + Table + "Columns>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tforeach (string column in columns)");
                        yaz.WriteLine("\t\t\t{");

                        int i = 0;

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                string elseText = i == 0 ? "" : "else ";

                                yaz.WriteLine("\t\t\t\t" + elseText + "if (column.ToHyperLinkText(true) == \"" + column.ColumnName + "\".ToHyperLinkText(true)) { returnColumns.Add(" + Table + "Columns." + column.ColumnName + "); }");

                                i++;
                            }
                        }


                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn returnColumns;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tprivate " + Table + "Columns ReturnOrderColumnByName(string orderColumn)");
                        yaz.WriteLine("\t\t{");

                        i = 0;

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                string elseText = i == 0 ? "" : "else ";

                                yaz.WriteLine("\t\t\t" + elseText + "if (orderColumn.ToHyperLinkText(true) == \"" + column.ColumnName + "\".ToHyperLinkText(true)) { return " + Table + "Columns." + column.ColumnName + "; }");

                                i++;
                            }
                        }
                        yaz.WriteLine("\t\t\telse { return " + Table + "Columns." + id + "; }");

                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        //ReturnData
                        yaz.WriteLine("\t\tprivate " + Table + " ReturnData(" + Table + "Data " + Table + "Data, List<" + Table + "Columns> Columns = null)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\t" + Table + " " + Table + " = new " + Table + "();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (Columns == null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tColumns = new List<" + Table + "Columns>();");
                        yaz.WriteLine("");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                yaz.WriteLine("\t\t\t\tColumns.Add(" + Table + "Columns." + column.ColumnName + ");");
                            }
                        }

                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                switch (column.TypeName.Name)
                                {
                                    case "Int16": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Int16.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "Int32": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Int32.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "Int64": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Int64.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "Decimal": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Decimal.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "Double": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Double.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "Char": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Char.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "Chars": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Char.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "String": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = " + Table + "Data." + column.ColumnName + "; } catch { }"); break;
                                    case "Byte": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Byte.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "Bytes": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Byte.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "Boolean": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Boolean.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "DateTime": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = DateTime.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "DateTimeOffset": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = DateTimeOffset.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "TimeSpan": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = TimeSpan.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "Single": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Single.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    case "Object": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = " + Table + "Data." + column.ColumnName + "; } catch { }"); break;
                                    case "Guid": yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = Guid.Parse(" + Table + "Data." + column.ColumnName + "); } catch { }"); break;
                                    default: yaz.WriteLine("\t\t\ttry { " + Table + "." + column.ColumnName + " = " + Table + "Data." + column.ColumnName + "; } catch { }"); break;
                                }
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn " + Table + ";");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }
        }

        #endregion

        #region Angular

        void CreateAngular()
        {
            CreateAngularDirectories();
            CreateAngularFiles();

            if (chkMVCHepsi.Checked == true)
            {
                CreateAngularModelLayer();
                CreateAngularViewLayer();
                CreateAngularControllerLayer();
                CreateAngularServicesLayer();
                CreateAngularTypeScriptLayer();
                CreateWebConfig();
                CreateStilScript();

                CreateStoredProcedure();
            }
            else
            {
                if (chkMVCModel.Checked == true)
                {
                    CreateAngularModelLayer();
                }

                if (chkMVCView.Checked == true)
                {
                    CreateAngularViewLayer();
                }

                if (chkMVCController.Checked == true)
                {
                    CreateAngularControllerLayer();
                    CreateAngularServicesLayer();
                    CreateAngularTypeScriptLayer();
                }

                if (chkMVCWebConfig.Checked == true)
                {
                    CreateWebConfig();
                }

                if (chkMVCWcfServis.Checked == true)
                {
                    CreateWcfServis();
                }

                if (chkMVCStoredProc.Checked == true)
                {
                    CreateStoredProcedure();
                }

                if (chkMVCStilScript.Checked == true)
                {
                    CreateStilScript();
                }
            }

            CreateAngularRegistrar();
        }

        void CreateAngularDirectories(string _tableName = null)
        {
            projectFolder = projectName;

            if (!Directory.Exists(PathAddress + "\\" + projectFolder))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder);
            }

            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src"))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src");
            }

            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app"))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app");
            }

            if (_tableName == null)
            {
                if (chkMVCHepsi.Checked)
                {
                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\models"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\models");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\services"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\services");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\home"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\home");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\shared"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\shared");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\shared\\controls"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\shared\\controls");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\home"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\home");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\models"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\models");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\services"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\services");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\App_Start"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\App_Start");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Models"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Models");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Controllers"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Controllers");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Shared"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Shared");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Home"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Home");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Views"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Views");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Models"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Models");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\css"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\css");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\img"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\img");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\js"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\js");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js");
                    }
                }
                else
                {
                    if (chkMVCModel.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Models"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Models");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\models"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\models");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\models"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\models");
                        }
                    }

                    if (chkMVCView.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\App_Start"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\App_Start");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Shared"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Shared");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Views\\Home"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Views\\Home");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\home"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\home");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\shared"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\shared");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\shared\\controls"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\shared\\controls");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\home"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\home");
                        }
                    }

                    if (chkMVCController.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\App_Start"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\App_Start");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Controllers"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Controllers");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\services"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\services");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\services"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\services");
                        }
                    }

                    if (chkMVCStilScript.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\css"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\css");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\img"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\img");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\js"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\js");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js");
                        }
                    }
                }
            }
            else
            {
                if (chkMVCHepsi.Checked || chkMVCView.Checked)
                {
                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + _tableName))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + _tableName);
                    }
                }
            }
        }

        void CreateAngularFiles()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\app.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<router-outlet></router-outlet>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\app.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from \"@angular/core\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("declare global {");
                    yaz.WriteLine("\tinterface JQuery {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: \"" + projectName.Substring(0, 3).ToLower() + "-app\",");
                    yaz.WriteLine("\ttemplateUrl: './app.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AppComponent {");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            CreateAngularAppModule();

            CreateAngularRoutingModule();
        }

        void CreateAngularAppModule()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\app.module.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { APP_BASE_HREF } from '@angular/common';");
                    yaz.WriteLine("import { NgModule } from '@angular/core';");
                    yaz.WriteLine("import { BrowserModule } from '@angular/platform-browser';");
                    yaz.WriteLine("import { ReactiveFormsModule } from '@angular/forms';");
                    yaz.WriteLine("import { AppRoutingModule } from './app-routing.module';");
                    yaz.WriteLine("import { HttpClientModule } from '@angular/common/http';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { AppComponent } from './app';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { LayoutComponent } from './shared/layout';");
                    yaz.WriteLine("import { HomeComponent } from './home/index';");
                    yaz.WriteLine("import { ScriptsComponent } from './shared/controls/scripts';");
                    yaz.WriteLine("");
                    yaz.WriteLine("//Admin");
                    yaz.WriteLine("import { AdminLoginComponent } from './admin/views/home/login';");
                    yaz.WriteLine("import { AdminLayoutComponent } from './admin/views/shared/layoutAdmin';");
                    yaz.WriteLine("import { AdminHomeComponent } from './admin/views/home/index';");
                    yaz.WriteLine("import { AdminHeaderComponent } from './admin/views/shared/controls/header';");
                    yaz.WriteLine("import { AdminLeftMenuComponent } from './admin/views/shared/controls/leftmenu';");
                    yaz.WriteLine("import { AdminScriptsComponent } from './admin/views/shared/controls/scripts';");
                    yaz.WriteLine("import { AdminCopyDeleteComponent } from './admin/views/shared/controls/copydelete';");
                    yaz.WriteLine("import { AdminFooterComponent } from './admin/views/shared/controls/footer';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { AdminBaglantiIndexComponent } from './admin/views/baglanti';");
                    yaz.WriteLine("import { AdminBaglantiEkleComponent } from './admin/views/baglanti/ekle';");
                    yaz.WriteLine("import { AdminBaglantiDuzenleComponent } from './admin/views/baglanti/duzenle';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import * as $ from \"jquery\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("@NgModule({");
                    yaz.WriteLine("\tdeclarations: [");
                    yaz.WriteLine("\t\tAppComponent,");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tLayoutComponent,");
                    yaz.WriteLine("\t\tHomeComponent,");
                    yaz.WriteLine("\t\tScriptsComponent,");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tAdminLoginComponent,");
                    yaz.WriteLine("\t\tAdminLayoutComponent,");
                    yaz.WriteLine("\t\tAdminHomeComponent,");
                    yaz.WriteLine("\t\tAdminHeaderComponent,");
                    yaz.WriteLine("\t\tAdminLeftMenuComponent,");
                    yaz.WriteLine("\t\tAdminScriptsComponent,");
                    yaz.WriteLine("\t\tAdminCopyDeleteComponent,");
                    yaz.WriteLine("\t\tAdminFooterComponent,");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tAdminBaglantiIndexComponent,");
                    yaz.WriteLine("\t\tAdminBaglantiEkleComponent,");
                    yaz.WriteLine("\t\tAdminBaglantiDuzenleComponent,");
                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\timports: [");
                    yaz.WriteLine("\t\tBrowserModule,");
                    yaz.WriteLine("\t\tAppRoutingModule,");
                    yaz.WriteLine("\t\tReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'never' }),");
                    yaz.WriteLine("\t\tHttpClientModule");
                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\t//'/' -> '/Emlak/' Bu şekilde değişecek");
                    yaz.WriteLine("\tproviders: [{ provide: APP_BASE_HREF, useValue: '/Emlak/' }],");
                    yaz.WriteLine("\tbootstrap: [AppComponent]");
                    yaz.WriteLine("})");
                    yaz.WriteLine("export class AppModule { }");
                    yaz.Close();
                }
            }
        }

        void CreateAngularRoutingModule()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\app-routing.module.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { NgModule } from '@angular/core';");
                    yaz.WriteLine("import { Routes, RouterModule } from '@angular/router';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { LayoutComponent } from './shared/layout';");
                    yaz.WriteLine("import { HomeComponent } from './home/home';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { AdminLayoutComponent } from './admin/views/shared/layoutAdmin';");
                    yaz.WriteLine("import { AdminHomeComponent } from './admin/views/home/index';");
                    yaz.WriteLine("import { AdminLoginComponent } from './admin/views/home/login';");
                    yaz.WriteLine("");

                    yaz.WriteLine("import { AdminBaglantiIndexComponent } from './admin/views/baglanti';");
                    yaz.WriteLine("import { AdminBaglantiEkleComponent } from './admin/views/baglanti/ekle';");
                    yaz.WriteLine("import { AdminBaglantiDuzenleComponent } from './admin/views/baglanti/duzenle';");
                    yaz.WriteLine("");

                    yaz.WriteLine("const routes: Routes = [");
                    yaz.WriteLine("\t{ path: 'Admin', component: AdminLoginComponent },");
                    yaz.WriteLine("\t{ path: 'Admin/Login', component: AdminLoginComponent },");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpath: '',");
                    yaz.WriteLine("\t\tcomponent: LayoutComponent,");
                    yaz.WriteLine("\t\tchildren: [");
                    yaz.WriteLine("\t\t\t//{ path: '', redirectTo: 'Home', pathMatch: 'full' },");
                    yaz.WriteLine("\t\t\t{ path: '', component: HomeComponent, pathMatch: 'full' },");
                    yaz.WriteLine("\t\t]");
                    yaz.WriteLine("\t},");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpath: '',");
                    yaz.WriteLine("\t\tcomponent: AdminLayoutComponent,");
                    yaz.WriteLine("\t\tchildren: [");
                    yaz.WriteLine("\t\t\t{ path: 'Admin/Home', component: AdminHomeComponent },");
                    yaz.WriteLine("");

                    yaz.WriteLine("\t\t\t{ path: 'Admin/Baglanti', component: AdminBaglantiIndexComponent },");
                    yaz.WriteLine("\t\t\t{ path: 'Admin/Baglanti/Index', component: AdminBaglantiIndexComponent },");
                    yaz.WriteLine("\t\t\t{ path: 'Admin/Baglanti/Ekle', component: AdminBaglantiEkleComponent },");
                    yaz.WriteLine("\t\t\t{ path: 'Admin/Baglanti/Ekle/:linkID', component: AdminBaglantiEkleComponent },");
                    yaz.WriteLine("\t\t\t{ path: 'Admin/Baglanti/Duzenle/:id', component: AdminBaglantiDuzenleComponent },");
                    yaz.WriteLine("");

                    yaz.WriteLine("\t\t]");
                    yaz.WriteLine("\t},");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t{ path: '**', redirectTo: '' }");
                    yaz.WriteLine("];");
                    yaz.WriteLine("");
                    yaz.WriteLine("@NgModule({");
                    yaz.WriteLine("\timports: [RouterModule.forRoot(routes)],");
                    yaz.WriteLine("\texports: [RouterModule]");
                    yaz.WriteLine("})");
                    yaz.WriteLine("export class AppRoutingModule { }");
                    yaz.Close();
                }
            }
        }

        void CreateAngularRegistrar()
        {
            if (Directory.Exists(PathAddress + "\\" + projectFolder + "\\App_Start"))
            {
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\App_Start\\RouteConfig.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System.Web.Mvc;");
                        yaz.WriteLine("using System.Web.Routing;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + "");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class RouteConfig");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\tpublic static void RegisterRoutes(RouteCollection routes)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\troutes.IgnoreRoute(\"{resource}.axd/{*pathInfo}\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\troutes.MapRoute(");
                        yaz.WriteLine("\t\t\t\tname: \"Shared\",");
                        yaz.WriteLine("\t\t\t\turl: \"Shared/{action}/{id}\",");
                        yaz.WriteLine("\t\t\t\tdefaults: new { controller = \"Shared\", action = \"Index\", id = UrlParameter.Optional },");
                        yaz.WriteLine("\t\t\t\tnamespaces: new[] { \"" + projectName + ".Controllers\" }");
                        yaz.WriteLine("\t\t\t);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\troutes.MapRoute(");
                        yaz.WriteLine("\t\t\t\tname: \"Default\",");
                        yaz.WriteLine("\t\t\t\turl: \"{*anything}\",");
                        yaz.WriteLine("\t\t\t\t// url: \"{controller}/{action}/{id}\",");
                        yaz.WriteLine("\t\t\t\tdefaults: new { controller = \"Home\", action = \"Index\", id = UrlParameter.Optional }");
                        yaz.WriteLine("\t\t\t);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }

            if (Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax"))
            {
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\AjaxAreaRegistration.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System.Web.Mvc;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + ".Areas.Ajax");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class AjaxAreaRegistration : AreaRegistration");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\tpublic override string AreaName");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tget");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\treturn \"Ajax\";");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tpublic override void RegisterArea(AreaRegistrationContext context)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tcontext.MapRoute(");
                        yaz.WriteLine("\t\t\t\t\"Ajax_default\",");
                        yaz.WriteLine("\t\t\t\t\"Ajax/{controller}/{action}/{id}\",");
                        yaz.WriteLine("\t\t\t\tnew { controller = \"Home\", action = \"Index\", id = UrlParameter.Optional },");
                        yaz.WriteLine("\t\t\t\tnamespaces: new[] { \"" + projectName + ".Areas.Ajax.Controllers\" }");
                        yaz.WriteLine("\t\t\t);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }
            }
        }

        void CreateAngularModelLayer()
        {
            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));
                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                CreateDirectories(Table);

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Models\\" + Table + ".cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        List<TableColumnNames> columnNames = tableColumnNames.Where(a => a.TableName == Table).ToList();

                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using System.Web.Mvc;");

                        yaz.WriteLine("namespace Models");
                        yaz.WriteLine("{");

                        yaz.WriteLine("\tpublic class " + Table);
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\tpublic " + Table + "()");
                        yaz.WriteLine("\t\t{");

                        if (chkRTables.Checked == true)
                        {
                            if (fkcList.Count > 0)
                            {

                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    yaz.WriteLine("\t\t\t" + ForeignTableName + "List = new List<" + ForeignTableName + ">();");

                                }
                            }

                            if (fkcListForeign.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    yaz.WriteLine("\t\t\t" + PrimaryTableName + "List = new List<SelectListItem>();");
                                }
                            }
                        }
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        int counter = columnNames.Count;
                        int i = 1;

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                if (column.IsNullable)
                                {
                                    switch (column.TypeName.Name)
                                    {
                                        case "Int16": yaz.WriteLine("\t\tpublic int? " + column.ColumnName + " { get; set; }"); break;
                                        case "Int32": yaz.WriteLine("\t\tpublic int? " + column.ColumnName + " { get; set; }"); break;
                                        case "Int64": yaz.WriteLine("\t\tpublic Int64? " + column.ColumnName + " { get; set; }"); break;
                                        case "Decimal": yaz.WriteLine("\t\tpublic decimal? " + column.ColumnName + " { get; set; }"); break;
                                        case "Double": yaz.WriteLine("\t\tpublic double? " + column.ColumnName + " { get; set; }"); break;
                                        case "Char": yaz.WriteLine("\t\tpublic char " + column.ColumnName + " { get; set; }"); break;
                                        case "Chars": yaz.WriteLine("\t\tpublic char[] " + column.ColumnName + " { get; set; }"); break;
                                        case "String": yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                        case "Byte": yaz.WriteLine("\t\tpublic byte " + column.ColumnName + " { get; set; }"); break;
                                        case "Bytes": yaz.WriteLine("\t\tpublic byte[] " + column.ColumnName + " { get; set; }"); break;
                                        case "Boolean": yaz.WriteLine("\t\tpublic bool? " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTime": yaz.WriteLine("\t\tpublic DateTime? " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTimeOffset": yaz.WriteLine("\t\tpublic DateTimeOffset? " + column.ColumnName + " { get; set; }"); break;
                                        case "TimeSpan": yaz.WriteLine("\t\tpublic TimeSpan? " + column.ColumnName + " { get; set; }"); break;
                                        case "Single": yaz.WriteLine("\t\tpublic Single? " + column.ColumnName + " { get; set; }"); break;
                                        case "Object": yaz.WriteLine("\t\tpublic object " + column.ColumnName + " { get; set; }"); break;
                                        case "Guid": yaz.WriteLine("\t\tpublic Guid? " + column.ColumnName + " { get; set; }"); break;
                                        default: yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                    }
                                }
                                else
                                {
                                    switch (column.TypeName.Name)
                                    {
                                        case "Int16": yaz.WriteLine("\t\tpublic int " + column.ColumnName + " { get; set; }"); break;
                                        case "Int32": yaz.WriteLine("\t\tpublic int " + column.ColumnName + " { get; set; }"); break;
                                        case "Int64": yaz.WriteLine("\t\tpublic Int64 " + column.ColumnName + " { get; set; }"); break;
                                        case "Decimal": yaz.WriteLine("\t\tpublic decimal " + column.ColumnName + " { get; set; }"); break;
                                        case "Double": yaz.WriteLine("\t\tpublic double " + column.ColumnName + " { get; set; }"); break;
                                        case "Char": yaz.WriteLine("\t\tpublic char " + column.ColumnName + " { get; set; }"); break;
                                        case "Chars": yaz.WriteLine("\t\tpublic char[] " + column.ColumnName + " { get; set; }"); break;
                                        case "String": yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                        case "Byte": yaz.WriteLine("\t\tpublic byte " + column.ColumnName + " { get; set; }"); break;
                                        case "Bytes": yaz.WriteLine("\t\tpublic byte[] " + column.ColumnName + " { get; set; }"); break;
                                        case "Boolean": yaz.WriteLine("\t\tpublic bool " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTime": yaz.WriteLine("\t\tpublic DateTime " + column.ColumnName + " { get; set; }"); break;
                                        case "DateTimeOffset": yaz.WriteLine("\t\tpublic DateTimeOffset " + column.ColumnName + " { get; set; }"); break;
                                        case "TimeSpan": yaz.WriteLine("\t\tpublic TimeSpan " + column.ColumnName + " { get; set; }"); break;
                                        case "Single": yaz.WriteLine("\t\tpublic Single " + column.ColumnName + " { get; set; }"); break;
                                        case "Object": yaz.WriteLine("\t\tpublic object " + column.ColumnName + " { get; set; }"); break;
                                        case "Guid": yaz.WriteLine("\t\tpublic Guid " + column.ColumnName + " { get; set; }"); break;
                                        default: yaz.WriteLine("\t\tpublic string " + column.ColumnName + " { get; set; }"); break;
                                    }
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }

                            i++;
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tpublic string Mesaj { get; set; }");


                        if (chkRTables.Checked == true)
                        {
                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("");

                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    yaz.WriteLine("\t\tpublic List<" + ForeignTableName + "> " + ForeignTableName + "List { get; set; }");
                                }
                            }

                            if (fkcListForeign.Count > 0)
                            {
                                yaz.WriteLine("");

                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    yaz.WriteLine("\t\tpublic List<SelectListItem> " + PrimaryTableName + "List { get; set; }");
                                }
                            }
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\models\\I" + Table + ".ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        List<TableColumnNames> columnNames = tableColumnNames.Where(a => a.TableName == Table).ToList();

                        yaz.WriteLine("export interface I" + Table);
                        yaz.WriteLine("{");

                        int counter = columnNames.Count;
                        int i = 1;

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                switch (column.TypeName.Name)
                                {
                                    case "Int16": yaz.WriteLine("\t" + column.ColumnName + " : number,"); break;
                                    case "Int32": yaz.WriteLine("\t" + column.ColumnName + " : number,"); break;
                                    case "Int64": yaz.WriteLine("\t" + column.ColumnName + " : number,"); break;
                                    case "Decimal": yaz.WriteLine("\t" + column.ColumnName + " : any,"); break;
                                    case "Double": yaz.WriteLine("\t" + column.ColumnName + " : any,"); break;
                                    case "Char": yaz.WriteLine("\t" + column.ColumnName + " : string,"); break;
                                    case "Chars": yaz.WriteLine("\t" + column.ColumnName + " : any,"); break;
                                    case "String": yaz.WriteLine("\t" + column.ColumnName + " : string,"); break;
                                    case "Byte": yaz.WriteLine("\t" + column.ColumnName + " : any,"); break;
                                    case "Bytes": yaz.WriteLine("\t" + column.ColumnName + " : any,"); break;
                                    case "Boolean": yaz.WriteLine("\t" + column.ColumnName + " : boolean,"); break;
                                    case "DateTime": yaz.WriteLine("\t" + column.ColumnName + " : Date,"); break;
                                    case "DateTimeOffset": yaz.WriteLine("\t" + column.ColumnName + " : string,"); break;
                                    case "TimeSpan": yaz.WriteLine("\t" + column.ColumnName + " : any,"); break;
                                    case "Single": yaz.WriteLine("\t" + column.ColumnName + " : any,"); break;
                                    case "Object": yaz.WriteLine("\t" + column.ColumnName + " : any,"); break;
                                    case "Guid": yaz.WriteLine("\t" + column.ColumnName + " : any,"); break;
                                    default: yaz.WriteLine("\t" + column.ColumnName + " : string,"); break;
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }

                            i++;
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tMesaj : string,");


                        if (chkRTables.Checked == true)
                        {
                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("");

                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    yaz.WriteLine("\t" + ForeignTableName + "List : Array<" + ForeignTableName + ">,");
                                }
                            }

                            if (fkcListForeign.Count > 0)
                            {
                                yaz.WriteLine("");

                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    yaz.WriteLine("\t" + PrimaryTableName + "List : any[],");
                                }
                            }
                        }

                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }
            }
        }

        void CreateAngularLayout()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\_Layout.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("<!DOCTYPE html>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<html>");
                    yaz.WriteLine("<head>");
                    yaz.WriteLine("\t<link rel=\"shortcut icon\" href=\"@AppMgr.MainPath/favicon.ico\" type=\"image/x-icon\">");
                    yaz.WriteLine("\t<link rel=\"icon\" href=\"@AppMgr.MainPath/favicon.ico\" type=\"image/x-icon\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
                    yaz.WriteLine("\t<title>@ViewBag.Title</title>");
                    yaz.WriteLine("</head>");
                    yaz.WriteLine("<body>");
                    yaz.WriteLine("\t@RenderBody()");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Views/Shared/Controls/_Scripts.cshtml\"); }");
                    yaz.WriteLine("</body>");
                    yaz.WriteLine("</html>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls\\_Scripts.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/libs/runtime-es2015.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/libs/polyfills-es2015.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/libs/styles-es2015.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/libs/vendor-es2015.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/libs/main-es2015.js\"></script>");

                    yaz.Close();
                }
            }
        }

        void CreateAngularViewLayer()
        {
            int i = 0;

            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));
                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                CreateAngularDirectories(Table);

                if (i <= 0)
                {
                    CreateAngularLayout();
                    CreateAngularHomePage();

                    i++;
                }

                //Index
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToLower() + "\\index.html", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        int tablewidth = 120;
                        int colcount = tableColumnNames.Where(a => a.TableName == Table).ToList().Count;

                        switch (colcount)
                        {
                            case 1:
                                tablewidth = tablewidth + 50;
                                break;
                            case 2:
                                tablewidth = tablewidth + 50 + 230;
                                break;
                            case 3:
                                tablewidth = tablewidth + 50 + 230 + 240;
                                break;
                            case 4:
                                tablewidth = tablewidth + 50 + 230 + 240 + 138;
                                break;
                            default:
                                tablewidth = tablewidth + 50 + 230 + 240 + 138;
                                tablewidth = tablewidth + ((colcount - 4) * 138);
                                break;
                        }

                        yaz.WriteLine("@using TDLibrary");
                        yaz.WriteLine("");
                        yaz.WriteLine("@{");
                        yaz.WriteLine("\tViewBag.Title = \"" + Table + "\";");
                        yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_Layout.cshtml\";");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("<div id=\"tablo" + Table + "\">");
                        yaz.WriteLine("</div>");
                        yaz.WriteLine("");
                        yaz.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"@AppMgr.ScriptPath/tdTable/tdTable.css\" />");
                        yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/tdTable/tdTable.js\"></script>");
                        yaz.WriteLine("");
                        yaz.WriteLine("<script>");
                        yaz.WriteLine("\t$(\"#tablo" + Table + "\").tdTable(");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\trootfolder: \"@AppMgr.ScriptPath/tdTable\",");
                        yaz.WriteLine("\t\ttitle: \"" + Table + "\",");
                        yaz.WriteLine("\t\tlistmethod: \"@AppMgr.AdminPath/" + Table + "/" + Table + "Index\",");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\tdeletemethod: \"@AppMgr.AdminPath/" + Table + "/Sil\",");
                        }

                        yaz.WriteLine("\t\titemperpage: 10,");
                        yaz.WriteLine("\t\ttablewidth: " + tablewidth + ",");
                        yaz.WriteLine("\t\tenablesorting: true,");
                        yaz.WriteLine("\t\tshowfooter: true,");
                        yaz.WriteLine("\t\tshowcommands: true,");
                        yaz.WriteLine("\t\ttheme: \"blue\",");
                        yaz.WriteLine("\t\tconditions: {");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\tOrderBy: \"" + id + "\",");
                        }

                        yaz.WriteLine("\t\t\tFields: {");

                        int cssSayac = 1;
                        string cssClass = "first";

                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                        {
                            switch (cssSayac)
                            {
                                case 1:
                                    cssClass = "first";
                                    break;
                                case 2:
                                    cssClass = "second";
                                    break;
                                case 3:
                                    cssClass = "third";
                                    break;
                                case 4:
                                    cssClass = "fourth";
                                    break;
                                default:
                                    cssClass = "fourth";
                                    break;
                            }
                            List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                            if (foreLst.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t\t" + foreLst.FirstOrDefault().PrimaryTableName + "Adi: {");
                                yaz.WriteLine("\t\t\t\t\tTitle: \"" + foreLst.FirstOrDefault().PrimaryTableName + "Adi\",");
                            }
                            else
                            {
                                yaz.WriteLine("\t\t\t\t" + column.ColumnName + ": {");
                                yaz.WriteLine("\t\t\t\t\tTitle: \"" + column.ColumnName + "\",");
                            }
                            yaz.WriteLine("\t\t\t\t\tCssClass: \"" + cssClass + "\"");

                            yaz.WriteLine("\t\t\t\t},");

                            cssSayac++;
                        }

                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t},");
                        yaz.WriteLine("\t\tcommands: {");
                        yaz.WriteLine("\t\t\tAddText: \"Ekle\",");
                        yaz.WriteLine("\t\t\tAddLink: \"" + Table + "/Ekle\",");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\tDetailLink: \"" + Table + "/Detay/{" + id + "}\",");
                            yaz.WriteLine("\t\t\tUpdateLink: \"" + Table + "/Duzenle/{" + id + "}\",");
                            yaz.WriteLine("\t\t\tDeleteLink: \"javascript:;\"");
                        }
                        else
                        {
                            yaz.WriteLine("\t\t\tShowDetailLink: false,");
                            yaz.WriteLine("\t\t\tShowUpdateLink: false,");
                            yaz.WriteLine("\t\t\tShowDeleteLink: false");
                        }

                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t});");
                        yaz.WriteLine("</script>");

                        yaz.Close();
                    }
                }

                //Ekle
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToLower() + "\\ekle.html", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("@model " + projectName + "." + Table);
                        yaz.WriteLine("");
                        yaz.WriteLine("@{");
                        yaz.WriteLine("\tViewBag.Title = \"" + Table + " Ekle\";");
                        yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_Layout.cshtml\";");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("@using (Html.BeginForm()) {");
                        yaz.WriteLine("\t@Html.ValidationSummary(true)");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t<fieldset>");
                        yaz.WriteLine("\t\t<legend>" + Table + " Ekle</legend>");
                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                        {
                            if (!identityColumns.Contains(column.ColumnName))
                            {
                                List<ForeignKeyChecker> frchkLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                if (frchkLst.Count > 0)
                                {
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t" + frchkLst.FirstOrDefault().PrimaryTableName + "Adi");
                                    yaz.WriteLine("\t\t</div>");
                                    yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("\t\t<div class=\"editor-field\">");
                                    yaz.WriteLine("\t\t\t@Html.DropDownListFor(model => model." + column.ColumnName + ", (List<SelectListItem>)Model." + frchkLst.FirstOrDefault().PrimaryTableName + "List)");
                                    yaz.WriteLine("\t\t</div>");
                                }
                                else
                                {
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t@Html.LabelFor(model => model." + column.ColumnName + ")");
                                    yaz.WriteLine("\t\t</div>");
                                    yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("\t\t<div class=\"editor-field\">");
                                    yaz.WriteLine("\t\t\t@Html.EditorFor(model => model." + column.ColumnName + ")");
                                    yaz.WriteLine("\t\t\t@Html.ValidationMessageFor(model => model." + column.ColumnName + ")");
                                    yaz.WriteLine("\t\t</div>");
                                }
                                yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t<p>");
                        yaz.WriteLine("\t\t\t<input type=\"submit\" value=\"Kaydet\" />");
                        yaz.WriteLine("\t\t</p>");
                        yaz.WriteLine("\t</fieldset>");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("<div class=\"pagelinks\">");
                        yaz.WriteLine("\t@Html.ActionLink(\"Listeye Git\", \"Index\", \"" + Table + "\")");
                        yaz.WriteLine("</div>");

                        yaz.Close();
                    }
                }

                if (identityColumns.Count > 0)
                {
                    //Duzenle
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToLower() + "\\duzenle.html", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                        {
                            yaz.WriteLine("@model " + projectName + "." + Table);
                            yaz.WriteLine("");
                            yaz.WriteLine("@{");
                            yaz.WriteLine("\tViewBag.Title = \"" + Table + " Düzenle\";");
                            yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_Layout.cshtml\";");
                            yaz.WriteLine("}");
                            yaz.WriteLine("");

                            yaz.WriteLine("@using (Html.BeginForm()) {");
                            yaz.WriteLine("\t@Html.ValidationSummary(true)");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t<fieldset>");
                            yaz.WriteLine("\t\t<legend>" + Table + " Düzenle</legend>");
                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                if (identityColumns.Contains(column.ColumnName))
                                {
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t@Html.HiddenFor(model => model." + column.ColumnName + ")");
                                    yaz.WriteLine("\t\t</div>");
                                }
                                else
                                {
                                    List<ForeignKeyChecker> frchkLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    if (frchkLst.Count > 0)
                                    {
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t<div class=\"editor-label\">");
                                        yaz.WriteLine("\t\t\t" + frchkLst.FirstOrDefault().PrimaryTableName + "Adi");
                                        yaz.WriteLine("\t\t</div>");
                                        yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t<div class=\"editor-field\">");
                                        yaz.WriteLine("\t\t\t@Html.DropDownListFor(model => model." + column.ColumnName + ", (List<SelectListItem>)Model." + frchkLst.FirstOrDefault().PrimaryTableName + "List)");
                                        yaz.WriteLine("\t\t</div>");
                                    }
                                    else
                                    {
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t<div class=\"editor-label\">");
                                        yaz.WriteLine("\t\t\t@Html.LabelFor(model => model." + column.ColumnName + ")");
                                        yaz.WriteLine("\t\t</div>");
                                        yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t<div class=\"editor-field\">");
                                        yaz.WriteLine("\t\t\t@Html.EditorFor(model => model." + column.ColumnName + ")");
                                        yaz.WriteLine("\t\t\t@Html.ValidationMessageFor(model => model." + column.ColumnName + ")");
                                        yaz.WriteLine("\t\t</div>");
                                    }
                                    yaz.WriteLine("\t\t<div class=\"clear\"></div>");
                                }
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t<p>");
                            yaz.WriteLine("\t\t\t<input type=\"submit\" value=\"Kaydet\" />");
                            yaz.WriteLine("\t\t</p>");
                            yaz.WriteLine("\t</fieldset>");
                            yaz.WriteLine("}");
                            yaz.WriteLine("");
                            yaz.WriteLine("<div class=\"pagelinks\">");
                            yaz.WriteLine("\t@Html.ActionLink(\"Listeye Git\", \"Index\", \"" + Table + "\")");
                            yaz.WriteLine("</div>");

                            yaz.Close();
                        }
                    }
                }
            }
        }

        void CreateAngularControllerLayer()
        {
            int i = 0;

            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                identityColumns = identityColumns.IdentityCheck(lstSeciliKolonlar);

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                if (i <= 0)
                {
                    CreateAngularHomeController();

                    i++;
                }

                StreamWriter yaz = File.CreateText(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers\\" + Table + "Controller.cs");

                yaz.WriteLine("using System;");
                yaz.WriteLine("using System.Linq;");
                yaz.WriteLine("using System.Web.Mvc;");
                yaz.WriteLine("using System.Collections.Generic;");
                yaz.WriteLine("using TDLibrary;");
                yaz.WriteLine("using Models;");

                yaz.WriteLine("");
                yaz.WriteLine("namespace " + projectName + ".Areas.Ajax.Controllers");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic class " + Table + "Controller : Controller");
                yaz.WriteLine("\t{");
                yaz.WriteLine("\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                yaz.WriteLine("");

                // Index
                string searchText = GetColumnText(tableColumnNames.Where(a => a.TableName == Table).ToList());

                yaz.WriteLine("\t\tpublic ViewResult Index(string conditions)");
                yaz.WriteLine("\t\t{");
                yaz.WriteLine("\t\t\treturn View();");
                yaz.WriteLine("\t\t}");
                yaz.WriteLine("");

                if (identityColumns.Count > 0)
                {
                    string columntype = tableColumnNames.Where(a => a.ColumnName == id && a.TableName == Table).FirstOrDefault().TypeName.Name.ToString();

                    // Ekle
                    yaz.WriteLine("\t\tpublic ActionResult Ekle()");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\t" + Table + " table = new " + Table + "();");
                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;

                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                    yaz.WriteLine("\t\tpublic ActionResult Ekle(" + Table + " table)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tentity." + Table + ".Add(table);");
                    yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;

                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(\"Ekle\", table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    //Duzenle
                    yaz.WriteLine("\t\t[HttpGet]");
                    yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + columntype.ReturnCSharpType() + " id)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(id);");
                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t\telse");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                    yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + Table + " table)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\t" + Table + " _table = entity." + Table + ".Find(table." + id + ");");
                    yaz.WriteLine("");

                    foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                    {
                        yaz.WriteLine("\t\t\t\t_table." + column.ColumnName + " = table." + column.ColumnName + ";");
                    }
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                    yaz.WriteLine("\t\t\t}");

                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t\telse");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(\"Duzenle\", table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    //Sil
                    yaz.WriteLine("\t\tpublic JsonResult Sil(" + columntype.ReturnCSharpType() + " conditions)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(conditions);");
                    yaz.WriteLine("\t\t\tentity." + Table + ".Remove(table);");
                    yaz.WriteLine("\t\t\tentity.SaveChanges();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\treturn Json(true);");
                    yaz.WriteLine("\t\t}");
                }

                yaz.WriteLine("\t}");
                yaz.WriteLine("}");

                yaz.Close();
            }
        }

        void CreateAngularServicesLayer()
        {
            int i = 0;

            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                identityColumns = identityColumns.IdentityCheck(lstSeciliKolonlar);

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                if (i <= 0)
                {
                    CreateAngularHomeController();

                    i++;
                }

                StreamWriter yaz = File.CreateText(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers\\" + Table + "Controller.cs");

                yaz.WriteLine("using System;");
                yaz.WriteLine("using System.Linq;");
                yaz.WriteLine("using System.Web.Mvc;");
                yaz.WriteLine("using System.Collections.Generic;");
                yaz.WriteLine("using TDLibrary;");
                yaz.WriteLine("using Models;");

                yaz.WriteLine("");
                yaz.WriteLine("namespace " + projectName + ".Areas.Ajax.Controllers");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic class " + Table + "Controller : Controller");
                yaz.WriteLine("\t{");
                yaz.WriteLine("\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                yaz.WriteLine("");

                // Index
                string searchText = GetColumnText(tableColumnNames.Where(a => a.TableName == Table).ToList());

                yaz.WriteLine("\t\tpublic ViewResult Index(string conditions)");
                yaz.WriteLine("\t\t{");
                yaz.WriteLine("\t\t\treturn View();");
                yaz.WriteLine("\t\t}");
                yaz.WriteLine("");

                if (identityColumns.Count > 0)
                {
                    string columntype = tableColumnNames.Where(a => a.ColumnName == id && a.TableName == Table).FirstOrDefault().TypeName.Name.ToString();

                    // Ekle
                    yaz.WriteLine("\t\tpublic ActionResult Ekle()");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\t" + Table + " table = new " + Table + "();");
                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;

                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                    yaz.WriteLine("\t\tpublic ActionResult Ekle(" + Table + " table)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tentity." + Table + ".Add(table);");
                    yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;

                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(\"Ekle\", table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    //Duzenle
                    yaz.WriteLine("\t\t[HttpGet]");
                    yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + columntype.ReturnCSharpType() + " id)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(id);");
                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t\telse");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                    yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + Table + " table)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\t" + Table + " _table = entity." + Table + ".Find(table." + id + ");");
                    yaz.WriteLine("");

                    foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                    {
                        yaz.WriteLine("\t\t\t\t_table." + column.ColumnName + " = table." + column.ColumnName + ";");
                    }
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                    yaz.WriteLine("\t\t\t}");

                    yaz.WriteLine("");

                    if (chkRTables.Checked == true)
                    {
                        if (fkcListForeign.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t\telse");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }
                        }
                    }

                    yaz.WriteLine("\t\t\treturn View(\"Duzenle\", table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    //Sil
                    yaz.WriteLine("\t\tpublic JsonResult Sil(" + columntype.ReturnCSharpType() + " conditions)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(conditions);");
                    yaz.WriteLine("\t\t\tentity." + Table + ".Remove(table);");
                    yaz.WriteLine("\t\t\tentity.SaveChanges();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\treturn Json(true);");
                    yaz.WriteLine("\t\t}");
                }

                yaz.WriteLine("\t}");
                yaz.WriteLine("}");

                yaz.Close();
            }
        }

        void CreateAngularTypeScriptLayer()
        {
            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                identityColumns = identityColumns.IdentityCheck(lstSeciliKolonlar);

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                CreateAngularDirectories(Table);

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToLower() + "\\index.ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("namespace " + projectName + ".Areas.Admin.Controllers");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class " + Table + "Controller : Controller");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");

                        // Index
                        string searchText = GetColumnText(tableColumnNames.Where(a => a.TableName == Table).ToList());

                        yaz.WriteLine("\t\tpublic ViewResult Index(string conditions)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn View();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        if (identityColumns.Count > 0)
                        {
                            string columntype = tableColumnNames.Where(a => a.ColumnName == id && a.TableName == Table).FirstOrDefault().TypeName.Name.ToString();

                            // Ekle
                            yaz.WriteLine("\t\tpublic ActionResult Ekle()");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t" + Table + " table = new " + Table + "();");
                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;

                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                            yaz.WriteLine("\t\tpublic ActionResult Ekle(" + Table + " table)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tentity." + Table + ".Add(table);");
                            yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;

                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(\"Ekle\", table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Duzenle
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + columntype.ReturnCSharpType() + " id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(id);");
                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t\telse");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                            yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + Table + " table)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\t" + Table + " _table = entity." + Table + ".Find(table." + id + ");");
                            yaz.WriteLine("");

                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                yaz.WriteLine("\t\t\t\t_table." + column.ColumnName + " = table." + column.ColumnName + ";");
                            }
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                            yaz.WriteLine("\t\t\t}");

                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t\telse");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(\"Duzenle\", table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Sil
                            yaz.WriteLine("\t\tpublic JsonResult Sil(" + columntype.ReturnCSharpType() + " conditions)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(conditions);");
                            yaz.WriteLine("\t\t\tentity." + Table + ".Remove(table);");
                            yaz.WriteLine("\t\t\tentity.SaveChanges();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn Json(true);");
                            yaz.WriteLine("\t\t}");
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToLower() + "\\ekle.ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("namespace " + projectName + ".Areas.Admin.Controllers");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class " + Table + "Controller : Controller");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");

                        // Index
                        string searchText = GetColumnText(tableColumnNames.Where(a => a.TableName == Table).ToList());

                        yaz.WriteLine("\t\tpublic ViewResult Index(string conditions)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn View();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        if (identityColumns.Count > 0)
                        {
                            string columntype = tableColumnNames.Where(a => a.ColumnName == id && a.TableName == Table).FirstOrDefault().TypeName.Name.ToString();

                            // Ekle
                            yaz.WriteLine("\t\tpublic ActionResult Ekle()");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t" + Table + " table = new " + Table + "();");
                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;

                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                            yaz.WriteLine("\t\tpublic ActionResult Ekle(" + Table + " table)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tentity." + Table + ".Add(table);");
                            yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;

                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(\"Ekle\", table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Duzenle
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + columntype.ReturnCSharpType() + " id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(id);");
                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t\telse");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                            yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + Table + " table)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\t" + Table + " _table = entity." + Table + ".Find(table." + id + ");");
                            yaz.WriteLine("");

                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                yaz.WriteLine("\t\t\t\t_table." + column.ColumnName + " = table." + column.ColumnName + ";");
                            }
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                            yaz.WriteLine("\t\t\t}");

                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t\telse");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(\"Duzenle\", table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Sil
                            yaz.WriteLine("\t\tpublic JsonResult Sil(" + columntype.ReturnCSharpType() + " conditions)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(conditions);");
                            yaz.WriteLine("\t\t\tentity." + Table + ".Remove(table);");
                            yaz.WriteLine("\t\t\tentity.SaveChanges();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn Json(true);");
                            yaz.WriteLine("\t\t}");
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToLower() + "\\duzenle.ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("namespace " + projectName + ".Areas.Admin.Controllers");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class " + Table + "Controller : Controller");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");

                        // Index
                        string searchText = GetColumnText(tableColumnNames.Where(a => a.TableName == Table).ToList());

                        yaz.WriteLine("\t\tpublic ViewResult Index(string conditions)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn View();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        if (identityColumns.Count > 0)
                        {
                            string columntype = tableColumnNames.Where(a => a.ColumnName == id && a.TableName == Table).FirstOrDefault().TypeName.Name.ToString();

                            // Ekle
                            yaz.WriteLine("\t\tpublic ActionResult Ekle()");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t" + Table + " table = new " + Table + "();");
                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;

                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                            yaz.WriteLine("\t\tpublic ActionResult Ekle(" + Table + " table)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tentity." + Table + ".Add(table);");
                            yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;

                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (" + PrimaryTableName + " item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + " });");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(\"Ekle\", table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Duzenle
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + columntype.ReturnCSharpType() + " id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(id);");
                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t\telse");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t[HttpPost, ValidateInput(false)]");
                            yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + Table + " table)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\t" + Table + " _table = entity." + Table + ".Find(table." + id + ");");
                            yaz.WriteLine("");

                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                yaz.WriteLine("\t\t\t\t_table." + column.ColumnName + " = table." + column.ColumnName + ";");
                            }
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\tentity.SaveChanges();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn RedirectToAction(\"Index\");");
                            yaz.WriteLine("\t\t\t}");

                            yaz.WriteLine("");

                            if (chkRTables.Checked == true)
                            {
                                if (fkcListForeign.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                        yaz.WriteLine("\t\t\tList<" + PrimaryTableName + "> table" + PrimaryTableName + " = entity." + PrimaryTableName + ".ToList();");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (var item in table" + PrimaryTableName + ")");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\tif(item." + fkc.PrimaryColumnName + " == table." + fkc.ForeignColumnName + ")");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = true });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t\telse");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List.Add(new SelectListItem() { Value = item." + fkc.PrimaryColumnName + ".ToString(), Text = item." + columnText + ", Selected = false });");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(\"Duzenle\", table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Sil
                            yaz.WriteLine("\t\tpublic JsonResult Sil(" + columntype.ReturnCSharpType() + " conditions)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(conditions);");
                            yaz.WriteLine("\t\t\tentity." + Table + ".Remove(table);");
                            yaz.WriteLine("\t\t\tentity.SaveChanges();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn Json(true);");
                            yaz.WriteLine("\t\t}");
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }
        }

        void CreateAngularHomePage()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Views\\Home\\Index.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@{");
                    yaz.WriteLine("\tViewBag.Title = \"" + projectName + " Ana Sayfa\";");
                    yaz.WriteLine("\tLayout = \"~/Views/Shared/_Layout.cshtml\";");
                    yaz.WriteLine("}");
                    yaz.WriteLine("");
                    yaz.WriteLine("<" + projectName.Substring(0, 3).ToLower() + "-app></" + projectName.Substring(0, 3).ToLower() + "-app>");
                    yaz.Close();
                }
            }
        }

        void CreateAngularHomeController()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Controllers\\HomeController.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using System;");
                    yaz.WriteLine("using System.Collections.Generic;");
                    yaz.WriteLine("using System.Linq;");
                    yaz.WriteLine("using System.Web;");
                    yaz.WriteLine("using System.Web.Mvc;");
                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName + ".Controllers");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic class HomeController : Controller");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpublic ActionResult Index()");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\treturn View();");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }
        }

        #endregion

        #region Common

        void CreateWebConfig()
        {
            string wcKullanici = String.IsNullOrEmpty(txtWCKullanici.Text) ? "user" : txtWCKullanici.Text;
            string wcSifre = String.IsNullOrEmpty(txtWCSifre.Text) ? "123456" : txtWCSifre.Text;

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Web.config.txt", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("\t\t<add key=\"SystemUser\" value=\"admin\" />");
                    yaz.WriteLine("\t\t<add key=\"MainPath\" value=\"http://localhost/" + projectName + "\" />");
                    yaz.WriteLine("\t\t<add key=\"ScriptPath\" value=\"/Content/js\" />");
                    yaz.WriteLine("\t\t<add key=\"StylePath\" value=\"/Content/css\" />");
                    yaz.WriteLine("\t\t<add key=\"ImagePath\" value=\"/Content/img\" />");
                    yaz.WriteLine("\t\t<add key=\"AjaxPath\" value=\"/Ajax\" />");
                    yaz.WriteLine("\t\t<add key=\"AdminPath\" value=\"http://localhost/" + projectName + "/Admin\" />");
                    yaz.WriteLine("\t\t<add key=\"AdminScriptPath\" value=\"/Content/admin/js\" />");
                    yaz.WriteLine("\t\t<add key=\"AdminStylePath\" value=\"/Content/admin/css\" />");
                    yaz.WriteLine("\t\t<add key=\"AdminImagePath\" value=\"/Content/admin/img\" />");
                    yaz.WriteLine("\t\t<add key=\"AdminAjaxPath\" value=\"/Ajax/Ajax\" />");
                    yaz.WriteLine("\t\t<add key=\"UploadPath\" value=\"/Uploads\" />");
                    yaz.WriteLine("\t\t<add key=\"MaxFileSize\" value=\"1024000\" />");
                    yaz.WriteLine("\t\t<add key=\"MaxPictureSize\" value=\"1024000\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<system.webServer>");
                    yaz.WriteLine("\t\t<validation validateIntegratedModeConfiguration=\"false\"/>");
                    yaz.WriteLine("\t\t<modules runAllManagedModulesForAllRequests=\"true\"/>");
                    yaz.WriteLine("\t</system.webServer>");

                    if (chkMVCWcfServis.Checked || chkMVCHepsi.Checked)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\t<system.serviceModel>");
                        yaz.WriteLine("\t\t<behaviors>");
                        yaz.WriteLine("\t\t\t<serviceBehaviors>");
                        yaz.WriteLine("\t\t\t\t<behavior name=\"\">");
                        yaz.WriteLine("\t\t\t\t\t<serviceMetadata httpGetEnabled=\"true\" />");
                        yaz.WriteLine("\t\t\t\t\t<serviceDebug includeExceptionDetailInFaults=\"false\" />");
                        yaz.WriteLine("\t\t\t\t</behavior>");
                        yaz.WriteLine("\t\t\t</serviceBehaviors>");
                        yaz.WriteLine("\t\t</behaviors>");
                        yaz.WriteLine("\t\t<serviceHostingEnvironment multipleSiteBindingsEnabled=\"true\" minFreeMemoryPercentageToActivateService=\"0\" />");
                        yaz.WriteLine("\t\t<services>");

                        foreach (string table in selectedTables)
                        {
                            yaz.WriteLine("\t\t\t<service name=\"" + projectName + ".Servis." + table + "Service\">");
                            yaz.WriteLine("\t\t\t\t<endpoint kind=\"webHttpEndpoint\" contract=\"" + projectName + ".Servis.I" + table + "Service\" />");
                            yaz.WriteLine("\t\t\t</service>");
                        }

                        yaz.WriteLine("\t\t</services>");
                        yaz.WriteLine("\t</system.serviceModel>");
                    }

                    yaz.Close();
                }
            }
        }

        void CreateStoredProcedure()
        {
            string schema = DefaultSchema(new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo)));

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\StoredProcedures.sql",
                FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("USE [" + DBName + "]");
                    yaz.WriteLine("GO");
                    yaz.WriteLine("");

                    foreach (string Table in selectedTables)
                    {
                        string idColumn = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table).FirstOrDefault();
                        string[] dizi = new string[lstSeciliKolonlar.Items.Count];
                        string[] diziML = new string[] { "nvarchar", "varchar", "binary", "char", "nchar", "varbinary" };

                        int i = 0;
                        foreach (string item in lstSeciliKolonlar.Items)
                        {
                            dizi[i] = item.Replace(" [" + Table + "]", "");
                            i++;
                        }

                        List<ColumnInfo> columnNames = Helper.Helper.ColumnNames(connectionInfo, Table).Where(a => a.ColumnName.In(dizi)).ToList();

                        string idType = null;

                        try
                        {
                            idType = columnNames.Where(a => a.ColumnName == idColumn).FirstOrDefault().DataType;
                        }
                        catch
                        {
                        }

                        //Select//
                        yaz.WriteLine("/* Select */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Select]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Select]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");

                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Select]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType);
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        string sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            sqlText += "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE ([" + idColumn + "] = @" + idColumn + " OR @" + idColumn + " IS NULL)");
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Select//

                        //SelectTop//
                        yaz.WriteLine("/* SelectTop */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "SelectTop]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "SelectTop]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");

                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "SelectTop]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType + ",");
                        }
                        yaz.WriteLine("\t@Top int");

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        sqlText = "\tSELECT Top (@Top) ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            sqlText += "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE ([" + idColumn + "] = @" + idColumn + " OR @" + idColumn + " IS NULL)");
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //SelectTop//

                        ////Insert//
                        yaz.WriteLine("/* Insert */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Insert]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Insert]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");

                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Insert]");

                        i = 1;
                        foreach (ColumnInfo column in columnNames)
                        {
                            if (column.IsIdentity == "NO")
                            {
                                string extra = "";

                                if (column.DataType.In(diziML))
                                    extra += column.MaxLength != "" ? "(" + column.MaxLength + ")" : "";

                                extra += column.IsNullable != "NO" ? " = NULL" : "";

                                if (i != columnNames.Count)
                                    yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd() + ",");
                                else
                                    yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd());
                            }

                            i++;
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("");

                        sqlText = "\tINSERT INTO " + schema + ".[" + Table + "] (";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (column.IsIdentity == "NO")
                            {
                                sqlText = sqlText + "[" + column.ColumnName + "],";
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText + ")";

                        yaz.WriteLine(sqlText);

                        sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (column.IsIdentity == "NO")
                            {
                                sqlText = sqlText + "@" + column.ColumnName + ",";
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);

                        yaz.WriteLine("");
                        yaz.WriteLine("\tSELECT cast(@@IDENTITY as int)");
                        yaz.WriteLine("END;");
                        yaz.WriteLine("");
                        ////Insert//

                        ////Update//
                        yaz.WriteLine("/* Update */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Update]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Update]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");

                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Update]");

                        i = 1;
                        foreach (ColumnInfo column in columnNames)
                        {
                            string extra = "";

                            if (column.DataType.In(diziML))
                                extra += column.MaxLength != "" ? "(" + column.MaxLength + ")" : "";

                            extra += column.IsNullable != "NO" ? " = NULL" : "";

                            if (i != columnNames.Count)
                                yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd() + ",");
                            else
                                yaz.WriteLine("\t@" + column.ColumnName + " " + column.DataType + extra.TrimEnd());

                            i++;
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");


                        yaz.WriteLine("\tUPDATE " + schema + ".[" + Table + "]");

                        sqlText = "\tSET ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (column.IsIdentity == "NO")
                            {
                                sqlText = sqlText + "[" + column.ColumnName + "] = @" + column.ColumnName + ",";
                            }
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);

                        yaz.WriteLine(sqlText);

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                        }

                        yaz.WriteLine("");

                        sqlText = "\tSELECT ";

                        foreach (ColumnInfo column in columnNames)
                        {
                            sqlText = sqlText + "[" + column.ColumnName + "],";
                        }

                        sqlText = sqlText.Remove(sqlText.Length - 1);
                        sqlText = sqlText.Replace(",", ", ");

                        yaz.WriteLine(sqlText);

                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        ////Update//

                        //Delete//
                        yaz.WriteLine("/* Delete */");
                        yaz.WriteLine("IF OBJECT_ID('" + schema + ".[usp_" + Table + "Delete]') IS NOT NULL");
                        yaz.WriteLine("BEGIN");
                        yaz.WriteLine("\tDROP PROC " + schema + ".[usp_" + Table + "Delete]");
                        yaz.WriteLine("END");
                        yaz.WriteLine("GO");

                        yaz.WriteLine("CREATE PROC " + schema + ".[usp_" + Table + "Delete]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\t@" + idColumn + " " + idType);
                        }

                        yaz.WriteLine("AS");
                        yaz.WriteLine("\tSET NOCOUNT ON");
                        yaz.WriteLine("\tSET XACT_ABORT ON");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tBEGIN TRAN");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tDELETE");
                        yaz.WriteLine("\tFROM " + schema + ".[" + Table + "]");

                        if (idType != null)
                        {
                            yaz.WriteLine("\tWHERE [" + idColumn + "] = @" + idColumn);
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tCOMMIT");
                        yaz.WriteLine("GO");
                        yaz.WriteLine("");
                        //Delete//
                    }

                    yaz.Close();
                }
            }
        }

        void CreateStilScript()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\js\\pathscript.js", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("var MainPath = null;");
                    yaz.WriteLine("var ScriptPath = null;");
                    yaz.WriteLine("var StylePath = null;");
                    yaz.WriteLine("var ImagePath = null;");
                    yaz.WriteLine("var AjaxPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var AdminPath = null;");
                    yaz.WriteLine("var AdminScriptPath = null;");
                    yaz.WriteLine("var AdminStylePath = null;");
                    yaz.WriteLine("var AdminImagePath = null;");
                    yaz.WriteLine("var AdminAjaxPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var SystemUser = \"admin\";");
                    yaz.WriteLine("var UploadPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var Lang = null;");
                    yaz.WriteLine("var UserID = null;");
                    yaz.WriteLine("var Url = null;");
                    yaz.WriteLine("var Urling = new Object();");
                    yaz.WriteLine("");
                    yaz.WriteLine("$(document).ready(function () {");
                    yaz.WriteLine("\tMainPath = \"http://localhost/" + projectName + "\";");
                    yaz.WriteLine("\tScriptPath = MainPath + \"/Content/js\";");
                    yaz.WriteLine("\tStylePath = MainPath + \"/Content/css\";");
                    yaz.WriteLine("\tImagePath = MainPath + \"/Content/img\";");
                    yaz.WriteLine("\tAjaxPath = MainPath + \"/Ajax\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tAdminPath = \"http://localhost/" + projectName + "/Admin\";");
                    yaz.WriteLine("\tAdminScriptPath = MainPath + \"/Content/admin/js\";");
                    yaz.WriteLine("\tAdminStylePath = MainPath + \"/Content/admin/css\";");
                    yaz.WriteLine("\tAdminImagePath = MainPath + \"/Content/admin/img\";");
                    yaz.WriteLine("\tAdminAjaxPath = MainPath + \"/Ajax/Ajax\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tUploadPath = MainPath + \"/Uploads\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tLang = $(\"#hdnLang\").val();");
                    yaz.WriteLine("\tUrl = $(\"#hdnUrl\").val();");
                    yaz.WriteLine("\tUserID = $(\"#hdnUserID\").val();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tif (Url != undefined) {");
                    yaz.WriteLine("\t\tvar tempurl = Url.replace(AdminPath + \"/\", \"\");");
                    yaz.WriteLine("\t\tvar extParams = tempurl.split('?')[1];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\ttempurl = tempurl.replace(\"?\" + extParams, \"\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tUrling.path = tempurl;");
                    yaz.WriteLine("\t\tUrling.controller = tempurl.split('/')[0];");
                    yaz.WriteLine("\t\tUrling.action = tempurl.split('/')[1];");
                    yaz.WriteLine("\t\tUrling.parameter = tempurl.split('/')[2];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (extParams != undefined)");
                    yaz.WriteLine("\t\t\tUrling.parameters = extParams.split('&');");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("});");

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\js\\main.js", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("$(document).ready(function() {");
                    yaz.WriteLine("");
                    yaz.WriteLine("});");
                    yaz.WriteLine("");
                    yaz.WriteLine("$(function () {");
                    yaz.WriteLine("");
                    yaz.WriteLine("});");

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\pathscript.js", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("var MainPath = null;");
                    yaz.WriteLine("var ScriptPath = null;");
                    yaz.WriteLine("var StylePath = null;");
                    yaz.WriteLine("var ImagePath = null;");
                    yaz.WriteLine("var AjaxPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var AdminPath = null;");
                    yaz.WriteLine("var AdminScriptPath = null;");
                    yaz.WriteLine("var AdminStylePath = null;");
                    yaz.WriteLine("var AdminImagePath = null;");
                    yaz.WriteLine("var AdminAjaxPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var SystemUser = \"admin\";");
                    yaz.WriteLine("var UploadPath = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("var Lang = null;");
                    yaz.WriteLine("var UserID = null;");
                    yaz.WriteLine("var Url = null;");
                    yaz.WriteLine("var Urling = new Object();");
                    yaz.WriteLine("");
                    yaz.WriteLine("$(document).ready(function () {");
                    yaz.WriteLine("\tMainPath = \"http://localhost/" + projectName + "\";");
                    yaz.WriteLine("\tScriptPath = MainPath + \"/Content/js\";");
                    yaz.WriteLine("\tStylePath = MainPath + \"/Content/css\";");
                    yaz.WriteLine("\tImagePath = MainPath + \"/Content/img\";");
                    yaz.WriteLine("\tAjaxPath = MainPath + \"/Ajax\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tAdminPath = \"http://localhost/" + projectName + "/Admin\";");
                    yaz.WriteLine("\tAdminScriptPath = MainPath + \"/Content/admin/js\";");
                    yaz.WriteLine("\tAdminStylePath = MainPath + \"/Content/admin/css\";");
                    yaz.WriteLine("\tAdminImagePath = MainPath + \"/Content/admin/img\";");
                    yaz.WriteLine("\tAdminAjaxPath = MainPath + \"/Ajax/Ajax\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tUploadPath = MainPath + \"/Uploads\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tLang = $(\"#hdnLang\").val();");
                    yaz.WriteLine("\tUrl = $(\"#hdnUrl\").val();");
                    yaz.WriteLine("\tUserID = $(\"#hdnUserID\").val();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tif (Url != undefined) {");
                    yaz.WriteLine("\t\tvar tempurl = Url.replace(AdminPath + \"/\", \"\");");
                    yaz.WriteLine("\t\tvar extParams = tempurl.split('?')[1];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\ttempurl = tempurl.replace(\"?\" + extParams, \"\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tUrling.path = tempurl;");
                    yaz.WriteLine("\t\tUrling.controller = tempurl.split('/')[0];");
                    yaz.WriteLine("\t\tUrling.action = tempurl.split('/')[1];");
                    yaz.WriteLine("\t\tUrling.parameter = tempurl.split('/')[2];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (extParams != undefined)");
                    yaz.WriteLine("\t\t\tUrling.parameters = extParams.split('&');");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("});");

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\main.js", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("$(document).ready(function() {");
                    yaz.WriteLine("");
                    yaz.WriteLine("});");
                    yaz.WriteLine("");
                    yaz.WriteLine("$(function () {");
                    yaz.WriteLine("");
                    yaz.WriteLine("});");

                    yaz.Close();
                }
            }

            CreateJquery();

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\css\\main.css", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("* {outline: none;}");
                    yaz.WriteLine("body {margin:0px 0px 0px 0px;font-family: Verdana;font-size: 12px;color: black;height:100%;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("img {border: none;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("a {font-family: Verdana;font-size: 12px;color: black;text-decoration: none;cursor:pointer;}");
                    yaz.WriteLine("a:hover {color: gray;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("ul {list-style-type:none;padding: 0px 0px 0px 0px;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("select {border: 1px solid lightgray;}");
                    yaz.WriteLine("input[type=text] {border: 1px solid lightgray;}");
                    yaz.WriteLine("input[type=password] {border: 1px solid lightgray;}");
                    yaz.WriteLine("textarea {border: 1px solid lightgray;}");
                    yaz.WriteLine("input[type=button] {border:1px solid lightgray;cursor: pointer;}");
                    yaz.WriteLine("input[type=submit] {border:1px solid lightgray;cursor: pointer;}");
                    yaz.WriteLine("input[type=checkbox] {cursor:pointer;}");
                    yaz.WriteLine("");
                    yaz.WriteLine(".clear {clear:both;}");

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\main.css", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("* {outline: none;}");
                    yaz.WriteLine("body {margin:0px 0px 0px 0px;font-family: Verdana;font-size: 12px;color: black;height:100%;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("img {border: none;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("a {font-family: Verdana;font-size: 12px;color: black;text-decoration: none;cursor:pointer;}");
                    yaz.WriteLine("a:hover {color: gray;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("input[type=checkbox] {cursor:pointer;}");
                    yaz.WriteLine("");
                    yaz.WriteLine(".clear {clear:both;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("ul {list-style-type:none;padding: 0px 0px 0px 0px;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("ul.topmenu {float: left;margin: 20px 0px 0px 20px;}");
                    yaz.WriteLine("ul.topmenu li {float: left;margin: 0px 0px 20px 0px;}");
                    yaz.WriteLine("ul.topmenu li a {padding: 5px 10px 5px 10px;background-color:#0A246A;color:#fff;border: 1px solid #0A246A;margin-left: -1px;}");
                    yaz.WriteLine("ul.topmenu li a:hover {background-color:#fff;color:#0A246A;}");
                    yaz.WriteLine("ul.topmenu li a:active {background-color:#000ad9;color:#fff;}");
                    yaz.WriteLine("ul.topmenu li a.website {background-color:#732794;border-color: #732794;margin-left: 5px;}");
                    yaz.WriteLine("ul.topmenu li a.website:hover {background-color:#fff;color:#732794;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("ul.mainmenu {float: left;margin: 20px 0px 0px 20px;width: 840px;}");
                    yaz.WriteLine("ul.mainmenu li {float: left;margin: 0px 0px 0px 0px;width: 200px;padding: 5px;}");
                    yaz.WriteLine("ul.mainmenu li a {padding: 5px 5px 5px 5px;background-color:#0A246A;color:#fff;border: 1px solid #0A246A;margin-left: -1px;width: 188px;");
                    yaz.WriteLine("height: 78px;float: left;text-align: center;line-height: 80px;vertical-align: middle;white-space: nowrap;overflow: hidden;");
                    yaz.WriteLine("text-overflow: ellipsis;font-size: 20px;}");
                    yaz.WriteLine("ul.mainmenu li a:hover {background-color:#fff;color:#0A246A;}");
                    yaz.WriteLine("ul.mainmenu li a:active {background-color:#000ad9;color:#fff;}");
                    yaz.WriteLine("");
                    yaz.WriteLine(".content {float: left;margin: 20px 0px 0px 20px;}");
                    yaz.WriteLine("");
                    yaz.WriteLine("fieldset {border: 1px solid #0A246A!important;min-width: 600px;}");
                    yaz.WriteLine("fieldset legend {color: #0A246A;font-size:18px;}");
                    yaz.WriteLine("fieldset select {border: 1px solid #0A246A;width: 598px;padding: 5px 5px 5px 5px;}");
                    yaz.WriteLine("fieldset input[type=text] {border: 1px solid #0A246A;width: 588px;padding: 5px 5px 5px 5px;}");
                    yaz.WriteLine("fieldset input[type=password] {border: 1px solid #0A246A;width: 588px;padding: 5px 5px 5px 5px;}");
                    yaz.WriteLine("fieldset input[type=number] {border: 1px solid #0A246A;width: 588px;padding: 5px 5px 5px 5px;}");
                    yaz.WriteLine("fieldset input[type=datetime] {border: 1px solid #0A246A;width: 588px;padding: 5px 5px 5px 5px;}");
                    yaz.WriteLine("fieldset textarea {border: 1px solid #0A246A;width: 588px;height: 100px;}");
                    yaz.WriteLine("fieldset input[type=button] {border: 1px solid #0A246A;background-color: #0A246A;color: #fff;padding: 5px 10px 5px 10px;cursor: pointer;}");
                    yaz.WriteLine("fieldset input[type=button]:hover {background-color:#fff;color:#0A246A;}");
                    yaz.WriteLine("fieldset input[type=button]:active {background-color:#000ad9;color:#fff;}");
                    yaz.WriteLine("fieldset input[type=submit] {border: 1px solid #0A246A;background-color: #0A246A;color: #fff;padding: 5px 10px 5px 10px;cursor: pointer;}");
                    yaz.WriteLine("fieldset input[type=submit]:hover {background-color:#fff;color:#0A246A;}");
                    yaz.WriteLine("fieldset input[type=submit]:active {background-color:#000ad9;color:#fff;}");
                    yaz.WriteLine("fieldset .editor-label, fieldset .display-label {font-size: 14px;color: #0A246A;margin: 10px 0px 5px 0px;float: left;}");
                    yaz.WriteLine("");
                    yaz.WriteLine(".pagelinks {float: left;margin: 10px 0px 10px 0px;}");
                    yaz.WriteLine(".pagelinks a {font-family: Verdana;font-size: 13px;color: #0A246A;text-decoration: none;cursor:pointer;float: left;margin: 0px 0px 0px 10px;}");
                    yaz.WriteLine(".pagelinks a:hover {color: #000ad9;}");
                    yaz.WriteLine(".pagelinks a:active {color: #373fdc;}");

                    yaz.Close();
                }
            }
        }

        void CreateJquery()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery\\jquery.min.js", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("/*! jQuery v1.10.2 | (c) 2005, 2013 jQuery Foundation, Inc. | jquery.org/license");
                    yaz.WriteLine("//@ sourceMappingURL=jquery.min.map");
                    yaz.WriteLine("*/");
                    yaz.WriteLine("(function(e,t){var n,r,i=typeof t,o=e.location,a=e.document,s=a.documentElement,l=e.jQuery,u=e.$,c={},p=[],f=\"1.10.2\",d=p.concat,h=p.push,g=p.slice,m=p.indexOf,y=c.toString,v=c.hasOwnProperty,b=f.trim,x=function(e,t){return new x.fn.init(e,t,r)},w=/[+-]?(?:\\d*\\.|)\\d+(?:[eE][+-]?\\d+|)/.source,T=/\\S+/g,C=/^[\\s\\uFEFF\\xA0]+|[\\s\\uFEFF\\xA0]+$/g,N=/^(?:\\s*(<[\\w\\W]+>)[^>]*|#([\\w-]*))$/,k=/^<(\\w+)\\s*\\/?>(?:<\\/\\1>|)$/,E=/^[\\],:{}\\s]*$/,S=/(?:^|:|,)(?:\\s*\\[)+/g,A=/\\\\(?:[\"\\\\\\/bfnrt]|u[\\da-fA-F]{4})/g,j=/\"[^\"\\\\\\r\\n]*\"|true|false|null|-?(?:\\d+\\.|)\\d+(?:[eE][+-]?\\d+|)/g,D=/^-ms-/,L=/-([\\da-z])/gi,H=function(e,t){return t.toUpperCase()},q=function(e){(a.addEventListener||\"load\"===e.type||\"complete\"===a.readyState)&&(_(),x.ready())},_=function(){a.addEventListener?(a.removeEventListener(\"DOMContentLoaded\",q,!1),e.removeEventListener(\"load\",q,!1)):(a.detachEvent(\"onreadystatechange\",q),e.detachEvent(\"onload\",q))};x.fn=x.prototype={jquery:f,constructor:x,init:function(e,n,r){var i,o;if(!e)return this;if(\"string\"==typeof e){if(i=\"<\"===e.charAt(0)&&\">\"===e.charAt(e.length-1)&&e.length>=3?[null,e,null]:N.exec(e),!i||!i[1]&&n)return!n||n.jquery?(n||r).find(e):this.constructor(n).find(e);if(i[1]){if(n=n instanceof x?n[0]:n,x.merge(this,x.parseHTML(i[1],n&&n.nodeType?n.ownerDocument||n:a,!0)),k.test(i[1])&&x.isPlainObject(n))for(i in n)x.isFunction(this[i])?this[i](n[i]):this.attr(i,n[i]);return this}if(o=a.getElementById(i[2]),o&&o.parentNode){if(o.id!==i[2])return r.find(e);this.length=1,this[0]=o}return this.context=a,this.selector=e,this}return e.nodeType?(this.context=this[0]=e,this.length=1,this):x.isFunction(e)?r.ready(e):(e.selector!==t&&(this.selector=e.selector,this.context=e.context),x.makeArray(e,this))},selector:\"\",length:0,toArray:function(){return g.call(this)},get:function(e){return null==e?this.toArray():0>e?this[this.length+e]:this[e]},pushStack:function(e){var t=x.merge(this.constructor(),e);return t.prevObject=this,t.context=this.context,t},each:function(e,t){return x.each(this,e,t)},ready:function(e){return x.ready.promise().done(e),this},slice:function(){return this.pushStack(g.apply(this,arguments))},first:function(){return this.eq(0)},last:function(){return this.eq(-1)},eq:function(e){var t=this.length,n=+e+(0>e?t:0);return this.pushStack(n>=0&&t>n?[this[n]]:[])},map:function(e){return this.pushStack(x.map(this,function(t,n){return e.call(t,n,t)}))},end:function(){return this.prevObject||this.constructor(null)},push:h,sort:[].sort,splice:[].splice},x.fn.init.prototype=x.fn,x.extend=x.fn.extend=function(){var e,n,r,i,o,a,s=arguments[0]||{},l=1,u=arguments.length,c=!1;for(\"boolean\"==typeof s&&(c=s,s=arguments[1]||{},l=2),\"object\"==typeof s||x.isFunction(s)||(s={}),u===l&&(s=this,--l);u>l;l++)if(null!=(o=arguments[l]))for(i in o)e=s[i],r=o[i],s!==r&&(c&&r&&(x.isPlainObject(r)||(n=x.isArray(r)))?(n?(n=!1,a=e&&x.isArray(e)?e:[]):a=e&&x.isPlainObject(e)?e:{},s[i]=x.extend(c,a,r)):r!==t&&(s[i]=r));return s},x.extend({expando:\"jQuery\"+(f+Math.random()).replace(/\\D/g,\"\"),noConflict:function(t){return e.$===x&&(e.$=u),t&&e.jQuery===x&&(e.jQuery=l),x},isReady:!1,readyWait:1,holdReady:function(e){e?x.readyWait++:x.ready(!0)},ready:function(e){if(e===!0?!--x.readyWait:!x.isReady){if(!a.body)return setTimeout(x.ready);x.isReady=!0,e!==!0&&--x.readyWait>0||(n.resolveWith(a,[x]),x.fn.trigger&&x(a).trigger(\"ready\").off(\"ready\"))}},isFunction:function(e){return\"function\"===x.type(e)},isArray:Array.isArray||function(e){return\"array\"===x.type(e)},isWindow:function(e){return null!=e&&e==e.window},isNumeric:function(e){return!isNaN(parseFloat(e))&&isFinite(e)},type:function(e){return null==e?e+\"\":\"object\"==typeof e||\"function\"==typeof e?c[y.call(e)]||\"object\":typeof e},isPlainObject:function(e){var n;if(!e||\"object\"!==x.type(e)||e.nodeType||x.isWindow(e))return!1;try{if(e.constructor&&!v.call(e,\"constructor\")&&!v.call(e.constructor.prototype,\"isPrototypeOf\"))return!1}catch(r){return!1}if(x.support.ownLast)for(n in e)return v.call(e,n);for(n in e);return n===t||v.call(e,n)},isEmptyObject:function(e){var t;for(t in e)return!1;return!0},error:function(e){throw Error(e)},parseHTML:function(e,t,n){if(!e||\"string\"!=typeof e)return null;\"boolean\"==typeof t&&(n=t,t=!1),t=t||a;var r=k.exec(e),i=!n&&[];return r?[t.createElement(r[1])]:(r=x.buildFragment([e],t,i),i&&x(i).remove(),x.merge([],r.childNodes))},parseJSON:function(n){return e.JSON&&e.JSON.parse?e.JSON.parse(n):null===n?n:\"string\"==typeof n&&(n=x.trim(n),n&&E.test(n.replace(A,\"@\").replace(j,\"]\").replace(S,\"\")))?Function(\"return \"+n)():(x.error(\"Invalid JSON: \"+n),t)},parseXML:function(n){var r,i;if(!n||\"string\"!=typeof n)return null;try{e.DOMParser?(i=new DOMParser,r=i.parseFromString(n,\"text/xml\")):(r=new ActiveXObject(\"Microsoft.XMLDOM\"),r.async=\"false\",r.loadXML(n))}catch(o){r=t}return r&&r.documentElement&&!r.getElementsByTagName(\"parsererror\").length||x.error(\"Invalid XML: \"+n),r},noop:function(){},globalEval:function(t){t&&x.trim(t)&&(e.execScript||function(t){e.eval.call(e,t)})(t)},camelCase:function(e){return e.replace(D,\"ms-\").replace(L,H)},nodeName:function(e,t){return e.nodeName&&e.nodeName.toLowerCase()===t.toLowerCase()},each:function(e,t,n){var r,i=0,o=e.length,a=M(e);if(n){if(a){for(;o>i;i++)if(r=t.apply(e[i],n),r===!1)break}else for(i in e)if(r=t.apply(e[i],n),r===!1)break}else if(a){for(;o>i;i++)if(r=t.call(e[i],i,e[i]),r===!1)break}else for(i in e)if(r=t.call(e[i],i,e[i]),r===!1)break;return e},trim:b&&!b.call(\"\\ufeff\\u00a0\")?function(e){return null==e?\"\":b.call(e)}:function(e){return null==e?\"\":(e+\"\").replace(C,\"\")},makeArray:function(e,t){var n=t||[];return null!=e&&(M(Object(e))?x.merge(n,\"string\"==typeof e?[e]:e):h.call(n,e)),n},inArray:function(e,t,n){var r;if(t){if(m)return m.call(t,e,n);for(r=t.length,n=n?0>n?Math.max(0,r+n):n:0;r>n;n++)if(n in t&&t[n]===e)return n}return-1},merge:function(e,n){var r=n.length,i=e.length,o=0;if(\"number\"==typeof r)for(;r>o;o++)e[i++]=n[o];else while(n[o]!==t)e[i++]=n[o++];return e.length=i,e},grep:function(e,t,n){var r,i=[],o=0,a=e.length;for(n=!!n;a>o;o++)r=!!t(e[o],o),n!==r&&i.push(e[o]);return i},map:function(e,t,n){var r,i=0,o=e.length,a=M(e),s=[];if(a)for(;o>i;i++)r=t(e[i],i,n),null!=r&&(s[s.length]=r);else for(i in e)r=t(e[i],i,n),null!=r&&(s[s.length]=r);return d.apply([],s)},guid:1,proxy:function(e,n){var r,i,o;return\"string\"==typeof n&&(o=e[n],n=e,e=o),x.isFunction(e)?(r=g.call(arguments,2),i=function(){return e.apply(n||this,r.concat(g.call(arguments)))},i.guid=e.guid=e.guid||x.guid++,i):t},access:function(e,n,r,i,o,a,s){var l=0,u=e.length,c=null==r;if(\"object\"===x.type(r)){o=!0;for(l in r)x.access(e,n,l,r[l],!0,a,s)}else if(i!==t&&(o=!0,x.isFunction(i)||(s=!0),c&&(s?(n.call(e,i),n=null):(c=n,n=function(e,t,n){return c.call(x(e),n)})),n))for(;u>l;l++)n(e[l],r,s?i:i.call(e[l],l,n(e[l],r)));return o?e:c?n.call(e):u?n(e[0],r):a},now:function(){return(new Date).getTime()},swap:function(e,t,n,r){var i,o,a={};for(o in t)a[o]=e.style[o],e.style[o]=t[o];i=n.apply(e,r||[]);for(o in t)e.style[o]=a[o];return i}}),x.ready.promise=function(t){if(!n)if(n=x.Deferred(),\"complete\"===a.readyState)setTimeout(x.ready);else if(a.addEventListener)a.addEventListener(\"DOMContentLoaded\",q,!1),e.addEventListener(\"load\",q,!1);else{a.attachEvent(\"onreadystatechange\",q),e.attachEvent(\"onload\",q);var r=!1;try{r=null==e.frameElement&&a.documentElement}catch(i){}r&&r.doScroll&&function o(){if(!x.isReady){try{r.doScroll(\"left\")}catch(e){return setTimeout(o,50)}_(),x.ready()}}()}return n.promise(t)},x.each(\"Boolean Number String Function Array Date RegExp Object Error\".split(\" \"),function(e,t){c[\"[object \"+t+\"]\"]=t.toLowerCase()});function M(e){var t=e.length,n=x.type(e);return x.isWindow(e)?!1:1===e.nodeType&&t?!0:\"array\"===n||\"function\"!==n&&(0===t||\"number\"==typeof t&&t>0&&t-1 in e)}r=x(a),function(e,t){var n,r,i,o,a,s,l,u,c,p,f,d,h,g,m,y,v,b=\"sizzle\"+-new Date,w=e.document,T=0,C=0,N=st(),k=st(),E=st(),S=!1,A=function(e,t){return e===t?(S=!0,0):0},j=typeof t,D=1<<31,L={}.hasOwnProperty,H=[],q=H.pop,_=H.push,M=H.push,O=H.slice,F=H.indexOf||function(e){var t=0,n=this.length;for(;n>t;t++)if(this[t]===e)return t;return-1},B=\"checked|selected|async|autofocus|autoplay|controls|defer|disabled|hidden|ismap|loop|multiple|open|readonly|required|scoped\",P=\"[\\\\x20\\\\t\\\\r\\\\n\\\\f]\",R=\"(?:\\\\\\\\.|[\\\\w-]|[^\\\\x00-\\\\xa0])+\",W=R.replace(\"w\",\"w#\"),$=\"\\\\[\"+P+\"*(\"+R+\")\"+P+\"*(?:([*^$|!~]?=)\"+P+\"*(?:([\'\\\"])((?:\\\\\\\\.|[^\\\\\\\\])*?)\\\\3|(\"+W+\")|)|)\"+P+\"*\\\\]\",I=\":(\"+R+\")(?:\\\\((([\'\\\"])((?:\\\\\\\\.|[^\\\\\\\\])*?)\\\\3|((?:\\\\\\\\.|[^\\\\\\\\()[\\\\]]|\"+$.replace(3,8)+\")*)|.*)\\\\)|)\",z=RegExp(\"^\"+P+\"+|((?:^|[^\\\\\\\\])(?:\\\\\\\\.)*)\"+P+\"+$\",\"g\"),X=RegExp(\"^\"+P+\"*,\"+P+\"*\"),U=RegExp(\"^\"+P+\"*([>+~]|\"+P+\")\"+P+\"*\"),V=RegExp(P+\"*[+~]\"),Y=RegExp(\"=\"+P+\"*([^\\\\]\'\\\"]*)\"+P+\"*\\\\]\",\"g\"),J=RegExp(I),G=RegExp(\"^\"+W+\"$\"),Q={ID:RegExp(\"^#(\"+R+\")\"),CLASS:RegExp(\"^\\\\.(\"+R+\")\"),TAG:RegExp(\"^(\"+R.replace(\"w\",\"w*\")+\")\"),ATTR:RegExp(\"^\"+$),PSEUDO:RegExp(\"^\"+I),CHILD:RegExp(\"^:(only|first|last|nth|nth-last)-(child|of-type)(?:\\\\(\"+P+\"*(even|odd|(([+-]|)(\\\\d*)n|)\"+P+\"*(?:([+-]|)\"+P+\"*(\\\\d+)|))\"+P+\"*\\\\)|)\",\"i\"),bool:RegExp(\"^(?:\"+B+\")$\",\"i\"),needsContext:RegExp(\"^\"+P+\"*[>+~]|:(even|odd|eq|gt|lt|nth|first|last)(?:\\\\(\"+P+\"*((?:-\\\\d)?\\\\d*)\"+P+\"*\\\\)|)(?=[^-]|$)\",\"i\")},K=/^[^{]+\\{\\s*\\[native \\w/,Z=/^(?:#([\\w-]+)|(\\w+)|\\.([\\w-]+))$/,et=/^(?:input|select|textarea|button)$/i,tt=/^h\\d$/i,nt=/\'|\\\\/g,rt=RegExp(\"\\\\\\\\([\\\\da-f]{1,6}\"+P+\"?|(\"+P+\")|.)\",\"ig\"),it=function(e,t,n){var r=\"0x\"+t-65536;return r!==r||n?t:0>r?String.fromCharCode(r+65536):String.fromCharCode(55296|r>>10,56320|1023&r)};try{M.apply(H=O.call(w.childNodes),w.childNodes),H[w.childNodes.length].nodeType}catch(ot){M={apply:H.length?function(e,t){_.apply(e,O.call(t))}:function(e,t){var n=e.length,r=0;while(e[n++]=t[r++]);e.length=n-1}}}function at(e,t,n,i){var o,a,s,l,u,c,d,m,y,x;if((t?t.ownerDocument||t:w)!==f&&p(t),t=t||f,n=n||[],!e||\"string\"!=typeof e)return n;if(1!==(l=t.nodeType)&&9!==l)return[];if(h&&!i){if(o=Z.exec(e))if(s=o[1]){if(9===l){if(a=t.getElementById(s),!a||!a.parentNode)return n;if(a.id===s)return n.push(a),n}else if(t.ownerDocument&&(a=t.ownerDocument.getElementById(s))&&v(t,a)&&a.id===s)return n.push(a),n}else{if(o[2])return M.apply(n,t.getElementsByTagName(e)),n;if((s=o[3])&&r.getElementsByClassName&&t.getElementsByClassName)return M.apply(n,t.getElementsByClassName(s)),n}if(r.qsa&&(!g||!g.test(e))){if(m=d=b,y=t,x=9===l&&e,1===l&&\"object\"!==t.nodeName.toLowerCase()){c=mt(e),(d=t.getAttribute(\"id\"))?m=d.replace(nt,\"\\\\$&\"):t.setAttribute(\"id\",m),m=\"[id=\'\"+m+\"\'] \",u=c.length;while(u--)c[u]=m+yt(c[u]);y=V.test(e)&&t.parentNode||t,x=c.join(\",\")}if(x)try{return M.apply(n,y.querySelectorAll(x)),n}catch(T){}finally{d||t.removeAttribute(\"id\")}}}return kt(e.replace(z,\"$1\"),t,n,i)}function st(){var e=[];function t(n,r){return e.push(n+=\" \")>o.cacheLength&&delete t[e.shift()],t[n]=r}return t}function lt(e){return e[b]=!0,e}function ut(e){var t=f.createElement(\"div\");try{return!!e(t)}catch(n){return!1}finally{t.parentNode&&t.parentNode.removeChild(t),t=null}}function ct(e,t){var n=e.split(\"|\"),r=e.length;while(r--)o.attrHandle[n[r]]=t}function pt(e,t){var n=t&&e,r=n&&1===e.nodeType&&1===t.nodeType&&(~t.sourceIndex||D)-(~e.sourceIndex||D);if(r)return r;if(n)while(n=n.nextSibling)if(n===t)return-1;return e?1:-1}function ft(e){return function(t){var n=t.nodeName.toLowerCase();return\"input\"===n&&t.type===e}}function dt(e){return function(t){var n=t.nodeName.toLowerCase();return(\"input\"===n||\"button\"===n)&&t.type===e}}function ht(e){return lt(function(t){return t=+t,lt(function(n,r){var i,o=e([],n.length,t),a=o.length;while(a--)n[i=o[a]]&&(n[i]=!(r[i]=n[i]))})})}s=at.isXML=function(e){var t=e&&(e.ownerDocument||e).documentElement;return t?\"HTML\"!==t.nodeName:!1},r=at.support={},p=at.setDocument=function(e){var n=e?e.ownerDocument||e:w,i=n.defaultView;return n!==f&&9===n.nodeType&&n.documentElement?(f=n,d=n.documentElement,h=!s(n),i&&i.attachEvent&&i!==i.top&&i.attachEvent(\"onbeforeunload\",function(){p()}),r.attributes=ut(function(e){return e.className=\"i\",!e.getAttribute(\"className\")}),r.getElementsByTagName=ut(function(e){return e.appendChild(n.createComment(\"\")),!e.getElementsByTagName(\"*\").length}),r.getElementsByClassName=ut(function(e){return e.innerHTML=\"<div class=\'a\'></div><div class=\'a i\'></div>\",e.firstChild.className=\"i\",2===e.getElementsByClassName(\"i\").length}),r.getById=ut(function(e){return d.appendChild(e).id=b,!n.getElementsByName||!n.getElementsByName(b).length}),r.getById?(o.find.ID=function(e,t){if(typeof t.getElementById!==j&&h){var n=t.getElementById(e);return n&&n.parentNode?[n]:[]}},o.filter.ID=function(e){var t=e.replace(rt,it);return function(e){return e.getAttribute(\"id\")===t}}):(delete o.find.ID,o.filter.ID=function(e){var t=e.replace(rt,it);return function(e){var n=typeof e.getAttributeNode!==j&&e.getAttributeNode(\"id\");return n&&n.value===t}}),o.find.TAG=r.getElementsByTagName?function(e,n){return typeof n.getElementsByTagName!==j?n.getElementsByTagName(e):t}:function(e,t){var n,r=[],i=0,o=t.getElementsByTagName(e);if(\"*\"===e){while(n=o[i++])1===n.nodeType&&r.push(n);return r}return o},o.find.CLASS=r.getElementsByClassName&&function(e,n){return typeof n.getElementsByClassName!==j&&h?n.getElementsByClassName(e):t},m=[],g=[],(r.qsa=K.test(n.querySelectorAll))&&(ut(function(e){e.innerHTML=\"<select><option selected=\'\'></option></select>\",e.querySelectorAll(\"[selected]\").length||g.push(\"\\\\[\"+P+\"*(?:value|\"+B+\")\"),e.querySelectorAll(\":checked\").length||g.push(\":checked\")}),ut(function(e){var t=n.createElement(\"input\");t.setAttribute(\"type\",\"hidden\"),e.appendChild(t).setAttribute(\"t\",\"\"),e.querySelectorAll(\"[t^=\'\']\").length&&g.push(\"[*^$]=\"+P+\"*(?:\'\'|\\\"\\\")\"),e.querySelectorAll(\":enabled\").length||g.push(\":enabled\",\":disabled\"),e.querySelectorAll(\"*,:x\"),g.push(\",.*:\")})),(r.matchesSelector=K.test(y=d.webkitMatchesSelector||d.mozMatchesSelector||d.oMatchesSelector||d.msMatchesSelector))&&ut(function(e){r.disconnectedMatch=y.call(e,\"div\"),y.call(e,\"[s!=\'\']:x\"),m.push(\"!=\",I)}),g=g.length&&RegExp(g.join(\"|\")),m=m.length&&RegExp(m.join(\"|\")),v=K.test(d.contains)||d.compareDocumentPosition?function(e,t){var n=9===e.nodeType?e.documentElement:e,r=t&&t.parentNode;return e===r||!(!r||1!==r.nodeType||!(n.contains?n.contains(r):e.compareDocumentPosition&&16&e.compareDocumentPosition(r)))}:function(e,t){if(t)while(t=t.parentNode)if(t===e)return!0;return!1},A=d.compareDocumentPosition?function(e,t){if(e===t)return S=!0,0;var i=t.compareDocumentPosition&&e.compareDocumentPosition&&e.compareDocumentPosition(t);return i?1&i||!r.sortDetached&&t.compareDocumentPosition(e)===i?e===n||v(w,e)?-1:t===n||v(w,t)?1:c?F.call(c,e)-F.call(c,t):0:4&i?-1:1:e.compareDocumentPosition?-1:1}:function(e,t){var r,i=0,o=e.parentNode,a=t.parentNode,s=[e],l=[t];if(e===t)return S=!0,0;if(!o||!a)return e===n?-1:t===n?1:o?-1:a?1:c?F.call(c,e)-F.call(c,t):0;if(o===a)return pt(e,t);r=e;while(r=r.parentNode)s.unshift(r);r=t;while(r=r.parentNode)l.unshift(r);while(s[i]===l[i])i++;return i?pt(s[i],l[i]):s[i]===w?-1:l[i]===w?1:0},n):f},at.matches=function(e,t){return at(e,null,null,t)},at.matchesSelector=function(e,t){if((e.ownerDocument||e)!==f&&p(e),t=t.replace(Y,\"=\'$1\']\"),!(!r.matchesSelector||!h||m&&m.test(t)||g&&g.test(t)))try{var n=y.call(e,t);if(n||r.disconnectedMatch||e.document&&11!==e.document.nodeType)return n}catch(i){}return at(t,f,null,[e]).length>0},at.contains=function(e,t){return(e.ownerDocument||e)!==f&&p(e),v(e,t)},at.attr=function(e,n){(e.ownerDocument||e)!==f&&p(e);var i=o.attrHandle[n.toLowerCase()],a=i&&L.call(o.attrHandle,n.toLowerCase())?i(e,n,!h):t;return a===t?r.attributes||!h?e.getAttribute(n):(a=e.getAttributeNode(n))&&a.specified?a.value:null:a},at.error=function(e){throw Error(\"Syntax error, unrecognized expression: \"+e)},at.uniqueSort=function(e){var t,n=[],i=0,o=0;if(S=!r.detectDuplicates,c=!r.sortStable&&e.slice(0),e.sort(A),S){while(t=e[o++])t===e[o]&&(i=n.push(o));while(i--)e.splice(n[i],1)}return e},a=at.getText=function(e){var t,n=\"\",r=0,i=e.nodeType;if(i){if(1===i||9===i||11===i){if(\"string\"==typeof e.textContent)return e.textContent;for(e=e.firstChild;e;e=e.nextSibling)n+=a(e)}else if(3===i||4===i)return e.nodeValue}else for(;t=e[r];r++)n+=a(t);return n},o=at.selectors={cacheLength:50,createPseudo:lt,match:Q,attrHandle:{},find:{},relative:{\">\":{dir:\"parentNode\",first:!0},\" \":{dir:\"parentNode\"},\"+\":{dir:\"previousSibling\",first:!0},\"~\":{dir:\"previousSibling\"}},preFilter:{ATTR:function(e){return e[1]=e[1].replace(rt,it),e[3]=(e[4]||e[5]||\"\").replace(rt,it),\"~=\"===e[2]&&(e[3]=\" \"+e[3]+\" \"),e.slice(0,4)},CHILD:function(e){return e[1]=e[1].toLowerCase(),\"nth\"===e[1].slice(0,3)?(e[3]||at.error(e[0]),e[4]=+(e[4]?e[5]+(e[6]||1):2*(\"even\"===e[3]||\"odd\"===e[3])),e[5]=+(e[7]+e[8]||\"odd\"===e[3])):e[3]&&at.error(e[0]),e},PSEUDO:function(e){var n,r=!e[5]&&e[2];return Q.CHILD.test(e[0])?null:(e[3]&&e[4]!==t?e[2]=e[4]:r&&J.test(r)&&(n=mt(r,!0))&&(n=r.indexOf(\")\",r.length-n)-r.length)&&(e[0]=e[0].slice(0,n),e[2]=r.slice(0,n)),e.slice(0,3))}},filter:{TAG:function(e){var t=e.replace(rt,it).toLowerCase();return\"*\"===e?function(){return!0}:function(e){return e.nodeName&&e.nodeName.toLowerCase()===t}},CLASS:function(e){var t=N[e+\" \"];return t||(t=RegExp(\"(^|\"+P+\")\"+e+\"(\"+P+\"|$)\"))&&N(e,function(e){return t.test(\"string\"==typeof e.className&&e.className||typeof e.getAttribute!==j&&e.getAttribute(\"class\")||\"\")})},ATTR:function(e,t,n){return function(r){var i=at.attr(r,e);return null==i?\"!=\"===t:t?(i+=\"\",\"=\"===t?i===n:\"!=\"===t?i!==n:\"^=\"===t?n&&0===i.indexOf(n):\"*=\"===t?n&&i.indexOf(n)>-1:\"$=\"===t?n&&i.slice(-n.length)===n:\"~=\"===t?(\" \"+i+\" \").indexOf(n)>-1:\"|=\"===t?i===n||i.slice(0,n.length+1)===n+\"-\":!1):!0}},CHILD:function(e,t,n,r,i){var o=\"nth\"!==e.slice(0,3),a=\"last\"!==e.slice(-4),s=\"of-type\"===t;return 1===r&&0===i?function(e){return!!e.parentNode}:function(t,n,l){var u,c,p,f,d,h,g=o!==a?\"nextSibling\":\"previousSibling\",m=t.parentNode,y=s&&t.nodeName.toLowerCase(),v=!l&&!s;if(m){if(o){while(g){p=t;while(p=p[g])if(s?p.nodeName.toLowerCase()===y:1===p.nodeType)return!1;h=g=\"only\"===e&&!h&&\"nextSibling\"}return!0}if(h=[a?m.firstChild:m.lastChild],a&&v){c=m[b]||(m[b]={}),u=c[e]||[],d=u[0]===T&&u[1],f=u[0]===T&&u[2],p=d&&m.childNodes[d];while(p=++d&&p&&p[g]||(f=d=0)||h.pop())if(1===p.nodeType&&++f&&p===t){c[e]=[T,d,f];break}}else if(v&&(u=(t[b]||(t[b]={}))[e])&&u[0]===T)f=u[1];else while(p=++d&&p&&p[g]||(f=d=0)||h.pop())if((s?p.nodeName.toLowerCase()===y:1===p.nodeType)&&++f&&(v&&((p[b]||(p[b]={}))[e]=[T,f]),p===t))break;return f-=i,f===r||0===f%r&&f/r>=0}}},PSEUDO:function(e,t){var n,r=o.pseudos[e]||o.setFilters[e.toLowerCase()]||at.error(\"unsupported pseudo: \"+e);return r[b]?r(t):r.length>1?(n=[e,e,\"\",t],o.setFilters.hasOwnProperty(e.toLowerCase())?lt(function(e,n){var i,o=r(e,t),a=o.length;while(a--)i=F.call(e,o[a]),e[i]=!(n[i]=o[a])}):function(e){return r(e,0,n)}):r}},pseudos:{not:lt(function(e){var t=[],n=[],r=l(e.replace(z,\"$1\"));return r[b]?lt(function(e,t,n,i){var o,a=r(e,null,i,[]),s=e.length;while(s--)(o=a[s])&&(e[s]=!(t[s]=o))}):function(e,i,o){return t[0]=e,r(t,null,o,n),!n.pop()}}),has:lt(function(e){return function(t){return at(e,t).length>0}}),contains:lt(function(e){return function(t){return(t.textContent||t.innerText||a(t)).indexOf(e)>-1}}),lang:lt(function(e){return G.test(e||\"\")||at.error(\"unsupported lang: \"+e),e=e.replace(rt,it).toLowerCase(),function(t){var n;do if(n=h?t.lang:t.getAttribute(\"xml:lang\")||t.getAttribute(\"lang\"))return n=n.toLowerCase(),n===e||0===n.indexOf(e+\"-\");while((t=t.parentNode)&&1===t.nodeType);return!1}}),target:function(t){var n=e.location&&e.location.hash;return n&&n.slice(1)===t.id},root:function(e){return e===d},focus:function(e){return e===f.activeElement&&(!f.hasFocus||f.hasFocus())&&!!(e.type||e.href||~e.tabIndex)},enabled:function(e){return e.disabled===!1},disabled:function(e){return e.disabled===!0},checked:function(e){var t=e.nodeName.toLowerCase();return\"input\"===t&&!!e.checked||\"option\"===t&&!!e.selected},selected:function(e){return e.parentNode&&e.parentNode.selectedIndex,e.selected===!0},empty:function(e){for(e=e.firstChild;e;e=e.nextSibling)if(e.nodeName>\"@\"||3===e.nodeType||4===e.nodeType)return!1;return!0},parent:function(e){return!o.pseudos.empty(e)},header:function(e){return tt.test(e.nodeName)},input:function(e){return et.test(e.nodeName)},button:function(e){var t=e.nodeName.toLowerCase();return\"input\"===t&&\"button\"===e.type||\"button\"===t},text:function(e){var t;return\"input\"===e.nodeName.toLowerCase()&&\"text\"===e.type&&(null==(t=e.getAttribute(\"type\"))||t.toLowerCase()===e.type)},first:ht(function(){return[0]}),last:ht(function(e,t){return[t-1]}),eq:ht(function(e,t,n){return[0>n?n+t:n]}),even:ht(function(e,t){var n=0;for(;t>n;n+=2)e.push(n);return e}),odd:ht(function(e,t){var n=1;for(;t>n;n+=2)e.push(n);return e}),lt:ht(function(e,t,n){var r=0>n?n+t:n;for(;--r>=0;)e.push(r);return e}),gt:ht(function(e,t,n){var r=0>n?n+t:n;for(;t>++r;)e.push(r);return e})}},o.pseudos.nth=o.pseudos.eq;for(n in{radio:!0,checkbox:!0,file:!0,password:!0,image:!0})o.pseudos[n]=ft(n);for(n in{submit:!0,reset:!0})o.pseudos[n]=dt(n);function gt(){}gt.prototype=o.filters=o.pseudos,o.setFilters=new gt;function mt(e,t){var n,r,i,a,s,l,u,c=k[e+\" \"];if(c)return t?0:c.slice(0);s=e,l=[],u=o.preFilter;while(s){(!n||(r=X.exec(s)))&&(r&&(s=s.slice(r[0].length)||s),l.push(i=[])),n=!1,(r=U.exec(s))&&(n=r.shift(),i.push({value:n,type:r[0].replace(z,\" \")}),s=s.slice(n.length));for(a in o.filter)!(r=Q[a].exec(s))||u[a]&&!(r=u[a](r))||(n=r.shift(),i.push({value:n,type:a,matches:r}),s=s.slice(n.length));if(!n)break}return t?s.length:s?at.error(e):k(e,l).slice(0)}function yt(e){var t=0,n=e.length,r=\"\";for(;n>t;t++)r+=e[t].value;return r}function vt(e,t,n){var r=t.dir,o=n&&\"parentNode\"===r,a=C++;return t.first?function(t,n,i){while(t=t[r])if(1===t.nodeType||o)return e(t,n,i)}:function(t,n,s){var l,u,c,p=T+\" \"+a;if(s){while(t=t[r])if((1===t.nodeType||o)&&e(t,n,s))return!0}else while(t=t[r])if(1===t.nodeType||o)if(c=t[b]||(t[b]={}),(u=c[r])&&u[0]===p){if((l=u[1])===!0||l===i)return l===!0}else if(u=c[r]=[p],u[1]=e(t,n,s)||i,u[1]===!0)return!0}}function bt(e){return e.length>1?function(t,n,r){var i=e.length;while(i--)if(!e[i](t,n,r))return!1;return!0}:e[0]}function xt(e,t,n,r,i){var o,a=[],s=0,l=e.length,u=null!=t;for(;l>s;s++)(o=e[s])&&(!n||n(o,r,i))&&(a.push(o),u&&t.push(s));return a}function wt(e,t,n,r,i,o){return r&&!r[b]&&(r=wt(r)),i&&!i[b]&&(i=wt(i,o)),lt(function(o,a,s,l){var u,c,p,f=[],d=[],h=a.length,g=o||Nt(t||\"*\",s.nodeType?[s]:s,[]),m=!e||!o&&t?g:xt(g,f,e,s,l),y=n?i||(o?e:h||r)?[]:a:m;if(n&&n(m,y,s,l),r){u=xt(y,d),r(u,[],s,l),c=u.length;while(c--)(p=u[c])&&(y[d[c]]=!(m[d[c]]=p))}if(o){if(i||e){if(i){u=[],c=y.length;while(c--)(p=y[c])&&u.push(m[c]=p);i(null,y=[],u,l)}c=y.length;while(c--)(p=y[c])&&(u=i?F.call(o,p):f[c])>-1&&(o[u]=!(a[u]=p))}}else y=xt(y===a?y.splice(h,y.length):y),i?i(null,a,y,l):M.apply(a,y)})}function Tt(e){var t,n,r,i=e.length,a=o.relative[e[0].type],s=a||o.relative[\" \"],l=a?1:0,c=vt(function(e){return e===t},s,!0),p=vt(function(e){return F.call(t,e)>-1},s,!0),f=[function(e,n,r){return!a&&(r||n!==u)||((t=n).nodeType?c(e,n,r):p(e,n,r))}];for(;i>l;l++)if(n=o.relative[e[l].type])f=[vt(bt(f),n)];else{if(n=o.filter[e[l].type].apply(null,e[l].matches),n[b]){for(r=++l;i>r;r++)if(o.relative[e[r].type])break;return wt(l>1&&bt(f),l>1&&yt(e.slice(0,l-1).concat({value:\" \"===e[l-2].type?\"*\":\"\"})).replace(z,\"$1\"),n,r>l&&Tt(e.slice(l,r)),i>r&&Tt(e=e.slice(r)),i>r&&yt(e))}f.push(n)}return bt(f)}function Ct(e,t){var n=0,r=t.length>0,a=e.length>0,s=function(s,l,c,p,d){var h,g,m,y=[],v=0,b=\"0\",x=s&&[],w=null!=d,C=u,N=s||a&&o.find.TAG(\"*\",d&&l.parentNode||l),k=T+=null==C?1:Math.random()||.1;for(w&&(u=l!==f&&l,i=n);null!=(h=N[b]);b++){if(a&&h){g=0;while(m=e[g++])if(m(h,l,c)){p.push(h);break}w&&(T=k,i=++n)}r&&((h=!m&&h)&&v--,s&&x.push(h))}if(v+=b,r&&b!==v){g=0;while(m=t[g++])m(x,y,l,c);if(s){if(v>0)while(b--)x[b]||y[b]||(y[b]=q.call(p));y=xt(y)}M.apply(p,y),w&&!s&&y.length>0&&v+t.length>1&&at.uniqueSort(p)}return w&&(T=k,u=C),x};return r?lt(s):s}l=at.compile=function(e,t){var n,r=[],i=[],o=E[e+\" \"];if(!o){t||(t=mt(e)),n=t.length;while(n--)o=Tt(t[n]),o[b]?r.push(o):i.push(o);o=E(e,Ct(i,r))}return o};function Nt(e,t,n){var r=0,i=t.length;for(;i>r;r++)at(e,t[r],n);return n}function kt(e,t,n,i){var a,s,u,c,p,f=mt(e);if(!i&&1===f.length){if(s=f[0]=f[0].slice(0),s.length>2&&\"ID\"===(u=s[0]).type&&r.getById&&9===t.nodeType&&h&&o.relative[s[1].type]){if(t=(o.find.ID(u.matches[0].replace(rt,it),t)||[])[0],!t)return n;e=e.slice(s.shift().value.length)}a=Q.needsContext.test(e)?0:s.length;while(a--){if(u=s[a],o.relative[c=u.type])break;if((p=o.find[c])&&(i=p(u.matches[0].replace(rt,it),V.test(s[0].type)&&t.parentNode||t))){if(s.splice(a,1),e=i.length&&yt(s),!e)return M.apply(n,i),n;break}}}return l(e,f)(i,t,!h,n,V.test(e)),n}r.sortStable=b.split(\"\").sort(A).join(\"\")===b,r.detectDuplicates=S,p(),r.sortDetached=ut(function(e){return 1&e.compareDocumentPosition(f.createElement(\"div\"))}),ut(function(e){return e.innerHTML=\"<a href=\'#\'></a>\",\"#\"===e.firstChild.getAttribute(\"href\")})||ct(\"type|href|height|width\",function(e,n,r){return r?t:e.getAttribute(n,\"type\"===n.toLowerCase()?1:2)}),r.attributes&&ut(function(e){return e.innerHTML=\"<input/>\",e.firstChild.setAttribute(\"value\",\"\"),\"\"===e.firstChild.getAttribute(\"value\")})||ct(\"value\",function(e,n,r){return r||\"input\"!==e.nodeName.toLowerCase()?t:e.defaultValue}),ut(function(e){return null==e.getAttribute(\"disabled\")})||ct(B,function(e,n,r){var i;return r?t:(i=e.getAttributeNode(n))&&i.specified?i.value:e[n]===!0?n.toLowerCase():null}),x.find=at,x.expr=at.selectors,x.expr[\":\"]=x.expr.pseudos,x.unique=at.uniqueSort,x.text=at.getText,x.isXMLDoc=at.isXML,x.contains=at.contains}(e);var O={};function F(e){var t=O[e]={};return x.each(e.match(T)||[],function(e,n){t[n]=!0}),t}x.Callbacks=function(e){e=\"string\"==typeof e?O[e]||F(e):x.extend({},e);var n,r,i,o,a,s,l=[],u=!e.once&&[],c=function(t){for(r=e.memory&&t,i=!0,a=s||0,s=0,o=l.length,n=!0;l&&o>a;a++)if(l[a].apply(t[0],t[1])===!1&&e.stopOnFalse){r=!1;break}n=!1,l&&(u?u.length&&c(u.shift()):r?l=[]:p.disable())},p={add:function(){if(l){var t=l.length;(function i(t){x.each(t,function(t,n){var r=x.type(n);\"function\"===r?e.unique&&p.has(n)||l.push(n):n&&n.length&&\"string\"!==r&&i(n)})})(arguments),n?o=l.length:r&&(s=t,c(r))}return this},remove:function(){return l&&x.each(arguments,function(e,t){var r;while((r=x.inArray(t,l,r))>-1)l.splice(r,1),n&&(o>=r&&o--,a>=r&&a--)}),this},has:function(e){return e?x.inArray(e,l)>-1:!(!l||!l.length)},empty:function(){return l=[],o=0,this},disable:function(){return l=u=r=t,this},disabled:function(){return!l},lock:function(){return u=t,r||p.disable(),this},locked:function(){return!u},fireWith:function(e,t){return!l||i&&!u||(t=t||[],t=[e,t.slice?t.slice():t],n?u.push(t):c(t)),this},fire:function(){return p.fireWith(this,arguments),this},fired:function(){return!!i}};return p},x.extend({Deferred:function(e){var t=[[\"resolve\",\"done\",x.Callbacks(\"once memory\"),\"resolved\"],[\"reject\",\"fail\",x.Callbacks(\"once memory\"),\"rejected\"],[\"notify\",\"progress\",x.Callbacks(\"memory\")]],n=\"pending\",r={state:function(){return n},always:function(){return i.done(arguments).fail(arguments),this},then:function(){var e=arguments;return x.Deferred(function(n){x.each(t,function(t,o){var a=o[0],s=x.isFunction(e[t])&&e[t];i[o[1]](function(){var e=s&&s.apply(this,arguments);e&&x.isFunction(e.promise)?e.promise().done(n.resolve).fail(n.reject).progress(n.notify):n[a+\"With\"](this===r?n.promise():this,s?[e]:arguments)})}),e=null}).promise()},promise:function(e){return null!=e?x.extend(e,r):r}},i={};return r.pipe=r.then,x.each(t,function(e,o){var a=o[2],s=o[3];r[o[1]]=a.add,s&&a.add(function(){n=s},t[1^e][2].disable,t[2][2].lock),i[o[0]]=function(){return i[o[0]+\"With\"](this===i?r:this,arguments),this},i[o[0]+\"With\"]=a.fireWith}),r.promise(i),e&&e.call(i,i),i},when:function(e){var t=0,n=g.call(arguments),r=n.length,i=1!==r||e&&x.isFunction(e.promise)?r:0,o=1===i?e:x.Deferred(),a=function(e,t,n){return function(r){t[e]=this,n[e]=arguments.length>1?g.call(arguments):r,n===s?o.notifyWith(t,n):--i||o.resolveWith(t,n)}},s,l,u;if(r>1)for(s=Array(r),l=Array(r),u=Array(r);r>t;t++)n[t]&&x.isFunction(n[t].promise)?n[t].promise().done(a(t,u,n)).fail(o.reject).progress(a(t,l,s)):--i;return i||o.resolveWith(u,n),o.promise()}}),x.support=function(t){var n,r,o,s,l,u,c,p,f,d=a.createElement(\"div\");if(d.setAttribute(\"className\",\"t\"),d.innerHTML=\"  <link/><table></table><a href=\'/a\'>a</a><input type=\'checkbox\'/>\",n=d.getElementsByTagName(\"*\")||[],r=d.getElementsByTagName(\"a\")[0],!r||!r.style||!n.length)return t;s=a.createElement(\"select\"),u=s.appendChild(a.createElement(\"option\")),o=d.getElementsByTagName(\"input\")[0],r.style.cssText=\"top:1px;float:left;opacity:.5\",t.getSetAttribute=\"t\"!==d.className,t.leadingWhitespace=3===d.firstChild.nodeType,t.tbody=!d.getElementsByTagName(\"tbody\").length,t.htmlSerialize=!!d.getElementsByTagName(\"link\").length,t.style=/top/.test(r.getAttribute(\"style\")),t.hrefNormalized=\"/a\"===r.getAttribute(\"href\"),t.opacity=/^0.5/.test(r.style.opacity),t.cssFloat=!!r.style.cssFloat,t.checkOn=!!o.value,t.optSelected=u.selected,t.enctype=!!a.createElement(\"form\").enctype,t.html5Clone=\"<:nav></:nav>\"!==a.createElement(\"nav\").cloneNode(!0).outerHTML,t.inlineBlockNeedsLayout=!1,t.shrinkWrapBlocks=!1,t.pixelPosition=!1,t.deleteExpando=!0,t.noCloneEvent=!0,t.reliableMarginRight=!0,t.boxSizingReliable=!0,o.checked=!0,t.noCloneChecked=o.cloneNode(!0).checked,s.disabled=!0,t.optDisabled=!u.disabled;try{delete d.test}catch(h){t.deleteExpando=!1}o=a.createElement(\"input\"),o.setAttribute(\"value\",\"\"),t.input=\"\"===o.getAttribute(\"value\"),o.value=\"t\",o.setAttribute(\"type\",\"radio\"),t.radioValue=\"t\"===o.value,o.setAttribute(\"checked\",\"t\"),o.setAttribute(\"name\",\"t\"),l=a.createDocumentFragment(),l.appendChild(o),t.appendChecked=o.checked,t.checkClone=l.cloneNode(!0).cloneNode(!0).lastChild.checked,d.attachEvent&&(d.attachEvent(\"onclick\",function(){t.noCloneEvent=!1}),d.cloneNode(!0).click());for(f in{submit:!0,change:!0,focusin:!0})d.setAttribute(c=\"on\"+f,\"t\"),t[f+\"Bubbles\"]=c in e||d.attributes[c].expando===!1;d.style.backgroundClip=\"content-box\",d.cloneNode(!0).style.backgroundClip=\"\",t.clearCloneStyle=\"content-box\"===d.style.backgroundClip;for(f in x(t))break;return t.ownLast=\"0\"!==f,x(function(){var n,r,o,s=\"padding:0;margin:0;border:0;display:block;box-sizing:content-box;-moz-box-sizing:content-box;-webkit-box-sizing:content-box;\",l=a.getElementsByTagName(\"body\")[0];l&&(n=a.createElement(\"div\"),n.style.cssText=\"border:0;width:0;height:0;position:absolute;top:0;left:-9999px;margin-top:1px\",l.appendChild(n).appendChild(d),d.innerHTML=\"<table><tr><td></td><td>t</td></tr></table>\",o=d.getElementsByTagName(\"td\"),o[0].style.cssText=\"padding:0;margin:0;border:0;display:none\",p=0===o[0].offsetHeight,o[0].style.display=\"\",o[1].style.display=\"none\",t.reliableHiddenOffsets=p&&0===o[0].offsetHeight,d.innerHTML=\"\",d.style.cssText=\"box-sizing:border-box;-moz-box-sizing:border-box;-webkit-box-sizing:border-box;padding:1px;border:1px;display:block;width:4px;margin-top:1%;position:absolute;top:1%;\",x.swap(l,null!=l.style.zoom?{zoom:1}:{},function(){t.boxSizing=4===d.offsetWidth}),e.getComputedStyle&&(t.pixelPosition=\"1%\"!==(e.getComputedStyle(d,null)||{}).top,t.boxSizingReliable=\"4px\"===(e.getComputedStyle(d,null)||{width:\"4px\"}).width,r=d.appendChild(a.createElement(\"div\")),r.style.cssText=d.style.cssText=s,r.style.marginRight=r.style.width=\"0\",d.style.width=\"1px\",t.reliableMarginRight=!parseFloat((e.getComputedStyle(r,null)||{}).marginRight)),typeof d.style.zoom!==i&&(d.innerHTML=\"\",d.style.cssText=s+\"width:1px;padding:1px;display:inline;zoom:1\",t.inlineBlockNeedsLayout=3===d.offsetWidth,d.style.display=\"block\",d.innerHTML=\"<div></div>\",d.firstChild.style.width=\"5px\",t.shrinkWrapBlocks=3!==d.offsetWidth,t.inlineBlockNeedsLayout&&(l.style.zoom=1)),l.removeChild(n),n=d=o=r=null)}),n=s=l=u=r=o=null,t");
                    yaz.WriteLine("}({});var B=/(?:\\{[\\s\\S]*\\}|\\[[\\s\\S]*\\])$/,P=/([A-Z])/g;function R(e,n,r,i){if(x.acceptData(e)){var o,a,s=x.expando,l=e.nodeType,u=l?x.cache:e,c=l?e[s]:e[s]&&s;if(c&&u[c]&&(i||u[c].data)||r!==t||\"string\"!=typeof n)return c||(c=l?e[s]=p.pop()||x.guid++:s),u[c]||(u[c]=l?{}:{toJSON:x.noop}),(\"object\"==typeof n||\"function\"==typeof n)&&(i?u[c]=x.extend(u[c],n):u[c].data=x.extend(u[c].data,n)),a=u[c],i||(a.data||(a.data={}),a=a.data),r!==t&&(a[x.camelCase(n)]=r),\"string\"==typeof n?(o=a[n],null==o&&(o=a[x.camelCase(n)])):o=a,o}}function W(e,t,n){if(x.acceptData(e)){var r,i,o=e.nodeType,a=o?x.cache:e,s=o?e[x.expando]:x.expando;if(a[s]){if(t&&(r=n?a[s]:a[s].data)){x.isArray(t)?t=t.concat(x.map(t,x.camelCase)):t in r?t=[t]:(t=x.camelCase(t),t=t in r?[t]:t.split(\" \")),i=t.length;while(i--)delete r[t[i]];if(n?!I(r):!x.isEmptyObject(r))return}(n||(delete a[s].data,I(a[s])))&&(o?x.cleanData([e],!0):x.support.deleteExpando||a!=a.window?delete a[s]:a[s]=null)}}}x.extend({cache:{},noData:{applet:!0,embed:!0,object:\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"},hasData:function(e){return e=e.nodeType?x.cache[e[x.expando]]:e[x.expando],!!e&&!I(e)},data:function(e,t,n){return R(e,t,n)},removeData:function(e,t){return W(e,t)},_data:function(e,t,n){return R(e,t,n,!0)},_removeData:function(e,t){return W(e,t,!0)},acceptData:function(e){if(e.nodeType&&1!==e.nodeType&&9!==e.nodeType)return!1;var t=e.nodeName&&x.noData[e.nodeName.toLowerCase()];return!t||t!==!0&&e.getAttribute(\"classid\")===t}}),x.fn.extend({data:function(e,n){var r,i,o=null,a=0,s=this[0];if(e===t){if(this.length&&(o=x.data(s),1===s.nodeType&&!x._data(s,\"parsedAttrs\"))){for(r=s.attributes;r.length>a;a++)i=r[a].name,0===i.indexOf(\"data-\")&&(i=x.camelCase(i.slice(5)),$(s,i,o[i]));x._data(s,\"parsedAttrs\",!0)}return o}return\"object\"==typeof e?this.each(function(){x.data(this,e)}):arguments.length>1?this.each(function(){x.data(this,e,n)}):s?$(s,e,x.data(s,e)):null},removeData:function(e){return this.each(function(){x.removeData(this,e)})}});function $(e,n,r){if(r===t&&1===e.nodeType){var i=\"data-\"+n.replace(P,\"-$1\").toLowerCase();if(r=e.getAttribute(i),\"string\"==typeof r){try{r=\"true\"===r?!0:\"false\"===r?!1:\"null\"===r?null:+r+\"\"===r?+r:B.test(r)?x.parseJSON(r):r}catch(o){}x.data(e,n,r)}else r=t}return r}function I(e){var t;for(t in e)if((\"data\"!==t||!x.isEmptyObject(e[t]))&&\"toJSON\"!==t)return!1;return!0}x.extend({queue:function(e,n,r){var i;return e?(n=(n||\"fx\")+\"queue\",i=x._data(e,n),r&&(!i||x.isArray(r)?i=x._data(e,n,x.makeArray(r)):i.push(r)),i||[]):t},dequeue:function(e,t){t=t||\"fx\";var n=x.queue(e,t),r=n.length,i=n.shift(),o=x._queueHooks(e,t),a=function(){x.dequeue(e,t)};\"inprogress\"===i&&(i=n.shift(),r--),i&&(\"fx\"===t&&n.unshift(\"inprogress\"),delete o.stop,i.call(e,a,o)),!r&&o&&o.empty.fire()},_queueHooks:function(e,t){var n=t+\"queueHooks\";return x._data(e,n)||x._data(e,n,{empty:x.Callbacks(\"once memory\").add(function(){x._removeData(e,t+\"queue\"),x._removeData(e,n)})})}}),x.fn.extend({queue:function(e,n){var r=2;return\"string\"!=typeof e&&(n=e,e=\"fx\",r--),r>arguments.length?x.queue(this[0],e):n===t?this:this.each(function(){var t=x.queue(this,e,n);x._queueHooks(this,e),\"fx\"===e&&\"inprogress\"!==t[0]&&x.dequeue(this,e)})},dequeue:function(e){return this.each(function(){x.dequeue(this,e)})},delay:function(e,t){return e=x.fx?x.fx.speeds[e]||e:e,t=t||\"fx\",this.queue(t,function(t,n){var r=setTimeout(t,e);n.stop=function(){clearTimeout(r)}})},clearQueue:function(e){return this.queue(e||\"fx\",[])},promise:function(e,n){var r,i=1,o=x.Deferred(),a=this,s=this.length,l=function(){--i||o.resolveWith(a,[a])};\"string\"!=typeof e&&(n=e,e=t),e=e||\"fx\";while(s--)r=x._data(a[s],e+\"queueHooks\"),r&&r.empty&&(i++,r.empty.add(l));return l(),o.promise(n)}});var z,X,U=/[\\t\\r\\n\\f]/g,V=/\\r/g,Y=/^(?:input|select|textarea|button|object)$/i,J=/^(?:a|area)$/i,G=/^(?:checked|selected)$/i,Q=x.support.getSetAttribute,K=x.support.input;x.fn.extend({attr:function(e,t){return x.access(this,x.attr,e,t,arguments.length>1)},removeAttr:function(e){return this.each(function(){x.removeAttr(this,e)})},prop:function(e,t){return x.access(this,x.prop,e,t,arguments.length>1)},removeProp:function(e){return e=x.propFix[e]||e,this.each(function(){try{this[e]=t,delete this[e]}catch(n){}})},addClass:function(e){var t,n,r,i,o,a=0,s=this.length,l=\"string\"==typeof e&&e;if(x.isFunction(e))return this.each(function(t){x(this).addClass(e.call(this,t,this.className))});if(l)for(t=(e||\"\").match(T)||[];s>a;a++)if(n=this[a],r=1===n.nodeType&&(n.className?(\" \"+n.className+\" \").replace(U,\" \"):\" \")){o=0;while(i=t[o++])0>r.indexOf(\" \"+i+\" \")&&(r+=i+\" \");n.className=x.trim(r)}return this},removeClass:function(e){var t,n,r,i,o,a=0,s=this.length,l=0===arguments.length||\"string\"==typeof e&&e;if(x.isFunction(e))return this.each(function(t){x(this).removeClass(e.call(this,t,this.className))});if(l)for(t=(e||\"\").match(T)||[];s>a;a++)if(n=this[a],r=1===n.nodeType&&(n.className?(\" \"+n.className+\" \").replace(U,\" \"):\"\")){o=0;while(i=t[o++])while(r.indexOf(\" \"+i+\" \")>=0)r=r.replace(\" \"+i+\" \",\" \");n.className=e?x.trim(r):\"\"}return this},toggleClass:function(e,t){var n=typeof e;return\"boolean\"==typeof t&&\"string\"===n?t?this.addClass(e):this.removeClass(e):x.isFunction(e)?this.each(function(n){x(this).toggleClass(e.call(this,n,this.className,t),t)}):this.each(function(){if(\"string\"===n){var t,r=0,o=x(this),a=e.match(T)||[];while(t=a[r++])o.hasClass(t)?o.removeClass(t):o.addClass(t)}else(n===i||\"boolean\"===n)&&(this.className&&x._data(this,\"__className__\",this.className),this.className=this.className||e===!1?\"\":x._data(this,\"__className__\")||\"\")})},hasClass:function(e){var t=\" \"+e+\" \",n=0,r=this.length;for(;r>n;n++)if(1===this[n].nodeType&&(\" \"+this[n].className+\" \").replace(U,\" \").indexOf(t)>=0)return!0;return!1},val:function(e){var n,r,i,o=this[0];{if(arguments.length)return i=x.isFunction(e),this.each(function(n){var o;1===this.nodeType&&(o=i?e.call(this,n,x(this).val()):e,null==o?o=\"\":\"number\"==typeof o?o+=\"\":x.isArray(o)&&(o=x.map(o,function(e){return null==e?\"\":e+\"\"})),r=x.valHooks[this.type]||x.valHooks[this.nodeName.toLowerCase()],r&&\"set\"in r&&r.set(this,o,\"value\")!==t||(this.value=o))});if(o)return r=x.valHooks[o.type]||x.valHooks[o.nodeName.toLowerCase()],r&&\"get\"in r&&(n=r.get(o,\"value\"))!==t?n:(n=o.value,\"string\"==typeof n?n.replace(V,\"\"):null==n?\"\":n)}}}),x.extend({valHooks:{option:{get:function(e){var t=x.find.attr(e,\"value\");return null!=t?t:e.text}},select:{get:function(e){var t,n,r=e.options,i=e.selectedIndex,o=\"select-one\"===e.type||0>i,a=o?null:[],s=o?i+1:r.length,l=0>i?s:o?i:0;for(;s>l;l++)if(n=r[l],!(!n.selected&&l!==i||(x.support.optDisabled?n.disabled:null!==n.getAttribute(\"disabled\"))||n.parentNode.disabled&&x.nodeName(n.parentNode,\"optgroup\"))){if(t=x(n).val(),o)return t;a.push(t)}return a},set:function(e,t){var n,r,i=e.options,o=x.makeArray(t),a=i.length;while(a--)r=i[a],(r.selected=x.inArray(x(r).val(),o)>=0)&&(n=!0);return n||(e.selectedIndex=-1),o}}},attr:function(e,n,r){var o,a,s=e.nodeType;if(e&&3!==s&&8!==s&&2!==s)return typeof e.getAttribute===i?x.prop(e,n,r):(1===s&&x.isXMLDoc(e)||(n=n.toLowerCase(),o=x.attrHooks[n]||(x.expr.match.bool.test(n)?X:z)),r===t?o&&\"get\"in o&&null!==(a=o.get(e,n))?a:(a=x.find.attr(e,n),null==a?t:a):null!==r?o&&\"set\"in o&&(a=o.set(e,r,n))!==t?a:(e.setAttribute(n,r+\"\"),r):(x.removeAttr(e,n),t))},removeAttr:function(e,t){var n,r,i=0,o=t&&t.match(T);if(o&&1===e.nodeType)while(n=o[i++])r=x.propFix[n]||n,x.expr.match.bool.test(n)?K&&Q||!G.test(n)?e[r]=!1:e[x.camelCase(\"default-\"+n)]=e[r]=!1:x.attr(e,n,\"\"),e.removeAttribute(Q?n:r)},attrHooks:{type:{set:function(e,t){if(!x.support.radioValue&&\"radio\"===t&&x.nodeName(e,\"input\")){var n=e.value;return e.setAttribute(\"type\",t),n&&(e.value=n),t}}}},propFix:{\"for\":\"htmlFor\",\"class\":\"className\"},prop:function(e,n,r){var i,o,a,s=e.nodeType;if(e&&3!==s&&8!==s&&2!==s)return a=1!==s||!x.isXMLDoc(e),a&&(n=x.propFix[n]||n,o=x.propHooks[n]),r!==t?o&&\"set\"in o&&(i=o.set(e,r,n))!==t?i:e[n]=r:o&&\"get\"in o&&null!==(i=o.get(e,n))?i:e[n]},propHooks:{tabIndex:{get:function(e){var t=x.find.attr(e,\"tabindex\");return t?parseInt(t,10):Y.test(e.nodeName)||J.test(e.nodeName)&&e.href?0:-1}}}}),X={set:function(e,t,n){return t===!1?x.removeAttr(e,n):K&&Q||!G.test(n)?e.setAttribute(!Q&&x.propFix[n]||n,n):e[x.camelCase(\"default-\"+n)]=e[n]=!0,n}},x.each(x.expr.match.bool.source.match(/\\w+/g),function(e,n){var r=x.expr.attrHandle[n]||x.find.attr;x.expr.attrHandle[n]=K&&Q||!G.test(n)?function(e,n,i){var o=x.expr.attrHandle[n],a=i?t:(x.expr.attrHandle[n]=t)!=r(e,n,i)?n.toLowerCase():null;return x.expr.attrHandle[n]=o,a}:function(e,n,r){return r?t:e[x.camelCase(\"default-\"+n)]?n.toLowerCase():null}}),K&&Q||(x.attrHooks.value={set:function(e,n,r){return x.nodeName(e,\"input\")?(e.defaultValue=n,t):z&&z.set(e,n,r)}}),Q||(z={set:function(e,n,r){var i=e.getAttributeNode(r);return i||e.setAttributeNode(i=e.ownerDocument.createAttribute(r)),i.value=n+=\"\",\"value\"===r||n===e.getAttribute(r)?n:t}},x.expr.attrHandle.id=x.expr.attrHandle.name=x.expr.attrHandle.coords=function(e,n,r){var i;return r?t:(i=e.getAttributeNode(n))&&\"\"!==i.value?i.value:null},x.valHooks.button={get:function(e,n){var r=e.getAttributeNode(n);return r&&r.specified?r.value:t},set:z.set},x.attrHooks.contenteditable={set:function(e,t,n){z.set(e,\"\"===t?!1:t,n)}},x.each([\"width\",\"height\"],function(e,n){x.attrHooks[n]={set:function(e,r){return\"\"===r?(e.setAttribute(n,\"auto\"),r):t}}})),x.support.hrefNormalized||x.each([\"href\",\"src\"],function(e,t){x.propHooks[t]={get:function(e){return e.getAttribute(t,4)}}}),x.support.style||(x.attrHooks.style={get:function(e){return e.style.cssText||t},set:function(e,t){return e.style.cssText=t+\"\"}}),x.support.optSelected||(x.propHooks.selected={get:function(e){var t=e.parentNode;return t&&(t.selectedIndex,t.parentNode&&t.parentNode.selectedIndex),null}}),x.each([\"tabIndex\",\"readOnly\",\"maxLength\",\"cellSpacing\",\"cellPadding\",\"rowSpan\",\"colSpan\",\"useMap\",\"frameBorder\",\"contentEditable\"],function(){x.propFix[this.toLowerCase()]=this}),x.support.enctype||(x.propFix.enctype=\"encoding\"),x.each([\"radio\",\"checkbox\"],function(){x.valHooks[this]={set:function(e,n){return x.isArray(n)?e.checked=x.inArray(x(e).val(),n)>=0:t}},x.support.checkOn||(x.valHooks[this].get=function(e){return null===e.getAttribute(\"value\")?\"on\":e.value})});var Z=/^(?:input|select|textarea)$/i,et=/^key/,tt=/^(?:mouse|contextmenu)|click/,nt=/^(?:focusinfocus|focusoutblur)$/,rt=/^([^.]*)(?:\\.(.+)|)$/;function it(){return!0}function ot(){return!1}function at(){try{return a.activeElement}catch(e){}}x.event={global:{},add:function(e,n,r,o,a){var s,l,u,c,p,f,d,h,g,m,y,v=x._data(e);if(v){r.handler&&(c=r,r=c.handler,a=c.selector),r.guid||(r.guid=x.guid++),(l=v.events)||(l=v.events={}),(f=v.handle)||(f=v.handle=function(e){return typeof x===i||e&&x.event.triggered===e.type?t:x.event.dispatch.apply(f.elem,arguments)},f.elem=e),n=(n||\"\").match(T)||[\"\"],u=n.length;while(u--)s=rt.exec(n[u])||[],g=y=s[1],m=(s[2]||\"\").split(\".\").sort(),g&&(p=x.event.special[g]||{},g=(a?p.delegateType:p.bindType)||g,p=x.event.special[g]||{},d=x.extend({type:g,origType:y,data:o,handler:r,guid:r.guid,selector:a,needsContext:a&&x.expr.match.needsContext.test(a),namespace:m.join(\".\")},c),(h=l[g])||(h=l[g]=[],h.delegateCount=0,p.setup&&p.setup.call(e,o,m,f)!==!1||(e.addEventListener?e.addEventListener(g,f,!1):e.attachEvent&&e.attachEvent(\"on\"+g,f))),p.add&&(p.add.call(e,d),d.handler.guid||(d.handler.guid=r.guid)),a?h.splice(h.delegateCount++,0,d):h.push(d),x.event.global[g]=!0);e=null}},remove:function(e,t,n,r,i){var o,a,s,l,u,c,p,f,d,h,g,m=x.hasData(e)&&x._data(e);if(m&&(c=m.events)){t=(t||\"\").match(T)||[\"\"],u=t.length;while(u--)if(s=rt.exec(t[u])||[],d=g=s[1],h=(s[2]||\"\").split(\".\").sort(),d){p=x.event.special[d]||{},d=(r?p.delegateType:p.bindType)||d,f=c[d]||[],s=s[2]&&RegExp(\"(^|\\\\.)\"+h.join(\"\\\\.(?:.*\\\\.|)\")+\"(\\\\.|$)\"),l=o=f.length;while(o--)a=f[o],!i&&g!==a.origType||n&&n.guid!==a.guid||s&&!s.test(a.namespace)||r&&r!==a.selector&&(\"**\"!==r||!a.selector)||(f.splice(o,1),a.selector&&f.delegateCount--,p.remove&&p.remove.call(e,a));l&&!f.length&&(p.teardown&&p.teardown.call(e,h,m.handle)!==!1||x.removeEvent(e,d,m.handle),delete c[d])}else for(d in c)x.event.remove(e,d+t[u],n,r,!0);x.isEmptyObject(c)&&(delete m.handle,x._removeData(e,\"events\"))}},trigger:function(n,r,i,o){var s,l,u,c,p,f,d,h=[i||a],g=v.call(n,\"type\")?n.type:n,m=v.call(n,\"namespace\")?n.namespace.split(\".\"):[];if(u=f=i=i||a,3!==i.nodeType&&8!==i.nodeType&&!nt.test(g+x.event.triggered)&&(g.indexOf(\".\")>=0&&(m=g.split(\".\"),g=m.shift(),m.sort()),l=0>g.indexOf(\":\")&&\"on\"+g,n=n[x.expando]?n:new x.Event(g,\"object\"==typeof n&&n),n.isTrigger=o?2:3,n.namespace=m.join(\".\"),n.namespace_re=n.namespace?RegExp(\"(^|\\\\.)\"+m.join(\"\\\\.(?:.*\\\\.|)\")+\"(\\\\.|$)\"):null,n.result=t,n.target||(n.target=i),r=null==r?[n]:x.makeArray(r,[n]),p=x.event.special[g]||{},o||!p.trigger||p.trigger.apply(i,r)!==!1)){if(!o&&!p.noBubble&&!x.isWindow(i)){for(c=p.delegateType||g,nt.test(c+g)||(u=u.parentNode);u;u=u.parentNode)h.push(u),f=u;f===(i.ownerDocument||a)&&h.push(f.defaultView||f.parentWindow||e)}d=0;while((u=h[d++])&&!n.isPropagationStopped())n.type=d>1?c:p.bindType||g,s=(x._data(u,\"events\")||{})[n.type]&&x._data(u,\"handle\"),s&&s.apply(u,r),s=l&&u[l],s&&x.acceptData(u)&&s.apply&&s.apply(u,r)===!1&&n.preventDefault();if(n.type=g,!o&&!n.isDefaultPrevented()&&(!p._default||p._default.apply(h.pop(),r)===!1)&&x.acceptData(i)&&l&&i[g]&&!x.isWindow(i)){f=i[l],f&&(i[l]=null),x.event.triggered=g;try{i[g]()}catch(y){}x.event.triggered=t,f&&(i[l]=f)}return n.result}},dispatch:function(e){e=x.event.fix(e);var n,r,i,o,a,s=[],l=g.call(arguments),u=(x._data(this,\"events\")||{})[e.type]||[],c=x.event.special[e.type]||{};if(l[0]=e,e.delegateTarget=this,!c.preDispatch||c.preDispatch.call(this,e)!==!1){s=x.event.handlers.call(this,e,u),n=0;while((o=s[n++])&&!e.isPropagationStopped()){e.currentTarget=o.elem,a=0;while((i=o.handlers[a++])&&!e.isImmediatePropagationStopped())(!e.namespace_re||e.namespace_re.test(i.namespace))&&(e.handleObj=i,e.data=i.data,r=((x.event.special[i.origType]||{}).handle||i.handler).apply(o.elem,l),r!==t&&(e.result=r)===!1&&(e.preventDefault(),e.stopPropagation()))}return c.postDispatch&&c.postDispatch.call(this,e),e.result}},handlers:function(e,n){var r,i,o,a,s=[],l=n.delegateCount,u=e.target;if(l&&u.nodeType&&(!e.button||\"click\"!==e.type))for(;u!=this;u=u.parentNode||this)if(1===u.nodeType&&(u.disabled!==!0||\"click\"!==e.type)){for(o=[],a=0;l>a;a++)i=n[a],r=i.selector+\" \",o[r]===t&&(o[r]=i.needsContext?x(r,this).index(u)>=0:x.find(r,this,null,[u]).length),o[r]&&o.push(i);o.length&&s.push({elem:u,handlers:o})}return n.length>l&&s.push({elem:this,handlers:n.slice(l)}),s},fix:function(e){if(e[x.expando])return e;var t,n,r,i=e.type,o=e,s=this.fixHooks[i];s||(this.fixHooks[i]=s=tt.test(i)?this.mouseHooks:et.test(i)?this.keyHooks:{}),r=s.props?this.props.concat(s.props):this.props,e=new x.Event(o),t=r.length;while(t--)n=r[t],e[n]=o[n];return e.target||(e.target=o.srcElement||a),3===e.target.nodeType&&(e.target=e.target.parentNode),e.metaKey=!!e.metaKey,s.filter?s.filter(e,o):e},props:\"altKey bubbles cancelable ctrlKey currentTarget eventPhase metaKey relatedTarget shiftKey target timeStamp view which\".split(\" \"),fixHooks:{},keyHooks:{props:\"char charCode key keyCode\".split(\" \"),filter:function(e,t){return null==e.which&&(e.which=null!=t.charCode?t.charCode:t.keyCode),e}},mouseHooks:{props:\"button buttons clientX clientY fromElement offsetX offsetY pageX pageY screenX screenY toElement\".split(\" \"),filter:function(e,n){var r,i,o,s=n.button,l=n.fromElement;return null==e.pageX&&null!=n.clientX&&(i=e.target.ownerDocument||a,o=i.documentElement,r=i.body,e.pageX=n.clientX+(o&&o.scrollLeft||r&&r.scrollLeft||0)-(o&&o.clientLeft||r&&r.clientLeft||0),e.pageY=n.clientY+(o&&o.scrollTop||r&&r.scrollTop||0)-(o&&o.clientTop||r&&r.clientTop||0)),!e.relatedTarget&&l&&(e.relatedTarget=l===e.target?n.toElement:l),e.which||s===t||(e.which=1&s?1:2&s?3:4&s?2:0),e}},special:{load:{noBubble:!0},focus:{trigger:function(){if(this!==at()&&this.focus)try{return this.focus(),!1}catch(e){}},delegateType:\"focusin\"},blur:{trigger:function(){return this===at()&&this.blur?(this.blur(),!1):t},delegateType:\"focusout\"},click:{trigger:function(){return x.nodeName(this,\"input\")&&\"checkbox\"===this.type&&this.click?(this.click(),!1):t},_default:function(e){return x.nodeName(e.target,\"a\")}},beforeunload:{postDispatch:function(e){e.result!==t&&(e.originalEvent.returnValue=e.result)}}},simulate:function(e,t,n,r){var i=x.extend(new x.Event,n,{type:e,isSimulated:!0,originalEvent:{}});r?x.event.trigger(i,null,t):x.event.dispatch.call(t,i),i.isDefaultPrevented()&&n.preventDefault()}},x.removeEvent=a.removeEventListener?function(e,t,n){e.removeEventListener&&e.removeEventListener(t,n,!1)}:function(e,t,n){var r=\"on\"+t;e.detachEvent&&(typeof e[r]===i&&(e[r]=null),e.detachEvent(r,n))},x.Event=function(e,n){return this instanceof x.Event?(e&&e.type?(this.originalEvent=e,this.type=e.type,this.isDefaultPrevented=e.defaultPrevented||e.returnValue===!1||e.getPreventDefault&&e.getPreventDefault()?it:ot):this.type=e,n&&x.extend(this,n),this.timeStamp=e&&e.timeStamp||x.now(),this[x.expando]=!0,t):new x.Event(e,n)},x.Event.prototype={isDefaultPrevented:ot,isPropagationStopped:ot,isImmediatePropagationStopped:ot,preventDefault:function(){var e=this.originalEvent;this.isDefaultPrevented=it,e&&(e.preventDefault?e.preventDefault():e.returnValue=!1)},stopPropagation:function(){var e=this.originalEvent;this.isPropagationStopped=it,e&&(e.stopPropagation&&e.stopPropagation(),e.cancelBubble=!0)},stopImmediatePropagation:function(){this.isImmediatePropagationStopped=it,this.stopPropagation()}},x.each({mouseenter:\"mouseover\",mouseleave:\"mouseout\"},function(e,t){x.event.special[e]={delegateType:t,bindType:t,handle:function(e){var n,r=this,i=e.relatedTarget,o=e.handleObj;return(!i||i!==r&&!x.contains(r,i))&&(e.type=o.origType,n=o.handler.apply(this,arguments),e.type=t),n}}}),x.support.submitBubbles||(x.event.special.submit={setup:function(){return x.nodeName(this,\"form\")?!1:(x.event.add(this,\"click._submit keypress._submit\",function(e){var n=e.target,r=x.nodeName(n,\"input\")||x.nodeName(n,\"button\")?n.form:t;r&&!x._data(r,\"submitBubbles\")&&(x.event.add(r,\"submit._submit\",function(e){e._submit_bubble=!0}),x._data(r,\"submitBubbles\",!0))}),t)},postDispatch:function(e){e._submit_bubble&&(delete e._submit_bubble,this.parentNode&&!e.isTrigger&&x.event.simulate(\"submit\",this.parentNode,e,!0))},teardown:function(){return x.nodeName(this,\"form\")?!1:(x.event.remove(this,\"._submit\"),t)}}),x.support.changeBubbles||(x.event.special.change={setup:function(){return Z.test(this.nodeName)?((\"checkbox\"===this.type||\"radio\"===this.type)&&(x.event.add(this,\"propertychange._change\",function(e){\"checked\"===e.originalEvent.propertyName&&(this._just_changed=!0)}),x.event.add(this,\"click._change\",function(e){this._just_changed&&!e.isTrigger&&(this._just_changed=!1),x.event.simulate(\"change\",this,e,!0)})),!1):(x.event.add(this,\"beforeactivate._change\",function(e){var t=e.target;Z.test(t.nodeName)&&!x._data(t,\"changeBubbles\")&&(x.event.add(t,\"change._change\",function(e){!this.parentNode||e.isSimulated||e.isTrigger||x.event.simulate(\"change\",this.parentNode,e,!0)}),x._data(t,\"changeBubbles\",!0))}),t)},handle:function(e){var n=e.target;return this!==n||e.isSimulated||e.isTrigger||\"radio\"!==n.type&&\"checkbox\"!==n.type?e.handleObj.handler.apply(this,arguments):t},teardown:function(){return x.event.remove(this,\"._change\"),!Z.test(this.nodeName)}}),x.support.focusinBubbles||x.each({focus:\"focusin\",blur:\"focusout\"},function(e,t){var n=0,r=function(e){x.event.simulate(t,e.target,x.event.fix(e),!0)};x.event.special[t]={setup:function(){0===n++&&a.addEventListener(e,r,!0)},teardown:function(){0===--n&&a.removeEventListener(e,r,!0)}}}),x.fn.extend({on:function(e,n,r,i,o){var a,s;if(\"object\"==typeof e){\"string\"!=typeof n&&(r=r||n,n=t);for(a in e)this.on(a,n,r,e[a],o);return this}if(null==r&&null==i?(i=n,r=n=t):null==i&&(\"string\"==typeof n?(i=r,r=t):(i=r,r=n,n=t)),i===!1)i=ot;else if(!i)return this;return 1===o&&(s=i,i=function(e){return x().off(e),s.apply(this,arguments)},i.guid=s.guid||(s.guid=x.guid++)),this.each(function(){x.event.add(this,e,i,r,n)})},one:function(e,t,n,r){return this.on(e,t,n,r,1)},off:function(e,n,r){var i,o;if(e&&e.preventDefault&&e.handleObj)return i=e.handleObj,x(e.delegateTarget).off(i.namespace?i.origType+\".\"+i.namespace:i.origType,i.selector,i.handler),this;if(\"object\"==typeof e){for(o in e)this.off(o,n,e[o]);return this}return(n===!1||\"function\"==typeof n)&&(r=n,n=t),r===!1&&(r=ot),this.each(function(){x.event.remove(this,e,r,n)})},trigger:function(e,t){return this.each(function(){x.event.trigger(e,t,this)})},triggerHandler:function(e,n){var r=this[0];return r?x.event.trigger(e,n,r,!0):t}});var st=/^.[^:#\\[\\.,]*$/,lt=/^(?:parents|prev(?:Until|All))/,ut=x.expr.match.needsContext,ct={children:!0,contents:!0,next:!0,prev:!0};x.fn.extend({find:function(e){var t,n=[],r=this,i=r.length;if(\"string\"!=typeof e)return this.pushStack(x(e).filter(function(){for(t=0;i>t;t++)if(x.contains(r[t],this))return!0}));for(t=0;i>t;t++)x.find(e,r[t],n);return n=this.pushStack(i>1?x.unique(n):n),n.selector=this.selector?this.selector+\" \"+e:e,n},has:function(e){var t,n=x(e,this),r=n.length;return this.filter(function(){for(t=0;r>t;t++)if(x.contains(this,n[t]))return!0})},not:function(e){return this.pushStack(ft(this,e||[],!0))},filter:function(e){return this.pushStack(ft(this,e||[],!1))},is:function(e){return!!ft(this,\"string\"==typeof e&&ut.test(e)?x(e):e||[],!1).length},closest:function(e,t){var n,r=0,i=this.length,o=[],a=ut.test(e)||\"string\"!=typeof e?x(e,t||this.context):0;for(;i>r;r++)for(n=this[r];n&&n!==t;n=n.parentNode)if(11>n.nodeType&&(a?a.index(n)>-1:1===n.nodeType&&x.find.matchesSelector(n,e))){n=o.push(n);break}return this.pushStack(o.length>1?x.unique(o):o)},index:function(e){return e?\"string\"==typeof e?x.inArray(this[0],x(e)):x.inArray(e.jquery?e[0]:e,this):this[0]&&this[0].parentNode?this.first().prevAll().length:-1},add:function(e,t){var n=\"string\"==typeof e?x(e,t):x.makeArray(e&&e.nodeType?[e]:e),r=x.merge(this.get(),n);return this.pushStack(x.unique(r))},addBack:function(e){return this.add(null==e?this.prevObject:this.prevObject.filter(e))}});function pt(e,t){do e=e[t];while(e&&1!==e.nodeType);return e}x.each({parent:function(e){var t=e.parentNode;return t&&11!==t.nodeType?t:null},parents:function(e){return x.dir(e,\"parentNode\")},parentsUntil:function(e,t,n){return x.dir(e,\"parentNode\",n)},next:function(e){return pt(e,\"nextSibling\")},prev:function(e){return pt(e,\"previousSibling\")},nextAll:function(e){return x.dir(e,\"nextSibling\")},prevAll:function(e){return x.dir(e,\"previousSibling\")},nextUntil:function(e,t,n){return x.dir(e,\"nextSibling\",n)},prevUntil:function(e,t,n){return x.dir(e,\"previousSibling\",n)},siblings:function(e){return x.sibling((e.parentNode||{}).firstChild,e)},children:function(e){return x.sibling(e.firstChild)},contents:function(e){return x.nodeName(e,\"iframe\")?e.contentDocument||e.contentWindow.document:x.merge([],e.childNodes)}},function(e,t){x.fn[e]=function(n,r){var i=x.map(this,t,n);return\"Until\"!==e.slice(-5)&&(r=n),r&&\"string\"==typeof r&&(i=x.filter(r,i)),this.length>1&&(ct[e]||(i=x.unique(i)),lt.test(e)&&(i=i.reverse())),this.pushStack(i)}}),x.extend({filter:function(e,t,n){var r=t[0];return n&&(e=\":not(\"+e+\")\"),1===t.length&&1===r.nodeType?x.find.matchesSelector(r,e)?[r]:[]:x.find.matches(e,x.grep(t,function(e){return 1===e.nodeType}))},dir:function(e,n,r){var i=[],o=e[n];while(o&&9!==o.nodeType&&(r===t||1!==o.nodeType||!x(o).is(r)))1===o.nodeType&&i.push(o),o=o[n];return i},sibling:function(e,t){var n=[];for(;e;e=e.nextSibling)1===e.nodeType&&e!==t&&n.push(e);return n}});function ft(e,t,n){if(x.isFunction(t))return x.grep(e,function(e,r){return!!t.call(e,r,e)!==n});if(t.nodeType)return x.grep(e,function(e){return e===t!==n});if(\"string\"==typeof t){if(st.test(t))return x.filter(t,e,n);t=x.filter(t,e)}return x.grep(e,function(e){return x.inArray(e,t)>=0!==n})}function dt(e){var t=ht.split(\"|\"),n=e.createDocumentFragment();if(n.createElement)while(t.length)n.createElement(t.pop());return n}var ht=\"abbr|article|aside|audio|bdi|canvas|data|datalist|details|figcaption|figure|footer|header|hgroup|mark|meter|nav|output|progress|section|summary|time|video\",gt=/ jQuery\\d+=\"(?:null|\\d+)\"/g,mt=RegExp(\"<(?:\"+ht+\")[\\\\s/>]\",\"i\"),yt=/^\\s+/,vt=/<(?!area|br|col|embed|hr|img|input|link|meta|param)(([\\w:]+)[^>]*)\\/>/gi,bt=/<([\\w:]+)/,xt=/<tbody/i,wt=/<|&#?\\w+;/,Tt=/<(?:script|style|link)/i,Ct=/^(?:checkbox|radio)$/i,Nt=/checked\\s*(?:[^=]|=\\s*.checked.)/i,kt=/^$|\\/(?:java|ecma)script/i,Et=/^true\\/(.*)/,St=/^\\s*<!(?:\\[CDATA\\[|--)|(?:\\]\\]|--)>\\s*$/g,At={option:[1,\"<select multiple=\'multiple\'>\",\"</select>\"],legend:[1,\"<fieldset>\",\"</fieldset>\"],area:[1,\"<map>\",\"</map>\"],param:[1,\"<object>\",\"</object>\"],thead:[1,\"<table>\",\"</table>\"],tr:[2,\"<table><tbody>\",\"</tbody></table>\"],col:[2,\"<table><tbody></tbody><colgroup>\",\"</colgroup></table>\"],td:[3,\"<table><tbody><tr>\",\"</tr></tbody></table>\"],_default:x.support.htmlSerialize?[0,\"\",\"\"]:[1,\"X<div>\",\"</div>\"]},jt=dt(a),Dt=jt.appendChild(a.createElement(\"div\"));At.optgroup=At.option,At.tbody=At.tfoot=At.colgroup=At.caption=At.thead,At.th=At.td,x.fn.extend({text:function(e){return x.access(this,function(e){return e===t?x.text(this):this.empty().append((this[0]&&this[0].ownerDocument||a).createTextNode(e))},null,e,arguments.length)},append:function(){return this.domManip(arguments,function(e){if(1===this.nodeType||11===this.nodeType||9===this.nodeType){var t=Lt(this,e);t.appendChild(e)}})},prepend:function(){return this.domManip(arguments,function(e){if(1===this.nodeType||11===this.nodeType||9===this.nodeType){var t=Lt(this,e);t.insertBefore(e,t.firstChild)}})},before:function(){return this.domManip(arguments,function(e){this.parentNode&&this.parentNode.insertBefore(e,this)})},after:function(){return this.domManip(arguments,function(e){this.parentNode&&this.parentNode.insertBefore(e,this.nextSibling)})},remove:function(e,t){var n,r=e?x.filter(e,this):this,i=0;for(;null!=(n=r[i]);i++)t||1!==n.nodeType||x.cleanData(Ft(n)),n.parentNode&&(t&&x.contains(n.ownerDocument,n)&&_t(Ft(n,\"script\")),n.parentNode.removeChild(n));return this},empty:function(){var e,t=0;for(;null!=(e=this[t]);t++){1===e.nodeType&&x.cleanData(Ft(e,!1));while(e.firstChild)e.removeChild(e.firstChild);e.options&&x.nodeName(e,\"select\")&&(e.options.length=0)}return this},clone:function(e,t){return e=null==e?!1:e,t=null==t?e:t,this.map(function(){return x.clone(this,e,t)})},html:function(e){return x.access(this,function(e){var n=this[0]||{},r=0,i=this.length;if(e===t)return 1===n.nodeType?n.innerHTML.replace(gt,\"\"):t;if(!(\"string\"!=typeof e||Tt.test(e)||!x.support.htmlSerialize&&mt.test(e)||!x.support.leadingWhitespace&&yt.test(e)||At[(bt.exec(e)||[\"\",\"\"])[1].toLowerCase()])){e=e.replace(vt,\"<$1></$2>\");try{for(;i>r;r++)n=this[r]||{},1===n.nodeType&&(x.cleanData(Ft(n,!1)),n.innerHTML=e);n=0}catch(o){}}n&&this.empty().append(e)},null,e,arguments.length)},replaceWith:function(){var e=x.map(this,function(e){return[e.nextSibling,e.parentNode]}),t=0;return this.domManip(arguments,function(n){var r=e[t++],i=e[t++];i&&(r&&r.parentNode!==i&&(r=this.nextSibling),x(this).remove(),i.insertBefore(n,r))},!0),t?this:this.remove()},detach:function(e){return this.remove(e,!0)},domManip:function(e,t,n){e=d.apply([],e);var r,i,o,a,s,l,u=0,c=this.length,p=this,f=c-1,h=e[0],g=x.isFunction(h);if(g||!(1>=c||\"string\"!=typeof h||x.support.checkClone)&&Nt.test(h))return this.each(function(r){var i=p.eq(r);g&&(e[0]=h.call(this,r,i.html())),i.domManip(e,t,n)});if(c&&(l=x.buildFragment(e,this[0].ownerDocument,!1,!n&&this),r=l.firstChild,1===l.childNodes.length&&(l=r),r)){for(a=x.map(Ft(l,\"script\"),Ht),o=a.length;c>u;u++)i=l,u!==f&&(i=x.clone(i,!0,!0),o&&x.merge(a,Ft(i,\"script\"))),t.call(this[u],i,u);if(o)for(s=a[a.length-1].ownerDocument,x.map(a,qt),u=0;o>u;u++)i=a[u],kt.test(i.type||\"\")&&!x._data(i,\"globalEval\")&&x.contains(s,i)&&(i.src?x._evalUrl(i.src):x.globalEval((i.text||i.textContent||i.innerHTML||\"\").replace(St,\"\")));l=r=null}return this}});function Lt(e,t){return x.nodeName(e,\"table\")&&x.nodeName(1===t.nodeType?t:t.firstChild,\"tr\")?e.getElementsByTagName(\"tbody\")[0]||e.appendChild(e.ownerDocument.createElement(\"tbody\")):e}function Ht(e){return e.type=(null!==x.find.attr(e,\"type\"))+\"/\"+e.type,e}function qt(e){var t=Et.exec(e.type);return t?e.type=t[1]:e.removeAttribute(\"type\"),e}function _t(e,t){var n,r=0;for(;null!=(n=e[r]);r++)x._data(n,\"globalEval\",!t||x._data(t[r],\"globalEval\"))}function Mt(e,t){if(1===t.nodeType&&x.hasData(e)){var n,r,i,o=x._data(e),a=x._data(t,o),s=o.events;if(s){delete a.handle,a.events={};for(n in s)for(r=0,i=s[n].length;i>r;r++)x.event.add(t,n,s[n][r])}a.data&&(a.data=x.extend({},a.data))}}function Ot(e,t){var n,r,i;if(1===t.nodeType){if(n=t.nodeName.toLowerCase(),!x.support.noCloneEvent&&t[x.expando]){i=x._data(t);for(r in i.events)x.removeEvent(t,r,i.handle);t.removeAttribute(x.expando)}\"script\"===n&&t.text!==e.text?(Ht(t).text=e.text,qt(t)):\"object\"===n?(t.parentNode&&(t.outerHTML=e.outerHTML),x.support.html5Clone&&e.innerHTML&&!x.trim(t.innerHTML)&&(t.innerHTML=e.innerHTML)):\"input\"===n&&Ct.test(e.type)?(t.defaultChecked=t.checked=e.checked,t.value!==e.value&&(t.value=e.value)):\"option\"===n?t.defaultSelected=t.selected=e.defaultSelected:(\"input\"===n||\"textarea\"===n)&&(t.defaultValue=e.defaultValue)}}x.each({appendTo:\"append\",prependTo:\"prepend\",insertBefore:\"before\",insertAfter:\"after\",replaceAll:\"replaceWith\"},function(e,t){x.fn[e]=function(e){var n,r=0,i=[],o=x(e),a=o.length-1;for(;a>=r;r++)n=r===a?this:this.clone(!0),x(o[r])[t](n),h.apply(i,n.get());return this.pushStack(i)}});function Ft(e,n){var r,o,a=0,s=typeof e.getElementsByTagName!==i?e.getElementsByTagName(n||\"*\"):typeof e.querySelectorAll!==i?e.querySelectorAll(n||\"*\"):t;if(!s)for(s=[],r=e.childNodes||e;null!=(o=r[a]);a++)!n||x.nodeName(o,n)?s.push(o):x.merge(s,Ft(o,n));return n===t||n&&x.nodeName(e,n)?x.merge([e],s):s}function Bt(e){Ct.test(e.type)&&(e.defaultChecked=e.checked)}x.extend({clone:function(e,t,n){var r,i,o,a,s,l=x.contains(e.ownerDocument,e);if(x.support.html5Clone||x.isXMLDoc(e)||!mt.test(\"<\"+e.nodeName+\">\")?o=e.cloneNode(!0):(Dt.innerHTML=e.outerHTML,Dt.removeChild(o=Dt.firstChild)),!(x.support.noCloneEvent&&x.support.noCloneChecked||1!==e.nodeType&&11!==e.nodeType||x.isXMLDoc(e)))for(r=Ft(o),s=Ft(e),a=0;null!=(i=s[a]);++a)r[a]&&Ot(i,r[a]);if(t)if(n)for(s=s||Ft(e),r=r||Ft(o),a=0;null!=(i=s[a]);a++)Mt(i,r[a]);else Mt(e,o);return r=Ft(o,\"script\"),r.length>0&&_t(r,!l&&Ft(e,\"script\")),r=s=i=null,o},buildFragment:function(e,t,n,r){var i,o,a,s,l,u,c,p=e.length,f=dt(t),d=[],h=0;for(;p>h;h++)if(o=e[h],o||0===o)if(\"object\"===x.type(o))x.merge(d,o.nodeType?[o]:o);else if(wt.test(o)){s=s||f.appendChild(t.createElement(\"div\")),l=(bt.exec(o)||[\"\",\"\"])[1].toLowerCase(),c=At[l]||At._default,s.innerHTML=c[1]+o.replace(vt,\"<$1></$2>\")+c[2],i=c[0];while(i--)s=s.lastChild;if(!x.support.leadingWhitespace&&yt.test(o)&&d.push(t.createTextNode(yt.exec(o)[0])),!x.support.tbody){o=\"table\"!==l||xt.test(o)?\"<table>\"!==c[1]||xt.test(o)?0:s:s.firstChild,i=o&&o.childNodes.length;while(i--)x.nodeName(u=o.childNodes[i],\"tbody\")&&!u.childNodes.length&&o.removeChild(u)}x.merge(d,s.childNodes),s.textContent=\"\";while(s.firstChild)s.removeChild(s.firstChild);s=f.lastChild}else d.push(t.createTextNode(o));s&&f.removeChild(s),x.support.appendChecked||x.grep(Ft(d,\"input\"),Bt),h=0;while(o=d[h++])if((!r||-1===x.inArray(o,r))&&(a=x.contains(o.ownerDocument,o),s=Ft(f.appendChild(o),\"script\"),a&&_t(s),n)){i=0;while(o=s[i++])kt.test(o.type||\"\")&&n.push(o)}return s=null,f},cleanData:function(e,t){var n,r,o,a,s=0,l=x.expando,u=x.cache,c=x.support.deleteExpando,f=x.event.special;for(;null!=(n=e[s]);s++)if((t||x.acceptData(n))&&(o=n[l],a=o&&u[o])){if(a.events)for(r in a.events)f[r]?x.event.remove(n,r):x.removeEvent(n,r,a.handle);");
                    yaz.WriteLine("u[o]&&(delete u[o],c?delete n[l]:typeof n.removeAttribute!==i?n.removeAttribute(l):n[l]=null,p.push(o))}},_evalUrl:function(e){return x.ajax({url:e,type:\"GET\",dataType:\"script\",async:!1,global:!1,\"throws\":!0})}}),x.fn.extend({wrapAll:function(e){if(x.isFunction(e))return this.each(function(t){x(this).wrapAll(e.call(this,t))});if(this[0]){var t=x(e,this[0].ownerDocument).eq(0).clone(!0);this[0].parentNode&&t.insertBefore(this[0]),t.map(function(){var e=this;while(e.firstChild&&1===e.firstChild.nodeType)e=e.firstChild;return e}).append(this)}return this},wrapInner:function(e){return x.isFunction(e)?this.each(function(t){x(this).wrapInner(e.call(this,t))}):this.each(function(){var t=x(this),n=t.contents();n.length?n.wrapAll(e):t.append(e)})},wrap:function(e){var t=x.isFunction(e);return this.each(function(n){x(this).wrapAll(t?e.call(this,n):e)})},unwrap:function(){return this.parent().each(function(){x.nodeName(this,\"body\")||x(this).replaceWith(this.childNodes)}).end()}});var Pt,Rt,Wt,$t=/alpha\\([^)]*\\)/i,It=/opacity\\s*=\\s*([^)]*)/,zt=/^(top|right|bottom|left)$/,Xt=/^(none|table(?!-c[ea]).+)/,Ut=/^margin/,Vt=RegExp(\"^(\"+w+\")(.*)$\",\"i\"),Yt=RegExp(\"^(\"+w+\")(?!px)[a-z%]+$\",\"i\"),Jt=RegExp(\"^([+-])=(\"+w+\")\",\"i\"),Gt={BODY:\"block\"},Qt={position:\"absolute\",visibility:\"hidden\",display:\"block\"},Kt={letterSpacing:0,fontWeight:400},Zt=[\"Top\",\"Right\",\"Bottom\",\"Left\"],en=[\"Webkit\",\"O\",\"Moz\",\"ms\"];function tn(e,t){if(t in e)return t;var n=t.charAt(0).toUpperCase()+t.slice(1),r=t,i=en.length;while(i--)if(t=en[i]+n,t in e)return t;return r}function nn(e,t){return e=t||e,\"none\"===x.css(e,\"display\")||!x.contains(e.ownerDocument,e)}function rn(e,t){var n,r,i,o=[],a=0,s=e.length;for(;s>a;a++)r=e[a],r.style&&(o[a]=x._data(r,\"olddisplay\"),n=r.style.display,t?(o[a]||\"none\"!==n||(r.style.display=\"\"),\"\"===r.style.display&&nn(r)&&(o[a]=x._data(r,\"olddisplay\",ln(r.nodeName)))):o[a]||(i=nn(r),(n&&\"none\"!==n||!i)&&x._data(r,\"olddisplay\",i?n:x.css(r,\"display\"))));for(a=0;s>a;a++)r=e[a],r.style&&(t&&\"none\"!==r.style.display&&\"\"!==r.style.display||(r.style.display=t?o[a]||\"\":\"none\"));return e}x.fn.extend({css:function(e,n){return x.access(this,function(e,n,r){var i,o,a={},s=0;if(x.isArray(n)){for(o=Rt(e),i=n.length;i>s;s++)a[n[s]]=x.css(e,n[s],!1,o);return a}return r!==t?x.style(e,n,r):x.css(e,n)},e,n,arguments.length>1)},show:function(){return rn(this,!0)},hide:function(){return rn(this)},toggle:function(e){return\"boolean\"==typeof e?e?this.show():this.hide():this.each(function(){nn(this)?x(this).show():x(this).hide()})}}),x.extend({cssHooks:{opacity:{get:function(e,t){if(t){var n=Wt(e,\"opacity\");return\"\"===n?\"1\":n}}}},cssNumber:{columnCount:!0,fillOpacity:!0,fontWeight:!0,lineHeight:!0,opacity:!0,order:!0,orphans:!0,widows:!0,zIndex:!0,zoom:!0},cssProps:{\"float\":x.support.cssFloat?\"cssFloat\":\"styleFloat\"},style:function(e,n,r,i){if(e&&3!==e.nodeType&&8!==e.nodeType&&e.style){var o,a,s,l=x.camelCase(n),u=e.style;if(n=x.cssProps[l]||(x.cssProps[l]=tn(u,l)),s=x.cssHooks[n]||x.cssHooks[l],r===t)return s&&\"get\"in s&&(o=s.get(e,!1,i))!==t?o:u[n];if(a=typeof r,\"string\"===a&&(o=Jt.exec(r))&&(r=(o[1]+1)*o[2]+parseFloat(x.css(e,n)),a=\"number\"),!(null==r||\"number\"===a&&isNaN(r)||(\"number\"!==a||x.cssNumber[l]||(r+=\"px\"),x.support.clearCloneStyle||\"\"!==r||0!==n.indexOf(\"background\")||(u[n]=\"inherit\"),s&&\"set\"in s&&(r=s.set(e,r,i))===t)))try{u[n]=r}catch(c){}}},css:function(e,n,r,i){var o,a,s,l=x.camelCase(n);return n=x.cssProps[l]||(x.cssProps[l]=tn(e.style,l)),s=x.cssHooks[n]||x.cssHooks[l],s&&\"get\"in s&&(a=s.get(e,!0,r)),a===t&&(a=Wt(e,n,i)),\"normal\"===a&&n in Kt&&(a=Kt[n]),\"\"===r||r?(o=parseFloat(a),r===!0||x.isNumeric(o)?o||0:a):a}}),e.getComputedStyle?(Rt=function(t){return e.getComputedStyle(t,null)},Wt=function(e,n,r){var i,o,a,s=r||Rt(e),l=s?s.getPropertyValue(n)||s[n]:t,u=e.style;return s&&(\"\"!==l||x.contains(e.ownerDocument,e)||(l=x.style(e,n)),Yt.test(l)&&Ut.test(n)&&(i=u.width,o=u.minWidth,a=u.maxWidth,u.minWidth=u.maxWidth=u.width=l,l=s.width,u.width=i,u.minWidth=o,u.maxWidth=a)),l}):a.documentElement.currentStyle&&(Rt=function(e){return e.currentStyle},Wt=function(e,n,r){var i,o,a,s=r||Rt(e),l=s?s[n]:t,u=e.style;return null==l&&u&&u[n]&&(l=u[n]),Yt.test(l)&&!zt.test(n)&&(i=u.left,o=e.runtimeStyle,a=o&&o.left,a&&(o.left=e.currentStyle.left),u.left=\"fontSize\"===n?\"1em\":l,l=u.pixelLeft+\"px\",u.left=i,a&&(o.left=a)),\"\"===l?\"auto\":l});function on(e,t,n){var r=Vt.exec(t);return r?Math.max(0,r[1]-(n||0))+(r[2]||\"px\"):t}function an(e,t,n,r,i){var o=n===(r?\"border\":\"content\")?4:\"width\"===t?1:0,a=0;for(;4>o;o+=2)\"margin\"===n&&(a+=x.css(e,n+Zt[o],!0,i)),r?(\"content\"===n&&(a-=x.css(e,\"padding\"+Zt[o],!0,i)),\"margin\"!==n&&(a-=x.css(e,\"border\"+Zt[o]+\"Width\",!0,i))):(a+=x.css(e,\"padding\"+Zt[o],!0,i),\"padding\"!==n&&(a+=x.css(e,\"border\"+Zt[o]+\"Width\",!0,i)));return a}function sn(e,t,n){var r=!0,i=\"width\"===t?e.offsetWidth:e.offsetHeight,o=Rt(e),a=x.support.boxSizing&&\"border-box\"===x.css(e,\"boxSizing\",!1,o);if(0>=i||null==i){if(i=Wt(e,t,o),(0>i||null==i)&&(i=e.style[t]),Yt.test(i))return i;r=a&&(x.support.boxSizingReliable||i===e.style[t]),i=parseFloat(i)||0}return i+an(e,t,n||(a?\"border\":\"content\"),r,o)+\"px\"}function ln(e){var t=a,n=Gt[e];return n||(n=un(e,t),\"none\"!==n&&n||(Pt=(Pt||x(\"<iframe frameborder=\'0\' width=\'0\' height=\'0\'/>\").css(\"cssText\",\"display:block !important\")).appendTo(t.documentElement),t=(Pt[0].contentWindow||Pt[0].contentDocument).document,t.write(\"<!doctype html><html><body>\"),t.close(),n=un(e,t),Pt.detach()),Gt[e]=n),n}function un(e,t){var n=x(t.createElement(e)).appendTo(t.body),r=x.css(n[0],\"display\");return n.remove(),r}x.each([\"height\",\"width\"],function(e,n){x.cssHooks[n]={get:function(e,r,i){return r?0===e.offsetWidth&&Xt.test(x.css(e,\"display\"))?x.swap(e,Qt,function(){return sn(e,n,i)}):sn(e,n,i):t},set:function(e,t,r){var i=r&&Rt(e);return on(e,t,r?an(e,n,r,x.support.boxSizing&&\"border-box\"===x.css(e,\"boxSizing\",!1,i),i):0)}}}),x.support.opacity||(x.cssHooks.opacity={get:function(e,t){return It.test((t&&e.currentStyle?e.currentStyle.filter:e.style.filter)||\"\")?.01*parseFloat(RegExp.$1)+\"\":t?\"1\":\"\"},set:function(e,t){var n=e.style,r=e.currentStyle,i=x.isNumeric(t)?\"alpha(opacity=\"+100*t+\")\":\"\",o=r&&r.filter||n.filter||\"\";n.zoom=1,(t>=1||\"\"===t)&&\"\"===x.trim(o.replace($t,\"\"))&&n.removeAttribute&&(n.removeAttribute(\"filter\"),\"\"===t||r&&!r.filter)||(n.filter=$t.test(o)?o.replace($t,i):o+\" \"+i)}}),x(function(){x.support.reliableMarginRight||(x.cssHooks.marginRight={get:function(e,n){return n?x.swap(e,{display:\"inline-block\"},Wt,[e,\"marginRight\"]):t}}),!x.support.pixelPosition&&x.fn.position&&x.each([\"top\",\"left\"],function(e,n){x.cssHooks[n]={get:function(e,r){return r?(r=Wt(e,n),Yt.test(r)?x(e).position()[n]+\"px\":r):t}}})}),x.expr&&x.expr.filters&&(x.expr.filters.hidden=function(e){return 0>=e.offsetWidth&&0>=e.offsetHeight||!x.support.reliableHiddenOffsets&&\"none\"===(e.style&&e.style.display||x.css(e,\"display\"))},x.expr.filters.visible=function(e){return!x.expr.filters.hidden(e)}),x.each({margin:\"\",padding:\"\",border:\"Width\"},function(e,t){x.cssHooks[e+t]={expand:function(n){var r=0,i={},o=\"string\"==typeof n?n.split(\" \"):[n];for(;4>r;r++)i[e+Zt[r]+t]=o[r]||o[r-2]||o[0];return i}},Ut.test(e)||(x.cssHooks[e+t].set=on)});var cn=/%20/g,pn=/\\[\\]$/,fn=/\\r?\\n/g,dn=/^(?:submit|button|image|reset|file)$/i,hn=/^(?:input|select|textarea|keygen)/i;x.fn.extend({serialize:function(){return x.param(this.serializeArray())},serializeArray:function(){return this.map(function(){var e=x.prop(this,\"elements\");return e?x.makeArray(e):this}).filter(function(){var e=this.type;return this.name&&!x(this).is(\":disabled\")&&hn.test(this.nodeName)&&!dn.test(e)&&(this.checked||!Ct.test(e))}).map(function(e,t){var n=x(this).val();return null==n?null:x.isArray(n)?x.map(n,function(e){return{name:t.name,value:e.replace(fn,\"\\r\\n\")}}):{name:t.name,value:n.replace(fn,\"\\r\\n\")}}).get()}}),x.param=function(e,n){var r,i=[],o=function(e,t){t=x.isFunction(t)?t():null==t?\"\":t,i[i.length]=encodeURIComponent(e)+\"=\"+encodeURIComponent(t)};if(n===t&&(n=x.ajaxSettings&&x.ajaxSettings.traditional),x.isArray(e)||e.jquery&&!x.isPlainObject(e))x.each(e,function(){o(this.name,this.value)});else for(r in e)gn(r,e[r],n,o);return i.join(\"&\").replace(cn,\"+\")};function gn(e,t,n,r){var i;if(x.isArray(t))x.each(t,function(t,i){n||pn.test(e)?r(e,i):gn(e+\"[\"+(\"object\"==typeof i?t:\"\")+\"]\",i,n,r)});else if(n||\"object\"!==x.type(t))r(e,t);else for(i in t)gn(e+\"[\"+i+\"]\",t[i],n,r)}x.each(\"blur focus focusin focusout load resize scroll unload click dblclick mousedown mouseup mousemove mouseover mouseout mouseenter mouseleave change select submit keydown keypress keyup error contextmenu\".split(\" \"),function(e,t){x.fn[t]=function(e,n){return arguments.length>0?this.on(t,null,e,n):this.trigger(t)}}),x.fn.extend({hover:function(e,t){return this.mouseenter(e).mouseleave(t||e)},bind:function(e,t,n){return this.on(e,null,t,n)},unbind:function(e,t){return this.off(e,null,t)},delegate:function(e,t,n,r){return this.on(t,e,n,r)},undelegate:function(e,t,n){return 1===arguments.length?this.off(e,\"**\"):this.off(t,e||\"**\",n)}});var mn,yn,vn=x.now(),bn=/\\?/,xn=/#.*$/,wn=/([?&])_=[^&]*/,Tn=/^(.*?):[ \\t]*([^\\r\\n]*)\\r?$/gm,Cn=/^(?:about|app|app-storage|.+-extension|file|res|widget):$/,Nn=/^(?:GET|HEAD)$/,kn=/^\\/\\//,En=/^([\\w.+-]+:)(?:\\/\\/([^\\/?#:]*)(?::(\\d+)|)|)/,Sn=x.fn.load,An={},jn={},Dn=\"*/\".concat(\"*\");try{yn=o.href}catch(Ln){yn=a.createElement(\"a\"),yn.href=\"\",yn=yn.href}mn=En.exec(yn.toLowerCase())||[];function Hn(e){return function(t,n){\"string\"!=typeof t&&(n=t,t=\"*\");var r,i=0,o=t.toLowerCase().match(T)||[];if(x.isFunction(n))while(r=o[i++])\"+\"===r[0]?(r=r.slice(1)||\"*\",(e[r]=e[r]||[]).unshift(n)):(e[r]=e[r]||[]).push(n)}}function qn(e,n,r,i){var o={},a=e===jn;function s(l){var u;return o[l]=!0,x.each(e[l]||[],function(e,l){var c=l(n,r,i);return\"string\"!=typeof c||a||o[c]?a?!(u=c):t:(n.dataTypes.unshift(c),s(c),!1)}),u}return s(n.dataTypes[0])||!o[\"*\"]&&s(\"*\")}function _n(e,n){var r,i,o=x.ajaxSettings.flatOptions||{};for(i in n)n[i]!==t&&((o[i]?e:r||(r={}))[i]=n[i]);return r&&x.extend(!0,e,r),e}x.fn.load=function(e,n,r){if(\"string\"!=typeof e&&Sn)return Sn.apply(this,arguments);var i,o,a,s=this,l=e.indexOf(\" \");return l>=0&&(i=e.slice(l,e.length),e=e.slice(0,l)),x.isFunction(n)?(r=n,n=t):n&&\"object\"==typeof n&&(a=\"POST\"),s.length>0&&x.ajax({url:e,type:a,dataType:\"html\",data:n}).done(function(e){o=arguments,s.html(i?x(\"<div>\").append(x.parseHTML(e)).find(i):e)}).complete(r&&function(e,t){s.each(r,o||[e.responseText,t,e])}),this},x.each([\"ajaxStart\",\"ajaxStop\",\"ajaxComplete\",\"ajaxError\",\"ajaxSuccess\",\"ajaxSend\"],function(e,t){x.fn[t]=function(e){return this.on(t,e)}}),x.extend({active:0,lastModified:{},etag:{},ajaxSettings:{url:yn,type:\"GET\",isLocal:Cn.test(mn[1]),global:!0,processData:!0,async:!0,contentType:\"application/x-www-form-urlencoded; charset=UTF-8\",accepts:{\"*\":Dn,text:\"text/plain\",html:\"text/html\",xml:\"application/xml, text/xml\",json:\"application/json, text/javascript\"},contents:{xml:/xml/,html:/html/,json:/json/},responseFields:{xml:\"responseXML\",text:\"responseText\",json:\"responseJSON\"},converters:{\"* text\":String,\"text html\":!0,\"text json\":x.parseJSON,\"text xml\":x.parseXML},flatOptions:{url:!0,context:!0}},ajaxSetup:function(e,t){return t?_n(_n(e,x.ajaxSettings),t):_n(x.ajaxSettings,e)},ajaxPrefilter:Hn(An),ajaxTransport:Hn(jn),ajax:function(e,n){\"object\"==typeof e&&(n=e,e=t),n=n||{};var r,i,o,a,s,l,u,c,p=x.ajaxSetup({},n),f=p.context||p,d=p.context&&(f.nodeType||f.jquery)?x(f):x.event,h=x.Deferred(),g=x.Callbacks(\"once memory\"),m=p.statusCode||{},y={},v={},b=0,w=\"canceled\",C={readyState:0,getResponseHeader:function(e){var t;if(2===b){if(!c){c={};while(t=Tn.exec(a))c[t[1].toLowerCase()]=t[2]}t=c[e.toLowerCase()]}return null==t?null:t},getAllResponseHeaders:function(){return 2===b?a:null},setRequestHeader:function(e,t){var n=e.toLowerCase();return b||(e=v[n]=v[n]||e,y[e]=t),this},overrideMimeType:function(e){return b||(p.mimeType=e),this},statusCode:function(e){var t;if(e)if(2>b)for(t in e)m[t]=[m[t],e[t]];else C.always(e[C.status]);return this},abort:function(e){var t=e||w;return u&&u.abort(t),k(0,t),this}};if(h.promise(C).complete=g.add,C.success=C.done,C.error=C.fail,p.url=((e||p.url||yn)+\"\").replace(xn,\"\").replace(kn,mn[1]+\"//\"),p.type=n.method||n.type||p.method||p.type,p.dataTypes=x.trim(p.dataType||\"*\").toLowerCase().match(T)||[\"\"],null==p.crossDomain&&(r=En.exec(p.url.toLowerCase()),p.crossDomain=!(!r||r[1]===mn[1]&&r[2]===mn[2]&&(r[3]||(\"http:\"===r[1]?\"80\":\"443\"))===(mn[3]||(\"http:\"===mn[1]?\"80\":\"443\")))),p.data&&p.processData&&\"string\"!=typeof p.data&&(p.data=x.param(p.data,p.traditional)),qn(An,p,n,C),2===b)return C;l=p.global,l&&0===x.active++&&x.event.trigger(\"ajaxStart\"),p.type=p.type.toUpperCase(),p.hasContent=!Nn.test(p.type),o=p.url,p.hasContent||(p.data&&(o=p.url+=(bn.test(o)?\"&\":\"?\")+p.data,delete p.data),p.cache===!1&&(p.url=wn.test(o)?o.replace(wn,\"$1_=\"+vn++):o+(bn.test(o)?\"&\":\"?\")+\"_=\"+vn++)),p.ifModified&&(x.lastModified[o]&&C.setRequestHeader(\"If-Modified-Since\",x.lastModified[o]),x.etag[o]&&C.setRequestHeader(\"If-None-Match\",x.etag[o])),(p.data&&p.hasContent&&p.contentType!==!1||n.contentType)&&C.setRequestHeader(\"Content-Type\",p.contentType),C.setRequestHeader(\"Accept\",p.dataTypes[0]&&p.accepts[p.dataTypes[0]]?p.accepts[p.dataTypes[0]]+(\"*\"!==p.dataTypes[0]?\", \"+Dn+\"; q=0.01\":\"\"):p.accepts[\"*\"]);for(i in p.headers)C.setRequestHeader(i,p.headers[i]);if(p.beforeSend&&(p.beforeSend.call(f,C,p)===!1||2===b))return C.abort();w=\"abort\";for(i in{success:1,error:1,complete:1})C[i](p[i]);if(u=qn(jn,p,n,C)){C.readyState=1,l&&d.trigger(\"ajaxSend\",[C,p]),p.async&&p.timeout>0&&(s=setTimeout(function(){C.abort(\"timeout\")},p.timeout));try{b=1,u.send(y,k)}catch(N){if(!(2>b))throw N;k(-1,N)}}else k(-1,\"No Transport\");function k(e,n,r,i){var c,y,v,w,T,N=n;2!==b&&(b=2,s&&clearTimeout(s),u=t,a=i||\"\",C.readyState=e>0?4:0,c=e>=200&&300>e||304===e,r&&(w=Mn(p,C,r)),w=On(p,w,C,c),c?(p.ifModified&&(T=C.getResponseHeader(\"Last-Modified\"),T&&(x.lastModified[o]=T),T=C.getResponseHeader(\"etag\"),T&&(x.etag[o]=T)),204===e||\"HEAD\"===p.type?N=\"nocontent\":304===e?N=\"notmodified\":(N=w.state,y=w.data,v=w.error,c=!v)):(v=N,(e||!N)&&(N=\"error\",0>e&&(e=0))),C.status=e,C.statusText=(n||N)+\"\",c?h.resolveWith(f,[y,N,C]):h.rejectWith(f,[C,N,v]),C.statusCode(m),m=t,l&&d.trigger(c?\"ajaxSuccess\":\"ajaxError\",[C,p,c?y:v]),g.fireWith(f,[C,N]),l&&(d.trigger(\"ajaxComplete\",[C,p]),--x.active||x.event.trigger(\"ajaxStop\")))}return C},getJSON:function(e,t,n){return x.get(e,t,n,\"json\")},getScript:function(e,n){return x.get(e,t,n,\"script\")}}),x.each([\"get\",\"post\"],function(e,n){x[n]=function(e,r,i,o){return x.isFunction(r)&&(o=o||i,i=r,r=t),x.ajax({url:e,type:n,dataType:o,data:r,success:i})}});function Mn(e,n,r){var i,o,a,s,l=e.contents,u=e.dataTypes;while(\"*\"===u[0])u.shift(),o===t&&(o=e.mimeType||n.getResponseHeader(\"Content-Type\"));if(o)for(s in l)if(l[s]&&l[s].test(o)){u.unshift(s);break}if(u[0]in r)a=u[0];else{for(s in r){if(!u[0]||e.converters[s+\" \"+u[0]]){a=s;break}i||(i=s)}a=a||i}return a?(a!==u[0]&&u.unshift(a),r[a]):t}function On(e,t,n,r){var i,o,a,s,l,u={},c=e.dataTypes.slice();if(c[1])for(a in e.converters)u[a.toLowerCase()]=e.converters[a];o=c.shift();while(o)if(e.responseFields[o]&&(n[e.responseFields[o]]=t),!l&&r&&e.dataFilter&&(t=e.dataFilter(t,e.dataType)),l=o,o=c.shift())if(\"*\"===o)o=l;else if(\"*\"!==l&&l!==o){if(a=u[l+\" \"+o]||u[\"* \"+o],!a)for(i in u)if(s=i.split(\" \"),s[1]===o&&(a=u[l+\" \"+s[0]]||u[\"* \"+s[0]])){a===!0?a=u[i]:u[i]!==!0&&(o=s[0],c.unshift(s[1]));break}if(a!==!0)if(a&&e[\"throws\"])t=a(t);else try{t=a(t)}catch(p){return{state:\"parsererror\",error:a?p:\"No conversion from \"+l+\" to \"+o}}}return{state:\"success\",data:t}}x.ajaxSetup({accepts:{script:\"text/javascript, application/javascript, application/ecmascript, application/x-ecmascript\"},contents:{script:/(?:java|ecma)script/},converters:{\"text script\":function(e){return x.globalEval(e),e}}}),x.ajaxPrefilter(\"script\",function(e){e.cache===t&&(e.cache=!1),e.crossDomain&&(e.type=\"GET\",e.global=!1)}),x.ajaxTransport(\"script\",function(e){if(e.crossDomain){var n,r=a.head||x(\"head\")[0]||a.documentElement;return{send:function(t,i){n=a.createElement(\"script\"),n.async=!0,e.scriptCharset&&(n.charset=e.scriptCharset),n.src=e.url,n.onload=n.onreadystatechange=function(e,t){(t||!n.readyState||/loaded|complete/.test(n.readyState))&&(n.onload=n.onreadystatechange=null,n.parentNode&&n.parentNode.removeChild(n),n=null,t||i(200,\"success\"))},r.insertBefore(n,r.firstChild)},abort:function(){n&&n.onload(t,!0)}}}});var Fn=[],Bn=/(=)\\?(?=&|$)|\\?\\?/;x.ajaxSetup({jsonp:\"callback\",jsonpCallback:function(){var e=Fn.pop()||x.expando+\"_\"+vn++;return this[e]=!0,e}}),x.ajaxPrefilter(\"json jsonp\",function(n,r,i){var o,a,s,l=n.jsonp!==!1&&(Bn.test(n.url)?\"url\":\"string\"==typeof n.data&&!(n.contentType||\"\").indexOf(\"application/x-www-form-urlencoded\")&&Bn.test(n.data)&&\"data\");return l||\"jsonp\"===n.dataTypes[0]?(o=n.jsonpCallback=x.isFunction(n.jsonpCallback)?n.jsonpCallback():n.jsonpCallback,l?n[l]=n[l].replace(Bn,\"$1\"+o):n.jsonp!==!1&&(n.url+=(bn.test(n.url)?\"&\":\"?\")+n.jsonp+\"=\"+o),n.converters[\"script json\"]=function(){return s||x.error(o+\" was not called\"),s[0]},n.dataTypes[0]=\"json\",a=e[o],e[o]=function(){s=arguments},i.always(function(){e[o]=a,n[o]&&(n.jsonpCallback=r.jsonpCallback,Fn.push(o)),s&&x.isFunction(a)&&a(s[0]),s=a=t}),\"script\"):t});var Pn,Rn,Wn=0,$n=e.ActiveXObject&&function(){var e;for(e in Pn)Pn[e](t,!0)};function In(){try{return new e.XMLHttpRequest}catch(t){}}function zn(){try{return new e.ActiveXObject(\"Microsoft.XMLHTTP\")}catch(t){}}x.ajaxSettings.xhr=e.ActiveXObject?function(){return!this.isLocal&&In()||zn()}:In,Rn=x.ajaxSettings.xhr(),x.support.cors=!!Rn&&\"withCredentials\"in Rn,Rn=x.support.ajax=!!Rn,Rn&&x.ajaxTransport(function(n){if(!n.crossDomain||x.support.cors){var r;return{send:function(i,o){var a,s,l=n.xhr();if(n.username?l.open(n.type,n.url,n.async,n.username,n.password):l.open(n.type,n.url,n.async),n.xhrFields)for(s in n.xhrFields)l[s]=n.xhrFields[s];n.mimeType&&l.overrideMimeType&&l.overrideMimeType(n.mimeType),n.crossDomain||i[\"X-Requested-With\"]||(i[\"X-Requested-With\"]=\"XMLHttpRequest\");try{for(s in i)l.setRequestHeader(s,i[s])}catch(u){}l.send(n.hasContent&&n.data||null),r=function(e,i){var s,u,c,p;try{if(r&&(i||4===l.readyState))if(r=t,a&&(l.onreadystatechange=x.noop,$n&&delete Pn[a]),i)4!==l.readyState&&l.abort();else{p={},s=l.status,u=l.getAllResponseHeaders(),\"string\"==typeof l.responseText&&(p.text=l.responseText);try{c=l.statusText}catch(f){c=\"\"}s||!n.isLocal||n.crossDomain?1223===s&&(s=204):s=p.text?200:404}}catch(d){i||o(-1,d)}p&&o(s,c,p,u)},n.async?4===l.readyState?setTimeout(r):(a=++Wn,$n&&(Pn||(Pn={},x(e).unload($n)),Pn[a]=r),l.onreadystatechange=r):r()},abort:function(){r&&r(t,!0)}}}});var Xn,Un,Vn=/^(?:toggle|show|hide)$/,Yn=RegExp(\"^(?:([+-])=|)(\"+w+\")([a-z%]*)$\",\"i\"),Jn=/queueHooks$/,Gn=[nr],Qn={\"*\":[function(e,t){var n=this.createTween(e,t),r=n.cur(),i=Yn.exec(t),o=i&&i[3]||(x.cssNumber[e]?\"\":\"px\"),a=(x.cssNumber[e]||\"px\"!==o&&+r)&&Yn.exec(x.css(n.elem,e)),s=1,l=20;if(a&&a[3]!==o){o=o||a[3],i=i||[],a=+r||1;do s=s||\".5\",a/=s,x.style(n.elem,e,a+o);while(s!==(s=n.cur()/r)&&1!==s&&--l)}return i&&(a=n.start=+a||+r||0,n.unit=o,n.end=i[1]?a+(i[1]+1)*i[2]:+i[2]),n}]};function Kn(){return setTimeout(function(){Xn=t}),Xn=x.now()}function Zn(e,t,n){var r,i=(Qn[t]||[]).concat(Qn[\"*\"]),o=0,a=i.length;for(;a>o;o++)if(r=i[o].call(n,t,e))return r}function er(e,t,n){var r,i,o=0,a=Gn.length,s=x.Deferred().always(function(){delete l.elem}),l=function(){if(i)return!1;var t=Xn||Kn(),n=Math.max(0,u.startTime+u.duration-t),r=n/u.duration||0,o=1-r,a=0,l=u.tweens.length;for(;l>a;a++)u.tweens[a].run(o);return s.notifyWith(e,[u,o,n]),1>o&&l?n:(s.resolveWith(e,[u]),!1)},u=s.promise({elem:e,props:x.extend({},t),opts:x.extend(!0,{specialEasing:{}},n),originalProperties:t,originalOptions:n,startTime:Xn||Kn(),duration:n.duration,tweens:[],createTween:function(t,n){var r=x.Tween(e,u.opts,t,n,u.opts.specialEasing[t]||u.opts.easing);return u.tweens.push(r),r},stop:function(t){var n=0,r=t?u.tweens.length:0;if(i)return this;for(i=!0;r>n;n++)u.tweens[n].run(1);return t?s.resolveWith(e,[u,t]):s.rejectWith(e,[u,t]),this}}),c=u.props;for(tr(c,u.opts.specialEasing);a>o;o++)if(r=Gn[o].call(u,e,c,u.opts))return r;return x.map(c,Zn,u),x.isFunction(u.opts.start)&&u.opts.start.call(e,u),x.fx.timer(x.extend(l,{elem:e,anim:u,queue:u.opts.queue})),u.progress(u.opts.progress).done(u.opts.done,u.opts.complete).fail(u.opts.fail).always(u.opts.always)}function tr(e,t){var n,r,i,o,a;for(n in e)if(r=x.camelCase(n),i=t[r],o=e[n],x.isArray(o)&&(i=o[1],o=e[n]=o[0]),n!==r&&(e[r]=o,delete e[n]),a=x.cssHooks[r],a&&\"expand\"in a){o=a.expand(o),delete e[r];for(n in o)n in e||(e[n]=o[n],t[n]=i)}else t[r]=i}x.Animation=x.extend(er,{tweener:function(e,t){x.isFunction(e)?(t=e,e=[\"*\"]):e=e.split(\" \");var n,r=0,i=e.length;for(;i>r;r++)n=e[r],Qn[n]=Qn[n]||[],Qn[n].unshift(t)},prefilter:function(e,t){t?Gn.unshift(e):Gn.push(e)}});function nr(e,t,n){var r,i,o,a,s,l,u=this,c={},p=e.style,f=e.nodeType&&nn(e),d=x._data(e,\"fxshow\");n.queue||(s=x._queueHooks(e,\"fx\"),null==s.unqueued&&(s.unqueued=0,l=s.empty.fire,s.empty.fire=function(){s.unqueued||l()}),s.unqueued++,u.always(function(){u.always(function(){s.unqueued--,x.queue(e,\"fx\").length||s.empty.fire()})})),1===e.nodeType&&(\"height\"in t||\"width\"in t)&&(n.overflow=[p.overflow,p.overflowX,p.overflowY],\"inline\"===x.css(e,\"display\")&&\"none\"===x.css(e,\"float\")&&(x.support.inlineBlockNeedsLayout&&\"inline\"!==ln(e.nodeName)?p.zoom=1:p.display=\"inline-block\")),n.overflow&&(p.overflow=\"hidden\",x.support.shrinkWrapBlocks||u.always(function(){p.overflow=n.overflow[0],p.overflowX=n.overflow[1],p.overflowY=n.overflow[2]}));for(r in t)if(i=t[r],Vn.exec(i)){if(delete t[r],o=o||\"toggle\"===i,i===(f?\"hide\":\"show\"))continue;c[r]=d&&d[r]||x.style(e,r)}if(!x.isEmptyObject(c)){d?\"hidden\"in d&&(f=d.hidden):d=x._data(e,\"fxshow\",{}),o&&(d.hidden=!f),f?x(e).show():u.done(function(){x(e).hide()}),u.done(function(){var t;x._removeData(e,\"fxshow\");for(t in c)x.style(e,t,c[t])});for(r in c)a=Zn(f?d[r]:0,r,u),r in d||(d[r]=a.start,f&&(a.end=a.start,a.start=\"width\"===r||\"height\"===r?1:0))}}function rr(e,t,n,r,i){return new rr.prototype.init(e,t,n,r,i)}x.Tween=rr,rr.prototype={constructor:rr,init:function(e,t,n,r,i,o){this.elem=e,this.prop=n,this.easing=i||\"swing\",this.options=t,this.start=this.now=this.cur(),this.end=r,this.unit=o||(x.cssNumber[n]?\"\":\"px\")},cur:function(){var e=rr.propHooks[this.prop];return e&&e.get?e.get(this):rr.propHooks._default.get(this)},run:function(e){var t,n=rr.propHooks[this.prop];return this.pos=t=this.options.duration?x.easing[this.easing](e,this.options.duration*e,0,1,this.options.duration):e,this.now=(this.end-this.start)*t+this.start,this.options.step&&this.options.step.call(this.elem,this.now,this),n&&n.set?n.set(this):rr.propHooks._default.set(this),this}},rr.prototype.init.prototype=rr.prototype,rr.propHooks={_default:{get:function(e){var t;return null==e.elem[e.prop]||e.elem.style&&null!=e.elem.style[e.prop]?(t=x.css(e.elem,e.prop,\"\"),t&&\"auto\"!==t?t:0):e.elem[e.prop]},set:function(e){x.fx.step[e.prop]?x.fx.step[e.prop](e):e.elem.style&&(null!=e.elem.style[x.cssProps[e.prop]]||x.cssHooks[e.prop])?x.style(e.elem,e.prop,e.now+e.unit):e.elem[e.prop]=e.now}}},rr.propHooks.scrollTop=rr.propHooks.scrollLeft={set:function(e){e.elem.nodeType&&e.elem.parentNode&&(e.elem[e.prop]=e.now)}},x.each([\"toggle\",\"show\",\"hide\"],function(e,t){var n=x.fn[t];x.fn[t]=function(e,r,i){return null==e||\"boolean\"==typeof e?n.apply(this,arguments):this.animate(ir(t,!0),e,r,i)}}),x.fn.extend({fadeTo:function(e,t,n,r){return this.filter(nn).css(\"opacity\",0).show().end().animate({opacity:t},e,n,r)},animate:function(e,t,n,r){var i=x.isEmptyObject(e),o=x.speed(t,n,r),a=function(){var t=er(this,x.extend({},e),o);(i||x._data(this,\"finish\"))&&t.stop(!0)};return a.finish=a,i||o.queue===!1?this.each(a):this.queue(o.queue,a)},stop:function(e,n,r){var i=function(e){var t=e.stop;delete e.stop,t(r)};return\"string\"!=typeof e&&(r=n,n=e,e=t),n&&e!==!1&&this.queue(e||\"fx\",[]),this.each(function(){var t=!0,n=null!=e&&e+\"queueHooks\",o=x.timers,a=x._data(this);if(n)a[n]&&a[n].stop&&i(a[n]);else for(n in a)a[n]&&a[n].stop&&Jn.test(n)&&i(a[n]);for(n=o.length;n--;)o[n].elem!==this||null!=e&&o[n].queue!==e||(o[n].anim.stop(r),t=!1,o.splice(n,1));(t||!r)&&x.dequeue(this,e)})},finish:function(e){return e!==!1&&(e=e||\"fx\"),this.each(function(){var t,n=x._data(this),r=n[e+\"queue\"],i=n[e+\"queueHooks\"],o=x.timers,a=r?r.length:0;for(n.finish=!0,x.queue(this,e,[]),i&&i.stop&&i.stop.call(this,!0),t=o.length;t--;)o[t].elem===this&&o[t].queue===e&&(o[t].anim.stop(!0),o.splice(t,1));for(t=0;a>t;t++)r[t]&&r[t].finish&&r[t].finish.call(this);delete n.finish})}});function ir(e,t){var n,r={height:e},i=0;for(t=t?1:0;4>i;i+=2-t)n=Zt[i],r[\"margin\"+n]=r[\"padding\"+n]=e;return t&&(r.opacity=r.width=e),r}x.each({slideDown:ir(\"show\"),slideUp:ir(\"hide\"),slideToggle:ir(\"toggle\"),fadeIn:{opacity:\"show\"},fadeOut:{opacity:\"hide\"},fadeToggle:{opacity:\"toggle\"}},function(e,t){x.fn[e]=function(e,n,r){return this.animate(t,e,n,r)}}),x.speed=function(e,t,n){var r=e&&\"object\"==typeof e?x.extend({},e):{complete:n||!n&&t||x.isFunction(e)&&e,duration:e,easing:n&&t||t&&!x.isFunction(t)&&t};return r.duration=x.fx.off?0:\"number\"==typeof r.duration?r.duration:r.duration in x.fx.speeds?x.fx.speeds[r.duration]:x.fx.speeds._default,(null==r.queue||r.queue===!0)&&(r.queue=\"fx\"),r.old=r.complete,r.complete=function(){x.isFunction(r.old)&&r.old.call(this),r.queue&&x.dequeue(this,r.queue)},r},x.easing={linear:function(e){return e},swing:function(e){return.5-Math.cos(e*Math.PI)/2}},x.timers=[],x.fx=rr.prototype.init,x.fx.tick=function(){var e,n=x.timers,r=0;for(Xn=x.now();n.length>r;r++)e=n[r],e()||n[r]!==e||n.splice(r--,1);n.length||x.fx.stop(),Xn=t},x.fx.timer=function(e){e()&&x.timers.push(e)&&x.fx.start()},x.fx.interval=13,x.fx.start=function(){Un||(Un=setInterval(x.fx.tick,x.fx.interval))},x.fx.stop=function(){clearInterval(Un),Un=null},x.fx.speeds={slow:600,fast:200,_default:400},x.fx.step={},x.expr&&x.expr.filters&&(x.expr.filters.animated=function(e){return x.grep(x.timers,function(t){return e===t.elem}).length}),x.fn.offset=function(e){if(arguments.length)return e===t?this:this.each(function(t){x.offset.setOffset(this,e,t)});var n,r,o={top:0,left:0},a=this[0],s=a&&a.ownerDocument;if(s)return n=s.documentElement,x.contains(n,a)?(typeof a.getBoundingClientRect!==i&&(o=a.getBoundingClientRect()),r=or(s),{top:o.top+(r.pageYOffset||n.scrollTop)-(n.clientTop||0),left:o.left+(r.pageXOffset||n.scrollLeft)-(n.clientLeft||0)}):o},x.offset={setOffset:function(e,t,n){var r=x.css(e,\"position\");\"static\"===r&&(e.style.position=\"relative\");var i=x(e),o=i.offset(),a=x.css(e,\"top\"),s=x.css(e,\"left\"),l=(\"absolute\"===r||\"fixed\"===r)&&x.inArray(\"auto\",[a,s])>-1,u={},c={},p,f;l?(c=i.position(),p=c.top,f=c.left):(p=parseFloat(a)||0,f=parseFloat(s)||0),x.isFunction(t)&&(t=t.call(e,n,o)),null!=t.top&&(u.top=t.top-o.top+p),null!=t.left&&(u.left=t.left-o.left+f),\"using\"in t?t.using.call(e,u):i.css(u)}},x.fn.extend({position:function(){if(this[0]){var e,t,n={top:0,left:0},r=this[0];return\"fixed\"===x.css(r,\"position\")?t=r.getBoundingClientRect():(e=this.offsetParent(),t=this.offset(),x.nodeName(e[0],\"html\")||(n=e.offset()),n.top+=x.css(e[0],\"borderTopWidth\",!0),n.left+=x.css(e[0],\"borderLeftWidth\",!0)),{top:t.top-n.top-x.css(r,\"marginTop\",!0),left:t.left-n.left-x.css(r,\"marginLeft\",!0)}}},offsetParent:function(){return this.map(function(){var e=this.offsetParent||s;while(e&&!x.nodeName(e,\"html\")&&\"static\"===x.css(e,\"position\"))e=e.offsetParent;return e||s})}}),x.each({scrollLeft:\"pageXOffset\",scrollTop:\"pageYOffset\"},function(e,n){var r=/Y/.test(n);x.fn[e]=function(i){return x.access(this,function(e,i,o){var a=or(e);return o===t?a?n in a?a[n]:a.document.documentElement[i]:e[i]:(a?a.scrollTo(r?x(a).scrollLeft():o,r?o:x(a).scrollTop()):e[i]=o,t)},e,i,arguments.length,null)}});function or(e){return x.isWindow(e)?e:9===e.nodeType?e.defaultView||e.parentWindow:!1}x.each({Height:\"height\",Width:\"width\"},function(e,n){x.each({padding:\"inner\"+e,content:n,\"\":\"outer\"+e},function(r,i){x.fn[i]=function(i,o){var a=arguments.length&&(r||\"boolean\"!=typeof i),s=r||(i===!0||o===!0?\"margin\":\"border\");return x.access(this,function(n,r,i){var o;return x.isWindow(n)?n.document.documentElement[\"client\"+e]:9===n.nodeType?(o=n.documentElement,Math.max(n.body[\"scroll\"+e],o[\"scroll\"+e],n.body[\"offset\"+e],o[\"offset\"+e],o[\"client\"+e])):i===t?x.css(n,r,s):x.style(n,r,i,s)},n,a?i:t,a,null)}})}),x.fn.size=function(){return this.length},x.fn.andSelf=x.fn.addBack,\"object\"==typeof module&&module&&\"object\"==typeof module.exports?module.exports=x:(e.jQuery=e.$=x,\"function\"==typeof define&&define.amd&&define(\"jquery\",[],function(){return x}))})(window);");

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery\\jquery.min.map", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("{\"version\":3,\"file\":\"jquery.min.js\",\"sources\":[\"jquery-1.10.2.js\"],\"names\":[\"window\",\"undefined\",\"readyList\",\"rootjQuery\",\"core_strundefined\",\"location\",\"document\",\"docElem\",\"documentElement\",\"_jQuery\",\"jQuery\",\"_$\",\"$\",\"class2type\",\"core_deletedIds\",\"core_version\",\"core_concat\",\"concat\",\"core_push\",\"push\",\"core_slice\",\"slice\",\"core_indexOf\",\"indexOf\",\"core_toString\",\"toString\",\"core_hasOwn\",\"hasOwnProperty\",\"core_trim\",\"trim\",\"selector\",\"context\",\"fn\",\"init\",\"core_pnum\",\"source\",\"core_rnotwhite\",\"rtrim\",\"rquickExpr\",\"rsingleTag\",\"rvalidchars\",\"rvalidbraces\",\"rvalidescape\",\"rvalidtokens\",\"rmsPrefix\",\"rdashAlpha\",\"fcamelCase\",\"all\",\"letter\",\"toUpperCase\",\"completed\",\"event\",\"addEventListener\",\"type\",\"readyState\",\"detach\",\"ready\",\"removeEventListener\",\"detachEvent\",\"prototype\",\"jquery\",\"constructor\",\"match\",\"elem\",\"this\",\"charAt\",\"length\",\"exec\",\"find\",\"merge\",\"parseHTML\",\"nodeType\",\"ownerDocument\",\"test\",\"isPlainObject\",\"isFunction\",\"attr\",\"getElementById\",\"parentNode\",\"id\",\"makeArray\",\"toArray\",\"call\",\"get\",\"num\",\"pushStack\",\"elems\",\"ret\",\"prevObject\",\"each\",\"callback\",\"args\",\"promise\",\"done\",\"apply\",\"arguments\",\"first\",\"eq\",\"last\",\"i\",\"len\",\"j\",\"map\",\"end\",\"sort\",\"splice\",\"extend\",\"src\",\"copyIsArray\",\"copy\",\"name\",\"options\",\"clone\",\"target\",\"deep\",\"isArray\",\"expando\",\"Math\",\"random\",\"replace\",\"noConflict\",\"isReady\",\"readyWait\",\"holdReady\",\"hold\",\"wait\",\"body\",\"setTimeout\",\"resolveWith\",\"trigger\",\"off\",\"obj\",\"Array\",\"isWindow\",\"isNumeric\",\"isNaN\",\"parseFloat\",\"isFinite\",\"String\",\"key\",\"e\",\"support\",\"ownLast\",\"isEmptyObject\",\"error\",\"msg\",\"Error\",\"data\",\"keepScripts\",\"parsed\",\"scripts\",\"createElement\",\"buildFragment\",\"remove\",\"childNodes\",\"parseJSON\",\"JSON\",\"parse\",\"Function\",\"parseXML\",\"xml\",\"tmp\",\"DOMParser\",\"parseFromString\",\"ActiveXObject\",\"async\",\"loadXML\",\"getElementsByTagName\",\"noop\",\"globalEval\",\"execScript\",\"camelCase\",\"string\",\"nodeName\",\"toLowerCase\",\"value\",\"isArraylike\",\"text\",\"arr\",\"results\",\"Object\",\"inArray\",\"max\",\"second\",\"l\",\"grep\",\"inv\",\"retVal\",\"arg\",\"guid\",\"proxy\",\"access\",\"chainable\",\"emptyGet\",\"raw\",\"bulk\",\"now\",\"Date\",\"getTime\",\"swap\",\"old\",\"style\",\"Deferred\",\"attachEvent\",\"top\",\"frameElement\",\"doScroll\",\"doScrollCheck\",\"split\",\"cachedruns\",\"Expr\",\"getText\",\"isXML\",\"compile\",\"outermostContext\",\"sortInput\",\"setDocument\",\"documentIsHTML\",\"rbuggyQSA\",\"rbuggyMatches\",\"matches\",\"contains\",\"preferredDoc\",\"dirruns\",\"classCache\",\"createCache\",\"tokenCache\",\"compilerCache\",\"hasDuplicate\",\"sortOrder\",\"a\",\"b\",\"strundefined\",\"MAX_NEGATIVE\",\"hasOwn\",\"pop\",\"push_native\",\"booleans\",\"whitespace\",\"characterEncoding\",\"identifier\",\"attributes\",\"pseudos\",\"RegExp\",\"rcomma\",\"rcombinators\",\"rsibling\",\"rattributeQuotes\",\"rpseudo\",\"ridentifier\",\"matchExpr\",\"ID\",\"CLASS\",\"TAG\",\"ATTR\",\"PSEUDO\",\"CHILD\",\"bool\",\"needsContext\",\"rnative\",\"rinputs\",\"rheader\",\"rescape\",\"runescape\",\"funescape\",\"_\",\"escaped\",\"escapedWhitespace\",\"high\",\"fromCharCode\",\"els\",\"Sizzle\",\"seed\",\"m\",\"groups\",\"nid\",\"newContext\",\"newSelector\",\"getElementsByClassName\",\"qsa\",\"tokenize\",\"getAttribute\",\"setAttribute\",\"toSelector\",\"join\",\"querySelectorAll\",\"qsaError\",\"removeAttribute\",\"select\",\"keys\",\"cache\",\"cacheLength\",\"shift\",\"markFunction\",\"assert\",\"div\",\"removeChild\",\"addHandle\",\"attrs\",\"handler\",\"attrHandle\",\"siblingCheck\",\"cur\",\"diff\",\"sourceIndex\",\"nextSibling\",\"createInputPseudo\",\"createButtonPseudo\",\"createPositionalPseudo\",\"argument\",\"matchIndexes\",\"node\",\"doc\",\"parent\",\"defaultView\",\"className\",\"appendChild\",\"createComment\",\"innerHTML\",\"firstChild\",\"getById\",\"getElementsByName\",\"filter\",\"attrId\",\"getAttributeNode\",\"tag\",\"input\",\"matchesSelector\",\"webkitMatchesSelector\",\"mozMatchesSelector\",\"oMatchesSelector\",\"msMatchesSelector\",\"disconnectedMatch\",\"compareDocumentPosition\",\"adown\",\"bup\",\"compare\",\"sortDetached\",\"aup\",\"ap\",\"bp\",\"unshift\",\"expr\",\"elements\",\"val\",\"specified\",\"uniqueSort\",\"duplicates\",\"detectDuplicates\",\"sortStable\",\"textContent\",\"nodeValue\",\"selectors\",\"createPseudo\",\"relative\",\">\",\"dir\",\" \",\"+\",\"~\",\"preFilter\",\"excess\",\"unquoted\",\"nodeNameSelector\",\"pattern\",\"operator\",\"check\",\"result\",\"what\",\"simple\",\"forward\",\"ofType\",\"outerCache\",\"nodeIndex\",\"start\",\"useCache\",\"lastChild\",\"pseudo\",\"setFilters\",\"idx\",\"matched\",\"not\",\"matcher\",\"unmatched\",\"has\",\"innerText\",\"lang\",\"elemLang\",\"hash\",\"root\",\"focus\",\"activeElement\",\"hasFocus\",\"href\",\"tabIndex\",\"enabled\",\"disabled\",\"checked\",\"selected\",\"selectedIndex\",\"empty\",\"header\",\"button\",\"even\",\"odd\",\"lt\",\"gt\",\"radio\",\"checkbox\",\"file\",\"password\",\"image\",\"submit\",\"reset\",\"filters\",\"parseOnly\",\"tokens\",\"soFar\",\"preFilters\",\"cached\",\"addCombinator\",\"combinator\",\"base\",\"checkNonElements\",\"doneName\",\"dirkey\",\"elementMatcher\",\"matchers\",\"condense\",\"newUnmatched\",\"mapped\",\"setMatcher\",\"postFilter\",\"postFinder\",\"postSelector\",\"temp\",\"preMap\",\"postMap\",\"preexisting\",\"multipleContexts\",\"matcherIn\",\"matcherOut\",\"matcherFromTokens\",\"checkContext\",\"leadingRelative\",\"implicitRelative\",\"matchContext\",\"matchAnyContext\",\"matcherFromGroupMatchers\",\"elementMatchers\",\"setMatchers\",\"matcherCachedRuns\",\"bySet\",\"byElement\",\"superMatcher\",\"expandContext\",\"setMatched\",\"matchedCount\",\"outermost\",\"contextBackup\",\"dirrunsUnique\",\"group\",\"contexts\",\"token\",\"div1\",\"defaultValue\",\"unique\",\"isXMLDoc\",\"optionsCache\",\"createOptions\",\"object\",\"flag\",\"Callbacks\",\"firing\",\"memory\",\"fired\",\"firingLength\",\"firingIndex\",\"firingStart\",\"list\",\"stack\",\"once\",\"fire\",\"stopOnFalse\",\"self\",\"disable\",\"add\",\"index\",\"lock\",\"locked\",\"fireWith\",\"func\",\"tuples\",\"state\",\"always\",\"deferred\",\"fail\",\"then\",\"fns\",\"newDefer\",\"tuple\",\"action\",\"returned\",\"resolve\",\"reject\",\"progress\",\"notify\",\"pipe\",\"stateString\",\"when\",\"subordinate\",\"resolveValues\",\"remaining\",\"updateFunc\",\"values\",\"progressValues\",\"notifyWith\",\"progressContexts\",\"resolveContexts\",\"fragment\",\"opt\",\"eventName\",\"isSupported\",\"cssText\",\"getSetAttribute\",\"leadingWhitespace\",\"tbody\",\"htmlSerialize\",\"hrefNormalized\",\"opacity\",\"cssFloat\",\"checkOn\",\"optSelected\",\"enctype\",\"html5Clone\",\"cloneNode\",\"outerHTML\",\"inlineBlockNeedsLayout\",\"shrinkWrapBlocks\",\"pixelPosition\",\"deleteExpando\",\"noCloneEvent\",\"reliableMarginRight\",\"boxSizingReliable\",\"noCloneChecked\",\"optDisabled\",\"radioValue\",\"createDocumentFragment\",\"appendChecked\",\"checkClone\",\"click\",\"change\",\"focusin\",\"backgroundClip\",\"clearCloneStyle\",\"container\",\"marginDiv\",\"tds\",\"divReset\",\"offsetHeight\",\"display\",\"reliableHiddenOffsets\",\"zoom\",\"boxSizing\",\"offsetWidth\",\"getComputedStyle\",\"width\",\"marginRight\",\"rbrace\",\"rmultiDash\",\"internalData\",\"pvt\",\"acceptData\",\"thisCache\",\"internalKey\",\"isNode\",\"toJSON\",\"internalRemoveData\",\"isEmptyDataObject\",\"cleanData\",\"noData\",\"applet\",\"embed\",\"hasData\",\"removeData\",\"_data\",\"_removeData\",\"dataAttr\",\"queue\",\"dequeue\",\"startLength\",\"hooks\",\"_queueHooks\",\"next\",\"stop\",\"setter\",\"delay\",\"time\",\"fx\",\"speeds\",\"timeout\",\"clearTimeout\",\"clearQueue\",\"count\",\"defer\",\"nodeHook\",\"boolHook\",\"rclass\",\"rreturn\",\"rfocusable\",\"rclickable\",\"ruseDefault\",\"getSetInput\",\"removeAttr\",\"prop\",\"removeProp\",\"propFix\",\"addClass\",\"classes\",\"clazz\",\"proceed\",\"removeClass\",\"toggleClass\",\"stateVal\",\"classNames\",\"hasClass\",\"valHooks\",\"set\",\"option\",\"one\",\"optionSet\",\"nType\",\"attrHooks\",\"propName\",\"attrNames\",\"for\",\"class\",\"notxml\",\"propHooks\",\"tabindex\",\"parseInt\",\"getter\",\"setAttributeNode\",\"createAttribute\",\"coords\",\"contenteditable\",\"rformElems\",\"rkeyEvent\",\"rmouseEvent\",\"rfocusMorph\",\"rtypenamespace\",\"returnTrue\",\"returnFalse\",\"safeActiveElement\",\"err\",\"global\",\"types\",\"events\",\"t\",\"handleObjIn\",\"special\",\"eventHandle\",\"handleObj\",\"handlers\",\"namespaces\",\"origType\",\"elemData\",\"handle\",\"triggered\",\"dispatch\",\"delegateType\",\"bindType\",\"namespace\",\"delegateCount\",\"setup\",\"mappedTypes\",\"origCount\",\"teardown\",\"removeEvent\",\"onlyHandlers\",\"ontype\",\"bubbleType\",\"eventPath\",\"Event\",\"isTrigger\",\"namespace_re\",\"noBubble\",\"parentWindow\",\"isPropagationStopped\",\"preventDefault\",\"isDefaultPrevented\",\"_default\",\"fix\",\"handlerQueue\",\"delegateTarget\",\"preDispatch\",\"currentTarget\",\"isImmediatePropagationStopped\",\"stopPropagation\",\"postDispatch\",\"sel\",\"originalEvent\",\"fixHook\",\"fixHooks\",\"mouseHooks\",\"keyHooks\",\"props\",\"srcElement\",\"metaKey\",\"original\",\"which\",\"charCode\",\"keyCode\",\"eventDoc\",\"fromElement\",\"pageX\",\"clientX\",\"scrollLeft\",\"clientLeft\",\"pageY\",\"clientY\",\"scrollTop\",\"clientTop\",\"relatedTarget\",\"toElement\",\"load\",\"blur\",\"beforeunload\",\"returnValue\",\"simulate\",\"bubble\",\"isSimulated\",\"defaultPrevented\",\"getPreventDefault\",\"timeStamp\",\"cancelBubble\",\"stopImmediatePropagation\",\"mouseenter\",\"mouseleave\",\"orig\",\"related\",\"submitBubbles\",\"form\",\"_submit_bubble\",\"changeBubbles\",\"propertyName\",\"_just_changed\",\"focusinBubbles\",\"attaches\",\"on\",\"origFn\",\"triggerHandler\",\"isSimple\",\"rparentsprev\",\"rneedsContext\",\"guaranteedUnique\",\"children\",\"contents\",\"prev\",\"targets\",\"winnow\",\"is\",\"closest\",\"pos\",\"prevAll\",\"addBack\",\"sibling\",\"parents\",\"parentsUntil\",\"until\",\"nextAll\",\"nextUntil\",\"prevUntil\",\"siblings\",\"contentDocument\",\"contentWindow\",\"reverse\",\"n\",\"r\",\"qualifier\",\"createSafeFragment\",\"nodeNames\",\"safeFrag\",\"rinlinejQuery\",\"rnoshimcache\",\"rleadingWhitespace\",\"rxhtmlTag\",\"rtagName\",\"rtbody\",\"rhtml\",\"rnoInnerhtml\",\"manipulation_rcheckableType\",\"rchecked\",\"rscriptType\",\"rscriptTypeMasked\",\"rcleanScript\",\"wrapMap\",\"legend\",\"area\",\"param\",\"thead\",\"tr\",\"col\",\"td\",\"safeFragment\",\"fragmentDiv\",\"optgroup\",\"tfoot\",\"colgroup\",\"caption\",\"th\",\"append\",\"createTextNode\",\"domManip\",\"manipulationTarget\",\"prepend\",\"insertBefore\",\"before\",\"after\",\"keepData\",\"getAll\",\"setGlobalEval\",\"dataAndEvents\",\"deepDataAndEvents\",\"html\",\"replaceWith\",\"allowIntersection\",\"hasScripts\",\"iNoClone\",\"disableScript\",\"restoreScript\",\"_evalUrl\",\"content\",\"refElements\",\"cloneCopyEvent\",\"dest\",\"oldData\",\"curData\",\"fixCloneNodeIssues\",\"defaultChecked\",\"defaultSelected\",\"appendTo\",\"prependTo\",\"insertAfter\",\"replaceAll\",\"insert\",\"found\",\"fixDefaultChecked\",\"destElements\",\"srcElements\",\"inPage\",\"selection\",\"wrap\",\"safe\",\"nodes\",\"url\",\"ajax\",\"dataType\",\"throws\",\"wrapAll\",\"wrapInner\",\"unwrap\",\"iframe\",\"getStyles\",\"curCSS\",\"ralpha\",\"ropacity\",\"rposition\",\"rdisplayswap\",\"rmargin\",\"rnumsplit\",\"rnumnonpx\",\"rrelNum\",\"elemdisplay\",\"BODY\",\"cssShow\",\"position\",\"visibility\",\"cssNormalTransform\",\"letterSpacing\",\"fontWeight\",\"cssExpand\",\"cssPrefixes\",\"vendorPropName\",\"capName\",\"origName\",\"isHidden\",\"el\",\"css\",\"showHide\",\"show\",\"hidden\",\"css_defaultDisplay\",\"styles\",\"hide\",\"toggle\",\"cssHooks\",\"computed\",\"cssNumber\",\"columnCount\",\"fillOpacity\",\"lineHeight\",\"order\",\"orphans\",\"widows\",\"zIndex\",\"cssProps\",\"float\",\"extra\",\"_computed\",\"minWidth\",\"maxWidth\",\"getPropertyValue\",\"currentStyle\",\"left\",\"rs\",\"rsLeft\",\"runtimeStyle\",\"pixelLeft\",\"setPositiveNumber\",\"subtract\",\"augmentWidthOrHeight\",\"isBorderBox\",\"getWidthOrHeight\",\"valueIsBorderBox\",\"actualDisplay\",\"write\",\"close\",\"$1\",\"visible\",\"margin\",\"padding\",\"border\",\"prefix\",\"suffix\",\"expand\",\"expanded\",\"parts\",\"r20\",\"rbracket\",\"rCRLF\",\"rsubmitterTypes\",\"rsubmittable\",\"serialize\",\"serializeArray\",\"traditional\",\"s\",\"encodeURIComponent\",\"ajaxSettings\",\"buildParams\",\"v\",\"hover\",\"fnOver\",\"fnOut\",\"bind\",\"unbind\",\"delegate\",\"undelegate\",\"ajaxLocParts\",\"ajaxLocation\",\"ajax_nonce\",\"ajax_rquery\",\"rhash\",\"rts\",\"rheaders\",\"rlocalProtocol\",\"rnoContent\",\"rprotocol\",\"rurl\",\"_load\",\"prefilters\",\"transports\",\"allTypes\",\"addToPrefiltersOrTransports\",\"structure\",\"dataTypeExpression\",\"dataTypes\",\"inspectPrefiltersOrTransports\",\"originalOptions\",\"jqXHR\",\"inspected\",\"seekingTransport\",\"inspect\",\"prefilterOrFactory\",\"dataTypeOrTransport\",\"ajaxExtend\",\"flatOptions\",\"params\",\"response\",\"responseText\",\"complete\",\"status\",\"active\",\"lastModified\",\"etag\",\"isLocal\",\"processData\",\"contentType\",\"accepts\",\"*\",\"json\",\"responseFields\",\"converters\",\"* text\",\"text html\",\"text json\",\"text xml\",\"ajaxSetup\",\"settings\",\"ajaxPrefilter\",\"ajaxTransport\",\"cacheURL\",\"responseHeadersString\",\"timeoutTimer\",\"fireGlobals\",\"transport\",\"responseHeaders\",\"callbackContext\",\"globalEventContext\",\"completeDeferred\",\"statusCode\",\"requestHeaders\",\"requestHeadersNames\",\"strAbort\",\"getResponseHeader\",\"getAllResponseHeaders\",\"setRequestHeader\",\"lname\",\"overrideMimeType\",\"mimeType\",\"code\",\"abort\",\"statusText\",\"finalText\",\"success\",\"method\",\"crossDomain\",\"hasContent\",\"ifModified\",\"headers\",\"beforeSend\",\"send\",\"nativeStatusText\",\"responses\",\"isSuccess\",\"modified\",\"ajaxHandleResponses\",\"ajaxConvert\",\"rejectWith\",\"getJSON\",\"getScript\",\"firstDataType\",\"ct\",\"finalDataType\",\"conv2\",\"current\",\"conv\",\"dataFilter\",\"script\",\"text script\",\"head\",\"scriptCharset\",\"charset\",\"onload\",\"onreadystatechange\",\"isAbort\",\"oldCallbacks\",\"rjsonp\",\"jsonp\",\"jsonpCallback\",\"originalSettings\",\"callbackName\",\"overwritten\",\"responseContainer\",\"jsonProp\",\"xhrCallbacks\",\"xhrSupported\",\"xhrId\",\"xhrOnUnloadAbort\",\"createStandardXHR\",\"XMLHttpRequest\",\"createActiveXHR\",\"xhr\",\"cors\",\"username\",\"open\",\"xhrFields\",\"firefoxAccessException\",\"unload\",\"fxNow\",\"timerId\",\"rfxtypes\",\"rfxnum\",\"rrun\",\"animationPrefilters\",\"defaultPrefilter\",\"tweeners\",\"tween\",\"createTween\",\"unit\",\"scale\",\"maxIterations\",\"createFxNow\",\"animation\",\"collection\",\"Animation\",\"properties\",\"stopped\",\"tick\",\"currentTime\",\"startTime\",\"duration\",\"percent\",\"tweens\",\"run\",\"opts\",\"specialEasing\",\"originalProperties\",\"Tween\",\"easing\",\"gotoEnd\",\"propFilter\",\"timer\",\"anim\",\"tweener\",\"prefilter\",\"oldfire\",\"dataShow\",\"unqueued\",\"overflow\",\"overflowX\",\"overflowY\",\"eased\",\"step\",\"cssFn\",\"speed\",\"animate\",\"genFx\",\"fadeTo\",\"to\",\"optall\",\"doAnimation\",\"finish\",\"stopQueue\",\"timers\",\"includeWidth\",\"height\",\"slideDown\",\"slideUp\",\"slideToggle\",\"fadeIn\",\"fadeOut\",\"fadeToggle\",\"linear\",\"p\",\"swing\",\"cos\",\"PI\",\"interval\",\"setInterval\",\"clearInterval\",\"slow\",\"fast\",\"animated\",\"offset\",\"setOffset\",\"win\",\"box\",\"getBoundingClientRect\",\"getWindow\",\"pageYOffset\",\"pageXOffset\",\"curElem\",\"curOffset\",\"curCSSTop\",\"curCSSLeft\",\"calculatePosition\",\"curPosition\",\"curTop\",\"curLeft\",\"using\",\"offsetParent\",\"parentOffset\",\"scrollTo\",\"Height\",\"Width\",\"defaultExtra\",\"funcName\",\"size\",\"andSelf\",\"module\",\"exports\",\"define\",\"amd\"],\"mappings\":\"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\"}");
                    yaz.Close();
                }
            }

            if (!chkAngular.Checked)
            {
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery\\json2.js", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("/*");
                        yaz.WriteLine("\tjson2.js");
                        yaz.WriteLine("\t2013-05-26");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tPublic Domain.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tNO WARRANTY EXPRESSED OR IMPLIED. USE AT YOUR OWN RISK.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tSee http://www.JSON.org/js.html");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tThis code should be minified before deployment.");
                        yaz.WriteLine("\tSee http://javascript.crockford.com/jsmin.html");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tUSE YOUR OWN COPY. IT IS EXTREMELY UNWISE TO LOAD CODE FROM SERVERS YOU DO");
                        yaz.WriteLine("\tNOT CONTROL.");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tThis file creates a global JSON object containing two methods: stringify");
                        yaz.WriteLine("\tand parse.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tJSON.stringify(value, replacer, space)");
                        yaz.WriteLine("\t\t\tvalue\t   any JavaScript value, usually an object or array.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treplacer\tan optional parameter that determines how object");
                        yaz.WriteLine("\t\t\t\t\t\tvalues are stringified for objects. It can be a");
                        yaz.WriteLine("\t\t\t\t\t\tfunction or an array of strings.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tspace\t   an optional parameter that specifies the indentation");
                        yaz.WriteLine("\t\t\t\t\t\tof nested structures. If it is omitted, the text will");
                        yaz.WriteLine("\t\t\t\t\t\tbe packed without extra whitespace. If it is a number,");
                        yaz.WriteLine("\t\t\t\t\t\tit will specify the number of spaces to indent at each");
                        yaz.WriteLine("\t\t\t\t\t\tlevel. If it is a string (such as \'\\t\' or \'&nbsp;\'),");
                        yaz.WriteLine("\t\t\t\t\t\tit contains the characters used to indent at each level.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tThis method produces a JSON text from a JavaScript value.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tWhen an object value is found, if the object contains a toJSON");
                        yaz.WriteLine("\t\t\tmethod, its toJSON method will be called and the result will be");
                        yaz.WriteLine("\t\t\tstringified. A toJSON method does not serialize: it returns the");
                        yaz.WriteLine("\t\t\tvalue represented by the name/value pair that should be serialized,");
                        yaz.WriteLine("\t\t\tor undefined if nothing should be serialized. The toJSON method");
                        yaz.WriteLine("\t\t\twill be passed the key associated with the value, and this will be");
                        yaz.WriteLine("\t\t\tbound to the value");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tFor example, this would serialize Dates as ISO strings.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tDate.prototype.toJSON = function (key) {");
                        yaz.WriteLine("\t\t\t\t\tfunction f(n) {");
                        yaz.WriteLine("\t\t\t\t\t\t// Format integers to have at least two digits.");
                        yaz.WriteLine("\t\t\t\t\t\treturn n < 10 ? \'0\' + n : n;");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\treturn this.getUTCFullYear()   + \'-\' +");
                        yaz.WriteLine("\t\t\t\t\t\t f(this.getUTCMonth() + 1) + \'-\' +");
                        yaz.WriteLine("\t\t\t\t\t\t f(this.getUTCDate())\t  + \'T\' +");
                        yaz.WriteLine("\t\t\t\t\t\t f(this.getUTCHours())\t + \':\' +");
                        yaz.WriteLine("\t\t\t\t\t\t f(this.getUTCMinutes())   + \':\' +");
                        yaz.WriteLine("\t\t\t\t\t\t f(this.getUTCSeconds())   + \'Z\';");
                        yaz.WriteLine("\t\t\t\t};");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tYou can provide an optional replacer method. It will be passed the");
                        yaz.WriteLine("\t\t\tkey and value of each member, with this bound to the containing");
                        yaz.WriteLine("\t\t\tobject. The value that is returned from your method will be");
                        yaz.WriteLine("\t\t\tserialized. If your method returns undefined, then the member will");
                        yaz.WriteLine("\t\t\tbe excluded from the serialization.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tIf the replacer parameter is an array of strings, then it will be");
                        yaz.WriteLine("\t\t\tused to select the members to be serialized. It filters the results");
                        yaz.WriteLine("\t\t\tsuch that only members with keys listed in the replacer array are");
                        yaz.WriteLine("\t\t\tstringified.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tValues that do not have JSON representations, such as undefined or");
                        yaz.WriteLine("\t\t\tfunctions, will not be serialized. Such values in objects will be");
                        yaz.WriteLine("\t\t\tdropped; in arrays they will be replaced with null. You can use");
                        yaz.WriteLine("\t\t\ta replacer function to replace those with JSON values.");
                        yaz.WriteLine("\t\t\tJSON.stringify(undefined) returns undefined.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tThe optional space parameter produces a stringification of the");
                        yaz.WriteLine("\t\t\tvalue that is filled with line breaks and indentation to make it");
                        yaz.WriteLine("\t\t\teasier to read.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tIf the space parameter is a non-empty string, then that string will");
                        yaz.WriteLine("\t\t\tbe used for indentation. If the space parameter is a number, then");
                        yaz.WriteLine("\t\t\tthe indentation will be that many spaces.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tExample:");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\ttext = JSON.stringify([\'e\', {pluribus: \'unum\'}]);");
                        yaz.WriteLine("\t\t\t// text is \'[\"e\",{\"pluribus\":\"unum\"}]\'");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\ttext = JSON.stringify([\'e\', {pluribus: \'unum\'}], null, \'\\t\');");
                        yaz.WriteLine("\t\t\t// text is \'[\\n\\t\"e\",\\n\\t{\\n\\t\\t\"pluribus\": \"unum\"\\n\\t}\\n]\'");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\ttext = JSON.stringify([new Date()], function (key, value) {");
                        yaz.WriteLine("\t\t\t\treturn this[key] instanceof Date ?");
                        yaz.WriteLine("\t\t\t\t\t\'Date(\' + this[key] + \')\' : value;");
                        yaz.WriteLine("\t\t\t});");
                        yaz.WriteLine("\t\t\t// text is \'[\"Date(---current time---)\"]\'");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tJSON.parse(text, reviver)");
                        yaz.WriteLine("\t\t\tThis method parses a JSON text to produce an object or array.");
                        yaz.WriteLine("\t\t\tIt can throw a SyntaxError exception.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tThe optional reviver parameter is a function that can filter and");
                        yaz.WriteLine("\t\t\ttransform the results. It receives each of the keys and values,");
                        yaz.WriteLine("\t\t\tand its return value is used instead of the original value.");
                        yaz.WriteLine("\t\t\tIf it returns what it received, then the structure is not modified.");
                        yaz.WriteLine("\t\t\tIf it returns undefined then the member is deleted.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tExample:");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t// Parse the text. Values that look like ISO date strings will");
                        yaz.WriteLine("\t\t\t// be converted to Date objects.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tmyData = JSON.parse(text, function (key, value) {");
                        yaz.WriteLine("\t\t\t\tvar a;");
                        yaz.WriteLine("\t\t\t\tif (typeof value === \'string\') {");
                        yaz.WriteLine("\t\t\t\t\ta =");
                        yaz.WriteLine("/^(\\d{4})-(\\d{2})-(\\d{2})T(\\d{2}):(\\d{2}):(\\d{2}(?:\\.\\d*)?)Z$/.exec(value);");
                        yaz.WriteLine("\t\t\t\t\tif (a) {");
                        yaz.WriteLine("\t\t\t\t\t\treturn new Date(Date.UTC(+a[1], +a[2] - 1, +a[3], +a[4],");
                        yaz.WriteLine("\t\t\t\t\t\t\t+a[5], +a[6]));");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\treturn value;");
                        yaz.WriteLine("\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tmyData = JSON.parse(\'[\"Date(09/09/2001)\"]\', function (key, value) {");
                        yaz.WriteLine("\t\t\t\tvar d;");
                        yaz.WriteLine("\t\t\t\tif (typeof value === \'string\' &&");
                        yaz.WriteLine("\t\t\t\t\t\tvalue.slice(0, 5) === \'Date(\' &&");
                        yaz.WriteLine("\t\t\t\t\t\tvalue.slice(-1) === \')\') {");
                        yaz.WriteLine("\t\t\t\t\td = new Date(value.slice(5, -1));");
                        yaz.WriteLine("\t\t\t\t\tif (d) {");
                        yaz.WriteLine("\t\t\t\t\t\treturn d;");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\treturn value;");
                        yaz.WriteLine("\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tThis is a reference implementation. You are free to copy, modify, or");
                        yaz.WriteLine("\tredistribute.");
                        yaz.WriteLine("*/");
                        yaz.WriteLine("");
                        yaz.WriteLine("/*jslint evil: true, regexp: true */");
                        yaz.WriteLine("");
                        yaz.WriteLine("/*members \"\", \"\\b\", \"\\t\", \"\\n\", \"\\f\", \"\\r\", \"\\\"\", JSON, \"\\\\\", apply,");
                        yaz.WriteLine("\tcall, charCodeAt, getUTCDate, getUTCFullYear, getUTCHours,");
                        yaz.WriteLine("\tgetUTCMinutes, getUTCMonth, getUTCSeconds, hasOwnProperty, join,");
                        yaz.WriteLine("\tlastIndex, length, parse, prototype, push, replace, slice, stringify,");
                        yaz.WriteLine("\ttest, toJSON, toString, valueOf");
                        yaz.WriteLine("*/");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("// Create a JSON object only if one does not already exist. We create the");
                        yaz.WriteLine("// methods in a closure to avoid creating global variables.");
                        yaz.WriteLine("");
                        yaz.WriteLine("if (typeof JSON !== \'object\') {");
                        yaz.WriteLine("\tJSON = {};");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("(function () {");
                        yaz.WriteLine("\t\'use strict\';");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tfunction f(n) {");
                        yaz.WriteLine("\t\t// Format integers to have at least two digits.");
                        yaz.WriteLine("\t\treturn n < 10 ? \'0\' + n : n;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tif (typeof Date.prototype.toJSON !== \'function\') {");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tDate.prototype.toJSON = function () {");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn isFinite(this.valueOf())");
                        yaz.WriteLine("\t\t\t\t? this.getUTCFullYear() + \'-\' +");
                        yaz.WriteLine("\t\t\t\t\tf(this.getUTCMonth() + 1) + \'-\' +");
                        yaz.WriteLine("\t\t\t\t\tf(this.getUTCDate()) + \'T\' +");
                        yaz.WriteLine("\t\t\t\t\tf(this.getUTCHours()) + \':\' +");
                        yaz.WriteLine("\t\t\t\t\tf(this.getUTCMinutes()) + \':\' +");
                        yaz.WriteLine("\t\t\t\t\tf(this.getUTCSeconds()) + \'Z\'");
                        yaz.WriteLine("\t\t\t\t: null;");
                        yaz.WriteLine("\t\t};");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tString.prototype.toJSON =");
                        yaz.WriteLine("\t\t\tNumber.prototype.toJSON =");
                        yaz.WriteLine("\t\t\tBoolean.prototype.toJSON = function () {");
                        yaz.WriteLine("\t\t\t\treturn this.valueOf();");
                        yaz.WriteLine("\t\t\t};");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tvar cx = /[\\u0000\\u00ad\\u0600-\\u0604\\u070f\\u17b4\\u17b5\\u200c-\\u200f\\u2028-\\u202f\\u2060-\\u206f\\ufeff\\ufff0-\\uffff]/g,");
                        yaz.WriteLine("\t\tescapable = /[\\\\\\\"\\x00-\\x1f\\x7f-\\x9f\\u00ad\\u0600-\\u0604\\u070f\\u17b4\\u17b5\\u200c-\\u200f\\u2028-\\u202f\\u2060-\\u206f\\ufeff\\ufff0-\\uffff]/g,");
                        yaz.WriteLine("\t\tgap,");
                        yaz.WriteLine("\t\tindent,");
                        yaz.WriteLine("\t\tmeta = {\t// table of character substitutions");
                        yaz.WriteLine("\t\t\t\'\\b\': \'\\\\b\',");
                        yaz.WriteLine("\t\t\t\'\\t\': \'\\\\t\',");
                        yaz.WriteLine("\t\t\t\'\\n\': \'\\\\n\',");
                        yaz.WriteLine("\t\t\t\'\\f\': \'\\\\f\',");
                        yaz.WriteLine("\t\t\t\'\\r\': \'\\\\r\',");
                        yaz.WriteLine("\t\t\t\'\"\': \'\\\\\"\',");
                        yaz.WriteLine("\t\t\t\'\\\\\': \'\\\\\\\\\'");
                        yaz.WriteLine("\t\t},");
                        yaz.WriteLine("\t\trep;");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tfunction quote(string) {");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t// If the string contains no control characters, no quote characters, and no");
                        yaz.WriteLine("\t\t// backslash characters, then we can safely slap some quotes around it.");
                        yaz.WriteLine("\t\t// Otherwise we must also replace the offending characters with safe escape");
                        yaz.WriteLine("\t\t// sequences.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tescapable.lastIndex = 0;");
                        yaz.WriteLine("\t\treturn escapable.test(string) ? \'\"\' + string.replace(escapable, function (a) {");
                        yaz.WriteLine("\t\t\tvar c = meta[a];");
                        yaz.WriteLine("\t\t\treturn typeof c === \'string\'");
                        yaz.WriteLine("\t\t\t\t? c");
                        yaz.WriteLine("\t\t\t\t: \'\\\\u\' + (\'0000\' + a.charCodeAt(0).toString(16)).slice(-4);");
                        yaz.WriteLine("\t\t}) + \'\"\' : \'\"\' + string + \'\"\';");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tfunction str(key, holder) {");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t// Produce a string from holder[key].");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tvar i,\t\t  // The loop counter.");
                        yaz.WriteLine("\t\t\tk,\t\t  // The member key.");
                        yaz.WriteLine("\t\t\tv,\t\t  // The member value.");
                        yaz.WriteLine("\t\t\tlength,");
                        yaz.WriteLine("\t\t\tmind = gap,");
                        yaz.WriteLine("\t\t\tpartial,");
                        yaz.WriteLine("\t\t\tvalue = holder[key];");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t// If the value has a toJSON method, call it to obtain a replacement value.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (value && typeof value === \'object\' &&");
                        yaz.WriteLine("\t\t\t\ttypeof value.toJSON === \'function\') {");
                        yaz.WriteLine("\t\t\tvalue = value.toJSON(key);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t// If we were called with a replacer function, then call the replacer to");
                        yaz.WriteLine("\t\t// obtain a replacement value.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (typeof rep === \'function\') {");
                        yaz.WriteLine("\t\t\tvalue = rep.call(holder, key, value);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t// What happens next depends on the value\'s type.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tswitch (typeof value) {");
                        yaz.WriteLine("\t\t\tcase \'string\':");
                        yaz.WriteLine("\t\t\t\treturn quote(value);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tcase \'number\':");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// JSON numbers must be finite. Encode non-finite numbers as null.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\treturn isFinite(value) ? String(value) : \'null\';");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tcase \'boolean\':");
                        yaz.WriteLine("\t\t\tcase \'null\':");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// If the value is a boolean or null, convert it to a string. Note:");
                        yaz.WriteLine("\t\t\t\t// typeof null does not produce \'null\'. The case is included here in");
                        yaz.WriteLine("\t\t\t\t// the remote chance that this gets fixed someday.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\treturn String(value);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// If the type is \'object\', we might be dealing with an object or an array or");
                        yaz.WriteLine("\t\t\t\t// null.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tcase \'object\':");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// Due to a specification blunder in ECMAScript, typeof null is \'object\',");
                        yaz.WriteLine("\t\t\t\t// so watch out for that case.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tif (!value) {");
                        yaz.WriteLine("\t\t\t\t\treturn \'null\';");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// Make an array to hold the partial results of stringifying this object value.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tgap += indent;");
                        yaz.WriteLine("\t\t\t\tpartial = [];");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// Is the value an array?");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tif (Object.prototype.toString.apply(value) === \'[object Array]\') {");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t// The value is an array. Stringify every element. Use null as a placeholder");
                        yaz.WriteLine("\t\t\t\t\t// for non-JSON values.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tlength = value.length;");
                        yaz.WriteLine("\t\t\t\t\tfor (i = 0; i < length; i += 1) {");
                        yaz.WriteLine("\t\t\t\t\t\tpartial[i] = str(i, value) || \'null\';");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t// Join all of the elements together, separated with commas, and wrap them in");
                        yaz.WriteLine("\t\t\t\t\t// brackets.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tv = partial.length === 0");
                        yaz.WriteLine("\t\t\t\t\t\t? \'[]\'");
                        yaz.WriteLine("\t\t\t\t\t\t: gap");
                        yaz.WriteLine("\t\t\t\t\t\t? \'[\\n\' + gap + partial.join(\',\\n\' + gap) + \'\\n\' + mind + \']\'");
                        yaz.WriteLine("\t\t\t\t\t\t: \'[\' + partial.join(\',\') + \']\';");
                        yaz.WriteLine("\t\t\t\t\tgap = mind;");
                        yaz.WriteLine("\t\t\t\t\treturn v;");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// If the replacer is an array, use it to select the members to be stringified.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tif (rep && typeof rep === \'object\') {");
                        yaz.WriteLine("\t\t\t\t\tlength = rep.length;");
                        yaz.WriteLine("\t\t\t\t\tfor (i = 0; i < length; i += 1) {");
                        yaz.WriteLine("\t\t\t\t\t\tif (typeof rep[i] === \'string\') {");
                        yaz.WriteLine("\t\t\t\t\t\t\tk = rep[i];");
                        yaz.WriteLine("\t\t\t\t\t\t\tv = str(k, value);");
                        yaz.WriteLine("\t\t\t\t\t\t\tif (v) {");
                        yaz.WriteLine("\t\t\t\t\t\t\t\tpartial.push(quote(k) + (gap ? \': \' : \':\') + v);");
                        yaz.WriteLine("\t\t\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t} else {");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t// Otherwise, iterate through all of the keys in the object.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tfor (k in value) {");
                        yaz.WriteLine("\t\t\t\t\t\tif (Object.prototype.hasOwnProperty.call(value, k)) {");
                        yaz.WriteLine("\t\t\t\t\t\t\tv = str(k, value);");
                        yaz.WriteLine("\t\t\t\t\t\t\tif (v) {");
                        yaz.WriteLine("\t\t\t\t\t\t\t\tpartial.push(quote(k) + (gap ? \': \' : \':\') + v);");
                        yaz.WriteLine("\t\t\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// Join all of the member texts together, separated with commas,");
                        yaz.WriteLine("\t\t\t\t// and wrap them in braces.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tv = partial.length === 0");
                        yaz.WriteLine("\t\t\t\t\t? \'{}\'");
                        yaz.WriteLine("\t\t\t\t\t: gap");
                        yaz.WriteLine("\t\t\t\t\t? \'{\\n\' + gap + partial.join(\',\\n\' + gap) + \'\\n\' + mind + \'}\'");
                        yaz.WriteLine("\t\t\t\t\t: \'{\' + partial.join(\',\') + \'}\';");
                        yaz.WriteLine("\t\t\t\tgap = mind;");
                        yaz.WriteLine("\t\t\t\treturn v;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t// If the JSON object does not yet have a stringify method, give it one.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tif (typeof JSON.stringify !== \'function\') {");
                        yaz.WriteLine("\t\tJSON.stringify = function (value, replacer, space) {");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t// The stringify method takes a value and an optional replacer, and an optional");
                        yaz.WriteLine("\t\t\t// space parameter, and returns a JSON text. The replacer can be a function");
                        yaz.WriteLine("\t\t\t// that can replace values, or an array of strings that will select the keys.");
                        yaz.WriteLine("\t\t\t// A default replacer method can be provided. Use of the space parameter can");
                        yaz.WriteLine("\t\t\t// produce text that is more easily readable.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tvar i;");
                        yaz.WriteLine("\t\t\tgap = \'\';");
                        yaz.WriteLine("\t\t\tindent = \'\';");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t// If the space parameter is a number, make an indent string containing that");
                        yaz.WriteLine("\t\t\t// many spaces.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (typeof space === \'number\') {");
                        yaz.WriteLine("\t\t\t\tfor (i = 0; i < space; i += 1) {");
                        yaz.WriteLine("\t\t\t\t\tindent += \' \';");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// If the space parameter is a string, it will be used as the indent string.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t} else if (typeof space === \'string\') {");
                        yaz.WriteLine("\t\t\t\tindent = space;");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t// If there is a replacer, it must be a function or an array.");
                        yaz.WriteLine("\t\t\t// Otherwise, throw an error.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\trep = replacer;");
                        yaz.WriteLine("\t\t\tif (replacer && typeof replacer !== \'function\' &&");
                        yaz.WriteLine("\t\t\t\t\t(typeof replacer !== \'object\' ||");
                        yaz.WriteLine("\t\t\t\t\ttypeof replacer.length !== \'number\')) {");
                        yaz.WriteLine("\t\t\t\tthrow new Error(\'JSON.stringify\');");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t// Make a fake root object containing our value under the key of \'\'.");
                        yaz.WriteLine("\t\t\t// Return the result of stringifying the value.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn str(\'\', { \'\': value });");
                        yaz.WriteLine("\t\t};");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t// If the JSON object does not yet have a parse method, give it one.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tif (typeof JSON.parse !== \'function\') {");
                        yaz.WriteLine("\t\tJSON.parse = function (text, reviver) {");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t// The parse method takes a text and an optional reviver function, and returns");
                        yaz.WriteLine("\t\t\t// a JavaScript value if the text is a valid JSON text.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tvar j;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tfunction walk(holder, key) {");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// The walk method is used to recursively walk the resulting structure so");
                        yaz.WriteLine("\t\t\t\t// that modifications can be made.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tvar k, v, value = holder[key];");
                        yaz.WriteLine("\t\t\t\tif (value && typeof value === \'object\') {");
                        yaz.WriteLine("\t\t\t\t\tfor (k in value) {");
                        yaz.WriteLine("\t\t\t\t\t\tif (Object.prototype.hasOwnProperty.call(value, k)) {");
                        yaz.WriteLine("\t\t\t\t\t\t\tv = walk(value, k);");
                        yaz.WriteLine("\t\t\t\t\t\t\tif (v !== undefined) {");
                        yaz.WriteLine("\t\t\t\t\t\t\t\tvalue[k] = v;");
                        yaz.WriteLine("\t\t\t\t\t\t\t} else {");
                        yaz.WriteLine("\t\t\t\t\t\t\t\tdelete value[k];");
                        yaz.WriteLine("\t\t\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\treturn reviver.call(holder, key, value);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t// Parsing happens in four stages. In the first stage, we replace certain");
                        yaz.WriteLine("\t\t\t// Unicode characters with escape sequences. JavaScript handles many characters");
                        yaz.WriteLine("\t\t\t// incorrectly, either silently deleting them, or treating them as line endings.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\ttext = String(text);");
                        yaz.WriteLine("\t\t\tcx.lastIndex = 0;");
                        yaz.WriteLine("\t\t\tif (cx.test(text)) {");
                        yaz.WriteLine("\t\t\t\ttext = text.replace(cx, function (a) {");
                        yaz.WriteLine("\t\t\t\t\treturn \'\\\\u\' +");
                        yaz.WriteLine("\t\t\t\t\t\t(\'0000\' + a.charCodeAt(0).toString(16)).slice(-4);");
                        yaz.WriteLine("\t\t\t\t});");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t// In the second stage, we run the text against regular expressions that look");
                        yaz.WriteLine("\t\t\t// for non-JSON patterns. We are especially concerned with \'()\' and \'new\'");
                        yaz.WriteLine("\t\t\t// because they can cause invocation, and \'=\' because it can cause mutation.");
                        yaz.WriteLine("\t\t\t// But just to be safe, we want to reject all unexpected forms.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t// We split the second stage into 4 regexp operations in order to work around");
                        yaz.WriteLine("\t\t\t// crippling inefficiencies in IE\'s and Safari\'s regexp engines. First we");
                        yaz.WriteLine("\t\t\t// replace the JSON backslash pairs with \'@\' (a non-JSON character). Second, we");
                        yaz.WriteLine("\t\t\t// replace all simple value tokens with \']\' characters. Third, we delete all");
                        yaz.WriteLine("\t\t\t// open brackets that follow a colon or comma or that begin the text. Finally,");
                        yaz.WriteLine("\t\t\t// we look to see that the remaining characters are only whitespace or \']\' or");
                        yaz.WriteLine("\t\t\t// \',\' or \':\' or \'{\' or \'}\'. If that is so, then the text is safe for eval.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (/^[\\],:{}\\s]*$/");
                        yaz.WriteLine("\t\t\t\t\t.test(text.replace(/\\\\(?:[\"\\\\\\/bfnrt]|u[0-9a-fA-F]{4})/g, \'@\')");
                        yaz.WriteLine("\t\t\t\t\t\t.replace(/\"[^\"\\\\\\n\\r]*\"|true|false|null|-?\\d+(?:\\.\\d*)?(?:[eE][+\\-]?\\d+)?/g, \']\')");
                        yaz.WriteLine("\t\t\t\t\t\t.replace(/(?:^|:|,)(?:\\s*\\[)+/g, \'\'))) {");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// In the third stage we use the eval function to compile the text into a");
                        yaz.WriteLine("\t\t\t\t// JavaScript structure. The \'{\' operator is subject to a syntactic ambiguity");
                        yaz.WriteLine("\t\t\t\t// in JavaScript: it can begin a block or an object literal. We wrap the text");
                        yaz.WriteLine("\t\t\t\t// in parens to eliminate the ambiguity.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tj = eval(\'(\' + text + \')\');");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t// In the optional fourth stage, we recursively walk the new structure, passing");
                        yaz.WriteLine("\t\t\t\t// each name/value pair to a reviver function for possible transformation.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\treturn typeof reviver === \'function\'");
                        yaz.WriteLine("\t\t\t\t\t? walk({ \'\': j }, \'\')");
                        yaz.WriteLine("\t\t\t\t\t: j;");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t// If the text is not JSON parseable, then a SyntaxError is thrown.");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tthrow new SyntaxError(\'JSON.parse\');");
                        yaz.WriteLine("\t\t};");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}());");
                        yaz.Close();
                    }
                }
            }
        }

        #endregion
    }
}
