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
using System.Collections.Generic;

namespace TDFramework.Common
{
    internal sealed class TDError<T>
    {
        internal TDError()
        {
            this.Columns = null;
            this.Values = null;
            this.Select = null;
            this.Where = null;
            this.WhereList = new List<Where>();
        }

        internal dynamic Columns { get; set; }
        internal dynamic Values { get; set; }
        internal Select Select { get; set; }
        internal Where Where { get; set; }
        internal List<Where> WhereList { get; set; }

        internal static ResultBox ReturnError(TDError<T> _tdError, MethodType methodType = MethodType.Select, ErrorTypes errorType = ErrorTypes.Columns)
        {
            ResultBox rbError = null;

            if (_tdError != null)
            {
                if (_tdError.Columns != null)
                {
                    rbError = ReturnError(_tdError.Columns, methodType, errorType);
                    if (rbError != null) { goto returnPoint; }
                }

                if (_tdError.Values != null)
                {
                    rbError = ReturnError(_tdError.Values, methodType, errorType);
                    if (rbError != null) { goto returnPoint; }
                }

                if (_tdError.Select != null)
                {
                    rbError = ReturnError(_tdError.Select);
                    if (rbError != null) { goto returnPoint; }

                    if (_tdError.Columns != null)
                    {
                        rbError = ReturnError(_tdError.Columns, _tdError.Select);
                        if (rbError != null) { goto returnPoint; }
                    }
                }

                if (_tdError.Where != null)
                {
                    rbError = ReturnError(_tdError.Where, methodType);
                    if (rbError != null) { goto returnPoint; }
                }

                if (_tdError.WhereList != null)
                {
                    rbError = ReturnError(_tdError.WhereList, methodType);
                    if (rbError != null) { goto returnPoint; }
                }
            }

        returnPoint: ;
            return rbError;
        }

        private static ResultBox ReturnError(Select select)
        {
            if (select == null)
            {
                return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "select parameter of Select method cannot be null." };
            }
            else
            {
                if (!((object)select.OrderColumn).InValidEnumType<T>())
                {
                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "OrderColumn property in select parameter of Select method is not a valid enum value." };
                }

                if (select.Aggregate != null)
                {
                    if (select.Aggregate.Column != null)
                    {
                        Type type = select.Aggregate.Column.GetType();

                        if (type.IsGenericType)
                        {
                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "column parameter of Aggregate constructor cannot be a generic list type." };
                        }

                        if (String.IsNullOrEmpty(select.Aggregate.Column.ToString()))
                        {
                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "column parameter of Aggregate constructor cannot be empty." };
                        }

