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
        static DataHelper()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        #region Select

        internal static ResultBox Select(dynamic _columns = null, List<Where> _whereList = null, Select _select = null)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();

            SqlCommand selectCmd = new SqlCommand();

            string querystring = "";

            if (_select != null)
            {
                if (_select.PageInfo != null)
                {
                    if (!String.IsNullOrEmpty(_select.PageInfo.FirstRecord) && !String.IsNullOrEmpty(_select.PageInfo.LastRecord))
                    {
                        querystring = ApplySkipTake(tableName, selectCmd, _select, _whereList, _columns);
                    }
                    else
                    {
                        querystring = "Select " + ApplyDistinct(_select) + ApplyTop(_select) + ApplyAggregate(_select);

                        querystring += ApplySelectColumns(_columns, _select);

                        querystring = querystring.Trim().TrimEnd(',');

                        querystring += " From " + tableName;

                        querystring += ApplyWhereList(selectCmd, _whereList);

                        querystring += ApplyGroupBy(_select);

                        querystring += ApplyHaving(selectCmd, _select);

                        querystring += ApplyOrder(_select);
                    }
                }
                else
                {
                    querystring = "Select " + ApplyDistinct(_select) + ApplyTop(_select) + ApplyAggregate(_select);

                    querystring += ApplySelectColumns(_columns, _select);

                    querystring = querystring.Trim().TrimEnd(',');

                    querystring += " From " + tableName;

                    querystring += ApplyWhereList(selectCmd, _whereList);

                    querystring += ApplyGroupBy(_select);

                    querystring += ApplyHaving(selectCmd, _select);

                    querystring += ApplyOrder(_select);
                }
            }
            else
            {
                querystring = "Select " + ApplyDistinct(_select) + ApplyTop(_select) + ApplyAggregate(_select);

                querystring += ApplySelectColumns(_columns, _select);

                querystring = querystring.Trim().TrimEnd(',');

                querystring += " From " + tableName;

                querystring += ApplyWhereList(selectCmd, _whereList);

                querystring += ApplyGroupBy(_select);

                querystring += ApplyHaving(selectCmd, _select);

                querystring += ApplyOrder(_select);
            }

            return ReturnSelect(selectCmd, querystring);
        }

        private static ResultBox ReturnSelect(SqlCommand _selectCmd, string _querystring)
        {
            ResultBox rb = new ResultBox();
            SqlDataAdapter dataAdap = new SqlDataAdapter();

            dataAdap.SelectCommand = _selectCmd;
            dataAdap.SelectCommand.Connection = TDConnection.SqlConnection;
            _querystring = _querystring.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            dataAdap.SelectCommand.CommandText = _querystring;
            rb.QueryString = _querystring;
            rb.Parameters = _selectCmd.Parameters.ToParameterList();

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
                rb.ErrorLayer = ErrorLayers.DATAHELPER;
            }
            finally
            {
                dataAdap.SelectCommand.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return rb;
        }

        private static string ApplyAggregate(Select _select = null)
        {
            string querystring = "";

            if (_select != null)
            {
                if (_select.Aggregate != null)
                {
                    if (_select.Aggregate.Column != null)
                    {
                        Type typeModel = typeof(T);
                        PropertyInfo propInfo = typeModel.GetProperty(_select.Aggregate.Column.ToString());

                        if (propInfo.PropertyType.InType())
                        {
                            switch (_select.Aggregate.Agregate)
                            {
                                case Aggregates.AVERAGE: querystring += "Avg";
                                    break;
                                case Aggregates.COUNT: querystring += "Count";
                                    break;
                                case Aggregates.MAXIMUM: querystring += "Max";
                                    break;
                                case Aggregates.MINIMUM: querystring += "Min";
                                    break;
                                case Aggregates.SUMMARY: querystring += "Sum";
                                    break;
                                default: querystring += "Count";
                                    break;
                            }

                            querystring += "([" + propInfo.GetDBColumnName() + "]) AggColumn, ";
                        }
                    }
                }
            }

            return querystring;
        }

        private static string ApplyTop(Select _select = null)
        {
            if (_select != null)
            {
                if (_select.Top != null)
                {
                    return " Top " + _select.Top.ToString() + " ";
                }
                else
                {
                    return " ";
                }
            }
            else
            {
                return " ";
            }
        }

        private static string ApplySelectColumns(dynamic _columns, Select _select)
        {
            string querystring = "";
            SelectColumns selectColumns;

            if (_columns == null)
            {
                querystring += "*";
            }
            else if (Enum.TryParse(_columns.ToString(), out selectColumns))
            {
                querystring += "";
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

        private static string ApplyGroupBy(Select _select)
        {
            string querystring = "";

            if (_select != null)
            {
                if (_select.Aggregate != null)
                {
                    if (_select.Aggregate.GroupColumns != null)
                    {
                        Type type = _select.Aggregate.GroupColumns.GetType();

                        if (type.IsGenericType)
                        {
                            if (_select.Aggregate.GroupColumns.Count > 0)
                            {
                                foreach (dynamic item in _select.Aggregate.GroupColumns)
                                {
                                    Type typeModel = typeof(T);
                                    PropertyInfo propInfo = typeModel.GetProperty(item.ToString());

                                    if (propInfo.PropertyType.InType())
                                    {
                                        querystring += "[" + propInfo.GetDBColumnName() + "], ";
                                    }
                                }
                            }
                        }
                        else
                        {
                            Type typeModel = typeof(T);
                            PropertyInfo propInfo = typeModel.GetProperty(_select.Aggregate.GroupColumns.ToString());

                            if (propInfo.PropertyType.InType())
                            {
                                querystring += "[" + propInfo.GetDBColumnName() + "]";
                            }
                        }
                    }

                    querystring = querystring.Trim().TrimEnd(',');

                    if (querystring != "")
                    {
                        querystring = " Group By " + querystring;
                    }
                }
            }

            return querystring;
        }

        private static string ApplyHaving(SqlCommand _cmd, Select _select)
        {
            Type typeModel = typeof(T);

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string querystring = "";
            if (_select != null)
            {
                if (_select.Aggregate != null)
                {
                    if (_select.Aggregate.GroupColumns != null)
                    {
                        if (_select.Aggregate.Having != null)
                        {
                            if (_select.Aggregate.Having.Count > 0)
                            {
                                foreach (Having item in _select.Aggregate.Having)
                                {
                                    if (props.Where(a => a.Name == item.Column.ToString()).ToList().Count > 0)
                                    {
                                        PropertyInfo pinfo = props.Where(a => a.Name == item.Column.ToString()).ToList().FirstOrDefault();
                                        item.Column = pinfo.GetDBColumnName();
                                    }
                                }

                                HavingValues cv = Having.CreateHaving(_select.Aggregate.Having);

                                querystring = " Having " + cv.QueryString;
                                _cmd.Parameters.AddRange(cv.Parameters.ToArray());
                            }
                        }
                    }
                }
            }

            return querystring;
        }

        private static string ApplyOrder(Select _select = null)
        {
            string querystring = "";

            if (_select != null)
            {
                if (_select.OrderColumn != null)
                {
                    Type typeModel = typeof(T);
                    PropertyInfo propInfo = typeModel.GetProperty(_select.OrderColumn.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        querystring += " Order By [" + propInfo.GetDBColumnName() + "]";

                        if (_select.OrderBy != null)
                        {
                            string orderby = _select.OrderBy.ToString() == "DESC" ? " Desc" : " Asc";
                            querystring += orderby;
                        }
                    }
                }
            }

            return querystring;
        }

        private static string ApplySkipTake(string _tableName, SqlCommand _selectCmd, Select _select, List<Where> _whereList = null, dynamic _columns = null)
        {
            string order = ApplyOrder(_select);

            Type typeModel = typeof(T);
            List<PropertyInfo> props = typeModel.GetProperties().ToList();

            order = order == "" ? "Order By " + props.ReturnFirstColumn() : order;

            string querystring = "With Pager As (Select Row_Number() Over (" + order + ") As 'RowNumber', ";

            querystring += ApplyAggregate(_select) + ApplySelectColumns(_columns, _select);

            querystring = querystring.Trim().TrimEnd(',');

            querystring += " From " + _tableName + " " + ApplyWhereList(_selectCmd, _whereList);

            querystring += ApplyGroupBy(_select);

            querystring += ApplyHaving(_selectCmd, _select);

            querystring += ") Select" + ApplyDistinct(_select) + ApplyTop(_select);

            querystring += " * From Pager ";

            querystring += "Where RowNumber Between " + _select.PageInfo.FirstRecord + " AND " + _select.PageInfo.LastRecord;

            return querystring;
        }

        #endregion

        #region Insert

        internal static ResultBox Insert(dynamic _values, bool _returnID = false)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();
            object obj = (T)_values;

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string identity = props.ReturnIDColumn();

            ResultBox rb = new ResultBox();
            SqlCommand insertCmd = new SqlCommand();

            string querystring = props.ReturnQueryStringForInsert(tableName, identity, _returnID);

            querystring = querystring.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            insertCmd.CommandText = querystring;
            rb.QueryString = querystring;
            rb.Parameters = insertCmd.Parameters.ToParameterList();

            foreach (PropertyInfo item in props)
            {
                if (item.PropertyType.InType())
                {
                    if (identity != item.Name)
                    {
                        insertCmd.Parameters.AddWithValue("@" + item.GetDBColumnName(), ((T)_values).GetType().GetProperty(item.Name).GetValue(obj, null) ?? (object)DBNull.Value);
                    }
                }
            }

            insertCmd.Connection = TDConnection.SqlConnection;

            try
            {
                insertCmd.Connection.Open();
                if (_returnID == true)
                {
                    rb.Data = insertCmd.ExecuteScalar();
                    if (rb.Data.GetType() != typeof(DBNull))
                    {
                        rb.DataCount = 1;
                    }
                }
                else
                {
                    insertCmd.ExecuteNonQuery();
                }
                rb.Result = true;
            }
            catch (Exception ex)
            {
                rb.CleanResultBox();
                rb.Result = false;
                rb.ErrorMessage = ex.Message;
                rb.ErrorLayer = ErrorLayers.DATAHELPER;
            }
            finally
            {
                insertCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return rb;
        }

        #endregion

        #region Update

        internal static ResultBox Update(dynamic _values, dynamic _columns = null, List<Where> _whereList = null)
        {
            SqlCommand updateCmd = new SqlCommand();

            Type typeUpdateColumns = _columns == null ? null : _columns.GetType();

            string querystring = ApplyParameter(updateCmd, _values, _columns, typeUpdateColumns);

            querystring += ApplyWhereList(updateCmd, _whereList);

            return ReturnUpdate(updateCmd, querystring);
        }

        private static ResultBox ReturnUpdate(SqlCommand _updateCmd, string _querystring)
        {
            ResultBox rb = new ResultBox();

            _updateCmd.Connection = TDConnection.SqlConnection;
            _querystring = _querystring.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            _updateCmd.CommandText = _querystring;
            rb.QueryString = _querystring;
            rb.Parameters = _updateCmd.Parameters.ToParameterList();

            try
            {
                _updateCmd.Connection.Open();
                _updateCmd.ExecuteNonQuery();
                rb.Result = true;
            }
            catch (Exception ex)
            {
                rb.CleanResultBox();
                rb.Result = false;
                rb.ErrorMessage = ex.Message;
                rb.ErrorLayer = ErrorLayers.DATAHELPER;
            }
            finally
            {
                _updateCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return rb;
        }

        private static string ApplyParameter(SqlCommand _updateCmd, dynamic _values, dynamic _columns, Type _typeUpdateColumns)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();
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
                                querystring += "[" + propInfo.GetDBColumnName() + "]=@" + propInfo.GetDBColumnName() + ",";

                                _updateCmd.Parameters.AddWithValue("@" + propInfo.GetDBColumnName() + "", propInfo.GetValue(obj, null) ?? (object)DBNull.Value);
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
                            querystring += "[" + propInfo.GetDBColumnName() + "]=@" + propInfo.GetDBColumnName() + ",";

                            _updateCmd.Parameters.AddWithValue("@" + propInfo.GetDBColumnName() + "", propInfo.GetValue(obj, null) ?? (object)DBNull.Value);
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
                            querystring += "[" + item.GetDBColumnName() + "]=@" + item.GetDBColumnName() + ",";
                        }
                    }
                }

                foreach (PropertyInfo item in props)
                {
                    if (item.PropertyType.InType())
                    {
                        if (identity != item.Name)
                        {
                            _updateCmd.Parameters.AddWithValue("@" + item.GetDBColumnName(), ((T)_values).GetType().GetProperty(item.Name).GetValue(obj, null) ?? (object)DBNull.Value);
                        }
                    }
                }
            }

            querystring = querystring.TrimEnd(',');

            return querystring;
        }

        #endregion

        #region Delete

        internal static ResultBox Delete(List<Where> _whereList = null)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();

            SqlCommand deleteCmd = new SqlCommand();

            string querystring = "Delete From " + tableName;

            querystring += ApplyWhereList(deleteCmd, _whereList);

            return ReturnDelete(deleteCmd, querystring);
        }

        private static ResultBox ReturnDelete(SqlCommand _deleteCmd, string _querystring)
        {
            ResultBox rb = new ResultBox();

            _deleteCmd.Connection = TDConnection.SqlConnection;
            _querystring = _querystring.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            _deleteCmd.CommandText = _querystring;
            rb.QueryString = _querystring;
            rb.Parameters = _deleteCmd.Parameters.ToParameterList();

            try
            {
                _deleteCmd.Connection.Open();
                _deleteCmd.ExecuteNonQuery();
                rb.Result = true;
            }
            catch (Exception ex)
            {
                rb.CleanResultBox();
                rb.Result = false;
                rb.ErrorMessage = ex.Message;
                rb.ErrorLayer = ErrorLayers.DATAHELPER;
            }
            finally
            {
                _deleteCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return rb;
        }

        #endregion

        #region ApplyWhereList

        private static string ApplyWhereList(SqlCommand _cmd, List<Where> _whereList)
        {
            Type typeModel = typeof(T);

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string querystring = "";

            if (_whereList != null)
            {
                if (_whereList.Count > 0)
                {
                    foreach (Where item in _whereList)
                    {
                        if (props.Where(a => a.Name == item.Column.ToString()).ToList().Count > 0)
                        {
                            PropertyInfo pinfo = props.Where(a => a.Name == item.Column.ToString()).ToList().FirstOrDefault();
                            item.Column = pinfo.GetDBColumnName();
                        }
                    }

                    WhereValues cv = Where.CreateWhere(_whereList);

                    querystring = " Where " + cv.QueryString;
                    _cmd.Parameters.AddRange(cv.Parameters.ToArray());
                }
            }

            return querystring;
        }

        #endregion

        #region ApplyDistinct

        private static string ApplyDistinct(Select _select = null)
        {
            if (_select != null)
            {
                if (_select.Distinct != false)
                {
                    return " Distinct ";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        private static string ApplyDistinct(bool _applyDistinct = false)
        {
            if (_applyDistinct != false)
            {
                return " Distinct ";
            }
            else
            {
                return "";
            }
        }

        #endregion
    }
}
