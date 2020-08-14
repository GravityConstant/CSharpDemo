using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FirstMVC.ControllersApi
{
    public class BaseApiController : ApiController
    {
        public HttpResponseMessage GetJsonMessage(string msg)
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent(msg, Encoding.UTF8, "application/json")
            };
        }
    }
}