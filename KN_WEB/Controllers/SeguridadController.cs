using KN_WEB.Filters;
using KN_WEB.Models;
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

        [HttpPost]
        public ActionResult CambiarAcceso(CambiarAccesoModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            ViewBag.Mensaje = "Contraseña actualizada correctamente.";
            return View();
        }
    }
}