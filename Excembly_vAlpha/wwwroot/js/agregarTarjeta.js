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
    const previewNombreTitular = document.getElementById("preview
nombreTitular"); 
    const previewNumeroTarjeta = document.getElementById("preview
numeroTarjeta"); 
    const previewFechaExpiracion = document.getElementById("preview
fechaExpiracion"); 
    const previewCVV = document.getElementById("preview-cvv");

    // Contenedor de la tarjeta 
    const tarjeta = document.getElementById("tarjeta");

    // Actualizar vista previa en tiempo real 
    const actualizarVistaPrevia = () => {
        previewNombreTitular.textContent =
            nombreTitular.value.trim().toUpperCase() || "NOMBRE TITULAR";
        previewNumeroTarjeta.textContent = numeroTarjeta.value.replace(/\D/g,
            "") || "0000 0000 0000 0000";
        previewFechaExpiracion.textContent =
            `${mesExpiracion.value.padStart(2,
                "0")}/${añoExpiracion.value.slice(-2)}` || "MM/AA";
    };

    [nombreTitular, numeroTarjeta, mesExpiracion,
        añoExpiracion].forEach((input) =>
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

        if (!/^[a-zA-Z\s]+$/.test(nombreTitular.value.trim())) {
            errores.push("El nombre debe contener solo letras y espacios.");
        }

        if (!/^\d{16}$/.test(numeroTarjeta.value.trim())) {
            errores.push("El número de tarjeta debe contener 16 dígitos.");
        }

        if (!/^(0[1-9]|1[0-2])$/.test(mesExpiracion.value.trim())) {
            errores.push("El mes debe estar entre 01 y 12.");
        }

        const year = parseInt(añoExpiracion.value, 10);
        if (isNaN(year) || year < 2024 || year > 2035) {
            errores.push("El año debe estar entre 2024 y 2035.");
        }

        if (!/^\d{3}$/.test(cvv.value.trim())) {
            errores.push("El CVV debe tener 3 dígitos.");
        }

        if (tipoTarjeta.value === "") {
            errores.push("Debe seleccionar un tipo de tarjeta.");
        }

        if (errores.length > 0) {
            event.preventDefault();
            alert(errores.join("\n"));
        }
    });
}); 