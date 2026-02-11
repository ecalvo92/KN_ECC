using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KN_WEB.Models
{
    public class UsuarioModel
    {
        public int Consecutivo { get; set; }
        public string Identificacion { get; set; }
        public string Contrasenna { get; set; }
        public string Nombre { get; set; }
    }
}