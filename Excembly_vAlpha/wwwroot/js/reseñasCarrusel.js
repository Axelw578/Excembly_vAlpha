
// Mostrar imagen en un modal al hacer clic
$(document).ready(function () {
    $('.comentario-img').click(function () {
        const imgSrc = $(this).attr('src');
        const modal = `
            <div class="modal-backdrop">
                <img src="${imgSrc}" class="modal-image" alt="Imagen Ampliada">
            </div>`;

        $('body').append(modal).css('overflow', 'hidden'); // Bloquea el scroll
    });

    // Cerrar modal y restaurar el scroll
    $(document).on('click', '.modal-backdrop', function () {
        $(this).remove();
        $('body').css('overflow', 'auto'); // Restaura el scroll
    });
});

