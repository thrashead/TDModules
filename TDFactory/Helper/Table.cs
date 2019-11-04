using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace TDFactory
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
                FkcList = ForeignKeyCheck(con, TableName).Where(a => a.PrimaryTableName == TableName).ToList();
                FkcForeignList = ForeignKeyCheck(con).Where(a => a.ForeignTableName == TableName).ToList();
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
        public List<ColumnInfo> MAILColumns { get; set; }
        public List<ColumnInfo> EDITORColumns { get; set; }
        public List<ForeignKeyChecker> FkcList { get; set; }
        public List<ForeignKeyChecker> FkcForeignList { get; set; }
        public List<string> IdentityColumns { get; set; }
    }
}
