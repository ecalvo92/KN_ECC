using System.ComponentModel.DataAnnotations;

namespace KN_WEB.Models
{
    public class CambiarAccesoModel
    {
        [Required(ErrorMessage = "La contraseña nueva es requerida.")]
        public string ContrasennaNueva { get; set; }

        [Required(ErrorMessage = "La confirmación de contraseña es requerida.")]
        [Compare("ContrasennaNueva", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarContrasenna { get; set; }
    }
}
