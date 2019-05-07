using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Common
{
    public static class SqlTypes
    {
        enum Types
        {
            INT16,
            INT32,
            INT64,
            DECIMAL,
            DOUBLE,
            CHAR,
            CHARS,
            STRING,
            BYTE,
            BYTES,
            BOOLEAN,
            DATETIME,
            DATETIMEOFFSET,
            TIMESPAN,
            SINGLE,
            OBJECT,
            GUID,
            NONE
        }

        public static Type ReturnType(this string type)
        {
            switch (type)
            {
                case "bigint": return CreateType(Types.INT64);
                case "binary": return CreateType(Types.BYTES);
                case "bit": return CreateType(Types.BOOLEAN);
                case "char": return CreateType(Types.CHAR);
                case "date": return CreateType(Types.DATETIME);
                case "datetime": return CreateType(Types.DATETIME);
                case "datetime2": return CreateType(Types.DATETIME);
                case "DATETIMEOFFSET": return CreateType(Types.DATETIMEOFFSET);
                case "decimal": return CreateType(Types.DECIMAL);
                case "float": return CreateType(Types.DOUBLE);
                case "int": return CreateType(Types.INT32);
                case "money": return CreateType(Types.DECIMAL);
                case "nchar": return CreateType(Types.STRING);
                case "numeric": return CreateType(Types.DECIMAL);
                case "nvarchar": return CreateType(Types.STRING);
                case "nvarchar(1)": return CreateType(Types.CHAR);
                case "nchar(1)": return CreateType(Types.CHAR);
                case "ntext": return CreateType(Types.STRING);
                case "real": return CreateType(Types.SINGLE);
                case "rowversion": return CreateType(Types.BYTES);
                case "smallint": return CreateType(Types.INT16);
                case "smallmoney": return CreateType(Types.DECIMAL);
                case "sql_variant": return CreateType(Types.OBJECT);
                case "text": return CreateType(Types.STRING);
                case "time": return CreateType(Types.TIMESPAN);
                case "tinyint": return CreateType(Types.BYTE);
                case "uniqueidentifier": return CreateType(Types.GUID);
                case "varbinary": return CreateType(Types.BYTES);
                case "varbinary(1)": return CreateType(Types.BYTE);
                case "binary(1)": return CreateType(Types.BYTE);
                case "varchar": return CreateType(Types.STRING);
                case "xml": return CreateType(Types.OBJECT);
                case "cursor": return CreateType(Types.NONE);
                case "geography": return CreateType(Types.NONE);
                case "geometry": return CreateType(Types.NONE);
                case "hierarchyid": return CreateType(Types.NONE);
                case "image": return CreateType(Types.NONE);
                case "table": return CreateType(Types.NONE);
                case "timestamp": return CreateType(Types.NONE);
                case "User-defined type(UDT)": return CreateType(Types.NONE);
                default: return CreateType(Types.STRING);
            }
        }

        static Type CreateType(Types _type)
        {
            switch (_type)
            {
                case Types.INT16: return typeof(int);
                case Types.INT32: return typeof(int);
                case Types.INT64: return typeof(Int64);
                case Types.DECIMAL: return typeof(decimal);
                case Types.DOUBLE: return typeof(double);
                case Types.CHAR: return typeof(char);
                case Types.CHARS: return typeof(char[]);
                case Types.STRING: return typeof(string);
                case Types.BYTE: return typeof(byte);
                case Types.BYTES: return typeof(byte[]);
                case Types.BOOLEAN: return typeof(bool);
                case Types.DATETIME: return typeof(DateTime);
                case Types.DATETIMEOFFSET: return typeof(DateTimeOffset);
                case Types.TIMESPAN: return typeof(TimeSpan);
                case Types.SINGLE: return typeof(Single);
                case Types.OBJECT: return typeof(object);
                case Types.GUID: return typeof(Guid);
                case Types.NONE: return null;
                default: return typeof(string);
            }
        }
    }

    public static class CommonExt
    {
        public static string ReturnCSharpType(this string _type)
        {
            switch (_type)
            {
                case "Int16" : return "int";
                case "Int32" : return "int";
                case "Int64" : return "Int64";
                case "Decimal" : return "decimal";
                case "Double" : return "double";
                case "Char" : return "char";
                case "Char[]": return "char[]";
                case "String" : return "string";
                case "Byte" : return "byte";
                case "Byte[]": return "byte[]";
                case "Boolean" : return "bool";
                case "DateTime" : return "DateTime";
                case "DateTimeOffset" : return "DateTimeOffset";
                case "TimeSpan" : return "TimeSpan";
                case "Single" : return "Single";
                case "Object" : return "object";
                case "Guid" : return "Guid";
                default: return "string";
            }
        }
     }
}
