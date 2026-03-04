$(function () {
  $("#FormRecuperarContrasenna").validate({
    rules: {
      CorreoElectronico: {
        required: true,
        email: true
      }
    },
    messages: {
      CorreoElectronico: {
        required: "Campo requerido",
        email: "Formato incorrecto"
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