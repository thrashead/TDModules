using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace TDLibrary
{
	public static class ExtMethods
	{
		#region ToInteger - String değeri int'e çevirmeye çalışır. Çeviremezse -1 döner.

		public static int ToInteger(this string _value)
		{
			int value = -1;
			if (int.TryParse(_value, out value))
				return value;
			else
				return -1;
		}

		#endregion

		#region ToTitleCase - Yazının ilk harflerini Büyük yapar. İstenirse CultureInfo param olarak gönderilebilir.

		public static string ToTitleCase(this string _text)
		{
			_text = _text.ToLower();
			string[] texts = _text.Split(' ');
			TextInfo myTI = new CultureInfo("tr-TR", false).TextInfo;
			_text = "";
			foreach (string item in texts)
			{
				_text += myTI.ToTitleCase(item) + " ";
			}

			return _text.Trim();
		}

		public static string ToTitleCase(this string _text, CultureInfo _culInfo)
		{
			_text = _text.ToLower();
			string[] texts = _text.Split(' ');
			TextInfo myTI = _culInfo.TextInfo;
			_text = "";
			foreach (string item in texts)
			{
				_text += myTI.ToTitleCase(item) + " ";
			}

			return _text.Trim();
		}

		#endregion

		#region ToHyperLinkText - Yazıyı hiperlinke çevirir. toLower param true giderse harfleri küçültür.

		public static string ToHyperLinkText(this string _text, bool _toLower = false)
		{
			if (_toLower == true)
			{
				_text = _text.ToLower();
			}

			_text = HyperLinkTextReplacer(_text);
			_text = _text.MakeSingle("-").Trim('-');

			return _text;
		}

		private static string HyperLinkTextReplacer(string _text)
		{
			_text = _text.Replace("&amp;", "");
			_text = _text.Replace("&#304;", "İ");
			_text = _text.Replace("&#305;", "ı");
			_text = _text.Replace("&#214;", "Ö");
			_text = _text.Replace("&#246;", "ö");
			_text = _text.Replace("&#220;", "Ü");
			_text = _text.Replace("&#252;", "ü");
			_text = _text.Replace("&#199;", "Ç");
			_text = _text.Replace("&#231;", "ç");
			_text = _text.Replace("&#286;", "Ğ");
			_text = _text.Replace("&#287;", "ğ");
			_text = _text.Replace("&#350;", "Ş");
			_text = _text.Replace("&#351;", "ş");
			_text = _text.Replace("%c4%9e", "Ğ");
			_text = _text.Replace("%c4%9f", "ğ");
			_text = _text.Replace("%c3%9c", "Ü");
			_text = _text.Replace("%c3%bc", "ü");
			_text = _text.Replace("%c5%9e", "Ş");
			_text = _text.Replace("%c5%9f", "ş");
			_text = _text.Replace("%c4%b0", "İ");
			_text = _text.Replace("%c4%b1", "ı");
			_text = _text.Replace("%c3%96", "Ö");
			_text = _text.Replace("%c3%b6", "ö");
			_text = _text.Replace("%c3%87", "Ç");
			_text = _text.Replace("%c3%a7", "ç");

			_text = _text.Replace(" ", "-");
			_text = _text.Replace("?", "-");
			_text = _text.Replace("%", "-");
			_text = _text.Replace("½", "-");
			_text = _text.Replace("$", "-");
			_text = _text.Replace("#", "-");
			_text = _text.Replace("£", "-");
			_text = _text.Replace("!", "-");
			_text = _text.Replace("^", "-");
			_text = _text.Replace("'", "-");
			_text = _text.Replace("&", "-");
			_text = _text.Replace("/", "-");
			_text = _text.Replace("*", "-");
			_text = _text.Replace("\"", "-");
			_text = _text.Replace("[", "-");
			_text = _text.Replace("]", "-");
			_text = _text.Replace("{", "-");
			_text = _text.Replace("}", "-");
			//text = text.Replace("(", "-");
			//text = text.Replace(")", "-");
			_text = _text.Replace("+", "-");
			_text = _text.Replace("é", "-");
			_text = _text.Replace(",", "-");
			_text = _text.Replace(".", "-");
			_text = _text.Replace("~", "-");
			_text = _text.Replace(";", "-");
			_text = _text.Replace(":", "-");
			_text = _text.Replace("<", "-");
			_text = _text.Replace(">", "-");
			_text = _text.Replace("|", "-");
			_text = _text.Replace("@", "-");
			_text = _text.Replace("æ", "-");
			_text = _text.Replace("ß", "-");
			_text = _text.Replace("¨", "-");

			_text = _text.Replace("Ğ", "G");
			_text = _text.Replace("ğ", "g");
			_text = _text.Replace("Ü", "U");
			_text = _text.Replace("ü", "u");
			_text = _text.Replace("Ş", "S");
			_text = _text.Replace("ş", "s");
			_text = _text.Replace("İ", "I");
			_text = _text.Replace("ı", "i");
			_text = _text.Replace("Ö", "O");
			_text = _text.Replace("ö", "o");
			_text = _text.Replace("Ç", "C");
			_text = _text.Replace("ç", "c");
			_text = _text.Replace("â", "a");
			_text = _text.Replace("Â", "a");

			_text = Regex.Replace(_text, @"[^\u0000-\u007F]", string.Empty);

			_text = Regex.Replace(_text, @"[^\u0000-\u007F]", string.Empty);

			return _text;
		}

        #endregion

        #region MakeSingle - Burada 2 tane yanyana aynı string geliyorsa tekleyene kadar döner. ("---" -> "-") veya ("alialialiali" -> "ali")

        public static string MakeSingle(this string _text, string _changeText)
		{
			do
			{
				_text = _text.Replace(_changeText + _changeText, _changeText);
			} while (_text.Contains(_changeText + _changeText));

			return _text;
		}

		#endregion

		#region ToMD5 - Yazıyı MD5'e çevirir.

		public static string ToMD5(this string _text)
		{
			byte[] ByteData = Encoding.ASCII.GetBytes(_text);
			MD5 Md5Object = MD5.Create();
			byte[] HashData = Md5Object.ComputeHash(ByteData);

			StringBuilder SB = new StringBuilder();
			for (int x = 0; x < HashData.Length; x++)
			{
				SB.Append(HashData[x].ToString("x2"));
			}

			return SB.ToString();
		}

		#endregion

		#region TextCompare - 2 yazıyı kıyaslar. doMD5Compare true giderse ilgili text'i MD5'e çevirip compareText ile kıyaslar ve bool değer döner.

		public static bool TextCompare(this string _text, string _compareText)
		{
			if (_text == _compareText)
				return true;
			else
				return false;
		}

		public static bool TextCompare(this string _text, string _compareText, bool _doMD5Compare)
		{
			if (_doMD5Compare)
			{
				if (_text.ToMD5() == _compareText)
					return true;
				else
					return false;
			}
			else
			{
				if (_text == _compareText)
					return true;
				else
					return false;
			}
		}

		#endregion

		#region IsNull - String Boş mu değil mi kontrol eder

		public static bool IsNull(this string text)
		{
			return String.IsNullOrEmpty(text);
		}

		#endregion

		#region IsUploadable - Dosya gönderilen uzantılara ("jpg,png,gif,bmp") göre gönderilebilir mi kontrol eder

		public static bool IsUploadable(this string text, string extensions)
		{
			bool control = false;

			string[] exts = extensions.Split(',');

			foreach(string item in exts)
			{
				if (text.Split('.').Length > 1)
				{
					if (item == text.Split('.')[1])
						control = true;
				}
			}

			return control;
		}

		#endregion

		#region SplitText - Gönderilen metni böler...

		public static string SplitText(this string text, int startIndex, int count)
		{
			if (text.Length > count)
			{
				text = text.Substring(startIndex, count) + "...";
				return text;
			}
			else
				return text;
		}

		#endregion

		#region Shuffle - Liste elemanlarının yerini Random Değişir...

		public static IList<T> Shuffle<T>(this IList<T> list)
		{
			Random rng = new Random();
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
			return list;
		}

		#endregion

		#region CreateThumb - Yüklenen Resimler İçin Thumbnail Oluşturur.

		public static void CreateThumb(this Image img, string savePath, double formatWidth)
		{
			using (System.Drawing.Image Img = img)
			{
				Size ThumbNailSize = NewImageSize(Img.Height, Img.Width, formatWidth);
				using (System.Drawing.Image ImgThnail = new Bitmap(Img, ThumbNailSize.Width, ThumbNailSize.Height))
				{
					ImgThnail.Save(savePath, Img.RawFormat);
					ImgThnail.Dispose();
				}
				Img.Dispose();
			}
		}

		private static Size NewImageSize(int OriginalHeight, int OriginalWidth, double FormatWidth)
		{
			Size NewSize;
			double tempval;
			bool landscape;
			if (OriginalHeight > OriginalWidth)
			{
				landscape = false;
			}
			else
			{
				landscape = true;
			}
			if (landscape)
			{
				if (OriginalWidth > FormatWidth)
				{
					tempval = FormatWidth / Convert.ToDouble(OriginalWidth);
					NewSize = new Size(Convert.ToInt32(tempval * OriginalWidth), Convert.ToInt32(tempval * OriginalHeight));
				}
				else
				{
					NewSize = new Size(OriginalWidth, OriginalHeight);
				}
			}
			else
			{
				if (OriginalHeight > FormatWidth)
				{
					tempval = FormatWidth / Convert.ToDouble(OriginalHeight);
					NewSize = new Size(Convert.ToInt32(tempval * OriginalWidth), Convert.ToInt32(tempval * OriginalHeight));
				}
				else
				{
					NewSize = new Size(OriginalWidth, OriginalHeight);
				}
			}
			return NewSize;
		}

		#endregion

		#region IsMailAddress - Mail adresi mi kontrol eder.

		public static bool IsMailAddress(this string mail)
		{
			const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
			@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
			@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

			return (new Regex(strRegex)).IsMatch(mail);
		}

		#endregion

        #region FirstCharToUpperCase - Stringin ilk harfini büyük yazar.

        public static string FirstCharToUpperCase(this string _text, bool _toEnglish = false, char splitChar = ' ')
        {
            string s = "";

            foreach (var item in _text.Split(splitChar))
            {
                s += item[0].ToString().ToUpper() + item.Remove(0, 1) + splitChar;
            }

            _text = s.TrimEnd(splitChar);

            if (_toEnglish == true)
            {
                _text = _text.Replace("Ğ", "G");
                _text = _text.Replace("Ü", "U");
                _text = _text.Replace("Ş", "S");
                _text = _text.Replace("İ", "I");
                _text = _text.Replace("Ö", "O");
                _text = _text.Replace("Ç", "C");
                _text = _text.Replace("Â", "a");
            }

            return _text;
        }

        #endregion

        #region FirstCharToLowerCase - Stringin ilk harfini küçük yazar.

        public static string FirstCharToLowerCase(this string _text, bool _toEnglish = false, char splitChar = ' ')
        {
            string s = "";

            foreach (var item in _text.Split(splitChar))
            {
                s += item[0].ToString().ToLower() + item.Remove(0, 1) + splitChar;
            }

            _text = s.TrimEnd(splitChar);

            if (_toEnglish == true)
            {
                _text = _text.Replace("ğ", "g");
                _text = _text.Replace("ü", "u");
                _text = _text.Replace("ş", "s");
                _text = _text.Replace("ı", "i");
                _text = _text.Replace("ö", "o");
                _text = _text.Replace("ç", "c");
                _text = _text.Replace("â", "a");
            }

            return _text;
        }

        #endregion

		#region StringToEnum - Stringi istenilen enuma çevirip döner.

		public static T StringToEnum<T>(this string _column)
		{
			object column = Activator.CreateInstance(typeof(T));

			if (typeof(T).BaseType.Name == "Enum")
			{
				var enums = Enum.GetValues(typeof(T));
				int i = 0;

				foreach (dynamic enumvalue in enums)
				{
					if (enumvalue.ToString() == _column)
					{
						column = enumvalue;
						i++;
						break;
					}
				}

				if (i == 0)
				{
					return default(T);
				}
				else
				{
					return (T)column;
				}
			}

			return default(T);
		}

		#endregion

		#region StringToEnumList - Virgül veya herhangi bir karakterle ayrılmış stringleri veya string diziyi istenilen enuma çevirip List<İstenilenEnumType> olarak döner.

		public static List<T> StringToEnumList<T>(this string _colums, char _splitter)
		{
			if (typeof(T).BaseType.Name == "Enum")
			{
				List<T> columns = new List<T>();
				var enums = Enum.GetValues(typeof(T));

				foreach (string item in _colums.Split(_splitter))
				{
					foreach (dynamic enumvalue in enums)
					{
						if (enumvalue.ToString() == item)
						{
							columns.Add(enumvalue);
							break;
						}
					}

				}
				return columns;
			}
			else
			{
				return null;
			}
		}

		public static List<T> StringToEnumList<T>(this string[] _colums)
		{
			if (typeof(T).BaseType.Name == "Enum")
			{
				List<T> columns = new List<T>();
				var enums = Enum.GetValues(typeof(T));

				foreach (string item in _colums)
				{
					foreach (dynamic enumvalue in enums)
					{
						if (enumvalue.ToString() == item)
						{
							columns.Add(enumvalue);
							break;
						}
					}

				}
				return columns;
			}
			else
			{
				return null;
			}
		}

		#endregion

		#region ChangeModel, ChangeModelList - Herhangi bir Nesneyi veya Nesne Listesini uyuşuyorsa diğer veriye çevirir. MVC'de model'de kullanmak için oluşturdum.

		public static TargetType ChangeModel<TargetType>(this object _model)
		{
			object typeObject = Activator.CreateInstance(typeof(TargetType));

			foreach (PropertyInfo item in typeObject.GetType().GetProperties())
			{
				foreach (PropertyInfo item2 in _model.GetType().GetProperties())
				{
					if (item.PropertyType == item2.PropertyType && item.Name == item2.Name)
					{
						item.SetValue(typeObject, item2.GetValue(_model, null), null);
					}
				}
			}

			return (TargetType)typeObject;
		}

		public static List<TargetType> ChangeModelList<TargetType, OwnerType>(this List<OwnerType> _modelList)
		{
			List<TargetType> returnList = new List<TargetType>();

			foreach (object item in _modelList)
			{
				returnList.Add(item.ChangeModel<TargetType>());
			}

			return returnList;
		}

		#endregion

        #region CellValue - DataTable nesnesinin belirtilen satırındaki belirtilen hücreye ait değeri döner.

        public static object CellValue(this DataTable _dataTable, int _rowNumber = 0, int _cellNumber = 0)
        {
            return _dataTable.Rows[_rowNumber].ItemArray[_cellNumber];
        }

        #endregion

        #region LetterCount - İlgili textteki belirtilen harf sayısını döner.

        public static int LetterCount(this string text, char letter)
        {
            int count = 0;

            foreach (char item in text)
            {
                if (item == letter)
                {
                    count++;
                }
            }

            return count;
        }

        #endregion

        #region In - İlgili nesne geçerli aynı türden dizideki  değerler arasında var mı kontrol eder.

        public static bool In<T>(this T item, T[] arrayList) where T : class
        {
            bool control = false;

            foreach (T obj in arrayList)
            {
                if (obj.GenericCompare<T>(item))
                {
                    control = true;
                }
            }

            return control;
        }

        #endregion

        #region GenericCompare - Aynı tip iki jenerik sınıfın değerini kıyaslar.

        public static bool GenericCompare<T>(this T x, T y) where T : class
        {
            return x == y;
        }

        #endregion
	}
}
