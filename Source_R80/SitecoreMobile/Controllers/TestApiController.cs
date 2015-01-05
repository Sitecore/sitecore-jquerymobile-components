using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SitecoreMobile.Controllers
{
    public class TestApiController : ApiController
    {
        [HttpGet]
        public string Echo(string echo)
        {
            return echo.Reverse().ToString();
        }
    }
}