                        if (!((object)select.Aggregate.Column).InValidEnumType<T>())
                        {
                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "column parameter of Aggregate constructor is not a valid enum value." };
                        }
                    }

                    if (select.Aggregate.GroupColumns != null)
                    {
                        if (!((object)select.Aggregate.GroupColumns).InValidEnumType<T>())
                        {
                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "groupColumns parameter of Aggregate constructor is not a valid enum value." };
                        }

                        if (select.Aggregate.Having != null)
                        {
                            foreach (Having item in select.Aggregate.Having)
                            {
                                if (item != null)
                                {
                                    if (item.Column == null)
                                    {
                                        return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column parameter of any objects in Having List object cannot be null." };
                                    }
                                    else
                                    {
                                        Type typeColumns = item.Column.GetType();

                                        if (typeColumns.IsGenericType)
                                        {
                                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column parameter of any objects in Having List object cannot be generic list" };
                                        }

                                        if (String.IsNullOrEmpty(item.Column.ToString()))
                                        {
                                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column parameter of any objects in Having List object cannot be empty." };
                                        }

                                        if (!((object)item.Column).InValidEnumType<T>())
                                        {
                                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column parameter of any objects in Having List object is not a valid enum value." };
                                        }
                                    }
                                }
                                else
                                {
                                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Any objects in Having List object cannot be null." };
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        private static ResultBox ReturnError(Where where, MethodType methodType)
        {
            if (where == null)
            {
                return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "where parameter of " + methodType.ToString() + " method cannot be null." };
            }
            else
            {
                if (where.Column == null)
                {
                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column property in where parameter of " + methodType.ToString() + " method cannot be null." };
                }
                else
                {
                    Type typeColumns = where.Column.GetType();

                    if (typeColumns.IsGenericType)
                    {
                        return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column property in where parameter of " + methodType.ToString() + " method cannot be generic list" };
                    }

                    if (String.IsNullOrEmpty(where.Column.ToString()))
                    {
                        return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column property in where parameter of " + methodType.ToString() + " method cannot be empty." };
                    }

                    if (!((object)where.Column).InValidEnumType<T>())
                    {
                        return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column property in where parameter of " + methodType.ToString() + " method is not a valid enum value." };
                    }
                }
            }

            return null;
        }

        private static ResultBox ReturnError(List<Where> where, MethodType methodType)
        {
            if (where == null)
            {
                return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "whereList parameter of " + methodType.ToString() + " method cannot be null." };
            }

            foreach (Where item in where)
            {
                if (item == null)
                {
                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "whereList parameter of " + methodType.ToString() + " method has some null values." };
                }
                else
                {
                    if (item.Column == null)
                    {
                        return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column property of any objects in whereList parameter of " + methodType.ToString() + " method cannot be null." };
                    }
                    else
                    {
                        Type typeColumns = item.Column.GetType();

                        if (typeColumns.IsGenericType)
                        {
                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column property of any objects in whereList parameter of " + methodType.ToString() + " method cannot be generic list" };
                        }

                        if (String.IsNullOrEmpty(item.Column.ToString()))
                        {
                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column property of any objects in whereList parameter of " + methodType.ToString() + " method cannot be empty." };
                        }

                        if (!((object)item.Column).InValidEnumType<T>())
                        {
                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "Column property of any objects in whereList parameter of " + methodType.ToString() + " method has some invalid enum values." };
                        }
                    }
                }
            }

            return null;
        }

        private static ResultBox ReturnError(dynamic columnsOrValues, MethodType methodType, ErrorTypes errorType = ErrorTypes.Columns)
        {
            if (errorType == ErrorTypes.Columns)
            {
                switch (methodType)
                {
                    case MethodType.Select:
                        if (columnsOrValues == null)
                        {
                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "dynamic columns parameter of Select method cannot be null." };
                        }
                        else
                        {
                            SelectColumns selectColumns;
                            if (!Enum.TryParse(columnsOrValues.ToString(), out selectColumns))
                            {
                                if (!((object)columnsOrValues).InValidEnumType<T>())
                                {
                                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "dynamic columns parameter of Select method is not a valid enum value." };
                                }

                                Type typeColumns = columnsOrValues.GetType();

                                if (typeColumns.IsGenericType)
                                {
                                    if (columnsOrValues.Count <= 0)
                                    {
                                        return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "dynamic columns parameter of Select method is Empty" };
                                    }
                                }
                                else if (String.IsNullOrEmpty(columnsOrValues.ToString()))
                                {
                                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "dynamic columns parameter of Select method is Empty" };
                                }
                            }
                        }
                        break;
                    case MethodType.Update:
                        if (columnsOrValues == null)
                        {
                            return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "dynamic columns parameter of Update method cannot be null." };
                        }
                        else
                        {
                            if (!((object)columnsOrValues).InValidEnumType<T>())
                            {
                                return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "dynamic columns parameter of Update method is not a valid enum value." };
                            }

                            Type typeColumns = columnsOrValues.GetType();

                            if (typeColumns.IsGenericType)
                            {
                                if (columnsOrValues.Count <= 0)
                                {
                                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "dynamic columns parameter of Update method is Empty" };
                                }
                            }
                            else if (String.IsNullOrEmpty(columnsOrValues.ToString()))
                            {
                                return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "dynamic columns parameter of Update method is Empty" };
                            }
                        }
                        break;
                }
            }
            else if (errorType == ErrorTypes.Values)
            {
                if (columnsOrValues == null)
                {
                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "dynamic values parameter of " + methodType.ToString() + " method cannot be null." };
                }
                else if (!((object)columnsOrValues).IsValidModel<T>())
                {
                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "dynamic values parameter of " + methodType.ToString() + " method is not a valid model." };
                }
            }

            return null;
        }

        private static ResultBox ReturnError(dynamic columns, Select select)
        {
            SelectColumns selectColumns;
            if (Enum.TryParse(columns.ToString(), out selectColumns))
            {
                if (select.Aggregate == null)
                {
                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "You can't use SelectColumns.NONE value if you don't create an aggregate function." };
                }
                else if (select.Aggregate.Column == null)
                {
                    return new ResultBox() { ErrorLayer = ErrorLayers.TDHELPER, Result = false, ErrorMessage = "You can't use SelectColumns.NONE value if your AggregateColumn property is null." };
                }
            }

            return null;
        }
    }

    internal sealed class TDError<T1, T2>
    {
        internal static ResultBox ReturnError<T>(Table table, string whichTable)
        {
            if (table != null)
            {
                if (table.RelatedColumn == null)
                {
                    return new ResultBox()
                    {
                        ErrorLayer = ErrorLayers.TDHELPER,
                        ErrorMessage = "RelatedColumn properties of " + whichTable + " Table object cannot be null.",
                        Result = false
                    };
                }

                if (!((object)table.RelatedColumn).InValidEnumType<T>())
                {
                    return new ResultBox()
                    {
                        ErrorLayer = ErrorLayers.TDHELPER,
                        ErrorMessage = "RelatedColumn property of " + whichTable + " Table object is not a valid enum value.",
                        Result = false
                    };
                }

                SelectColumns selectColumns;

                if (table.SelectColumns != null)
                {
                    if (!Enum.TryParse(table.SelectColumns.ToString(), out selectColumns))
                    {
                        if (!((object)table.SelectColumns).InValidEnumType<T>())
                        {
                            return new ResultBox()
                            {
                                ErrorLayer = ErrorLayers.TDHELPER,
                                ErrorMessage = "SelectColumns property of " + whichTable + " Table object is not a valid enum value.",
                                Result = false
                            };
                        }
                    }
                }

                if (table.Select != null)
                {
                    if (!((object)table.Select.OrderColumn).InValidEnumType<T>())
                    {
                        return new ResultBox()
                        {
                            ErrorLayer = ErrorLayers.TDHELPER,
                            ErrorMessage = "OrderColumn property of Select property in " + whichTable + " Table object is not a valid enum value.",
                            Result = false
                        };
                    }

                    if (table.Select.Aggregate != null)
                    {
                        if (!((object)table.Select.Aggregate.Column).InValidEnumType<T>())
                        {
                            return new ResultBox()
                            {
                                ErrorLayer = ErrorLayers.TDHELPER,
                                ErrorMessage = "Aggregate property's column parameter of Select property in " + whichTable + " Table object is not a valid enum value.",
                                Result = false
                            };
                        }

                        if (table.Select.Aggregate.GroupColumns != null)
                        {
                            if (!((object)table.Select.Aggregate.GroupColumns).InValidEnumType<T>())
                            {
                                return new ResultBox()
                                {
                                    ErrorLayer = ErrorLayers.TDHELPER,
                                    ErrorMessage = "Aggregate property's groupColumns parameter of Select property in " + whichTable + " Table object is not a valid enum value.",
                                    Result = false
                                };
                            }

                            if (table.Select.Aggregate.Having != null)
                            {
                                foreach (Having item in table.Select.Aggregate.Having)
                                {
                                    if (item != null)
                                    {
                                        if (item.Column == null)
                                        {
                                            return new ResultBox()
                                            {
                                                ErrorLayer = ErrorLayers.TDHELPER,
                                                ErrorMessage = "Column parameter of Having List object in " + whichTable + " Table object cannot be null.",
                                                Result = false
                                            };
                                        }
                                        else
                                        {
                                            Type typeColumns = item.Column.GetType();

                                            if (typeColumns.IsGenericType)
                                            {
                                                return new ResultBox()
                                                {
                                                    ErrorLayer = ErrorLayers.TDHELPER,
                                                    ErrorMessage = "Column parameter of Having List object in " + whichTable + " Table object cannot be generic list",
                                                    Result = false
                                                };
                                            }

                                            if (String.IsNullOrEmpty(item.Column.ToString()))
                                            {
                                                return new ResultBox()
                                                {
                                                    ErrorLayer = ErrorLayers.TDHELPER,
                                                    ErrorMessage = "Column parameter of Having List object in " + whichTable + " Table object cannot be empty.",
                                                    Result = false
                                                };
                                            }

                                            if (!((object)item.Column).InValidEnumType<T>())
                                            {
                                                return new ResultBox()
                                                {
                                                    ErrorLayer = ErrorLayers.TDHELPER,
                                                    ErrorMessage = "Column parameter of Having List object in " + whichTable + " Table object is not a valid enum value.",
                                                    Result = false
                                                };
                                            }
                                        }
                                    }
                                    else
                                    {
                                        return new ResultBox()
                                        {
                                            ErrorLayer = ErrorLayers.TDHELPER,
                                            ErrorMessage = "Having object in " + whichTable + " Table object cannot be null.",
                                            Result = false
                                        };
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    return new ResultBox()
                    {
                        ErrorLayer = ErrorLayers.TDHELPER,
                        ErrorMessage = "Select property of " + whichTable + " Table object cannot be null",
                        Result = false
                    };
                }

                if (table.WhereList != null)
                {
                    foreach (Where item in table.WhereList)
                    {
                        if (!((object)item.Column).InValidEnumType<T>())
                        {
                            return new ResultBox()
                            {
                                ErrorLayer = ErrorLayers.TDHELPER,
                                ErrorMessage = "Column property of WhereList property in " + whichTable + " Table object has some invalid enum values.",
                                Result = false
                            };
                        }
                    }
                }
                else
                {
                    return new ResultBox()
                    {
                        ErrorLayer = ErrorLayers.TDHELPER,
                        ErrorMessage = "WhereList property of " + whichTable + " Table object cannot be null",
                        Result = false
                    };
                }
            }
            else
            {
                return new ResultBox()
                {
                    ErrorLayer = ErrorLayers.TDHELPER,
                    ErrorMessage = whichTable + " Table object is null. Table items cannot be null.",
                    Result = false
                };
            }

            return null;
        }

        internal static ResultBox ReturnError(Table table1, Table table2)
        {
            bool control = true;
            SelectColumns selectColumns;

            if (!String.IsNullOrEmpty(table1.Alias))
            {
                if (table1.Alias == table2.Alias)
                {
                    return new ResultBox()
                    {
                        ErrorLayer = ErrorLayers.TDHELPER,
                        ErrorMessage = "Alias properties of Table objects cannot have the same values.",
                        Result = false
                    };
                }
            }

            if (table1.SelectColumns != null)
            {
                if (Enum.TryParse(table1.SelectColumns.ToString(), out selectColumns))
                {
                    if (table2.SelectColumns != null)
                    {
                        if (Enum.TryParse(table2.SelectColumns.ToString(), out selectColumns))
                        {
                            if (table1.Select.Aggregate == null && table2.Select.Aggregate == null)
                            {
                                control = false;
                            }
                            else if (table1.Select.Aggregate == null && table2.Select.Aggregate != null)
                            {
                                if (table2.Select.Aggregate.Column == null)
                                {
                                    control = false;
                                }
                            }
                            else if (table1.Select.Aggregate != null && table2.Select.Aggregate == null)
                            {
                                if (table1.Select.Aggregate.Column == null)
                                {
                                    control = false;
                                }
                            }
                            else if (table1.Select.Aggregate != null && table2.Select.Aggregate != null)
                            {
                                if (table1.Select.Aggregate.Column == null && table2.Select.Aggregate.Column == null)
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
                return new ResultBox()
                {
                    ErrorLayer = ErrorLayers.TDHELPER,
                    ErrorMessage = "You didn't select any column from any table and you don't create any aggregate function. In this case you can't create a valid querystring.",
                    Result = false
                };
            }

            return null;
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
