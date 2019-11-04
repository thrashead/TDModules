using Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace TDFactory
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
        public bool IsNullable { get; set; }
        public string MaxLength { get; set; }
        public string DefaultValue { get; set; }
        public string OrdinalPosition { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsIdentity { get; set; }
        public string SeedValue { get; set; }
        public string IncrementValue { get; set; }
        public Type Type { get; set; }
        public int CharLength { get; set; }
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
            else
            {
                connectionString += "Data Source=.;";
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

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = CreateConnectionText(_conInfo);

                if (_conInfo.IsWindowsAuthentication == true)
                {
                    cmd.CommandText = "SELECT NAME FROM SYS.DATABASES";
                }
                else
                {
                    cmd.CommandText = "select dbname from sys.syslogins where name not like '%##%' and name = '" + _conInfo.Username + "'";
                }

                cmd.Connection = con;

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    dbNames.Add(rd[0].ToString());
                }

                con.Close();
            }

            return dbNames;
        }

        public static List<string> TableNames(ConnectionInfo _conInfo)
        {
            List<string> tableNames = new List<string>();
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = CreateConnectionText(_conInfo);

                cmd.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '" + _conInfo.DatabaseName + "' ORDER BY TABLE_NAME ASC";
                cmd.Connection = con;

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    tableNames.Add(rd[0].ToString());
                }

                con.Close();
            }
            return tableNames;
        }

        public static List<ColumnInfo> GetColumnsInfo(ConnectionInfo _conInfo, string _tableName)
        {
            List<ColumnInfo> columnInfo = new List<ColumnInfo>();
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = CreateConnectionText(_conInfo);

                cmd.CommandText = "SELECT c.TABLE_NAME, c.COLUMN_NAME, c.DATA_TYPE, c.is_nullable, case when c.character_maximum_length = -1 Then 'MAX' else Convert(nvarchar, c.character_maximum_length) end as character_maximum_length, c.Column_default, c.ORDINAL_POSITION, CASE WHEN (SELECT COUNT(1) FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc, INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku WHERE tc.CONSTRAINT_TYPE = 'PRIMARY KEY' AND tc.CONSTRAINT_NAME = ku.CONSTRAINT_NAME AND c.COLUMN_NAME = ku.COLUMN_NAME AND c.TABLE_CATALOG = ku.TABLE_CATALOG AND c.TABLE_NAME = ku.TABLE_NAME AND c.COLUMN_NAME = ku.COLUMN_NAME) = 1 THEN 'YES' ELSE 'NO' END AS IsPrimaryKey, CASE WHEN (Select COUNT(1) from sys.columns sc, sys.tables st where sc.object_id = st.object_id and sc.is_identity = 1 and sc.name = c.COLUMN_NAME and st.name = c.TABLE_NAME) = 1 THEN 'YES' ELSE 'NO' END AS IsIdentity, (Select sic.seed_value from sys.columns sc, sys.identity_columns sic, sys.tables st where sc.object_id = st.object_id and sic.object_id = st.object_id and st.name = c.TABLE_NAME and sc.name = c.COLUMN_NAME and sic.is_identity = sc.is_identity) AS SeedValue, (Select sic.increment_value from sys.columns sc, sys.identity_columns sic, sys.tables st where sc.object_id = st.object_id and sic.object_id = st.object_id and st.name = c.TABLE_NAME and sc.name = c.COLUMN_NAME and sic.is_identity = sc.is_identity) AS IncrementValue FROM INFORMATION_SCHEMA.COLUMNS c WHERE c.TABLE_NAME = '" + _tableName + "' AND c.TABLE_CATALOG = '" + _conInfo.DatabaseName + "' ORDER BY c.ORDINAL_POSITION ASC";
                cmd.Connection = con;

                try
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        columnInfo.Add(new ColumnInfo()
                        {
                            TableName = rd[0].ToString(),
                            ColumnName = rd[1].ToString(),
                            DataType = rd[2].ToString() == "decimal" ? "decimal(18, 2)" : rd[2].ToString(),
                            IsNullable = rd[3].ToString() == "YES" ? true : false,
                            MaxLength = rd[4].ToString(),
                            DefaultValue = rd[5].ToString() == "NULL" || rd[5].ToString() == "(NULL)" ? null : rd[5].ToString().Replace("(", "").Replace(")", ""),
                            OrdinalPosition = rd[6].ToString(),
                            IsPrimaryKey = rd[7].ToString() == "YES" ? true : false,
                            IsIdentity = rd[8].ToString() == "YES" ? true : false,
                            SeedValue = rd[9].ToString(),
                            IncrementValue = rd[10].ToString(),
                            Type = SqlTypes.ReturnType(rd[2].ToString()),
                            CharLength = rd[4].ToString().ToUpper() == "" || rd[4].ToString().ToUpper() == "MAX" ? -1 : Convert.ToInt32(rd[4].ToString())
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
            }
            return columnInfo;
        }

        public static List<string> ReturnIdentityColumn(ConnectionInfo _conInfo, string _tableName)
        {
            List<string> identityColumns = new List<string>();
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = CreateConnectionText(_conInfo);

                cmd.CommandText = "Select c.COLUMN_NAME from sys.columns sc, sys.identity_columns sic, sys.tables st, INFORMATION_SCHEMA.COLUMNS c where sc.object_id = st.object_id and sic.object_id = st.object_id and st.name = c.TABLE_NAME and sc.name = c.COLUMN_NAME and sic.is_identity = sc.is_identity and c.TABLE_NAME = '" + _tableName + "'  AND c.TABLE_CATALOG = '" + _conInfo.DatabaseName + "'";
                cmd.Connection = con;

                try
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }

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
            }

            return identityColumns;
        }

        public static bool HasUserRights(List<string> tableNames)
        {
            bool control = true;

            if (!tableNames.Contains("Types"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("UserGroupProcess"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("UserGroupRights"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("UserGroups"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("UserGroupTables"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("Users"))
            {
                control = false;
                goto end;
            }

            end:;

            return control;
        }

        public static bool HasLogs(List<string> tableNames)
        {
            bool control = true;

            if (!tableNames.Contains("LogTypes"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("LogProcess"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("Logs"))
            {
                control = false;
                goto end;
            }

            end:;

            return control;
        }

        public static bool HasLinks(List<string> tableNames)
        {
            bool control = true;

            if (!tableNames.Contains("LinkTypes"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("Links"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("Types"))
            {
                control = false;
                goto end;
            }

            end:;

            return control;
        }

        public static bool HasLangs(List<string> tableNames)
        {
            bool control = true;

            if (!tableNames.Contains("LangContent"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("LangContentT"))
            {
                control = false;
                goto end;
            }

            if (!tableNames.Contains("NoLangContent"))
            {
                control = false;
                goto end;
            }

            end:;

            return control;
        }
    }
}
