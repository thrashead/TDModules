using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TDLibrary
{
    public class Urling
    {
        public static string FullURL
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString().Split('?').First();
            }
        }
        public static string FullURLWithParams
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString();
            }
        }
        public static string URL
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString().Split('/').Last().Split('?').First();
            }
        }
        public static string URLWithParams
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString().Split('/').Last();
            }
        }
        public static List<URLParameters> URLParams
        {
            get
            {
                List<URLParameters> urlParams = new List<URLParameters>();
                string[] paramArray = HttpContext.Current.Request.Url.ToString().Split('/').Last().Split('?').Last().Split('&');

                foreach(string item in paramArray)
                {
                    urlParams.Add(new URLParameters()
                    {
                        Parameter = item.Split('=').Length > 0 ? item.Split('=').First() : "",
                        Value = item.Split('=').Length > 1 ? item.Split('=').Last() : ""
                    });
                }

                return urlParams;
            }
        }
        public class URLParameters
        {
            public string Parameter { get; set; }
            public string Value { get; set; }
        }

        public static List<string> URLBlocks
        {
            get
            {
                List<string> returnBlocks = new List<string>();

                returnBlocks = HttpContext.Current.Request.Url.ToString().Replace("http://", "").Replace("https://", "").Replace("ftp://", "").Replace("localhost/", "").Split('?').First().TrimEnd('/').Split('/').ToList();

                return returnBlocks;
            }
        }
    }
}
