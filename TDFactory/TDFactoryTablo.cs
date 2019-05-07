using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using TDFactory.Helper;

namespace TDFactory
{
    public partial class TDFactory : Form
    {
        #region Tablo

        private void cmbVTVeriTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVTVeriTipi.Text == "int" || cmbVTVeriTipi.Text == "bigint" || cmbVTVeriTipi.Text == "decimal" || cmbVTVeriTipi.Text == "numeric" || cmbVTVeriTipi.Text == "smallint" || cmbVTVeriTipi.Text == "tinyint")
            {
                chkVTAutoIncrement.Enabled = true;
            }
            else
            {
                chkVTAutoIncrement.Checked = false;
                chkVTAutoIncrement.Enabled = false;
                numVTArtisMiktari.Value = 1;
                numVTBaslangicSayisi.Value = 1;
            }

            if (txtVTKolonAdi.Text.Length > 0 && cmbVTVeriTipi.SelectedIndex >= 0)
            {
                btnVTKolonEkle.Enabled = true;
            }
            else
            {
                btnVTKolonEkle.Enabled = false;
            }
        }

        private void btnVTVeritabaniGetir_Click(object sender, EventArgs e)
        {
            ClearTableControls();
            lstVTKolonlar.Items.Clear();
            btnVTTabloOlustur.Enabled = false;
            cmbVTVeritabani.DataSource = null;
            cmbVTTabloAdi.Items.Clear();
            txtVTTabloAdi.Visible = true;
            txtVTTabloAdi.Text = "";
            txtVTTabloAdi.Enabled = false;
            cmbVTTabloAdi.Visible = false;
            cmbVTTabloAdi.Enabled = false;
            chkVTTabloDuzenle.Enabled = false;
            chkVTTabloDuzenle.Checked = false;

            try
            {
                cmbVTVeritabani.DataSource = Helper.Helper.DatabaseNames(new ConnectionInfo() { IsWindowsAuthentication = chkVTWindowsAuthentication.Checked, DatabaseName = cmbVTVeritabani.Text, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text, Server = txtVTSunucu.Text });

                if (cmbVTVeritabani.Items.Count > 0)
                {
                    cmbVTVeritabani.Enabled = true;
                    txtVTTabloAdi.Enabled = true;
                    cmbVTTabloAdi.Enabled = true;
                    chkVTTabloDuzenle.Enabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Bağlantı Sağlanamadı");
                cmbVTVeritabani.Enabled = false;
            }
        }

        private void chkVTPrimaryKey_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVTPrimaryKey.Checked)
            {
                chkVTAllowNull.Checked = false;
                chkVTAllowNull.Enabled = false;
                txtVTVarsayilanDeger.Enabled = false;
            }
            else
            {
                if (chkVTAutoIncrement.Checked == false)
                {
                    chkVTAllowNull.Enabled = true;
                    txtVTVarsayilanDeger.Enabled = true;
                }
            }
        }

        private void chkVTAutoIncrement_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVTAutoIncrement.Checked)
            {
                numVTBaslangicSayisi.Enabled = true;
                numVTArtisMiktari.Enabled = true;
                chkVTAllowNull.Checked = false;
                chkVTAllowNull.Enabled = false;
                txtVTVarsayilanDeger.Text = "";
                txtVTVarsayilanDeger.Enabled = false;
            }
            else
            {
                numVTBaslangicSayisi.Enabled = false;
                numVTArtisMiktari.Enabled = false;
                numVTBaslangicSayisi.Value = 1;
                numVTArtisMiktari.Value = 1;
                if (chkVTPrimaryKey.Checked == false)
                {
                    chkVTAllowNull.Enabled = true;
                    txtVTVarsayilanDeger.Enabled = true;
                }
            }
        }

        private void btnVTVarsayilanDegerler_Click(object sender, EventArgs e)
        {
            ClearTableControls();
        }

        private void btnVTKolonEkle_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtVTKolonAdi.Text) && cmbVTVeriTipi.SelectedIndex >= 0)
            {
                foreach (object item in lstVTKolonlar.Items)
                {
                    if (item.ToString().Split('_')[0].Replace("[", "").Replace("]", "") == txtVTKolonAdi.Text)
                    {
                        MessageBox.Show("Bu isimde kolon zaten oluşturulmuş.");
                        return;
                    }
                }

                ColumnInfo tempColumn = new ColumnInfo();

                tempColumn.TableName = txtVTTabloAdi.Text;

                string kolon = @"[" + txtVTKolonAdi.Text + "]";
                tempColumn.ColumnName = txtVTKolonAdi.Text;

                kolon += "_" + cmbVTVeriTipi.Text;
                tempColumn.DataType = cmbVTVeriTipi.Text;

                if (!String.IsNullOrEmpty(txtVTVeriBoyutu.Text))
                {
                    kolon += "(" + txtVTVeriBoyutu.Text + ")";
                    tempColumn.MaxLength = txtVTVeriBoyutu.Text;
                }

                if (chkVTPrimaryKey.Checked)
                {
                    kolon += "_PRIMARY KEY";
                    tempColumn.IsPrimaryKey = "YES";
                }

                if (chkVTAutoIncrement.Checked)
                {
                    tempColumn.IsIdentity = "YES";

                    if (numVTArtisMiktari.Enabled == true && numVTBaslangicSayisi.Enabled == true)
                    {
                        kolon += "_IDENTITY(" + numVTBaslangicSayisi.Value.ToString() + "," + numVTArtisMiktari.Value.ToString() + ")";
                        tempColumn.SeedValue = numVTBaslangicSayisi.Value.ToString();
                        tempColumn.IncrementValue = numVTArtisMiktari.Value.ToString();
                    }
                    else
                    {
                        if (numVTArtisMiktari.Enabled == false && numVTBaslangicSayisi.Enabled == true)
                        {
                            kolon += "_IDENTITY(" + numVTBaslangicSayisi.Value.ToString() + ",1)";

                            tempColumn.SeedValue = numVTBaslangicSayisi.Value.ToString();
                            tempColumn.IncrementValue = "1";
                        }
                        else if (numVTBaslangicSayisi.Enabled == false && numVTArtisMiktari.Enabled == true)
                        {
                            kolon += "_IDENTITY(1," + numVTArtisMiktari.Value.ToString() + ")";
                            tempColumn.SeedValue = "1";
                            tempColumn.IncrementValue = numVTArtisMiktari.Value.ToString();
                        }
                        else
                        {
                            kolon += "_IDENTITY(1,1)";

                            tempColumn.SeedValue = "1";
                            tempColumn.IncrementValue = "1";
                        }
                    }
                }

                if (chkVTAllowNull.Checked)
                {
                    kolon += "_NULL";
                    tempColumn.IsNullable = "YES";
                }
                else
                {
                    kolon += "_NOT NULL";
                    tempColumn.IsNullable = "NO";
                }

                if (!String.IsNullOrEmpty(txtVTVarsayilanDeger.Text))
                {
                    if (cmbVTVeriTipi.Text == "decimal")
                    {
                        kolon += "_default " + txtVTVarsayilanDeger.Text.Replace(",", ".");
                        tempColumn.DefaultValue = txtVTVarsayilanDeger.Text.Replace(",", ".");
                    }
                    else if (cmbVTVeriTipi.Text == "nvarchar")
                    {
                        kolon += "_default '" + txtVTVarsayilanDeger.Text + "'";
                        tempColumn.DefaultValue = txtVTVarsayilanDeger.Text;
                    }
                    else
                    {
                        kolon += "_default " + txtVTVarsayilanDeger.Text + "";
                        tempColumn.DefaultValue = txtVTVarsayilanDeger.Text;
                    }
                }

                lstVTKolonlar.Items.Add(kolon);
                tempColumnNames.Add(tempColumn);
            }
            else
            {
                MessageBox.Show("Lütfen Kolon İsmi ve Veri Tipini Kontrol Ediniz.");
            }

            if (lstVTKolonlar.Items.Count > 0)
            {
                btnVTKolonSil.Enabled = true;
                btnVTKolonTemizle.Enabled = true;

                btnVTTabloOlustur.Enabled = true;
            }
            else
            {
                btnVTKolonSil.Enabled = false;
                btnVTKolonTemizle.Enabled = false;

                btnVTTabloOlustur.Enabled = false;
            }

            if (lstVTKolonlar.Items.Count > 1)
            {
                btnVTKolonYukari.Enabled = true;
                btnVTKolonAsagi.Enabled = true;
            }
            else
            {
                btnVTKolonYukari.Enabled = false;
                btnVTKolonAsagi.Enabled = false;
            }
        }

        private void btnVTKolonSil_Click(object sender, EventArgs e)
        {
            if (Relations.Where(a => a.ForeignColumn == lstVTKolonlar.SelectedItem.ToString().KolonAdi()).ToList().Count > 0)
            {
                Relations.Remove(Relations.Where(a => a.ForeignColumn == lstVTKolonlar.SelectedItem.ToString().KolonAdi()).FirstOrDefault());
            }

            if (lstVTKolonlar.SelectedIndex >= 0)
            {
                if (tempColumnNames.Where(a => a.ColumnName == lstVTKolonlar.SelectedItem.ToString().KolonAdi()).ToList().Count > 0)
                {
                    tempColumnNames.Remove(tempColumnNames.Where(a => a.ColumnName == lstVTKolonlar.SelectedItem.ToString().KolonAdi()).ToList().FirstOrDefault());
                }

                lstVTKolonlar.Items.RemoveAt(lstVTKolonlar.SelectedIndex);
            }

            if (lstVTKolonlar.Items.Count > 0)
            {
                if (lstVTKolonlar.SelectedIndex >= 0)
                {
                    btnVTRelationOlustur.Enabled = true;
                }
                else
                {
                    btnVTRelationOlustur.Enabled = false;
                }

                btnVTKolonSil.Enabled = true;
                btnVTKolonTemizle.Enabled = true;

                btnVTTabloOlustur.Enabled = true;
            }
            else
            {
                btnVTRelationOlustur.Enabled = false;

                btnVTKolonSil.Enabled = false;
                btnVTKolonTemizle.Enabled = false;

                btnVTTabloOlustur.Enabled = false;
            }

            if (lstVTKolonlar.Items.Count > 1)
            {
                btnVTKolonYukari.Enabled = true;
                btnVTKolonAsagi.Enabled = true;
            }
            else
            {
                btnVTKolonYukari.Enabled = false;
                btnVTKolonAsagi.Enabled = false;
            }
        }

        private void btnVTKolonTemizle_Click(object sender, EventArgs e)
        {
            Relations.Clear();
            tempColumnNames.Clear();
            lstVTKolonlar.Items.Clear();

            btnVTRelationOlustur.Enabled = false;

            btnVTKolonSil.Enabled = false;
            btnVTKolonTemizle.Enabled = false;

            btnVTKolonYukari.Enabled = false;
            btnVTKolonAsagi.Enabled = false;

            btnVTTabloOlustur.Enabled = false;
        }

        private void btnVTRelationOlustur_Click(object sender, EventArgs e)
        {
            if (lstVTKolonlar.SelectedItem.ToString().Contains("PRIMARY KEY") == false)
            {
                CreateRelaion _cre;

                if (chkVTTabloDuzenle.Checked)
                {
                    _cre = new CreateRelaion(true);
                    _cre.TableName = cmbVTTabloAdi.Text;
                }
                else
                {
                    _cre = new CreateRelaion();
                    _cre.TableName = txtVTTabloAdi.Text;
                }

                List<ColumnInfo> collst = tempColumnNames.Where(a => a.ColumnName == lstVTKolonlar.SelectedItem.ToString().KolonAdi()).ToList();

                if (collst.Count > 0)
                {
                    _cre.ForeignInfo = collst.FirstOrDefault();
                }
                else
                {
                    _cre.ForeignInfo = new ColumnInfo();
                    _cre.ForeignInfo.DataType = cmbVTVeriTipi.Text;
                    _cre.ForeignInfo.DefaultValue = txtVTVarsayilanDeger.Text;
                    _cre.ForeignInfo.IncrementValue = numVTBaslangicSayisi.Value.ToString();
                    _cre.ForeignInfo.IsIdentity = chkVTAutoIncrement.Checked == true ? "YES" : "NO";
                    _cre.ForeignInfo.IsNullable = chkVTAllowNull.Checked == true ? "YES" : "NO";
                    _cre.ForeignInfo.IsPrimaryKey = chkVTPrimaryKey.Checked == true ? "YES" : "NO";
                    _cre.ForeignInfo.MaxLength = txtVTVeriBoyutu.Text;
                    _cre.ForeignInfo.SeedValue = numVTArtisMiktari.Value.ToString();
                    _cre.ForeignInfo.TableName = cmbVTTabloAdi.Text;
                    _cre.ForeignInfo.OrdinalPosition = lstVTKolonlar.SelectedIndex.ToString();
                    _cre.ForeignInfo.ColumnName = lstVTKolonlar.SelectedItem.ToString().KolonAdi();
                }

                _cre.ColumnName = lstVTKolonlar.SelectedItem.ToString().KolonAdi();
                _cre.ConnectionInfo = new ConnectionInfo() { IsWindowsAuthentication = chkVTWindowsAuthentication.Checked, DatabaseName = cmbVTVeritabani.Text, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text, Server = txtVTSunucu.Text };

                _cre.ShowDialog();
            }
            else
            {
                MessageBox.Show("Primary Key üzerine Relation oluşturulamaz.");
            }
        }

        private void txtVTTabloAdi_TextChanged(object sender, EventArgs e)
        {
            if (lstVTKolonlar.SelectedIndex >= 0)
            {
                btnVTRelationOlustur.Enabled = true;
            }
            else
            {
                btnVTRelationOlustur.Enabled = false;
            }

            if (txtVTTabloAdi.Text.Length > 0)
            {
                grpKolonlar.Enabled = true;

                if (lstVTKolonlar.Items.Count > 0)
                {
                    btnVTKolonAsagi.Enabled = true;
                    btnVTKolonYukari.Enabled = true;
                    btnVTKolonSil.Enabled = true;
                    btnVTKolonTemizle.Enabled = true;
                    btnVTRelationOlustur.Enabled = true;
                }
                else
                {
                    btnVTKolonAsagi.Enabled = false;
                    btnVTKolonYukari.Enabled = false;
                    btnVTKolonSil.Enabled = false;
                    btnVTKolonTemizle.Enabled = false;
                }
            }
            else
            {
                grpKolonlar.Enabled = false;

                if (lstVTKolonlar.Items.Count > 0)
                {
                    btnVTKolonAsagi.Enabled = true;
                    btnVTKolonYukari.Enabled = true;
                    btnVTKolonSil.Enabled = true;
                    btnVTKolonTemizle.Enabled = true;
                }
                else
                {
                    btnVTKolonAsagi.Enabled = false;
                    btnVTKolonYukari.Enabled = false;
                    btnVTKolonSil.Enabled = false;
                    btnVTKolonTemizle.Enabled = false;
                }
            }
        }

        private void txtVTKolonAdi_TextChanged(object sender, EventArgs e)
        {
            if (txtVTKolonAdi.Text.Length > 0 && cmbVTVeriTipi.SelectedIndex >= 0)
            {
                btnVTKolonEkle.Enabled = true;
            }
            else
            {
                btnVTKolonEkle.Enabled = false;
            }
        }

        private void cmbVTVeriTipi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cmbVTVeritabani_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnVTKolonYukari_Click(object sender, EventArgs e)
        {
            if (lstVTKolonlar.SelectedIndex > 0)
            {
                int index = lstVTKolonlar.SelectedIndex;
                object item = lstVTKolonlar.Items[index];

                lstVTKolonlar.Items.RemoveAt(index);
                lstVTKolonlar.Items.Insert(index - 1, item);

                lstVTKolonlar.SelectedIndex = index - 1;
            }
        }

        private void btnVTKolonAsagi_Click(object sender, EventArgs e)
        {
            if (lstVTKolonlar.SelectedIndex < lstVTKolonlar.Items.Count - 1)
            {
                int index = lstVTKolonlar.SelectedIndex;
                object item = lstVTKolonlar.Items[index];

                lstVTKolonlar.Items.RemoveAt(index);
                lstVTKolonlar.Items.Insert(index + 1, item);

                lstVTKolonlar.SelectedIndex = index + 1;
            }
        }

        private void btnVTTabloOlustur_Click(object sender, EventArgs e)
        {
            bool tabloadikontrol = false;

            if (chkVTTabloDuzenle.Checked)
            {
                if (cmbVTTabloAdi.SelectedIndex >= 0)
                {
                    tabloadikontrol = true;
                }
            }
            else
            {
                if (!String.IsNullOrEmpty(txtVTTabloAdi.Text))
                {
                    tabloadikontrol = true;
                }
            }

            if (cmbVTVeritabani.SelectedIndex >= 0 && lstVTKolonlar.Items.Count > 0 && tabloadikontrol == true)
            {
                if (chkVTTabloDuzenle.Checked)
                {
                    DialogResult result = MessageBox.Show("Tabloya ait eski tüm veriler silinecek.", "Uyarı", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        DropTable();
                    }
                    else
                    {
                        return;
                    }
                }

                if (CreateTable() == true)
                {

                    if (chkVTTabloDuzenle.Checked)
                    {
                        cmbVTTabloAdi.SelectedIndex = -1;
                        cmbVTTabloAdi.SelectedIndex = 0;
                    }
                    else
                    {
                        btnVTTabloOlustur.Enabled = false;
                        btnVTTabloSil.Enabled = false;
                        txtVTTabloAdi.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Bilgileri Eksiksik Giriniz.");
            }
        }

        private void btnVTTabloSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tabloyu silmek istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                DropTable();
                ClearTableControls();
                lstVTKolonlar.Items.Clear();
                btnVTTabloOlustur.Enabled = false;
                cmbVTTabloAdi.Items.Remove(cmbVTTabloAdi.SelectedItem);
                cmbVTTabloAdi.SelectedIndex = 0;
            }
            else
            {
                return;
            }
        }

        private void chkVTTabloDuzenle_CheckedChanged(object sender, EventArgs e)
        {
            Relations.Clear();

            if (chkVTTabloDuzenle.Checked)
            {
                List<ForeignKeyChecker> fkc = ForeignKeyCheck(new SqlConnection() { ConnectionString = Helper.Helper.CreateConnectionText(new ConnectionInfo() { IsWindowsAuthentication = chkVTWindowsAuthentication.Checked, DatabaseName = cmbVTVeritabani.Text, Server = txtVTSunucu.Text, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text }) }, cmbVTTabloAdi.Text);

                foreach (ForeignKeyChecker item in fkc)
                {
                    Relations.Add(new Relation()
                    {
                        RelationName = item.ForeignKeyName,
                        ForeignColumn = item.ForeignColumnName,
                        ForeignTable = item.ForeignTableName,
                        PrimaryColumn = item.PrimaryColumnName,
                        PrimaryTable = item.PrimaryTableName
                    });
                }

                cmbVTTabloAdi.Visible = true;
                txtVTTabloAdi.Visible = false;
                txtVTTabloAdi.Text = "";

                if (cmbVTVeritabani.Text != "")
                {
                    try
                    {
                        tableNames = new List<string>();

                        if (chkVTWindowsAuthentication.Checked == true)
                        {
                            tableNames = Helper.Helper.TableNames(new ConnectionInfo() { Server = txtVTSunucu.Text, DatabaseName = cmbVTVeritabani.Text });
                        }
                        else
                        {
                            tableNames = Helper.Helper.TableNames(new ConnectionInfo() { Server = txtVTSunucu.Text, DatabaseName = cmbVTVeritabani.Text, IsWindowsAuthentication = false, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text });
                        }

                        DBName = cmbVTVeritabani.Text;

                        cmbVTTabloAdi.Items.Clear();

                        foreach (string item in tableNames)
                        {
                            cmbVTTabloAdi.Items.Add(item);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Bağlantı Sağlanamadı");
                    }

                    if (cmbVTTabloAdi.Items.Count > 0)
                    {
                        cmbVTTabloAdi.SelectedIndex = 0;

                        if (chkWindowsAuthentication.Checked == true)
                        {
                            columnNames = Helper.Helper.ColumnNames(new ConnectionInfo() { Server = txtVTSunucu.Text, DatabaseName = cmbVTVeritabani.Text }, cmbVTTabloAdi.Text);
                        }
                        else
                        {
                            columnNames = Helper.Helper.ColumnNames(new ConnectionInfo() { Server = txtVTSunucu.Text, DatabaseName = cmbVTVeritabani.Text, IsWindowsAuthentication = false, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text }, cmbVTTabloAdi.Text);
                        }

                        lstVTKolonlar.Items.Clear();

                        foreach (ColumnInfo item in columnNames)
                        {
                            string kolon = @"[" + item.ColumnName + "]";
                            kolon += "_" + item.DataType;

                            if (!String.IsNullOrEmpty(item.MaxLength))
                            {
                                kolon += "(" + item.MaxLength + ")";
                            }

                            if (item.IsPrimaryKey == "YES")
                            {
                                kolon += "_PRIMARY KEY";
                            }

                            if (item.IsIdentity == "YES" && !String.IsNullOrEmpty(item.IncrementValue) && !String.IsNullOrEmpty(item.SeedValue))
                            {
                                kolon += "_IDENTITY(" + item.SeedValue + "," + item.IncrementValue + ")";
                            }

                            if (item.IsNullable == "YES")
                            {
                                kolon += "_NULL";
                            }
                            else
                            {
                                kolon += "_NOT NULL";
                            }

                            if (!String.IsNullOrEmpty(item.DefaultValue))
                            {
                                if (item.DataType == "decimal")
                                {
                                    kolon += "_default " + item.DefaultValue.Replace(",", ".").Replace(")", "").Replace("(", "");
                                }
                                else
                                {
                                    kolon += "_default " + item.DefaultValue.Replace(")", "").Replace("(", "");
                                }
                            }

                            lstVTKolonlar.Items.Add(kolon);
                        }

                        if (lstVTKolonlar.Items.Count > 0)
                        {
                            lstVTKolonlar.SelectedIndex = 0;

                            btnVTKolonSil.Enabled = true;
                            btnVTKolonTemizle.Enabled = true;

                            btnVTTabloOlustur.Enabled = true;
                        }
                        else
                        {
                            btnVTKolonSil.Enabled = false;
                            btnVTKolonTemizle.Enabled = false;

                            btnVTTabloOlustur.Enabled = false;
                        }

                        if (lstVTKolonlar.Items.Count > 1)
                        {
                            btnVTKolonYukari.Enabled = true;
                            btnVTKolonAsagi.Enabled = true;
                        }
                        else
                        {
                            btnVTKolonYukari.Enabled = false;
                            btnVTKolonAsagi.Enabled = false;
                        }
                    }
                }

                if (cmbVTTabloAdi.Items.Count > 0)
                {
                    btnVTTabloSil.Enabled = true;
                }
                else
                {
                    btnVTTabloSil.Enabled = false;
                }

                if (cmbVTTabloAdi.Items.Count > 0 && lstVTKolonlar.Items.Count > 0)
                {
                    btnVTTabloOlustur.Enabled = true;
                }
                else
                {
                    btnVTTabloOlustur.Enabled = false;
                }
            }
            else
            {
                ClearTableControls();
                grpKolonlar.Enabled = false;
                btnVTTabloSil.Enabled = false;
                cmbVTTabloAdi.Visible = false;
                txtVTTabloAdi.Visible = true;
                cmbVTTabloAdi.Items.Clear();
                lstVTKolonlar.Items.Clear();

                if (lstVTKolonlar.Items.Count > 0)
                {
                    btnVTTabloOlustur.Enabled = true;
                }
                else
                {
                    btnVTTabloOlustur.Enabled = false;
                }
            }
        }

        private void cmbVTVeritabani_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVTVeritabani.Text != "")
            {
                if (chkVTTabloDuzenle.Checked)
                {
                    try
                    {
                        tableNames = new List<string>();

                        if (chkVTWindowsAuthentication.Checked == true)
                        {
                            tableNames = Helper.Helper.TableNames(new ConnectionInfo() { Server = txtVTSunucu.Text, DatabaseName = cmbVTVeritabani.Text });
                        }
                        else
                        {
                            tableNames = Helper.Helper.TableNames(new ConnectionInfo() { Server = txtVTSunucu.Text, DatabaseName = cmbVTVeritabani.Text, IsWindowsAuthentication = false, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text });
                        }

                        DBName = cmbVTVeritabani.Text;

                        cmbVTTabloAdi.Items.Clear();

                        foreach (string item in tableNames)
                        {
                            cmbVTTabloAdi.Items.Add(item);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Bağlantı Sağlanamadı");
                    }

                    if (cmbVTTabloAdi.Items.Count > 0)
                    {
                        cmbVTTabloAdi.SelectedIndex = 0;
                    }
                    else
                    {
                        cmbVTTabloAdi.Text = "";
                        ClearTableControls();
                        grpKolonlar.Enabled = false;
                        cmbVTTabloAdi.Items.Clear();
                        lstVTKolonlar.Items.Clear();
                    }
                }
            }
        }

        private void cmbVTTabloAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstVTKolonlar.Items.Clear();
            Relations.Clear();

            List<ForeignKeyChecker> fkc = ForeignKeyCheck(new SqlConnection() { ConnectionString = Helper.Helper.CreateConnectionText(new ConnectionInfo() { IsWindowsAuthentication = chkVTWindowsAuthentication.Checked, DatabaseName = cmbVTVeritabani.Text, Server = txtVTSunucu.Text, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text }) }, cmbVTTabloAdi.Text);

            foreach (ForeignKeyChecker item in fkc)
            {
                Relations.Add(new Relation()
                {
                    RelationName = item.ForeignKeyName,
                    ForeignColumn = item.ForeignColumnName,
                    ForeignTable = item.ForeignTableName,
                    PrimaryColumn = item.PrimaryColumnName,
                    PrimaryTable = item.PrimaryTableName
                });
            }

            if (cmbVTTabloAdi.Text != "")
            {
                grpKolonlar.Enabled = true;

                if (chkWindowsAuthentication.Checked == true)
                {
                    columnNames = Helper.Helper.ColumnNames(new ConnectionInfo() { Server = txtVTSunucu.Text, DatabaseName = cmbVTVeritabani.Text }, cmbVTTabloAdi.Text);
                }
                else
                {
                    columnNames = Helper.Helper.ColumnNames(new ConnectionInfo() { Server = txtVTSunucu.Text, DatabaseName = cmbVTVeritabani.Text, IsWindowsAuthentication = false, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text }, cmbVTTabloAdi.Text);
                }

                foreach (ColumnInfo item in columnNames)
                {
                    string kolon = @"[" + item.ColumnName + "]";
                    kolon += "_" + item.DataType;

                    if (!String.IsNullOrEmpty(item.MaxLength))
                    {
                        kolon += "(" + item.MaxLength + ")";
                    }

                    if (item.IsPrimaryKey == "YES")
                    {
                        kolon += "_PRIMARY KEY";
                    }

                    if (item.IsIdentity == "YES" && !String.IsNullOrEmpty(item.IncrementValue) && !String.IsNullOrEmpty(item.SeedValue))
                    {
                        kolon += "_IDENTITY(" + item.SeedValue + "," + item.IncrementValue + ")";
                    }

                    if (item.IsNullable == "YES")
                    {
                        kolon += "_NULL";
                    }
                    else
                    {
                        kolon += "_NOT NULL";
                    }

                    if (!String.IsNullOrEmpty(item.DefaultValue))
                    {
                        if (item.DataType == "decimal")
                        {
                            kolon += "_default " + item.DefaultValue.Replace(",", ".").Replace(")", "").Replace("(", "");
                        }
                        else
                        {
                            kolon += "_default " + item.DefaultValue.Replace(")", "").Replace("(", "");
                        }
                    }

                    lstVTKolonlar.Items.Add(kolon);
                }

                if (lstVTKolonlar.Items.Count > 0)
                {
                    lstVTKolonlar.SelectedIndex = 0;

                    btnVTKolonSil.Enabled = true;
                    btnVTKolonTemizle.Enabled = true;

                    btnVTTabloOlustur.Enabled = true;
                }
                else
                {
                    btnVTKolonSil.Enabled = false;
                    btnVTKolonTemizle.Enabled = false;

                    btnVTTabloOlustur.Enabled = false;
                }

                if (lstVTKolonlar.Items.Count > 1)
                {
                    btnVTKolonYukari.Enabled = true;
                    btnVTKolonAsagi.Enabled = true;
                }
                else
                {
                    btnVTKolonYukari.Enabled = false;
                    btnVTKolonAsagi.Enabled = false;
                }
            }
            else
            {
                grpKolonlar.Enabled = false;
            }
        }

        private void lstVTKolonlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstVTKolonlar.SelectedIndex >= 0)
            {
                if (lstVTKolonlar.SelectedItem.ToString().Contains("_PRIMARY KEY_") == false)
                {
                    btnVTRelationOlustur.Enabled = true;
                }
                else
                {
                    btnVTRelationOlustur.Enabled = false;
                }
            }
            else
            {
                btnVTRelationOlustur.Enabled = false;
            }

            if (chkVTTabloDuzenle.Checked)
            {
                try
                {
                    ColumnInfo ci = columnNames.Where(a => a.ColumnName == lstVTKolonlar.SelectedItem.ToString().Split(']')[0].Replace("[", "")).FirstOrDefault();

                    if (ci != null)
                    {
                        txtVTKolonAdi.Text = ci.ColumnName;
                        cmbVTVeriTipi.SelectedIndex = cmbVTVeriTipi.Items.IndexOf(ci.DataType);
                        chkVTAllowNull.Checked = ci.IsNullable == "YES" ? true : false;
                        chkVTPrimaryKey.Checked = ci.IsPrimaryKey == "YES" ? true : false;
                        txtVTVeriBoyutu.Text = ci.MaxLength;
                        txtVTVarsayilanDeger.Text = ci.DefaultValue.Replace("'", "");

                        if (chkVTPrimaryKey.Checked)
                        {
                            numVTBaslangicSayisi.Value = Convert.ToInt32(ci.SeedValue);
                            numVTArtisMiktari.Value = Convert.ToInt32(ci.IncrementValue);
                            chkVTAllowNull.Checked = false;
                            chkVTAllowNull.Enabled = false;
                            txtVTVarsayilanDeger.Text = "";
                            txtVTVarsayilanDeger.Enabled = false;
                        }
                        else
                        {
                            numVTBaslangicSayisi.Value = 1;
                            numVTArtisMiktari.Value = 1;
                        }
                        chkVTAutoIncrement.Checked = ci.IsIdentity == "YES" ? true : false;
                        if (chkVTAutoIncrement.Checked)
                        {
                            numVTBaslangicSayisi.Enabled = true;
                            numVTArtisMiktari.Enabled = true;
                        }
                        else
                        {
                            numVTBaslangicSayisi.Enabled = false;
                            numVTArtisMiktari.Enabled = false;
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void cmbVTTabloAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        bool CreateTable()
        {
            string tablename = chkVTTabloDuzenle.Checked ? cmbVTTabloAdi.Text : txtVTTabloAdi.Text;

            SqlCommand cmd = new SqlCommand();

            string alanlar = "";

            foreach (object item in lstVTKolonlar.Items)
            {
                string columnname = item.ToString().Split(']')[0] + "]";

                alanlar += columnname + item.ToString().Split(']')[1].Replace("_", " ") + ",";
            }

            alanlar = alanlar.TrimEnd(',');

            cmd.Connection = new SqlConnection(Helper.Helper.CreateConnectionText(new ConnectionInfo() { IsWindowsAuthentication = chkVTWindowsAuthentication.Checked, DatabaseName = cmbVTVeritabani.Text, Server = txtVTSunucu.Text, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text }));

            cmd.CommandText = "USE [" + cmbVTVeritabani.Text + "] ";

            try
            {
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cmd.Connection.Close();
                    return false;
                }

                cmd.CommandText = "SET ANSI_NULLS ON ";

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cmd.Connection.Close();
                    return false;
                }

                cmd.CommandText = "SET QUOTED_IDENTIFIER ON";

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cmd.Connection.Close();
                    return false;
                }

                cmd.CommandText = @"CREATE TABLE " + tablename + "(" +
                                 @"" + alanlar +
                                 @") ON [PRIMARY]";

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cmd.Connection.Close();
                    return false;
                }

                if (Relations.Count > 0)
                {
                    string tabloadi = chkVTTabloDuzenle.Checked ? cmbVTTabloAdi.Text : txtVTTabloAdi.Text;

                    foreach (Relation item in Relations.Where(a => a.ForeignTable == tabloadi).ToList())
                    {
                        CreateRelations(item);
                    }

                    Relations.Clear();
                }

                MessageBox.Show("Tablo Başarıyla Oluşturuldu.");
                ClearTableControls();
                lstVTKolonlar.Items.Clear();
                btnVTKolonSil.Enabled = false;
                btnVTKolonTemizle.Enabled = false;
                btnVTKolonEkle.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return true;
        }

        void DropTable()
        {
            string tablename = chkVTTabloDuzenle.Checked ? cmbVTTabloAdi.Text : txtVTTabloAdi.Text;

            SqlCommand cmd = new SqlCommand();

            string alanlar = "";

            foreach (object item in lstVTKolonlar.Items)
            {
                alanlar += item.ToString().Replace("_", " ") + ",";
            }

            alanlar = alanlar.TrimEnd(',');

            cmd.Connection = new SqlConnection(Helper.Helper.CreateConnectionText(new ConnectionInfo() { IsWindowsAuthentication = chkVTWindowsAuthentication.Checked, DatabaseName = cmbVTVeritabani.Text, Server = txtVTSunucu.Text, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text }));

            cmd.CommandText = "USE [" + cmbVTVeritabani.Text + "] ";

            try
            {
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cmd.Connection.Close();
                    return;
                }

                cmd.CommandText = @"DROP TABLE " + tablename; //DROP TABLE table_name [CASCADE CONSTRAINTS] şeklinde bağlantılı tabloları da siler

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cmd.Connection.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        void ClearTableControls()
        {
            txtVTKolonAdi.Text = "";
            cmbVTVeriTipi.SelectedIndex = 0;
            txtVTVeriBoyutu.Text = "";
            txtVTVarsayilanDeger.Text = "";
            chkVTAllowNull.Checked = false;
            chkVTAutoIncrement.Checked = false;
            chkVTPrimaryKey.Checked = false;
            numVTBaslangicSayisi.Value = 1;
            numVTArtisMiktari.Value = 1;
        }

        void CreateRelations(Relation _relation)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = new SqlConnection(Helper.Helper.CreateConnectionText(new ConnectionInfo() { IsWindowsAuthentication = chkVTWindowsAuthentication.Checked, DatabaseName = cmbVTVeritabani.Text, Server = txtVTSunucu.Text, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text }));

            cmd.CommandText = "USE [" + cmbVTVeritabani.Text + "] ";

            try
            {
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cmd.Connection.Close();
                    return;
                }

                cmd.CommandText = @"ALTER TABLE " + _relation.ForeignTable +
                                  @" ADD CONSTRAINT " + _relation.RelationName + " FOREIGN KEY (" + _relation.ForeignColumn + ")" +
                                  @" REFERENCES " + _relation.PrimaryTable + " (" + _relation.PrimaryColumn + ")" +
                                  @" ON DELETE CASCADE" +
                                  @" ON UPDATE CASCADE" +
                                  @";";

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cmd.Connection.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        #endregion
    }
}
