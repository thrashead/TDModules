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
    internal sealed class DataHelper<T1, T2>
        where T1 : ITDModel
        where T2 : ITDModel
    {
        static DataHelper()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        #region Select

        internal static ResultBox Select(Table _table1, Table _table2, JoinTypes _joinType, bool _asDataTable)
        {
            SqlCommand selectCmd = new SqlCommand();
            string querystring = "";

            Type typeModel1 = typeof(T1);
            _table1.Name = typeModel1.GetDBTableName();

            Type typeModel2 = typeof(T2);
            _table2.Name = typeModel2.GetDBTableName();

            FillAlias(_table1, _table2);

            querystring = "Select ";

            if (_table1.Select.PageInfo != null)
            {
                if (!String.IsNullOrEmpty(_table1.Select.PageInfo.FirstRecord) && !String.IsNullOrEmpty(_table1.Select.PageInfo.LastRecord))
                {
                    querystring = ApplySkipTake(selectCmd, _table1, _table2, _joinType, true);
                }
                else
                {
                    querystring = CreateQueryString(_table1, _table2, selectCmd, querystring, _joinType);
                }
            }
            else if (_table2.Select.PageInfo != null)
            {
                if (!String.IsNullOrEmpty(_table2.Select.PageInfo.FirstRecord) && !String.IsNullOrEmpty(_table2.Select.PageInfo.LastRecord))
                {
                    querystring = ApplySkipTake(selectCmd, _table1, _table2, _joinType, false);
                }
                else
                {
                    querystring = CreateQueryString(_table1, _table2, selectCmd, querystring, _joinType);
                }
            }
            else
            {
                querystring = CreateQueryString(_table1, _table2, selectCmd, querystring, _joinType);
            }

            return ReturnSelect(selectCmd, querystring, _asDataTable);
        }

        private static ResultBox ReturnSelect(SqlCommand _selectCmd, string _querystring, bool _asDataTable)
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

                if (_asDataTable == false)
                {
                    Type typeModel1 = typeof(T1);
                    string tableName1 = typeModel1.Name;

                    Type typeModel2 = typeof(T2);
                    string tableName2 = typeModel2.Name;

                    rb.Data = ((DataTable)rb.Data).ToDynamicList(tableName1 + tableName2);
                }

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

        private static string CreateQueryString(Table _table1, Table _table2, SqlCommand _selectCmd, string _querystring, JoinTypes _joinType, bool _applyTop = true, bool _applyOrder = true, bool _applyDistinct = true)
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

            textCreater = ApplyAggregate<T1>(_table1.Select, _table1.Alias);

            if (textCreater.Trim() == "")
            {
                if (_table2.Select.Aggregate != null)
                {
                    if (_table2.Select.Aggregate.Column != null)
                    {
                        textCreater = ApplyAggregate<T2>(_table2.Select, _table2.Alias);
                    }
                }
            }

            _querystring += textCreater;

            string selectColumns1 = "";
            string selectColumns2 = "";

            selectColumns1 = ApplySelectColumns<T1>(_table1.SelectColumns, _table1.Select, _table1.Alias);

            selectColumns2 = ApplySelectColumns<T2>(_table2.SelectColumns, _table2.Select, _table2.Alias);

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

            _querystring += " From " + _table1.Name + " " + _table1.Alias + " " + _joinType.ToJoiner() + " " + _table2.Name + " " + _table2.Alias;

            if (_joinType != JoinTypes.CROSS)
            {
                _querystring += " On " + _table1.Alias + ".[" + ExtMethods.ReturnRealColumnName<T1>(_table1.RelatedColumn) + "] = " + _table2.Alias + ".[" + ExtMethods.ReturnRealColumnName<T2>(_table2.RelatedColumn) + "]";
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

            _querystring += ApplyGroupBy<T1>(_table1.Select, _table1.Alias);

            _querystring += ApplyGroupBy<T2>(_table2.Select, _table2.Alias, _querystring);

            //Having
            textCreater = "";
            textCreater += ApplyHaving<T1>(_selectCmd, _table1.Select, _table1.Alias);

            knot = " Having ";

            if (textCreater != "")
            {
                if (_table2.Select.Aggregate != null)
                {
                    if (_table2.Select.Aggregate.Having != null)
                    {
                        if (_table2.Select.Aggregate.Having.Count > 0)
                        {
                            knot = " " + _table1.Select.Aggregate.Having.First().Knot.ToString() + " ";
                            textCreater += ApplyHaving<T2>(_selectCmd, _table2.Select, _table2.Alias, knot);
                        }
                    }
                }
            }
            else
            {
                textCreater += ApplyHaving<T2>(_selectCmd, _table2.Select, _table2.Alias, knot);
            }

            _querystring += textCreater;

            if (_applyOrder == true)
            {
                if (_table1.Select.OrderColumn != null)
                {
                    _querystring += ApplyOrder<T1>(_table1.Select, _table1.Alias);
                }
                else if (_table2.Select.OrderColumn != null)
                {
                    _querystring += ApplyOrder<T2>(_table2.Select, _table2.Alias);
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

                        querystring += "(" + _alias + ".[" + propInfo.GetDBColumnName() + "]) AggColumn, ";
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
                                    querystring += _alias + "[" + propInfo.GetDBColumnName() + "] as " + _aliasJoin + propInfo.GetDBColumnName();
                                }
                                else
                                {
                                    querystring += _aliasJoin + propInfo.GetDBColumnName();
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
                            querystring += _alias + "[" + propInfo.GetDBColumnName() + "] as " + _aliasJoin + propInfo.GetDBColumnName() + ", ";
                        }
                        else
                        {
                            querystring += _aliasJoin + propInfo.GetDBColumnName();
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
                                    querystring += _alias + ".[" + propInfo.GetDBColumnName() + "], ";
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
                            querystring += _alias + ".[" + propInfo.GetDBColumnName() + "]";
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
                    querystring += " Order By " + _alias + ".[" + propInfo.GetDBColumnName() + "]";

                    if (_select.OrderBy != null)
                    {
                        string orderby = _select.OrderBy.ToString() == "DESC" ? " Desc" : " Asc";
                        querystring += orderby;
                    }
                }
            }

            return querystring;
        }

        private static string ApplySkipTake(SqlCommand _selectCmd, Table _table1, Table _table2, JoinTypes _joinType, bool _isFirstTable)
        {
            //Order
            string textCreater = "";
            textCreater = ApplyOrder<T1>(_table1.Select, _table1.Alias);
            if (textCreater.Trim() == "")
            {
                textCreater = ApplyOrder<T2>(_table2.Select, _table2.Alias);
            }

            Type typeModel1 = typeof(T1);
            List<PropertyInfo> props1 = typeModel1.GetProperties().ToList();

            textCreater = textCreater == "" ? "Order By " + _table1.Alias + ".[" + props1.ReturnFirstColumn() + "]" : textCreater;

            string querystring = "With Pager As (Select Row_Number() Over (" + textCreater + ") As 'RowNumber', ";

            querystring = CreateQueryString(_table1, _table2, _selectCmd, querystring, _joinType, false, false, false);

            querystring += ") Select";

            //Distinct
            textCreater = "";
            textCreater = ApplyDistinct(_table1.Select);
            if (textCreater.Trim() == "")
            {
                textCreater = ApplyDistinct(_table2.Select);
            }
            querystring += textCreater;

            //Top
            textCreater = "";
            textCreater = ApplyTop(_table1.Select);
            if (textCreater.Trim() == "")
            {
                textCreater = ApplyTop(_table2.Select);
            }
            querystring += textCreater;

            if (_table1.Select.Aggregate != null)
            {
                if (_table1.Select.Aggregate.Column != null)
                {
                    querystring += " AggColumn, ";
                }
                else if (_table2.Select.Aggregate != null)
                {
                    if (_table2.Select.Aggregate.Column != null)
                    {
                        querystring += " AggColumn, ";
                    }
                }
            }
            else if (_table2.Select.Aggregate != null)
            {
                if (_table2.Select.Aggregate.Column != null)
                {
                    querystring += " AggColumn, ";
                }
            }

            SelectColumns selectColumns;

            if (_table1.SelectColumns == null && _table2.SelectColumns == null)
            {
                querystring += ReturnAllColumns<T1>(_table1.Alias, true);
                querystring += ReturnAllColumns<T2>(_table2.Alias, true);
            }
            else
            {
                if (_table1.SelectColumns != null)
                {
                    if (!Enum.TryParse(_table1.SelectColumns.ToString(), out selectColumns))
                    {
                        querystring += ApplySelectColumns<T1>(_table1.SelectColumns, _table1.Select, _table1.Alias, true) + ", ";
                    }
                }
                else
                {
                    querystring += ReturnAllColumns<T1>(_table1.Alias, true);
                }

                if (_table2.SelectColumns != null)
                {
                    if (!Enum.TryParse(_table2.SelectColumns.ToString(), out selectColumns))
                    {
                        querystring += ApplySelectColumns<T2>(_table2.SelectColumns, _table2.Select, _table2.Alias, true);
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
                querystring += "Where RowNumber Between " + _table1.Select.PageInfo.FirstRecord + " AND " + _table1.Select.PageInfo.LastRecord;
            }
            else
            {
                querystring += "Where RowNumber Between " + _table2.Select.PageInfo.FirstRecord + " AND " + _table2.Select.PageInfo.LastRecord;
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
                        item.Column = pinfo.GetDBColumnName();
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
                                    item.Column = pinfo.GetDBColumnName();
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

        private static void FillAlias(Table _table1, Table _table2)
        {
            if (String.IsNullOrEmpty(_table1.Alias))
            {
                _table1.Alias = "A";
            }

            if (String.IsNullOrEmpty(_table2.Alias))
            {
                if (_table1.Alias != "B")
                {
                    _table2.Alias = "B";
                }
                else
                {
                    _table2.Alias = "C";
                }
            }
        }

        private static string FillDistinct(Table _table1, Table _table2)
        {
            if (_table1.Select.Distinct != false)
            {
                return ApplyDistinct(_table1.Select);
            }
            else if (_table2.Select.Distinct != false)
            {
                return ApplyDistinct(_table2.Select);
            }

            return "";
        }

        private static string FillTop(Table _table1, Table _table2)
        {
            if (_table1.Select.Top != null)
            {
                return ApplyTop(_table1.Select);
            }
            else if (_table2.Select.Top != null)
            {
                return ApplyTop(_table2.Select);
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
                    returnText += _alias + "[" + item.GetDBColumnName() + "] as " + _alias.Replace(".", "_") + item.GetDBColumnName() + ", ";
                }
                else
                {
                    returnText += _alias + "_" + item.GetDBColumnName() + ", ";
                }
            }

            return returnText;
        }

        #endregion
    }
}
