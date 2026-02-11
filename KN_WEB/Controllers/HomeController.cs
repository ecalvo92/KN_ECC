using KN_WEB.EntityFramework;
using KN_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KN_WEB.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #region Iniciar Sesión

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsuarioModel modelo)
        {            
            using (var context = new KN_DBEntities())
            {
                var result = context.tUsuario.Where(p => p.Identificacion == modelo.Identificacion 
                                                       && p.Contrasenna == modelo.Contrasenna).FirstOrDefault();

                if (result == null)
                {
                    ViewBag.Mensaje = "Usuario o contraseña incorrecta.";
                    return View();
                }

                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

        #region Registrar Usuario

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(UsuarioModel model)
        {
            using (var context = new KN_DBEntities())
            {
                var tabla = new tUsuario
                {
                    Identificacion = model.Identificacion,
                    Nombre = model.Nombre,
                    Contrasenna = model.Contrasenna
                };

                context.tUsuario.Add(tabla);
                context.SaveChanges();
            }
        
            return View();
        }

        #endregion

        #region Recuperar Contraseña

        [HttpGet]
        public ActionResult RecuperarContrasenna()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarContrasenna(UsuarioModel modelo)
        {
            return View();
        }

        #endregion

    }
}