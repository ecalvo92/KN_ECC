using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace KN_WEB.Services
{
    public class Generales
    {
        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
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

        public string GenerarContrasena()
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

    }
}