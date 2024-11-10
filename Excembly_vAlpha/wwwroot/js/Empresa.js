document.addEventListener("DOMContentLoaded", function () {
    const animatedElements = document.querySelectorAll(".animate-on-scroll");

    // Configuración de Intersection Observer para activar cuando el 50% del elemento es visible
    const observerOptions = {
        threshold: 0.5
    };

    // Callback del observer
    const observerCallback = (entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add("visible");
                observer.unobserve(entry.target); // Detener la observación una vez que el elemento es visible
            }
        });
    };

    // Crear el observer
    const observer = new IntersectionObserver(observerCallback, observerOptions);

    // Añadir observer a cada elemento
    animatedElements.forEach(element => observer.observe(element));

    // Cargar la vista parcial de reseñas usando AJAX
    $.ajax({
        url: '/Empresa/Reseñas', // Llama a la acción Reseñas del controlador Empresa
        type: 'GET',
        success: function (data) {
            $('#reseñasContainer').html(data); // Cargar la vista parcial en el contenedor
        },
        error: function () {
            $('#reseñasContainer').html("<p>Error al cargar las reseñas. Inténtelo más tarde.</p>");
        }
    });
});
