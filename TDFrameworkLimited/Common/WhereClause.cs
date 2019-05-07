// ================================
// AUTHOR           : Sina SALIK
// PROJECT NAME     : TDFramework
// VERSION          : v2.2.0.2
// CREATE DATE      : 05.10.2015
// RELEASE DATE     : 29.10.2015
// LAST UPDATE      : 22.02.2016
// SPECIAL NOTES    : Güzel Program
// ================================

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace TDFramework.Common
{
    public sealed class WhereClause
    {
        public dynamic Column { get; set; }
        public List<dynamic> Values { get; set; }
        public bool Not { get; set; }
        public Operators Operator { get; set; }

        public WhereClause()
		{
			this.Values = new List<dynamic>();
			this.Not = false;
            this.Operator = Operators.EQUAL;
        }

        public WhereClause(dynamic column, dynamic values)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = false;

            if (values.GetType().IsGenericType == true)
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

        public WhereClause(dynamic column, dynamic values, Operators operatorr)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = false;

            if (values.GetType().IsGenericType == true)
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

        public WhereClause(dynamic column, dynamic values, bool not)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = Operators.EQUAL;
            this.Not = not;

            if (values.GetType().IsGenericType == true)
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

        public WhereClause(dynamic column, dynamic values, Operators operatorr, bool not)
        {
            this.Column = column;
            this.Values = new List<dynamic>();
            this.Operator = operatorr;
            this.Not = not;

            if (values.GetType().IsGenericType == true)
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

		internal static SqlConditionValues CreateCondition(List<WhereClause> _conditions, string _alias = "")
		{
			SqlConditionValues cv = new SqlConditionValues();
			SqlConditionValues tempCV = new SqlConditionValues();

			_alias = _alias == "" ? "" : _alias + ".";

			foreach (WhereClause item in _conditions)
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
                        tempCV = CreateEqualCondition(item, not, _alias);
						break;
					case Operators.LIKE:
						tempCV = CreateLikeCondition(item, not, _alias);
                        break;
                    case Operators.BETWEEN:
                        tempCV = CreateBetweenCondition(item, not, _alias);
                        break;
                    case Operators.IN:
                        tempCV = CreateInCondition(item, not, _alias);
                        break;
					default:
                        tempCV = CreateEqualCondition(item, not, _alias);
						break;
				}

				cv.QueryString += tempCV.QueryString;
				cv.Parameters.AddRange(tempCV.Parameters);
			}

			cv.QueryString = cv.QueryString.TrimStart(' ').Remove(0, cv.QueryString.TrimStart(' ').IndexOf(' ') + 1);

			return cv;
		}

		private static SqlConditionValues CreateEqualCondition(WhereClause _condition, string not, string _alias = "")
		{
			SqlConditionValues cv = new SqlConditionValues();
			string paramName = _condition.Column.ToString() + Guider.GetGuid(5);

            cv.QueryString += " AND (" + not + " " + _alias + "[" + _condition.Column.ToString() + "] = @" + paramName + ")";
			cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = _condition.Values.FirstOrDefault() });

			return cv;
		}
        
        private static SqlConditionValues CreateLikeCondition(WhereClause _condition, string not, string _alias = "")
		{
			SqlConditionValues cv = new SqlConditionValues();
            string paramName = _condition.Column.ToString() + Guider.GetGuid(5);

			cv.QueryString += " AND (" + _alias + "[" + _condition.Column.ToString() + "] " + not + " Like @" + paramName + ")";
			cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = "%" + _condition.Values.FirstOrDefault() + "%" });

			return cv;
		}

        private static SqlConditionValues CreateBetweenCondition(WhereClause _condition, string not, string _alias = "")
        {
            SqlConditionValues cv = new SqlConditionValues();
            dynamic[] values = _condition.Values.ToArray();

            if (values.Length > 1)
            {
                string paramName = _condition.Column.ToString() + Guider.GetGuid(5);
                string paramName2 = _condition.Column.ToString() + Guider.GetGuid(5);

                cv.QueryString += " AND (" + _alias + "[" + _condition.Column.ToString() + "] " + not + " Between @" + paramName + " And @" + paramName2 + ")";
                cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = values[0] });
                cv.Parameters.Add(new SqlParameter() { ParameterName = paramName2, Value = values[1] });
            }

            return cv;
        }

        private static SqlConditionValues CreateInCondition(WhereClause _condition, string not, string _alias = "")
        {
            SqlConditionValues cv = new SqlConditionValues();

            if (_condition.Values.Count > 0)
            {
                cv.QueryString += " AND (" + _alias + "[" + _condition.Column.ToString() + "] " + not + " In(";

                foreach (dynamic item in _condition.Values)
                {
                    string paramName = _condition.Column.ToString() + Guider.GetGuid(5);

                    cv.QueryString += "@" + paramName + ", ";
                    cv.Parameters.Add(new SqlParameter() { ParameterName = paramName, Value = item });
                }

                cv.QueryString = cv.QueryString.Trim().TrimEnd(',');
                cv.QueryString += "))";
            }

            return cv;
        }
	}

    public enum Operators
    {
        EQUAL,
        LIKE,
        BETWEEN,
        IN
    }

    internal class SqlConditionValues
	{
        public SqlConditionValues()
		{
			this.QueryString = "";
			this.Parameters = new List<SqlParameter>();
		}

        internal string QueryString { get; set; }
        internal List<SqlParameter> Parameters { get; set; }
	}
}
