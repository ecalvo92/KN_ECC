using KN_WEB.EntityFramework;
using KN_WEB.Filters;
using KN_WEB.Models;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace KN_WEB.Controllers
{
    public class HomeController : Controller
    {
        [SesionActiva]
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

                Session["Nombre"] = result.Nombre;
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
            using (var context = new KN_DBEntities())
            {
                var result = context.ValidarCorreo(modelo.CorreoElectronico).FirstOrDefault();

                if (result == null)
                {
                    ViewBag.Mensaje = "Su información no se validó correctamente.";
                    return View();
                }

                //Se genera la contraseña nueva
                var nuevaContrasenna = GenerarContrasena();

                //Se actualiza la contraseña en la base de datos
                var actualizacion = context.ActualizarContrasenna(nuevaContrasenna, result.Consecutivo);

                if (actualizacion <= 0)
                {
                    ViewBag.Mensaje = "Su información no se actualizó correctamente.";
                    return View();
                }

                //Se envía un correo electrónico al usuario con la nueva contraseña
                string rutaHtml = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template", "RecuperarContrasenna.html");
                string contenidoHtml = System.IO.File.ReadAllText(rutaHtml);

                string htmlFinal = contenidoHtml
                    .Replace("{{NOMBRE_USUARIO}}", result.Nombre)
                    .Replace("{{NUEVA_CONTRASENA}}", nuevaContrasenna);

                EnviarCorreo(result.CorreoElectronico, "Recuperar Acceso", htmlFinal);
                return RedirectToAction("Login", "Home");
            }
        }

        #endregion

        #region Cerrar Sesión

        [SesionActiva]
        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

        #endregion

        private string GenerarContrasena()
        {
            int longitud = 8;
            const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder resultado = new StringBuilder(longitud);

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[1];
                for (int i = 0; i < longitud; i++)
                {
                    rng.GetBytes(bytes);
                    int index = bytes[0] % letras.Length;
                    resultado.Append(letras[index]);
                }
            }

            return resultado.ToString();
        }

        private void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            var cuentaCorreo = ConfigurationManager.AppSettings["CuentaCorreo"];
            var contrasennaCorreo = ConfigurationManager.AppSettings["contrasennaCorreo"];

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(cuentaCorreo);
                mail.To.Add(destinatario);
                mail.Subject = asunto;
                mail.Body = cuerpo;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.office365.com", 587))
                {
                    smtp.Credentials = new NetworkCredential(cuentaCorreo, contrasennaCorreo);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }

    }
}