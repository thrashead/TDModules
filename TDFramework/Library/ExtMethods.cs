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
using System.Reflection.Emit;
using System.Threading;
using TDFramework.Common;

namespace TDFramework.Library
{
    internal static class ExtMethods
    {
        static ExtMethods()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        internal static string GetTypeName(this Type type)
        {
            Type nullableType = Nullable.GetUnderlyingType(type);

            bool isNullableType = nullableType != null;

            if (isNullableType)
                return nullableType.Name;
            else
                return type.Name;
        }

        internal static string GetTableColumnName(this PropertyInfo propInfo)
        {
            string columnName = propInfo.Name;

            if (propInfo.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "TableColumn").ToList().Count > 0)
            {
                object o = propInfo.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "TableColumn").ToList().FirstOrDefault();

                if (o != null)
                {
                    PropertyInfo propsAttr = o.GetType().GetProperty("Name");

                    if (propsAttr != null)
                    {
                        columnName = propsAttr.GetValue(o, null).ToString();
                    }
                }
            }

            return columnName;
        }

        internal static string GetDBTableName(this Type type)
        {
            string tableName = type.Name;

            foreach (object item in type.GetCustomAttributes(false))
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

        internal static bool InType(this Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = type.GetGenericArguments()[0];
            }

            string[] types = { "Int16", "Int32", "Int64", "Decimal", "Double", "Char", "String", "Byte", "Boolean", "DateTime", "DateTimeOffset", "TimeSpan", "Single", "Object", "Guid" };

            if (types.Contains(type.Name))
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

        internal static string ReturnIDColumn(this List<PropertyInfo> props)
        {
            string identity = "";

            foreach (PropertyInfo item in props.ReturnValidProperties())
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

        internal static string ReturnFirstColumn(this List<PropertyInfo> props)
        {
            string firstColumn = "ID";

            if (props.Count > 0)
            {
                firstColumn = props.FirstOrDefault().GetTableColumnName();
            }

            return firstColumn;
        }

        internal static string ReturnQueryStringForInsert(this List<PropertyInfo> props, string tableName, string identity, bool returnID = false)
        {
            string queryStr = "Insert Into " + tableName + "(";

            foreach (PropertyInfo item in props.ReturnValidProperties())
            {
                if (item.PropertyType.InType())
                {
                    if (identity != item.Name)
                    {
                        queryStr += "[" + item.GetTableColumnName() + "],";
                    }
                }
            }

            queryStr = queryStr.TrimEnd(',');
            queryStr += ") Values(";

            foreach (PropertyInfo item in props)
            {
                if (item.PropertyType.InType())
                {
                    if (identity != item.Name)
                    {
                        queryStr += "@" + item.GetTableColumnName() + ",";
                    }
                }
            }

            queryStr = queryStr.TrimEnd(',');
            queryStr += ")";

            queryStr = returnID ? queryStr + " Select SCOPE_IDENTITY()" : queryStr;

            return queryStr;
        }

        internal static List<PropertyInfo> ReturnValidProperties(this List<PropertyInfo> props, bool forSelect = false)
        {
            List<PropertyInfo> returnProps = new List<PropertyInfo>();

            foreach (PropertyInfo item in props)
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

        internal static string ToJoiner(this JoinTypes joinType)
        {
            switch (joinType)
            {
                case JoinTypes.CROSS: return "Cross Join";
                case JoinTypes.FULL: return "Full Join";
                case JoinTypes.INNER: return "Inner Join";
                case JoinTypes.LEFT: return "Left Join";
                case JoinTypes.RIGHT: return "Right Join";
                default: return "Inner Join";
            }
        }

        internal static List<dynamic> ToDynamicList(this DataTable dataTable, string className)
        {
            return ToDynamicList(ToDictionary(dataTable), GetNewObject(dataTable.Columns, className));
        }

        #region ToDynamicList

        private static List<Dictionary<string, object>> ToDictionary(DataTable dataTable)
        {
            var columns = dataTable.Columns.Cast<DataColumn>();

            var temp = dataTable.AsEnumerable().Select(dataRow => columns.Select(column =>
                                 new { Column = column.ColumnName, Value = dataRow[column] })
                             .ToDictionary(data => data.Column, data => data.Value)).ToList();

            return temp.ToList();
        }

        private static List<dynamic> ToDynamicList(List<Dictionary<string, object>> list, Type typeObj)
        {
            dynamic temp = new List<dynamic>();

            foreach (Dictionary<string, object> item in list)
            {
                object obj = Activator.CreateInstance(typeObj);

                PropertyInfo[] properties = obj.GetType().GetProperties();

                Dictionary<string, object> dictList = item;

                foreach (KeyValuePair<string, object> keyValuePair in dictList)
                {
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.Name == keyValuePair.Key)
                        {
                            if (keyValuePair.Value != null && keyValuePair.Value.GetType() != typeof(DBNull))
                            {
                                if (keyValuePair.Value is Guid)
                                {
                                    property.SetValue(obj, keyValuePair.Value, null);
                                }
                                else
                                {
                                    property.SetValue(obj, keyValuePair.Value, null);
                                }
                            }
                            break;
                        }
                    }
                }
                temp.Add(obj);
            }
            return temp;
        }

        private static Type GetNewObject(DataColumnCollection columns, string className)
        {
            AssemblyName assemblyName = new AssemblyName { Name = "YourAssembly" };
            AssemblyBuilder assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder module = assemblyBuilder.DefineDynamicModule("YourDynamicModule");
            TypeBuilder typeBuilder = module.DefineType(className, TypeAttributes.Public);

            foreach (DataColumn column in columns)
            {
                string propertyName = column.ColumnName;
                FieldBuilder field = typeBuilder.DefineField(propertyName, column.DataType, FieldAttributes.Public);
                PropertyBuilder property = typeBuilder.DefineProperty(propertyName, System.Reflection.PropertyAttributes.None, column.DataType, new[] { column.DataType });
                MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.HideBySig;
                MethodBuilder currGetPropMthdBldr = typeBuilder.DefineMethod("get_value", getSetAttr, column.DataType, Type.EmptyTypes); // new Type[] { column.DataType });
                ILGenerator currGetIL = currGetPropMthdBldr.GetILGenerator();
                currGetIL.Emit(OpCodes.Ldarg_0);
                currGetIL.Emit(OpCodes.Ldfld, field);
                currGetIL.Emit(OpCodes.Ret);
                MethodBuilder currSetPropMthdBldr = typeBuilder.DefineMethod("set_value", getSetAttr, null, new[] { column.DataType });
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

        internal static string MakeSingle(this string text, string changeText)
        {
            do
            {
                text = text.Replace(changeText + changeText, changeText);
            } while (text.Contains(changeText + changeText));

            return text;
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
