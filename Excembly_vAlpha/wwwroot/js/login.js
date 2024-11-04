$(document).ready(function () {
    // Manejar el evento de envío del formulario
    $("#loginForm").submit(function (event) {
        // Prevenir el envío del formulario si hay errores
        if (!validateForm()) {
            event.preventDefault();
            return;
        }

        // Si la validación es exitosa, puedes hacer otras operaciones aquí
    });

    function validateForm() {
        let isValid = true;

        // Limpiar mensajes de error previos
        $(".text-danger").text("");

        // Verificar cada campo
        if ($("#Correo").val().trim() === "") {
            $("#Correo").next(".text-danger").text("El correo es requerido.");
            isValid = false;
        }

        if ($("#Contraseña").val().trim() === "") {
            $("#Contraseña").next(".text-danger").text("La contraseña es requerida.");
            isValid = false;
        }

        return isValid;
    }

    // Función para mostrar errores devueltos por el servicio
    function showError(message) {
        // Suponiendo que hay un contenedor para mostrar mensajes de error
        let errorDiv = $("<div class='alert alert-danger'></div>").text(message);
        $("body").prepend(errorDiv);

        // Remover el mensaje de error después de 5 segundos
        setTimeout(function () {
            errorDiv.fadeOut();
        }, 5000);
    }

});
