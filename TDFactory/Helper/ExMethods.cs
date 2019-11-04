using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TDFactory
{
    public static class ExMethods
    {
        public static string ClearColumn(this string column)
        {
            return column.Split('[')[0].Trim();
        }

        public static string TableName(this string column)
        {
            return column.Split('[')[1].Replace("]", "").Trim();
        }

        public static string ColumnWithTable(this string column, string table)
        {
            return column + " [" + table + "]";
        }

        public static string ColumnName(this string selectedItem)
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

        public static string ReturnType(this string type)
        {
            switch (type)
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
        }

        public static List<string> IdentityCheck(this List<string> list, ListBox listBox)
        {
            bool identitykon = false;

            foreach (string item in listBox.Items)
            {
                if (list.Contains(item.Split(' ')[0]))
                {
                    identitykon = true;
                }
            }

            list = identitykon == true ? list : new List<string>();

            return list;
        }

        public static string[] ToStringList(this ListBox.ObjectCollection list)
        {
            List<string> returnList = new List<string>();

            foreach (string item in list)
            {
                returnList.Add(item);
            }

            return returnList.ToArray();
        }

        public static string ToUrl(this string text, bool toLower = false)
        {
            if (toLower == true)
            {
                text = text.ToLower();
            }

            text = UrlReplacer(text);
            text = text.MakeSingle("-").Trim('-');

            return text;
        }

        private static string UrlReplacer(string text)
        {
            text = text.Replace("&amp;", "");
            text = text.Replace("&#304;", "İ");
            text = text.Replace("&#305;", "ı");
            text = text.Replace("&#214;", "Ö");
            text = text.Replace("&#246;", "ö");
            text = text.Replace("&#220;", "Ü");
            text = text.Replace("&#252;", "ü");
            text = text.Replace("&#199;", "Ç");
            text = text.Replace("&#231;", "ç");
            text = text.Replace("&#286;", "Ğ");
            text = text.Replace("&#287;", "ğ");
            text = text.Replace("&#350;", "Ş");
            text = text.Replace("&#351;", "ş");
            text = text.Replace("%c4%9e", "Ğ");
            text = text.Replace("%c4%9f", "ğ");
            text = text.Replace("%c3%9c", "Ü");
            text = text.Replace("%c3%bc", "ü");
            text = text.Replace("%c5%9e", "Ş");
            text = text.Replace("%c5%9f", "ş");
            text = text.Replace("%c4%b0", "İ");
            text = text.Replace("%c4%b1", "ı");
            text = text.Replace("%c3%96", "Ö");
            text = text.Replace("%c3%b6", "ö");
            text = text.Replace("%c3%87", "Ç");
            text = text.Replace("%c3%a7", "ç");

            text = text.Replace(" ", "-");
            text = text.Replace("?", "-");
            text = text.Replace("%", "-");
            text = text.Replace("½", "-");
            text = text.Replace("$", "-");
            text = text.Replace("#", "-");
            text = text.Replace("£", "-");
            text = text.Replace("!", "-");
            text = text.Replace("^", "-");
            text = text.Replace("'", "-");
            text = text.Replace("&", "-");
            text = text.Replace("/", "-");
            text = text.Replace("*", "-");
            text = text.Replace("\"", "-");
            text = text.Replace("[", "-");
            text = text.Replace("]", "-");
            text = text.Replace("{", "-");
            text = text.Replace("}", "-");
            //text = text.Replace("(", "-");
            //text = text.Replace(")", "-");
            text = text.Replace("+", "-");
            text = text.Replace("é", "-");
            text = text.Replace(",", "-");
            text = text.Replace(".", "-");
            text = text.Replace("~", "-");
            text = text.Replace(";", "-");
            text = text.Replace(":", "-");
            text = text.Replace("<", "-");
            text = text.Replace(">", "-");
            text = text.Replace("|", "-");
            text = text.Replace("@", "-");
            text = text.Replace("æ", "-");
            text = text.Replace("ß", "-");
            text = text.Replace("¨", "-");

            text = text.Replace("Ğ", "G");
            text = text.Replace("ğ", "g");
            text = text.Replace("Ü", "U");
            text = text.Replace("ü", "u");
            text = text.Replace("Ş", "S");
            text = text.Replace("ş", "s");
            text = text.Replace("İ", "I");
            text = text.Replace("ı", "i");
            text = text.Replace("Ö", "O");
            text = text.Replace("ö", "o");
            text = text.Replace("Ç", "C");
            text = text.Replace("ç", "c");
            text = text.Replace("â", "a");
            text = text.Replace("Â", "a");

            text = Regex.Replace(text, @"[^\u0000-\u007F]", string.Empty);

            text = Regex.Replace(text, @"[^\u0000-\u007F]", string.Empty);

            return text;
        }

        public static string MakeSingle(this string text, string changeText)
        {
            do
            {
                text = text.Replace(changeText + changeText, changeText);
            } while (text.Contains(changeText + changeText));

            return text;
        }

        public static bool In(this string item, string[] arrayList, InType toLower = InType.Nothing)
        {
            switch (toLower)
            {
                case InType.ToLower:
                    item = item.ToLower();
                    break;
                case InType.ToUrl:
                    item = item.ToUrl();
                    break;
                case InType.ToUrlLower:
                    item = item.ToUrl(true);
                    break;
            }

            return arrayList.ToList().IndexOf(item) >= 0 ? true : false;
        }

        public enum InType
        {
            Nothing,
            ToLower,
            ToUrl,
            ToUrlLower
        }

        public static string FirstCharToLowerCase(this string text, bool toEnglish = false, char splitChar = ' ')
        {
            string s = "";

            foreach (var item in text.Split(splitChar))
            {
                s += item[0].ToString().ToLower() + item.Remove(0, 1) + splitChar;
            }

            text = s.TrimEnd(splitChar);

            if (toEnglish == true)
            {
                text = text.Replace("ğ", "g");
                text = text.Replace("ü", "u");
                text = text.Replace("ş", "s");
                text = text.Replace("ı", "i");
                text = text.Replace("ö", "o");
                text = text.Replace("ç", "c");
                text = text.Replace("â", "a");
            }

            return text;
        }

        public static string ToTurkish(this string english, ListBox langList)
        {
            string returnValue = english;

            if (TDFactory.langChecked)
            {
                List<Language> list = Language.List(langList).Where(a => a.EN == english).ToList();

                if (list.Count > 0)
                {
                    returnValue = string.IsNullOrEmpty(list.FirstOrDefault().TR) ? english : list.FirstOrDefault().TR;
                }
            }

            return returnValue;
        }

        public static string ToUserRightType(this string tableName)
        {
            switch (tableName)
            {
                case "Category": return "Category";
                case "CategoryT": return "Category";
                case "Content": return "Content";
                case "ContentT": return "Content";
                case "Gallery": return "Gallery";
                case "GalleryT": return "Gallery";
                case "Meta": return "Meta";
                case "MetaT": return "Meta";
                case "Product": return "Product";
                case "ProductT": return "Product";
                case "Files": return "Files";
                case "Pictures": return "Pictures";
                case "LangContent": return "Translation";
                case "LangContentT": return "Translation";
                case "Links": return "LinkTypes";
                case "LinkTypes": return "LinkTypes";
                case "Logs": return "Logs";
                case "LogProcess": return "Logs";
                case "LogTypes": return "Logs";
                case "NoLangContent": return "Translation";
                case "Users": return "Users";
                case "UserGroups": return "Users";
                case "UserGroupTables": return "Users";
                case "UserGroupProcess": return "Users";
                case "UserGroupRights": return "Users";
                case "Translation": return "Translation";
                case "Types": return "Types";
                case "Visitors": return "Website";
                default: return "Website";
            }
        }

        public static string ToIcon(this string tableName)
        {
            switch (tableName)
            {
                case "Category": return "icon-sitemap";
                case "CategoryT": return "icon-sitemap";
                case "Content": return "icon-edit";
                case "ContentT": return "icon-edit";
                case "Gallery": return "icon-picture";
                case "GalleryT": return "icon-picture";
                case "Meta": return "icon-tags";
                case "MetaT": return "icon-tags";
                case "Product": return "icon-barcode";
                case "ProductT": return "icon-barcode";
                case "Files": return "icon-file";
                case "Pictures": return "icon-camera-retro";
                case "LangContent": return "icon-flag";
                case "LangContentT": return "icon-flag";
                case "Links": return "icon-link";
                case "LinkTypes": return "icon-link";
                case "Logs": return "icon-eye-open";
                case "LogProcess": return "icon-eye-open";
                case "LogTypes": return "icon-eye-open";
                case "NoLangContent": return "icon-flag";
                case "Users": return "icon-user";
                case "UserGroups": return "icon-user";
                case "UserGroupTables": return "icon-user";
                case "UserGroupProcess": return "icon-user";
                case "UserGroupRights": return "icon-user";
                case "Translation": return "icon-globe";
                case "Types": return "icon-pushpin";
                case "Visitors": return "icon-plane";
                default: return "icon-home";
            }
        }
    }
}
