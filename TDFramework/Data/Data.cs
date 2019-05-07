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
using System.Linq;
using System.Reflection;
using TDFramework.Common;
using TDFramework.Common.TDModel;
using TDFramework.Library;

namespace TDFramework.Data
{
    internal sealed class Data<T> where T : ITDModel
    {
        static Data()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        #region Select

        internal static Table<T> Select(dynamic columns = null, List<Where> whereList = null, Select selectValues = null)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();

            SqlCommand selectCmd = new SqlCommand();

            string queryStr;

            if (selectValues != null)
            {
                if (selectValues.Pager != null)
                {
                    if (!string.IsNullOrEmpty(selectValues.Pager.FirstRecord) && !string.IsNullOrEmpty(selectValues.Pager.LastRecord))
                    {
                        queryStr = ApplySkipTake(tableName, selectCmd, selectValues, whereList, columns);
                    }
                    else
                    {
                        queryStr = "Select " + ApplyDistinct(selectValues) + ApplyTop(selectValues) + ApplyAggregate(selectValues);

                        queryStr += ApplySelectColumns(columns);

                        queryStr = queryStr.Trim().TrimEnd(',');

                        queryStr += " From " + tableName;

                        queryStr += ApplyWhereList(selectCmd, whereList);

                        queryStr += ApplyGroupBy(selectValues);

                        queryStr += ApplyHaving(selectCmd, selectValues);

                        queryStr += ApplyOrder(selectValues);
                    }
                }
                else
                {
                    queryStr = "Select " + ApplyDistinct(selectValues) + ApplyTop(selectValues) + ApplyAggregate(selectValues);

                    queryStr += ApplySelectColumns(columns);

                    queryStr = queryStr.Trim().TrimEnd(',');

                    queryStr += " From " + tableName;

                    queryStr += ApplyWhereList(selectCmd, whereList);

                    queryStr += ApplyGroupBy(selectValues);

                    queryStr += ApplyHaving(selectCmd, selectValues);

                    queryStr += ApplyOrder(selectValues);
                }
            }
            else
            {
                queryStr = "Select " + ApplyDistinct(selectValues) + ApplyTop(selectValues) + ApplyAggregate(selectValues);

                queryStr += ApplySelectColumns(columns);

                queryStr = queryStr.Trim().TrimEnd(',');

                queryStr += " From " + tableName;

                queryStr += ApplyWhereList(selectCmd, whereList);

                queryStr += ApplyGroupBy(selectValues);

                queryStr += ApplyHaving(selectCmd, selectValues);

                queryStr += ApplyOrder(selectValues);
            }

            return ReturnSelect(selectCmd, queryStr);
        }

