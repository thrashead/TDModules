using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TDFactory.Helper
{
    public static class ExMethods
    {
        public static string KolonTemizle(this string kolon)
        {
            return kolon.Split('[')[0].Trim();
        }

        public static string TabloAdi(this string kolon)
        {
            return kolon.Split('[')[1].Replace("]", "").Trim();
        }

        public static string TabloluKolon(this string kolon, string tablo)
        {
            return kolon + " [" + tablo + "]";
        }

        public static string KolonAdi(this string selectedItem)
        {
            if (selectedItem.Split(']').Length > 0)
            {
                return selectedItem.Split(']')[0].Replace("[", "");
            }
            else
            {
                return "";
            }
        }

        public static string TipDon(this string tip)
        {
            switch (tip)
            {
                case "Int16": return "int";
                case "Int32": return "int";
                case "Int64": return "Int64";
                case "Decimal": return "decimal";
                case "Double": return "double";
                case "Char": return "char";
                case "Chars": return "char[]";
                case "String": return "string";
                case "Byte": return "byte";
                case "Bytes": return "byte[]";
                case "Boolean": return "bool";
                case "DateTime": return "DateTime";
                case "DateTimeOffset": return "DateTimeOffset";
                case "TimeSpan": return "TimeSpan";
                case "Single": return "Single";
                case "Object": return "object";
                case "Guid": return "Guid";
                case "None": return "object";
                default: return "string";
            }

            return tip;
        }

        public static List<string> IdentityCheck(this List<string> _list, ListBox _listBox)
        {
            bool identitykon = false;

            foreach (string item in _listBox.Items)
            {
                if (_list.Contains(item.Split(' ')[0]))
                {
                    identitykon = true;
                }
            }

            _list = identitykon == true ? _list : new List<string>();

            return _list;
        }

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

        public static string MakeSingle(this string _text, string _changeText)
        {
            do
            {
                _text = _text.Replace(_changeText + _changeText, _changeText);
            } while (_text.Contains(_changeText + _changeText));

            return _text;
        }

        public static bool In(this string item, string[] arrayList)
        {
            return arrayList.ToList().IndexOf(item) >= 0 ? true : false;
        }
    }
}
