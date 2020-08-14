using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace FirstMVC.Controllers
{
    public class DepartmentController : BaseController
    {
        // GET: Department
        public ActionResult Index()
        {
            string sql = "select id, dname from angel_sys_department";
            DataTable table = Utils.MySqlHelpers.ExecuteDataTable(sql);

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None);//json序列化
            string json = "{\"code\":0,\"msg\":\"\",\"count\":" + table.Rows.Count + ",\"data\":";//json内容格式
            json = json + JsonString + "}";//vds数据为data:
            Debug.WriteLine("json string: " + json);

            ViewData["table"] = json;
            ViewBag.canEdit = true;
            return View();
        }

        public ActionResult Edit()
        {
            if (Request.HttpMethod.ToString() == "POST")
            {
                RedirectPermanent("/api/departmentapi/post");
            }

            int id = Convert.ToInt32(Request.QueryString["id"]);
            Debug.WriteLine("department edit, id: " + id.ToString());

            DataTable table;

            ViewBag.id = 0;
            ViewBag.dname = "";
            if (id > 0)
            {
                string sql = "select id, dname from angel_sys_department where id={0}";
                sql = string.Format(sql, id);
                table = Utils.MySqlHelpers.ExecuteDataTable(sql);

                if (table.Rows.Count > 0)
                {
                    ViewBag.id = Convert.ToInt32(table.Rows[0][0].ToString());
                    ViewBag.dname = table.Rows[0][1].ToString();
                }
            }

            return View();
        }

    }

    
}