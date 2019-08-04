using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TDFactory.Helper;

namespace TDFactory
{
    public partial class TDFactory : Form
    {
        #region Katman

        //Web
        private void btnVeriTabaniGetir_Click(object sender, EventArgs e)
        {
            cmbVeritabani.DataSource = null;
            ClearLabel();
            ClearListBox();

            try
            {
                cmbVeritabani.DataSource = Helper.Helper.DatabaseNames(new ConnectionInfo() { IsWindowsAuthentication = chkWindowsAuthentication.Checked, DatabaseName = cmbVeritabani.Text, Username = txtKullaniciAdi.Text, Password = txtSifre.Text, Server = txtSunucu.Text });
            }
            catch
            {
                MessageBox.Show("Bağlantı Sağlanamadı");

                if (cmbVeritabani.Items.Count <= 0)
                {
                    cmbVeritabani.Enabled = false;
                    btnBaglan.Enabled = false;
                }
            }

            if (cmbVeritabani.Items.Count > 0)
            {
                cmbVeritabani.Enabled = true;
                btnBaglan.Enabled = true;
            }

            lstSeciliTablolar.Enabled = false;
            lstKolonlar.Enabled = false;
            btnTabloCikar.Enabled = false;
            btnTablolarCikar.Enabled = false;

            lstTablolar.Enabled = false;
            btnTabloEkle.Enabled = false;
            btnTablolarEkle.Enabled = false;
            btnTabloTumunuSec.Enabled = false;
            btnTabloTumunuBirak.Enabled = false;

            lstSeciliKolonlar.Enabled = false;
            btnKolonCikar.Enabled = false;
            btnKolonlarCikar.Enabled = false;
            btnKolonTumunuSec.Enabled = false;
            btnKolonTumunuBirak.Enabled = false;

            lstKolonlar.Enabled = false;
            btnKolonEkle.Enabled = false;
            btnKolonlarEkle.Enabled = false;

            btnButunTabloAlanlar.Enabled = false;

            btnVTVeritabaniGetir_Click();
        }

        private void btnBaglan_Click(object sender, EventArgs e)
        {
            if (cmbVeritabani.Text != "")
            {
                try
                {
                    tableNames = new List<string>();

                    lstTablolar.Items.Clear();
                    lstKolonlar.Items.Clear();
                    ClearLabel();
                    ClearListBox();

                    if (chkWindowsAuthentication.Checked == true)
                    {
                        tableNames = Helper.Helper.TableNames(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text });
                    }
                    else
                    {
                        tableNames = Helper.Helper.TableNames(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text, IsWindowsAuthentication = false, Username = txtKullaniciAdi.Text, Password = txtSifre.Text });
                    }

                    DBName = cmbVeritabani.Text;
                    txtProjectName.Text = cmbVeritabani.Text;
                    txtProjectName.Text = chkAngular.Checked ? txtProjectName.Text + "Angular" : txtProjectName.Text;

                    foreach (string item in tableNames)
                    {
                        lstTablolar.Items.Add(item);
                    }
                }
                catch
                {
                    MessageBox.Show("Bağlantı Sağlanamadı");
                }
            }

            if (lstTablolar.Items.Count > 0)
            {
                lstTablolar.Enabled = true;
                btnTabloTumunuSec.Enabled = true;
                btnTabloTumunuBirak.Enabled = true;
                btnTabloEkle.Enabled = true;
                btnTablolarEkle.Enabled = true;
                btnButunTabloAlanlar.Enabled = true;

                lstSeciliTablolar.Enabled = false;
                lstKolonlar.Enabled = false;
                btnTabloCikar.Enabled = false;
                btnTablolarCikar.Enabled = false;

                lstSeciliKolonlar.Enabled = false;
                btnKolonCikar.Enabled = false;
                btnKolonlarCikar.Enabled = false;
                btnKolonTumunuSec.Enabled = false;
                btnKolonTumunuBirak.Enabled = false;

                lstKolonlar.Enabled = false;
                btnKolonEkle.Enabled = false;
                btnKolonlarEkle.Enabled = false;
            }

