$(document).ready(function () {
    // Aplicar la validación cuando se envíe el formulario
    $("form.needs-validation").on("submit", function (event) {
        var form = $(this)[0];
        var isValid = true;

        // Validar todos los campos de entrada requeridos (input y textarea)
        $(form).find("input[required], textarea[required]").each(function () {
            var input = $(this);

            // Verificar si el campo está vacío
            if (input.val().trim() === "") {
                input.addClass("is-invalid");
                input.siblings(".invalid-feedback").text("Este campo es obligatorio.");
                isValid = false;
            } else {
                input.removeClass("is-invalid");
                input.siblings(".invalid-feedback").text("");
            }
        });

        // Si el formulario no es válido, prevenir el envío
        if (!isValid) {
            event.preventDefault();
            event.stopPropagation();
        }
    });

    // Limpiar los mensajes de error cuando el usuario empieza a escribir
    $("input[required], textarea[required]").on("input", function () {
        var input = $(this);

        // Si el campo es válido, quitar la clase de error
        if (input.val().trim() !== "") {
            input.removeClass("is-invalid");
            input.siblings(".invalid-feedback").text("");
        }
    });
});