        private static Table<T> ReturnSelect(SqlCommand selectCmd, string queryString)
        {
            Table<T> table = new Table<T>();

            SqlDataAdapter dataAdap = new SqlDataAdapter { SelectCommand = selectCmd };

            dataAdap.SelectCommand.Connection = TDConnection.SqlConnection;
            queryString = queryString.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            dataAdap.SelectCommand.CommandText = queryString;
            table.QueryString = queryString;
            table.Parameters = selectCmd.Parameters.ToParameterList();

            try
            {
                dataAdap.SelectCommand.Connection.Open();
                table.Data = new DataTable();
                dataAdap.Fill(table.Data);
                table.Count = table.Data.Rows.Count;
            }
            catch (Exception ex)
            {
                table.Error = new Error { Message = ex.Message, Layer = ErrorLayers.DATA };
            }
            finally
            {
                dataAdap.SelectCommand.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
        }

        private static string ApplyAggregate(Select selectValues = null)
        {
            string queryStr = "";

            if (selectValues?.Aggregate != null)
            {
                if (selectValues.Aggregate.Column != null)
                {
                    Type typeModel = typeof(T);
                    PropertyInfo propInfo = typeModel.GetProperty(selectValues.Aggregate.Column.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        switch (selectValues.Aggregate.Agregate)
                        {
                            case Aggregates.AVERAGE:
                                queryStr += "Avg";
                                break;
                            case Aggregates.COUNT:
                                queryStr += "Count";
                                break;
                            case Aggregates.MAXIMUM:
                                queryStr += "Max";
                                break;
                            case Aggregates.MINIMUM:
                                queryStr += "Min";
                                break;
                            case Aggregates.SUMMARY:
                                queryStr += "Sum";
                                break;
                            default:
                                queryStr += "Count";
                                break;
                        }

                        queryStr += "([" + propInfo.GetTableColumnName() + "]) AggColumn, ";
                    }
                }
            }

            return queryStr;
        }

        private static string ApplyTop(Select selectValues = null)
        {
            if (selectValues != null)
            {
                if (selectValues.Top != null)
                {
                    return " Top " + selectValues.Top.ToString() + " ";
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

        private static string ApplySelectColumns(dynamic columns)
        {
            string queryStr = "";

            if (columns == null)
            {
                queryStr += "*";
            }
            else if (Enum.TryParse(columns.ToString(), out SelectColumns _))
            {
                queryStr += "";
            }
            else
            {
                Type type = columns.GetType();

                if (type.IsGenericType)
                {
                    if (columns.Count > 0)
                    {
                        foreach (dynamic item in columns)
                        {
                            Type typeModel = typeof(T);
                            PropertyInfo propInfo = typeModel.GetProperty(item.ToString());

                            if (propInfo.PropertyType.InType())
                            {
                                queryStr += "[" + propInfo.GetTableColumnName() + "], ";
                            }
                        }
                    }
                    else
                    {
                        queryStr += "*";
                    }
                }
                else
                {
                    Type typeModel = typeof(T);
                    PropertyInfo propInfo = typeModel.GetProperty(columns.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        queryStr += "[" + propInfo.GetTableColumnName() + "]";
                    }
                }
            }

            queryStr = queryStr.Trim().TrimEnd(',');

            return queryStr;
        }

        private static string ApplyGroupBy(Select selectValues)
        {
            string queryStr = "";

            if (selectValues?.Aggregate != null)
            {
                if (selectValues.Aggregate.GroupColumns != null)
                {
                    Type type = selectValues.Aggregate.GroupColumns.GetType();

                    if (type.IsGenericType)
                    {
                        if (selectValues.Aggregate.GroupColumns.Count > 0)
                        {
                            foreach (dynamic item in selectValues.Aggregate.GroupColumns)
                            {
                                Type typeModel = typeof(T);
                                PropertyInfo propInfo = typeModel.GetProperty(item.ToString());

                                if (propInfo.PropertyType.InType())
                                {
                                    queryStr += "[" + propInfo.GetTableColumnName() + "], ";
                                }
                            }
                        }
                    }
                    else
                    {
                        Type typeModel = typeof(T);
                        PropertyInfo propInfo = typeModel.GetProperty(selectValues.Aggregate.GroupColumns.ToString());

                        if (propInfo.PropertyType.InType())
                        {
                            queryStr += "[" + propInfo.GetTableColumnName() + "]";
                        }
                    }
                }

                queryStr = queryStr.Trim().TrimEnd(',');

                if (queryStr != "")
                {
                    queryStr = " Group By " + queryStr;
                }
            }

            return queryStr;
        }

        private static string ApplyHaving(SqlCommand cmd, Select selectValues)
        {
            Type typeModel = typeof(T);

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string queryStr = "";
            if (selectValues?.Aggregate != null)
            {
                if (selectValues.Aggregate.GroupColumns != null)
                {
                    if (selectValues.Aggregate.Having != null)
                    {
                        if (selectValues.Aggregate.Having.Count > 0)
                        {
                            foreach (Having item in selectValues.Aggregate.Having)
                            {
                                if (props.Where(a => a.Name == item.Column.ToString()).ToList().Count > 0)
                                {
                                    PropertyInfo pinfo = props.Where(a => a.Name == item.Column.ToString()).ToList().FirstOrDefault();
                                    item.Column = pinfo.GetTableColumnName();
                                }
                            }

                            HavingValues cv = Having.CreateHaving(selectValues.Aggregate.Having);

                            queryStr = " Having " + cv.QueryString;
                            cmd.Parameters.AddRange(cv.Parameters.ToArray());
                        }
                    }
                }
            }

            return queryStr;
        }

        private static string ApplyOrder(Select selectValues = null)
        {
            string queryStr = "";

            if (selectValues != null)
            {
                if (selectValues.OrderColumn != null)
                {
                    Type typeModel = typeof(T);
                    PropertyInfo propInfo = typeModel.GetProperty(selectValues.OrderColumn.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        queryStr += " Order By [" + propInfo.GetTableColumnName() + "]";

                        if (selectValues.OrderBy != null)
                        {
                            string orderby = selectValues.OrderBy.ToString() == "DESC" ? " Desc" : " Asc";
                            queryStr += orderby;
                        }
                    }
                }
            }

            return queryStr;
        }

        private static string ApplySkipTake(string tableName, SqlCommand selectCmd, Select selectValues, List<Where> whereList = null, dynamic columns = null)
        {
            string order = ApplyOrder(selectValues);

            Type typeModel = typeof(T);
            List<PropertyInfo> props = typeModel.GetProperties().ToList();

            order = order == "" ? "Order By " + props.ReturnFirstColumn() : order;

            string queryStr = "With Pager As (Select Row_Number() Over (" + order + ") As 'RowNumber', ";

            queryStr += ApplyAggregate(selectValues) + ApplySelectColumns(columns);

            queryStr = queryStr.Trim().TrimEnd(',');

            queryStr += " From " + tableName + " " + ApplyWhereList(selectCmd, whereList);

            queryStr += ApplyGroupBy(selectValues);

            queryStr += ApplyHaving(selectCmd, selectValues);

            queryStr += ") Select" + ApplyDistinct(selectValues) + ApplyTop(selectValues);

            queryStr += " * From Pager ";

            queryStr += "Where RowNumber Between " + selectValues.Pager.FirstRecord + " AND " + selectValues.Pager.LastRecord;

            return queryStr;
        }

        #endregion

        #region Insert

        internal static Table<T> Insert(dynamic values, bool returnID = false)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();
            object obj = (T)values;

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string identity = props.ReturnIDColumn();

            Table<T> table = new Table<T>();
            SqlCommand insertCmd = new SqlCommand();

            string queryStr = props.ReturnQueryStringForInsert(tableName, identity, returnID);

            queryStr = queryStr.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            insertCmd.CommandText = queryStr;
            table.QueryString = queryStr;

            foreach (PropertyInfo item in props)
            {
                if (item.PropertyType.InType())
                {
                    if (identity != item.Name)
                    {
                        insertCmd.Parameters.AddWithValue("@" + item.GetTableColumnName(), ((T)values).GetType().GetProperty(item.Name)?.GetValue(obj, null) ?? DBNull.Value);
                    }
                }
            }

            table.Parameters = insertCmd.Parameters.ToParameterList();

            insertCmd.Connection = TDConnection.SqlConnection;

            try
            {
                insertCmd.Connection.Open();

                if (returnID)
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
                table.Error = new Error { Message = ex.Message, Layer = ErrorLayers.DATA };
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

        internal static Table<T> Update(dynamic values, dynamic columns = null, List<Where> whereList = null)
        {
            SqlCommand updateCmd = new SqlCommand();

            Type typeUpdateColumns = columns == null ? null : columns.GetType();

            string queryStr = ApplyParameter(updateCmd, values, columns, typeUpdateColumns);

            queryStr += ApplyWhereList(updateCmd, whereList);

            return ReturnUpdate(updateCmd, queryStr);
        }

        private static Table<T> ReturnUpdate(SqlCommand updateCmd, string queryString)
        {
            Table<T> table = new Table<T>();

            updateCmd.Connection = TDConnection.SqlConnection;
            queryString = queryString.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            updateCmd.CommandText = queryString;
            table.QueryString = queryString;
            table.Parameters = updateCmd.Parameters.ToParameterList();

            try
            {
                updateCmd.Connection.Open();
                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                table.Error = new Error { Message = ex.Message, Layer = ErrorLayers.DATA };
            }
            finally
            {
                updateCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
        }

        private static string ApplyParameter(SqlCommand updateCmd, dynamic values, dynamic columns, Type typeUpdateColumns)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();
            object obj = (T)values;

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string identity = props.ReturnIDColumn();

            string queryStr = "Update " + tableName + " Set ";

            if (columns != null)
            {
                typeUpdateColumns = columns.GetType();

                if (typeUpdateColumns.IsGenericType)
                {
                    foreach (dynamic item in columns)
                    {
                        PropertyInfo propInfo = values.GetType().GetProperty(item.ToString());

                        if (propInfo.PropertyType.InType())
                        {
                            if (identity != propInfo.Name)
                            {
                                queryStr += "[" + propInfo.GetTableColumnName() + "]=@" + propInfo.GetTableColumnName() + ",";

                                updateCmd.Parameters.AddWithValue("@" + propInfo.GetTableColumnName() + "", propInfo.GetValue(obj, null) ?? DBNull.Value);
                            }
                        }
                    }
                }
                else
                {
                    PropertyInfo propInfo = values.GetType().GetProperty(columns.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        if (identity != propInfo.Name)
                        {
                            queryStr += "[" + propInfo.GetTableColumnName() + "]=@" + propInfo.GetTableColumnName() + ",";

                            updateCmd.Parameters.AddWithValue("@" + propInfo.GetTableColumnName() + "", propInfo.GetValue(obj, null) ?? DBNull.Value);
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
                            queryStr += "[" + item.GetTableColumnName() + "]=@" + item.GetTableColumnName() + ",";
                        }
                    }
                }

                foreach (PropertyInfo item in props)
                {
                    if (item.PropertyType.InType())
                    {
                        if (identity != item.Name)
                        {
                            updateCmd.Parameters.AddWithValue("@" + item.GetTableColumnName(), ((T)values).GetType().GetProperty(item.Name)?.GetValue(obj, null) ?? DBNull.Value);
                        }
                    }
                }
            }

            queryStr = queryStr.TrimEnd(',');

            return queryStr;
        }

        #endregion

        #region Delete

        internal static Table<T> Delete(List<Where> whereList = null)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.GetDBTableName();

            SqlCommand deleteCmd = new SqlCommand();

            string queryStr = "Delete From " + tableName;

            queryStr += ApplyWhereList(deleteCmd, whereList);

            return ReturnDelete(deleteCmd, queryStr);
        }

        private static Table<T> ReturnDelete(SqlCommand deleteCmd, string queryString)
        {
            Table<T> table = new Table<T>();

            deleteCmd.Connection = TDConnection.SqlConnection;
            queryString = queryString.MakeSingle(" ").Replace("( ", "(").Replace(" )", ")");
            deleteCmd.CommandText = queryString;
            table.QueryString = queryString;
            table.Parameters = deleteCmd.Parameters.ToParameterList();

            try
            {
                deleteCmd.Connection.Open();
                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                table.Error = new Error { Message = ex.Message, Layer = ErrorLayers.DATA };
            }
            finally
            {
                deleteCmd.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
        }

        #endregion

        #region ApplyWhereList

        private static string ApplyWhereList(SqlCommand cmd, List<Where> whereList)
        {
            Type typeModel = typeof(T);

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string queryStr = "";

            if (whereList != null)
            {
                if (whereList.Count > 0)
                {
                    foreach (Where item in whereList)
                    {
                        if (props.Where(a => a.Name == item.Column.ToString()).ToList().Count > 0)
                        {
                            PropertyInfo pinfo = props.Where(a => a.Name == item.Column.ToString()).ToList().FirstOrDefault();
                            item.Column = pinfo.GetTableColumnName();
                        }
                    }

                    WhereValues cv = Where.CreateWhere(whereList);

                    queryStr = " Where " + cv.QueryString;
                    cmd.Parameters.AddRange(cv.Parameters.ToArray());
                }
            }

            return queryStr;
        }

        #endregion

        #region ApplyDistinct

        private static string ApplyDistinct(Select select = null)
        {
            if (select != null)
            {
                if (select.Distinct)
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

        #endregion
    }
}
