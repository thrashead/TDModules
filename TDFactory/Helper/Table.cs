using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace TDFactory.Helper
{
    public class Table : TDFactory
    {
        public Table(string tableName, ConnectionInfo conInfo)
        {
            if (!String.IsNullOrEmpty(tableName))
            {
                SqlConnection con = new SqlConnection(Helper.CreateConnectionText(conInfo));
                List<ColumnInfo> tableColumnInfos = GetTableColumnInfos().Where(a => a.TableName == tableName).ToList();
                TableName = tableName;
                Columns = Helper.GetColumnsInfo(conInfo, tableName);
                SearchTextColumn = GetColumnText(Columns);
                URLColumns = Columns.Where(a => a.ColumnName.ToUrl(true).In(ListBoxItems(lstUrlColumns))).ToList();
                GUIDColumns = Columns.Where(a => a.ColumnName.ToUrl(true).In(ListBoxItems(lstGuidColumns))).ToList();
                CODEColumns = Columns.Where(a => a.ColumnName.ToUrl(true).In(ListBoxItems(lstCodeColumns))).ToList();
                DELETEDColumns = Columns.Where(a => a.ColumnName.ToUrl(true).In(ListBoxItems(lstDeletedColumns))).ToList();
                FILEColumns = Columns.Where(a => a.ColumnName.ToUrl(true).In(ListBoxItems(lstFileColumns))).ToList();
                IMAGEColumns = Columns.Where(a => a.ColumnName.ToUrl(true).In(ListBoxItems(lstImageColumns))).ToList();
                SEARCHColumns = Columns.Where(a => a.ColumnName.ToUrl(true).In(ListBoxItems(lstSearchColumns))).ToList();
                EDITORColumns = Columns.Where(a => a.Type.Name == "String" && a.CharLength == -1 && !a.ColumnName.In(ListBoxItems(lstDeletedColumns)) && !a.ColumnName.In(ListBoxItems(lstFileColumns)) && !a.ColumnName.In(ListBoxItems(lstImageColumns))).ToList();
                FkcList = ForeignKeyCheck(con, tableName).Where(a => a.PrimaryTableName == tableName).ToList();
                FkcForeignList = ForeignKeyCheck(con).Where(a => a.ForeignTableName == tableName).ToList();
                IdentityColumns = Helper.ReturnIdentityColumn(conInfo, tableName);
                ID = IdentityColumns.Count > 0 ? IdentityColumns.FirstOrDefault() : "id";
                Deleted = DELETEDColumns.Count > 0 ? true : false;
            }
        }

        public string TableName { get; set; }
        public string ID { get; set; }
        public string SearchTextColumn { get; set; }
        public bool Deleted { get; set; }
        public List<ColumnInfo> Columns { get; set; }
        public List<ColumnInfo> URLColumns { get; set; }
        public List<ColumnInfo> GUIDColumns { get; set; }
        public List<ColumnInfo> CODEColumns { get; set; }
        public List<ColumnInfo> DELETEDColumns { get; set; }
        public List<ColumnInfo> FILEColumns { get; set; }
        public List<ColumnInfo> IMAGEColumns { get; set; }
        public List<ColumnInfo> SEARCHColumns { get; set; }
        public List<ColumnInfo> EDITORColumns { get; set; }
        public List<ForeignKeyChecker> FkcList { get; set; }
        public List<ForeignKeyChecker> FkcForeignList { get; set; }
        public List<string> IdentityColumns { get; set; }

        string[] ListBoxItems(ListBox listBox)
        {
            return listBox.Items.ToStringList();
        }
    }
}
