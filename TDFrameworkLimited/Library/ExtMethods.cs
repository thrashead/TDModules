using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using TDFramework.Common;

namespace TDFramework
{
    internal static class ExtMethods
    {
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

        internal static bool IsValidFunctionType<T>(this object _object)
        {
            Type typeModel = typeof(T);
            string tableName = typeModel.Name;

            Type type = _object.GetType();
            bool control = true;

            if (!type.IsEnum)
            {
                control = false;
            }

            if (type.Name != tableName + "Columns")
            {
                control = false;
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

        internal static string GetDBColumnName(this PropertyInfo _propInfo)
        {
            string columnName = _propInfo.Name;

            if (_propInfo.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "DBColumn").ToList().Count > 0)
            {
                object o = _propInfo.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "DBColumn").ToList().FirstOrDefault();

                PropertyInfo propsAttr = o.GetType().GetProperty("Name");

                if (propsAttr != null)
                {
                    columnName = propsAttr.GetValue(o, null).ToString();
                }
            }

            return columnName;
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
                firstColumn = _props.FirstOrDefault().Name;
            }

            return firstColumn;
        }

        internal static string ReturnQueryStringForInsert(this List<PropertyInfo> _props, string tableName, string _identity)
        {
            string querystring = "Insert Into " + tableName + "(";

            foreach (PropertyInfo item in _props.ReturnValidProperties())
            {
                if (item.PropertyType.InType())
                {
                    if (_identity != item.Name)
                    {
                        querystring += "[" + item.Name + "],";
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
                        querystring += "@" + item.Name + ",";
                    }
                }
            }

            querystring = querystring.TrimEnd(',');
            querystring += ")";

            return querystring;
        }

        internal static List<PropertyInfo> ReturnValidProperties(this List<PropertyInfo> _props)
        {
            List<PropertyInfo> returnProps = new List<PropertyInfo>();

            foreach (PropertyInfo item in _props)
            {
                if (item.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "NotDBColumn").ToList().Count <= 0)
                {
                    if (item.GetCustomAttributes(false).Where(a => a.ToString().Split('+').Last().Replace("Attribute", "").Split('.').Last() == "RTable").ToList().Count <= 0)
                    {
                        returnProps.Add(item);
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

        internal static ResultBox Clean(this ResultBox _resultBox)
        {
            _resultBox.Data = null;
            _resultBox.ErrorMessage = null;
            _resultBox.Result = false;

            return new ResultBox();
        }
    }
}
