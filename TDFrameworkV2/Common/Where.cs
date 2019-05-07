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
using System.Data.SqlClient;
using System.Linq;

namespace TDFramework.Common
{
    public sealed class Where
    {
        public dynamic Column { get; set; }
        public List<dynamic> Values { get; set; }
        public bool Not { get; set; }
        public Knots Knot { get; set; }
        public Operators Operator { get; set; }
        public Paranthesis Parantheses { get; set; }

        static Where()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

		public Where()
		{
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;
            this.Knot = Knots.AND;
            this.Parantheses = null;
        }

        public Where(dynamic column, dynamic values)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;
            this.Knot = Knots.AND;
            this.Parantheses = null;

            if (values.GetType().IsGenericType == true)
            {
                foreach (var item in values)
                {
                    this.Values.Add(item);
                }
            }
            else if(values.GetType().BaseType.Name == "Array")
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

        public Where(dynamic column, dynamic values, Operators operatorr)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = false;
            this.Knot = Knots.AND;
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

        public Where(dynamic column, dynamic values, bool not)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = not;
            this.Knot = Knots.AND;
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

        public Where(dynamic column, dynamic values, Knots knot)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;
            this.Knot = knot;
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

        public Where(dynamic column, dynamic values, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;
            this.Knot = Knots.AND;
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

        public Where(dynamic column, dynamic values, Operators operatorr, bool not)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = not;
            this.Knot = Knots.AND;
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

        public Where(dynamic column, dynamic values, Operators operatorr, Knots knot)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = false;
            this.Knot = knot;
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

        public Where(dynamic column, dynamic values, Operators operatorr, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = false;
            this.Knot = Knots.AND;
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

        public Where(dynamic column, dynamic values, bool not, Knots knot)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = not;
            this.Knot = knot;
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

        public Where(dynamic column, dynamic values, bool not, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = not;
            this.Knot = Knots.AND;
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

        public Where(dynamic column, dynamic values, Knots knot, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;
            this.Knot = knot;
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

        public Where(dynamic column, dynamic values, Operators operatorr, bool not, Knots knot)
		{
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = not;
            this.Knot = knot;
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

        public Where(dynamic column, dynamic values, Operators operatorr, bool not, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = not;
            this.Knot = Knots.AND;
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

        public Where(dynamic column, dynamic values, Operators operatorr, Knots knot, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = false;
            this.Knot = knot;
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

        public Where(dynamic column, dynamic values, bool not, Knots knot, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = not;
            this.Knot = knot;
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

        public Where(dynamic column, dynamic values, Operators operatorr, bool not, Knots knot, Paranthesis paranthesis)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = not;
            this.Knot = knot;
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

		internal static WhereValues CreateWhere(List<Where> _whereList, string _alias = "")
		{
			WhereValues cv = new WhereValues();
			WhereValues tempCV = new WhereValues();

			_alias = _alias == "" ? "" : _alias + ".";

            foreach (Where item in _whereList)
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
                        tempCV = CreateEqualWhere(item, not, _alias);
						break;
					case Operators.GREATER:
                        tempCV = CreateGreaterWhere(item, not, _alias);
						break;
					case Operators.GREATEREQUAL:
                        tempCV = CreateGreaterEqualWhere(item, not, _alias);
						break;
					case Operators.SMALLER:
                        tempCV = CreateSmallerWhere(item, not, _alias);
						break;
					case Operators.SMALLEREQUAL:
                        tempCV = CreateSmallerEqualWhere(item, not, _alias);
						break;
					case Operators.LIKE:
						tempCV = CreateLikeWhere(item, not, _alias);
						break;
					case Operators.STARTLIKE:
                        tempCV = CreateStartLikeWhere(item, not, _alias);
						break;
					case Operators.ENDLIKE:
                        tempCV = CreateEndLikeWhere(item, not, _alias);
						break;
					case Operators.EXACTLIKE:
                        tempCV = CreateStartEndLikeWhere(item, not, _alias);
						break;
					case Operators.BETWEEN:
                        tempCV = CreateBetweenWhere(item, not, _alias);
						break;
					case Operators.IN:
                        tempCV = CreateInWhere(item, not, _alias);
						break;
					default:
                        tempCV = CreateEqualWhere(item, not, _alias);
						break;
				}

				cv.QueryString += tempCV.QueryString;
				cv.Parameters.AddRange(tempCV.Parameters);
			}

			cv.QueryString = cv.QueryString.TrimStart(' ').Remove(0, cv.QueryString.TrimStart(' ').IndexOf(' ') + 1);

			return cv;
		}

        private static WhereValues CreateEqualWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();
            string paramName = _where.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_where.Parantheses != null)
            {
                for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + not + " " + _alias + "[" + _where.Column.ToString() + "] = @" + paramName + ")" + closedParanthesis;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _where.Values.FirstOrDefault() });

			return cv;
		}

        private static WhereValues CreateGreaterWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();
            string paramName = _where.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_where.Parantheses != null)
            {
                for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + not + " " + _alias + "[" + _where.Column.ToString() + "] > @" + paramName + ")" + closedParanthesis;
			cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _where.Values.FirstOrDefault() });

			return cv;
		}

