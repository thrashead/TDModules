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
    internal sealed class Data<T1, T2>
        where T1 : ITDModel
        where T2 : ITDModel
    {
        static Data()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        #region Select

        internal static Table<T1> Select(Table<T1> _table1, Table<T2> _table2, List<Relation<T1, T2>> _relationList, bool _asDataTable)
        {
            SqlCommand selectCmd = new SqlCommand();
            string querystring = "";

            FillAlias(_table1, _table2);

            querystring = "Select ";

            if (_table1.SelectSettings.Pager != null)
            {
                if (!String.IsNullOrEmpty(_table1.SelectSettings.Pager.FirstRecord) && !String.IsNullOrEmpty(_table1.SelectSettings.Pager.LastRecord))
                {
                    querystring = ApplySkipTake(selectCmd, _table1, _table2, _relationList, true);
                }
                else
                {
                    querystring = CreateQueryString(_table1, _table2, selectCmd, querystring, _relationList);
                }
            }
            else if (_table2.SelectSettings.Pager != null)
            {
                if (!String.IsNullOrEmpty(_table2.SelectSettings.Pager.FirstRecord) && !String.IsNullOrEmpty(_table2.SelectSettings.Pager.LastRecord))
                {
                    querystring = ApplySkipTake(selectCmd, _table1, _table2, _relationList, false);
                }
                else
                {
                    querystring = CreateQueryString(_table1, _table2, selectCmd, querystring, _relationList);
                }
            }
            else
            {
                querystring = CreateQueryString(_table1, _table2, selectCmd, querystring, _relationList);
            }

            return ReturnSelect(selectCmd, querystring, _asDataTable);
        }

        private static Table<T1> ReturnSelect(SqlCommand _selectCmd, string _querystring, bool _asDataTable)
        {
            Table<T1> table = new Table<T1>();
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

                if (_asDataTable == false)
                {
                    Type typeModel1 = typeof(T1);
                    string tableName1 = typeModel1.Name;

                    Type typeModel2 = typeof(T2);
                    string tableName2 = typeModel2.Name;

                    table.Data = ((DataTable)table.Data).ToDynamicList(tableName1 + tableName2);
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
                dataAdap.SelectCommand.Connection.Close();
                TDConnection.ConnectionStringForOnce = null;
            }

            return table;
        }

        private static string CreateQueryString(Table<T1> _table1, Table<T2> _table2, SqlCommand _selectCmd, string _querystring, List<Relation<T1, T2>> _relationList, bool _applyTop = true, bool _applyOrder = true, bool _applyDistinct = true)
        {
            string textCreater = "";

            //Distinct
            if (_applyDistinct == true)
            {
                textCreater = FillDistinct(_table1, _table2);
            }

            _querystring += textCreater;

            textCreater = "";

            //Top
            if (_applyTop == true)
            {
                textCreater = FillTop(_table1, _table2);
            }

            _querystring += textCreater;

            //Aggregate
            textCreater = "";

            textCreater = ApplyAggregate<T1>(_table1.SelectSettings, _table1.Alias);

            if (textCreater.Trim() == "")
            {
                if (_table2.SelectSettings.Aggregate != null)
                {
                    if (_table2.SelectSettings.Aggregate.Column != null)
                    {
                        textCreater = ApplyAggregate<T2>(_table2.SelectSettings, _table2.Alias);
                    }
                }
            }

            _querystring += textCreater;

            string selectColumns1 = "";
            string selectColumns2 = "";

            selectColumns1 = ApplySelectColumns<T1>(_table1.Columns, _table1.SelectSettings, _table1.Alias);

            selectColumns2 = ApplySelectColumns<T2>(_table2.Columns, _table2.SelectSettings, _table2.Alias);

            if (selectColumns1 != "" && selectColumns2 == "")
            {
                _querystring += selectColumns1;
            }
            else if (selectColumns1 == "" && selectColumns2 != "")
            {
                _querystring += selectColumns2;
            }
            else if (selectColumns1 != "" && selectColumns2 != "")
            {
                _querystring += selectColumns1 + ", " + selectColumns2;
            }

            _querystring = _querystring.Trim().TrimEnd(',');

            _querystring += " From " + _table1.TableName + " " + _table1.Alias + " ";

            int counter = 0;
            if (_relationList != null)
            {
                foreach (Relation<T1, T2> item in _relationList)
                {
                    if (counter == 0)
                    {
                        string joinQuery = item.JoinType.ToJoiner() + " " + _table2.TableName + " " + _table2.Alias + " ";

                        _querystring += _querystring.Contains(joinQuery) ? item.QueryString(_table1, _table2, "And") : joinQuery + item.QueryString(_table1, _table2);
                    }
                    else
                    {
                        _querystring += item.QueryString(_table1, _table2, "And");
                    }

                    counter++;
                }
            }

            string knot = " Where ";

            _querystring += ApplyWhereList<T1>(_selectCmd, _table1.WhereList, _table1.Alias);

            if (_table1.WhereList.Count > 0 && _table2.WhereList.Count > 0)
            {
                knot = " " + _table2.WhereList.First().Knot.ToString() + " ";
                _querystring += ApplyWhereList<T2>(_selectCmd, _table2.WhereList, _table2.Alias, knot);
            }
            else if (_table1.WhereList.Count <= 0 && _table2.WhereList.Count > 0)
            {
                _querystring += ApplyWhereList<T2>(_selectCmd, _table2.WhereList, _table2.Alias, knot);
            }

            _querystring += ApplyGroupBy<T1>(_table1.SelectSettings, _table1.Alias);

            _querystring += ApplyGroupBy<T2>(_table2.SelectSettings, _table2.Alias, _querystring);

            //Having
            textCreater = "";
            textCreater += ApplyHaving<T1>(_selectCmd, _table1.SelectSettings, _table1.Alias);

            knot = " Having ";

            if (textCreater != "")
            {
                if (_table2.SelectSettings.Aggregate != null)
                {
                    if (_table2.SelectSettings.Aggregate.Having != null)
                    {
                        if (_table2.SelectSettings.Aggregate.Having.Count > 0)
                        {
                            knot = " " + _table1.SelectSettings.Aggregate.Having.First().Knot.ToString() + " ";
                            textCreater += ApplyHaving<T2>(_selectCmd, _table2.SelectSettings, _table2.Alias, knot);
                        }
                    }
                }
            }
            else
            {
                textCreater += ApplyHaving<T2>(_selectCmd, _table2.SelectSettings, _table2.Alias, knot);
            }

            _querystring += textCreater;

            if (_applyOrder == true)
            {
                if (_table1.SelectSettings.OrderColumn != null)
                {
                    _querystring += ApplyOrder<T1>(_table1.SelectSettings, _table1.Alias);
                }
                else if (_table2.SelectSettings.OrderColumn != null)
                {
                    _querystring += ApplyOrder<T2>(_table2.SelectSettings, _table2.Alias);
                }
            }

            return _querystring;
        }

        private static string ApplyDistinct(Select _select = null)
        {
            if (_select.Distinct != false)
            {
                return " Distinct ";
            }
            else
            {
                return " ";
            }
        }

        private static string ApplyTop(Select _select = null)
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

        private static string ApplyAggregate<T>(Select _select, string _alias)
        {
            string querystring = "";

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

                        querystring += "(" + _alias + ".[" + propInfo.GetTableColumnName() + "]) AggColumn, ";
                    }
                }
            }

            return querystring;
        }

        private static string ApplySelectColumns<T>(dynamic _columns, Select _select, string _alias, bool forPager = false)
        {
            string querystring = "";
            SelectColumns selectColumns;

            string _aliasJoin = _alias == "" ? "" : _alias + "_";
            _alias = _alias == "" ? "" : _alias + ".";

            if (_columns == null)
            {
                querystring += ReturnAllColumns<T>(_alias, forPager);
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
                                if (!forPager)
                                {
                                    querystring += _alias + "[" + propInfo.GetTableColumnName() + "] as " + _aliasJoin + propInfo.GetTableColumnName();
                                }
                                else
                                {
                                    querystring += _aliasJoin + propInfo.GetTableColumnName();
                                }

                                querystring += ", ";
                            }
                        }
                    }
                    else
                    {
                        querystring += ReturnAllColumns<T>(_alias, forPager); ;
                    }
                }
                else
                {
                    Type typeModel = typeof(T);
                    PropertyInfo propInfo = typeModel.GetProperty(_columns.ToString());

                    if (propInfo.PropertyType.InType())
                    {
                        if (!forPager)
                        {
                            querystring += _alias + "[" + propInfo.GetTableColumnName() + "] as " + _aliasJoin + propInfo.GetTableColumnName() + ", ";
                        }
                        else
                        {
                            querystring += _aliasJoin + propInfo.GetTableColumnName();
                        }
                    }
                }
            }

            querystring = querystring.Trim().TrimEnd(',');

            return querystring;
        }

        private static string ApplyGroupBy<T>(Select _select, string _alias, string _querystring = "")
        {
            string querystring = "";

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
                                    querystring += _alias + ".[" + propInfo.GetTableColumnName() + "], ";
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
                            querystring += _alias + ".[" + propInfo.GetTableColumnName() + "]";
                        }
                    }
                }

                querystring = querystring.Trim().TrimEnd(',');

                if (querystring != "")
                {
                    querystring = _querystring.Contains(" Group By ") ? ", " + querystring : " Group By " + querystring;
                }
            }

            return querystring;
        }

        private static string ApplyOrder<T>(Select _select = null, string _alias = null)
        {
            string querystring = "";

            if (_select.OrderColumn != null)
            {
                Type typeModel = typeof(T);
                PropertyInfo propInfo = typeModel.GetProperty(_select.OrderColumn.ToString());

                if (propInfo.PropertyType.InType())
                {
                    querystring += " Order By " + _alias + ".[" + propInfo.GetTableColumnName() + "]";

                    if (_select.OrderBy != null)
                    {
                        string orderby = _select.OrderBy.ToString() == "DESC" ? " Desc" : " Asc";
                        querystring += orderby;
                    }
                }
            }

            return querystring;
        }

        private static string ApplySkipTake(SqlCommand _selectCmd, Table<T1> _table1, Table<T2> _table2, List<Relation<T1, T2>> _relationList, bool _isFirstTable)
        {
            //Order
            string textCreater = "";
            textCreater = ApplyOrder<T1>(_table1.SelectSettings, _table1.Alias);
            if (textCreater.Trim() == "")
            {
                textCreater = ApplyOrder<T2>(_table2.SelectSettings, _table2.Alias);
            }

            Type typeModel1 = typeof(T1);
            List<PropertyInfo> props1 = typeModel1.GetProperties().ToList();

            textCreater = textCreater == "" ? "Order By " + _table1.Alias + ".[" + props1.ReturnFirstColumn() + "]" : textCreater;

            string querystring = "With Pager As (Select Row_Number() Over (" + textCreater + ") As 'RowNumber', ";

            querystring = CreateQueryString(_table1, _table2, _selectCmd, querystring, _relationList, false, false, false);

            querystring += ") Select";

            //Distinct
            textCreater = "";
            textCreater = ApplyDistinct(_table1.SelectSettings);
            if (textCreater.Trim() == "")
            {
                textCreater = ApplyDistinct(_table2.SelectSettings);
            }
            querystring += textCreater;

            //Top
            textCreater = "";
            textCreater = ApplyTop(_table1.SelectSettings);
            if (textCreater.Trim() == "")
            {
                textCreater = ApplyTop(_table2.SelectSettings);
            }
            querystring += textCreater;

            if (_table1.SelectSettings.Aggregate != null)
            {
                if (_table1.SelectSettings.Aggregate.Column != null)
                {
                    querystring += " AggColumn, ";
                }
                else if (_table2.SelectSettings.Aggregate != null)
                {
                    if (_table2.SelectSettings.Aggregate.Column != null)
                    {
                        querystring += " AggColumn, ";
                    }
                }
            }
            else if (_table2.SelectSettings.Aggregate != null)
            {
                if (_table2.SelectSettings.Aggregate.Column != null)
                {
                    querystring += " AggColumn, ";
                }
            }

            SelectColumns selectColumns;

            if (_table1.Columns == null && _table2.Columns == null)
            {
                querystring += ReturnAllColumns<T1>(_table1.Alias, true);
                querystring += ReturnAllColumns<T2>(_table2.Alias, true);
            }
            else
            {
                if (_table1.Columns != null)
                {
                    if (!Enum.TryParse(_table1.Columns.ToString(), out selectColumns))
                    {
                        querystring += ApplySelectColumns<T1>(_table1.Columns, _table1.SelectSettings, _table1.Alias, true) + ", ";
                    }
                }
                else
                {
                    querystring += ReturnAllColumns<T1>(_table1.Alias, true);
                }

                if (_table2.Columns != null)
                {
                    if (!Enum.TryParse(_table2.Columns.ToString(), out selectColumns))
                    {
                        querystring += ApplySelectColumns<T2>(_table2.Columns, _table2.SelectSettings, _table2.Alias, true);
                    }
                }
                else
                {
                    querystring += ReturnAllColumns<T2>(_table2.Alias, true);
                }
            }

            querystring = querystring.Trim().TrimEnd(',');

            querystring += " From Pager ";

            if (_isFirstTable)
            {
                querystring += "Where RowNumber Between " + _table1.SelectSettings.Pager.FirstRecord + " AND " + _table1.SelectSettings.Pager.LastRecord;
            }
            else
            {
                querystring += "Where RowNumber Between " + _table2.SelectSettings.Pager.FirstRecord + " AND " + _table2.SelectSettings.Pager.LastRecord;
            }

            return querystring;
        }

        private static string ApplyWhereList<T>(SqlCommand _cmd, List<Where> _whereList, string _alias, string _knot = " Where ")
        {
            Type typeModel = typeof(T);

            List<PropertyInfo> props1 = typeModel.GetProperties().ToList().ReturnValidProperties();

            string querystring = "";

            if (_whereList.Count > 0)
            {
                foreach (Where item in _whereList)
                {
                    if (props1.Where(a => a.Name == item.Column.ToString()).ToList().Count > 0)
                    {
                        PropertyInfo pinfo = props1.Where(a => a.Name == item.Column.ToString()).ToList().FirstOrDefault();
                        item.Column = pinfo.GetTableColumnName();
                    }
                }

                WhereValues cv = Where.CreateWhere(_whereList, _alias);

                querystring = _knot + cv.QueryString;
                _cmd.Parameters.AddRange(cv.Parameters.ToArray());
            }

            return querystring;
        }

        private static string ApplyHaving<T>(SqlCommand _cmd, Select _select, string _alias, string _knot = " Having ")
        {
            Type typeModel = typeof(T);

            List<PropertyInfo> props = typeModel.GetProperties().ToList().ReturnValidProperties();

            string querystring = "";
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
                                    item.Column = pinfo.GetTableColumnName();
                                }
                            }

                            HavingValues cv = Having.CreateHaving(_select.Aggregate.Having, _alias);

                            querystring = _knot + cv.QueryString;
                            _cmd.Parameters.AddRange(cv.Parameters.ToArray());
                        }
                    }
                }
            }

            return querystring;
        }

        private static void FillAlias(Table<T1> _table1, Table<T2> _table2)
        {
            _table1.Alias = "A";
            _table2.Alias = "B";
        }

        private static string FillDistinct(Table<T1> _table1, Table<T2> _table2)
        {
            if (_table1.SelectSettings.Distinct != false)
            {
                return ApplyDistinct(_table1.SelectSettings);
            }
            else if (_table2.SelectSettings.Distinct != false)
            {
                return ApplyDistinct(_table2.SelectSettings);
            }

            return "";
        }

        private static string FillTop(Table<T1> _table1, Table<T2> _table2)
        {
            if (_table1.SelectSettings.Top != null)
            {
                return ApplyTop(_table1.SelectSettings);
            }
            else if (_table2.SelectSettings.Top != null)
            {
                return ApplyTop(_table2.SelectSettings);
            }

            return "";
        }

        private static string ReturnAllColumns<T>(string _alias, bool _forPager)
        {
            string returnText = "";

            Type typeModel = typeof(T);
            List<PropertyInfo> propList = typeModel.GetProperties().ToList().ReturnValidProperties();

            foreach (PropertyInfo item in propList)
            {
                if (!_forPager)
                {
                    returnText += _alias + "[" + item.GetTableColumnName() + "] as " + _alias.Replace(".", "_") + item.GetTableColumnName() + ", ";
                }
                else
                {
                    returnText += _alias + "_" + item.GetTableColumnName() + ", ";
                }
            }

            return returnText;
        }

        #endregion
    }
}
