using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TDFactory.Helper;

namespace TDFactory
{
    public partial class TDFactory : Form
    {
        #region Android

        private void btnAndroidBaslat_Click(object sender, EventArgs e)
        {
            projectName = !String.IsNullOrEmpty(txtProjectName.Text) ? txtProjectName.Text : "Proje";
            projectName = projectName.Replace(" ", "");

            connectionInfo = new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text, IsWindowsAuthentication = chkWindowsAuthentication.Checked, Username = txtKullaniciAdi.Text, Password = txtSifre.Text };

            folderDialogKatmanOlustur.SelectedPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            tableColumnNames = GetTableColumnNames();
            selectedTables = GetSelectedTableNames(tableColumnNames);

            if (folderDialogKatmanOlustur.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(DBName))
                {
                    PathAddress = folderDialogKatmanOlustur.SelectedPath;

                    tableColumnNames = GetTableColumnNames();

                    AndKlasorOlustur();
                    AndGradleOlustur();

                    if (chkAndHepsi.Checked)
                    {
                        AndManifestOlustur();
                        AndLayoutOlustur();
                        AndValuesOlustur();
                        AndJavaOlustur();
                        AndModelOlustur();

                        if (rdAndSqlite.Checked)
                        {
                            AndDataSQLite();
                        }
                        else
                        {
                            AndDataWCF();
                        }
                    }
                    else
                    {
                        if (chkAndManifest.Checked)
                        {
                            AndManifestOlustur();
                        }

                        if (chkAndLayout.Checked)
                        {
                            AndLayoutOlustur();
                            AndValuesOlustur();
                        }

                        if (chkAndJava.Checked)
                        {
                            AndJavaOlustur();
                        }

                        if (chkAndModel.Checked)
                        {
                            AndModelOlustur();
                        }

                        if (chkAndData.Checked)
                        {
                            if (rdAndSqlite.Checked)
                            {
                                AndDataSQLite();
                            }
                            else
                            {
                                AndDataWCF();
                            }
                        }
                    }

                    MessageBox.Show("Android Katmanları Başarıyla Oluşturuldu.");

                    if (chkKlasorAc.Checked)
                    {
                        try
                        {
                            Process.Start(folderDialogKatmanOlustur.SelectedPath + "\\" + projectName + "\\Android");
                        }
                        catch
                        {
                            MessageBox.Show("Klasör bulunamadı.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen önce bir veritabanına bağlanın.");
                }

                PathAddress = null;
            }
        }

