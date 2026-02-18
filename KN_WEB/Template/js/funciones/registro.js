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

function ConsultarNombre() {

  //$("#Nombre").val("");
  document.getElementById("Nombre").value = "";

  let identificacion = document.getElementById("Identificacion").value;

  if (identificacion.length >= 9) {
    $.ajax({
      url: `https://apis.gometa.org/cedulas/${identificacion}`,
      type: 'GET',
      dataType: 'json',
      success: function (response) {

        if (response?.results?.[0]?.fullname)
          document.getElementById("Nombre").value = response.results[0].fullname;

      }
    });
  }

}