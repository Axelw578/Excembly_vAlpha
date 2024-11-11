$(document).ready(function () {
    // Manejar el envío del formulario
    $('#registroForm').on('submit', function (e) {
        e.preventDefault(); // Evitar el envío normal del formulario

        // Limpiar mensajes de error anteriores y el mensaje de carga
        $('.error-message').remove();
        $('.loading-message').remove();

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

        // Validar teléfono (opcional, agregar regla de validación si es necesario)
        if (telefono && !validatePhone(telefono)) {
            showError('#Telefono', 'El número de teléfono no es válido.');
            valid = false;
        }

        // Validar URL de la foto de perfil (opcional, solo si el campo es obligatorio)
        if (fotoPerfilUrl && !validateUrl(fotoPerfilUrl)) {
            showError('#FotoPerfilUrl', 'La URL de la foto de perfil no es válida.');
            valid = false;
        }

        // Si el formulario es válido, enviar el formulario
        if (valid) {
            // Mostrar mensaje de carga
            $('#registroForm').append('<div class="loading-message text-info">Enviando...</div>');
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

    // Función para validar el formato del número de teléfono (ejemplo simple, adaptar según sea necesario)
    function validatePhone(phone) {
        const phoneRe = /^\d{10}$/; // Acepta solo números de 10 dígitos
        return phoneRe.test(phone);
    }

    // Función para validar URLs (opcional)
    function validateUrl(url) {
        const urlRe = /^(https?:\/\/)?([\w\-]+\.)+[\w\-]+(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$/;
        return urlRe.test(url);
    }
});
