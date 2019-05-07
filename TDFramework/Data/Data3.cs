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
    internal sealed class Data<T1, T2, T3>
        where T1 : ITDModel
        where T2 : ITDModel
        where T3 : ITDModel
    {
        static Data()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        #region Select

        internal static Table<T1> Select(Table<T1> table1, Table<T2> table2, Table<T3> table3, List<Relation<T1, T2>> relationList1, List<Relation<T2, T3>> relationList2, List<Relation<T1, T3>> relationList3, bool asDataTable)
        {
            SqlCommand selectCmd = new SqlCommand();

            FillAlias(table1, table2, table3);

            string queryStr = "Select ";

            if (table1.SelectSettings.Pager != null)
            {
                if (!string.IsNullOrEmpty(table1.SelectSettings.Pager.FirstRecord) && !string.IsNullOrEmpty(table1.SelectSettings.Pager.LastRecord))
                {
                    queryStr = ApplySkipTake(selectCmd, table1, table2, table3, relationList1, relationList2, relationList3, 1);
                }
                else
                {
                    queryStr = CreateQueryString(table1, table2, table3, selectCmd, queryStr, relationList1, relationList2, relationList3);
                }
            }
            else if (table2.SelectSettings.Pager != null)
            {
                if (!string.IsNullOrEmpty(table2.SelectSettings.Pager.FirstRecord) && !string.IsNullOrEmpty(table2.SelectSettings.Pager.LastRecord))
                {
                    queryStr = ApplySkipTake(selectCmd, table1, table2, table3, relationList1, relationList2, relationList3, 2);
                }
                else
                {
                    queryStr = CreateQueryString(table1, table2, table3, selectCmd, queryStr, relationList1, relationList2, relationList3);
                }
            }
            else if (table3.SelectSettings.Pager != null)
            {
                if (!string.IsNullOrEmpty(table3.SelectSettings.Pager.FirstRecord) && !string.IsNullOrEmpty(table3.SelectSettings.Pager.LastRecord))
                {
                    queryStr = ApplySkipTake(selectCmd, table1, table2, table3, relationList1, relationList2, relationList3, 3);
                }
                else
                {
                    queryStr = CreateQueryString(table1, table2, table3, selectCmd, queryStr, relationList1, relationList2, relationList3);
                }
            }
            else
            {
                queryStr = CreateQueryString(table1, table2, table3, selectCmd, queryStr, relationList1, relationList2, relationList3);
            }

            return ReturnSelect(selectCmd, queryStr, asDataTable);
        }

        private static Table<T1> ReturnSelect(SqlCommand selectCmd, string queryString, bool asDataTable)
        {
            Table<T1> table = new Table<T1>();

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

                if (asDataTable == false)
                {
                    Type typeModel1 = typeof(T1);
                    string tableName1 = typeModel1.Name;

                    Type typeModel2 = typeof(T2);
                    string tableName2 = typeModel2.Name;

                    Type typeModel3 = typeof(T3);
                    string tableName3 = typeModel3.Name;

                    table.Data = ((DataTable)table.Data).ToDynamicList(tableName1 + tableName2 + tableName3);
                }

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

        private static string CreateQueryString(Table<T1> table1, Table<T2> table2, Table<T3> table3, SqlCommand selectCmd, string queryString, List<Relation<T1, T2>> relationList1, List<Relation<T2, T3>> relationList2, List<Relation<T1, T3>> relationList3, bool applyTop = true, bool applyOrder = true, bool applyDistinct = true)
        {
            string textCreater = "";

            //Distinct
            if (applyDistinct)
            {
                textCreater = FillDistinct(table1, table2, table3);
            }

            queryString += textCreater;

            textCreater = "";

            //Top
            if (applyTop)
            {
                textCreater = FillTop(table1, table2, table3);
            }

            queryString += textCreater;

            //Aggregate

            textCreater = ApplyAggregate<T1>(table1.SelectSettings, table1.Alias);

            if (textCreater.Trim() == "")
            {
                if (table2.SelectSettings.Aggregate != null)
                {
                    if (table2.SelectSettings.Aggregate.Column != null)
                    {
                        textCreater = ApplyAggregate<T2>(table2.SelectSettings, table2.Alias);
                    }
                }
            }

            queryString += textCreater;

            string selectColumns1 = ApplySelectColumns<T1>(table1.Columns, table1.Alias);

            string selectColumns2 = ApplySelectColumns<T2>(table2.Columns, table2.Alias);

            string selectColumns3 = ApplySelectColumns<T3>(table3.Columns, table3.Alias);

            if (selectColumns1 != "" && selectColumns2 == "" && selectColumns3 == "")
            {
                queryString += selectColumns1;
            }
            else if (selectColumns1 == "" && selectColumns2 != "" && selectColumns3 == "")
            {
                queryString += selectColumns2;
            }
            else if (selectColumns1 == "" && selectColumns2 == "" && selectColumns3 != "")
            {
                queryString += selectColumns3;
            }
            else if (selectColumns1 != "" && selectColumns2 != "" && selectColumns3 == "")
            {
                queryString += selectColumns1 + ", " + selectColumns2;
            }
            else if (selectColumns1 == "" && selectColumns2 != "" && selectColumns3 != "")
            {
                queryString += selectColumns2 + ", " + selectColumns3;
            }
            else if (selectColumns1 != "" && selectColumns2 == "" && selectColumns3 != "")
            {
                queryString += selectColumns1 + ", " + selectColumns3;
            }
            else if (selectColumns1 != "" && selectColumns2 != "" && selectColumns3 != "")
            {
                queryString += selectColumns1 + ", " + selectColumns2 + ", " + selectColumns3;
            }

            queryString = queryString.Trim().TrimEnd(',');

            queryString += " From " + table1.TableName + " " + table1.Alias + " ";

            int counter = 0;
            if (relationList1 != null)
            {
                foreach (Relation<T1, T2> item in relationList1)
                {
                    if (counter == 0)
                    {
                        string joinQuery = item.JoinType.ToJoiner() + " " + table2.TableName + " " + table2.Alias + " ";

                        queryString += queryString.Contains(joinQuery) ? item.QueryString(table1, table2, "And") : joinQuery + item.QueryString(table1, table2);
                    }
                    else
                    {
                        queryString += item.QueryString(table1, table2, "And");
                    }

                    counter++;
                }
            }

            counter = 0;
            if (relationList2 != null)
            {
                foreach (Relation<T2, T3> item in relationList2)
                {
                    if (counter == 0)
                    {
                        string joinQuery = item.JoinType.ToJoiner() + " " + table3.TableName + " " + table3.Alias + " ";

                        queryString += queryString.Contains(joinQuery) ? item.QueryString(table2, table3, "And") : joinQuery + item.QueryString(table2, table3);
                    }
                    else
                    {
                        queryString += item.QueryString(table2, table3, "And");
                    }

                    counter++;
                }
            }

            counter = 0;
            if (relationList3 != null)
            {
                foreach (Relation<T1, T3> item in relationList3)
                {
                    if (counter == 0)
                    {
                        string joinQuery = item.JoinType.ToJoiner() + " " + table3.TableName + " " + table3.Alias + " ";

                        queryString += queryString.Contains(joinQuery) ? item.QueryString(table1, table3, "And") : joinQuery + item.QueryString(table1, table3);
                    }
                    else
                    {
                        queryString += item.QueryString(table1, table3, "And");
                    }

                    counter++;
                }
            }

            string knot = " Where ";

            if (table1.WhereList.Count > 0 && table2.WhereList.Count <= 0 && table3.WhereList.Count <= 0)
            {
                queryString += ApplyWhereList<T1>(selectCmd, table1.WhereList, table1.Alias);
            }
            else if (table1.WhereList.Count <= 0 && table2.WhereList.Count > 0 && table3.WhereList.Count <= 0)
            {
                queryString += ApplyWhereList<T2>(selectCmd, table2.WhereList, table2.Alias);
            }
            else if (table1.WhereList.Count <= 0 && table2.WhereList.Count <= 0 && table3.WhereList.Count > 0)
            {
                queryString += ApplyWhereList<T3>(selectCmd, table3.WhereList, table3.Alias);
            }
            else if (table1.WhereList.Count > 0 && table2.WhereList.Count > 0 && table3.WhereList.Count <= 0)
            {
                queryString += ApplyWhereList<T1>(selectCmd, table1.WhereList, table1.Alias);

                knot = " " + table2.WhereList.First().Knot.ToString() + " ";
                queryString += ApplyWhereList<T2>(selectCmd, table2.WhereList, table2.Alias, knot);
            }
            else if (table1.WhereList.Count <= 0 && table2.WhereList.Count > 0 && table3.WhereList.Count > 0)
            {
                queryString += ApplyWhereList<T2>(selectCmd, table2.WhereList, table2.Alias);

                knot = " " + table3.WhereList.First().Knot.ToString() + " ";
                queryString += ApplyWhereList<T3>(selectCmd, table3.WhereList, table3.Alias, knot);
            }
            else if (table1.WhereList.Count > 0 && table2.WhereList.Count <= 0 && table3.WhereList.Count > 0)
            {
                queryString += ApplyWhereList<T1>(selectCmd, table1.WhereList, table1.Alias);

                knot = " " + table3.WhereList.First().Knot.ToString() + " ";
                queryString += ApplyWhereList<T3>(selectCmd, table3.WhereList, table3.Alias, knot);
            }
            else if (table1.WhereList.Count > 0 && table2.WhereList.Count > 0 && table3.WhereList.Count > 0)
            {
                queryString += ApplyWhereList<T1>(selectCmd, table1.WhereList, table1.Alias);

                knot = " " + table2.WhereList.First().Knot.ToString() + " ";
                queryString += ApplyWhereList<T2>(selectCmd, table2.WhereList, table2.Alias, knot);

                knot = " " + table3.WhereList.First().Knot.ToString() + " ";
                queryString += ApplyWhereList<T3>(selectCmd, table3.WhereList, table3.Alias, knot);
            }

            queryString += ApplyGroupBy<T1>(table1.SelectSettings, table1.Alias);

            queryString += ApplyGroupBy<T2>(table2.SelectSettings, table2.Alias, queryString);

            queryString += ApplyGroupBy<T3>(table3.SelectSettings, table3.Alias, queryString);

            //Having
            textCreater = "";
            textCreater += ApplyHaving<T1>(selectCmd, table1.SelectSettings, table1.Alias);

            knot = " Having ";

            if (textCreater != "")
            {
                if (table2.SelectSettings.Aggregate?.Having != null)
                {
                    if (table2.SelectSettings.Aggregate.Having.Count > 0)
                    {
                        knot = " " + table1.SelectSettings.Aggregate.Having.First().Knot.ToString() + " ";
                        textCreater += ApplyHaving<T2>(selectCmd, table2.SelectSettings, table2.Alias, knot);
                    }
                }
            }
            else
            {
                textCreater += ApplyHaving<T2>(selectCmd, table2.SelectSettings, table2.Alias, knot);
            }

            if (textCreater != "")
            {
                if (table3.SelectSettings.Aggregate?.Having != null)
                {
                    if (table3.SelectSettings.Aggregate.Having.Count > 0)
                    {
                        knot = " " + table2.SelectSettings.Aggregate.Having.First().Knot.ToString() + " ";
                        textCreater += ApplyHaving<T3>(selectCmd, table3.SelectSettings, table3.Alias, knot);
                    }
                }
            }
            else
            {
                textCreater += ApplyHaving<T3>(selectCmd, table3.SelectSettings, table3.Alias, knot);
            }

            queryString += textCreater;

            if (applyOrder)
            {
                if (table1.SelectSettings.OrderColumn != null)
                {
                    queryString += ApplyOrder<T1>(table1.SelectSettings, table1.Alias);
                }
                else if (table2.SelectSettings.OrderColumn != null)
                {
                    queryString += ApplyOrder<T2>(table2.SelectSettings, table2.Alias);
                }
                else if (table3.SelectSettings.OrderColumn != null)
                {
                    queryString += ApplyOrder<T3>(table3.SelectSettings, table3.Alias);
                }
            }

            return queryString;
        }

        private static string ApplyDistinct(Select select = null)
        {
            if (select != null && select.Distinct)
            {
                return " Distinct ";
            }
            else
            {
                return " ";
            }
        }

        private static string ApplyTop(Select select = null)
        {
            if (select?.Top != null)
            {
                return " Top " + select.Top.ToString() + " ";
            }
            else
            {
                return " ";
            }
        }

        private static string ApplyAggregate<T>(Select select, string alias)
        {
            string queryStr = "";

            if (select.Aggregate != null)
            {
                if (select.Aggregate.Column != null)
                {
                    Type typeModel = typeof(T);
                    PropertyInfo propInfo = typeModel.GetProperty(select.Aggregate.Column.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        switch (select.Aggregate.Agregate)
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

                        queryStr += "(" + alias + ".[" + propInfo.GetTableColumnName() + "]) AggColumn, ";
                    }
                }
            }

            return queryStr;
        }

        private static string ApplySelectColumns<T>(dynamic columns, string alias, bool forPager = false)
        {
            string queryStr = "";

            string aliasJoin = alias == "" ? "" : alias + "_";
            alias = alias == "" ? "" : alias + ".";

            if (columns == null)
            {
                queryStr += ReturnAllColumns<T>(alias, forPager);
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
                                if (!forPager)
                                {
                                    queryStr += alias + "[" + propInfo.GetTableColumnName() + "] as " + aliasJoin + propInfo.GetTableColumnName();
                                }
                                else
                                {
                                    queryStr += aliasJoin + propInfo.GetTableColumnName();
                                }

                                queryStr += ", ";
                            }
                        }
                    }
                    else
                    {
                        queryStr += ReturnAllColumns<T>(alias, forPager);
                    }
                }
                else
                {
                    Type typeModel = typeof(T);
                    PropertyInfo propInfo = typeModel.GetProperty(columns.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        if (!forPager)
                        {
                            queryStr += alias + "[" + propInfo.GetTableColumnName() + "] as " + aliasJoin + propInfo.GetTableColumnName() + ", ";
                        }
                        else
                        {
                            queryStr += aliasJoin + propInfo.GetTableColumnName();
                        }
                    }
                }
            }

            queryStr = queryStr.Trim().TrimEnd(',');

            return queryStr;
        }

        private static string ApplyGroupBy<T>(Select select, string alias, string queryString = "")
        {
            string queryStr = "";

            if (select.Aggregate != null)
            {
                if (select.Aggregate.GroupColumns != null)
                {
                    Type type = select.Aggregate.GroupColumns.GetType();

                    if (type.IsGenericType)
                    {
                        if (select.Aggregate.GroupColumns.Count > 0)
                        {
                            foreach (dynamic item in select.Aggregate.GroupColumns)
                            {
                                Type typeModel = typeof(T);
                                PropertyInfo propInfo = typeModel.GetProperty(item.ToString());

                                if (propInfo.PropertyType.InType())
                                {
                                    queryStr += alias + ".[" + propInfo.GetTableColumnName() + "], ";
                                }
                            }
                        }
                    }
                    else
                    {
                        Type typeModel = typeof(T);
                        PropertyInfo propInfo = typeModel.GetProperty(select.Aggregate.GroupColumns.ToString());

                        if (propInfo.PropertyType.InType())
                        {
                            queryStr += alias + ".[" + propInfo.GetTableColumnName() + "]";
                        }
                    }
                }

                queryStr = queryStr.Trim().TrimEnd(',');

                if (queryStr != "")
                {
                    queryStr = queryString.Contains(" Group By ") ? ", " + queryStr : " Group By " + queryStr;
                }
            }

            return queryStr;
        }

        private static string ApplyOrder<T>(Select select = null, string alias = null)
        {
            string queryStr = "";

            if (select != null && select.OrderColumn != null)
            {
                Type typeModel = typeof(T);
                PropertyInfo propInfo = typeModel.GetProperty(select.OrderColumn.ToString());

                if (propInfo.PropertyType.InType())
                {
                    queryStr += " Order By " + alias + ".[" + propInfo.GetTableColumnName() + "]";

                    if (select.OrderBy != null)
                    {
                        string orderby = select.OrderBy.ToString() == "DESC" ? " Desc" : " Asc";
                        queryStr += orderby;
                    }
                }
            }

            return queryStr;
        }

        private static string ApplySkipTake(SqlCommand selectCmd, Table<T1> table1, Table<T2> table2, Table<T3> table3, List<Relation<T1, T2>> relationList1, List<Relation<T2, T3>> relationList2, List<Relation<T1, T3>> relationList3, int whichTable)
        {
            //Order
            string textCreater = ApplyOrder<T1>(table1.SelectSettings, table1.Alias);

            if (textCreater.Trim() == "")
            {
                textCreater = ApplyOrder<T2>(table2.SelectSettings, table2.Alias);
            }

            if (textCreater.Trim() == "")
            {
                textCreater = ApplyOrder<T3>(table3.SelectSettings, table3.Alias);
            }

            Type typeModel1 = typeof(T1);
            List<PropertyInfo> props1 = typeModel1.GetProperties().ToList();

            textCreater = textCreater == "" ? "Order By " + table1.Alias + ".[" + props1.ReturnFirstColumn() + "]" : textCreater;

            string queryStr = "With Pager As (Select Row_Number() Over (" + textCreater + ") As 'RowNumber', ";

            queryStr = CreateQueryString(table1, table2, table3, selectCmd, queryStr, relationList1, relationList2, relationList3, false, false, false);

            queryStr += ") Select";

            //Distinct
            textCreater = ApplyDistinct(table1.SelectSettings);

            if (textCreater.Trim() == "")
            {
                textCreater = ApplyDistinct(table2.SelectSettings);
            }
            if (textCreater.Trim() == "")
            {
                textCreater = ApplyDistinct(table3.SelectSettings);
            }
            queryStr += textCreater;

            //Top
            textCreater = ApplyTop(table1.SelectSettings);
            if (textCreater.Trim() == "")
            {
                textCreater = ApplyTop(table2.SelectSettings);
            }
            if (textCreater.Trim() == "")
            {
                textCreater = ApplyTop(table3.SelectSettings);
            }
            queryStr += textCreater;

            if (table1.SelectSettings.Aggregate != null)
            {
                if (table1.SelectSettings.Aggregate.Column != null)
                {
                    queryStr += " AggColumn, ";
                }
                else if (table2.SelectSettings.Aggregate != null)
                {
                    if (table2.SelectSettings.Aggregate.Column != null)
                    {
                        queryStr += " AggColumn, ";
                    }
                    else if (table3.SelectSettings.Aggregate != null)
                    {
                        if (table3.SelectSettings.Aggregate.Column != null)
                        {
                            queryStr += " AggColumn, ";
                        }
                    }
                }
                else if (table3.SelectSettings.Aggregate != null)
                {
                    if (table3.SelectSettings.Aggregate.Column != null)
                    {
                        queryStr += " AggColumn, ";
                    }
                }
            }
            else if (table2.SelectSettings.Aggregate != null)
            {
                if (table2.SelectSettings.Aggregate.Column != null)
                {
                    queryStr += " AggColumn, ";
                }
            }
            else if (table3.SelectSettings.Aggregate != null)
            {
                if (table3.SelectSettings.Aggregate.Column != null)
                {
                    queryStr += " AggColumn, ";
                }
            }

            if (table1.Columns == null && table2.Columns == null && table3.Columns == null)
            {
                queryStr += ReturnAllColumns<T1>(table1.Alias, true);
                queryStr += ReturnAllColumns<T2>(table2.Alias, true);
                queryStr += ReturnAllColumns<T3>(table3.Alias, true);
            }
            else
            {
                if (table1.Columns != null)
                {
                    if (!Enum.TryParse(table1.Columns.ToString(), out SelectColumns _))
                    {
                        queryStr += ApplySelectColumns<T1>(table1.Columns, table1.Alias, true) + ", ";
                    }
                }
                else
                {
                    queryStr += ReturnAllColumns<T1>(table1.Alias, true);
                }

                if (table2.Columns != null)
                {
                    if (!Enum.TryParse(table2.Columns.ToString(), out SelectColumns _))
                    {
                        queryStr += ApplySelectColumns<T2>(table2.Columns, table2.Alias, true);
                    }
                }
                else
                {
                    queryStr += ReturnAllColumns<T2>(table2.Alias, true);
                }

                if (table3.Columns != null)
                {
                    if (!Enum.TryParse(table3.Columns.ToString(), out SelectColumns _))
                    {
                        queryStr += ApplySelectColumns<T3>(table3.Columns, table3.Alias, true);
                    }
                }
                else
                {
                    queryStr += ReturnAllColumns<T3>(table3.Alias, true);
                }
            }

            queryStr = queryStr.Trim().TrimEnd(',');

            queryStr += " From Pager ";

            if (whichTable == 1)
            {
                queryStr += "Where RowNumber Between " + table1.SelectSettings.Pager.FirstRecord + " AND " + table1.SelectSettings.Pager.LastRecord;
            }
            else if (whichTable == 2)
            {
                queryStr += "Where RowNumber Between " + table2.SelectSettings.Pager.FirstRecord + " AND " + table2.SelectSettings.Pager.LastRecord;
            }
            else if (whichTable == 3)
            {
                queryStr += "Where RowNumber Between " + table3.SelectSettings.Pager.FirstRecord + " AND " + table3.SelectSettings.Pager.LastRecord;
            }

            return queryStr;
        }

        private static string ApplyWhereList<T>(SqlCommand cmd, List<Where> whereList, string alias, string knot = " Where ")
        {
            Type typeModel = typeof(T);

            List<PropertyInfo> props1 = typeModel.GetProperties().ToList().ReturnValidProperties();

            string queryStr = "";

            if (whereList != null)
            {
                if (whereList.Count > 0)
                {
                    foreach (Where item in whereList)
                    {
                        if (props1.Where(a => a.Name == item.Column.ToString()).ToList().Count > 0)
                        {
                            PropertyInfo pinfo = props1.Where(a => a.Name == item.Column.ToString()).ToList().FirstOrDefault();
                            item.Column = pinfo.GetTableColumnName();
                        }
                    }

                    WhereValues cv = Where.CreateWhere(whereList, alias);

                    queryStr = knot + cv.QueryString;
                    cmd.Parameters.AddRange(cv.Parameters.ToArray());
                }
            }

            return queryStr;
        }

        private static string ApplyHaving<T>(SqlCommand cmd, Select select, string alias, string knot = " Having ")
        {
            Type typeModel = typeof(T);

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string queryStr = "";
            if (select?.Aggregate != null)
            {
                if (select.Aggregate.GroupColumns != null)
                {
                    if (select.Aggregate.Having != null)
                    {
                        if (select.Aggregate.Having.Count > 0)
                        {
                            foreach (Having item in select.Aggregate.Having)
                            {
                                if (props.Where(a => a.Name == item.Column.ToString()).ToList().Count > 0)
                                {
                                    PropertyInfo pinfo = props.Where(a => a.Name == item.Column.ToString()).ToList().FirstOrDefault();
                                    item.Column = pinfo.GetTableColumnName();
                                }
                            }

                            HavingValues cv = Having.CreateHaving(select.Aggregate.Having, alias);

                            queryStr = knot + cv.QueryString;
                            cmd.Parameters.AddRange(cv.Parameters.ToArray());
                        }
                    }
                }
            }

            return queryStr;
        }

        private static void FillAlias(Table<T1> table1, Table<T2> table2, Table<T3> table3)
        {
            table1.Alias = "A";
            table2.Alias = "B";
            table3.Alias = "C";
        }

        private static string FillDistinct(Table<T1> table1, Table<T2> table2, Table<T3> table3)
        {
            if (table1.SelectSettings.Distinct)
            {
                return ApplyDistinct(table1.SelectSettings);
            }
            else if (table2.SelectSettings.Distinct)
            {
                return ApplyDistinct(table2.SelectSettings);
            }
            else if (table3.SelectSettings.Distinct)
            {
                return ApplyDistinct(table3.SelectSettings);
            }

            return "";
        }

        private static string FillTop(Table<T1> table1, Table<T2> table2, Table<T3> table3)
        {
            if (table1.SelectSettings.Top != null)
            {
                return ApplyTop(table1.SelectSettings);
            }
            else if (table2.SelectSettings.Top != null)
            {
                return ApplyTop(table2.SelectSettings);
            }
            else if (table3.SelectSettings.Top != null)
            {
                return ApplyTop(table3.SelectSettings);
            }

            return "";
        }

        private static string ReturnAllColumns<T>(string alias, bool forPager)
        {
            string returnText = "";

            Type typeModel = typeof(T);
            List<PropertyInfo> propList = typeModel.GetProperties().ToList().ReturnValidProperties();

            foreach (PropertyInfo item in propList)
            {
                if (!forPager)
                {
                    returnText += alias + "[" + item.GetTableColumnName() + "] as " + alias.Replace(".", "_") + item.GetTableColumnName() + ", ";
                }
                else
                {
                    returnText += alias + "_" + item.GetTableColumnName() + ", ";
                }
            }

            return returnText;
        }

        #endregion
    }
}
