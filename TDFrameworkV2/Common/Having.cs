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
using System.Linq;
using System.Data.SqlClient;

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
        public Paranthesis Parantheses { get; set; }

        static Having()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

		public Having()
		{
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;
            this.Knot = Knots.AND;
            this.Aggregate = Aggregates.COUNT;
            this.Parantheses = null;
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;
            this.Knot = Knots.AND;
            this.Aggregate = aggregate;
            this.Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = false;
            this.Knot = Knots.AND;
            this.Aggregate = aggregate;
            this.Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, bool not)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = not;
            this.Knot = Knots.AND;
            this.Aggregate = aggregate;
            this.Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, Knots knot)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;
            this.Knot = knot;
            this.Aggregate = aggregate;
            this.Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;
            this.Knot = Knots.AND;
            this.Aggregate = aggregate;
            this.Parantheses = paranthesis;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, bool not)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = not;
            this.Knot = Knots.AND;
            this.Aggregate = aggregate;
            this.Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, Knots knot)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = false;
            this.Knot = knot;
            this.Aggregate = aggregate;
            this.Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = false;
            this.Knot = Knots.AND;
            this.Aggregate = aggregate;
            this.Parantheses = paranthesis;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, bool not, Knots knot)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = not;
            this.Knot = knot;
            this.Aggregate = aggregate;
            this.Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, bool not, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = not;
            this.Knot = Knots.AND;
            this.Aggregate = aggregate;
            this.Parantheses = paranthesis;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, Knots knot, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;
            this.Knot = knot;
            this.Aggregate = aggregate;
            this.Parantheses = paranthesis;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, bool not, Knots knot)
		{
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = not;
            this.Knot = knot;
            this.Aggregate = aggregate;
            this.Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
		}

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, bool not, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = not;
            this.Knot = Knots.AND;
            this.Aggregate = aggregate;
            this.Parantheses = paranthesis;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, Knots knot, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = false;
            this.Knot = knot;
            this.Aggregate = aggregate;
            this.Parantheses = paranthesis;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, bool not, Knots knot, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = not;
            this.Knot = knot;
            this.Aggregate = aggregate;
            this.Parantheses = paranthesis;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        public Having(dynamic column, dynamic values, Aggregates aggregate, Operators operatorr, bool not, Knots knot, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = not;
            this.Knot = knot;
            this.Aggregate = aggregate;
            this.Parantheses = paranthesis;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if (values.GetType().BaseType.Name == "Array")
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else
            {
                this.Values.Add(values);
            }
        }

        internal static HavingValues CreateHaving(List<Having> _having, string _alias = "")
        {
            HavingValues cv = new HavingValues();
            HavingValues tempCV = new HavingValues();

            _alias = _alias == "" ? "" : _alias + ".";

            foreach (Having item in _having)
            {
                string not = item.Not == false ? "" : "Not";

            begin: ;
                int i = 0;
                foreach (dynamic value in item.Values)
                {
                    if (value != null)
                    {
                        if (value.GetType() == typeof(bool))
                        {
                            item.Values[i] = value.ToString().ToLower().Replace("true", "1").Replace("false", "0");
                            goto begin;
                        }
                    }

                    i++;
                }

                switch (item.Operator)
                {
                    case Operators.EQUAL:
                        tempCV = CreateEqualHaving(item, not, _alias);
                        break;
                    case Operators.GREATER:
                        tempCV = CreateGreaterHaving(item, not, _alias);
                        break;
                    case Operators.GREATEREQUAL:
                        tempCV = CreateGreaterEqualHaving(item, not, _alias);
                        break;
                    case Operators.SMALLER:
                        tempCV = CreateSmallerHaving(item, not, _alias);
                        break;
                    case Operators.SMALLEREQUAL:
                        tempCV = CreateSmallerEqualHaving(item, not, _alias);
                        break;
                    case Operators.BETWEEN:
                        tempCV = CreateBetweenHaving(item, not, _alias);
                        break;
                    case Operators.IN:
                        tempCV = CreateInHaving(item, not, _alias);
                        break;
                    default:
                        tempCV = CreateEqualHaving(item, not, _alias);
                        break;
                }

                cv.QueryString += tempCV.QueryString;
                cv.Parameters.AddRange(tempCV.Parameters);
            }

            cv.QueryString = cv.QueryString.TrimStart(' ').Remove(0, cv.QueryString.TrimStart(' ').IndexOf(' ') + 1);

            return cv;
        }

        private static HavingValues CreateEqualHaving(Having _having, string not, string _alias = "")
        {
            HavingValues cv = new HavingValues();
            string paramName = _having.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_having.Parantheses != null)
            {
                for (int i = 1; i <= _having.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _having.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _having.Knot.ToString() + " " + openParanthesis + "(" + not + " " + _having.Aggregate.ToShortAggregateName() + "(" + _alias + "[" + _having.Column.ToString() + "]) = @" + paramName + ")" + closedParanthesis;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _having.Values.FirstOrDefault() });

            return cv;
        }

        private static HavingValues CreateGreaterHaving(Having _having, string not, string _alias = "")
        {
            HavingValues cv = new HavingValues();
            string paramName = _having.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_having.Parantheses != null)
            {
                for (int i = 1; i <= _having.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _having.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _having.Knot.ToString() + " " + openParanthesis + "(" + not + " " + _having.Aggregate.ToShortAggregateName() + "(" + _alias + "[" + _having.Column.ToString() + "]) > @" + paramName + ")" + closedParanthesis;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _having.Values.FirstOrDefault() });

            return cv;
        }

        private static HavingValues CreateGreaterEqualHaving(Having _having, string not, string _alias = "")
        {
            HavingValues cv = new HavingValues();
            string paramName = _having.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_having.Parantheses != null)
            {
                for (int i = 1; i <= _having.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _having.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _having.Knot.ToString() + " " + openParanthesis + "(" + not + " " + _having.Aggregate.ToShortAggregateName() + "(" + _alias + "[" + _having.Column.ToString() + "]) >= @" + paramName + ")" + closedParanthesis;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _having.Values.FirstOrDefault() });

            return cv;
        }

        private static HavingValues CreateSmallerHaving(Having _having, string not, string _alias = "")
        {
            HavingValues cv = new HavingValues();
            string paramName = _having.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_having.Parantheses != null)
            {
                for (int i = 1; i <= _having.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _having.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _having.Knot.ToString() + " " + openParanthesis + "(" + not + " " + _having.Aggregate.ToShortAggregateName() + "(" + _alias + "[" + _having.Column.ToString() + "]) < @" + paramName + ")" + closedParanthesis;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _having.Values.FirstOrDefault() });

            return cv;
        }

        private static HavingValues CreateSmallerEqualHaving(Having _having, string not, string _alias = "")
        {
            HavingValues cv = new HavingValues();
            string paramName = _having.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_having.Parantheses != null)
            {
                for (int i = 1; i <= _having.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _having.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _having.Knot.ToString() + " " + openParanthesis + "(" + not + " " + _having.Aggregate.ToShortAggregateName() + "(" + _alias + "[" + _having.Column.ToString() + "]) <= @" + paramName + ")" + closedParanthesis;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _having.Values.FirstOrDefault() });

            return cv;
        }

        private static HavingValues CreateBetweenHaving(Having _having, string not, string _alias = "")
        {
            HavingValues cv = new HavingValues();
            dynamic[] values = _having.Values.ToArray();

            if (values.Length > 1)
            {
                string paramName = _having.Column.ToString() + Guider.GetGuid(5);
                string paramName2 = _having.Column.ToString() + Guider.GetGuid(5);

                string openParanthesis = "";
                string closedParanthesis = "";

                if (_having.Parantheses != null)
                {
                    for (int i = 1; i <= _having.Parantheses.OpenCount; i++)
                    {
                        openParanthesis += "(";
                    }

                    for (int i = 1; i <= _having.Parantheses.ClosedCount; i++)
                    {
                        closedParanthesis += ")";
                    }
                }

                cv.QueryString += " " + _having.Knot.ToString() + " " + openParanthesis + "(" + _having.Aggregate.ToShortAggregateName() + "(" + _alias + "[" + _having.Column.ToString() + "]) " + not + " Between @" + paramName + " And @" + paramName2 + ")" + closedParanthesis;
                cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = values[0] });
                cv.Parameters.Add(new SqlParameter() { ParameterName = paramName2, Value = values[1] });
            }

            return cv;
        }

        private static HavingValues CreateInHaving(Having _having, string not, string _alias = "")
        {
            HavingValues cv = new HavingValues();

            if (_having.Values.Count > 0)
            {
                string openParanthesis = "";
                string closedParanthesis = "";

                if (_having.Parantheses != null)
                {
                    for (int i = 1; i <= _having.Parantheses.OpenCount; i++)
                    {
                        openParanthesis += "(";
                    }

                    for (int i = 1; i <= _having.Parantheses.ClosedCount; i++)
                    {
                        closedParanthesis += ")";
                    }
                }

                cv.QueryString += " " + _having.Knot.ToString() + " " + openParanthesis + "(" + _having.Aggregate.ToShortAggregateName() + "(" + _alias + "[" + _having.Column.ToString() + "]) " + not + " In(";

                foreach (dynamic item in _having.Values)
                {
                    string paramName = _having.Column.ToString() + Guider.GetGuid(5);

                    cv.QueryString += "@" + paramName + ", ";
                    cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = item });
                }

                cv.QueryString = cv.QueryString.Trim().TrimEnd(',');
                cv.QueryString += "))" + closedParanthesis;
            }

            return cv;
        }
    }

    internal sealed class HavingValues
    {
        static HavingValues()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

        public HavingValues()
        {
            this.QueryString = "";
            this.Parameters = new List<SqlParameter>();
        }

        internal string QueryString { get; set; }
        internal List<SqlParameter> Parameters { get; set; }
    }
}
