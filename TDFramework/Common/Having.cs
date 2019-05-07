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
using System.Linq;
using System.Data.SqlClient;
using TDFramework.Common.Internal;
using TDFramework.Library;

namespace TDFramework.Common
{
    public sealed class Having
    {
        public dynamic Column { get; set; }
        public List<dynamic> Values { get; set; }
        public bool Not { get; set; }
        public Knots Knot { get; set; }
        public Operators Operator { get; set; }
        public Aggregates Aggregate { get; set; }
        public Parantheses Parantheses { get; set; }

        static Having()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public Having()
        {
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = false;
            Knot = Knots.AND;
            Aggregate = Aggregates.COUNT;
            Parantheses = null;
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = false;
            Knot = Knots.AND;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = false;
            Knot = Knots.AND;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, bool not)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = not;
            Knot = Knots.AND;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Knots knot)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = false;
            Knot = knot;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = false;
            Knot = Knots.AND;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, bool not)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = not;
            Knot = Knots.AND;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, Knots knot)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = false;
            Knot = knot;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = false;
            Knot = Knots.AND;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, bool not, Knots knot)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = not;
            Knot = knot;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, bool not, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = not;
            Knot = Knots.AND;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Knots knot, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = false;
            Knot = knot;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, bool not, Knots knot)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = not;
            Knot = knot;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, bool not, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = not;
            Knot = Knots.AND;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, Knots knot, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = false;
            Knot = knot;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, bool not, Knots knot, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = Operators.EQUAL;
            Not = not;
            Knot = knot;
            Aggregate = aggregate;
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

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, bool not, Knots knot, Parantheses parantheses)
        {
            Column = column;
            Values = new List<dynamic>();
            Operator = operatorr;
            Not = not;
            Knot = knot;
            Aggregate = aggregate;
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

        internal static HavingValues CreateHaving(List<Having> having, string alias = "")
        {
            HavingValues cv = new HavingValues();

            alias = alias == "" ? "" : alias + ".";

            foreach (Having item in having)
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

                HavingValues tempCV;

                switch (item.Operator)
                {
                    case Operators.EQUAL:
                        tempCV = CreateEqualHaving(item, not, alias);
                        break;
                    case Operators.GREATER:
                        tempCV = CreateGreaterHaving(item, not, alias);
                        break;
                    case Operators.GREATEREQUAL:
                        tempCV = CreateGreaterEqualHaving(item, not, alias);
                        break;
                    case Operators.SMALLER:
                        tempCV = CreateSmallerHaving(item, not, alias);
                        break;
                    case Operators.SMALLEREQUAL:
                        tempCV = CreateSmallerEqualHaving(item, not, alias);
                        break;
                    case Operators.BETWEEN:
                        tempCV = CreateBetweenHaving(item, not, alias);
                        break;
                    case Operators.IN:
                        tempCV = CreateInHaving(item, not, alias);
                        break;
                    default:
                        tempCV = CreateEqualHaving(item, not, alias);
                        break;
                }

                cv.QueryString += tempCV.QueryString;
                cv.Parameters.AddRange(tempCV.Parameters);
            }

            cv.QueryString = cv.QueryString.TrimStart(' ').Remove(0, cv.QueryString.TrimStart(' ').IndexOf(' ') + 1);

            return cv;
        }

        private static HavingValues CreateEqualHaving(Having having, string not, string alias = "")
        {
            HavingValues cv = new HavingValues();
            string paramName = having.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (having.Parantheses != null)
            {
                for (int i = 1; i <= having.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= having.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + having.Knot.ToString() + " " + openParantheses + "(" + not + " " + having.Aggregate.ToShortAggregateName() + "(" + alias + "[" + having.Column.ToString() + "]) = @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = having.Values.FirstOrDefault() });

            return cv;
        }

        private static HavingValues CreateGreaterHaving(Having having, string not, string alias = "")
        {
            HavingValues cv = new HavingValues();
            string paramName = having.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (having.Parantheses != null)
            {
                for (int i = 1; i <= having.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= having.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + having.Knot.ToString() + " " + openParantheses + "(" + not + " " + having.Aggregate.ToShortAggregateName() + "(" + alias + "[" + having.Column.ToString() + "]) > @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = having.Values.FirstOrDefault() });

            return cv;
        }

        private static HavingValues CreateGreaterEqualHaving(Having having, string not, string alias = "")
        {
            HavingValues cv = new HavingValues();
            string paramName = having.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (having.Parantheses != null)
            {
                for (int i = 1; i <= having.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= having.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + having.Knot.ToString() + " " + openParantheses + "(" + not + " " + having.Aggregate.ToShortAggregateName() + "(" + alias + "[" + having.Column.ToString() + "]) >= @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = having.Values.FirstOrDefault() });

            return cv;
        }

        private static HavingValues CreateSmallerHaving(Having having, string not, string alias = "")
        {
            HavingValues cv = new HavingValues();
            string paramName = having.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (having.Parantheses != null)
            {
                for (int i = 1; i <= having.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= having.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + having.Knot.ToString() + " " + openParantheses + "(" + not + " " + having.Aggregate.ToShortAggregateName() + "(" + alias + "[" + having.Column.ToString() + "]) < @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = having.Values.FirstOrDefault() });

            return cv;
        }

        private static HavingValues CreateSmallerEqualHaving(Having having, string not, string alias = "")
        {
            HavingValues cv = new HavingValues();
            string paramName = having.Column.ToString() + Guider.GetGuid(5);

            string openParantheses = "";
            string closedParantheses = "";

            if (having.Parantheses != null)
            {
                for (int i = 1; i <= having.Parantheses.OpenCount; i++)
                {
                    openParantheses += "(";
                }

                for (int i = 1; i <= having.Parantheses.ClosedCount; i++)
                {
                    closedParantheses += ")";
                }
            }

            cv.QueryString += " " + having.Knot.ToString() + " " + openParantheses + "(" + not + " " + having.Aggregate.ToShortAggregateName() + "(" + alias + "[" + having.Column.ToString() + "]) <= @" + paramName + ")" + closedParantheses;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = having.Values.FirstOrDefault() });

            return cv;
        }

        private static HavingValues CreateBetweenHaving(Having having, string not, string alias = "")
        {
            HavingValues cv = new HavingValues();
            dynamic[] values = having.Values.ToArray();

            if (values.Length > 1)
            {
                string paramName = having.Column.ToString() + Guider.GetGuid(5);
                string paramName2 = having.Column.ToString() + Guider.GetGuid(5);

                string openParantheses = "";
                string closedParantheses = "";

                if (having.Parantheses != null)
                {
                    for (int i = 1; i <= having.Parantheses.OpenCount; i++)
                    {
                        openParantheses += "(";
                    }

                    for (int i = 1; i <= having.Parantheses.ClosedCount; i++)
                    {
                        closedParantheses += ")";
                    }
                }

                cv.QueryString += " " + having.Knot.ToString() + " " + openParantheses + "(" + having.Aggregate.ToShortAggregateName() + "(" + alias + "[" + having.Column.ToString() + "]) " + not + " Between @" + paramName + " And @" + paramName2 + ")" + closedParantheses;
                cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = values[0] });
                cv.Parameters.Add(new SqlParameter() { ParameterName = paramName2, Value = values[1] });
            }

            return cv;
        }

        private static HavingValues CreateInHaving(Having having, string not, string alias = "")
        {
            HavingValues cv = new HavingValues();

            if (having.Values.Count > 0)
            {
                string openParantheses = "";
                string closedParantheses = "";

                if (having.Parantheses != null)
                {
                    for (int i = 1; i <= having.Parantheses.OpenCount; i++)
                    {
                        openParantheses += "(";
                    }

                    for (int i = 1; i <= having.Parantheses.ClosedCount; i++)
                    {
                        closedParantheses += ")";
                    }
                }

                cv.QueryString += " " + having.Knot.ToString() + " " + openParantheses + "(" + having.Aggregate.ToShortAggregateName() + "(" + alias + "[" + having.Column.ToString() + "]) " + not + " In(";

                foreach (dynamic item in having.Values)
                {
                    string paramName = having.Column.ToString() + Guider.GetGuid(5);

                    cv.QueryString += "@" + paramName + ", ";
                    cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = item });
                }

                cv.QueryString = cv.QueryString.Trim().TrimEnd(',');
                cv.QueryString += "))" + closedParantheses;
            }

            return cv;
        }
    }

    internal sealed class HavingValues
    {
        static HavingValues()
        {
            AppDomain.CurrentDomain.UnhandledException += delegate
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public HavingValues()
        {
            QueryString = "";
            Parameters = new List<SqlParameter>();
        }

        internal string QueryString { get; set; }
        internal List<SqlParameter> Parameters { get; set; }
    }
}
