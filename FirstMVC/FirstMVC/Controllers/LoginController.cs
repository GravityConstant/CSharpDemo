using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string name = form["username"];
            string passwd = form["passwd"];

            string sql = "select password from angel_sys_user where username='{0}'";
            sql = string.Format(sql, name);
            object result = Utils.MySqlHelpers.ExecuteScalar(sql);
            string sqlPasswd = result.ToString();
            Debug.WriteLine("login passwd:" + sqlPasswd);
            if (sqlPasswd == passwd)
            {
                Session["checkcode"] = "aeiou";
                Session.Contents["oldcheckcode"] = "jpeg";
                Utils.Functions.WriteCookie("uname", name.ToString(), 30);
                return RedirectToAction("Index", "Home");
            } 
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

    }
}