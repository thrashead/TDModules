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
using TDFramework.Common.TDModel;
using TDFramework.Library;

namespace TDFramework.Common.Internal
{
    internal sealed class TDError<T>
            where T : ITDModel
    {
        static TDError()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        internal TDError()
        {
            Columns = null;
            Values = null;
            SelectValues = null;
            Where = null;
            WhereList = new List<Where>();
        }

        internal dynamic Columns { get; set; }
        internal dynamic Values { get; set; }
        internal Select SelectValues { get; set; }
        internal Where Where { get; set; }
        internal List<Where> WhereList { get; set; }

        internal static Error ReturnError(TDError<T> tdError, MethodType methodType = MethodType.Select, ErrorTypes errorType = ErrorTypes.Columns)
        {
            Error rbError = null;

            if (tdError != null)
            {
                if (tdError.Columns != null)
                {
                    rbError = ReturnError(tdError.Columns, methodType, errorType);
                    if (rbError != null) { goto returnPoint; }
                }

                if (tdError.Values != null)
                {
                    rbError = ReturnError(tdError.Values, methodType, errorType);
                    if (rbError != null) { goto returnPoint; }
                }

                if (tdError.SelectValues != null)
                {
                    rbError = ReturnError(tdError.SelectValues);
                    if (rbError != null) { goto returnPoint; }

                    if (tdError.Columns != null)
                    {
                        rbError = ReturnError(tdError.Columns, tdError.SelectValues);
                        if (rbError != null) { goto returnPoint; }
                    }
                }

                if (tdError.Where != null)
                {
                    rbError = ReturnError(tdError.Where, methodType);
                    if (rbError != null) { goto returnPoint; }
                }

                if (tdError.WhereList != null)
                {
                    rbError = ReturnError(tdError.WhereList, methodType);
                }
            }

            returnPoint:

            return rbError;
        }

        private static Error ReturnError(Select select)
        {
            if (select == null)
            {
                return new Error() { Layer = ErrorLayers.TABLE, Message = "select parameter of Select method cannot be null." };
            }
            else
            {
                if (!((object)select.OrderColumn).InValidEnumType<T>())
                {
                    return new Error() { Layer = ErrorLayers.TABLE, Message = "OrderColumn property in select parameter of Select method is not a valid enum value." };
                }

                if (select.Aggregate != null)
                {
                    if (select.Aggregate.Column != null)
                    {
                        Type type = select.Aggregate.Column.GetType();

                        if (type.IsGenericType)
                        {
                            return new Error() { Layer = ErrorLayers.TABLE, Message = "column parameter of Aggregate constructor cannot be a generic list type." };
                        }

                        if (string.IsNullOrEmpty(select.Aggregate.Column.ToString()))
                        {
                            return new Error() { Layer = ErrorLayers.TABLE, Message = "column parameter of Aggregate constructor cannot be empty." };
                        }

                        if (!((object)select.Aggregate.Column).InValidEnumType<T>())
                        {
                            return new Error() { Layer = ErrorLayers.TABLE, Message = "column parameter of Aggregate constructor is not a valid enum value." };
                        }
                    }

                    if (select.Aggregate.GroupColumns != null)
                    {
                        if (!((object)select.Aggregate.GroupColumns).InValidEnumType<T>())
                        {
                            return new Error() { Layer = ErrorLayers.TABLE, Message = "groupColumns parameter of Aggregate constructor is not a valid enum value." };
                        }

                        if (select.Aggregate.Having != null)
                        {
                            foreach (Having item in select.Aggregate.Having)
                            {
                                if (item != null)
                                {
                                    if (item.Column == null)
                                    {
                                        return new Error() { Layer = ErrorLayers.TABLE, Message = "Column parameter of any objects in Having List object cannot be null." };
                                    }
                                    else
                                    {
                                        Type typeColumns = item.Column.GetType();

                                        if (typeColumns.IsGenericType)
                                        {
                                            return new Error() { Layer = ErrorLayers.TABLE, Message = "Column parameter of any objects in Having List object cannot be generic list" };
                                        }

                                        if (string.IsNullOrEmpty(item.Column.ToString()))
                                        {
                                            return new Error() { Layer = ErrorLayers.TABLE, Message = "Column parameter of any objects in Having List object cannot be empty." };
                                        }

                                        if (!((object)item.Column).InValidEnumType<T>())
                                        {
                                            return new Error() { Layer = ErrorLayers.TABLE, Message = "Column parameter of any objects in Having List object is not a valid enum value." };
                                        }
                                    }
                                }
                                else
                                {
                                    return new Error() { Layer = ErrorLayers.TABLE, Message = "Any objects in Having List object cannot be null." };
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        private static Error ReturnError(Where where, MethodType methodType)
        {
            if (where == null)
            {
                return new Error() { Layer = ErrorLayers.TABLE, Message = "where parameter of " + methodType.ToString() + " method cannot be null." };
            }
            else
            {
                if (where.Column == null)
                {
                    return new Error() { Layer = ErrorLayers.TABLE, Message = "Column property in where parameter of " + methodType.ToString() + " method cannot be null." };
                }
                else
                {
                    Type typeColumns = where.Column.GetType();

                    if (typeColumns.IsGenericType)
                    {
                        return new Error() { Layer = ErrorLayers.TABLE, Message = "Column property in where parameter of " + methodType.ToString() + " method cannot be generic list" };
                    }

                    if (string.IsNullOrEmpty(where.Column.ToString()))
                    {
                        return new Error() { Layer = ErrorLayers.TABLE, Message = "Column property in where parameter of " + methodType.ToString() + " method cannot be empty." };
                    }

                    if (!((object)where.Column).InValidEnumType<T>())
                    {
                        return new Error() { Layer = ErrorLayers.TABLE, Message = "Column property in where parameter of " + methodType.ToString() + " method is not a valid enum value." };
                    }
                }
            }

            return null;
        }

        private static Error ReturnError(List<Where> where, MethodType methodType)
        {
            if (where == null)
            {
                return new Error() { Layer = ErrorLayers.TABLE, Message = "whereList parameter of " + methodType.ToString() + " method cannot be null." };
            }

            foreach (Where item in where)
            {
                if (item == null)
                {
                    return new Error() { Layer = ErrorLayers.TABLE, Message = "whereList parameter of " + methodType.ToString() + " method has some null values." };
                }
                else
                {
                    if (item.Column == null)
                    {
                        return new Error() { Layer = ErrorLayers.TABLE, Message = "Column property of any objects in whereList parameter of " + methodType.ToString() + " method cannot be null." };
                    }
                    else
                    {
                        Type typeColumns = item.Column.GetType();

                        if (typeColumns.IsGenericType)
                        {
                            return new Error() { Layer = ErrorLayers.TABLE, Message = "Column property of any objects in whereList parameter of " + methodType.ToString() + " method cannot be generic list" };
                        }

                        if (string.IsNullOrEmpty(item.Column.ToString()))
                        {
                            return new Error() { Layer = ErrorLayers.TABLE, Message = "Column property of any objects in whereList parameter of " + methodType.ToString() + " method cannot be empty." };
                        }

                        if (!((object)item.Column).InValidEnumType<T>())
                        {
                            return new Error() { Layer = ErrorLayers.TABLE, Message = "Column property of any objects in whereList parameter of " + methodType.ToString() + " method has some invalid enum values." };
                        }
                    }
                }
            }

            return null;
        }

        private static Error ReturnError(dynamic columnsOrValues, MethodType methodType, ErrorTypes errorType = ErrorTypes.Columns)
        {
            if (errorType == ErrorTypes.Columns)
            {
                switch (methodType)
                {
                    case MethodType.Select:
                        if (columnsOrValues == null)
                        {
                            return new Error() { Layer = ErrorLayers.TABLE, Message = "dynamic columns parameter of Select method cannot be null." };
                        }
                        else
                        {
                            if (!Enum.TryParse(columnsOrValues.ToString(), out SelectColumns _))
                            {
                                if (!((object)columnsOrValues).InValidEnumType<T>())
                                {
                                    return new Error() { Layer = ErrorLayers.TABLE, Message = "dynamic columns parameter of Select method is not a valid enum value." };
                                }

                                Type typeColumns = columnsOrValues.GetType();

                                if (typeColumns.IsGenericType)
                                {
                                    if (columnsOrValues.Count <= 0)
                                    {
                                        return new Error() { Layer = ErrorLayers.TABLE, Message = "dynamic columns parameter of Select method is Empty" };
                                    }
                                }
                                else if (string.IsNullOrEmpty(columnsOrValues.ToString()))
                                {
                                    return new Error() { Layer = ErrorLayers.TABLE, Message = "dynamic columns parameter of Select method is Empty" };
                                }
                            }
                        }
                        break;
                    case MethodType.Update:
                        if (columnsOrValues == null)
                        {
                            return new Error() { Layer = ErrorLayers.TABLE, Message = "dynamic columns parameter of Update method cannot be null." };
                        }
                        else
                        {
                            if (!((object)columnsOrValues).InValidEnumType<T>())
                            {
                                return new Error() { Layer = ErrorLayers.TABLE, Message = "dynamic columns parameter of Update method is not a valid enum value." };
                            }

                            Type typeColumns = columnsOrValues.GetType();

                            if (typeColumns.IsGenericType)
                            {
                                if (columnsOrValues.Count <= 0)
                                {
                                    return new Error() { Layer = ErrorLayers.TABLE, Message = "dynamic columns parameter of Update method is Empty" };
                                }
                            }
                            else if (string.IsNullOrEmpty(columnsOrValues.ToString()))
                            {
                                return new Error() { Layer = ErrorLayers.TABLE, Message = "dynamic columns parameter of Update method is Empty" };
                            }
                        }
                        break;
                }
            }
            else if (errorType == ErrorTypes.Values)
            {
                if (columnsOrValues == null)
                {
                    return new Error() { Layer = ErrorLayers.TABLE, Message = "dynamic values parameter of " + methodType.ToString() + " method cannot be null." };
                }
                else if (!((object)columnsOrValues).IsValidModel<T>())
                {
                    return new Error() { Layer = ErrorLayers.TABLE, Message = "dynamic values parameter of " + methodType.ToString() + " method is not a valid model." };
                }
            }

            return null;
        }

        private static Error ReturnError(dynamic columns, Select select)
        {
            if (Enum.TryParse(columns.ToString(), out SelectColumns _))
            {
                if (select.Aggregate == null)
                {
                    return new Error() { Layer = ErrorLayers.TABLE, Message = "You can't use SelectColumns.NONE value if you don't create an aggregate function." };
                }
                else if (select.Aggregate.Column == null)
                {
                    return new Error() { Layer = ErrorLayers.TABLE, Message = "You can't use SelectColumns.NONE value if your AggregateColumn property is null." };
                }
            }

            return null;
        }

        internal static Error ReturnError(Table<T> table, string whichTable)
        {
            if (table != null)
            {
                if (table.Columns != null)
                {
                    if (!Enum.TryParse(table.Columns.ToString(), out SelectColumns _))
                    {
                        if (!((object)table.Columns).InValidEnumType<T>())
                        {
                            return new Error()
                            {
                                Layer = ErrorLayers.TABLE,
                                Message = "SelectColumns property of " + whichTable + " Table object is not a valid enum value."
                            };
                        }
                    }
                }

                if (table.SelectSettings != null)
                {
                    if (!((object)table.SelectSettings.OrderColumn).InValidEnumType<T>())
                    {
                        return new Error()
                        {
                            Layer = ErrorLayers.TABLE,
                            Message = "OrderColumn property of Select property in " + whichTable + " Table object is not a valid enum value."
                        };
                    }

                    if (table.SelectSettings.Aggregate != null)
                    {
                        if (!((object)table.SelectSettings.Aggregate.Column).InValidEnumType<T>())
                        {
                            return new Error()
                            {
                                Layer = ErrorLayers.TABLE,
                                Message = "Aggregate property's column parameter of Select property in " + whichTable + " Table object is not a valid enum value."
                            };
                        }

                        if (table.SelectSettings.Aggregate.GroupColumns != null)
                        {
                            if (!((object)table.SelectSettings.Aggregate.GroupColumns).InValidEnumType<T>())
                            {
                                return new Error()
                                {
                                    Layer = ErrorLayers.TABLE,
                                    Message = "Aggregate property's groupColumns parameter of Select property in " + whichTable + " Table object is not a valid enum value."
                                };
                            }

                            if (table.SelectSettings.Aggregate.Having != null)
                            {
                                foreach (Having item in table.SelectSettings.Aggregate.Having)
                                {
                                    if (item != null)
                                    {
                                        if (item.Column == null)
                                        {
                                            return new Error()
                                            {
                                                Layer = ErrorLayers.TABLE,
                                                Message = "Column parameter of Having List object in " + whichTable + " Table object cannot be null."
                                            };
                                        }
                                        else
                                        {
                                            Type typeColumns = item.Column.GetType();

                                            if (typeColumns.IsGenericType)
                                            {
                                                return new Error()
                                                {
                                                    Layer = ErrorLayers.TABLE,
                                                    Message = "Column parameter of Having List object in " + whichTable + " Table object cannot be generic list"
                                                };
                                            }

                                            if (string.IsNullOrEmpty(item.Column.ToString()))
                                            {
                                                return new Error()
                                                {
                                                    Layer = ErrorLayers.TABLE,
                                                    Message = "Column parameter of Having List object in " + whichTable + " Table object cannot be empty."
                                                };
                                            }

                                            if (!((object)item.Column).InValidEnumType<T>())
                                            {
                                                return new Error()
                                                {
                                                    Layer = ErrorLayers.TABLE,
                                                    Message = "Column parameter of Having List object in " + whichTable + " Table object is not a valid enum value."
                                                };
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return new Error()
                                        {
                                            Layer = ErrorLayers.TABLE,
                                            Message = "Having object in " + whichTable + " Table object cannot be null."
                                        };
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return new Error()
                    {
                        Layer = ErrorLayers.TABLE,
                        Message = "Select property of " + whichTable + " Table object cannot be null"
                    };
                }

                if (table.WhereList != null)
                {
                    foreach (Where item in table.WhereList)
                    {
                        if (!((object)item.Column).InValidEnumType<T>())
                        {
                            return new Error()
                            {
                                Layer = ErrorLayers.TABLE,
                                Message = "Column property of WhereList property in " + whichTable + " Table object has some invalid enum values."
                            };
                        }
                    }
                }
                else
                {
                    return new Error()
                    {
                        Layer = ErrorLayers.TABLE,
                        Message = "WhereList property of " + whichTable + " Table object cannot be null"
                    };
                }
            }
            else
            {
                return new Error()
                {
                    Layer = ErrorLayers.TABLE,
                    Message = whichTable + " Table object is null. Table items cannot be null."
                };
            }

            return null;
        }

        internal static Error ReturnError(dynamic relatedColumn, string whichTable)
        {
            if (relatedColumn == null)
            {
                return new Error()
                {
                    Layer = ErrorLayers.TABLE,
                    Message = "RelatedColumn properties of " + whichTable + " Table object cannot be null."
                };
            }

            if (!((object)relatedColumn).InValidEnumType<T>())
            {
                return new Error()
                {
                    Layer = ErrorLayers.TABLE,
                    Message = "RelatedColumn property of " + whichTable + " Table object is not a valid enum value."
                };
            }

            return null;
        }
    }

    internal sealed class TDError<T1, T2>
        where T1 : ITDModel
        where T2 : ITDModel
    {
        internal static Error ReturnError(Table<T1> table1, Table<T2> table2)
        {
            Error rb = null;
            bool control = true;

            if (!string.IsNullOrEmpty(table1.Alias))
            {
                if (table1.Alias == table2.Alias)
                {
                    return new Error()
                    {
                        Layer = ErrorLayers.TABLE,
                        Message = "Alias properties of Table objects cannot have the same values."
                    };
                }
            }

            if (table1.Columns != null)
            {
                if (Enum.TryParse(table1.Columns.ToString(), out SelectColumns _))
                {
                    if (table2.Columns != null)
                    {
                        if (Enum.TryParse(table2.Columns.ToString(), out SelectColumns _))
                        {
                            if (table1.SelectSettings.Aggregate == null && table2.SelectSettings.Aggregate == null)
                            {
                                control = false;
                            }
                            else if (table1.SelectSettings.Aggregate == null && table2.SelectSettings.Aggregate != null)
                            {
                                if (table2.SelectSettings.Aggregate.Column == null)
                                {
                                    control = false;
                                }
                            }
                            else if (table1.SelectSettings.Aggregate != null && table2.SelectSettings.Aggregate == null)
                            {
                                if (table1.SelectSettings.Aggregate.Column == null)
                                {
                                    control = false;
                                }
                            }
                            else if (table1.SelectSettings.Aggregate != null && table2.SelectSettings.Aggregate != null)
                            {
                                if (table1.SelectSettings.Aggregate.Column == null && table2.SelectSettings.Aggregate.Column == null)
                                {
                                    control = false;
                                }
                            }
                        }
                    }
                }
            }

            if (!control)
            {
                rb = new Error()
                {
                    Layer = ErrorLayers.TABLE,
                    Message = "You didn't select any column from any table and you don't create any aggregate function. In this case you can't create a valid querystring."
                };
            }

            return rb;
        }
    }

    internal sealed class TDError<T1, T2, T3>
        where T1 : ITDModel
        where T2 : ITDModel
        where T3 : ITDModel
    {
        internal static Error ReturnError(Table<T1> table1, Table<T2> table2, Table<T3> table3)
        {
            Error rb = null;
            bool control = true;

            if (!string.IsNullOrEmpty(table1.Alias))
            {
                if ((table1.Alias == table2.Alias) || (table1.Alias == table3.Alias) || (table2.Alias == table3.Alias))
                {
                    return new Error()
                    {
                        Layer = ErrorLayers.TABLE,
                        Message = "Alias properties of Table objects cannot have the same values."
                    };
                }
            }

            if (table1.Columns != null)
            {
                if (Enum.TryParse(table1.Columns.ToString(), out SelectColumns _))
                {
                    if (table2.Columns != null)
                    {
                        if (Enum.TryParse(table2.Columns.ToString(), out SelectColumns _))
                        {
                            if (table3.Columns != null)
                            {
                                if (Enum.TryParse(table3.Columns.ToString(), out SelectColumns _))
                                {
                                    if (table1.SelectSettings.Aggregate == null && table2.SelectSettings.Aggregate == null && table3.SelectSettings.Aggregate == null)
                                    {
                                        control = false;
                                    }

                                    if (table1.SelectSettings.Aggregate != null && table2.SelectSettings.Aggregate == null && table3.SelectSettings.Aggregate == null)
                                    {
                                        if (table1.SelectSettings.Aggregate.Column == null)
                                        {
                                            control = false;
                                        }
                                    }

                                    if (table1.SelectSettings.Aggregate == null && table2.SelectSettings.Aggregate != null && table3.SelectSettings.Aggregate == null)
                                    {
                                        if (table2.SelectSettings.Aggregate.Column == null)
                                        {
                                            control = false;
                                        }
                                    }

                                    if (table1.SelectSettings.Aggregate == null && table2.SelectSettings.Aggregate == null && table3.SelectSettings.Aggregate != null)
                                    {
                                        if (table3.SelectSettings.Aggregate.Column == null)
                                        {
                                            control = false;
                                        }
                                    }

                                    if (table1.SelectSettings.Aggregate != null && table2.SelectSettings.Aggregate != null && table3.SelectSettings.Aggregate == null)
                                    {
                                        if (table1.SelectSettings.Aggregate.Column == null && table2.SelectSettings.Aggregate.Column == null)
                                        {
                                            control = false;
                                        }
                                    }

                                    if (table1.SelectSettings.Aggregate != null && table2.SelectSettings.Aggregate == null && table3.SelectSettings.Aggregate != null)
                                    {
                                        if (table1.SelectSettings.Aggregate.Column == null && table3.SelectSettings.Aggregate.Column == null)
                                        {
                                            control = false;
                                        }
                                    }

                                    if (table1.SelectSettings.Aggregate == null && table2.SelectSettings.Aggregate != null && table3.SelectSettings.Aggregate != null)
                                    {
                                        if (table2.SelectSettings.Aggregate.Column == null && table3.SelectSettings.Aggregate.Column == null)
                                        {
                                            control = false;
                                        }
                                    }

                                    if (table1.SelectSettings.Aggregate != null && table2.SelectSettings.Aggregate != null && table3.SelectSettings.Aggregate != null)
                                    {
                                        if (table1.SelectSettings.Aggregate.Column == null && table2.SelectSettings.Aggregate.Column == null && table3.SelectSettings.Aggregate.Column == null)
                                        {
                                            control = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (!control)
            {
                rb = new Error()
                {
                    Layer = ErrorLayers.TABLE,
                    Message = "You didn't select any column from any table and you don't create any aggregate function. In this case you can't create a valid querystring."
                };
            }

            return rb;
        }
    }

    internal enum MethodType
    {
        Select,
        Insert,
        Update,
        Delete
    }

    internal enum ErrorTypes
    {
        Columns,
        Values
    }
}
