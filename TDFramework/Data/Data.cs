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
using System.Data;
using System.Linq;
using System.Reflection;
using System.Data.SqlClient;
using System.Collections.Generic;
using TDFramework.Common;
using TDFramework.Common.TDModel;

namespace TDFramework
{
    internal sealed class Data<T> where T : ITDModel
    {
        static Data()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        #region Select

        internal static Table<T> Select(dynamic _columns = null, List<Where> _whereList = null, Select _selectValues = null)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();

            SqlCommand selectCmd = new SqlCommand();

            string querystring = "";

            if (_selectValues != null)
            {
                if (_selectValues.Pager != null)
                {
                    if (!String.IsNullOrEmpty(_selectValues.Pager.FirstRecord) && !String.IsNullOrEmpty(_selectValues.Pager.LastRecord))
                    {
                        querystring = ApplySkipTake(tableName, selectCmd, _selectValues, _whereList, _columns);
                    }
                    else
                    {
                        querystring = "Select " + ApplyDistinct(_selectValues) + ApplyTop(_selectValues) + ApplyAggregate(_selectValues);

                        querystring += ApplySelectColumns(_columns, _selectValues);

                        querystring = querystring.Trim().TrimEnd(',');

                        querystring += " From " + tableName;

                        querystring += ApplyWhereList(selectCmd, _whereList);

                        querystring += ApplyGroupBy(_selectValues);

                        querystring += ApplyHaving(selectCmd, _selectValues);

                        querystring += ApplyOrder(_selectValues);
                    }
                }
                else
                {
                    querystring = "Select " + ApplyDistinct(_selectValues) + ApplyTop(_selectValues) + ApplyAggregate(_selectValues);

                    querystring += ApplySelectColumns(_columns, _selectValues);

                    querystring = querystring.Trim().TrimEnd(',');

                    querystring += " From " + tableName;

                    querystring += ApplyWhereList(selectCmd, _whereList);

                    querystring += ApplyGroupBy(_selectValues);

                    querystring += ApplyHaving(selectCmd, _selectValues);

                    querystring += ApplyOrder(_selectValues);
                }
            }
            else
            {
                querystring = "Select " + ApplyDistinct(_selectValues) + ApplyTop(_selectValues) + ApplyAggregate(_selectValues);

                querystring += ApplySelectColumns(_columns, _selectValues);

                querystring = querystring.Trim().TrimEnd(',');

                querystring += " From " + tableName;

                querystring += ApplyWhereList(selectCmd, _whereList);

                querystring += ApplyGroupBy(_selectValues);

                querystring += ApplyHaving(selectCmd, _selectValues);

                querystring += ApplyOrder(_selectValues);
            }

            return ReturnSelect(selectCmd, querystring);
        }

        private static Table<T> ReturnSelect(SqlCommand _selectCmd, string _querystring)
        {
            Table<T> table = new Table<T>();
            SqlDataAdapter dataAdap = new SqlDataAdapter();

            dataAdap.SelectCommand = _selectCmd;
            dataAdap.SelectCommand.Connection = TDConnection.SqlConnection;
            _querystring = _querystring.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            dataAdap.SelectCommand.CommandText = _querystring;
            table.QueryString = _querystring;
            table.Parameters = _selectCmd.Parameters.ToParameterList();

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
                table.Error.Layer = ErrorLayers.DATA;
            }
            finally
            {
                dataAdap.SelectCommand.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
        }

        private static string ApplyAggregate(Select _selectValues = null)
        {
            string querystring = "";

            if (_selectValues != null)
            {
                if (_selectValues.Aggregate != null)
                {
                    if (_selectValues.Aggregate.Column != null)
                    {
                        Type typeModel = typeof(T);
                        PropertyInfo propInfo = typeModel.GetProperty(_selectValues.Aggregate.Column.ToString());

                        if (propInfo.PropertyType.InType())
                        {
                            switch (_selectValues.Aggregate.Agregate)
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

                            querystring += "([" + propInfo.GetTableColumnName() + "]) AggColumn, ";
                        }
                    }
                }
            }

            return querystring;
        }

        private static string ApplyTop(Select _selectValues = null)
        {
            if (_selectValues != null)
            {
                if (_selectValues.Top != null)
                {
                    return " Top " + _selectValues.Top.ToString() + " ";
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

        private static string ApplySelectColumns(dynamic _columns, Select _selectValues)
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
                                querystring += "[" + propInfo.GetTableColumnName() + "], ";
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
                        querystring += "[" + propInfo.GetTableColumnName() + "]";
                    }
                }
            }

