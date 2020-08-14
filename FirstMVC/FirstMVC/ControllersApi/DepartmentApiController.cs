using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace FirstMVC.ControllersApi
{
    public class DepartmentApiController : BaseApiController
    {
        public HttpResponseMessage Get()
        {
            string sql = "select id, dname from angel_sys_department";
            DataTable table = Utils.MySqlHelpers.ExecuteDataTable(sql);

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(table, Newtonsoft.Json.Formatting.None,
                new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                });//json序列化


            return GetJsonMessage(JsonString);
        }
        public HttpResponseMessage Post()
        {

            int id = Convert.ToInt32(HttpContext.Current.Request.Form["id"]);
            string dname = HttpContext.Current.Request.Form["dname"];
            Debug.WriteLine("id:" + id.ToString() + ", dname:" + dname);

            string sql = "update angel_sys_department set dname='{0}' where id={1}";
            sql = string.Format(sql, dname, id);
            int count = Utils.MySqlHelpers.ExecuteNonQuery(sql);

            int code;
            string msg;
            if (count == 0)
            {
                code = 1;
                msg = "没更改";
            } 
            else
            {
                code = 0;
                msg = "更改了{0}行";
            }
            msg = string.Format(msg, count);

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(count, Newtonsoft.Json.Formatting.None);//json序列化

            string json = "\"code\":{0},\"msg\":\"{1}\",\"obj\":";//json内容格式
            json = string.Format(json, code.ToString(), msg);
            json = "{" + json + JsonString + "}";
            Debug.WriteLine("post: " + json);
            return GetJsonMessage(json);
        }
    }

    public class Department
    {
        public int id;
        public string dname;
    }
}