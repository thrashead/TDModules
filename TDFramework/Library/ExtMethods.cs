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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using TDFramework.Common;

namespace TDFramework
{
    internal static class ExtMethods
    {
        static ExtMethods()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDFramework.Common.TDConnection.ConnectionStringForOnce = null;
            };
        }

        internal static string GetTypeName(this Type _type)
        {
            var nullableType = Nullable.GetUnderlyingType(_type);

            bool isNullableType = nullableType != null;

            if (isNullableType)
                return nullableType.Name;
            else
                return _type.Name;
        }

        internal static string GetTableColumnName(this PropertyInfo _propInfo)
        {
            string columnName = _propInfo.Name;

            if (_propInfo.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "TableColumn").ToList().Count > 0)
            {
                object o = _propInfo.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "TableColumn").ToList().FirstOrDefault();

                PropertyInfo propsAttr = o.GetType().GetProperty("Name");

                if (propsAttr != null)
                {
                    columnName = propsAttr.GetValue(o, null).ToString();
                }
            }

            return columnName;
        }

        internal static string GetDBTableName(this Type _type)
        {
            string tableName = _type.Name;

            foreach (object item in _type.GetCustomAttributes(false))
            {
                string attr = item.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last();

                if (attr == "DBTable")
                {
                    PropertyInfo propsAttr = item.GetType().GetProperty("Name");

                    if (propsAttr != null)
                    {
                        tableName = propsAttr.GetValue(item, null).ToString();
                    }
                }
            }

            return tableName;
        }

        internal static bool InType(this Type _type)
        {
            if (_type.IsGenericType && _type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                _type = _type.GetGenericArguments()[0];
            }

            string[] types = new string[15] { "Int16", "Int32", "Int64", "Decimal", "Double", "Char", "String", "Byte", "Boolean", "DateTime", "DateTimeOffset", "TimeSpan", "Single", "Object", "Guid" };

            if (types.Contains(_type.Name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static bool InValidEnumType<T>(this object _object)
        {
            bool control = true;

            if (_object != null)
            {
                Type typeModel = typeof(T);
                string typeName = typeModel.Name;

                Type type = _object.GetType();

                if (type.IsGenericType)
                {
                    type = type.GetGenericArguments()[0];
                }

                if (!type.IsEnum)
                {
                    control = false;
                }

                if (type.Name != typeName + "Columns")
                {
                    control = false;
                }
            }

            return control;
        }

        internal static bool IsValidModel<T>(this object _object)
        {
            Type typeModel = typeof(T);

            Type type = _object.GetType();

            if (type != typeModel)
            {
                return false;
            }

            return true;
        }

        internal static string ReturnRealColumnName<T>(this object _column)
        {
            Type typeModel = typeof(T);
            PropertyInfo propInfo = typeModel.GetProperty(_column.ToString());

            return propInfo.GetTableColumnName().ToString();
        }

        internal static string ReturnIDColumn(this List<PropertyInfo> _props)
        {
            string identity = "";

            foreach (PropertyInfo item in _props.ReturnValidProperties())
            {
                foreach (object itemAttr in item.GetCustomAttributes(false))
                {
                    string attr = itemAttr.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last();

                    if (attr == "IDColumn")
                    {
                        identity = item.Name;
                    }
                }
            }

            return identity;
        }

        internal static string ReturnFirstColumn(this List<PropertyInfo> _props)
        {
            string firstColumn = "ID";

            if (_props.Count > 0)
            {
                firstColumn = _props.FirstOrDefault().GetTableColumnName();
            }

            return firstColumn;
        }

        internal static string ReturnQueryStringForInsert(this List<PropertyInfo> _props, string tableName, string _identity, bool returnID = false)
        {
            string querystring = "Insert Into " + tableName + "(";

            foreach (PropertyInfo item in _props.ReturnValidProperties())
            {
                if (item.PropertyType.InType())
                {
                    if (_identity != item.Name)
                    {
                        querystring += "[" + item.GetTableColumnName() + "],";
                    }
                }
            }

            querystring = querystring.TrimEnd(',');
            querystring += ") Values(";

            foreach (PropertyInfo item in _props)
            {
                if (item.PropertyType.InType())
                {
                    if (_identity != item.Name)
                    {
                        querystring += "@" + item.GetTableColumnName() + ",";
                    }
                }
            }

            querystring = querystring.TrimEnd(',');
            querystring += ")";

            querystring = returnID == true ? querystring + " Select SCOPE_IDENTITY()" : querystring;

            return querystring;
        }

        internal static List<PropertyInfo> ReturnValidProperties(this List<PropertyInfo> _props, bool forSelect = false)
        {
            List<PropertyInfo> returnProps = new List<PropertyInfo>();

            foreach (PropertyInfo item in _props)
            {
                if (item.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "NotTableColumn").ToList().Count <= 0)
                {
                    if (item.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "RTable").ToList().Count <= 0)
                    {
                        if (forSelect)
                        {
                            returnProps.Add(item);
                        }
                        else if (item.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "AggregateColumn").ToList().Count <= 0)
                        {
                            returnProps.Add(item);
                        }
                    }
                }
            }

            return returnProps;
        }

        internal static int SelectCount(this object _data)
        {
            try
            {
                string count = ((DBNull)_data).ToString();

                return 0;
            }
            catch
            {
                return 1;
            }
        }

        internal static string ToJoiner(this JoinTypes _joinType)
        {
            switch (_joinType)
            {
                case JoinTypes.CROSS: return "Cross Join";
                case JoinTypes.FULL: return "Full Join";
                case JoinTypes.INNER: return "Inner Join";
                case JoinTypes.LEFT: return "Left Join";
                case JoinTypes.RIGHT: return "Right Join";
                default: return "Inner Join";
            }
        }

        internal static List<dynamic> ToDynamicList(this DataTable _dataTable, string _className)
        {
            return ToDynamicList(ToDictionary(_dataTable), GetNewObject(_dataTable.Columns, _className));
        }
        #region ToDynamicList

        private static List<Dictionary<string, object>> ToDictionary(DataTable _dataTable)
        {
            var columns = _dataTable.Columns.Cast<DataColumn>();
            var Temp = _dataTable.AsEnumerable().Select(dataRow => columns.Select(column =>
                                 new { Column = column.ColumnName, Value = dataRow[column] })
                             .ToDictionary(data => data.Column, data => data.Value)).ToList();
            return Temp.ToList();
        }

        private static List<dynamic> ToDynamicList(List<Dictionary<string, object>> _list, Type _typeObj)
        {
            dynamic temp = new List<dynamic>();
            foreach (Dictionary<string, object> item in _list)
            {
                object Obj = Activator.CreateInstance(_typeObj);

                PropertyInfo[] properties = Obj.GetType().GetProperties();

                Dictionary<string, object> DictList = (Dictionary<string, object>)item;

                foreach (KeyValuePair<string, object> keyValuePair in DictList)
                {
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.Name == keyValuePair.Key)
                        {
                            if (keyValuePair.Value != null && keyValuePair.Value.GetType() != typeof(System.DBNull))
                            {
                                if (keyValuePair.Value.GetType() == typeof(System.Guid))
                                {
                                    property.SetValue(Obj, keyValuePair.Value, null);
                                }
                                else
                                {
                                    property.SetValue(Obj, keyValuePair.Value, null);
                                }
                            }
                            break;
                        }
                    }
                }
                temp.Add(Obj);
            }
            return temp;
        }

        private static Type GetNewObject(DataColumnCollection _columns, string _className)
        {
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = "YourAssembly";
            System.Reflection.Emit.AssemblyBuilder assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder module = assemblyBuilder.DefineDynamicModule("YourDynamicModule");
            TypeBuilder typeBuilder = module.DefineType(_className, TypeAttributes.Public);

            foreach (DataColumn column in _columns)
            {
                string propertyName = column.ColumnName;
                FieldBuilder field = typeBuilder.DefineField(propertyName, column.DataType, FieldAttributes.Public);
                PropertyBuilder property = typeBuilder.DefineProperty(propertyName, System.Reflection.PropertyAttributes.None, column.DataType, new Type[] { column.DataType });
                MethodAttributes GetSetAttr = MethodAttributes.Public | MethodAttributes.HideBySig;
                MethodBuilder currGetPropMthdBldr = typeBuilder.DefineMethod("get_value", GetSetAttr, column.DataType, Type.EmptyTypes); // new Type[] { column.DataType });
                ILGenerator currGetIL = currGetPropMthdBldr.GetILGenerator();
                currGetIL.Emit(OpCodes.Ldarg_0);
                currGetIL.Emit(OpCodes.Ldfld, field);
                currGetIL.Emit(OpCodes.Ret);
                MethodBuilder currSetPropMthdBldr = typeBuilder.DefineMethod("set_value", GetSetAttr, null, new Type[] { column.DataType });
                ILGenerator currSetIL = currSetPropMthdBldr.GetILGenerator();
                currSetIL.Emit(OpCodes.Ldarg_0);
                currSetIL.Emit(OpCodes.Ldarg_1);
                currSetIL.Emit(OpCodes.Stfld, field);
                currSetIL.Emit(OpCodes.Ret);
                property.SetGetMethod(currGetPropMthdBldr);
                property.SetSetMethod(currSetPropMthdBldr);
            }
            Type obj = typeBuilder.CreateType();
            return obj;
        }

        #endregion

        internal static string MakeSingle(this string _text, string _changeText)
        {
            do
            {
                _text = _text.Replace(_changeText + _changeText, _changeText);
            } while (_text.Contains(_changeText + _changeText));

            return _text;
        }

        internal static string ToShortAggregateName(this Aggregates aggregate)
        {
            switch (aggregate)
            {
                case Aggregates.AVERAGE: return "Avg";
                case Aggregates.COUNT: return "Count";
                case Aggregates.MAXIMUM: return "Max";
                case Aggregates.MINIMUM: return "Min";
                case Aggregates.SUMMARY: return "Sum";
                default: return "Count";
            }
        }

        internal static List<SqlParameter> ToParameterList(this SqlParameterCollection parameters)
        {
            List<SqlParameter> returnParams = new List<SqlParameter>();

            foreach (SqlParameter item in parameters)
            {
                returnParams.Add(item);
            }

            return returnParams;
        }
    }
}
