// Contratacion.js
document.addEventListener("DOMContentLoaded", function () {
    // Confirmación antes de cancelar una contratación
    const cancelarBotones = document.querySelectorAll(".btn-danger");
    cancelarBotones.forEach(boton => {
        boton.addEventListener("click", function (e) {
            const confirmacion = confirm("¿Estás seguro de que deseas cancelar esta contratación?");
            if (!confirmacion) {
                e.preventDefault();
            }
        });
    });

    // Efecto de hover para destacar las tarjetas
    const cards = document.querySelectorAll(".card");
    cards.forEach(card => {
        card.addEventListener("mouseenter", function () {
            card.style.boxShadow = "0 6px 12px rgba(0, 0, 0, 0.2)";
        });

        card.addEventListener("mouseleave", function () {
            card.style.boxShadow = "0 4px 8px rgba(0, 0, 0, 0.1)";
        });
    });

    // Función para ajustar el formato de las imágenes dinámicamente
    const tecnicosFotos = document.querySelectorAll(".img-fluid");
    tecnicosFotos.forEach(foto => {
        foto.addEventListener("error", function () {
            // Si la imagen no se carga, reemplazarla con una imagen por defecto
            this.src = "/path/to/default-image.jpg";
        });
    });
});