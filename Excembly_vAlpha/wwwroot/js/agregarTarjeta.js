document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector("form");

    // Elementos del formulario
    const nombreTitular = document.getElementById("nombreTitular");
    const numeroTarjeta = document.getElementById("numeroTarjeta");
    const mesExpiracion = document.getElementById("mesExpiracion");
    const añoExpiracion = document.getElementById("añoExpiracion");

    // Elementos de vista previa
    const previewNombreTitular = document.getElementById("preview-nombreTitular");
    const previewNumeroTarjeta = document.getElementById("preview-numeroTarjeta");
    const previewFechaExpiracion = document.getElementById("preview-fechaExpiracion");

    // Actualizar vista previa en tiempo real
    nombreTitular.addEventListener("input", () => {
        previewNombreTitular.textContent = nombreTitular.value.trim().toUpperCase() || "NOMBRE TITULAR";
    });

    numeroTarjeta.addEventListener("input", () => {
        let value = numeroTarjeta.value.replace(/\D/g, ""); // Eliminar caracteres no numéricos
        numeroTarjeta.value = value.trim(); // Actualizar el valor del campo de entrada
        previewNumeroTarjeta.textContent = value || "0000 0000 0000 0000"; // Mostrar en la vista previa
    });

    const actualizarFechaExpiracion = () => {
        const mes = mesExpiracion.value.padStart(2, "0"); // Agregar ceros a la izquierda si es necesario
        const año = añoExpiracion.value.slice(-2); // Tomar los últimos 2 dígitos del año
        previewFechaExpiracion.textContent = `${mes}/${año}` || "MM/AA";
    };

    mesExpiracion.addEventListener("input", actualizarFechaExpiracion);
    añoExpiracion.addEventListener("input", actualizarFechaExpiracion);

    // Validación general al enviar el formulario
    form.addEventListener("submit", (event) => {
        let valid = true;

        // Validar Nombre del Titular (sólo letras y espacios)
        if (!/^[a-zA-Z\s]+$/.test(nombreTitular.value.trim())) {
            alert("El nombre del titular solo debe contener letras y espacios.");
            valid = false;
        }

        // Validar Mes de Expiración (1-12)
        if (!/^(0[1-9]|1[0-2])$/.test(mesExpiracion.value.trim())) {
            alert("El mes de expiración debe estar entre 01 y 12.");
            valid = false;
        }

        // Validar Año de Expiración (2024-2035)
        const currentYear = new Date().getFullYear();
        const yearValue = parseInt(añoExpiracion.value.trim(), 10);
        if (!/^\d{4}$/.test(añoExpiracion.value.trim()) || yearValue < 2024 || yearValue > 2035) {
            alert("El año de expiración debe estar entre 2024 y 2035.");
            valid = false;
        }

        // Validar CVV (3 dígitos numéricos)
        const cvv = document.getElementById("cvv");
        if (!/^\d{3}$/.test(cvv.value.trim())) {
            alert("El CVV debe contener exactamente 3 dígitos.");
            valid = false;
        }

        // Validar Tipo de Tarjeta (Debe seleccionarse una opción válida)
        const tipoTarjeta = document.getElementById("tipoTarjeta");
        if (tipoTarjeta.value === "") {
            alert("Debe seleccionar el tipo de tarjeta.");
            valid = false;
        }

        // Detener envío del formulario si alguna validación falla
        if (!valid) {
            event.preventDefault();
        }
    });
});
