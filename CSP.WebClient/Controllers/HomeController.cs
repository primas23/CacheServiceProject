﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CSP.CacheService;

namespace CSP.WebClient.Controllers
{
    public class HomeController : Controller
    {
        [HttpCache(100)]
        public ActionResult Index(string param)
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}