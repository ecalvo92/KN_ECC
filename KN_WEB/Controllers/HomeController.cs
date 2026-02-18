using KN_WEB.EntityFramework;
using KN_WEB.Models;
using System.Linq;
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
                //var result = context.tUsuario.Where(p => p.CorreoElectronico == modelo.CorreoElectronico 
                //                                      && p.Contrasenna == modelo.Contrasenna
                //                                      && p.Estado == true).FirstOrDefault();

                var result = context.IniciarSesion(modelo.CorreoElectronico, modelo.Contrasenna).FirstOrDefault();

                if (result == null)
                {
                    ViewBag.Mensaje = "Su información no se autenticó correctamente.";
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
                //var tabla = new tUsuario
                //{
                //    Identificacion = model.Identificacion,
                //    Nombre = model.Nombre,
                //    Contrasenna = model.Contrasenna,
                //    CorreoElectronico = model.CorreoElectronico,
                //    Estado = true
                //};

                //context.tUsuario.Add(tabla);
                //var result = context.SaveChanges();

                var result = context.RegistrarUsuario(model.Identificacion, model.Contrasenna, model.Nombre, model.CorreoElectronico);

                if (result <= 0)
                {
                    ViewBag.Mensaje = "Su información no se registró correctamente.";
                    return View();
                }

                return RedirectToAction("Login", "Home");
            }
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