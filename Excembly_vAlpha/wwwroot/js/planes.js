$(document).ready(function () {
    $(".adicional-checkbox").change(function () {
        let planDiv = $(this).closest(".plan");
        let subtotal = parseFloat(planDiv.find(".subtotal-valor").text().replace(/[^\d.-]/g, ''));
        let total = subtotal;

        // Recorre cada checkbox adicional para sumar los precios de los seleccionados
        planDiv.find(".adicional-checkbox:checked").each(function () {
            total += parseFloat($(this).data("precio"));
        });

        // Actualiza el valor mostrado del total
        planDiv.find(".total-valor").text(total.toLocaleString("es-MX", { style: "currency", currency: "MXN" }));
    });
});

