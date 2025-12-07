/**
 * Helper para mostrar notificaciones Toast usando Bootstrap 5
 */
const Notificaciones = {
    /**
     * Muestra una notificación de éxito
     * @param {string} mensaje - Mensaje a mostrar
     * @param {string} titulo - Título opcional (por defecto "Éxito")
     */
    exito: function(mensaje, titulo = 'Éxito') {
        this.mostrar(mensaje, titulo, 'success');
    },

    /**
     * Muestra una notificación de error
     * @param {string} mensaje - Mensaje a mostrar
     * @param {string} titulo - Título opcional (por defecto "Error")
     */
    error: function(mensaje, titulo = 'Error') {
        this.mostrar(mensaje, titulo, 'danger');
    },

    /**
     * Muestra una notificación de información
     * @param {string} mensaje - Mensaje a mostrar
     * @param {string} titulo - Título opcional (por defecto "Información")
     */
    info: function(mensaje, titulo = 'Información') {
        this.mostrar(mensaje, titulo, 'info');
    },

    /**
     * Muestra una notificación de advertencia
     * @param {string} mensaje - Mensaje a mostrar
     * @param {string} titulo - Título opcional (por defecto "Advertencia")
     */
    advertencia: function(mensaje, titulo = 'Advertencia') {
        this.mostrar(mensaje, titulo, 'warning');
    },

    /**
     * Muestra una notificación Toast
     * @param {string} mensaje - Mensaje a mostrar
     * @param {string} titulo - Título de la notificación
     * @param {string} tipo - Tipo de notificación: 'success', 'danger', 'info', 'warning'
     */
    mostrar: function(mensaje, titulo, tipo) {
        // Crear contenedor de toasts si no existe
        let toastContainer = document.getElementById('toast-container');
        if (!toastContainer) {
            toastContainer = document.createElement('div');
            toastContainer.id = 'toast-container';
            toastContainer.className = 'toast-container position-fixed top-0 end-0 p-3';
            toastContainer.style.zIndex = '9999';
            document.body.appendChild(toastContainer);
        }

        // Crear el toast
        const toastId = 'toast-' + Date.now();
        const toastHtml = `
            <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="5000">
                <div class="toast-header bg-${tipo} text-white">
                    <i class="fas ${this.getIcono(tipo)} me-2"></i>
                    <strong class="me-auto">${titulo}</strong>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
                <div class="toast-body">
                    ${mensaje}
                </div>
            </div>
        `;

        toastContainer.insertAdjacentHTML('beforeend', toastHtml);

        // Mostrar el toast
        const toastElement = document.getElementById(toastId);
        const toast = new bootstrap.Toast(toastElement);
        toast.show();

        // Remover el elemento después de que se oculte
        toastElement.addEventListener('hidden.bs.toast', function() {
            toastElement.remove();
        });
    },

    /**
     * Obtiene el ícono según el tipo de notificación
     * @param {string} tipo - Tipo de notificación
     * @returns {string} - Clase del ícono FontAwesome
     */
    getIcono: function(tipo) {
        const iconos = {
            'success': 'fa-check-circle',
            'danger': 'fa-exclamation-circle',
            'info': 'fa-info-circle',
            'warning': 'fa-exclamation-triangle'
        };
        return iconos[tipo] || 'fa-info-circle';
    }
};

// Función global para compatibilidad
window.mostrarNotificacion = function(mensaje, tipo = 'info') {
    const tipos = {
        'success': () => Notificaciones.exito(mensaje),
        'error': () => Notificaciones.error(mensaje),
        'danger': () => Notificaciones.error(mensaje),
        'info': () => Notificaciones.info(mensaje),
        'warning': () => Notificaciones.advertencia(mensaje)
    };
    
    const funcion = tipos[tipo] || tipos['info'];
    funcion();
};

