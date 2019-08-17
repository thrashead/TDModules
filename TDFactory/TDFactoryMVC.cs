using Common;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TDFactory.Helper;
using InType = TDFactory.Helper.ExMethods.InType;

namespace TDFactory
{
    public partial class TDFactory : Form
    {
        #region MVC

        void CreateMVC()
        {
            CreateMVCDirectories();
            CreateMVCReadMe();

            if (chkMVCHepsi.Checked == true)
            {
                CreateRepository();
                CreateMVCViewLayer();
                CreateMVCControllerLayer();
                CreateWebConfig();
                CreateWcfService();
                CreateStylelScript();
                CreateDllFiles();

                CreateStoredProcedure();
            }
            else
            {
                if (chkRepository.Checked == true)
                {
                    CreateRepository();
                }

                if (chkMVCView.Checked == true)
                {
                    CreateMVCViewLayer();
                }

                if (chkMVCController.Checked == true)
                {
                    CreateMVCControllerLayer();
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

            CreateMVCRegistrar();
        }

        void CreateMVCDirectories(string _tableName = null)
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
                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\bin"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\bin");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\" + repositoryName))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\" + repositoryName);
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Models"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Models");
                    }

                    foreach (string Table in selectedTables)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Models\\" + Table))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Models\\" + Table);
                        }
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Data"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Data");
                    }

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

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Service"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Service");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Uploads"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Uploads");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Uploads\\Deleted"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Uploads\\Deleted");
                    }
                }
                else
                {
                    if (chkRepository.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\" + repositoryName))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\" + repositoryName);
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Models"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Models");
                        }

                        foreach (string Table in selectedTables)
                        {
                            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Models\\" + Table))
                            {
                                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Models\\" + Table);
                            }
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Data"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\" + repositoryName + "\\Data");
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
                if (chkMVCHepsi.Checked || chkMVCView.Checked)
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

        void CreateMVCRegistrar()
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

        void CreateMVCLayout()
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
                    yaz.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"@AppMgr.StylePath/style.css\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/jquery/jquery.min.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/jquery/json2.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/pathscript.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/script.js\"></script>");
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

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\Shared\\_LayoutAdmin.cshtml", FileMode.Create))
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
                    yaz.WriteLine("\t@{");
                    yaz.WriteLine("\t\tif (Session[\"CurrentUser\"] == null)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tResponse.Redirect(AppMgr.AdminPath + \"/Home/Login\");");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
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
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/matrix-style.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/matrix-media.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/font-awesome/css/font-awesome.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("<link href=\"@AppMgr.AdminStylePath/style.css\" rel=\"stylesheet\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/jquery/jquery.min.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/jquery/json2.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.AdminScriptPath/jquery.gritter.min.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.AdminScriptPath/jquery.dataTables.min.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.AdminScriptPath/bootstrap.min.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.AdminScriptPath/matrix.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.AdminScriptPath/matrix.tables.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.AdminScriptPath/ckeditor/ckeditor.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.AdminScriptPath/pathscript.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.AdminScriptPath/script.js\"></script>");

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
                    yaz.WriteLine("\t<h1><a><img src=\"@AppMgr.AdminImagePath/logo.png\" /></a></h1>");
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
                    yaz.WriteLine("<div id=\"rmvData\" class=\"modal hide\" aria-hidden=\"true\" style=\"display: none;\">");
                    yaz.WriteLine("\t<div class=\"modal-header\">");
                    yaz.WriteLine("\t\t<button data-dismiss=\"modal\" class=\"close\" type=\"button\">×</button>");
                    yaz.WriteLine("\t\t<h3>Kaldır</h3>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-body\">");
                    yaz.WriteLine("\t\t<p>İlgili veriyi kaldırmak istediğinize emin misiniz?</p>");
                    yaz.WriteLine("\t</div>");
                    yaz.WriteLine("\t<div class=\"modal-footer\">");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn btn-primary rmv-yes\" href=\"javascript:;\">Evet</a>");
                    yaz.WriteLine("\t\t<a data-dismiss=\"modal\" class=\"btn rmv-no\" href=\"javascript:;\">Hayır</a>");
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

        void CreateMVCViewLayer()
        {
            int i = 0;

            foreach (string Table in selectedTables)
            {
                Table table = new Table(Table, connectionInfo);
                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                string formdata = "";

                CreateMVCDirectories(Table);

                if (i <= 0)
                {
                    CreateMVCLayout();
                    CreateMVCHomePage();

                    i++;
                }

                //Index
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\" + Table + "\\Index.cshtml", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("@model List<" + repositoryName + "." + Table + "Model." + Table + ">");
                        yaz.WriteLine("@using " + repositoryName + "." + Table + "Model");
                        yaz.WriteLine("@using TDLibrary");

                        yaz.WriteLine("");
                        yaz.WriteLine("@{");
                        yaz.WriteLine("\tViewBag.Title = \"" + Table + "\";");
                        yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_LayoutAdmin.cshtml\";");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("<div id=\"content\">");
                        yaz.WriteLine("\t<div id=\"content-header\">");
                        yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a class=\"tip-bottom\"><i class=\"icon-home\"></i> " + Table + "</a></div>");
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

                        foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower)).Take(4).ToList())
                        {
                            List<ForeignKeyChecker> frchkForeignLst2 = table.FkcForeignList.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

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

                        if (table.IdentityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>İşlem</th>");
                        }

                        yaz.WriteLine("\t\t\t\t\t\t\t\t</tr>");
                        yaz.WriteLine("\t\t\t\t\t\t\t</thead>");
                        yaz.WriteLine("\t\t\t\t\t\t\t<tbody>");
                        yaz.WriteLine("\t\t\t\t\t\t\t\t@{");
                        yaz.WriteLine("\t\t\t\t\t\t\t\t\tforeach (" + Table + " item in Model)");
                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<tr>");

                        i = 0;

                        foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower)).Take(4).ToList())
                        {
                            List<ForeignKeyChecker> frchkForeignLst2 = table.FkcForeignList.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                            string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                            if (column.Type.Name != "Boolean")
                            {
                                if (frchkForeignLst2.Count > 0)
                                {
                                    yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">@item." + frchkForeignLst2.FirstOrDefault().PrimaryTableName + "Adi</td>");
                                }
                                else
                                {
                                    if (column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<td" + hideColumn + "><a href=\"@AppMgr.UploadPath/@item." + column.ColumnName + "\" target=\"_blank\"><img src=\"@AppMgr.UploadPath/thumb_@item." + column.ColumnName + "\" style=\"height:40px; max-width:80px;\" /></a></td>");
                                    else if (column.ColumnName.In(FileColumns, InType.ToUrlLower))
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<td" + hideColumn + "><a class=\"btn btn-mini btn-info\" href=\"@AppMgr.UploadPath/@item." + column.ColumnName + "\" target=\"_blank\">@item." + column.ColumnName + "</a></td>");
                                    else
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">@item." + column.ColumnName + "</td>");
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<td" + hideColumn + " style=\"text-align:center;\">@(item." + column.ColumnName + " == true ? Html.Raw(\"<img class='active' />\") : Html.Raw(\"<img class='passive' />\"))</td>");
                            }

                            i++;
                        }

                        if (table.IdentityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<td style=\"text-align:center;\">");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<div class=\"btn-group\" style=\"text-align:left;\">");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t<button data-toggle=\"dropdown\" class=\"btn btn-mini btn-primary dropdown-toggle\">İşlem <span class=\"caret\"></span></button>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t<ul class=\"dropdown-menu\">");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"updLink\" href=\"@AppMgr.AdminPath/" + Table + "/Update/@item." + table.ID + "\">Düzenle</a></li>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"cpyLink\" href=\"#cpyData\" data-type=\"" + Table + "\" data-id=\"@item." + table.ID + "\" data-link=\"" + Table + "\" data-toggle=\"modal\">Kopyala</a></li>");

                            if (table.Deleted)
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"rmvLink\" href=\"#rmvData\" data-type=\"" + Table + "\" data-id=\"@item." + table.ID + "\" data-link=\"" + Table + "\" data-toggle=\"modal\">Kaldır</a></li>");

                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"dltLink\" href=\"#dltData\" data-type=\"" + Table + "\" data-id=\"@item." + table.ID + "\" data-link=\"" + Table + "\" data-toggle=\"modal\">Sil</a></li>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t</ul>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t</div>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t</td>");
                        }

                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t</tr>");
                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\t\t\t</tbody>");
                        yaz.WriteLine("\t\t\t\t\t\t</table>");
                        yaz.WriteLine("\t\t\t\t\t</div>");
                        yaz.WriteLine("\t\t\t\t</div>");
                        yaz.WriteLine("\t\t\t</div>");
                        yaz.WriteLine("\t\t</div>");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t<div class=\"pagelinks\">");
                        yaz.WriteLine("\t\t\t@Html.ActionLink(\"" + Table + " Ekle\", \"Insert\", \"" + Table + "\", null, new { @class = \"btn btn-primary btn-add\", data_type = \"" + Table + "\" })");
                        yaz.WriteLine("\t\t</div>");
                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("</div>");
                        yaz.WriteLine("");
                        yaz.WriteLine("@{ Html.RenderPartial(\"~/Areas/Admin/Views/Shared/Controls/_CopyDelete.cshtml\"); }");
                        yaz.Close();
                    }
                }

                //Ekle
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\" + Table + "\\Insert.cshtml", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("@model " + repositoryName + "." + Table + "Model." + Table);
                        yaz.WriteLine("");
                        yaz.WriteLine("@{");
                        yaz.WriteLine("\tViewBag.Title = \"" + Table + " Ekle\";");
                        yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_LayoutAdmin.cshtml\";");
                        yaz.WriteLine("}");
                        yaz.WriteLine("");
                        yaz.WriteLine("<div id=\"content\">");
                        yaz.WriteLine("\t<div id=\"content-header\">");
                        yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a class=\"tip-bottom\"><i class=\"icon-home\"></i> " + Table + " Ekle</a></div>");
                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("\t<div class=\"container-fluid\">");

                        if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                            formdata = "\"Insert\", \"" + Table + "\", FormMethod.Post, new { encType = \"multipart/form-data\" }";

                        yaz.WriteLine("\t\t@using (Html.BeginForm(" + formdata + "))");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\t@Html.ValidationSummary(true)");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t<fieldset>");

                        foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList())
                        {
                            if (!table.IdentityColumns.Contains(column.ColumnName))
                            {
                                List<ForeignKeyChecker> frchkLst = table.FkcForeignList.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                if (frchkLst.Count > 0)
                                {
                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t\t\tBağlı " + frchkLst.FirstOrDefault().PrimaryTableName);
                                    yaz.WriteLine("\t\t\t\t</div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                    yaz.WriteLine("\t\t\t\t\t@Html.DropDownListFor(model => model." + column.ColumnName + ", (List<SelectListItem>)Model." + frchkLst.FirstOrDefault().PrimaryTableName + "List)");
                                }
                                else
                                {
                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t\t\t@Html.LabelFor(model => model." + column.ColumnName + ")");
                                    yaz.WriteLine("\t\t\t\t</div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");

                                    if (column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                    {

                                        yaz.WriteLine("\t\t\t\t\t@Html.HiddenFor(model => model." + column.ColumnName + ")");
                                        yaz.WriteLine("\t\t\t\t\t@Html.TextBoxFor(model => model." + column.ColumnName + ", new { type = \"file\", name = \"" + column.ColumnName.ToUrl(true) + "Temp\",  accept = \"image/*\" })");
                                    }
                                    else if (column.ColumnName.In(FileColumns, InType.ToUrlLower))
                                    {
                                        yaz.WriteLine("\t\t\t\t\t@Html.HiddenFor(model => model." + column.ColumnName + ")");
                                        yaz.WriteLine("\t\t\t\t\t@Html.TextBoxFor(model => model." + column.ColumnName + ", new { type = \"file\", name = \"" + column.ColumnName.ToUrl(true) + "Temp\" })");
                                    }
                                    else
                                    {
                                        if (column.Type.Name == "String" && column.CharLength != -1)
                                        {
                                            yaz.WriteLine("\t\t\t\t\t@Html.TextBoxFor(model => model." + column.ColumnName + ")");
                                        }
                                        else if (column.Type.Name == "String" && column.CharLength == -1)
                                        {
                                            yaz.WriteLine("\t\t\t\t\t@Html.TextAreaFor(model => model." + column.ColumnName + ")");
                                        }
                                        else
                                        {
                                            yaz.WriteLine("\t\t\t\t\t@Html.EditorFor(model => model." + column.ColumnName + ")");
                                        }
                                    }
                                }

                                yaz.WriteLine("\t\t\t\t\t<br />");
                                yaz.WriteLine("\t\t\t\t\t@Html.ValidationMessageFor(model => model." + column.ColumnName + ")");
                                yaz.WriteLine("\t\t\t\t</div>");
                                yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                yaz.WriteLine("");
                            }
                        }

                        yaz.WriteLine("\t\t\t\t<p>");
                        yaz.WriteLine("\t\t\t\t\t<div class=\"alert alert-error\" style=\"display:@(String.IsNullOrEmpty(Model.Mesaj) ? \"none;\" : \"block;\")\">");
                        yaz.WriteLine("\t\t\t\t\t\t<button class=\"close\" data-dismiss=\"alert\">×</button>");
                        yaz.WriteLine("\t\t\t\t\t\t<strong>Hata!</strong> @Model.Mesaj");
                        yaz.WriteLine("\t\t\t\t\t</div>");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t<input type=\"submit\" value=\"Kaydet\" class=\"btn btn-success btn-save\" />");
                        yaz.WriteLine("\t\t\t\t\t@Html.ActionLink(\"İptal\", \"Index\", \"" + Table + "\", null, new { @class = \"btn btn-danger btn-cancel\", data_controller=\"" + Table + "\" })");
                        yaz.WriteLine("\t\t\t\t</p>");
                        yaz.WriteLine("\t\t\t</fieldset>");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("</div>");
                        yaz.Close();
                    }
                }

                if (table.IdentityColumns.Count > 0)
                {
                    //Duzenle
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Views\\" + Table + "\\Update.cshtml", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                        {
                            yaz.WriteLine("@model " + repositoryName + "." + Table + "Model." + Table);

                            if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0 || table.FkcList.Count > 0)
                                yaz.WriteLine("@using TDLibrary");

                            if (table.FkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in table.FkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    yaz.WriteLine("@using " + repositoryName + "." + ForeignTableName + "Model");

                                }
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("@{");
                            yaz.WriteLine("\tViewBag.Title = \"" + Table + " Düzenle\";");
                            yaz.WriteLine("\tLayout = \"~/Areas/Admin/Views/Shared/_LayoutAdmin.cshtml\";");
                            yaz.WriteLine("}");
                            yaz.WriteLine("");
                            yaz.WriteLine("<div id=\"content\">");
                            yaz.WriteLine("\t<div id=\"content-header\">");
                            yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a class=\"tip-bottom\"><i class=\"icon-home\"></i> " + Table + " Düzenle</a></div>");
                            yaz.WriteLine("\t</div>");
                            yaz.WriteLine("\t<div class=\"container-fluid\">");

                            if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                                formdata = "\"Update\", \"" + Table + "\", FormMethod.Post, new { encType = \"multipart/form-data\" }";

                            yaz.WriteLine("\t\t@using (Html.BeginForm(" + formdata + "))");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t@Html.ValidationSummary(true)");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t<fieldset>");

                            foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList())
                            {
                                if (!table.IdentityColumns.Contains(column.ColumnName))
                                {
                                    List<ForeignKeyChecker> frchkLst = table.FkcForeignList.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    if (frchkLst.Count > 0)
                                    {
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");
                                        yaz.WriteLine("\t\t\t\t\tBağlı " + frchkLst.FirstOrDefault().PrimaryTableName);
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                        yaz.WriteLine("\t\t\t\t\t@Html.DropDownListFor(model => model." + column.ColumnName + ", (List<SelectListItem>)Model." + frchkLst.FirstOrDefault().PrimaryTableName + "List)");
                                    }
                                    else
                                    {
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");
                                        yaz.WriteLine("\t\t\t\t\t@Html.LabelFor(model => model." + column.ColumnName + ")");
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");

                                        if (column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                        {
                                            yaz.WriteLine("\t\t\t\t\t<a href=\"@AppMgr.UploadPath/@Model." + column.ColumnName + "\" target=\"_blank\"><img src=\"@(AppMgr.UploadPath + \"/thumb_\" + Model." + column.ColumnName + ")\" style=\"height:40px; max-width:80px;\" /></a><br /><br />");
                                            yaz.WriteLine("\t\t\t\t\t@Html.HiddenFor(model => model." + column.ColumnName + ")");
                                            yaz.WriteLine("\t\t\t\t\t@Html.HiddenFor(model => model.Old" + column.ColumnName + ")");
                                            yaz.WriteLine("\t\t\t\t\t@Html.TextBoxFor(model => model." + column.ColumnName + ", new { type = \"file\", name = Model." + column.ColumnName + ",  accept = \"image/*\" })");
                                        }
                                        else if (column.ColumnName.In(FileColumns, InType.ToUrlLower))
                                        {
                                            yaz.WriteLine("\t\t\t\t\t<a class=\"btn btn-mini btn-info\" href=\"@AppMgr.UploadPath/@Model." + column.ColumnName + "\" target=\"_blank\">@Model." + column.ColumnName + "</a><br /><br />");
                                            yaz.WriteLine("\t\t\t\t\t@Html.HiddenFor(model => model." + column.ColumnName + ")");
                                            yaz.WriteLine("\t\t\t\t\t@Html.HiddenFor(model => model.Old" + column.ColumnName + ")");
                                            yaz.WriteLine("\t\t\t\t\t@Html.TextBoxFor(model => model." + column.ColumnName + ", new { type = \"file\", name = Model." + column.ColumnName + " })");
                                        }
                                        else
                                        {
                                            if (column.Type.Name == "String" && column.CharLength != -1)
                                            {
                                                yaz.WriteLine("\t\t\t\t\t@Html.TextBoxFor(model => model." + column.ColumnName + ")");
                                            }
                                            else if (column.Type.Name == "String" && column.CharLength == -1)
                                            {
                                                yaz.WriteLine("\t\t\t\t\t@Html.TextAreaFor(model => model." + column.ColumnName + ")");
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t\t\t@Html.EditorFor(model => model." + column.ColumnName + ")");
                                            }
                                        }
                                    }

                                    yaz.WriteLine("\t\t\t\t\t<br />");
                                    yaz.WriteLine("\t\t\t\t\t@Html.ValidationMessageFor(model => model." + column.ColumnName + ")");
                                    yaz.WriteLine("\t\t\t\t</div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("");
                                }
                                else
                                {
                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t\t\t@Html.HiddenFor(model => model." + table.ID + ")");
                                    yaz.WriteLine("\t\t\t\t</div>");
                                }
                            }

                            yaz.WriteLine("\t\t\t\t<p>");
                            yaz.WriteLine("\t\t\t\t\t<div class=\"alert alert-error\" style=\"display:@(String.IsNullOrEmpty(Model.Mesaj) ? \"none;\" : \"block;\")\">");
                            yaz.WriteLine("\t\t\t\t\t\t<button class=\"close\" data-dismiss=\"alert\">×</button>");
                            yaz.WriteLine("\t\t\t\t\t\t<strong>Hata!</strong> @Model.Mesaj");
                            yaz.WriteLine("\t\t\t\t\t</div>");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\t<input type=\"submit\" value=\"Kaydet\" class=\"btn btn-success btn-save\" />");
                            yaz.WriteLine("\t\t\t\t\t@Html.ActionLink(\"İptal\", \"Index\", \"" + Table + "\", null, new { @class = \"btn btn-danger btn-cancel\", data_controller=\"" + Table + "\" })");
                            yaz.WriteLine("\t\t\t\t</p>");
                            yaz.WriteLine("\t\t\t</fieldset>");
                            yaz.WriteLine("\t\t}");

                            if (table.FkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in table.FkcList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                                {
                                    foreach (ForeignKeyChecker fkc2 in table.FkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                    {
                                        string PrimaryTableName = fkc.PrimaryTableName;
                                        string ForeignTableName = fkc2.ForeignTableName;

                                        List<string> identityForeignColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, ForeignTableName);
                                        string idFrgn = identityForeignColumns.Count > 0 ? identityForeignColumns.FirstOrDefault() : "id";

                                        List<ColumnInfo> foreignColumns = tableColumnInfos.Where(a => a.TableName == ForeignTableName && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower)).Take(4).ToList();

                                        List<ForeignKeyChecker> fkcListForeign2 = ForeignKeyCheck(con);
                                        fkcListForeign2 = fkcListForeign2.Where(a => a.ForeignTableName == ForeignTableName).ToList();

                                        List<ColumnInfo> fColumnNames = Helper.Helper.GetColumnsInfo(connectionInfo, ForeignTableName).ToList();
                                        bool fDeleted = fColumnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? true : false;

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

                                        foreach (ColumnInfo item in foreignColumns)
                                        {
                                            List<ForeignKeyChecker> frchkForeignLst = fkcListForeign2.Where(a => a.ForeignColumnName == item.ColumnName).ToList();

                                            string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                                            if (frchkForeignLst.Count <= 0)
                                            {
                                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th" + hideColumn + ">" + item.ColumnName + "</th>");
                                                i++;
                                            }
                                        }

                                        if (identityForeignColumns.Count > 0)
                                        {
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>İşlem</th>");
                                        }

                                        yaz.WriteLine("\t\t\t\t\t\t\t\t</tr>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t</thead>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t<tbody>");

                                        yaz.WriteLine("\t\t\t\t\t\t\t@{");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\tforeach (I" + ForeignTableName + " item in Model." + ForeignTableName + "List)");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t{");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t<tr>");

                                        i = 0;

                                        foreach (ColumnInfo item in foreignColumns)
                                        {
                                            List<ForeignKeyChecker> frchkForeignLst = fkcListForeign2.Where(a => a.ForeignColumnName == item.ColumnName).ToList();

                                            string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                                            if (frchkForeignLst.Count <= 0)
                                            {
                                                if (item.Type.Name != "Boolean")
                                                {
                                                    if (item.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<td" + hideColumn + "><a href=\"@AppMgr.UploadPath/@item." + item.ColumnName + "\" target=\"_blank\"><img src=\"@AppMgr.UploadPath/thumb_@item." + item.ColumnName + "\" style=\"height:40px; max-width:80px;\" /></a></td>");
                                                    else if (item.ColumnName.In(FileColumns, InType.ToUrlLower))
                                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<td" + hideColumn + "><a class=\"btn btn-mini btn-info\" href=\"@AppMgr.UploadPath/@item." + item.ColumnName + "\" target=\"_blank\">@item." + item.ColumnName + "</a></td>");
                                                    else
                                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">@item." + item.ColumnName + "</td>");
                                                    i++;
                                                }
                                                else
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<td" + hideColumn + " style=\"text-align:center;\">@(item." + item.ColumnName + " == true ? Html.Raw(\"<img class='active' />\") : Html.Raw(\"<img class='passive' />\"))</td>");
                                                    i++;
                                                }
                                            }
                                        }

                                        if (identityForeignColumns.Count > 0)
                                        {
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<td style=\"text-align:center;\">");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<div class=\"btn-group\" style=\"text-align:left;\">");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<button data-toggle=\"dropdown\" class=\"btn btn-mini btn-primary dropdown-toggle\">İşlem <span class=\"caret\"></span></button>");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<ul class=\"dropdown-menu\">");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"updLink\" href=\"@AppMgr.AdminPath/" + ForeignTableName + "/Update/@item." + idFrgn + "\">Düzenle</a></li>");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"cpyLink\" href=\"#cpyData\" data-toggle=\"modal\" data-link=\"" + ForeignTableName + "\" data-id=\"@item." + idFrgn + "\">Kopyala</a></li>");

                                            if (fDeleted)
                                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"rmvLink\" href=\"#rmvData\" data-toggle=\"modal\" data-link=\"" + ForeignTableName + "\" data-id=\"@item." + idFrgn + "\">Kaldır</a></li>");

                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"dltLink\" href=\"#dltData\" data-toggle=\"modal\" data-link=\"" + ForeignTableName + "\" data-id=\"@item." + idFrgn + "\">Sil</a></li>");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t</ul>");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t</div>");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t</td>");
                                        }

                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t</tr>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t\t\t\t\t</tbody>");
                                        yaz.WriteLine("\t\t\t\t\t\t</table>");
                                        yaz.WriteLine("\t\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t</div>");
                                        yaz.WriteLine("\t\t</div>");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t<div class=\"pagelinks\">");
                                        yaz.WriteLine("\t\t\t@Html.ActionLink(\"" + ForeignTableName + " Ekle\", \"Insert\", \"" + ForeignTableName + "\", null, new { @class = \"btn btn-primary btn-add\", data_type = \"" + ForeignTableName + "\" })");
                                        yaz.WriteLine("\t\t</div>");
                                    }
                                }
                            }

                            yaz.WriteLine("\t</div>");
                            yaz.WriteLine("</div>");

                            if (table.FkcList.Count > 0)
                            {
                                yaz.WriteLine("");
                                yaz.WriteLine("@{ Html.RenderPartial(\"~/Areas/Admin/Views/Shared/Controls/_CopyDelete.cshtml\"); }");
                            }

                            yaz.Close();
                        }
                    }
                }
            }
        }

        void CreateMVCControllerLayer()
        {
            CreateMVCHomeController();

            foreach (string Table in selectedTables)
            {
                Table table = new Table(Table, connectionInfo);
                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                CreateMVCDirectories(Table);

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Admin\\Controllers\\" + Table + "Controller.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System.Web.Mvc;");

                        if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                        {
                            yaz.WriteLine("using System.Collections.Generic;");
                            yaz.WriteLine("using TDLibrary;");
                        }

                        yaz.WriteLine("using " + repositoryName + "." + Table + "Model;");
                        yaz.WriteLine("");

                        yaz.WriteLine("namespace " + projectName + ".Areas.Admin.Controllers");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class " + Table + "Controller : Controller");
                        yaz.WriteLine("\t{");

                        yaz.WriteLine("\t\treadonly " + Table + " model = new " + Table + "();");
                        yaz.WriteLine("");

                        // Index
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic ViewResult Index(int? id)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn View(model.List(id));");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        // Ekle
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic ActionResult Insert()");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn View(model.Insert());");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t\t[HttpPost]");
                        yaz.WriteLine("\t\tpublic ActionResult Insert(" + Table + " table)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                        yaz.WriteLine("\t\t\t{");

                        if (table.FILEColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t\tList<Uploader> files = Uploader.UploadFiles(false);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\tforeach (var item in files)");
                            yaz.WriteLine("\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\tif (!item.Control)");
                            yaz.WriteLine("\t\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\t\ttable.Mesaj = item.ErrorMessage;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\t\treturn View(\"Insert\", table);");
                            yaz.WriteLine("\t\t\t\t\t}");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("");
                        }

                        if (table.IMAGEColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t\tList<Uploader> pictures = Uploader.UploadPictures(false);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\tforeach (var item in pictures)");
                            yaz.WriteLine("\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\tif (!item.Control)");
                            yaz.WriteLine("\t\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\t\ttable.Mesaj = item.ErrorMessage;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\t\treturn View(\"Insert\", table);");
                            yaz.WriteLine("\t\t\t\t\t}");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\t\t\t\tbool result = model.Insert(table);");

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tif(result)");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\treturn RedirectToAction(\"Index\");");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\telse");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\ttable.Mesaj = \"Kayıt eklenemedi.\";");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");


                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\ttable.Mesaj = \"Model uygun değil.\";");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");

                        string linkID = ", null";

                        if (table.FkcForeignList.Count > 0)
                        {
                            linkID = "";

                            foreach (ForeignKeyChecker fkc in table.FkcForeignList.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                linkID += ", table." + fkc.ForeignColumnName;
                            }
                        }

                        yaz.WriteLine("\t\t\ttable = (" + Table + ")model.Insert(table" + linkID + ");");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t\t\treturn View(\"Insert\", table);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        if (table.IdentityColumns.Count > 0)
                        {
                            string columntype = tableColumnInfos.Where(a => a.ColumnName == table.ID && a.TableName == Table).FirstOrDefault().Type.Name.ToString();

                            //Duzenle
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic ActionResult Update(" + columntype.ReturnCSharpType() + "? id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\t" + Table + " table = (" + Table + ")model.Update(id);");
                            yaz.WriteLine("");

                            int j = 1;
                            if (table.FILEColumns.Count > 0)
                            {
                                foreach (ColumnInfo item in table.FILEColumns)
                                {
                                    yaz.WriteLine("\t\t\ttable.Old" + item.ColumnName + " = table." + item.ColumnName + ";");

                                    if (j == table.FILEColumns.Count)
                                        yaz.WriteLine("");

                                    j++;
                                }

                                if (table.IMAGEColumns.Count > 0)
                                    yaz.WriteLine("");
                            }

                            j = 1;
                            if (table.IMAGEColumns.Count > 0)
                            {
                                foreach (ColumnInfo item in table.IMAGEColumns)
                                {
                                    yaz.WriteLine("\t\t\ttable.Old" + item.ColumnName + " = table." + item.ColumnName + ";");

                                    if (j == table.IMAGEColumns.Count)
                                        yaz.WriteLine("");
                                }
                            }

                            yaz.WriteLine("\t\t\treturn View(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t[HttpPost]");
                            yaz.WriteLine("\t\tpublic ActionResult Update(" + Table + " table)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (ModelState.IsValid)");
                            yaz.WriteLine("\t\t\t{");

                            if (table.FILEColumns.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t\tList<Uploader> files = Uploader.UploadFiles(false);");
                                yaz.WriteLine("");

                                yaz.WriteLine("\t\t\t\tforeach (var item in files)");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\t\t\tif (item.UploadError != null)");
                                yaz.WriteLine("\t\t\t\t\t{");
                                yaz.WriteLine("\t\t\t\t\t\ttable.Mesaj = item.ErrorMessage;");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\t\treturn View(\"Update\", table);");
                                yaz.WriteLine("\t\t\t\t\t}");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("");

                                foreach (ColumnInfo item in table.FILEColumns)
                                {
                                    yaz.WriteLine("\t\t\t\tif (table." + item.ColumnName + " != table.Old" + item.ColumnName + ")");
                                    yaz.WriteLine("\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\ttry");
                                    yaz.WriteLine("\t\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\t\tSystem.IO.File.Delete(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table.Old" + item.ColumnName + "));");
                                    yaz.WriteLine("\t\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t\t\tcatch");
                                    yaz.WriteLine("\t\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\t\ttable.Mesaj = \"Eski dosya silinemedi.\";");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t\t\treturn View(\"Update\", table);");
                                    yaz.WriteLine("\t\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t\t}");
                                    yaz.WriteLine("");
                                }
                            }

                            if (table.IMAGEColumns.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t\tList<Uploader> pictures = Uploader.UploadPictures(false);");
                                yaz.WriteLine("");

                                yaz.WriteLine("\t\t\t\tforeach (var item in pictures)");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\t\t\tif (item.UploadError != null)");
                                yaz.WriteLine("\t\t\t\t\t{");
                                yaz.WriteLine("\t\t\t\t\t\ttable.Mesaj = item.ErrorMessage;");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\t\treturn View(\"Update\", table);");
                                yaz.WriteLine("\t\t\t\t\t}");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("");

                                foreach (ColumnInfo item in table.IMAGEColumns)
                                {
                                    yaz.WriteLine("\t\t\t\tif (table." + item.ColumnName + " != table.Old" + item.ColumnName + ")");
                                    yaz.WriteLine("\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\ttry");
                                    yaz.WriteLine("\t\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\t\tSystem.IO.File.Delete(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table.Old" + item.ColumnName + "));");
                                    yaz.WriteLine("\t\t\t\t\t\tSystem.IO.File.Delete(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/thumb_\" + table.Old" + item.ColumnName + "));");
                                    yaz.WriteLine("\t\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t\t\tcatch");
                                    yaz.WriteLine("\t\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\t\ttable.Mesaj = \"Eski dosya silinemedi.\";");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t\t\treturn View(\"Update\", table);");
                                    yaz.WriteLine("\t\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t\t}");
                                    yaz.WriteLine("");
                                }
                            }

                            yaz.WriteLine("\t\t\t\tbool result = model.Update(table);");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t\t\tif(result)");
                            yaz.WriteLine("\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\treturn RedirectToAction(\"Index\");");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("\t\t\t\telse");
                            yaz.WriteLine("\t\t\t\t{");
                            yaz.WriteLine("\t\t\t\t\ttable.Mesaj = \"Kayıt düzenlenemedi.\";");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t\telse");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\ttable.Mesaj = \"Model uygun değil.\";");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t\ttable = (" + Table + ")model.Update(table." + table.ID + ", table);");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t\treturn View(\"Update\", table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Kopyala
                            yaz.WriteLine("\t\t[HttpPost]");
                            yaz.WriteLine("\t\tpublic JsonResult Copy(" + columntype.ReturnCSharpType() + " id)");
                            yaz.WriteLine("\t\t{");
                            if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                            {
                                yaz.WriteLine("\t\t\ttry");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\t" + Table + " table = (" + Table + ")model.Select(id, false);");
                                yaz.WriteLine("");

                                foreach (ColumnInfo item in table.FILEColumns)
                                {
                                    yaz.WriteLine("\t\t\t\tSystem.IO.File.Copy(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table." + item.ColumnName + "), Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/Kopya_\" + table." + item.ColumnName + "));");
                                }

                                foreach (ColumnInfo item in table.IMAGEColumns)
                                {
                                    yaz.WriteLine("\t\t\t\tSystem.IO.File.Copy(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table." + item.ColumnName + "), Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/Kopya_\" + table." + item.ColumnName + "));");
                                    yaz.WriteLine("\t\t\t\tSystem.IO.File.Copy(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/thumb_\" + table." + item.ColumnName + "), Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/thumb_Kopya_\" + table." + item.ColumnName + "));");
                                }

                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("\t\t\tcatch");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\treturn Json(false);");
                                yaz.WriteLine("\t\t\t}");

                                yaz.WriteLine("");
                            }
                            yaz.WriteLine("\t\t\tbool result = model.Copy(id);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (result)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\treturn Json(true);");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn Json(false);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Sil
                            yaz.WriteLine("\t\t[HttpPost]");
                            yaz.WriteLine("\t\tpublic JsonResult Delete(" + columntype.ReturnCSharpType() + "? id)");
                            yaz.WriteLine("\t\t{");
                            if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                            {
                                yaz.WriteLine("\t\t\ttry");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\t" + Table + " table = (" + Table + ")model.Select(id, false);");
                                yaz.WriteLine("");

                                foreach (ColumnInfo item in table.FILEColumns)
                                {
                                    yaz.WriteLine("\t\t\t\tSystem.IO.File.Delete(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table." + item.ColumnName + "));");
                                }

                                foreach (ColumnInfo item in table.IMAGEColumns)
                                {
                                    yaz.WriteLine("\t\t\t\tSystem.IO.File.Delete(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table." + item.ColumnName + "));");
                                    yaz.WriteLine("\t\t\t\tSystem.IO.File.Delete(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/thumb_\" + table." + item.ColumnName + "));");
                                }

                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("\t\t\tcatch");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\treturn Json(false);");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\t\t\tbool result = model.Delete(id);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (result)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\treturn Json(true);");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn Json(false);");
                            yaz.WriteLine("\t\t}");

                            if (table.Deleted)
                            {
                                //Kaldır
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t[HttpPost]");
                                yaz.WriteLine("\t\tpublic JsonResult Remove(" + columntype.ReturnCSharpType() + "? id)");
                                yaz.WriteLine("\t\t{");
                                if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                                {
                                    yaz.WriteLine("\t\t\ttry");
                                    yaz.WriteLine("\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t" + Table + " table = (" + Table + ")model.Select(id, false);");
                                    yaz.WriteLine("");

                                    foreach (ColumnInfo item in table.FILEColumns)
                                    {
                                        yaz.WriteLine("\t\t\t\tSystem.IO.File.Move(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table." + item.ColumnName + "), Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/Deleted/\" + table." + item.ColumnName + "));");
                                    }

                                    foreach (ColumnInfo item in table.IMAGEColumns)
                                    {
                                        yaz.WriteLine("\t\t\t\tSystem.IO.File.Move(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table." + item.ColumnName + "), Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/Deleted/\" + table." + item.ColumnName + "));");
                                        yaz.WriteLine("\t\t\t\tSystem.IO.File.Move(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/thumb_\" + table." + item.ColumnName + "), Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/Deleted/thumb_\" + table." + item.ColumnName + "));");
                                    }

                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t\tcatch");
                                    yaz.WriteLine("\t\t\t{");
                                    yaz.WriteLine("\t\t\t\treturn Json(false);");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("");
                                }
                                yaz.WriteLine("\t\t\tbool result = model.Remove(id);");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tif (result)");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\treturn Json(true);");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\treturn Json(false);");
                                yaz.WriteLine("\t\t}");
                            }
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }
        }

        void CreateMVCHomePage()
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
                    yaz.WriteLine("\t<script src=\"@AppMgr.AdminScriptPath/script.js\"></script>");
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

        void CreateMVCHomeController()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Controllers\\HomeController.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
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

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers\\AjaxController.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {

                    yaz.WriteLine("using System.Linq;");
                    yaz.WriteLine("using System.Web.Mvc;");
                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName + ".Areas.Ajax.Controllers");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic class AjaxController : Controller");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\t[HttpPost]");
                    yaz.WriteLine("\t\tpublic JsonResult Login(string login)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tSession[\"CurrentUser\"] = 1;");
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
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }
        }

        void CreateMVCReadMe()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\readme.txt", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("-------------");
                    yaz.WriteLine("-- Hatalar --");
                    yaz.WriteLine("-------------");
                    yaz.WriteLine("");
                    yaz.WriteLine("Hata 1 : The type or namespace name 'Http' does not exist in the namespace 'System.Web'");
                    yaz.WriteLine("Çözüm 1 : Package Manager Console üzerinden \"Install-Package Microsoft.AspNet.WebApi.Core\" kodunu çalıştır.");
                    yaz.WriteLine("");
                    yaz.WriteLine("Hata 2 : EntityFramework Kurulumu");
                    yaz.WriteLine("Çözüm 2 : Package Manager Console üzerinden \"Install-Package EntityFramework\" kodunu çalıştır.");
                    yaz.Close();
                }
            }
        }

        #endregion
    }
}
