using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCECharts.Models;

namespace MVCECharts.Controllers
{
    public class HomeController : Controller
    {
        DBShoping db= new DBShoping();
        // GET: Home
        public ActionResult Index()
        {
            db.ShopingType.Add(new ShopingType {ShopingName = "衣服", ShopngingDesc = "保暖"});
            db.SaveChanges();
            return View();
        }
    }
}