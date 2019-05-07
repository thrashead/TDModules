using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using TDModelCreator.SqlDbCodderHelper;
using System.IO;
using Common;
using System.Text;

namespace TDModelCreator
{
    public partial class TDModelCreatorForm : Form
    {

        public TDModelCreatorForm()
        {
            InitializeComponent();
        }

        public static string PathAddress { get; set; }

        ConnectionInfo conInfo = new ConnectionInfo();
        List<TableColumns> tableColumns;
        List<string> tableNames;
        string DBName = "";

        private void ModelCreatorForm_Load(object sender, EventArgs e)
        {
            cmbDil.SelectedIndex = 0;
        }

        #region Bağlantı

        private void btnVeriTabaniGetir_Click(object sender, EventArgs e)
        {
            if (chkWindowsAuthentication.Checked == true)
            {
                conInfo = new ConnectionInfo() { Server = txtSunucu.Text };
            }
            else
            {
                conInfo = new ConnectionInfo() { Server = txtSunucu.Text, IsWindowsAuthentication = false, Username = txtKullaniciAdi.Text, Password = txtSifre.Text };
            }

            cmbVeritabani.DataSource = null;

            try
            {
                cmbVeritabani.DataSource = Helper.DatabaseNames(conInfo);
                conInfo.DatabaseName = cmbVeritabani.Text;
            }
            catch
            {
                if (cmbDil.SelectedIndex == 1)
                {
                    MessageBox.Show("Bağlantı Sağlanamadı");
                }
                else
                {
                    MessageBox.Show("Check connection info");
                }
            }

            chkRTables.Checked = false;
            chkDBTable.Checked = false;
            chkDBColumn.Checked = false;

            if (cmbVeritabani.Items.Count <= 0)
            {
                cmbVeritabani.Enabled = false;

                btnBaslat.Enabled = false;
                chkRTables.Enabled = false;
                chkDBTable.Enabled = false;
                chkDBColumn.Enabled = false;
            }
            else
            {
                cmbVeritabani.Enabled = true;

                btnBaslat.Enabled = true;
                chkRTables.Enabled = true;
                chkDBTable.Enabled = true;
                chkDBColumn.Enabled = true;

                if (cmbVeritabani.Text != "")
                {
                    try
                    {
                        tableNames.Clear();
                        tableNames = Helper.TableNames(conInfo);

                        DBName = cmbVeritabani.Text;
                    }
                    catch
                    {
                        if (cmbDil.SelectedIndex == 1)
                        {
                            MessageBox.Show("Bağlantı Sağlanamadı");
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Check connection info");
                            return;
                        }
                    }
                }
            }
        }

        private void btnBaslat_Click(object sender, EventArgs e)
        {
            folderDialogKatmanOlustur.SelectedPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (folderDialogKatmanOlustur.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathAddress = folderDialogKatmanOlustur.SelectedPath;

                if (DBName != "")
                {
                    GetTableColumns();
                    CreateModels();

                    if (cmbDil.SelectedIndex == 1)
                    {
                        MessageBox.Show("Modeller Başarıyla Oluşturuldu.");
                    }
                    else
                    {
                        MessageBox.Show("Models created succesfully.");
                    }
                }
                else
                {
                    if (cmbDil.SelectedIndex == 1)
                    {
                        MessageBox.Show("Lütfen önce bir veritabanına bağlanın.");
                    }
                    else
                    {
                        MessageBox.Show("Please connect to database.");
                    }
                }
            }
        }

        private void cmbVeritabani_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVeritabani.Text != "")
            {
                try
                {
                    conInfo.DatabaseName = cmbVeritabani.Text;
                    tableNames = Helper.TableNames(conInfo);

                    DBName = cmbVeritabani.Text;
                }
                catch
                {
                    if (cmbDil.SelectedIndex == 1)
                    {
                        MessageBox.Show("Bağlantı Sağlanamadı");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Check connection info");
                        return;
                    }
                }
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
        }

        void GetTableColumns()
        {
            tableColumns = new List<TableColumns>();

            foreach (string item in tableNames)
            {
                tableColumns.Add(new TableColumns()
                {
                    TableName = item,
                    ColumnInfos = Helper.ColumnNames(conInfo, item)
                });
            }
        }

        #endregion

        #region Create Models

