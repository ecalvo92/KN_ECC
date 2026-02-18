$(function () {
  $("#FormLogin").validate({
    rules: {
      CorreoElectronico: {
        required: true,
        email: true
      },
      Contrasenna: {
        required: true
      }
    },
    messages: {
      CorreoElectronico: {
        required: "Campo requerido",
        email: "Formato incorrecto"
      },
      Contrasenna: {
        required: "Campo requerido"
      }
    },
    errorElement: "span",
    errorClass: "text-danger",
    highlight: function (element) {
      $(element).addClass("is-invalid");
    },
    unhighlight: function (element) {
      $(element).removeClass("is-invalid");
    }
  });
});