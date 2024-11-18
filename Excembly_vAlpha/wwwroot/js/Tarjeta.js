// Función para abrir el modal de edición y cargar datos
function openEditModal(tarjetaId, nombreTitular, mesExpiracion, añoExpiracion, cvv) {
    try {
        // Rellenar los campos del formulario de edición
        document.getElementById('editCardId').value = tarjetaId;
        document.getElementById('editNombreTitular').value = nombreTitular;
        document.getElementById('editMesExpiracion').value = mesExpiracion;
        document.getElementById('editAnoExpiracion').value = añoExpiracion;
        document.getElementById('editCVV').value = cvv;

        // Inicializar y mostrar el modal
        const editModal = new bootstrap.Modal(document.getElementById('editModal'));
        editModal.show();
    } catch (error) {
        console.error("Error al abrir el modal de edición:", error);
        alert("Ocurrió un error al intentar abrir el modal de edición. Revisa la consola para más detalles.");
    }
}

// Función para abrir el modal de confirmación de eliminación
function openDeleteModal(tarjetaId) {
    try {
        // Rellenar el ID de la tarjeta a eliminar
        document.getElementById('deleteCardId').value = tarjetaId;

        // Inicializar y mostrar el modal
        const deleteModal = new bootstrap.Modal(document.getElementById('deleteModal'));
        deleteModal.show();
    } catch (error) {
        console.error("Error al abrir el modal de eliminación:", error);
        alert("Ocurrió un error al intentar abrir el modal de eliminación. Revisa la consola para más detalles.");
    }
}

// Función para guardar los cambios de la tarjeta editada
async function saveCardEdits(event) {
    event.preventDefault(); // Prevenir el comportamiento por defecto del formulario.

    const cardData = {
        cardId: document.getElementById('editCardId').value,
        nombreTitular: document.getElementById('editNombreTitular').value,
        mesExpiracion: parseInt(document.getElementById('editMesExpiracion').value),
        anoExpiracion: parseInt(document.getElementById('editAnoExpiracion').value),
        cvv: document.getElementById('editCVV').value
    };

    console.log("Datos enviados al servidor:", cardData);

    try {
        const response = await fetch('/Editar', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(cardData)
        });

        const result = await response.json();

        console.log("Respuesta del servidor:", result);

        if (!result.success) {
            console.error("Error al editar tarjeta:", result.message);
            if (result.details) {
                console.error("Detalles del error:", result.details.join(", "));
            }
            alert(`Error: ${result.message}`);
        } else {
            alert('Tarjeta editada exitosamente.');
            $('#editModal').modal('hide');
            location.reload(); // Recargar la página para reflejar los cambios
        }
    } catch (error) {
        console.error("Error en la comunicación con el servidor:", error);
        alert('Ocurrió un error inesperado al intentar editar la tarjeta.');
    }
}


// Función para confirmar la eliminación de una tarjeta
async function confirmDelete() {
    const tarjetaId = document.getElementById('deleteCardId').value;

    try {
        const response = await fetch(`/TarjetasGuardadas/Eliminar`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ tarjetaId })
        });

        const result = await response.json();
        if (result.success) {
            console.log("Tarjeta eliminada exitosamente.");
            alert("Tarjeta eliminada exitosamente.");
            location.reload(); // Refrescar la página para mostrar los cambios
        } else {
            console.error("Error al eliminar tarjeta:", result.message);
            alert(`Error al eliminar tarjeta: ${result.message}`);
        }
    } catch (error) {
        console.error("Error en la solicitud de eliminación:", error);
        alert("Ocurrió un error al eliminar la tarjeta. Revisa la consola para más detalles.");
    }
}

// Función para seleccionar una tarjeta (opcional, según el flujo de tu app)
function selectCard(tarjetaId) {
    console.log(`Tarjeta seleccionada con ID: ${tarjetaId}`);
    alert(`Tarjeta seleccionada con ID: ${tarjetaId}`);
    // Aquí puedes manejar la selección de la tarjeta según sea necesario
}
