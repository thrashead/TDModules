// ================================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v2.2.0.2
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 22.02.2016
// SPECIAL NOTES    : Güzel Program
// ================================

using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;
using System.Collections.Generic;
using TDFramework.Common;
using TDFramework.Common.TDModel;

namespace TDFramework
{
    internal sealed class DataHelper<T> where T : ITDModel
    {
        #region Select

        internal static ResultBox Select(bool _asDataTable, dynamic _columns = null, List<WhereClause> _whereClauses = null, SelectClause _selectClause = null)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.Name;

            SqlCommand selectCmd = new SqlCommand();

            string querystring = "";

            querystring = "Select ";

            querystring += ApplySelectColumns(_columns) + " From " + tableName;

            querystring += ApplyConditions(selectCmd, _whereClauses);

            querystring += ApplyOrder(_selectClause);

            return ReturnSelect(selectCmd, querystring, _asDataTable);
        }

        private static ResultBox ReturnSelect(SqlCommand _selectCmd, string _querystring, bool _asDataTable)
        {
            ResultBox rb = new ResultBox();
            SqlDataAdapter dataAdap = new SqlDataAdapter();

            dataAdap.SelectCommand = _selectCmd;
            dataAdap.SelectCommand.Connection = TDConnection.SqlConnection;
            dataAdap.SelectCommand.CommandText = _querystring;

            try
            {
                dataAdap.SelectCommand.Connection.Open();
                rb.Data = new DataTable();
                dataAdap.Fill(rb.Data);

                rb.Result = true;
            }
            catch (Exception ex)
            {
                rb.Clean();
                rb.Result = false;
                rb.ErrorMessage = ex.Message;
            }
            finally
            {
                dataAdap.SelectCommand.Connection.Close();
            }

            return rb;
        }

        private static string ApplySelectColumns(dynamic _columns)
        {
            string querystring = "";

            if (_columns == null)
            {
                querystring += "*";
            }
            else
            {
                Type type = _columns.GetType();

                if (type.IsGenericType)
                {
                    if (_columns.Count > 0)
                    {
                        foreach (dynamic item in _columns)
                        {
                            Type typeModel = typeof(T);
                            PropertyInfo propInfo = typeModel.GetProperty(item.ToString());

                            if (propInfo.PropertyType.InType())
                            {
                                querystring += "[" + propInfo.GetDBColumnName() + "], ";
                            }
                        }
                    }
                    else
                    {
                        querystring += "*";
                    }
                }
                else
                {
                    Type typeModel = typeof(T);
                    PropertyInfo propInfo = typeModel.GetProperty(_columns.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        querystring += "[" + propInfo.GetDBColumnName() + "]";
                    }
                }
            }

            querystring = querystring.Trim().TrimEnd(',');

            return querystring;
        }

        private static string ApplyOrder(SelectClause _selectClause = null)
        {
            string querystring = "";

            if (_selectClause != null)
            {
                if (_selectClause.OrderColumn != null)
                {
                    Type typeModel = typeof(T);
                    PropertyInfo propInfo = typeModel.GetProperty(_selectClause.OrderColumn.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        querystring += " Order By [" + propInfo.GetDBColumnName() + "]";

                        if (_selectClause.OrderDirection != null)
                        {
                            string orderby = _selectClause.OrderDirection.ToString() == "DESCENDING" ? " Desc" : " Asc";
                            querystring += orderby;
                        }
                    }
                }
            }

            return querystring;
        }

        #endregion

        #region Insert

        internal static ResultBox Insert(dynamic _values)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.Name;
            object obj = (T)_values;

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string identity = props.ReturnIDColumn();

            ResultBox rb = new ResultBox();
            SqlCommand insertCmd = new SqlCommand();

            string querystring = props.ReturnQueryStringForInsert(tableName, identity);

            insertCmd.CommandText = querystring;

            foreach (PropertyInfo item in props)
            {
                if (item.PropertyType.InType())
                {
                    if (identity != item.Name)
                    {
                        insertCmd.Parameters.AddWithValue("@" + item.Name, ((T)_values).GetType().GetProperty(item.Name).GetValue(obj, null) ?? (object)DBNull.Value);
                    }
                }
            }

            insertCmd.Connection = TDConnection.SqlConnection;

            try
            {
                insertCmd.Connection.Open();
                insertCmd.ExecuteNonQuery();
                rb.Result = true;
            }
            catch (Exception ex)
            {
                rb.Clean();
                rb.Result = false;
                rb.ErrorMessage = ex.Message;
            }
            finally
            {
                insertCmd.Connection.Close();
            }

