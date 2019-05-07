using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TDFactoryEF.Helper
{
    public class ConnectionInfo
    {
        public ConnectionInfo()
        {
            this.IsWindowsAuthentication = true;
        }

        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsWindowsAuthentication { get; set; }
    }

    public class ColumnInfo
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public string IsNullable { get; set; }
        public string MaxLength { get; set; }
        public string DefaultValue { get; set; }
        public string OrdinalPosition { get; set; }
        public string IsPrimaryKey { get; set; }
        public string IsIdentity { get; set; }
        public string SeedValue { get; set; }
        public string IncrementValue { get; set; }
    }

    public class Helper
    {
        public static string CreateConnectionText(ConnectionInfo _conInfo)
        {
            string connectionString = "";

            if (!String.IsNullOrEmpty(_conInfo.Server))
            {
                connectionString += "Data Source=" + _conInfo.Server + ";";
            }

            if (!String.IsNullOrEmpty(_conInfo.DatabaseName))
            {
                connectionString += "Initial Catalog=" + _conInfo.DatabaseName + ";";
            }

            if (_conInfo.IsWindowsAuthentication)
            {
                connectionString += "Trusted_Connection=True;";
            }
            else
            {
                if (!String.IsNullOrEmpty(_conInfo.Username))
                {
                    connectionString += "User ID=" + _conInfo.Username + ";";
                }

                if (!String.IsNullOrEmpty(_conInfo.Password))
                {
                    connectionString += "Password=" + _conInfo.Password + ";";
                }
            }

            return connectionString;
        }

        public static List<string> DatabaseNames(ConnectionInfo _conInfo)
        {
            List<string> dbNames = new List<string>();
            SqlCommand cmd = new SqlCommand();

            string connectionString = Helper.CreateConnectionText(_conInfo);

            SqlConnection con = new SqlConnection(connectionString);

            if (_conInfo.IsWindowsAuthentication == true)
            {
                cmd.CommandText = "SELECT NAME FROM SYS.DATABASES";
            }
            else
            {
                cmd.CommandText = "select dbname from sys.syslogins where name not like '%##%' and name = '" + _conInfo.Username + "'";
            }

            cmd.Connection = con;
            con.Open();

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                dbNames.Add(rd[0].ToString());
            }

            con.Close();

            return dbNames;
        }

        public static List<string> TableNames(ConnectionInfo _connection)
        {
            List<string> tableNames = new List<string>();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            string connectionString = String.IsNullOrEmpty(_connection.Server) ? "" : "Data Source=" + _connection.Server + ";";
            connectionString += String.IsNullOrEmpty(_connection.DatabaseName) ? "" : "Initial Catalog = " + _connection.DatabaseName + ";";

            if (_connection.IsWindowsAuthentication)
            {
                con.ConnectionString = connectionString + "Trusted_Connection=True;";
            }
            else
            {
                if (!String.IsNullOrEmpty(_connection.Username))
                {
                    con.ConnectionString += "User ID=" + _connection.Username + ";";
                }

                if (!String.IsNullOrEmpty(_connection.Password))
                {
                    con.ConnectionString += "Password=" + _connection.Password + ";";
                }
            }

            cmd.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '" + _connection.DatabaseName + "' ORDER BY TABLE_NAME ASC";
            cmd.Connection = con;

            con.Open();

            SqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                tableNames.Add(rd[0].ToString());
            }

            con.Close();

            return tableNames;
        }

        public static List<ColumnInfo> ColumnNames(ConnectionInfo _conInfo, string _tableName)
        {
            List<ColumnInfo> columnInfo = new List<ColumnInfo>();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            string connectionString = String.IsNullOrEmpty(_conInfo.Server) ? "" : "Data Source=" + _conInfo.Server + ";";
            connectionString += String.IsNullOrEmpty(_conInfo.DatabaseName) ? "" : "Initial Catalog = " + _conInfo.DatabaseName + ";";

            if (_conInfo.IsWindowsAuthentication)
            {
                con.ConnectionString = connectionString + "Trusted_Connection=True;";
            }
            else
            {
                if (!String.IsNullOrEmpty(_conInfo.Username))
                {
                    con.ConnectionString += "User ID=" + _conInfo.Username + ";";
                }

                if (!String.IsNullOrEmpty(_conInfo.Password))
                {
                    con.ConnectionString += "Password=" + _conInfo.Password + ";";
                }
            }

            cmd.CommandText = "SELECT c.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE, c.is_nullable, case when c.character_maximum_length = -1 Then 'MAX' else Convert(nvarchar, c.character_maximum_length) end as character_maximum_length, c.Column_default, c.ORDINAL_POSITION, CASE WHEN (SELECT COUNT(1) FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc, INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME AND c.COLUMN_NAME = ku.COLUMN_NAME AND c.TABLE_CATALOG = ku.TABLE_CATALOG AND c.TABLE_NAME = ku.TABLE_NAME AND c.COLUMN_NAME = ku.COLUMN_NAME) = 1 THEN 'YES' ELSE 'NO' END AS IsPrimaryKey, CASE WHEN (Select COUNT(1) from sys.columns sc, sys.tables st where sc.object_id = st.object_id and sc.is_identity = 1 and sc.name = c.COLUMN_NAME and st.name = c.TABLE_NAME) = 1 THEN 'YES' ELSE 'NO' END AS IsIdentity, (Select sic.seed_value from sys.columns sc, sys.identity_columns sic, sys.tables st where sc.object_id = st.object_id and sic.object_id = st.object_id and st.name = c.TABLE_NAME and sc.name = c.COLUMN_NAME and sic.is_identity = sc.is_identity) AS SeedValue, (Select sic.increment_value from sys.columns sc, sys.identity_columns sic, sys.tables st where sc.object_id = st.object_id and sic.object_id = st.object_id and st.name = c.TABLE_NAME and sc.name = c.COLUMN_NAME and sic.is_identity = sc.is_identity) AS IncrementValue FROM INFORMATION_SCHEMA.COLUMNS c WHERE c.TABLE_NAME = '" + _tableName + "' AND c.TABLE_CATALOG = '" + _conInfo.DatabaseName + "' ORDER BY c.ORDINAL_POSITION ASC";
            cmd.Connection = con;

            try
            {
                con.Open();

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    columnInfo.Add(new ColumnInfo()
                    {
                        TableName = rd[0].ToString(),
                        ColumnName = rd[1].ToString(),
                        DataType = rd[2].ToString(),
                        IsNullable = rd[3].ToString(),
                        MaxLength = rd[4].ToString(),
                        DefaultValue = rd[5].ToString() == "NULL" || rd[5].ToString() == "(NULL)" ? null : rd[5].ToString().Replace("(", "").Replace(")", ""),
                        OrdinalPosition = rd[6].ToString(),
                        IsPrimaryKey = rd[7].ToString(),
                        IsIdentity = rd[8].ToString(),
                        SeedValue = rd[9].ToString(),
                        IncrementValue = rd[10].ToString()
                    });
                }

            }
            catch
            {
                return new List<ColumnInfo>();
            }
            finally
            {
                con.Close();
            }

            return columnInfo;
        }

        public static List<string> ReturnIdentityColumn(ConnectionInfo _conInfo, string _tableName)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            List<string> identityColumns = new List<string>();

            string connectionString = String.IsNullOrEmpty(_conInfo.Server) ? "" : "Data Source=" + _conInfo.Server + ";";
            connectionString += String.IsNullOrEmpty(_conInfo.DatabaseName) ? "" : "Initial Catalog = " + _conInfo.DatabaseName + ";";

            if (_conInfo.IsWindowsAuthentication)
            {
                con.ConnectionString = connectionString + "Trusted_Connection=True;";
            }
            else
            {
                if (!String.IsNullOrEmpty(_conInfo.Username))
                {
                    con.ConnectionString += "User ID=" + _conInfo.Username + ";";
                }

                if (!String.IsNullOrEmpty(_conInfo.Password))
                {
                    con.ConnectionString += "Password=" + _conInfo.Password + ";";
                }
            }

            cmd.CommandText = "Select c.COLUMN_NAME from sys.columns sc, sys.identity_columns sic, sys.tables st, INFORMATION_SCHEMA.COLUMNS c where sc.object_id = st.object_id and sic.object_id = st.object_id and st.name = c.TABLE_NAME and sc.name = c.COLUMN_NAME and sic.is_identity = sc.is_identity and c.TABLE_NAME = '" + _tableName +"'  AND c.TABLE_CATALOG = '" + _conInfo.DatabaseName + "'";
            cmd.Connection = con;

            try
            {
                con.Open();

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    identityColumns.Add(rd[0].ToString());
                }

            }
            catch
            {
                return new List<string>();
            }
            finally
            {
                con.Close();
            }

            return identityColumns;
        }
    }

    public class TableColumnNames
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public Type TypeName { get; set; }
        public bool IsNullable { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string CharLength { get; set; }
    }
}
