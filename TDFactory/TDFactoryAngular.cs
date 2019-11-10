using Common;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using InType = TDFactory.ExMethods.InType;

namespace TDFactory
{
    public partial class TDFactory : Form
    {
        #region Angular

        void CreateAngular()
        {
            CreateAngularDirectories();
            CreateAngularReadMe();
            CreateAngularFiles();

            if (chkMVCHepsi.Checked == true)
            {
                CreateRepository();
                CreateAngularViewLayer();
                CreateAngularControllerLayer();
                CreateAngularServiceLayer();
                CreateAngularTypeScriptLayer();
                CreateWcfService();
                CreateWebConfig();
                CreateStylelScript();
                CreateDllFiles();

                if (hasUserRights || hasLogs)
                {
                    CreateAngularLib();
                }

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
                    CreateAngularViewLayer();
                }

                if (chkMVCController.Checked == true)
                {
                    CreateAngularControllerLayer();
                    CreateAngularServiceLayer();
                    CreateAngularTypeScriptLayer();

                    if (hasUserRights || hasLogs)
                    {
                        CreateAngularLib();
                    }
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

            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\general"))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\general");
            }

            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + projectName.ToUrl(true)))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + projectName.ToUrl(true));
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

            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\lib"))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\lib");
            }

            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\lib"))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\lib");
            }

            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\views"))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\views");
            }

            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared"))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared");
            }

            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared\\controls"))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared\\controls");
            }

            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\home"))
            {
                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\home");
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

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Service"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Service");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\StoredProcedures"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\StoredProcedures");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Uploads"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Uploads");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Uploads\\Deleted"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Uploads\\Deleted");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Models"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Models");
                    }

                    if (hasUserRights || hasLogs)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Lib"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Lib");
                        }
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

                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Models"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Models");
                        }

                        if (hasUserRights || hasLogs)
                        {
                            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Lib"))
                            {
                                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Lib");
                            }
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

                    if (chkMVCStoredProc.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\StoredProcedures"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\StoredProcedures");
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
                string tableFolder = _tableName.ToUserRightType() == "Website" ? projectName.ToUrl(true) : "general";

                if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + tableFolder + "\\" + _tableName.ToUrl(true)))
                {
                    Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + tableFolder + "\\" + _tableName.ToUrl(true));
                }
            }
        }

        void CreateAngularFiles()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\ngbuild.bat", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                {
                    yaz.WriteLine("ng build --watch");
                    yaz.Close();
                }
            }

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
                    yaz.WriteLine("import { Component } from '@angular/core';");
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
                    yaz.WriteLine("\tselector: \"" + projectName.Substring(0, 3).ToUrl(true) + "-app\",");
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

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared\\layout.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {

                    yaz.WriteLine("<" + projectName.Substring(0, 3).ToUrl(true) + "-header></" + projectName.Substring(0, 3).ToUrl(true) + "-header>");
                    yaz.WriteLine("<router-outlet></router-outlet>");
                    yaz.WriteLine("<" + projectName.Substring(0, 3).ToUrl(true) + "-footer></" + projectName.Substring(0, 3).ToUrl(true) + "-footer>");
                    yaz.WriteLine("<" + projectName.Substring(0, 3).ToUrl(true) + "-scripts></" + projectName.Substring(0, 3).ToUrl(true) + "-scripts>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared\\layout.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: '" + projectName.Substring(0, 3).ToUrl(true) + "-layout',");
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

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared\\controls\\header.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<ul class=\"menu\">");
                    yaz.WriteLine("\t<li><a data-url=\"Index\" routerLink=\"/\">Ana Sayfa</a></li>");
                    yaz.WriteLine("</ul>");

                    if (hasLangs)
                    {
                        yaz.WriteLine("<br />");
                        yaz.WriteLine("<br />");
                        yaz.WriteLine("<img src=\"/" + projectName + "/Uploads/{{ flag?.Flag }}\" /> {{ flag?.ShortName }}");
                        yaz.WriteLine("<ul>");
                        yaz.WriteLine("\t<li *ngFor=\"let item of LangList\">");
                        yaz.WriteLine("\t\t<a href=\"javascript:;\" (click)=\"OnLangSelect(item?.ID)\">");
                        yaz.WriteLine("\t\t\t<img src=\"/" + projectName + "/Uploads/{{ item?.Flag }}\" /> {{ item?.ShortName }}");
                        yaz.WriteLine("\t\t</a>");
                        yaz.WriteLine("\t</li>");
                        yaz.WriteLine("</ul>");
                    }

                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared\\controls\\header.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");

                    if (hasLangs)
                    {
                        yaz.WriteLine("import { SiteService } from '../../../services/site';");
                    }

                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: '" + projectName.Substring(0, 3).ToUrl(true) + "-header',");
                    yaz.WriteLine("\ttemplateUrl: './header.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");

                    yaz.WriteLine("export class HeaderComponent {");

                    if (hasLangs)
                    {
                        yaz.WriteLine("\terrorMsg: string;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tflag: any = {};");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tLangList: any;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tconstructor(private service: SiteService) {");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\tngOnInit() {");

                    if (hasLangs)
                    {
                        yaz.WriteLine("\t\tthis.GetLangs();");
                    }

                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");

                    if (hasLangs)
                    {
                        yaz.WriteLine("\t//Translation");
                        yaz.WriteLine("\tGetLangs() {");
                        yaz.WriteLine("\t\tthis.service.get(\"Site\", \"GetLangs\").subscribe((resData: any) => {");
                        yaz.WriteLine("\t\t\tthis.LangList = resData;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tthis.service.get(\"Site\", \"SelectedLang\").subscribe((resData: any) => {");
                        yaz.WriteLine("\t\t\t\tthis.flag = resData;");
                        yaz.WriteLine("\t\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tOnLangSelect(id) {");
                        yaz.WriteLine("\t\tthis.service.get(\"Site\", \"SelectLang\", id).subscribe((resData: any) => {");
                        yaz.WriteLine("\t\t\tif (resData == true) {");
                        yaz.WriteLine("\t\t\t\twindow.location.reload();");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t}");
                    }

                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared\\controls\\footer.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    if (hasVisitors)
                    {
                        yaz.WriteLine("<br />");
                        yaz.WriteLine("<br />");
                        yaz.WriteLine("<span><b>Ziyaretçi Sayısı : </b>{{ visitorCount }}</span>");
                    }

                    yaz.WriteLine("<br />");
                    yaz.WriteLine("<br />");
                    yaz.WriteLine("<ul class=\"menu\">");
                    yaz.WriteLine("\t<li><a data-url=\"Index\" routerLink=\"/\">Ana Sayfa</a></li>");
                    yaz.WriteLine("</ul>");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared\\controls\\footer.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");

                    if (hasVisitors)
                    {
                        yaz.WriteLine("import { SiteService } from '../../../services/site';");
                    }

                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: '" + projectName.Substring(0, 3).ToUrl(true) + "-footer',");
                    yaz.WriteLine("\ttemplateUrl: './footer.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class FooterComponent {");

                    if (hasVisitors)
                    {
                        yaz.WriteLine("\terrorMsg: string;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tvisitorCount: string;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tconstructor(private service: SiteService) {");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\tngOnInit() {");

                    if (hasVisitors)
                    {
                        yaz.WriteLine("\t\tthis.VisitorCount();");
                    }

                    yaz.WriteLine("\t}");

                    if (hasVisitors)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\tVisitorCount() {");
                        yaz.WriteLine("\t\tthis.service.get(\"Site\", \"VisitorCount\").subscribe((resData: any) => {");
                        yaz.WriteLine("\t\t\tthis.visitorCount = resData;");
                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t}");
                    }

                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\shared\\controls\\scripts.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component, ViewEncapsulation } from '@angular/core';");
                    yaz.WriteLine("import { Router, RouterEvent, ActivationEnd } from '@angular/router';");
                    yaz.WriteLine("import { Lib } from '../../../lib/lib';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import '../../../../../Content/js/pathscript.js';");
                    yaz.WriteLine("import '../../../../../Content/js/main.js';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: '" + projectName.Substring(0, 3).ToUrl(true) + "-scripts',");
                    yaz.WriteLine("\ttemplate: '',");
                    yaz.WriteLine("\tstyleUrls: [");
                    yaz.WriteLine("\t\t'../../../../../Content/css/main.css'");
                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\tencapsulation: ViewEncapsulation.None");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class ScriptsComponent {");
                    yaz.WriteLine("\tconstructor(private router: Router) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tthis.LoadScripts();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tthis.router.events.subscribe((event: RouterEvent) => {");
                    yaz.WriteLine("\t\t\tif (event instanceof ActivationEnd) {");
                    yaz.WriteLine("\t\t\t\tthis.LoadScripts();");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tLoadScripts() {");
                    yaz.WriteLine("\t\tLib.LinkActivation();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\home\\index.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("Giriş Sayfası");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\views\\home\\index.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component } from '@angular/core';");

                    if (hasLangs)
                    {
                        yaz.WriteLine("import { SiteService } from '../../services/site';");
                        yaz.WriteLine("import { LangItem } from '../../models/LangItem';");
                        yaz.WriteLine("import { Lib } from '../../lib/lib';");
                    }

                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\ttemplateUrl: './index.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class IndexComponent {");

                    if (hasLangs)
                    {
                        yaz.WriteLine("\terrorMsg: string;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tconstructor(private service: SiteService) {");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\tngOnInit() {");

                    if (hasLangs)
                    {
                        yaz.WriteLine("\t\tthis.SetLangContents();");
                    }

                    yaz.WriteLine("\t}");

                    if (hasLangs)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\t//LangContents");
                        yaz.WriteLine("\tlangItems: Array<LangItem>;");
                        yaz.WriteLine("\tlangItem: LangItem;");
                        yaz.WriteLine("\tlangs: any;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t//LangContent");
                        yaz.WriteLine("\tSetLangContents() {");
                        yaz.WriteLine("\t\tthis.PushLangItems();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tthis.service.post(\"Site\", \"SetLangContents\", this.langItems).subscribe((resData: any) => {");
                        yaz.WriteLine("\t\t\tthis.langs = new Object();");
                        yaz.WriteLine("\t\t\tthis.langs.code2 = new Object();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tresData.forEach((item, i) => {");
                        yaz.WriteLine("\t\t\t\tswitch (item.Code) {");
                        yaz.WriteLine("\t\t\t\t\tcase \"code\": this.langs.code = item.ShortDescription; break;");
                        yaz.WriteLine("\t\t\t\t\tcase \"code2\":");
                        yaz.WriteLine("\t\t\t\t\t\tswitch (item.ShortCode) {");
                        yaz.WriteLine("\t\t\t\t\t\t\tcase \"scode\": this.langs.code2.scode = item.Description; break;");
                        yaz.WriteLine("\t\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\t\tbreak;");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t});");
                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tPushLangItems() {");
                        yaz.WriteLine("\t\tthis.langItems = new Array<LangItem>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tthis.langItems.push(Lib.SetLangItem(this.langItem, \"code\"));");
                        yaz.WriteLine("\t\tthis.langItems.push(Lib.SetLangItem(this.langItem, \"code2\", \"scode\"));");
                        yaz.WriteLine("\t}");
                    }

                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\lib\\lib.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Injectable } from '@angular/core';");
                    yaz.WriteLine("import { Router } from '@angular/router';");

                    if (hasLangs)
                    {
                        yaz.WriteLine("import { LangItem } from '../models/LangItem';");
                    }

                    yaz.WriteLine("");
                    yaz.WriteLine("@Injectable({ providedIn: 'root' })");
                    yaz.WriteLine("export class Lib {");
                    yaz.WriteLine("\tstatic RefreshRoute(router: Router, changeUrl: string = \"/\", skipChangeLocation: boolean = true) {");
                    yaz.WriteLine("\t\tlet currentUrl = router.url;");
                    yaz.WriteLine("\t\trouter.navigate([changeUrl], { skipLocationChange: skipChangeLocation }).then(() => { router.navigate([currentUrl]) });");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tstatic ParseFloat(value: string) : any {");
                    yaz.WriteLine("\t\tvar returnValue;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (value == null || value == undefined) {");
                    yaz.WriteLine("\t\t\treturnValue = null;");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t\telse if (value.toString().indexOf(',') > 0) {");
                    yaz.WriteLine("\t\t\treturnValue = parseFloat(value.toString().replace(\",\", \".\"));");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t\telse {");
                    yaz.WriteLine("\t\t\treturnValue = parseFloat(value);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\treturn returnValue;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t//LinkActivation");
                    yaz.WriteLine("\tstatic LinkActivation() {");
                    yaz.WriteLine("\t\t$(\"#hdnUrl\").val(location.href);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tvar MainPath = \"http://localhost/" + projectName + "\";");
                    yaz.WriteLine("\t\tvar Url = location.href;");
                    yaz.WriteLine("\t\tvar Urling = Object();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (Url != undefined) {");
                    yaz.WriteLine("\t\t\tvar tempurl = Url.replace(MainPath + \"/\", \"\");");
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
                    yaz.WriteLine("\t\t$(\"ul.menu li a\").removeClass(\"active\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (Urling.controller != \"\") {");
                    yaz.WriteLine("\t\t\t$(\"ul.menu li a[data-url='\" + Urling.controller + \"']\").addClass(\"active\");");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t\telse {");
                    yaz.WriteLine("\t\t\t$(\"ul.menu li a[data-url='Index']\").addClass(\"active\");");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");

                    if (hasLangs)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\tstatic SetLangItem(langItem: LangItem, code: string = null, shortCode: string = null) {");
                        yaz.WriteLine("\t\tlangItem = new Object() as LangItem;");
                        yaz.WriteLine("\t\tlangItem.Code = code;");
                        yaz.WriteLine("\t\tlangItem.ShortCode = shortCode;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn langItem;");
                        yaz.WriteLine("\t}");
                    }

                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            CreateAngularAdminFiles();
        }

        void CreateAngularAdminFiles()
        {
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

                    string username = hasUserRights ? "user?.Username" : "username";
                    string userlink = hasUserRights ? "/Admin/Users/Update/{{ user?.ID }}" : "/Admin/Index";

                    yaz.WriteLine("\t\t<li><a title=\"Bilgilerinizi düzenlemek için tıklayın.\" routerLink=\"" + userlink + "\"><i class=\"icon icon-user\"></i> <span class=\"text\"> Hoşgeldiniz ({{ " + username + " }})</span></a></li>");
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
                    yaz.WriteLine("\ttemplateUrl: './header.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminHeaderComponent {");
                    yaz.WriteLine("\terrorMsg: string;");
                    yaz.WriteLine("\twebsite: string = \"http://localhost/" + projectName + "/\";");

                    if (hasUserRights)
                        yaz.WriteLine("\tuser: any;");
                    else
                        yaz.WriteLine("\tusername: string;");

                    yaz.WriteLine("");
                    yaz.WriteLine("\tconstructor(private service: SharedService, private router: Router) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t\tthis.service.getCurrentUser().subscribe((answer: any) => {");
                    yaz.WriteLine("\t\t\tif (answer != null) {");

                    if (hasUserRights)
                        yaz.WriteLine("\t\t\t\tthis.user = answer;");
                    else
                        yaz.WriteLine("\t\t\t\tthis.username = answer;");

                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t$('#txtMainSearch').typeahead({");
                    yaz.WriteLine("\t\t\tsource: [");

                    int i = 0;
                    foreach (string Table in selectedTables)
                    {
                        if (selectedTables.Count == i + 1)
                        {
                            yaz.WriteLine("\t\t\t\t'" + Table.ToTurkish(lstLang) + "'");
                        }
                        else
                        {
                            yaz.WriteLine("\t\t\t\t'" + Table.ToTurkish(lstLang) + "',");
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
                        yaz.WriteLine("\t\t\tcase \"" + Table.ToTurkish(lstLang) + "\":");
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
                    yaz.WriteLine("\t\tthis.service.getLogout().subscribe((answer: any) => {");
                    yaz.WriteLine("\t\t\tif (answer == true) {");
                    yaz.WriteLine("\t\t\t\tthis.router.navigate(['/Admin/Login']);");
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
                    yaz.WriteLine("\t\t<li data-url=\"Index\" class=\"active\">");
                    yaz.WriteLine("\t\t\t<a routerLink=\"/Admin/Index\"><i class=\"icon icon-home\"></i> <span>Ana Sayfa</span></a>");
                    yaz.WriteLine("\t\t</li>");

                    List<string> addedTables = new List<string>();

                    foreach (AdminMenu item in adminMenu)
                    {
                        string userRightControl = hasUserRights ? " *ngIf =\"hasRight" + item.Title.ToUserRightType() + " && showType" + item.Title.ToUserRightType() + "\"" : "";

                        if (item.SubMenu.Count <= 0)
                        {
                            yaz.WriteLine("\t\t<li data-url=\"" + item.Title + "\"" + userRightControl + ">");
                            yaz.WriteLine("\t\t\t<a routerLink=\"/Admin/" + item.Title + "\"><i class=\"icon " + item.Title.ToIcon() + "\"></i> <span>" + item.Title.ToTurkish(lstLang) + "</span></a>");
                            yaz.WriteLine("\t\t</li>");
                        }
                        else
                        {
                            yaz.WriteLine("\t\t<li class=\"submenu\"" + userRightControl + ">");
                            yaz.WriteLine("\t\t\t<a href=\"javascript:;\"><i class=\"icon " + item.Title.ToIcon() + "\"></i> <span>" + item.Title.ToTurkish(lstLang) + "</span></a>");
                            yaz.WriteLine("\t\t\t<ul>");
                            yaz.WriteLine("\t\t\t\t<li data-url=\"" + item.Title + "\"><a routerLink=\"/Admin/" + item.Title + "\">" + item.Title.ToTurkish(lstLang) + "</a></li>");

                            foreach (AdminMenu subItem in item.SubMenu)
                            {
                                yaz.WriteLine("\t\t\t\t<li data-url=\"" + subItem.Title + "\"><a routerLink=\"/Admin/" + subItem.Title + "\">" + subItem.Title.ToTurkish(lstLang) + "</a></li>");
                            }

                            yaz.WriteLine("\t\t\t</ul>");
                            yaz.WriteLine("\t\t</li>");
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
                    yaz.WriteLine("import { Component, AfterViewInit } from '@angular/core';");
                    yaz.WriteLine("import { Router, RouterEvent, ActivationEnd } from '@angular/router';");

                    if (hasUserRights)
                        yaz.WriteLine("import { SharedService } from '../../../services/shared';");

                    yaz.WriteLine("import { AdminLib } from '../../../lib/lib';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: 'admin-leftmenu',");
                    yaz.WriteLine("\ttemplateUrl: './leftmenu.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminLeftMenuComponent implements AfterViewInit {");
                    yaz.WriteLine("\terrorMsg: string;");
                    yaz.WriteLine("");

                    string sharedService = hasUserRights ? "private sharedService: SharedService, " : "";

                    yaz.WriteLine("\tconstructor(" + sharedService + "private router: Router) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tngAfterViewInit() {");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("\t\tthis.HasRightShowTypeControl();");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\t\tthis.router.events.subscribe((event: RouterEvent) => {");
                    yaz.WriteLine("\t\t\tif (event instanceof ActivationEnd) {");
                    yaz.WriteLine("\t\t\t\tAdminLib.LinkActivation();");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("\t}");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\thasRightCategory: boolean = false;");
                        yaz.WriteLine("\thasRightContent: boolean = false;");
                        yaz.WriteLine("\thasRightProduct: boolean = false;");
                        yaz.WriteLine("\thasRightGallery: boolean = false;");
                        yaz.WriteLine("\thasRightPictures: boolean = false;");
                        yaz.WriteLine("\thasRightFiles: boolean = false;");
                        yaz.WriteLine("\thasRightMeta: boolean = false;");
                        yaz.WriteLine("\thasRightLinkTypes: boolean = false;");
                        yaz.WriteLine("\thasRightTranslation: boolean = false;");
                        yaz.WriteLine("\thasRightLogs: boolean = false;");
                        yaz.WriteLine("\thasRightWebsite: boolean = false;");
                        yaz.WriteLine("\thasRightUsers: boolean = false;");
                        yaz.WriteLine("\thasRightTypes: boolean = false;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tshowTypeCategory: boolean = false;");
                        yaz.WriteLine("\tshowTypeContent: boolean = false;");
                        yaz.WriteLine("\tshowTypeProduct: boolean = false;");
                        yaz.WriteLine("\tshowTypeGallery: boolean = false;");
                        yaz.WriteLine("\tshowTypePictures: boolean = false;");
                        yaz.WriteLine("\tshowTypeFiles: boolean = false;");
                        yaz.WriteLine("\tshowTypeMeta: boolean = false;");
                        yaz.WriteLine("\tshowTypeLinkTypes: boolean = false;");
                        yaz.WriteLine("\tshowTypeTranslation: boolean = false;");
                        yaz.WriteLine("\tshowTypeLogs: boolean = false;");
                        yaz.WriteLine("\tshowTypeWebsite: boolean = false;");
                        yaz.WriteLine("\tshowTypeUsers: boolean = false;");
                        yaz.WriteLine("\tshowTypeTypes: boolean = false;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tHasRightShowTypeControl() {");
                        yaz.WriteLine("\t\tthis.sharedService.getCurrentUserRights().subscribe((userRights: any) => {");
                        yaz.WriteLine("\t\t\tuserRights.forEach((item, i) => {");
                        yaz.WriteLine("\t\t\t\tthis.hasRightCategory = AdminLib.UserRight(userRights, \"Category\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightContent = AdminLib.UserRight(userRights, \"Content\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightProduct = AdminLib.UserRight(userRights, \"Product\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightGallery = AdminLib.UserRight(userRights, \"Gallery\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightPictures = AdminLib.UserRight(userRights, \"Pictures\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightFiles = AdminLib.UserRight(userRights, \"Files\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightMeta = AdminLib.UserRight(userRights, \"Meta\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightLinkTypes = AdminLib.UserRight(userRights, \"LinkTypes\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightTranslation = AdminLib.UserRight(userRights, \"Translation\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightLogs = AdminLib.UserRight(userRights, \"Logs\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightUsers = AdminLib.UserRight(userRights, \"Users\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightTypes = AdminLib.UserRight(userRights, \"Types\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightWebsite = AdminLib.UserRight(userRights, \"Website\");");
                        yaz.WriteLine("\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tthis.sharedService.getShowTypes().subscribe((showTypes: any) => {");
                        yaz.WriteLine("\t\t\t\tthis.showTypeCategory = AdminLib.ShowType(showTypes, \"Category\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeContent = AdminLib.ShowType(showTypes, \"Content\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeProduct = AdminLib.ShowType(showTypes, \"Product\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeGallery = AdminLib.ShowType(showTypes, \"Gallery\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypePictures = AdminLib.ShowType(showTypes, \"Pictures\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeFiles = AdminLib.ShowType(showTypes, \"Files\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeMeta = AdminLib.ShowType(showTypes, \"Meta\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeLinkTypes = AdminLib.ShowType(showTypes, \"LinkTypes\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeTranslation = AdminLib.ShowType(showTypes, \"Translation\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeLogs = AdminLib.ShowType(showTypes, \"Logs\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeUsers = AdminLib.ShowType(showTypes, \"Users\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeTypes = AdminLib.ShowType(showTypes, \"Types\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeWebsite = AdminLib.ShowType(showTypes, \"Website\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tAdminLib.LinkActivation();");
                        yaz.WriteLine("\t\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t}");
                    }

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

                    string[] colors = new string[] { "bg_lg", "bg_lb", "bg_ly", "bg_lo", "bg_ls", "bg_lr", "bg_lv" };
                    int i = 0;

                    foreach (AdminMenu item in adminMenu)
                    {
                        string userRightControl = hasUserRights ? " *ngIf =\"hasRight" + item.Title.ToUserRightType() + " && showType" + item.Title.ToUserRightType() + "\"" : "";

                        yaz.WriteLine("\t\t\t\t<li class=\"" + colors[i % 7] + "\"" + userRightControl + "> <a routerLink=\"/Admin/" + item.Title + "\"> <i class=\"" + item.Title.ToIcon() + "\"></i> " + item.Title.ToTurkish(lstLang) + "</a> </li>");
                        i++;
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

                    if (hasUserRights)
                    {
                        yaz.WriteLine("import { SharedService } from '../../services/shared';");
                        yaz.WriteLine("import { AdminLib } from '../../lib/lib';");
                    }

                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\ttemplateUrl: './index.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminIndexComponent {");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("\terrorMsg: string;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tconstructor(private sharedService: SharedService) {");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\tngOnInit() {");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("\t\tthis.HasRightShowTypeControl();");
                    }

                    yaz.WriteLine("\t}");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\thasRightCategory: boolean = false;");
                        yaz.WriteLine("\thasRightContent: boolean = false;");
                        yaz.WriteLine("\thasRightProduct: boolean = false;");
                        yaz.WriteLine("\thasRightGallery: boolean = false;");
                        yaz.WriteLine("\thasRightPictures: boolean = false;");
                        yaz.WriteLine("\thasRightFiles: boolean = false;");
                        yaz.WriteLine("\thasRightMeta: boolean = false;");
                        yaz.WriteLine("\thasRightLinkTypes: boolean = false;");
                        yaz.WriteLine("\thasRightTranslation: boolean = false;");
                        yaz.WriteLine("\thasRightLogs: boolean = false;");
                        yaz.WriteLine("\thasRightWebsite: boolean = false;");
                        yaz.WriteLine("\thasRightUsers: boolean = false;");
                        yaz.WriteLine("\thasRightTypes: boolean = false;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tshowTypeCategory: boolean = false;");
                        yaz.WriteLine("\tshowTypeContent: boolean = false;");
                        yaz.WriteLine("\tshowTypeProduct: boolean = false;");
                        yaz.WriteLine("\tshowTypeGallery: boolean = false;");
                        yaz.WriteLine("\tshowTypePictures: boolean = false;");
                        yaz.WriteLine("\tshowTypeFiles: boolean = false;");
                        yaz.WriteLine("\tshowTypeMeta: boolean = false;");
                        yaz.WriteLine("\tshowTypeLinkTypes: boolean = false;");
                        yaz.WriteLine("\tshowTypeTranslation: boolean = false;");
                        yaz.WriteLine("\tshowTypeLogs: boolean = false;");
                        yaz.WriteLine("\tshowTypeWebsite: boolean = false;");
                        yaz.WriteLine("\tshowTypeUsers: boolean = false;");
                        yaz.WriteLine("\tshowTypeTypes: boolean = false;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tHasRightShowTypeControl() {");
                        yaz.WriteLine("\t\tthis.sharedService.getCurrentUserRights().subscribe((userRights: any) => {");
                        yaz.WriteLine("\t\t\tuserRights.forEach((item, i) => {");
                        yaz.WriteLine("\t\t\t\tthis.hasRightCategory = AdminLib.UserRight(userRights, \"Category\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightContent = AdminLib.UserRight(userRights, \"Content\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightProduct = AdminLib.UserRight(userRights, \"Product\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightGallery = AdminLib.UserRight(userRights, \"Gallery\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightPictures = AdminLib.UserRight(userRights, \"Pictures\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightFiles = AdminLib.UserRight(userRights, \"Files\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightMeta = AdminLib.UserRight(userRights, \"Meta\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightLinkTypes = AdminLib.UserRight(userRights, \"LinkTypes\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightTranslation = AdminLib.UserRight(userRights, \"Translation\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightLogs = AdminLib.UserRight(userRights, \"Logs\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightUsers = AdminLib.UserRight(userRights, \"Users\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightTypes = AdminLib.UserRight(userRights, \"Types\");");
                        yaz.WriteLine("\t\t\t\tthis.hasRightWebsite = AdminLib.UserRight(userRights, \"Website\");");
                        yaz.WriteLine("\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tthis.sharedService.getShowTypes().subscribe((showTypes: any) => {");
                        yaz.WriteLine("\t\t\t\tthis.showTypeCategory = AdminLib.ShowType(showTypes, \"Category\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeContent = AdminLib.ShowType(showTypes, \"Content\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeProduct = AdminLib.ShowType(showTypes, \"Product\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeGallery = AdminLib.ShowType(showTypes, \"Gallery\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypePictures = AdminLib.ShowType(showTypes, \"Pictures\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeFiles = AdminLib.ShowType(showTypes, \"Files\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeMeta = AdminLib.ShowType(showTypes, \"Meta\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeLinkTypes = AdminLib.ShowType(showTypes, \"LinkTypes\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeTranslation = AdminLib.ShowType(showTypes, \"Translation\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeLogs = AdminLib.ShowType(showTypes, \"Logs\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeUsers = AdminLib.ShowType(showTypes, \"Users\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeTypes = AdminLib.ShowType(showTypes, \"Types\");");
                        yaz.WriteLine("\t\t\t\tthis.showTypeWebsite = AdminLib.ShowType(showTypes, \"Website\");");
                        yaz.WriteLine("\t\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                        yaz.WriteLine("\t}");
                    }

                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\lib\\lib.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Injectable } from '@angular/core';");
                    yaz.WriteLine("import { Router } from '@angular/router';");
                    yaz.WriteLine("import ClassicEditor from '../../../../Content/admin/js/ckeditor/ckeditor.js';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Injectable({ providedIn: 'root' })");
                    yaz.WriteLine("export class AdminLib {");
                    yaz.WriteLine("\tstatic RefreshRoute(router: Router, changeUrl: string = \"/\", skipChangeLocation: boolean = true) {");
                    yaz.WriteLine("\t\tlet currentUrl = router.url;");
                    yaz.WriteLine("\t\trouter.navigate([changeUrl], { skipLocationChange: skipChangeLocation }).then(() => { router.navigate([currentUrl]) });");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tstatic ParseFloat(value: string) : any {");
                    yaz.WriteLine("\t\tvar returnValue;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (value == null || value == undefined) {");
                    yaz.WriteLine("\t\t\treturnValue = null;");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t\telse if (value.toString().indexOf(',') > 0) {");
                    yaz.WriteLine("\t\t\treturnValue = parseFloat(value.toString().replace(\",\", \".\"));");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t\telse {");
                    yaz.WriteLine("\t\t\treturnValue = parseFloat(value);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\treturn returnValue;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tstatic UploadFileName(filename: string, guidCount: number = 5) {");
                    yaz.WriteLine("\t\tlet x: string = \"\";");
                    yaz.WriteLine("\t\tlet ext: string = filename.split('.').pop();");
                    yaz.WriteLine("\t\tlet name: string = filename.replace(\".\" + ext, \"\");");
                    yaz.WriteLine("\t\tlet guid: string;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tfor (var i = 0; i < guidCount; i++) {");
                    yaz.WriteLine("\t\t\tx += \"x\";");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tguid = x.replace(/[x]/g, function (c) {");
                    yaz.WriteLine("\t\t\tvar r = Math.random() * 16 | 0,");
                    yaz.WriteLine("\t\t\t\tv = c == 'x' ? r : (r & 0x3 | 0x8);");
                    yaz.WriteLine("\t\t\treturn v.toString(16);");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\treturn name + \"-\" + guid + \".\" + ext;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tstatic CKValue(id: string) {");
                    yaz.WriteLine("\tif ($(\".ck-content[data-id='\" + id + \"']\").html() == null)");
                    yaz.WriteLine("\t\treturn \"\";");
                    yaz.WriteLine("\telse");
                    yaz.WriteLine("\t\treturn $(\".ck-content[data-id='\" + id + \"']\").html().replace(\"<p>\", \"\").replace(\"</p>\", \"\");");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tstatic ConvertToCKEditor(id: string, time: number = 1000) {");
                    yaz.WriteLine("\t\tsetTimeout(function () {");
                    yaz.WriteLine("\t\t\tClassicEditor");
                    yaz.WriteLine("\t\t\t\t.create(document.querySelector(\"#\" + id), {");
                    yaz.WriteLine("\t\t\t\t})");
                    yaz.WriteLine("\t\t\t\t.then(editor => {");
                    yaz.WriteLine("\t\t\t\t\tconsole.log(editor);");
                    yaz.WriteLine("\t\t\t\t});");
                    yaz.WriteLine("\t\t}, time);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tstatic UserRight(userRights: any, model: string, shortname: string = \"s\"): boolean {");
                    yaz.WriteLine("\t\tlet returnItem: boolean = false;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tuserRights.forEach((item, i) => {");
                    yaz.WriteLine("\t\t\tif (item.Url == model) {");
                    yaz.WriteLine("\t\t\t\tif (item.ShortName == shortname) {");
                    yaz.WriteLine("\t\t\t\t\treturnItem = true;");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\treturn returnItem;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tstatic ShowType(showTypes: any, model: string): boolean {");
                    yaz.WriteLine("\t\tlet returnItem: boolean = false;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tshowTypes.forEach((item, i) => {");
                    yaz.WriteLine("\t\t\tif (item.Url == model) {");
                    yaz.WriteLine("\t\t\t\treturnItem = true;");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("\t\treturn returnItem;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tstatic LinkActivation() {");
                    yaz.WriteLine("\t\tsetTimeout(function () {");
                    yaz.WriteLine("\t\t\t$(\"#hdnUrl\").val(location.href);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tvar AdminPath = \"http://localhost/" + projectName + "/Admin\";");
                    yaz.WriteLine("\t\t\tvar Url = location.href;");
                    yaz.WriteLine("\t\t\tvar Urling = Object();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tif (Url != undefined) {");
                    yaz.WriteLine("\t\t\t\tvar tempurl = Url.replace(AdminPath + \"/\", \"\");");
                    yaz.WriteLine("\t\t\t\tvar extParams = tempurl.split('?')[1];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\ttempurl = tempurl.replace(\"?\" + extParams, \"\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tUrling.path = tempurl;");
                    yaz.WriteLine("\t\t\t\tUrling.controller = tempurl.split('/')[0];");
                    yaz.WriteLine("\t\t\t\tUrling.action = tempurl.split('/')[1];");
                    yaz.WriteLine("\t\t\t\tUrling.parameter = tempurl.split('/')[2];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tif (extParams != undefined)");
                    yaz.WriteLine("\t\t\t\t\tUrling.parameters = extParams.split('&');");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tif (Urling.controller != undefined) {");
                    yaz.WriteLine("\t\t\t\tvar activeLi = $(\"#sidebar li[data-url='\" + Urling.controller + \"']\");");
                    yaz.WriteLine("\t\t\t\tvar passiveSubmenuLi = $(\"#sidebar li.submenu\");");
                    yaz.WriteLine("\t\t\t\tvar submenuLi = activeLi.parent(\"ul\").parent(\"li\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\t$(\"#sidebar li\").removeClass(\"active\");");
                    yaz.WriteLine("\t\t\t\t$(\"#sidebar li\").removeClass(\"open\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tactiveLi.addClass(\"active\");");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tif (submenuLi.hasClass(\"submenu\")) {");
                    yaz.WriteLine("\t\t\t\t\tif ($(\"body\").width() > 970 || $(\"body\").width() <= 480) {");
                    yaz.WriteLine("\t\t\t\t\t\tsubmenuLi.addClass(\"open\");");
                    yaz.WriteLine("\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t\tsubmenuLi.addClass(\"active\");");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tpassiveSubmenuLi.each(function () {");
                    yaz.WriteLine("\t\t\t\t\tif (!$(this).hasClass(\"open\")) {");
                    yaz.WriteLine("\t\t\t\t\t\t$(this).children(\"ul\").slideUp();");
                    yaz.WriteLine("\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t\telse {");
                    yaz.WriteLine("\t\t\t\t\t\t$(this).children(\"ul\").slideDown();");
                    yaz.WriteLine("\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t});");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}, 100);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\shared\\controls\\scripts.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component, ViewEncapsulation, NgZone } from '@angular/core';");
                    yaz.WriteLine("import { Router } from '@angular/router';");
                    yaz.WriteLine("import { ModelService } from '../../../services/model';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/jquery.dataTables.min.js';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/bootstrap.min.js';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/matrix.js';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/ckeditor/ckeditor.js';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/pathscript.js';");
                    yaz.WriteLine("import '../../../../../../Content/admin/js/main.js';");
                    yaz.WriteLine("import * as $ from 'jquery';");
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
                    yaz.WriteLine("\terrorMsg: string;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate zone = new NgZone({});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tconstructor(private service: ModelService, private router: Router) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t\t$(document).off(\"keyup\", \".dataTables_filter label input[type='text']\")");
                    yaz.WriteLine("\t\t\t.on(\"keyup\", \".dataTables_filter label input[type='text']\", function () {");
                    yaz.WriteLine("\t\t\t\tif ($(\".dropdown-menu\").first().find(\"a\").length <= 0) {");
                    yaz.WriteLine("\t\t\t\t\t$(\".btn-group\").remove();");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t$(document).off(\"click\", \"a.dltLink\")");
                    yaz.WriteLine("\t\t\t.on(\"click\", \"a.dltLink\", function () {");
                    yaz.WriteLine("\t\t\t\t$(this).addClass(\"active-dlt\");");
                    yaz.WriteLine("\t\t\t\t$(\"a.dlt-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                    yaz.WriteLine("\t\t\t\t$(\"a.dlt-yes\").attr(\"data-controller\", $(this).attr(\"data-controller\"));");
                    yaz.WriteLine("\t\t\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t$(document).off(\"click\", \"a.rmvLink\")");
                    yaz.WriteLine("\t\t\t.on(\"click\", \"a.rmvLink\", function () {");
                    yaz.WriteLine("\t\t\t\t$(this).addClass(\"active-rmv\");");
                    yaz.WriteLine("\t\t\t\t$(\"a.rmv-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                    yaz.WriteLine("\t\t\t\t$(\"a.rmv-yes\").attr(\"data-controller\", $(this).attr(\"data-controller\"));");
                    yaz.WriteLine("\t\t\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t$(document).off(\"click\", \"a.cpyLink\")");
                    yaz.WriteLine("\t\t\t.on(\"click\", \"a.cpyLink\", function () {");
                    yaz.WriteLine("\t\t\t\t$(this).addClass(\"active-cpy\");");
                    yaz.WriteLine("\t\t\t\t$(\"a.cpy-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                    yaz.WriteLine("\t\t\t\t$(\"a.cpy-yes\").attr(\"data-controller\", $(this).attr(\"data-controller\"));");
                    yaz.WriteLine("\t\t\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t$(document).off(\"click\", \"a.clrLink\")");
                    yaz.WriteLine("\t\t\t.on(\"click\", \"a.clrLink\", function () {");
                    yaz.WriteLine("\t\t\t\t$(this).addClass(\"active-clr\");");
                    yaz.WriteLine("\t\t\t\t$(\"a.clr-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                    yaz.WriteLine("\t\t\t\t$(\"a.clr-yes\").attr(\"data-controller\", $(this).attr(\"data-controller\"));");
                    yaz.WriteLine("\t\t\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t$(document).off(\"click\", \"a.dlt-yes\").on(\"click\", \"a.dlt-yes\", () => {");
                    yaz.WriteLine("\t\t\tlet id: string = $(\"a.dlt-yes\").attr(\"data-id\");");
                    yaz.WriteLine("\t\t\tlet controller: string = $(\"a.dlt-yes\").attr(\"data-controller\");");
                    yaz.WriteLine("\t\t\tthis.onAction(controller, \"Delete\", id);");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t$(document).off(\"click\", \"a.rmv-yes\").on(\"click\", \"a.rmv-yes\", () => {");
                    yaz.WriteLine("\t\t\tlet id: string = $(\"a.rmv-yes\").attr(\"data-id\");");
                    yaz.WriteLine("\t\t\tlet controller: string = $(\"a.rmv-yes\").attr(\"data-controller\");");
                    yaz.WriteLine("\t\t\tthis.onAction(controller, \"Remove\", id);");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t$(document).off(\"click\", \"a.cpy-yes\").on(\"click\", \"a.cpy-yes\", () => {");
                    yaz.WriteLine("\t\t\tlet id: string = $(\"a.cpy-yes\").attr(\"data-id\");");
                    yaz.WriteLine("\t\t\tlet controller: string = $(\"a.cpy-yes\").attr(\"data-controller\");");
                    yaz.WriteLine("\t\t\tthis.onAction(controller, \"Copy\", id);");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t$(document).off(\"click\", \"a.clr-yes\").on(\"click\", \"a.clr-yes\", () => {");
                    yaz.WriteLine("\t\t\tlet id: string = $(\"a.clr-yes\").attr(\"data-id\");");
                    yaz.WriteLine("\t\t\tlet controller: string = $(\"a.clr-yes\").attr(\"data-controller\");");
                    yaz.WriteLine("\t\t\tthis.onAction(controller, \"Clear\", id);");
                    yaz.WriteLine("\t\t});");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tonAction(controller: any, action: string, id: string) {");
                    yaz.WriteLine("\t\tthis.service.get(controller, action, id).subscribe((answer: boolean) => {");
                    yaz.WriteLine("\t\t\tif (answer) {");
                    yaz.WriteLine("\t\t\t\tShowAlert(action);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tif (action == \"Copy\" || action == \"Clear\") {");
                    yaz.WriteLine("\t\t\t\t\tlet currentUrl = this.router.url;");
                    yaz.WriteLine("\t\t\t\t\tthis.zone.run(() => this.router.navigate(['/Admin'], { skipLocationChange: true }).then(() => { this.router.navigate([currentUrl]) }));");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t\telse {");
                    yaz.WriteLine("\t\t\t\tShowAlert(action + \"Not\");");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.WriteLine("");
                    yaz.WriteLine("(window as any).ShowAlert = ShowAlert;");
                    yaz.WriteLine("(window as any).DataTable = DataTable;");
                    yaz.WriteLine("");
                    yaz.WriteLine("function ShowAlert(type: string) {");
                    yaz.WriteLine("\t$(\"#tdAlertMessage li.tdAlert\" + type).fadeIn(\"slow\", function () {");
                    yaz.WriteLine("\t\tswitch (type) {");
                    yaz.WriteLine("\t\t\tcase \"Delete\":");
                    yaz.WriteLine("\t\t\t\t$(\"a.dltLink.active-dlt\").parent(\"li\").parent(\"ul\").parent(\"div\").parent(\"td\").parent(\"tr\").fadeOut(\"slow\", function () {");
                    yaz.WriteLine("\t\t\t\t\t$(this).remove();");
                    yaz.WriteLine("\t\t\t\t});");
                    yaz.WriteLine("\t\t\t\tbreak;");
                    yaz.WriteLine("\t\t\tcase \"Remove\":");
                    yaz.WriteLine("\t\t\t\t$(\"a.rmvLink.active-rmv\").parent(\"li\").parent(\"ul\").parent(\"div\").parent(\"td\").parent(\"tr\").fadeOut(\"slow\", function () {");
                    yaz.WriteLine("\t\t\t\t\t$(this).remove();");
                    yaz.WriteLine("\t\t\t\t});");
                    yaz.WriteLine("\t\t\t\tbreak;");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tsetInterval(function () {");
                    yaz.WriteLine("\t\t$(\"#tdAlertMessage li.tdAlert\" + type).fadeOut(\"slow\");");
                    yaz.WriteLine("\t}, 2000);");
                    yaz.WriteLine("};");
                    yaz.WriteLine("");
                    yaz.WriteLine("function DataTable() {");
                    yaz.WriteLine("\t$(\".data-table\").dataTable({");
                    yaz.WriteLine("\t\t\"bJQueryUI\": true,");
                    yaz.WriteLine("\t\t\"sPaginationType\": \"full_numbers\",");
                    yaz.WriteLine("\t\t\"sDom\": '<\"\"l>t<\"F\"fp>'");
                    yaz.WriteLine("\t});");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tif ($(\".dropdown-menu\").first().find(\"a\").length <= 0) {");
                    yaz.WriteLine("\t\t$(\".btn-group\").remove();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("};");
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
                    yaz.WriteLine("");
                    yaz.WriteLine("<ul id=\"tdAlertMessage\" class=\"tdAlert\">");
                    yaz.WriteLine("\t<li class=\"tdAlertDelete\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veri silindi.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("\t<li class=\"tdAlertDeleteNot\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veri silinemedi.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("\t<li class=\"tdAlertRemove\">");
                    yaz.WriteLine("\t\t<div class=\"tdAlertMessage\">İlgili veri kaldırıldı.</div>");
                    yaz.WriteLine("\t</li>");
                    yaz.WriteLine("\t<li class=\"tdAlertRemoveNot\">");
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
                    yaz.WriteLine("import { Router } from '@angular/router';");
                    yaz.WriteLine("import { SharedService } from '../../services/shared';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: 'admin-layout',");
                    yaz.WriteLine("\ttemplateUrl: './layoutAdmin.html'");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
                    yaz.WriteLine("export class AdminLayoutComponent {");
                    yaz.WriteLine("\terrorMsg: string;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tconstructor(private service: SharedService, private router: Router) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tngOnInit() {");
                    yaz.WriteLine("\t\tthis.service.getLoginControl().subscribe((answer) => {");
                    yaz.WriteLine("\t\t\tif (answer == false) {");
                    yaz.WriteLine("\t\t\t\tthis.router.navigate(['/Admin/Login']);");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
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
                    yaz.WriteLine("import { Component, ViewEncapsulation } from '@angular/core';");
                    yaz.WriteLine("import { Router } from '@angular/router';");
                    yaz.WriteLine("import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';");
                    yaz.WriteLine("import { SharedService } from '../../services/shared.js';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\ttemplateUrl: './login.html',");
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
                    yaz.WriteLine("\t\t\t\t\tthis.router.navigate(['/Admin/Index']);");
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

        void CreateAngularServiceLayer()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\services\\shared.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Injectable } from '@angular/core';");
                    yaz.WriteLine("import { HttpClient, HttpParams } from '@angular/common/http';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Injectable({ providedIn: 'root' })");
                    yaz.WriteLine("export class SharedService {");
                    yaz.WriteLine("\tprivate linkLogin: string = \"Ajax/Shared/Login\";");
                    yaz.WriteLine("\tprivate linkLogout: string = \"Ajax/Shared/Logout\";");
                    yaz.WriteLine("\tprivate linkLoginControl: string = \"Ajax/Shared/LoginControl\";");
                    yaz.WriteLine("\tprivate linkCurrentUser: string = \"Ajax/Shared/CurrentUser\";");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("\tprivate linkCurrentUserRights: string = \"Ajax/Shared/CurrentUserRights\";");
                        yaz.WriteLine("\tprivate linkHasRight: string = \"Ajax/Shared/HasRight\";");
                        yaz.WriteLine("\tprivate linkShowTypes: string = \"Ajax/Shared/ShowTypes\";");
                    }

                    yaz.WriteLine("");
                    yaz.WriteLine("\tconstructor(private http: HttpClient) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpostLogin(user: any) {");
                    yaz.WriteLine("\t\treturn this.http.post(this.linkLogin, user);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tgetLogout() {");
                    yaz.WriteLine("\t\treturn this.http.get(this.linkLogout);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tgetLoginControl() {");
                    yaz.WriteLine("\t\treturn this.http.get(this.linkLoginControl);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tgetCurrentUser() {");
                    yaz.WriteLine("\t\treturn this.http.get(this.linkCurrentUser);");
                    yaz.WriteLine("\t}");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\tgetCurrentUserRights(url: string = null, process: string = null) {");
                        yaz.WriteLine("\t\tlet params = new HttpParams().set(\"url\", url).set(\"process\", process);");
                        yaz.WriteLine("\t\treturn this.http.get(this.linkCurrentUserRights, { params: params });");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tgetHasRight(url: string = null, process: string = null) {");
                        yaz.WriteLine("\t\tlet params = new HttpParams().set(\"url\", url).set(\"process\", process);");
                        yaz.WriteLine("\t\treturn this.http.get(this.linkHasRight, { params: params });");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tgetShowTypes(url: string = null) {");
                        yaz.WriteLine("\t\tlet params = new HttpParams().set(\"url\", url);");
                        yaz.WriteLine("\t\treturn this.http.get(this.linkShowTypes, { params: params });");
                        yaz.WriteLine("\t}");
                    }

                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\services\\model.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Injectable } from '@angular/core';");
                    yaz.WriteLine("import { HttpClient, HttpParams } from '@angular/common/http';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Injectable()");
                    yaz.WriteLine("export class ModelService {");
                    yaz.WriteLine("\tconstructor(private http: HttpClient) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");

                    if (hasLinks)
                    {
                        yaz.WriteLine("\tget(controller: string, action: string, id: string = null, linkid: string = null, linkTypeID: string = null, typeID: string = null) {");
                        yaz.WriteLine("\t\tif (id != null) {");
                        yaz.WriteLine("\t\t\tlet params = new HttpParams().set(\"id\", id);");
                        yaz.WriteLine("\t\t\treturn this.http.get(\"Ajax/\" + controller + \"/\" + action, { params: params });");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\telse if (linkid != null) {");
                        yaz.WriteLine("\t\t\tlet params = new HttpParams().set(\"linkid\", linkid);");
                        yaz.WriteLine("\t\t\treturn this.http.get(\"Ajax/\" + controller + \"/\" + action, { params: params });");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\telse if (linkTypeID != null) {");
                        yaz.WriteLine("\t\t\tlet params = new HttpParams().set(\"linkTypeID\", linkTypeID);");
                        yaz.WriteLine("\t\t\treturn this.http.get(\"Ajax/\" + controller + \"/\" + action, { params: params });");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\telse if (typeID != null) {");
                        yaz.WriteLine("\t\t\tlet params = new HttpParams().set(\"typeID\", typeID);");
                        yaz.WriteLine("\t\t\treturn this.http.get(\"Ajax/\" + controller + \"/\" + action, { params: params });");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\telse");
                        yaz.WriteLine("\t\t\treturn this.http.get(\"Ajax/\" + controller + \"/\" + action);");
                        yaz.WriteLine("\t}");
                    }
                    else
                    {
                        yaz.WriteLine("\tget(controller: string, action: string, id: string = null) {");
                        yaz.WriteLine("\t\tif (id == null)");
                        yaz.WriteLine("\t\t\treturn this.http.get(\"Ajax/\" + controller + \"/\" + action);");
                        yaz.WriteLine("\t\telse {");
                        yaz.WriteLine("\t\t\tlet params = new HttpParams().set(\"id\", id);");
                        yaz.WriteLine("\t\t\treturn this.http.get(\"Ajax/\" + controller + \"/\" + action, { params: params });");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                    }
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpost(controller: string, action: string, model: any) {");
                    yaz.WriteLine("\t\treturn this.http.post(\"Ajax/\" + controller + \"/\" + action, model);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\services\\site.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Injectable } from \"@angular/core\";");
                    yaz.WriteLine("import { HttpClient, HttpParams } from '@angular/common/http';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Injectable({ providedIn: 'root' })");
                    yaz.WriteLine("export class SiteService {");
                    yaz.WriteLine("\tconstructor(private http: HttpClient) {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tget(controller: string, action: string, param: any = null, param2: any = null, param3: any = null, param4: any = null) {");
                    yaz.WriteLine("\t\tlet params = new HttpParams().set(\"param\", param).set(\"param2\", param2).set(\"param3\", param3).set(\"param4\", param4);");
                    yaz.WriteLine("\t\treturn this.http.get(controller + \"/\" + action, { params: params });");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpost(controller: string, action: string, model: any = null) {");
                    yaz.WriteLine("\t\treturn this.http.post(controller + \"/\" + action, model);");
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
                    yaz.WriteLine("import { AdminLayoutComponent } from './admin/views/shared/layoutAdmin';");
                    yaz.WriteLine("import { AdminHeaderComponent } from './admin/views/shared/controls/header';");
                    yaz.WriteLine("import { AdminLeftMenuComponent } from './admin/views/shared/controls/leftmenu';");
                    yaz.WriteLine("import { AdminScriptsComponent } from './admin/views/shared/controls/scripts';");
                    yaz.WriteLine("import { AdminCopyDeleteComponent } from './admin/views/shared/controls/copydelete';");
                    yaz.WriteLine("import { AdminFooterComponent } from './admin/views/shared/controls/footer';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { AdminLoginComponent } from './admin/views/home/login';");
                    yaz.WriteLine("import { AdminIndexComponent } from './admin/views/home/index';");
                    yaz.WriteLine("");

                    foreach (string Table in selectedTables)
                    {
                        string tableFolder = Table.ToUserRightType() == "Website" ? projectName.ToUrl(true) : "general";

                        yaz.WriteLine("import { Admin" + Table + "IndexComponent } from './admin/views/" + tableFolder + "/" + Table.ToUrl(true) + "';");

                        if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                        {
                            yaz.WriteLine("import { Admin" + Table + "InsertComponent } from './admin/views/" + tableFolder + "/" + Table.ToUrl(true) + "/insert';");
                            yaz.WriteLine("import { Admin" + Table + "UpdateComponent } from './admin/views/" + tableFolder + "/" + Table.ToUrl(true) + "/update';");
                        }
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("import { IndexComponent } from './views/home/index';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { SharedService } from './admin/services/shared';");
                    yaz.WriteLine("import { ModelService } from './admin/services/model';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { AdminLib } from './admin/lib/lib';");
                    yaz.WriteLine("import { Lib } from './lib/lib';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { LayoutComponent } from './views/shared/layout';");
                    yaz.WriteLine("import { HeaderComponent } from './views/shared/controls/header';");
                    yaz.WriteLine("import { FooterComponent } from './views/shared/controls/footer';");
                    yaz.WriteLine("import { ScriptsComponent } from './views/shared/controls/scripts';");
                    yaz.WriteLine("");

                    yaz.WriteLine("@NgModule({");
                    yaz.WriteLine("\tdeclarations: [");
                    yaz.WriteLine("\t\tAppComponent,");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tLayoutComponent,");
                    yaz.WriteLine("\t\tHeaderComponent,");
                    yaz.WriteLine("\t\tFooterComponent,");
                    yaz.WriteLine("\t\tScriptsComponent,");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tIndexComponent,");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tAdminLayoutComponent,");
                    yaz.WriteLine("\t\tAdminHeaderComponent,");
                    yaz.WriteLine("\t\tAdminLeftMenuComponent,");
                    yaz.WriteLine("\t\tAdminScriptsComponent,");
                    yaz.WriteLine("\t\tAdminCopyDeleteComponent,");
                    yaz.WriteLine("\t\tAdminFooterComponent,");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tAdminLoginComponent,");
                    yaz.WriteLine("\t\tAdminIndexComponent,");

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tAdmin" + Table + "IndexComponent,");

                        if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                        {
                            yaz.WriteLine("\t\tAdmin" + Table + "InsertComponent,");
                            yaz.WriteLine("\t\tAdmin" + Table + "UpdateComponent,");
                        }
                    }

                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\timports: [");
                    yaz.WriteLine("\t\tBrowserModule,");
                    yaz.WriteLine("\t\tAppRoutingModule,");
                    yaz.WriteLine("\t\tReactiveFormsModule.withConfig({ warnOnNgModelWithFormControl: 'never' }),");
                    yaz.WriteLine("\t\tHttpClientModule");
                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\t//'/' -> '/" + projectName + "/' Bu şekilde değişecek");

                    yaz.WriteLine("\tproviders: [{ provide: APP_BASE_HREF, useValue: '/" + projectName + "/' },");
                    yaz.WriteLine("\t\tSharedService,");
                    yaz.WriteLine("\t\tModelService,");
                    yaz.WriteLine("\t\tLib,");
                    yaz.WriteLine("\t\tAdminLib");
                    yaz.WriteLine("\t],");
                    yaz.WriteLine("\tbootstrap: [AppComponent]");
                    yaz.WriteLine("})");
                    yaz.WriteLine("");
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
                    yaz.WriteLine("import { AdminLayoutComponent } from './admin/views/shared/layoutAdmin';");
                    yaz.WriteLine("import { AdminLoginComponent } from './admin/views/home/login';");
                    yaz.WriteLine("import { AdminIndexComponent } from './admin/views/home/index';");
                    yaz.WriteLine("");

                    foreach (string Table in selectedTables)
                    {
                        string tableFolder = Table.ToUserRightType() == "Website" ? projectName.ToUrl(true) : "general";

                        yaz.WriteLine("import { Admin" + Table + "IndexComponent } from './admin/views/" + tableFolder + "/" + Table.ToUrl(true) + "';");

                        if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                        {
                            yaz.WriteLine("import { Admin" + Table + "InsertComponent } from './admin/views/" + tableFolder + "/" + Table.ToUrl(true) + "/insert';");
                            yaz.WriteLine("import { Admin" + Table + "UpdateComponent } from './admin/views/" + tableFolder + "/" + Table.ToUrl(true) + "/update';");
                        }
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("import { LayoutComponent } from './views/shared/layout';");
                    yaz.WriteLine("import { IndexComponent } from './views/home/index';");
                    yaz.WriteLine("");

                    yaz.WriteLine("const routes: Routes = [");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpath: '',");
                    yaz.WriteLine("\t\tcomponent: LayoutComponent,");
                    yaz.WriteLine("\t\tchildren: [");
                    yaz.WriteLine("\t\t\t//{ path: '', redirectTo: 'Index', pathMatch: 'full' },");
                    yaz.WriteLine("\t\t\t{ path: '', component: IndexComponent, pathMatch: 'full' },");
                    yaz.WriteLine("\t\t], runGuardsAndResolvers: 'always'");
                    yaz.WriteLine("\t},");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t{ path: 'Admin/Login', component: AdminLoginComponent, runGuardsAndResolvers: 'always' },");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpath: '',");
                    yaz.WriteLine("\t\tcomponent: AdminLayoutComponent,");
                    yaz.WriteLine("\t\tchildren: [");
                    yaz.WriteLine("\t\t\t{ path: 'Admin', component: AdminIndexComponent },");
                    yaz.WriteLine("\t\t\t{ path: 'Admin/Index', component: AdminIndexComponent },");

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t{ path: 'Admin/" + Table + "', component: Admin" + Table + "IndexComponent },");
                        yaz.WriteLine("\t\t\t{ path: 'Admin/" + Table + "/Index', component: Admin" + Table + "IndexComponent },");

                        if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                        {
                            yaz.WriteLine("\t\t\t{ path: 'Admin/" + Table + "/Insert', component: Admin" + Table + "InsertComponent },");
                            yaz.WriteLine("\t\t\t{ path: 'Admin/" + Table + "/Update/:id', component: Admin" + Table + "UpdateComponent },");
                        }
                    }

                    yaz.WriteLine("\t\t], runGuardsAndResolvers: 'always'");
                    yaz.WriteLine("\t},");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t{ path: '**', redirectTo: '', runGuardsAndResolvers: 'always' }");
                    yaz.WriteLine("];");
                    yaz.WriteLine("");
                    yaz.WriteLine("@NgModule({");
                    yaz.WriteLine("\timports: [RouterModule.forRoot(routes, { onSameUrlNavigation: 'reload' })],");
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
                        yaz.WriteLine("\t\t\t\tname: \"Site\",");
                        yaz.WriteLine("\t\t\t\turl: \"Site/{action}/{id}\",");
                        yaz.WriteLine("\t\t\t\tdefaults: new { controller = \"Site\", action = \"Index\", id = UrlParameter.Optional }");
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

        void CreateAngularViewLayer()
        {
            int i = 0;

            foreach (string Table in selectedTables)
            {
                Table table = new Table(Table, connectionInfo);
                FillTable(table);
                SqlConnection con = new SqlConnection(Helper.CreateConnectionText(connectionInfo));

                string tableLang = Table.ToTurkish(lstLang);

                CreateAngularDirectories(Table);

                if (i <= 0)
                {
                    CreateAngularLayout();
                    CreateAngularHomePage();

                    i++;
                }

                string tableFolder = Table.ToUserRightType() == "Website" ? projectName.ToUrl(true) : "general";

                //Index
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + tableFolder + "\\" + Table.ToUrl(true) + "\\index.html", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("<div id=\"content\">");
                        yaz.WriteLine("\t<div id=\"content-header\">");
                        yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a href=\"javascript:;\" class=\"tip-bottom\"><i class=\"" + Table.ToIcon() + "\"></i> " + tableLang + " Tablosu</a></div>");
                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("\t<div class=\"container-fluid\">");
                        yaz.WriteLine("\t\t<div class=\"row-fluid\">");
                        yaz.WriteLine("\t\t\t<div class=\"span12\">");
                        yaz.WriteLine("\t\t\t\t<div class=\"widget-box\">");
                        yaz.WriteLine("\t\t\t\t\t<div class=\"widget-title\">");
                        yaz.WriteLine("\t\t\t\t\t\t<span class=\"icon\"><i class=\"" + Table.ToIcon() + "\"></i></span>");
                        yaz.WriteLine("\t\t\t\t\t\t<h5>" + tableLang + " Tablosu</h5>");
                        yaz.WriteLine("\t\t\t\t\t</div>");
                        yaz.WriteLine("\t\t\t\t\t<div class=\"widget-content nopadding\">");
                        yaz.WriteLine("\t\t\t\t\t\t<table class=\"table table-bordered data-table\">");
                        yaz.WriteLine("\t\t\t\t\t\t\t<thead>");
                        yaz.WriteLine("\t\t\t\t\t\t\t\t<tr>");


                        if (hasLinks && (Table == "Links" || Table == "LinkTypes"))
                        {
                            if (Table == "Links")
                            {
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>ID</th>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>Başlık</th>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>Bağlı Nesne</th>");
                            }

                            if (Table == "LinkTypes")
                            {
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>ID</th>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>Başlık</th>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>Ana Tip</th>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>Ana Nesne</th>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th class=\"hideColumn2\">Bağlanacak Tip</th>");
                            }
                        }
                        else
                        {
                            i = 0;

                            List<ColumnInfo> viewColumns = tableColumnInfos;

                            if (hasUserRights)
                                viewColumns = viewColumns.Where(a => a.ColumnName != "Password").ToList();

                            viewColumns = viewColumns.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower)).Take(4).ToList();

                            foreach (ColumnInfo column in viewColumns)
                            {
                                List<ForeignKeyChecker> frchkForeignLst2 = table.FkcForeignList.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                                if (frchkForeignLst2.Count > 0)
                                {
                                    string colName = column.ColumnName.ToTurkish(lstLang);

                                    if (colName == column.ColumnName)
                                        colName = "Bağlı " + frchkForeignLst2.FirstOrDefault().PrimaryTableName.ToTurkish(lstLang);

                                    yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th" + hideColumn + ">" + colName + "</th>");
                                }
                                else
                                {
                                    yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th" + hideColumn + ">" + column.ColumnName.ToTurkish(lstLang) + "</th>");
                                }

                                i++;
                            }
                        }

                        if (table.IdentityColumns.Count > 0)
                        {
                            if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                            {
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>İşlem</th>");
                            }
                        }

                        yaz.WriteLine("\t\t\t\t\t\t\t\t</tr>");
                        yaz.WriteLine("\t\t\t\t\t\t\t</thead>");

                        yaz.WriteLine("\t\t\t\t\t\t\t<tbody>");
                        yaz.WriteLine("\t\t\t\t\t\t\t\t<tr *ngFor=\"let item of " + Table + "List\">");

                        if (hasLinks && (Table == "Links" || Table == "LinkTypes"))
                        {
                            if (Table == "Links")
                            {
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td>{{ item?.ID }}</td>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td>{{ item?.Title }}</td>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td>{{ item?.LinkedAdi }}</td>");
                            }

                            if (Table == "LinkTypes")
                            {
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td>{{ item?.ID }}</td>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td>{{ item?.Title }}</td>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td>{{ item?.MainTypeAdi }}</td>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td>{{ item?.MainAdi }}</td>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td class=\"hideColumn2\">{{ item?.LinkedTypeAdi }}</td>");
                            }
                        }
                        else
                        {
                            i = 0;

                            List<ColumnInfo> viewColumns = tableColumnInfos;

                            if (hasUserRights)
                                viewColumns = viewColumns.Where(a => a.ColumnName != "Password").ToList();

                            viewColumns = viewColumns.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower)).Take(4).ToList();

                            foreach (ColumnInfo column in viewColumns)
                            {
                                List<ForeignKeyChecker> frchkForeignLst2 = table.FkcForeignList.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                                if (column.Type.Name != "Boolean")
                                {
                                    if (frchkForeignLst2.Count > 0)
                                    {
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">{{ item?." + frchkForeignLst2.FirstOrDefault().PrimaryTableName + "Adi }}</td>");
                                    }
                                    else
                                    {
                                        if (column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + "><a href=\"/" + projectName + "/Uploads/{{ item?." + column.ColumnName + " }}\" target=\"_blank\"><img src=\"/" + projectName + "/Uploads/thumb_{{ item?." + column.ColumnName + " }}\" style=\"height:40px; max-width:80px;\" /></a></td>");
                                        else if (column.ColumnName.In(FileColumns, InType.ToUrlLower))
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + "><a class=\"btn btn-mini btn-info\" href=\"/" + projectName + "/Uploads/{{ item?." + column.ColumnName + " }}\" target=\"_blank\">{{ item?." + column.ColumnName + " }}</a></td>");
                                        else
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">{{ item?." + column.ColumnName + " }}</td>");
                                    }
                                }
                                else
                                {
                                    yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + " style=\"text-align:center;\"><img *ngIf=\"item?." + column.ColumnName + "\" class=\"active\" /><img *ngIf=\"!item?." + column.ColumnName + "\" class=\"passive\" /></td>");
                                }

                                i++;
                            }
                        }

                        string insertShow = hasUserRights ? " *ngIf=\"insertShow\"" : "";
                        string deleteShow = hasUserRights ? " *ngIf=\"deleteShow\"" : "";
                        string updateShow = hasUserRights ? " *ngIf=\"updateShow\"" : "";
                        string copyShow = hasUserRights ? " *ngIf=\"copyShow\"" : "";
                        string removeShow = hasUserRights ? " *ngIf=\"removeShow\"" : "";

                        if (table.IdentityColumns.Count > 0)
                        {
                            if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                            {
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td style=\"text-align:center;\">");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<div class=\"btn-group\" style=\"text-align:left;\">");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<button data-toggle=\"dropdown\" class=\"btn btn-mini btn-primary dropdown-toggle\">İşlem <span class=\"caret\"></span></button>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<ul class=\"dropdown-menu\">");

                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li" + updateShow + "><a class=\"updLink\" [routerLink]=\"['/Admin/" + Table + "/Update/' + item?." + table.ID + "]\">Düzenle</a></li>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li" + copyShow + "><a class=\"cpyLink\" href=\"#cpyData\" data-toggle=\"modal\" data-controller=\"" + Table + "\" [attr.data-id]=\"item?." + table.ID + "\">Kopyala</a></li>");

                                if (table.Deleted)
                                    yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li" + removeShow + "><a class=\"rmvLink\" href=\"#rmvData\" data-toggle=\"modal\" data-controller=\"" + Table + "\" [attr.data-id]=\"item?." + table.ID + "\">Kaldır</a></li>");

                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li" + deleteShow + "><a class=\"dltLink\" href=\"#dltData\" data-toggle=\"modal\" data-controller=\"" + Table + "\" [attr.data-id]=\"item?." + table.ID + "\">Sil</a></li>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t</ul>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t</div>");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t</td>");
                            }
                        }

                        yaz.WriteLine("\t\t\t\t\t\t\t\t</tr>");
                        yaz.WriteLine("\t\t\t\t\t\t\t</tbody>");
                        yaz.WriteLine("\t\t\t\t\t\t</table>");
                        yaz.WriteLine("\t\t\t\t\t</div>");
                        yaz.WriteLine("\t\t\t\t</div>");
                        yaz.WriteLine("\t\t\t</div>");
                        yaz.WriteLine("\t\t</div>");

                        if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t<div class=\"pagelinks\"" + insertShow + ">");
                            yaz.WriteLine("\t\t\t<a routerLink=\"/Admin/" + Table + "/Insert\" class=\"btn btn-primary btn-add\">" + tableLang + " Ekle</a>");
                            yaz.WriteLine("\t\t</div>");
                        }
                        else
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t<div class=\"pagelinks\"" + deleteShow + ">");
                            yaz.WriteLine("\t\t\t<a class=\"btn btn-primary btn-clear clrLink\" data-toggle=\"modal\" data-controller=\"" + Table + "\" href=\"#clrData\">Temizle</a>");
                            yaz.WriteLine("\t\t</div>");
                        }

                        if (hasUserRights)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t<input id=\"hdnType\" type=\"hidden\" value=\"" + Table.ToUserRightType() + "\" />");
                        }

                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("</div>");
                        yaz.WriteLine("");
                        yaz.WriteLine("<admin-copydelete></admin-copydelete>");
                        yaz.Close();
                    }
                }

                if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                {
                    //Ekle
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + tableFolder + "\\" + Table.ToUrl(true) + "\\insert.html", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                        {
                            yaz.WriteLine("<div id=\"content\">");
                            yaz.WriteLine("\t<div id=\"content-header\">");
                            yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a class=\"tip-bottom\"><i class=\"" + Table.ToIcon() + "\"></i> " + tableLang + " Ekle</a></div>");
                            yaz.WriteLine("\t</div>");
                            yaz.WriteLine("\t<div class=\"container-fluid\">");
                            yaz.WriteLine("\t\t<form [formGroup]=\"insertForm\" (ngSubmit)=\"onSubmit()\">");
                            yaz.WriteLine("\t\t\t<fieldset>");

                            List<ColumnInfo> viewColumns = table.Columns;

                            if (hasUserRights)
                                viewColumns = viewColumns.Where(a => a.ColumnName != "LoginTime").ToList();

                            viewColumns = viewColumns.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList();

                            foreach (ColumnInfo column in viewColumns)
                            {
                                if (!table.IdentityColumns.Contains(column.ColumnName))
                                {
                                    List<ForeignKeyChecker> frchkLst = table.FkcForeignList.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");

                                    if (frchkLst.Count > 0)
                                    {
                                        string colName = column.ColumnName.ToTurkish(lstLang);

                                        if (colName == column.ColumnName)
                                            colName = "Bağlı " + frchkLst.FirstOrDefault().PrimaryTableName.ToTurkish(lstLang);

                                        yaz.WriteLine("\t\t\t\t\t" + colName);
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");

                                        if (hasLinks && (Table == "Links" || Table == "LinkTypes"))
                                        {
                                            if (Table == "Links")
                                            {
                                                if (column.ColumnName == "LinkTypeID")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t<select id=\"LinkTypeID\" (change)=\"onChange($event)\" [ngModel]=\"model?.LinkTypeID\" formControlName=\"LinkTypeID\"><option *ngFor=\"let item of model?.LinkTypesList\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                                }
                                            }

                                            if (Table == "LinkTypes")
                                            {
                                                if (column.ColumnName == "MainTypeID")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t<select id=\"MainTypeID\" class=\"mainType\" (change)=\"onChange($event)\" [ngModel]=\"model?.MainTypeID\" formControlName=\"MainTypeID\"><option *ngFor=\"let item of model?.MainTypeList\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                                }

                                                if (column.ColumnName == "LinkedTypeID")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t<select id=\"LinkedTypeID\" class=\"linkedType\" [ngModel]=\"model?.LinkedTypeID\" formControlName=\"LinkedTypeID\"><option *ngFor=\"let item of model?.LinkedTypeList\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            yaz.WriteLine("\t\t\t\t\t<select id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\"><option *ngFor=\"let item of model?." + frchkLst.FirstOrDefault().PrimaryTableName + "List\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                        }
                                    }
                                    else
                                    {
                                        yaz.WriteLine("\t\t\t\t\t" + column.ColumnName.ToTurkish(lstLang));
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");

                                        if (column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                        {
                                            yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"file\" name=\"" + column.ColumnName.ToUrl(true) + "Temp\" (change)=\"on" + column.ColumnName + "FileSelect($event)\" accept=\"image/*\" />");
                                        }
                                        else if (column.ColumnName.In(FileColumns, InType.ToUrlLower))
                                        {
                                            yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"file\" name=\"" + column.ColumnName.ToUrl(true) + "Temp\" (change)=\"on" + column.ColumnName + "FileSelect($event)\" />");
                                        }
                                        else
                                        {
                                            if (column.Type.Name == "Boolean")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"checkbox\" />");
                                            }
                                            else if (column.Type.Name == "Int16" ||
                                                     column.Type.Name == "Int32" ||
                                                     column.Type.Name == "Int64")
                                            {
                                                if (hasLinks && (Table == "Links" || Table == "LinkTypes"))
                                                {
                                                    if (Table == "Links")
                                                    {
                                                        if (column.ColumnName == "LinkID")
                                                        {
                                                            yaz.WriteLine("\t\t\t\t\t<select id=\"LinkID\" class=\"selectLinkID\" [ngModel]=\"model?.LinkID\" formControlName=\"LinkID\"><option *ngFor=\"let item of model?.LinkedItemList\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                                        }
                                                    }

                                                    if (Table == "LinkTypes")
                                                    {
                                                        if (column.ColumnName == "MainID")
                                                        {
                                                            yaz.WriteLine("\t\t\t\t\t<select id=\"MainID\" class=\"selectMain\" [ngModel]=\"model?.MainID\" formControlName=\"MainID\"><option *ngFor=\"let item of model?.MainList\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                                        }
                                                    }
                                                }
                                                else if (Table == "Category" && column.ColumnName == "ParentID")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t<select id=\"ParentID\" [ngModel]=\"model?.ParentID\" formControlName=\"ParentID\"><option *ngFor=\"let item of model?.ParentCategories\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                                }
                                                else
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"number\" />");
                                                }
                                            }
                                            else if (column.Type.Name == "String" && column.CharLength == -1)
                                            {
                                                yaz.WriteLine("\t\t\t\t\t<textarea id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\"></textarea>");
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"text\" />");
                                            }
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
                            yaz.WriteLine("\t\t\t\t\t<input type=\"submit\" value=\"Kaydet\" class=\"btn btn-success btn-save\" [disabled]=\"!insertForm.valid\" />");
                            yaz.WriteLine("\t\t\t\t\t<a routerLink=\"/Admin/" + Table + "\" class=\"btn btn-danger btn-cancel\">İptal</a>");
                            yaz.WriteLine("\t\t\t</fieldset>");
                            yaz.WriteLine("\t\t</form>");
                            yaz.WriteLine("\t</div>");
                            yaz.WriteLine("</div>");
                            yaz.Close();
                        }
                    }

                    if (table.IdentityColumns.Count > 0)
                    {
                        //Duzenle
                        using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + tableFolder + "\\" + Table.ToUrl(true) + "\\update.html", FileMode.Create))
                        {
                            using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                            {
                                yaz.WriteLine("<div id=\"content\">");
                                yaz.WriteLine("\t<div id=\"content-header\">");
                                yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a class=\"tip-bottom\"><i class=\"" + Table.ToIcon() + "\"></i> " + tableLang + " Düzenle</a></div>");
                                yaz.WriteLine("\t</div>");
                                yaz.WriteLine("\t<div class=\"container-fluid\">");
                                yaz.WriteLine("\t\t<form [formGroup]=\"updateForm\" (ngSubmit)=\"onSubmit()\">");
                                yaz.WriteLine("\t\t\t<fieldset>");

                                List<ColumnInfo> viewColumns = table.Columns;

                                if (hasUserRights)
                                    viewColumns = viewColumns.Where(a => a.ColumnName != "LoginTime").ToList();

                                viewColumns = viewColumns.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList();

                                foreach (ColumnInfo column in viewColumns)
                                {
                                    if (table.IdentityColumns.Contains(column.ColumnName))
                                    {
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");
                                        yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"hidden\" value=\"{{ model?." + column.ColumnName + " }}\" />");
                                        yaz.WriteLine("\t\t\t\t</div>");
                                    }
                                    else
                                    {
                                        List<ForeignKeyChecker> frchkLst = table.FkcForeignList.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");

                                        if (frchkLst.Count > 0)
                                        {
                                            string colName = column.ColumnName.ToTurkish(lstLang);

                                            if (colName == column.ColumnName)
                                                colName = "Bağlı " + frchkLst.FirstOrDefault().PrimaryTableName.ToTurkish(lstLang);

                                            yaz.WriteLine("\t\t\t\t\t" + colName);
                                            yaz.WriteLine("\t\t\t\t</div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");

                                            if (hasLinks && (Table == "Links" || Table == "LinkTypes"))
                                            {
                                                if (Table == "Links")
                                                {
                                                    if (column.ColumnName == "LinkTypeID")
                                                    {
                                                        yaz.WriteLine("\t\t\t\t\t<input id=\"LinkTypeID\" [ngModel]=\"model?.LinkTypeID\" formControlName=\"LinkTypeID\" type=\"hidden\" />");
                                                        yaz.WriteLine("\t\t\t\t\t<input id=\"LinkedTypesAdi\" type=\"text\" disabled=\"disabled\" value=\"{{ model?.LinkedTypesAdi }}\" />");
                                                    }
                                                }

                                                if (Table == "LinkTypes")
                                                {
                                                    if (column.ColumnName == "MainTypeID")
                                                    {
                                                        yaz.WriteLine("\t\t\t\t\t<select id=\"MainTypeID\" class=\"mainType\" (change)=\"onChange($event)\" [ngModel]=\"model?.MainTypeID\" formControlName=\"MainTypeID\"><option *ngFor=\"let item of model?.MainTypeList\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                                    }

                                                    if (column.ColumnName == "LinkedTypeID")
                                                    {
                                                        yaz.WriteLine("\t\t\t\t\t<select id=\"LinkedTypeID\" class=\"linkedType\" [ngModel]=\"model?.LinkedTypeID\" formControlName=\"LinkedTypeID\"><option *ngFor=\"let item of model?.LinkedTypeList\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\" >{{ item?.Text }}</option></select>");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t\t\t<select id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\"><option *ngFor=\"let item of model?." + frchkLst.FirstOrDefault().PrimaryTableName + "List\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                            }
                                        }
                                        else
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + column.ColumnName.ToTurkish(lstLang));
                                            yaz.WriteLine("\t\t\t\t</div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                            yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");

                                            if (column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                            {
                                                yaz.WriteLine("\t\t\t\t\t<a href=\"/" + projectName + "/Uploads/{{ model?." + column.ColumnName + " }}\" target=\"_blank\"><img [src]=\"['/" + projectName + "/Uploads/thumb_' + model?." + column.ColumnName + "]\" style=\"height:40px; max-width:80px;\" /></a><br /><br />");
                                                yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"hidden\" value=\"{{ model?." + column.ColumnName + " }}\" />");
                                                yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName.ToUrl(true) + "Temp\" type=\"file\" name=\"" + column.ColumnName.ToUrl(true) + "Temp\" (change)=\"on" + column.ColumnName + "FileSelect($event)\" accept=\"image/*\" />");
                                            }
                                            else if (column.ColumnName.In(FileColumns, InType.ToUrlLower))
                                            {
                                                yaz.WriteLine("\t\t\t\t\t<a class=\"btn btn-mini btn-info\" href=\"/" + projectName + "/Uploads/{{ model?." + column.ColumnName + " }}\" target=\"_blank\">{{ model?." + column.ColumnName + " }}</a><br /><br />");
                                                yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"hidden\" value=\"{{ model?." + column.ColumnName + " }}\" />");
                                                yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName.ToUrl(true) + "Temp\" type=\"file\" name=\"" + column.ColumnName.ToUrl(true) + "Temp\" (change)=\"on" + column.ColumnName + "FileSelect($event)\" />");
                                            }
                                            else
                                            {
                                                if (column.Type.Name == "Boolean")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"checkbox\" checked=\"checked\" *ngIf=\"model?." + column.ColumnName + "\" />");
                                                    yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"checkbox\" *ngIf=\"!model?." + column.ColumnName + "\" />");
                                                }
                                                else if (column.Type.Name == "Int16" ||
                                                         column.Type.Name == "Int32" ||
                                                         column.Type.Name == "Int64")
                                                {
                                                    if (hasLinks && (Table == "Links" || Table == "LinkTypes"))
                                                    {
                                                        if (Table == "Links")
                                                        {
                                                            if (column.ColumnName == "LinkID")
                                                            {
                                                                yaz.WriteLine("\t\t\t\t\t<select id=\"LinkID\" [ngModel]=\"model?.LinkID\" formControlName=\"LinkID\"><option *ngFor=\"let item of model?.LinkedItemList\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                                            }
                                                        }

                                                        if (Table == "LinkTypes")
                                                        {
                                                            if (column.ColumnName == "MainID")
                                                            {
                                                                yaz.WriteLine("\t\t\t\t\t<select id=\"MainID\" class=\"selectMain\" [ngModel]=\"model?.MainID\" formControlName=\"MainID\"><option *ngFor=\"let item of model?.MainList\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                                            }
                                                        }
                                                    }
                                                    else if (Table == "Category" && column.ColumnName == "ParentID")
                                                    {
                                                        yaz.WriteLine("\t\t\t\t\t<select id=\"ParentID\" [ngModel]=\"model?.ParentID\" formControlName=\"ParentID\"><option *ngFor=\"let item of model?.ParentCategories\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                                    }
                                                    else
                                                    {
                                                        yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"number\" value=\"{{ model?." + column.ColumnName + " }}\" />");
                                                    }
                                                }
                                                else if (column.Type.Name == "String" && column.CharLength == -1)
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t<textarea id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\">{{ model?." + column.ColumnName + " }}</textarea>");
                                                }
                                                else
                                                {
                                                    string type = hasUserRights && column.ColumnName == "Password" && Table == "Users" ? "password" : "text";
                                                    string value = hasUserRights && column.ColumnName == "Password" && Table == "Users" ? "" : " value=\"{{ model?." + column.ColumnName + " }}\"";
                                                    string model = hasUserRights && column.ColumnName == "Password" && Table == "Users" ? "" : " [ngModel]=\"model?." + column.ColumnName + "\"";

                                                    yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\"" + model + " formControlName=\"" + column.ColumnName + "\" type=\"" + type + "\"" + value + " />");
                                                }
                                            }
                                        }

                                        yaz.WriteLine("\t\t\t\t\t<br />");
                                        yaz.WriteLine("\t\t\t\t\t<!-- " + column.ColumnName + " -->");

                                        if (hasUserRights && column.ColumnName == "Password" && Table == "Users")
                                        {
                                            yaz.WriteLine("\t\t\t\t\t<span class=\"badge badge-info\">Şifre boş bırakılırsa eski şifre saklanacaktır.</span>");
                                            yaz.WriteLine("\t\t\t\t\t<br />");
                                            yaz.WriteLine("\t\t\t\t\t<br />");
                                        }

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
                                yaz.WriteLine("\t\t\t\t\t<input type=\"submit\" value=\"Kaydet\" class=\"btn btn-success btn-save\" [disabled]=\"!updateForm.valid\" />");
                                yaz.WriteLine("\t\t\t\t\t<a routerLink=\"/Admin/" + Table + "\" class=\"btn btn-danger btn-cancel\">İptal</a>");
                                yaz.WriteLine("\t\t\t</fieldset>");
                                yaz.WriteLine("\t\t</form>");

                                if (table.FkcList.Count > 0)
                                {
                                    foreach (ForeignKeyChecker fkc in table.FkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                    {
                                        Table tableFrgn = new Table(fkc.ForeignTableName, connectionInfo);
                                        FillTable(tableFrgn);

                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t<div class=\"row-fluid\">");
                                        yaz.WriteLine("\t\t\t<div class=\"span12\">");
                                        yaz.WriteLine("\t\t\t\t<div class=\"widget-box\">");
                                        yaz.WriteLine("\t\t\t\t\t<div class=\"widget-title\">");
                                        yaz.WriteLine("\t\t\t\t\t\t<span class=\"icon\"><i class=\"" + Table.ToIcon() + "\"></i></span>");
                                        yaz.WriteLine("\t\t\t\t\t\t<h5>Bağlı " + tableFrgn.TableName.ToTurkish(lstLang) + " Tablosu</h5>");
                                        yaz.WriteLine("\t\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t\t<div class=\"widget-content nopadding\">");
                                        yaz.WriteLine("\t\t\t\t\t\t<table class=\"table table-bordered data-table\">");
                                        yaz.WriteLine("\t\t\t\t\t\t\t<thead>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t<tr>");

                                        i = 0;

                                        List<ColumnInfo> foreignColumns = tableFrgn.Columns;

                                        if (hasUserRights)
                                            foreignColumns = foreignColumns.Where(a => a.ColumnName != "Password").ToList();

                                        foreignColumns = foreignColumns.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower)).Take(4).ToList();

                                        foreach (ColumnInfo item in foreignColumns)
                                        {
                                            List<ForeignKeyChecker> frchkForeignLst = tableFrgn.FkcForeignList.Where(a => a.ForeignColumnName == item.ColumnName).ToList();

                                            string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                                            if (frchkForeignLst.Count <= 0)
                                            {
                                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th" + hideColumn + ">" + item.ColumnName.ToTurkish(lstLang) + "</th>");
                                                i++;
                                            }
                                        }

                                        if (tableFrgn.IdentityColumns.Count > 0)
                                        {
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<th>İşlem</th>");
                                        }

                                        yaz.WriteLine("\t\t\t\t\t\t\t\t</tr>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t</thead>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t<tbody>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t<tr *ngFor=\"let item of model?." + tableFrgn.TableName + "List\">");

                                        i = 0;

                                        foreach (ColumnInfo item in foreignColumns)
                                        {
                                            List<ForeignKeyChecker> frchkForeignLst = tableFrgn.FkcForeignList.Where(a => a.ForeignColumnName == item.ColumnName).ToList();

                                            string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";
                                            if (item.Type.Name != "Boolean")
                                            {
                                                if (frchkForeignLst.Count <= 0)
                                                {
                                                    if (item.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + "><a href=\"/" + projectName + "/Uploads/{{ item?." + item.ColumnName + " }}\" target=\"_blank\"><img src=\"/" + projectName + "/Uploads/thumb_{{ item?." + item.ColumnName + " }}\" style=\"height:40px; max-width:80px;\" /></a></td>");
                                                    else if (item.ColumnName.In(FileColumns, InType.ToUrlLower))
                                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + "><a class=\"btn btn-mini btn-info\" href=\"/" + projectName + "/Uploads/{{ item?." + item.ColumnName + " }}\" target=\"_blank\">{{ item?." + item.ColumnName + " }}</a></td>");
                                                    else
                                                    {
                                                        if (hasLinks && Table == "LinkTypes" && item.ColumnName == "LinkID")
                                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">{{ item?.LinkedAdi }}</td>");
                                                        else
                                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">{{ item?." + item.ColumnName + " }}</td>");
                                                    }
                                                    i++;
                                                }
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + " style=\"text-align:center;\"><img *ngIf=\"item?." + item.ColumnName + "\" class=\"active\" /><img *ngIf=\"!item?." + item.ColumnName + "\" class=\"passive\" /></td>");
                                            }
                                        }

                                        string insertShow = hasUserRights ? " *ngIf=\"insertShow\"" : "";

                                        if (tableFrgn.IdentityColumns.Count > 0)
                                        {
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td style=\"text-align:center;\">");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<div class=\"btn-group\" style=\"text-align:left;\">");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<button data-toggle=\"dropdown\" class=\"btn btn-mini btn-primary dropdown-toggle\">İşlem <span class=\"caret\"></span></button>");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<ul class=\"dropdown-menu\">");

                                            string updateShow = hasUserRights ? " *ngIf=\"updateShow\"" : "";
                                            string copyShow = hasUserRights ? " *ngIf=\"copyShow\"" : "";
                                            string removeShow = hasUserRights ? " *ngIf=\"removeShow\"" : "";
                                            string deleteShow = hasUserRights ? " *ngIf=\"deleteShow\"" : "";

                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li" + updateShow + "><a class=\"updLink\" [routerLink]=\"['/Admin/" + tableFrgn.TableName + "/Update/' + item?." + tableFrgn.ID + "]\">Düzenle</a></li>");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li" + copyShow + "><a class=\"cpyLink\" href=\"#cpyData\" data-toggle=\"modal\" data-controller=\"" + tableFrgn.TableName + "\" [attr.data-id]=\"item?." + tableFrgn.ID + "\">Kopyala</a></li>");

                                            if (tableFrgn.Deleted)
                                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li" + removeShow + "><a class=\"rmvLink\" href=\"#rmvData\" data-toggle=\"modal\" data-controller=\"" + tableFrgn.TableName + "\" [attr.data-id]=\"item?." + tableFrgn.ID + "\">Kaldır</a></li>");

                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li" + deleteShow + "><a class=\"dltLink\" href=\"#dltData\" data-toggle=\"modal\" data-controller=\"" + tableFrgn.TableName + "\" [attr.data-id]=\"item?." + tableFrgn.ID + "\">Sil</a></li>");
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
                                        yaz.WriteLine("\t\t<div class=\"pagelinks\"" + insertShow + ">");
                                        yaz.WriteLine("\t\t\t<a routerLink=\"/Admin/" + tableFrgn.TableName + "/Insert\" class=\"btn btn-primary btn-add\">" + tableFrgn.TableName.ToTurkish(lstLang) + " Ekle</a>");
                                        yaz.WriteLine("\t\t</div>");
                                    }
                                }

                                if (hasUserRights)
                                {
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t<input id=\"hdnType\" type=\"hidden\" value=\"" + Table.ToUserRightType() + "\" />");
                                }

                                yaz.WriteLine("\t</div>");
                                yaz.WriteLine("</div>");

                                if (table.FkcList.Count > 0)
                                {
                                    yaz.WriteLine("");
                                    yaz.WriteLine("<admin-copydelete></admin-copydelete>");
                                }

                                yaz.Close();
                            }
                        }

                        if (Table == "Users" && hasUserRights)
                        {
                            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + tableFolder + "\\" + Table.ToUrl(true) + "\\changegroup.html", FileMode.Create))
                            {
                                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                                {
                                    yaz.WriteLine("<div id=\"content\">");
                                    yaz.WriteLine("\t<div id=\"content-header\">");
                                    yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a href=\"javascript:;\" class=\"tip-bottom\"><i class=\"icon-group\"></i> Kullanıcı Grubu Değiştir</a></div>");
                                    yaz.WriteLine("\t</div>");
                                    yaz.WriteLine("\t<div class=\"container-fluid\">");
                                    yaz.WriteLine("\t\t<form [formGroup]=\"changeGroupForm\" (ngSubmit)=\"onSubmit()\">");
                                    yaz.WriteLine("\t\t\t<fieldset>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t\t\t<input id=\"ID\" [ngModel]=\"model?.ID\" formControlName=\"ID\" type=\"hidden\" value=\"{{ model?.ID }}\" />");
                                    yaz.WriteLine("\t\t\t\t</div>");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-label\">");
                                    yaz.WriteLine("\t\t\t\t\tBağlı Grup");
                                    yaz.WriteLine("\t\t\t\t</div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");
                                    yaz.WriteLine("\t\t\t\t\t<select id=\"GroupID\" [ngModel]=\"model?.GroupID\" formControlName=\"GroupID\"><option *ngFor=\"let item of model?.UserGroupsList\" selected=\"{{ item?.Selected ? 'selected' : '' }}\" value=\"{{ item?.Value }}\">{{ item?.Text }}</option></select>");
                                    yaz.WriteLine("\t\t\t\t\t<br />");
                                    yaz.WriteLine("\t\t\t\t\t<!--GroupID-->");
                                    yaz.WriteLine("\t\t\t\t</div>");
                                    yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t<p>");
                                    yaz.WriteLine("\t\t\t\t\t<div class=\"alert alert-error\">");
                                    yaz.WriteLine("\t\t\t\t\t\t<strong>Hata! </strong> <span class=\"alertMessage\">{{ model?.Mesaj }}</span>");
                                    yaz.WriteLine("\t\t\t\t\t</div>");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t\t<input type=\"submit\" value=\"Kaydet\" class=\"btn btn-success btn-save\" [disabled]=\"!grupDegistirForm.valid\" />");
                                    yaz.WriteLine("\t\t\t\t\t<a routerLink=\"/Admin/Users\" class=\"btn btn-danger btn-cancel\">İptal</a>");
                                    yaz.WriteLine("\t\t\t</fieldset>");
                                    yaz.WriteLine("\t\t</form>");
                                    yaz.WriteLine("\t</div>");
                                    yaz.WriteLine("</div>");
                                }
                            }
                        }
                    }
                }
            }
        }

        void CreateAngularTypeScriptLayer()
        {
            foreach (string Table in selectedTables)
            {
                Table table = new Table(Table, connectionInfo);
                FillTable(table);

                SqlConnection con = new SqlConnection(Helper.CreateConnectionText(connectionInfo));

                CreateAngularDirectories(Table);

                string tableFolder = Table.ToUserRightType() == "Website" ? projectName.ToUrl(true) : "general";

                //Index
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + tableFolder + "\\" + Table.ToUrl(true) + "\\index.ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("import { Component, OnInit, OnDestroy } from '@angular/core';");
                        yaz.WriteLine("import { Subscription } from 'rxjs';");
                        yaz.WriteLine("import { ModelService } from '../../../services/model';");

                        if (hasUserRights)
                        {
                            yaz.WriteLine("import { SharedService } from '../../../services/shared';");
                            yaz.WriteLine("import { AdminLib } from '../../../lib/lib';");
                        }

                        yaz.WriteLine("declare var DataTable;");
                        yaz.WriteLine("");
                        yaz.WriteLine("@Component({");
                        yaz.WriteLine("\ttemplateUrl: './index.html'");
                        yaz.WriteLine("})");
                        yaz.WriteLine("");
                        yaz.WriteLine("export class Admin" + Table + "IndexComponent implements OnInit, OnDestroy {");
                        yaz.WriteLine("\terrorMsg: string;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tcallTable: boolean;");
                        yaz.WriteLine("");

                        if (hasUserRights)
                        {
                            yaz.WriteLine("\tinsertShow: boolean = false;");

                            if (table.IdentityColumns.Count > 0)
                            {
                                yaz.WriteLine("\tupdateShow: boolean = false;");
                                yaz.WriteLine("\tdeleteShow: boolean = false;");
                                yaz.WriteLine("\tcopyShow: boolean = false;");

                                if (table.Deleted)
                                    yaz.WriteLine("\tremoveShow: boolean = false;");

                                if (Table == "Users")
                                    yaz.WriteLine("\tchangeGroupShow: boolean = false;");
                            }

                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\tprivate subscription: Subscription = new Subscription();");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t" + Table + "List: any;");
                        yaz.WriteLine("");

                        string constHasUserRight = hasUserRights ? ", private sharedService: SharedService" : "";

                        yaz.WriteLine("\tconstructor(private service: ModelService" + constHasUserRight + ") {");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tngOnInit() {");
                        yaz.WriteLine("\t\tthis.callTable = true;");

                        string hasRightHiddenModel = hasUserRights ? "$(\"#hdnType\").val()" : "";

                        yaz.WriteLine("\t\tthis.FillData(" + hasRightHiddenModel + ");");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");

                        string hasRightModel = hasUserRights ? "Model: any" : "";

                        yaz.WriteLine("\tFillData(" + hasRightModel + ") {");

                        string ttab = hasUserRights ? "\t" : "";

                        if (hasUserRights)
                        {
                            yaz.WriteLine("\t\tthis.sharedService.getCurrentUserRights(Model).subscribe((userRights: any) => {");
                            yaz.WriteLine("\t\t\tthis.insertShow = AdminLib.UserRight(userRights, Model, \"i\");");

                            if (table.IdentityColumns.Count > 0)
                            {
                                yaz.WriteLine("\t\t\tthis.updateShow = AdminLib.UserRight(userRights, Model, \"u\");");
                                yaz.WriteLine("\t\t\tthis.copyShow = AdminLib.UserRight(userRights, Model, \"c\");");
                                yaz.WriteLine("\t\t\tthis.deleteShow = AdminLib.UserRight(userRights, Model, \"d\");");

                                if (Table == "Users")
                                    yaz.WriteLine("\t\t\tthis.changeGroupShow = AdminLib.UserRight(userRights, Model, \"cg\");");

                                if (table.Deleted)
                                    yaz.WriteLine("\t\t\tthis.removeShow = AdminLib.UserRight(userRights, Model, \"r\");");

                            }
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine(ttab + "\t\tif (this.callTable == true) {");
                        yaz.WriteLine(ttab + "\t\t\tthis.subscription = this.service.get(\"" + Table + "\", \"Index\").subscribe((resData: any) => {");


                        if (hasLinks)
                        {
                            if (Table == "Links")
                            {
                                yaz.WriteLine(ttab + "\t\t\t\tif (resData != null) {");
                                yaz.WriteLine(ttab + "\t\t\t\t\tfor (var i = 0; i < resData.length; i++) {");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\tswitch (resData[i].LinkedTypeID) {");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 1: resData[i].LinkedAdi = resData[i].LinkedCategoryAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 2: resData[i].LinkedAdi = resData[i].LinkedContentAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 3: resData[i].LinkedAdi = resData[i].LinkedProductAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 4: resData[i].LinkedAdi = resData[i].LinkedGalleryAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 5: resData[i].LinkedAdi = resData[i].LinkedPicturesAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 6: resData[i].LinkedAdi = resData[i].LinkedFilesAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 7: resData[i].LinkedAdi = resData[i].LinkedMetaAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t}");
                                yaz.WriteLine(ttab + "\t\t\t\t\t}");
                                yaz.WriteLine(ttab + "\t\t\t\t}");
                            }

                            if (Table == "LinkTypes")
                            {
                                yaz.WriteLine(ttab + "\t\t\t\tif (resData != null) {");
                                yaz.WriteLine(ttab + "\t\t\t\t\tfor (var i = 0; i < resData.length; i++) {");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\tswitch (resData[i].MainTypeID) {");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 1: resData[i].MainAdi = resData[i].MainCategoryAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 2: resData[i].MainAdi = resData[i].MainContentAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 3: resData[i].MainAdi = resData[i].MainProductAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 4: resData[i].MainAdi = resData[i].MainGalleryAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 5: resData[i].MainAdi = resData[i].MainPicturesAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 6: resData[i].MainAdi = resData[i].MainFilesAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t\tcase 7: resData[i].MainAdi = resData[i].MainMetaAdi; break;");
                                yaz.WriteLine(ttab + "\t\t\t\t\t\t}");
                                yaz.WriteLine(ttab + "\t\t\t\t\t}");
                                yaz.WriteLine(ttab + "\t\t\t\t}");
                            }

                            yaz.WriteLine("");
                        }

                        yaz.WriteLine(ttab + "\t\t\t\tthis." + Table + "List = resData;");
                        yaz.WriteLine(ttab + "\t\t\t\tthis.callTable = false;");
                        yaz.WriteLine(ttab + "");
                        yaz.WriteLine(ttab + "\t\t\t\tsetTimeout(() => {");
                        yaz.WriteLine(ttab + "\t\t\t\t\tDataTable();");
                        yaz.WriteLine(ttab + "");
                        yaz.WriteLine(ttab + "\t\t\t\t\t$(document)");
                        yaz.WriteLine(ttab + "\t\t\t\t\t\t.off(\"click\", \".fg-button\")");
                        yaz.WriteLine(ttab + "\t\t\t\t\t\t.on(\"click\", \".fg-button\", () => {");
                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\tsetTimeout(() => {");
                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\tthis.FillData(" + hasRightHiddenModel + ");");
                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\t}, 1);");
                        yaz.WriteLine(ttab + "\t\t\t\t\t\t});");
                        yaz.WriteLine(ttab + "\t\t\t\t}, 1);");
                        yaz.WriteLine(ttab + "\t\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
                        yaz.WriteLine(ttab + "\t\t}");
                        yaz.WriteLine(ttab + "");
                        yaz.WriteLine(ttab + "\t\tsetTimeout(() => {");
                        yaz.WriteLine(ttab + "\t\t\tif ($(\".dropdown-menu\").first().find(\"a\").length <= 0) {");
                        yaz.WriteLine(ttab + "\t\t\t\t$(\".btn-group\").remove();");
                        yaz.WriteLine(ttab + "\t\t\t}");
                        yaz.WriteLine(ttab + "\t\t}, 1);");

                        if (hasUserRights)
                        {
                            yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tngOnDestroy(): void {");
                        yaz.WriteLine("\t\tthis.subscription.unsubscribe();");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }

                if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                {
                    //Ekle
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + tableFolder + "\\" + Table.ToUrl(true) + "\\insert.ts", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                        {
                            string afterViewChecked = table.EDITORColumns.Count > 0 ? ", AfterViewChecked" : "";

                            yaz.WriteLine("import { Component" + afterViewChecked + " } from '@angular/core';");
                            yaz.WriteLine("import { Router } from '@angular/router';");
                            yaz.WriteLine("import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';");
                            yaz.WriteLine("import { Subscription } from 'rxjs';");
                            yaz.WriteLine("import { ModelService } from '../../../services/model';");

                            if (table.Columns.Where(a => a.DataType.ToLower().Contains("decimal")).Count() > 0 || table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0 || table.EDITORColumns.Count > 0)
                            {
                                yaz.WriteLine("import { AdminLib } from '../../../lib/lib';");
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("@Component({");
                            yaz.WriteLine("\ttemplateUrl: './insert.html'");
                            yaz.WriteLine("})");
                            yaz.WriteLine("");

                            afterViewChecked = table.EDITORColumns.Count > 0 ? " implements AfterViewChecked" : "";

                            yaz.WriteLine("export class Admin" + Table + "InsertComponent" + afterViewChecked + " {");
                            yaz.WriteLine("\terrorMsg: string;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tinsertForm: FormGroup;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tdata: any;");
                            yaz.WriteLine("\tmodel: any;");
                            yaz.WriteLine("");

                            if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                            {
                                yaz.WriteLine("\tuploadData: any;");

                                foreach (ColumnInfo item in table.FILEColumns)
                                {
                                    yaz.WriteLine("\tfile" + item.ColumnName + " : any;");
                                }

                                foreach (ColumnInfo item in table.IMAGEColumns)
                                {
                                    yaz.WriteLine("\timage" + item.ColumnName + ": any;");
                                }

                                foreach (ColumnInfo item in table.FILEColumns)
                                {
                                    yaz.WriteLine("\tname" + item.ColumnName + ": string;");
                                }

                                foreach (ColumnInfo item in table.IMAGEColumns)
                                {
                                    yaz.WriteLine("\tname" + item.ColumnName + ": string;");
                                }

                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\tprivate subscription: Subscription = new Subscription();");
                            yaz.WriteLine("");

                            yaz.WriteLine("\tconstructor(private service: ModelService, private formBuilder: FormBuilder, private router: Router) {");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\tngOnInit() {");
                            yaz.WriteLine("\t\tthis.data = new Object();");
                            yaz.WriteLine("");

                            if (table.FkcForeignList.Count > 0 || (Table == "Category" && table.Columns.Where(a => a.ColumnName == "ParentID").Count() > 0))
                            {
                                yaz.WriteLine("\t\tthis.subscription = this.service.get(\"" + Table + "\", \"Insert\").subscribe((answer: any) => {");
                                yaz.WriteLine("\t\t\tthis.model = answer;");
                                yaz.WriteLine("\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
                                yaz.WriteLine("");
                            }

                            int i = 0;

                            List<ColumnInfo> tempTableColumns = table.EDITORColumns;

                            foreach (ColumnInfo column in table.EDITORColumns)
                            {
                                yaz.WriteLine("\t\tAdminLib.ConvertToCKEditor(\"" + column.ColumnName + "\");");

                                if (tempTableColumns.Count == i + 1)
                                    yaz.WriteLine("");

                                i++;
                            }

                            yaz.WriteLine("\t\tthis.insertForm = this.formBuilder.group({");

                            List<ColumnInfo> viewColumns = table.Columns;

                            if (hasUserRights && Table == "Users")
                                viewColumns = viewColumns.Where(a => a.ColumnName != "LoginTime").ToList();

                            viewColumns = viewColumns.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList();

                            foreach (ColumnInfo column in viewColumns)
                            {
                                if (!column.IsIdentity)
                                {
                                    if (column.ColumnName.In(MailColumns, InType.ToUrlLower))
                                    {
                                        yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, Validators.compose([Validators.required, Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')])),");
                                    }
                                    else if (column.Type.Name == "Boolean")
                                    {
                                        yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null),");
                                    }
                                    else
                                    {
                                        if (column.IsNullable)
                                        {
                                            if (column.Type.Name == "String")
                                            {
                                                if (column.CharLength == -1)
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null),");
                                                }
                                                else
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.maxLength(" + column.CharLength + ")]),");
                                                }
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null),");
                                            }
                                        }
                                        else
                                        {
                                            if (column.Type.Name.In(new string[] { "Int16", "Int32", "Int64" }))
                                            {
                                                if (Table == "Category" && column.ColumnName == "ParentID")
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.min(0)]),");
                                                }
                                                else
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.min(1)]),");
                                                }
                                            }
                                            else if (column.Type.Name == "String")
                                            {
                                                if (column.Type.Name == "String" && column.CharLength == -1)
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.minLength(1)]),");
                                                }
                                                else
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.minLength(1), Validators.maxLength(" + column.CharLength + ")]),");
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t});");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");

                            if (table.EDITORColumns.Count > 0)
                            {
                                yaz.WriteLine("\tngAfterViewChecked() {");

                                foreach (ColumnInfo item in table.EDITORColumns)
                                {
                                    yaz.WriteLine("\t\t$(\"#" + item.ColumnName + "\").next(\"div.ck\").find(\".ck-content\").attr(\"data-id\", \"" + item.ColumnName + "\");");
                                }

                                yaz.WriteLine("\t}");
                                yaz.WriteLine("");
                            }

                            if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                            {
                                foreach (ColumnInfo item in table.FILEColumns)
                                {
                                    yaz.WriteLine("\ton" + item.ColumnName + "FileSelect(event) {");
                                    yaz.WriteLine("\t\tif (event.target.files.length > 0) {");
                                    yaz.WriteLine("\t\t\tthis.name" + item.ColumnName + " = AdminLib.UploadFileName(event.target.files[0].name);");
                                    yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + " = this.name" + item.ColumnName + ";");
                                    yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + "HasFile = true;");
                                    yaz.WriteLine("\t\t\tthis.file" + item.ColumnName + " = event.target.files[0];");
                                    yaz.WriteLine("\t\t}");
                                    yaz.WriteLine("\t}");
                                    yaz.WriteLine("");
                                }

                                foreach (ColumnInfo item in table.IMAGEColumns)
                                {
                                    yaz.WriteLine("\ton" + item.ColumnName + "FileSelect(event) {");
                                    yaz.WriteLine("\t\tif (event.target.files.length > 0) {");
                                    yaz.WriteLine("\t\t\tthis.name" + item.ColumnName + " = AdminLib.UploadFileName(event.target.files[0].name);");
                                    yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + " = this.name" + item.ColumnName + ";");
                                    yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + "HasFile = true;");
                                    yaz.WriteLine("\t\t\tthis.image" + item.ColumnName + " = event.target.files[0];");
                                    yaz.WriteLine("\t\t}");
                                    yaz.WriteLine("\t}");
                                    yaz.WriteLine("");
                                }
                            }

                            yaz.WriteLine("\tngOnDestroy(): void {");
                            yaz.WriteLine("\t\tthis.subscription.unsubscribe();");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");

                            if (hasLinks)
                            {
                                if (Table == "Links")
                                {
                                    yaz.WriteLine("\tonChange(event) {");
                                    yaz.WriteLine("\t\tvar target = event.target || event.srcElement || event.currentTarget;");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\tthis.service.get(\"Links\", \"FillObject\", null, null, target.value, null).subscribe((answer: any) => {");
                                    yaz.WriteLine("\t\t\tif (answer != null) {");
                                    yaz.WriteLine("\t\t\t\t$(\"select.selectLinkID\").html(\"\");");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\tfor (var i = 0; i < answer.length; i++) {");
                                    yaz.WriteLine("\t\t\t\t\t$(\"select.selectLinkID\").append(\"<option value='\" + answer[i].Value + \"'>\" + answer[i].Text + \"</option>\");");
                                    yaz.WriteLine("\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t\telse {");
                                    yaz.WriteLine("\t\t\t\t$(\".alertMessage\").text(\"Bağlı Nesne getirilemedi yada ilgili Bağlı Tip'e ait nesne henüz tanımlanmamış.\");");
                                    yaz.WriteLine("\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                                    yaz.WriteLine("\t}");
                                }

                                if (Table == "LinkTypes")
                                {
                                    yaz.WriteLine("\tonChange(event) {");
                                    yaz.WriteLine("\t\tvar target = event.target || event.srcElement || event.currentTarget;");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\tthis.service.get(\"LinkTypes\", \"FillTypes\", null, null, null, target.value).subscribe((answer: any) => {");
                                    yaz.WriteLine("\t\t\tif (answer != null) {");
                                    yaz.WriteLine("\t\t\t\t$(\"select.selectMain\").html(\"\");");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\tfor (var i = 0; i < answer.length; i++) {");
                                    yaz.WriteLine("\t\t\t\t\t$(\"select.selectMain\").append(\"<option value='\" + answer[i].Value + \"'>\" + answer[i].Text + \"</option>\");");
                                    yaz.WriteLine("\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t\telse {");
                                    yaz.WriteLine("\t\t\t\t$(\".alertMessage\").text(\"Ana Nesne getirilemedi yada ilgili Ana Tip'e ait nesne henüz tanımlanmamış.\");");
                                    yaz.WriteLine("\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                                    yaz.WriteLine("\t}");
                                }

                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\tonSubmit() {");

                            string tttab = table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0 ? "\t\t" : "";

                            if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                            {
                                yaz.WriteLine("\t\tthis.uploadData = new FormData();");
                                yaz.WriteLine("");

                                foreach (ColumnInfo item in table.FILEColumns)
                                {
                                    yaz.WriteLine("\t\tif (this.data." + item.ColumnName + "HasFile)");
                                    yaz.WriteLine("\t\t\tthis.uploadData.append(\"file\", this.file" + item.ColumnName + ", this.name" + item.ColumnName + ");");
                                    yaz.WriteLine("");
                                }

                                foreach (ColumnInfo item in table.IMAGEColumns)
                                {
                                    yaz.WriteLine("\t\tif (this.data." + item.ColumnName + "HasFile)");
                                    yaz.WriteLine("\t\t\tthis.uploadData.append(\"file\", this.image" + item.ColumnName + ", this.name" + item.ColumnName + ");");
                                    yaz.WriteLine("");
                                }

                                yaz.WriteLine("\t\tthis.subscription = this.service.post(\"" + Table + "\", \"InsertUpload\", this.uploadData).subscribe((answerUpload: any) => {");
                                yaz.WriteLine("\t\t\tif (answerUpload.Mesaj == null)");
                                yaz.WriteLine("\t\t\t{");
                            }

                            viewColumns = table.Columns;

                            if (hasUserRights && Table == "Users")
                                viewColumns = viewColumns.Where(a => a.ColumnName != "LoginTime").ToList();

                            viewColumns = viewColumns.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList();

                            int tcCount = viewColumns.Count;

                            i = 0;
                            foreach (ColumnInfo column in viewColumns)
                            {
                                if (!column.IsIdentity)
                                {
                                    if (column.Type.Name == "String" && column.CharLength == -1 && !column.ColumnName.In(FileColumns, InType.ToUrlLower) && !column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                    {
                                        yaz.WriteLine(tttab + "\t\tthis.data." + column.ColumnName + " = AdminLib.CKValue(\"" + column.ColumnName + "\");");
                                    }
                                    else if (!column.ColumnName.In(FileColumns, InType.ToUrlLower) && !column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                    {
                                        if (column.Type.Name.In(new string[] { "Decimal" }))
                                        {
                                            yaz.WriteLine(tttab + "\t\tthis.data." + column.ColumnName + " = AdminLib.ParseFloat(this.insertForm.get(\"" + column.ColumnName + "\").value);");
                                        }
                                        else
                                        {
                                            yaz.WriteLine(tttab + "\t\tthis.data." + column.ColumnName + " = this.insertForm.get(\"" + column.ColumnName + "\").value;");
                                        }
                                    }
                                }

                                i++;

                                if (i == tcCount)
                                {
                                    yaz.WriteLine("");
                                }
                            }

                            yaz.WriteLine(tttab + "\t\tthis.service.post(\"" + Table + "\", \"Insert\", this.data).subscribe((answer: any) => {");
                            yaz.WriteLine(tttab + "\t\t\tif (answer.Mesaj == null) {");
                            yaz.WriteLine(tttab + "\t\t\t\tthis.router.navigate(['/Admin/" + Table + "']);");
                            yaz.WriteLine(tttab + "\t\t\t}");
                            yaz.WriteLine(tttab + "\t\t\telse {");
                            yaz.WriteLine(tttab + "\t\t\t\t$(\".alertMessage\").text(answer.Mesaj);");
                            yaz.WriteLine(tttab + "\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                            yaz.WriteLine(tttab + "\t\t\t}");
                            yaz.WriteLine(tttab + "\t\t}, resError => this.errorMsg = resError);");

                            if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                            {
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("\t\t\telse");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\t$(\".alertMessage\").text(answerUpload.Mesaj);");
                                yaz.WriteLine("\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
                            }

                            yaz.WriteLine("\t}");
                            yaz.WriteLine("}");
                            yaz.Close();
                        }
                    }

                    if (table.IdentityColumns.Count > 0)
                    {
                        //Duzenle
                        using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + tableFolder + "\\" + Table.ToUrl(true) + "\\update.ts", FileMode.Create))
                        {
                            using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                            {
                                string afterViewChecked = table.EDITORColumns.Count > 0 ? ", AfterViewChecked" : "";

                                yaz.WriteLine("import { Component" + afterViewChecked + " } from '@angular/core';");
                                yaz.WriteLine("import { Router, ActivatedRoute, Params } from '@angular/router';");
                                yaz.WriteLine("import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';");
                                yaz.WriteLine("import { Subscription } from 'rxjs';");
                                yaz.WriteLine("import { ModelService } from '../../../services/model';");

                                bool updateHasRights = hasUserRights && table.FkcList.Count > 0 ? true : false;

                                if (updateHasRights)
                                {
                                    yaz.WriteLine("import { SharedService } from '../../../services/shared';");
                                }

                                if (updateHasRights || table.Columns.Where(a => a.DataType.ToLower().Contains("decimal")).Count() > 0 || table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0 || table.EDITORColumns.Count > 0)
                                {
                                    yaz.WriteLine("import { AdminLib } from '../../../lib/lib';");
                                }

                                if (table.FkcList.Count > 0)
                                {
                                    yaz.WriteLine("declare var DataTable;");
                                }

                                yaz.WriteLine("");
                                yaz.WriteLine("@Component({");
                                yaz.WriteLine("\ttemplateUrl: './update.html'");
                                yaz.WriteLine("})");
                                yaz.WriteLine("");

                                afterViewChecked = table.EDITORColumns.Count > 0 ? " implements AfterViewChecked" : "";

                                yaz.WriteLine("export class Admin" + Table + "UpdateComponent" + afterViewChecked + " {");
                                yaz.WriteLine("\terrorMsg: string;");
                                yaz.WriteLine("\tid: string;");
                                yaz.WriteLine("");
                                yaz.WriteLine("\tcallTable: boolean;");
                                yaz.WriteLine("");
                                yaz.WriteLine("\tupdateForm: FormGroup;");
                                yaz.WriteLine("");

                                if (updateHasRights)
                                {
                                    yaz.WriteLine("\tinsertShow: boolean = false;");
                                    yaz.WriteLine("\tupdateShow: boolean = false;");
                                    yaz.WriteLine("\tdeleteShow: boolean = false;");
                                    yaz.WriteLine("\tcopyShow: boolean = false;");

                                    if (table.Deleted)
                                        yaz.WriteLine("\tremoveShow: boolean = false;");

                                    if (Table == "Users")
                                        yaz.WriteLine("\tchangeGroupShow: boolean = false;");

                                    yaz.WriteLine("");
                                }

                                yaz.WriteLine("\tdata: any;");
                                yaz.WriteLine("\tmodel: any;");
                                yaz.WriteLine("");

                                if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                                {
                                    yaz.WriteLine("\tuploadData: any;");

                                    foreach (ColumnInfo item in table.FILEColumns)
                                    {
                                        yaz.WriteLine("\tfile" + item.ColumnName + ": any;");
                                    }

                                    foreach (ColumnInfo item in table.IMAGEColumns)
                                    {
                                        yaz.WriteLine("\timage" + item.ColumnName + ": any;");
                                    }

                                    foreach (ColumnInfo item in table.FILEColumns)
                                    {
                                        yaz.WriteLine("\tname" + item.ColumnName + ": string;");
                                    }

                                    foreach (ColumnInfo item in table.IMAGEColumns)
                                    {
                                        yaz.WriteLine("\tname" + item.ColumnName + ": string;");
                                    }

                                    yaz.WriteLine("");
                                }

                                yaz.WriteLine("\tprivate subscription: Subscription = new Subscription();");
                                yaz.WriteLine("");

                                string constHasUserRight = updateHasRights ? ", private sharedService: SharedService" : "";

                                yaz.WriteLine("\tconstructor(private service: ModelService" + constHasUserRight + ", private formBuilder: FormBuilder, private router: Router, private route: ActivatedRoute) {");
                                yaz.WriteLine("\t}");
                                yaz.WriteLine("");
                                yaz.WriteLine("\tngOnInit() {");
                                yaz.WriteLine("\t\tthis.data = new Object();");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\tthis.callTable = true;");

                                string hasRightHiddenModel = updateHasRights ? "$(\"#hdnType\").val()" : "";

                                yaz.WriteLine("\t\tthis.FillData(" + hasRightHiddenModel + ");");
                                yaz.WriteLine("");

                                int i = 0;

                                List<ColumnInfo> tempTableColumns = table.EDITORColumns;

                                foreach (ColumnInfo column in table.EDITORColumns)
                                {
                                    yaz.WriteLine("\t\tAdminLib.ConvertToCKEditor(\"Description\");");

                                    if (tempTableColumns.Count == i + 1)
                                        yaz.WriteLine("");

                                    i++;
                                }

                                yaz.WriteLine("\t\tthis.updateForm = this.formBuilder.group({");

                                List<ColumnInfo> viewColumns = table.Columns;

                                if (hasUserRights && Table == "Users")
                                    viewColumns = viewColumns.Where(a => a.ColumnName != "LoginTime").ToList();

                                viewColumns = viewColumns.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList();

                                foreach (ColumnInfo column in viewColumns)
                                {
                                    if (column.ColumnName.In(MailColumns, InType.ToUrlLower))
                                    {
                                        yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, Validators.compose([Validators.required, Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')])),");
                                    }
                                    else if (column.Type.Name == "Boolean")
                                    {
                                        yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null),");
                                    }
                                    else
                                    {
                                        if (column.IsNullable)
                                        {
                                            if (column.Type.Name == "String")
                                            {
                                                if (column.CharLength == -1)
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null),");
                                                }
                                                else
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.maxLength(" + column.CharLength + ")]),");
                                                }
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null),");
                                            }
                                        }
                                        else
                                        {
                                            if (column.Type.Name.In(new string[] { "Int16", "Int32", "Int64" }))
                                            {
                                                if (Table == "Category" && column.ColumnName == "ParentID")
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.min(0)]),");
                                                }
                                                else
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.min(1)]),");
                                                }
                                            }
                                            else if (column.Type.Name == "String")
                                            {
                                                if (column.Type.Name == "String" && column.CharLength == -1)
                                                {
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.minLength(1)]),");
                                                }
                                                else
                                                {
                                                    string validator = hasUserRights && Table == "Users" && column.ColumnName == "Password" ? "" : ", [Validators.required, Validators.minLength(1), Validators.maxLength(" + column.CharLength + ")]";
                                                    yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null" + validator + "),");
                                                }
                                            }
                                        }
                                    }
                                }

                                yaz.WriteLine("\t\t});");
                                yaz.WriteLine("\t}");
                                yaz.WriteLine("");

                                if (table.EDITORColumns.Count > 0)
                                {
                                    yaz.WriteLine("\tngAfterViewChecked() {");

                                    foreach (ColumnInfo item in table.EDITORColumns)
                                    {
                                        yaz.WriteLine("\t\t$(\"#" + item.ColumnName + "\").next(\"div.ck\").find(\".ck-content\").attr(\"data-id\", \"" + item.ColumnName + "\");");
                                    }

                                    yaz.WriteLine("\t}");
                                    yaz.WriteLine("");
                                }

                                if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                                {
                                    foreach (ColumnInfo item in table.FILEColumns)
                                    {
                                        yaz.WriteLine("\ton" + item.ColumnName + "FileSelect(event) {");
                                        yaz.WriteLine("\t\tif (event.target.files.length > 0) {");
                                        yaz.WriteLine("\t\t\tthis.name" + item.ColumnName + " = AdminLib.UploadFileName(event.target.files[0].name);");
                                        yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + " = this.name" + item.ColumnName + ";");
                                        yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + "HasFile = true;");
                                        yaz.WriteLine("\t\t\tthis.file" + item.ColumnName + " = event.target.files[0];");
                                        yaz.WriteLine("\t\t}");
                                        yaz.WriteLine("\t}");
                                        yaz.WriteLine("");
                                    }

                                    foreach (ColumnInfo item in table.IMAGEColumns)
                                    {
                                        yaz.WriteLine("\ton" + item.ColumnName + "FileSelect(event) {");
                                        yaz.WriteLine("\t\tif (event.target.files.length > 0) {");
                                        yaz.WriteLine("\t\t\tthis.name" + item.ColumnName + " = AdminLib.UploadFileName(event.target.files[0].name);");
                                        yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + " = this.name" + item.ColumnName + ";");
                                        yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + "HasFile = true;");
                                        yaz.WriteLine("\t\t\tthis.image" + item.ColumnName + " = event.target.files[0];");
                                        yaz.WriteLine("\t\t}");
                                        yaz.WriteLine("\t}");
                                        yaz.WriteLine("");
                                    }
                                }

                                yaz.WriteLine("\tngOnDestroy(): void {");
                                yaz.WriteLine("\t\tthis.subscription.unsubscribe();");
                                yaz.WriteLine("\t}");
                                yaz.WriteLine("");

                                if (hasLinks)
                                {
                                    if (Table == "LinkTypes")
                                    {
                                        yaz.WriteLine("\tonChange(event) {");
                                        yaz.WriteLine("\t\tvar target = event.target || event.srcElement || event.currentTarget;");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\tthis.service.get(\"LinkTypes\", null, null, null, target.value).subscribe((answer: any) => {");
                                        yaz.WriteLine("\t\t\tif (answer != null) {");
                                        yaz.WriteLine("\t\t\t\t$(\"select.selectMain\").html(\"\");");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\t\tfor (var i = 0; i < answer.length; i++) {");
                                        yaz.WriteLine("\t\t\t\t\t$(\"select.selectMain\").append(\"<option value='\" + answer[i].Value + \"'>\" + answer[i].Text + \"</option>\");");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("\t\t\telse {");
                                        yaz.WriteLine("\t\t\t\t$(\".alertMessage\").text(\"Ana Nesne getirilemedi yada ilgili Ana Tip'e ait nesne henüz tanımlanmamış.\");");
                                        yaz.WriteLine("\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                                        yaz.WriteLine("\t}");
                                        yaz.WriteLine("");
                                    }
                                }

                                yaz.WriteLine("\tonSubmit() {");

                                string tttab = table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0 ? "\t\t" : "";

                                if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                                {
                                    yaz.WriteLine("\t\tthis.uploadData = new FormData();");
                                    yaz.WriteLine("");

                                    foreach (ColumnInfo item in table.FILEColumns)
                                    {
                                        yaz.WriteLine("\t\tif (this.data." + item.ColumnName + "HasFile)");
                                        yaz.WriteLine("\t\t\tthis.uploadData.append(\"file\", this.file" + item.ColumnName + ", this.name" + item.ColumnName + ");");
                                        yaz.WriteLine("");
                                    }

                                    foreach (ColumnInfo item in table.IMAGEColumns)
                                    {
                                        yaz.WriteLine("\t\tif (this.data." + item.ColumnName + "HasFile)");
                                        yaz.WriteLine("\t\t\tthis.uploadData.append(\"file\", this.image" + item.ColumnName + ", this.name" + item.ColumnName + ");");
                                        yaz.WriteLine("");
                                    }

                                    yaz.WriteLine("\t\tthis.subscription = this.service.post(\"" + Table + "\", \"UpdateUpload\", this.uploadData).subscribe((answerUpload: any) => {");
                                    yaz.WriteLine("\t\t\tif (answerUpload.Mesaj == null)");
                                    yaz.WriteLine("\t\t\t{");
                                }

                                viewColumns = table.Columns;

                                if (hasUserRights && Table == "Users")
                                    viewColumns = viewColumns.Where(a => a.ColumnName != "LoginTime").ToList();

                                viewColumns = viewColumns.Where(a => !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList();

                                int tcCount = viewColumns.Count;

                                i = 0;
                                foreach (ColumnInfo column in viewColumns)
                                {
                                    if (column.Type.Name == "String" && column.CharLength == -1 && !column.ColumnName.In(FileColumns, InType.ToUrlLower) && !column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                    {
                                        yaz.WriteLine(tttab + "\t\tthis.data." + column.ColumnName + " = AdminLib.CKValue(\"" + column.ColumnName + "\");");
                                    }
                                    else if (column.ColumnName.In(FileColumns, InType.ToUrlLower) || column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                    {
                                        yaz.WriteLine("");
                                        yaz.WriteLine(tttab + "\t\tif (this.data." + column.ColumnName + "HasFile) {");
                                        yaz.WriteLine(tttab + "\t\t\tthis.data.Old" + column.ColumnName + " = this.updateForm.get(\"" + column.ColumnName + "\").value;");
                                        yaz.WriteLine(tttab + "\t\t}");
                                        yaz.WriteLine(tttab + "\t\telse {");
                                        yaz.WriteLine(tttab + "\t\t\tthis.data." + column.ColumnName + " = this.updateForm.get(\"" + column.ColumnName + "\").value;");
                                        yaz.WriteLine(tttab + "\t\t}");
                                        yaz.WriteLine("");
                                    }
                                    else
                                    {
                                        if (column.Type.Name.In(new string[] { "Decimal" }))
                                        {
                                            yaz.WriteLine(tttab + "\t\tthis.data." + column.ColumnName + " = AdminLib.ParseFloat(this.updateForm.get(\"" + column.ColumnName + "\").value);");
                                        }
                                        else
                                        {
                                            string value = hasUserRights && Table == "Users" && column.ColumnName == "Password" ? "$(\"#" + column.ColumnName + "\").val()" : "this.updateForm.get(\"" + column.ColumnName + "\").value";
                                            yaz.WriteLine(tttab + "\t\tthis.data." + column.ColumnName + " = " + value + ";");
                                        }
                                    }

                                    i++;

                                    if (i == tcCount)
                                    {
                                        yaz.WriteLine("");
                                    }
                                }

                                yaz.WriteLine(tttab + "\t\tthis.service.post(\"" + Table + "\", \"Update\", this.data).subscribe((answer: any) => {");
                                yaz.WriteLine(tttab + "\t\t\tif (answer.Mesaj == null) {");
                                yaz.WriteLine(tttab + "\t\t\t\tthis.router.navigate(['/Admin/" + Table + "']);");
                                yaz.WriteLine(tttab + "\t\t\t}");
                                yaz.WriteLine(tttab + "\t\t\telse {");
                                yaz.WriteLine(tttab + "\t\t\t\t$(\".alertMessage\").text(answer.Mesaj);");
                                yaz.WriteLine(tttab + "\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                                yaz.WriteLine(tttab + "\t\t\t}");
                                yaz.WriteLine(tttab + "\t\t}, resError => this.errorMsg = resError);");

                                if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                                {
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t\telse");
                                    yaz.WriteLine("\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t$(\".alertMessage\").text(answerUpload.Mesaj);");
                                    yaz.WriteLine("\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
                                }

                                yaz.WriteLine("\t}");
                                yaz.WriteLine("");

                                string hasRightModel = updateHasRights ? "Model: any" : "";

                                yaz.WriteLine("\tFillData(" + hasRightModel + ") {");

                                string ttab = updateHasRights ? "\t\t" : "";

                                if (updateHasRights)
                                {
                                    yaz.WriteLine("\t\tthis.sharedService.getCurrentUserRights(Model).subscribe((userRights: any) => {");
                                    yaz.WriteLine("\t\t\tthis.insertShow = AdminLib.UserRight(userRights, Model, \"i\");");
                                    yaz.WriteLine("\t\t\tthis.updateShow = AdminLib.UserRight(userRights, Model, \"u\");");
                                    yaz.WriteLine("\t\t\tthis.copyShow = AdminLib.UserRight(userRights, Model, \"c\");");
                                    yaz.WriteLine("\t\t\tthis.deleteShow = AdminLib.UserRight(userRights, Model, \"d\");");

                                    if (Table == "Users")
                                        yaz.WriteLine("\t\t\tthis.changeGroupShow = AdminLib.UserRight(userRights, Model, \"cg\");");

                                    if (table.Deleted)
                                        yaz.WriteLine("\t\t\tthis.removeShow = AdminLib.UserRight(userRights, Model, \"r\");");

                                    yaz.WriteLine("");
                                }

                                yaz.WriteLine(ttab + "\t\tif (this.callTable == true) {");
                                yaz.WriteLine(ttab + "\t\t\tthis.route.params.subscribe((params: Params) => {");
                                yaz.WriteLine(ttab + "\t\t\t\tthis.id = params['id'];");
                                yaz.WriteLine(ttab + "\t\t\t\tthis.subscription = this.service.get(\"" + Table + "\", \"Update\", this.id).subscribe((resData: any) => {");

                                if (hasLinks)
                                {
                                    if (Table == "LinkTypes")
                                    {
                                        yaz.WriteLine(ttab + "\t\t\t\t\tif (resData != null) {");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\tfor (var i = 0; i < resData.LinkList.length; i++) {");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\tswitch (resData.LinkedTypeID) {");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\tcase 1: resData.LinksList[i].LinkedAdi = resData.LinkList[i].LinkedCategoryAdi; break;");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\tcase 2: resData.LinksList[i].LinkedAdi = resData.LinkList[i].LinkedContentAdi; break;");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\tcase 3: resData.LinksList[i].LinkedAdi = resData.LinkList[i].LinkedProductAdi; break;");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\tcase 4: resData.LinksList[i].LinkedAdi = resData.LinkList[i].LinkedGalleryAdi; break;");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\tcase 5: resData.LinksList[i].LinkedAdi = resData.LinkList[i].LinkedPicturesAdi; break;");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\tcase 6: resData.LinksList[i].LinkedAdi = resData.LinkList[i].LinkedFilesAdi; break;");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\tcase 7: resData.LinksList[i].LinkedAdi = resData.LinkList[i].LinkedMetaAdi; break;");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\t\t}");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t\t}");
                                        yaz.WriteLine(ttab + "\t\t\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }

                                yaz.WriteLine(ttab + "\t\t\t\t\tthis.model = resData;");
                                yaz.WriteLine(ttab + "\t\t\t\t\tthis.callTable = false;");

                                if (table.FkcList.Count > 0)
                                {
                                    yaz.WriteLine("");
                                    yaz.WriteLine(ttab + "\t\t\t\t\tsetTimeout(() => {");
                                    yaz.WriteLine(ttab + "\t\t\t\t\t\tDataTable();");
                                    yaz.WriteLine("");
                                    yaz.WriteLine(ttab + "\t\t\t\t\t\t$(document)");
                                    yaz.WriteLine(ttab + "\t\t\t\t\t\t\t.off(\"click\", \".fg-button\")");
                                    yaz.WriteLine(ttab + "\t\t\t\t\t\t\t.on(\"click\", \".fg-button\", () => {");
                                    yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\tsetTimeout(() => {");
                                    yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\t\tthis.FillData(" + hasRightHiddenModel + ");");
                                    yaz.WriteLine(ttab + "\t\t\t\t\t\t\t\t}, 1);");
                                    yaz.WriteLine(ttab + "\t\t\t\t\t\t\t});");
                                    yaz.WriteLine(ttab + "\t\t\t\t\t}, 1);");
                                }

                                yaz.WriteLine(ttab + "\t\t\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
                                yaz.WriteLine(ttab + "\t\t\t});");
                                yaz.WriteLine(ttab + "\t\t}");

                                if (table.FkcList.Count > 0)
                                {
                                    yaz.WriteLine("");
                                    yaz.WriteLine(ttab + "\t\tsetTimeout(() => {");
                                    yaz.WriteLine(ttab + "\t\t\tif ($(\".dropdown-menu\").first().find(\"a\").length <= 0) {");
                                    yaz.WriteLine(ttab + "\t\t\t\t$(\".btn-group\").remove();");
                                    yaz.WriteLine(ttab + "\t\t\t}");
                                    yaz.WriteLine(ttab + "\t\t}, 1);");
                                }

                                if (updateHasRights)
                                {
                                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                                }

                                yaz.WriteLine("\t}");

                                yaz.WriteLine("}");
                                yaz.Close();
                            }
                        }

                        if (Table == "Users" && hasUserRights)
                        {
                            //GrupDegistir
                            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + tableFolder + "\\" + Table.ToUrl(true) + "\\changegroup.ts", FileMode.Create))
                            {
                                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                                {
                                    yaz.WriteLine("import { Component } from '@angular/core';");
                                    yaz.WriteLine("import { ModelService } from '../../../services/model';");
                                    yaz.WriteLine("import { Router, ActivatedRoute, Params } from '@angular/router';");
                                    yaz.WriteLine("import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("@Component({");
                                    yaz.WriteLine("\ttemplateUrl: './changegroup.html'");
                                    yaz.WriteLine("})");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("export class AdminUsersChangeGroupComponent {");
                                    yaz.WriteLine("\terrorMsg: string;");
                                    yaz.WriteLine("\tid: string;");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\tchangeGroupForm: FormGroup;");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\tdata: any;");
                                    yaz.WriteLine("\tmodel: any;");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\tconstructor(private service: ModelService, private router: Router, private route: ActivatedRoute, private formBuilder: FormBuilder) {");
                                    yaz.WriteLine("\t}");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\tngOnInit() {");
                                    yaz.WriteLine("\t\tthis.data = new Object();");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\tthis.route.params.subscribe((params: Params) => {");
                                    yaz.WriteLine("\t\t\tthis.id = params['id'];");
                                    yaz.WriteLine("\t\t\tthis.service.get(\"Users\", \"ChangeGroup\", this.id).subscribe((resData: any) => {");
                                    yaz.WriteLine("\t\t\t\tthis.model = resData;");
                                    yaz.WriteLine("\t\t\t}, resError => this.errorMsg = resError);");
                                    yaz.WriteLine("\t\t});");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\tthis.changeGroupForm = this.formBuilder.group({");
                                    yaz.WriteLine("\t\t\tID: new FormControl(null, [Validators.required, Validators.min(1)]),");
                                    yaz.WriteLine("\t\t\tGroupID: new FormControl(null, [Validators.required, Validators.min(1)]),");
                                    yaz.WriteLine("\t\t});");
                                    yaz.WriteLine("\t}");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\tonSubmit() {");
                                    yaz.WriteLine("\t\tthis.data.ID = this.changeGroupForm.get(\"ID\").value;");
                                    yaz.WriteLine("\t\tthis.data.GroupID = this.changeGroupForm.get(\"GroupID\").value;");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\tthis.service.post(\"Users\", \"ChangeGroup\", this.data).subscribe((answer: any) => {");
                                    yaz.WriteLine("\t\t\tif (answer.Mesaj == null) {");
                                    yaz.WriteLine("\t\t\t\tthis.router.navigate(['/Admin/Users']);");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t\telse {");
                                    yaz.WriteLine("\t\t\t\t$(\".alertMessage\").text(answer.Mesaj);");
                                    yaz.WriteLine("\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError);");
                                    yaz.WriteLine("\t}");
                                    yaz.WriteLine("}");
                                    yaz.Close();
                                }
                            }
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
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/libs/runtime.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/libs/polyfills.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/libs/styles.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/libs/vendor.js\"></script>");
                    yaz.WriteLine("<script type=\"text/javascript\" src=\"@AppMgr.ScriptPath/libs/main.js\"></script>");

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
                    yaz.WriteLine("<" + projectName.Substring(0, 3).ToUrl(true) + "-app></" + projectName.Substring(0, 3).ToUrl(true) + "-app>");
                    yaz.Close();
                }
            }
        }

        void CreateAngularControllerLayer()
        {
            CreateAngularHomeController();
            CreateAngularSiteController();
            CreateAngularSharedController();

            foreach (string Table in selectedTables)
            {
                Table table = new Table(Table, connectionInfo);
                FillTable(table);
                SqlConnection con = new SqlConnection(Helper.CreateConnectionText(connectionInfo));

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers\\" + Table + "Controller.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System.Web.Mvc;");

                        if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                        {
                            yaz.WriteLine("using System.Collections.Generic;");
                        }

                        if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0 || (hasLinks && Table == "Links"))
                        {
                            yaz.WriteLine("using TDLibrary;");
                        }

                        yaz.WriteLine("using " + repositoryName + "." + Table + "Model;");

                        if (hasUserRights)
                            yaz.WriteLine("using " + repositoryName + ".UsersModel;");

                        yaz.WriteLine("");

                        yaz.WriteLine("namespace " + projectName + ".Areas.Ajax.Controllers");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class " + Table + "Controller : Controller");
                        yaz.WriteLine("\t{");

                        yaz.WriteLine("\t\treadonly " + Table + " model = new " + Table + "();");
                        yaz.WriteLine("\t\treadonly Users curUser = AppTools.User;");

                        yaz.WriteLine("");

                        // Index
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult Index(int? id)");
                        yaz.WriteLine("\t\t{");

                        if (hasUserRights)
                        {
                            yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\"))");
                            yaz.WriteLine("\t\t\t\treturn Json(null, JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\t\t\treturn Json(model.List(id, null, false), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                        {
                            if (table.FkcForeignList.Count > 0 || (Table == "Category" && table.Columns.Where(a => a.ColumnName == "ParentID").Count() > 0))
                            {
                                // Ekle
                                yaz.WriteLine("\t\t[HttpGet]");
                                yaz.WriteLine("\t\tpublic JsonResult Insert()");
                                yaz.WriteLine("\t\t{");

                                if (hasUserRights)
                                {
                                    yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"i\"))");
                                    yaz.WriteLine("\t\t\t\treturn Json(null, JsonRequestBehavior.AllowGet);");
                                    yaz.WriteLine("");
                                }

                                yaz.WriteLine("\t\t\treturn Json(model.Insert(), JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("\t\t}");
                                yaz.WriteLine("");
                            }

                            // Ekle
                            yaz.WriteLine("\t\t[HttpPost]");
                            yaz.WriteLine("\t\tpublic JsonResult Insert([System.Web.Http.FromBody] " + Table + " table)");
                            yaz.WriteLine("\t\t{");

                            if (hasUserRights)
                            {
                                yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"i\"))");
                                yaz.WriteLine("\t\t\t\treturn Json(null);");
                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\t\t\tbool result = model.Insert(table);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (result)");
                            yaz.WriteLine("\t\t\t{");

                            if (hasLogs)
                            {
                                yaz.WriteLine("\t\t\t\tcurUser.Log(table, \"i\", \"" + Table.ToTurkish(lstLang) + "\");");
                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\t\t\t\treturn Json(table);");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t\telse");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\ttable.Mesaj = \"Kayıt eklenemedi.\";");
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

                            yaz.WriteLine("\t\t\treturn Json(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                            {
                                // EkleYukle
                                yaz.WriteLine("\t\t[HttpPost]");
                                yaz.WriteLine("\t\tpublic JsonResult InsertUpload([System.Web.Http.FromBody] " + Table + " table)");
                                yaz.WriteLine("\t\t{");

                                if (hasUserRights)
                                {
                                    yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"i\"))");
                                    yaz.WriteLine("\t\t\t\treturn Json(null);");
                                    yaz.WriteLine("");
                                }

                                if (table.FILEColumns.Count > 0)
                                {
                                    yaz.WriteLine("\t\t\tList<Uploader> files = Uploader.UploadFiles(false);");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\tforeach (var item in files)");
                                    yaz.WriteLine("\t\t\t{");
                                    yaz.WriteLine("\t\t\t\tif (!item.Control)");
                                    yaz.WriteLine("\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\ttable.Mesaj = item.ErrorMessage;");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t\treturn Json(table);");
                                    yaz.WriteLine("\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("");
                                }

                                if (table.IMAGEColumns.Count > 0)
                                {
                                    yaz.WriteLine("\t\t\tList<Uploader> pictures = Uploader.UploadPictures(false);");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\tforeach (var item in pictures)");
                                    yaz.WriteLine("\t\t\t{");
                                    yaz.WriteLine("\t\t\t\tif (!item.Control)");
                                    yaz.WriteLine("\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\ttable.Mesaj = item.ErrorMessage;");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t\treturn Json(table);");
                                    yaz.WriteLine("\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("");
                                }

                                yaz.WriteLine("\t\t\treturn Json(table);");
                                yaz.WriteLine("\t\t}");
                                yaz.WriteLine("");
                            }
                        }

                        if (table.IdentityColumns.Count > 0)
                        {
                            string columntype = table.Columns.Where(a => a.ColumnName == table.ID).FirstOrDefault().Type.Name.ToString();

                            if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                            {
                                //Duzenle
                                yaz.WriteLine("\t\t[HttpGet]");
                                yaz.WriteLine("\t\tpublic JsonResult Update(" + columntype.ReturnCSharpType() + "? id)");
                                yaz.WriteLine("\t\t{");

                                if (hasUserRights)
                                {
                                    yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"u\"))");
                                    yaz.WriteLine("\t\t\t\treturn Json(null, JsonRequestBehavior.AllowGet);");
                                    yaz.WriteLine("");
                                }

                                yaz.WriteLine("\t\t\treturn Json(model.Update(id), JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("\t\t}");
                                yaz.WriteLine("");

                                //Duzenle
                                yaz.WriteLine("\t\t[HttpPost]");
                                yaz.WriteLine("\t\tpublic JsonResult Update([System.Web.Http.FromBody] " + Table + " table)");
                                yaz.WriteLine("\t\t{");

                                if (hasUserRights)
                                {
                                    yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"u\"))");
                                    yaz.WriteLine("\t\t\t\treturn Json(null);");
                                    yaz.WriteLine("");
                                }

                                foreach (ColumnInfo item in table.FILEColumns)
                                {
                                    yaz.WriteLine("\t\t\tif (table." + item.ColumnName + "HasFile)");
                                    yaz.WriteLine("\t\t\t{");
                                    yaz.WriteLine("\t\t\t\ttry");
                                    yaz.WriteLine("\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\tSystem.IO.File.Delete(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table.Old" + item.ColumnName + "));");
                                    yaz.WriteLine("\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t\tcatch");
                                    yaz.WriteLine("\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\ttable.Mesaj = \"Eski (\" + table.Old" + item.ColumnName + " + \") dosyası silinemedi.\";");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t\treturn Json(table);");
                                    yaz.WriteLine("\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("");
                                }

                                foreach (ColumnInfo item in table.IMAGEColumns)
                                {
                                    yaz.WriteLine("\t\t\tif (table." + item.ColumnName + "HasFile)");
                                    yaz.WriteLine("\t\t\t{");
                                    yaz.WriteLine("\t\t\t\ttry");
                                    yaz.WriteLine("\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\tSystem.IO.File.Delete(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table.Old" + item.ColumnName + "));");
                                    yaz.WriteLine("\t\t\t\t\tSystem.IO.File.Delete(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/thumb_\" + table.Old" + item.ColumnName + "));");
                                    yaz.WriteLine("\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t\tcatch");
                                    yaz.WriteLine("\t\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t\ttable.Mesaj = \"Eski (\" + table.Old" + item.ColumnName + " + \") dosyası silinemedi.\";");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t\treturn Json(table);");
                                    yaz.WriteLine("\t\t\t\t}");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("");
                                }

                                if (hasUserRights && Table == "Users")
                                {
                                    yaz.WriteLine("\t\t\tif (curUser?.ID == table.ID)");
                                    yaz.WriteLine("\t\t\t{");
                                    yaz.WriteLine("");
                                }

                                string tttab = hasUserRights && Table == "Users" ? "\t" : "";
                                string curUserID = hasUserRights && Table == "Users" ? ", curUser.ID" : "";

                                yaz.WriteLine(tttab + "\t\t\tbool result = model.Update(table" + curUserID + ");");
                                yaz.WriteLine("");

                                yaz.WriteLine(tttab + "\t\t\tif (result)");
                                yaz.WriteLine(tttab + "\t\t\t{");

                                if (hasLogs)
                                {
                                    yaz.WriteLine(tttab + "\t\t\t\tcurUser.Log(table, \"u\", \"" + Table.ToTurkish(lstLang) + "\");");
                                    yaz.WriteLine(tttab + "");
                                }

                                yaz.WriteLine(tttab + "\t\t\t\treturn Json(table);");
                                yaz.WriteLine(tttab + "\t\t\t}");
                                yaz.WriteLine(tttab + "\t\t\telse");
                                yaz.WriteLine(tttab + "\t\t\t{");
                                yaz.WriteLine(tttab + "\t\t\t\ttable.Mesaj = \"Kayıt düzenlenemedi.\";");
                                yaz.WriteLine(tttab + "\t\t\t}");

                                if (hasUserRights && Table == "Users")
                                {
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t\telse");
                                    yaz.WriteLine("\t\t\t{");
                                    yaz.WriteLine("\t\t\t\ttable.Mesaj = \"Sadece kendi kullanıcı bilgilerinizi düzenleyebilirsiniz.\";");
                                    yaz.WriteLine("\t\t\t}");
                                }

                                yaz.WriteLine("");

                                yaz.WriteLine("\t\t\ttable = (" + Table + ")model.Update(table." + table.ID + ", table);");
                                yaz.WriteLine("");

                                yaz.WriteLine("\t\t\treturn Json(table);");
                                yaz.WriteLine("\t\t}");
                                yaz.WriteLine("");

                                if (table.FILEColumns.Count > 0 || table.IMAGEColumns.Count > 0)
                                {
                                    //DuzenleYukle
                                    yaz.WriteLine("\t\t[HttpPost]");
                                    yaz.WriteLine("\t\tpublic JsonResult UpdateUpload([System.Web.Http.FromBody] " + Table + " table)");
                                    yaz.WriteLine("\t\t{");

                                    if (hasUserRights)
                                    {
                                        yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"u\"))");
                                        yaz.WriteLine("\t\t\t\treturn Json(null);");
                                        yaz.WriteLine("");
                                    }

                                    if (table.FILEColumns.Count > 0)
                                    {
                                        yaz.WriteLine("\t\t\tList<Uploader> files = Uploader.UploadFiles(false);");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (var item in files)");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\tif (item.UploadError != null)");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\t\t\ttable.Mesaj = item.ErrorMessage;");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\t\t\treturn Json(table);");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }

                                    if (table.IMAGEColumns.Count > 0)
                                    {
                                        yaz.WriteLine("\t\t\tList<Uploader> pictures = Uploader.UploadPictures(false);");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\tforeach (var item in pictures)");
                                        yaz.WriteLine("\t\t\t{");
                                        yaz.WriteLine("\t\t\t\tif (item.UploadError != null)");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\t\t\ttable.Mesaj = item.ErrorMessage;");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\t\t\treturn Json(table);");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }

                                    yaz.WriteLine("\t\t\treturn Json(table);");
                                    yaz.WriteLine("\t\t}");
                                    yaz.WriteLine("");
                                }
                            }

                            string idUserControl = hasUserRights && Table == "Users" ? " || curUser?.ID == id" : "";

                            if (!((hasVisitors || hasLogs) && (Table == "Visitors" || Table == "Logs")))
                            {
                                //Kopyala
                                yaz.WriteLine("\t\t[HttpGet]");
                                yaz.WriteLine("\t\tpublic JsonResult Copy(" + columntype.ReturnCSharpType() + " id)");
                                yaz.WriteLine("\t\t{");

                                if (hasUserRights)
                                {
                                    yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"c\")" + idUserControl + ")");
                                    yaz.WriteLine("\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                                    yaz.WriteLine("");
                                }

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
                                    yaz.WriteLine("\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                                    yaz.WriteLine("\t\t\t}");

                                    yaz.WriteLine("");
                                }
                                yaz.WriteLine("\t\t\tbool result = model.Copy(id);");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tif (result)");
                                yaz.WriteLine("\t\t\t{");

                                if (hasLogs)
                                {
                                    yaz.WriteLine("\t\t\t\tcurUser.Log(id, \"c\", \"" + Table.ToTurkish(lstLang) + "\");");
                                    yaz.WriteLine("");
                                }

                                yaz.WriteLine("\t\t\t\treturn Json(true, JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("\t\t}");
                                yaz.WriteLine("");

                                //Sil
                                yaz.WriteLine("\t\t[HttpGet]");
                                yaz.WriteLine("\t\tpublic JsonResult Delete(" + columntype.ReturnCSharpType() + "? id)");
                                yaz.WriteLine("\t\t{");

                                if (hasUserRights)
                                {
                                    yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"d\")" + idUserControl + ")");
                                    yaz.WriteLine("\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                                    yaz.WriteLine("");
                                }

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
                                    yaz.WriteLine("\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("");
                                }
                                yaz.WriteLine("\t\t\tbool result = model.Delete(id);");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\tif (result)");
                                yaz.WriteLine("\t\t\t{");

                                if (hasLogs)
                                {
                                    yaz.WriteLine("\t\t\t\tcurUser.Log(id, \"d\", \"" + Table.ToTurkish(lstLang) + "\");");
                                    yaz.WriteLine("");
                                }

                                yaz.WriteLine("\t\t\t\treturn Json(true, JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("\t\t}");

                                if (table.Deleted)
                                {
                                    //Kaldır
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t[HttpGet]");
                                    yaz.WriteLine("\t\tpublic JsonResult Remove(" + columntype.ReturnCSharpType() + "? id)");
                                    yaz.WriteLine("\t\t{");

                                    if (hasUserRights)
                                    {
                                        yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"r\")" + idUserControl + ")");
                                        yaz.WriteLine("\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                                        yaz.WriteLine("");
                                    }

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
                                        yaz.WriteLine("\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("");
                                    }

                                    yaz.WriteLine("\t\t\tbool result = model.Remove(id);");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\tif (result)");
                                    yaz.WriteLine("\t\t\t{");

                                    if (hasLogs)
                                    {
                                        yaz.WriteLine("\t\t\t\tcurUser.Log(id, \"r\", \"" + Table.ToTurkish(lstLang) + "\");");
                                        yaz.WriteLine("");
                                    }

                                    yaz.WriteLine("\t\t\t\treturn Json(true, JsonRequestBehavior.AllowGet);");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                                    yaz.WriteLine("\t\t}");
                                }
                            }

                            if (Table == "Users" && hasUserRights)
                            {
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t[HttpGet]");
                                yaz.WriteLine("\t\tpublic JsonResult ChangeGroup(int id)");
                                yaz.WriteLine("\t\t{");
                                yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"Users\", \"cg\"))");
                                yaz.WriteLine("\t\t\t\treturn Json(null, JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\treturn Json(model.ChangeGroup(id), JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("\t\t}");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t[HttpPost]");
                                yaz.WriteLine("\t\tpublic JsonResult ChangeGroup([System.Web.Http.FromBody] Users table)");
                                yaz.WriteLine("\t\t{");
                                yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"Users\", \"cg\"))");
                                yaz.WriteLine("\t\t\t\treturn Json(null);");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\ttry");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\tbool result = model.ChangeGroup(table);");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\tif (result)");
                                yaz.WriteLine("\t\t\t\t{");
                                yaz.WriteLine("\t\t\t\t\tcurUser.Log(table, \"cg\", \"" + Table.ToUserRightType() + "\");");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\treturn Json(table);");
                                yaz.WriteLine("\t\t\t\t}");
                                yaz.WriteLine("\t\t\t\telse");
                                yaz.WriteLine("\t\t\t\t\ttable.Mesaj = \"Grup değiştirilemedi.\";");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("\t\t\tcatch");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\ttable.Mesaj = \"Grup değiştirilemedi.\";");
                                yaz.WriteLine("\t\t\t}");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\ttable = (Users)model.ChangeGroup(table.ID, table);");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\treturn Json(table);");
                                yaz.WriteLine("\t\t}");
                            }
                        }

                        if (hasLinks)
                        {
                            if (Table == "Links")
                            {
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t[HttpGet]");
                                yaz.WriteLine("\t\tpublic JsonResult FillObject(string linkTypeID)");
                                yaz.WriteLine("\t\t{");
                                yaz.WriteLine("\t\t\treturn Json(Links.ReturnList(null, null, linkTypeID.ToInteger()), JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("\t\t}");
                            }

                            if (Table == "LinkTypes")
                            {
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t[HttpGet]");
                                yaz.WriteLine("\t\tpublic JsonResult FillTypes(int? typeID)");
                                yaz.WriteLine("\t\t{");
                                yaz.WriteLine("\t\t\treturn Json(LinkTypes.ReturnList(typeID), JsonRequestBehavior.AllowGet);");
                                yaz.WriteLine("\t\t}");
                            }
                        }

                        if (hasVisitors && Table == "Visitors")
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic JsonResult Clear()");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"d\"))");
                            yaz.WriteLine("\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tbool result = model.Clear();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (result)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tcurUser.Log<" + Table + ">(null, \"t\", \"" + Table.ToTurkish(lstLang) + "\");");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn Json(true, JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t}");
                        }

                        if (hasLogs && Table == "Logs")
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic JsonResult Clear()");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\tif (!curUser.HasRight(\"" + Table.ToUserRightType() + "\", \"d\"))");
                            yaz.WriteLine("\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tbool result = model.Clear();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (result)");
                            yaz.WriteLine("\t\t\t{");
                            yaz.WriteLine("\t\t\t\tcurUser.Log<" + Table + ">(null, \"t\", \"" + Table.ToTurkish(lstLang) + "\");");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\treturn Json(true, JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t}");
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }
        }

        void CreateAngularHomeController()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Controllers\\HomeController.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using System.Web.Mvc;");

                    if (hasLangs)
                    {
                        yaz.WriteLine("using Repository.TranslationModel;");
                    }

                    if (hasVisitors)
                    {
                        yaz.WriteLine("using Repository.VisitorsModel;");
                    }

                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName + ".Controllers");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic class HomeController : Controller");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpublic ActionResult Index()");
                    yaz.WriteLine("\t\t{");

                    if (hasLangs)
                    {
                        yaz.WriteLine("\t\t\tif (Session[\"CurrentLang\"] == null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tTranslation translation = new Translation();");
                        yaz.WriteLine("\t\t\t\tSession[\"CurrentLang\"] = translation.ByCode(\"TR\");");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                    }

                    if (hasVisitors)
                    {
                        yaz.WriteLine("\t\t\tVisitors visitor = new Visitors();");
                        yaz.WriteLine("\t\t\tvisitor.VisitorCount(AppTools.GetIPAddress);");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\t\t\treturn View();");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }
        }

        void CreateAngularSiteController()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Controllers\\SiteController.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using System.Web.Mvc;");

                    if (hasLangs)
                    {
                        yaz.WriteLine("using System.Linq;");
                        yaz.WriteLine("using System.Collections.Generic;");
                        yaz.WriteLine("using Models;");
                        yaz.WriteLine("using " + repositoryName + ".TranslationModel;");
                        yaz.WriteLine("using " + repositoryName + ".LangContentModel;");
                    }

                    if (hasNoLangs)
                    {
                        yaz.WriteLine("using " + repositoryName + ".NoLangContentModel;");
                    }

                    if (hasVisitors)
                    {
                        yaz.WriteLine("using " + repositoryName + ".VisitorsModel;");
                    }

                    if (hasLangs || hasNoLangs)
                    {
                        yaz.WriteLine("using TDLibrary;");
                    }

                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName + ".Controllers");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic class SiteController : Controller");
                    yaz.WriteLine("\t{");

                    if (hasLangs)
                    {
                        CreateAngularLangItemModel();

                        yaz.WriteLine("\t\t#region Translation");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult GetLangs()");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tTranslation translation = new Translation();");
                        yaz.WriteLine("\t\t\treturn Json(translation.List(), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult SelectedLang()");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn Json((Translation)Session[\"CurrentLang\"], JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult SelectLang(int param)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tTranslation translation = new Translation();");
                        yaz.WriteLine("\t\t\tSession[\"CurrentLang\"] = translation.Select(param);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn Json(true, JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#endregion");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t\t#region LangContent");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpPost]");
                        yaz.WriteLine("\t\tpublic JsonResult SetLangContents([System.Web.Http.FromBody] List<LangItem> codes)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tLangContent langContent = new LangContent();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn Json(langContent.FillLangs(codes, AppTools.GetLang.ID));");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult GetLangContentByCode(string param, string param2)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tLangContent langContent = new LangContent();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (param2.ToInteger() > 1 || param2.ToNull() == null)");
                        yaz.WriteLine("\t\t\t\treturn Json(langContent.ByCode(param, AppTools.GetLang.ID, param2.ToInteger()), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t\treturn Json(langContent.ByCode(param, AppTools.GetLang.ID, param2.ToInteger()).FirstOrDefault(), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult GetLangContentByShortCode(string param, string param2)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tLangContent langContent = new LangContent();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (param2.ToInteger() > 1 || param2.ToNull() == null)");
                        yaz.WriteLine("\t\t\t\treturn Json(langContent.ByShortCode(param, AppTools.GetLang.ID, param2.ToInteger()), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t\treturn Json(langContent.ByShortCode(param, AppTools.GetLang.ID, param2.ToInteger()).FirstOrDefault(), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult GetLangContentByCodeAndShortCode(string param, string param2, string param3)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tLangContent langContent = new LangContent();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (param3.ToInteger() > 1 || param3.ToNull() == null)");
                        yaz.WriteLine("\t\t\t\treturn Json(langContent.ByCodeAndShortCode(param, param2, AppTools.GetLang.ID, param3.ToInteger()), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t\treturn Json(langContent.ByCodeAndShortCode(param, param2, AppTools.GetLang.ID, param3.ToInteger()).FirstOrDefault(), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#endregion");
                        yaz.WriteLine("");
                    }

                    if (hasNoLangs) { 
                        yaz.WriteLine("\t\t#region NoLangContent");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpPost]");
                        yaz.WriteLine("\t\tpublic JsonResult SetNoLangContents([System.Web.Http.FromBody] List<LangItem> codes)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tNoLangContent noLangContent = new NoLangContent();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn Json(noLangContent.FillNoLangs(codes));");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult GetNoLangContentByCode(string param, string param2)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tNoLangContent noLangContent = new NoLangContent();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (param2.ToInteger() > 1 || param2.ToNull() == null)");
                        yaz.WriteLine("\t\t\t\treturn Json(noLangContent.ByCode(param, param2.ToInteger()), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t\treturn Json(noLangContent.ByCode(param, param2.ToInteger()).FirstOrDefault(), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult GetNoLangContentByShortCode(string param, string param2)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tNoLangContent noLangContent = new NoLangContent();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (param2.ToInteger() > 1 || param2.ToNull() == null)");
                        yaz.WriteLine("\t\t\t\treturn Json(noLangContent.ByShortCode(param, param2.ToInteger()), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t\treturn Json(noLangContent.ByShortCode(param, param2.ToInteger()).FirstOrDefault(), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult GetNoLangContentByCodeAndShortCode(string param, string param2, string param3)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tNoLangContent noLangContent = new NoLangContent();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (param3.ToInteger() > 1 || param3.ToNull() == null)");
                        yaz.WriteLine("\t\t\t\treturn Json(noLangContent.ByCodeAndShortCode(param, param2, param3.ToInteger()), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t\treturn Json(noLangContent.ByCodeAndShortCode(param, param2, param3.ToInteger()).FirstOrDefault(), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#endregion");
                        yaz.WriteLine("");
                    }

                    if (hasVisitors)
                    {
                        yaz.WriteLine("\t\t#region Visitors");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult VisitorCount()");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tVisitors visitors = new Visitors();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn Json(visitors.VisitorCount(AppTools.GetIPAddress), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t#endregion");
                    }

                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }
        }

        void CreateAngularSharedController()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers\\SharedController.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    if (hasUserRights)
                    {
                        yaz.WriteLine("using System;");
                        yaz.WriteLine("using System.Linq;");
                        yaz.WriteLine("using System.Web.Mvc;");
                        yaz.WriteLine("using System.Globalization;");
                        yaz.WriteLine("using " + repositoryName + ".Data;");
                        yaz.WriteLine("using " + repositoryName + ".UsersModel;");
                        yaz.WriteLine("using TDLibrary;");
                        yaz.WriteLine("");
                        yaz.WriteLine("namespace " + projectName + ".Areas.Ajax.Controllers");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class SharedController : Controller");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\treadonly " + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("\t\treadonly Users curUser = AppTools.User;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpPost]");
                        yaz.WriteLine("\t\tpublic JsonResult Login([System.Web.Http.FromBody] Users user)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tusp_UsersSelectLogin_Result rb = entity.usp_UsersSelectLogin(user.Username, user.Password.ToMD5()).FirstOrDefault();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (rb != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tuser = rb.ChangeModel<Users>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tSession[\"CurrentUser\"] = user;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tentity.usp_UsersLoginTimeUpdate(user.ID, DateTime.Now.ToString(CultureInfo.CurrentCulture));");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tAppTools.User.Log(\"grs\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\treturn Json(true);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn Json(false);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult Logout()");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tAppTools.User.Log(\"cks\");");
                        yaz.WriteLine("");
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
                        yaz.WriteLine("\t\t\t\ttry");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\tUsers user = Session[\"CurrentUser\"] as Users;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tusp_UsersSelectLogin_Result rb = entity.usp_UsersSelectLogin(user.Username, user.Password).FirstOrDefault();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tif (rb != null)");
                        yaz.WriteLine("\t\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\t\treturn Json(true, JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t\telse");
                        yaz.WriteLine("\t\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\tcatch");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\treturn Json(false, JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult CurrentUser()");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn Json((Users)Session[\"CurrentUser\"], JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult CurrentUserRights(string url, string process)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn Json(curUser.UserRights(url, process), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult ShowTypes()");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn Json(Lib.ShowTypes(), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult HasRight(string url, string process)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn Json(curUser.HasRight(url, process), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                    }
                    else
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
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult CurrentUser()");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn Json(\"Username\", JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                    }
                    yaz.Close();
                }
            }
        }

        void CreateAngularLangItemModel()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Models\\LangItem.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("namespace Models");
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic class LangItem");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpublic string Code { get; set; }");
                    yaz.WriteLine("\t\tpublic string ShortCode { get; set; }");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\models\\LangItem.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("export interface LangItem {");
                    yaz.WriteLine("\tCode: string;");
                    yaz.WriteLine("\tShortCode: string;");
                    yaz.WriteLine("}");
                    yaz.Close();
                }
            }
        }

        void CreateAngularLib()
        {
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Lib\\Lib.cs", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                {
                    yaz.WriteLine("using System;");
                    yaz.WriteLine("using System.Collections.Generic;");
                    yaz.WriteLine("using System.Linq;");
                    yaz.WriteLine("using System.Reflection;");
                    yaz.WriteLine("using System.Web;");
                    yaz.WriteLine("using System.Web.Configuration;");
                    yaz.WriteLine("using System.Web.Caching;");
                    yaz.WriteLine("using Cacher = System.Web.HttpRuntime;");
                    yaz.WriteLine("using " + repositoryName + ".Data;");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("using " + repositoryName + ".UsersModel;");
                    }

                    if (hasLangs)
                    {
                        yaz.WriteLine("using " + repositoryName + ".TranslationModel;");
                    }

                    yaz.WriteLine("using TDLibrary;");
                    yaz.WriteLine("");
                    yaz.WriteLine("namespace " + projectName);
                    yaz.WriteLine("{");
                    yaz.WriteLine("\tpublic class Lib");
                    yaz.WriteLine("\t{");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("\t\tpublic static List<usp_TypesShowSelect_Result> ShowTypes()");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tList<usp_TypesShowSelect_Result> result;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (Cacher.Cache[\"ShowTypes\"] == null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tList<usp_TypesShowSelect_Result> showTypes = entity.usp_TypesShowSelect(null).ToList();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tCacher.Cache.Insert(\"ShowTypes\", showTypes, null, DateTime.Now.AddMinutes(15), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tresult = Cacher.Cache[\"ShowTypes\"] as List<usp_TypesShowSelect_Result>;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn result;");
                        yaz.WriteLine("\t\t}");
                    }

                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic class AppTools");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpublic static string UploadPath");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tget");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\treturn WebConfigurationManager.AppSettings[\"UploadPath\"] != null ? WebConfigurationManager.AppSettings[\"UploadPath\"].ToString() : string.Empty;");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tpublic static Users User");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tget");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\treturn HttpContext.Current.Session[\"CurrentUser\"] != null ? HttpContext.Current.Session[\"CurrentUser\"] as Users : null;");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                    }

                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tpublic static string GetTime");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tget");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\treturn DateTime.Now.ToShortDateString() + \" \" + DateTime.Now.ToShortTimeString();");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");

                    if (hasLangs)
                    {
                        yaz.WriteLine("\t\tpublic static Translation GetLang");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tget");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\treturn HttpContext.Current.Session[\"CurrentLang\"] != null ? HttpContext.Current.Session[\"CurrentLang\"] as Translation : null;");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\t\tpublic static string GetIPAddress");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\tget");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tHttpContext context = HttpContext.Current;");
                    yaz.WriteLine("\t\t\t\tstring ipAddress = context.Request.ServerVariables[\"HTTP_X_FORWARDED_FOR\"];");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\tif (!string.IsNullOrEmpty(ipAddress))");
                    yaz.WriteLine("\t\t\t\t{");
                    yaz.WriteLine("\t\t\t\t\tstring[] addresses = ipAddress.Split(',');");
                    yaz.WriteLine("\t\t\t\t\tif (addresses.Length != 0)");
                    yaz.WriteLine("\t\t\t\t\t{");
                    yaz.WriteLine("\t\t\t\t\t\treturn addresses[0];");
                    yaz.WriteLine("\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t\treturn context.Request.ServerVariables[\"REMOTE_ADDR\"];");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static class UserProcesses");
                        yaz.WriteLine("\t{");
                        yaz.WriteLine("\t\tpublic static List<usp_UserGroupRightsByUserIDAndUrl_Result> UserRights(this Users user, string url = null, string process = null)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tList<usp_UserGroupRightsByUserIDAndUrl_Result> result;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (Cacher.Cache[\"CurrentUserRights_\" + user.ID.ToString()] == null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tList<usp_UserGroupRightsByUserIDAndUrl_Result> userRights = entity.usp_UserGroupRightsByUserIDAndUrl(user.ID, null, null).ToList();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tCacher.Cache.Insert(\"CurrentUserRights_\" + user.ID.ToString(), userRights, null, DateTime.Now.AddMinutes(15), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tresult = Cacher.Cache[\"CurrentUserRights_\" + user.ID.ToString()] as List<usp_UserGroupRightsByUserIDAndUrl_Result>;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (url.ToNull() != null)");
                        yaz.WriteLine("\t\t\t\tresult = result.Where(a => a.Url == url).ToList();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (process.ToNull() != null)");
                        yaz.WriteLine("\t\t\t\tresult = result.Where(a => a.ShortName == process).ToList();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn result;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tpublic static bool HasRight(this Users user, string url, string process = \"s\")");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tif (user == null)");
                        yaz.WriteLine("\t\t\t\treturn false;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tList<usp_UserGroupRightsByUserIDAndUrl_Result> list = user.UserRights(url, process);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tbool result = list.Count > 0 ? true : false;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn result;");
                        yaz.WriteLine("\t\t}");
                    }

                    if (hasUserRights && hasLogs)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tpublic static void Log<T>(this Users user, T model, string processShortName, string description = null, string idName = \"ID\")");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tif (user != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tif (model != null)");
                        yaz.WriteLine("\t\t\t\t{");
                        yaz.WriteLine("\t\t\t\t\tif (model.GetType() == typeof(int))");
                        yaz.WriteLine("\t\t\t\t\t\tdescription += CreateLogValues(model.ToString(), idName);");
                        yaz.WriteLine("\t\t\t\t\telse");
                        yaz.WriteLine("\t\t\t\t\t\tdescription += model.CreateLogValues(idName);");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tdescription = description == null ? null : description.SplitText(0, 255);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tentity.usp_LogsByProcessShortNameInsert(processShortName, user.ID, AppTools.GetTime, description);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tpublic static void Log(this Users user, string processShortName, string description = null)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tif (user != null)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\t" + cmbVeritabani.Text + "Entities entity = new " + cmbVeritabani.Text + "Entities();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tdescription = description == null ? null : description.SplitText(0, 255);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tentity.usp_LogsByProcessShortNameInsert(processShortName, user.ID, AppTools.GetTime, description);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tprivate static string CreateLogValues<T>(this T model, string idName)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tstring result = \" [\";");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tforeach (PropertyInfo item in model.GetType().GetProperties())");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\tif (idName == item.Name && item.GetValue(model).ToString() == \"0\")");
                        yaz.WriteLine("\t\t\t\t\tgoto devam;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tif (item.Name == \"Mesaj\")");
                        yaz.WriteLine("\t\t\t\t\tbreak;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tresult += item.Name + \": \" + item.GetValue(model) + \", \";");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tdevam:;");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn result.TrimEnd(' ').TrimEnd(',') + \"]\";");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tprivate static string CreateLogValues(string model, string idName)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn \" [\" + idName + \": \" + model.ToString() + \"]\";");
                        yaz.WriteLine("\t\t}");
                    }

                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic static class ExtMethods");
                    yaz.WriteLine("\t{");

                    if (hasUserRights)
                    {
                        yaz.WriteLine("\t\tpublic static bool? ShowType(this string url)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tList<usp_TypesShowSelect_Result> list = Lib.ShowTypes().Where(a => a.Url == url).ToList();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tbool result = list.Count > 0 ? list.FirstOrDefault().Show : false;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\treturn result;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\t\tpublic static string ToNull(this string value)");
                    yaz.WriteLine("\t\t{");
                    yaz.WriteLine("\t\t\treturn value == \"null\" ? null : value;");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                }
            }
        }

        void CreateAngularReadMe()
        {
            CopyFromResource(StringToByteArray(Properties.Resources.Angular_readme_txt, FileFormat.Unicode), PathAddress + "\\" + projectFolder + "\\readme.txt");
        }

        #endregion
    }
}
