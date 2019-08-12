using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using TDFactory.Helper;
using Common;
using System.Threading;

namespace TDFactory
{
    public partial class TDFactory : Form
    {
        public TDFactory()
        {
            InitializeComponent();
            Relations = new List<Relation>();
        }

        ConnectionInfo connectionInfo;
        Thread t;

        public static List<Relation> Relations { get; set; }
        public static string PathAddress { get; set; }

        List<ColumnInfo> columnInfos;
        List<ColumnInfo> tempColumnInfos = new List<ColumnInfo>();
        List<ColumnInfo> tableColumnInfos = new List<ColumnInfo>();

        List<string> foreignTables;
        List<string> tableNames;
        List<string> selectedTables;
        List<string> ckEditors;

        public string[] UrlColumns;
        public string[] GuidColumns;
        public string[] CodeColumns;
        public string[] DeletedColumns;
        public string[] FileColumns;
        public string[] ImageColumns;


        int tableindex, selectedtableindex;
        bool tableselected = false;
        string DBName = "";
        string selectedcolumn;
        string projectName = "Proje";
        string projectFolder = "";
        string[] aliases = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        private void TDFactoryForm_Load(object sender, EventArgs e)
        {
            cmbVTVeriTipi.SelectedIndex = 0;
            lstAndIzin.SetSelected(0, true);

            t = new Thread(new ThreadStart(ListControl));
            t.Start();
        }

        private void TDFactoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }

        delegate void SetResultCallBack(bool result);

        private void SetResult(bool result)
        {
            pnlKaydet.Enabled = result;
        }

        void ListControl()
        {
            SetResultCallBack d = new SetResultCallBack(SetResult);

            while (Application.OpenForms.Count > 0)
            {
                if (lstSeciliKolonlar.Items.Count > 0)
                {
                    this.Invoke(d, new object[] { true });
                }
                else
                {
                    this.Invoke(d, new object[] { false });
                }

                Thread.Sleep(100);
            }
        }