            return rb;
        }

        #endregion

        #region Update

        internal static ResultBox Update(dynamic _values, dynamic _columns = null, List<WhereClause> _whereClauses = null)
        {
            SqlCommand updateCmd = new SqlCommand();
            Type typeUpdateColumns = null;

            if (_columns != null)
            {
                typeUpdateColumns = _columns.GetType();

                if (typeUpdateColumns.IsGenericType)
                {
                    if (_columns.Count <= 0)
                    {
                        return new ResultBox()
                        {
                            ErrorMessage = "updateColumns List is Empty",
                            Result = false
                        };
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(_columns.ToString()))
                    {
                        return new ResultBox()
                        {
                            ErrorMessage = "updateColumns is Empty",
                            Result = false
                        };
                    }
                }
            }

            string querystring = ApplyParameter(updateCmd, _values, _columns, typeUpdateColumns);

            querystring += ApplyConditions(updateCmd, _whereClauses);

            return ReturnUpdate(updateCmd, querystring);
        }

        private static ResultBox ReturnUpdate(SqlCommand _updateCmd, string _querystring)
        {
            ResultBox rb = new ResultBox();

            _updateCmd.Connection = TDConnection.SqlConnection;
            _updateCmd.CommandText = _querystring;

            try
            {
                _updateCmd.Connection.Open();
                _updateCmd.ExecuteNonQuery();
                rb.Result = true;
            }
            catch (Exception ex)
            {
                rb.Clean();
                rb.Result = false;
                rb.ErrorMessage = ex.Message;
            }
            finally
            {
                _updateCmd.Connection.Close();
            }

            return rb;
        }

        private static string ApplyParameter(SqlCommand _updateCmd, dynamic _values, dynamic _columns, Type _typeUpdateColumns)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.Name;
            object obj = (T)_values;

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string identity = props.ReturnIDColumn();

            string querystring = "Update " + tableName + " Set ";

            if (_columns != null)
            {
                _typeUpdateColumns = _columns.GetType();

                if (_typeUpdateColumns.IsGenericType)
                {
                    foreach (dynamic item in _columns)
                    {
                        PropertyInfo propInfo = _values.GetType().GetProperty(item.ToString());

                        if (propInfo.PropertyType.InType())
                        {
                            if (identity != propInfo.Name)
                            {
                                querystring += "[" + propInfo.Name + "]=@" + propInfo.Name + ",";

                                _updateCmd.Parameters.AddWithValue("@" + propInfo.Name + "", propInfo.GetValue(obj, null) ?? (object)DBNull.Value);
                            }
                        }
                    }
                }
                else
                {
                    PropertyInfo propInfo = _values.GetType().GetProperty(_columns.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        if (identity != propInfo.Name)
                        {
                            querystring += "[" + propInfo.Name + "]=@" + propInfo.Name + ",";

                            _updateCmd.Parameters.AddWithValue("@" + propInfo.Name + "", propInfo.GetValue(obj, null) ?? (object)DBNull.Value);
                        }
                    }
                }
            }
            else
            {
                foreach (PropertyInfo item in props)
                {
                    if (item.PropertyType.InType())
                    {
                        if (identity != item.Name)
                        {
                            querystring += "[" + item.Name + "]=@" + item.Name + ",";
                        }
                    }
                }

                foreach (PropertyInfo item in props)
                {
                    if (item.PropertyType.InType())
                    {
                        if (identity != item.Name)
                        {
                            _updateCmd.Parameters.AddWithValue("@" + item.Name, ((T)_values).GetType().GetProperty(item.Name).GetValue(obj, null) ?? (object)DBNull.Value);
                        }
                    }
                }
            }

            querystring = querystring.TrimEnd(',');

            return querystring;
        }

        #endregion

        #region Delete

        internal static ResultBox Delete(List<WhereClause> _whereClauses = null)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.Name;

            SqlCommand deleteCmd = new SqlCommand();

            string querystring = "Delete From " + tableName;

            querystring += ApplyConditions(deleteCmd, _whereClauses);

            return ReturnDelete(deleteCmd, querystring);
        }

        private static ResultBox ReturnDelete(SqlCommand _deleteCmd, string _querystring)
        {
            ResultBox rb = new ResultBox();

            _deleteCmd.Connection = TDConnection.SqlConnection;
            _deleteCmd.CommandText = _querystring;

            try
            {
                _deleteCmd.Connection.Open();
                _deleteCmd.ExecuteNonQuery();
                rb.Result = true;
            }
            catch (Exception ex)
            {
                rb.Clean();
                rb.Result = false;
                rb.ErrorMessage = ex.Message;
            }
            finally
            {
                _deleteCmd.Connection.Close();
            }

            return rb;
        }

        #endregion

        #region ApplyConditions

        private static string ApplyConditions(SqlCommand _cmd, List<WhereClause> _whereClauses)
        {
            Type typeModel = typeof(T);

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string querystring = "";

            if (_whereClauses != null)
            {
                if (_whereClauses.Count > 0)
                {
                    foreach (WhereClause item in _whereClauses)
                    {
                        if (props.Where(a => a.Name == item.Column.ToString()).ToList().Count > 0)
                        {
                            PropertyInfo pinfo = props.Where(a => a.Name == item.Column.ToString()).ToList().FirstOrDefault();
                            item.Column = pinfo.Name;
                        }
                    }

                    SqlConditionValues cv = WhereClause.CreateCondition(_whereClauses);

                    querystring = " Where " + cv.QueryString;
                    _cmd.Parameters.AddRange(cv.Parameters.ToArray());
                }
            }

            return querystring;
        }

        #endregion
    }
}
