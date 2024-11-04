$(document).ready(function () {
    // Manejar el envío del formulario
    $('#registroForm').on('submit', function (e) {
        e.preventDefault(); // Evitar el envío normal del formulario

        // Limpiar mensajes de error anteriores
        $('.error-message').remove();

        // Validar campos
        let valid = true;
        const nombre = $('#Nombre').val().trim();
        const apellidos = $('#Apellidos').val().trim();
        const correo = $('#Correo').val().trim();
        const contrasena = $('#Contraseña').val().trim();
        const confirmarContrasena = $('#ConfirmarContraseña').val().trim();
        const telefono = $('#Telefono').val().trim();
        const fotoPerfilUrl = $('#FotoPerfilUrl').val().trim();

        // Validar el nombre
        if (!nombre) {
            showError('#Nombre', 'El nombre es obligatorio.');
            valid = false;
        }

        // Validar apellidos
        if (!apellidos) {
            showError('#Apellidos', 'Los apellidos son obligatorios.');
            valid = false;
        }

        // Validar correo
        if (!correo) {
            showError('#Correo', 'El correo electrónico es obligatorio.');
            valid = false;
        } else if (!validateEmail(correo)) {
            showError('#Correo', 'El correo electrónico no es válido.');
            valid = false;
        }

        // Validar contraseña
        if (!contrasena) {
            showError('#Contraseña', 'La contraseña es obligatoria.');
            valid = false;
        } else if (contrasena.length < 6) {
            showError('#Contraseña', 'La contraseña debe tener al menos 6 caracteres.');
            valid = false;
        }

        // Validar confirmación de contraseña
        if (contrasena !== confirmarContrasena) {
            showError('#ConfirmarContraseña', 'Las contraseñas no coinciden.');
            valid = false;
        }

        // Si el formulario es válido, enviar el formulario
        if (valid) {
            // Mostrar mensaje de espera (puedes personalizar esto)
            $('#registroForm').append('<div class="loading-message">Enviando...</div>');
            this.submit(); // Enviar el formulario
        }
    });

    // Función para mostrar mensajes de error
    function showError(selector, message) {
        $(selector).after(`<div class="error-message text-danger">${message}</div>`);
    }

    // Función para validar el formato del correo electrónico
    function validateEmail(email) {
        const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return re.test(String(email).toLowerCase());
    }
});
