using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FirstMVC.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 执行控制器前执行该方法，判断cookie是否为空
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            try
            {
                var checkcode = Session["checkcode"];
                var oldcheckcode = Session["oldcheckcode"];
                if (checkcode == null || oldcheckcode == null)
                {
                    Debug.WriteLine("no seesion");
                } 
                else
                {
                    Debug.WriteLine("session checkcode:" + checkcode.ToString());
                    Debug.WriteLine("session old checkcode:" + oldcheckcode.ToString());
                }
                
                string username = Utils.Functions.GetCookie("uname");
                if (string.IsNullOrEmpty(username))
                {
                    #region##删除cookies
                    foreach (string cookiename in Request.Cookies.AllKeys)
                    {
                        HttpCookie cookie = Request.Cookies[cookiename];
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Today.AddDays(-1);
                            Response.Cookies.Add(cookie);
                            Request.Cookies.Remove(cookiename);
                        }
                    }
                    #endregion
                    filterContext.HttpContext.Response.Write("<script>window.location.href='login/index';</script>");
                    filterContext.HttpContext.Response.End();
                }
            }
            catch (Exception e)
            {
                filterContext.HttpContext.Response.Write("<script>window.location.href='login/index';</script>");
                filterContext.HttpContext.Response.End();
            }
        }
    }
}