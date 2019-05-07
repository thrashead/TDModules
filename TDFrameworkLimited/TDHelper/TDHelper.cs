// ================================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v2.2.0.2
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 22.02.2016
// SPECIAL NOTES    : G�zel Program
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
    public sealed class TDHelper<T> where T : ITDModel
    {
        #region Select

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(bool asDataTable = false)
        {
            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="selectClause">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan SelectCondition t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(SelectClause selectClause, bool asDataTable = false)
        {
            if (!((object)selectClause.OrderColumn).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in selectCondition object is an invalid enum value.",
                    Result = false
                };
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, null, null, selectClause));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="whereClause">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(WhereClause whereClause, bool asDataTable = false)
        {
            if (!((object)whereClause.Column).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in sqlCondition object is an invalid enum value.",
                    Result = false
                };
            }

            List<WhereClause> conditions = new List<WhereClause>();

            if (whereClause.Column != null)
            {
                conditions.Add(whereClause);
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, null, conditions));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="whereClause">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir.</param>
        /// <param name="selectClause">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan SelectCondition t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(SelectClause selectClause, WhereClause whereClause, bool asDataTable = false)
        {
            if (!((object)selectClause.OrderColumn).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in selectCondition object is an invalid enum value.",
                    Result = false
                };
            }

            if (!((object)whereClause.Column).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in sqlCondition object is an invalid enum value.",
                    Result = false
                };
            }

            List<WhereClause> conditions = new List<WhereClause>();

            if (whereClause.Column != null)
            {
                conditions.Add(whereClause);
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, null, conditions, selectClause));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="whereClauses">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(List<WhereClause> whereClauses, bool asDataTable = false)
        {
            foreach (WhereClause item in whereClauses)
            {
                if (!((object)item.Column).InValidEnumType<T>())
                {
                    return new ResultBox()
                    {
                        ErrorMessage = "Column property in sqlConditions List object has invalid enum value.",
                        Result = false
                    };
                }
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, null, whereClauses));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="whereClauses">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <param name="selectClause">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan SelectCondition t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(SelectClause selectClause, List<WhereClause> whereClauses, bool asDataTable = false)
        {
            if (!((object)selectClause.OrderColumn).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in selectCondition object is an invalid enum value.",
                    Result = false
                };
            }

            foreach (WhereClause item in whereClauses)
            {
                if (!((object)item.Column).InValidEnumType<T>())
                {
                    return new ResultBox()
                    {
                        ErrorMessage = "Column property in sqlConditions List object has invalid enum value.",
                        Result = false
                    };
                }
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, null, whereClauses, selectClause));
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
            if (!((object)columns).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "selectColumns object is not a valid enum object.",
                    Result = false
                };
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, columns));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="selectClause">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan SelectCondition t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, SelectClause selectClause, bool asDataTable = false)
        {
            if (!((object)selectClause.OrderColumn).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in selectCondition object is an invalid enum value.",
                    Result = false
                };
            }

            if (!((object)columns).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "selectColumns object is not a valid enum object.",
                    Result = false
                };
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, columns, null, selectClause));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="whereClause">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, WhereClause whereClause, bool asDataTable = false)
        {
            if (!((object)columns).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "selectColumns object is not a valid enum object.",
                    Result = false
                };
            }

            if (!((object)whereClause.Column).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in sqlCondition object is an invalid enum value.",
                    Result = false
                };
            }

            List<WhereClause> conditions = new List<WhereClause>();

            if (whereClause.Column != null)
            {
                conditions.Add(whereClause);
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, columns, conditions));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="whereClause">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir.</param>
        /// <param name="selectClause">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan SelectCondition t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, SelectClause selectClause, WhereClause whereClause, bool asDataTable = false)
        {
            if (!((object)selectClause.OrderColumn).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in selectCondition object is an invalid enum value.",
                    Result = false
                };
            }

            if (!((object)columns).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "selectColumns object is not a valid enum object.",
                    Result = false
                };
            }

            if (whereClause.Column != null)
            {
                if (!((object)whereClause.Column).InValidEnumType<T>())
                {
                    return new ResultBox()
                    {
                        ErrorMessage = "Column property in sqlCondition object is an invalid enum value.",
                        Result = false
                    };
                }
            }

            List<WhereClause> conditions = new List<WhereClause>();

            if (whereClause.Column != null)
            {
                conditions.Add(whereClause);
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, columns, conditions, selectClause));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="whereClauses">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, List<WhereClause> whereClauses, bool asDataTable = false)
        {
            if (!((object)columns).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "selectColumns object is not a valid enum object.",
                    Result = false
                };
            }

            foreach (WhereClause item in whereClauses)
            {
                if (!((object)item.Column).InValidEnumType<T>())
                {
                    return new ResultBox()
                    {
                        ErrorMessage = "Column property in sqlConditions List object has invalid enum value.",
                        Result = false
                    };
                }
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, columns, whereClauses));
        }

        /// <summary>
        /// �stenilen �artlara uygun ResultBox d�ner. ResultBox'�n Data �zelli�inde veriyi saklar. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="columns">�ekilecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="whereClauses">Veri �ekerken select sql c�mlesine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <param name="selectClause">Veri �ekerken select sql c�mlesine ait Top, OrderBy gibi �artlar� tutan SelectCondition t�r� nesnedir.</param>
        /// <param name="asDataTable">varsay�lan olarak false'tur. true atan�rsa model olarak de�il DataTable olarak veri tutan ResultBox d�ner yoksa varsay�lan olarak List format�nda model tutan ResultBox d�ner.</param>
        /// <returns></returns>
        public static ResultBox Select(dynamic columns, SelectClause selectClause, List<WhereClause> whereClauses, bool asDataTable = false)
        {
            if (!((object)selectClause.OrderColumn).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in selectCondition object is an invalid enum value.",
                    Result = false
                };
            }

            if (!((object)columns).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "selectColumns object is not a valid enum object.",
                    Result = false
                };
            }

            foreach (WhereClause item in whereClauses)
            {
                if (!((object)item.Column).InValidEnumType<T>())
                {
                    return new ResultBox()
                    {
                        ErrorMessage = "Column property in sqlConditions List object has invalid enum value.",
                        Result = false
                    };
                }
            }

            return ReturnSelect(asDataTable, DataHelper<T>.Select(asDataTable, columns, whereClauses, selectClause));
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

                                    foreach (PropertyInfo item in type.GetProperties().ToList().ReturnValidProperties())
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
                                _rb.Clean();
                                _rb.Result = false;
                                _rb.ErrorMessage = ex.Message;
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
        public static ResultBox Insert(dynamic values)
        {
            if (!((object)values).IsValidModel<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "insertValues object is not a valid model.",
                    Result = false
                };
            }

            return DataHelper<T>.Insert(values);
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
            if (!((object)values).IsValidModel<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "updateValues object is not a valid model.",
                    Result = false
                };
            }

            return DataHelper<T>.Update(values);
        }

        /// <summary>
        /// Update i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
        /// <param name="whereClause">Update i�lemine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, WhereClause whereClause)
        {
            if (!((object)values).IsValidModel<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "updateValues object is not a valid model.",
                    Result = false
                };
            }

            if (!((object)whereClause.Column).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in sqlCondition object is an invalid enum value.",
                    Result = false
                };
            }

            List<WhereClause> conditions = new List<WhereClause>();

            if (whereClause.Column != null)
            {
                conditions.Add(whereClause);
            }

            return DataHelper<T>.Update(values, null, conditions);
        }

        /// <summary>
        /// Update i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
        /// <param name="whereClauses">Update i�lemine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, List<WhereClause> whereClauses)
        {
            if (!((object)values).IsValidModel<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "updateValues object is not a valid model.",
                    Result = false
                };
            }

            foreach (WhereClause item in whereClauses)
            {
                if (!((object)item.Column).InValidEnumType<T>())
                {
                    return new ResultBox()
                    {
                        ErrorMessage = "Column property in sqlConditions List object has invalid enum value.",
                        Result = false
                    };
                }
            }

            return DataHelper<T>.Update(values, null, whereClauses);
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
            if (!((object)values).IsValidModel<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "updateValues object is not a valid model.",
                    Result = false
                };
            }

            if (!((object)columns).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "updateColumns object is not a valid enum object.",
                    Result = false
                };
            }

            return DataHelper<T>.Update(values, columns);
        }

        /// <summary>
        /// Update i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
        /// <param name="columns">B�t�n kolonlar� de�ilde istenilen kolonlar� g�ncellemek i�in g�ncellenecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="whereClause">Update i�lemine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, dynamic columns, WhereClause whereClause)
        {
            if (!((object)values).IsValidModel<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "updateValues object is not a valid model.",
                    Result = false
                };
            }

            if (!((object)columns).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "updateColumns object is not a valid enum object.",
                    Result = false
                };
            }

            if (!((object)whereClause.Column).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in sqlCondition object is an invalid enum value.",
                    Result = false
                };
            }

            List<WhereClause> conditions = new List<WhereClause>();

            if (whereClause.Column != null)
            {
                conditions.Add(whereClause);
            }

            return DataHelper<T>.Update(values, columns, conditions);
        }

        /// <summary>
        /// Update i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon de�erlerini tutar. Model tipi �eklinde nesnedir.</param>
        /// <param name="columns">B�t�n kolonlar� de�ilde istenilen kolonlar� g�ncellemek i�in g�ncellenecek kolonlar g�nderilir. Birden fazla kolon �ekilecekse List �eklinde g�nderilir.</param>
        /// <param name="whereClauses">Update i�lemine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <returns></returns>
        public static ResultBox Update(dynamic values, dynamic columns, List<WhereClause> whereClauses)
        {
            if (!((object)values).IsValidModel<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "updateValues object is not a valid model.",
                    Result = false
                };
            }

            if (!((object)columns).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "updateColumns object is not a valid enum object.",
                    Result = false
                };
            }

            foreach (WhereClause item in whereClauses)
            {
                if (!((object)item.Column).InValidEnumType<T>())
                {
                    return new ResultBox()
                    {
                        ErrorMessage = "Column property in sqlConditions List object has invalid enum value.",
                        Result = false
                    };
                }
            }

            return DataHelper<T>.Update(values, columns, whereClauses);
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
        /// <param name="whereClause">Delete i�lemine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir.</param>
        /// <returns></returns>
        public static ResultBox Delete(WhereClause whereClause)
        {
            if (!((object)whereClause.Column).InValidEnumType<T>())
            {
                return new ResultBox()
                {
                    ErrorMessage = "Column property in sqlCondition object is an invalid enum value.",
                    Result = false
                };
            }

            List<WhereClause> conditions = new List<WhereClause>();

            if (whereClause.Column != null)
            {
                conditions.Add(whereClause);
            }

            return DataHelper<T>.Delete(conditions);
        }

        /// <summary>
        /// Delete i�leminin sonucuna g�re ResultBox d�ner. Hata olu�tuysa Result de�eri false'tur ve ErrorMessage �zelli�inde hata mesaj� yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi g�nderilir.</typeparam>
        /// <param name="whereClauses">Delete i�lemine ait Like, Between gibi �artlar� tutan WhereCondition t�r� nesnedir. Birden fazla �art var ise List �eklinde �art g�nderir.</param>
        /// <returns></returns>
        public static ResultBox Delete(List<WhereClause> whereClauses)
        {
            foreach (WhereClause item in whereClauses)
            {
                if (!((object)item.Column).InValidEnumType<T>())
                {
                    return new ResultBox()
                    {
                        ErrorMessage = "Column property in sqlConditions List object has invalid enum value.",
                        Result = false
                    };
                }
            }

            return DataHelper<T>.Delete(whereClauses);
        }

        #endregion
    }
}
