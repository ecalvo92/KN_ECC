using KN_WEB.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_WEB.Controllers
{
    [SesionActiva]
    public class SeguridadController : Controller
    {
        [HttpGet]
        public ActionResult CambiarAcceso()
        {
            return View();
        }
    }
}