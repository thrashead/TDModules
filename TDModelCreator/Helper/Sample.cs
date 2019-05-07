using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace DatabasetoClass
{
    public partial class Form1
    {
        OleDbDataReader veri;
        OleDbCommand bag = new OleDbCommand();
        double? D;
        string dosya, baglantitext;
        int i, j;
        string[] tablolar;

        private void CreateDirectories(DatabaseType _dbType)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "");
            Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers");
            Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Business");
            Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Common");
            Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\DataAccess");
            Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Entity");
        }
        private void CreateConnection(DatabaseType _dbType)
        {
            StreamWriter yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Common\\Connection.cs");

            switch (_dbType)
            {
                case DatabaseType.MsSql:
                    MsSqlConnection(yaz);
                    break;
                case DatabaseType.MySql:
                    MySqlConnection(yaz);
                    break;
                case DatabaseType.Access:
                    AccessConnection(yaz);
                    break;
                default:
                    MsSqlConnection(yaz);
                    break;
            }
        }
        private void CreateEntity(DatabaseType _dbType)
        {
            OleDbConnection baglanti = new OleDbConnection(baglantitext);

            dosya = dosya.Substring(0, dosya.IndexOf('.'));
            baglanti.Open();
            bag.Connection = baglanti;

            DataTable dt = new DataTable();
            dt = baglanti.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            tablolar = new string[dt.Rows.Count];

            for (i = 0; i < dt.Rows.Count; i++)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Entity\\" + dt.Rows[i].ItemArray[2].ToString());

                StreamWriter yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Entity\\" + dt.Rows[i].ItemArray[2].ToString() + "\\I" + dt.Rows[i].ItemArray[2].ToString() + ".cs");

                string tablo = dt.Rows[i].ItemArray[2].ToString();
                tablolar[i] = dt.Rows[i].ItemArray[2].ToString();

                bag.CommandText = "select * from " + tablo;
                veri = bag.ExecuteReader();
                // Interface
                yaz.WriteLine("using System;");
                yaz.WriteLine("");
                yaz.WriteLine("namespace Entity." + tablo + "Ent");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic interface I" + tablo);
                yaz.WriteLine("\t{");

                for (j = 0; j < veri.FieldCount; j++)
                {
                    string tip = veri.GetDataTypeName(j).ToString();
                    string kolon = veri.GetName(j).ToString();

                    tip = tip.Replace("DBTYPE_I4", "int");
                    tip = tip.Replace("DBTYPE_BOOL", "bool");
                    tip = tip.Replace("DBTYPE_I2", "bool");
                    tip = tip.Replace("DBTYPE_DATE", "DateTime");
                    tip = tip.Replace("DBTYPE_WVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_WLONGVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_NUMERIC", "double");

                    yaz.WriteLine("\t\t" + tip + " " + kolon + " { get; set; }");
                }
                yaz.WriteLine("");
                yaz.WriteLine("\t\tint Count { get; set; }");
                yaz.WriteLine("\t}");
                yaz.WriteLine("}");
                yaz.Close();

                // Enum GP
                yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Entity\\" + dt.Rows[i].ItemArray[2].ToString() + "\\I" + dt.Rows[i].ItemArray[2].ToString() + "GP.cs");

                yaz.WriteLine("using System;");
                yaz.WriteLine("");
                yaz.WriteLine("namespace Entity." + tablo + "Ent");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic enum " + tablo + "GP");
                yaz.WriteLine("\t{");

                for (j = 0; j < veri.FieldCount; j++)
                {
                    string kolon = veri.GetName(j).ToString();

                    if (j < veri.FieldCount - 1)
                        yaz.WriteLine("\t\t" + kolon + ",");
                    else
                        yaz.WriteLine("\t\t" + kolon);
                }
                yaz.WriteLine("\t}");
                yaz.WriteLine("}");
                yaz.Close();

                // Interface CP
                yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Entity\\" + dt.Rows[i].ItemArray[2].ToString() + "\\I" + dt.Rows[i].ItemArray[2].ToString() + "CP.cs");

                yaz.WriteLine("using System;");
                yaz.WriteLine("");
                yaz.WriteLine("namespace Entity." + tablo + "Ent");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic interface I" + tablo + "CP");
                yaz.WriteLine("\t{");

                for (j = 0; j < veri.FieldCount; j++)
                {
                    string tip = veri.GetDataTypeName(j).ToString();
                    string kolon = veri.GetName(j).ToString();

                    tip = tip.Replace("DBTYPE_I4", "int?");
                    tip = tip.Replace("DBTYPE_BOOL", "bool?");
                    tip = tip.Replace("DBTYPE_I2", "bool?");
                    tip = tip.Replace("DBTYPE_DATE", "DateTime?");
                    tip = tip.Replace("DBTYPE_WVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_WLONGVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_NUMERIC", "double?");

                    yaz.WriteLine("\t\t" + tip + " " + kolon + " { get; set; }");
                }

                yaz.WriteLine("");
                yaz.WriteLine("\t\t" + tablo + "OrderBy? OrderBy { get; set; }");
                yaz.WriteLine("\t\t" + tablo + "OrderDirection? OrderDirection { get; set; }");
                yaz.WriteLine("");
                yaz.WriteLine("\t\tint? Top { get; set; }");
                yaz.WriteLine("\t\tbool? SearchTextLike { get; set; }");

                yaz.WriteLine("\t}");
                yaz.WriteLine("}");
                yaz.Close();

                // Interface VP
                yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Entity\\" + dt.Rows[i].ItemArray[2].ToString() + "\\I" + dt.Rows[i].ItemArray[2].ToString() + "VP.cs");

                yaz.WriteLine("using System;");
                yaz.WriteLine("");
                yaz.WriteLine("namespace Entity." + tablo + "Ent");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic interface I" + tablo + "VP");
                yaz.WriteLine("\t{");

                for (j = 0; j < veri.FieldCount; j++)
                {
                    if (j > 0)
                    {
                        string tip = veri.GetDataTypeName(j).ToString();
                        string kolon = veri.GetName(j).ToString();

                        tip = tip.Replace("DBTYPE_I4", "int?");
                        tip = tip.Replace("DBTYPE_BOOL", "bool?");
                        tip = tip.Replace("DBTYPE_I2", "bool?");
                        tip = tip.Replace("DBTYPE_DATE", "DateTime?");
                        tip = tip.Replace("DBTYPE_WVARCHAR", "string");
                        tip = tip.Replace("DBTYPE_WLONGVARCHAR", "string");
                        tip = tip.Replace("DBTYPE_NUMERIC", "double?");

                        yaz.WriteLine("\t\t" + tip + " " + kolon + " { get; set; }");
                    }
                }
                yaz.WriteLine("\t}");
                yaz.WriteLine("}");
                yaz.Close();


                // enum Order
                yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Entity\\" + dt.Rows[i].ItemArray[2].ToString() + "\\I" + dt.Rows[i].ItemArray[2].ToString() + "Order.cs");

                yaz.WriteLine("using System;");
                yaz.WriteLine("");
                yaz.WriteLine("namespace Entity." + tablo + "Ent");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic enum " + tablo + "OrderBy");
                yaz.WriteLine("\t{");

                for (j = 0; j < veri.FieldCount; j++)
                {
                    string kolon = veri.GetName(j).ToString();

                    if (j < veri.FieldCount - 1)
                        yaz.WriteLine("\t\t" + kolon + ",");
                    else
                        yaz.WriteLine("\t\t" + kolon);
                }
                yaz.WriteLine("\t}");

                yaz.WriteLine("");

                yaz.WriteLine("\tpublic enum " + tablo + "OrderDirection");
                yaz.WriteLine("\t{");

                yaz.WriteLine("\t\tAsc,");
                yaz.WriteLine("\t\tDesc");
                yaz.WriteLine("\t}");

                yaz.WriteLine("}");
                yaz.Close();

                veri.Close();
            }

            baglanti.Close();

            //webSPolustur(dosya);
        }
        private void CreateBusiness(DatabaseType _dbType)
        {
            OleDbConnection baglanti = new OleDbConnection(baglantitext);
            baglanti.Open();
            bag.Connection = baglanti;

            for (i = 0; i < tablolar.Length; i++)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Business\\" + tablolar[i]);

                StreamWriter yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Business\\" + tablolar[i] + "\\" + tablolar[i] + ".cs");

                yaz.WriteLine("using System;");
                yaz.WriteLine("using System.Data;");
                yaz.WriteLine("using System.Collections.Generic;");
                yaz.WriteLine("using Common;");
                yaz.WriteLine("using Entity." + tablolar[i] + "Ent;");
                yaz.WriteLine("using DataAccess." + tablolar[i] + "Acc;");
                yaz.WriteLine("");
                yaz.WriteLine("namespace Business." + tablolar[i] + "Bus");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic class " + tablolar[i] + " : I" + tablolar[i]);
                yaz.WriteLine("\t{");
                yaz.WriteLine("\t\t#region Properties");
                yaz.WriteLine("");

                bag.CommandText = "select * from " + tablolar[i];
                veri = bag.ExecuteReader();

                for (j = 0; j < veri.FieldCount; j++)
                {
                    string tip = veri.GetDataTypeName(j).ToString();
                    string kolon = veri.GetName(j).ToString();

                    tip = tip.Replace("DBTYPE_I4", "int");
                    tip = tip.Replace("DBTYPE_BOOL", "bool");
                    tip = tip.Replace("DBTYPE_I2", "bool");
                    tip = tip.Replace("DBTYPE_DATE", "DateTime");
                    tip = tip.Replace("DBTYPE_WVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_WLONGVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_NUMERIC", "double");

                    yaz.WriteLine("\t\tpublic " + tip + " " + kolon + " { get; set; }");
                }

                yaz.WriteLine("");
                yaz.WriteLine("\t\tpublic int Count { get; set; }");
                yaz.WriteLine("");
                yaz.WriteLine("\t\t#endregion");
                yaz.WriteLine("");
                yaz.WriteLine("\t\t#region Methods");
                yaz.WriteLine("");
                yaz.WriteLine("\t\tpublic static List<" + tablolar[i] + "> Get(List<" + tablolar[i] + "GP> _" + tablolar[i].ToLower() + "GP = null, " + tablolar[i] + "CP _" + tablolar[i].ToLower() + "CP = null)");
                yaz.WriteLine("\t\t{");
                yaz.WriteLine("\t\t\tList<" + tablolar[i] + "> _" + tablolar[i].ToLower() + "List = new List<" + tablolar[i] + ">();");
                yaz.WriteLine("");
                yaz.WriteLine("\t\t\tDataTable dataTable = new DataTable();");
                yaz.WriteLine("\t\t\t" + tablolar[i] + "Access.GetItem(dataTable, _" + tablolar[i].ToLower() + "GP, _" + tablolar[i].ToLower() + "CP);");
                yaz.WriteLine("\t\t\tforeach (DataRow _dataItem in dataTable.Rows)");
                yaz.WriteLine("\t\t\t{");
                yaz.WriteLine("\t\t\t\t" + tablolar[i] + " _" + tablolar[i].ToLower() + ";");
                yaz.WriteLine("");
                yaz.WriteLine("\t\t\t\tif (_" + tablolar[i].ToLower() + "GP == null)");
                yaz.WriteLine("\t\t\t\t{");
                yaz.WriteLine("\t\t\t\t\t_" + tablolar[i].ToLower() + " = new " + tablolar[i] + "()");
                yaz.WriteLine("\t\t\t\t\t{");

                for (j = 0; j < veri.FieldCount; j++)
                {
                    string tip = veri.GetDataTypeName(j).ToString();
                    string kolon = veri.GetName(j).ToString();

                    tip = tip.Replace("DBTYPE_I4", "int");
                    tip = tip.Replace("DBTYPE_BOOL", "bool");
                    tip = tip.Replace("DBTYPE_I2", "bool");
                    tip = tip.Replace("DBTYPE_DATE", "DateTime");
                    tip = tip.Replace("DBTYPE_WVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_WLONGVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_NUMERIC", "double");

                    if (tip == "int")
                        yaz.WriteLine("\t\t\t\t\t\t" + kolon + " = _dataItem[\"" + kolon + "\"] != DBNull.Value ? Convert.ToInt32(_dataItem[\"" + kolon + "\"].ToString()) : -1,");
                    else if (tip == "bool")
                        yaz.WriteLine("\t\t\t\t\t\t" + kolon + " = _dataItem[\"" + kolon + "\"] != DBNull.Value ? Convert.ToBoolean(_dataItem[\"" + kolon + "\"]) : false,");
                    else if (tip == "DateTime")
                        yaz.WriteLine("\t\t\t\t\t\t" + kolon + " = _dataItem[\"" + kolon + "\"] != DBNull.Value ? Convert.ToDateTime(_dataItem[\"" + kolon + "\"].ToString()) : DateTime.MinValue,");
                    else if (tip == "string")
                        yaz.WriteLine("\t\t\t\t\t\t" + kolon + " = _dataItem[\"" + kolon + "\"] != DBNull.Value ? _dataItem[\"" + kolon + "\"].ToString() : string.Empty,");
                    else if (tip == "double")
                        yaz.WriteLine("\t\t\t\t\t\t" + kolon + " = _dataItem[\"" + kolon + "\"] != DBNull.Value ? Convert.ToDouble(_dataItem[\"" + kolon + "\"].ToString()) : -1,");
                }

                yaz.WriteLine("\t\t\t\t\t\tCount = dataTable.Rows.Count");
                yaz.WriteLine("\t\t\t\t\t};");
                yaz.WriteLine("\t\t\t\t}");
                yaz.WriteLine("\t\t\t\telse");
                yaz.WriteLine("\t\t\t\t{");
                yaz.WriteLine("\t\t\t\t\t_" + tablolar[i].ToLower() + " = new " + tablolar[i] + "()");
                yaz.WriteLine("\t\t\t\t\t{");

                for (j = 0; j < veri.FieldCount; j++)
                {
                    string tip = veri.GetDataTypeName(j).ToString();
                    string kolon = veri.GetName(j).ToString();

                    tip = tip.Replace("DBTYPE_I4", "int");
                    tip = tip.Replace("DBTYPE_BOOL", "bool");
                    tip = tip.Replace("DBTYPE_I2", "bool");
                    tip = tip.Replace("DBTYPE_DATE", "DateTime");
                    tip = tip.Replace("DBTYPE_WVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_WLONGVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_NUMERIC", "double");

                    if (tip == "int")
                        yaz.WriteLine("\t\t\t\t\t\t" + kolon + " = _" + tablolar[i].ToLower() + "GP.Contains(" + tablolar[i] + "GP." + kolon + ") ? " + "_dataItem[\"" + kolon + "\"] != DBNull.Value ? Convert.ToInt32(_dataItem[\"" + kolon + "\"].ToString()) : -1 : -1,");
                    else if (tip == "bool")
                        yaz.WriteLine("\t\t\t\t\t\t" + kolon + " = _" + tablolar[i].ToLower() + "GP.Contains(" + tablolar[i] + "GP." + kolon + ") ? " + "_dataItem[\"" + kolon + "\"] != DBNull.Value ? Convert.ToBoolean(_dataItem[\"" + kolon + "\"]) : false : false,");
                    else if (tip == "DateTime")
                        yaz.WriteLine("\t\t\t\t\t\t" + kolon + " = _" + tablolar[i].ToLower() + "GP.Contains(" + tablolar[i] + "GP." + kolon + ") ? " + "_dataItem[\"" + kolon + "\"] != DBNull.Value ? Convert.ToDateTime(_dataItem[\"" + kolon + "\"].ToString()) : DateTime.MinValue : DateTime.MinValue,");
                    else if (tip == "string")
                        yaz.WriteLine("\t\t\t\t\t\t" + kolon + " = _" + tablolar[i].ToLower() + "GP.Contains(" + tablolar[i] + "GP." + kolon + ") ? " + "_dataItem[\"" + kolon + "\"] != DBNull.Value ? _dataItem[\"" + kolon + "\"].ToString() : string.Empty : string.Empty,");
                    else if (tip == "double")
                        yaz.WriteLine("\t\t\t\t\t\t" + kolon + " = _" + tablolar[i].ToLower() + "GP.Contains(" + tablolar[i] + "GP." + kolon + ") ? " + "_dataItem[\"" + kolon + "\"] != DBNull.Value ? Convert.ToDouble(_dataItem[\"" + kolon + "\"].ToString()) : -1 : -1,");
                }

                yaz.WriteLine("\t\t\t\t\t\tCount = dataTable.Rows.Count");
                yaz.WriteLine("\t\t\t\t\t};");
                yaz.WriteLine("\t\t\t\t}");

                yaz.WriteLine("\t\t\t\t_" + tablolar[i].ToLower() + "List.Add(_" + tablolar[i].ToLower() + ");");
                yaz.WriteLine("\t\t\t}");
                yaz.WriteLine("\t\t\treturn _" + tablolar[i].ToLower() + "List;");
                yaz.WriteLine("\t\t}");
                yaz.WriteLine("");

                yaz.WriteLine("\t\tpublic static string Insert(" + tablolar[i] + "VP _" + tablolar[i].ToLower() + "VP)");
                yaz.WriteLine("\t\t{");
                yaz.WriteLine("\t\t\treturn " + tablolar[i] + "Access.InsertItem(_" + tablolar[i].ToLower() + "VP);");
                yaz.WriteLine("\t\t}");
                yaz.WriteLine("");

                yaz.WriteLine("\t\tpublic static string Update(" + tablolar[i] + "VP _" + tablolar[i].ToLower() + "VP, " + tablolar[i] + "CP _" + tablolar[i].ToLower() + "CP = null)");
                yaz.WriteLine("\t\t{");
                yaz.WriteLine("\t\t\treturn " + tablolar[i] + "Access.UpdateItem(_" + tablolar[i].ToLower() + "VP, _" + tablolar[i].ToLower() + "CP);");
                yaz.WriteLine("\t\t}");
                yaz.WriteLine("");

                yaz.WriteLine("\t\tpublic static string Delete(" + tablolar[i] + "CP _" + tablolar[i].ToLower() + "CP)");
                yaz.WriteLine("\t\t{");
                yaz.WriteLine("\t\t\treturn " + tablolar[i] + "Access.DeleteItem(_" + tablolar[i].ToLower() + "CP);");
                yaz.WriteLine("\t\t}");
                yaz.WriteLine("");
                yaz.WriteLine("\t\t#endregion");
                yaz.WriteLine("");

                yaz.WriteLine("\t}");
                yaz.WriteLine("}");

                yaz.Close();

                // Bussiness CP
                yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Business\\" + tablolar[i] + "\\" + tablolar[i] + "CP.cs");

                yaz.WriteLine("using System;");
                yaz.WriteLine("using Entity." + tablolar[i] + "Ent;");
                yaz.WriteLine("");
                yaz.WriteLine("namespace Business." + tablolar[i] + "Bus");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic class " + tablolar[i] + "CP : I" + tablolar[i] + "CP");
                yaz.WriteLine("\t{");

                for (j = 0; j < veri.FieldCount; j++)
                {
                    string tip = veri.GetDataTypeName(j).ToString();
                    string kolon = veri.GetName(j).ToString();

                    tip = tip.Replace("DBTYPE_I4", "int?");
                    tip = tip.Replace("DBTYPE_BOOL", "bool?");
                    tip = tip.Replace("DBTYPE_I2", "bool?");
                    tip = tip.Replace("DBTYPE_DATE", "DateTime?");
                    tip = tip.Replace("DBTYPE_WVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_WLONGVARCHAR", "string");
                    tip = tip.Replace("DBTYPE_NUMERIC", "double?");

                    yaz.WriteLine("\t\tpublic " + tip + " " + kolon + " { get; set; }");
                }

                yaz.WriteLine("");
                yaz.WriteLine("\t\tpublic " + tablolar[i] + "OrderBy? OrderBy { get; set; }");
                yaz.WriteLine("\t\tpublic " + tablolar[i] + "OrderDirection? OrderDirection { get; set; }");
                yaz.WriteLine("");
                yaz.WriteLine("\t\tpublic int? Top { get; set; }");
                yaz.WriteLine("\t\tpublic bool? SearchTextLike { get; set; }");

                yaz.WriteLine("\t}");
                yaz.WriteLine("}");
                yaz.Close();


                // Bussiness VP
                yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\Business\\" + tablolar[i] + "\\" + tablolar[i] + "VP.cs");

                yaz.WriteLine("using System;");
                yaz.WriteLine("using Entity." + tablolar[i] + "Ent;");
                yaz.WriteLine("");
                yaz.WriteLine("namespace Business." + tablolar[i] + "Bus");
                yaz.WriteLine("{");
                yaz.WriteLine("\tpublic class " + tablolar[i] + "VP : I" + tablolar[i] + "VP");
                yaz.WriteLine("\t{");

                for (j = 0; j < veri.FieldCount; j++)
                {
                    if (j > 0)
                    {
                        string tip = veri.GetDataTypeName(j).ToString();
                        string kolon = veri.GetName(j).ToString();

                        tip = tip.Replace("DBTYPE_I4", "int?");
                        tip = tip.Replace("DBTYPE_BOOL", "bool?");
                        tip = tip.Replace("DBTYPE_I2", "bool?");
                        tip = tip.Replace("DBTYPE_DATE", "DateTime?");
                        tip = tip.Replace("DBTYPE_WVARCHAR", "string");
                        tip = tip.Replace("DBTYPE_WLONGVARCHAR", "string");
                        tip = tip.Replace("DBTYPE_NUMERIC", "double?");

                        yaz.WriteLine("\t\tpublic " + tip + " " + kolon + " { get; set; }");
                    }
                }

                yaz.WriteLine("\t}");
                yaz.WriteLine("}");
                yaz.Close();

                veri.Close();
            }
            baglanti.Close();
        }
        private void CreateDataAccess(DatabaseType _dbType)
        {
            for (i = 0; i < tablolar.Length; i++)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\DataAccess\\" + tablolar[i]);
                StreamWriter yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + _dbType.ToString() + "\\Layers\\DataAccess\\" + tablolar[i] + "\\" + tablolar[i] + "Access.cs");

                switch (_dbType)
                {
                    case DatabaseType.MsSql:
                        MsSqlDataAccess(yaz, tablolar[i]);
                        break;
                    case DatabaseType.MySql:
                        MySqlDataAccess(yaz, tablolar[i]);
                        break;
                    case DatabaseType.Access:
                        AccessDataAccess(yaz, tablolar[i]);
                        break;
                    default:
                        MsSqlDataAccess(yaz, tablolar[i]);
                        break;
                }

            }
        }

        #region SQL (MS SQL Server)

        private void MsSqlConnection(StreamWriter yaz)
        {
            yaz.WriteLine("using System.Data.SqlClient;");
            yaz.WriteLine("using System.Web.Configuration;");
            yaz.WriteLine("using System.Web;");
            yaz.WriteLine("");
            yaz.WriteLine("namespace Common");
            yaz.WriteLine("{");
            yaz.WriteLine("\tpublic class Connection");
            yaz.WriteLine("\t{");
            yaz.WriteLine("\t\tstatic SqlConnection _SQLConnection = null;");
            yaz.WriteLine("\t\tpublic static SqlConnection SQLConnection");
            yaz.WriteLine("\t\t{");
            yaz.WriteLine("\t\t\tget");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tif(_SQLConnection == null)");
            yaz.WriteLine("\t\t\t\t\t_SQLConnection = new SqlConnection(WebConfigurationManager.AppSettings[\"SQLConnection\"].ToString());");
            yaz.WriteLine("\t\t\t\treturn _SQLConnection;");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t}");
            yaz.WriteLine("\t}");
            yaz.WriteLine("}");
            yaz.Close();
        }
        private void MsSqlDataAccess(StreamWriter yaz, string tabloadi)
        {
            OleDbConnection baglanti = new OleDbConnection(baglantitext);
            baglanti.Open();
            bag.Connection = baglanti;

            yaz.WriteLine("using System;");
            yaz.WriteLine("using System.Data;");
            yaz.WriteLine("using System.Data.SqlClient;");
            yaz.WriteLine("using System.Collections.Generic;");
            yaz.WriteLine("using Common;");
            yaz.WriteLine("using Entity." + tabloadi + "Ent;");
            yaz.WriteLine("");
            yaz.WriteLine("namespace DataAccess." + tabloadi + "Acc");
            yaz.WriteLine("{");
            yaz.WriteLine("\tpublic class " + tabloadi + "Access : Connection");
            yaz.WriteLine("\t{");

            bag.CommandText = "select * from " + tabloadi;
            veri = bag.ExecuteReader();

            string ilkharf = tabloadi[0].ToString();

            yaz.WriteLine("\t\tpublic static string GetItem(DataTable _dataTable, List<" + tabloadi + "GP> _" + tabloadi.ToLower() + "GP = null, I" + tabloadi + "CP _" + tabloadi.ToLower() + "CP = null)");
            yaz.WriteLine("\t\t{");

            yaz.WriteLine("\t\t\tstring returnMessage = null;");
            yaz.WriteLine("\t\t\tstring query = \"Select \";");
            yaz.WriteLine("\t\t\tSqlDataAdapter _dataAdap = new SqlDataAdapter();");
            yaz.WriteLine("\t\t\tSqlCommand _getCmd = new SqlCommand();");
            yaz.WriteLine("\t\t\t_dataAdap.SelectCommand = _getCmd;");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\tif (_" + tabloadi.ToLower() + "CP != null)");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tif (_" + tabloadi.ToLower() + "CP.Top != null)");
            yaz.WriteLine("\t\t\t\t{");
            yaz.WriteLine("\t\t\t\t\tquery += \"Top \" + _" + tabloadi.ToLower() + "CP.Top.ToString() + \" \";");
            yaz.WriteLine("\t\t\t\t}");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\tif (_" + tabloadi.ToLower() + "GP == null)");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tquery += \"" + ilkharf + ".* from " + tabloadi + " " + ilkharf + " Where 1=1 \";");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\telse");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tforeach (" + tabloadi + "GP item in _" + tabloadi.ToLower() + "GP)");
            yaz.WriteLine("\t\t\t\t{");
            yaz.WriteLine("\t\t\t\t\tquery += \"" + ilkharf + ".\" + item.ToString() + \", \";");
            yaz.WriteLine("\t\t\t\t}");
            yaz.WriteLine("");
            yaz.WriteLine("\t\t\t\tquery = query.TrimEnd(\' \').TrimEnd(\',\');");
            yaz.WriteLine("\t\t\t\tquery += \" from " + tabloadi + " " + ilkharf + " Where 1=1 \";");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\tif (_" + tabloadi.ToLower() + "CP != null)");
            yaz.WriteLine("\t\t\t{");

            for (j = 0; j < veri.FieldCount; j++)
            {
                string tip = veri.GetDataTypeName(j).ToString();
                string kolon = veri.GetName(j).ToString();
                tip = tip.Replace("DBTYPE_I4", "int");
                tip = tip.Replace("DBTYPE_BOOL", "bool");
                tip = tip.Replace("DBTYPE_I2", "bool");
                tip = tip.Replace("DBTYPE_DATE", "DateTime");
                tip = tip.Replace("DBTYPE_WVARCHAR", "string");
                tip = tip.Replace("DBTYPE_WLONGVARCHAR", "string");
                tip = tip.Replace("DBTYPE_NUMERIC", "double");

                if (tip == "string" && j == 1)
                {
                    yaz.WriteLine("\t\t\t\tif (_" + tabloadi.ToLower() + "CP." + kolon + " != null)");
                    yaz.WriteLine("\t\t\t\t{");

                    yaz.WriteLine("\t\t\t\t\tif (_" + tabloadi.ToLower() + "CP.SearchTextLike != null)");
                    yaz.WriteLine("\t\t\t\t\t{");
                    yaz.WriteLine("\t\t\t\t\t\tif (_" + tabloadi.ToLower() + "CP.SearchTextLike != true)");
                    yaz.WriteLine("\t\t\t\t\t\t{");
                    yaz.WriteLine("\t\t\t\t\t\t\tquery += \"And " + ilkharf + "." + kolon + "=@" + kolon + " \";");
                    yaz.WriteLine("\t\t\t\t\t\t\t_dataAdap.SelectCommand.Parameters.AddWithValue(\"@" + kolon + "\", _" + tabloadi.ToLower() + "CP." + kolon + ");");
                    yaz.WriteLine("\t\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t\t\telse");
                    yaz.WriteLine("\t\t\t\t\t\t{");
                    yaz.WriteLine("\t\t\t\t\t\t\tquery += \"And " + ilkharf + "." + kolon + " Like @" + kolon + " \";");
                    yaz.WriteLine("\t\t\t\t\t\t\t_dataAdap.SelectCommand.Parameters.AddWithValue(\"@" + kolon + "\", \"%\" + _" + tabloadi.ToLower() + "CP." + kolon + " + \"%\");");
                    yaz.WriteLine("\t\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t\telse");
                    yaz.WriteLine("\t\t\t\t\t{");
                    yaz.WriteLine("\t\t\t\t\t\tquery += \"And " + ilkharf + "." + kolon + "=@" + kolon + " \";");
                    yaz.WriteLine("\t\t\t\t\t\t_dataAdap.SelectCommand.Parameters.AddWithValue(\"@" + kolon + "\", _" + tabloadi.ToLower() + "CP." + kolon + ");");
                    yaz.WriteLine("\t\t\t\t\t}");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("");
                }
                else
                {
                    yaz.WriteLine("\t\t\t\tif (_" + tabloadi.ToLower() + "CP." + kolon + " != null)");
                    yaz.WriteLine("\t\t\t\t{");
                    yaz.WriteLine("\t\t\t\t\tquery += \"And " + ilkharf + "." + kolon + "=@" + kolon + " \";");
                    yaz.WriteLine("\t\t\t\t\t_dataAdap.SelectCommand.Parameters.AddWithValue(\"@" + kolon + "\", _" + tabloadi.ToLower() + "CP." + kolon + ");");
                    yaz.WriteLine("\t\t\t\t}");
                    yaz.WriteLine("");
                }
            }

            yaz.WriteLine("\t\t\t\tif (_" + tabloadi.ToLower() + "CP.OrderBy != null)");
            yaz.WriteLine("\t\t\t\t{");
            yaz.WriteLine("\t\t\t\t\tif (_" + tabloadi.ToLower() + "CP.OrderDirection != null)");
            yaz.WriteLine("\t\t\t\t\t{");
            yaz.WriteLine("\t\t\t\t\t\tquery += \"Order By " + ilkharf + ".\" + _" + tabloadi.ToLower() + "CP.OrderBy.ToString() + \" \" + _" + tabloadi.ToLower() + "CP.OrderDirection.ToString();");
            yaz.WriteLine("\t\t\t\t\t}");
            yaz.WriteLine("\t\t\t\t\telse");
            yaz.WriteLine("\t\t\t\t\t{");
            yaz.WriteLine("\t\t\t\t\t\tquery += \"Order By " + ilkharf + ".\" + _" + tabloadi.ToLower() + "CP.OrderBy.ToString() + \" Asc\";");
            yaz.WriteLine("\t\t\t\t\t}");
            yaz.WriteLine("\t\t\t\t}");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\t_dataAdap.SelectCommand.CommandText = query;");
            yaz.WriteLine("\t\t\t_dataAdap.SelectCommand.Connection = SQLConnection;");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\ttry");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tSQLConnection.Open();");
            yaz.WriteLine("\t\t\t\t_dataAdap.Fill(_dataTable);");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\tcatch (Exception e)");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\treturnMessage = e.Message;");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\tfinally");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tSQLConnection.Close();");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\treturn returnMessage;");
            yaz.WriteLine("\t\t}");

            yaz.WriteLine("");

            yaz.WriteLine("\t\tpublic static string InsertItem(I" + tabloadi + "VP _" + tabloadi.ToLower() + "VP)");
            yaz.WriteLine("\t\t{");

            yaz.WriteLine("\t\t\tstring returnMessage = null;");
            yaz.WriteLine("\t\t\tstring query = \"Insert Into " + tabloadi + "(\";");
            yaz.WriteLine("\t\t\tstring queryValues = \"Values(\";");
            yaz.WriteLine("\t\t\tSqlCommand _addCmd = new SqlCommand();");
            yaz.WriteLine("");

            for (j = 0; j < veri.FieldCount; j++)
            {
                if (j > 0)
                {
                    string kolon = veri.GetName(j).ToString();

                    yaz.WriteLine("\t\t\tif (_" + tabloadi.ToLower() + "VP." + kolon + " != null)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tquery += \"" + kolon + ",\";");
                    yaz.WriteLine("\t\t\t\tqueryValues += \"@" + kolon + ",\";");
                    yaz.WriteLine("\t\t\t\t_addCmd.Parameters.AddWithValue(\"@" + kolon + "\", _" + tabloadi.ToLower() + "VP." + kolon + ");");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("");
                }
            }

            yaz.WriteLine("\t\t\tquery = query.TrimEnd(\',\');");
            yaz.WriteLine("\t\t\tquery += \") \";");
            yaz.WriteLine("\t\t\tqueryValues = queryValues.TrimEnd(\',\');");
            yaz.WriteLine("\t\t\tqueryValues += \")\";");
            yaz.WriteLine("");
            yaz.WriteLine("\t\t\tquery = query + queryValues;");
            yaz.WriteLine("");
            yaz.WriteLine("\t\t\t_addCmd.CommandText = query;");
            yaz.WriteLine("\t\t\t_addCmd.Connection = SQLConnection;");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\ttry");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tSQLConnection.Open();");
            yaz.WriteLine("\t\t\t\t_addCmd.ExecuteNonQuery();");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\tcatch (Exception e)");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\treturnMessage = e.Message;");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\tfinally");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tSQLConnection.Close();");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\treturn returnMessage;");
            yaz.WriteLine("\t\t}");

            yaz.WriteLine("");

            yaz.WriteLine("\t\tpublic static string UpdateItem(I" + tabloadi + "VP _" + tabloadi.ToLower() + "VP, I" + tabloadi + "CP _" + tabloadi.ToLower() + "CP)");
            yaz.WriteLine("\t\t{");

            yaz.WriteLine("\t\t\tstring returnMessage = null;");
            yaz.WriteLine("\t\t\tstring query = \"Update " + tabloadi + " set \";");
            yaz.WriteLine("\t\t\tstring queryConditions = \" Where 1=1 \";");
            yaz.WriteLine("\t\t\tSqlCommand _updateCmd = new SqlCommand();");
            yaz.WriteLine("");

            for (j = 0; j < veri.FieldCount; j++)
            {
                if (j > 0)
                {
                    string kolon = veri.GetName(j).ToString();

                    yaz.WriteLine("\t\t\tif (_" + tabloadi.ToLower() + "VP." + kolon + " != null)");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\tquery += \"" + kolon + "=@V" + kolon + ",\";");
                    yaz.WriteLine("\t\t\t\t_updateCmd.Parameters.AddWithValue(\"@V" + kolon + "\", _" + tabloadi.ToLower() + "VP." + kolon + ");");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("");
                }
            }

            yaz.WriteLine("\t\t\tquery = query.TrimEnd(\',\');");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\tif (_" + tabloadi.ToLower() + "CP != null)");
            yaz.WriteLine("\t\t\t{");
            for (j = 0; j < veri.FieldCount; j++)
            {
                string kolon = veri.GetName(j).ToString();

                yaz.WriteLine("\t\t\t\tif (_" + tabloadi.ToLower() + "CP." + kolon + " != null)");
                yaz.WriteLine("\t\t\t\t{");
                yaz.WriteLine("\t\t\t\t\tqueryConditions += \"And " + kolon + "=@" + kolon + " \";");
                yaz.WriteLine("\t\t\t\t\t_updateCmd.Parameters.AddWithValue(\"@" + kolon + "\", _" + tabloadi.ToLower() + "CP." + kolon + ");");
                yaz.WriteLine("\t\t\t\t}");
                yaz.WriteLine("");
            }
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\tquery = query + queryConditions;");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\t_updateCmd.CommandText = query;");
            yaz.WriteLine("\t\t\t_updateCmd.Connection = SQLConnection;");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\ttry");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tSQLConnection.Open();");
            yaz.WriteLine("\t\t\t\t_updateCmd.ExecuteNonQuery();");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\tcatch (Exception e)");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\treturnMessage = e.Message;");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\tfinally");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tSQLConnection.Close();");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\treturn returnMessage;");
            yaz.WriteLine("\t\t}");

            yaz.WriteLine("");

            yaz.WriteLine("\t\tpublic static string DeleteItem(I" + tabloadi + "CP _" + tabloadi.ToLower() + "CP)");
            yaz.WriteLine("\t\t{");

            yaz.WriteLine("\t\t\tstring returnMessage = null;");
            yaz.WriteLine("\t\t\tstring query = \"Delete From " + tabloadi + " Where 1=1 \";");
            yaz.WriteLine("\t\t\tSqlCommand _deleteCmd = new SqlCommand();");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\tif (_" + tabloadi.ToLower() + "CP != null)");
            yaz.WriteLine("\t\t\t{");
            for (j = 0; j < veri.FieldCount; j++)
            {
                string kolon = veri.GetName(j).ToString();

                yaz.WriteLine("\t\t\t\tif (_" + tabloadi.ToLower() + "CP." + kolon + " != null)");
                yaz.WriteLine("\t\t\t\t{");
                yaz.WriteLine("\t\t\t\t\tquery += \"And " + kolon + "=@" + kolon + " \";");
                yaz.WriteLine("\t\t\t\t\t_deleteCmd.Parameters.AddWithValue(\"@" + kolon + "\", _" + tabloadi.ToLower() + "CP." + kolon + ");");
                yaz.WriteLine("\t\t\t\t}");
                yaz.WriteLine("");
            }
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\t_deleteCmd.CommandText = query;");
            yaz.WriteLine("\t\t\t_deleteCmd.Connection = SQLConnection;");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t\ttry");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tSQLConnection.Open();");
            yaz.WriteLine("\t\t\t\t_deleteCmd.ExecuteNonQuery();");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\tcatch (Exception e)");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\treturnMessage = e.Message;");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\tfinally");
            yaz.WriteLine("\t\t\t{");
            yaz.WriteLine("\t\t\t\tSQLConnection.Close();");
            yaz.WriteLine("\t\t\t}");
            yaz.WriteLine("\t\t\treturn returnMessage;");
            yaz.WriteLine("\t\t}");

            yaz.WriteLine("");

            yaz.WriteLine("\t}");
            yaz.WriteLine("}");

            yaz.Close();

            veri.Close();
            baglanti.Close();
        }
        private void webSPolustur(string dosya)
        {
            OleDbConnection baglanti = new OleDbConnection(baglantitext);
            baglanti.Open();
            bag.Connection = baglanti;

            StreamWriter yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\MsSql\\StoredProcedures\\" + dosya + ".sql");
            yaz.WriteLine("use [" + dosya + "]");
            yaz.WriteLine("go");
            yaz.WriteLine("");

            for (i = 0; i < tablolar.Length; i++)
            {
                bag.CommandText = "select * from " + tablolar[i];
                veri = bag.ExecuteReader();
                //Ekle//
                yaz.WriteLine("Create Procedure sp" + tablolar[i] + "InsertItem");
                yaz.WriteLine("(");
                for (j = 1; j < veri.FieldCount; j++)
                {
                    string tip = veri.GetDataTypeName(j).ToString();
                    tip = tip.Replace("DBTYPE_I4", "int");
                    tip = tip.Replace("DBTYPE_BOOL", "bit");
                    tip = tip.Replace("DBTYPE_I2", "bit");
                    tip = tip.Replace("DBTYPE_DATE", "DateTime");
                    tip = tip.Replace("DBTYPE_WVARCHAR", "nvarchar(max)");
                    tip = tip.Replace("DBTYPE_WLONGVARCHAR", "nvarchar(max)");
                    tip = tip.Replace("DBTYPE_NUMERIC", "decimal(18,2)");

                    if (j != veri.FieldCount - 1)
                        yaz.WriteLine("\t@" + veri.GetName(j) + " " + tip + ",");
                    else
                        yaz.WriteLine("\t@" + veri.GetName(j) + " " + tip);
                }
                yaz.WriteLine(")");
                yaz.WriteLine("as");
                yaz.WriteLine("begin");


                string oledbstr = "insert into " + tablolar[i] + "(";

                for (j = 1; j < veri.FieldCount; j++)
                {
                    oledbstr = oledbstr + veri.GetName(j) + ",";
                }

                oledbstr = oledbstr.Remove(oledbstr.Length - 1);
                oledbstr = oledbstr + ") Values(";

                for (j = 1; j < veri.FieldCount; j++)
                {
                    oledbstr = oledbstr + "@" + veri.GetName(j) + ",";
                }

                oledbstr = oledbstr.Remove(oledbstr.Length - 1);

                oledbstr = oledbstr + ")";
                yaz.WriteLine(oledbstr);
                yaz.WriteLine("end");
                yaz.WriteLine("go");
                yaz.WriteLine("");
                //Ekle//

                //Güncelle//
                yaz.WriteLine("Create Procedure sp" + tablolar[i] + "UpdateItem");
                yaz.WriteLine("(");
                for (j = 0; j < veri.FieldCount; j++)
                {
                    string tip = veri.GetDataTypeName(j).ToString();
                    tip = tip.Replace("DBTYPE_I4", "int");
                    tip = tip.Replace("DBTYPE_BOOL", "bit");
                    tip = tip.Replace("DBTYPE_I2", "bit");
                    tip = tip.Replace("DBTYPE_DATE", "DateTime");
                    tip = tip.Replace("DBTYPE_WVARCHAR", "nvarchar(max)");
                    tip = tip.Replace("DBTYPE_WLONGVARCHAR", "nvarchar(max)");
                    tip = tip.Replace("DBTYPE_NUMERIC", "decimal(18,2)");

                    if (j != veri.FieldCount - 1)
                        yaz.WriteLine("\t@" + veri.GetName(j) + " " + tip + ",");
                    else
                        yaz.WriteLine("\t@" + veri.GetName(j) + " " + tip);
                }
                yaz.WriteLine(")");
                yaz.WriteLine("as");
                yaz.WriteLine("begin");

                oledbstr = "Update " + tablolar[i] + " set ";

                for (j = 1; j < veri.FieldCount; j++)
                {
                    oledbstr = oledbstr + veri.GetName(j) + "=@" + veri.GetName(j) + ",";
                }

                oledbstr = oledbstr.Remove(oledbstr.Length - 1);
                oledbstr = oledbstr + " where " + veri.GetName(0) + "=@" + veri.GetName(0);

                yaz.WriteLine(oledbstr);
                yaz.WriteLine("end");
                yaz.WriteLine("go");
                yaz.WriteLine("");
                //Güncelle//

                //Getir//
                yaz.WriteLine("Create Procedure sp" + tablolar[i] + "GetItem");
                yaz.WriteLine("(");
                string tip2 = veri.GetDataTypeName(0).ToString();
                tip2 = tip2.Replace("DBTYPE_I4", "int");
                tip2 = tip2.Replace("DBTYPE_BOOL", "bit");
                tip2 = tip2.Replace("DBTYPE_I2", "bit");
                tip2 = tip2.Replace("DBTYPE_DATE", "DateTime");
                tip2 = tip2.Replace("DBTYPE_WVARCHAR", "nvarchar(max)");
                tip2 = tip2.Replace("DBTYPE_WLONGVARCHAR", "nvarchar(max)");
                tip2 = tip2.Replace("DBTYPE_NUMERIC", "decimal(18,2)");
                yaz.WriteLine("\t@" + veri.GetName(0) + " " + tip2);
                yaz.WriteLine(")");
                yaz.WriteLine("as");
                yaz.WriteLine("begin");
                yaz.WriteLine("Select * from " + tablolar[i] + " where " + veri.GetName(0) + "=@" + veri.GetName(0));
                yaz.WriteLine("end");
                yaz.WriteLine("go");
                yaz.WriteLine("");
                //Getir//

                //HepsiniGetir//
                yaz.WriteLine("Create Procedure sp" + tablolar[i] + "GetAllItems");
                yaz.WriteLine("as");
                yaz.WriteLine("begin");
                yaz.WriteLine("Select * from " + tablolar[i]);
                yaz.WriteLine("end");
                yaz.WriteLine("go");
                yaz.WriteLine("");
                //HepsiniGetir//

                //Sil//
                yaz.WriteLine("Create Procedure sp" + tablolar[i] + "DeleteItem");
                yaz.WriteLine("(");
                tip2 = veri.GetDataTypeName(0).ToString();
                tip2 = tip2.Replace("DBTYPE_I4", "int");
                tip2 = tip2.Replace("DBTYPE_BOOL", "bit");
                tip2 = tip2.Replace("DBTYPE_I2", "bit");
                tip2 = tip2.Replace("DBTYPE_DATE", "DateTime");
                tip2 = tip2.Replace("DBTYPE_WVARCHAR", "nvarchar(max)");
                tip2 = tip2.Replace("DBTYPE_WLONGVARCHAR", "nvarchar(max)");
                tip2 = tip2.Replace("DBTYPE_NUMERIC", "decimal(18,2)");
                yaz.WriteLine("\t@" + veri.GetName(0) + " " + tip2);
                yaz.WriteLine(")");
                yaz.WriteLine("as");
                yaz.WriteLine("begin");
                yaz.WriteLine("Delete From " + tablolar[i] + " Where " + veri.GetName(0) + "=@" + veri.GetName(0));
                yaz.WriteLine("end");
                yaz.WriteLine("go");
                yaz.WriteLine("");
                //Sil//
                veri.Close();
            }
            yaz.Close();
            baglanti.Close();
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "MDB Dosyayý Seçiniz";
            openFileDialog1.Filter = "MDB Dosyalarý (*.mdb)|*.mdb";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                dosya = openFileDialog1.FileName;
            else
                return;

            dosya = dosya.Substring(dosya.LastIndexOf('\\') + 1, dosya.Length - dosya.LastIndexOf('\\') - 1);
            baglantitext = "Provider=Microsoft.Jet.OLEDB.4.0; DATA Source=" + dosya;

            if (radioButton1.Checked)
            {
                CreateDirectories(DatabaseType.MsSql);
                CreateEntity(DatabaseType.MsSql);
                CreateBusiness(DatabaseType.MsSql);
                CreateConnection(DatabaseType.MsSql);
                CreateDataAccess(DatabaseType.MsSql);
            }
            else if (radioButton2.Checked)
            {
                CreateDirectories(DatabaseType.MySql);
                CreateEntity(DatabaseType.MySql);
                CreateBusiness(DatabaseType.MySql);
                CreateConnection(DatabaseType.MySql);
                CreateDataAccess(DatabaseType.MySql);
            }
            else if (radioButton3.Checked)
            {
                CreateDirectories(DatabaseType.Access);
                CreateEntity(DatabaseType.Access);
                CreateBusiness(DatabaseType.Access);
                CreateConnection(DatabaseType.Access);
                CreateDataAccess(DatabaseType.Access);
            }

            CreateTableMethods();
            CreateNotes();
            //CreateRoutes();

            MessageBox.Show("Ýþlem Tamamlandý...");
        }

        private void CreateTableMethods()
        {
            OleDbConnection baglanti = new OleDbConnection(baglantitext);
            baglanti.Open();
            bag.Connection = baglanti;

            StreamWriter yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\TableMethods.cs");

            yaz.WriteLine("using System;");
            yaz.WriteLine("using System.Collections.Generic;");
            yaz.WriteLine("using System.Linq;");
            yaz.WriteLine("using System.Web;");
            yaz.WriteLine("using System.Web.Services;");
            yaz.WriteLine("using Structs;");
            yaz.WriteLine("using Library;");
            yaz.WriteLine("using System.Xml;");
            yaz.WriteLine("using Newtonsoft.Json;");

            for (i = 0; i < tablolar.Length; i++)
            {
                yaz.WriteLine("using Business." + tablolar[i] + "Bus;");
                yaz.WriteLine("using Entity." + tablolar[i] + "Ent;");
            }

            yaz.WriteLine("");
            yaz.WriteLine("namespace " + dosya + ".Ajax");
            yaz.WriteLine("{");
            yaz.WriteLine("\tpublic partial class TableMethods : PageStruct");
            yaz.WriteLine("\t{");
            yaz.WriteLine("\t\tprotected void Page_Load(object sender, EventArgs e)");
            yaz.WriteLine("\t\t{");
            yaz.WriteLine("");
            yaz.WriteLine("\t\t}");
            yaz.WriteLine("");

            yaz.WriteLine("\t\t#region Table Class (Tablolarda kullanýlan genel sýnýflar)");
            yaz.WriteLine("");
            yaz.WriteLine("\t\tpublic class TableJson");
            yaz.WriteLine("\t\t{");
            yaz.WriteLine("\t\t\tpublic string SearchText { get; set; }");
            yaz.WriteLine("\t\t\tpublic string OrderBy { get; set; }");
            yaz.WriteLine("\t\t\tpublic string OrderDirection { get; set; }");
            yaz.WriteLine("\t\t\tpublic int Top { get; set; }");
            yaz.WriteLine("\t\t\tpublic int ItemPerPage { get; set; }");
            yaz.WriteLine("\t\t\tpublic int Page { get; set; }");
            yaz.WriteLine("\t\t\tpublic bool WithPassive { get; set; }");
            yaz.WriteLine("\t\t\tpublic dynamic Fields { get; set; }");
            yaz.WriteLine("\t\t}");
            yaz.WriteLine("");
            yaz.WriteLine("\t\tpublic class TableFieldOptionsJson");
            yaz.WriteLine("\t\t{");
            yaz.WriteLine("\t\t\tpublic string Name { get; set; }");
            yaz.WriteLine("\t\t}");
            yaz.WriteLine("");
            yaz.WriteLine("\t\t#endregion");
            yaz.WriteLine("");

            for (i = 0; i < tablolar.Length; i++)
            {
                bag.CommandText = "select * from " + tablolar[i];
                veri = bag.ExecuteReader();

                yaz.WriteLine("");
                yaz.WriteLine("\t\t#region " + tablolar[i] + " Table");
                yaz.WriteLine("");
                yaz.WriteLine("\t\t[WebMethod]");
                yaz.WriteLine("\t\tpublic static List<" + tablolar[i] + "> " + tablolar[i] + "Table(string tableProps)");
                yaz.WriteLine("\t\t{");

                yaz.WriteLine("\t\t\tTableJson _table = JsonConvert.DeserializeObject<TableJson>(tableProps);");
                yaz.WriteLine("\t\t\tList<TableFieldOptionsJson> fieldOptions = new List<TableFieldOptionsJson>();");
                yaz.WriteLine("");
                yaz.WriteLine("\t\t\tforeach (var field in _table.Fields)");
                yaz.WriteLine("\t\t\t{");
                yaz.WriteLine("\t\t\t\tTableFieldOptionsJson fieldOption = JsonConvert.DeserializeObject<TableFieldOptionsJson>(JsonConvert.SerializeObject(field.Value));");
                yaz.WriteLine("\t\t\t\tfieldOption.Name = field.Name;");
                yaz.WriteLine("\t\t\t\tfieldOptions.Add(fieldOption);");
                yaz.WriteLine("\t\t\t}");
                yaz.WriteLine("");
                yaz.WriteLine("\t\t\t" + tablolar[i] + "CP _" + tablolar[i].ToLower() + "CP = new " + tablolar[i] + "CP();");
                yaz.WriteLine("");

                string tipst = veri.GetDataTypeName(1).ToString();
                tipst = tipst.Replace("DBTYPE_I4", "int");
                tipst = tipst.Replace("DBTYPE_BOOL", "bool");
                tipst = tipst.Replace("DBTYPE_I2", "bool");
                tipst = tipst.Replace("DBTYPE_DATE", "DateTime");
                tipst = tipst.Replace("DBTYPE_WVARCHAR", "string");
                tipst = tipst.Replace("DBTYPE_WLONGVARCHAR", "string");
                tipst = tipst.Replace("DBTYPE_NUMERIC", "double");

                if (tipst == "string")
                {
                    string kolon = veri.GetName(1).ToString();
                    yaz.WriteLine("\t\t\tif (!_table.SearchText.IsNull())");
                    yaz.WriteLine("\t\t\t{");
                    yaz.WriteLine("\t\t\t\t_" + tablolar[i].ToLower() + "CP.SearchTextLike = true;");
                    yaz.WriteLine("\t\t\t\t_" + tablolar[i].ToLower() + "CP." + kolon + " = _table.SearchText;");
                    yaz.WriteLine("\t\t\t}");
                    yaz.WriteLine("");
                }

                yaz.WriteLine("\t\t\tif (_table.Top > 0)");
                yaz.WriteLine("\t\t\t{");
                yaz.WriteLine("\t\t\t\t_" + tablolar[i].ToLower() + "CP.Top = _table.Top;");
                yaz.WriteLine("\t\t\t}");
                yaz.WriteLine("");

                for (j = 0; j < veri.FieldCount; j++)
                {
                    string kolon = veri.GetName(j).ToString();

                    if (kolon == "Active")
                    {
                        yaz.WriteLine("\t\t\tif (!_table.WithPassive)");
                        yaz.WriteLine("\t\t\t{");
                        yaz.WriteLine("\t\t\t\t_" + tablolar[i].ToLower() + "CP.Active = true;");
                        yaz.WriteLine("\t\t\t}");
                        yaz.WriteLine("");
                    }
                }

                yaz.WriteLine("\t\t\tswitch (_table.OrderBy)");
                yaz.WriteLine("\t\t\t{");
                for (j = 0; j < veri.FieldCount; j++)
                {
                    string kolon = veri.GetName(j).ToString();

                    yaz.WriteLine("\t\t\t\tcase \"" + kolon + "\": _" + tablolar[i].ToLower() + "CP.OrderBy = " + tablolar[i] + "OrderBy." + kolon + "; break;");
                }
                yaz.WriteLine("\t\t\t\tdefault: _" + tablolar[i].ToLower() + "CP.OrderBy = " + tablolar[i] + "OrderBy." + veri.GetName(0).ToString() + "; break;");
                yaz.WriteLine("\t\t\t}");
                yaz.WriteLine("");

                yaz.WriteLine("\t\t\tswitch (_table.OrderDirection)");
                yaz.WriteLine("\t\t\t{");
                yaz.WriteLine("\t\t\t\tcase \"Asc\": _" + tablolar[i].ToLower() + "CP.OrderDirection = " + tablolar[i] + "OrderDirection.Asc; break;");
                yaz.WriteLine("\t\t\t\tcase \"Desc\": _" + tablolar[i].ToLower() + "CP.OrderDirection = " + tablolar[i] + "OrderDirection.Desc; break;");
                yaz.WriteLine("\t\t\t\tdefault: _" + tablolar[i].ToLower() + "CP.OrderDirection = " + tablolar[i] + "OrderDirection.Asc; break;");
                yaz.WriteLine("\t\t\t}");
                yaz.WriteLine("");

                yaz.WriteLine("\t\t\tList<" + tablolar[i] + "GP> _" + tablolar[i].ToLower() + "GP = new List<" + tablolar[i] + "GP>();");
                yaz.WriteLine("");
                yaz.WriteLine("\t\t\tforeach (TableFieldOptionsJson fieldOption in fieldOptions)");
                yaz.WriteLine("\t\t\t{");
                yaz.WriteLine("\t\t\t\tif (Enum.IsDefined(typeof(" + tablolar[i] + "GP), fieldOption.Name))");
                yaz.WriteLine("\t\t\t\t{");
                yaz.WriteLine("\t\t\t\t\t" + tablolar[i] + "GP _tempGP;");
                yaz.WriteLine("\t\t\t\t\tEnum.TryParse(fieldOption.Name, out _tempGP);");
                yaz.WriteLine("\t\t\t\t\t_" + tablolar[i].ToLower() + "GP.Add(_tempGP);");
                yaz.WriteLine("\t\t\t\t}");
                yaz.WriteLine("\t\t\t}");
                yaz.WriteLine("");

                yaz.WriteLine("\t\t\tList<" + tablolar[i] + "> _" + tablolar[i].ToLower() + "List = " + tablolar[i] + ".Get(_" + tablolar[i].ToLower() + "GP, _" + tablolar[i].ToLower() + "CP).Skip(_table.ItemPerPage * (_table.Page - 1)).Take(_table.ItemPerPage).ToList();");
                yaz.WriteLine("");

                yaz.WriteLine("\t\t\treturn _" + tablolar[i].ToLower() + "List;");
                yaz.WriteLine("\t\t}");
                yaz.WriteLine("");
                yaz.WriteLine("\t\t#endregion");

                veri.Close();
            }

            yaz.WriteLine("\t}");
            yaz.WriteLine("}");

            yaz.Close();
            baglanti.Close();
        }

        private void CreateNotes()
        {
            StreamWriter yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Notlar.txt");
            yaz.WriteLine("Projelere Eklenecek Referanslar :");
            yaz.WriteLine("");
            yaz.WriteLine("Business : Common, DataAccess, Entity");
            yaz.WriteLine("Data Access : Common, Entity, Library");
            yaz.WriteLine("Ana Proje : Business, Entity, Library");
            yaz.WriteLine("");
            yaz.WriteLine("Web.Config'de Örnek MsAccessConnection Value (Dosya Yolu Þeklinde Olacak) :");
            yaz.WriteLine("");
            yaz.WriteLine("<appSettings>");
            yaz.WriteLine("\t<add key=\"MSAccessConnection\" value=\"~/data/data.mdb\" />");
            yaz.WriteLine("</appSettings>");
            yaz.Close();
        }

        //private void CreateRoutes()
        //{
        //    StreamWriter yaz = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Routes.txt");
        //    int j = 1;

        //    for (i = 0; i < tablolar.Length; i++)
        //    {
        //        yaz.WriteLine("<Route RouteName=\"" + (j).ToString() + "-" + tablolar[i].ToLower() + "\" RouteURL=\"" + tablolar[i].ToLower() + "\" PhysicalFile=\"~/Pages/" + tablolar[i] + "/List.aspx\"></Route>");
        //        j++;
        //        yaz.WriteLine("<Route RouteName=\"" + (j).ToString() + "-" + tablolar[i].ToLower() + "-ekle\" RouteURL=\"" + tablolar[i].ToLower() + "-ekle\" PhysicalFile=\"~/Pages/" + tablolar[i] + "/Item.aspx\"></Route>");
        //        j++;
        //        yaz.WriteLine("<Route RouteName=\"" + (j).ToString() + "-" + tablolar[i].ToLower() + "-duzenle\" RouteURL=\"" + tablolar[i].ToLower() + "-duzenle-{ID}\" PhysicalFile=\"~/Pages/" + tablolar[i] + "/Item.aspx\"></Route>");
        //        j++;

        //        CreateRouteDir(tablolar[i]);
        //    }
        //    yaz.Close();
        //}

        //void CreateRouteDir(string tabloadi)
        //{
        //    Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Pages");
        //    Directory.CreateDirectory(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Pages\\" + tabloadi + "");

        //    StreamWriter yaz2 = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Pages\\" + tabloadi + "\\List.aspx");
        //    yaz2.WriteLine("<%@ Page Title=\"\" Language=\"C#\" MasterPageFile=\"~/MasterPages/MPMaster.Master\" AutoEventWireup=\"true\" CodeBehind=\"List.aspx.cs\" Inherits=\"MessagePort.Pages." + tabloadi + ".List\" %>");
        //    yaz2.WriteLine("<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"head\" runat=\"server\">");
        //    yaz2.WriteLine("</asp:Content>");
        //    yaz2.WriteLine("<asp:Content ID=\"Content2\" ContentPlaceHolderID=\"ContentPlaceHolder1\" runat=\"server\">");
        //    yaz2.WriteLine("</asp:Content>");
        //    yaz2.Close();

        //    yaz2 = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Pages\\" + tabloadi + "\\List.aspx.cs");
        //    yaz2.WriteLine("using System;");
        //    yaz2.WriteLine("using System.Collections.Generic;");
        //    yaz2.WriteLine("using System.Linq;");
        //    yaz2.WriteLine("using System.Web;");
        //    yaz2.WriteLine("using System.Web.UI;");
        //    yaz2.WriteLine("using Structs;");
        //    yaz2.WriteLine("");
        //    yaz2.WriteLine("namespace MessagePort.Pages." + tabloadi + "");
        //    yaz2.WriteLine("{");
        //    yaz2.WriteLine("\tpublic partial class List : PageStruct");
        //    yaz2.WriteLine("\t{");
        //    yaz2.WriteLine("\t\tprotected void Page_Load(object sender, EventArgs e)");
        //    yaz2.WriteLine("\t\t{");
        //    yaz2.WriteLine("");
        //    yaz2.WriteLine("\t\t}");
        //    yaz2.WriteLine("\t}");
        //    yaz2.WriteLine("}");
        //    yaz2.Close();

        //    yaz2 = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Pages\\" + tabloadi + "\\Item.aspx");
        //    yaz2.WriteLine("<%@ Page Title=\"\" Language=\"C#\" MasterPageFile=\"~/MasterPages/MPMaster.Master\" AutoEventWireup=\"true\" CodeBehind=\"Item.aspx.cs\" Inherits=\"MessagePort.Pages." + tabloadi + ".Item\" %>");
        //    yaz2.WriteLine("<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"head\" runat=\"server\">");
        //    yaz2.WriteLine("</asp:Content>");
        //    yaz2.WriteLine("<asp:Content ID=\"Content2\" ContentPlaceHolderID=\"ContentPlaceHolder1\" runat=\"server\">");
        //    yaz2.WriteLine("</asp:Content>");
        //    yaz2.Close();

        //    yaz2 = File.CreateText(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Pages\\" + tabloadi + "\\Item.aspx.cs");
        //    yaz2.WriteLine("using System;");
        //    yaz2.WriteLine("using System.Collections.Generic;");
        //    yaz2.WriteLine("using System.Linq;");
        //    yaz2.WriteLine("using System.Web;");
        //    yaz2.WriteLine("using System.Web.UI;");
        //    yaz2.WriteLine("using Structs;");
        //    yaz2.WriteLine("");
        //    yaz2.WriteLine("namespace MessagePort.Pages." + tabloadi + "");
        //    yaz2.WriteLine("{");
        //    yaz2.WriteLine("\tpublic partial class Item : PageStruct");
        //    yaz2.WriteLine("\t{");
        //    yaz2.WriteLine("\t\tprotected void Page_Load(object sender, EventArgs e)");
        //    yaz2.WriteLine("\t\t{");
        //    yaz2.WriteLine("");
        //    yaz2.WriteLine("\t\t}");
        //    yaz2.WriteLine("\t}");
        //    yaz2.WriteLine("}");
        //    yaz2.Close();
        //}

        public enum DatabaseType
        {
            Access,
            MySql,
            MsSql
        }
    }
}