            btnButunTabloAlanlar_Click(null, null);
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            projectName = !String.IsNullOrEmpty(txtProjectName.Text) ? txtProjectName.Text : "ProjectName";
            projectName = projectName.Replace(" ", "");

            connectionInfo = new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text, IsWindowsAuthentication = chkWindowsAuthentication.Checked, Username = txtKullaniciAdi.Text, Password = txtSifre.Text };

            folderDialogKatmanOlustur.SelectedPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (folderDialogKatmanOlustur.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathAddress = folderDialogKatmanOlustur.SelectedPath;

                if (!String.IsNullOrEmpty(DBName))
                {
                    tableColumnInfos = GetTableColumnInfos();
                    selectedTables = GetSelectedTableNames(tableColumnInfos);

                    UrlColumns = lstUrlColumns.Items.ToStringList();
                    GuidColumns = lstGuidColumns.Items.ToStringList();
                    DeletedColumns = lstDeletedColumns.Items.ToStringList();
                    FileColumns = lstFileColumns.Items.ToStringList();
                    ImageColumns = lstImageColumns.Items.ToStringList();
                    CodeColumns = lstCodeColumns.Items.ToStringList();

                    if (chkAngular.Checked)
                    {
                        CreateAngular();
                    }
                    else
                    {
                        CreateMVC();
                    }

                    MessageBox.Show("Katmanlar Başarıyla Oluşturuldu.");

                    if (chkKlasorAc.Checked)
                    {
                        try
                        {
                            Process.Start(folderDialogKatmanOlustur.SelectedPath + "\\" + projectFolder);
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

        private void cmbVeritabani_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void chkWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWindowsAuthentication.Checked == false)
            {
                txtKullaniciAdi.Enabled = true;
                txtSifre.Enabled = true;
            }
            else
            {
                txtKullaniciAdi.Enabled = false;
                txtSifre.Enabled = false;
            }

            txtKullaniciAdi.Text = "";
            txtSifre.Text = "";
            ClearLabel();
        }

        private void lstSeciliTablolar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSeciliTablolar.SelectedIndex >= 0)
            {
                selectedtableindex = lstSeciliTablolar.SelectedIndex;
            }

            lstKolonlar.Items.Clear();
            ClearLabel();

            if (lstSeciliTablolar.SelectedIndex == tableindex && tableselected == true)
            {
                lstSeciliTablolar.SelectedIndex = -1;
            }
            else
            {
                if (chkWindowsAuthentication.Checked == true)
                {
                    columnInfos = Helper.Helper.GetColumnsInfo(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text }, lstSeciliTablolar.Text);
                }
                else
                {
                    columnInfos = Helper.Helper.GetColumnsInfo(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text, IsWindowsAuthentication = false, Username = txtKullaniciAdi.Text, Password = txtSifre.Text }, lstSeciliTablolar.Text);
                }

                foreach (ColumnInfo item in columnInfos)
                {
                    lstKolonlar.Items.Add(item.ColumnName);
                }

                Tekrar:
                foreach (object item in lstKolonlar.Items)
                {
                    if (lstSeciliKolonlar.Items.Contains(item.ToString().ColumnWithTable(lstSeciliTablolar.SelectedItem.ToString())))
                    {
                        lstKolonlar.Items.Remove(item);
                        goto Tekrar;
                    }
                }

                tableselected = true;
            }

            tableindex = lstSeciliTablolar.SelectedIndex;

            if (lstKolonlar.Items.Count > 0)
            {
                lstKolonlar.Enabled = true;
                btnKolonEkle.Enabled = true;
                btnKolonlarEkle.Enabled = true;
            }
            else
            {
                lstKolonlar.Enabled = false;
                btnKolonEkle.Enabled = false;
                btnKolonlarEkle.Enabled = false;
            }
        }

        private void lstSeciliKolonlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItems.Count > 0)
            {
                selectedcolumn = selectedcolumn ?? lstSeciliKolonlar.SelectedItem.ToString();

                lstSeciliTablolar.SelectedIndex = lstSeciliTablolar.Items.IndexOf(selectedcolumn.TableName());
            }
        }

        private void lstSeciliKolonlar_MouseClick(object sender, MouseEventArgs e)
        {
            if (lstSeciliKolonlar.IndexFromPoint(e.Location) > -1)
            {
                selectedcolumn = lstSeciliKolonlar.Items[lstSeciliKolonlar.IndexFromPoint(e.Location)].ToString();
            }
        }

        private void btnTabloEkle_Click(object sender, EventArgs e)
        {
            if (lstTablolar.SelectedIndex >= 0)
            {
                foreach (object item in lstTablolar.SelectedItems)
                {
                    lstSeciliTablolar.Items.Add(item);
                }

                foreach (object item in lstSeciliTablolar.Items)
                {
                    lstTablolar.Items.Remove(item);
                }

                lstSeciliTablolar.Enabled = true;
                lstKolonlar.Enabled = true;
                btnTabloCikar.Enabled = true;
                btnTablolarCikar.Enabled = true;

                if (lstTablolar.Items.Count <= 0)
                {
                    lstTablolar.Enabled = false;
                    btnTabloEkle.Enabled = false;
                    btnTablolarEkle.Enabled = false;
                    btnTabloTumunuSec.Enabled = false;
                    btnTabloTumunuBirak.Enabled = false;
                }
            }
        }

        private void btnTablolarEkle_Click(object sender, EventArgs e)
        {
            foreach (object item in lstTablolar.Items)
            {
                lstSeciliTablolar.Items.Add(item);
            }

            lstTablolar.Items.Clear();

            lstSeciliTablolar.Enabled = true;
            lstKolonlar.Enabled = true;
            btnTabloCikar.Enabled = true;
            btnTablolarCikar.Enabled = true;

            lstTablolar.Enabled = false;
            btnTabloEkle.Enabled = false;
            btnTablolarEkle.Enabled = false;
            btnTabloTumunuSec.Enabled = false;
            btnTabloTumunuBirak.Enabled = false;
        }

        private void btnTabloCikar_Click(object sender, EventArgs e)
        {
            if (lstSeciliTablolar.SelectedIndex >= 0)
            {
                tekrar:;
                foreach (object item in lstSeciliKolonlar.Items)
                {
                    if (item.ToString().Split('[')[1].Replace("]", "") == lstSeciliTablolar.SelectedItem.ToString())
                    {
                        lstSeciliKolonlar.Items.Remove(item);
                        goto tekrar;
                    }
                }

                lstTablolar.Items.Add(lstSeciliTablolar.SelectedItem);

                lstSeciliTablolar.Items.Remove(lstSeciliTablolar.SelectedItem);

                if (lstSeciliTablolar.Items.Count <= 0)
                {
                    lstKolonlar.Items.Clear();
                    lstSeciliKolonlar.Items.Clear();

                    lstSeciliTablolar.Enabled = false;
                    lstKolonlar.Enabled = false;
                    btnTabloCikar.Enabled = false;
                    btnTablolarCikar.Enabled = false;

                    btnKolonEkle.Enabled = false;
                    btnKolonCikar.Enabled = false;
                    btnKolonlarEkle.Enabled = false;
                    btnKolonlarCikar.Enabled = false;
                    btnKolonTumunuSec.Enabled = false;
                    btnKolonTumunuBirak.Enabled = false;
                    lstSeciliKolonlar.Enabled = false;
                    btnKolonEkle.Enabled = false;

                    ClearLabel();

                    btnButunTabloAlanlar.Enabled = true;
                }

                lstTablolar.Enabled = true;
                btnTabloTumunuSec.Enabled = true;
                btnTabloTumunuBirak.Enabled = true;
                btnTabloEkle.Enabled = true;
                btnTablolarEkle.Enabled = true;

                btnButunTabloAlanlar.Enabled = true;
            }
        }

        private void btnTablolarCikar_Click(object sender, EventArgs e)
        {
            foreach (object item in lstSeciliTablolar.Items)
            {
                lstTablolar.Items.Add(item);
            }

            lstSeciliTablolar.Items.Clear();
            lstKolonlar.Items.Clear();
            lstSeciliKolonlar.Items.Clear();

            lstSeciliTablolar.Enabled = false;
            lstKolonlar.Enabled = false;
            btnTabloCikar.Enabled = false;
            btnTablolarCikar.Enabled = false;

            btnKolonEkle.Enabled = false;
            btnKolonCikar.Enabled = false;
            btnKolonlarEkle.Enabled = false;
            btnKolonlarCikar.Enabled = false;
            btnKolonTumunuSec.Enabled = false;
            btnKolonTumunuBirak.Enabled = false;
            lstSeciliKolonlar.Enabled = false;
            btnKolonEkle.Enabled = false;

            lstTablolar.Enabled = true;
            btnTabloEkle.Enabled = true;
            btnTablolarEkle.Enabled = true;
            btnTabloTumunuSec.Enabled = true;
            btnTabloTumunuBirak.Enabled = true;

            ClearLabel();

            btnButunTabloAlanlar.Enabled = true;
        }

        private void btnKolonEkle_Click(object sender, EventArgs e)
        {
            Tekrar:
            if (lstSeciliTablolar.SelectedIndex >= 0)
            {
                if (lstKolonlar.SelectedIndex >= 0)
                {
                    lstSeciliKolonlar.Items.Add(lstKolonlar.SelectedItem.ToString().ColumnWithTable(lstSeciliTablolar.SelectedItem.ToString()));
                    lstKolonlar.Items.Remove(lstKolonlar.SelectedItem);

                    lstSeciliKolonlar.Enabled = true;
                    btnKolonCikar.Enabled = true;
                    btnKolonlarCikar.Enabled = true;
                    btnKolonTumunuSec.Enabled = true;
                    btnKolonTumunuBirak.Enabled = true;
                }
            }
            else
            {
                object selecteditem = lstKolonlar.SelectedItem;
                lstSeciliTablolar.SelectedIndex = selectedtableindex;
                lstKolonlar.SelectedIndex = lstKolonlar.Items.IndexOf(selecteditem);
                goto Tekrar;
            }
        }

        private void btnKolonlarEkle_Click(object sender, EventArgs e)
        {
            Tekrar:
            if (lstSeciliTablolar.SelectedIndex >= 0)
            {
                foreach (object item in lstKolonlar.Items)
                {
                    lstSeciliKolonlar.Items.Add(item.ToString().ColumnWithTable(lstSeciliTablolar.SelectedItem.ToString()));
                }

                lstKolonlar.Items.Clear();

                lstSeciliKolonlar.Enabled = true;
                btnKolonCikar.Enabled = true;
                btnKolonlarCikar.Enabled = true;
                btnKolonTumunuSec.Enabled = true;
                btnKolonTumunuBirak.Enabled = true;

                lstKolonlar.Enabled = false;
                btnKolonEkle.Enabled = false;
                btnKolonlarEkle.Enabled = false;
            }
            else
            {
                lstSeciliTablolar.SelectedIndex = selectedtableindex;
                goto Tekrar;
            }
        }

        private void btnKolonCikar_Click(object sender, EventArgs e)
        {
            if (lstSeciliKolonlar.SelectedIndex >= 0)
            {
                Tekrar:
                foreach (object item in lstSeciliKolonlar.SelectedItems)
                {
                    lstSeciliKolonlar.Items.Remove(item.ToString());
                    goto Tekrar;
                }

                if (lstKolonlar.Items.Count > 0)
                {
                    lstKolonlar.Enabled = true;
                    btnKolonEkle.Enabled = true;
                    btnKolonlarEkle.Enabled = true;
                }
                else
                {
                    lstKolonlar.Enabled = false;
                    btnKolonEkle.Enabled = false;
                    btnKolonlarEkle.Enabled = false;
                }

                if (lstSeciliKolonlar.Items.Count <= 0)
                {
                    lstSeciliKolonlar.Enabled = false;
                    btnKolonCikar.Enabled = false;
                    btnKolonlarCikar.Enabled = false;
                    btnKolonTumunuSec.Enabled = false;
                    btnKolonTumunuBirak.Enabled = false;

                    ClearLabel();
                }

                btnButunTabloAlanlar.Enabled = true;

                lstSeciliTablolar.SelectedIndex = -1;
            }
        }

        private void btnKolonlarCikar_Click(object sender, EventArgs e)
        {
            lstSeciliTablolar.SelectedIndex = -1;
            lstSeciliKolonlar.Items.Clear();

            if (lstKolonlar.Items.Count > 0)
            {
                lstKolonlar.Enabled = true;
                btnKolonEkle.Enabled = true;
                btnKolonlarEkle.Enabled = true;
            }
            else
            {
                lstKolonlar.Enabled = false;
                btnKolonEkle.Enabled = false;
                btnKolonlarEkle.Enabled = false;
            }

            lstSeciliKolonlar.Enabled = false;
            btnKolonCikar.Enabled = false;
            btnKolonlarCikar.Enabled = false;
            btnKolonTumunuSec.Enabled = false;
            btnKolonTumunuBirak.Enabled = false;

            ClearLabel();

            btnButunTabloAlanlar.Enabled = true;
        }

        private void btnTabloTumunuSec_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstTablolar.Items.Count; i++)
            {
                lstTablolar.SetSelected(i, true);
            }
        }

        private void btnTabloTumunuBirak_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstTablolar.Items.Count; i++)
            {
                lstTablolar.SetSelected(i, false);
            }
        }

        private void btnButunTabloAlanlar_Click(object sender, EventArgs e)
        {

            lstSeciliKolonlar.Items.Clear();
            lstKolonlar.Items.Clear();

            foreach (object item in lstSeciliTablolar.Items)
            {
                lstTablolar.Items.Add(item);
            }

            lstSeciliTablolar.Items.Clear();

            ClearLabel();

            foreach (object item in lstTablolar.Items)
            {
                lstSeciliTablolar.Items.Add(item);

                if (chkWindowsAuthentication.Checked == true)
                {
                    columnInfos = Helper.Helper.GetColumnsInfo(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text }, item.ToString());
                }
                else
                {
                    columnInfos = Helper.Helper.GetColumnsInfo(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text, IsWindowsAuthentication = false, Username = txtKullaniciAdi.Text, Password = txtSifre.Text }, item.ToString());
                }

                lstKolonlar.Items.Clear();

                foreach (ColumnInfo item2 in columnInfos)
                {
                    lstKolonlar.Items.Add(item2.ColumnName);
                    lstSeciliKolonlar.Items.Add(item2.ColumnName.ColumnWithTable(item.ToString()));
                }
            }

            lstTablolar.Items.Clear();
            lstKolonlar.Items.Clear();

            btnButunTabloAlanlar.Enabled = false;

            lstTablolar.Enabled = false;
            btnTabloEkle.Enabled = false;
            btnTablolarEkle.Enabled = false;
            btnTabloTumunuSec.Enabled = false;
            btnTabloTumunuBirak.Enabled = false;
            btnKolonEkle.Enabled = false;
            btnKolonlarEkle.Enabled = false;

            lstSeciliTablolar.Enabled = true;
            lstKolonlar.Enabled = true;
            lstSeciliKolonlar.Enabled = true;
            btnTabloCikar.Enabled = true;
            btnTablolarCikar.Enabled = true;
            btnKolonCikar.Enabled = true;
            btnKolonlarCikar.Enabled = true;
            btnKolonTumunuSec.Enabled = true;
            btnKolonTumunuBirak.Enabled = true;
        }

        private void btnKolonTumunuSec_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSeciliKolonlar.Items.Count; i++)
            {
                lstSeciliKolonlar.SetSelected(i, true);
            }

            ClearLabel();
        }

        private void btnKolonTumunuBirak_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSeciliKolonlar.Items.Count; i++)
            {
                lstSeciliKolonlar.SetSelected(i, false);
            }

            ClearLabel();
        }

        private void chkMVCHepsi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMVCHepsi.Checked)
            {
                chkRepository.Checked = false;
                chkMVCController.Checked = false;
                chkMVCView.Checked = false;
                chkMVCWebConfig.Checked = false;
                chkMVCStoredProc.Checked = false;
                chkMVCStilScript.Checked = false;
                chkMVCWcfServis.Checked = false;
                chkMVCHepsi.Checked = true;
            }
        }

        private void chkMVCDiger_CheckedChanged(object sender, EventArgs e)
        {
            chkMVCHepsi.Checked = false;
        }

        private void txtProjectName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8)
            {
                if (e.KeyChar < 48 || e.KeyChar > 56)
                {
                    if (e.KeyChar < 65 || e.KeyChar > 90)
                    {
                        if (e.KeyChar < 97 || e.KeyChar > 122)
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        private void chkAngular_CheckedChanged(object sender, EventArgs e)
        {
            if(((CheckBox)sender).Checked) {
                if (!txtProjectName.Text.ToUrl(true).Contains("angular"))
                {
                    txtProjectName.Text = txtProjectName.Text + "Angular";
                }
            }
            else
            {
                if (txtProjectName.Text.ToUrl(true).Contains("angular"))
                {
                    txtProjectName.Text = txtProjectName.Text.Replace("Angular", "").Replace("angular", "").Replace("ANGULAR", "");
                }
            }
        }

        private void btnUrlColumns_Click(object sender, EventArgs e)
        {
            string column = txtUrlColumns.Text.Trim().Replace(" ", "").ToUrl(true);

            if (!String.IsNullOrEmpty(column))
            {
                if (!lstUrlColumns.Items.Contains(column))
                {
                    lstUrlColumns.Items.Add(column);
                }
            }
        }

        private void btnDeletedColumns_Click(object sender, EventArgs e)
        {
            string column = txtDeletedColumns.Text.Trim().Replace(" ", "").ToUrl(true);

            if (!String.IsNullOrEmpty(column))
            {
                if (!lstDeletedColumns.Items.Contains(column))
                {
                    lstDeletedColumns.Items.Add(column);
                }
            }
        }

        private void btnGuidColumns_Click(object sender, EventArgs e)
        {
            string column = txtGuidColumns.Text.Trim().Replace(" ", "").ToUrl(true);

            if (!String.IsNullOrEmpty(column))
            {
                if (!lstGuidColumns.Items.Contains(column))
                {
                    lstGuidColumns.Items.Add(column);
                }
            }
        }

        private void btnFileColumns_Click(object sender, EventArgs e)
        {
            string column = txtFileColumns.Text.Trim().Replace(" ", "").ToUrl(true);

            if (!String.IsNullOrEmpty(column))
            {
                if (!lstFileColumns.Items.Contains(column))
                {
                    lstFileColumns.Items.Add(column);
                }
            }
        }

        private void btnImageColumns_Click(object sender, EventArgs e)
        {
            string column = txtImageColumns.Text.Trim().Replace(" ", "").ToUrl(true);

            if (!String.IsNullOrEmpty(column))
            {
                if (!lstImageColumns.Items.Contains(column))
                {
                    lstImageColumns.Items.Add(column);
                }
            }
        }

        private void btnCodeColumns_Click(object sender, EventArgs e)
        {
            string column = txtCodeColumns.Text.Trim().Replace(" ", "").ToUrl(true);

            if (!String.IsNullOrEmpty(column))
            {
                if (!lstCodeColumns.Items.Contains(column))
                {
                    lstCodeColumns.Items.Add(column);
                }
            }
        }

        private void btnUrlColumnsSub_Click(object sender, EventArgs e)
        {
            lstUrlColumns.Items.Remove(lstUrlColumns.SelectedItem);
        }

        private void btnDeletedColumnsSub_Click(object sender, EventArgs e)
        {
            lstDeletedColumns.Items.Remove(lstDeletedColumns.SelectedItem);
        }

        private void btnGuidColumnsSub_Click(object sender, EventArgs e)
        {
            lstGuidColumns.Items.Remove(lstGuidColumns.SelectedItem);
        }

        private void btnFileColumnsSub_Click(object sender, EventArgs e)
        {
            lstFileColumns.Items.Remove(lstFileColumns.SelectedItem);
        }

        private void btnImageColumnsSub_Click(object sender, EventArgs e)
        {
            lstImageColumns.Items.Remove(lstImageColumns.SelectedItem);
        }

        private void btnCodeColumnsSub_Click(object sender, EventArgs e)
        {
            lstCodeColumns.Items.Remove(lstCodeColumns.SelectedItem);
        }

        void ClearLabel()
        {
            lblKolonAdi.Text = "";
            lblDataTipi.Text = "";
            lblMaksimumKarakter.Text = "";
            lblSiraNo.Text = "";
            lblVarsayilanDeger.Text = "";
            lblBosVeri.Text = "";
        }

        void ClearListBox()
        {
            lstTablolar.Items.Clear();
            lstSeciliTablolar.Items.Clear();
            lstKolonlar.Items.Clear();
            lstSeciliKolonlar.Items.Clear();
        }


        //Android
        private void btnAndroidBaslat_Click(object sender, EventArgs e)
        {
            projectName = !String.IsNullOrEmpty(txtProjectName.Text) ? txtProjectName.Text : "Proje";
            projectName = projectName.Replace(" ", "");

            connectionInfo = new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text, IsWindowsAuthentication = chkWindowsAuthentication.Checked, Username = txtKullaniciAdi.Text, Password = txtSifre.Text };

            folderDialogKatmanOlustur.SelectedPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (folderDialogKatmanOlustur.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(DBName))
                {
                    PathAddress = folderDialogKatmanOlustur.SelectedPath;

                    tableColumnInfos = GetTableColumnInfos();
                    selectedTables = GetSelectedTableNames(tableColumnInfos);

                    CreateAndroidDirectories();
                    CreateAndroidGradle();

                    if (chkAndHepsi.Checked)
                    {
                        CreateAndroidManifest();
                        CreateAndroidLayout();
                        CreateAndroidValues();
                        CreateAndroidJava();
                        CreateAndroidModel();

                        if (rdAndSqlite.Checked)
                        {
                            CreateAndroidDataSQLite();
                        }
                        else
                        {
                            CreateAndroidDataWCF();
                        }
                    }
                    else
                    {
                        if (chkAndManifest.Checked)
                        {
                            CreateAndroidManifest();
                        }

                        if (chkAndLayout.Checked)
                        {
                            CreateAndroidLayout();
                            CreateAndroidValues();
                        }

                        if (chkAndJava.Checked)
                        {
                            CreateAndroidJava();
                        }

                        if (chkAndModel.Checked)
                        {
                            CreateAndroidModel();
                        }

                        if (chkAndData.Checked)
                        {
                            if (rdAndSqlite.Checked)
                            {
                                CreateAndroidDataSQLite();
                            }
                            else
                            {
                                CreateAndroidDataWCF();
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

        #endregion
    }
}