        void CreateModels()
        {
            Directory.CreateDirectory(PathAddress + "\\" + DBName);
            Directory.CreateDirectory(PathAddress + "\\" + DBName + "\\Models");

            foreach (TableColumns table in tableColumns)
            {
                string Table = table.TableName.IlkHarfBuyuk();
                StreamWriter yaz = File.CreateText(PathAddress + "\\" + DBName + "\\Models\\" + Table + ".cs");
                List<string> identityColumns = Helper.ReturnIdentityColumn(conInfo, table.TableName);

                SqlConnection con = new SqlConnection(Helper.CreateConnectionText(conInfo));
                List<ForeignKeyChecker> fkcList = Check(con, table.TableName);
                fkcList = fkcList.Where(a => a.PrimaryTableName == table.TableName).ToList();

                yaz.WriteLine("using System;");

                if (chkRTables.Checked == true)
                {
                    if (fkcList.Count > 0)
                    {
                        yaz.WriteLine("using System.Collections.Generic;");

                        foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                        {
                            string ForeignTableName = fkc.ForeignTableName.IlkHarfBuyuk();
                            yaz.WriteLine("using Models." + ForeignTableName + "Model;");
                        }
                    }
                }

                yaz.WriteLine("using TDFramework.Common.TDModel;");
                yaz.WriteLine("using TDFramework.Common.Attributes;");

                yaz.WriteLine("");

                yaz.WriteLine("namespace Models." + Table + "Model");
                yaz.WriteLine("{");

                if (chkDBTable.Checked == true)
                {
                    yaz.WriteLine("\t[DBTable(Name=\"" + Table + "\")]");
                }

                yaz.WriteLine("\tpublic class " + Table + " : ITDModel");
                yaz.WriteLine("\t{");

                if (chkRTables.Checked == true)
                {
                    if (fkcList.Count > 0)
                    {
                        yaz.WriteLine("\t\tpublic " + Table + "()");
                        yaz.WriteLine("\t\t{");

                        foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                        {
                            string ForeignTableName = fkc.ForeignTableName.IlkHarfBuyuk();
                            yaz.WriteLine("\t\t\tthis." + ForeignTableName + "List = new List<" + ForeignTableName + ">();");

                        }
                        yaz.WriteLine("\t\t}");
                        yaz.WriteLine("");
                    }
                }

                int counter = table.ColumnInfos.Count;
                int i = 1;

                foreach (ColumnInfo column in table.ColumnInfos)
                {
                    if (column.DataType != null)
                    {
                        if (column.IsPrimaryKey == true)
                        {
                            yaz.WriteLine("\t\t[PKey]");
                        }

                        if (column.IsIdentity == true)
                        {
                            yaz.WriteLine("\t\t[IDColumn]");
                        }

                        if (chkDBColumn.Checked == true)
                        {
                            yaz.WriteLine("\t\t[TableColumn(Name=\"" + column.ColumnName.IlkHarfBuyuk() + "\")]");

                        }

                        if (column.DataType.ReturnType().Name == "String")
                        {
                            if (column.MaxLength.ToUpper() == "MAX")
                            {
                                yaz.WriteLine("\t\t[DataType(DataType.MultilineText)]");
                            }
                        }

                        if (column.IsNullable)
                        {
                            switch (column.DataType.ReturnType().Name)
                            {
                                case "Int16": yaz.WriteLine("\t\tpublic int? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Int32": yaz.WriteLine("\t\tpublic int? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Int64": yaz.WriteLine("\t\tpublic Int64? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Decimal": yaz.WriteLine("\t\tpublic decimal? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Double": yaz.WriteLine("\t\tpublic double? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Char": yaz.WriteLine("\t\tpublic char " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Chars": yaz.WriteLine("\t\tpublic char[] " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "String": yaz.WriteLine("\t\tpublic string " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Byte": yaz.WriteLine("\t\tpublic byte " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Bytes": yaz.WriteLine("\t\tpublic byte[] " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Boolean": yaz.WriteLine("\t\tpublic bool? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "DateTime": yaz.WriteLine("\t\tpublic DateTime? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "DateTimeOffset": yaz.WriteLine("\t\tpublic DateTimeOffset? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "TimeSpan": yaz.WriteLine("\t\tpublic TimeSpan? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Single": yaz.WriteLine("\t\tpublic Single? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Object": yaz.WriteLine("\t\tpublic object " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Guid": yaz.WriteLine("\t\tpublic Guid? " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                default: yaz.WriteLine("\t\tpublic string " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                            }
                        }
                        else
                        {
                            switch (column.DataType.ReturnType().Name)
                            {
                                case "Int16": yaz.WriteLine("\t\tpublic int " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Int32": yaz.WriteLine("\t\tpublic int " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Int64": yaz.WriteLine("\t\tpublic Int64 " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Decimal": yaz.WriteLine("\t\tpublic decimal " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Double": yaz.WriteLine("\t\tpublic double " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Char": yaz.WriteLine("\t\tpublic char " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Chars": yaz.WriteLine("\t\tpublic char[] " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "String": yaz.WriteLine("\t\tpublic string " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Byte": yaz.WriteLine("\t\tpublic byte " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Bytes": yaz.WriteLine("\t\tpublic byte[] " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Boolean": yaz.WriteLine("\t\tpublic bool " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "DateTime": yaz.WriteLine("\t\tpublic DateTime " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "DateTimeOffset": yaz.WriteLine("\t\tpublic DateTimeOffset " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "TimeSpan": yaz.WriteLine("\t\tpublic TimeSpan " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Single": yaz.WriteLine("\t\tpublic Single " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Object": yaz.WriteLine("\t\tpublic object " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                case "Guid": yaz.WriteLine("\t\tpublic Guid " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                                default: yaz.WriteLine("\t\tpublic string " + column.ColumnName.IlkHarfBuyuk() + " { get; set; }"); break;
                            }
                        }
                    }
                    else
                    {
                        if (cmbDil.SelectedIndex == 1)
                        {
                            yaz.WriteLine("\t\t//" + column.ColumnName.IlkHarfBuyuk() + " isimli kolonun veri tipi bu programda tanumlı değil.");
                        }
                        else
                        {
                            yaz.WriteLine("\t\t//" + column.ColumnName.IlkHarfBuyuk() + " data type is unknown");
                        }
                    }

                    if (chkDBColumn.Checked == true)
                    {
                        if (counter != i)
                        {
                            yaz.WriteLine("");
                        }
                    }

                    i++;
                }

                yaz.WriteLine("");
                yaz.WriteLine("\t\t[AggregateColumn]");
                yaz.WriteLine("\t\tpublic dynamic AggColumn { get; set; }");

                if (chkRTables.Checked == true)
                {
                    if (fkcList.Count > 0)
                    {
                        yaz.WriteLine("");

                        foreach (ForeignKeyChecker fkc in fkcList.GroupBy(a => a.ForeignTableName).Select(a => a.First()).ToList())
                        {
                            string ForeignTableName = fkc.ForeignTableName.IlkHarfBuyuk();
                            yaz.WriteLine("\t\t[RTable]");
                            yaz.WriteLine("\t\tpublic List<" + ForeignTableName + "> " + ForeignTableName + "List { get; set; }");
                        }
                    }
                }

                yaz.WriteLine("\t}");

                yaz.WriteLine("");

                yaz.WriteLine("\tpublic enum " + Table + "Columns");
                yaz.WriteLine("\t{");

                ColumnInfo lastColumn = table.ColumnInfos.Last();

                foreach (ColumnInfo column in table.ColumnInfos)
                {
                    if (column.DataType.ReturnType() != null)
                    {
                        if (column != lastColumn)
                        {
                            yaz.WriteLine("\t\t" + column.ColumnName.IlkHarfBuyuk() + ",");
                        }
                        else
                        {
                            yaz.WriteLine("\t\t" + column.ColumnName.IlkHarfBuyuk());
                        }
                    }
                    else
                    {
                        if (cmbDil.SelectedIndex == 1)
                        {
                            yaz.WriteLine("\t\t//" + column.ColumnName.IlkHarfBuyuk() + " isimli kolonun veri tipi bu programda tanumlı değil.");
                        }
                        else
                        {
                            yaz.WriteLine("\t\t//" + column.ColumnName.IlkHarfBuyuk() + " data type is unknown");
                        }
                    }
                }

                yaz.WriteLine("\t}");
                yaz.WriteLine("}");

                yaz.Close();
            }
        }

        #endregion

        public List<ForeignKeyChecker> Check(SqlConnection con, string _tableName = null)
        {
            SqlDataAdapter dataAdap = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            List<ForeignKeyChecker> fkList = new List<ForeignKeyChecker>();

            string _queryString = @"SELECT fk.Name as ForeignKeyName, " +
                                    "fk.referenced_object_id as PrimaryTableID, " +
                                    "OBJECT_NAME(fk.referenced_object_id) as PrimaryTableName, " +
                                    "cpa.object_id as PrimaryColumnID, " +
                                    "cref.name as PrimaryColumnName, " +
                                    "fk.parent_object_id as ForeignTableID, " +
                                    "OBJECT_NAME(fk.parent_object_id) as ForeignTableName, " +
                                    "cref.object_id as ForeignColumnID, " +
                                    "cpa.name as ForeignColumnName " +
                                    "FROM sys.foreign_keys fk, sys.foreign_key_columns fkc, sys.columns cpa, sys.columns cref " +
                                    "WHERE  fkc.constraint_object_id = fk.object_id " +
                                    "AND fkc.parent_object_id = cpa.object_id " +
                                    "AND fkc.parent_column_id = cpa.column_id " +
                                    "AND  fkc.referenced_object_id = cref.object_id " +
                                    "AND fkc.referenced_column_id = cref.column_id ";

            dataAdap.SelectCommand = new SqlCommand();
            dataAdap.SelectCommand.Connection = con;

            if (_tableName != null)
            {
                _queryString += " AND (OBJECT_NAME(fk.referenced_object_id)=@TableName OR OBJECT_NAME(fk.parent_object_id)=@TableName)";
                dataAdap.SelectCommand.Parameters.AddWithValue("TableName", _tableName);
            }

            dataAdap.SelectCommand.CommandText = _queryString;

            try
            {
                con.Open();
                dataAdap.Fill(dataTable);

                if (dataTable != null)
                {
                    try
                    {
                        foreach (DataRow tableItem in dataTable.Rows)
                        {
                            ForeignKeyChecker data = new ForeignKeyChecker()
                            {
                                ForeignKeyName = tableItem.Table.Columns.Contains("ForeignKeyName") ? tableItem["ForeignKeyName"] != DBNull.Value ? tableItem["ForeignKeyName"].ToString() : null : null,
                                PrimaryTableName = tableItem.Table.Columns.Contains("PrimaryTableName") ? tableItem["PrimaryTableName"] != DBNull.Value ? tableItem["PrimaryTableName"].ToString() : null : null,
                                PrimaryColumnName = tableItem.Table.Columns.Contains("PrimaryColumnName") ? tableItem["PrimaryColumnName"] != DBNull.Value ? tableItem["PrimaryColumnName"].ToString() : null : null,
                                ForeignTableName = tableItem.Table.Columns.Contains("ForeignTableName") ? tableItem["ForeignTableName"] != DBNull.Value ? tableItem["ForeignTableName"].ToString() : null : null,
                                ForeignColumnName = tableItem.Table.Columns.Contains("ForeignColumnName") ? tableItem["ForeignColumnName"] != DBNull.Value ? tableItem["ForeignColumnName"].ToString() : null : null,
                            };

                            fkList.Add(data);
                        }
                    }
                    catch
                    {
                        return new List<ForeignKeyChecker>();
                    }
                }
            }
            catch
            {
                return new List<ForeignKeyChecker>();
            }
            finally
            {
                con.Close();
            }

            return fkList;
        }

        private void cmbDil_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetText();
        }

        private void SetText()
        {
            if (cmbDil.SelectedIndex == 1)
            {
                lblDil.Text = "Dil :";
                lblSunucu.Text = "Sunucu :";
                lblKullaniciAdi.Text = "Kullanıcı Adı :";
                lblSifre.Text = "Şifre : ";
                btnVeriTabaniGetir.Text = "Veritabanlarını Getir";
                chkRTables.Text = "Bağlı Tablolar - Tablolar Arası Relation'a göre özellik ekler";
                chkDBTable.Text = "DBTable Attribute Ekle";
                chkDBColumn.Text = "TableColumn Attribute Ekle";
                btnBaslat.Text = "Modelleri Oluştur";
                tabpageModelOlustur.Text = "Model Oluştur";
            }
            else
            {
                lblDil.Text = "Lang :";
                lblSunucu.Text = "Server :";
                lblKullaniciAdi.Text = "Username :";
                lblSifre.Text = "Password : ";
                btnVeriTabaniGetir.Text = "Fill Databases";
                chkRTables.Text = "Related Tables - If column has relation, it adds property for it";
                chkDBTable.Text = "Add DBTable Attribute";
                chkDBColumn.Text = "Add TableColumn Attributes";
                btnBaslat.Text = "Create Models";
                tabpageModelOlustur.Text = "Create Model";
            }
        }
    }

    public static class ExMethods
    {
        public static string IlkHarfBuyuk(this string text)
        {
            text = text[0].ToString().ToUpper() + text.Remove(0, 1);

            text = text.Replace("Ğ", "G");
            text = text.Replace("Ü", "U");
            text = text.Replace("Ş", "S");
            text = text.Replace("İ", "I");
            text = text.Replace("Ö", "O");
            text = text.Replace("Ç", "C");
            text = text.Replace("Â", "a");

            return text;
        }

        public static string IlkHarfKucuk(this string text)
        {
            text = text[0].ToString().ToLower() + text.Remove(0, 1);

            text = text.Replace("ğ", "g");
            text = text.Replace("ü", "u");
            text = text.Replace("ş", "s");
            text = text.Replace("ı", "i");
            text = text.Replace("ö", "o");
            text = text.Replace("ç", "c");
            text = text.Replace("â", "a");

            return text;
        }
    }

    public class ForeignKeyChecker
    {
        public string ForeignKeyName { get; set; }
        public string PrimaryTableName { get; set; }
        public string PrimaryColumnName { get; set; }
        public string ForeignTableName { get; set; }
        public string ForeignColumnName { get; set; }
    }
}