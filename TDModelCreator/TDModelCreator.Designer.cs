namespace TDModelCreator
{
    partial class TDModelCreatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TDModelCreatorForm));
            this.tbconTablar = new System.Windows.Forms.TabControl();
            this.tabpageModelOlustur = new System.Windows.Forms.TabPage();
            this.btnVeriTabaniGetir = new System.Windows.Forms.Button();
            this.cmbVeritabani = new System.Windows.Forms.ComboBox();
            this.chkDBColumn = new System.Windows.Forms.CheckBox();
            this.chkDBTable = new System.Windows.Forms.CheckBox();
            this.chkWindowsAuthentication = new System.Windows.Forms.CheckBox();
            this.txtSunucu = new System.Windows.Forms.TextBox();
            this.lblSunucu = new System.Windows.Forms.Label();
            this.chkRTables = new System.Windows.Forms.CheckBox();
            this.lblSifre = new System.Windows.Forms.Label();
            this.btnBaslat = new System.Windows.Forms.Button();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.lblKullaniciAdi = new System.Windows.Forms.Label();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.folderDialogKatmanOlustur = new System.Windows.Forms.FolderBrowserDialog();
            this.cmbDil = new System.Windows.Forms.ComboBox();
            this.lblDil = new System.Windows.Forms.Label();
            this.tbconTablar.SuspendLayout();
            this.tabpageModelOlustur.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbconTablar
            // 
            this.tbconTablar.Controls.Add(this.tabpageModelOlustur);
            this.tbconTablar.Location = new System.Drawing.Point(12, 12);
            this.tbconTablar.Name = "tbconTablar";
            this.tbconTablar.SelectedIndex = 0;
            this.tbconTablar.Size = new System.Drawing.Size(352, 348);
            this.tbconTablar.TabIndex = 28;
            // 
            // tabpageModelOlustur
            // 
            this.tabpageModelOlustur.Controls.Add(this.btnVeriTabaniGetir);
            this.tabpageModelOlustur.Controls.Add(this.cmbVeritabani);
            this.tabpageModelOlustur.Controls.Add(this.chkDBColumn);
            this.tabpageModelOlustur.Controls.Add(this.chkDBTable);
            this.tabpageModelOlustur.Controls.Add(this.chkWindowsAuthentication);
            this.tabpageModelOlustur.Controls.Add(this.txtSunucu);
            this.tabpageModelOlustur.Controls.Add(this.lblSunucu);
            this.tabpageModelOlustur.Controls.Add(this.chkRTables);
            this.tabpageModelOlustur.Controls.Add(this.lblSifre);
            this.tabpageModelOlustur.Controls.Add(this.btnBaslat);
            this.tabpageModelOlustur.Controls.Add(this.txtKullaniciAdi);
            this.tabpageModelOlustur.Controls.Add(this.txtSifre);
            this.tabpageModelOlustur.Controls.Add(this.lblKullaniciAdi);
            this.tabpageModelOlustur.Location = new System.Drawing.Point(4, 22);
            this.tabpageModelOlustur.Name = "tabpageModelOlustur";
            this.tabpageModelOlustur.Padding = new System.Windows.Forms.Padding(3);
            this.tabpageModelOlustur.Size = new System.Drawing.Size(344, 322);
            this.tabpageModelOlustur.TabIndex = 1;
            this.tabpageModelOlustur.Text = "Model Oluştur";
            this.tabpageModelOlustur.UseVisualStyleBackColor = true;
            // 
            // btnVeriTabaniGetir
            // 
            this.btnVeriTabaniGetir.Location = new System.Drawing.Point(94, 124);
            this.btnVeriTabaniGetir.Name = "btnVeriTabaniGetir";
            this.btnVeriTabaniGetir.Size = new System.Drawing.Size(238, 23);
            this.btnVeriTabaniGetir.TabIndex = 5;
            this.btnVeriTabaniGetir.Text = "Veritabanlarını Getir";
            this.btnVeriTabaniGetir.UseVisualStyleBackColor = true;
            this.btnVeriTabaniGetir.Click += new System.EventHandler(this.btnVeriTabaniGetir_Click);
            // 
            // cmbVeritabani
            // 
            this.cmbVeritabani.Enabled = false;
            this.cmbVeritabani.FormattingEnabled = true;
            this.cmbVeritabani.Location = new System.Drawing.Point(94, 153);
            this.cmbVeritabani.Name = "cmbVeritabani";
            this.cmbVeritabani.Size = new System.Drawing.Size(238, 21);
            this.cmbVeritabani.TabIndex = 6;
            this.cmbVeritabani.SelectedIndexChanged += new System.EventHandler(this.cmbVeritabani_SelectedIndexChanged);
            this.cmbVeritabani.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbVeritabani_KeyPress);
            // 
            // chkDBColumn
            // 
            this.chkDBColumn.AutoSize = true;
            this.chkDBColumn.Enabled = false;
            this.chkDBColumn.Location = new System.Drawing.Point(22, 246);
            this.chkDBColumn.Name = "chkDBColumn";
            this.chkDBColumn.Size = new System.Drawing.Size(142, 17);
            this.chkDBColumn.TabIndex = 7;
            this.chkDBColumn.Text = "DBColumn Attribute Ekle";
            this.chkDBColumn.UseVisualStyleBackColor = true;
            // 
            // chkDBTable
            // 
            this.chkDBTable.AutoSize = true;
            this.chkDBTable.Enabled = false;
            this.chkDBTable.Location = new System.Drawing.Point(22, 223);
            this.chkDBTable.Name = "chkDBTable";
            this.chkDBTable.Size = new System.Drawing.Size(134, 17);
            this.chkDBTable.TabIndex = 8;
            this.chkDBTable.Text = "DBTable Attribute Ekle";
            this.chkDBTable.UseVisualStyleBackColor = true;
            // 
            // chkWindowsAuthentication
            // 
            this.chkWindowsAuthentication.AutoSize = true;
            this.chkWindowsAuthentication.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkWindowsAuthentication.Checked = true;
            this.chkWindowsAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWindowsAuthentication.Location = new System.Drawing.Point(191, 101);
            this.chkWindowsAuthentication.Name = "chkWindowsAuthentication";
            this.chkWindowsAuthentication.Size = new System.Drawing.Size(141, 17);
            this.chkWindowsAuthentication.TabIndex = 4;
            this.chkWindowsAuthentication.Text = "Windows Authentication";
            this.chkWindowsAuthentication.UseVisualStyleBackColor = true;
            this.chkWindowsAuthentication.CheckedChanged += new System.EventHandler(this.chkWindowsAuthentication_CheckedChanged);
            // 
            // txtSunucu
            // 
            this.txtSunucu.Location = new System.Drawing.Point(94, 23);
            this.txtSunucu.Name = "txtSunucu";
            this.txtSunucu.Size = new System.Drawing.Size(238, 20);
            this.txtSunucu.TabIndex = 3;
            // 
            // lblSunucu
            // 
            this.lblSunucu.AutoSize = true;
            this.lblSunucu.Location = new System.Drawing.Point(19, 26);
            this.lblSunucu.Name = "lblSunucu";
            this.lblSunucu.Size = new System.Drawing.Size(50, 13);
            this.lblSunucu.TabIndex = 61;
            this.lblSunucu.Text = "Sunucu :";
            // 
            // chkRTables
            // 
            this.chkRTables.AutoSize = true;
            this.chkRTables.Enabled = false;
            this.chkRTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkRTables.Location = new System.Drawing.Point(22, 200);
            this.chkRTables.Name = "chkRTables";
            this.chkRTables.Size = new System.Drawing.Size(295, 17);
            this.chkRTables.TabIndex = 9;
            this.chkRTables.Text = "Bağlı Tablolar - Tablolar Arası Relation\'a göre özellik ekler";
            this.chkRTables.UseVisualStyleBackColor = true;
            // 
            // lblSifre
            // 
            this.lblSifre.AutoSize = true;
            this.lblSifre.Location = new System.Drawing.Point(19, 78);
            this.lblSifre.Name = "lblSifre";
            this.lblSifre.Size = new System.Drawing.Size(37, 13);
            this.lblSifre.TabIndex = 34;
            this.lblSifre.Text = "Şifre : ";
            // 
            // btnBaslat
            // 
            this.btnBaslat.Enabled = false;
            this.btnBaslat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnBaslat.Location = new System.Drawing.Point(22, 278);
            this.btnBaslat.Name = "btnBaslat";
            this.btnBaslat.Size = new System.Drawing.Size(310, 23);
            this.btnBaslat.TabIndex = 10;
            this.btnBaslat.Text = "Modelleri Oluştur";
            this.btnBaslat.UseVisualStyleBackColor = true;
            this.btnBaslat.Click += new System.EventHandler(this.btnBaslat_Click);
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.Enabled = false;
            this.txtKullaniciAdi.Location = new System.Drawing.Point(94, 49);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(238, 20);
            this.txtKullaniciAdi.TabIndex = 1;
            // 
            // txtSifre
            // 
            this.txtSifre.Enabled = false;
            this.txtSifre.Location = new System.Drawing.Point(94, 75);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(238, 20);
            this.txtSifre.TabIndex = 2;
            this.txtSifre.UseSystemPasswordChar = true;
            // 
            // lblKullaniciAdi
            // 
            this.lblKullaniciAdi.AutoSize = true;
            this.lblKullaniciAdi.Location = new System.Drawing.Point(19, 52);
            this.lblKullaniciAdi.Name = "lblKullaniciAdi";
            this.lblKullaniciAdi.Size = new System.Drawing.Size(70, 13);
            this.lblKullaniciAdi.TabIndex = 33;
            this.lblKullaniciAdi.Text = "Kullanıcı Adı :";
            // 
            // cmbDil
            // 
            this.cmbDil.FormattingEnabled = true;
            this.cmbDil.Items.AddRange(new object[] {
            "English",
            "Türkçe"});
            this.cmbDil.Location = new System.Drawing.Point(246, 6);
            this.cmbDil.Name = "cmbDil";
            this.cmbDil.Size = new System.Drawing.Size(118, 21);
            this.cmbDil.TabIndex = 61;
            this.cmbDil.SelectedIndexChanged += new System.EventHandler(this.cmbDil_SelectedIndexChanged);
            // 
            // lblDil
            // 
            this.lblDil.AutoSize = true;
            this.lblDil.Location = new System.Drawing.Point(212, 9);
            this.lblDil.Name = "lblDil";
            this.lblDil.Size = new System.Drawing.Size(25, 13);
            this.lblDil.TabIndex = 62;
            this.lblDil.Text = "Dil :";
            // 
            // ModelCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 370);
            this.Controls.Add(this.cmbDil);
            this.Controls.Add(this.lblDil);
            this.Controls.Add(this.tbconTablar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ModelCreatorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TDModelCreator by Sina SALIK (Thrashead)";
            this.Load += new System.EventHandler(this.ModelCreatorForm_Load);
            this.tbconTablar.ResumeLayout(false);
            this.tabpageModelOlustur.ResumeLayout(false);
            this.tabpageModelOlustur.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tbconTablar;
        private System.Windows.Forms.TabPage tabpageModelOlustur;
        private System.Windows.Forms.Label lblSunucu;
        private System.Windows.Forms.Button btnVeriTabaniGetir;
        private System.Windows.Forms.Button btnBaslat;
        private System.Windows.Forms.CheckBox chkWindowsAuthentication;
        private System.Windows.Forms.Label lblSifre;
        private System.Windows.Forms.Label lblKullaniciAdi;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.TextBox txtSunucu;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
        private System.Windows.Forms.FolderBrowserDialog folderDialogKatmanOlustur;
        private System.Windows.Forms.CheckBox chkRTables;
        private System.Windows.Forms.CheckBox chkDBColumn;
        private System.Windows.Forms.CheckBox chkDBTable;
        private System.Windows.Forms.ComboBox cmbVeritabani;
        private System.Windows.Forms.ComboBox cmbDil;
        private System.Windows.Forms.Label lblDil;

    }
}