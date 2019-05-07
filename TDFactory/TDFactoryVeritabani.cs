using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TDFactory.Helper;

namespace TDFactory
{
    public partial class TDFactory : Form
    {
        #region Veritabanı

        private void btnVeritabaniOlustur_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtVeritabaniIsmi.Text))
            {
                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CreateDatabase(folderDialog.SelectedPath);
                }

            }
            else
            {
                MessageBox.Show("Lütfen önce bir veritabanı ismi giriniz.");
            }
        }

        private void chkVTWindowsAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVTWindowsAuthentication.Checked == false)
            {
                txtVTKullaniciAdi.Enabled = true;
                txtVTSifre.Enabled = true;
            }
            else
            {
                txtVTKullaniciAdi.Enabled = false;
                txtVTSifre.Enabled = false;
            }

            txtVTKullaniciAdi.Text = "";
            txtVTSifre.Text = "";
        }

        private void chkLimitsizBoyut_CheckedChanged(object sender, EventArgs e)
        {
            if (numMaxDosyaBoyutu.Enabled == true)
            {
                numMaxDosyaBoyutu.Enabled = false;
            }
            else
            {
                numMaxDosyaBoyutu.Enabled = true;
            }
        }

        private void chkLogLimitsizBoyut_CheckedChanged(object sender, EventArgs e)
        {
            if (numMaxLogBoyutu.Enabled == true)
            {
                numMaxLogBoyutu.Enabled = false;
            }
            else
            {
                numMaxLogBoyutu.Enabled = true;
            }
        }

        private void txtVeritabaniIsmi_TextChanged(object sender, EventArgs e)
        {
            if (txtVeritabaniIsmi.Text.Length > 0)
            {
                btnVeritabaniOlustur.Enabled = true;
            }
            else
            {
                btnVeritabaniOlustur.Enabled = false;
            }
        }

        private void btnCreateLogin_Click(object sender, EventArgs e)
        {
            CreateLogin creLogForm = new CreateLogin();
            creLogForm.ConnectionInfo = new ConnectionInfo() { IsWindowsAuthentication = chkVTWindowsAuthentication.Checked, DatabaseName = cmbVTVeritabani.Text, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text, Server = txtVTSunucu.Text };
            creLogForm.ShowDialog();
        }

        void CreateDatabase(string address)
        {
            SqlCommand cmd = new SqlCommand();

            string dosyaBoyutu = chkLimitsizBoyut.Checked ? "Unlimited" : numMaxDosyaBoyutu.Value.ToString() + "MB";
            string logBoyutu = chkLogLimitsizBoyut.Checked ? "Unlimited" : numMaxLogBoyutu.Value.ToString() + "MB";

            string cmdText = @"CREATE DATABASE " + txtVeritabaniIsmi.Text + " ON PRIMARY" +
                             @"(NAME = " + txtVeritabaniIsmi.Text + "_Data," +
                             @"FILENAME = '" + address + "\\" + txtVeritabaniIsmi.Text + ".mdf'," +
                             @"SIZE = " + numDosyaBoyutu.Value.ToString() + "MB, MAXSIZE = " + dosyaBoyutu + ", FILEGROWTH = " + numDosyaGenislemeYuzdesi.Value.ToString() + "%)" +
                             @"LOG ON (NAME = " + txtVeritabaniIsmi.Text + "_Log," +
                             @"FILENAME = '" + address + "\\" + txtVeritabaniIsmi.Text + "Log.ldf'," +
                             @"SIZE = " + numLogBoyutu.Value.ToString() + "MB, MAXSIZE = " + logBoyutu + ", FILEGROWTH = " + numLogGenislemeYuzdesi.Value.ToString() + "%)";

            cmd.CommandText = cmdText;
            cmd.Connection = new SqlConnection(Helper.Helper.CreateConnectionText(new ConnectionInfo() { IsWindowsAuthentication = chkVTWindowsAuthentication.Checked, Server = txtVTSunucu.Text, Username = txtVTKullaniciAdi.Text, Password = txtVTSifre.Text }));

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Veritabanı Başarıyla Oluşturuldu.");
                ClearVTControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        void ClearVTControls()
        {
            chkLimitsizBoyut.Checked = false;
            chkLogLimitsizBoyut.Checked = false;
            txtVeritabaniIsmi.Text = "";
            numDosyaBoyutu.Value = numDosyaBoyutu.Minimum;
            numMaxDosyaBoyutu.Value = numMaxDosyaBoyutu.Minimum;
            numMaxDosyaBoyutu.Enabled = true;
            numDosyaGenislemeYuzdesi.Value = numDosyaGenislemeYuzdesi.Minimum;
            numLogBoyutu.Value = numLogBoyutu.Minimum;
            numMaxLogBoyutu.Value = numMaxLogBoyutu.Minimum;
            numMaxLogBoyutu.Enabled = true;
            numLogGenislemeYuzdesi.Value = numLogGenislemeYuzdesi.Minimum;
            numLogGenislemeYuzdesi.Value = numLogGenislemeYuzdesi.Minimum;
        }

        #endregion
    }
}
