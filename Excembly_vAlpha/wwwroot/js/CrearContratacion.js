document.addEventListener("DOMContentLoaded", function () {
    // Parsear los servicios adicionales desde el elemento oculto en el HTML
    const serviciosAdicionalesPorPlan = JSON.parse(document.getElementById("serviciosAdicionalesData").textContent);

    // Obtener las tarjetas seleccionables
    const selectableCards = document.querySelectorAll(".selectable-card");

    // Agregar evento click a cada tarjeta
    selectableCards.forEach(card => {
        card.addEventListener("click", function () {
            const radio = this.querySelector(".plan-selector");

            if (radio && !radio.checked) {
                radio.checked = true; // Selecciona el radio button
                radio.dispatchEvent(new Event("change")); // Dispara el evento de cambio manualmente
            }
            highlightSelectedCard(); // Aplica estilos de selección
        });

        // Efecto de hover 
        card.addEventListener("mouseenter", function () {
            this.style.transform = "scale(1.01)";
            this.style.transition = "transform 0.2s ease-in-out";
        });

        card.addEventListener("mouseleave", function () {
            this.style.transform = "scale(1)";
        });
    });

    // Función para manejar el cambio en los radio buttons
    document.querySelectorAll('.plan-selector').forEach(planRadio => {
        planRadio.addEventListener('change', function () {
            const planId = this.dataset.planId;
            const servicios = serviciosAdicionalesPorPlan[planId] || [];
            const serviciosContainer = document.getElementById('serviciosAdicionalesList');
            serviciosContainer.innerHTML = "";

            if (servicios.length > 0) {
                servicios.forEach(servicio => {
                    const item = `
                        <li class="list-group-item">
                            <div class="d-flex justify-content-between align-items-center">
                                <div>
                                    <strong>${servicio.NombreServicio}</strong>
                                    <span class="text-success">${servicio.PrecioConDescuento.toFixed(2)} MXN</span>
                                    <span class="text-muted text-decoration-line-through">${servicio.PrecioOriginal.toFixed(2)} MXN</span>
                                    <span class="badge bg-success">-${(servicio.Descuento * 100).toFixed(0)}%</span>
                                </div>
                                <input type="checkbox" name="ServiciosAdicionalesIds" value="${servicio.ServicioId}" />
                            </div>
                        </li>`;
                    serviciosContainer.insertAdjacentHTML('beforeend', item);
                });
            } else {
                serviciosContainer.innerHTML = "<li class='list-group-item'>Este plan no tiene servicios adicionales.</li>";
            }
        });
    });

    // Función para resaltar la tarjeta seleccionada
    function highlightSelectedCard() {
        // Quitar selección previa
        selectableCards.forEach(card => card.classList.remove("selected-card"));

        // Agregar clase a la tarjeta seleccionada
        const selectedCard = document.querySelector(".selectable-card input:checked")?.closest(".selectable-card");
        if (selectedCard) {
            selectedCard.classList.add("selected-card");
        }
    }

    // Llama a la función inicialmente si ya hay un seleccionado
    highlightSelectedCard();

    const individualServices = document.querySelectorAll(".individual-service");

    // Agregar evento click a cada contenedor de servicio
    individualServices.forEach(service => {
        service.addEventListener("click", function () {
            const checkbox = this.querySelector(".service-checkbox");
            checkbox.checked = !checkbox.checked; // Cambiar el estado del checkbox
            toggleSelectedService(); // Aplicar estilos de selección
        });

        // Efecto de hover (iluminación)
        service.addEventListener("mouseenter", function () {
            this.style.transform = "scale(1.02)";
        });

        service.addEventListener("mouseleave", function () {
            this.style.transform = "scale(1)";
        });
    });

    // Función para aplicar estilos de selección
    function toggleSelectedService() {
        individualServices.forEach(service => {
            const checkbox = service.querySelector(".service-checkbox");
            if (checkbox.checked) {
                service.classList.add("selected-service");
            } else {
                service.classList.remove("selected-service");
            }
        });
    }

    // Llama a la función inicialmente si ya hay servicios seleccionados
    toggleSelectedService();


    document.ready(function () {
        // Al seleccionar un plan, cargar los servicios adicionales correspondientes
        ("input[name='PlanId']").change(function () {
            var planId = $(this).val();
            var servicios = JSON.parse($("#serviciosAdicionalesData").text())[planId];

            // Limpiar la lista de servicios adicionales
            $("#serviciosAdicionalesList").empty();

            if (servicios && servicios.length > 0) {
                servicios.forEach(function (servicio) {
                    $("#serviciosAdicionalesList").append(
                        `<li class="list-group-item">
                        <div class="d-flex justify-content-between align-items-center">
                            <div class="d-flex align-items-center">
                                <img src="${servicio.Imagen}" alt="${servicio.Nombre}" class="rounded me-3" style="width: 50px; height: 50px; object-fit: cover;" />
                                <div>
                                    <strong>${servicio.Nombre}</strong> - ${servicio.Precio.toFixed(2)}
                                    <p class="text-muted mb-0">${servicio.Descripcion}</p>
                                </div>
                            </div>
                            <input type="checkbox" name="ServiciosAdicionalesIds" value="${servicio.ServicioAdicionalId}" class="service-checkbox" />
                        </div>
                    </li>`
                    );
                });
            } else {
                $("#serviciosAdicionalesList").append("<li class='list-group-item'>No hay servicios adicionales disponibles.</li>");
            }
        });
    });

});