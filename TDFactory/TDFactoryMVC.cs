using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
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

        //
        void CreateMVC()
        {
            CreateDirectories();
            CreateReadMe();

            if (chkMVCHepsi.Checked == true)
            {
                CreateModelLayer();
                CreateViewLayer();
                CreateControllerLayer();
                CreateWebConfig();
                CreateWcfService();
                CreateStylelScript();

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
                    CreateWcfService();
                }

                if (chkMVCStoredProc.Checked == true)
                {
                    CreateStoredProcedure();
                }

                if (chkMVCStilScript.Checked == true)
                {
                    CreateStylelScript();
                }
            }

            CreateRegistrar();
        }

        //
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

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Views"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Views");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Models"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Models");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Views\\Shared"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Views\\Shared");
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

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Service"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Service");
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

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Models"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Models");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Models"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Models");
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

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Views"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Views");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Views\\Shared"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Views\\Shared");
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

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers");
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
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Service"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Service");
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

        //
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

        //
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

                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                yaz.WriteLine("\t\tpublic string " + PrimaryTableName + "Adi { get; set; }");
                            }
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }
            }
        }

        //
        void CreateLayout()
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
                    yaz.WriteLine("\t<meta charset=\"UTF-8\" />");
                    yaz.WriteLine("\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<title>@ViewBag.Title</title>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Views/Shared/Controls/_Scripts.cshtml\"); }");
                    yaz.WriteLine("</head>");
                    yaz.WriteLine("<body>");
                    yaz.WriteLine("\t<input id=\"hdnUrl\" type=\"hidden\" value=\"@Urling.FullURL\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Views/Shared/Controls/_Header.cshtml\"); }");
                    yaz.WriteLine("\t<div id=\"content\">");
                    yaz.WriteLine("\t\t@RenderBody()");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Views/Shared/Controls/_Footer.cshtml\"); }");
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
                    yaz.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"@AppMgr.StylePath/main.css\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/jquery/jquery.min.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/pathscript.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/main.js\"></script>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls\\_Footer.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Views\\Shared\\Controls\\_Header.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\_Layout.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("<!DOCTYPE html>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<html>");
                    yaz.WriteLine("<head>");
                    yaz.WriteLine("\t<meta charset=\"UTF-8\" />");
                    yaz.WriteLine("\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
                    yaz.WriteLine("\t<title>" + projectName + " Admin Panel</title>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Areas/Admin/Views/Shared/Controls/_Scripts.cshtml\"); }");
                    yaz.WriteLine("</head>");
                    yaz.WriteLine("<body>");
                    yaz.WriteLine("\t<input id=\"hdnUrl\" type=\"hidden\" value=\"@Urling.FullURLWithParams\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Areas/Admin/Views/Shared/Controls/_Header.cshtml\"); }");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Areas/Admin/Views/Shared/Controls/_LeftMenu.cshtml\"); }");
                    yaz.WriteLine("\t@RenderBody()");
                    yaz.WriteLine("\t@{ Html.RenderPartial(\"~/Areas/Admin/Views/Shared/Controls/_Footer.cshtml\"); }");
                    yaz.WriteLine("</body>");
                    yaz.WriteLine("</html>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls\\_Scripts.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("<link rel=\"shortcut icon\" href=\"@AppMgr.MainPath/favicon.ico\" type=\"image/x-icon\">");
                    yaz.WriteLine("<link rel=\"icon\" href=\"@AppMgr.MainPath/favicon.ico\" type=\"image/x-icon\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/bootstrap.min.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/bootstrap-responsive.min.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/jquery.gritter.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/select2.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/matrix-style.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/matrix-media.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/font-awesome/css/font-awesome.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/main.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("<script src=\"@AppMgr.ScriptPath/jquery/jquery.min.js\"></script>");
                    yaz.WriteLine("<script src=\"@AppMgr.AdminScriptPath/jquery.gritter.min.js\"></script>");
                    yaz.WriteLine("<script src=\"@AppMgr.AdminScriptPath/jquery.dataTables.min.js\"></script>");
                    yaz.WriteLine("<script src=\"@AppMgr.AdminScriptPath/bootstrap.min.js\"></script>");
                    yaz.WriteLine("<script src=\"@AppMgr.AdminScriptPath/matrix.js\"></script>");
                    yaz.WriteLine("<script src=\"@AppMgr.AdminScriptPath/matrix.popover.js\"></script>");
                    yaz.WriteLine("<script src=\"@AppMgr.AdminScriptPath/matrix.tables.js\"></script>");
                    yaz.WriteLine("<script src=\"@AppMgr.AdminScriptPath/select2.min.js\"></script>");
                    yaz.WriteLine("<script src=\"@AppMgr.AdminScriptPath/ckeditor/ckeditor.js\"></script>");
                    yaz.WriteLine("<script src=\"@AppMgr.AdminScriptPath/pathscript.js\"></script>");
                    yaz.WriteLine("<script src=\"@AppMgr.AdminScriptPath/main.js\"></script>");

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls\\_Header.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"header\">");
                    yaz.WriteLine("\t<h1><a><img src=\"@AppMgr.ImagePath/logo.png\" /></a></h1>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"user-nav\" class=\"navbar navbar-inverse\">");
                    yaz.WriteLine("\t<ul class=\"nav\">");
                    yaz.WriteLine("\t\t<li class=\"\"><a title=\"Bilgilerinizi düzenlemek için tıklayın.\" href=\"@AppMgr.AdminPath/Home\"><i class=\"icon icon-user\"></i> <span class=\"text\"> Hoşgeldiniz (Kullanıcı Adı)</span></a></li>");
                    yaz.WriteLine("\t\t<li class=\"\"><a href=\"javascript:;\" class=\"logout\"><i class=\"icon icon-share-alt\"></i> <span class=\"text\"> Çıkış</span></a></li>");
                    yaz.WriteLine("\t\t<li><a target=\"_blank\" href=\"@AppMgr.MainPath\" ><i class=\"icon icon-home\"></i> <span class=\"text\"> Web Sitesine Git</span></a></li>");
                    yaz.WriteLine("\t</ul>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"search\">");
                    yaz.WriteLine("\t<input id=\"txtMainSearch\" type=\"text\" placeholder=\"Kelime...\" />");
                    yaz.WriteLine("\t<button id=\"btnMainSearch\" type=\"button\" class=\"tip-bottom\" title=\"Ara\"><i class=\"icon-search icon-white\"></i></button>");
                    yaz.WriteLine("</div>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls\\_LeftMenu.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"sidebar\">");
                    yaz.WriteLine("\t<a href=\"javascript:;\" class=\"visible-phone\"><i class=\"icon icon-reorder\"></i> Menü</a>");
                    yaz.WriteLine("\t<ul>");
                    yaz.WriteLine("\t\t<li data-url=\"Home\" class=\"active\">");
                    yaz.WriteLine("\t\t\t<a href=\"@AppMgr.AdminPath/Home/Index\"><i class=\"icon icon-home\"></i> <span>Ana Sayfa</span></a>");
                    yaz.WriteLine("\t\t</li>");

                    List<string> addedTables = new List<string>();

                    foreach (string Table in selectedTables)
                    {
                        if (!addedTables.Contains(Table))
                        {
                            List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                            SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                            List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                            fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                            List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                            fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                            if (fkcList.Count <= 0)
                            {
                                yaz.WriteLine("\t\t<li data-url=\"" + Table + "\">");
                                yaz.WriteLine("\t\t\t<a href=\"@AppMgr.AdminPath/" + Table + "\"><i class=\"icon icon-home\"></i> <span>" + Table + "</span></a>");
                                yaz.WriteLine("\t\t</li>");
                            }
                            else
                            {
                                yaz.WriteLine("\t\t<li class=\"submenu\">");
                                yaz.WriteLine("\t\t\t<a href=\"javascript:;\"><i class=\"icon icon-home\"></i> <span>" + Table + "</span></a>");
                                yaz.WriteLine("\t\t\t<ul>");
                                yaz.WriteLine("\t\t\t\t<li data-url=\"" + Table + "\"><a href=\"@AppMgr.AdminPath/" + Table + "\">" + Table + "</a></li>");

                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;

                                    if (!addedTables.Contains(ForeignTableName))
                                    {
                                        yaz.WriteLine("\t\t\t\t<li data-url=\"" + ForeignTableName + "\"><a href=\"@AppMgr.AdminPath/" + ForeignTableName + "\">" + ForeignTableName + "</a></li>");
                                        addedTables.Add(ForeignTableName);
                                    }
                                }

                                yaz.WriteLine("\t\t\t</ul>");
                                yaz.WriteLine("\t\t</li>");
                            }

                            addedTables.Add(Table);
                        }
                    }

                    yaz.WriteLine("\t</ul>");
                    yaz.WriteLine("</div>");

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls\\_Footer.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("<div class=\"row-fluid\">");
                    yaz.WriteLine("\t<div id=\"footer\" class=\"span12\"> Developed by <a target=\"_blank\" href=\"http://www.sinasalik.net\">Sina SALIK</a> </div>");
                    yaz.WriteLine("</div>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\Controls\\_CopyDelete.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("<div id=\"cpyData\" class=\"modal hide\" aria-hidden=\"true\" style=\"display: none;\">");
                    yaz.WriteLine("\t<div class=\"modal-header\">");
                    yaz.WriteLine("\t\t<button data-dismiss=\"modal\" class=\"close\" type=\"button\">×</button>");
                    yaz.WriteLine("\t\t<h3>Kopyala</h3>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-body\">");
                    yaz.WriteLine("\t\t<p>İlgili veriyi kopyalamak istediğinize emin misiniz?</p>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-footer\">");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn btn-primary cpy-yes\" href=\"javascript:;\">Evet</a>");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn cpy-no\" href=\"javascript:;\">Hayır</a>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"dltData\" class=\"modal hide\" aria-hidden=\"true\" style=\"display: none;\">");
                    yaz.WriteLine("\t<div class=\"modal-header\">");
                    yaz.WriteLine("\t\t<button data-dismiss=\"modal\" class=\"close\" type=\"button\">×</button>");
                    yaz.WriteLine("\t\t<h3>Sil</h3>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-body\">");
                    yaz.WriteLine("\t\t<p>İlgili veriyi silmek istediğinize emin misiniz?</p>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-footer\">");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn btn-primary dlt-yes\" href=\"javascript:;\">Evet</a>");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn dlt-no\" href=\"javascript:;\">Hayır</a>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"rdltData\" class=\"modal hide\" aria-hidden=\"true\" style=\"display: none;\">");
                    yaz.WriteLine("\t<div class=\"modal-header\">");
                    yaz.WriteLine("\t\t<button data-dismiss=\"modal\" class=\"close\" type=\"button\">×</button>");
                    yaz.WriteLine("\t\t<h3>Kaldır</h3>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-body\">");
                    yaz.WriteLine("\t\t<p>İlgili veriyi kaldırmak istediğinize emin misiniz?</p>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-footer\">");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn btn-primary rdlt-yes\" href=\"javascript:;\">Evet</a>");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn rdlt-no\" href=\"javascript:;\">Hayır</a>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"clrData\" class=\"modal hide\" aria-hidden=\"true\" style=\"display: none;\">");
                    yaz.WriteLine("\t<div class=\"modal-header\">");
                    yaz.WriteLine("\t\t<button data-dismiss=\"modal\" class=\"close\" type=\"button\">×</button>");
                    yaz.WriteLine("\t\t<h3>Temizle</h3>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-body\">");
                    yaz.WriteLine("\t\t<p>İlgili verileri temizlemek istediğinize emin misiniz?</p>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-footer\">");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn btn-primary clr-yes\" href=\"javascript:;\">Evet</a>");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn clr-no\" href=\"javascript:;\">Hayır</a>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</div>");
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

                    yaz.WriteLine("\t\t\treturn View(\"Ekle\", table);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    //Duzenle
                    yaz.WriteLine("\t\t[HttpGet]");
                    yaz.WriteLine("\t\tpublic ActionResult Duzenle(" + columntype.ReturnCSharpType() + " id)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\t" + Table + " table = entity." + Table + ".Find(id);");
                    yaz.WriteLine("");

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

        //
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

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Home\\Login.cshtml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("@using TDLibrary");
                    yaz.WriteLine("");
                    yaz.WriteLine("@{");
                    yaz.WriteLine("\tLayout = null;");
                    yaz.WriteLine("}");
                    yaz.WriteLine("");
                    yaz.WriteLine("<!DOCTYPE html>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<html>");
                    yaz.WriteLine("<head>");
                    yaz.WriteLine("\t<meta charset=\"UTF-8\" />");
                    yaz.WriteLine("\t<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<title>Admin Panel Giriş</title>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<link rel=\"shortcut icon\" href=\"@AppMgr.MainPath/favicon.ico\" type=\"image/x-icon\">");
                    yaz.WriteLine("\t<link rel=\"icon\" href=\"@AppMgr.MainPath/favicon.ico\" type=\"image/x-icon\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<link href=\"@AppMgr.AdminStylePath/bootstrap.min.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("\t<link href=\"@AppMgr.AdminStylePath/bootstrap-responsive.min.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<link href=\"@AppMgr.AdminStylePath/matrix-login.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("\t<link href=\"@AppMgr.AdminStylePath/font-awesome/css/font-awesome.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<style>");
                    yaz.WriteLine("\t\t#imgLoading { float: left; margin: 5px 5px 0px 0px; height: 20px; display: none; }");
                    yaz.WriteLine("\t</style>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<script src=\"@AppMgr.ScriptPath/jquery/jquery.min.js\"></script>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<script src=\"@AppMgr.AdminScriptPath/pathscript.js\"></script>");
                    yaz.WriteLine("\t<script src=\"@AppMgr.AdminScriptPath/login.js\"></script");
                    yaz.WriteLine("</head>");
                    yaz.WriteLine("<body>");
                    yaz.WriteLine("\t<input id=\"hdnUrl\" type=\"hidden\" value=\"@Urling.FullURL\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<div id=\"loginbox\">");
                    yaz.WriteLine("\t\t<div class=\"control-group normal_text\"> <h3><img src=\"@AppMgr.AdminImagePath/logo.png\" alt=\"Logo\" /></h3></div>");
                    yaz.WriteLine("\t\t<div class=\"control-group\">");
                    yaz.WriteLine("\t\t\t<div class=\"controls\">");
                    yaz.WriteLine("\t\t\t\t<div class=\"main_input_box\">");
                    yaz.WriteLine("\t\t\t\t\t<span class=\"add-on bg_lg\"><i class=\"icon-user\"> </i></span><input id=\"txtUserName\" type=\"text\" placeholder=\"Kullanıcı Adı\" />");
                    yaz.WriteLine("\t\t\t\t</div>");
                    yaz.WriteLine("\t\t\t</div>");
                    yaz.WriteLine("\t\t</div>");
                    yaz.WriteLine("\t\t<div class=\"control-group\">");
                    yaz.WriteLine("\t\t\t<div class=\"controls\">");
                    yaz.WriteLine("\t\t\t\t<div class=\"main_input_box\">");
                    yaz.WriteLine("\t\t\t\t\t<span class=\"add-on bg_ly\"><i class=\"icon-lock\"></i></span><input id=\"txtPassword\" type=\"password\" placeholder=\"Şifre\" />");
                    yaz.WriteLine("\t\t\t\t</div>");
                    yaz.WriteLine("\t\t\t</div>");
                    yaz.WriteLine("\t\t</div>");
                    yaz.WriteLine("\t\t<div class=\"alert alert-error\" style=\"display:none;\">");
                    yaz.WriteLine("\t\t\t<button class=\"close\" data-dismiss=\"alert\">×</button>");
                    yaz.WriteLine("\t\t\t<strong>Hata!</strong> <span id=\"hataMesaj\"></span>");
                    yaz.WriteLine("\t\t</div>");
                    yaz.WriteLine("\t\t<div class=\"form-actions\">");
                    yaz.WriteLine("\t\t\t<span class=\"pull-right\"><img id=\"imgLoading\" src=\"@AppMgr.AdminImagePath/loading.gif\" /><input id=\"btnGiris\" type=\"button\" class=\"btn btn-success\" value=\"Giriş Yap\"></span>");
                    yaz.WriteLine("\t\t</div>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</body>");
                    yaz.WriteLine("</html>");

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
                    yaz.WriteLine("\tViewBag.Title = \"Index\";");
                    yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_LayoutAdmin.cshtml\";");
                    yaz.WriteLine("}");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"content\">");
                    yaz.WriteLine("\t<div id=\"content-header\">");
                    yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a href=\"javascript:;\" class=\"tip-bottom\"><i class=\"icon-home\"></i> Ana Sayfa</a></div>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<div class=\"container-fluid\">");
                    yaz.WriteLine("\t\t<div class=\"quick-actions_homepage\">");
                    yaz.WriteLine("\t\t\t<ul class=\"quick-actions\">");

                    string[] colors = new string[] { "bg_lb", "bg_lg", "bg_ly", "bg_lo", "bg_ls", "bg_lr", "bg_lv" };
                    List<string> addedTables = new List<string>();
                    List<string> tempTables = new List<string>();
                    int i = 0;

                    foreach (string Table in selectedTables)
                    {
                        if (!addedTables.Contains(Table))
                        {
                            List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                            SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                            List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                            fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t\t<li class=\"" + colors[i % 7] + "\"> <a href=\"@AppMgr.AdminPath/" + Table + "\"> <i class=\"icon-home\"></i> " + Table + "</a> </li>");

                                tempTables.Add(Table);

                                i++;
                            }

                            addedTables.Add(Table);
                        }
                    }

                    if (tempTables.Count == 0)
                    {
                        i = 0;

                        foreach (string Table in selectedTables)
                        {
                            yaz.WriteLine("\t\t\t\t<li class=\"" + colors[i % 7] + "\"> <a href=\"@AppMgr.AdminPath/" + Table + "\"> <i class=\"icon-home\"></i> " + Table + "</a> </li>");

                            i++;
                        }
                    }

                    yaz.WriteLine("\t\t\t</ul>");
                    yaz.WriteLine("\t\t</div>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</div>");

                    yaz.Close();
                }
            }
        }

        //
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
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tpublic ActionResult Login()");
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

        #region Angular

        void CreateAngular()
        {
            CreateAngularDirectories();
            CreateReadMe();
            CreateAngularFiles();

            if (chkMVCHepsi.Checked == true)
            {
                CreateAngularModelLayer();
                CreateAngularViewLayer();
                CreateAngularControllerLayer();
                CreateAngularServiceLayer();
                CreateAngularSharedService();
                CreateAngularTypeScriptLayer();
                CreateWcfService();
                CreateWebConfig();
                CreateStylelScript();

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
                    CreateAngularServiceLayer();
                    CreateAngularSharedService();
                    CreateAngularTypeScriptLayer();
                }

                if (chkMVCWebConfig.Checked == true)
                {
                    CreateWebConfig();
                }

                if (chkMVCWcfServis.Checked == true)
                {
                    CreateWcfService();
                }

                if (chkMVCStoredProc.Checked == true)
                {
                    CreateStoredProcedure();
                }

                if (chkMVCStilScript.Checked == true)
                {
                    CreateStylelScript();
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

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Lib"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Lib");
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

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\css"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\css");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\translations"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\translations");
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
                    }

                    if (chkMVCController.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\App_Start"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\App_Start");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Lib"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Lib");
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

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\css"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\css");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\img");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\translations"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\translations");
                        }
                    }
                }
            }
            else
            {
                if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + _tableName.ToHyperLinkText(true)))
                {
                    Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + _tableName.ToHyperLinkText(true));
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
                    yaz.WriteLine("import '../../Content/js/jquery/jquery.min.js';");
                    yaz.WriteLine("");
                    yaz.WriteLine("declare global {");
                    yaz.WriteLine("\tinterface JQuery {");
                    yaz.WriteLine("\t\tdataTable(obj: any): JQuery;");
                    yaz.WriteLine("\t\ttypeahead(obj: any): JQuery;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: \"" + projectName.Substring(0, 3).ToHyperLinkText(true) + "-app\",");
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

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\shared\\layout.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<router-outlet></router-outlet>");
                    yaz.WriteLine("<" + projectName.Substring(0, 3).ToHyperLinkText(true) + "-scripts></" + projectName.Substring(0, 3).ToHyperLinkText(true) + "-scripts>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\shared\\layout.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: '" + projectName.Substring(0, 3).ToHyperLinkText(true) + "-layout',");
                    yaz.WriteLine("\ttemplateUrl: './layout.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class LayoutComponent {");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\shared\\controls\\scripts.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component, ViewEncapsulation } from '@angular/core';");
                    yaz.WriteLine("import '../../../../Content/js/pathscript.js';");
                    yaz.WriteLine("import '../../../../Content/js/main.js';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: '" + projectName.Substring(0, 3).ToHyperLinkText(true) + "-scripts',");
                    yaz.WriteLine("\ttemplate: '',");
                    yaz.WriteLine("\tstyleUrls: [");
                    yaz.WriteLine("\t\t'../../../../Content/css/main.css'");
                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\tencapsulation: ViewEncapsulation.None");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class ScriptsComponent {");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\home\\index.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("Giriş Sayfası");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\home\\index.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\ttemplateUrl: './index.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class HomeComponent {");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            CreateAngularAppModule();
            CreateAngularRoutingModule();
            CreateAngularAdminFiles();
        }

        void CreateAngularAdminFiles()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\layoutAdmin.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<admin-header></admin-header>");
                    yaz.WriteLine("<admin-leftmenu></admin-leftmenu>");
                    yaz.WriteLine("<router-outlet></router-outlet>");
                    yaz.WriteLine("<admin-footer></admin-footer>");
                    yaz.WriteLine("<admin-scripts></admin-scripts>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\layoutAdmin.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");
                    yaz.WriteLine("import { SharedService } from '../../services/shared';");
                    yaz.WriteLine("import { Router } from '@angular/router';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: 'admin-layout',");
                    yaz.WriteLine("\ttemplateUrl: './layoutAdmin.html',");
                    yaz.WriteLine("\tproviders: [SharedService]");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminLayoutComponent {");
                    yaz.WriteLine("\terrorMsg: string;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tconstructor(private service: SharedService, private router: Router) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t\tthis.service.getLoginControl().subscribe((resData) => {");
                    yaz.WriteLine("\t\t\tif (resData == false) {");
                    yaz.WriteLine("\t\t\t\tthis.router.navigate(['/Admin']);");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls\\copydelete.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<div id=\"cpyData\" class=\"modal hide\" aria-hidden=\"true\" style=\"display: none;\">");
                    yaz.WriteLine("\t<div class=\"modal-header\">");
                    yaz.WriteLine("\t\t<button data-dismiss=\"modal\" class=\"close\" type=\"button\">×</button>");
                    yaz.WriteLine("\t\t<h3>Kopyala</h3>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-body\">");
                    yaz.WriteLine("\t\t<p>İlgili veriyi kopyalamak istediğinize emin misiniz?</p>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-footer\">");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn btn-primary cpy-yes\" href=\"javascript:;\">Evet</a>");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn cpy-no\" href=\"javascript:;\">Hayır</a>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"dltData\" class=\"modal hide\" aria-hidden=\"true\" style=\"display: none;\">");
                    yaz.WriteLine("\t<div class=\"modal-header\">");
                    yaz.WriteLine("\t\t<button data-dismiss=\"modal\" class=\"close\" type=\"button\">×</button>");
                    yaz.WriteLine("\t\t<h3>Sil</h3>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-body\">");
                    yaz.WriteLine("\t\t<p>İlgili veriyi silmek istediğinize emin misiniz?</p>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-footer\">");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn btn-primary dlt-yes\" href=\"javascript:;\">Evet</a>");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn dlt-no\" href=\"javascript:;\">Hayır</a>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"rdltData\" class=\"modal hide\" aria-hidden=\"true\" style=\"display: none;\">");
                    yaz.WriteLine("\t<div class=\"modal-header\">");
                    yaz.WriteLine("\t\t<button data-dismiss=\"modal\" class=\"close\" type=\"button\">×</button>");
                    yaz.WriteLine("\t\t<h3>Kaldır</h3>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-body\">");
                    yaz.WriteLine("\t\t<p>İlgili veriyi kaldırmak istediğinize emin misiniz?</p>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-footer\">");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn btn-primary rdlt-yes\" href=\"javascript:;\">Evet</a>");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn rdlt-no\" href=\"javascript:;\">Hayır</a>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"clrData\" class=\"modal hide\" aria-hidden=\"true\" style=\"display: none;\">");
                    yaz.WriteLine("\t<div class=\"modal-header\">");
                    yaz.WriteLine("\t\t<button data-dismiss=\"modal\" class=\"close\" type=\"button\">×</button>");
                    yaz.WriteLine("\t\t<h3>Temizle</h3>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-body\">");
                    yaz.WriteLine("\t\t<p>İlgili verileri temizlemek istediğinize emin misiniz?</p>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-footer\">");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn btn-primary clr-yes\" href=\"javascript:;\">Evet</a>");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn clr-no\" href=\"javascript:;\">Hayır</a>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<ul id=\"tdAlertMessage\" class=\"tdAlert\">");
                    yaz.WriteLine("\t<li class=\"tdAlertDelete\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veri silindi.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("\t<li class=\"tdAlertDeleteNot\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veri silinemedi.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("\t<li class=\"tdAlertRealDelete\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veri kaldırıldı.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("\t<li class=\"tdAlertRealDeleteNot\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veri kaldırılamadı.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("\t<li class=\"tdAlertCopy\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veri kopyalandı.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("\t<li class=\"tdAlertCopyNot\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veri kopyalanamadı.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("\t<li class=\"tdAlertClear\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veriler temizlendi.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("\t<li class=\"tdAlertClearNot\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veriler temizlenemedi.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("</ul>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls\\copydelete.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: 'admin-copydelete',");
                    yaz.WriteLine("\ttemplateUrl: './copydelete.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminCopyDeleteComponent {");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls\\footer.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<div class=\"row-fluid\">");
                    yaz.WriteLine("\t<div id=\"footer\" class=\"span12\"> Developed by <a target=\"_blank\" href=\"http://www.sinasalik.net\">Sina SALIK</a> </div>");
                    yaz.WriteLine("</div>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls\\footer.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: 'admin-footer',");
                    yaz.WriteLine("\ttemplateUrl: './footer.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminFooterComponent {");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls\\header.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<div id=\"header\">");
                    yaz.WriteLine("\t<h1><a><img src=\"Content/admin/img/logo.png\" /></a></h1>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"user-nav\" class=\"navbar navbar-inverse\">");
                    yaz.WriteLine("\t<ul class=\"nav\">");
                    yaz.WriteLine("\t\t<li><a title=\"Bilgilerinizi düzenlemek için tıklayın.\" routerLink=\"/Admin/Home\"><i class=\"icon icon-user\"></i> <span class=\"text\"> Hoşgeldiniz (Kullanıcı Adı)</span></a></li>");
                    yaz.WriteLine("\t\t<li><a class=\"logout\" (click)=\"onLogout()\"><i class=\"icon icon-share-alt\"></i> <span class=\"text\"> Çıkış</span></a></li>");
                    yaz.WriteLine("\t\t<li><a target=\"_blank\" href=\"{{ website }}\" ><i class=\"icon icon-home\"></i> <span class=\"text\"> Web Sitesine Git</span></a></li>");
                    yaz.WriteLine("\t</ul>");
                    yaz.WriteLine("</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("<div id=\"search\">");
                    yaz.WriteLine("\t<input id=\"txtMainSearch\" type=\"text\" placeholder=\"Kelime...\" (keypress)=\"onKeyPress($event)\" />");
                    yaz.WriteLine("\t<button id=\"btnMainSearch\" type=\"button\" class=\"tip-bottom\" title=\"Ara\" (click)=\"onClick()\"><i class=\"icon-search icon-white\"></i></button>");
                    yaz.WriteLine("</div>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls\\header.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");
                    yaz.WriteLine("import { Router } from '@angular/router';");
                    yaz.WriteLine("import { SharedService } from '../../../services/shared';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: 'admin-header',");
                    yaz.WriteLine("\ttemplateUrl: './header.html',");
                    yaz.WriteLine("\tproviders: [SharedService]");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminHeaderComponent {");
                    yaz.WriteLine("\terrorMsg: string;");
                    yaz.WriteLine("\twebsite: string = \"http://localhost/" + projectName + "/\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tconstructor(private service: SharedService, private router: Router) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t\t$('#txtMainSearch').typeahead({");
                    yaz.WriteLine("\t\t\tsource: [");

                    int i = 0;
                    foreach (string Table in selectedTables)
                    {
                        if (selectedTables.Count == i + 1)
                        {
                            yaz.WriteLine("\t\t\t\t'" + Table + "'");
                        }
                        else
                        {
                            yaz.WriteLine("\t\t\t\t'" + Table + "',");
                        }

                        i++;
                    }

                    yaz.WriteLine("\t\t\t],");
                    yaz.WriteLine("\t\t\titems: 4");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tonClick() {");
                    yaz.WriteLine("\t\tvar txtValue = $(\"#txtMainSearch\").val();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tswitch (txtValue) {");

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("\t\t\tcase \"" + Table + "\":");
                        yaz.WriteLine("\t\t\t\tthis.router.navigate(['/Admin/" + Table + "']);");
                        yaz.WriteLine("\t\t\t\tbreak;");
                    }

                    yaz.WriteLine("\t\t\tdefault:");
                    yaz.WriteLine("\t\t\t\talert(\"Aradığınız kelimeye uygun sonuç bulunamadı...\");");
                    yaz.WriteLine("\t\t\t\tbreak;");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tonLogout() {");
                    yaz.WriteLine("\t\tthis.service.getLogout().subscribe((resData) => {");
                    yaz.WriteLine("\t\t\tif (resData == true) {");
                    yaz.WriteLine("\t\t\t\tthis.router.navigate(['/Admin/']);");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tonKeyPress(event: any) {");
                    yaz.WriteLine("\t\tif (event.keyCode == \"13\") {");
                    yaz.WriteLine("\t\t\tthis.onClick();");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls\\leftmenu.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<div id=\"sidebar\">");
                    yaz.WriteLine("\t<a href=\"javascript:;\" class=\"visible-phone\"><i class=\"icon icon-reorder\"></i> Menü</a>");
                    yaz.WriteLine("\t<ul>");
                    yaz.WriteLine("\t\t<li data-url=\"Home\" class=\"active\">");
                    yaz.WriteLine("\t\t\t<a routerLink=\"/Admin/Home\"><i class=\"icon icon-home\"></i> <span>Ana Sayfa</span></a>");
                    yaz.WriteLine("\t\t</li>");

                    List<string> addedTables = new List<string>();

                    foreach (string Table in selectedTables)
                    {
                        if (!addedTables.Contains(Table))
                        {
                            List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                            SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                            List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                            fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                            List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                            fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                            if (fkcList.Count <= 0)
                            {
                                yaz.WriteLine("\t\t<li data-url=\"" + Table + "\">");
                                yaz.WriteLine("\t\t\t<a routerLink=\"/Admin/" + Table + "\"><i class=\"icon icon-home\"></i> <span>" + Table + "</span></a>");
                                yaz.WriteLine("\t\t</li>");
                            }
                            else
                            {
                                yaz.WriteLine("\t\t<li class=\"submenu\">");
                                yaz.WriteLine("\t\t\t<a href=\"javascript:;\"><i class=\"icon icon-home\"></i> <span>" + Table + "</span></a>");
                                yaz.WriteLine("\t\t\t<ul>");
                                yaz.WriteLine("\t\t\t\t<li data-url=\"" + Table + "\"><a routerLink=\"/Admin/" + Table + "\">" + Table + "</a></li>");

                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;

                                    if (!addedTables.Contains(ForeignTableName))
                                    {
                                        yaz.WriteLine("\t\t\t\t<li data-url=\"" + ForeignTableName + "\"><a routerLink=\"/Admin/" + ForeignTableName + "\">" + ForeignTableName + "</a></li>");
                                        addedTables.Add(ForeignTableName);
                                    }
                                }

                                yaz.WriteLine("\t\t\t</ul>");
                                yaz.WriteLine("\t\t</li>");
                            }

                            addedTables.Add(Table);
                        }
                    }

                    yaz.WriteLine("\t</ul>");
                    yaz.WriteLine("</div>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls\\leftmenu.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");
                    yaz.WriteLine("import { Router, RouterEvent, ActivationEnd } from '@angular/router';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: 'admin-leftmenu',");
                    yaz.WriteLine("\ttemplateUrl: './leftmenu.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminLeftMenuComponent {");
                    yaz.WriteLine("\terrorMsg: string;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tconstructor(private router: Router) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tngAfterContentInit() {");
                    yaz.WriteLine("\t\tthis.LeftMenuClick();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tthis.router.events.subscribe((event: RouterEvent) => {");
                    yaz.WriteLine("\t\t\tif (event instanceof ActivationEnd) {");
                    yaz.WriteLine("\t\t\t\tthis.LeftMenuClick();");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tLeftMenuClick() {");
                    yaz.WriteLine("\t\t$(\"#hdnUrl\").val(location.href);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tvar AdminPath = \"http://localhost/" + projectName + "/Admin\";");
                    yaz.WriteLine("\t\tvar Url = location.href;");
                    yaz.WriteLine("\t\tvar Urling = Object();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (Url != undefined) {");
                    yaz.WriteLine("\t\t\tvar tempurl = Url.replace(AdminPath + \"/\", \"\");");
                    yaz.WriteLine("\t\t\tvar extParams = tempurl.split('?')[1];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\ttempurl = tempurl.replace(\"?\" + extParams, \"\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tUrling.path = tempurl;");
                    yaz.WriteLine("\t\t\tUrling.controller = tempurl.split('/')[0];");
                    yaz.WriteLine("\t\t\tUrling.action = tempurl.split('/')[1];");
                    yaz.WriteLine("\t\t\tUrling.parameter = tempurl.split('/')[2];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tif (extParams != undefined)");
                    yaz.WriteLine("\t\t\t\tUrling.parameters = extParams.split('&');");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (Urling.controller != undefined) {");
                    yaz.WriteLine("\t\t\tvar activeLi = $(\"#sidebar li[data-url='\" + Urling.controller + \"']\");");
                    yaz.WriteLine("\t\t\tvar passiveSubmenuLi = $(\"#sidebar li.submenu\");");
                    yaz.WriteLine("\t\t\tvar submenuLi = activeLi.parent(\"ul\").parent(\"li\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t$(\"#sidebar li\").removeClass(\"active\");");
                    yaz.WriteLine("\t\t\t$(\"#sidebar li\").removeClass(\"open\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tactiveLi.addClass(\"active\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tif (submenuLi.hasClass(\"submenu\")) {");
                    yaz.WriteLine("\t\t\t\tif ($(\"body\").width() > 970 || $(\"body\").width() <= 480) {");
                    yaz.WriteLine("\t\t\t\t\tsubmenuLi.addClass(\"open\");");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\tsubmenuLi.addClass(\"active\");");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tsetTimeout(function () {");
                    yaz.WriteLine("\t\t\t\tpassiveSubmenuLi.each(function () {");
                    yaz.WriteLine("\t\t\t\t\tif (!$(this).hasClass(\"open\")) {");
                    yaz.WriteLine("\t\t\t\t\t\t$(this).children(\"ul\").slideUp();");
                    yaz.WriteLine("\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t\telse {");
                    yaz.WriteLine("\t\t\t\t\t\t$(this).children(\"ul\").slideDown();");
                    yaz.WriteLine("\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t});");
                    yaz.WriteLine("\t\t\t}, 1);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls\\scripts.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component, ViewEncapsulation } from '@angular/core';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/jquery.dataTables.min.js';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/bootstrap.min.js';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/matrix.js';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/ckeditor/ckeditor.js';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/pathscript.js';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/main.js';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: 'admin-scripts',");
                    yaz.WriteLine("\ttemplate: '',");
                    yaz.WriteLine("\tstyleUrls: [");
                    yaz.WriteLine("\t\t'../../../../../../Content/admin/css/bootstrap.min.css',");
                    yaz.WriteLine("\t\t'../../../../../../Content/admin/css/bootstrap-responsive.min.css',");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t'../../../../../../Content/admin/css/matrix-style.css',");
                    yaz.WriteLine("\t\t'../../../../../../Content/admin/css/matrix-media.css',");
                    yaz.WriteLine("\t\t'../../../../../../Content/admin/css/font-awesome/css/font-awesome.css',");
                    yaz.WriteLine("\t\t'../../../../../../Content/admin/css/main.css'");
                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\tencapsulation: ViewEncapsulation.None");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminScriptsComponent {");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\home\\index.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<div id=\"content\">");
                    yaz.WriteLine("\t<div id=\"content-header\">");
                    yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a href=\"javascript:;\" class=\"tip-bottom\"><i class=\"icon-home\"></i> Ana Sayfa</a></div>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<div class=\"container-fluid\">");
                    yaz.WriteLine("\t\t<div class=\"quick-actions_homepage\">");
                    yaz.WriteLine("\t\t\t<ul class=\"quick-actions\">");

                    string[] colors = new string[] { "bg_lb", "bg_lg", "bg_ly", "bg_lo", "bg_ls", "bg_lr", "bg_lv" };
                    List<string> addedTables = new List<string>();
                    List<string> tempTables = new List<string>();
                    int i = 0;

                    foreach (string Table in selectedTables)
                    {
                        if (!addedTables.Contains(Table))
                        {
                            List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                            SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                            List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                            fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t\t<li class=\"" + colors[i % 7] + "\"> <a routerLink=\"/Admin/" + Table + "\"> <i class=\"icon-home\"></i> " + Table + "</a> </li>");
                                tempTables.Add(Table);
                                i++;
                            }

                            addedTables.Add(Table);
                        }
                    }

                    if (tempTables.Count == 0)
                    {
                        foreach (string Table in selectedTables)
                        {
                            yaz.WriteLine("\t\t\t\t<li class=\"" + colors[i % 7] + "\"> <a routerLink=\"/Admin/" + Table + "\"> <i class=\"icon-home\"></i> " + Table + "</a> </li>");
                            i++;
                        }
                    }

                    yaz.WriteLine("\t\t\t</ul>");
                    yaz.WriteLine("\t\t</div>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</div>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\home\\index.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\ttemplateUrl: './index.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminHomeComponent {");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\home\\login.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<form [formGroup]=\"girisForm\" (ngSubmit)=\"onSubmit()\">");
                    yaz.WriteLine("\t<div id=\"loginbox\">");
                    yaz.WriteLine("\t\t<div class=\"control-group normal_text\"> <h3><img src=\"Content/admin/img/logo.png\" alt=\"Logo\" /></h3></div>");
                    yaz.WriteLine("\t\t<div class=\"control-group\">");
                    yaz.WriteLine("\t\t\t<div class=\"controls\">");
                    yaz.WriteLine("\t\t\t\t<div class=\"main_input_box\">");
                    yaz.WriteLine("\t\t\t\t\t<span class=\"add-on bg_lg\"><i class=\"icon-user\"> </i></span><input id=\"username\" type=\"text\" formControlName=\"username\" placeholder=\"Kullanıcı Adı\" (keypress)=\"onKeyPress($event)\" />");
                    yaz.WriteLine("\t\t\t\t</div>");
                    yaz.WriteLine("\t\t\t</div>");
                    yaz.WriteLine("\t\t</div>");
                    yaz.WriteLine("\t\t<div class=\"control-group\">");
                    yaz.WriteLine("\t\t\t<div class=\"controls\">");
                    yaz.WriteLine("\t\t\t\t<div class=\"main_input_box\">");
                    yaz.WriteLine("\t\t\t\t\t<span class=\"add-on bg_ly\"><i class=\"icon-lock\"></i></span><input id=\"password\" type=\"password\" formControlName=\"password\" placeholder=\"Şifre\" (keypress)=\"onKeyPress($event)\" />");
                    yaz.WriteLine("\t\t\t\t</div>");
                    yaz.WriteLine("\t\t\t</div>");
                    yaz.WriteLine("\t\t</div>");
                    yaz.WriteLine("\t\t<div class=\"alert alert-error\" style=\"display:none;\">");
                    yaz.WriteLine("\t\t\t<strong>Hata! </strong> <span id=\"hataMesaj\">{{ hataMesaj }}</span>");
                    yaz.WriteLine("\t\t</div>");
                    yaz.WriteLine("\t\t<div class=\"form-actions\">");
                    yaz.WriteLine("\t\t\t<span class=\"pull-right\"><img id=\"imgLoading\" src=\"Content/admin/img/loading.gif\" /><input id=\"btnGiris\" type=\"submit\" class=\"btn btn-success\" value=\"Giriş Yap\" [disabled]=\"!girisForm.valid\" /></span>");
                    yaz.WriteLine("\t\t</div>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("</form>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\home\\login.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component, ViewEncapsulation } from \"@angular/core\";");
                    yaz.WriteLine("import { FormBuilder, FormGroup, Validators, FormControl } from \"@angular/forms\";");
                    yaz.WriteLine("import { Router } from '@angular/router';");
                    yaz.WriteLine("import { SharedService } from '../../services/shared.js';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\ttemplateUrl: './login.html',");
                    yaz.WriteLine("\tproviders: [SharedService],");
                    yaz.WriteLine("\tstyleUrls: [");
                    yaz.WriteLine("\t\t'../../../../../Content/admin/css/bootstrap.min.css',");
                    yaz.WriteLine("\t\t'../../../../../Content/admin/css/bootstrap-responsive.min.css',");
                    yaz.WriteLine("\t\t'../../../../../Content/admin/css/matrix-login.css',");
                    yaz.WriteLine("\t\t'../../../../../Content/admin/css/font-awesome/css/font-awesome.css'");
                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\tstyles: [");
                    yaz.WriteLine("\t\t'#imgLoading { float: left; margin: 5px 5px 0px 0px; height: 20px; display: none; }'");
                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\tencapsulation: ViewEncapsulation.None");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminLoginComponent {");
                    yaz.WriteLine("\terrorMsg: string;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tgirisForm: FormGroup;");
                    yaz.WriteLine("\tloginData: any;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\thataMesaj: string;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tconstructor(private service: SharedService, private formBuilder: FormBuilder, private router: Router) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t\tthis.girisForm = this.formBuilder.group({");
                    yaz.WriteLine("\t\t\tusername: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(25)]),");
                    yaz.WriteLine("\t\t\tpassword: new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(25)])");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tonSubmit() {");
                    yaz.WriteLine("\t\t$(\"#imgLoading\").fadeIn(\"slow\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tthis.loginData = new Object();");
                    yaz.WriteLine("\t\tthis.loginData.Username = this.girisForm.get(\"username\").value;");
                    yaz.WriteLine("\t\tthis.loginData.Password = this.girisForm.get(\"password\").value;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tthis.service.postLogin(this.loginData)");
                    yaz.WriteLine("\t\t\t.subscribe((answer) => {");
                    yaz.WriteLine("\t\t\t\tif (answer == true) {");
                    yaz.WriteLine("\t\t\t\t\tthis.router.navigate(['/Admin/Home']);");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\telse {");
                    yaz.WriteLine("\t\t\t\t\tthis.hataMesaj = \"Lütfen kullanıcı adı ve şifrenizi kontrol ediniz.\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\t\t$(\"#imgLoading\").fadeOut(\"slow\");");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\t},");
                    yaz.WriteLine("\t\t\t\tresError => this.errorMsg = resError);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tonKeyPress(event: any) {");
                    yaz.WriteLine("\t\tif (event.keyCode == \"13\") {");
                    yaz.WriteLine("\t\t\tthis.onSubmit();");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            CreateAngularAppModule();
            CreateAngularRoutingModule();
        }

        void CreateAngularSharedService()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\services\\shared.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Injectable } from \"@angular/core\";");
                    yaz.WriteLine("import { HttpClient, HttpParams } from '@angular/common/http';");
                    yaz.WriteLine("import { Observable } from 'rxjs';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Injectable()");
                    yaz.WriteLine("export class SharedService {");
                    yaz.WriteLine("\tprivate linkLogin: string = \"Ajax/Shared/Login\";");
                    yaz.WriteLine("\tprivate linkLogout: string = \"Ajax/Shared/Logout\";");
                    yaz.WriteLine("\tprivate linkLoginControl: string = \"Ajax/Shared/LoginControl\";");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tconstructor(private http: HttpClient) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpostLogin(user: any): Observable<boolean> {");
                    yaz.WriteLine("\t\treturn this.http.post<boolean>(this.linkLogin, user);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tgetLogout(): Observable<boolean> {");
                    yaz.WriteLine("\t\treturn this.http.get<boolean>(this.linkLogout);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tgetLoginControl(): Observable<boolean> {");
                    yaz.WriteLine("\t\treturn this.http.get<boolean>(this.linkLoginControl);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers\\SharedController.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using System.Web.Mvc;");
                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName + ".Areas.Ajax.Controllers");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic class SharedController : Controller");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\t[HttpPost]");
                    yaz.WriteLine("\t\tpublic JsonResult Login([System.Web.Http.FromBody] dynamic user)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tSession[\"CurrentUser\"] = user;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\treturn Json(true);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[HttpGet]");
                    yaz.WriteLine("\t\tpublic JsonResult Logout()");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tSession[\"CurrentUser\"] = null;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\treturn Json(true, JsonRequestBehavior.AllowGet);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t[HttpGet]");
                    yaz.WriteLine("\t\tpublic JsonResult LoginControl()");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tif (Session[\"CurrentUser\"] == null)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t\telse");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\treturn Json(true, JsonRequestBehavior.AllowGet);");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }
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
                    yaz.WriteLine("import { AdminLoginComponent } from './admin/views/home/login';");
                    yaz.WriteLine("import { AdminLayoutComponent } from './admin/views/shared/layoutAdmin';");
                    yaz.WriteLine("import { AdminHomeComponent } from './admin/views/home/index';");
                    yaz.WriteLine("import { AdminHeaderComponent } from './admin/views/shared/controls/header';");
                    yaz.WriteLine("import { AdminLeftMenuComponent } from './admin/views/shared/controls/leftmenu';");
                    yaz.WriteLine("import { AdminScriptsComponent } from './admin/views/shared/controls/scripts';");
                    yaz.WriteLine("import { AdminCopyDeleteComponent } from './admin/views/shared/controls/copydelete';");
                    yaz.WriteLine("import { AdminFooterComponent } from './admin/views/shared/controls/footer';");
                    yaz.WriteLine("");

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("import { Admin" + Table + "IndexComponent } from './admin/views/" + Table.ToHyperLinkText(true) + "';");
                        yaz.WriteLine("import { Admin" + Table + "EkleComponent } from './admin/views/" + Table.ToHyperLinkText(true) + "/ekle';");
                        yaz.WriteLine("import { Admin" + Table + "DuzenleComponent } from './admin/views/" + Table.ToHyperLinkText(true) + "/duzenle';");
                        yaz.WriteLine("");
                    }

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

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tAdmin" + Table + "IndexComponent,");
                        yaz.WriteLine("\t\tAdmin" + Table + "EkleComponent,");
                        yaz.WriteLine("\t\tAdmin" + Table + "DuzenleComponent,");
                    }

                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\timports: [");
                    yaz.WriteLine("\t\tBrowserModule,");
                    yaz.WriteLine("\t\tAppRoutingModule,");
                    yaz.WriteLine("\t\tReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'never' }),");
                    yaz.WriteLine("\t\tHttpClientModule");
                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\t//'/' -> '/" + projectName + "/' Bu şekilde değişecek");
                    yaz.WriteLine("\tproviders: [{ provide: APP_BASE_HREF, useValue: '/" + projectName + "/' }],");
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
                    yaz.WriteLine("import { HomeComponent } from './home/index';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { AdminLayoutComponent } from './admin/views/shared/layoutAdmin';");
                    yaz.WriteLine("import { AdminHomeComponent } from './admin/views/home/index';");
                    yaz.WriteLine("import { AdminLoginComponent } from './admin/views/home/login';");
                    yaz.WriteLine("");

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("import { Admin" + Table + "IndexComponent } from './admin/views/" + Table.ToHyperLinkText(true) + "';");
                        yaz.WriteLine("import { Admin" + Table + "EkleComponent } from './admin/views/" + Table.ToHyperLinkText(true) + "/ekle';");
                        yaz.WriteLine("import { Admin" + Table + "DuzenleComponent } from './admin/views/" + Table.ToHyperLinkText(true) + "/duzenle';");
                        yaz.WriteLine("");
                    }

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

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t{ path: 'Admin/" + Table + "', component: Admin" + Table + "IndexComponent },");
                        yaz.WriteLine("\t\t\t{ path: 'Admin/" + Table + "/Index', component: Admin" + Table + "IndexComponent },");
                        yaz.WriteLine("\t\t\t{ path: 'Admin/" + Table + "/Ekle', component: Admin" + Table + "EkleComponent },");
                        yaz.WriteLine("\t\t\t{ path: 'Admin/" + Table + "/Duzenle/:id', component: Admin" + Table + "DuzenleComponent },");
                    }

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

                CreateAngularDirectories(Table);

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Models\\" + Table + "Model.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        List<TableColumnNames> columnNames = tableColumnNames.Where(a => a.TableName == Table).ToList();

                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using System.Web.Mvc;");
                        yaz.WriteLine("");

                        yaz.WriteLine("namespace Models");
                        yaz.WriteLine("{");

                        yaz.WriteLine("\tpublic class " + Table + "Model");
                        yaz.WriteLine("\t{");

                        if (fkcList.Count > 0 || fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("\t\tpublic " + Table + "Model()");
                            yaz.WriteLine("\t\t{");
                        }

                        if (fkcList.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                            {
                                string ForeignTableName = fkc.ForeignTableName;
                                yaz.WriteLine("\t\t\t" + ForeignTableName + "List = new List<" + ForeignTableName + "Model>();");

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

                        if (fkcList.Count > 0 || fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

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


                        if (fkcList.Count > 0)
                        {
                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                            {
                                string ForeignTableName = fkc.ForeignTableName;
                                yaz.WriteLine("\t\tpublic List<" + ForeignTableName + "Model> " + ForeignTableName + "List { get; set; }");
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

                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                yaz.WriteLine("\t\tpublic string " + PrimaryTableName + "Adi { get; set; }");
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

                        if (fkcList.Count > 0)
                        {
                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                            {
                                string ForeignTableName = fkc.ForeignTableName;
                                yaz.WriteLine("import { I" + ForeignTableName + " } from './I" + ForeignTableName + "';");
                            }

                            yaz.WriteLine("");
                        }

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


                        if (fkcList.Count > 0)
                        {
                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                            {
                                string ForeignTableName = fkc.ForeignTableName;
                                yaz.WriteLine("\t" + ForeignTableName + "List : Array<I" + ForeignTableName + ">,");
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

                            yaz.WriteLine("");

                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                string PrimaryTableName = fkc.PrimaryTableName;
                                yaz.WriteLine("\t" + PrimaryTableName + "Adi : string,");
                            }
                        }

                        yaz.WriteLine("}");
                        yaz.Close();
                    }
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
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToHyperLinkText(true) + "\\index.html", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("<div id=\"content\">");
                        yaz.WriteLine("\t<div id=\"content-header\">");
                        yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a href=\"javascript:;\" class=\"tip-bottom\"><i class=\"icon-home\"></i> " + Table + "</a></div>");
                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("\t<div class=\"container-fluid\">");
                        yaz.WriteLine("\t\t<div class=\"row-fluid\">");
                        yaz.WriteLine("\t\t\t<div class=\"span12\">");
                        yaz.WriteLine("\t\t\t\t<div class=\"widget-box\">");
                        yaz.WriteLine("\t\t\t\t\t<div class=\"widget-title\">");
                        yaz.WriteLine("\t\t\t\t\t\t<span class=\"icon\"><i class=\"icon-home\"></i></span>");
                        yaz.WriteLine("\t\t\t\t\t\t<h5>" + Table + "</h5>");
                        yaz.WriteLine("\t\t\t\t\t</div>");
                        yaz.WriteLine("\t\t\t\t\t<div class=\"widget-content nopadding\">");
                        yaz.WriteLine("\t\t\t\t\t\t<table class=\"table table-bordered data-table\">");
                        yaz.WriteLine("\t\t\t\t\t\t\t<thead>");
                        yaz.WriteLine("\t\t\t\t\t\t\t\t<tr>");

                        i = 0;

                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).Take(4).ToList())
                        {
                            List<ForeignKeyChecker> frchkForeignLst2 = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                            string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                            if (frchkForeignLst2.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th" + hideColumn + ">Bağlı " + frchkForeignLst2.FirstOrDefault().PrimaryTableName + "</th>");
                            }
                            else
                            {
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th" + hideColumn + ">" + column.ColumnName + "</th>");
                            }

                            i++;
                        }

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>İşlem</th>");
                        }

                        yaz.WriteLine("\t\t\t\t\t\t\t\t</tr>");
                        yaz.WriteLine("\t\t\t\t\t\t\t</thead>");
                        yaz.WriteLine("\t\t\t\t\t\t\t<tbody>");
                        yaz.WriteLine("\t\t\t\t\t\t\t\t<tr *ngFor=\"let item of " + Table + "List\">");

                        i = 0;

                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).Take(4).ToList())
                        {
                            List<ForeignKeyChecker> frchkForeignLst2 = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                            string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                            if (column.TypeName.Name != "Boolean")
                            {
                                if (frchkForeignLst2.Count > 0)
                                {
                                    yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">{{ item?." + frchkForeignLst2.FirstOrDefault().PrimaryTableName + "Adi }}</td>");
                                }
                                else
                                {
                                    yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">{{ item?." + column.ColumnName + " }}</td>");
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + " style=\"text-align:center;\"><img *ngIf=\"item?." + column.ColumnName + "\" class=\"active\" /><img *ngIf=\"!item?." + column.ColumnName + "\" class=\"passive\" /></td>");
                            }

                            i++;
                        }


                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td style=\"text-align:center;\">");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<div class=\"btn-group\" style=\"text-align:left;\">");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<button data-toggle=\"dropdown\" class=\"btn btn-mini btn-primary dropdown-toggle\">İşlem <span class=\"caret\"></span></button>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<ul class=\"dropdown-menu\">");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"updLink\" [routerLink]=\"['/Admin/" + Table + "/Duzenle/' + item?." + id + "]\">Düzenle</a></li>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"dltLink\" data-toggle=\"modal\" href=\"#dltData\" [attr.data-id]=\"item?." + id + "\">Sil</a></li>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t</ul>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t</div>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t</td>");
                        }

                        yaz.WriteLine("\t\t\t\t\t\t\t\t</tr>");
                        yaz.WriteLine("\t\t\t\t\t\t\t</tbody>");
                        yaz.WriteLine("\t\t\t\t\t\t</table>");
                        yaz.WriteLine("\t\t\t\t\t</div>");
                        yaz.WriteLine("\t\t\t\t</div>");
                        yaz.WriteLine("\t\t\t</div>");
                        yaz.WriteLine("\t\t</div>");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t<div class=\"pagelinks\">");
                        yaz.WriteLine("\t\t\t<a routerLink=\"/Admin/" + Table + "/Ekle\" class=\"btn btn-primary btn-add\">" + Table + " Ekle</a>");
                        yaz.WriteLine("\t\t</div>");
                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("</div>");
                        yaz.WriteLine("");
                        yaz.WriteLine("<admin-copydelete></admin-copydelete>");
                        yaz.Close();
                    }
                }

                //Ekle
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToHyperLinkText(true) + "\\ekle.html", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("<div id=\"content\">");
                        yaz.WriteLine("\t<div id=\"content-header\">");
                        yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a class=\"tip-bottom\"><i class=\"icon-home\"></i> " + Table + " Ekle</a></div>");
                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("\t<div class=\"container-fluid\">");
                        yaz.WriteLine("\t\t<form [formGroup]=\"ekleForm\" (ngSubmit)=\"onSubmit()\">");
                        yaz.WriteLine("\t\t\t<fieldset>");

                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                        {
                            if (!identityColumns.Contains(column.ColumnName))
                            {
                                List<ForeignKeyChecker> frchkLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");

                                if (frchkLst.Count > 0)
                                {
                                    yaz.WriteLine("\t\t\t\t\tBağlı " + frchkLst.FirstOrDefault().PrimaryTableName);
                                    yaz.WriteLine("\t\t\t\t</div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                    yaz.WriteLine("\t\t\t\t\t<select id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\"><option *ngFor=\"let item of model?." + frchkLst.FirstOrDefault().PrimaryTableName + "List\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                }
                                else
                                {
                                    if (column.TypeName.Name == "Boolean")
                                    {
                                        yaz.WriteLine("\t\t\t\t\t" + column.ColumnName);
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                        yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"checkbox\" />");
                                    }
                                    else if (column.TypeName.Name == "Int16" ||
                                             column.TypeName.Name == "Int32" ||
                                             column.TypeName.Name == "Int64")
                                    {
                                        yaz.WriteLine("\t\t\t\t\t" + column.ColumnName);
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                        yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"number\" />");
                                    }
                                    else if (column.TypeName.Name == "String" &&
                                             column.CharLength == "")
                                    {
                                        yaz.WriteLine("\t\t\t\t\t" + column.ColumnName);
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                        yaz.WriteLine("\t\t\t\t\t<textarea id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\"></textarea>");
                                    }
                                    else
                                    {
                                        yaz.WriteLine("\t\t\t\t\t" + column.ColumnName);
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                        yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"text\" />");
                                    }
                                }

                                yaz.WriteLine("\t\t\t\t\t<br />");
                                yaz.WriteLine("\t\t\t\t\t<!-- " + column.ColumnName + " -->");

                                yaz.WriteLine("\t\t\t\t</div>");
                                yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                yaz.WriteLine("");
                            }
                        }

                        yaz.WriteLine("\t\t\t\t<p>");
                        yaz.WriteLine("\t\t\t\t\t<div class=\"alert alert-error\">");
                        yaz.WriteLine("\t\t\t\t\t\t<strong>Hata! </strong> <span class=\"alertMessage\">{{ model?.Mesaj }}</span>");
                        yaz.WriteLine("\t\t\t\t\t</div>");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t<input type=\"submit\" value=\"Kaydet\" class=\"btn btn-success btn-save\" [disabled]=\"!ekleForm.valid\" />");
                        yaz.WriteLine("\t\t\t\t\t<a routerLink=\"/Admin/" + Table + "\" class=\"btn btn-danger btn-cancel\">İptal</a>");
                        yaz.WriteLine("\t\t\t</fieldset>");
                        yaz.WriteLine("\t\t</form>");
                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("</div>");
                        yaz.Close();
                    }
                }

                //Duzenle
                if (identityColumns.Count > 0)
                {
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToHyperLinkText(true) + "\\duzenle.html", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                        {
                            yaz.WriteLine("<div id=\"content\">");
                            yaz.WriteLine("\t<div id=\"content-header\">");
                            yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a class=\"tip-bottom\"><i class=\"icon-home\"></i> " + Table + " Düzenle</a></div>");
                            yaz.WriteLine("\t</div>");
                            yaz.WriteLine("\t<div class=\"container-fluid\">");
                            yaz.WriteLine("\t\t<form [formGroup]=\"duzenleForm\" (ngSubmit)=\"onSubmit()\">");
                            yaz.WriteLine("\t\t\t<fieldset>");

                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                if (identityColumns.Contains(column.ColumnName))
                                {
                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"hidden\" value=\"{{ model?." + column.ColumnName + " }}\" />");
                                    yaz.WriteLine("\t\t\t\t</div>");
                                }
                                else
                                {
                                    List<ForeignKeyChecker> frchkLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");

                                    if (frchkLst.Count > 0)
                                    {
                                        yaz.WriteLine("\t\t\t\t\tBağlı " + frchkLst.FirstOrDefault().PrimaryTableName);
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                        yaz.WriteLine("\t\t\t\t\t<select id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\"><option *ngFor=\"let item of model?." + frchkLst.FirstOrDefault().PrimaryTableName + "List\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                    }
                                    else
                                    {
                                        if (column.TypeName.Name == "Boolean")
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + column.ColumnName);
                                            yaz.WriteLine("\t\t\t\t</div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                            yaz.WriteLine("\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"checkbox\" checked=\"checked\" *ngIf=\"model?." + column.ColumnName + "\" />");
                                            yaz.WriteLine("\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"checkbox\" *ngIf=\"!model?." + column.ColumnName + "\" />");
                                        }
                                        else if (column.TypeName.Name == "Int16" ||
                                                 column.TypeName.Name == "Int32" ||
                                                 column.TypeName.Name == "Int64")
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + column.ColumnName);
                                            yaz.WriteLine("\t\t\t\t</div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                            yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"number\" value=\"{{ model?." + column.ColumnName + " }}\" />");
                                        }
                                        else if (column.TypeName.Name == "String" &&
                                                 column.CharLength.ToLower() == "")
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + column.ColumnName);
                                            yaz.WriteLine("\t\t\t\t</div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                            yaz.WriteLine("\t\t\t\t\t<textarea id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\">{{ model?." + column.ColumnName + " }}</textarea>");
                                        }
                                        else
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + column.ColumnName);
                                            yaz.WriteLine("\t\t\t\t</div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                            yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"text\" value=\"{{ model?." + column.ColumnName + " }}\" />");
                                        }
                                    }

                                    yaz.WriteLine("\t\t\t\t\t<br />");
                                    yaz.WriteLine("\t\t\t\t\t<!-- " + column.ColumnName + " -->");

                                    yaz.WriteLine("\t\t\t\t</div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("");
                                }
                            }

                            yaz.WriteLine("\t\t\t\t<p>");
                            yaz.WriteLine("\t\t\t\t\t<div class=\"alert alert-error\">");
                            yaz.WriteLine("\t\t\t\t\t\t<strong>Hata! </strong> <span class=\"alertMessage\">{{ model?.Mesaj }}</span>");
                            yaz.WriteLine("\t\t\t\t\t</div>");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\t<input type=\"submit\" value=\"Kaydet\" class=\"btn btn-success btn-save\" [disabled]=\"!duzenleForm.valid\" />");
                            yaz.WriteLine("\t\t\t\t\t<a routerLink=\"/Admin/" + Table + "\" class=\"btn btn-danger btn-cancel\">İptal</a>");
                            yaz.WriteLine("\t\t\t</fieldset>");
                            yaz.WriteLine("\t\t</form>");

                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;

                                    List<string> identityForeignColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, ForeignTableName);
                                    string idFrgn = identityForeignColumns.Count > 0 ? identityForeignColumns.FirstOrDefault() : "id";

                                    List<TableColumnNames> foreignColumns = tableColumnNames.Where(a => a.TableName == ForeignTableName).Take(4).ToList();

                                    List<ForeignKeyChecker> fkcListForeign2 = ForeignKeyCheck(con);
                                    fkcListForeign2 = fkcListForeign2.Where(a => a.ForeignTableName == ForeignTableName).ToList();

                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t<div class=\"row-fluid\">");
                                    yaz.WriteLine("\t\t\t<div class=\"span12\">");
                                    yaz.WriteLine("\t\t\t\t<div class=\"widget-box\">");
                                    yaz.WriteLine("\t\t\t\t\t<div class=\"widget-title\">");
                                    yaz.WriteLine("\t\t\t\t\t\t<span class=\"icon\"><i class=\"icon-home\"></i></span>");
                                    yaz.WriteLine("\t\t\t\t\t\t<h5>Bağlı " + ForeignTableName + "</h5>");
                                    yaz.WriteLine("\t\t\t\t\t</div>");
                                    yaz.WriteLine("\t\t\t\t\t<div class=\"widget-content nopadding\">");
                                    yaz.WriteLine("\t\t\t\t\t\t<table class=\"table table-bordered data-table\">");
                                    yaz.WriteLine("\t\t\t\t\t\t\t<thead>");
                                    yaz.WriteLine("\t\t\t\t\t\t\t\t<tr>");

                                    i = 0;

                                    foreach (TableColumnNames item in foreignColumns)
                                    {
                                        List<ForeignKeyChecker> frchkForeignLst = fkcListForeign2.Where(a => a.ForeignColumnName == item.ColumnName).ToList();

                                        string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                                        if (frchkForeignLst.Count > 0)
                                        {
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th" + hideColumn + ">Bağlı " + frchkForeignLst.FirstOrDefault().PrimaryTableName + "</th>");
                                        }
                                        else
                                        {
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th" + hideColumn + ">" + item.ColumnName + "</th>");
                                        }

                                        i++;
                                    }

                                    if (identityForeignColumns.Count > 0)
                                    {
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>İşlem</th>");
                                    }

                                    yaz.WriteLine("\t\t\t\t\t\t\t\t</tr>");
                                    yaz.WriteLine("\t\t\t\t\t\t\t</thead>");
                                    yaz.WriteLine("\t\t\t\t\t\t\t<tbody>");
                                    yaz.WriteLine("\t\t\t\t\t\t\t\t<tr *ngFor=\"let item of model?." + ForeignTableName + "List\">");

                                    i = 0;

                                    foreach (TableColumnNames item in foreignColumns)
                                    {
                                        List<ForeignKeyChecker> frchkForeignLst = fkcListForeign2.Where(a => a.ForeignColumnName == item.ColumnName).ToList();

                                        string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                                        if (frchkForeignLst.Count > 0)
                                        {
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">{{ item?." + frchkForeignLst.FirstOrDefault().PrimaryTableName + "Adi }}</td>");
                                        }
                                        else
                                        {
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">{{ item?." + item.ColumnName + " }}</td>");
                                        }

                                        i++;
                                    }

                                    if (identityForeignColumns.Count > 0)
                                    {
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td style=\"text-align:center;\">");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<div class=\"btn-group\" style=\"text-align:left;\">");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<button data-toggle=\"dropdown\" class=\"btn btn-mini btn-primary dropdown-toggle\">İşlem <span class=\"caret\"></span></button>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<ul class=\"dropdown-menu\">");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"updLink\" [routerLink]=\"['/Admin/" + ForeignTableName + "/Duzenle/' + item?." + idFrgn + "]\">Düzenle</a></li>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"dltLink\" data-toggle=\"modal\" href=\"#dltData\" [attr.data-link]=\"" + ForeignTableName + "\" [attr.data-id]=\"item?." + idFrgn + "\">Sil</a></li>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t</ul>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t</td>");
                                    }

                                    yaz.WriteLine("\t\t\t\t\t\t\t\t</tr>");
                                    yaz.WriteLine("\t\t\t\t\t\t\t</tbody>");
                                    yaz.WriteLine("\t\t\t\t\t\t</table>");
                                    yaz.WriteLine("\t\t\t\t\t</div>");
                                    yaz.WriteLine("\t\t\t\t</div>");
                                    yaz.WriteLine("\t\t\t</div>");
                                    yaz.WriteLine("\t\t</div>");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t<div class=\"pagelinks\">");
                                    yaz.WriteLine("\t\t\t<a routerLink=\"/Admin/" + ForeignTableName + "/Ekle\" class=\"btn btn-primary btn-add\">" + ForeignTableName + " Ekle</a>");
                                    yaz.WriteLine("\t\t</div>");
                                }
                            }

                            yaz.WriteLine("\t</div>");
                            yaz.WriteLine("</div>");

                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("");
                                yaz.WriteLine("<admin-copydelete></admin-copydelete>");
                            }

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
                    CreateLib();

                    i++;
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers\\" + Table + "Controller.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System.Linq;");
                        yaz.WriteLine("using System.Web.Mvc;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using " + projectName + ".Data;");
                        yaz.WriteLine("using TDLibrary;");
                        yaz.WriteLine("using Models;");

                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + ".Areas.Ajax.Controllers");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class " + Table + "Controller : Controller");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\treadonly " + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");

                        // Index
                        string searchText = GetColumnText(tableColumnNames.Where(a => a.TableName == Table).ToList());

                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult Index()");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tList<usp_" + Table + "Select_Result> table = entity.usp_" + Table + "Select(null).ToList();");
                        yaz.WriteLine("\t\t\t");
                        yaz.WriteLine("\t\t\treturn Json(table, JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        // Ekle
                        if (identityColumns.Count > 0)
                        {
                            string columntype = tableColumnNames.Where(a => a.ColumnName == id && a.TableName == Table).FirstOrDefault().TypeName.Name.ToString();

                            if (fkcListForeign.Count > 0)
                            {
                                yaz.WriteLine("\t\t[HttpGet]");
                                yaz.WriteLine("\t\tpublic JsonResult Ekle()");
                                yaz.WriteLine("\t\t{");
                                yaz.WriteLine("\t\t\t" + Table + "Model table = new " + Table + "Model();");
                                yaz.WriteLine("");

                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;

                                    string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                    yaz.WriteLine("\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                    yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\");");
                                    yaz.WriteLine("");
                                }

                                yaz.WriteLine("\t\t\treturn Json(table, JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("\t\t}");
                                yaz.WriteLine("");
                            }

                            // Ekle
                            yaz.WriteLine("\t\t[HttpPost]");
                            yaz.WriteLine("\t\tpublic JsonResult Ekle([System.Web.Http.FromBody] " + Table + "Model table)");
                            yaz.WriteLine("\t\t{");

                            string insertSql = "var result = entity.usp_" + Table + "Insert(";
                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                if (!column.IsIdentity)
                                {
                                    insertSql += "table." + column.ColumnName + ", ";
                                }
                            }
                            insertSql = insertSql.TrimEnd(' ').TrimEnd(',');
                            insertSql += ").FirstOrDefault();";

                            yaz.WriteLine("\t\t\t" + insertSql);
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (result != null)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\treturn Json(table);");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t\telse");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\ttable.Mesaj = \"Kayıt eklenemedi.\";");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");

                            if (fkcListForeign.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;

                                    string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                    yaz.WriteLine("\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                    yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\");");
                                    yaz.WriteLine("");
                                }
                            }

                            yaz.WriteLine("\t\t\treturn Json(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Duzenle
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic JsonResult Duzenle(" + columntype.ReturnCSharpType() + " id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tusp_" + Table + "SelectTop_Result uspTable = entity.usp_" + Table + "SelectTop(id, 1).FirstOrDefault();");
                            yaz.WriteLine("\t\t\t" + Table + "Model table = uspTable.ChangeModel<" + Table + "Model>();");
                            yaz.WriteLine("");

                            if (fkcListForeign.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                    yaz.WriteLine("\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                    yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\", table." + fkc.ForeignColumnName + ");");
                                    yaz.WriteLine("");
                                }
                            }

                            yaz.WriteLine("\t\t\treturn Json(table, JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Duzenle
                            yaz.WriteLine("\t\t[HttpPost]");
                            yaz.WriteLine("\t\tpublic JsonResult Duzenle([System.Web.Http.FromBody] " + Table + "Model table)");
                            yaz.WriteLine("\t\t{");

                            string updateSql = "var result = entity.usp_" + Table + "Update(";
                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                updateSql += "table." + column.ColumnName + ", ";
                            }
                            updateSql = updateSql.TrimEnd(' ').TrimEnd(',');
                            updateSql += ").FirstOrDefault();";

                            yaz.WriteLine("\t\t\t" + updateSql);
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (result != null)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\treturn Json(table);");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t\telse");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\ttable.Mesaj = \"Kayıt düzenlenemedi.\";");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");

                            if (fkcListForeign.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.PrimaryTableName;
                                    string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());

                                    yaz.WriteLine("\t\t\tList<usp_" + PrimaryTableName + "Select_Result> table" + PrimaryTableName + " = entity.usp_" + PrimaryTableName + "Select(null).ToList();");
                                    yaz.WriteLine("\t\t\ttable." + PrimaryTableName + "List = table" + PrimaryTableName + ".ToSelectList(\"" + fkc.PrimaryColumnName + "\", \"" + columnText + "\", table." + fkc.ForeignColumnName + ");");
                                    yaz.WriteLine("");
                                }
                            }

                            yaz.WriteLine("\t\t\treturn Json(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Sil
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic JsonResult Sil(" + columntype.ReturnCSharpType() + " id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\ttry");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tentity.usp_" + Table + "Delete(id);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn Json(true, JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t\tcatch");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t}");
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }
        }

        void CreateAngularServiceLayer()
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

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\services\\" + Table.ToHyperLinkText(true) + ".ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {

                        yaz.WriteLine("import { Injectable } from \"@angular/core\";");
                        yaz.WriteLine("import { HttpClient, HttpParams } from '@angular/common/http';");
                        yaz.WriteLine("import { Observable } from 'rxjs';");
                        yaz.WriteLine("import { I" + Table + " } from '../models/I" + Table + "';");
                        yaz.WriteLine("");
                        yaz.WriteLine("@Injectable()");
                        yaz.WriteLine("export class " + Table + "Service {");
                        yaz.WriteLine("\tprivate linkIndex: string = \"Ajax/" + Table + "/Index\";");
                        yaz.WriteLine("\tprivate linkEkle: string = \"Ajax/" + Table + "/Ekle\";");
                        yaz.WriteLine("\tprivate linkDuzenle: string = \"Ajax/" + Table + "/Duzenle\";");
                        yaz.WriteLine("\tprivate linkSil: string = \"Ajax/" + Table + "/Sil\";");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tconstructor(private http: HttpClient) {");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tgetIndex(): Observable<Array<I" + Table + ">> {");
                        yaz.WriteLine("\t\treturn this.http.get<Array<I" + Table + ">>(this.linkIndex);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");

                        if (fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("\tgetEkle(): Observable<I" + Table + "> {");
                            yaz.WriteLine("\t\treturn this.http.get<I" + Table + ">(this.linkEkle);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\tpostEkle(model: any): Observable<I" + Table + "> {");
                        yaz.WriteLine("\t\treturn this.http.post<I" + Table + ">(this.linkEkle, model);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tgetDuzenle(id: string): Observable<I" + Table + "> {");
                        yaz.WriteLine("\t\tlet params = new HttpParams().set(\"id\", id);");
                        yaz.WriteLine("\t\treturn this.http.get<I" + Table + ">(this.linkDuzenle, { params: params });");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpostDuzenle(model: any): Observable<I" + Table + "> {");
                        yaz.WriteLine("\t\treturn this.http.post<I" + Table + ">(this.linkDuzenle, model);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tgetSil(id: string): Observable<boolean> {");
                        yaz.WriteLine("\t\tlet params = new HttpParams().set(\"id\", id);");
                        yaz.WriteLine("\t\treturn this.http.get<boolean>(this.linkSil, { params: params });");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }
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

                //Index
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToHyperLinkText(true) + "\\index.ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("import { Component } from \"@angular/core\";");
                        yaz.WriteLine("import { " + Table + "Service } from \"../../services/" + Table.ToHyperLinkText(true) + "\";");
                        yaz.WriteLine("import * as $ from \"jquery\";");
                        yaz.WriteLine("");
                        yaz.WriteLine("@Component({");
                        yaz.WriteLine("\ttemplateUrl: './index.html',");
                        yaz.WriteLine("\tproviders: [" + Table + "Service]");
                        yaz.WriteLine("})");
                        yaz.WriteLine("");
                        yaz.WriteLine("export class Admin" + Table + "IndexComponent {");
                        yaz.WriteLine("\terrorMsg: string;");
                        yaz.WriteLine("\t" + Table + "List: any;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tcallTable: boolean;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tconstructor(private service: " + Table + "Service) {");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tngOnInit() {");
                        yaz.WriteLine("\t\tthis.callTable = true;");
                        yaz.WriteLine("\t\tthis.FillData();");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tFillData() {");
                        yaz.WriteLine("\t\tif (this.callTable == true) {");
                        yaz.WriteLine("\t\t\tthis.service.getIndex().subscribe((resData) => {");
                        yaz.WriteLine("\t\t\t\tthis." + Table + "List = resData;");
                        yaz.WriteLine("\t\t\t\tthis.callTable = false;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tsetTimeout(() => {");
                        yaz.WriteLine("\t\t\t\t\t$(\".data-table\").dataTable({");
                        yaz.WriteLine("\t\t\t\t\t\t\"bJQueryUI\": true,");
                        yaz.WriteLine("\t\t\t\t\t\t\"sPaginationType\": \"full_numbers\",");
                        yaz.WriteLine("\t\t\t\t\t\t\"sDom\": '<\"\"l>t<\"F\"fp>'");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tif ($(\".dropdown-menu\").first().find(\"a\").length <= 0) {");
                        yaz.WriteLine("\t\t\t\t\t\t$(\".btn-group\").remove();");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t$(document).on(\"click\", \".fg-button\", () => {");
                        yaz.WriteLine("\t\t\t\t\t\tsetTimeout(() => {");
                        yaz.WriteLine("\t\t\t\t\t\t\tthis.FillData();");
                        yaz.WriteLine("\t\t\t\t\t\t}, 1);");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t$(document).on(\"click\", \"a.dltLink\", function () {");
                        yaz.WriteLine("\t\t\t\t\t\t$(this).addClass(\"active-dlt\");");
                        yaz.WriteLine("\t\t\t\t\t\t$(\"a.dlt-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t$(document).on(\"click\", \"a.dlt-yes\", () => {");
                        yaz.WriteLine("\t\t\t\t\t\tlet id: string = $(\"a.dlt-yes\").attr(\"data-id\");");
                        yaz.WriteLine("\t\t\t\t\t\tthis.onDelete(id);");
                        yaz.WriteLine("\t\t\t\t\t});");
                        yaz.WriteLine("\t\t\t\t}, 1);");
                        yaz.WriteLine("\t\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tsetTimeout(() => {");
                        yaz.WriteLine("\t\t\tif ($(\".dropdown-menu\").first().find(\"a\").length <= 0) {");
                        yaz.WriteLine("\t\t\t\t$(\".btn-group\").remove();");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}, 1);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tonDelete(id) {");
                        yaz.WriteLine("\t\tthis.service.getSil(id).subscribe((resData) => {");
                        yaz.WriteLine("\t\t\tif (resData == true) {");
                        yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"Delete\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t$(\"a.dltLink.active-dlt\").parent(\"li\").parent(\"ul\").parent(\"div\").parent(\"td\").parent(\"tr\").fadeOut(\"slow\", function () {");
                        yaz.WriteLine("\t\t\t\t\t$(this).remove();");
                        yaz.WriteLine("\t\t\t\t});");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t\telse {");
                        yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"DeleteNot\");");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tShowAlert(type: string) {");
                        yaz.WriteLine("\t\t$(\"#tdAlertMessage li.tdAlert\" + type).fadeIn(\"slow\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tsetInterval(function () {");
                        yaz.WriteLine("\t\t\t$(\"#tdAlertMessage li.tdAlert\" + type).fadeOut(\"slow\");");
                        yaz.WriteLine("\t\t}, 2000);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }

                //Ekle
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToHyperLinkText(true) + "\\ekle.ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("import { Component } from \"@angular/core\";");
                        yaz.WriteLine("import { " + Table + "Service } from \"../../services/" + Table.ToHyperLinkText(true) + "\";");
                        yaz.WriteLine("import { Router } from '@angular/router';");

                        yaz.WriteLine("import { FormBuilder, FormGroup, Validators, FormControl } from \"@angular/forms\";");

                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table && a.TypeName.Name == "String" && a.CharLength == "").ToList())
                        {
                            yaz.WriteLine("import ClassicEditor from '../../../../../Content/admin/js/ckeditor/ckeditor.js';");
                            break;
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("@Component({");
                        yaz.WriteLine("\ttemplateUrl: './ekle.html',");
                        yaz.WriteLine("\tproviders: [" + Table + "Service]");
                        yaz.WriteLine("})");
                        yaz.WriteLine("");
                        yaz.WriteLine("export class Admin" + Table + "EkleComponent {");
                        yaz.WriteLine("\terrorMsg: string;");

                        yaz.WriteLine("");
                        yaz.WriteLine("\tekleForm: FormGroup;");
                        yaz.WriteLine("\tdata: any;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tmodel: any;");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tconstructor(private service: " + Table + "Service, private formBuilder: FormBuilder, private router: Router) {");

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tngOnInit() {");

                        if (fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("\t\tthis.service.getEkle().subscribe((resData) => {");
                            yaz.WriteLine("\t\t\tthis.model = resData;");
                            yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                            yaz.WriteLine("");
                        }

                        int i = 0;
                        List<TableColumnNames> tempTableColumns = tableColumnNames.Where(a => a.TableName == Table && a.TypeName.Name == "String" && a.CharLength == "").ToList();

                        foreach (TableColumnNames column in tempTableColumns)
                        {
                            if (i == 0)
                            {
                                yaz.WriteLine("\t\tsetTimeout(function () {");
                            }

                            yaz.WriteLine("\t\t\tClassicEditor");
                            yaz.WriteLine("\t\t\t\t.create(document.querySelector('#" + column.ColumnName + "'), {");
                            yaz.WriteLine("\t\t\t\t\t//toolbar: ['bold', 'italic']");
                            yaz.WriteLine("\t\t\t\t})");
                            yaz.WriteLine("\t\t\t\t.then(editor => {");
                            yaz.WriteLine("\t\t\t\t\tconsole.log(editor);");
                            yaz.WriteLine("\t\t\t\t})");
                            yaz.WriteLine("\t\t\t\t.catch(err => {");
                            yaz.WriteLine("\t\t\t\t\tconsole.error(err.stack);");
                            yaz.WriteLine("\t\t\t\t});");
                            yaz.WriteLine("");

                            if (i == 0)
                            {
                                yaz.WriteLine("\t\t}, 1000);");
                            }

                            if (tempTableColumns.Count == i + 1)
                            {
                                yaz.WriteLine("");
                            }
                        }

                        yaz.WriteLine("\t\tthis.ekleForm = this.formBuilder.group({");

                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                        {
                            if (!column.IsIdentity)
                            {
                                if (column.TypeName.Name == "Boolean")
                                {
                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null),");
                                }
                                else
                                {
                                    if (column.IsNullable)
                                    {
                                        yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null),");
                                    }
                                    else
                                    {
                                        if (column.TypeName.Name.In(new string[] { "Int16", "Int32", "Int64" }))
                                        {
                                            yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.min(1)]),");
                                        }
                                        else if (column.TypeName.Name == "String")
                                        {
                                            if (column.TypeName.Name == "String" && column.CharLength == "")
                                            {
                                                yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.minLength(1)]),");
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(255)]),");
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tonSubmit() {");
                        yaz.WriteLine("\t\tthis.data = new Object();");

                        int tcCount = tableColumnNames.Where(a => a.TableName == Table).ToList().Count;

                        i = 0;

                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                        {
                            if (!column.IsIdentity)
                            {
                                if (column.TypeName.Name == "String" && column.CharLength == "")
                                {
                                    yaz.WriteLine("\t\tthis.data." + column.ColumnName + " = $(\".ck-content\").html().replace(\"<p>\", \"\").replace(\"</p>\", \"\");");
                                }
                                else
                                {
                                    yaz.WriteLine("\t\tthis.data." + column.ColumnName + " = this.ekleForm.get(\"" + column.ColumnName + "\").value;");
                                }
                            }

                            i++;

                            if (i == tcCount)
                            {
                                yaz.WriteLine("");
                            }
                        }

                        yaz.WriteLine("\t\tthis.service.postEkle(this.data)");
                        yaz.WriteLine("\t\t\t.subscribe((answer) => {");
                        yaz.WriteLine("\t\t\t\tif (answer.Mesaj == null) {");
                        yaz.WriteLine("\t\t\t\t\tthis.router.navigate(['/Admin/" + Table + "']);");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\telse {");
                        yaz.WriteLine("\t\t\t\t\t$(\".alertMessage\").text(answer.Mesaj);");
                        yaz.WriteLine("\t\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t},");
                        yaz.WriteLine("\t\t\t\tresError => this.errorMsg = resError);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }

                //Duzenle
                if (identityColumns.Count > 0)
                {
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToHyperLinkText(true) + "\\duzenle.ts", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                        {
                            yaz.WriteLine("import { Component } from \"@angular/core\";");
                            yaz.WriteLine("import { " + Table + "Service } from \"../../services/" + Table.ToHyperLinkText(true) + "\";");

                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.ForeignTableName;

                                    yaz.WriteLine("import { " + PrimaryTableName + "Service } from '../../services/" + PrimaryTableName.ToHyperLinkText(true) + "';");
                                }
                            }

                            yaz.WriteLine("import { ActivatedRoute, Params, Router } from '@angular/router';");
                            yaz.WriteLine("import { FormBuilder, FormGroup, Validators, FormControl } from \"@angular/forms\";");

                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table && a.TypeName.Name == "String" && a.CharLength == "").ToList())
                            {
                                yaz.WriteLine("import ClassicEditor from '../../../../../Content/admin/js/ckeditor/ckeditor.js';");
                                break;
                            }

                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("import * as $ from \"jquery\";");
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("@Component({");
                            yaz.WriteLine("\ttemplateUrl: './duzenle.html',");

                            string providers = "providers: [" + Table + "Service";
                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;

                                    providers += ", " + ForeignTableName + "Service";
                                }
                            }
                            providers += "]";

                            yaz.WriteLine("\t" + providers);
                            yaz.WriteLine("})");
                            yaz.WriteLine("");
                            yaz.WriteLine("export class Admin" + Table + "DuzenleComponent {");
                            yaz.WriteLine("\terrorMsg: string;");
                            yaz.WriteLine("\tid: string;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tduzenleForm: FormGroup;");
                            yaz.WriteLine("\tdata: any;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tmodel: any;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tcallTable: boolean;");
                            yaz.WriteLine("");

                            string constructor = "constructor(private service: " + Table + "Service, private formBuilder: FormBuilder, private router: Router, private route: ActivatedRoute";
                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    constructor += ", private service" + ForeignTableName + ": " + ForeignTableName + "Service";
                                }
                            }
                            constructor += ") {";

                            yaz.WriteLine("\t" + constructor);
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tngOnInit() {");
                            yaz.WriteLine("\t\tthis.callTable = true;");
                            yaz.WriteLine("\t\tthis.FillData();");
                            yaz.WriteLine("");

                            int i = 0;
                            List<TableColumnNames> tempTableColumns = tableColumnNames.Where(a => a.TableName == Table && a.TypeName.Name == "String" && a.CharLength == "").ToList();

                            foreach (TableColumnNames column in tempTableColumns)
                            {
                                if (i == 0)
                                {
                                    yaz.WriteLine("\t\tsetTimeout(function () {");
                                }

                                yaz.WriteLine("\t\t\tClassicEditor");
                                yaz.WriteLine("\t\t\t\t.create(document.querySelector('#" + column.ColumnName + "'), {");
                                yaz.WriteLine("\t\t\t\t\t//toolbar: ['bold', 'italic']");
                                yaz.WriteLine("\t\t\t\t})");
                                yaz.WriteLine("\t\t\t\t.then(editor => {");
                                yaz.WriteLine("\t\t\t\t\tconsole.log(editor);");
                                yaz.WriteLine("\t\t\t\t})");
                                yaz.WriteLine("\t\t\t\t.catch(err => {");
                                yaz.WriteLine("\t\t\t\t\tconsole.error(err.stack);");
                                yaz.WriteLine("\t\t\t\t});");
                                yaz.WriteLine("");

                                if (i == 0)
                                {
                                    yaz.WriteLine("\t\t}, 1000);");
                                }

                                if (tempTableColumns.Count == i + 1)
                                {
                                    yaz.WriteLine("");
                                }
                            }

                            yaz.WriteLine("\t\tthis.duzenleForm = this.formBuilder.group({");

                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                if (column.TypeName.Name == "Boolean")
                                {
                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null),");
                                }
                                else
                                {
                                    if (column.IsNullable)
                                    {
                                        yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null),");
                                    }
                                    else
                                    {
                                        if (column.TypeName.Name.In(new string[] { "Int16", "Int32", "Int64" }))
                                        {
                                            yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.min(1)]),");
                                        }
                                        else if (column.TypeName.Name == "String")
                                        {
                                            if (column.TypeName.Name == "String" && column.CharLength == "")
                                            {
                                                yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.minLength(1)]),");
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(255)]),");
                                            }
                                        }
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t});");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tFillData() {");
                            yaz.WriteLine("\t\tif (this.callTable == true) {");
                            yaz.WriteLine("\t\t\tthis.route.params.subscribe((params: Params) => {");
                            yaz.WriteLine("\t\t\t\tthis.id = params['id'];");
                            yaz.WriteLine("\t\t\t\tthis.service.getDuzenle(this.id).subscribe((resData) => {");
                            yaz.WriteLine("\t\t\t\t\tthis.model = resData;");
                            yaz.WriteLine("\t\t\t\t\tthis.callTable = false;");

                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\tsetTimeout(() => {");
                                yaz.WriteLine("\t\t\t\t\t\t$(\".data-table\").dataTable({");
                                yaz.WriteLine("\t\t\t\t\t\t\t\"bJQueryUI\": true,");
                                yaz.WriteLine("\t\t\t\t\t\t\t\"sPaginationType\": \"full_numbers\",");
                                yaz.WriteLine("\t\t\t\t\t\t\t\"sDom\": '<\"\"l>t<\"F\"fp>'");
                                yaz.WriteLine("\t\t\t\t\t\t});");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\t\tif ($(\".dropdown-menu\").first().find(\"a\").length <= 0) {");
                                yaz.WriteLine("\t\t\t\t\t\t\t$(\".btn-group\").remove();");
                                yaz.WriteLine("\t\t\t\t\t\t}");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\t\t$(document).on(\"click\", \".fg-button\", () => {");
                                yaz.WriteLine("\t\t\t\t\t\t\tsetTimeout(() => {");
                                yaz.WriteLine("\t\t\t\t\t\t\t\tthis.FillData();");
                                yaz.WriteLine("\t\t\t\t\t\t\t}, 1);");
                                yaz.WriteLine("\t\t\t\t\t\t});");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\t\t$(document).on(\"click\", \"a.dltLink\", function () {");
                                yaz.WriteLine("\t\t\t\t\t\t\t$(this).addClass(\"active-dlt\");");
                                yaz.WriteLine("\t\t\t\t\t\t\t$(\"a.dlt-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                                yaz.WriteLine("\t\t\t\t\t\t});");

                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t\t\t$(document).on(\"click\", \"a.dlt-yes\", () => {");
                                    yaz.WriteLine("\t\t\t\t\t\t\tlet id: string = $(\"a.dlt-yes\").attr(\"data-id\");");
                                    yaz.WriteLine("\t\t\t\t\t\t\tthis.on" + ForeignTableName + "Delete(id);");
                                    yaz.WriteLine("\t\t\t\t\t\t});");
                                }

                                yaz.WriteLine("\t\t\t\t\t}, 1);");
                            }

                            yaz.WriteLine("\t\t\t\t}, resError => this.errorMsg = resError);");
                            yaz.WriteLine("\t\t\t});");
                            yaz.WriteLine("\t\t}");

                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\tsetTimeout(() => {");
                                yaz.WriteLine("\t\t\tif ($(\".dropdown-menu\").first().find(\"a\").length <= 0) {");
                                yaz.WriteLine("\t\t\t\t$(\".btn-group\").remove();");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("\t\t}, 1);");
                            }

                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tonSubmit() {");
                            yaz.WriteLine("\t\tthis.data = new Object();");

                            int tcCount = tableColumnNames.Where(a => a.TableName == Table).ToList().Count;

                            i = 0;

                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                if (column.TypeName.Name == "String" && column.CharLength == "")
                                {
                                    yaz.WriteLine("\t\tthis.data." + column.ColumnName + " = $(\".ck-content\").html().replace(\"<p>\", \"\").replace(\"</p>\", \"\");");
                                }
                                else
                                {
                                    yaz.WriteLine("\t\tthis.data." + column.ColumnName + " = this.duzenleForm.get(\"" + column.ColumnName + "\").value;");
                                }
                            }

                            i++;

                            if (i == tcCount - 1)
                            {
                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\t\tthis.service.postDuzenle(this.data)");
                            yaz.WriteLine("\t\t\t.subscribe((answer) => {");
                            yaz.WriteLine("\t\t\t\tif (answer.Mesaj == null) {");
                            yaz.WriteLine("\t\t\t\t\tthis.router.navigate(['/Admin/" + Table + "']);");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("\t\t\t\telse {");
                            yaz.WriteLine("\t\t\t\t\t$(\".alertMessage\").text(answer.Mesaj);");
                            yaz.WriteLine("\t\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("\t\t\t},");
                            yaz.WriteLine("\t\t\t\tresError => this.errorMsg = resError);");
                            yaz.WriteLine("\t}");

                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;

                                    yaz.WriteLine("");
                                    yaz.WriteLine("\ton" + ForeignTableName + "Delete(id) {");
                                    yaz.WriteLine("\t\tthis.service" + ForeignTableName + ".getSil(id).subscribe((resData) => {");
                                    yaz.WriteLine("\t\t\tif (resData == true) {");
                                    yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"Delete\");");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t$(\"a.dltLink.active-dlt\").parent(\"li\").parent(\"ul\").parent(\"div\").parent(\"td\").parent(\"tr\").fadeOut(\"slow\", function () {");
                                    yaz.WriteLine("\t\t\t\t\t$(this).remove();");
                                    yaz.WriteLine("\t\t\t\t});");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t\telse {");
                                    yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"DeleteNot\");");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                                    yaz.WriteLine("\t}");
                                }

                                yaz.WriteLine("");
                                yaz.WriteLine("\tShowAlert(type: string) {");
                                yaz.WriteLine("\t\t$(\"#tdAlertMessage li.tdAlert\" + type).fadeIn(\"slow\");");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\tsetInterval(function () {");
                                yaz.WriteLine("\t\t\t$(\"#tdAlertMessage li.tdAlert\" + type).fadeOut(\"slow\");");
                                yaz.WriteLine("\t\t}, 2000);");
                                yaz.WriteLine("\t}");
                            }

                            yaz.WriteLine("}");
                            yaz.Close();
                        }
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
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<base href=\"/" + projectName + "/\" />");
                    yaz.WriteLine("</head>");
                    yaz.WriteLine("<body>");
                    yaz.WriteLine("\t@RenderBody()");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<input id=\"hdnUrl\" type=\"hidden\" value=\"@Urling.FullURL\" />");
                    yaz.WriteLine("");
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
                    yaz.WriteLine("<" + projectName.Substring(0, 3).ToHyperLinkText(true) + "-app></" + projectName.Substring(0, 3).ToHyperLinkText(true) + "-app>");
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

        void CreateReadMe()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\readme.txt", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("---------------------");
                    yaz.WriteLine("-- Angular Kurulum --");
                    yaz.WriteLine("---------------------");
                    yaz.WriteLine("");
                    yaz.WriteLine("- Önce projenin ana dizininde cmd'yi çalıştırıp \"ng new ngClient --routing\" diyip node_modules'ü indireceksin.");
                    yaz.WriteLine("Yukarıdaki komut yerine \"ng new ngClient --routing --skip-install\" dersen node_modules klasörü hariç dosyaları indirir. (Hızlı)");
                    yaz.WriteLine("- Bunu yapınca seçeneklerden CSS'i seçeceksin ve uzunca kurulumu bekleyeceksin.");
                    yaz.WriteLine("- İlgili dosyalar ngClient klasörüne kopyalandıktan sonra aşağıdaki dosyaları Kes/Yapıştır ile ana dizine atacaksın;");
                    yaz.WriteLine("\tnode_modules (klasör)");
                    yaz.WriteLine("\tsrc (klasör)");
                    yaz.WriteLine("\tangular.json");
                    yaz.WriteLine("\tpackage.json");
                    yaz.WriteLine("\ttsconfig.app.json");
                    yaz.WriteLine("\ttsconfig.json");
                    yaz.WriteLine("\ttslint.json");
                    yaz.WriteLine("- Visual Studio içinde \"Show All files\" diyip \"node_modules\" klasörü hariç bu dosyalar projeye dahil edeceksin.");
                    yaz.WriteLine("- \"npm install --save rxjs-compat\" diyerek rxjs tipini yükleyeceksin.");
                    yaz.WriteLine("- \"npm install --save @types/jquery\" diyerek jquery tipini yükleyeceksin.");
                    yaz.WriteLine("- \"npm install --save @types/jest\" diyerek jest tipini yükleyeceksin.");
                    yaz.WriteLine("- tsconfig.json dosyası içine \"types\": [ \"jquery\", \"jest\" ] tanımlamasını gireceksin.");
                    yaz.WriteLine("- Views\\Home\\Index.cshtml içine <app-root></app-root> tagini ekleyeceksin. (Başka bir şey olmasın)");
                    yaz.WriteLine("- Scripts klasörü içine libs adında klasör açacaksın. Scripts yerine kendi scriptlerini sakladığın klasör içine açacaksın.");
                    yaz.WriteLine("- angular.json içinde \"options\": { \"outputPath\": \"Content/js/libs\", şeklinde libs yolunu belirteceksin.");
                    yaz.WriteLine("- app.module.ts içinde \"import * as $ from \"jquery\";\" tanımlamasını yapacaksın.");
                    yaz.WriteLine("- libs klasörü içindeki *.js dosyalarını _Scripts partial view'inde aşağıdaki şekilde çağıracaksın;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<script type=\"text/javascript\" src=\"/Content/js/libs/runtime-es2015.js\"></script>");
                    yaz.WriteLine("\t<script type=\"text/javascript\" src=\"/Content/js/libs/polyfills-es2015.js\"></script>");
                    yaz.WriteLine("\t<script type=\"text/javascript\" src=\"/Content/js/libs/styles-es2015.js\"></script>");
                    yaz.WriteLine("\t<script type=\"text/javascript\" src=\"/Content/js/libs/vendor-es2015.js\"></script>");
                    yaz.WriteLine("\t<script type=\"text/javascript\" src=\"/Content/js/libs/main-es2015.js\"></script>");
                    yaz.WriteLine("");
                    yaz.WriteLine("  Bu kısımda dikkat etmen gereken bu scriptler <app-root></app-root> taginin altında yer almalı.");
                    yaz.WriteLine("");
                    yaz.WriteLine("- Aşağıdaki kodu RouteConfig.cs dosyasına Default olarak ekleyebilirsin;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\troutes.MapRoute(");
                    yaz.WriteLine("\t\tname: \"Default\",");
                    yaz.WriteLine("\t\turl: \"{*anything}\",");
                    yaz.WriteLine("\t\t// url: \"{controller}/{action}/{id}\",");
                    yaz.WriteLine("\t\tdefaults: new { controller = \"Home\", action = \"Index\", id = UrlParameter.Optional }");
                    yaz.WriteLine("\t);");
                    yaz.WriteLine("");
                    yaz.WriteLine("- Proje ana dizininde cmd çalıştırıp \"ng build\" komutunu çalıştıracaksın. Bu komutu projede yaptığın her değişiklik sonrası çalıştıracaksın. Yoksa değişiklikler");
                    yaz.WriteLine("çalışmaz. cmd arkada çalışır durumda kalsın ikide bir açmak zorunda kalma.");
                    yaz.WriteLine("- Projeye yeni component eklediğinde bunu routing ve app.module.ts içinde declarations kısmında belirtmelisin.");
                    yaz.WriteLine("");
                    yaz.WriteLine("- \"ng build --prod\" diyerek publish olacak şekilde libs klasörü içini hazırlarsın. (daha küçük boyutta oluyor)");
                    yaz.WriteLine("");
                    yaz.WriteLine("Not: package.json içinde değişiklik yapıp Restore Package deme asla. Nasıl yüklendiyse o çalışsın.");
                    yaz.WriteLine("");
                    yaz.WriteLine("");
                    yaz.WriteLine("-------------");
                    yaz.WriteLine("-- Hatalar --");
                    yaz.WriteLine("-------------");
                    yaz.WriteLine("");
                    yaz.WriteLine("Hata 1 : The type or namespace name 'Http' does not exist in the namespace 'System.Web'");
                    yaz.WriteLine("Çözüm 1 : Package Manager Console üzerinden \"Install-Package Microsoft.AspNet.WebApi.Core\" kodunu çalıştır.");

                    yaz.Close();
                }
            }
        }

        void CreateWebConfig()
        {
            string wcKullanici = String.IsNullOrEmpty(txtWCKullanici.Text) ? "user" : txtWCKullanici.Text;
            string wcSifre = String.IsNullOrEmpty(txtWCSifre.Text) ? "123456" : txtWCSifre.Text;

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Web.config.txt", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("<add key=\"SystemUser\" value=\"admin\" />");
                    yaz.WriteLine("<add key=\"MainPath\" value=\"http://localhost/" + projectName + "\" />");
                    yaz.WriteLine("<add key=\"ScriptPath\" value=\"/Content/js\" />");
                    yaz.WriteLine("<add key=\"StylePath\" value=\"/Content/css\" />");
                    yaz.WriteLine("<add key=\"ImagePath\" value=\"/Content/img\" />");
                    yaz.WriteLine("<add key=\"AjaxPath\" value=\"/Ajax\" />");
                    yaz.WriteLine("<add key=\"AdminPath\" value=\"http://localhost/" + projectName + "/Admin\" />");
                    yaz.WriteLine("<add key=\"AdminScriptPath\" value=\"/Content/admin/js\" />");
                    yaz.WriteLine("<add key=\"AdminStylePath\" value=\"/Content/admin/css\" />");
                    yaz.WriteLine("<add key=\"AdminImagePath\" value=\"/Content/admin/img\" />");
                    yaz.WriteLine("<add key=\"AdminAjaxPath\" value=\"/Ajax/Ajax\" />");
                    yaz.WriteLine("<add key=\"UploadPath\" value=\"/Uploads\" />");
                    yaz.WriteLine("<add key=\"MaxFileSize\" value=\"1024000\" />");
                    yaz.WriteLine("<add key=\"MaxPictureSize\" value=\"1024000\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("<system.webServer>");
                    yaz.WriteLine("\t<validation validateIntegratedModeConfiguration=\"false\"/>");
                    yaz.WriteLine("\t<modules runAllManagedModulesForAllRequests=\"true\"/>");
                    yaz.WriteLine("</system.webServer>");

                    if (chkMVCWcfServis.Checked || chkMVCHepsi.Checked)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("<system.serviceModel>");
                        yaz.WriteLine("\t<behaviors>");
                        yaz.WriteLine("\t\t<serviceBehaviors>");
                        yaz.WriteLine("\t\t\t<behavior name=\"\">");
                        yaz.WriteLine("\t\t\t\t<serviceMetadata httpGetEnabled=\"true\" />");
                        yaz.WriteLine("\t\t\t\t<serviceDebug includeExceptionDetailInFaults=\"false\" />");
                        yaz.WriteLine("\t\t\t</behavior>");
                        yaz.WriteLine("\t\t</serviceBehaviors>");
                        yaz.WriteLine("\t</behaviors>");
                        yaz.WriteLine("\t<serviceHostingEnvironment multipleSiteBindingsEnabled=\"true\" minFreeMemoryPercentageToActivateService=\"0\" />");
                        yaz.WriteLine("\t<services>");

                        foreach (string table in selectedTables)
                        {
                            yaz.WriteLine("\t\t<service name=\"" + projectName + ".Service." + table + "Service\">");
                            yaz.WriteLine("\t\t\t<endpoint kind=\"webHttpEndpoint\" contract=\"" + projectName + ".Service.I" + table + "Service\" />");
                            yaz.WriteLine("\t\t</service>");
                        }

                        yaz.WriteLine("\t</services>");
                        yaz.WriteLine("</system.serviceModel>");
                    }

                    yaz.Close();
                }
            }
        }

        void CreateWcfService()
        {
            CreateDirectories();

            foreach (string Table in selectedTables)
            {
                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                List<TableColumnNames> columnNames = tableColumnNames.Where(a => a.TableName == Table).ToList();

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Service\\I" + Table + "Service.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using System.Runtime.Serialization;");
                        yaz.WriteLine("using System.ServiceModel;");
                        yaz.WriteLine("using System.ServiceModel.Web;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + ".Service");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\t[ServiceContract]");
                        yaz.WriteLine("\tpublic interface I" + Table + "Service");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\t[OperationContract]");
                        yaz.WriteLine("\t\t[WebInvoke(UriTemplate = \"/Select/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                        yaz.WriteLine("\t\tList<" + Table + "Data> Select(int? top);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[OperationContract]");
                        yaz.WriteLine("\t\t[WebInvoke(UriTemplate = \"/Insert/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                        yaz.WriteLine("\t\tbool Insert(" + Table + "Data " + Table + "Data);");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebInvoke(UriTemplate = \"/Update/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\tbool Update(" + Table + "Data " + Table + "Data);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t[OperationContract]");
                            yaz.WriteLine("\t\t[WebInvoke(UriTemplate = \"/Delete/\", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]");
                            yaz.WriteLine("\t\tbool Delete(int? id);");
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
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Service\\" + Table + "Service.svc", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("<%@ ServiceHost Language=\"C#\" Debug=\"true\" Service=\"" + projectName + ".Service." + Table + "Service\" CodeBehind=\"" + Table + "Service.svc.cs\" %>");

                        yaz.Close();
                    }
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Service\\" + Table + "Service.svc.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        string columns = "";
                        string idcolumns = "";

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                if (!column.IsIdentity)
                                    columns += Table + "Data." + column.ColumnName + ", ";
                                else
                                    idcolumns = Table + "Data." + column.ColumnName + ", ";
                            }
                        }

                        columns = columns.TrimEnd(' ').TrimEnd(',');

                        yaz.WriteLine("using System.Linq;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using " + projectName + ".Data;");
                        yaz.WriteLine("using TDLibrary;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + ".Service");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class " + Table + "Service : I" + Table + "Service");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t\tpublic List<" + Table + "Data> Select(int? top)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tList<" + Table + "Data> table;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (top > 0)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\ttable = entity.usp_" + Table + "SelectTop(null, (int)top).ToList().ChangeModelList<" + Table + "Data, usp_" + Table + "SelectTop_Result>();");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\ttable = entity.usp_" + Table + "Select(null).ToList().ChangeModelList<" + Table + "Data, usp_" + Table + "Select_Result>();");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn table;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t\tpublic bool Insert(" + Table + "Data " + Table + "Data)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tif (" + Table + "Data != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tvar result = entity.usp_" + Table + "Insert(" + columns + ").FirstOrDefault();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tif (result != null)");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\treturn true;");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn false;");
                        yaz.WriteLine("\t\t}");

                        if (identityColumns.Count > 0)
                        {
                            //Update
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tpublic bool Update(" + Table + "Data " + Table + "Data)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (" + Table + "Data != null)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tvar result = entity.usp_" + Table + "Update(" + idcolumns + columns + ").FirstOrDefault();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\tif (result != null)");
                            yaz.WriteLine("\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\treturn true;");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn false;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tpublic bool Delete(int? id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\ttry");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tentity.usp_" + Table + "Delete(id);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn true;");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t\tcatch");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\treturn false;");
                            yaz.WriteLine("\t\t\t}");
                        }

                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }
        }

        void CreateLib()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Lib\\ExtMethods.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using System.Collections.Generic;");
                    yaz.WriteLine("using System.Linq;");
                    yaz.WriteLine("using System.Web.Mvc;");
                    yaz.WriteLine("");
                    yaz.WriteLine("namespace RentACar");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic static class ExtMethods");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpublic static List<SelectListItem> ToSelectList<T>(this List<T> itemList, string value, string text, int? selectedItem = null, bool addEmpty = false, string emptyText = \"-\", string emptyValue = \"0\")");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tList<SelectListItem> list = new List<SelectListItem>();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tif (addEmpty)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tlist.Add(new SelectListItem()");
                    yaz.WriteLine("\t\t\t\t{");
                    yaz.WriteLine("\t\t\t\t\tText = emptyText,");
                    yaz.WriteLine("\t\t\t\t\tValue = emptyValue");
                    yaz.WriteLine("\t\t\t\t});");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tforeach (var item in itemList)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tstring _value = item.GetType().GetProperties().Where(a => a.Name == value).FirstOrDefault().GetValue(item).ToString();");
                    yaz.WriteLine("\t\t\t\tstring _text = item.GetType().GetProperties().Where(a => a.Name == text).FirstOrDefault().GetValue(item).ToString();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tif (selectedItem != null)");
                    yaz.WriteLine("\t\t\t\t{");
                    yaz.WriteLine("\t\t\t\t\tif (_value == selectedItem.ToString())");
                    yaz.WriteLine("\t\t\t\t\t\tlist.Add(new SelectListItem() { Value = _value.ToString(), Text = _text, Selected = true });");
                    yaz.WriteLine("\t\t\t\t\telse");
                    yaz.WriteLine("\t\t\t\t\t\tlist.Add(new SelectListItem() { Value = _value.ToString(), Text = _text });");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\telse");
                    yaz.WriteLine("\t\t\t\t{");
                    yaz.WriteLine("\t\t\t\t\tlist.Add(new SelectListItem() { Value = _value.ToString(), Text = _text });");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\treturn list;");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
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

        void CreateStylelScript()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\js\\pathscript.js", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
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

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\pathscript.js", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
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

            CopyFromResource(StringToByteArray(Properties.Resources.Content_js_main_js), PathAddress + "\\" + projectFolder + "\\Content\\js\\main.js");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_js_main_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\main.js");

            CreateJquery();

            if (chkAngular.Checked)
            {
                CreateAngularStylelScripts();
                CreateImages();
            }

            CopyFromResource(StringToByteArray(Properties.Resources.Content_css_main_css), PathAddress + "\\" + projectFolder + "\\Content\\css\\main.css");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_css_main_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\main.css");
        }

        void CreateJquery()
        {
            CopyFromResource(StringToByteArray(Properties.Resources.Content_js_jquery_jquery_min_js), PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery\\jquery.min.js");

            if (!chkAngular.Checked)
            {
                CopyFromResource(StringToByteArray(Properties.Resources.Content_js_jquery_json2_js), PathAddress + "\\" + projectFolder + "\\Content\\js\\jquery\\json2.js");
            }
        }

        void CreateImages()
        {
            CopyFromResource(BitmapToByteArray(Properties.Resources.Content_admin_img_active_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\active.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Content_admin_img_breadcrumb_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\breadcrumb.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Content_admin_img_glyphicons_halflings_white_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\glyphicons-halflings-white.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Content_admin_img_line_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\line.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Content_admin_img_logo_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\logo.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Content_admin_img_menu_active_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\menu-active.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Content_admin_img_passive_png), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\passive.png");
            CopyFromResource(BitmapToByteArray(Properties.Resources.Content_admin_img_loading_gif), PathAddress + "\\" + projectName + "\\Content\\admin\\img\\loading.gif");
        }

        void CreateAngularStylelScripts()
        {
            CreateCKEditor();

            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_js_bootstrap_min_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\bootstrap.min.js");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_js_jquery_dataTables_min_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\jquery.dataTables.min.js");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_js_matrix_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\matrix.js");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_css_bootstrap_min_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\bootstrap.min.css");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_css_bootstrap_responsive_min_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\bootstrap-responsive.min.css");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_css_matrix_login_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\matrix-login.css");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_css_matrix_media_css), PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\matrix-media.css");

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\matrix-style.css", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("*{outline:0!important;-moz-outline:none!important}body,html{height:100%;margin-top:-5px!important}body{overflow-x:hidden;margin-top:-10px;font-family:'Open Sans',sans-serif;font-size:12px;color:#666}a{color:#666}a:focus,a:hover{text-decoration:none;color:#28b779}tr.odd{background-color:#fff}.dropdown-menu .divider{margin:4px 0}.dropdown-menu{min-width:65px}.dropdown-menu>li>a{padding:3px 10px;color:#666;font-size:12px}.dropdown-menu>li>a i{padding-right:3px}.userphoto img{width:19px;height:19px}.alert{display:none}.alert,.btn,.btn-group>.btn:first-child,.btn-group>.btn:last-child,.btn-group>.dropdown-toggle,.dropdown-menu,.label,.progress,.table-bordered,.uneditable-input,.well,input[type=color],input[type=date],input[type=datetime-local],input[type=datetime],input[type=email],input[type=month],input[type=number],input[type=password],input[type=search],input[type=tel],input[type=text],input[type=time],input[type=url],input[type=week],select,textarea{border-radius:0}.btn,.uneditable-input,input[type=color],input[type=date],input[type=datetime-local],input[type=datetime],input[type=email],input[type=month],input[type=number],input[type=password],input[type=search],input[type=tel],input[type=text],input[type=time],input[type=url],input[type=week],textarea{box-shadow:none}.btn,.btn-primary,.progress,.progress .bar-danger,.progress .bar-info,.progress .bar-success,.progress .bar-warning,.progress-danger .bar,.progress-info .bar,.progress-success .bar,.progress-warning .bar{background-image:none}.accordion-heading h5{width:70%}.form-horizontal .form-actions{padding-left:20px}#footer{padding:10px;text-align:center}hr{border-top-color:#dadada}.carousel{margin-bottom:0}.fl{float:left}.fr{float:right}.badge-important,.label-important{background:#f74d4d}.bg_lb{background:#27a9e3}.bg_db{background:#2295c9}.bg_lg{background:#28b779}.bg_dg{background:#28b779}.bg_ly{background:#ffb848}.bg_dy{background:#da9628}.bg_ls{background:#2255a4}.bg_lo{background:#da542e}.bg_lr{background:#f74d4d}.bg_lv{background:#603bbc}.bg_lh{background:#b6b3b3}#header{height:77px;position:relative;width:100%;z-index:-9}#header h1{height:67px;left:0;overflow:hidden;position:relative;top:5px;width:211px;margin:0}#header h1 a{display:block;text-align:center}#header h1 a img{height:67px;vertical-align:top}#search{position:absolute;z-index:25;top:6px;right:10px}#search input[type=text]{padding:4px 10px 5px;border:0;width:100px}#search button{border:0;margin-left:-3px;margin-top:-11px;padding:5px 10px 4px}#search button i{opacity:.8;color:#fff}#search button:active i,#search button:hover i{opacity:1}#user-nav{position:absolute;left:220px;top:0;z-index:20;margin:0}#user-nav>ul{margin:0;padding:0;list-style:none;border-right:1px solid #2e363f;border-left:1px solid #000}#user-nav>ul>li{float:left;list-style-type:none;margin:0;position:relative;padding:0;border-left:1px solid #2e363f;border-right:1px solid #000}#user-nav>ul>li>a{padding:9px 10px;display:block;font-size:11px;cursor:pointer}#user-nav>ul>li.open>a,#user-nav>ul>li>a:hover{color:#fff;background:#000}#sidebar li a i,#user-nav>ul>li>a>i{opacity:.5;margin-top:2px}#user-nav>ul>li.open>a>i,#user-nav>ul>li>a:hover>i{opacity:1}#user-nav>ul>li>a>.label{vertical-align:middle;padding:1px 4px 1px;margin:-2px 4px 0;display:inline-block}#user-nav>ul ul>li>a{text-align:left}#sidebar{display:block;float:left;position:relative;width:220px;z-index:16}#sidebar>ul{list-style:none;margin:0 0 0;padding:0;position:absolute;width:220px}#sidebar>ul>li{display:block;position:relative}#sidebar>ul>li>a{padding:10px 0 10px 15px;display:block;color:#939da8}#sidebar>ul>li>a>i{margin-right:10px}#sidebar>ul>li.active>a{background:url(/" + projectName + "/Content/admin/img/menu-active.png) no-repeat scroll right center transparent!important;text-decoration:none}#sidebar>ul>li>a>.label{margin:0 20px 0 0;float:right;padding:3px 5px 2px}#sidebar>ul li ul{display:none;margin:0;padding:0}#sidebar>ul li.open ul{display:block}#sidebar>ul li ul li a{padding:10px 0 10px 25px;display:block;color:#777}#sidebar>ul li ul li:first-child a{border-top:0}#sidebar>ul li ul li:last-child a{border-bottom:0}#content{background:none repeat scroll 0 0 #eee;margin-left:220px;margin-right:0;padding-bottom:25px;position:relative;min-height:100%;width:auto}#content-header{position:abslute;width:100%;margin-top:-38px;z-index:20}#content-header h1{color:#555;font-size:28px;font-weight:400;float:none;text-shadow:0 1px 0 #fff;margin-left:20px;position:relative}#content-header .btn-group{float:right;right:20px;position:absolute}#content-header .btn-group,#content-header h1{margin-top:20px}#content-header .btn-group .btn{padding:11px 14px 9px}#content-header .btn-group .btn .label{position:absolute;top:-7px}.container-fluid .row-fluid:first-child{margin-top:20px}#breadcrumb{background-color:#fff;border-bottom:1px solid #e3ebed;text-align:center}#breadcrumb a{padding:8px 20px 8px 10px;display:inline-block;background-image:url(/../img/breadcrumb.png);background-position:center right;background-repeat:no-repeat;font-size:16px;font-weight:700;color:#2255a4;cursor:pointer}#breadcrumb a:hover{color:#333}#breadcrumb a:last-child{background-image:none}#breadcrumb a.current{font-weight:700;color:#444}#breadcrumb a i{margin-right:5px;opacity:.6}#breadcrumb a:hover i{margin-right:5px;opacity:.8}.todo ul{list-style:none outside none;margin:0}.todo li{border-bottom:1px solid #ebebeb;margin-bottom:0;padding:10px 0}.todo li:hover{background:none repeat scroll 0 0 #fcfcfc;border-color:#d9d9d9}.todo li:last-child{border-bottom:0}.todo li .txt{float:left}.todo li .by{margin-left:10px;margin-right:10px}.todo li .date{margin-right:10px}.quick-actions_homepage{width:100%;text-align:center;position:relative;float:left;margin-top:10px}.quick-actions,.quick-actions-horizontal,.stat-boxes,.stats-plain{display:block;list-style:none outside none;margin:15px 0;text-align:center}.stat-boxes2{display:inline-block;list-style:none outside none;margin:0;text-align:center}.stat-boxes2 li{display:inline-block;line-height:18px;margin:0 10px 10px;padding:0 10px;background:#fff;border:1px solid #dcdcdc}.stat-boxes2 li:hover{background:#f6f6f6}.stat-boxes .right,.stat-boxes2 .left{text-shadow:0 1px 0 #fff;float:left}.stat-boxes2 .left{border-right:1px solid #dcdcdc;box-shadow:1px 0 0 0 #fff;font-size:10px;font-weight:700;margin-right:10px;padding:10px 14px 6px 4px}.stat-boxes2 .right{color:#666;font-size:12px;padding:9px 10px 7px 0;text-align:center;min-width:70px;float:left}.stat-boxes2 .left span,.stat-boxes2 .right strong{display:block}.stat-boxes2 .right strong{font-size:26px;margin-bottom:3px;margin-top:6px}.quick-actions_homepage .quick-actions li{position:relative}.quick-actions_homepage .quick-actions li .label{position:absolute;padding:5px;top:-10px;right:-5px}.stats-plain{width:100%}.quick-actions li,.quick-actions-horizontal li,.stat-boxes li{float:left;line-height:18px;margin:0 10px 10px 0;padding:0 10px}.quick-actions li a:hover,.quick-actions li:hover,.quick-actions-horizontal li a:hover,.quick-actions-horizontal li:hover,.stat-boxes li a:hover,.stat-boxes li:hover{background:#2e363f}.quick-actions li{height:124px;width:210px}.quick-actions_homepage .quick-actions .span3{width:30%}.quick-actions li,.quick-actions-horizontal li{padding:0}.stats-plain li{padding:0 30px;display:inline-block;margin:0 10px 20px}.quick-actions li a{padding:20px 10px}.stats-plain li h4{font-size:40px;margin-bottom:15px}.stats-plain li span{font-size:14px;color:#fff}.quick-actions-horizontal li a span{padding:10px 12px 10px 10px;display:inline-block}.quick-actions li a,.quick-actions-horizontal li a{display:block;color:#fff;font-size:14px;font-weight:lighter}.quick-actions li a i[class*=\" icon-\"],.quick-actions li a i[class^=icon-]{font-size:60px;display:block;margin:0 auto 5px}.quick-actions-horizontal li a i[class*=\" icon-\"],.quick-actions-horizontal li a i[class^=icon-]{background-repeat:no-repeat;background-attachment:scroll;background-position:center;background-color:transparent;width:16px;height:16px;display:inline-block;margin:-2px 0 0!important;border-right:1px solid #ddd;margin-right:10px;padding:10px;vertical-align:middle}.quick-actions li:active,.quick-actions-horizontal li:active{background-image:-webkit-gradient(linear,0 0,0 100%,from(#eee),to(#f4f4f4));background-image:-webkit-linear-gradient(top,#eee 0,#f4f4f4 100%);background-image:-moz-linear-gradient(top,#eee 0,#f4f4f4 100%);background-image:-ms-linear-gradient(top,#eee 0,#f4f4f4 100%);background-image:-o-linear-gradient(top,#eee 0,#f4f4f4 100%);background-image:linear-gradient(top,#eee 0,#f4f4f4 100%);box-shadow:0 1px 4px 0 rgba(0,0,0,.2) inset,0 1px 0 rgba(255,255,255,.4)}.stat-boxes .left,.stat-boxes .right{text-shadow:0 1px 0 #fff;float:left}.stat-boxes .left{border-right:1px solid #dcdcdc;box-shadow:1px 0 0 0 #fff;font-size:10px;font-weight:700;margin-right:10px;padding:10px 14px 6px 4px}.stat-boxes .right{color:#666;font-size:12px;padding:9px 10px 7px 0;text-align:center;min-width:70px}.stat-boxes .left span,.stat-boxes .right strong{display:block}.stat-boxes .right strong{font-size:26px;margin-bottom:3px;margin-top:6px},.stat-boxes .peity_bar_good,.stat-boxes .peity_line_good{color:#459d1c}.stat-boxes .peity_bar_neutral,.stat-boxes .peity_line_neutral{color:#757575}.stat-boxes .peity_bar_bad,.stat-boxes .peity_line_bad{color:#ba1e20}.site-stats{margin:0;padding:0;text-align:center;list-style:none}.site-stats li{cursor:pointer;display:inline-block;margin:0 5px 10px;text-align:center;width:42%;padding:10px 0;color:#fff;position:relative}.site-stats li i{font-size:24px;clear:both}.site-stats li:hover{background:#2e363f}.site-stats li i{vertical-align:baseline}.site-stats li strong{font-weight:700;font-size:20px;width:100%;float:left;margin-left:0}.site-stats li small{margin-left:0;font-size:11px;width:100%;float:left}#donut,#interactive,#placeholder,#placeholder2,.bars,.chart,.pie{height:300px;max-width:100%}#choices{border-top:1px solid #dcdcdc;margin:10px 0;padding:10px}#choices br{display:none}#choices input{margin:-5px 5px 0 0}#choices label{display:inline-block;padding-right:20px}#tooltip{position:absolute;display:none;border:none;padding:3px 8px;border-radius:3px;font-size:10px;background-color:#222;color:#fff;z-index:25}.widget-box{background:none repeat scroll 0 0 #f9f9f9;border-left:1px solid #cdcdcd;border-top:1px solid #cdcdcd;border-right:1px solid #cdcdcd;clear:both;margin-top:16px;margin-bottom:16px;position:relative}.widget-box.widget-calendar,.widget-box.widget-chat{overflow:hidden!important}.accordion .widget-box{margin-top:-2px;margin-bottom:0;border-radius:0}.widget-box.widget-plain{background:0 0;border:none;margin-top:0;margin-bottom:0}.modal-header,.table th,.widget-title,div.dataTables_wrapper .ui-widget-header{background:#efefef;border-bottom:1px solid #cdcdcd;height:36px}.widget-title .nav-tabs{border-bottom:0 none}.widget-title .nav-tabs li a{border-bottom:medium none!important;border-left:1px solid #ddd;border-radius:0;border-right:1px solid #ddd;border-top:medium none;color:#999;margin:0;outline:medium none;padding:9px 10px 8px;font-weight:700;text-shadow:0 1px 0 #fff}.widget-title .nav-tabs li:first-child a{border-left:medium none!important}.widget-title .nav-tabs li a:hover{background-color:transparent!important;border-color:#d6d6d6;border-width:0 1px;color:#2b2b2b}.widget-title .nav-tabs li.active a{background-color:#f9f9f9!important;color:#444}.widget-title span.icon{padding:9px 10px 7px 11px;float:left;border-right:1px solid #dadada}.widget-title h5{color:#666;float:left;font-size:12px;font-weight:700;padding:12px;line-height:12px;margin:0}.widget-title .buttons{float:right;margin:8px 10px 0 0}.widget-title .label{padding:3px 5px 2px;float:right;margin:9px 11px 0 0;box-shadow:0 1px 2px rgba(0,0,0,.3) inset,0 1px 0 #fff}.widget-calendar .widget-title .label{margin-right:190px}.widget-content{padding:15px;border-bottom:1px solid #cdcdcd}.widget-box.widget-plain .widget-content{padding:12px 0 0}.widget-box.collapsible .collapse.in .widget-content{border-bottom:1px solid #cdcdcd}.recent-comments,.recent-posts,.recent-users{margin:0;padding:0}.article-post li,.recent-comments li,.recent-posts li,.recent-users li{border-bottom:1px dotted #aebdc8;list-style:none outside none;padding:10px}.recent-comments li.viewall,.recent-posts li.viewall,.recent-users li.viewall{padding:0}.recent-comments li.viewall a,.recent-posts li.viewall a,.recent-users li.viewall a{padding:5px;text-align:center;display:block;color:#888}.recent-comments li.viewall a:hover,.recent-posts li.viewall a:hover,.recent-users li.viewall a:hover{background-color:#eee}.recent-comments li:last-child,.recent-posts li:last-child,.recent-users li:last-child{border-bottom:none!important}.user-thumb{background:none repeat scroll 0 0 #fff;float:left;height:40px;margin-right:10px;margin-top:5px;padding:2px;width:40px}.user-info{color:#666;font-size:11px}.invoice-content{padding:20px}.invoice-action{margin-bottom:30px}.invoice-head{clear:both;margin-bottom:40px;overflow:hidden;width:auto}.invoice-meta{font-size:18px;margin-bottom:40px}.invoice-date{float:right;font-size:80%}.invoice-content h5{color:#333;font-size:16px;font-weight:400;margin-bottom:10px}.invoice-content ul{list-style:none;margin:0;padding:0}.invoice-to{float:left;width:370px}.invoice-from{float:right;width:300px}.invoice-from li,.invoice-to li{clear:left}.invoice-from li span,.invoice-to li span{display:block}.invoice-content th.total-label{text-align:right}.invoice-content th.total-amount{text-align:left}.amount-word{color:#666;margin-bottom:40px;margin-top:40px}.amount-word span{color:#5476a6;font-weight:700;padding-left:20px}.panel-left{margin-top:103px}.panel-left2{margin-left:176px}.panel-right{width:100%;background-color:#fff;border-bottom:1px solid #ddd;position:absolute;right:0;overflow:auto;top:38px;height:76px}.panel-right2{width:100%;background-color:#fff;border-right:1px solid #ddd;position:absolute;left:0;overflow:auto;top:0;height:94%;width:175px}.panel-right .panel-title,.panel-right2 .panel-title{width:100%;background-color:#ececec;border-bottom:1px solid #ddd}.panel-right .panel-title h5,.panel-right2 .panel-title h5{font-size:12px;color:#777;text-shadow:0 1px 0 #fff;padding:6px 10px 5px;margin:0}.panel-right .panel-content{padding:10px}.chat-content{height:470px;padding:15px}.chat-messages{height:420px;overflow:auto;position:relative}.chat-message{padding:7px 15px;margin:7px 0 0}.chat-message input[type=text]{margin-bottom:0!important;width:100%}.chat-message .input-box{display:block;margin-right:90px}.chat-message button{float:right}#chat-messages-inner p{padding:0;margin:10px 0 0 0}#chat-messages-inner p img{display:inline-block;float:left;vertical-align:middle;width:28px;height:28px;margin-top:-1px;margin-right:10px}#chat-messages-inner .msg-block,#chat-messages-inner p.offline span{background:none repeat scroll 0 0 #fff;border:1px solid #ccc;box-shadow:1px 1px 0 1px rgba(0,0,0,.05);display:block;margin-left:0;padding:10px;position:relative}#chat-messages-inner p.offline span{background:none repeat scroll 0 0 #fff5f5}#chat-messages-inner .time{color:#999;font-size:11px;float:right}#chat-messages-inner .msg{display:block;margin-top:13px;border-top:1px solid #dadada}#chat-messages-inner .msg-block:before{border-right:7px solid rgba(0,0,0,.1);border-top:7px solid transparent;border-bottom:7px solid transparent;content:\"\";display:none;left:-7px;position:absolute;top:11px}#chat-messages-inner .msg-block:after{border-right:6px solid #fff;border-top:6px solid transparent;border-bottom:6px solid transparent;content:\"\";display:none;left:-6px;position:absolute;top:12px}.chat-users{padding:0 0 30px}.chat-users .contact-list{line-height:21px;list-style:none outside none;margin:0;padding:0;font-size:10px}.chat-users .contact-list li{border:1px solid #dadada;margin:5px 5px;padding:1px;position:relative}.chat-users .contact-list li:hover{background-color:#efefef}.chat-users .contact-list li a{color:#666;display:block;padding:8px 5px}.chat-users .contact-list li.online a{font-weight:700}.chat-users .contact-list li.new{background-color:#eaeaea}.chat-users .contact-list li.offline{background-color:#ede0e0}.chat-users .contact-list li a img{display:inline-block;margin-right:10px;vertical-align:middle;width:28px;height:28px}.chat-users .contact-list li .msg-count{padding:3px 5px;position:absolute;right:10px;top:12px}.taskDesc i{margin:1px 5px 0}.taskOptions,.taskStatus{text-align:center!important}.taskStatus .in-progress{color:#64909e}.taskStatus .pending{color:#ac6363}.taskStatus .done{color:#75b468}.activity-list{list-style:none outside none;margin:0}.activity-list li{border-bottom:1px solid #eee;display:block}.activity-list li:last-child{border-bottom:medium none}.activity-list li a{display:block;padding:7px 10px}.activity-list li a:hover{background-color:#fbfbfb}.activity-list li a span{color:#aaa;font-size:11px;font-style:italic}.activity-list li a i{margin-right:10px;opacity:.6;vertical-align:middle}.new-update{border-top:1px solid #ddd;padding:10px 12px}.new-update:first-child{border-top:medium none}.new-update span{display:block}.new-update i{float:left;margin-top:3px;margin-right:13px}.new-update .update-date{color:#bbb;float:right;margin:4px -2px 0 0;text-align:center;width:30px}.new-update .update-date .update-day{display:block;font-size:20px;font-weight:700;margin-bottom:-4px}.update-alert,.update-done,.update-notice{display:block;float:left;max-width:76%}tr:hover{background:#f6f6f6}span.icon .checker{margin-top:-5px;margin-right:0}.dataTables_length{color:#878787;margin:7px 5px 0;position:relative;left:5px;width:50%;top:-2px}.dataTables_length div{vertical-align:middle}.dataTables_paginate{line-height:16px;text-align:right;margin-top:5px;margin-right:10px}.dataTables_paginate{line-height:16px;text-align:right;margin-top:5px;margin-right:10px}.dataTables_paginate .ui-button,.pagination.alternate li a{font-size:12px;padding:4px 10px!important;border-style:solid;border-width:1px;border-color:#ddd #ddd #ccc;border-color:rgba(0,0,0,.1) rgba(0,0,0,.1) rgba(0,0,0,.25);display:inline-block;line-height:16px;background:#f5f5f5;color:#333;text-shadow:0 1px 0 #fff}.dataTables_paginate .ui-button:hover,.pagination.alternate li a:hover{background:#e8e8e8;color:#222;text-shadow:0 1px 0 #fff;cursor:pointer}.dataTables_paginate .first{border-radius:4px 0 0 4px}.dataTables_paginate .last{border-radius:0 4px 4px 0}.dataTables_paginate .ui-state-disabled,.fc-state-disabled,.pagination.alternate li.disabled a{color:#aaa!important}.dataTables_paginate .ui-state-disabled:hover,.fc-state-disabled:hover,.pagination.alternate li.disabled a:hover{background:#f5f5f5;cursor:default!important}.dataTables_paginate span .ui-state-disabled,.pagination.alternate li.active a{background:#41bedd!important;color:#fff!important;cursor:default!important}div.dataTables_wrapper .ui-widget-header{border-right:medium none;border-top:1px solid #d5d5d5;font-weight:400;margin-top:-1px}.dataTables_wrapper .ui-toolbar{padding:5px}.dataTables_filter{color:#878787;font-size:11px;right:0;top:37px;margin:4px 8px 2px 10px;position:absolute;text-align:left}.dataTables_filter input{margin-bottom:0}.table th{height:auto;font-size:10px;padding:5px 10px 2px;border-bottom:0;text-align:center;color:#666}.table.with-check tr td:first-child,.table.with-check tr th:first-child{width:10px}.table.with-check tr th:first-child i{margin-top:-2px;opacity:.6}.table.with-check tr td:first-child .checker{margin-right:0}.table tr.checked td{background-color:#ffffe3!important}.nopadding{padding:0!important}.nopadding .table{margin-bottom:0}.nopadding .table-bordered{border:0}.thumbnails{margin-left:-2.12766%!important}.thumbnails [class*=span]{margin-left:2.12766%!important;position:relative}.thumbnails .actions{width:auto;height:16px;background-color:#000;padding:4px 8px 8px 8px;position:absolute;bottom:0;left:50%;margin-top:-13px;margin-left:-24px;opacity:0;top:10%;transition:1s ease-out;-moz-transition:opacity .3s ease-in-out}.thumbnails li:hover .actions,.thumbnails li:hover .actions a:hover{opacity:1;color:#fff;top:50%;transition:1s ease-out}.line{background:url(/../img/line.png) repeat-x scroll 0 0 transparent;display:block;height:8px}.modal{z-index:99999!important}.modal-backdrop{z-index:999!important}.modal-header{height:auto;padding:8px 15px 5px}.modal-header h3{font-size:12px;text-shadow:0 1px 0 #fff}.notify-ui ul{list-style:none;margin:0;padding:0}.notify-ui li{background:#eee;margin-bottom:5px;padding:5px 10px;text-align:center;border:1px solid #ddd}.notify-ui li:hover{cursor:pointer;color:#777}form{margin-bottom:0}.form-horizontal .control-group{border-top:1px solid #fff;border-bottom:1px solid #eee;margin-bottom:0}.form-horizontal .control-group:last-child{border-bottom:0}.form-horizontal .control-label{padding-top:15px;width:180px}.form-horizontal .controls{margin-left:200px;padding:10px 0}.row-fluid .span20{width:97.8%}.form-horizontal .form-actions{margin-top:0;margin-bottom:0}.help-block,.help-inline{color:#999}#lightbox{position:fixed;top:0;left:0;width:100%;height:100%;background:url(/overlay.png) repeat #000;text-align:center;z-index:9999}#lightbox p{position:absolute;top:10px;right:10px;width:22px;height:22px;cursor:pointer;z-index:22;border:1px solid #fff;border-radius:100%;padding:2px;text-align:center;transition:.5s}#lightbox p:hover{transform:rotate(180deg)}#imgbox{position:absolute;left:0;top:0;width:100%;height:100%;background:url(/overlay.png) repeat #000;text-align:center;z-index:21}#imgbox img{margin-top:100px;border:10px solid #fff}.error_ex{text-align:center}.error_ex h1{font-size:140px;font-weight:700;padding:50px 0;color:#28b779}#sidebar .content{padding:10px;position:relative;color:#939da8}#sidebar .percent{font-weight:700;position:absolute;right:10px;top:25px}#sidebar .progress{margin-bottom:2px;margin-top:2px;width:70%}#sidebar .progress-mini{height:6px}#sidebar .stat{font-size:11px}.btn-icon-pg ul{margin:0;padding:0}.btn-icon-pg ul li{margin:5px;padding:5px;list-style:none;display:inline-block;border:1px solid #dadada;min-width:187px;cursor:pointer}.btn-icon-pg ul li:hover i{transition:.3s;-moz-transition:.3s;-webkit-transition:.3s;-o-transition:.3s;margin-left:8px}.accordion{margin-top:16px}.fix_hgt{height:115px;overflow-x:auto}.input-append .add-on:last-child,.input-append .btn:last-child{border-radius:0;padding:6px 5px 2px}.input-append input,.input-append input[class*=span],.input-prepend input,.input-prepend input[class*=span]{width:none}.input-append input,.input-append select,.input-prepend input,.input-prepend span{border-radius:0!important}.bs-docs-tooltip-examples{list-style:none outside none;margin:0 0 10px;position:relative;text-align:center}.bs-docs-tooltip-examples li{display:inline;padding:0 10px;list-style:none;position:relative}@media (max-width:480px){#header{height:115px}#header h1{top:10px;left:5px;margin:3px auto}#user-nav{position:relative;left:auto;right:auto;width:100%;margin-top:-31px;border-top:1px solid #363e48;margin-bottom:0;background:#2e363f;float:right}.navbar>.nav{float:none}#my_menu{display:none}#my_menu_input{display:block}#user-nav>ul{right:0;margin-left:20%!important;margin-top:0;width:100%;background:#000;position:relative}#user-nav>ul>li{padding:0 0}#user-nav>ul>li>a{padding:5px 10px}#sidebar .content{display:none}#content{margin-left:0!important;border-top-left-radius:0;margin-top:0}#content-header{margin-top:0;text-align:center}#content-header .btn-group,#content-header h1{float:none}#content-header h1{display:block;text-align:center;margin-left:auto;margin-top:0;padding-top:15px;width:100%}#content-header .btn-group{margin-top:70px;margin-bottom:0;margin-right:0;left:30%}#sidebar{float:none;width:100%!important;display:block;position:relative;top:0}#sidebar>ul{margin:0;padding:0;width:100%;display:block;z-index:999;position:relative}#sidebar>ul>li{list-style-type:none;display:block;border-top:1px solid #41bedd;float:none!important;margin:0;position:relative;padding:2px 10px;cursor:pointer}#sidebar>ul>li:hover ul{display:none}#sidebar>ul>li:hover{background-color:#27a9e3}#sidebar>ul>li:hover a{background:0 0}#sidebar>ul li ul{margin:0;padding:0;top:35px;left:0;z-index:999;display:none;position:absolute;width:100%;min-width:100%;border-radius:none}#sidebar>ul li ul li{list-style-type:none;margin:0;font-size:12px;line-height:30px}#sidebar>ul li ul li a{display:block;padding:5px 10px;color:#fff;text-decoration:none;font-weight:700}#sidebar>ul li ul li:hover a{border-radius:0}#sidebar>ul li span{cursor:pointer;margin:0 2px 0 5px;font-weight:700;color:#fff;font-size:12px}#sidebar>ul li a i{background-image:url(/../img/glyphicons-halflings-white.png);margin-top:4px;vertical-align:top}#sidebar>a{padding:9px 20px 9px 15px;display:block!important;color:#eee;float:none!important;font-size:12px;font-weight:700}#sidebar>ul>li>a{padding:5px;display:block;color:#aaa}.widget-title .buttons>.btn{width:11px;white-space:nowrap;overflow:hidden}.form-horizontal .control-label{padding-left:30px}.form-horizontal .controls{margin-left:0;padding:10px 30px}.form-actions{text-align:center}.panel-right2{width:100%;background-color:#fff;border-right:1px solid #ddd;position:relative;left:0;overflow:auto;top:0;height:87%;width:100%}.panel-left2{margin-left:0}.dataTables_paginate .ui-button,.pagination.alternate li a{padding:4px 4px!important}.table th{padding:5px 4px 2px}}@media (min-width:481px) and (max-width:970px){body{background:#49cced}#header h1{top:5px;left:5px;width:35px}#header h1 a img{height:30px}#search{top:5px}#my_menu{display:none}#my_menu_input{display:block}#content{margin-top:0}#sidebar>ul>li{float:none}#sidebar>ul>li:hover ul{display:block}#sidebar,#sidebar>ul{width:43px;display:block;position:absolute;height:100%;z-index:1}#sidebar>ul ul{display:none;position:absolute;left:43px;top:0;min-width:150px;list-style:none}#sidebar>ul ul li a{white-space:nowrap;padding:10px 25px}#sidebar>ul ul:before{border-top:7px solid transparent;border-bottom:7px solid transparent;content:\"\";display:inline-block;left:-6px;position:absolute;top:11px}#sidebar>ul ul:after{content:\"\";display:inline-block;left:-5px;position:absolute;top:12px}#sidebar>a{display:none!important}#sidebar>ul>li.open.submenu>a{border-bottom:none!important}#sidebar>ul>li>a>span{display:none}#content{margin-left:43px}#sidebar .content{display:none}}@media (max-width:600px){.widget-title .buttons{float:left}.panel-left{margin-right:0}.panel-right{border-top:1px solid #ddd;border-left:none;position:relative;top:auto;right:auto;height:auto;width:auto}#sidebar .content{display:none}}@media (max-width:767px){body{padding:0!important}.container-fluid{padding-left:20px;padding-right:20px}#search{display:none}#user-nav>ul>li>a>span.text{display:none}#sidebar .content{display:none}}@media (min-width:768px) and (max-width:979px){.row-fluid [class*=span],[class*=span]{display:block;float:none;margin-left:0;width:auto}}@media (max-width:979px){div.dataTables_wrapper .ui-widget-header{height:68px}.dataTables_filter{position:relative;top:0}.dataTables_length{width:100%;text-align:center}.dataTables_filter,.dataTables_paginate{text-align:center}#sidebar .content{display:none}}");
                    yaz.Close();
                }
            }

            CreateFontAwesome();
        }

        void CreateFontAwesome()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\css\\font-awesome.css", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("@font-face{font-family:FontAwesome;src:url(/" + projectName + "/Content/admin/css/font-awesome/fontawesome-webfont.eot);src:url(/" + projectName + "/Content/admin/css/font-awesome/fontawesome-webfont.eot?#iefix) format('embedded-opentype'),url(/" + projectName + "/Content/admin/css/font-awesome/fontawesome-webfont.woff) format('woff'),url(/" + projectName + "/Content/admin/css/font-awesome/fontawesome-webfont.ttf) format('truetype');font-weight:400;font-style:normal}[class*=\" icon-\"],[class^=icon-]{font-family:FontAwesome;font-weight:400;font-style:normal;text-decoration:inherit;display:inline;width:auto;height:auto;line-height:normal;vertical-align:baseline;background-image:none!important;background-position:0 0;background-repeat:repeat}[class*=\" icon-\"]:before,[class^=icon-]:before{text-decoration:inherit;display:inline-block;speak:none}a [class*=\" icon-\"],a [class^=icon-]{display:inline-block}.icon-large:before{vertical-align:-10%;font-size:1.3333333333333333em}.btn [class*=\" icon-\"],.btn [class^=icon-],.nav [class*=\" icon-\"],.nav [class^=icon-]{display:inline;line-height:.6em}.btn [class*=\" icon-\"].icon-spin,.btn [class^=icon-].icon-spin,.nav [class*=\" icon-\"].icon-spin,.nav [class^=icon-].icon-spin{display:inline-block}li [class*=\" icon-\"],li [class^=icon-]{display:inline-block;width:1.25em;text-align:center}li [class*=\" icon-\"].icon-large,li [class^=icon-].icon-large{width:1.5625em}ul.icons{list-style-type:none;text-indent:-.75em}ul.icons li [class*=\" icon-\"],ul.icons li [class^=icon-]{width:.75em}.icon-muted{color:#eee}.icon-border{border:solid 1px #eee;padding:.2em .25em .15em;-webkit-border-radius:3px;-moz-border-radius:3px;border-radius:3px}.icon-2x{font-size:2em}.icon-2x.icon-border{border-width:2px;-webkit-border-radius:4px;-moz-border-radius:4px;border-radius:4px}.icon-3x{font-size:3em}.icon-3x.icon-border{border-width:3px;-webkit-border-radius:5px;-moz-border-radius:5px;border-radius:5px}.icon-4x{font-size:4em}.icon-4x.icon-border{border-width:4px;-webkit-border-radius:6px;-moz-border-radius:6px;border-radius:6px}.pull-right{float:right}.pull-left{float:left}[class*=\" icon-\"].pull-left,[class^=icon-].pull-left{margin-right:.35em}[class*=\" icon-\"].pull-right,[class^=icon-].pull-right{margin-left:.35em}.btn [class*=\" icon-\"].pull-left.icon-2x,.btn [class*=\" icon-\"].pull-right.icon-2x,.btn [class^=icon-].pull-left.icon-2x,.btn [class^=icon-].pull-right.icon-2x{margin-top:.35em}.btn [class*=\" icon-\"].icon-spin.icon-large,.btn [class^=icon-].icon-spin.icon-large{height:.75em}.btn.btn-small [class*=\" icon-\"].pull-left.icon-2x,.btn.btn-small [class*=\" icon-\"].pull-right.icon-2x,.btn.btn-small [class^=icon-].pull-left.icon-2x,.btn.btn-small [class^=icon-].pull-right.icon-2x{margin-top:.45em}.btn.btn-large [class*=\" icon-\"].pull-left.icon-2x,.btn.btn-large [class*=\" icon-\"].pull-right.icon-2x,.btn.btn-large [class^=icon-].pull-left.icon-2x,.btn.btn-large [class^=icon-].pull-right.icon-2x{margin-top:.2em}.icon-spin{display:inline-block;-moz-animation:spin 2s infinite linear;-o-animation:spin 2s infinite linear;-webkit-animation:spin 2s infinite linear;animation:spin 2s infinite linear}@-moz-keyframes spin{0%{-moz-transform:rotate(0)}100%{-moz-transform:rotate(359deg)}}@-webkit-keyframes spin{0%{-webkit-transform:rotate(0)}100%{-webkit-transform:rotate(359deg)}}@-o-keyframes spin{0%{-o-transform:rotate(0)}100%{-o-transform:rotate(359deg)}}@-ms-keyframes spin{0%{-ms-transform:rotate(0)}100%{-ms-transform:rotate(359deg)}}@keyframes spin{0%{transform:rotate(0)}100%{transform:rotate(359deg)}}.icon-glass:before{content:\"\\f000\"}.icon-music:before{content:\"\\f001\"}.icon-search:before{content:\"\\f002\"}.icon-envelope:before{content:\"\\f003\"}.icon-heart:before{content:\"\\f004\"}.icon-star:before{content:\"\\f005\"}.icon-star-empty:before{content:\"\\f006\"}.icon-user:before{content:\"\\f007\"}.icon-film:before{content:\"\\f008\"}.icon-th-large:before{content:\"\\f009\"}.icon-th:before{content:\"\\f00a\"}.icon-th-list:before{content:\"\\f00b\"}.icon-ok:before{content:\"\\f00c\"}.icon-remove:before{content:\"\\f00d\"}.icon-zoom-in:before{content:\"\\f00e\"}.icon-zoom-out:before{content:\"\\f010\"}.icon-off:before{content:\"\\f011\"}.icon-signal:before{content:\"\\f012\"}.icon-cog:before{content:\"\\f013\"}.icon-trash:before{content:\"\\f014\"}.icon-home:before{content:\"\\f015\"}.icon-file:before{content:\"\\f016\"}.icon-time:before{content:\"\\f017\"}.icon-road:before{content:\"\\f018\"}.icon-download-alt:before{content:\"\\f019\"}.icon-download:before{content:\"\\f01a\"}.icon-upload:before{content:\"\\f01b\"}.icon-inbox:before{content:\"\\f01c\"}.icon-play-circle:before{content:\"\\f01d\"}.icon-repeat:before{content:\"\\f01e\"}.icon-refresh:before{content:\"\\f021\"}.icon-list-alt:before{content:\"\\f022\"}.icon-lock:before{content:\"\\f023\"}.icon-flag:before{content:\"\\f024\"}.icon-headphones:before{content:\"\\f025\"}.icon-volume-off:before{content:\"\\f026\"}.icon-volume-down:before{content:\"\\f027\"}.icon-volume-up:before{content:\"\\f028\"}.icon-qrcode:before{content:\"\\f029\"}.icon-barcode:before{content:\"\\f02a\"}.icon-tag:before{content:\"\\f02b\"}.icon-tags:before{content:\"\\f02c\"}.icon-book:before{content:\"\\f02d\"}.icon-bookmark:before{content:\"\\f02e\"}.icon-print:before{content:\"\\f02f\"}.icon-camera:before{content:\"\\f030\"}.icon-font:before{content:\"\\f031\"}.icon-bold:before{content:\"\\f032\"}.icon-italic:before{content:\"\\f033\"}.icon-text-height:before{content:\"\\f034\"}.icon-text-width:before{content:\"\\f035\"}.icon-align-left:before{content:\"\\f036\"}.icon-align-center:before{content:\"\\f037\"}.icon-align-right:before{content:\"\\f038\"}.icon-align-justify:before{content:\"\\f039\"}.icon-list:before{content:\"\\f03a\"}.icon-indent-left:before{content:\"\\f03b\"}.icon-indent-right:before{content:\"\\f03c\"}.icon-facetime-video:before{content:\"\\f03d\"}.icon-picture:before{content:\"\\f03e\"}.icon-pencil:before{content:\"\\f040\"}.icon-map-marker:before{content:\"\\f041\"}.icon-adjust:before{content:\"\\f042\"}.icon-tint:before{content:\"\\f043\"}.icon-edit:before{content:\"\\f044\"}.icon-share:before{content:\"\\f045\"}.icon-check:before{content:\"\\f046\"}.icon-move:before{content:\"\\f047\"}.icon-step-backward:before{content:\"\\f048\"}.icon-fast-backward:before{content:\"\\f049\"}.icon-backward:before{content:\"\\f04a\"}.icon-play:before{content:\"\\f04b\"}.icon-pause:before{content:\"\\f04c\"}.icon-stop:before{content:\"\\f04d\"}.icon-forward:before{content:\"\\f04e\"}.icon-fast-forward:before{content:\"\\f050\"}.icon-step-forward:before{content:\"\\f051\"}.icon-eject:before{content:\"\\f052\"}.icon-chevron-left:before{content:\"\\f053\"}.icon-chevron-right:before{content:\"\\f054\"}.icon-plus-sign:before{content:\"\\f055\"}.icon-minus-sign:before{content:\"\\f056\"}.icon-remove-sign:before{content:\"\\f057\"}.icon-ok-sign:before{content:\"\\f058\"}.icon-question-sign:before{content:\"\\f059\"}.icon-info-sign:before{content:\"\\f05a\"}.icon-screenshot:before{content:\"\\f05b\"}.icon-remove-circle:before{content:\"\\f05c\"}.icon-ok-circle:before{content:\"\\f05d\"}.icon-ban-circle:before{content:\"\\f05e\"}.icon-arrow-left:before{content:\"\\f060\"}.icon-arrow-right:before{content:\"\\f061\"}.icon-arrow-up:before{content:\"\\f062\"}.icon-arrow-down:before{content:\"\\f063\"}.icon-share-alt:before{content:\"\\f064\"}.icon-resize-full:before{content:\"\\f065\"}.icon-resize-small:before{content:\"\\f066\"}.icon-plus:before{content:\"\\f067\"}.icon-minus:before{content:\"\\f068\"}.icon-asterisk:before{content:\"\\f069\"}.icon-exclamation-sign:before{content:\"\\f06a\"}.icon-gift:before{content:\"\\f06b\"}.icon-leaf:before{content:\"\\f06c\"}.icon-fire:before{content:\"\\f06d\"}.icon-eye-open:before{content:\"\\f06e\"}.icon-eye-close:before{content:\"\\f070\"}.icon-warning-sign:before{content:\"\\f071\"}.icon-plane:before{content:\"\\f072\"}.icon-calendar:before{content:\"\\f073\"}.icon-random:before{content:\"\\f074\"}.icon-comment:before{content:\"\\f075\"}.icon-magnet:before{content:\"\\f076\"}.icon-chevron-up:before{content:\"\\f077\"}.icon-chevron-down:before{content:\"\\f078\"}.icon-retweet:before{content:\"\\f079\"}.icon-shopping-cart:before{content:\"\\f07a\"}.icon-folder-close:before{content:\"\\f07b\"}.icon-folder-open:before{content:\"\\f07c\"}.icon-resize-vertical:before{content:\"\\f07d\"}.icon-resize-horizontal:before{content:\"\\f07e\"}.icon-bar-chart:before{content:\"\\f080\"}.icon-twitter-sign:before{content:\"\\f081\"}.icon-facebook-sign:before{content:\"\\f082\"}.icon-camera-retro:before{content:\"\\f083\"}.icon-key:before{content:\"\\f084\"}.icon-cogs:before{content:\"\\f085\"}.icon-comments:before{content:\"\\f086\"}.icon-thumbs-up:before{content:\"\\f087\"}.icon-thumbs-down:before{content:\"\\f088\"}.icon-star-half:before{content:\"\\f089\"}.icon-heart-empty:before{content:\"\\f08a\"}.icon-signout:before{content:\"\\f08b\"}.icon-linkedin-sign:before{content:\"\\f08c\"}.icon-pushpin:before{content:\"\\f08d\"}.icon-external-link:before{content:\"\\f08e\"}.icon-signin:before{content:\"\\f090\"}.icon-trophy:before{content:\"\\f091\"}.icon-github-sign:before{content:\"\\f092\"}.icon-upload-alt:before{content:\"\\f093\"}.icon-lemon:before{content:\"\\f094\"}.icon-phone:before{content:\"\\f095\"}.icon-check-empty:before{content:\"\\f096\"}.icon-bookmark-empty:before{content:\"\\f097\"}.icon-phone-sign:before{content:\"\\f098\"}.icon-twitter:before{content:\"\\f099\"}.icon-facebook:before{content:\"\\f09a\"}.icon-github:before{content:\"\\f09b\"}.icon-unlock:before{content:\"\\f09c\"}.icon-credit-card:before{content:\"\\f09d\"}.icon-rss:before{content:\"\\f09e\"}.icon-hdd:before{content:\"\\f0a0\"}.icon-bullhorn:before{content:\"\\f0a1\"}.icon-bell:before{content:\"\\f0a2\"}.icon-certificate:before{content:\"\\f0a3\"}.icon-hand-right:before{content:\"\\f0a4\"}.icon-hand-left:before{content:\"\\f0a5\"}.icon-hand-up:before{content:\"\\f0a6\"}.icon-hand-down:before{content:\"\\f0a7\"}.icon-circle-arrow-left:before{content:\"\\f0a8\"}.icon-circle-arrow-right:before{content:\"\\f0a9\"}.icon-circle-arrow-up:before{content:\"\\f0aa\"}.icon-circle-arrow-down:before{content:\"\\f0ab\"}.icon-globe:before{content:\"\\f0ac\"}.icon-wrench:before{content:\"\\f0ad\"}.icon-tasks:before{content:\"\\f0ae\"}.icon-filter:before{content:\"\\f0b0\"}.icon-briefcase:before{content:\"\\f0b1\"}.icon-fullscreen:before{content:\"\\f0b2\"}.icon-group:before{content:\"\\f0c0\"}.icon-link:before{content:\"\\f0c1\"}.icon-cloud:before{content:\"\\f0c2\"}.icon-beaker:before{content:\"\\f0c3\"}.icon-cut:before{content:\"\\f0c4\"}.icon-copy:before{content:\"\\f0c5\"}.icon-paper-clip:before{content:\"\\f0c6\"}.icon-save:before{content:\"\\f0c7\"}.icon-sign-blank:before{content:\"\\f0c8\"}.icon-reorder:before{content:\"\\f0c9\"}.icon-list-ul:before{content:\"\\f0ca\"}.icon-list-ol:before{content:\"\\f0cb\"}.icon-strikethrough:before{content:\"\\f0cc\"}.icon-underline:before{content:\"\\f0cd\"}.icon-table:before{content:\"\\f0ce\"}.icon-magic:before{content:\"\\f0d0\"}.icon-truck:before{content:\"\\f0d1\"}.icon-pinterest:before{content:\"\\f0d2\"}.icon-pinterest-sign:before{content:\"\\f0d3\"}.icon-google-plus-sign:before{content:\"\\f0d4\"}.icon-google-plus:before{content:\"\\f0d5\"}.icon-money:before{content:\"\\f0d6\"}.icon-caret-down:before{content:\"\\f0d7\"}.icon-caret-up:before{content:\"\\f0d8\"}.icon-caret-left:before{content:\"\\f0d9\"}.icon-caret-right:before{content:\"\\f0da\"}.icon-columns:before{content:\"\\f0db\"}.icon-sort:before{content:\"\\f0dc\"}.icon-sort-down:before{content:\"\\f0dd\"}.icon-sort-up:before{content:\"\\f0de\"}.icon-envelope-alt:before{content:\"\\f0e0\"}.icon-linkedin:before{content:\"\\f0e1\"}.icon-undo:before{content:\"\\f0e2\"}.icon-legal:before{content:\"\\f0e3\"}.icon-dashboard:before{content:\"\\f0e4\"}.icon-comment-alt:before{content:\"\\f0e5\"}.icon-comments-alt:before{content:\"\\f0e6\"}.icon-bolt:before{content:\"\\f0e7\"}.icon-sitemap:before{content:\"\\f0e8\"}.icon-umbrella:before{content:\"\\f0e9\"}.icon-paste:before{content:\"\\f0ea\"}.icon-lightbulb:before{content:\"\\f0eb\"}.icon-exchange:before{content:\"\\f0ec\"}.icon-cloud-download:before{content:\"\\f0ed\"}.icon-cloud-upload:before{content:\"\\f0ee\"}.icon-user-md:before{content:\"\\f0f0\"}.icon-stethoscope:before{content:\"\\f0f1\"}.icon-suitcase:before{content:\"\\f0f2\"}.icon-bell-alt:before{content:\"\\f0f3\"}.icon-coffee:before{content:\"\\f0f4\"}.icon-food:before{content:\"\\f0f5\"}.icon-file-alt:before{content:\"\\f0f6\"}.icon-building:before{content:\"\\f0f7\"}.icon-hospital:before{content:\"\\f0f8\"}.icon-ambulance:before{content:\"\\f0f9\"}.icon-medkit:before{content:\"\\f0fa\"}.icon-fighter-jet:before{content:\"\\f0fb\"}.icon-beer:before{content:\"\\f0fc\"}.icon-h-sign:before{content:\"\\f0fd\"}.icon-plus-sign-alt:before{content:\"\\f0fe\"}.icon-double-angle-left:before{content:\"\\f100\"}.icon-double-angle-right:before{content:\"\\f101\"}.icon-double-angle-up:before{content:\"\\f102\"}.icon-double-angle-down:before{content:\"\\f103\"}.icon-angle-left:before{content:\"\\f104\"}.icon-angle-right:before{content:\"\\f105\"}.icon-angle-up:before{content:\"\\f106\"}.icon-angle-down:before{content:\"\\f107\"}.icon-desktop:before{content:\"\\f108\"}.icon-laptop:before{content:\"\\f109\"}.icon-tablet:before{content:\"\\f10a\"}.icon-mobile-phone:before{content:\"\\f10b\"}.icon-circle-blank:before{content:\"\\f10c\"}.icon-quote-left:before{content:\"\\f10d\"}.icon-quote-right:before{content:\"\\f10e\"}.icon-spinner:before{content:\"\\f110\"}.icon-circle:before{content:\"\\f111\"}.icon-reply:before{content:\"\\f112\"}.icon-github-alt:before{content:\"\\f113\"}.icon-folder-close-alt:before{content:\"\\f114\"}.icon-folder-open-alt:before{content:\"\\f115\"}");
                    yaz.Close();
                }
            }

            CopyFromResource(Properties.Resources.Content_admin_css_font_awesome_FontAwesome_otf, PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\FontAwesome.otf");
            CopyFromResource(Properties.Resources.Content_admin_css_font_awesome_fontawesome_webfont_eot, PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\fontawesome_webfont.eot");
            CopyFromResource(Properties.Resources.Content_admin_css_font_awesome_fontawesome_webfont_svg, PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\fontawesome_webfont.svg");
            CopyFromResource(Properties.Resources.Content_admin_css_font_awesome_fontawesome_webfont_ttf, PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\fontawesome_webfont.ttf");
            CopyFromResource(Properties.Resources.Content_admin_css_font_awesome_fontawesome_webfont_woff, PathAddress + "\\" + projectFolder + "\\Content\\admin\\css\\font-awesome\\fontawesome_webfont.woff");
        }

        void CreateCKEditor()
        {
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_js_ckeditor_ckeditor_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\ckeditor.js");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_js_ckeditor_ckeditor_js_map), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\ckeditor.js.map");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_js_ckeditor_translations_en_au_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\translations\\en-au.js");
            CopyFromResource(StringToByteArray(Properties.Resources.Content_admin_js_ckeditor_translations_tr_js), PathAddress + "\\" + projectFolder + "\\Content\\admin\\js\\ckeditor\\translations\\tr.js");
        }

        void CopyFromResource(byte[] resourceFile, string destFile)
        {
            File.WriteAllBytes(destFile, resourceFile);
        }

        byte[] StringToByteArray(string file)
        {
            return Encoding.ASCII.GetBytes(file);
        }

        byte[] BitmapToByteArray(Bitmap file)
        {
            byte[] result = null;

            if (file != null)
            {
                MemoryStream stream = new MemoryStream();
                file.Save(stream, file.RawFormat);
                result = stream.ToArray();
            }

            return result;
        }

        #endregion
    }
}
