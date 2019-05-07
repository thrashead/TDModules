// ==============================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v3.2.2.3
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 07.05.2019
// SPECIAL NOTES    : Thrashead
// ==============================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TDFramework.Library;

namespace TDFramework.Common.Internal
{
    internal sealed class SqlMethods
    {
        static SqlMethods()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        private static CommandType ReturnCommandType(CommandType? sqlCommandType)
        {
            switch (sqlCommandType)
            {
                case CommandType.StoredProcedure: return CommandType.StoredProcedure;
                case CommandType.TableDirect: return CommandType.TableDirect;
                case CommandType.Text: return CommandType.Text;
                default: return CommandType.Text;
            }
        }

        internal static Table ExecuteReader(string queryString, List<SqlParameter> parameters = null, CommandType? sqlCommandType = null)
        {
            Table table = new Table();

            SqlDataAdapter dataAdap = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand { Connection = TDConnection.SqlConnection }
            };

            queryString = queryString.MakeSingle(" ");
            dataAdap.SelectCommand.CommandText = queryString;
            dataAdap.SelectCommand.CommandType = ReturnCommandType(sqlCommandType);
            table.QueryString = queryString;
            table.Parameters = parameters;

            if (parameters != null)
            {
                foreach (SqlParameter item in parameters)
                {
                    object value = item.Value ?? DBNull.Value;
                    dataAdap.SelectCommand.Parameters.AddWithValue(item.ParameterName, value);
                }
            }

            try
            {
                dataAdap.SelectCommand.Connection.Open();
                table.Data = new DataTable();
                dataAdap.Fill(table.Data);
                table.Count = table.Data.Rows.Count;
            }
            catch (Exception ex)
            {
                table.Error = new Error { Message = ex.Message, Layer = ErrorLayers.COMMON };
            }
            finally
            {
                dataAdap.SelectCommand.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
        }

        internal static Table ExecuteNonQuery(string queryString, List<SqlParameter> parameters = null, CommandType? sqlCommandType = null)
        {
            Table table = new Table();

            SqlCommand executeCmd = new SqlCommand
            {
                Connection = TDConnection.SqlConnection,
                CommandType = ReturnCommandType(sqlCommandType)
            };

            queryString = queryString.MakeSingle(" ");
            executeCmd.CommandText = queryString;
            table.QueryString = queryString;
            table.Parameters = parameters;

            if (parameters != null)
            {
                foreach (SqlParameter item in parameters)
                {
                    object value = item.Value ?? DBNull.Value;
                    executeCmd.Parameters.AddWithValue(item.ParameterName, value);
                }
            }

            try
            {
                executeCmd.Connection.Open();
                executeCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                table.Error = new Error { Message = ex.Message, Layer = ErrorLayers.COMMON };
            }
            finally
            {
                executeCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
        }

        internal static Table ExecuteScalar(string queryString, List<SqlParameter> parameters = null, CommandType? sqlCommandType = null)
        {
            Table table = new Table();

            SqlCommand executeCmd = new SqlCommand
            {
                Connection = TDConnection.SqlConnection,
                CommandType = ReturnCommandType(sqlCommandType)
            };

            queryString = queryString.MakeSingle(" ");
            executeCmd.CommandText = queryString;
            table.QueryString = queryString;
            table.Parameters = parameters;

            if (parameters != null)
            {
                foreach (SqlParameter item in parameters)
                {
                    object value = item.Value ?? DBNull.Value;
                    executeCmd.Parameters.AddWithValue(item.ParameterName, value);
                }
            }

            try
            {
                executeCmd.Connection.Open();
                table.Data = executeCmd.ExecuteScalar();
                table.Count = 1;
            }
            catch (Exception ex)
            {
                table.Error = new Error { Message = ex.Message, Layer = ErrorLayers.COMMON };
            }
            finally
            {
                executeCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
        }
    }
}
