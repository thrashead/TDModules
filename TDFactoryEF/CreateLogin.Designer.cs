namespace TDFactoryEF
{
    partial class CreateLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateLogin));
            this.cmbUserVeritabani = new System.Windows.Forms.ComboBox();
            this.lblUserVeriTabani = new System.Windows.Forms.Label();
            this.grpUserLogin = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.rbtnUserEnglish = new System.Windows.Forms.RadioButton();
            this.rbtnUserTurkish = new System.Windows.Forms.RadioButton();
            this.chkUserCreateLogin = new System.Windows.Forms.CheckBox();
            this.chkUserPolicy = new System.Windows.Forms.CheckBox();
            this.chkUserCreateUser = new System.Windows.Forms.CheckBox();
            this.chkUserExpiration = new System.Windows.Forms.CheckBox();
            this.btnUserCreateLogin = new System.Windows.Forms.Button();
            this.lblUserVarsayilanDil = new System.Windows.Forms.Label();
            this.lblUserLoginName = new System.Windows.Forms.Label();
            this.lblUserCreateUser = new System.Windows.Forms.Label();
            this.lblUserExpiration = new System.Windows.Forms.Label();
            this.lblUserCreateLogin = new System.Windows.Forms.Label();
            this.lblUserPolicy = new System.Windows.Forms.Label();
            this.label1lblUserPassword = new System.Windows.Forms.Label();
            this.txtUserLoginName = new System.Windows.Forms.TextBox();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.grpUserLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbUserVeritabani
            // 
            this.cmbUserVeritabani.FormattingEnabled = true;
            this.cmbUserVeritabani.Location = new System.Drawing.Point(132, 135);
            this.cmbUserVeritabani.Name = "cmbUserVeritabani";
            this.cmbUserVeritabani.Size = new System.Drawing.Size(180, 21);
            this.cmbUserVeritabani.TabIndex = 5;
            this.cmbUserVeritabani.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUserVeritabani_KeyPress);
            // 
            // lblUserVeriTabani
            // 
            this.lblUserVeriTabani.AutoSize = true;
            this.lblUserVeriTabani.Location = new System.Drawing.Point(15, 138);
            this.lblUserVeriTabani.Name = "lblUserVeriTabani";
            this.lblUserVeriTabani.Size = new System.Drawing.Size(111, 13);
            this.lblUserVeriTabani.TabIndex = 6;
            this.lblUserVeriTabani.Text = "Varsayılan Veritabanı :";
            // 
            // grpUserLogin
            // 
            this.grpUserLogin.Controls.Add(this.textBox1);
            this.grpUserLogin.Controls.Add(this.rbtnUserEnglish);
            this.grpUserLogin.Controls.Add(this.rbtnUserTurkish);
            this.grpUserLogin.Controls.Add(this.chkUserCreateLogin);
            this.grpUserLogin.Controls.Add(this.chkUserPolicy);
            this.grpUserLogin.Controls.Add(this.chkUserCreateUser);
            this.grpUserLogin.Controls.Add(this.chkUserExpiration);
            this.grpUserLogin.Controls.Add(this.cmbUserVeritabani);
            this.grpUserLogin.Controls.Add(this.btnUserCreateLogin);
            this.grpUserLogin.Controls.Add(this.lblUserVarsayilanDil);
            this.grpUserLogin.Controls.Add(this.lblUserVeriTabani);
            this.grpUserLogin.Controls.Add(this.lblUserLoginName);
            this.grpUserLogin.Controls.Add(this.lblUserCreateUser);
            this.grpUserLogin.Controls.Add(this.lblUserExpiration);
            this.grpUserLogin.Controls.Add(this.lblUserCreateLogin);
            this.grpUserLogin.Controls.Add(this.lblUserPolicy);
            this.grpUserLogin.Controls.Add(this.label1lblUserPassword);
            this.grpUserLogin.Controls.Add(this.txtUserLoginName);
            this.grpUserLogin.Controls.Add(this.txtUserPassword);
            this.grpUserLogin.Location = new System.Drawing.Point(12, 12);
            this.grpUserLogin.Name = "grpUserLogin";
            this.grpUserLogin.Size = new System.Drawing.Size(318, 222);
            this.grpUserLogin.TabIndex = 74;
            this.grpUserLogin.TabStop = false;
            this.grpUserLogin.Text = "Login / User İşlemleri";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(7, 166);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(305, 20);
            this.textBox1.TabIndex = 76;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // rbtnUserEnglish
            // 
            this.rbtnUserEnglish.AutoSize = true;
            this.rbtnUserEnglish.Location = new System.Drawing.Point(198, 36);
            this.rbtnUserEnglish.Name = "rbtnUserEnglish";
            this.rbtnUserEnglish.Size = new System.Drawing.Size(63, 17);
            this.rbtnUserEnglish.TabIndex = 75;
            this.rbtnUserEnglish.TabStop = true;
            this.rbtnUserEnglish.Text = "İngilizce";
            this.rbtnUserEnglish.UseVisualStyleBackColor = true;
            // 
            // rbtnUserTurkish
            // 
            this.rbtnUserTurkish.AutoSize = true;
            this.rbtnUserTurkish.Checked = true;
            this.rbtnUserTurkish.Location = new System.Drawing.Point(133, 36);
            this.rbtnUserTurkish.Name = "rbtnUserTurkish";
            this.rbtnUserTurkish.Size = new System.Drawing.Size(59, 17);
            this.rbtnUserTurkish.TabIndex = 74;
            this.rbtnUserTurkish.TabStop = true;
            this.rbtnUserTurkish.Text = "Türkçe";
            this.rbtnUserTurkish.UseVisualStyleBackColor = true;
            // 
            // chkUserCreateLogin
            // 
            this.chkUserCreateLogin.AutoSize = true;
            this.chkUserCreateLogin.Checked = true;
            this.chkUserCreateLogin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserCreateLogin.Location = new System.Drawing.Point(132, 20);
            this.chkUserCreateLogin.Name = "chkUserCreateLogin";
            this.chkUserCreateLogin.Size = new System.Drawing.Size(15, 14);
            this.chkUserCreateLogin.TabIndex = 73;
            this.chkUserCreateLogin.UseVisualStyleBackColor = true;
            this.chkUserCreateLogin.CheckedChanged += new System.EventHandler(this.chkUserCreateLogin_CheckedChanged);
            // 
            // chkUserPolicy
            // 
            this.chkUserPolicy.AutoSize = true;
            this.chkUserPolicy.Location = new System.Drawing.Point(132, 60);
            this.chkUserPolicy.Name = "chkUserPolicy";
            this.chkUserPolicy.Size = new System.Drawing.Size(15, 14);
            this.chkUserPolicy.TabIndex = 73;
            this.chkUserPolicy.UseVisualStyleBackColor = true;
            this.chkUserPolicy.CheckedChanged += new System.EventHandler(this.chkUserPolicy_CheckedChanged);
            // 
            // chkUserCreateUser
            // 
            this.chkUserCreateUser.AutoSize = true;
            this.chkUserCreateUser.Checked = true;
            this.chkUserCreateUser.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUserCreateUser.Location = new System.Drawing.Point(230, 20);
            this.chkUserCreateUser.Name = "chkUserCreateUser";
            this.chkUserCreateUser.Size = new System.Drawing.Size(15, 14);
            this.chkUserCreateUser.TabIndex = 73;
            this.chkUserCreateUser.UseVisualStyleBackColor = true;
            this.chkUserCreateUser.CheckedChanged += new System.EventHandler(this.chkUserCreateUser_CheckedChanged);
            // 
            // chkUserExpiration
            // 
            this.chkUserExpiration.AutoSize = true;
            this.chkUserExpiration.Enabled = false;
            this.chkUserExpiration.Location = new System.Drawing.Point(271, 60);
            this.chkUserExpiration.Name = "chkUserExpiration";
            this.chkUserExpiration.Size = new System.Drawing.Size(15, 14);
            this.chkUserExpiration.TabIndex = 73;
            this.chkUserExpiration.UseVisualStyleBackColor = true;
            // 
            // btnUserCreateLogin
            // 
            this.btnUserCreateLogin.Enabled = false;
            this.btnUserCreateLogin.Location = new System.Drawing.Point(177, 193);
            this.btnUserCreateLogin.Name = "btnUserCreateLogin";
            this.btnUserCreateLogin.Size = new System.Drawing.Size(135, 23);
            this.btnUserCreateLogin.TabIndex = 72;
            this.btnUserCreateLogin.Text = "Kullanıcı Oluştur";
            this.btnUserCreateLogin.UseVisualStyleBackColor = true;
            this.btnUserCreateLogin.Click += new System.EventHandler(this.btnUserCreateLogin_Click);
            // 
            // lblUserVarsayilanDil
            // 
            this.lblUserVarsayilanDil.AutoSize = true;
            this.lblUserVarsayilanDil.Location = new System.Drawing.Point(50, 38);
            this.lblUserVarsayilanDil.Name = "lblUserVarsayilanDil";
            this.lblUserVarsayilanDil.Size = new System.Drawing.Size(76, 13);
            this.lblUserVarsayilanDil.TabIndex = 6;
            this.lblUserVarsayilanDil.Text = "Varsayılan Dil :";
            // 
            // lblUserLoginName
            // 
            this.lblUserLoginName.AutoSize = true;
            this.lblUserLoginName.Location = new System.Drawing.Point(36, 86);
            this.lblUserLoginName.Name = "lblUserLoginName";
            this.lblUserLoginName.Size = new System.Drawing.Size(90, 13);
            this.lblUserLoginName.TabIndex = 69;
            this.lblUserLoginName.Text = "Login / User Adı :";
            // 
            // lblUserCreateUser
            // 
            this.lblUserCreateUser.AutoSize = true;
            this.lblUserCreateUser.Location = new System.Drawing.Point(153, 20);
            this.lblUserCreateUser.Name = "lblUserCreateUser";
            this.lblUserCreateUser.Size = new System.Drawing.Size(71, 13);
            this.lblUserCreateUser.TabIndex = 70;
            this.lblUserCreateUser.Text = "User Oluştur :";
            // 
            // lblUserExpiration
            // 
            this.lblUserExpiration.AutoSize = true;
            this.lblUserExpiration.Location = new System.Drawing.Point(153, 60);
            this.lblUserExpiration.Name = "lblUserExpiration";
            this.lblUserExpiration.Size = new System.Drawing.Size(112, 13);
            this.lblUserExpiration.TabIndex = 70;
            this.lblUserExpiration.Text = "Şifre Geçerlilik Süresi :";
            // 
            // lblUserCreateLogin
            // 
            this.lblUserCreateLogin.AutoSize = true;
            this.lblUserCreateLogin.Location = new System.Drawing.Point(51, 20);
            this.lblUserCreateLogin.Name = "lblUserCreateLogin";
            this.lblUserCreateLogin.Size = new System.Drawing.Size(75, 13);
            this.lblUserCreateLogin.TabIndex = 70;
            this.lblUserCreateLogin.Text = "Login Oluştur :";
            // 
            // lblUserPolicy
            // 
            this.lblUserPolicy.AutoSize = true;
            this.lblUserPolicy.Location = new System.Drawing.Point(45, 60);
            this.lblUserPolicy.Name = "lblUserPolicy";
            this.lblUserPolicy.Size = new System.Drawing.Size(81, 13);
            this.lblUserPolicy.TabIndex = 70;
            this.lblUserPolicy.Text = "Şifre Güvenliği :";
            // 
            // label1lblUserPassword
            // 
            this.label1lblUserPassword.AutoSize = true;
            this.label1lblUserPassword.Location = new System.Drawing.Point(27, 112);
            this.label1lblUserPassword.Name = "label1lblUserPassword";
            this.label1lblUserPassword.Size = new System.Drawing.Size(99, 13);
            this.label1lblUserPassword.TabIndex = 70;
            this.label1lblUserPassword.Text = "Login / User Şifre : ";
            // 
            // txtUserLoginName
            // 
            this.txtUserLoginName.Location = new System.Drawing.Point(132, 83);
            this.txtUserLoginName.Name = "txtUserLoginName";
            this.txtUserLoginName.Size = new System.Drawing.Size(180, 20);
            this.txtUserLoginName.TabIndex = 66;
            this.txtUserLoginName.TextChanged += new System.EventHandler(this.txtUserLoginName_TextChanged);
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Location = new System.Drawing.Point(132, 109);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new System.Drawing.Size(180, 20);
            this.txtUserPassword.TabIndex = 67;
            this.txtUserPassword.UseSystemPasswordChar = true;
            this.txtUserPassword.TextChanged += new System.EventHandler(this.txtUserPassword_TextChanged);
            // 
            // CreateLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 246);
            this.Controls.Add(this.grpUserLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login / User Oluştur";
            this.Load += new System.EventHandler(this.CreateLogin_Load);
            this.grpUserLogin.ResumeLayout(false);
            this.grpUserLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbUserVeritabani;
        private System.Windows.Forms.Label lblUserVeriTabani;
        private System.Windows.Forms.GroupBox grpUserLogin;
        private System.Windows.Forms.Button btnUserCreateLogin;
        private System.Windows.Forms.Label lblUserLoginName;
        private System.Windows.Forms.Label label1lblUserPassword;
        private System.Windows.Forms.TextBox txtUserLoginName;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.CheckBox chkUserPolicy;
        private System.Windows.Forms.CheckBox chkUserExpiration;
        private System.Windows.Forms.Label lblUserExpiration;
        private System.Windows.Forms.Label lblUserPolicy;
        private System.Windows.Forms.RadioButton rbtnUserEnglish;
        private System.Windows.Forms.RadioButton rbtnUserTurkish;
        private System.Windows.Forms.Label lblUserVarsayilanDil;
        private System.Windows.Forms.CheckBox chkUserCreateLogin;
        private System.Windows.Forms.CheckBox chkUserCreateUser;
        private System.Windows.Forms.Label lblUserCreateUser;
        private System.Windows.Forms.Label lblUserCreateLogin;
        private System.Windows.Forms.TextBox textBox1;
    }
}