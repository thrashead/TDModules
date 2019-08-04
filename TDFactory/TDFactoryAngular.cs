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
        #region Angular

        void CreateAngular()
        {
            CreateAngularDirectories();
            CreateAngularReadMe();
            CreateAngularFiles();

            if (chkMVCHepsi.Checked == true)
            {
                CreateRepository();
                CreateAngularModelLayer();
                CreateAngularViewLayer();
                CreateAngularControllerLayer();
                CreateAngularServiceLayer();
                CreateAngularSharedService();
                CreateAngularTypeScriptLayer();
                CreateWcfService();
                CreateWebConfig();
                CreateStylelScript();
                CreateDllFiles();

                CreateStoredProcedure();
            }
            else
            {
                if (chkRepository.Checked == true)
                {
                    CreateRepository();
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
                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\bin"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\bin");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Repository"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Repository");
                    }

                    foreach (string Table in selectedTables)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Repository\\" + Table))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Repository\\" + Table);
                        }
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

                    if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Data"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Data");
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
                        if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Repository"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Repository");
                        }

                        foreach (string Table in selectedTables)
                        {
                            if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\Repository\\" + Table))
                            {
                                Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\Repository\\" + Table);
                            }
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
                if (!Directory.Exists(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + _tableName.ToUrl(true)))
                {
                    Directory.CreateDirectory(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + _tableName.ToUrl(true));
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

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\shared\\layout.html", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<router-outlet></router-outlet>");
                    yaz.WriteLine("<" + projectName.Substring(0, 3).ToUrl(true) + "-scripts></" + projectName.Substring(0, 3).ToUrl(true) + "-scripts>");
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

            using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\shared\\controls\\scripts.ts", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("import { Component, ViewEncapsulation } from '@angular/core';");
                    yaz.WriteLine("import '../../../../Content/js/pathscript.js';");
                    yaz.WriteLine("import '../../../../Content/js/main.js';");
                    yaz.WriteLine("");
                    yaz.WriteLine("@Component({");
                    yaz.WriteLine("\tselector: '" + projectName.Substring(0, 3).ToUrl(true) + "-scripts',");
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
                    yaz.WriteLine("\ttemplateUrl: './header.html'");
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
                    yaz.WriteLine("\t\tthis.service.getLogout().subscribe((answer) => {");
                    yaz.WriteLine("\t\t\tif (answer == true) {");
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
                    yaz.WriteLine("@Injectable({ providedIn: 'root' })");
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
                        yaz.WriteLine("import { Admin" + Table + "IndexComponent } from './admin/views/" + Table.ToUrl(true) + "';");
                        yaz.WriteLine("import { Admin" + Table + "InsertComponent } from './admin/views/" + Table.ToUrl(true) + "/insert';");
                        yaz.WriteLine("import { Admin" + Table + "UpdateComponent } from './admin/views/" + Table.ToUrl(true) + "/update';");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("import { SharedService } from './admin/services/shared';");
                    yaz.WriteLine("");

                    int i = 1;

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("import { " + Table + "Service } from './admin/services/" + Table.ToUrl(true) + "';");

                        if (i == selectedTables.Count)
                            yaz.WriteLine("");

                        i++;
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
                        yaz.WriteLine("\t\tAdmin" + Table + "InsertComponent,");
                        yaz.WriteLine("\t\tAdmin" + Table + "UpdateComponent,");
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

                    string virgul = selectedTables.Count > 0 ? "," : "";

                    yaz.WriteLine("\t\tSharedService" + virgul);

                    i = 1;
                    foreach (string Table in selectedTables)
                    {
                        if (i == selectedTables.Count)
                            yaz.WriteLine("\t\t" + Table + "Service");
                        else
                            yaz.WriteLine("\t\t" + Table + "Service,");

                        i++;
                    }

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
                    yaz.WriteLine("import { LayoutComponent } from './shared/layout';");
                    yaz.WriteLine("import { HomeComponent } from './home/index';");
                    yaz.WriteLine("");
                    yaz.WriteLine("import { AdminLayoutComponent } from './admin/views/shared/layoutAdmin';");
                    yaz.WriteLine("import { AdminHomeComponent } from './admin/views/home/index';");
                    yaz.WriteLine("import { AdminLoginComponent } from './admin/views/home/login';");
                    yaz.WriteLine("");

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("import { Admin" + Table + "IndexComponent } from './admin/views/" + Table.ToUrl(true) + "';");
                        yaz.WriteLine("import { Admin" + Table + "InsertComponent } from './admin/views/" + Table.ToUrl(true) + "/insert';");
                        yaz.WriteLine("import { Admin" + Table + "UpdateComponent } from './admin/views/" + Table.ToUrl(true) + "/update';");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("const routes: Routes = [");
                    yaz.WriteLine("\t{ path: 'Admin', component: AdminLoginComponent, runGuardsAndResolvers: 'always' },");
                    yaz.WriteLine("\t{ path: 'Admin/Login', component: AdminLoginComponent, runGuardsAndResolvers: 'always' },");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t{");
                    yaz.WriteLine("\t\tpath: '',");
                    yaz.WriteLine("\t\tcomponent: LayoutComponent,");
                    yaz.WriteLine("\t\tchildren: [");
                    yaz.WriteLine("\t\t\t//{ path: '', redirectTo: 'Home', pathMatch: 'full' },");
                    yaz.WriteLine("\t\t\t{ path: '', component: HomeComponent, pathMatch: 'full' },");
                    yaz.WriteLine("\t\t], runGuardsAndResolvers: 'always'");
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
                        yaz.WriteLine("\t\t\t{ path: 'Admin/" + Table + "/Insert', component: Admin" + Table + "InsertComponent },");
                        yaz.WriteLine("\t\t\t{ path: 'Admin/" + Table + "/Update/:id', component: Admin" + Table + "UpdateComponent },");
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

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\models\\I" + Table + ".ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        List<ColumnInfo> columnNames = tableColumnInfos.Where(a => a.TableName == Table).ToList();

                        List<ColumnInfo> fileColumns = columnNames.Where(a => a.ColumnName.In(FileColumns, InType.ToUrlLower)).ToList();
                        List<ColumnInfo> imageColumns = columnNames.Where(a => a.ColumnName.In(ImageColumns, InType.ToUrlLower)).ToList();

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

                        foreach (ColumnInfo column in columnNames)
                        {
                            if (column.Type != null)
                            {
                                switch (column.Type.Name)
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

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            foreach (ColumnInfo item in fileColumns)
                            {
                                yaz.WriteLine("");
                                yaz.WriteLine("\tOld" + item.ColumnName + ": string,");
                                yaz.WriteLine("\t" + item.ColumnName + "HasFile : boolean,");
                            }

                            foreach (ColumnInfo item in imageColumns)
                            {
                                yaz.WriteLine("");
                                yaz.WriteLine("\tOld" + item.ColumnName + ": string,");
                                yaz.WriteLine("\t" + item.ColumnName + "HasFile : boolean,");
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

                List<ColumnInfo> columnNames = Helper.Helper.GetColumnsInfo(connectionInfo, Table).ToList();
                bool deleted = columnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? true : false;

                CreateAngularDirectories(Table);

                if (i <= 0)
                {
                    CreateAngularLayout();
                    CreateAngularHomePage();

                    i++;
                }

                //Index
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToUrl(true) + "\\index.html", FileMode.Create))
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

                        foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower)).Take(4).ToList())
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

                        foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower)).Take(4).ToList())
                        {
                            List<ForeignKeyChecker> frchkForeignLst2 = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

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

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td style=\"text-align:center;\">");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<div class=\"btn-group\" style=\"text-align:left;\">");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<button data-toggle=\"dropdown\" class=\"btn btn-mini btn-primary dropdown-toggle\">İşlem <span class=\"caret\"></span></button>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<ul class=\"dropdown-menu\">");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"updLink\" [routerLink]=\"['/Admin/" + Table + "/Update/' + item?." + id + "]\">Düzenle</a></li>");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"cpyLink\" href=\"#cpyData\" data-toggle=\"modal\" [attr.data-id]=\"item?." + id + "\">Kopyala</a></li>");

                            if (deleted)
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"rmvLink\" href=\"#rmvData\" data-toggle=\"modal\" [attr.data-id]=\"item?." + id + "\">Kaldır</a></li>");

                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"dltLink\" href=\"#dltData\" data-toggle=\"modal\" [attr.data-id]=\"item?." + id + "\">Sil</a></li>");
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
                        yaz.WriteLine("\t\t\t<a routerLink=\"/Admin/" + Table + "/Insert\" class=\"btn btn-primary btn-add\">" + Table + " Ekle</a>");
                        yaz.WriteLine("\t\t</div>");
                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("</div>");
                        yaz.WriteLine("");
                        yaz.WriteLine("<admin-copydelete></admin-copydelete>");
                        yaz.Close();
                    }
                }

                //Ekle
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToUrl(true) + "\\insert.html", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("<div id=\"content\">");
                        yaz.WriteLine("\t<div id=\"content-header\">");
                        yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a class=\"tip-bottom\"><i class=\"icon-home\"></i> " + Table + " Ekle</a></div>");
                        yaz.WriteLine("\t</div>");
                        yaz.WriteLine("\t<div class=\"container-fluid\">");
                        yaz.WriteLine("\t\t<form [formGroup]=\"insertForm\" (ngSubmit)=\"onSubmit()\">");
                        yaz.WriteLine("\t\t\t<fieldset>");

                        foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList())
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
                                    yaz.WriteLine("\t\t\t\t\t" + column.ColumnName);
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
                                            yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"number\" />");
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

                //Duzenle
                if (identityColumns.Count > 0)
                {
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToUrl(true) + "\\update.html", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                        {
                            yaz.WriteLine("<div id=\"content\">");
                            yaz.WriteLine("\t<div id=\"content-header\">");
                            yaz.WriteLine("\t\t<div id=\"breadcrumb\"> <a class=\"tip-bottom\"><i class=\"icon-home\"></i> " + Table + " Düzenle</a></div>");
                            yaz.WriteLine("\t</div>");
                            yaz.WriteLine("\t<div class=\"container-fluid\">");
                            yaz.WriteLine("\t\t<form [formGroup]=\"updateForm\" (ngSubmit)=\"onSubmit()\">");
                            yaz.WriteLine("\t\t\t<fieldset>");

                            foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList())
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
                                        yaz.WriteLine("\t\t\t\t\t" + column.ColumnName);
                                        yaz.WriteLine("\t\t\t\t</div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"clear\"></div>");
                                        yaz.WriteLine("\t\t\t\t<div class=\"editor-field\">");

                                        if (column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                        {
                                            yaz.WriteLine("\t\t\t\t<a href=\"/" + projectName + "/Uploads/{{ model?." + column.ColumnName + " }}\" target=\"_blank\"><img [src]=\"['/" + projectName + "/Uploads/thumb_' + model?." + column.ColumnName + "]\" style=\"height:40px; max-width:80px;\" /></a><br /><br />");
                                            yaz.WriteLine("\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"hidden\" value=\"{{ model?." + column.ColumnName + " }}\" />");
                                            yaz.WriteLine("\t\t\t\t<input id=\"" + column.ColumnName.ToUrl(true) + "Temp\" type=\"file\" name=\"" + column.ColumnName.ToUrl(true) + "Temp\" (change)=\"on" + column.ColumnName + "FileSelect($event)\" accept=\"image/*\" />");
                                        }
                                        else if (column.ColumnName.In(FileColumns, InType.ToUrlLower))
                                        {
                                            yaz.WriteLine("\t\t\t\t<a class=\"btn btn-mini btn-info\" href=\"/" + projectName + "/Uploads/{{ model?." + column.ColumnName + " }}\" target=\"_blank\">{{ model?." + column.ColumnName + " }}</a><br /><br />");
                                            yaz.WriteLine("\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"hidden\" value=\"{{ model?." + column.ColumnName + " }}\" />");
                                            yaz.WriteLine("\t\t\t\t<input id=\"" + column.ColumnName.ToUrl(true) + "Temp\" type=\"file\" name=\"" + column.ColumnName.ToUrl(true) + "Temp\" (change)=\"on" + column.ColumnName + "FileSelect($event)\" />");
                                        }
                                        else
                                        {
                                            if (column.Type.Name == "Boolean")
                                            {
                                                yaz.WriteLine("\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"checkbox\" checked=\"checked\" *ngIf=\"model?." + column.ColumnName + "\" />");
                                                yaz.WriteLine("\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"checkbox\" *ngIf=\"!model?." + column.ColumnName + "\" />");
                                            }
                                            else if (column.Type.Name == "Int16" ||
                                                     column.Type.Name == "Int32" ||
                                                     column.Type.Name == "Int64")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"number\" value=\"{{ model?." + column.ColumnName + " }}\" />");
                                            }
                                            else if (column.Type.Name == "String" && column.CharLength == -1)
                                            {
                                                yaz.WriteLine("\t\t\t\t\t<textarea id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\">{{ model?." + column.ColumnName + " }}</textarea>");
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t\t\t<input id=\"" + column.ColumnName + "\" [ngModel]=\"model?." + column.ColumnName + "\" formControlName=\"" + column.ColumnName + "\" type=\"text\" value=\"{{ model?." + column.ColumnName + " }}\" />");
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
                            yaz.WriteLine("\t\t\t\t\t<input type=\"submit\" value=\"Kaydet\" class=\"btn btn-success btn-save\" [disabled]=\"!updateForm.valid\" />");
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
                                    yaz.WriteLine("\t\t\t\t\t\t\t\t<tr *ngFor=\"let item of model?." + ForeignTableName + "List\">");

                                    i = 0;

                                    foreach (ColumnInfo item in foreignColumns)
                                    {
                                        List<ForeignKeyChecker> frchkForeignLst = fkcListForeign2.Where(a => a.ForeignColumnName == item.ColumnName).ToList();

                                        string hideColumn = i == 3 ? " class=\"hideColumn\"" : "";

                                        if (frchkForeignLst.Count <= 0)
                                        {
                                            if (item.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + "><a href=\"/" + projectName + "/Uploads/{{ item?." + item.ColumnName + " }}\" target=\"_blank\"><img src=\"/" + projectName + "/Uploads/thumb_{{ item?." + item.ColumnName + " }}\" style=\"height:40px; max-width:80px;\" /></a></td>");
                                            else if (item.ColumnName.In(FileColumns, InType.ToUrlLower))
                                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + "><a class=\"btn btn-mini btn-info\" href=\"/" + projectName + "/Uploads/{{ item?." + item.ColumnName + " }}\" target=\"_blank\">{{ item?." + item.ColumnName + " }}</a></td>");
                                            else
                                                yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td" + hideColumn + ">{{ item?." + item.ColumnName + " }}</td>");

                                            i++;
                                        }
                                    }

                                    if (identityForeignColumns.Count > 0)
                                    {
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t<td style=\"text-align:center;\">");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t<div class=\"btn-group\" style=\"text-align:left;\">");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<button data-toggle=\"dropdown\" class=\"btn btn-mini btn-primary dropdown-toggle\">İşlem <span class=\"caret\"></span></button>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t<ul class=\"dropdown-menu\">");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"updLink\" [routerLink]=\"['/Admin/" + ForeignTableName + "/Update/' + item?." + idFrgn + "]\">Düzenle</a></li>");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"cpyLink\" href=\"#cpyData\" data-toggle=\"modal\" data-controller=\"" + ForeignTableName + "\" [attr.data-id]=\"item?." + idFrgn + "\">Kopyala</a></li>");

                                        if (fDeleted)
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"rmvLink\" href=\"#rmvData\" data-toggle=\"modal\" data-controller=\"" + ForeignTableName + "\" [attr.data-id]=\"item?." + idFrgn + "\">Kaldır</a></li>");

                                        yaz.WriteLine("\t\t\t\t\t\t\t\t\t\t\t\t<li><a class=\"dltLink\" href=\"#dltData\" data-toggle=\"modal\" data-controller=\"" + ForeignTableName + "\" [attr.data-id]=\"item?." + idFrgn + "\">Sil</a></li>");
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
                                    yaz.WriteLine("\t\t\t<a routerLink=\"/Admin/" + ForeignTableName + "/Insert\" class=\"btn btn-primary btn-add\">" + ForeignTableName + " Ekle</a>");
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
            CreateAngularHomeController();

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

                List<ColumnInfo> columnNames = Helper.Helper.GetColumnsInfo(connectionInfo, Table).ToList();
                List<ColumnInfo> urlColumns = columnNames.Where(a => a.ColumnName.In(UrlColumns, InType.ToUrlLower)).ToList();
                List<ColumnInfo> fileColumns = columnNames.Where(a => a.ColumnName.In(FileColumns, InType.ToUrlLower)).ToList();
                List<ColumnInfo> imageColumns = columnNames.Where(a => a.ColumnName.In(ImageColumns, InType.ToUrlLower)).ToList();
                bool deleted = columnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? true : false;

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\Areas\\Ajax\\Controllers\\" + Table + "Controller.cs", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.Unicode))
                    {
                        yaz.WriteLine("using System.Web.Mvc;");

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            yaz.WriteLine("using System.Collections.Generic;");
                            yaz.WriteLine("using TDLibrary;");
                        }

                        yaz.WriteLine("using Repository." + Table + "Model;");
                        yaz.WriteLine("");

                        yaz.WriteLine("namespace " + projectName + ".Areas.Ajax.Controllers");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tpublic class " + Table + "Controller : Controller");
                        yaz.WriteLine("\t{");

                        yaz.WriteLine("\t\t" + Table + " model = new " + Table + "();");

                        yaz.WriteLine("");

                        // Index
                        string searchText = GetColumnText(tableColumnInfos.Where(a => a.TableName == Table).ToList());

                        yaz.WriteLine("\t\t[HttpGet]");
                        yaz.WriteLine("\t\tpublic JsonResult Index(int? id)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\treturn Json(model.List(id), JsonRequestBehavior.AllowGet);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        if (fkcListForeign.Count > 0)
                        {
                            // Ekle
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic JsonResult Insert()");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\treturn Json(model.Insert(), JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                        }

                        // Ekle
                        yaz.WriteLine("\t\t[HttpPost]");
                        yaz.WriteLine("\t\tpublic JsonResult Insert([System.Web.Http.FromBody] " + Table + " table)");
                        yaz.WriteLine("\t\t{");
                        yaz.WriteLine("\t\t\tbool result = model.Insert(table);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (result)");
                        yaz.WriteLine("\t\t\t\treturn Json(table);");
                        yaz.WriteLine("\t\t\telse");
                        yaz.WriteLine("\t\t\t\ttable.Mesaj = \"Kayıt eklenemedi.\";");
                        yaz.WriteLine("");

                        string linkID = ", null";

                        if (fkcListForeign.Count > 0)
                        {
                            linkID = "";

                            foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                            {
                                linkID += ", table." + fkc.ForeignColumnName;
                            }
                        }

                        yaz.WriteLine("\t\t\ttable = (" + Table + ")model.Insert(table" + linkID + ");");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t\t\treturn Json(table);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            // EkleYukle
                            yaz.WriteLine("\t\t[HttpPost]");
                            yaz.WriteLine("\t\tpublic JsonResult InsertUpload([System.Web.Http.FromBody] " + Table + " table)");
                            yaz.WriteLine("\t\t{");

                            if (fileColumns.Count > 0)
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

                            if (imageColumns.Count > 0)
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

                        if (identityColumns.Count > 0)
                        {
                            string columntype = tableColumnInfos.Where(a => a.ColumnName == id && a.TableName == Table).FirstOrDefault().Type.Name.ToString();

                            //Duzenle
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic JsonResult Update(" + columntype.ReturnCSharpType() + "? id)");
                            yaz.WriteLine("\t\t{");
                            yaz.WriteLine("\t\t\treturn Json(model.Update(id), JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t[HttpPost]");
                            yaz.WriteLine("\t\tpublic JsonResult Update([System.Web.Http.FromBody] " + Table + " table)");
                            yaz.WriteLine("\t\t{");

                            foreach (ColumnInfo item in fileColumns)
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

                            foreach (ColumnInfo item in imageColumns)
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

                            yaz.WriteLine("\t\t\tbool result = model.Update(table);");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t\tif (result)");
                            yaz.WriteLine("\t\t\t\treturn Json(table);");
                            yaz.WriteLine("\t\t\telse");
                            yaz.WriteLine("\t\t\t\ttable.Mesaj = \"Kayıt düzenlenemedi.\";");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t\ttable = (" + Table + ")model.Update(table." + id + ", table);");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\t\treturn Json(table);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            if (fileColumns.Count > 0 || imageColumns.Count > 0)
                            {
                                //DuzenleYukle
                                yaz.WriteLine("\t\t[HttpPost]");
                                yaz.WriteLine("\t\tpublic JsonResult UpdateUpload([System.Web.Http.FromBody] " + Table + " table)");
                                yaz.WriteLine("\t\t{");

                                if (fileColumns.Count > 0)
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

                                if (imageColumns.Count > 0)
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

                            //Kopyala
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic JsonResult Copy(" + columntype.ReturnCSharpType() + " id)");
                            yaz.WriteLine("\t\t{");
                            if (fileColumns.Count > 0 || imageColumns.Count > 0)
                            {
                                yaz.WriteLine("\t\t\ttry");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\t" + Table + " table = (" + Table + ")model.Select(id, false);");
                                yaz.WriteLine("");

                                foreach (ColumnInfo item in fileColumns)
                                {
                                    yaz.WriteLine("\t\t\t\tSystem.IO.File.Copy(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table." + item.ColumnName + "), Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/Kopya_\" + table." + item.ColumnName + "));");
                                }

                                foreach (ColumnInfo item in imageColumns)
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
                            yaz.WriteLine("\t\t\treturn Json(model.Copy(id), JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            //Sil
                            yaz.WriteLine("\t\t[HttpGet]");
                            yaz.WriteLine("\t\tpublic JsonResult Delete(" + columntype.ReturnCSharpType() + "? id)");
                            yaz.WriteLine("\t\t{");
                            if (fileColumns.Count > 0 || imageColumns.Count > 0)
                            {
                                yaz.WriteLine("\t\t\ttry");
                                yaz.WriteLine("\t\t\t{");
                                yaz.WriteLine("\t\t\t\t" + Table + " table = (" + Table + ")model.Select(id, false);");
                                yaz.WriteLine("");

                                foreach (ColumnInfo item in fileColumns)
                                {
                                    yaz.WriteLine("\t\t\t\tSystem.IO.File.Delete(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table." + item.ColumnName + "));");
                                }

                                foreach (ColumnInfo item in imageColumns)
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
                            yaz.WriteLine("\t\t\treturn Json(model.Delete(id), JsonRequestBehavior.AllowGet);");
                            yaz.WriteLine("\t\t}");

                            if (deleted)
                            {
                                //Kaldır
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t[HttpGet]");
                                yaz.WriteLine("\t\tpublic JsonResult Remove(" + columntype.ReturnCSharpType() + "? id)");
                                yaz.WriteLine("\t\t{");
                                if (fileColumns.Count > 0 || imageColumns.Count > 0)
                                {
                                    yaz.WriteLine("\t\t\ttry");
                                    yaz.WriteLine("\t\t\t{");
                                    yaz.WriteLine("\t\t\t\t" + Table + " table = (" + Table + ")model.Select(id, false);");
                                    yaz.WriteLine("");

                                    foreach (ColumnInfo item in fileColumns)
                                    {
                                        yaz.WriteLine("\t\t\t\tSystem.IO.File.Move(Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/\" + table." + item.ColumnName + "), Server.MapPath(\"~/\" + AppMgr.UploadPath.Replace(AppMgr.MainPath, \"\") + \"/Deleted/\" + table." + item.ColumnName + "));");
                                    }

                                    foreach (ColumnInfo item in imageColumns)
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
                                yaz.WriteLine("\t\t\treturn Json(model.Remove(id), JsonRequestBehavior.AllowGet);");
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

                List<ColumnInfo> columnNames = Helper.Helper.GetColumnsInfo(connectionInfo, Table).ToList();
                List<ColumnInfo> fileColumns = columnNames.Where(a => a.ColumnName.In(FileColumns, InType.ToUrlLower)).ToList();
                List<ColumnInfo> imageColumns = columnNames.Where(a => a.ColumnName.In(ImageColumns, InType.ToUrlLower)).ToList();
                bool deleted = columnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? true : false;

                if (i <= 0)
                {
                    CreateAngularHomeController();

                    i++;
                }

                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\services\\" + Table.ToUrl(true) + ".ts", FileMode.Create))
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
                        yaz.WriteLine("\tprivate linkInsert: string = \"Ajax/" + Table + "/Insert\";");
                        yaz.WriteLine("\tprivate linkUpdate: string = \"Ajax/" + Table + "/Update\";");

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            yaz.WriteLine("\tprivate linkInsertUpload: string = \"Ajax/" + Table + "/InsertUpload\";");
                            yaz.WriteLine("\tprivate linkUpdateUpload: string = \"Ajax/" + Table + "/UpdateUpload\";");
                        }

                        yaz.WriteLine("\tprivate linkCopy: string = \"Ajax/" + Table + "/Copy\";");
                        yaz.WriteLine("\tprivate linkDelete: string = \"Ajax/" + Table + "/Delete\";");

                        if (deleted)
                            yaz.WriteLine("\tprivate linkRemove: string = \"Ajax/" + Table + "/Remove\";");

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
                            yaz.WriteLine("\tgetInsert(): Observable<I" + Table + "> {");
                            yaz.WriteLine("\t\treturn this.http.get<I" + Table + ">(this.linkInsert);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\tpostInsert(model: any): Observable<I" + Table + "> {");
                        yaz.WriteLine("\t\treturn this.http.post<I" + Table + ">(this.linkInsert, model);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            yaz.WriteLine("\tpostInsertUpload(model: any): Observable<I" + Table + "> {");
                            yaz.WriteLine("\t\treturn this.http.post<I" + Table + ">(this.linkInsertUpload, model);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\tgetUpdate(id: string): Observable<I" + Table + "> {");
                        yaz.WriteLine("\t\tlet params = new HttpParams().set(\"id\", id);");
                        yaz.WriteLine("\t\treturn this.http.get<I" + Table + ">(this.linkUpdate, { params: params });");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpostUpdate(model: any): Observable<I" + Table + "> {");
                        yaz.WriteLine("\t\treturn this.http.post<I" + Table + ">(this.linkUpdate, model);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            yaz.WriteLine("\tpostUpdateUpload(model: any): Observable<I" + Table + "> {");
                            yaz.WriteLine("\t\treturn this.http.post<I" + Table + ">(this.linkUpdateUpload, model);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\tgetCopy(id: string): Observable<boolean> {");
                        yaz.WriteLine("\t\tlet params = new HttpParams().set(\"id\", id);");
                        yaz.WriteLine("\t\treturn this.http.get<boolean>(this.linkCopy, { params: params });");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tgetDelete(id: string): Observable<boolean> {");
                        yaz.WriteLine("\t\tlet params = new HttpParams().set(\"id\", id);");
                        yaz.WriteLine("\t\treturn this.http.get<boolean>(this.linkDelete, { params: params });");
                        yaz.WriteLine("\t}");

                        if (deleted)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\tgetRemove(id: string): Observable<boolean> {");
                            yaz.WriteLine("\t\tlet params = new HttpParams().set(\"id\", id);");
                            yaz.WriteLine("\t\treturn this.http.get<boolean>(this.linkRemove, { params: params });");
                            yaz.WriteLine("\t}");
                        }

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

                List<ColumnInfo> columnNames = Helper.Helper.GetColumnsInfo(connectionInfo, Table).ToList();
                List<ColumnInfo> fileColumns = columnNames.Where(a => a.ColumnName.In(FileColumns, InType.ToUrlLower)).ToList();
                List<ColumnInfo> imageColumns = columnNames.Where(a => a.ColumnName.In(ImageColumns, InType.ToUrlLower)).ToList();
                bool deleted = columnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? true : false;

                CreateAngularDirectories(Table);

                //Index
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToUrl(true) + "\\index.ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("import { Component, OnDestroy, OnInit } from \"@angular/core\";");
                        yaz.WriteLine("import { Subscription } from \"rxjs\";");
                        yaz.WriteLine("import { Router } from \"@angular/router\";");
                        yaz.WriteLine("import { " + Table + "Service } from \"../../services/" + Table.ToUrl(true) + "\";");
                        yaz.WriteLine("import * as $ from \"jquery\";");
                        yaz.WriteLine("");
                        yaz.WriteLine("@Component({");
                        yaz.WriteLine("\ttemplateUrl: './index.html'");
                        yaz.WriteLine("})");
                        yaz.WriteLine("");
                        yaz.WriteLine("export class Admin" + Table + "IndexComponent implements OnInit, OnDestroy {");
                        yaz.WriteLine("\terrorMsg: string;");
                        yaz.WriteLine("\t" + Table + "List: any;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tcallTable: boolean;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate subscription: Subscription = new Subscription();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tconstructor(private service: " + Table + "Service, private router: Router) {");
                        yaz.WriteLine("\t}");

                        yaz.WriteLine("");
                        yaz.WriteLine("\tngOnInit() {");
                        yaz.WriteLine("\t\tthis.callTable = true;");
                        yaz.WriteLine("\t\tthis.FillData();");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tFillData() {");
                        yaz.WriteLine("\t\tif (this.callTable == true) {");
                        yaz.WriteLine("\t\t\tthis.subscription = this.service.getIndex().subscribe((answer) => {");
                        yaz.WriteLine("\t\t\t\tthis." + Table + "List = answer;");
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
                        yaz.WriteLine("\t\t\t\t\t$(document)");
                        yaz.WriteLine("\t\t\t\t\t\t.off(\"click\", \".fg-button\")");
                        yaz.WriteLine("\t\t\t\t\t\t.on(\"click\", \".fg-button\", () => {");
                        yaz.WriteLine("\t\t\t\t\t\t\tsetTimeout(() => {");
                        yaz.WriteLine("\t\t\t\t\t\t\t\tthis.FillData();");
                        yaz.WriteLine("\t\t\t\t\t\t\t}, 1);");
                        yaz.WriteLine("\t\t\t\t\t\t});");

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t$(document)");
                        yaz.WriteLine("\t\t\t\t\t\t.off(\"click\", \"a.cpyLink\")");
                        yaz.WriteLine("\t\t\t\t\t\t.on(\"click\", \"a.cpyLink\", function () {");
                        yaz.WriteLine("\t\t\t\t\t\t\t$(this).addClass(\"active-cpy\");");
                        yaz.WriteLine("\t\t\t\t\t\t\t$(\"a.cpy-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                        yaz.WriteLine("\t\t\t\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t$(document)");
                        yaz.WriteLine("\t\t\t\t\t\t.off(\"click\", \"a.cpy-yes\")");
                        yaz.WriteLine("\t\t\t\t\t\t.on(\"click\", \"a.cpy-yes\", () => {");
                        yaz.WriteLine("\t\t\t\t\t\t\tlet id: string = $(\"a.cpy-yes\").attr(\"data-id\");");
                        yaz.WriteLine("\t\t\t\t\t\t\tthis.onCopy(id);");
                        yaz.WriteLine("\t\t\t\t\t\t});");

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t$(document)");
                        yaz.WriteLine("\t\t\t\t\t\t.off(\"click\", \"a.dltLink\")");
                        yaz.WriteLine("\t\t\t\t\t\t.on(\"click\", \"a.dltLink\", function () {");
                        yaz.WriteLine("\t\t\t\t\t\t\t$(this).addClass(\"active-dlt\");");
                        yaz.WriteLine("\t\t\t\t\t\t\t$(\"a.dlt-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                        yaz.WriteLine("\t\t\t\t\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t$(document)");
                        yaz.WriteLine("\t\t\t\t\t\t.off(\"click\", \"a.dlt-yes\")");
                        yaz.WriteLine("\t\t\t\t\t\t.on(\"click\", \"a.dlt-yes\", () => {");
                        yaz.WriteLine("\t\t\t\t\t\t\tlet id: string = $(\"a.dlt-yes\").attr(\"data-id\");");
                        yaz.WriteLine("\t\t\t\t\t\t\tthis.onDelete(id);");
                        yaz.WriteLine("\t\t\t\t\t\t});");

                        if (deleted)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\t$(document)");
                            yaz.WriteLine("\t\t\t\t\t\t.off(\"click\", \"a.rmvLink\")");
                            yaz.WriteLine("\t\t\t\t\t\t.on(\"click\", \"a.rmvLink\", function () {");
                            yaz.WriteLine("\t\t\t\t\t\t\t$(this).addClass(\"active-rmv\");");
                            yaz.WriteLine("\t\t\t\t\t\t\t$(\"a.rmv-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                            yaz.WriteLine("\t\t\t\t\t\t});");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\t$(document)");
                            yaz.WriteLine("\t\t\t\t\t\t.off(\"click\", \"a.rmv-yes\")");
                            yaz.WriteLine("\t\t\t\t\t\t.on(\"click\", \"a.rmv-yes\", () => {");
                            yaz.WriteLine("\t\t\t\t\t\t\tlet id: string = $(\"a.rmv-yes\").attr(\"data-id\");");
                            yaz.WriteLine("\t\t\t\t\t\t\tthis.onRemove(id);");
                            yaz.WriteLine("\t\t\t\t\t\t});");
                        }

                        yaz.WriteLine("\t\t\t\t}, 1);");
                        yaz.WriteLine("\t\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tsetTimeout(() => {");
                        yaz.WriteLine("\t\t\tif ($(\".dropdown-menu\").first().find(\"a\").length <= 0) {");
                        yaz.WriteLine("\t\t\t\t$(\".btn-group\").remove();");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}, 1);");
                        yaz.WriteLine("\t}");

                        yaz.WriteLine("");
                        yaz.WriteLine("\tngOnDestroy(): void {");
                        yaz.WriteLine("\t\tthis.subscription.unsubscribe();");
                        yaz.WriteLine("\t}");

                        yaz.WriteLine("");
                        yaz.WriteLine("\tonCopy(id) {");
                        yaz.WriteLine("\t\tthis.subscription = this.service.getCopy(id).subscribe((answer) => {");
                        yaz.WriteLine("\t\t\t$(\"a.cpyLink.active-cpy\").removeClass(\"active-cpy\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (answer == true) {");
                        yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"Copy\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tlet currentUrl = this.router.url;");
                        yaz.WriteLine("\t\t\t\tthis.router.navigate(['/'], { skipLocationChange: true }).then(() => { this.router.navigate([currentUrl]) });");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t\telse {");
                        yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"CopyNot\");");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
                        yaz.WriteLine("\t}");

                        yaz.WriteLine("");
                        yaz.WriteLine("\tonDelete(id) {");
                        yaz.WriteLine("\t\tthis.subscription = this.service.getDelete(id).subscribe((answer) => {");
                        yaz.WriteLine("\t\t\tif (answer == true) {");
                        yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"Delete\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t$(\"a.dltLink.active-dlt\").parent(\"li\").parent(\"ul\").parent(\"div\").parent(\"td\").parent(\"tr\").fadeOut(\"slow\", function () {");
                        yaz.WriteLine("\t\t\t\t\t$(this).remove();");
                        yaz.WriteLine("\t\t\t\t});");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t\telse {");
                        yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"DeleteNot\");");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
                        yaz.WriteLine("\t}");

                        if (deleted)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\tonRemove(id) {");
                            yaz.WriteLine("\t\tthis.subscription = this.service.getRemove(id).subscribe((answer) => {");
                            yaz.WriteLine("\t\t\tif (answer == true) {");
                            yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"Remove\");");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t$(\"a.rmvLink.active-rmv\").parent(\"li\").parent(\"ul\").parent(\"div\").parent(\"td\").parent(\"tr\").fadeOut(\"slow\", function () {");
                            yaz.WriteLine("\t\t\t\t\t$(this).remove();");
                            yaz.WriteLine("\t\t\t\t});");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t\telse {");
                            yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"RemoveNot\");");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
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

                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }

                //Ekle
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToUrl(true) + "\\insert.ts", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("import { Component } from \"@angular/core\";");
                        yaz.WriteLine("import { Subscription } from \"rxjs\";");
                        yaz.WriteLine("import { " + Table + "Service } from \"../../services/" + Table.ToUrl(true) + "\";");
                        yaz.WriteLine("import { Router } from \"@angular/router\";");

                        yaz.WriteLine("import { FormBuilder, FormGroup, Validators, FormControl } from \"@angular/forms\";");

                        foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && a.Type.Name == "String" && a.CharLength == -1).ToList())
                        {
                            yaz.WriteLine("import ClassicEditor from '../../../../../Content/admin/js/ckeditor/ckeditor.js';");
                            break;
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("@Component({");
                        yaz.WriteLine("\ttemplateUrl: './insert.html'");
                        yaz.WriteLine("})");
                        yaz.WriteLine("");
                        yaz.WriteLine("export class Admin" + Table + "InsertComponent {");
                        yaz.WriteLine("\terrorMsg: string;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tinsertForm: FormGroup;");
                        yaz.WriteLine("\tdata: any;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tmodel: any;");
                        yaz.WriteLine("");

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            yaz.WriteLine("\tuploadData: any;");

                            foreach (ColumnInfo item in fileColumns)
                            {
                                yaz.WriteLine("\tfile" + item.ColumnName + " : any;");
                            }

                            foreach (ColumnInfo item in imageColumns)
                            {
                                yaz.WriteLine("\timage" + item.ColumnName + ": any;");
                            }

                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\tprivate subscription: Subscription = new Subscription();");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tconstructor(private service: " + Table + "Service, private formBuilder: FormBuilder, private router: Router) {");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tngOnInit() {");
                        yaz.WriteLine("\t\tthis.data = new Object();");
                        yaz.WriteLine("");

                        if (fkcListForeign.Count > 0)
                        {
                            yaz.WriteLine("\t\tthis.subscription = this.service.getInsert().subscribe((answer) => {");
                            yaz.WriteLine("\t\t\tthis.model = answer;");
                            yaz.WriteLine("\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
                            yaz.WriteLine("");
                        }

                        int i = 0;

                        List<ColumnInfo> tempTableColumns = tableColumnInfos.Where(a => a.TableName == Table && a.Type.Name == "String" && a.CharLength == -1 && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(FileColumns, InType.ToUrlLower) && !a.ColumnName.In(ImageColumns, InType.ToUrlLower)).ToList();

                        foreach (ColumnInfo column in tempTableColumns)
                        {
                            if (i == 0)
                            {
                                yaz.WriteLine("\t\tsetTimeout(function () {");
                            }

                            yaz.WriteLine("\t\t\tClassicEditor");
                            yaz.WriteLine("\t\t\t\t.create(document.querySelector('#" + column.ColumnName + "'), {");
                            yaz.WriteLine("\t\t\t\t})");
                            yaz.WriteLine("\t\t\t\t.then(editor => {");
                            yaz.WriteLine("\t\t\t\t\tconsole.log(editor);");
                            yaz.WriteLine("\t\t\t\t});");

                            if (tempTableColumns.Count <= i)
                            {
                                yaz.WriteLine("");
                            }

                            if (tempTableColumns.Count == i + 1)
                            {
                                yaz.WriteLine("\t\t}, 1000);");
                                yaz.WriteLine("");
                            }

                            i++;
                        }

                        yaz.WriteLine("\t\tthis.insertForm = this.formBuilder.group({");

                        foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList())
                        {
                            if (!column.IsIdentity)
                            {
                                if (column.Type.Name == "Boolean")
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
                                        if (column.Type.Name.In(new string[] { "Int16", "Int32", "Int64" }))
                                        {
                                            yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.min(0)]),");
                                        }
                                        else if (column.Type.Name == "String")
                                        {
                                            if (column.Type.Name == "String" && column.CharLength == -1)
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

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            foreach (ColumnInfo item in fileColumns)
                            {
                                yaz.WriteLine("\ton" + item.ColumnName + "FileSelect(event) {");
                                yaz.WriteLine("\t\tif (event.target.files.length > 0) {");
                                yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + " = event.target.files[0].name;");
                                yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + "HasFile = true;");
                                yaz.WriteLine("\t\t\tthis.file" + item.ColumnName + " = event.target.files[0];");
                                yaz.WriteLine("\t\t}");
                                yaz.WriteLine("\t}");
                                yaz.WriteLine("");
                            }

                            foreach (ColumnInfo item in imageColumns)
                            {
                                yaz.WriteLine("\ton" + item.ColumnName + "FileSelect(event) {");
                                yaz.WriteLine("\t\tif (event.target.files.length > 0) {");
                                yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + " = event.target.files[0].name;");
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

                        yaz.WriteLine("\tonSubmit() {");

                        string tttab = fileColumns.Count > 0 || imageColumns.Count > 0 ? "\t\t" : "";

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\tthis.uploadData = new FormData();");

                            foreach (ColumnInfo item in fileColumns)
                            {
                                yaz.WriteLine("\t\tthis.uploadData.append(\"file\", this.file" + item.ColumnName + ");");
                            }

                            foreach (ColumnInfo item in imageColumns)
                            {
                                yaz.WriteLine("\t\tthis.uploadData.append(\"file\", this.image" + item.ColumnName + ");");
                            }

                            yaz.WriteLine("");

                            yaz.WriteLine("\t\tthis.subscription = this.service.postInsertUpload(this.uploadData).subscribe((answerUpload) => {");
                            yaz.WriteLine("\t\t\tif (answerUpload.Mesaj == null)");
                            yaz.WriteLine("\t\t\t{");
                        }

                        int tcCount = tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList().Count;

                        i = 0;

                        foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList())
                        {
                            if (!column.IsIdentity)
                            {
                                if (column.Type.Name == "String" && column.CharLength == -1 && !column.ColumnName.In(FileColumns, InType.ToUrlLower) && !column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                {
                                    yaz.WriteLine(tttab + "\t\tthis.data." + column.ColumnName + " = $(\".ck-content\").html().replace(\"<p>\", \"\").replace(\"</p>\", \"\");");
                                }
                                else if (!column.ColumnName.In(FileColumns, InType.ToUrlLower) && !column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                {
                                    yaz.WriteLine(tttab + "\t\tthis.data." + column.ColumnName + " = this.insertForm.get(\"" + column.ColumnName + "\").value;");
                                }
                            }

                            i++;

                            if (i == tcCount)
                            {
                                yaz.WriteLine("");
                            }
                        }

                        yaz.WriteLine(tttab + "\t\tthis.service.postInsert(this.data)");
                        yaz.WriteLine(tttab + "\t\t\t.subscribe((answer) => {");
                        yaz.WriteLine(tttab + "\t\t\t\tif (answer.Mesaj == null) {");
                        yaz.WriteLine(tttab + "\t\t\t\t\tthis.router.navigate(['/Admin/" + Table + "']);");
                        yaz.WriteLine(tttab + "\t\t\t\t}");
                        yaz.WriteLine(tttab + "\t\t\t\telse {");
                        yaz.WriteLine(tttab + "\t\t\t\t\t$(\".alertMessage\").text(answer.Mesaj);");
                        yaz.WriteLine(tttab + "\t\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                        yaz.WriteLine(tttab + "\t\t\t\t}");
                        yaz.WriteLine(tttab + "\t\t\t},");
                        yaz.WriteLine(tttab + "\t\t\t\tresError => this.errorMsg = resError);");

                        if (fileColumns.Count > 0 || imageColumns.Count > 0)
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

                //Duzenle
                if (identityColumns.Count > 0)
                {
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectFolder + "\\src\\app\\admin\\views\\" + Table.ToUrl(true) + "\\update.ts", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                        {
                            yaz.WriteLine("import { Component } from \"@angular/core\";");
                            yaz.WriteLine("import { Subscription } from \"rxjs\";");
                            yaz.WriteLine("import { " + Table + "Service } from \"../../services/" + Table.ToUrl(true) + "\";");

                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string PrimaryTableName = fkc.ForeignTableName;

                                    yaz.WriteLine("import { " + PrimaryTableName + "Service } from '../../services/" + PrimaryTableName.ToUrl(true) + "';");
                                }
                            }

                            yaz.WriteLine("import { ActivatedRoute, Params, Router } from \"@angular/router\";");
                            yaz.WriteLine("import { FormBuilder, FormGroup, Validators, FormControl } from \"@angular/forms\";");

                            foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && a.Type.Name == "String" && a.CharLength == -1 && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList())
                            {
                                yaz.WriteLine("import ClassicEditor from \"../../../../../Content/admin/js/ckeditor/ckeditor.js\";");
                                break;
                            }

                            if (fkcList.Count > 0)
                            {
                                yaz.WriteLine("import * as $ from \"jquery\";");
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("@Component({");
                            yaz.WriteLine("\ttemplateUrl: './update.html'");
                            yaz.WriteLine("})");
                            yaz.WriteLine("");
                            yaz.WriteLine("export class Admin" + Table + "UpdateComponent {");
                            yaz.WriteLine("\terrorMsg: string;");
                            yaz.WriteLine("\tid: string;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tupdateForm: FormGroup;");
                            yaz.WriteLine("\tdata: any;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tmodel: any;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tcallTable: boolean;");
                            yaz.WriteLine("");

                            if (fileColumns.Count > 0 || imageColumns.Count > 0)
                            {
                                yaz.WriteLine("\tuploadData: any;");

                                foreach (ColumnInfo item in fileColumns)
                                {
                                    yaz.WriteLine("\tfile" + item.ColumnName + " : any;");
                                }

                                foreach (ColumnInfo item in imageColumns)
                                {
                                    yaz.WriteLine("\timage" + item.ColumnName + ": any;");
                                }

                                yaz.WriteLine("");
                            }

                            yaz.WriteLine("\tprivate subscription: Subscription = new Subscription();");
                            yaz.WriteLine("");

                            string constructor = "constructor(private service: " + Table + "Service, private formBuilder: FormBuilder, private router: Router, private route: ActivatedRoute";
                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
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
                            yaz.WriteLine("\t\tthis.data = new Object();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tthis.callTable = true;");
                            yaz.WriteLine("\t\tthis.FillData();");
                            yaz.WriteLine("");

                            int i = 0;
                            List<ColumnInfo> tempTableColumns = tableColumnInfos.Where(a => a.TableName == Table && a.Type.Name == "String" && a.CharLength == -1 && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(FileColumns, InType.ToUrlLower) && !a.ColumnName.In(ImageColumns, InType.ToUrlLower)).ToList();

                            foreach (ColumnInfo column in tempTableColumns)
                            {
                                if (i == 0)
                                {
                                    yaz.WriteLine("\t\tsetTimeout(function () {");
                                }

                                yaz.WriteLine("\t\t\tClassicEditor");
                                yaz.WriteLine("\t\t\t\t.create(document.querySelector('#" + column.ColumnName + "'), {");
                                yaz.WriteLine("\t\t\t\t})");
                                yaz.WriteLine("\t\t\t\t.then(editor => {");
                                yaz.WriteLine("\t\t\t\t\tconsole.log(editor);");
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

                            yaz.WriteLine("\t\tthis.updateForm = this.formBuilder.group({");

                            foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList())
                            {
                                if (column.Type.Name == "Boolean")
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
                                        if (column.Type.Name.In(new string[] { "Int16", "Int32", "Int64" }))
                                        {
                                            yaz.WriteLine("\t\t\t" + column.ColumnName + ": new FormControl(null, [Validators.required, Validators.min(0)]),");
                                        }
                                        else if (column.Type.Name == "String")
                                        {
                                            if (column.Type.Name == "String" && column.CharLength == -1)
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
                            yaz.WriteLine("\t\t\t\tthis.subscription = this.service.getUpdate(this.id).subscribe((answer) => {");
                            yaz.WriteLine("\t\t\t\t\tthis.model = answer;");
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
                                yaz.WriteLine("\t\t\t\t\t\t$(document)");
                                yaz.WriteLine("\t\t\t\t\t\t\t.off(\"click\", \".fg-button\")");
                                yaz.WriteLine("\t\t\t\t\t\t\t.on(\"click\", \".fg-button\", () => {");
                                yaz.WriteLine("\t\t\t\t\t\t\t\tsetTimeout(() => {");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t\tthis.FillData();");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t}, 1);");
                                yaz.WriteLine("\t\t\t\t\t\t\t});");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\t\t$(document)");
                                yaz.WriteLine("\t\t\t\t\t\t\t.off(\"click\", \"a.cpyLink\")");
                                yaz.WriteLine("\t\t\t\t\t\t\t.on(\"click\", \"a.cpyLink\", function () {");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t$(this).addClass(\"active-cpy\");");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t$(\"a.cpy-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t$(\"a.cpy-yes\").attr(\"data-link\", $(this).attr(\"data-controller\"));");
                                yaz.WriteLine("\t\t\t\t\t\t\t});");
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t\t\t\t$(document)");
                                yaz.WriteLine("\t\t\t\t\t\t\t.off(\"click\", \"a.dltLink\")");
                                yaz.WriteLine("\t\t\t\t\t\t\t.on(\"click\", \"a.dltLink\", function () {");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t$(this).addClass(\"active-dlt\");");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t$(\"a.dlt-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                                yaz.WriteLine("\t\t\t\t\t\t\t\t$(\"a.dlt-yes\").attr(\"data-link\", $(this).attr(\"data-controller\"));");
                                yaz.WriteLine("\t\t\t\t\t\t\t});");

                                int y = 0;

                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    List<ColumnInfo> fColumnNames = Helper.Helper.GetColumnsInfo(connectionInfo, ForeignTableName).ToList();
                                    bool fDeleted = fColumnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? true : false;

                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t\t\t$(document)");
                                    yaz.WriteLine("\t\t\t\t\t\t\t.off(\"click\", \"a.cpy-yes[data-link='" + ForeignTableName + "']\")");
                                    yaz.WriteLine("\t\t\t\t\t\t\t.on(\"click\", \"a.cpy-yes[data-link='" + ForeignTableName + "']\", () => {");
                                    yaz.WriteLine("\t\t\t\t\t\t\t\tlet id: string = $(\"a.cpy-yes\").attr(\"data-id\");");
                                    yaz.WriteLine("\t\t\t\t\t\t\t\tthis.on" + ForeignTableName + "Copy(id);");
                                    yaz.WriteLine("\t\t\t\t\t\t\t});");

                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t\t\t$(document)");
                                    yaz.WriteLine("\t\t\t\t\t\t\t.off(\"click\", \"a.dlt-yes[data-link='" + ForeignTableName + "']\")");
                                    yaz.WriteLine("\t\t\t\t\t\t\t.on(\"click\", \"a.dlt-yes[data-link='" + ForeignTableName + "']\", () => {");
                                    yaz.WriteLine("\t\t\t\t\t\t\t\tlet id: string = $(\"a.dlt-yes\").attr(\"data-id\");");
                                    yaz.WriteLine("\t\t\t\t\t\t\t\tthis.on" + ForeignTableName + "Delete(id);");
                                    yaz.WriteLine("\t\t\t\t\t\t\t});");

                                    if (fDeleted)
                                    {
                                        if (y == 0)
                                        {
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\t\t$(document)");
                                            yaz.WriteLine("\t\t\t\t\t\t\t.off(\"click\", \"a.rmvLink\")");
                                            yaz.WriteLine("\t\t\t\t\t\t\t.on(\"click\", \"a.rmvLink\", function () {");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t$(this).addClass(\"active-rmv\");");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t$(\"a.rmv-yes\").attr(\"data-id\", $(this).attr(\"data-id\"));");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\t$(\"a.rmv-yes\").attr(\"data-link\", $(this).attr(\"data-controller\"));");
                                            yaz.WriteLine("\t\t\t\t\t\t\t});");

                                            y++;
                                        }

                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\t\t\t\t$(document)");
                                        yaz.WriteLine("\t\t\t\t\t\t\t.off(\"click\", \"a.rmv-yes[data-link='" + ForeignTableName + "']\")");
                                        yaz.WriteLine("\t\t\t\t\t\t\t.on(\"click\", \"a.rmv-yes[data-link='" + ForeignTableName + "']\", () => {");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\tlet id: string = $(\"a.rmv-yes\").attr(\"data-id\");");
                                        yaz.WriteLine("\t\t\t\t\t\t\t\tthis.on" + ForeignTableName + "Remove(id);");
                                        yaz.WriteLine("\t\t\t\t\t\t\t});");
                                    }
                                }

                                yaz.WriteLine("\t\t\t\t\t}, 1);");
                            }

                            yaz.WriteLine("\t\t\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
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

                            if (fileColumns.Count > 0 || imageColumns.Count > 0)
                            {
                                foreach (ColumnInfo item in fileColumns)
                                {
                                    yaz.WriteLine("\ton" + item.ColumnName + "FileSelect(event) {");
                                    yaz.WriteLine("\t\tif (event.target.files.length > 0) {");
                                    yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + " = event.target.files[0].name;");
                                    yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + "HasFile = true;");
                                    yaz.WriteLine("\t\t\tthis.file" + item.ColumnName + " = event.target.files[0];");
                                    yaz.WriteLine("\t\t}");
                                    yaz.WriteLine("\t}");
                                    yaz.WriteLine("");
                                }

                                foreach (ColumnInfo item in imageColumns)
                                {
                                    yaz.WriteLine("\ton" + item.ColumnName + "FileSelect(event) {");
                                    yaz.WriteLine("\t\tif (event.target.files.length > 0) {");
                                    yaz.WriteLine("\t\t\tthis.data." + item.ColumnName + " = event.target.files[0].name;");
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

                            yaz.WriteLine("\tonSubmit() {");

                            string tttab = fileColumns.Count > 0 || imageColumns.Count > 0 ? "\t\t" : "";

                            if (fileColumns.Count > 0 || imageColumns.Count > 0)
                            {
                                yaz.WriteLine("\t\tthis.uploadData = new FormData();");

                                foreach (ColumnInfo item in fileColumns)
                                {
                                    yaz.WriteLine("\t\tthis.uploadData.append(\"file\", this.file" + item.ColumnName + ");");
                                }

                                foreach (ColumnInfo item in imageColumns)
                                {
                                    yaz.WriteLine("\t\tthis.uploadData.append(\"file\", this.image" + item.ColumnName + ");");
                                }

                                yaz.WriteLine("");

                                yaz.WriteLine("\t\tthis.subscription = this.service.postUpdateUpload(this.uploadData).subscribe((answerUpload) => {");
                                yaz.WriteLine("\t\t\tif (answerUpload.Mesaj == null)");
                                yaz.WriteLine("\t\t\t{");
                            }

                            int tcCount = tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList().Count;

                            i = 0;

                            foreach (ColumnInfo column in tableColumnInfos.Where(a => a.TableName == Table && !a.ColumnName.In(DeletedColumns, InType.ToUrlLower) && !a.ColumnName.In(UrlColumns, InType.ToUrlLower) && !a.ColumnName.In(GuidColumns, InType.ToUrlLower)).ToList())
                            {
                                if (column.Type.Name == "String" && column.CharLength == -1 && !column.ColumnName.In(FileColumns, InType.ToUrlLower) && !column.ColumnName.In(ImageColumns, InType.ToUrlLower))
                                {
                                    yaz.WriteLine(tttab + "\t\tthis.data." + column.ColumnName + " = $(\".ck-content\").html().replace(\"<p>\", \"\").replace(\"</p>\", \"\");");
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
                                    yaz.WriteLine(tttab + "\t\tthis.data." + column.ColumnName + " = this.updateForm.get(\"" + column.ColumnName + "\").value;");
                                }

                                i++;

                                if (i == tcCount)
                                {
                                    yaz.WriteLine("");
                                }
                            }

                            yaz.WriteLine(tttab + "\t\tthis.service.postUpdate(this.data)");
                            yaz.WriteLine(tttab + "\t\t\t.subscribe((answer) => {");
                            yaz.WriteLine(tttab + "\t\t\t\tif (answer.Mesaj == null) {");
                            yaz.WriteLine(tttab + "\t\t\t\t\tthis.router.navigate(['/Admin/" + Table + "']);");
                            yaz.WriteLine(tttab + "\t\t\t\t}");
                            yaz.WriteLine(tttab + "\t\t\t\telse {");
                            yaz.WriteLine(tttab + "\t\t\t\t\t$(\".alertMessage\").text(answer.Mesaj);");
                            yaz.WriteLine(tttab + "\t\t\t\t\t$(\".alert-error\").fadeIn(\"slow\");");
                            yaz.WriteLine(tttab + "\t\t\t\t}");
                            yaz.WriteLine(tttab + "\t\t\t},");
                            yaz.WriteLine(tttab + "\t\t\t\tresError => this.errorMsg = resError);");

                            if (fileColumns.Count > 0 || imageColumns.Count > 0)
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

                            if (fkcList.Count > 0)
                            {
                                foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                                {
                                    string ForeignTableName = fkc.ForeignTableName;
                                    List<ColumnInfo> fColumnNames = Helper.Helper.GetColumnsInfo(connectionInfo, ForeignTableName).ToList();
                                    bool fDeleted = fColumnNames.Where(a => a.ColumnName.In(DeletedColumns, InType.ToUrlLower)).ToList().Count > 0 ? true : false;

                                    yaz.WriteLine("");
                                    yaz.WriteLine("\ton" + ForeignTableName + "Copy(id) {");
                                    yaz.WriteLine("\t\tthis.subscription = this.service" + ForeignTableName + ".getCopy(id).subscribe((answer) => {");
                                    yaz.WriteLine("\t\t\t$(\"a.cpyLink.active-cpy\").removeClass(\"active-cpy\");");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\tif (answer == true) {");
                                    yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"Copy\");");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\tlet currentUrl = this.router.url;");
                                    yaz.WriteLine("\t\t\t\tthis.router.navigate(['/'], { skipLocationChange: true }).then(() => { this.router.navigate([currentUrl]) });");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t\telse {");
                                    yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"CopyNot\");");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError, () => { this.subscription.unsubscribe(); });");
                                    yaz.WriteLine("\t}");

                                    yaz.WriteLine("");
                                    yaz.WriteLine("\ton" + ForeignTableName + "Delete(id) {");
                                    yaz.WriteLine("\t\tthis.subscription = this.service" + ForeignTableName + ".getDelete(id).subscribe((answer) => {");
                                    yaz.WriteLine("\t\t\tif (answer == true) {");
                                    yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"Delete\");");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\t\t$(\"a.dltLink.active-dlt\").parent(\"li\").parent(\"ul\").parent(\"div\").parent(\"td\").parent(\"tr\").fadeOut(\"slow\", function () {");
                                    yaz.WriteLine("\t\t\t\t\t$(this).remove();");
                                    yaz.WriteLine("\t\t\t\t});");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t\telse {");
                                    yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"DeleteNot\");");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("\t\t}, resError => this.errorMsg = resError,");
                                    yaz.WriteLine("\t\t\t() => { this.subscription.unsubscribe(); });");
                                    yaz.WriteLine("\t}");

                                    if (fDeleted)
                                    {
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\ton" + ForeignTableName + "Remove(id) {");
                                        yaz.WriteLine("\t\tthis.subscription = this.service" + ForeignTableName + ".getRemove(id).subscribe((answer) => {");
                                        yaz.WriteLine("\t\t\tif (answer == true) {");
                                        yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"Remove\");");
                                        yaz.WriteLine("");
                                        yaz.WriteLine("\t\t\t\t$(\"a.rmvLink.active-rmv\").parent(\"li\").parent(\"ul\").parent(\"div\").parent(\"td\").parent(\"tr\").fadeOut(\"slow\", function () {");
                                        yaz.WriteLine("\t\t\t\t\t$(this).remove();");
                                        yaz.WriteLine("\t\t\t\t});");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("\t\t\telse {");
                                        yaz.WriteLine("\t\t\t\tthis.ShowAlert(\"RemoveNot\");");
                                        yaz.WriteLine("\t\t\t}");
                                        yaz.WriteLine("\t\t}, resError => this.errorMsg = resError,");
                                        yaz.WriteLine("\t\t\t() => { this.subscription.unsubscribe(); });");
                                        yaz.WriteLine("\t}");
                                    }
                                }
                            }

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
                    yaz.WriteLine("<" + projectName.Substring(0, 3).ToUrl(true) + "-app></" + projectName.Substring(0, 3).ToUrl(true) + "-app>");
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

        void CreateAngularReadMe()
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
                    yaz.WriteLine("\tsrc (klasör) [İçinde bulunan app klasörünü sil.]");
                    yaz.WriteLine("\tangular.json");
                    yaz.WriteLine("\tpackage.json");
                    yaz.WriteLine("\ttsconfig.app.json");
                    yaz.WriteLine("\ttsconfig.json");
                    yaz.WriteLine("\ttslint.json");
                    yaz.WriteLine("- Visual Studio içinde \"Show All files\" diyip \"node_modules\" klasörü hariç bu dosyalar projeye dahil edeceksin.");
                    yaz.WriteLine("- Programın ürettiği src klasöründe bulunan app klasörünü projedeki src klasörü içine yapıştır.");
                    yaz.WriteLine("- \"npm install --save rxjs-compat\" diyerek rxjs tipini yükleyeceksin.");
                    yaz.WriteLine("- \"npm install jquery --save\" diyerek jquery yükleyeceksin.");
                    yaz.WriteLine("- \"npm install --save @types/jquery\" diyerek jquery tipini yükleyeceksin.");
                    yaz.WriteLine("- \"npm install --save @types/jest\" diyerek jest tipini yükleyeceksin.");
                    yaz.WriteLine("- tsconfig.json dosyası içine \"types\": [ \"jquery\", \"jest\" ] tanımlamasını gireceksin.");
                    yaz.WriteLine("- Content/js klasörü içine libs adında klasör açacaksın. Content/js yerine kendi scriptlerini sakladığın klasör içine açacaksın.");
                    yaz.WriteLine("- angular.json içinde \"options\": { \"outputPath\": \"Content/js/libs\", şeklinde libs yolunu belirteceksin.");
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