            querystring = querystring.Trim().TrimEnd(',');

            return querystring;
        }

        private static string ApplyGroupBy(Select _selectValues)
        {
            string querystring = "";

            if (_selectValues != null)
            {
                if (_selectValues.Aggregate != null)
                {
                    if (_selectValues.Aggregate.GroupColumns != null)
                    {
                        Type type = _selectValues.Aggregate.GroupColumns.GetType();

                        if (type.IsGenericType)
                        {
                            if (_selectValues.Aggregate.GroupColumns.Count > 0)
                            {
                                foreach (dynamic item in _selectValues.Aggregate.GroupColumns)
                                {
                                    Type typeModel = typeof(T);
                                    PropertyInfo propInfo = typeModel.GetProperty(item.ToString());

                                    if (propInfo.PropertyType.InType())
                                    {
                                        querystring += "[" + propInfo.GetTableColumnName() + "], ";
                                    }
                                }
                            }
                        }
                        else
                        {
                            Type typeModel = typeof(T);
                            PropertyInfo propInfo = typeModel.GetProperty(_selectValues.Aggregate.GroupColumns.ToString());

                            if (propInfo.PropertyType.InType())
                            {
                                querystring += "[" + propInfo.GetTableColumnName() + "]";
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

        private static string ApplyHaving(SqlCommand _cmd, Select _selectValues)
        {
            Type typeModel = typeof(T);

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string querystring = "";
            if (_selectValues != null)
            {
                if (_selectValues.Aggregate != null)
                {
                    if (_selectValues.Aggregate.GroupColumns != null)
                    {
                        if (_selectValues.Aggregate.Having != null)
                        {
                            if (_selectValues.Aggregate.Having.Count > 0)
                            {
                                foreach (Having item in _selectValues.Aggregate.Having)
                                {
                                    if (props.Where(a => a.Name == item.Column.ToString()).ToList().Count > 0)
                                    {
                                        PropertyInfo pinfo = props.Where(a => a.Name == item.Column.ToString()).ToList().FirstOrDefault();
                                        item.Column = pinfo.GetTableColumnName();
                                    }
                                }

                                HavingValues cv = Having.CreateHaving(_selectValues.Aggregate.Having);

                                querystring = " Having " + cv.QueryString;
                                _cmd.Parameters.AddRange(cv.Parameters.ToArray());
                            }
                        }
                    }
                }
            }

            return querystring;
        }

        private static string ApplyOrder(Select _selectValues = null)
        {
            string querystring = "";

            if (_selectValues != null)
            {
                if (_selectValues.OrderColumn != null)
                {
                    Type typeModel = typeof(T);
                    PropertyInfo propInfo = typeModel.GetProperty(_selectValues.OrderColumn.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        querystring += " Order By [" + propInfo.GetTableColumnName() + "]";

                        if (_selectValues.OrderBy != null)
                        {
                            string orderby = _selectValues.OrderBy.ToString() == "DESC" ? " Desc" : " Asc";
                            querystring += orderby;
                        }
                    }
                }
            }

            return querystring;
        }

        private static string ApplySkipTake(string _tableName, SqlCommand _selectCmd, Select _selectValues, List<Where> _whereList = null, dynamic _columns = null)
        {
            string order = ApplyOrder(_selectValues);

            Type typeModel = typeof(T);
            List<PropertyInfo> props = typeModel.GetProperties().ToList();

            order = order == "" ? "Order By " + props.ReturnFirstColumn() : order;

            string querystring = "With Pager As (Select Row_Number() Over (" + order + ") As 'RowNumber', ";

            querystring += ApplyAggregate(_selectValues) + ApplySelectColumns(_columns, _selectValues);

            querystring = querystring.Trim().TrimEnd(',');

            querystring += " From " + _tableName + " " + ApplyWhereList(_selectCmd, _whereList);

            querystring += ApplyGroupBy(_selectValues);

            querystring += ApplyHaving(_selectCmd, _selectValues);

            querystring += ") Select" + ApplyDistinct(_selectValues) + ApplyTop(_selectValues);

            querystring += " * From Pager ";

            querystring += "Where RowNumber Between " + _selectValues.Pager.FirstRecord + " AND " + _selectValues.Pager.LastRecord;

            return querystring;
        }

        #endregion

        #region Insert

        internal static Table<T> Insert(dynamic _values, bool returnID = false)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();
            object obj = (T)_values;

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string identity = props.ReturnIDColumn();

            Table<T> table = new Table<T>();
            SqlCommand insertCmd = new SqlCommand();

            string querystring = props.ReturnQueryStringForInsert(tableName, identity, returnID);

            querystring = querystring.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            insertCmd.CommandText = querystring;
            table.QueryString = querystring;

            foreach (PropertyInfo item in props)
            {
                if (item.PropertyType.InType())
                {
                    if (identity != item.Name)
                    {
                        insertCmd.Parameters.AddWithValue("@" + item.GetTableColumnName(), ((T)_values).GetType().GetProperty(item.Name).GetValue(obj, null) ?? (object)DBNull.Value);
                    }
                }
            }

            table.Parameters = insertCmd.Parameters.ToParameterList();

            insertCmd.Connection = TDConnection.SqlConnection;

            try
            {
                insertCmd.Connection.Open();

                if (returnID == true)
                {
                    table.Data = insertCmd.ExecuteScalar();
                    if (table.Data.GetType() != typeof(DBNull))
                    {
                        table.Count = 1;
                    }
                }
                else
                {
                    insertCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                table.Error = new Error();
                table.Error.Message = ex.Message;
                table.Error.Layer = ErrorLayers.DATA;
            }
            finally
            {
                insertCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
        }

        #endregion

        #region Update

        internal static Table<T> Update(dynamic _values, dynamic _columns = null, List<Where> _whereList = null)
        {
            SqlCommand updateCmd = new SqlCommand();

            Type typeUpdateColumns = _columns == null ? null : _columns.GetType();

            string querystring = ApplyParameter(updateCmd, _values, _columns, typeUpdateColumns);

            querystring += ApplyWhereList(updateCmd, _whereList);

            return ReturnUpdate(updateCmd, querystring);
        }

        private static Table<T> ReturnUpdate(SqlCommand _updateCmd, string _querystring)
        {
            Table<T> table = new Table<T>();

            _updateCmd.Connection = TDConnection.SqlConnection;
            _querystring = _querystring.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            _updateCmd.CommandText = _querystring;
            table.QueryString = _querystring;
            table.Parameters = _updateCmd.Parameters.ToParameterList();

            try
            {
                _updateCmd.Connection.Open();
                _updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                table.Error = new Error();
                table.Error.Message = ex.Message;
                table.Error.Layer = ErrorLayers.DATA;
            }
            finally
            {
                _updateCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
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
                                querystring += "[" + propInfo.GetTableColumnName() + "]=@" + propInfo.GetTableColumnName() + ",";

                                _updateCmd.Parameters.AddWithValue("@" + propInfo.GetTableColumnName() + "", propInfo.GetValue(obj, null) ?? (object)DBNull.Value);
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
                            querystring += "[" + propInfo.GetTableColumnName() + "]=@" + propInfo.GetTableColumnName() + ",";

                            _updateCmd.Parameters.AddWithValue("@" + propInfo.GetTableColumnName() + "", propInfo.GetValue(obj, null) ?? (object)DBNull.Value);
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
                            querystring += "[" + item.GetTableColumnName() + "]=@" + item.GetTableColumnName() + ",";
                        }
                    }
                }

                foreach (PropertyInfo item in props)
                {
                    if (item.PropertyType.InType())
                    {
                        if (identity != item.Name)
                        {
                            _updateCmd.Parameters.AddWithValue("@" + item.GetTableColumnName(), ((T)_values).GetType().GetProperty(item.Name).GetValue(obj, null) ?? (object)DBNull.Value);
                        }
                    }
                }
            }

            querystring = querystring.TrimEnd(',');

            return querystring;
        }

        #endregion

        #region Delete

        internal static Table<T> Delete(List<Where> _whereList = null)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();

            SqlCommand deleteCmd = new SqlCommand();

            string querystring = "Delete From " + tableName;

            querystring += ApplyWhereList(deleteCmd, _whereList);

            return ReturnDelete(deleteCmd, querystring);
        }

        private static Table<T> ReturnDelete(SqlCommand _deleteCmd, string _querystring)
        {
            Table<T> table = new Table<T>();

            _deleteCmd.Connection = TDConnection.SqlConnection;
            _querystring = _querystring.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            _deleteCmd.CommandText = _querystring;
            table.QueryString = _querystring;
            table.Parameters = _deleteCmd.Parameters.ToParameterList();

            try
            {
                _deleteCmd.Connection.Open();
                _deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                table.Error = new Error();
                table.Error.Message = ex.Message;
                table.Error.Layer = ErrorLayers.DATA;
            }
            finally
            {
                _deleteCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
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
                            item.Column = pinfo.GetTableColumnName();
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
