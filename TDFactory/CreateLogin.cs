using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TDFactory.Helper;

namespace TDFactory
{
    public partial class CreateLogin : Form
    {
        public ConnectionInfo ConnectionInfo { get; set; }

        public CreateLogin()
        {
            InitializeComponent();
        }

        private void CreateLogin_Load(object sender, EventArgs e)
        {
            try
            {
                cmbUserVeritabani.DataSource = Helper.Helper.DatabaseNames(ConnectionInfo);

                if (cmbUserVeritabani.Items.Count <= 0)
                {
                    MessageBox.Show("Bağlantı Sağlanamadı");
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Bağlantı Sağlanamadı");
                this.Close();
            }
        }

        private void txtUserLoginName_TextChanged(object sender, EventArgs e)
        {
            SetButtonEnable();
        }

        private void txtUserPassword_TextChanged(object sender, EventArgs e)
        {
            SetButtonEnable();
        }

        private void chkUserPolicy_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUserPolicy.Checked)
            {
                chkUserExpiration.Enabled = true;
            }
            else
            {
                chkUserExpiration.Enabled = false;
                chkUserExpiration.Checked = false;
            }
        }

        private void btnUserCreateLogin_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            int i = 0;
            int say = 0;

            cmd.Connection = new SqlConnection(Helper.Helper.CreateConnectionText(ConnectionInfo));

