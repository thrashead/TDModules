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
    public sealed class TDHelper<T> where T : ITDModel
    {
        static TDHelper()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        #region Select

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(bool asDataTable = false)
        {
            return ReturnSelect(asDataTable, DataHelper<T>.Select());
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi þartlarý tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(Select select, bool asDataTable = false)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Select = select }, MethodType.Select);

            if (rbError != null) { return rbError; }

            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(null, null, select));
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="where">Veri çekerken select sql cümlesine ait Like, Between gibi þartlarý tutan Where türü nesnedir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(Where where, bool asDataTable = false)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Where = where }, MethodType.Select);

            if (rbError != null) { return rbError; }

            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(null, new List<Where>() { where }));
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi þartlarý tutan Where türü nesnedir. Birden fazla þart var ise List þeklinde þart gönderir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(List<Where> whereList, bool asDataTable = false)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { WhereList = whereList }, MethodType.Select);

            if (rbError != null) { return rbError; }

            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(null, whereList));
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="where">Veri çekerken select sql cümlesine ait Like, Between gibi þartlarý tutan Where türü nesnedir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi þartlarý tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(Select select, Where where, bool asDataTable = false)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Select = select }, MethodType.Select);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Where = where }, MethodType.Select);

            if (rbError != null) { return rbError; }

        returnPoint: ;
            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(null, new List<Where>() { where }, select));
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi þartlarý tutan Where türü nesnedir. Birden fazla þart var ise List þeklinde þart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi þartlarý tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(Select select, List<Where> whereList, bool asDataTable = false)
        {
            ResultBox rbError = new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false };

            rbError = TDError<T>.ReturnError(new TDError<T>() { Select = select }, MethodType.Select);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { WhereList = whereList }, MethodType.Select);

            if (rbError != null) { return rbError; }

        returnPoint: ;
            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(null, whereList, select));
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List þeklinde gönderilir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, bool asDataTable = false)
        {
            ResultBox rbError = new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false };

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Select);

            if (rbError != null) { return rbError; }

            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(columns));
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List þeklinde gönderilir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi þartlarý tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, Select select, bool asDataTable = false)
        {
            ResultBox rbError = new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false };

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Select);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns, Select = select }, MethodType.Select);

            if (rbError != null) { return rbError; }

        returnPoint: ;
            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(columns, null, select));
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List þeklinde gönderilir.</param>
        /// <param name="where">Veri çekerken select sql cümlesine ait Like, Between gibi þartlarý tutan Where türü nesnedir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, Where where, bool asDataTable = false)
        {
            ResultBox rbError = new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false };

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Select);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Where = where }, MethodType.Select);

            if (rbError != null) { return rbError; }

        returnPoint: ;
            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(columns, new List<Where>() { where }));
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List þeklinde gönderilir.</param>
        /// <param name="where">Veri çekerken select sql cümlesine ait Like, Between gibi þartlarý tutan Where türü nesnedir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi þartlarý tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, Select select, Where where, bool asDataTable = false)
        {
            ResultBox rbError = new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false };

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Select);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns, Select = select }, MethodType.Select);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Where = where }, MethodType.Select);

            if (rbError != null) { return rbError; }

        returnPoint: ;
            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(columns, new List<Where>() { where }, select));
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List þeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi þartlarý tutan Where türü nesnedir. Birden fazla þart var ise List þeklinde þart gönderir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, List<Where> whereList, bool asDataTable = false)
        {
            ResultBox rbError = new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false };

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Select);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { WhereList = whereList }, MethodType.Select);

            if (rbError != null) { return rbError; }

        returnPoint: ;
            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(columns, whereList));
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List þeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi þartlarý tutan Where türü nesnedir. Birden fazla þart var ise List þeklinde þart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi þartlarý tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayýlan olarak false'tur. true atanýrsa model olarak deðil DataTable olarak veri tutan ResultBox döner yoksa varsayýlan olarak List formatýnda model tutan ResultBox döner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, Select select, List<Where> whereList, bool asDataTable = false)
        {
            ResultBox rbError = new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false };

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Select);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns, Select = select }, MethodType.Select);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { WhereList = whereList }, MethodType.Select);

            if (rbError != null) { return rbError; }

        returnPoint: ;
            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(columns, whereList, select));
        }

        private static ResultBox ReturnSelect(bool asDataTable, ResultBox _rb)
        {
            if (asDataTable == false)
            {
                if (_rb != null)
                {
                    if (_rb.Result == true)
                    {
                        if (_rb.Data != null)
                        {
                            List<T> dataList = new List<T>();
                            Type type = typeof(T);

                            try
                            {
                                foreach (DataRow tableItem in _rb.Data.Rows)
                                {
                                    object obj = Activator.CreateInstance(type);

                                    foreach (PropertyInfo item in type.GetProperties().ToList().ReturnValidProperties(true))
                                    {
                                        string propTypeName = GetTypeName(item.PropertyType);
                                        string columnName = item.Name;

                                        foreach (object itemAttr in item.GetCustomAttributes(false))
                                        {
                                            string attr = itemAttr.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last();

                                            if (attr == "DBColumn")
                                            {
                                                PropertyInfo propsAttr = itemAttr.GetType().GetProperty("Name");

                                                if (propsAttr != null)
                                                {
                                                    columnName = propsAttr.GetValue(itemAttr, null).ToString();
                                                }
                                            }
                                        }

                                        if (item.PropertyType.InType())
                                        {
                                            if (tableItem.Table.Columns.Contains(columnName))
                                            {
                                                switch (propTypeName)
                                                {
                                                    case "Int16": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Int16?)null : Convert.ToInt16(tableItem[columnName].ToString()), null); break;
                                                    case "Int32": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Int32?)null : Convert.ToInt32(tableItem[columnName].ToString()), null); break;
                                                    case "Int64": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Int64?)null : Convert.ToInt64(tableItem[columnName].ToString()), null); break;
                                                    case "Decimal": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Decimal?)null : Convert.ToDecimal(tableItem[columnName].ToString()), null); break;
                                                    case "Double": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Double?)null : Convert.ToDouble(tableItem[columnName].ToString()), null); break;
                                                    case "Char": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Char?)null : Convert.ToChar(tableItem[columnName].ToString()), null); break;
                                                    case "String": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? null : tableItem[columnName].ToString(), null); break;
                                                    case "Byte": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Byte?)null : Convert.ToByte(tableItem[columnName].ToString()), null); break;
                                                    case "Boolean": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Boolean?)null : Convert.ToBoolean(tableItem[columnName].ToString()), null); break;
                                                    case "DateTime": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(tableItem[columnName].ToString()), null); break;
                                                    case "DateTimeOffset": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (DateTimeOffset?)null : Convert.ToDateTime(tableItem[columnName].ToString()), null); break;
                                                    case "TimeSpan": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(tableItem[columnName].ToString()), null); break;
                                                    case "Single": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Single?)null : Convert.ToSingle(tableItem[columnName].ToString()), null); break;
                                                    case "Object": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? null : tableItem[columnName], null); break;
                                                    case "Guid": item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Guid?)null : (Guid)tableItem[columnName], null); break;
                                                    default: item.SetValue(obj, tableItem[columnName] == DBNull.Value ? (Int16?)null : tableItem[columnName], null); break;
                                                }
                                            }
                                        }
                                    }

                                    dataList.Add((T)obj);
                                }
                            }
                            catch (Exception ex)
                            {
                                _rb.CleanResultBox();
                                _rb.Result = false;
                                _rb.ErrorMessage = ex.Message;
                                _rb.ErrorLayer = ErrorLayers.TDHELPER;
                                return _rb;
                            }

                            _rb.Data = dataList;
                        }
                    }
                }
            }

            return _rb;
        }

        private static string GetTypeName(Type _type)
        {
            var nullableType = Nullable.GetUnderlyingType(_type);

            bool isNullableType = nullableType != null;

            if (isNullableType)
                return nullableType.Name;
            else
                return _type.Name;
        }

        #endregion

        #region Insert

        /// <summary>
        /// Insert iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="values">Insert edilecek nesnenin kolon deðerlerini tutar. Model tipi þeklinde nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Insert(dynamic values, bool returnID = false)
        {
            ResultBox rbError = new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false };

            rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Insert, ErrorTypes.Values);

            if (rbError != null) { return rbError; }

            return rbError ?? DataHelper<T>.Insert(values, returnID);
        }

        #endregion

        #region Update

        /// <summary>
        /// Update iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon deðerlerini tutar. Model tipi þeklinde nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Update, ErrorTypes.Values);

            return rbError ?? DataHelper<T>.Update(values);
        }

        /// <summary>
        /// Update iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon deðerlerini tutar. Model tipi þeklinde nesnedir.</param>
        /// <param name="where">Update iþlemine ait Like, Between gibi þartlarý tutan Where türü nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, Where where)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Update, ErrorTypes.Values);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Where = where }, MethodType.Update);

            return rbError ?? DataHelper<T>.Update(values, null, new List<Where>() { where });
        }

        /// <summary>
        /// Update iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon deðerlerini tutar. Model tipi þeklinde nesnedir.</param>
        /// <param name="whereList">Update iþlemine ait Like, Between gibi þartlarý tutan Where türü nesnedir. Birden fazla þart var ise List þeklinde þart gönderir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, List<Where> whereList)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Update, ErrorTypes.Values);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { WhereList = whereList }, MethodType.Update);

            return rbError ?? DataHelper<T>.Update(values, null, whereList);
        }

        /// <summary>
        /// Update iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon deðerlerini tutar. Model tipi þeklinde nesnedir.</param>
        /// <param name="columns">Bütün kolonlarý deðilde istenilen kolonlarý güncellemek için güncellenecek kolonlar gönderilir. Birden fazla kolon çekilecekse List þeklinde gönderilir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, dynamic columns)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Update, ErrorTypes.Values);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Update);

            return rbError ?? DataHelper<T>.Update(values, columns);
        }

        /// <summary>
        /// Update iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon deðerlerini tutar. Model tipi þeklinde nesnedir.</param>
        /// <param name="columns">Bütün kolonlarý deðilde istenilen kolonlarý güncellemek için güncellenecek kolonlar gönderilir. Birden fazla kolon çekilecekse List þeklinde gönderilir.</param>
        /// <param name="where">Update iþlemine ait Like, Between gibi þartlarý tutan Where türü nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, dynamic columns, Where where)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Update, ErrorTypes.Values);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Update);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Where = where }, MethodType.Update);

            return rbError ?? DataHelper<T>.Update(values, columns, new List<Where>() { where });
        }

        /// <summary>
        /// Update iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon deðerlerini tutar. Model tipi þeklinde nesnedir.</param>
        /// <param name="columns">Bütün kolonlarý deðilde istenilen kolonlarý güncellemek için güncellenecek kolonlar gönderilir. Birden fazla kolon çekilecekse List þeklinde gönderilir.</param>
        /// <param name="whereList">Update iþlemine ait Like, Between gibi þartlarý tutan Where türü nesnedir. Birden fazla þart var ise List þeklinde þart gönderir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, dynamic columns, List<Where> whereList)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Update, ErrorTypes.Values);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Update);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { WhereList = whereList }, MethodType.Update);

            return rbError ?? DataHelper<T>.Update(values, columns, whereList);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <returns></returns>
        public static ResultBox Delete()
        {
            return DataHelper<T>.Delete();
        }

        /// <summary>
        /// Delete iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="where">Delete iþlemine ait Like, Between gibi þartlarý tutan Where türü nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Delete(Where where)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Where = where }, MethodType.Delete);

            return rbError ?? DataHelper<T>.Delete(new List<Where>() { where });
        }

        /// <summary>
        /// Delete iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="whereList">Delete iþlemine ait Like, Between gibi þartlarý tutan Where türü nesnedir. Birden fazla þart var ise List þeklinde þart gönderir.</param>
        /// <returns></returns>
        public static ResultBox Delete(List<Where> whereList)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { WhereList = whereList }, MethodType.Delete);

            return rbError ?? DataHelper<T>.Delete(whereList);
        }

        #endregion
    }

    public sealed class TDHelper<T1, T2>
        where T1 : ITDModel
        where T2 : ITDModel
    {
        static TDHelper()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        #region Select

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <param name="firstTable">Seçilecek tablodan ilkinin select ve where cümleciklerini tutar.</param>
        /// <param name="secondTable">Seçilecek tablodan ikincisinin select ve where cümleciklerini tutar.</param>
        /// <param name="joinType">Join tipi seçilir.</param>
        /// <param name="asDataTable">DataTable nesnesi döndürmek için deðeri true yapýlýr.</param>
        /// <returns></returns>
        public static ResultBox Select(Table firstTable, Table secondTable, JoinTypes joinType = JoinTypes.INNER, bool asDataTable = false)
        {
            ResultBox returnRB = TDError<T1, T2>.ReturnError<T1>(firstTable, "first");

            if (returnRB != null)
            {
                return returnRB;
            }

            returnRB = TDError<T1, T2>.ReturnError<T2>(secondTable, "second");

            if (returnRB != null)
            {
                return returnRB;
            }

            returnRB = TDError<T1, T2>.ReturnError(firstTable, secondTable);

            if (returnRB != null)
            {
                return returnRB;
            }

            return DataHelper<T1, T2>.Select(firstTable, secondTable, joinType, asDataTable);
        }

        private static string GetTypeName(Type _type)
        {
            var nullableType = Nullable.GetUnderlyingType(_type);

            bool isNullableType = nullableType != null;

            if (isNullableType)
                return nullableType.Name;
            else
                return _type.Name;
        }

        #endregion
    }

    public sealed class TDHelper
    {
        static TDHelper()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        #region SqlMethods

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Select komutu için sql cümlesi gönderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteReader(string queryString, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteReader(queryString, null, commandType);
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Select komutu için sql cümlesi gönderir.</param>
        /// <param name="sqlParameter">Sql cümlesinde tek bir parametre kullanýlýyorsa bu parametre gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteReader(string queryString, SqlParameter sqlParameter, CommandType? commandType = null)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (sqlParameter != null)
            {
                sqlParameters.Add(sqlParameter);
            }

            return SqlMethods.ExecuteReader(queryString, sqlParameters, commandType);
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Select komutu için sql cümlesi gönderir.</param>
        /// <param name="sqlParameters">Sql cümlesinde birden çok parametre kullanýlýyorsa bu parametreler gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteReader(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteReader(queryString, sqlParameters, commandType);
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// Insert, Update ve Delete iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlarý için sql cümlesi gönderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteNonQuery(string queryString, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteNonQuery(queryString, null, commandType);
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// Insert, Update ve Delete iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlarý için sql cümlesi gönderir.</param>
        /// <param name="sqlParameter">Sql cümlesinde tek bir parametre kullanýlýyorsa bu parametre gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteNonQuery(string queryString, SqlParameter sqlParameter, CommandType? commandType = null)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (sqlParameter != null)
            {
                sqlParameters.Add(sqlParameter);
            }

            return SqlMethods.ExecuteNonQuery(queryString, sqlParameters, commandType);
        }

        /// <summary>
        /// Ýstenilen þartlara uygun ResultBox döner. ResultBox'ýn Data özelliðinde veriyi saklar. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// Insert, Update ve Delete iþleminin sonucuna göre ResultBox döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlarý için sql cümlesi gönderir.</param>
        /// <param name="sqlParameters">Sql cümlesinde birden çok parametre kullanýlýyorsa bu parametreler gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteNonQuery(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteNonQuery(queryString, sqlParameters, commandType);
        }

        /// <summary>
        /// Tablo üzerinde Scalar iþlem sonuçlarý döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlarý için sql cümlesi gönderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteScalar(string queryString, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteScalar(queryString, null, commandType);
        }

        /// <summary>
        /// Tablo üzerinde Scalar iþlem sonuçlarý döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlarý için sql cümlesi gönderir.</param>
        /// <param name="sqlParameter">Sql cümlesinde tek bir parametre kullanýlýyorsa bu parametre gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteScalar(string queryString, SqlParameter sqlParameter, CommandType? commandType = null)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (sqlParameter != null)
            {
                sqlParameters.Add(sqlParameter);
            }

            return SqlMethods.ExecuteScalar(queryString, sqlParameters, commandType);
        }

        /// <summary>
        /// Tablo üzerinde Scalar iþlem sonuçlarý döner. Hata oluþtuysa Result deðeri false'tur ve ErrorMessage özelliðinde hata mesajý yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlarý için sql cümlesi gönderir.</param>
        /// <param name="sqlParameters">Sql cümlesinde birden çok parametre kullanýlýyorsa bu parametreler gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteScalar(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteScalar(queryString, sqlParameters, commandType);
        }

        #endregion
    }
}
