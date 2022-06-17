using MyDigitalMenu.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyDigitalMenu.Controllers
{
    [AllowAnonymous]
    [SessionExpire]
    public class SepetController : Controller
    {
        // GET: Sepet
        public ActionResult Index()
        {
            return View();
        }
    }
}