﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
namespace Dr_Heart_Specialist_api.Controllers
{
    public class HomeController :Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(); ;
        }
    }
}