            try
            {
                cmd.Connection.Open();

                try
                {
                    if (chkUserCreateLogin.Checked)
                    {
                        cmd.CommandText = @"SELECT count(1) FROM sys.sql_logins WHERE name = N'" + txtUserLoginName.Text + "' ";
                        i = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                        if (i <= 0)
                        {
                            try
                            {
                                string language = rbtnUserTurkish.Checked ? "Türkçe" : "us_english";
                                string expirepolicy = "CHECK_POLICY=" + (chkUserPolicy.Checked ? "ON" + (chkUserExpiration.Checked ? ", CHECK_EXPIRATION=ON" : ", CHECK_EXPIRATION=OFF") : "OFF");

                                cmd.CommandText = @"CREATE LOGIN [" + txtUserLoginName.Text + "] " +
                                                  @"WITH PASSWORD=N'" + txtUserPassword.Text + "', " +
                                                  @"DEFAULT_DATABASE=[" + cmbUserVeritabani.Text + "]," +
                                                  @"DEFAULT_LANGUAGE=[" + language + "]," + expirepolicy;
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                cmd.Connection.Close();
                                return;
                            }

                            try
                            {
                                cmd.CommandText = @"SELECT count(1) FROM sys.sql_logins WHERE name = N'" + txtUserLoginName.Text + "' ";

                                i = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                                if (i <= 0)
                                {
                                    MessageBox.Show("Login Oluşturulamadı.");
                                    return;
                                }
                                else
                                {
                                    MessageBox.Show("Login Başarıyla Oluşturuldu.");
                                    say++;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                cmd.Connection.Close();
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bu isimde Login zaten oluşturulmuş.");
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    cmd.Connection.Close();
                    return;
                }

                try
                {
                    if (chkUserCreateUser.Checked)
                    {
                        cmd.CommandText = "use [" + cmbUserVeritabani.Text + "]";
                        cmd.ExecuteNonQuery();

                        try
                        {
                            cmd.CommandText = @"SELECT count(1) FROM dbo.sysusers where name = N'" + txtUserLoginName.Text + "'";
                            i = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            cmd.Connection.Close();
                            return;
                        }

                        if (i <= 0)
                        {
                            try
                            {
                                cmd.CommandText = @"CREATE user [" + txtUserLoginName.Text + "] for login " + txtUserLoginName.Text + " exec sp_addrolemember N'db_owner', N'" + txtUserLoginName.Text + "'";
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                cmd.Connection.Close();
                                return;
                            }

                            try
                            {
                                cmd.CommandText = @"SELECT count(1) FROM dbo.sysusers where name = N'" + txtUserLoginName.Text + "'";
                                i = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                cmd.Connection.Close();
                                return;
                            }

                            if (i <= 0)
                            {
                                MessageBox.Show("User Oluşturulamadı.");
                                return;
                            }
                            else
                            {
                                MessageBox.Show("User Başarıyla Oluşturuldu.");
                                say++;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bu isimde User zaten oluşturulmuş.");
                            return;
                        }
                    }
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
            }
            finally
            {
                cmd.Connection.Close();
            }

            if (say == 2)
            {
                textBox1.Enabled = true;
                textBox1.Text = "Data Source=.;Initial Catalog=" + cmbUserVeritabani.Text + ";User ID=" + txtUserLoginName.Text + ";Password=" + txtUserPassword.Text + ";";
            }
        }

        private void chkUserCreateLogin_CheckedChanged(object sender, EventArgs e)
        {
            SetButtonEnable();

            if (!chkUserCreateLogin.Checked)
            {
                rbtnUserTurkish.Enabled = false;
                rbtnUserEnglish.Enabled = false;
                chkUserPolicy.Enabled = false;
                chkUserExpiration.Enabled = false;
                chkUserPolicy.Checked = false;
                chkUserExpiration.Checked = false;
                txtUserPassword.Enabled = false;
                txtUserPassword.Text = "";

                if (!chkUserCreateUser.Checked)
                {
                    txtUserLoginName.Enabled = false;
                }
            }
            else
            {
                rbtnUserTurkish.Enabled = true;
                rbtnUserEnglish.Enabled = true;
                chkUserPolicy.Enabled = true;
                chkUserExpiration.Enabled = true;
                chkUserPolicy.Checked = true;
                chkUserExpiration.Checked = true;
                txtUserLoginName.Enabled = true;
                txtUserPassword.Enabled = true;

                if (chkUserCreateUser.Checked)
                {
                    cmbUserVeritabani.Enabled = true;
                }
            }
        }

        private void chkUserCreateUser_CheckedChanged(object sender, EventArgs e)
        {
            SetButtonEnable();

            if (!chkUserCreateUser.Checked)
            {
                cmbUserVeritabani.Enabled = false;

                if (!chkUserCreateLogin.Checked)
                {
                    txtUserLoginName.Enabled = false;
                }
            }
            else
            {
                cmbUserVeritabani.Enabled = true;
                txtUserLoginName.Enabled = true;
            }
        }

        private void cmbUserVeritabani_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        void SetButtonEnable()
        {
            if (chkUserCreateLogin.Checked && chkUserCreateUser.Checked)
            {
                if (!String.IsNullOrEmpty(txtUserLoginName.Text) && !String.IsNullOrEmpty(txtUserPassword.Text))
                {
                    btnUserCreateLogin.Enabled = true;
                }
                else
                {
                    btnUserCreateLogin.Enabled = false;
                }
            }
            else if (chkUserCreateLogin.Checked && !chkUserCreateUser.Checked)
            {
                if (!String.IsNullOrEmpty(txtUserLoginName.Text) && !String.IsNullOrEmpty(txtUserPassword.Text))
                {
                    btnUserCreateLogin.Enabled = true;
                }
                else
                {
                    btnUserCreateLogin.Enabled = false;
                }
            }
            else if (!chkUserCreateLogin.Checked && chkUserCreateUser.Checked)
            {
                if (!String.IsNullOrEmpty(txtUserLoginName.Text))
                {
                    btnUserCreateLogin.Enabled = true;
                }
                else
                {
                    btnUserCreateLogin.Enabled = false;
                }
            }
            else if (!chkUserCreateLogin.Checked && !chkUserCreateUser.Checked)
            {
                btnUserCreateLogin.Enabled = false;
            }
        }
    }
}

//   CREATE LOGIN [hruser] WITH PASSWORD=N'powerful', DEFAULT_DATABASE=[humanresource], 
//   DEFAULT_LANGUAGE=[Türkçe], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
//   GO

//   use [humanresource]
//   go

//   if not exists (select * from sys.database_principals where name = N'hruser')
//   begin
//   CREATE user [hruser] for login hruser exec sp_addrolemember N'db_owner', N'hruser'
//   end;
//   go