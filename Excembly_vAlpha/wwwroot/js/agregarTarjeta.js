document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector("form");

    // Elementos del formulario
    const nombreTitular = document.getElementById("nombreTitular");
    const numeroTarjeta = document.getElementById("numeroTarjeta");
    const mesExpiracion = document.getElementById("mesExpiracion");
    const añoExpiracion = document.getElementById("añoExpiracion");
    const cvv = document.getElementById("cvv");
    const tipoTarjeta = document.getElementById("tipoTarjeta");

    // Elementos de vista previa
    const previewNombreTitular = document.getElementById("previewnombreTitular");
    const previewNumeroTarjeta = document.getElementById("previewnumeroTarjeta");
    const previewFechaExpiracion = document.getElementById("previewfechaExpiracion");
    const previewCVV = document.getElementById("preview-cvv");

    // Contenedor de la tarjeta
    const tarjeta = document.getElementById("tarjeta");

    // Actualizar vista previa en tiempo real
    const actualizarVistaPrevia = () => {
        previewNombreTitular.textContent =
            nombreTitular.value.trim().toUpperCase() || "NOMBRE TITULAR";

        previewNumeroTarjeta.textContent =
            numeroTarjeta.value.replace(/\D/g, "") || "0000 0000 0000 0000";

        previewFechaExpiracion.textContent =
            `${mesExpiracion.value.padStart(2, "0")}/${añoExpiracion.value.slice(-2)}` || "MM/AA";
    };
    document.querySelector('.tarjeta-preview').addEventListener('click', function () {
        this.classList.toggle('flip');
    });


    [nombreTitular, numeroTarjeta, mesExpiracion, añoExpiracion].forEach((input) =>
        input.addEventListener("input", actualizarVistaPrevia)
    );

    // Mostrar CVV en la tarjeta (solo en reverso)
    cvv.addEventListener("input", () => {
        previewCVV.textContent = cvv.value || "***";
    });

    // Girar tarjeta al enfocar/desenfocar el campo CVV
    cvv.addEventListener("focus", () => {
        tarjeta.classList.add("flip");
    });

    cvv.addEventListener("blur", () => {
        tarjeta.classList.remove("flip");
    });

    // Validación del formulario
    form.addEventListener("submit", (event) => {
        const errores = [];

        // Validar nombre del titular
        if (!/^[a-zA-Z\s]+$/.test(nombreTitular.value.trim())) {
            errores.push("El nombre debe contener solo letras y espacios.");
        }

        // Validar número de tarjeta
        if (!/^\d{16}$/.test(numeroTarjeta.value.trim())) {
            errores.push("El número de tarjeta debe contener 16 dígitos.");
        }

        // Validar mes de expiración
        if (!/^(0[1-9]|1[0-2])$/.test(mesExpiracion.value.trim())) {
            errores.push("El mes debe estar entre 01 y 12.");
        }

        // Validar año de expiración
        const year = parseInt(añoExpiracion.value, 10);
        if (isNaN(year) || year < 2024 || year > 2035) {
            errores.push("El año debe estar entre 2024 y 2035.");
        }

        // Validar CVV
        if (!/^\d{3}$/.test(cvv.value.trim())) {
            errores.push("El CVV debe tener 3 dígitos.");
        }

        // Validar tipo de tarjeta
        if (tipoTarjeta.value === "") {
            errores.push("Debe seleccionar un tipo de tarjeta.");
        }

        // Mostrar errores y evitar el envío si hay errores
        if (errores.length > 0) {
            event.preventDefault();
            alert(errores.join("\n"));
        }
    });
});