        public string DefaultSchema(SqlConnection con)
        {
            SqlDataAdapter dataAdap = new SqlDataAdapter();
            DataTable dataTable = new DataTable();
            List<ForeignKeyChecker> fkList = new List<ForeignKeyChecker>();

            string _queryString = @"SELECT top 1 default_schema_name FROM sys.database_principals WHERE type = 'S'";

            dataAdap.SelectCommand = new SqlCommand();
            dataAdap.SelectCommand.Connection = con;

            if (txtWCKullanici.Text != "")
            {
                _queryString += " and name = @user";
                dataAdap.SelectCommand.Parameters.AddWithValue("user", txtWCKullanici.Text);
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
                        return "[" + dataTable.Rows[0]["default_schema_name"].ToString() + "]";
                    }
                    catch
                    {
                        return "[dbo]";
                    }
                }
            }
            catch
            {
                return "[dbo]";
            }
            finally
            {
                con.Close();
            }

            return "[dbo]";
        }

        public List<ForeignKeyChecker> ForeignKeyCheck(SqlConnection con, string _tableName = null)
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
                                PrimaryTableAndColumnName = tableItem.Table.Columns.Contains("PrimaryTableName") && tableItem.Table.Columns.Contains("ForeignColumnName") ? tableItem["PrimaryTableName"] != DBNull.Value && tableItem["ForeignColumnName"] != DBNull.Value ? tableItem["PrimaryTableName"].ToString() + tableItem["ForeignColumnName"].ToString() : null : null,
                                ForeignTableAndColumnName = tableItem.Table.Columns.Contains("ForeignTableName") && tableItem.Table.Columns.Contains("ForeignColumnName") ? tableItem["ForeignTableName"] != DBNull.Value && tableItem["ForeignColumnName"] != DBNull.Value ? tableItem["ForeignTableName"].ToString() + tableItem["ForeignColumnName"].ToString() : null : null,
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

            foreach (ForeignKeyChecker fkc in fkList)
            {
                if (fkc.PrimaryColumnName != null)
                {
                    List<ColumnInfo> columns = tableColumnInfos.Where(a => a.TableName == _tableName && a.ColumnName == fkc.PrimaryColumnName).ToList();

                    if (columns.Count > 0)
                    {
                        fkc.PrimaryColumnType = columns.FirstOrDefault().Type.Name;
                        fkc.ForeignColumnType = columns.FirstOrDefault().Type.Name;
                    }
                }
            }

            return fkList;
        }

        public List<ColumnInfo> GetTableColumnInfos()
        {
            List<ColumnInfo> columnInfo;

            string[] tablecolumn = new string[2];

            tableColumnInfos.Clear();

            foreach (object item in lstSeciliKolonlar.Items)
            {
                if (chkWindowsAuthentication.Checked == true)
                {
                    columnInfo = Helper.Helper.GetColumnsInfo(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text }, item.ToString().Split(' ')[1].Replace("[", "").Replace("]", ""));
                }
                else
                {
                    columnInfo = Helper.Helper.GetColumnsInfo(new ConnectionInfo() { Server = txtSunucu.Text, DatabaseName = cmbVeritabani.Text, IsWindowsAuthentication = false, Username = txtKullaniciAdi.Text, Password = txtSifre.Text }, item.ToString().Split(' ')[1].Replace("[", "").Replace("]", ""));
                }

                tableColumnInfos.Add(columnInfo.Where(a => a.ColumnName == item.ToString().Split(' ')[0]).FirstOrDefault());
            }

            return tableColumnInfos;
        }

        public List<string> GetSelectedTableNames(List<ColumnInfo> _tableColumnNames)
        {
            List<string> returnList = new List<string>();

            foreach (ColumnInfo item in _tableColumnNames)
            {
                if (!returnList.Contains(item.TableName))
                {
                    returnList.Add(item.TableName);
                }
            }

            return returnList;
        }

        public string GetColumnText(List<ColumnInfo> columns, bool toString = true)
        {
            string columnText = "";

            if (columns.Count > 1)
            {
                foreach (ColumnInfo item in columns)
                {
                    if (item.Type.Name == "String")
                    {
                        columnText = item.ColumnName;
                        break;
                    }
                }
            }

            string tostring = toString ? ".ToString()" : "";

            if (columnText == "")
            {
                if (columns.Count > 1)
                {
                    columnText = columns[1].ColumnName + tostring;
                }
                else
                {
                    columnText = columns[0].ColumnName + tostring;
                }
            }

            return columnText;
        }

        public List<ColumnInfo> TableColumns(string Table, ColumnType? columnType = null)
        {
            List<ColumnInfo> columnNames = tableColumnInfos.Where(a => a.TableName == Table).ToList();

            switch (columnType)
            {
                case ColumnType.UrlColumns:
                    return columnNames.Where(a => a.ColumnName.In(UrlColumns, ExMethods.InType.ToUrlLower)).ToList();
                case ColumnType.GuidColumns:
                    return columnNames.Where(a => a.ColumnName.In(GuidColumns, ExMethods.InType.ToUrlLower)).ToList();
                case ColumnType.CodeColumns:
                    return columnNames.Where(a => a.ColumnName.In(CodeColumns, ExMethods.InType.ToUrlLower)).ToList();
                case ColumnType.DeletedColumns:
                    return columnNames.Where(a => a.ColumnName.In(DeletedColumns, ExMethods.InType.ToUrlLower)).ToList();
                case ColumnType.FileColumns:
                    return columnNames.Where(a => a.ColumnName.In(FileColumns, ExMethods.InType.ToUrlLower)).ToList();
                case ColumnType.ImageColumns:
                    return columnNames.Where(a => a.ColumnName.In(ImageColumns, ExMethods.InType.ToUrlLower)).ToList();
                default:
                    return columnNames;
            }

        }

        public enum ColumnType
        {
            UrlColumns,
            GuidColumns,
            CodeColumns,
            DeletedColumns,
            FileColumns,
            ImageColumns
        }
    }
}