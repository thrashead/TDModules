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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(bool asDataTable = false)
        {
            return ReturnSelect(asDataTable, DataHelper<T>.Select());
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="select">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan Select t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(Select select, bool asDataTable = false)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Select = select }, MethodType.Select);

            if (rbError != null) { return rbError; }

            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(null, null, select));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="where">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(Where where, bool asDataTable = false)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Where = where }, MethodType.Select);

            if (rbError != null) { return rbError; }

            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(null, new List<Where>() { where }));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="whereList">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(List<Where> whereList, bool asDataTable = false)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { WhereList = whereList }, MethodType.Select);

            if (rbError != null) { return rbError; }

            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(null, whereList));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="where">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir.</param>
        /// <param name="select">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan Select t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="whereList">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <param name="select">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan Select t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, bool asDataTable = false)
        {
            ResultBox rbError = new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false };

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Select);

            if (rbError != null) { return rbError; }

            return rbError ?? ReturnSelect(asDataTable, DataHelper<T>.Select(columns));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="select">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan Select t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="where">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="where">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir.</param>
        /// <param name="select">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan Select t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="whereList">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="whereList">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <param name="select">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan Select t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
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
        /// Insert i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Insert edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
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
        /// Update i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Update, ErrorTypes.Values);

            return rbError ?? DataHelper<T>.Update(values);
        }

        /// <summary>
        /// Update i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
        /// <param name="where">Update i�lemine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, Where where)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Update, ErrorTypes.Values);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Where = where }, MethodType.Update);

            return rbError ?? DataHelper<T>.Update(values, null, new List<Where>() { where });
        }

        /// <summary>
        /// Update i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
        /// <param name="whereList">Update i�lemine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, List<Where> whereList)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Update, ErrorTypes.Values);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { WhereList = whereList }, MethodType.Update);

            return rbError ?? DataHelper<T>.Update(values, null, whereList);
        }

        /// <summary>
        /// Update i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
        /// <param name="columns">B�t�n kolonlar� de�ilde istenilen kolonlar� g�ncellemek i�in g�ncellenecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, dynamic columns)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Values = values }, MethodType.Update, ErrorTypes.Values);
            if (rbError != null) { return rbError; }

            rbError = TDError<T>.ReturnError(new TDError<T>() { Columns = columns }, MethodType.Update);

            return rbError ?? DataHelper<T>.Update(values, columns);
        }

        /// <summary>
        /// Update i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
        /// <param name="columns">B�t�n kolonlar� de�ilde istenilen kolonlar� g�ncellemek i�in g�ncellenecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="where">Update i�lemine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir.</param>
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
        /// Update i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
        /// <param name="columns">B�t�n kolonlar� de�ilde istenilen kolonlar� g�ncellemek i�in g�ncellenecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="whereList">Update i�lemine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
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
        /// Delete i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <returns></returns>
        public static ResultBox Delete()
        {
            return DataHelper<T>.Delete();
        }

        /// <summary>
        /// Delete i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="where">Delete i�lemine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Delete(Where where)
        {
            ResultBox rbError = TDError<T>.ReturnError(new TDError<T>() { Where = where }, MethodType.Delete);

            return rbError ?? DataHelper<T>.Delete(new List<Where>() { where });
        }

        /// <summary>
        /// Delete i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="whereList">Delete i�lemine ait Like, Between gibi �artlar� tutan Where t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <param name="firstTable">Se�ilecek tablodan ilkinin select ve where c�mleciklerini tutar.</param>
        /// <param name="secondTable">Se�ilecek tablodan ikincisinin select ve where c�mleciklerini tutar.</param>
        /// <param name="joinType">Join tipi se�ilir.</param>
        /// <param name="asDataTable">DataTable nesnesi d�nd�rmek i�in de�eri true yap�l�r.</param>
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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="queryString">Select komutu i�in sql c�mlesi g�nderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure m� yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteReader(string queryString, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteReader(queryString, null, commandType);
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="queryString">Select komutu i�in sql c�mlesi g�nderir.</param>
        /// <param name="sqlParameter">Sql c�mlesinde tek bir parametre kullan�l�yorsa bu parametre g�nderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure m� yoksa TableDirect mi belirtilir.</param>
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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="queryString">Select komutu i�in sql c�mlesi g�nderir.</param>
        /// <param name="sqlParameters">Sql c�mlesinde birden �ok parametre kullan�l�yorsa bu parametreler g�nderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure m� yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteReader(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteReader(queryString, sqlParameters, commandType);
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// Insert, Update ve Delete i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlar� i�in sql c�mlesi g�nderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure m� yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteNonQuery(string queryString, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteNonQuery(queryString, null, commandType);
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// Insert, Update ve Delete i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlar� i�in sql c�mlesi g�nderir.</param>
        /// <param name="sqlParameter">Sql c�mlesinde tek bir parametre kullan�l�yorsa bu parametre g�nderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure m� yoksa TableDirect mi belirtilir.</param>
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
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// Insert, Update ve Delete i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlar� i�in sql c�mlesi g�nderir.</param>
        /// <param name="sqlParameters">Sql c�mlesinde birden �ok parametre kullan�l�yorsa bu parametreler g�nderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure m� yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteNonQuery(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteNonQuery(queryString, sqlParameters, commandType);
        }

        /// <summary>
        /// Tablo �zerinde Scalar i�lem sonu�lar� d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlar� i�in sql c�mlesi g�nderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure m� yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteScalar(string queryString, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteScalar(queryString, null, commandType);
        }

        /// <summary>
        /// Tablo �zerinde Scalar i�lem sonu�lar� d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlar� i�in sql c�mlesi g�nderir.</param>
        /// <param name="sqlParameter">Sql c�mlesinde tek bir parametre kullan�l�yorsa bu parametre g�nderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure m� yoksa TableDirect mi belirtilir.</param>
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
        /// Tablo �zerinde Scalar i�lem sonu�lar� d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutlar� i�in sql c�mlesi g�nderir.</param>
        /// <param name="sqlParameters">Sql c�mlesinde birden �ok parametre kullan�l�yorsa bu parametreler g�nderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure m� yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public static ResultBox ExecuteScalar(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            return SqlMethods.ExecuteScalar(queryString, sqlParameters, commandType);
        }

        #endregion
    }
}