        private static WhereValues CreateGreaterEqualWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();
            string paramName = _where.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_where.Parantheses != null)
            {
                for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + not + " " + _alias + "[" + _where.Column.ToString() + "] >= @" + paramName + ")" + closedParanthesis;
			cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _where.Values.FirstOrDefault() });

			return cv;
		}

        private static WhereValues CreateSmallerWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();
            string paramName = _where.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_where.Parantheses != null)
            {
                for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + not + " " + _alias + "[" + _where.Column.ToString() + "] < @" + paramName + ")" + closedParanthesis;
			cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _where.Values.FirstOrDefault() });

			return cv;
		}

        private static WhereValues CreateSmallerEqualWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();
            string paramName = _where.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_where.Parantheses != null)
            {
                for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + not + " " + _alias + "[" + _where.Column.ToString() + "] <= @" + paramName + ")" + closedParanthesis;
			cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _where.Values.FirstOrDefault() });

			return cv;
		}

        private static WhereValues CreateLikeWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();
            string paramName = _where.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_where.Parantheses != null)
            {
                for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + _alias + "[" + _where.Column.ToString() + "] " + not + " Like @" + paramName + ")" + closedParanthesis;
            cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = "%" + _where.Values.FirstOrDefault() + "%" });

			return cv;
		}

        private static WhereValues CreateStartLikeWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();
            string paramName = _where.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_where.Parantheses != null)
            {
                for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + _alias + "[" + _where.Column.ToString() + "] " + not + " Like @" + paramName + ")" + closedParanthesis;
			cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _where.Values.FirstOrDefault() + "%" });

			return cv;
		}

        private static WhereValues CreateEndLikeWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();
            string paramName = _where.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_where.Parantheses != null)
            {
                for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + _alias + "[" + _where.Column.ToString() + "] " + not + " Like @" + paramName + ")" + closedParanthesis;
			cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = "%" + _where.Values.FirstOrDefault() });

			return cv;
		}

        private static WhereValues CreateStartEndLikeWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();
            string paramName = _where.Column.ToString() + Guider.GetGuid(5);

            string openParanthesis = "";
            string closedParanthesis = "";

            if (_where.Parantheses != null)
            {
                for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                {
                    openParanthesis += "(";
                }

                for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                {
                    closedParanthesis += ")";
                }
            }

            cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + _alias + "[" + _where.Column.ToString() + "] " + not + " Like @" + paramName + ")" + closedParanthesis;
			cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _where.Values.FirstOrDefault() });

			return cv;
		}

        private static WhereValues CreateBetweenWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();
			dynamic[] values = _where.Values.ToArray();

			if (values.Length > 1)
			{
                string paramName = _where.Column.ToString() + Guider.GetGuid(5);
                string paramName2 = _where.Column.ToString() + Guider.GetGuid(5);

                string openParanthesis = "";
                string closedParanthesis = "";

                if (_where.Parantheses != null)
                {
                    for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                    {
                        openParanthesis += "(";
                    }

                    for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                    {
                        closedParanthesis += ")";
                    }
                }

                cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + _alias + "[" + _where.Column.ToString() + "] " + not + " Between @" + paramName + " And @" + paramName2 + ")" + closedParanthesis;
				cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = values[0] });
				cv.Parameters.Add(new SqlParameter() { ParameterName = paramName2, Value = values[1] });
			}

			return cv;
		}

        private static WhereValues CreateInWhere(Where _where, string not, string _alias = "")
		{
			WhereValues cv = new WhereValues();

			if (_where.Values.Count > 0)
            {
                string openParanthesis = "";
                string closedParanthesis = "";

                if (_where.Parantheses != null)
                {
                    for (int i = 1; i <= _where.Parantheses.OpenCount; i++)
                    {
                        openParanthesis += "(";
                    }

                    for (int i = 1; i <= _where.Parantheses.ClosedCount; i++)
                    {
                        closedParanthesis += ")";
                    }
                }

                cv.QueryString += " " + _where.Knot.ToString() + " " + openParanthesis + "(" + _alias + "[" + _where.Column.ToString() + "] " + not + " In(";

				foreach (dynamic item in _where.Values)
				{
                    string paramName = _where.Column.ToString() + Guider.GetGuid(5);

					cv.QueryString += "@" + paramName + ", ";
					cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = item });
				}

				cv.QueryString = cv.QueryString.Trim().TrimEnd(',');
                cv.QueryString += "))" + closedParanthesis;
			}

		    return cv;
		}
	}

    public sealed class Paranthesis
    {
        static Paranthesis()
        {
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
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
            System.AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e)
            {
                TDConnection.ConnectionStringForOnce = null;
            };
        }

		public WhereValues()
		{
			this.QueryString = "";
			this.Parameters = new List<SqlParameter>();
		}

        internal string QueryString { get; set; }
        internal List<SqlParameter> Parameters { get; set; }
	}
}