        private void chkAndHepsi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAndHepsi.Checked)
            {
                chkAndJava.Checked = false;
                chkAndLayout.Checked = false;
                chkAndManifest.Checked = false;
                chkAndModel.Checked = false;
                chkAndData.Checked = false;
                chkAndHepsi.Checked = true;
                rdAndWcf.Checked = true;
            }
            else
            {
                rdAndSqlite.Checked = false;
                rdAndWcf.Checked = false;
            }
        }

        private void chkAndDiger_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            chkAndHepsi.Checked = false;

            if (chk.Name == "chkAndData")
            {
                if (chk.Checked)
                {
                    rdAndWcf.Checked = true;
                }
                else
                {
                    rdAndSqlite.Checked = false;
                    rdAndWcf.Checked = false;
                }
            }
        }

        private void rdAndData_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAndWcf.Checked)
            {
                lstAndIzin.SetSelected(0, true);
                lstAndIzin.SetSelected(2, false);
                lstAndIzin.SetSelected(3, false);
            }
            else if (rdAndSqlite.Checked)
            {
                lstAndIzin.SetSelected(0, false);
                lstAndIzin.SetSelected(2, true);
                lstAndIzin.SetSelected(3, true);
            }
        }

        void AndKlasorOlustur(string tabloAdi = null, bool res = false)
        {
            string projectNameKucuk = projectName.ToLower();

            if (tabloAdi == null)
            {
                if (!Directory.Exists(PathAddress + "\\" + projectName))
                {
                    Directory.CreateDirectory(PathAddress + "\\" + projectName);
                }

                if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android"))
                {
                    Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android");
                }

                if (chkAndHepsi.Checked)
                {
                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk);
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Menu"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Menu");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Kontroller"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Kontroller");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Kontroller\\Adaptor"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Kontroller\\Adaptor");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\DataAccess"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\DataAccess");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\Business"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\Business");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\layout"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\layout");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\layouts"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\layouts");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar\\layout"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar\\layout");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\values"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\values");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\values-v21"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\values-v21");
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\menu"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\menu");
                    }

                    if (rdAndSqlite.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\assets"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\assets");
                        }
                    }

                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Model"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Model");
                    }
                }
                else
                {
                    if (chkAndJava.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk);
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Menu"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Menu");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Kontroller"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Kontroller");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Kontroller\\Adaptor"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Kontroller\\Adaptor");
                        }
                    }

                    if (chkAndLayout.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\layout"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\layout");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\layouts"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\layouts");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar\\layout"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar\\layout");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\values"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\values");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\values-v21"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\values-v21");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\menu"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\menu");
                        }
                    }

                    if (chkAndModel.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk);
                        }

                        if (rdAndSqlite.Checked)
                        {
                            if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\assets"))
                            {
                                Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\assets");
                            }
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Model"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Model");
                        }
                    }

                    if (chkAndData.Checked)
                    {
                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\DataAccess"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\DataAccess");
                        }

                        if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\Business"))
                        {
                            Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\Business");
                        }
                    }
                }
            }
            else
            {
                if (res == false)
                {
                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin\\" + tabloAdi))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin\\" + tabloAdi);
                    }
                }
                else
                {
                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\" + tabloAdi))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\" + tabloAdi);
                    }


                    if (!Directory.Exists(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\" + tabloAdi + "\\layout"))
                    {
                        Directory.CreateDirectory(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\" + tabloAdi + "\\layout");
                    }
                }
            }
        }

        void AndLayoutOlustur()
        {
            //Aktivite Sayac
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layout\\aktivite_sayac.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    yaz.WriteLine("<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\tandroid:layout_height=\"match_parent\"");
                    yaz.WriteLine("\tandroid:gravity=\"center\"");
                    yaz.WriteLine("\tandroid:orientation=\"vertical\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<ImageView");
                    yaz.WriteLine("\t\tandroid:src=\"@mipmap/ic_launcher\"");
                    yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<TextView");
                    yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:layout_marginTop=\"10dp\"");
                    yaz.WriteLine("\t\tandroid:text=\"" + projectName + " Projesine Hoşgeldiniz...\"");
                    yaz.WriteLine("\t\tandroid:textSize=\"15sp\" />");
                    yaz.WriteLine("</LinearLayout>");

                    yaz.Close();
                }
            }

            //Aktivite Giriş
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layout\\aktivite_giris.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    yaz.WriteLine("<android.support.v4.widget.DrawerLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    yaz.WriteLine("\txmlns:tools=\"http://schemas.android.com/tools\"");
                    yaz.WriteLine("\tandroid:id=\"@+id/drawerLayout\"");
                    yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\tandroid:layout_height=\"match_parent\"");
                    yaz.WriteLine("\ttools:openDrawer=\"start\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<android.support.design.widget.CoordinatorLayout");
                    yaz.WriteLine("\t\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"match_parent\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t<include layout=\"@layout/toolbar\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t<FrameLayout");
                    yaz.WriteLine("\t\t\tandroid:id=\"@+id/flytGiris\"");
                    yaz.WriteLine("\t\t\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\t\t\tandroid:layout_height=\"match_parent\"");
                    yaz.WriteLine("\t\t\tandroid:layout_marginTop=\"55dp\"");
                    yaz.WriteLine("\t\t\tandroid:padding=\"10dp\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t</android.support.design.widget.CoordinatorLayout>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<include layout=\"@layout/navbar\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("</android.support.v4.widget.DrawerLayout>");

                    yaz.Close();
                }
            }

            //Fragman Giriş
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layout\\fragman_giris.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    yaz.WriteLine("<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\tandroid:layout_height=\"match_parent\"");
                    yaz.WriteLine("\tandroid:gravity=\"center_horizontal\"");
                    yaz.WriteLine("\tandroid:paddingBottom=\"10dp\"");
                    yaz.WriteLine("\tandroid:paddingLeft=\"10dp\"");
                    yaz.WriteLine("\tandroid:paddingRight=\"10dp\"");
                    yaz.WriteLine("\tandroid:paddingTop=\"10dp\"");
                    yaz.WriteLine("\tandroid:orientation=\"vertical\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<ImageView");
                    yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:src=\"@mipmap/ic_launcher\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<TextView");
                    yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:layout_marginTop=\"10dp\"");
                    yaz.WriteLine("\t\tandroid:textSize=\"15sp\"");
                    yaz.WriteLine("\t\tandroid:text=\"" + projectName + " Giriş Sayfası\" />");
                    yaz.WriteLine("</LinearLayout>");

                    yaz.Close();
                }
            }

            //Liste Satır
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layout\\liste_satir.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    yaz.WriteLine("<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    yaz.WriteLine("\tandroid:id=\"@+id/lvSatir\"");
                    yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\tandroid:layout_height=\"match_parent\"");
                    yaz.WriteLine("\tandroid:padding=\"10dp\"");
                    yaz.WriteLine("\tandroid:orientation=\"horizontal\">");
                    yaz.WriteLine("\t<TextView");
                    yaz.WriteLine("\t\tandroid:id=\"@+id/lblID\"");
                    yaz.WriteLine("\t\tandroid:visibility=\"gone\"");
                    yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\" />");
                    yaz.WriteLine("\t<TextView");
                    yaz.WriteLine("\t\tandroid:id=\"@+id/lblBaslik\"");
                    yaz.WriteLine("\t\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:gravity=\"center_vertical\"");
                    yaz.WriteLine("\t\tandroid:textSize=\"15sp\"/>");
                    yaz.WriteLine("</LinearLayout>");

                    yaz.Close();
                }
            }

            /* Navigasyon Bar */
            //NavBar
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar\\layout\\navbar.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    yaz.WriteLine("<android.support.design.widget.NavigationView xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    yaz.WriteLine("\txmlns:app=\"http://schemas.android.com/apk/res-auto\"");
                    yaz.WriteLine("\tandroid:id=\"@+id/navView\"");
                    yaz.WriteLine("\tandroid:layout_width=\"wrap_content\"");
                    yaz.WriteLine("\tandroid:layout_height=\"match_parent\"");
                    yaz.WriteLine("\tandroid:layout_gravity=\"start\"");
                    yaz.WriteLine("\tandroid:fitsSystemWindows=\"true\"");
                    yaz.WriteLine("\tandroid:orientation=\"vertical\"");
                    yaz.WriteLine("\tapp:headerLayout=\"@layout/navbar_ust\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<ScrollView");
                    yaz.WriteLine("\t\tandroid:layout_marginTop=\"@dimen/navbar_ust_yukseklik\"");
                    yaz.WriteLine("\t\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t<LinearLayout");
                    yaz.WriteLine("\t\t\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\t\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\t\tandroid:orientation=\"vertical\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t<com.sinasalik.thrashead.tdmenu.TDMenu");
                    yaz.WriteLine("\t\t\t\tandroid:id=\"@+id/tdMenuSayfa\"");
                    yaz.WriteLine("\t\t\t\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\t\t\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\t\t\tandroid:layout_marginTop=\"2dp\"");
                    yaz.WriteLine("\t\t\t\tandroid:orientation=\"vertical\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t<com.sinasalik.thrashead.tdmenu.TDMenu");
                    yaz.WriteLine("\t\t\t\tandroid:id=\"@+id/tdMenuAdmin\"");
                    yaz.WriteLine("\t\t\t\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\t\t\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\t\t\tandroid:layout_marginTop=\"2dp\"");
                    yaz.WriteLine("\t\t\t\tandroid:orientation=\"vertical\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\t<com.sinasalik.thrashead.tdmenu.TDMenu");
                    yaz.WriteLine("\t\t\t\tandroid:id=\"@+id/tdMenuAlt\"");
                    yaz.WriteLine("\t\t\t\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\t\t\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\t\t\tandroid:layout_marginTop=\"2dp\"");
                    yaz.WriteLine("\t\t\t\tandroid:orientation=\"vertical\" />");
                    yaz.WriteLine("\t\t</LinearLayout>");
                    yaz.WriteLine("\t</ScrollView>");
                    yaz.WriteLine("</android.support.design.widget.NavigationView>");

                    yaz.Close();
                }
            }

            //NavBar Üst
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar\\layout\\navbar_ust.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    yaz.WriteLine("<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    yaz.WriteLine("\txmlns:app=\"http://schemas.android.com/apk/res-auto\"");
                    yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\tandroid:layout_height=\"@dimen/navbar_ust_yukseklik\"");
                    yaz.WriteLine("\tandroid:gravity=\"bottom|right\"");
                    yaz.WriteLine("\tandroid:orientation=\"vertical\"");
                    yaz.WriteLine("\tandroid:paddingBottom=\"@dimen/navbar_dikey_margin\"");
                    yaz.WriteLine("\tandroid:paddingLeft=\"@dimen/navbar_yatay_margin\"");
                    yaz.WriteLine("\tandroid:paddingRight=\"@dimen/navbar_yatay_margin\"");
                    yaz.WriteLine("\tandroid:paddingTop=\"@dimen/navbar_dikey_margin\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<ImageView");
                    yaz.WriteLine("\t\tandroid:id=\"@+id/imageView\"");
                    yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:paddingTop=\"@dimen/navbar_ust_bosluk\"");
                    yaz.WriteLine("\t\tapp:srcCompat=\"@mipmap/ic_launcher\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<TextView");
                    yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:text=\"@string/app_name\"");
                    yaz.WriteLine("\t\tandroid:layout_marginTop=\"10dp\"");
                    yaz.WriteLine("\t\tandroid:textAppearance=\"@style/TextAppearance.AppCompat.Body1\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<TextView");
                    yaz.WriteLine("\t\tandroid:id=\"@+id/textView\"");
                    yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:textSize=\"10sp\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\t\tandroid:text=\"@string/app_name\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("</LinearLayout>");

                    yaz.Close();
                }
            }

            //Toolbar
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\navbar\\layout\\toolbar.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    yaz.WriteLine("<android.support.design.widget.AppBarLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\tandroid:layout_height=\"wrap_content\"");
                    yaz.WriteLine("\txmlns:app=\"http://schemas.android.com/apk/res-auto\"");
                    yaz.WriteLine("\tandroid:theme=\"@style/" + projectName + "Theme.AppBarOverlay\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<android.support.v7.widget.Toolbar");
                    yaz.WriteLine("\t\tandroid:id=\"@+id/toolbar\"");
                    yaz.WriteLine("\t\tandroid:layout_width=\"match_parent\"");
                    yaz.WriteLine("\t\tandroid:layout_height=\"?attr/actionBarSize\"");
                    yaz.WriteLine("\t\tapp:popupTheme=\"@style/" + projectName + "Theme.PopupOverlay\" />");
                    yaz.WriteLine("");
                    yaz.WriteLine("</android.support.design.widget.AppBarLayout>");

                    yaz.Close();
                }
            }

            foreach (string Table in selectedTables)
            {
                string _table = Table.ToHyperLinkText(true);

                AndKlasorOlustur(_table, true);

                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));
                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                //FrameLayout
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\" + _table + "\\layout\\admin_" + _table + "_sayfa.xml", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        yaz.WriteLine("<android.support.v4.widget.DrawerLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                        yaz.WriteLine("\txmlns:tools=\"http://schemas.android.com/tools\"");
                        yaz.WriteLine("\tandroid:id=\"@+id/drawerLayout\"");
                        yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                        yaz.WriteLine("\tandroid:layout_height=\"match_parent\"");
                        yaz.WriteLine("\ttools:openDrawer=\"start\">");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t<android.support.design.widget.CoordinatorLayout");
                        yaz.WriteLine("\t\tandroid:layout_width=\"match_parent\"");
                        yaz.WriteLine("\t\tandroid:layout_height=\"match_parent\">");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t<include layout=\"@layout/toolbar\" />");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t<FrameLayout");
                        yaz.WriteLine("\t\t\tandroid:id=\"@+id/flyt" + Table + "\"");
                        yaz.WriteLine("\t\t\tandroid:layout_width=\"match_parent\"");
                        yaz.WriteLine("\t\t\tandroid:layout_height=\"match_parent\"");
                        yaz.WriteLine("\t\t\tandroid:layout_marginTop=\"55dp\"");
                        yaz.WriteLine("\t\t\tandroid:padding=\"10dp\" />");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t</android.support.design.widget.CoordinatorLayout>");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t<include layout=\"@layout/navbar\" />");
                        yaz.WriteLine("");
                        yaz.WriteLine("</android.support.v4.widget.DrawerLayout>");

                        yaz.Close();
                    }
                }

                //Liste
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\" + _table + "\\layout\\admin_" + _table + "_liste.xml", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        yaz.WriteLine("<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                        yaz.WriteLine("\txmlns:tools=\"http://schemas.android.com/tools\"");
                        yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                        yaz.WriteLine("\tandroid:layout_height=\"match_parent\"");
                        yaz.WriteLine("\tandroid:gravity=\"center_horizontal\"");
                        yaz.WriteLine("\tandroid:orientation=\"vertical\"");
                        yaz.WriteLine("\ttools:context=\".Admin." + Table + "." + Table + "Liste\">");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t<TextView");
                        yaz.WriteLine("\t\tandroid:id=\"@+id/lbl" + Table + "\"");
                        yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                        yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                        yaz.WriteLine("\t\tandroid:layout_marginBottom=\"10dp\"");
                        yaz.WriteLine("\t\tandroid:text=\"" + Table + " Listesi\"");
                        yaz.WriteLine("\t\tandroid:textSize=\"20sp\"");
                        yaz.WriteLine("\t\tandroid:textStyle=\"bold\" />");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t<Button");
                        yaz.WriteLine("\t\tandroid:id=\"@+id/btnEkle\"");
                        yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                        yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                        yaz.WriteLine("\t\tandroid:layout_marginBottom=\"10dp\"");
                        yaz.WriteLine("\t\tandroid:text=\"Ekle\" />");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t<ListView");
                        yaz.WriteLine("\t\tandroid:id=\"@+id/lvListe\"");
                        yaz.WriteLine("\t\tandroid:layout_width=\"match_parent\"");
                        yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\" />");
                        yaz.WriteLine("</LinearLayout>");

                        yaz.Close();
                    }
                }

                //Ekle
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\" + _table + "\\layout\\admin_" + _table + "_ekle.xml", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                    {
                        yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                        yaz.WriteLine("<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                        yaz.WriteLine("\txmlns:tools=\"http://schemas.android.com/tools\"");
                        yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                        yaz.WriteLine("\tandroid:layout_height=\"match_parent\"");
                        yaz.WriteLine("\tandroid:gravity=\"center_horizontal\"");
                        yaz.WriteLine("\tandroid:orientation=\"vertical\"");
                        yaz.WriteLine("\ttools:context=\".Admin." + Table + "." + Table + "Ekle\">");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t<TextView");
                        yaz.WriteLine("\t\tandroid:id=\"@+id/lbl" + Table + "\"");
                        yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                        yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                        yaz.WriteLine("\t\tandroid:layout_marginBottom=\"10dp\"");
                        yaz.WriteLine("\t\tandroid:textSize=\"20sp\"");
                        yaz.WriteLine("\t\tandroid:textStyle=\"bold\"");
                        yaz.WriteLine("\t\tandroid:text=\"" + Table + " Ekle\" />");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t<ScrollView");
                        yaz.WriteLine("\t\tandroid:layout_width=\"match_parent\"");
                        yaz.WriteLine("\t\tandroid:layout_height=\"match_parent\">");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t<LinearLayout");
                        yaz.WriteLine("\t\t\tandroid:layout_width=\"match_parent\"");
                        yaz.WriteLine("\t\t\tandroid:layout_height=\"wrap_content\"");
                        yaz.WriteLine("\t\t\tandroid:orientation=\"vertical\">");

                        foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t<LinearLayout");
                            yaz.WriteLine("\t\t\t\tandroid:layout_width=\"match_parent\"");
                            yaz.WriteLine("\t\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\tandroid:layout_marginTop=\"10dp\"");
                            yaz.WriteLine("\t\t\t\tandroid:orientation=\"horizontal\">");
                            yaz.WriteLine("");

                            if (column.ColumnName != id)
                            {
                                yaz.WriteLine("\t\t\t\t<TextView");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"match_parent\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_weight=\"2\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:textStyle=\"bold\"");

                                List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                if (foreLst.Count > 0)
                                {
                                    yaz.WriteLine("\t\t\t\t\tandroid:text=\"" + column.ColumnName + " : \" />");
                                    yaz.WriteLine("");

                                    yaz.WriteLine("\t\t\t\t<Spinner");
                                    yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/spin" + column.ColumnName + "\"");
                                }
                                else
                                {
                                    yaz.WriteLine("\t\t\t\t\tandroid:text=\"" + column.ColumnName + " : \" />");
                                    yaz.WriteLine("");

                                    string ct = column.TypeName.Name;

                                    string[] tString = { "Char", "String", "TimeSpan", "Guid", "Object", "Byte", "Chars", "Bytes" };
                                    string[] tInt = { "Int16", "Int32", "Int64" };
                                    string[] tDecimal = { "Decimal", "Double", "Single" };
                                    string[] tDate = { "DateTime", "DateTimeOffset" };
                                    string[] tBool = { "Boolean" };

                                    if (ct == "Boolean")
                                    {
                                        yaz.WriteLine("\t\t\t\t<CheckBox");
                                        yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/chk" + column.ColumnName + "\"");
                                    }
                                    else
                                    {
                                        yaz.WriteLine("\t\t\t\t<EditText");
                                        yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/txt" + column.ColumnName + "\"");

                                        if (ct.In(tInt))
                                        {
                                            yaz.WriteLine("\t\t\t\t\tandroid:inputType=\"number\"");
                                        }
                                        else if (ct.In(tDecimal))
                                        {
                                            yaz.WriteLine("\t\t\t\t\tandroid:inputType=\"numberDecimal\"");
                                        }
                                        else if (ct.In(tDate))
                                        {
                                            if (ct == "Date")
                                            {
                                                yaz.WriteLine("\t\t\t\t\tandroid:inputType=\"date\"");
                                            }
                                            else if (ct == "DateTime")
                                            {
                                                yaz.WriteLine("\t\t\t\t\tandroid:inputType=\"datetime\"");
                                            }
                                        }
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"match_parent\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_weight=\"1\" />");

                            yaz.WriteLine("\t\t\t</LinearLayout>");
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t<LinearLayout");
                        yaz.WriteLine("\t\t\t\tandroid:layout_width=\"match_parent\"");
                        yaz.WriteLine("\t\t\t\tandroid:layout_height=\"wrap_content\"");
                        yaz.WriteLine("\t\t\t\tandroid:layout_marginTop=\"20dp\"");
                        yaz.WriteLine("\t\t\t\tandroid:gravity=\"center_horizontal\"");
                        yaz.WriteLine("\t\t\t\tandroid:orientation=\"horizontal\">");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t<Button");
                        yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/btnKaydet\"");
                        yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"wrap_content\"");
                        yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                        yaz.WriteLine("\t\t\t\t\tandroid:text=\"Kaydet\" />");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t<Button");
                        yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/btnIptal\"");
                        yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"wrap_content\"");
                        yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                        yaz.WriteLine("\t\t\t\t\tandroid:text=\"İptal\" />");
                        yaz.WriteLine("\t\t\t</LinearLayout>");
                        yaz.WriteLine("\t\t</LinearLayout>");
                        yaz.WriteLine("\t</ScrollView>");
                        yaz.WriteLine("</LinearLayout>");

                        yaz.Close();
                    }
                }

                if (identityColumns.Count > 0)
                {
                    //Detay
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\" + _table + "\\layout\\admin_" + _table + "_detay.xml", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                        {
                            yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                            yaz.WriteLine("<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                            yaz.WriteLine("\txmlns:tools=\"http://schemas.android.com/tools\"");
                            yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                            yaz.WriteLine("\tandroid:layout_height=\"match_parent\"");
                            yaz.WriteLine("\tandroid:gravity=\"center_horizontal\"");
                            yaz.WriteLine("\tandroid:orientation=\"vertical\"");
                            yaz.WriteLine("\ttools:context=\".Admin." + Table + "." + Table + "Detay\">");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t<TextView");
                            yaz.WriteLine("\t\tandroid:id=\"@+id/lbl" + Table + "\"");
                            yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                            yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\tandroid:layout_marginBottom=\"10dp\"");
                            yaz.WriteLine("\t\tandroid:textSize=\"20sp\"");
                            yaz.WriteLine("\t\tandroid:textStyle=\"bold\"");
                            yaz.WriteLine("\t\tandroid:text=\"" + Table + " Detay\" />");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t<ScrollView");
                            yaz.WriteLine("\t\tandroid:layout_width=\"match_parent\"");
                            yaz.WriteLine("\t\tandroid:layout_height=\"match_parent\">");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t<LinearLayout");
                            yaz.WriteLine("\t\t\tandroid:layout_width=\"match_parent\"");
                            yaz.WriteLine("\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\tandroid:orientation=\"vertical\">");

                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();
                                
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t<LinearLayout");
                                yaz.WriteLine("\t\t\t\tandroid:layout_width=\"match_parent\"");
                                yaz.WriteLine("\t\t\t\tandroid:layout_height=\"wrap_content\"");
                                yaz.WriteLine("\t\t\t\tandroid:layout_marginTop=\"10dp\"");
                                yaz.WriteLine("\t\t\t\tandroid:orientation=\"horizontal\">");
                                yaz.WriteLine("");

                                yaz.WriteLine("\t\t\t\t<TextView");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"match_parent\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_weight=\"2\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:textStyle=\"bold\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:text=\"" + column.ColumnName + " : \" />");
                                yaz.WriteLine("");

                                if (column.TypeName.Name == "Boolean")
                                {
                                    yaz.WriteLine("\t\t\t\t<CheckBox");
                                    yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/chk" + column.ColumnName + "\"");
                                    yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"match_parent\"");
                                    yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                                    yaz.WriteLine("\t\t\t\t\tandroid:layout_weight=\"1\"");
                                    yaz.WriteLine("\t\t\t\t\tandroid:enabled=\"false\" />");
                                }
                                else
                                {
                                    yaz.WriteLine("\t\t\t\t<TextView");
                                    yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/lbl" + column.ColumnName + "\"");
                                    yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"match_parent\"");
                                    yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                                    yaz.WriteLine("\t\t\t\t\tandroid:layout_weight=\"1\" />");
                                }

                                yaz.WriteLine("\t\t\t</LinearLayout>");
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t<LinearLayout");
                            yaz.WriteLine("\t\t\t\tandroid:layout_width=\"match_parent\"");
                            yaz.WriteLine("\t\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\tandroid:layout_marginTop=\"20dp\"");
                            yaz.WriteLine("\t\t\t\tandroid:gravity=\"center_horizontal\"");
                            yaz.WriteLine("\t\t\t\tandroid:orientation=\"horizontal\">");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t<Button");
                            yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/btnListe\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:text=\"Listele\" />");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t<Button");
                            yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/btnDuzenle\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:text=\"Düzenle\" />");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t<Button");
                            yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/btnSil\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:text=\"Sil\" />");
                            yaz.WriteLine("\t\t\t</LinearLayout>");
                            yaz.WriteLine("\t\t</LinearLayout>");
                            yaz.WriteLine("\t</ScrollView>");
                            yaz.WriteLine("</LinearLayout>");

                            yaz.Close();
                        }
                    }

                    //Düzenle
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\layouts\\" + _table + "\\layout\\admin_" + _table + "_duzenle.xml", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                        {
                            yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                            yaz.WriteLine("<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                            yaz.WriteLine("\txmlns:tools=\"http://schemas.android.com/tools\"");
                            yaz.WriteLine("\tandroid:layout_width=\"match_parent\"");
                            yaz.WriteLine("\tandroid:layout_height=\"match_parent\"");
                            yaz.WriteLine("\tandroid:gravity=\"center_horizontal\"");
                            yaz.WriteLine("\tandroid:orientation=\"vertical\"");
                            yaz.WriteLine("\ttools:context=\".Admin." + Table + "." + Table + "Duzenle\">");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t<TextView");
                            yaz.WriteLine("\t\tandroid:id=\"@+id/lbl" + Table + "\"");
                            yaz.WriteLine("\t\tandroid:layout_width=\"wrap_content\"");
                            yaz.WriteLine("\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\tandroid:layout_marginBottom=\"10dp\"");
                            yaz.WriteLine("\t\tandroid:textSize=\"20sp\"");
                            yaz.WriteLine("\t\tandroid:textStyle=\"bold\"");
                            yaz.WriteLine("\t\tandroid:text=\"" + Table + " Düzenle\" />");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t<ScrollView");
                            yaz.WriteLine("\t\tandroid:layout_width=\"match_parent\"");
                            yaz.WriteLine("\t\tandroid:layout_height=\"match_parent\">");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t<LinearLayout");
                            yaz.WriteLine("\t\t\tandroid:layout_width=\"match_parent\"");
                            yaz.WriteLine("\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\tandroid:orientation=\"vertical\">");

                            foreach (TableColumnNames column in tableColumnNames.Where(a => a.TableName == Table).ToList())
                            {
                                yaz.WriteLine("");
                                yaz.WriteLine("\t\t\t<LinearLayout");
                                yaz.WriteLine("\t\t\t\tandroid:layout_width=\"match_parent\"");
                                yaz.WriteLine("\t\t\t\tandroid:layout_height=\"wrap_content\"");
                                yaz.WriteLine("\t\t\t\tandroid:layout_marginTop=\"10dp\"");
                                yaz.WriteLine("\t\t\t\tandroid:orientation=\"horizontal\">");
                                yaz.WriteLine("");

                                yaz.WriteLine("\t\t\t\t<TextView");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"match_parent\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_weight=\"2\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:textStyle=\"bold\"");

                                if (column.ColumnName == id)
                                {
                                    yaz.WriteLine("\t\t\t\t\tandroid:text=\"" + column.ColumnName + " : \" />");
                                    yaz.WriteLine("");

                                    yaz.WriteLine("\t\t\t\t<TextView");
                                    yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/lbl" + column.ColumnName + "\"");
                                }
                                else
                                {
                                    List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    if (foreLst.Count > 0)
                                    {
                                        yaz.WriteLine("\t\t\t\t\tandroid:text=\"" + column.ColumnName + " : \" />");
                                        yaz.WriteLine("");

                                        yaz.WriteLine("\t\t\t\t<Spinner");
                                        yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/spin" + column.ColumnName + "\"");
                                    }
                                    else
                                    {
                                        yaz.WriteLine("\t\t\t\t\tandroid:text=\"" + column.ColumnName + " : \" />");
                                        yaz.WriteLine("");

                                        string ct = column.TypeName.Name;

                                        string[] tString = { "Char", "String", "TimeSpan", "Guid", "Object", "Byte", "Chars", "Bytes" };
                                        string[] tInt = { "Int16", "Int32", "Int64" };
                                        string[] tDecimal = { "Decimal", "Double", "Single" };
                                        string[] tDate = { "DateTime", "DateTimeOffset" };
                                        string[] tBool = { "Boolean" };

                                        if (ct == "Boolean")
                                        {
                                            yaz.WriteLine("\t\t\t\t<CheckBox");
                                            yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/chk" + column.ColumnName + "\"");
                                        }
                                        else
                                        {
                                            yaz.WriteLine("\t\t\t\t<EditText");
                                            yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/txt" + column.ColumnName + "\"");

                                            if (ct.In(tInt))
                                            {
                                                yaz.WriteLine("\t\t\t\t\tandroid:inputType=\"number\"");
                                            }
                                            else if (ct.In(tDecimal))
                                            {
                                                yaz.WriteLine("\t\t\t\t\tandroid:inputType=\"numberDecimal\"");
                                            }
                                            else if (ct.In(tDate))
                                            {
                                                if (ct == "Date")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\tandroid:inputType=\"date\"");
                                                }
                                                else if (ct == "DateTime")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\tandroid:inputType=\"datetime\"");
                                                }
                                            }
                                        }
                                    }
                                }

                                yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"match_parent\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                                yaz.WriteLine("\t\t\t\t\tandroid:layout_weight=\"1\" />");

                                yaz.WriteLine("\t\t\t</LinearLayout>");

                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t<LinearLayout");
                            yaz.WriteLine("\t\t\t\tandroid:layout_width=\"match_parent\"");
                            yaz.WriteLine("\t\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\tandroid:layout_marginTop=\"20dp\"");
                            yaz.WriteLine("\t\t\t\tandroid:gravity=\"center_horizontal\"");
                            yaz.WriteLine("\t\t\t\tandroid:orientation=\"horizontal\">");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t<Button");
                            yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/btnKaydet\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:text=\"Kaydet\" />");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t<Button");
                            yaz.WriteLine("\t\t\t\t\tandroid:id=\"@+id/btnIptal\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_width=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:layout_height=\"wrap_content\"");
                            yaz.WriteLine("\t\t\t\t\tandroid:text=\"İptal\" />");
                            yaz.WriteLine("\t\t\t</LinearLayout>");
                            yaz.WriteLine("\t\t</LinearLayout>");
                            yaz.WriteLine("\t</ScrollView>");
                            yaz.WriteLine("</LinearLayout>");

                            yaz.Close();
                        }
                    }
                }
            }
        }

        void AndJavaOlustur()
        {
            string projectNameKucuk = projectName.ToLower();

            //Aktivite Sayac
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\AktiviteSayac.java", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                {
                    yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ";");
                    yaz.WriteLine("");
                    yaz.WriteLine("import android.content.Context;");
                    yaz.WriteLine("import android.content.Intent;");
                    yaz.WriteLine("import android.support.v7.app.AppCompatActivity;");
                    yaz.WriteLine("import android.os.Bundle;");
                    yaz.WriteLine("");
                    yaz.WriteLine("public class AktiviteSayac extends AppCompatActivity {");
                    yaz.WriteLine("\tContext context = this;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tprotected void onCreate(Bundle savedInstanceState) {");
                    yaz.WriteLine("\t\tsuper.onCreate(savedInstanceState);");
                    yaz.WriteLine("\t\tsetContentView(R.layout.aktivite_sayac);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tSayac();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tprotected void onStart() {");
                    yaz.WriteLine("\t\tsuper.onStart();");
                    yaz.WriteLine("\t\tSayac();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tprotected void onRestart() {");
                    yaz.WriteLine("\t\tsuper.onRestart();");
                    yaz.WriteLine("\t\tSayac();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tprotected void onResume() {");
                    yaz.WriteLine("\t\tsuper.onResume();");
                    yaz.WriteLine("\t\tSayac();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tpublic void onBackPressed() {");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate void Sayac() {");
                    yaz.WriteLine("\t\tnew Thread(new Runnable() {");
                    yaz.WriteLine("\t\t\tpublic void run() {");
                    yaz.WriteLine("\t\t\t\ttry {");
                    yaz.WriteLine("\t\t\t\t\tThread.sleep(3000);");
                    yaz.WriteLine("\t\t\t\t\tstartActivity(new Intent(context, AktiviteGiris.class));");
                    yaz.WriteLine("\t\t\t\t} catch (InterruptedException e) {");
                    yaz.WriteLine("\t\t\t\t\te.printStackTrace();");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}).start();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                }
            }

            //Aktivite Giriş
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\AktiviteGiris.java", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                {
                    yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ";");
                    yaz.WriteLine("");
                    yaz.WriteLine("import android.app.AlertDialog;");
                    yaz.WriteLine("import android.content.Context;");
                    yaz.WriteLine("import android.content.DialogInterface;");
                    yaz.WriteLine("import android.content.Intent;");
                    yaz.WriteLine("import android.support.v4.app.Fragment;");
                    yaz.WriteLine("import android.support.v4.view.GravityCompat;");
                    yaz.WriteLine("import android.support.v4.widget.DrawerLayout;");
                    yaz.WriteLine("import android.support.v7.app.AppCompatActivity;");
                    yaz.WriteLine("import android.os.Bundle;");
                    yaz.WriteLine("import android.view.MenuItem;");
                    yaz.WriteLine("import android.widget.Toast;");
                    yaz.WriteLine("");
                    yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Araclar.Araclar;");
                    yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Araclar.Menu.Menu;");
                    yaz.WriteLine("import com.sinasalik.thrashead.tdlibrary.TDAraclar;");
                    yaz.WriteLine("");
                    yaz.WriteLine("public class AktiviteGiris extends AppCompatActivity {");
                    yaz.WriteLine("\tprivate Context context;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate FragmanGiris fragmanGiris;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate AlertDialog.Builder alertKapat;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tprotected void onCreate(Bundle savedInstanceState) {");
                    yaz.WriteLine("\t\tsuper.onCreate(savedInstanceState);");
                    yaz.WriteLine("\t\tsetContentView(R.layout.aktivite_giris);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tSayfaHazirla();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate void SayfaHazirla() {");
                    yaz.WriteLine("\t\tNesneler();");
                    yaz.WriteLine("\t\tOlaylar();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate void Nesneler() {");
                    yaz.WriteLine("\t\tcontext = this;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\talertKapat = new AlertDialog.Builder(context);");
                    yaz.WriteLine("\t\talertKapat.setTitle(R.string.alertCikisBaslik)");
                    yaz.WriteLine("\t\t\t\t.setMessage(R.string.alertKapatMesaj)");
                    yaz.WriteLine("\t\t\t\t.setIcon(R.mipmap.ic_launcher)");
                    yaz.WriteLine("\t\t\t\t.setPositiveButton(R.string.alertKapatTamam, new DialogInterface.OnClickListener() {");
                    yaz.WriteLine("\t\t\t\t\t@Override");
                    yaz.WriteLine("\t\t\t\t\tpublic void onClick(DialogInterface dialog, int which) {");
                    yaz.WriteLine("\t\t\t\t\t\tKapat();");
                    yaz.WriteLine("\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t})");
                    yaz.WriteLine("\t\t\t\t.setNegativeButton(R.string.alertKapatIptal, null);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tfragmanGiris = new FragmanGiris();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate void Olaylar() {");
                    yaz.WriteLine("\t\tMenu.MenuHazirla(this);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif(Araclar.KlasorleriOlustur(context)) {");
                    yaz.WriteLine("\t\t\tTDAraclar.BellegeYaz(this, \"thrashead\", \"klasor\", true);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t\telse {");
                    yaz.WriteLine("\t\t\tToast.makeText(context, (String) TDAraclar.KaynakDegerDon(context, R.string.klasorHata, TDAraclar.KaynakTip.String), Toast.LENGTH_SHORT).show();");
                    yaz.WriteLine("\t\t\tTDAraclar.BellegeYaz(this, \"thrashead\", \"klasor\", false);");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tgetSupportFragmentManager().beginTransaction().add(R.id.flytGiris, fragmanGiris, \"FragmanGiris\").commit();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic Fragment AktifFragman() {");
                    yaz.WriteLine("\t\treturn this.getSupportFragmentManager().findFragmentById(R.id.flytGiris);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic void Kapat() {");
                    yaz.WriteLine("\t\tthis.finish();");
                    yaz.WriteLine("\t\tIntent intent = new Intent(Intent.ACTION_MAIN);");
                    yaz.WriteLine("\t\tintent.addCategory(Intent.CATEGORY_HOME);");
                    yaz.WriteLine("\t\tintent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);");
                    yaz.WriteLine("\t\tstartActivity(intent);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tpublic void onBackPressed() {");
                    yaz.WriteLine("\t\tDrawerLayout drawerLayout = (DrawerLayout) findViewById(R.id.drawerLayout);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif (drawerLayout.isDrawerOpen(GravityCompat.START)) {");
                    yaz.WriteLine("\t\t\tdrawerLayout.closeDrawer(GravityCompat.START);");
                    yaz.WriteLine("\t\t} else {");
                    yaz.WriteLine("\t\t\talertKapat.show();");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tpublic boolean onCreateOptionsMenu(android.view.Menu menu) {");
                    yaz.WriteLine("\t\tgetMenuInflater().inflate(R.menu.sagmenu, menu);");
                    yaz.WriteLine("\t\treturn true;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tpublic boolean onOptionsItemSelected(MenuItem item) {");
                    yaz.WriteLine("\t\tint id = item.getItemId();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tswitch (id) {");
                    yaz.WriteLine("\t\t\tcase R.id.menuAnaSayfa:");
                    yaz.WriteLine("\t\t\t\tIntent intent = new Intent(context, AktiviteGiris.class);");
                    yaz.WriteLine("\t\t\t\tstartActivity(intent);");
                    yaz.WriteLine("\t\t\t\treturn true;");
                    yaz.WriteLine("\t\t\tcase R.id.menuCikis:");
                    yaz.WriteLine("\t\t\t\talertKapat.show();");
                    yaz.WriteLine("\t\t\t\treturn true;");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\treturn super.onOptionsItemSelected(item);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");

                    yaz.Close();
                }
            }

            //Fragman Giriş
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\FragmanGiris.java", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                {
                    yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ";");
                    yaz.WriteLine("");
                    yaz.WriteLine("import android.content.Context;");
                    yaz.WriteLine("import android.os.Bundle;");
                    yaz.WriteLine("import android.support.v4.app.Fragment;");
                    yaz.WriteLine("import android.view.LayoutInflater;");
                    yaz.WriteLine("import android.view.View;");
                    yaz.WriteLine("import android.view.ViewGroup;");
                    yaz.WriteLine("");
                    yaz.WriteLine("public class FragmanGiris extends Fragment {");
                    yaz.WriteLine("\tprivate Context context;");
                    yaz.WriteLine("\tprivate View view;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {");
                    yaz.WriteLine("\t\tview = inflater.inflate(R.layout.fragman_giris, container, false);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tSayfaHazirla();");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\treturn view;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate void SayfaHazirla() {");
                    yaz.WriteLine("\t\tNesneler();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate void Nesneler() {");
                    yaz.WriteLine("\t\tcontext = getContext();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");

                    yaz.Close();
                }
            }

            /* Araçlar */
            //Araçlar
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Araclar.java", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                {
                    yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Araclar;");
                    yaz.WriteLine("");
                    yaz.WriteLine("import android.content.Context;");
                    yaz.WriteLine("import android.os.Environment;");
                    yaz.WriteLine("");
                    yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                    yaz.WriteLine("import com.sinasalik.thrashead.tdlibrary.TDAraclar;");
                    yaz.WriteLine("");
                    yaz.WriteLine("public class Araclar {");
                    yaz.WriteLine("\tpublic static boolean KlasorleriOlustur(Context context) {");
                    yaz.WriteLine("\t\tString rootPath = (String)TDAraclar.KaynakDegerDon(context, R.string.rootPath, TDAraclar.KaynakTip.String);");
                    yaz.WriteLine("\t\tString projectPath = (String)TDAraclar.KaynakDegerDon(context, R.string.projectPath, TDAraclar.KaynakTip.String);");
                    yaz.WriteLine("\t\tString dbPath = (String)TDAraclar.KaynakDegerDon(context, R.string.dbPath, TDAraclar.KaynakTip.String);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tboolean kontrol = TDAraclar.KlasorOlustur(Environment.getExternalStorageDirectory() + rootPath);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tif(kontrol) {");
                    yaz.WriteLine("\t\t\tkontrol = TDAraclar.KlasorOlustur(Environment.getExternalStorageDirectory() + projectPath);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\t\tif(kontrol) {");
                    yaz.WriteLine("\t\t\t\tkontrol = TDAraclar.KlasorOlustur(Environment.getExternalStorageDirectory() + dbPath);");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\treturn kontrol;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");

                    yaz.Close();
                }
            }

            //Menü
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Menu\\Menu.java", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                {
                    yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Araclar.Menu;");
                    yaz.WriteLine("");
                    yaz.WriteLine("import android.app.Activity;");
                    yaz.WriteLine("import android.support.v4.widget.DrawerLayout;");
                    yaz.WriteLine("import android.support.v7.app.ActionBarDrawerToggle;");
                    yaz.WriteLine("import android.support.v7.app.AppCompatActivity;");
                    yaz.WriteLine("import android.support.v7.widget.Toolbar;");
                    yaz.WriteLine("");
                    yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                    yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".AktiviteGiris;");
                    yaz.WriteLine("import com.sinasalik.thrashead.tdlibrary.TDAraclar;");
                    yaz.WriteLine("import com.sinasalik.thrashead.tdmenu.TDMenu;");
                    yaz.WriteLine("");

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Admin." + Table + "." + Table + "Sayfa;");
                    }

                    yaz.WriteLine("");
                    yaz.WriteLine("import java.util.ArrayList;");
                    yaz.WriteLine("");
                    yaz.WriteLine("public class Menu {");
                    yaz.WriteLine("\tpublic static void MenuHazirla(Activity activity) {");
                    yaz.WriteLine("\t\tMenuOlustur(activity);");
                    yaz.WriteLine("\t\tMenuDoldur(activity);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate static void MenuOlustur(Activity activity) {");
                    yaz.WriteLine("\t\tToolbar toolbar = (Toolbar) activity.findViewById(R.id.toolbar);");
                    yaz.WriteLine("\t\t((AppCompatActivity) activity).setSupportActionBar(toolbar);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tDrawerLayout drawer = (DrawerLayout) activity.findViewById(R.id.drawerLayout);");
                    yaz.WriteLine("\t\tActionBarDrawerToggle toggle = new ActionBarDrawerToggle(activity, drawer, toolbar, 0, 0);");
                    yaz.WriteLine("\t\tdrawer.addDrawerListener(toggle);");
                    yaz.WriteLine("\t\ttoggle.syncState();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate static void MenuDoldur(Activity activity) {");
                    yaz.WriteLine("\t\tTDMenu tdMenuSayfa = (TDMenu) activity.findViewById(R.id.tdMenuSayfa);");
                    yaz.WriteLine("\t\tTDMenu tdMenuAdmin = (TDMenu) activity.findViewById(R.id.tdMenuAdmin);");
                    yaz.WriteLine("\t\tTDMenu tdMenuAlt = (TDMenu) activity.findViewById(R.id.tdMenuAlt);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\ttdMenuSayfa.MenuListe = SayfaMenuList(activity);");
                    yaz.WriteLine("\t\ttdMenuAdmin.MenuListe = AdminMenuList(activity);");
                    yaz.WriteLine("\t\ttdMenuAlt.MenuListe = AltMenuList(activity);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\ttdMenuSayfa.MenuDoldur();");
                    yaz.WriteLine("\t\ttdMenuAdmin.MenuDoldur();");
                    yaz.WriteLine("\t\ttdMenuAlt.MenuDoldur();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate static ArrayList<TDMenu.MenuNesne> SayfaMenuList(Activity activity) {");
                    yaz.WriteLine("\t\tArrayList<TDMenu.MenuNesne> menuList = new ArrayList<>();");
                    yaz.WriteLine("\t\tTDMenu.MenuNesne menu;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tmenu = new TDMenu.MenuNesne();");
                    yaz.WriteLine("\t\tmenu.Baslik = (String) TDAraclar.KaynakDegerDon(activity.getApplicationContext(), R.string.menuAnasayfaBaslik, TDAraclar.KaynakTip.String);");
                    yaz.WriteLine("\t\tmenu.LinkTipi = TDMenu.MenuNesne.LinkTip.Sayfa;");
                    yaz.WriteLine("\t\tmenu.Sinif = AktiviteGiris.class;");
                    yaz.WriteLine("\t\tmenuList.add(menu);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\treturn menuList;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate static ArrayList<TDMenu.MenuNesne> AdminMenuList(Activity activity) {");
                    yaz.WriteLine("\t\tArrayList<TDMenu.MenuNesne> menuList = new ArrayList<>();");
                    yaz.WriteLine("\t\tTDMenu.MenuNesne menu;");
                    yaz.WriteLine("");

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("\t\tmenu = new TDMenu.MenuNesne();");
                        yaz.WriteLine("\t\tmenu.Baslik = \"" + Table + " Listesi\";");
                        yaz.WriteLine("\t\tmenu.LinkTipi = TDMenu.MenuNesne.LinkTip.Sayfa;");
                        yaz.WriteLine("\t\tmenu.Sinif = " + Table + "Sayfa.class;");
                        yaz.WriteLine("\t\tmenuList.add(menu);");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\t\treturn menuList;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate static ArrayList<TDMenu.MenuNesne> AltMenuList(Activity activity) {");
                    yaz.WriteLine("\t\tArrayList<TDMenu.MenuNesne> menuList = new ArrayList<>();");
                    yaz.WriteLine("\t\tTDMenu.MenuNesne menu;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tmenu = new TDMenu.MenuNesne();");
                    yaz.WriteLine("\t\tmenu.Baslik = (String) TDAraclar.KaynakDegerDon(activity.getApplicationContext(), R.string.menuDeveloperBaslik, TDAraclar.KaynakTip.String);");
                    yaz.WriteLine("\t\tmenu.Ekstra = (String) TDAraclar.KaynakDegerDon(activity.getApplicationContext(), R.string.web_developer, TDAraclar.KaynakTip.String);");
                    yaz.WriteLine("\t\tmenu.LinkTipi = TDMenu.MenuNesne.LinkTip.WebSayfasi;");
                    yaz.WriteLine("\t\tmenuList.add(menu);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tmenu = new TDMenu.MenuNesne();");
                    yaz.WriteLine("\t\tmenu.Baslik = (String) TDAraclar.KaynakDegerDon(activity.getApplicationContext(), R.string.menuCikisBaslik, TDAraclar.KaynakTip.String);");
                    yaz.WriteLine("\t\tmenu.LinkTipi = TDMenu.MenuNesne.LinkTip.Cikis;");
                    yaz.WriteLine("\t\tmenu.CikisIkon = R.mipmap.ic_launcher;");
                    yaz.WriteLine("\t\tmenuList.add(menu);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\treturn menuList;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");

                    yaz.Close();
                }
            }

            //Menü Renk
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Menu\\MenuRenk.java", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                {
                    yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Araclar.Menu;");
                    yaz.WriteLine("");
                    yaz.WriteLine("import android.content.Context;");
                    yaz.WriteLine("");
                    yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                    yaz.WriteLine("");
                    yaz.WriteLine("public class MenuRenk {");
                    yaz.WriteLine("\tpublic enum RenkTip {");
                    yaz.WriteLine("\t\tArkaPlan,");
                    yaz.WriteLine("\t\tMetin,");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic static int RenkDon(Context context, RenkTip renkTip, int seviye) {");
                    yaz.WriteLine("\t\tswitch (renkTip) {");
                    yaz.WriteLine("\t\t\tcase ArkaPlan:");
                    yaz.WriteLine("\t\t\t\tswitch (seviye) {");
                    yaz.WriteLine("\t\t\t\t\tcase 1:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye1);");
                    yaz.WriteLine("\t\t\t\t\tcase 2:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye2);");
                    yaz.WriteLine("\t\t\t\t\tcase 3:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye3);");
                    yaz.WriteLine("\t\t\t\t\tcase 4:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye4);");
                    yaz.WriteLine("\t\t\t\t\tcase 5:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye5);");
                    yaz.WriteLine("\t\t\t\t\tcase 6:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye6);");
                    yaz.WriteLine("\t\t\t\t\tcase 7:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye7);");
                    yaz.WriteLine("\t\t\t\t\tcase 8:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye8);");
                    yaz.WriteLine("\t\t\t\t\tcase 9:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye9);");
                    yaz.WriteLine("\t\t\t\t\tcase 10:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye10);");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\tcase Metin:");
                    yaz.WriteLine("\t\t\t\tswitch (seviye) {");
                    yaz.WriteLine("\t\t\t\t\tcase 1:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuTextSeviye1);");
                    yaz.WriteLine("\t\t\t\t\tcase 2:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuTextSeviye2);");
                    yaz.WriteLine("\t\t\t\t\tcase 3:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuTextSeviye3);");
                    yaz.WriteLine("\t\t\t\t\tcase 4:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuTextSeviye4);");
                    yaz.WriteLine("\t\t\t\t\tcase 5:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuTextSeviye5);");
                    yaz.WriteLine("\t\t\t\t\tcase 6:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuTextSeviye6);");
                    yaz.WriteLine("\t\t\t\t\tcase 7:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuTextSeviye7);");
                    yaz.WriteLine("\t\t\t\t\tcase 8:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuTextSeviye8);");
                    yaz.WriteLine("\t\t\t\t\tcase 9:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuTextSeviye9);");
                    yaz.WriteLine("\t\t\t\t\tcase 10:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuTextSeviye10);");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t\tdefault:");
                    yaz.WriteLine("\t\t\t\tswitch (seviye) {");
                    yaz.WriteLine("\t\t\t\t\tcase 1:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye1);");
                    yaz.WriteLine("\t\t\t\t\tcase 2:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye2);");
                    yaz.WriteLine("\t\t\t\t\tcase 3:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye3);");
                    yaz.WriteLine("\t\t\t\t\tcase 4:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye4);");
                    yaz.WriteLine("\t\t\t\t\tcase 5:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye5);");
                    yaz.WriteLine("\t\t\t\t\tcase 6:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye6);");
                    yaz.WriteLine("\t\t\t\t\tcase 7:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye7);");
                    yaz.WriteLine("\t\t\t\t\tcase 8:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye8);");
                    yaz.WriteLine("\t\t\t\t\tcase 9:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye9);");
                    yaz.WriteLine("\t\t\t\t\tcase 10:");
                    yaz.WriteLine("\t\t\t\t\t\treturn context.getResources().getColor(R.color.menuSeviye10);");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t\treturn 0;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");

                    yaz.Close();
                }
            }

            //Adaptor
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Kontroller\\Adaptor\\Adaptor.java", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                {
                    yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Araclar.Kontroller.Adaptor;");
                    yaz.WriteLine("");
                    yaz.WriteLine("import android.content.Context;");
                    yaz.WriteLine("import android.view.LayoutInflater;");
                    yaz.WriteLine("import android.view.View;");
                    yaz.WriteLine("import android.view.ViewGroup;");
                    yaz.WriteLine("import android.widget.BaseAdapter;");
                    yaz.WriteLine("import android.widget.TextView;");
                    yaz.WriteLine("");
                    yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                    yaz.WriteLine("");
                    yaz.WriteLine("import java.util.ArrayList;");
                    yaz.WriteLine("");
                    yaz.WriteLine("public class Adaptor extends BaseAdapter {");
                    yaz.WriteLine("\tprivate ArrayList<Satir> _liste;");
                    yaz.WriteLine("\tprivate Context _context;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tprivate LayoutInflater inflater;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic Adaptor(Context context, ArrayList<Satir> liste) {");
                    yaz.WriteLine("\t\tthis._context = context;");
                    yaz.WriteLine("\t\tthis._liste = liste;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tpublic int getCount() {");
                    yaz.WriteLine("\t\treturn _liste.size();");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tpublic Object getItem(int position) {");
                    yaz.WriteLine("\t\treturn _liste.get(position);");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tpublic long getItemId(int position) {");
                    yaz.WriteLine("\t\treturn 0;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t@Override");
                    yaz.WriteLine("\tpublic View getView(int position, View convertView, ViewGroup parent) {");
                    yaz.WriteLine("\t\tinflater = LayoutInflater.from(_context);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tView satir = inflater.inflate(R.layout.liste_satir, null);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tTextView lblID = (TextView) satir.findViewById(R.id.lblID);");
                    yaz.WriteLine("\t\tTextView lblBaslik = (TextView) satir.findViewById(R.id.lblBaslik);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tSatir listeNesne = _liste.get(position);");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\tlblID.setText(listeNesne.getID());");
                    yaz.WriteLine("\t\tlblBaslik.setText(listeNesne.getBaslik());");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t\treturn satir;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");

                    yaz.Close();
                }
            }

            //Satır
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Araclar\\Kontroller\\Adaptor\\Satir.java", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                {
                    yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Araclar.Kontroller.Adaptor;");
                    yaz.WriteLine("");
                    yaz.WriteLine("public class Satir {");
                    yaz.WriteLine("\tprivate String ID;");
                    yaz.WriteLine("\tprivate String Baslik;");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic String getID() {");
                    yaz.WriteLine("\t\treturn ID;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic String getBaslik() {");
                    yaz.WriteLine("\t\treturn Baslik;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic void setID(String ID) {");
                    yaz.WriteLine("\t\tthis.ID = ID;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("");
                    yaz.WriteLine("\tpublic void setBaslik(String baslik) {");
                    yaz.WriteLine("\t\tBaslik = baslik;");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");

                    yaz.Close();
                }
            }

            foreach (string Table in selectedTables)
            {
                string _table = Table.ToHyperLinkText(true);
                string PrimaryTableName = "";
                string foreignColumnId = "";
                string foreignColumnText = "";

                AndKlasorOlustur(Table);

                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));

                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con, Table);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                List<TableColumnNames> columnNames = tableColumnNames.Where(a => a.TableName == Table).ToList();

                string columnText = GetColumnText(tableColumnNames.Where(a => a.TableName == Table).ToList());

                if (fkcListForeign.Count > 0)
                {
                    foreach (ForeignKeyChecker fkc in fkcListForeign.GroupBy(a => a.PrimaryTableName).Select(a => a.First()).ToList())
                    {
                        PrimaryTableName = fkc.PrimaryTableName;
                        foreignColumnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());
                        List<string> identityColumnsForeign = Helper.Helper.ReturnIdentityColumn(connectionInfo, fkc.PrimaryTableName);
                        foreignColumnId = identityColumnsForeign.Count > 0 ? identityColumnsForeign.FirstOrDefault() : "id";
                    }
                }

                //Sayfa
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin\\" + Table + "\\" + Table + "Sayfa.java", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                    {
                        yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Admin" + "." + Table + ";");
                        yaz.WriteLine("");
                        yaz.WriteLine("import android.app.AlertDialog;");
                        yaz.WriteLine("import android.content.Context;");
                        yaz.WriteLine("import android.content.DialogInterface;");
                        yaz.WriteLine("import android.content.Intent;");

                        if (rdAndWcf.Checked)
                        {
                            yaz.WriteLine("import android.os.StrictMode;");
                        }

                        yaz.WriteLine("import android.support.v4.app.Fragment;");
                        yaz.WriteLine("import android.support.v4.view.GravityCompat;");
                        yaz.WriteLine("import android.support.v4.widget.DrawerLayout;");
                        yaz.WriteLine("import android.support.v7.app.AppCompatActivity;");
                        yaz.WriteLine("import android.os.Bundle;");
                        yaz.WriteLine("import android.view.MenuItem;");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".AktiviteGiris;");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Araclar.Menu.Menu;");
                        yaz.WriteLine("");

                        yaz.WriteLine("public class " + Table + "Sayfa extends AppCompatActivity {");
                        yaz.WriteLine("\tprivate Context context;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate " + Table + "Liste " + _table + "Liste;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate AlertDialog.Builder alertKapat;");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t@Override");
                        yaz.WriteLine("\tprotected void onCreate(Bundle savedInstanceState) {");
                        yaz.WriteLine("\t\tsuper.onCreate(savedInstanceState);");

                        if (rdAndWcf.Checked)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tStrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();");
                            yaz.WriteLine("\t\tStrictMode.setThreadPolicy(policy);");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\t\tsetContentView(R.layout.admin_" + _table + "_sayfa);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tSayfaHazirla();");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate void SayfaHazirla() {");
                        yaz.WriteLine("\t\tNesneler();");
                        yaz.WriteLine("\t\tOlaylar();");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate void Nesneler() {");
                        yaz.WriteLine("\t\tcontext = this;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\talertKapat = new AlertDialog.Builder(context);");
                        yaz.WriteLine("\t\talertKapat.setTitle(R.string.alertKapatBaslik)");
                        yaz.WriteLine("\t\t\t\t.setMessage(R.string.alertKapatMesaj)");
                        yaz.WriteLine("\t\t\t\t.setIcon(R.mipmap.ic_launcher)");
                        yaz.WriteLine("\t\t\t\t.setPositiveButton(R.string.alertKapatTamam, new DialogInterface.OnClickListener() {");
                        yaz.WriteLine("\t\t\t\t\t@Override");
                        yaz.WriteLine("\t\t\t\t\tpublic void onClick(DialogInterface dialog, int which) {");
                        yaz.WriteLine("\t\t\t\t\t\tKapat();");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t})");
                        yaz.WriteLine("\t\t\t\t.setNegativeButton(R.string.alertKapatIptal, null);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t" + _table + "Liste = new " + Table + "Liste();");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate void Olaylar() {");
                        yaz.WriteLine("\t\tMenu.MenuHazirla(this);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tgetSupportFragmentManager().beginTransaction().add(R.id.flyt" + Table + ", " + _table + "Liste, \"" + Table + "Liste\").commit();");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic Fragment AktifFragman() {");
                        yaz.WriteLine("\t\treturn this.getSupportFragmentManager().findFragmentById(R.id.flyt" + Table + ");");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic void Kapat() {");
                        yaz.WriteLine("\t\tthis.finish();");
                        yaz.WriteLine("\t\tIntent intent = new Intent(Intent.ACTION_MAIN);");
                        yaz.WriteLine("\t\tintent.addCategory(Intent.CATEGORY_HOME);");
                        yaz.WriteLine("\t\tintent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);");
                        yaz.WriteLine("\t\tstartActivity(intent);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t@Override");
                        yaz.WriteLine("\tpublic void onBackPressed() {");
                        yaz.WriteLine("\t\tDrawerLayout drawerLayout = (DrawerLayout) findViewById(R.id.drawerLayout);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (drawerLayout.isDrawerOpen(GravityCompat.START)) {");
                        yaz.WriteLine("\t\t\tdrawerLayout.closeDrawer(GravityCompat.START);");
                        yaz.WriteLine("\t\t} else {");
                        yaz.WriteLine("\t\t\tsuper.onBackPressed();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t@Override");
                        yaz.WriteLine("\tpublic boolean onCreateOptionsMenu(android.view.Menu menu) {");
                        yaz.WriteLine("\t\tgetMenuInflater().inflate(R.menu.sagmenu, menu);");
                        yaz.WriteLine("\t\treturn true;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t@Override");
                        yaz.WriteLine("\tpublic boolean onOptionsItemSelected(MenuItem item) {");
                        yaz.WriteLine("\t\tint id = item.getItemId();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tswitch (id) {");
                        yaz.WriteLine("\t\t\tcase R.id.menuAnaSayfa:");
                        yaz.WriteLine("\t\t\t\tIntent intent = new Intent(context, AktiviteGiris.class);");
                        yaz.WriteLine("\t\t\t\tstartActivity(intent);");
                        yaz.WriteLine("\t\t\t\treturn true;");
                        yaz.WriteLine("\t\t\tcase R.id.menuCikis:");
                        yaz.WriteLine("\t\t\t\talertKapat.show();");
                        yaz.WriteLine("\t\t\t\treturn true;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn super.onOptionsItemSelected(item);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }

                //Liste
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin\\" + Table + "\\" + Table + "Liste.java", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                    {
                        yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Admin." + Table + ";");
                        yaz.WriteLine("");
                        yaz.WriteLine("import android.content.Context;");
                        yaz.WriteLine("import android.os.Bundle;");
                        yaz.WriteLine("import android.support.v4.app.Fragment;");
                        yaz.WriteLine("import android.view.LayoutInflater;");
                        yaz.WriteLine("import android.view.View;");
                        yaz.WriteLine("import android.view.ViewGroup;");
                        yaz.WriteLine("import android.widget.AdapterView;");
                        yaz.WriteLine("import android.widget.Button;");
                        yaz.WriteLine("import android.widget.LinearLayout;");
                        yaz.WriteLine("import android.widget.ListView;");
                        yaz.WriteLine("import android.widget.TextView;");
                        yaz.WriteLine("import android.widget.Toast;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Araclar.Kontroller.Adaptor.Adaptor;");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Araclar.Kontroller.Adaptor.Satir;");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".DB.Business." + Table + "Business;");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + Table + ";");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import java.util.ArrayList;");
                        yaz.WriteLine("");
                        yaz.WriteLine("public class " + Table + "Liste extends Fragment {");
                        yaz.WriteLine("\tprivate Context context;");
                        yaz.WriteLine("\tprivate View view;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate Button btnEkle;");
                        yaz.WriteLine("\tprivate ListView lvListe;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate " + Table + "Ekle " + _table + "Ekle;");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\tprivate " + Table + "Detay " + _table + "Detay;");
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate ArrayList<" + Table + "> " + _table + "DataList;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t@Override");
                        yaz.WriteLine("\tpublic View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {");
                        yaz.WriteLine("\t\tview = inflater.inflate(R.layout.admin_" + _table + "_liste, container, false);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tSayfaHazirla();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn view;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate void SayfaHazirla() {");
                        yaz.WriteLine("\t\tNesneler();");
                        yaz.WriteLine("\t\tOlaylar();");
                        yaz.WriteLine("\t\tVeriDoldur();");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate void Nesneler() {");
                        yaz.WriteLine("\t\tcontext = getContext();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tbtnEkle = (Button) view.findViewById(R.id.btnEkle);");
                        yaz.WriteLine("\t\tlvListe = (ListView) view.findViewById(R.id.lvListe);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t" + _table + "Ekle = new " + Table + "Ekle();");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t" + _table + "Detay = new " + Table + "Detay();");
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate void Olaylar() {");
                        yaz.WriteLine("\t\tbtnEkle.setOnClickListener(new View.OnClickListener() {");
                        yaz.WriteLine("\t\t\t@Override");
                        yaz.WriteLine("\t\t\tpublic void onClick(View v) {");
                        yaz.WriteLine("\t\t\t\tgetFragmentManager().beginTransaction().replace(R.id.flyt" + Table + ", " + _table + "Ekle, \"" + Table + "Ekle\").addToBackStack(\"" + Table + "Ekle\").commit();");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate void VeriDoldur() {");
                        yaz.WriteLine("\t\ttry {");
                        yaz.WriteLine("\t\t\tArrayList<Satir> satirList = new ArrayList<>();");
                        yaz.WriteLine("\t\t\tSatir satir;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t" + _table + "DataList = " + Table + "Business.Select(context);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tfor (" + Table + " " + _table + " : " + _table + "DataList) {");
                        yaz.WriteLine("\t\t\t\tsatir = new Satir();");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("\t\t\t\tsatir.setID(String.valueOf(" + _table + ".get" + id + "()));");
                        }
                        else
                        {
                            yaz.WriteLine("\t\t\t\tsatir.setID(\"0\");");
                        }

                        yaz.WriteLine("\t\t\t\tsatir.setBaslik(" + _table + ".get" + columnText + "());");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tsatirList.add(satir);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tlvListe.setAdapter(new Adaptor(context, satirList));");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tlvListe.setOnItemClickListener(new AdapterView.OnItemClickListener() {");
                            yaz.WriteLine("\t\t\t\t@Override");
                            yaz.WriteLine("\t\t\t\tpublic void onItemClick(AdapterView<?> parent, View view, int position, long id) {");
                            yaz.WriteLine("\t\t\t\t\tLinearLayout lv" + Table + " = (LinearLayout) view;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\tString " + _table + "ID = ((TextView) lv" + Table + ".findViewById(R.id.lblID)).getText().toString();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\tBundle bundle = new Bundle();");
                            yaz.WriteLine("\t\t\t\t\tbundle.putString(\"id\", " + _table + "ID);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\t" + _table + "Detay.setArguments(bundle);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\tgetFragmentManager().beginTransaction().replace(R.id.flyt" + Table + ", " + _table + "Detay, \"" + Table + "Detay\").addToBackStack(\"" + Table + "Detay\").commit();");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("\t\t\t});");
                        }

                        yaz.WriteLine("\t\t} catch(Exception e){");
                        yaz.WriteLine("\t\t\te.printStackTrace();");
                        yaz.WriteLine("\t\t\tToast.makeText(context, \"Veri getirilemedi.\", Toast.LENGTH_SHORT).show();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }

                //Ekle
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin\\" + Table + "\\" + Table + "Ekle.java", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                    {
                        yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Admin" + "." + Table + ";");
                        yaz.WriteLine("");
                        yaz.WriteLine("import android.content.Context;");
                        yaz.WriteLine("import android.os.Bundle;");
                        yaz.WriteLine("import android.support.v4.app.Fragment;");
                        yaz.WriteLine("import android.view.LayoutInflater;");
                        yaz.WriteLine("import android.view.View;");
                        yaz.WriteLine("import android.view.ViewGroup;");

                        if (!String.IsNullOrEmpty(PrimaryTableName))
                        {
                            yaz.WriteLine("import android.widget.ArrayAdapter;");
                        }

                        yaz.WriteLine("import android.widget.Button;");

                        bool boolEditText = false, boolCheckBox = false, boolSpinner = false;

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                if (column.ColumnName != id)
                                {
                                    if (foreLst.Count > 0)
                                    {
                                        if (!boolSpinner)
                                        {
                                            yaz.WriteLine("import android.widget.Spinner;");
                                            boolSpinner = true;
                                        }
                                    }
                                    else
                                    {
                                        if (column.TypeName.Name == "Boolean")
                                        {
                                            if (!boolCheckBox)
                                            {
                                                yaz.WriteLine("import android.widget.CheckBox;");
                                                boolCheckBox = true;
                                            }
                                        }
                                        else
                                        {
                                            if (!boolEditText)
                                            {
                                                yaz.WriteLine("import android.widget.EditText;");
                                                boolEditText = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        yaz.WriteLine("import android.widget.Toast;");

                        yaz.WriteLine("");


                        foreignTables = new List<string>();

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                if (foreLst.Count > 0)
                                {
                                    PrimaryTableName = foreLst.FirstOrDefault().PrimaryTableName;

                                    if (!foreignTables.Contains(PrimaryTableName))
                                    {
                                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".DB.Business." + PrimaryTableName + "Business;");
                                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + PrimaryTableName + ";");
                                    }

                                    foreignTables.Add(PrimaryTableName);
                                }
                            }
                        }

                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".DB.Business." + Table + "Business;");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + Table + ";");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                        yaz.WriteLine("");

                        if (!String.IsNullOrEmpty(PrimaryTableName))
                        {
                            yaz.WriteLine("import java.util.ArrayList;");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("public class " + Table + "Ekle extends Fragment");
                        yaz.WriteLine("{");
                        yaz.WriteLine("\tprivate Context context;");
                        yaz.WriteLine("\tprivate View view;");
                        yaz.WriteLine("");

                        string lblEditText = "";
                        string lblCheckBox = "";
                        string lblSpinner = "";

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                if (column.ColumnName != id)
                                {
                                    if (foreLst.Count > 0)
                                    {
                                        lblSpinner += "spin" + column.ColumnName + ", ";
                                    }
                                    else
                                    {
                                        if (column.TypeName.Name == "Boolean")
                                        {
                                            lblCheckBox += "chk" + column.ColumnName + ", ";
                                        }
                                        else
                                        {
                                            lblEditText += "txt" + column.ColumnName + ", ";
                                        }
                                    }
                                }
                            }
                        }

                        lblEditText = lblEditText.Trim().TrimEnd(',');
                        lblCheckBox = lblCheckBox.Trim().TrimEnd(',');
                        lblSpinner = lblSpinner.Trim().TrimEnd(',');

                        if (!String.IsNullOrEmpty(lblEditText))
                        {
                            yaz.WriteLine("\tprivate EditText " + lblEditText + ";");
                        }

                        if (!String.IsNullOrEmpty(lblCheckBox))
                        {
                            yaz.WriteLine("\tprivate CheckBox " + lblCheckBox + ";");
                        }

                        if (!String.IsNullOrEmpty(lblSpinner))
                        {
                            yaz.WriteLine("\tprivate Spinner " + lblSpinner + ";");
                        }

                        yaz.WriteLine("\tprivate Button btnKaydet, btnIptal;");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tprivate " + Table + "Liste " + _table + "Liste;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate " + Table + " " + _table + "Data;");
                        yaz.WriteLine("");

                        yaz.WriteLine("\t@Override");
                        yaz.WriteLine("\tpublic View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {");
                        yaz.WriteLine("\t\tview = inflater.inflate(R.layout.admin_" + _table + "_ekle, container, false);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tSayfaHazirla();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn view;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tprivate void SayfaHazirla() {");
                        yaz.WriteLine("\t\tNesneler();");
                        yaz.WriteLine("\t\tOlaylar();");

                        if (!String.IsNullOrEmpty(PrimaryTableName))
                        {
                            yaz.WriteLine("\t\tVeriDoldur();");
                        }

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tprivate void Nesneler() {");
                        yaz.WriteLine("\t\tcontext = getContext();");
                        yaz.WriteLine("");

                        if (columnNames.Count > 0)
                        {
                            foreach (string item in lblEditText.Split(','))
                            {
                                if (!String.IsNullOrEmpty(item))
                                {
                                    yaz.WriteLine("\t\t" + item.Trim() + " = (EditText)view.findViewById(R.id." + item.Trim() + ");");
                                }
                            }

                            foreach (string item in lblCheckBox.Split(','))
                            {
                                if (!String.IsNullOrEmpty(item))
                                {
                                    yaz.WriteLine("\t\t" + item.Trim() + " = (CheckBox)view.findViewById(R.id." + item.Trim() + ");");
                                }
                            }

                            foreach (string item in lblSpinner.Split(','))
                            {
                                if (!String.IsNullOrEmpty(item))
                                {
                                    yaz.WriteLine("\t\t" + item.Trim() + " = (Spinner)view.findViewById(R.id." + item.Trim() + ");");
                                }
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tbtnKaydet = (Button)view.findViewById(R.id.btnKaydet);");
                        yaz.WriteLine("\t\tbtnIptal = (Button)view.findViewById(R.id.btnIptal);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t" + _table + "Liste = new " + Table + "Liste();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t" + _table + "Data = new " + Table + "();");

                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");

                        yaz.WriteLine("\tprivate void Olaylar() {");
                        yaz.WriteLine("\t\tbtnKaydet.setOnClickListener(new View.OnClickListener() {");
                        yaz.WriteLine("\t\t\t@Override");
                        yaz.WriteLine("\t\t\tpublic void onClick(View v) {");
                        yaz.WriteLine("\t\t\t\ttry {");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                if (column.ColumnName != id)
                                {
                                    if (foreLst.Count > 0)
                                    {
                                        yaz.WriteLine("\t\t\t\t\tString " + column.ColumnName + " = spin" + column.ColumnName + ".getSelectedItem().toString().split(\"\\\\(\")[1].replace(\")\", \"\");");

                                        if (foreLst.First().PrimaryColumnType == "String")
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(" + column.ColumnName + ");");
                                        }
                                        else if (foreLst.First().PrimaryColumnType == "Int16" || foreLst.First().PrimaryColumnType == "Int32")
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Integer.valueOf(" + column.ColumnName + "));");
                                        }
                                        else if (foreLst.First().PrimaryColumnType == "Int64")
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Long.valueOf(" + column.ColumnName + "));");
                                        }
                                        else if (foreLst.First().PrimaryColumnType == "Decimal" || foreLst.First().PrimaryColumnType == "Double" || foreLst.First().PrimaryColumnType == "Single")
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Float.valueOf(" + column.ColumnName + "));");
                                        }
                                        else if (foreLst.First().PrimaryColumnType == "Byte")
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Byte.valueOf(" + column.ColumnName + "));");
                                        }
                                        else
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(String.valueOf(" + column.ColumnName + "));");
                                        }
                                    }
                                    else
                                    {
                                        if (column.TypeName.Name == "Boolean")
                                        {
                                            yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(chk" + column.ColumnName + ".isChecked());");
                                        }
                                        else
                                        {
                                            if (column.TypeName.Name == "String")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(txt" + column.ColumnName + ".getText().toString());");
                                            }
                                            else if (column.TypeName.Name == "Int16" || column.TypeName.Name == "Int32")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Integer.valueOf(txt" + column.ColumnName + ".getText().toString()));");
                                            }
                                            else if (column.TypeName.Name == "Int64")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Long.valueOf(txt" + column.ColumnName + ".getText().toString()));");
                                            }
                                            else if (column.TypeName.Name == "Decimal" || column.TypeName.Name == "Double" || column.TypeName.Name == "Single")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Float.valueOf(txt" + column.ColumnName + ".getText().toString()));");
                                            }
                                            else if (column.TypeName.Name == "Byte")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Byte.valueOf(txt" + column.ColumnName + ".getText().toString()));");
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(String.valueOf(txt" + column.ColumnName + ".getText().toString()));");
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        yaz.WriteLine("");

                        yaz.WriteLine("\t\t\t\t\tBoolean sonuc = " + Table + "Business.Insert(context, " + _table + "Data);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\tif (sonuc) {");
                        yaz.WriteLine("\t\t\t\t\t\tToast.makeText(context, \"Veri kaydedildi.\", Toast.LENGTH_SHORT).show();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t\t\tListeyeDon();");
                        yaz.WriteLine("\t\t\t\t\t} else {");
                        yaz.WriteLine("\t\t\t\t\t\tToast.makeText(context, \"Veri kaydedilemedi.\", Toast.LENGTH_SHORT).show();");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t} catch (Exception e) {");
                        yaz.WriteLine("\t\t\t\t\te.printStackTrace();");
                        yaz.WriteLine("\t\t\t\t\tToast.makeText(context, \"Veri kaydedilemedi.\", Toast.LENGTH_SHORT).show();");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tbtnIptal.setOnClickListener(new View.OnClickListener() {");
                        yaz.WriteLine("\t\t\t@Override");
                        yaz.WriteLine("\t\t\tpublic void onClick(View v) {");
                        yaz.WriteLine("\t\t\t\tListeyeDon();");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t});");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");

                        int i = 0;
                        PrimaryTableName = "";

                        foreach (TableColumnNames column in columnNames)
                        {
                            string num = "";

                            if (column.TypeName != null)
                            {
                                List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).GroupBy(a => a.PrimaryTableName).Select(a => a.FirstOrDefault()).ToList();

                                if (foreLst.Count > 0)
                                {
                                    PrimaryTableName = foreLst.FirstOrDefault().PrimaryTableName;
                                    foreignColumnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());
                                    List<string> identityColumnsForeign = Helper.Helper.ReturnIdentityColumn(connectionInfo, PrimaryTableName);
                                    foreignColumnId = identityColumnsForeign.Count > 0 ? identityColumnsForeign.FirstOrDefault() : "id";

                                    if (i == 0)
                                    {
                                        yaz.WriteLine("\tprivate void VeriDoldur() {");
                                        yaz.WriteLine("\t\ttry {");
                                    }
                                    else
                                    {
                                        num = (i + 1).ToString();
                                    }

                                    yaz.WriteLine("\t\t\tArrayList<" + PrimaryTableName + "> " + PrimaryTableName + "DataList" + num + " = " + PrimaryTableName + "Business.Select(context);");
                                    yaz.WriteLine("\t\t\tArrayList<String> " + PrimaryTableName + "Dizi" + num + " = new ArrayList<>();");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\tfor (" + PrimaryTableName + " data : " + PrimaryTableName + "DataList" + num + ") {");
                                    yaz.WriteLine("\t\t\t\t" + PrimaryTableName + "Dizi" + num + ".add(data.get" + foreignColumnText + "() + \" (\" + data.get" + foreignColumnId + "() + \")\");");
                                    yaz.WriteLine("\t\t\t}");
                                    yaz.WriteLine("");
                                    yaz.WriteLine("\t\t\tspin" + column.ColumnName + ".setAdapter(new ArrayAdapter(context, android.R.layout.simple_list_item_1, " + PrimaryTableName + "Dizi" + num + "));");
                                    yaz.WriteLine("");

                                    i++;
                                }
                            }
                        }

                        if (!String.IsNullOrEmpty(PrimaryTableName))
                        {
                            yaz.WriteLine("\t\t} catch (Exception e) {");
                            yaz.WriteLine("\t\t\te.printStackTrace();");
                            yaz.WriteLine("\t\t\tToast.makeText(context, \"Veri getirilemedi.\", Toast.LENGTH_SHORT).show();");
                            yaz.WriteLine("\t\t\tListeyeDon();");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\tprivate void ListeyeDon() {");
                        yaz.WriteLine("\t\tgetFragmentManager().beginTransaction().replace(R.id.flyt" + Table + ", " + _table + "Liste, \"" + Table + "Liste\").addToBackStack(\"" + Table + "Liste\").commit();");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }

                if (identityColumns.Count > 0)
                {
                    //Detay
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin\\" + Table + "\\" + Table + "Detay.java", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                        {
                            yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Admin." + Table + ";");
                            yaz.WriteLine("");
                            yaz.WriteLine("import android.app.AlertDialog;");
                            yaz.WriteLine("import android.content.Context;");
                            yaz.WriteLine("import android.content.DialogInterface;");
                            yaz.WriteLine("import android.os.Bundle;");
                            yaz.WriteLine("import android.support.v4.app.Fragment;");
                            yaz.WriteLine("import android.view.LayoutInflater;");
                            yaz.WriteLine("import android.view.View;");
                            yaz.WriteLine("import android.view.ViewGroup;");
                            yaz.WriteLine("import android.widget.Button;");

                            bool boolTextView = false, boolCheckBox = false;

                            foreach (TableColumnNames column in columnNames)
                            {
                                List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                if (column.TypeName.Name == "Boolean")
                                {
                                    if (!boolCheckBox)
                                    {
                                        yaz.WriteLine("import android.widget.CheckBox;");
                                        boolCheckBox = true;
                                    }
                                }
                                else
                                {
                                    if (!boolTextView)
                                    {
                                        yaz.WriteLine("import android.widget.TextView;");
                                        boolTextView = true;
                                    }
                                }
                            }

                            yaz.WriteLine("import android.widget.Toast;");
                            yaz.WriteLine("");

                            foreignTables = new List<string>();

                            foreach (TableColumnNames column in columnNames)
                            {
                                if (column.TypeName != null)
                                {
                                    List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    if (foreLst.Count > 0)
                                    {
                                        PrimaryTableName = foreLst.FirstOrDefault().PrimaryTableName;

                                        if (!foreignTables.Contains(PrimaryTableName))
                                        {
                                            yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".DB.Business." + PrimaryTableName + "Business;");
                                            yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + PrimaryTableName + ";");
                                        }

                                        foreignTables.Add(PrimaryTableName);
                                    }
                                }
                            }

                            yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".DB.Business." + Table + "Business;");
                            yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + Table + ";");
                            yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                            yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Where;");
                            yaz.WriteLine("");
                            yaz.WriteLine("import java.util.ArrayList;");
                            yaz.WriteLine("");
                            yaz.WriteLine("public class " + Table + "Detay extends Fragment {");
                            yaz.WriteLine("\tprivate Context context;");
                            yaz.WriteLine("\tprivate View view;");
                            yaz.WriteLine("\tprivate Bundle bundle;");
                            yaz.WriteLine("\tprivate ArrayList<String> listValues;");
                            yaz.WriteLine("");

                            string lblCheckBox = "";
                            string lblTextView = "";

                            foreach (TableColumnNames column in columnNames)
                            {
                                if (column.TypeName != null)
                                {
                                    List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();


                                    if (column.TypeName.Name == "Boolean")
                                    {
                                        lblCheckBox += "chk" + column.ColumnName + ", ";
                                    }
                                    else
                                    {
                                        lblTextView += "lbl" + column.ColumnName + ", ";
                                    }
                                }
                            }

                            lblTextView = lblTextView.Trim().TrimEnd(',');
                            lblCheckBox = lblCheckBox.Trim().TrimEnd(',');

                            if (!String.IsNullOrEmpty(lblTextView))
                            {
                                yaz.WriteLine("\tprivate TextView " + lblTextView + ";");
                            }

                            if (!String.IsNullOrEmpty(lblCheckBox))
                            {
                                yaz.WriteLine("\tprivate CheckBox " + lblCheckBox + ";");
                            }

                            yaz.WriteLine("\tprivate Button btnListe, btnDuzenle, btnSil;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate " + Table + "Liste " + _table + "Liste;");
                            yaz.WriteLine("\tprivate " + Table + "Duzenle " + _table + "Duzenle;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate " + Table + " " + _table + "Data;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate Where where;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t@Override");
                            yaz.WriteLine("\tpublic View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {");
                            yaz.WriteLine("\t\tview = inflater.inflate(R.layout.admin_" + _table + "_detay, container, false);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tSayfaHazirla();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\treturn view;");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate void SayfaHazirla() {");
                            yaz.WriteLine("\t\tNesneler();");
                            yaz.WriteLine("\t\tOlaylar();");
                            yaz.WriteLine("\t\tVeriDoldur();");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate void Nesneler() {");
                            yaz.WriteLine("\t\tcontext = getContext();");
                            yaz.WriteLine("\t\tbundle = getArguments();");
                            yaz.WriteLine("");

                            if (columnNames.Count > 0)
                            {
                                foreach (string item in lblTextView.Split(','))
                                {
                                    if (!String.IsNullOrEmpty(item))
                                    {
                                        yaz.WriteLine("\t\t" + item.Trim() + " = (TextView)view.findViewById(R.id." + item.Trim() + ");");
                                    }
                                }

                                foreach (string item in lblCheckBox.Split(','))
                                {
                                    if (!String.IsNullOrEmpty(item))
                                    {
                                        yaz.WriteLine("\t\t" + item.Trim() + " = (CheckBox)view.findViewById(R.id." + item.Trim() + ");");
                                    }
                                }
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tbtnListe = (Button) view.findViewById(R.id.btnListe);");
                            yaz.WriteLine("\t\tbtnDuzenle = (Button) view.findViewById(R.id.btnDuzenle);");
                            yaz.WriteLine("\t\tbtnSil = (Button) view.findViewById(R.id.btnSil);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t" + _table + "Liste = new " + Table + "Liste();");
                            yaz.WriteLine("\t\t" + _table + "Duzenle = new " + Table + "Duzenle();");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate void Olaylar() {");
                            yaz.WriteLine("\t\tbtnListe.setOnClickListener(new View.OnClickListener() {");
                            yaz.WriteLine("\t\t\t@Override");
                            yaz.WriteLine("\t\t\tpublic void onClick(View v) {");
                            yaz.WriteLine("\t\t\t\tListeyeDon();");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t});");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tbtnDuzenle.setOnClickListener(new View.OnClickListener() {");
                            yaz.WriteLine("\t\t\t@Override");
                            yaz.WriteLine("\t\t\tpublic void onClick(View v) {");
                            yaz.WriteLine("\t\t\t\t" + _table + "Duzenle.setArguments(bundle);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\tgetFragmentManager().beginTransaction().replace(R.id.flyt" + Table + ", " + _table + "Duzenle, \"" + Table + "Duzenle\").addToBackStack(\"" + Table + "Duzenle\").commit();");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t});");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tbtnSil.setOnClickListener(new View.OnClickListener() {");
                            yaz.WriteLine("\t\t\t@Override");
                            yaz.WriteLine("\t\t\tpublic void onClick(View v) {");
                            yaz.WriteLine("\t\t\t\ttry {");
                            yaz.WriteLine("\t\t\t\t\tlistValues = new ArrayList<>();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\twhere = new Where();");
                            yaz.WriteLine("\t\t\t\t\twhere.setColumn(\"" + id + "\");");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\tlistValues.add(bundle.getString(\"id\"));");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\twhere.setValues(listValues);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\tAlertDialog.Builder alertSil = new AlertDialog.Builder(context);");
                            yaz.WriteLine("\t\t\t\t\talertSil.setTitle(R.string.alertSilBaslik)");
                            yaz.WriteLine("\t\t\t\t\t\t\t.setMessage(R.string.alertSilMesaj)");
                            yaz.WriteLine("\t\t\t\t\t\t\t.setIcon(R.mipmap.ic_launcher)");
                            yaz.WriteLine("\t\t\t\t\t\t\t.setPositiveButton(R.string.alertKapatTamam, new DialogInterface.OnClickListener() {");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t@Override");
                            yaz.WriteLine("\t\t\t\t\t\t\t\tpublic void onClick(DialogInterface dialog, int which) {");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\tBoolean sonuc = " + Table + "Business.Delete(context, where);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\tif (sonuc) {");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\tToast.makeText(context, \"Veri silindi.\", Toast.LENGTH_SHORT).show();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\tListeyeDon();");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t} else {");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t\tToast.makeText(context, \"Veri silinemedi.\", Toast.LENGTH_SHORT).show();");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t\t}");
                            yaz.WriteLine("\t\t\t\t\t\t\t\t}");
                            yaz.WriteLine("\t\t\t\t\t\t\t})");
                            yaz.WriteLine("\t\t\t\t\t\t\t.setNegativeButton(R.string.alertKapatIptal, null)");
                            yaz.WriteLine("\t\t\t\t\t\t\t.show();");
                            yaz.WriteLine("\t\t\t\t} catch (Exception e) {");
                            yaz.WriteLine("\t\t\t\t\te.printStackTrace();");
                            yaz.WriteLine("\t\t\t\t\tToast.makeText(context, \"Veri silinemedi.\", Toast.LENGTH_SHORT).show();");
                            yaz.WriteLine("\t\t\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t});");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate void VeriDoldur() {");
                            yaz.WriteLine("\t\ttry {");
                            yaz.WriteLine("\t\t\tlistValues = new ArrayList<>();");
                            yaz.WriteLine("\t\t\t");
                            yaz.WriteLine("\t\t\twhere = new Where();");
                            yaz.WriteLine("\t\t\twhere.setColumn(\"" + id + "\");");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tlistValues.add(bundle.getString(\"id\"));");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\twhere.setValues(listValues);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t" + _table + "Data = " + Table + "Business.SelectSingle(context, where);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (" + _table + "Data != null) {");


                            int i = 1;

                            foreach (TableColumnNames column in columnNames)
                            {
                                if (column.TypeName != null)
                                {
                                    List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).GroupBy(a => a.PrimaryTableName).Select(a => a.FirstOrDefault()).ToList();

                                    if (column.ColumnName != id)
                                    {
                                        if (foreLst.Count > 0)
                                        {
                                            PrimaryTableName = foreLst.FirstOrDefault().PrimaryTableName;
                                            foreignColumnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());
                                            List<string> identityColumnsForeign = Helper.Helper.ReturnIdentityColumn(connectionInfo, PrimaryTableName);
                                            foreignColumnId = identityColumnsForeign.Count > 0 ? identityColumnsForeign.FirstOrDefault() : "id";

                                            string ptNum = i > 1 ? i.ToString() : "";

                                            yaz.WriteLine("\t\t\t\tif (" + _table + "Data.get" + column.ColumnName + "() != null)");
                                            yaz.WriteLine("\t\t\t\t{");
                                            yaz.WriteLine("\t\t\t\tlistValues = new ArrayList<>();");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\twhere = new Where();");
                                            yaz.WriteLine("\t\t\t\twhere.setColumn(\"" + foreignColumnId + "\");");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\tlistValues.add(" + _table + "Data.get" + column.ColumnName + "().toString());");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\twhere.setValues(listValues);");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\t" + PrimaryTableName + " " + PrimaryTableName + "Data" + ptNum + " = " + PrimaryTableName + "Business.SelectSingle(context, where);");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\tif (" + PrimaryTableName + "Data" + ptNum + " != null) {");
                                            yaz.WriteLine("\t\t\t\t\t\tlbl" + column.ColumnName + ".setText(" + PrimaryTableName + "Data" + ptNum + ".get" + foreignColumnText + "().toString());");
                                            yaz.WriteLine("\t\t\t\t\t} else {");
                                            yaz.WriteLine("\t\t\t\t\t\tlbl" + column.ColumnName + ".setText(" + _table + "Data.get" + column.ColumnName + "().toString());");
                                            yaz.WriteLine("\t\t\t\t\t}");
                                            yaz.WriteLine("\t\t\t\t}");
                                            yaz.WriteLine("");

                                            i++;
                                        }
                                        else
                                        {
                                            if (column.TypeName.Name == "Boolean")
                                            {
                                                yaz.WriteLine("\t\t\t\tif (" + _table + "Data.get" + column.ColumnName + "() != null)");
                                                yaz.WriteLine("\t\t\t\t{");
                                                yaz.WriteLine("\t\t\t\t\tchk" + column.ColumnName + ".setChecked(" + _table + "Data.get" + column.ColumnName + "());");
                                                yaz.WriteLine("\t\t\t\t}");
                                                yaz.WriteLine("");
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t\tif (" + _table + "Data.get" + column.ColumnName + "() != null)");
                                                yaz.WriteLine("\t\t\t\t{");
                                                yaz.WriteLine("\t\t\t\t\tlbl" + column.ColumnName + ".setText(" + _table + "Data.get" + column.ColumnName + "().toString());");
                                                yaz.WriteLine("\t\t\t\t}");
                                                yaz.WriteLine("");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        yaz.WriteLine("\t\t\t\tif (" + _table + "Data.get" + column.ColumnName + "() != null)");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\t\t\tlblID.setText(" + _table + "Data.get" + column.ColumnName + "().toString());");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t} catch (Exception e) {");
                            yaz.WriteLine("\t\t\te.printStackTrace();");
                            yaz.WriteLine("\t\t\tToast.makeText(context, \"Veri getirilemedi.\", Toast.LENGTH_SHORT).show();");
                            yaz.WriteLine("\t\t\tListeyeDon();");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate void ListeyeDon() {");
                            yaz.WriteLine("\t\tgetFragmentManager().beginTransaction().replace(R.id.flyt" + Table + ", " + _table + "Liste, \"" + Table + "Liste\").addToBackStack(\"" + Table + "Liste\").commit();");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("}");

                            yaz.Close();
                        }
                    }

                    //Düzenle
                    using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Admin\\" + Table + "\\" + Table + "Duzenle.java", FileMode.Create))
                    {
                        using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                        {
                            yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Admin" + "." + Table + ";");
                            yaz.WriteLine("");
                            yaz.WriteLine("import android.content.Context;");
                            yaz.WriteLine("import android.os.Bundle;");
                            yaz.WriteLine("import android.support.v4.app.Fragment;");
                            yaz.WriteLine("import android.view.LayoutInflater;");
                            yaz.WriteLine("import android.view.View;");
                            yaz.WriteLine("import android.view.ViewGroup;");

                            if (!String.IsNullOrEmpty(PrimaryTableName))
                            {
                                yaz.WriteLine("import android.widget.ArrayAdapter;");
                            }

                            yaz.WriteLine("import android.widget.Button;");

                            bool boolEditText = false, boolCheckBox = false, boolSpinner = false;

                            foreach (TableColumnNames column in columnNames)
                            {
                                if (column.TypeName != null)
                                {
                                    List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    if (column.ColumnName != id)
                                    {
                                        if (foreLst.Count > 0)
                                        {
                                            if (!boolSpinner)
                                            {
                                                yaz.WriteLine("import android.widget.Spinner;");
                                                boolSpinner = true;
                                            }
                                        }
                                        else
                                        {
                                            if (column.TypeName.Name == "Boolean")
                                            {
                                                if (!boolCheckBox)
                                                {
                                                    yaz.WriteLine("import android.widget.CheckBox;");
                                                    boolCheckBox = true;
                                                }
                                            }
                                            else
                                            {
                                                if (!boolEditText)
                                                {
                                                    yaz.WriteLine("import android.widget.EditText;");
                                                    boolEditText = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        yaz.WriteLine("import android.widget.TextView;");
                                    }
                                }
                            }
                            yaz.WriteLine("import android.widget.Toast;");

                            yaz.WriteLine("");


                            foreignTables = new List<string>();

                            foreach (TableColumnNames column in columnNames)
                            {
                                if (column.TypeName != null)
                                {
                                    List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    if (foreLst.Count > 0)
                                    {
                                        PrimaryTableName = foreLst.FirstOrDefault().PrimaryTableName;

                                        if (!foreignTables.Contains(PrimaryTableName))
                                        {
                                            yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".DB.Business." + PrimaryTableName + "Business;");
                                            yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + PrimaryTableName + ";");
                                        }

                                        foreignTables.Add(PrimaryTableName);
                                    }
                                }
                            }

                            yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".DB.Business." + Table + "Business;");
                            yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + Table + ";");
                            yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                            yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Where;");
                            yaz.WriteLine("");
                            yaz.WriteLine("import java.util.ArrayList;");
                            yaz.WriteLine("");

                            yaz.WriteLine("public class " + Table + "Duzenle extends Fragment");
                            yaz.WriteLine("{");
                            yaz.WriteLine("\tprivate Context context;");
                            yaz.WriteLine("\tprivate View view;");
                            yaz.WriteLine("\tprivate Bundle bundle;");
                            yaz.WriteLine("\tprivate ArrayList<String> listValues;");
                            yaz.WriteLine("");

                            string lblEditText = "";
                            string lblCheckBox = "";
                            string lblSpinner = "";
                            string lblTextView = "";

                            foreach (TableColumnNames column in columnNames)
                            {
                                if (column.TypeName != null)
                                {
                                    List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    if (column.ColumnName != id)
                                    {
                                        if (foreLst.Count > 0)
                                        {
                                            lblSpinner += "spin" + column.ColumnName + ", ";
                                        }
                                        else
                                        {
                                            if (column.TypeName.Name == "Boolean")
                                            {
                                                lblCheckBox += "chk" + column.ColumnName + ", ";
                                            }
                                            else
                                            {
                                                lblEditText += "txt" + column.ColumnName + ", ";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblTextView += "lbl" + column.ColumnName + ", ";
                                    }
                                }
                            }

                            lblEditText = lblEditText.Trim().TrimEnd(',');
                            lblCheckBox = lblCheckBox.Trim().TrimEnd(',');
                            lblSpinner = lblSpinner.Trim().TrimEnd(',');
                            lblTextView = lblTextView.Trim().TrimEnd(',');

                            if (!String.IsNullOrEmpty(lblEditText))
                            {
                                yaz.WriteLine("\tprivate EditText " + lblEditText + ";");
                            }

                            if (!String.IsNullOrEmpty(lblCheckBox))
                            {
                                yaz.WriteLine("\tprivate CheckBox " + lblCheckBox + ";");
                            }

                            if (!String.IsNullOrEmpty(lblSpinner))
                            {
                                yaz.WriteLine("\tprivate Spinner " + lblSpinner + ";");
                            }

                            if (!String.IsNullOrEmpty(lblTextView))
                            {
                                yaz.WriteLine("\tprivate TextView " + lblTextView + ";");
                            }

                            yaz.WriteLine("\tprivate Button btnKaydet, btnIptal;");
                            yaz.WriteLine("");

                            yaz.WriteLine("\tprivate " + Table + "Liste " + _table + "Liste;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate " + Table + " " + _table + "Data;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate Where where;");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t@Override");
                            yaz.WriteLine("\tpublic View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {");
                            yaz.WriteLine("\t\tview = inflater.inflate(R.layout.admin_" + _table + "_duzenle, container, false);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tSayfaHazirla();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\treturn view;");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\tprivate void SayfaHazirla() {");
                            yaz.WriteLine("\t\tNesneler();");
                            yaz.WriteLine("\t\tOlaylar();");
                            yaz.WriteLine("\t\tVeriDoldur();");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\tprivate void Nesneler() {");
                            yaz.WriteLine("\t\tcontext = getContext();");
                            yaz.WriteLine("\t\tbundle = getArguments();");
                            yaz.WriteLine("");

                            if (columnNames.Count > 0)
                            {
                                foreach (string item in lblEditText.Split(','))
                                {
                                    if (!String.IsNullOrEmpty(item))
                                    {
                                        yaz.WriteLine("\t\t" + item.Trim() + " = (EditText)view.findViewById(R.id." + item.Trim() + ");");
                                    }
                                }

                                foreach (string item in lblCheckBox.Split(','))
                                {
                                    if (!String.IsNullOrEmpty(item))
                                    {
                                        yaz.WriteLine("\t\t" + item.Trim() + " = (CheckBox)view.findViewById(R.id." + item.Trim() + ");");
                                    }
                                }

                                foreach (string item in lblSpinner.Split(','))
                                {
                                    if (!String.IsNullOrEmpty(item))
                                    {
                                        yaz.WriteLine("\t\t" + item.Trim() + " = (Spinner)view.findViewById(R.id." + item.Trim() + ");");
                                    }
                                }

                                foreach (string item in lblTextView.Split(','))
                                {
                                    if (!String.IsNullOrEmpty(item))
                                    {
                                        yaz.WriteLine("\t\t" + item.Trim() + " = (TextView)view.findViewById(R.id." + item.Trim() + ");");
                                    }
                                }
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tbtnKaydet = (Button)view.findViewById(R.id.btnKaydet);");
                            yaz.WriteLine("\t\tbtnIptal = (Button)view.findViewById(R.id.btnIptal);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t" + _table + "Liste = new " + Table + "Liste();");
                            yaz.WriteLine("\t\t" + _table + "Data = new " + Table + "();");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\tprivate void Olaylar() {");
                            yaz.WriteLine("\t\tbtnKaydet.setOnClickListener(new View.OnClickListener() {");
                            yaz.WriteLine("\t\t\t@Override");
                            yaz.WriteLine("\t\t\tpublic void onClick(View v) {");
                            yaz.WriteLine("\t\t\t\ttry {");
                            yaz.WriteLine("\t\t\t\t\tlistValues = new ArrayList<>();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\twhere = new Where();");
                            yaz.WriteLine("\t\t\t\t\twhere.setColumn(\"" + id + "\");");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\tlistValues.add(bundle.getString(\"id\"));");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\twhere.setValues(listValues);");
                            yaz.WriteLine("");

                            foreach (TableColumnNames column in columnNames)
                            {
                                if (column.TypeName != null)
                                {
                                    List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).ToList();

                                    if (column.ColumnName != id)
                                    {
                                        if (foreLst.Count > 0)
                                        {
                                            yaz.WriteLine("\t\t\t\t\tString " + column.ColumnName + " = spin" + column.ColumnName + ".getSelectedItem().toString().split(\"\\\\(\")[1].replace(\")\", \"\");");

                                            if (foreLst.First().PrimaryColumnType == "String")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(" + column.ColumnName + ");");
                                            }
                                            else if (foreLst.First().PrimaryColumnType == "Int16" || foreLst.First().PrimaryColumnType == "Int32")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Integer.valueOf(" + column.ColumnName + "));");
                                            }
                                            else if (foreLst.First().PrimaryColumnType == "Int64")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Long.valueOf(" + column.ColumnName + "));");
                                            }
                                            else if (foreLst.First().PrimaryColumnType == "Decimal" || foreLst.First().PrimaryColumnType == "Double" || foreLst.First().PrimaryColumnType == "Single")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Float.valueOf(" + column.ColumnName + "));");
                                            }
                                            else if (foreLst.First().PrimaryColumnType == "Byte")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Byte.valueOf(" + column.ColumnName + "));");
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(String.valueOf(" + column.ColumnName + "));");
                                            }
                                        }
                                        else
                                        {
                                            if (column.TypeName.Name == "Boolean")
                                            {
                                                yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(chk" + column.ColumnName + ".isChecked());");
                                            }
                                            else
                                            {
                                                if (column.TypeName.Name == "String")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(txt" + column.ColumnName + ".getText().toString());");
                                                }
                                                else if (column.TypeName.Name == "Int16" || column.TypeName.Name == "Int32")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Integer.valueOf(txt" + column.ColumnName + ".getText().toString()));");
                                                }
                                                else if (column.TypeName.Name == "Int64")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Long.valueOf(txt" + column.ColumnName + ".getText().toString()));");
                                                }
                                                else if (column.TypeName.Name == "Decimal" || column.TypeName.Name == "Double" || column.TypeName.Name == "Single")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Float.valueOf(txt" + column.ColumnName + ".getText().toString()));");
                                                }
                                                else if (column.TypeName.Name == "Byte")
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(Byte.valueOf(txt" + column.ColumnName + ".getText().toString()));");
                                                }
                                                else
                                                {
                                                    yaz.WriteLine("\t\t\t\t\t" + _table + "Data.set" + column.ColumnName + "(String.valueOf(txt" + column.ColumnName + ".getText().toString()));");
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\tBoolean sonuc = " + Table + "Business.Update(context, " + _table + "Data, where);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\tif (sonuc) {");
                            yaz.WriteLine("\t\t\t\t\t\tToast.makeText(context, \"Veri kaydedildi.\", Toast.LENGTH_SHORT).show();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t\t\t\tListeyeDon();");
                            yaz.WriteLine("\t\t\t\t\t} else {");
                            yaz.WriteLine("\t\t\t\t\t\tToast.makeText(context, \"Veri kaydedilemedi.\", Toast.LENGTH_SHORT).show();");
                            yaz.WriteLine("\t\t\t\t\t}");
                            yaz.WriteLine("\t\t\t\t} catch (Exception e) {");
                            yaz.WriteLine("\t\t\t\t\te.printStackTrace();");
                            yaz.WriteLine("\t\t\t\t\tToast.makeText(context, \"Veri kaydedilemedi.\", Toast.LENGTH_SHORT).show();");
                            yaz.WriteLine("\t\t\t\t}");

                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t});");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tbtnIptal.setOnClickListener(new View.OnClickListener() {");
                            yaz.WriteLine("\t\t\t@Override");
                            yaz.WriteLine("\t\t\tpublic void onClick(View v) {");
                            yaz.WriteLine("\t\t\t\tListeyeDon();");
                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t});");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\tprivate void VeriDoldur() {");
                            yaz.WriteLine("\t\ttry {");
                            yaz.WriteLine("\t\t\tArrayList<String> listValues = new ArrayList<>();");
                            yaz.WriteLine("\t\t\t");
                            yaz.WriteLine("\t\t\twhere = new Where();");
                            yaz.WriteLine("\t\t\twhere.setColumn(\"" + id + "\");");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tlistValues.add(bundle.getString(\"id\"));");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\twhere.setValues(listValues);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\t" + _table + "Data = " + Table + "Business.SelectSingle(context, where);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tif (" + _table + "Data != null) {");

                            int i = 0;

                            foreach (TableColumnNames column in columnNames)
                            {
                                string num = "";

                                if (i > 0)
                                {
                                    yaz.WriteLine("");
                                    num = (i + 1).ToString();
                                }

                                if (column.TypeName != null)
                                {
                                    List<ForeignKeyChecker> foreLst = fkcListForeign.Where(a => a.ForeignColumnName == column.ColumnName).GroupBy(a => a.PrimaryTableName).Select(a => a.FirstOrDefault()).ToList();

                                    if (column.ColumnName != id)
                                    {
                                        if (foreLst.Count > 0)
                                        {
                                            PrimaryTableName = foreLst.FirstOrDefault().PrimaryTableName;
                                            foreignColumnText = GetColumnText(tableColumnNames.Where(a => a.TableName == PrimaryTableName).ToList());
                                            List<string> identityColumnsForeign = Helper.Helper.ReturnIdentityColumn(connectionInfo, PrimaryTableName);
                                            foreignColumnId = identityColumnsForeign.Count > 0 ? identityColumnsForeign.FirstOrDefault() : "id";

                                            yaz.WriteLine("\t\t\t\tif (" + _table + "Data.get" + column.ColumnName + "() != null)");
                                            yaz.WriteLine("\t\t\t\t{");
                                            yaz.WriteLine("\t\t\t\t\tArrayList<" + PrimaryTableName + "> " + PrimaryTableName + "DataList" + num + " = " + PrimaryTableName + "Business.Select(context);");
                                            yaz.WriteLine("\t\t\t\t\tArrayList<String> " + PrimaryTableName + "Dizi" + num + " = new ArrayList<>();");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\tfor (" + PrimaryTableName + " data : " + PrimaryTableName + "DataList" + num + ") {");
                                            yaz.WriteLine("\t\t\t\t\t\t" + PrimaryTableName + "Dizi" + num + ".add(data.get" + foreignColumnText + "() + \" (\" + data.get" + foreignColumnId + "() + \")\");");
                                            yaz.WriteLine("\t\t\t\t\t}");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\tspin" + column.ColumnName + ".setAdapter(new ArrayAdapter(context, android.R.layout.simple_list_item_1, " + PrimaryTableName + "Dizi" + num + "));");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\tlistValues = new ArrayList<>();");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\twhere = new Where();");
                                            yaz.WriteLine("\t\t\t\t\twhere.setColumn(\"" + foreignColumnId + "\");");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\tlistValues.add(" + _table + "Data.get" + column.ColumnName + "().toString());");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\twhere.setValues(listValues);");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\t" + PrimaryTableName + " " + PrimaryTableName + "Data" + num + " = " + PrimaryTableName + "Business.SelectSingle(context, where);");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\tif (" + PrimaryTableName + "Data" + num + " != null) {");
                                            yaz.WriteLine("\t\t\t\t\t\tint i = 0;");
                                            yaz.WriteLine("\t\t\t\t\t\tint indis = 0;");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\t\tfor (String " + PrimaryTableName + " : " + PrimaryTableName + "Dizi" + num + ") {");
                                            yaz.WriteLine("\t\t\t\t\t\t\tString id = " + PrimaryTableName + ".split(\"\\\\(\")[1].replace(\")\", \"\");");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\t\t\tif (id.equals(" + PrimaryTableName + "Data" + num + ".get" + foreignColumnId + "().toString())) {");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\tindis = i;");
                                            yaz.WriteLine("\t\t\t\t\t\t\t\tbreak;");
                                            yaz.WriteLine("\t\t\t\t\t\t\t}");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\t\t\ti++;");
                                            yaz.WriteLine("\t\t\t\t\t\t}");
                                            yaz.WriteLine("");
                                            yaz.WriteLine("\t\t\t\t\t\tspin" + column.ColumnName + ".setSelection(indis);");
                                            yaz.WriteLine("\t\t\t\t\t}");
                                            yaz.WriteLine("\t\t\t\t}");
                                            yaz.WriteLine("");

                                            i++;
                                        }
                                        else
                                        {
                                            if (column.TypeName.Name == "Boolean")
                                            {
                                                yaz.WriteLine("\t\t\t\tif (" + _table + "Data.get" + column.ColumnName + "() != null)");
                                                yaz.WriteLine("\t\t\t\t{");
                                                yaz.WriteLine("\t\t\t\t\tchk" + column.ColumnName + ".setChecked(" + _table + "Data.get" + column.ColumnName + "());");
                                                yaz.WriteLine("\t\t\t\t}");
                                                yaz.WriteLine("");
                                            }
                                            else
                                            {
                                                yaz.WriteLine("\t\t\t\tif (" + _table + "Data.get" + column.ColumnName + "() != null)");
                                                yaz.WriteLine("\t\t\t\t{");
                                                yaz.WriteLine("\t\t\t\t\ttxt" + column.ColumnName + ".setText(" + _table + "Data.get" + column.ColumnName + "().toString());");
                                                yaz.WriteLine("\t\t\t\t}");
                                                yaz.WriteLine("");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        yaz.WriteLine("\t\t\t\tif (" + _table + "Data.get" + column.ColumnName + "() != null)");
                                        yaz.WriteLine("\t\t\t\t{");
                                        yaz.WriteLine("\t\t\t\t\tlbl" + column.ColumnName + ".setText(" + _table + "Data.get" + column.ColumnName + "().toString());");
                                        yaz.WriteLine("\t\t\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                            }

                            yaz.WriteLine("\t\t\t}");
                            yaz.WriteLine("\t\t} catch (Exception e) {");
                            yaz.WriteLine("\t\t\te.printStackTrace();");
                            yaz.WriteLine("\t\t\tToast.makeText(context, \"Veri getirilemedi.\", Toast.LENGTH_SHORT).show();");
                            yaz.WriteLine("\t\t\tListeyeDon();");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tprivate void ListeyeDon() {");
                            yaz.WriteLine("\t\tgetFragmentManager().beginTransaction().replace(R.id.flyt" + Table + ", " + _table + "Liste, \"" + Table + "Liste\").addToBackStack(\"" + Table + "Liste\").commit();");
                            yaz.WriteLine("\t}");

                            yaz.WriteLine("}");
                            yaz.Close();
                        }
                    }
                }
            }
        }

        void AndModelOlustur()
        {
            string projectNameKucuk = projectName.ToLower();

            foreach (string Table in selectedTables)
            {
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\Model\\" + Table + ".java", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                    {
                        yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".Model;");
                        yaz.WriteLine("");

                        yaz.WriteLine("public class " + Table);
                        yaz.WriteLine("{");

                        List<TableColumnNames> columnNames = tableColumnNames.Where(a => a.TableName == Table).ToList();

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                switch (column.TypeName.Name)
                                {
                                    case "Int16": yaz.WriteLine("\tprivate Integer " + column.ColumnName + ";"); break;
                                    case "Int32": yaz.WriteLine("\tprivate Integer " + column.ColumnName + ";"); break;
                                    case "Int64": yaz.WriteLine("\tprivate Long " + column.ColumnName + ";"); break;
                                    case "Decimal": yaz.WriteLine("\tprivate Float " + column.ColumnName + ";"); break;
                                    case "Double": yaz.WriteLine("\tprivate Float " + column.ColumnName + ";"); break;
                                    case "Char": yaz.WriteLine("\tprivate Character " + column.ColumnName + ";"); break;
                                    case "Chars": yaz.WriteLine("\tprivate Character[] " + column.ColumnName + ";"); break;
                                    case "String": yaz.WriteLine("\tprivate String " + column.ColumnName + ";"); break;
                                    case "Byte": yaz.WriteLine("\tprivate Byte " + column.ColumnName + ";"); break;
                                    case "Bytes": yaz.WriteLine("\tprivate Byte[] " + column.ColumnName + ";"); break;
                                    case "Boolean": yaz.WriteLine("\tprivate Boolean " + column.ColumnName + ";"); break;
                                    case "DateTime": yaz.WriteLine("\tprivate String " + column.ColumnName + ";"); break;
                                    case "DateTimeOffset": yaz.WriteLine("\tprivate String " + column.ColumnName + ";"); break;
                                    case "TimeSpan": yaz.WriteLine("\tprivate String " + column.ColumnName + ";"); break;
                                    case "Single": yaz.WriteLine("\tprivate Float " + column.ColumnName + ";"); break;
                                    case "Object": yaz.WriteLine("\tprivate Object " + column.ColumnName + ";"); break;
                                    case "Guid": yaz.WriteLine("\tprivate String " + column.ColumnName + ";"); break;
                                    default: yaz.WriteLine("\tprivate String " + column.ColumnName + ";"); break;
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t/*********** Getterlar ******************/");
                        yaz.WriteLine("");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                switch (column.TypeName.Name)
                                {
                                    case "Int16": yaz.WriteLine("\tpublic Integer get" + column.ColumnName + "()"); break;
                                    case "Int32": yaz.WriteLine("\tpublic Integer get" + column.ColumnName + "()"); break;
                                    case "Int64": yaz.WriteLine("\tpublic Long get" + column.ColumnName + "()"); break;
                                    case "Decimal": yaz.WriteLine("\tpublic Float get" + column.ColumnName + "()"); break;
                                    case "Double": yaz.WriteLine("\tpublic Float get" + column.ColumnName + "()"); break;
                                    case "Char": yaz.WriteLine("\tpublic Character get" + column.ColumnName + "()"); break;
                                    case "Chars": yaz.WriteLine("\tpublic Character[] get" + column.ColumnName + "()"); break;
                                    case "String": yaz.WriteLine("\tpublic String get" + column.ColumnName + "()"); break;
                                    case "Byte": yaz.WriteLine("\tpublic Byte get" + column.ColumnName + "()"); break;
                                    case "Bytes": yaz.WriteLine("\tpublic Byte[] get" + column.ColumnName + "()"); break;
                                    case "Boolean": yaz.WriteLine("\tpublic Boolean get" + column.ColumnName + "()"); break;
                                    case "DateTime": yaz.WriteLine("\tpublic String get" + column.ColumnName + "()"); break;
                                    case "DateTimeOffset": yaz.WriteLine("\tpublic String get" + column.ColumnName + "()"); break;
                                    case "TimeSpan": yaz.WriteLine("\tpublic String get" + column.ColumnName + "()"); break;
                                    case "Single": yaz.WriteLine("\tpublic Float get" + column.ColumnName + "()"); break;
                                    case "Object": yaz.WriteLine("\tpublic Object get" + column.ColumnName + "()"); break;
                                    case "Guid": yaz.WriteLine("\tpublic String get" + column.ColumnName + "()"); break;
                                    default: yaz.WriteLine("\tpublic String get" + column.ColumnName + "()"); break;
                                }

                                yaz.WriteLine("\t{");
                                yaz.WriteLine("\t\t return this." + column.ColumnName + ";");
                                yaz.WriteLine("\t}");
                            }
                            else
                            {
                                yaz.WriteLine("\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("\t/*********** Setterlar ******************/");
                        yaz.WriteLine("");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                switch (column.TypeName.Name)
                                {
                                    case "Int16": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Integer _" + column.ColumnName + ")"); break;
                                    case "Int32": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Integer _" + column.ColumnName + ")"); break;
                                    case "Int64": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Long _" + column.ColumnName + ")"); break;
                                    case "Decimal": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Float _" + column.ColumnName + ")"); break;
                                    case "Double": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Float _" + column.ColumnName + ")"); break;
                                    case "Char": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Character _" + column.ColumnName + ")"); break;
                                    case "Chars": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Character[] _" + column.ColumnName + ")"); break;
                                    case "String": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(String _" + column.ColumnName + ")"); break;
                                    case "Byte": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Byte _" + column.ColumnName + ")"); break;
                                    case "Bytes": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Byte[] _" + column.ColumnName + ")"); break;
                                    case "Boolean": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Boolean _" + column.ColumnName + ")"); break;
                                    case "DateTime": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(String _" + column.ColumnName + ")"); break;
                                    case "DateTimeOffset": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(String _" + column.ColumnName + ")"); break;
                                    case "TimeSpan": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(String _" + column.ColumnName + ")"); break;
                                    case "Single": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Float _" + column.ColumnName + ")"); break;
                                    case "Object": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(Object _" + column.ColumnName + ")"); break;
                                    case "Guid": yaz.WriteLine("\tpublic void set" + column.ColumnName + "(String _" + column.ColumnName + ")"); break;
                                    default: yaz.WriteLine("\tpublic void set" + column.ColumnName + "(String _" + column.ColumnName + ")"); break;
                                }

                                yaz.WriteLine("\t{");
                                yaz.WriteLine("\t\tthis." + column.ColumnName + " = _" + column.ColumnName + ";");
                                yaz.WriteLine("\t}");
                            }
                            else
                            {
                                yaz.WriteLine("\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }
                            yaz.WriteLine("");
                        }

                        yaz.WriteLine("}");
                        yaz.Close();
                    }
                }
            }
        }

        void AndDataSQLite()
        {
            string projectNameKucuk = projectName.ToLower();

            foreach (string Table in selectedTables)
            {
                string _table = Table.ToHyperLinkText(true);

                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));
                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                List<TableColumnNames> columnNames = tableColumnNames.Where(a => a.TableName == Table).ToList();

                //Business
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\Business\\" + Table + "Business.java", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                    {
                        yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".DB.Business;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import android.content.Context;");
                        yaz.WriteLine("import android.database.Cursor;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".DB.DataAccess." + Table + "Access;");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + Table + ";");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Select;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Where;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import java.util.ArrayList;");
                        yaz.WriteLine("");
                        yaz.WriteLine("public class " + Table + "Business {");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context) {");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, null, null);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn Select(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, Select select) {");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, select, null);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn Select(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, Where where) {");
                        yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(where != null) {");
                        yaz.WriteLine("\t\t\twhereList.add(where);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, null, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn Select(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, Select select, Where where) {");
                        yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(where != null) {");
                        yaz.WriteLine("\t\t\twhereList.add(where);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn Select(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, ArrayList<Where> whereList) {");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, null, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn Select(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, Select select, ArrayList<Where> whereList) {");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn Select(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate static ArrayList<" + Table + "> Select(Cursor cursor) {");
                        yaz.WriteLine("\t\tArrayList<" + Table + "> " + _table + "List = new ArrayList<>();");
                        yaz.WriteLine("\t\t" + Table + " " + _table + ";");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (cursor.moveToFirst()) {");
                        yaz.WriteLine("\t\t\tdo {");
                        yaz.WriteLine("\t\t\t\t" + _table + " = new " + Table + "();");
                        yaz.WriteLine("");

                        int i = 0;

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                switch (column.TypeName.Name)
                                {
                                    case "Int16": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getInt(" + i.ToString() + "));"); break;
                                    case "Int32": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getInt(" + i.ToString() + "));"); break;
                                    case "Int64": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getLong(" + i.ToString() + "));"); break;
                                    case "Decimal": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getFloat(" + i.ToString() + "));"); break;
                                    case "Double": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getFloat(" + i.ToString() + "));"); break;
                                    case "Char": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Chars": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "String": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Byte": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Bytes": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getBlob(" + i.ToString() + "));"); break;
                                    case "Boolean": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getInt(" + i.ToString() + ") != 0);"); break;
                                    case "DateTime": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "DateTimeOffset": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "TimeSpan": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Single": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Object": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Guid": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    default: yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }

                            i++;
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t" + _table + "List.add(" + _table + ");");
                        yaz.WriteLine("\t\t\t} while (cursor.moveToNext());");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\treturn " + _table + "List;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context) {");
                        yaz.WriteLine("\t\tSelect select = new Select();");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, select, null);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn SelectSingle(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context, Select select) {");
                        yaz.WriteLine("\t\tif (select == null) {");
                        yaz.WriteLine("\t\t\tselect = new Select();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, select, null);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn SelectSingle(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context, Where where) {");
                        yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tSelect select = new Select();");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(where != null) {");
                        yaz.WriteLine("\t\t\twhereList.add(where);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn SelectSingle(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context, Select select, Where where) {");
                        yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (select == null) {");
                        yaz.WriteLine("\t\t\tselect = new Select();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(where != null) {");
                        yaz.WriteLine("\t\t\twhereList.add(where);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn SelectSingle(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context, ArrayList<Where> whereList) {");
                        yaz.WriteLine("\t\tSelect select = new Select();");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn SelectSingle(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context, Select select, ArrayList<Where> whereList) {");
                        yaz.WriteLine("\t\tif (select == null) {");
                        yaz.WriteLine("\t\t\tselect = new Select();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tCursor cursor = " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn SelectSingle(cursor);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tprivate static " + Table + " SelectSingle(Cursor cursor) {");
                        yaz.WriteLine("\t\t" + Table + " " + _table + " = null;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (cursor != null) {");
                        yaz.WriteLine("\t\t\tif (cursor.getCount() > 0) {");
                        yaz.WriteLine("\t\t\t\tcursor.moveToFirst();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t" + _table + " = new " + Table + "();");
                        yaz.WriteLine("");

                        i = 0;

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                switch (column.TypeName.Name)
                                {
                                    case "Int16": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getInt(" + i.ToString() + "));"); break;
                                    case "Int32": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getInt(" + i.ToString() + "));"); break;
                                    case "Int64": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getLong(" + i.ToString() + "));"); break;
                                    case "Decimal": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getFloat(" + i.ToString() + "));"); break;
                                    case "Double": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getFloat(" + i.ToString() + "));"); break;
                                    case "Char": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Chars": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "String": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Byte": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Bytes": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getBlob(" + i.ToString() + "));"); break;
                                    case "Boolean": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getInt(" + i.ToString() + ") != 0);"); break;
                                    case "DateTime": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "DateTimeOffset": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "TimeSpan": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Single": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Object": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    case "Guid": yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                    default: yaz.WriteLine("\t\t\t\t" + _table + ".set" + column.ColumnName + "(cursor.getString(" + i.ToString() + "));"); break;
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }

                            i++;
                        }

                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn " + _table + ";");
                        yaz.WriteLine("\t}");

                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static Boolean Insert(Context context, " + Table + " " + _table + ") {");
                        yaz.WriteLine("\t\treturn " + Table + "Access.Insert(context, " + _table + ");");
                        yaz.WriteLine("\t}");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + _table + ") {");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Update(context, " + _table + ", null);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + _table + ", Where where) {");
                            yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tif(where != null) {");
                            yaz.WriteLine("\t\t\twhereList.add(where);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");

                            yaz.WriteLine("\t\treturn " + Table + "Access.Update(context, " + _table + ", whereList);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + _table + ", ArrayList<Where> whereList) {");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Update(context, " + _table + ", whereList);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");






                            yaz.WriteLine("\tpublic static Boolean Delete(Context context) {");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Delete(context, null);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Delete(Context context, Where where) {");
                            yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tif(where != null) {");
                            yaz.WriteLine("\t\t\twhereList.add(where);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Delete(context, whereList);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Delete(Context context, ArrayList<Where> whereList) {");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Delete(context, whereList);");
                            yaz.WriteLine("\t}");
                        }

                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }

                //DataAccess
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\DataAccess\\" + Table + "Access.java", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                    {
                        yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".DB.DataAccess;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import android.content.ContentValues;");
                        yaz.WriteLine("import android.content.Context;");
                        yaz.WriteLine("import android.database.Cursor;");
                        yaz.WriteLine("import android.database.sqlite.SQLiteDatabase;");
                        yaz.WriteLine("import android.os.Environment;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + Table + ";");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Connection;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Select;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Where;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdframework.WhereArgs;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdlibrary.TDAraclar;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import java.util.ArrayList;");
                        yaz.WriteLine("");
                        yaz.WriteLine("public class " + Table + "Access {");
                        yaz.WriteLine("\tprivate static String dbPath, dbName;");
                        yaz.WriteLine("\tprivate static Integer dbVer;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static Cursor Select(Context context, Select select, ArrayList<Where> whereList) {");
                        yaz.WriteLine("\t\tSQLiteDatabase database;");
                        yaz.WriteLine("\t\tCursor cursor;");
                        yaz.WriteLine("\t\tString orderBy = null;");
                        yaz.WriteLine("\t\tString limit = null;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tdbVer = (Integer) TDAraclar.KaynakDegerDon(context, R.integer.dbVer, TDAraclar.KaynakTip.Integer);");
                        yaz.WriteLine("\t\tdbName = (String) TDAraclar.KaynakDegerDon(context, R.string.dbName, TDAraclar.KaynakTip.String);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tboolean klasorKontrol = (Boolean) TDAraclar.BellektenOku(TDAraclar.AktiviteDon(context), \"thrashead\", \"klasor\", Boolean.class);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (klasorKontrol) {");
                        yaz.WriteLine("\t\t\tdbPath = Environment.getExternalStorageDirectory() + (String) TDAraclar.KaynakDegerDon(context, R.string.dbPath, TDAraclar.KaynakTip.String);");
                        yaz.WriteLine("\t\t} else {");
                        yaz.WriteLine("\t\t\treturn null;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tConnection con = new Connection(context, dbVer, dbPath, dbName);");
                        yaz.WriteLine("\t\tdatabase = con.getReadableDatabase();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tWhereArgs whereArgs = WhereArgs.CreateArguments(whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (select != null) {");
                        yaz.WriteLine("\t\t\tif (select.getTop() != null) {");
                        yaz.WriteLine("\t\t\t\tlimit = String.valueOf(select.getTop());");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tif (select.getOrderColumn() != null) {");
                        yaz.WriteLine("\t\t\t\torderBy = \" orderby \" + select.getOrderColumn();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\tif (select.getOrderBy() != null) {");
                        yaz.WriteLine("\t\t\t\t\tif (select.getOrderBy() == Select.OrderBy.Asc) {");
                        yaz.WriteLine("\t\t\t\t\t\torderBy += \" asc\";");
                        yaz.WriteLine("\t\t\t\t\t} else {");
                        yaz.WriteLine("\t\t\t\t\t\torderBy += \" desc\";");
                        yaz.WriteLine("\t\t\t\t\t}");
                        yaz.WriteLine("\t\t\t\t}");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (select == null) {");
                        yaz.WriteLine("\t\t\tselect = new Select();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tcursor = database.query(\"" + Table + "\", select.getColumns(), whereArgs.getWhereClauses(), whereArgs.getArguments(), null, null, orderBy, limit);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn cursor;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static Boolean Insert(Context context, " + Table + " " + _table + ") {");
                        yaz.WriteLine("\t\tSQLiteDatabase database;");
                        yaz.WriteLine("\t\tBoolean result = false;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tdbVer = (Integer) TDAraclar.KaynakDegerDon(context, R.integer.dbVer, TDAraclar.KaynakTip.Integer);");
                        yaz.WriteLine("\t\tdbName = (String) TDAraclar.KaynakDegerDon(context, R.string.dbName, TDAraclar.KaynakTip.String);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tboolean klasorKontrol = (Boolean) TDAraclar.BellektenOku(TDAraclar.AktiviteDon(context), \"thrashead\", \"klasor\", Boolean.class);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (klasorKontrol) {");
                        yaz.WriteLine("\t\t\tdbPath = Environment.getExternalStorageDirectory() + (String) TDAraclar.KaynakDegerDon(context, R.string.dbPath, TDAraclar.KaynakTip.String);");
                        yaz.WriteLine("\t\t} else {");
                        yaz.WriteLine("\t\t\treturn false;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tConnection con = new Connection(context, dbVer, dbPath, dbName);");
                        yaz.WriteLine("\t\tdatabase = con.getWritableDatabase();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tContentValues values = new ContentValues();");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                if (column.ColumnName != id)
                                {
                                    yaz.WriteLine("\t\tvalues.put(\"" + column.ColumnName + "\", " + _table + ".get" + column.ColumnName + "());");
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tlong id = database.insert(\"" + Table + "\", null, values);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (id > 0) {");
                        yaz.WriteLine("\t\t\tresult = true;");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\treturn result;");
                        yaz.WriteLine("\t}");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + _table + ", ArrayList<Where> whereList) {");
                            yaz.WriteLine("\t\tSQLiteDatabase database;");
                            yaz.WriteLine("\t\tBoolean result = false;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tdbVer = (Integer) TDAraclar.KaynakDegerDon(context, R.integer.dbVer, TDAraclar.KaynakTip.Integer);");
                            yaz.WriteLine("\t\tdbName = (String) TDAraclar.KaynakDegerDon(context, R.string.dbName, TDAraclar.KaynakTip.String);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tboolean klasorKontrol = (Boolean) TDAraclar.BellektenOku(TDAraclar.AktiviteDon(context), \"thrashead\", \"klasor\", Boolean.class);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tif (klasorKontrol) {");
                            yaz.WriteLine("\t\t\tdbPath = Environment.getExternalStorageDirectory() + (String) TDAraclar.KaynakDegerDon(context, R.string.dbPath, TDAraclar.KaynakTip.String);");
                            yaz.WriteLine("\t\t} else {");
                            yaz.WriteLine("\t\t\treturn false;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tConnection con = new Connection(context, dbVer, dbPath, dbName);");
                            yaz.WriteLine("\t\tdatabase = con.getWritableDatabase();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tContentValues values = new ContentValues();");
                            yaz.WriteLine("\t\tWhereArgs whereArgs = WhereArgs.CreateArguments(whereList);");
                            yaz.WriteLine("");

                            foreach (TableColumnNames column in columnNames)
                            {
                                if (column.TypeName != null)
                                {
                                    if (column.ColumnName != id)
                                    {
                                        yaz.WriteLine("\t\tif (" + _table + ".get" + column.ColumnName + "() != null) {");
                                        yaz.WriteLine("\t\t\tvalues.put(\"" + column.ColumnName + "\", " + _table + ".get" + column.ColumnName + "());");
                                        yaz.WriteLine("\t\t}");
                                        yaz.WriteLine("");
                                    }
                                }
                                else
                                {
                                    yaz.WriteLine("\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                                }
                            }

                            yaz.WriteLine("\t\tlong id = database.update(\"" + Table + "\", values, whereArgs.getWhereClauses(), whereArgs.getArguments());");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tif (id > 0) {");
                            yaz.WriteLine("\t\t\tresult = true;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\treturn result;");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Delete(Context context, ArrayList<Where> whereList) {");
                            yaz.WriteLine("\t\tSQLiteDatabase database;");
                            yaz.WriteLine("\t\tBoolean result = false;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tdbVer = (Integer) TDAraclar.KaynakDegerDon(context, R.integer.dbVer, TDAraclar.KaynakTip.Integer);");
                            yaz.WriteLine("\t\tdbName = (String) TDAraclar.KaynakDegerDon(context, R.string.dbName, TDAraclar.KaynakTip.String);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tboolean klasorKontrol = (Boolean) TDAraclar.BellektenOku(TDAraclar.AktiviteDon(context), \"thrashead\", \"klasor\", Boolean.class);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tif (klasorKontrol) {");
                            yaz.WriteLine("\t\t\tdbPath = Environment.getExternalStorageDirectory() + (String) TDAraclar.KaynakDegerDon(context, R.string.dbPath, TDAraclar.KaynakTip.String);");
                            yaz.WriteLine("\t\t} else {");
                            yaz.WriteLine("\t\t\treturn false;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tConnection con = new Connection(context, dbVer, dbPath, dbName);");
                            yaz.WriteLine("\t\tdatabase = con.getWritableDatabase();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tWhereArgs whereArgs = WhereArgs.CreateArguments(whereList);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tlong id = database.delete(\"" + Table + "\", whereArgs.getWhereClauses(), whereArgs.getArguments());");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tif (id > 0) {");
                            yaz.WriteLine("\t\t\tresult = true;");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\treturn result;");
                            yaz.WriteLine("\t}");
                        }

                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }
        }

        void AndDataWCF()
        {
            string projectNameKucuk = projectName.ToLower();

            foreach (string Table in selectedTables)
            {
                string _table = Table.ToHyperLinkText(true);

                List<string> identityColumns = Helper.Helper.ReturnIdentityColumn(connectionInfo, Table);

                string id = identityColumns.Count > 0 ? identityColumns.FirstOrDefault() : "id";

                SqlConnection con = new SqlConnection(Helper.Helper.CreateConnectionText(connectionInfo));
                List<ForeignKeyChecker> fkcList = ForeignKeyCheck(con, Table);
                fkcList = fkcList.Where(a => a.PrimaryTableName == Table).ToList();

                List<ForeignKeyChecker> fkcListForeign = ForeignKeyCheck(con);
                fkcListForeign = fkcListForeign.Where(a => a.ForeignTableName == Table).ToList();

                List<TableColumnNames> columnNames = tableColumnNames.Where(a => a.TableName == Table).ToList();

                //Business
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\Business\\" + Table + "Business.java", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                    {
                        yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".DB.Business;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import android.content.Context;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".DB.DataAccess." + Table + "Access;");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + Table + ";");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Select;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Where;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import java.util.ArrayList;");
                        yaz.WriteLine("");
                        yaz.WriteLine("public class " + Table + "Business {");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context) {");
                        yaz.WriteLine("\t\treturn " + Table + "Access.Select(context, null, null);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, Select select) {");
                        yaz.WriteLine("\t\treturn " + Table + "Access.Select(context, select, null);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, Where where) {");
                        yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(where != null) {");
                        yaz.WriteLine("\t\t\twhereList.add(where);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn " + Table + "Access.Select(context, null, whereList);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, Select select, Where where) {");
                        yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(where != null) {");
                        yaz.WriteLine("\t\t\twhereList.add(where);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, ArrayList<Where> whereList) {");
                        yaz.WriteLine("\t\treturn " + Table + "Access.Select(context, null, whereList);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, Select select, ArrayList<Where> whereList) {");
                        yaz.WriteLine("\t\treturn " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context) {");
                        yaz.WriteLine("\t\tArrayList<" + Table + "> " + _table + "List;");
                        yaz.WriteLine("\t\t");
                        yaz.WriteLine("\t\tSelect select = new Select();");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t" + _table + "List = " + Table + "Access.Select(context, select, null);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(" + _table + "List != null) {");
                        yaz.WriteLine("\t\t\tif(" + _table + "List.size() > 0) {");
                        yaz.WriteLine("\t\t\t\treturn " + _table + "List.get(0);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\t");
                        yaz.WriteLine("\t\treturn null;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context, Select select) {");
                        yaz.WriteLine("\t\tArrayList<" + Table + "> " + _table + "List;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (select == null) {");
                        yaz.WriteLine("\t\t\tselect = new Select();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t" + _table + "List = " + Table + "Access.Select(context, select, null);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(" + _table + "List != null) {");
                        yaz.WriteLine("\t\t\tif(" + _table + "List.size() > 0) {");
                        yaz.WriteLine("\t\t\t\treturn " + _table + "List.get(0);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn null;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context, Where where) {");
                        yaz.WriteLine("\t\tArrayList<" + Table + "> " + _table + "List;");
                        yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tSelect select = new Select();");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(where != null) {");
                        yaz.WriteLine("\t\t\twhereList.add(where);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t" + _table + "List = " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(" + _table + "List != null) {");
                        yaz.WriteLine("\t\t\tif(" + _table + "List.size() > 0) {");
                        yaz.WriteLine("\t\t\t\treturn " + _table + "List.get(0);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn null;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context, Select select, Where where) {");
                        yaz.WriteLine("\t\tArrayList<" + Table + "> " + _table + "List;");
                        yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif (select == null) {");
                        yaz.WriteLine("\t\t\tselect = new Select();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(where != null) {");
                        yaz.WriteLine("\t\t\twhereList.add(where);");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t" + _table + "List = " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(" + _table + "List != null) {");
                        yaz.WriteLine("\t\t\tif(" + _table + "List.size() > 0) {");
                        yaz.WriteLine("\t\t\t\treturn " + _table + "List.get(0);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn null;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context, ArrayList<Where> whereList) {");
                        yaz.WriteLine("\t\tArrayList<" + Table + "> " + _table + "List;");
                        yaz.WriteLine("\t\t");
                        yaz.WriteLine("\t\tSelect select = new Select();");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t" + _table + "List = " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(" + _table + "List != null) {");
                        yaz.WriteLine("\t\t\tif(" + _table + "List.size() > 0) {");
                        yaz.WriteLine("\t\t\t\treturn " + _table + "List.get(0);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn null;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static " + Table + " SelectSingle(Context context, Select select, ArrayList<Where> whereList) {");
                        yaz.WriteLine("\t\tArrayList<" + Table + "> " + _table + "List;");
                        yaz.WriteLine("\t\t");
                        yaz.WriteLine("\t\tif (select == null) {");
                        yaz.WriteLine("\t\t\tselect = new Select();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tselect.setTop(1);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t" + _table + "List = " + Table + "Access.Select(context, select, whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tif(" + _table + "List != null) {");
                        yaz.WriteLine("\t\t\tif(" + _table + "List.size() > 0) {");
                        yaz.WriteLine("\t\t\t\treturn " + _table + "List.get(0);");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn null;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\tpublic static Boolean Insert(Context context, " + Table + " " + _table + ") {");
                        yaz.WriteLine("\t\treturn " + Table + "Access.Insert(context, " + _table + ");");
                        yaz.WriteLine("\t}");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + _table + ") {");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Update(context, " + _table + ", null, null);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + _table + ", Where where) {");
                            yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tif(where != null) {");
                            yaz.WriteLine("\t\t\twhereList.add(where);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Update(context, " + _table + ", null, whereList);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + _table + ", ArrayList<Where> whereList) {");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Update(context, " + _table + ", null, whereList);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + _table + ", Select select) {");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Update(context, " + _table + ", select, null);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + _table + ", Select select, Where where) {");
                            yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tif(where != null) {");
                            yaz.WriteLine("\t\t\twhereList.add(where);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Update(context, " + _table + ", select, whereList);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + _table + ", Select select, ArrayList<Where> whereList) {");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Update(context, " + _table + ", select, whereList);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Delete(Context context) {");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Delete(context, null);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Delete(Context context, Where where) {");
                            yaz.WriteLine("\t\tArrayList<Where> whereList = new ArrayList<>();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tif(where != null) {");
                            yaz.WriteLine("\t\t\twhereList.add(where);");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Delete(context, whereList);");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\tpublic static Boolean Delete(Context context, ArrayList<Where> whereList) {");
                            yaz.WriteLine("\t\treturn " + Table + "Access.Delete(context, whereList);");
                            yaz.WriteLine("\t}");
                        }

                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }

                //DataAccess
                using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\java\\com\\sinasalik\\thrashead\\" + projectNameKucuk + "\\DB\\DataAccess\\" + Table + "Access.java", FileMode.Create))
                {
                    using (StreamWriter yaz = new StreamWriter(fs, Encoding.ASCII))
                    {
                        yaz.WriteLine("package com.sinasalik.thrashead." + projectNameKucuk + ".DB.DataAccess;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import android.content.Context;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import com.google.gson.Gson;");
                        yaz.WriteLine("import com.google.gson.JsonElement;");
                        yaz.WriteLine("import com.google.gson.JsonObject;");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".Model." + Table + ";");
                        yaz.WriteLine("import com.sinasalik.thrashead." + projectNameKucuk + ".R;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Select;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdframework.Where;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdlibrary.TDAraclar;");
                        yaz.WriteLine("import com.sinasalik.thrashead.tdlibrary.TDJson;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import org.json.JSONArray;");
                        yaz.WriteLine("import org.json.JSONObject;");
                        yaz.WriteLine("");
                        yaz.WriteLine("import java.util.ArrayList;");
                        yaz.WriteLine("");
                        yaz.WriteLine("public class " + Table + "Access {");
                        yaz.WriteLine("\tprivate static Gson gson;");
                        yaz.WriteLine("\tprivate static JsonObject json;");
                        yaz.WriteLine("");

                        //Select
                        yaz.WriteLine("\tpublic static ArrayList<" + Table + "> Select(Context context, Select select, ArrayList<Where> whereList) {");
                        yaz.WriteLine("\t\tArrayList<" + Table + "> " + Table + "List = new ArrayList<>();");
                        yaz.WriteLine("\t\t" + Table + " " + Table + ";");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tgson = new Gson();");
                        yaz.WriteLine("\t\tjson = new JsonObject();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\ttry {");
                        yaz.WriteLine("\t\t\tJsonElement jSelect = gson.toJsonTree(select);");
                        yaz.WriteLine("\t\t\tJsonElement jWhereList = gson.toJsonTree(whereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tjson.add(\"select\", jSelect);");
                        yaz.WriteLine("\t\t\tjson.add(\"whereList\", jWhereList);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tString jsonResult = TDJson.ReturnResult(TDAraclar.KaynakDegerDon(context, R.string.servis_root, TDAraclar.KaynakTip.String) + \"" + Table + "Service.svc/Select/\", json, true);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tJSONArray array = new JSONObject(jsonResult).optJSONArray(\"SelectResult\");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tfor (int i = 0; i < array.length(); i++) {");
                        yaz.WriteLine("\t\t\t\t" + Table + " = new " + Table + "();");
                        yaz.WriteLine("\t\t\t\tJSONObject object = array.getJSONObject(i);");
                        yaz.WriteLine("");

                        foreach (TableColumnNames column in columnNames)
                        {
                            if (column.TypeName != null)
                            {
                                switch (column.TypeName.Name)
                                {
                                    case "Int16": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getInt(\"" + column.ColumnName + "\") : -1);"); break;
                                    case "Int32": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getInt(\"" + column.ColumnName + "\") : -1);"); break;
                                    case "Int64": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getLong(\"" + column.ColumnName + "\") : -1);"); break;
                                    case "Decimal": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getDouble(\"" + column.ColumnName + "\") : -1);"); break;
                                    case "Double": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getDouble(\"" + column.ColumnName + "\") : -1);"); break;
                                    case "Char": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getString(\"" + column.ColumnName + "\") : \"\");"); break;
                                    case "Chars": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getString(\"" + column.ColumnName + "\") : \"\");"); break;
                                    case "String": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getString(\"" + column.ColumnName + "\") : \"\");"); break;
                                    case "Byte": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getInt(\"" + column.ColumnName + "\") : 0);"); break;
                                    case "Bytes": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getInt(\"" + column.ColumnName + "\") : 0);"); break;
                                    case "Boolean": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getBoolean(\"" + column.ColumnName + "\") : false);"); break;
                                    case "DateTime": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getString(\"" + column.ColumnName + "\") : \"\");"); break;
                                    case "DateTimeOffset": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getString(\"" + column.ColumnName + "\") : \"\");"); break;
                                    case "TimeSpan": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getString(\"" + column.ColumnName + "\") : \"\");"); break;
                                    case "Single": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getInt(\"" + column.ColumnName + "\") : -1);"); break;
                                    case "Object": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.get(\"" + column.ColumnName + "\") : null);"); break;
                                    case "Guid": yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getString(\"" + column.ColumnName + "\") : \"\");"); break;
                                    default: yaz.WriteLine("\t\t\t\t" + Table + ".set" + column.ColumnName + "(!object.get(\"" + column.ColumnName + "\").equals(null) ? object.getString(\"" + column.ColumnName + "\") : \"\");"); break;
                                }
                            }
                            else
                            {
                                yaz.WriteLine("\t\t\t\t//" + column.ColumnName + " isimli kolonun veri tipi bu programda tanumlı değil.");
                            }
                        }

                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\t\t" + Table + "List.add(" + Table + ");");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("\t\t} catch (Exception e) {");
                        yaz.WriteLine("\t\t\te.printStackTrace();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn " + Table + "List;");
                        yaz.WriteLine("\t}");
                        yaz.WriteLine("");

                        //Insert
                        yaz.WriteLine("\tpublic static Boolean Insert(Context context, " + Table + " " + Table + "Data) {");
                        yaz.WriteLine("\t\tBoolean result = false;");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\tgson = new Gson();");
                        yaz.WriteLine("\t\tjson = new JsonObject();");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\ttry {");
                        yaz.WriteLine("\t\t\tJsonElement j" + Table + " = gson.toJsonTree(" + Table + "Data);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tjson.add(\"" + Table + "Data\", j" + Table + ");");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tString jsonResult = TDJson.ReturnResult(TDAraclar.KaynakDegerDon(context, R.string.servis_root, TDAraclar.KaynakTip.String) + \"" + Table + "Service.svc/Insert/\", json, true);");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\t\tresult = jsonResult.toLowerCase().contains(\"true\") ? true : false;");
                        yaz.WriteLine("\t\t} catch (Exception e) {");
                        yaz.WriteLine("\t\t\te.printStackTrace();");
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                        yaz.WriteLine("\t\treturn result;");
                        yaz.WriteLine("\t}");

                        if (identityColumns.Count > 0)
                        {
                            yaz.WriteLine("");

                            //Update
                            yaz.WriteLine("\tpublic static Boolean Update(Context context, " + Table + " " + Table + "Data, Select select, ArrayList<Where> whereList) {");
                            yaz.WriteLine("\t\tBoolean result = false;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tgson = new Gson();");
                            yaz.WriteLine("\t\tjson = new JsonObject();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\ttry {");
                            yaz.WriteLine("\t\t\tJsonElement j" + Table + " = gson.toJsonTree(" + Table + "Data);");
                            yaz.WriteLine("\t\t\tJsonElement jSelect = gson.toJsonTree(select);");
                            yaz.WriteLine("\t\t\tJsonElement jWhereList = gson.toJsonTree(whereList);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tjson.add(\"" + Table + "Data\", j" + Table + ");");
                            yaz.WriteLine("\t\t\tjson.add(\"select\", jSelect);");
                            yaz.WriteLine("\t\t\tjson.add(\"whereList\", jWhereList);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tString jsonResult = TDJson.ReturnResult(TDAraclar.KaynakDegerDon(context, R.string.servis_root, TDAraclar.KaynakTip.String) + \"" + Table + "Service.svc/Update/\", json, true);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tresult = jsonResult.toLowerCase().contains(\"true\") ? true : false;");
                            yaz.WriteLine("\t\t} catch (Exception e) {");
                            yaz.WriteLine("\t\t\te.printStackTrace();");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\treturn result;");
                            yaz.WriteLine("\t}");
                            yaz.WriteLine("");

                            //Delete
                            yaz.WriteLine("\tpublic static Boolean Delete(Context context, ArrayList<Where> whereList) {");
                            yaz.WriteLine("\t\tBoolean result = false;");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\tgson = new Gson();");
                            yaz.WriteLine("\t\tjson = new JsonObject();");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\ttry {");
                            yaz.WriteLine("\t\t\tJsonElement jWhereList = gson.toJsonTree(whereList);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tjson.add(\"whereList\", jWhereList);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tString jsonResult = TDJson.ReturnResult(TDAraclar.KaynakDegerDon(context, R.string.servis_root, TDAraclar.KaynakTip.String) + \"" + Table + "Service.svc/Delete/\", json, true);");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\t\tresult = jsonResult.toLowerCase().contains(\"true\") ? true : false;");
                            yaz.WriteLine("\t\t} catch (Exception e) {");
                            yaz.WriteLine("\t\t\te.printStackTrace();");
                            yaz.WriteLine("\t\t}");
                            yaz.WriteLine("");
                            yaz.WriteLine("\t\treturn result;");
                            yaz.WriteLine("\t}");
                        }

                        yaz.WriteLine("}");

                        yaz.Close();
                    }
                }
            }
        }

        void AndValuesOlustur()
        {
            string projectNameKucuk = projectName.ToLower();

            //strings
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\values\\strings.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<resources>");
                    yaz.WriteLine("\t<string name=\"app_name\">" + projectName + "</string>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<string name=\"menuAnasayfaBaslik\">Ana Sayfa</string>");
                    yaz.WriteLine("\t<string name=\"menuDeveloperBaslik\">Sina SALIK Website</string>");
                    yaz.WriteLine("\t<string name=\"menuCikisBaslik\">Çıkış</string>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<string name=\"alertKapatBaslik\">Çıkış Kontrol</string>");
                    yaz.WriteLine("\t<string name=\"alertKapatMesaj\">Kapatmak istediğinize emin misiniz?</string>");
                    yaz.WriteLine("\t<string name=\"alertSilBaslik\">Sil Kontrol</string>");
                    yaz.WriteLine("\t<string name=\"alertSilMesaj\">Silmek istediğinize emin misiniz?</string>");
                    yaz.WriteLine("\t<string name=\"alertKapatTamam\">Tamam</string>");
                    yaz.WriteLine("\t<string name=\"alertKapatIptal\">İptal</string>");
                    yaz.WriteLine("\t<string name=\"klasorHata\">Sistem Klasörleri Oluşturulamadı</string>");
                    yaz.WriteLine("");

                    if (rdAndSqlite.Checked)
                    {
                        yaz.WriteLine("\t<string name=\"rootPath\">/thrashead</string>");
                        yaz.WriteLine("\t<string name=\"projectPath\">/thrashead/" + projectNameKucuk + "</string>");
                        yaz.WriteLine("\t<string name=\"dbPath\">/thrashead/" + projectNameKucuk + "/db</string>");
                        yaz.WriteLine("\t<string name=\"dbName\">" + projectName + ".db</string>");
                        yaz.WriteLine("\t<integer name=\"dbVer\">1</integer>");
                        yaz.WriteLine("");
                    }

                    if (rdAndWcf.Checked)
                    {
                        yaz.WriteLine("\t<string name=\"servis_root\">http://10.0.2.2/" + projectName + "/Servis/</string>");
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\t<string name=\"web_developer\">http://www.sinasalikweb.xyz/</string>");
                    yaz.WriteLine("</resources>");
                }
            }

            //colors
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\values\\colors.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<resources>");
                    yaz.WriteLine("\t<color name=\"colorPrimary\">#646464</color>");
                    yaz.WriteLine("\t<color name=\"colorPrimaryDark\">#000000</color>");
                    yaz.WriteLine("\t<color name=\"colorAccent\">#FFFFFF</color>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<color name=\"coloraqua\">#00FFFF</color>");
                    yaz.WriteLine("\t<color name=\"colorblack\">#000000</color>");
                    yaz.WriteLine("\t<color name=\"colorblue\">#0000FF</color>");
                    yaz.WriteLine("\t<color name=\"colorfuchsia\">#FF00FF</color>");
                    yaz.WriteLine("\t<color name=\"colorgray\">#808080</color>");
                    yaz.WriteLine("\t<color name=\"colorgreen\">#008000</color>");
                    yaz.WriteLine("\t<color name=\"colorlime\">#00FF00</color>");
                    yaz.WriteLine("\t<color name=\"colormaroon\">#800000</color>");
                    yaz.WriteLine("\t<color name=\"colornavy\">#000080</color>");
                    yaz.WriteLine("\t<color name=\"colorolive\">#808000</color>");
                    yaz.WriteLine("\t<color name=\"colorpurple\">#800080</color>");
                    yaz.WriteLine("\t<color name=\"colorred\">#FF0000</color>");
                    yaz.WriteLine("\t<color name=\"colorsilver\">#C0C0C0</color>");
                    yaz.WriteLine("\t<color name=\"colorteal\">#008080</color>");
                    yaz.WriteLine("\t<color name=\"colorwhite\">#FFFFFF</color>");
                    yaz.WriteLine("\t<color name=\"coloryellow\">#FFFF00</color>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<!--TDMenu-->");
                    yaz.WriteLine("\t<color name=\"menuSeviye1\">#000000</color>");
                    yaz.WriteLine("\t<color name=\"menuTextSeviye1\">#ffffff</color>");
                    yaz.WriteLine("\t<color name=\"menuSeviye2\">#1d1d1d</color>");
                    yaz.WriteLine("\t<color name=\"menuTextSeviye2\">#ffffff</color>");
                    yaz.WriteLine("\t<color name=\"menuSeviye3\">#313131</color>");
                    yaz.WriteLine("\t<color name=\"menuTextSeviye3\">#ffffff</color>");
                    yaz.WriteLine("\t<color name=\"menuSeviye4\">#434343</color>");
                    yaz.WriteLine("\t<color name=\"menuTextSeviye4\">#ffffff</color>");
                    yaz.WriteLine("\t<color name=\"menuSeviye5\">#505050</color>");
                    yaz.WriteLine("\t<color name=\"menuTextSeviye5\">#ffffff</color>");
                    yaz.WriteLine("\t<color name=\"menuSeviye6\">#646464</color>");
                    yaz.WriteLine("\t<color name=\"menuTextSeviye6\">#ffffff</color>");
                    yaz.WriteLine("\t<color name=\"menuSeviye7\">#767676</color>");
                    yaz.WriteLine("\t<color name=\"menuTextSeviye7\">#1d1d1d</color>");
                    yaz.WriteLine("\t<color name=\"menuSeviye8\">#898989</color>");
                    yaz.WriteLine("\t<color name=\"menuTextSeviye8\">#1d1d1d</color>");
                    yaz.WriteLine("\t<color name=\"menuSeviye9\">#a4a4a4</color>");
                    yaz.WriteLine("\t<color name=\"menuTextSeviye9\">#1d1d1d</color>");
                    yaz.WriteLine("\t<color name=\"menuSeviye10\">#ffffff</color>");
                    yaz.WriteLine("\t<color name=\"menuTextSeviye10\">#000000</color>");
                    yaz.WriteLine("</resources>");
                }
            }

            //dimens
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\values\\dimens.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<resources>");
                    yaz.WriteLine("\t<dimen name=\"navbar_yatay_margin\">16dp</dimen>");
                    yaz.WriteLine("\t<dimen name=\"navbar_dikey_margin\">16dp</dimen>");
                    yaz.WriteLine("\t<dimen name=\"navbar_ust_bosluk\">8dp</dimen>");
                    yaz.WriteLine("\t<dimen name=\"navbar_ust_yukseklik\">176dp</dimen>");
                    yaz.WriteLine("</resources>");
                }
            }

            //styles
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\values\\styles.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<resources>");
                    yaz.WriteLine("\t<style name=\"" + projectName + "Theme\" parent=\"Theme.AppCompat\">");
                    yaz.WriteLine("\t\t<item name=\"colorPrimary\">@color/colorPrimary</item>");
                    yaz.WriteLine("\t\t<item name=\"colorPrimaryDark\">@color/colorPrimaryDark</item>");
                    yaz.WriteLine("\t\t<item name=\"colorAccent\">@color/colorAccent</item>");
                    yaz.WriteLine("\t</style>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<style name=\"" + projectName + "Theme.NoActionBar\">");
                    yaz.WriteLine("\t\t<item name=\"windowActionBar\">false</item>");
                    yaz.WriteLine("\t\t<item name=\"windowNoTitle\">true</item>");
                    yaz.WriteLine("\t</style>");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<style name=\"" + projectName + "Theme.AppBarOverlay\" parent=\"ThemeOverlay.AppCompat.Dark.ActionBar\" />");
                    yaz.WriteLine("\t<style name=\"" + projectName + "Theme.PopupOverlay\" parent=\"ThemeOverlay.AppCompat.Light\" />");
                    yaz.WriteLine("</resources>");
                }
            }

            //styles-v21
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\values-v21\\styles.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<resources>");
                    yaz.WriteLine("\t<style name=\"" + projectName + "Theme.NoActionBar\">");
                    yaz.WriteLine("\t\t<item name=\"windowActionBar\">false</item>");
                    yaz.WriteLine("\t\t<item name=\"windowNoTitle\">true</item>");
                    yaz.WriteLine("\t\t<item name=\"android:statusBarColor\">@android:color/transparent</item>");
                    yaz.WriteLine("\t</style>");
                    yaz.WriteLine("</resources>");
                }
            }

            //sagmenu
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\res\\menu\\sagmenu.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<menu xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    yaz.WriteLine("\txmlns:app=\"http://schemas.android.com/apk/res-auto\">");
                    yaz.WriteLine("");
                    yaz.WriteLine("\t<item");
                    yaz.WriteLine("\t\tandroid:id=\"@+id/menuAnaSayfa\"");
                    yaz.WriteLine("\t\tandroid:orderInCategory=\"1\"");
                    yaz.WriteLine("\t\tandroid:title=\"@string/menuAnasayfaBaslik\"");
                    yaz.WriteLine("\t\tapp:showAsAction=\"never\" />");
                    yaz.WriteLine("\t<item");
                    yaz.WriteLine("\t\tandroid:id=\"@+id/menuCikis\"");
                    yaz.WriteLine("\t\tandroid:orderInCategory=\"2\"");
                    yaz.WriteLine("\t\tandroid:title=\"@string/menuCikisBaslik\"");
                    yaz.WriteLine("\t\tapp:showAsAction=\"never\" />");
                    yaz.WriteLine("</menu>");
                }
            }
        }

        void AndManifestOlustur()
        {
            string projectNameKucuk = projectName.ToLower();
            bool izinVar = false;

            //Manifest
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\AndroidManifest.xml", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    yaz.WriteLine("<manifest xmlns:android=\"http://schemas.android.com/apk/res/android\"");
                    yaz.WriteLine("\tpackage=\"com.sinasalik.thrashead." + projectNameKucuk + "\">");
                    yaz.WriteLine("");

                    foreach (object item in lstAndIzin.SelectedItems)
                    {
                        if (item.ToString().IndexOf('.') >= 0)
                        {
                            string[] splitIzin = item.ToString().Split('.');
                            yaz.WriteLine("\t<uses-permission android:name=\"com.android." + splitIzin[1] + "permission." + splitIzin[0] + "\" />");
                        }
                        else
                        {
                            yaz.WriteLine("\t<uses-permission android:name=\"android.permission." + item.ToString() + "\" />");
                        }

                        izinVar = true;
                    }

                    if (izinVar)
                    {
                        yaz.WriteLine("");
                    }

                    yaz.WriteLine("\t<application");
                    yaz.WriteLine("\t\tandroid:allowBackup=\"true\"");
                    yaz.WriteLine("\t\tandroid:icon=\"@mipmap/ic_launcher\"");
                    yaz.WriteLine("\t\tandroid:label=\"@string/app_name\"");
                    yaz.WriteLine("\t\tandroid:roundIcon=\"@mipmap/ic_launcher_round\"");
                    yaz.WriteLine("\t\tandroid:supportsRtl=\"true\"");
                    yaz.WriteLine("\t\tandroid:theme=\"@style/" + projectName + "Theme\">");
                    yaz.WriteLine("\t\t<activity");
                    yaz.WriteLine("\t\t\tandroid:name=\".AktiviteSayac\"");
                    yaz.WriteLine("\t\t\tandroid:label=\"@string/app_name\"");
                    yaz.WriteLine("\t\t\tandroid:theme=\"@style/" + projectName + "Theme.NoActionBar\">");
                    yaz.WriteLine("\t\t\t<intent-filter>");
                    yaz.WriteLine("\t\t\t\t<action android:name=\"android.intent.action.MAIN\" />");
                    yaz.WriteLine("\t\t\t\t<category android:name=\"android.intent.category.LAUNCHER\" />");
                    yaz.WriteLine("\t\t\t</intent-filter>");
                    yaz.WriteLine("\t\t</activity>");
                    yaz.WriteLine("\t\t<activity android:name=\".AktiviteGiris\" android:theme=\"@style/" + projectName + "Theme.NoActionBar\" />");
                    yaz.WriteLine("");

                    foreach (string Table in selectedTables)
                    {
                        yaz.WriteLine("\t\t<activity android:name=\".Admin." + Table + "." + Table + "Sayfa\" android:theme=\"@style/" + projectName + "Theme.NoActionBar\" />");
                    }

                    yaz.WriteLine("\t</application>");
                    yaz.WriteLine("</manifest>");

                    yaz.Close();
                }
            }
        }

        void AndGradleOlustur()
        {
            string projectNameKucuk = projectName.ToLower();

            //Gradle
            using (FileStream fs = new FileStream(PathAddress + "\\" + projectName + "\\Android\\gradle.txt", FileMode.Create))
            {
                using (StreamWriter yaz = new StreamWriter(fs, Encoding.UTF8))
                {
                    yaz.WriteLine("apply plugin: 'com.android.application'");
                    yaz.WriteLine("");
                    yaz.WriteLine("android {");
                    yaz.WriteLine("\tcompileSdkVersion 27");
                    yaz.WriteLine("\tdefaultConfig {");
                    yaz.WriteLine("\t\tapplicationId \"com.sinasalik.thrashead." + projectNameKucuk + "\"");
                    yaz.WriteLine("\t\tminSdkVersion 16");
                    yaz.WriteLine("\t\ttargetSdkVersion 27");
                    yaz.WriteLine("\t\tversionCode 1");
                    yaz.WriteLine("\t\tversionName \"1.0\"");
                    yaz.WriteLine("\t\ttestInstrumentationRunner \"android.support.test.runner.AndroidJUnitRunner\"");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("\tbuildTypes {");
                    yaz.WriteLine("\t\trelease {");
                    yaz.WriteLine("\t\t\tminifyEnabled false");
                    yaz.WriteLine("\t\t\tproguardFiles getDefaultProguardFile('proguard-android.txt'), 'proguard-rules.pro'");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("\tsourceSets {");
                    yaz.WriteLine("\t\tmain {");
                    yaz.WriteLine("\t\t\tres.srcDirs = [");

                    foreach (string table in selectedTables)
                    {
                        string _table = table.ToHyperLinkText(true);

                        yaz.WriteLine("\t\t\t\t\t'src/main/res/layouts/" + _table + "',");
                    }

                    yaz.WriteLine("\t\t\t\t\t'src/main/res/layouts/navbar',");
                    yaz.WriteLine("\t\t\t\t\t'src/main/res',");
                    yaz.WriteLine("\t\t\t]");
                    yaz.WriteLine("\t\t}");
                    yaz.WriteLine("\t}");
                    yaz.WriteLine("}");
                    yaz.WriteLine("");
                    yaz.WriteLine("dependencies {");
                    yaz.WriteLine("\timplementation fileTree(dir: 'libs', include: ['*.jar'])");
                    yaz.WriteLine("\timplementation 'com.android.support:appcompat-v7:27.0.2'");
                    yaz.WriteLine("\timplementation 'com.android.support.constraint:constraint-layout:1.0.2'");
                    yaz.WriteLine("\timplementation 'com.android.support:support-v4:27.0.2'");
                    yaz.WriteLine("\timplementation 'com.android.support:design:27.0.2'");
                    yaz.WriteLine("\ttestImplementation 'junit:junit:4.12'");
                    yaz.WriteLine("\tandroidTestImplementation 'com.android.support.test:runner:1.0.1'");
                    yaz.WriteLine("\tandroidTestImplementation 'com.android.support.test.espresso:espresso-core:3.0.1'");
                    yaz.WriteLine("\timplementation files('libs/tdmenu.aar')");
                    yaz.WriteLine("\timplementation files('libs/tdlibrary.aar')");
                    yaz.WriteLine("\timplementation files('libs/tdframework.aar')");

                    if (rdAndWcf.Checked)
                    {
                        yaz.WriteLine("");
                        yaz.WriteLine("\timplementation 'org.jbundle.util.osgi.wrapped:org.jbundle.util.osgi.wrapped.org.apache.http.client:4.1.2'");
                        yaz.WriteLine("\timplementation 'com.google.code.gson:gson:2.8.5'");
                    }

                    yaz.WriteLine("}");
                }
            }
        }

        #endregion
    }
}