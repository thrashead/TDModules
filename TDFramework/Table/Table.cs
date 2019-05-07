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
    public sealed class Table<T>
        where T : ITDModel
    {
        static Table()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Table()
        {
            this.Alias = null;
            this.Data = null;
            this.Count = 0;
            this.QueryString = null;
            this.Parameters = new List<SqlParameter>();
            this.Columns = null;
            this.Values = null;
            this.Error = null;
            this.SelectSettings = new Select();
            this.WhereList = new List<Where>();
        }

        internal string Alias { get; set; }
        internal string TableName
        {
            get
            {
                return typeof(T).GetDBTableName();
            }
        }

        public dynamic Data { get; internal set; }
        public int Count { get; internal set; }
        public bool HasData
        {
            get
            {
                if (this.Error == null && this.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string QueryString { get; internal set; }
        public List<SqlParameter> Parameters { get; internal set; }

        public dynamic Columns { get; set; }
        public dynamic Values { get; set; }
        public Error Error { get; internal set; }
        public Select SelectSettings { get; set; }
        public List<Where> WhereList { get; set; }

        #region Select

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select(bool asDataTable = false)
        {
            this.Error = TDError<T>.ReturnError(new TDError<T>() { Columns = this.Columns }, MethodType.Select);
            if (this.Error != null) { return; }

            this.Error = TDError<T>.ReturnError(new TDError<T>() { Columns = this.Columns, SelectValues = this.SelectSettings }, MethodType.Select);
            if (this.Error != null) { return; }

            this.Error = TDError<T>.ReturnError(new TDError<T>() { WhereList = this.WhereList }, MethodType.Select);
            if (this.Error != null) { return; }

            Table<T> returnTable = ReturnSelect(asDataTable, Data<T>.Select(this.Columns, this.WhereList, this.SelectSettings));

            AssignTableValues(this, returnTable);
        }
        private static Table<T> ReturnSelect(bool asDataTable, Table<T> _table)
        {
            if (asDataTable == false)
            {
                if (_table != null)
                {
                    if (_table.Error == null)
                    {
                        if (_table.Data != null)
                        {
                            List<T> dataList = new List<T>();
                            Type type = typeof(T);

                            try
                            {
                                foreach (DataRow tableItem in _table.Data.Rows)
                                {
                                    object obj = Activator.CreateInstance(type);

                                    foreach (PropertyInfo item in type.GetProperties().ToList().ReturnValidProperties(true))
                                    {
                                        string propTypeName = item.PropertyType.GetTypeName();
                                        string columnName = item.Name;

                                        foreach (object itemAttr in item.GetCustomAttributes(false))
                                        {
                                            string attr = itemAttr.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last();

                                            if (attr == "TableColumn")
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
                                _table.Error = new Error();
                                _table.Error.Message = ex.Message;
                                _table.Error.Layer = ErrorLayers.TABLE;
                                return _table;
                            }

                            _table.Data = dataList;
                        }
                    }
                }
            }

            return _table;
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="firstTable">Seçilecek tablodan ilkinin select ve where cümleciklerini tutar.</param>
        /// <param name="secondTable">Seçilecek tablodan ikincisinin select ve where cümleciklerini tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="asDataTable">DataTable nesnesi döndürmek için değeri true yapılır.</param>
        /// <returns></returns>
        public void Select<T2>(Table<T2> secondTable, Relation<T, T2> relation, bool asDataTable = false)
            where T2 : ITDModel
        {
            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T>.ReturnError(relation.FirstRelatedColumn, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(relation.SecondRelatedColumn, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            List<Relation<T, T2>> relationList = new List<Relation<T, T2>>();
            relationList.Add(relation);

            Table<T> returnTable = Data<T, T2>.Select((Table<T>)this, secondTable, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="firstTable">Seçilecek tablodan ilkinin select ve where cümleciklerini tutar.</param>
        /// <param name="secondTable">Seçilecek tablodan ikincisinin select ve where cümleciklerini tutar.</param>
        /// <param name="relationList">Join tipi seçilir.</param>
        /// <param name="asDataTable">DataTable nesnesi döndürmek için değeri true yapılır.</param>
        /// <returns></returns>
        public void Select<T2>(Table<T2> secondTable, List<Relation<T, T2>> relationList, bool asDataTable = false)
            where T2 : ITDModel
        {
            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            foreach (var item in relationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }

                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2>.Select((Table<T>)this, secondTable, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="firstTable">Seçilecek tablodan ilkinin select ve where cümleciklerini tutar.</param>
        /// <param name="secondTable">Seçilecek tablodan ikincisinin select ve where cümleciklerini tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="asDataTable">DataTable nesnesi döndürmek için değeri true yapılır.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> firstRelationList = new List<Relation<T, T2>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, firstRelationList, null, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> firstRelation, Relation<T2, T3> secondRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> firstRelationList = new List<Relation<T, T2>>() { firstRelation };
            List<Relation<T2, T3>> secondRelationList = new List<Relation<T2, T3>>() { secondRelation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, firstRelationList, secondRelationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> firstRelation, Relation<T, T3> secondRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> firstRelationList = new List<Relation<T, T2>>() { firstRelation };
            List<Relation<T, T3>> secondRelationList = new List<Relation<T, T3>>() { secondRelation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, firstRelationList, null, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T2, T3> firstRelation, Relation<T, T3> secondRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> firstRelationList = new List<Relation<T2, T3>>() { firstRelation };
            List<Relation<T, T3>> secondRelationList = new List<Relation<T, T3>>() { secondRelation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, null, firstRelationList, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> firstRelation, Relation<T2, T3> secondRelation, Relation<T, T3> thirdRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> firstRelationList = new List<Relation<T, T2>>() { firstRelation };
            List<Relation<T2, T3>> secondRelationList = new List<Relation<T2, T3>>() { secondRelation };
            List<Relation<T, T3>> thirdRelationList = new List<Relation<T, T3>>() { thirdRelation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, firstRelationList, secondRelationList, thirdRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> relation, List<Relation<T2, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> secondRelationList = new List<Relation<T, T2>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, secondRelationList, relationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> relation, List<Relation<T, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> secondRelationList = new List<Relation<T, T2>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, secondRelationList, null, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> firstRelation, List<Relation<T2, T3>> relationList, Relation<T, T3> secondRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> secondRelationList = new List<Relation<T, T2>>() { firstRelation };
            List<Relation<T, T3>> thirdRelationList = new List<Relation<T, T3>>() { secondRelation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, secondRelationList, relationList, thirdRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> firstRelation, Relation<T2, T3> secondRelation, List<Relation<T, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> secondRelationList = new List<Relation<T, T2>>() { firstRelation };
            List<Relation<T2, T3>> thirdRelationList = new List<Relation<T2, T3>>() { secondRelation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in thirdRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, secondRelationList, thirdRelationList, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> relation, List<Relation<T2, T3>> firstRelationList, List<Relation<T, T3>> secondRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> thirdRelationList = new List<Relation<T, T2>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in thirdRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in thirdRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, thirdRelationList, firstRelationList, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T2, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> relationList = new List<Relation<T2, T3>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in relationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, null, relationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T2, T3> relation, List<Relation<T, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> secondRelationList = new List<Relation<T2, T3>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in relationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, null, secondRelationList, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T3>> relationList = new List<Relation<T, T3>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in relationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, null, null, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in relationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, relationList, null, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> relationList, Relation<T2, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> secondRelationList = new List<Relation<T2, T3>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in relationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, relationList, secondRelationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> relationList, Relation<T, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T3>> secondRelationList = new List<Relation<T, T3>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in relationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, relationList, null, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> relationList, Relation<T2, T3> firstRelation, Relation<T, T3> secondRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> secondRelationList = new List<Relation<T2, T3>>() { firstRelation };
            List<Relation<T, T3>> thirdRelationList = new List<Relation<T, T3>>() { secondRelation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in relationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, relationList, secondRelationList, thirdRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> firstRelationList, List<Relation<T2, T3>> secondRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, firstRelationList, secondRelationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> firstRelationList, List<Relation<T, T3>> secondRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, firstRelationList, null, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> firstRelationList, List<Relation<T2, T3>> secondRelationList, Relation<T, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T3>> thirdRelationList = new List<Relation<T, T3>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, firstRelationList, secondRelationList, thirdRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> firstRelationList, Relation<T2, T3> relation, List<Relation<T, T3>> secondRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> thirdRelationList = new List<Relation<T2, T3>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in thirdRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, firstRelationList, thirdRelationList, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> firstRelationList, List<Relation<T2, T3>> secondRelationList, List<Relation<T, T3>> thirdRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, firstRelationList, secondRelationList, thirdRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T2, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in relationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, null, relationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T2, T3>> relationList, Relation<T, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T3>> secondRelationList = new List<Relation<T, T3>>() { relation };

            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, null, relationList, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T2, T3>> firstRelationList, List<Relation<T, T3>> secondRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (this.Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, null, firstRelationList, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="columns">Çekilecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Veri çekerken select sql cümlesine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <param name="select">Veri çekerken select sql cümlesine ait Top, OrderBy gibi şartları tutan Select türü nesnedir.</param>
        /// <param name="asDataTable">varsayılan olarak false'tur. true atanırsa model olarak değil DataTable olarak veri tutan ResultBox döner yoksa varsayılan olarak List formatında model tutan ResultBox döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            this.Error = TDError<T>.ReturnError((Table<T>)this, "first");
            if (this.Error != null) { return; }

            this.Error = TDError<T2>.ReturnError(secondTable, "second");
            if (this.Error != null) { return; }

            this.Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (this.Error != null) { return; }

            foreach (var item in relationList)
            {
                this.Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (this.Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                this.Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (this.Error != null) { return; }
            }

            this.Error = TDError<T, T2>.ReturnError((Table<T>)this, secondTable);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select((Table<T>)this, secondTable, thirdTable, null, null, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        private static void AssignTableValues(Table<T> table1, Table<T> table2)
        {
            table1.Alias = table2.Alias;
            table1.Columns = table2.Columns;
            table1.Data = table2.Data;
            table1.Count = table2.Count;
            table1.Parameters = table2.Parameters;
            table1.QueryString = table2.QueryString;
            table1.Error = table2.Error;
            table1.SelectSettings = table2.SelectSettings;
            table1.Values = table2.Values;
            table1.WhereList = table2.WhereList;
        }

        #endregion

        #region Insert

        /// <summary>
        /// Insert işleminin sonucuna göre ResultBox döner. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="values">Insert edilecek nesnenin kolon değerlerini tutar. Model tipi şeklinde nesnedir.</param>
        /// <returns></returns>
        public void Insert(bool returnID = false)
        {
            this.Error = TDError<T>.ReturnError(new TDError<T>() { Values = this.Values }, MethodType.Insert, ErrorTypes.Values);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T>.Insert(this.Values, returnID);

            AssignTableValues(this, returnTable);
        }

        #endregion

        #region Update

        /// <summary>
        /// Update işleminin sonucuna göre ResultBox döner. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="values">Update edilecek nesnenin kolon değerlerini tutar. Model tipi şeklinde nesnedir.</param>
        /// <param name="columns">Bütün kolonları değilde istenilen kolonları güncellemek için güncellenecek kolonlar gönderilir. Birden fazla kolon çekilecekse List şeklinde gönderilir.</param>
        /// <param name="whereList">Update işlemine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <returns></returns>
        public void Update()
        {
            this.Error = TDError<T>.ReturnError(new TDError<T>() { Values = this.Values }, MethodType.Update, ErrorTypes.Values);
            if (this.Error != null) { return; }

            this.Error = TDError<T>.ReturnError(new TDError<T>() { Columns = this.Columns }, MethodType.Update);
            if (this.Error != null) { return; }

            this.Error = TDError<T>.ReturnError(new TDError<T>() { WhereList = this.WhereList }, MethodType.Update);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T>.Update(this.Values, this.Columns, this.WhereList);

            AssignTableValues(this, returnTable);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete işleminin sonucuna göre ResultBox döner. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="whereList">Delete işlemine ait Like, Between gibi şartları tutan Where türü nesnedir. Birden fazla şart var ise List şeklinde şart gönderir.</param>
        /// <returns></returns>
        public void Delete()
        {
            this.Error = TDError<T>.ReturnError(new TDError<T>() { WhereList = this.WhereList }, MethodType.Delete);
            if (this.Error != null) { return; }

            Table<T> returnTable = Data<T>.Delete(this.WhereList);

            AssignTableValues(this, returnTable);
        }

        #endregion
    }

    public sealed class Table
    {
        static Table()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate (object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Table()
        {
            this.Data = null;
            this.Count = 0;
            this.QueryString = null;
            this.Parameters = new List<SqlParameter>();
            this.Error = null;
        }

        public dynamic Data { get; internal set; }
        public int Count { get; internal set; }
        public bool HasData
        {
            get
            {
                if (this.Error == null && this.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string QueryString { get; internal set; }
        public List<SqlParameter> Parameters { get; internal set; }
        public Error Error { get; internal set; }

        #region SqlMethods

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Select komutu için sql cümlesi gönderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public void ExecuteReader(string queryString, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteReader(queryString, null, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Select komutu için sql cümlesi gönderir.</param>
        /// <param name="sqlParameter">Sql cümlesinde tek bir parametre kullanılıyorsa bu parametre gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <Table returnTable =s></Table returnTable =s>
        public void ExecuteReader(string queryString, SqlParameter sqlParameter, CommandType? commandType = null)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (sqlParameter != null)
            {
                sqlParameters.Add(sqlParameter);
            }

            Table returnTable = SqlMethods.ExecuteReader(queryString, sqlParameters, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Select komutu için sql cümlesi gönderir.</param>
        /// <param name="sqlParameters">Sql cümlesinde birden çok parametre kullanılıyorsa bu parametreler gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <Table returnTable =s></Table returnTable =s>
        public void ExecuteReader(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteReader(queryString, sqlParameters, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// Insert, Update ve Delete işleminin sonucuna göre ResultBox döner. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <Table returnTable =s></Table returnTable =s>
        public void ExecuteNonQuery(string queryString, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteNonQuery(queryString, null, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// Insert, Update ve Delete işleminin sonucuna göre ResultBox döner. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="sqlParameter">Sql cümlesinde tek bir parametre kullanılıyorsa bu parametre gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <Table returnTable =s></Table returnTable =s>
        public void ExecuteNonQuery(string queryString, SqlParameter sqlParameter, CommandType? commandType = null)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (sqlParameter != null)
            {
                sqlParameters.Add(sqlParameter);
            }

            Table returnTable = SqlMethods.ExecuteNonQuery(queryString, sqlParameters, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun ResultBox döner. ResultBox'ın Data özelliğinde veriyi saklar. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// Insert, Update ve Delete işleminin sonucuna göre ResultBox döner. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="sqlParameters">Sql cümlesinde birden çok parametre kullanılıyorsa bu parametreler gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <Table returnTable =s></Table returnTable =s>
        public void ExecuteNonQuery(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteNonQuery(queryString, sqlParameters, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// Tablo üzerinde Scalar işlem sonuçları döner. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <Table returnTable =s></Table returnTable =s>
        public void ExecuteScalar(string queryString, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteScalar(queryString, null, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// Tablo üzerinde Scalar işlem sonuçları döner. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="sqlParameter">Sql cümlesinde tek bir parametre kullanılıyorsa bu parametre gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <Table returnTable =s></Table returnTable =s>
        public void ExecuteScalar(string queryString, SqlParameter sqlParameter, CommandType? commandType = null)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            if (sqlParameter != null)
            {
                sqlParameters.Add(sqlParameter);
            }

            Table returnTable = SqlMethods.ExecuteScalar(queryString, sqlParameters, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// Tablo üzerinde Scalar işlem sonuçları döner. Hata oluştuysa Result değeri false'tur ve ErrorMessage özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="sqlParameters">Sql cümlesinde birden çok parametre kullanılıyorsa bu parametreler gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <Table returnTable =s></Table returnTable =s>
        public void ExecuteScalar(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteScalar(queryString, sqlParameters, commandType);

            AssignTableValues(this, returnTable);
        }

        private static void AssignTableValues(Table table1, Table table2)
        {
            table1.Data = table2.Data;
            table1.Count = table2.Count;
            table1.Parameters = table2.Parameters;
            table1.QueryString = table2.QueryString;
            table1.Error = table2.Error;
        }

        #endregion
    }
}
