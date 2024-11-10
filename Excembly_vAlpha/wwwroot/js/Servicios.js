// Verifica si el elemento es visible en la pantalla
function isVisible(element) {
    const rect = element.getBoundingClientRect();
    return rect.top < window.innerHeight && rect.bottom >= 0;
}

// Aplica la clase 'visible' a los elementos visibles
function revealOnScroll() {
    const services = document.querySelectorAll('.servicio');
    services.forEach(servicio => {
        if (isVisible(servicio)) {
            servicio.classList.add('visible');
        } else {
            servicio.classList.remove('visible');
        }
    });
}


// Ejecuta revealOnScroll al cargar la página y al hacer scroll
window.addEventListener('load', revealOnScroll);
window.addEventListener('scroll', revealOnScroll);

