$(function () {
  $("#FormRegistro").validate({
    rules: {
      Identificacion: {
        required: true
      },
      Nombre: {
        required: true
      },
      CorreoElectronico: {
        required: true,
        email: true
      },
      Contrasenna: {
        required: true
      }
    },
    messages: {
      Identificacion: {
        required: "Campo requerido"
      },
      Nombre: {
        required: "Campo requerido"
      },
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