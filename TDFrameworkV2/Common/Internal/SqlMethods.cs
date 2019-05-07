// ==============================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v2.9.2.1
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 03.08.2016
// SPECIAL NOTES    : Thrashead
// ==============================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TDFramework;

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

        internal static ResultBox ExecuteReader(string _querystring, List<SqlParameter> _parameters = null, CommandType? _sqlCommandType = null)
		{
			ResultBox rb = new ResultBox();
			SqlDataAdapter dataAdap = new SqlDataAdapter();

			dataAdap.SelectCommand = new SqlCommand();
			dataAdap.SelectCommand.Connection = TDConnection.SqlConnection;
            _querystring = _querystring.MakeSingle(" ");
            dataAdap.SelectCommand.CommandText = _querystring;
			dataAdap.SelectCommand.CommandType = ReturnCommandType(_sqlCommandType);
            rb.QueryString = _querystring;
            rb.Parameters = _parameters;

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
				rb.Data = new DataTable();
                dataAdap.Fill(rb.Data);
                rb.DataCount = rb.Data.Rows.Count;
				rb.Result = true;
			}
			catch (Exception ex)
			{
                rb.CleanResultBox();
                rb.Result = false;
				rb.ErrorMessage = ex.Message;
				rb.ErrorLayer = ErrorLayers.COMMON;
			}
			finally
			{
                dataAdap.SelectCommand.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

			return rb;
		}
        
        internal static ResultBox ExecuteNonQuery(string _querystring, List<SqlParameter> _parameters = null, CommandType? _sqlCommandType = null)
		{
			ResultBox rb = new ResultBox();
            SqlCommand executeCmd = new SqlCommand();

            executeCmd.Connection = TDConnection.SqlConnection;
			executeCmd.CommandType = ReturnCommandType(_sqlCommandType);
            _querystring = _querystring.MakeSingle(" ");
            executeCmd.CommandText = _querystring;
            rb.QueryString = _querystring;
            rb.Parameters = _parameters;

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
				rb.Result = true;
			}
			catch (Exception ex)
			{
                rb.CleanResultBox();
                rb.Result = false;
				rb.ErrorMessage = ex.Message;
				rb.ErrorLayer = ErrorLayers.COMMON;
			}
			finally
			{
                executeCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

			return rb;
		}

        internal static ResultBox ExecuteScalar(string _querystring, List<SqlParameter> _parameters = null, CommandType? _sqlCommandType = null)
		{
			ResultBox rb = new ResultBox();
			SqlCommand executeCmd = new SqlCommand();

            executeCmd.Connection = TDConnection.SqlConnection;
            executeCmd.CommandType = ReturnCommandType(_sqlCommandType);
            _querystring = _querystring.MakeSingle(" ");
            executeCmd.CommandText = _querystring;
            rb.QueryString = _querystring;
            rb.Parameters = _parameters;

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
				rb.Data = executeCmd.ExecuteScalar();
                rb.DataCount = 1;
                rb.Result = true;
			}
			catch(Exception ex)
			{
                rb.CleanResultBox();
                rb.Result = false;
				rb.ErrorMessage = ex.Message;
				rb.ErrorLayer = ErrorLayers.COMMON;
			}
			finally
			{
                executeCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

			return rb;
		}
	}
}
