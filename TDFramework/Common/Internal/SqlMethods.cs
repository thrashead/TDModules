// ==============================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v3.2.2.2
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 03.07.2018
// SPECIAL NOTES    : Thrashead
// ==============================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TDFramework;
using TDFramework.Common.TDModel;

namespace TDFramework.Common
{
    internal sealed class SqlMethods
	{
        static SqlMethods()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

		private static CommandType ReturnCommandType(CommandType? _sqlCommandType)
		{
			switch (_sqlCommandType)
			{
				case CommandType.StoredProcedure: return CommandType.StoredProcedure;
				case CommandType.TableDirect: return CommandType.TableDirect;
				case CommandType.Text: return CommandType.Text;
				default: return CommandType.Text;
			}
		}

        internal static Table ExecuteReader(string _querystring, List<SqlParameter> _parameters = null, CommandType? _sqlCommandType = null)
		{
            Table table = new Table();
			SqlDataAdapter dataAdap = new SqlDataAdapter();

			dataAdap.SelectCommand = new SqlCommand();
			dataAdap.SelectCommand.Connection = TDConnection.SqlConnection;
            _querystring = _querystring.MakeSingle(" ");
            dataAdap.SelectCommand.CommandText = _querystring;
			dataAdap.SelectCommand.CommandType = ReturnCommandType(_sqlCommandType);
            table.QueryString = _querystring;
            table.Parameters = _parameters;

			if (_parameters != null)
			{
				foreach (SqlParameter item in _parameters)
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
                table.Error = new Error();
                table.Error.Message = ex.Message;
                table.Error.Layer = ErrorLayers.COMMON;
			}
			finally
			{
                dataAdap.SelectCommand.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

			return table;
		}

        internal static Table ExecuteNonQuery(string _querystring, List<SqlParameter> _parameters = null, CommandType? _sqlCommandType = null)
		{
            Table table = new Table();
            SqlCommand executeCmd = new SqlCommand();

            executeCmd.Connection = TDConnection.SqlConnection;
			executeCmd.CommandType = ReturnCommandType(_sqlCommandType);
            _querystring = _querystring.MakeSingle(" ");
            executeCmd.CommandText = _querystring;
            table.QueryString = _querystring;
            table.Parameters = _parameters;

			if (_parameters != null)
			{
				foreach (SqlParameter item in _parameters)
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
                table.Error = new Error();
                table.Error.Message = ex.Message;
                table.Error.Layer = ErrorLayers.COMMON;
			}
			finally
			{
                executeCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

			return table;
		}

        internal static Table ExecuteScalar(string _querystring, List<SqlParameter> _parameters = null, CommandType? _sqlCommandType = null)
		{
            Table table = new Table();
			SqlCommand executeCmd = new SqlCommand();

            executeCmd.Connection = TDConnection.SqlConnection;
            executeCmd.CommandType = ReturnCommandType(_sqlCommandType);
            _querystring = _querystring.MakeSingle(" ");
            executeCmd.CommandText = _querystring;
            table.QueryString = _querystring;
            table.Parameters = _parameters;

			if (_parameters != null)
			{
				foreach (SqlParameter item in _parameters)
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
			catch(Exception ex)
			{
                table.Error = new Error();
                table.Error.Message = ex.Message;
                table.Error.Layer = ErrorLayers.COMMON;
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
