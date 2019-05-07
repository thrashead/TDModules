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
using TDFramework.Common.Internal;
using TDFramework.Common.TDModel;
using TDFramework.Data;
using TDFramework.Library;

namespace TDFramework
{
    public sealed class Table<T>
        where T : ITDModel
    {
        static Table()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Table()
        {
            Alias = null;
            Data = null;
            Count = 0;
            QueryString = null;
            Parameters = new List<SqlParameter>();
            Columns = null;
            Values = null;
            Error = null;
            SelectSettings = new Select();
            WhereList = new List<Where>();
        }

        internal string Alias { get; set; }
        internal string TableName => typeof(T).GetDBTableName();

        public dynamic Data { get; internal set; }
        public int Count { get; internal set; }
        public bool HasData
        {
            get
            {
                if (Error == null && Count > 0)
                {
                    return true;
                }

                return false;
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
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select(bool asDataTable = false)
        {
            Error = TDError<T>.ReturnError(new TDError<T>() { Columns = Columns });
            if (Error != null) { return; }

            Error = TDError<T>.ReturnError(new TDError<T>() { Columns = Columns, SelectValues = SelectSettings });
            if (Error != null) { return; }

            Error = TDError<T>.ReturnError(new TDError<T>() { WhereList = WhereList });
            if (Error != null) { return; }

            Table<T> returnTable = ReturnSelect(asDataTable, Data<T>.Select(Columns, WhereList, SelectSettings));

            AssignTableValues(this, returnTable);
        }
        private static Table<T> ReturnSelect(bool asDataTable, Table<T> table)
        {
            if (asDataTable == false)
            {
                if (table != null)
                {
                    if (table.Error == null)
                    {
                        if (table.Data != null)
                        {
                            List<T> dataList = new List<T>();
                            Type type = typeof(T);

                            try
                            {
                                foreach (DataRow tableItem in table.Data.Rows)
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
                                                    default: item.SetValue(obj, tableItem[columnName] != DBNull.Value ? tableItem[columnName] : null, null); break;
                                                }
                                            }
                                        }
                                    }

                                    dataList.Add((T)obj);
                                }
                            }
                            catch (Exception ex)
                            {
                                table.Error = new Error { Message = ex.Message, Layer = ErrorLayers.TABLE };

                                return table;
                            }

                            table.Data = dataList;
                        }
                    }
                }
            }

            return table;
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2>(Table<T2> secondTable, Relation<T, T2> relation, bool asDataTable = false)
            where T2 : ITDModel
        {
            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T>.ReturnError(relation.FirstRelatedColumn, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(relation.SecondRelatedColumn, "second");
            if (Error != null) { return; }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            List<Relation<T, T2>> relationList = new List<Relation<T, T2>> { relation };

            Table<T> returnTable = Data<T, T2>.Select(this, secondTable, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2>(Table<T2> secondTable, List<Relation<T, T2>> relationList, bool asDataTable = false)
            where T2 : ITDModel
        {
            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            foreach (var item in relationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }

                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2>.Select(this, secondTable, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> firstRelationList = new List<Relation<T, T2>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, firstRelationList, null, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="secondRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> firstRelation, Relation<T2, T3> secondRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> firstRelationList = new List<Relation<T, T2>>() { firstRelation };
            List<Relation<T2, T3>> secondRelationList = new List<Relation<T2, T3>>() { secondRelation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, firstRelationList, secondRelationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="secondRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> firstRelation, Relation<T, T3> secondRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> firstRelationList = new List<Relation<T, T2>>() { firstRelation };
            List<Relation<T, T3>> secondRelationList = new List<Relation<T, T3>>() { secondRelation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, firstRelationList, null, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="secondRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T2, T3> firstRelation, Relation<T, T3> secondRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> firstRelationList = new List<Relation<T2, T3>>() { firstRelation };
            List<Relation<T, T3>> secondRelationList = new List<Relation<T, T3>>() { secondRelation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, null, firstRelationList, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="secondRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="thirdRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> firstRelation, Relation<T2, T3> secondRelation, Relation<T, T3> thirdRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> firstRelationList = new List<Relation<T, T2>>() { firstRelation };
            List<Relation<T2, T3>> secondRelationList = new List<Relation<T2, T3>>() { secondRelation };
            List<Relation<T, T3>> thirdRelationList = new List<Relation<T, T3>>() { thirdRelation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, firstRelationList, secondRelationList, thirdRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> relation, List<Relation<T2, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> secondRelationList = new List<Relation<T, T2>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, secondRelationList, relationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> relation, List<Relation<T, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> secondRelationList = new List<Relation<T, T2>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, secondRelationList, null, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="secondRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> firstRelation, List<Relation<T2, T3>> relationList, Relation<T, T3> secondRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> secondRelationList = new List<Relation<T, T2>>() { firstRelation };
            List<Relation<T, T3>> thirdRelationList = new List<Relation<T, T3>>() { secondRelation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, secondRelationList, relationList, thirdRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="secondRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> firstRelation, Relation<T2, T3> secondRelation, List<Relation<T, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> secondRelationList = new List<Relation<T, T2>>() { firstRelation };
            List<Relation<T2, T3>> thirdRelationList = new List<Relation<T2, T3>>() { secondRelation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in thirdRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, secondRelationList, thirdRelationList, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="firstRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="secondRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T2> relation, List<Relation<T2, T3>> firstRelationList, List<Relation<T, T3>> secondRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T2>> thirdRelationList = new List<Relation<T, T2>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in thirdRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in thirdRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, thirdRelationList, firstRelationList, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T2, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> relationList = new List<Relation<T2, T3>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in relationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, null, relationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T2, T3> relation, List<Relation<T, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> secondRelationList = new List<Relation<T2, T3>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in relationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, null, secondRelationList, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, Relation<T, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T3>> relationList = new List<Relation<T, T3>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in relationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, null, null, relationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in relationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, relationList, null, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> relationList, Relation<T2, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> secondRelationList = new List<Relation<T2, T3>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in relationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, relationList, secondRelationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> relationList, Relation<T, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T3>> secondRelationList = new List<Relation<T, T3>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in relationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, relationList, null, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="firstRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="secondRelation">Bağlanacak ilgili tablolara Relation sınıfında belirlenen join işlemini tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> relationList, Relation<T2, T3> firstRelation, Relation<T, T3> secondRelation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> secondRelationList = new List<Relation<T2, T3>>() { firstRelation };
            List<Relation<T, T3>> thirdRelationList = new List<Relation<T, T3>>() { secondRelation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in relationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, relationList, secondRelationList, thirdRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="secondRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> firstRelationList, List<Relation<T2, T3>> secondRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, firstRelationList, secondRelationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="secondRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> firstRelationList, List<Relation<T, T3>> secondRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, firstRelationList, null, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="secondRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> firstRelationList, List<Relation<T2, T3>> secondRelationList, Relation<T, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T3>> thirdRelationList = new List<Relation<T, T3>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, firstRelationList, secondRelationList, thirdRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="secondRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> firstRelationList, Relation<T2, T3> relation, List<Relation<T, T3>> secondRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T2, T3>> thirdRelationList = new List<Relation<T2, T3>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in thirdRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, firstRelationList, thirdRelationList, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="secondRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="thirdRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T2>> firstRelationList, List<Relation<T2, T3>> secondRelationList, List<Relation<T, T3>> thirdRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, firstRelationList, secondRelationList, thirdRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T2, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in relationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, null, relationList, null, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="relation">Join tipi seçilir.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T2, T3>> relationList, Relation<T, T3> relation, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            List<Relation<T, T3>> secondRelationList = new List<Relation<T, T3>>() { relation };

            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, null, relationList, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="firstRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="secondRelationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T2, T3>> firstRelationList, List<Relation<T, T3>> secondRelationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in secondRelationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T2>.ReturnError(item.SecondRelatedColumn, "second");
                if (Error != null) { return; }
            }

            foreach (var item in firstRelationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, null, firstRelationList, secondRelationList, asDataTable);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <typeparam name="T2">İkinci modelin tipi gönderilir.</typeparam>
        /// <typeparam name="T3">Üçüncü modelin tipi gönderilir.</typeparam>
        /// <param name="secondTable">Seçilecek tablolardan ikincisini tutar.</param>
        /// <param name="thirdTable">Seçilecek tablolardan üçüncüsünü tutar.</param>
        /// <param name="relationList">Bağlanacak ilgili tablolar arasında birden fazla join işlemi varsa belirlenen bu işlemleri liste şeklinde Relation sınıfında tutar.</param>
        /// <param name="asDataTable">Dönen Table nesnesinin Data özelliğinde tutulan verinin DataTable nesnesi olması istenirse değeri true yapılır. Varsayılan olarak false'tur ve bu şekildeyken istenilen modelde liste döner.</param>
        /// <returns></returns>
        public void Select<T2, T3>(Table<T2> secondTable, Table<T3> thirdTable, List<Relation<T, T3>> relationList, bool asDataTable = false)
            where T2 : ITDModel
            where T3 : ITDModel
        {
            Error = TDError<T>.ReturnError(this, "first");
            if (Error != null) { return; }

            Error = TDError<T2>.ReturnError(secondTable, "second");
            if (Error != null) { return; }

            Error = TDError<T3>.ReturnError(thirdTable, "third");
            if (Error != null) { return; }

            foreach (var item in relationList)
            {
                Error = TDError<T>.ReturnError(item.FirstRelatedColumn, "first");
                if (Error != null) { return; }
            }

            foreach (var item in relationList)
            {
                Error = TDError<T3>.ReturnError(item.SecondRelatedColumn, "third");
                if (Error != null) { return; }
            }

            Error = TDError<T, T2>.ReturnError(this, secondTable);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T, T2, T3>.Select(this, secondTable, thirdTable, null, null, relationList, asDataTable);

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
        /// Insert işleminin sonucuna göre Table nesnesi döner. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <param name="returnID">Eklenen yeni veriye ait identity kolonu değerinin Table nesnesinin Data özelliğinde tutulup tutulmayacağına karar verilir. Varsayılan olarak tutulur.</param>
        /// <returns></returns>
        public void Insert(bool returnID = true)
        {
            Error = TDError<T>.ReturnError(new TDError<T>() { Values = Values }, MethodType.Insert, ErrorTypes.Values);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T>.Insert(Values, returnID);

            AssignTableValues(this, returnTable);
        }

        #endregion

        #region Update

        /// <summary>
        /// Update işleminin sonucuna göre Table nesnesi döner. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <returns></returns>
        public void Update()
        {
            Error = TDError<T>.ReturnError(new TDError<T>() { Values = Values }, MethodType.Update, ErrorTypes.Values);
            if (Error != null) { return; }

            Error = TDError<T>.ReturnError(new TDError<T>() { Columns = Columns }, MethodType.Update);
            if (Error != null) { return; }

            Error = TDError<T>.ReturnError(new TDError<T>() { WhereList = WhereList }, MethodType.Update);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T>.Update(Values, Columns, WhereList);

            AssignTableValues(this, returnTable);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete işleminin sonucuna göre Table nesnesi döner. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <typeparam name="T">Model tipi gönderilir.</typeparam>
        /// <returns></returns>
        public void Delete()
        {
            Error = TDError<T>.ReturnError(new TDError<T>() { WhereList = WhereList }, MethodType.Delete);
            if (Error != null) { return; }

            Table<T> returnTable = Data<T>.Delete(WhereList);

            AssignTableValues(this, returnTable);
        }

        #endregion
    }

    public sealed class Table
    {
        static Table()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Table()
        {
            Data = null;
            Count = 0;
            QueryString = null;
            Parameters = new List<SqlParameter>();
            Error = null;
        }

        public dynamic Data { get; internal set; }
        public int Count { get; internal set; }
        public bool HasData
        {
            get
            {
                if (Error == null && Count > 0)
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
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="queryString">Select komutu için sql cümlesi gönderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public void ExecuteReader(string queryString, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteReader(queryString, null, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="queryString">Select komutu için sql cümlesi gönderir.</param>
        /// <param name="sqlParameter">Sql cümlesinde tek bir parametre kullanılıyorsa bu parametre gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
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
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="queryString">Select komutu için sql cümlesi gönderir.</param>
        /// <param name="sqlParameters">Sql cümlesinde birden çok parametre kullanılıyorsa bu parametreler gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public void ExecuteReader(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteReader(queryString, sqlParameters, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// Insert, Update ve Delete işleminin sonucuna göre Table nesnesi döner. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public void ExecuteNonQuery(string queryString, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteNonQuery(queryString, null, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// Insert, Update ve Delete işleminin sonucuna göre Table nesnesi döner. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="sqlParameter">Sql cümlesinde tek bir parametre kullanılıyorsa bu parametre gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
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
        /// İstenilen şartlara uygun Table nesnesi döner. Table nesnesinin Data özelliğinde veriyi saklar. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// Insert, Update ve Delete işleminin sonucuna göre Table nesnesi döner. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="sqlParameters">Sql cümlesinde birden çok parametre kullanılıyorsa bu parametreler gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public void ExecuteNonQuery(string queryString, List<SqlParameter> sqlParameters, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteNonQuery(queryString, sqlParameters, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// Tablo üzerinde Scalar işlem sonuçları döner. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
        public void ExecuteScalar(string queryString, CommandType? commandType = null)
        {
            Table returnTable = SqlMethods.ExecuteScalar(queryString, null, commandType);

            AssignTableValues(this, returnTable);
        }

        /// <summary>
        /// Tablo üzerinde Scalar işlem sonuçları döner. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="sqlParameter">Sql cümlesinde tek bir parametre kullanılıyorsa bu parametre gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
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
        /// Tablo üzerinde Scalar işlem sonuçları döner. Hata oluştuysa Error özelliğinde Hata nesnesi tutar ve ilgili Hata nesnesinin Message özelliğinde hata mesajı yazar.
        /// </summary>
        /// <param name="queryString">Insert, Update ve Delete komutları için sql cümlesi gönderir.</param>
        /// <param name="sqlParameters">Sql cümlesinde birden çok parametre kullanılıyorsa bu parametreler gönderilir.</param>
        /// <param name="commandType">Sql komutu Text mi, StoredProcedure mü yoksa TableDirect mi belirtilir.</param>
        /// <returns></returns>
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
