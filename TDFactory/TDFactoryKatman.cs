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
                    tableColumnNames = GetTableColumnNames();
                    selectedTables = GetSelectedTableNames(tableColumnNames);

                    if (chkMVCModel.Checked || chkMVCHepsi.Checked)
                    {
                        CreateMVCLayers(true);
                    }
                    else
                    {
                        CreateMVCLayers(false);
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
                    columnNames = Helper.Helper.ColumnNames(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text }, lstSeciliTablolar.Text);
                }
                else
                {
                    columnNames = Helper.Helper.ColumnNames(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text, IsWindowsAuthentication = false, Username = txtKullaniciAdi.Text, Password = txtSifre.Text }, lstSeciliTablolar.Text);
                }

                foreach (ColumnInfo item in columnNames)
                {
                    lstKolonlar.Items.Add(item.ColumnName);
                }

                Tekrar:
                foreach (object item in lstKolonlar.Items)
                {
                    if (lstSeciliKolonlar.Items.Contains(item.ToString().TabloluKolon(lstSeciliTablolar.SelectedItem.ToString())))
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

        private void lstKolonlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstKolonlar.SelectedIndex == columnindex && columnselected == true)
            {
                lstKolonlar.SelectedIndex = -1;
            }
            else
            {
                ColumnInfo columnInfo = columnNames.Where(a => a.ColumnName == lstKolonlar.Text).FirstOrDefault();

                if (columnInfo != null)
                {
                    lblKolonAdi.Text = columnInfo.ColumnName == "" ? "-" : columnInfo.ColumnName;
                    lblKolonAdi.Text = columnInfo.IsPrimaryKey == "" ? lblKolonAdi.Text : lblKolonAdi.Text + " (" + columnInfo.IsPrimaryKey + ")";
                    lblDataTipi.Text = columnInfo.DataType == "" ? "-" : columnInfo.DataType;
                    lblMaksimumKarakter.Text = columnInfo.MaxLength == "" ? "-" : columnInfo.MaxLength == "-1" ? "Sınırsız" : columnInfo.MaxLength;
                    lblSiraNo.Text = columnInfo.OrdinalPosition == "" ? "-" : columnInfo.OrdinalPosition;
                    lblVarsayilanDeger.Text = columnInfo.DefaultValue == "" ? "-" : columnInfo.DefaultValue;
                    lblBosVeri.Text = columnInfo.IsNullable == "" ? "-" : columnInfo.IsNullable == "YES" ? "Var" : "Yok";
                }
                else
                {
                    ClearLabel();
                }

                columnselected = true;
            }

            columnindex = lstKolonlar.SelectedIndex;
        }

        private void lstSeciliKolonlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ListBox)sender).SelectedItems.Count > 0)
            {
                selectedcolumn = selectedcolumn ?? lstSeciliKolonlar.SelectedItem.ToString();

                lstSeciliTablolar.SelectedIndex = lstSeciliTablolar.Items.IndexOf(selectedcolumn.TabloAdi());
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
                    lstSeciliKolonlar.Items.Add(lstKolonlar.SelectedItem.ToString().TabloluKolon(lstSeciliTablolar.SelectedItem.ToString()));
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
                    lstSeciliKolonlar.Items.Add(item.ToString().TabloluKolon(lstSeciliTablolar.SelectedItem.ToString()));
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
                    columnNames = Helper.Helper.ColumnNames(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text }, item.ToString());
                }
                else
                {
                    columnNames = Helper.Helper.ColumnNames(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text, IsWindowsAuthentication = false, Username = txtKullaniciAdi.Text, Password = txtSifre.Text }, item.ToString());
                }

                lstKolonlar.Items.Clear();

                foreach (ColumnInfo item2 in columnNames)
                {
                    lstKolonlar.Items.Add(item2.ColumnName);
                    lstSeciliKolonlar.Items.Add(item2.ColumnName.TabloluKolon(item.ToString()));
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
                chkMVCModel.Checked = false;
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

        #endregion
    }
}
