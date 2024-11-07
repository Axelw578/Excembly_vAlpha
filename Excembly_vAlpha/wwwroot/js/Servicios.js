$(document).ready(function () {
    // Inicializar tooltips de Bootstrap
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Configuración del modal
    $('.boton').on('click', function (event) {
        event.preventDefault();

        let servicio = $(this).closest('.servicio');
        let nombre = servicio.find('.header').text();
        let precio = servicio.find('.precio').text();

        $('#nombreServicio').text(nombre);
        $('#precioServicio').text(precio);
        $('#modalConfirmacion').modal('show');
    });

    // Confirmación de contratación
    $('#confirmarContratacion').on('click', function () {
        alert("Servicio contratado.");
        $('#modalConfirmacion').modal('hide');
    });
});
