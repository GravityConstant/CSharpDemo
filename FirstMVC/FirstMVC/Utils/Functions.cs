using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Protocols;

namespace FirstMVC.Utils
{
    public class Functions
    {
        /// <summary>
        /// 写cookie值
        /// </summary>
        public static void WriteCookie(string name, string value, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie == null)
            {
                cookie = new HttpCookie(name);
            }
            cookie.Value = UrlEncode(value);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        public static string GetCookie(string s)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[s] != null)
            {
                return UrlDecode(HttpContext.Current.Request.Cookies[s].Value.ToString());
            }
            return "";
        }

        /// <summary>
        /// URL编码
        /// </summary>
        public static string UrlEncode(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            s = s.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(s);
        }

        /// <summary>
        /// URL解码
        /// </summary>
        public static string UrlDecode(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(s);
        }
    }
}