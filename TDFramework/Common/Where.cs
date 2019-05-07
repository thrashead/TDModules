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
using System.Data.SqlClient;
using System.Linq;
using TDFramework.Common.Internal;

namespace TDFramework.Common
{
    public sealed class Where
    {
        public dynamic Column { get; set; }
        public List<dynamic> Values { get; set; }
        public bool Not { get; set; }
        public Knots Knot { get; set; }
        public Operators Operator { get; set; }
        public Parantheses Parantheses { get; set; }

        static Where()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Where()
        {
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = false;
            Knot = Knots.AND;
            Parantheses = null;
        }

        public Where(dynamic column, dynamic values)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = false;
            Knot = Knots.AND;
            Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Operators operatorr)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = false;
            Knot = Knots.AND;
            Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, bool not)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = not;
            Knot = Knots.AND;
            Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Knots knot)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = false;
            Knot = knot;
            Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = false;
            Knot = Knots.AND;
            Parantheses = parantheses;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Operators operatorr, bool not)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = not;
            Knot = Knots.AND;
            Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Operators operatorr, Knots knot)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = false;
            Knot = knot;
            Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Operators operatorr, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = false;
            Knot = Knots.AND;
            Parantheses = parantheses;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, bool not, Knots knot)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = not;
            Knot = knot;
            Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, bool not, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = not;
            Knot = Knots.AND;
            Parantheses = parantheses;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Knots knot, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = false;
            Knot = knot;
            Parantheses = parantheses;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Operators operatorr, bool not, Knots knot)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = not;
            Knot = knot;
            Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Operators operatorr, bool not, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = not;
            Knot = Knots.AND;
            Parantheses = parantheses;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Operators operatorr, Knots knot, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = false;
            Knot = knot;
            Parantheses = parantheses;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, bool not, Knots knot, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = not;
            Knot = knot;
            Parantheses = parantheses;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        public Where(dynamic column, dynamic values, Operators operatorr, bool not, Knots knot, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = not;
            Knot = knot;
            Parantheses = parantheses;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    Values.Add(item);
                }
            }
            else
            {
                Values.Add(values);
            }
        }

        internal static WhereValues CreateWhere(List<Where> whereList, string alias = "")
        {
            WhereValues cv = new WhereValues();

            alias = alias == "" ? "" : alias + ".";

            foreach (Where item in whereList)
            {
                string not = item.Not == false ? "" : "Not";

                begin:
                int i = 0;
                foreach (dynamic value in item.Values)
                {
                    if (value != null)
                    {
                        if (value is bool)
                        {
                            item.Values[i] = value.ToString().ToLower().Replace("true", "1").Replace("false", "0");
                            goto begin;
                        }
                    }

                    i++;
                }

                WhereValues tempCV;

                switch (item.Operator)
                {
                    case Operators.EQUAL:
                        tempCV = CreateEqualWhere(item, not, alias);
                        break;
                    case Operators.GREATER:
                        tempCV = CreateGreaterWhere(item, not, alias);
                        break;
                    case Operators.GREATEREQUAL:
                        tempCV = CreateGreaterEqualWhere(item, not, alias);
                        break;
                    case Operators.SMALLER:
                        tempCV = CreateSmallerWhere(item, not, alias);
                        break;
                    case Operators.SMALLEREQUAL:
                        tempCV = CreateSmallerEqualWhere(item, not, alias);
                        break;
                    case Operators.LIKE:
                        tempCV = CreateLikeWhere(item, not, alias);
                        break;
                    case Operators.STARTLIKE:
                        tempCV = CreateStartLikeWhere(item, not, alias);
                        break;
                    case Operators.ENDLIKE:
                        tempCV = CreateEndLikeWhere(item, not, alias);
                        break;
                    case Operators.EXACTLIKE:
                        tempCV = CreateStartEndLikeWhere(item, not, alias);
                        break;
                    case Operators.BETWEEN:
                        tempCV = CreateBetweenWhere(item, not, alias);
                        break;
                    case Operators.IN:
                        tempCV = CreateInWhere(item, not, alias);
                        break;
                    default:
                        tempCV = CreateEqualWhere(item, not, alias);
                        break;
                }

                cv.QueryString += tempCV.QueryString;
                cv.Parameters.AddRange(tempCV.Parameters);
            }

            cv.QueryString = cv.QueryString.TrimStart(' ').Remove(0, cv.QueryString.TrimStart(' ').IndexOf(' ') + 1);

            return cv;
        }

        private static WhereValues CreateEqualWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();
            string paramName = where.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (where.Parantheses != null)
            {
                for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + not + " " + alias + "[" + where.Column.ToString() + "] = @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = where.Values.FirstOrDefault() });

            return cv;
        }

        private static WhereValues CreateGreaterWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();
            string paramName = where.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (where.Parantheses != null)
            {
                for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + not + " " + alias + "[" + where.Column.ToString() + "] > @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = where.Values.FirstOrDefault() });

            return cv;
        }

        private static WhereValues CreateGreaterEqualWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();
            string paramName = where.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (where.Parantheses != null)
            {
                for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + not + " " + alias + "[" + where.Column.ToString() + "] >= @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = where.Values.FirstOrDefault() });

            return cv;
        }

        private static WhereValues CreateSmallerWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();
            string paramName = where.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (where.Parantheses != null)
            {
                for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + not + " " + alias + "[" + where.Column.ToString() + "] < @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = where.Values.FirstOrDefault() });

            return cv;
        }

        private static WhereValues CreateSmallerEqualWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();
            string paramName = where.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (where.Parantheses != null)
            {
                for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + not + " " + alias + "[" + where.Column.ToString() + "] <= @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = where.Values.FirstOrDefault() });

            return cv;
        }

        private static WhereValues CreateLikeWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();
            string paramName = where.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (where.Parantheses != null)
            {
                for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + alias + "[" + where.Column.ToString() + "] " + not + " Like @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = "%" + where.Values.FirstOrDefault() + "%" });

            return cv;
        }

        private static WhereValues CreateStartLikeWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();
            string paramName = where.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (where.Parantheses != null)
            {
                for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + alias + "[" + where.Column.ToString() + "] " + not + " Like @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = where.Values.FirstOrDefault() + "%" });

            return cv;
        }

        private static WhereValues CreateEndLikeWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();
            string paramName = where.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (where.Parantheses != null)
            {
                for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + alias + "[" + where.Column.ToString() + "] " + not + " Like @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = "%" + where.Values.FirstOrDefault() });

            return cv;
        }

        private static WhereValues CreateStartEndLikeWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();
            string paramName = where.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (where.Parantheses != null)
            {
                for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + alias + "[" + where.Column.ToString() + "] " + not + " Like @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = where.Values.FirstOrDefault() });

            return cv;
        }

        private static WhereValues CreateBetweenWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();
            dynamic[] values = where.Values.ToArray();

            if (values.Length > 1)
            {
                string paramName = where.Column.ToString() + Guider.GetGuid(5);
                string paramName2 = where.Column.ToString() + Guider.GetGuid(5);

                string openParantheses = "";
                string closedParantheses = "";

                if (where.Parantheses != null)
                {
                    for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                    {
                        openParantheses += "(";
                    }

                    for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                    {
                        closedParantheses += ")";
                    }
                }

                cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + alias + "[" + where.Column.ToString() + "] " + not + " Between @" + paramName + " And @" + paramName2 + ")" + closedParantheses;
                cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = values[0] });
                cv.Parameters.Add(new SqlParameter() { ParameterName = paramName2, Value = values[1] });
            }

            return cv;
        }

        private static WhereValues CreateInWhere(Where where, string not, string alias = "")
        {
            WhereValues cv = new WhereValues();

            if (where.Values.Count > 0)
            {
                string openParantheses = "";
                string closedParantheses = "";

                if (where.Parantheses != null)
                {
                    for (int i = 1; i <= where.Parantheses.OpenCount; i++)
                    {
                        openParantheses += "(";
                    }

                    for (int i = 1; i <= where.Parantheses.ClosedCount; i++)
                    {
                        closedParantheses += ")";
                    }
                }

                cv.QueryString += " " + where.Knot.ToString() + " " + openParantheses + "(" + alias + "[" + where.Column.ToString() + "] " + not + " In(";

                foreach (dynamic item in where.Values)
                {
                    string paramName = where.Column.ToString() + Guider.GetGuid(5);

                    cv.QueryString += "@" + paramName + ", ";
                    cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = item });
                }

                cv.QueryString = cv.QueryString.Trim().TrimEnd(',');
                cv.QueryString += "))" + closedParantheses;
            }

            return cv;
        }
    }

    public sealed class Parantheses
    {
        static Parantheses()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public int OpenCount { get; set; }
        public int ClosedCount { get; set; }
    }

    public enum Operators
    {
        EQUAL,
        GREATER,
        GREATEREQUAL,
        SMALLER,
        SMALLEREQUAL,
        LIKE,
        STARTLIKE,
        ENDLIKE,
        EXACTLIKE,
        BETWEEN,
        IN
    }

    public enum Knots
    {
        AND,
        OR
    }

    internal sealed class WhereValues
    {
        static WhereValues()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public WhereValues()
        {
            QueryString = "";
            Parameters = new List<SqlParameter>();
        }

        internal string QueryString { get; set; }
        internal List<SqlParameter> Parameters { get; set; }
    }
}
