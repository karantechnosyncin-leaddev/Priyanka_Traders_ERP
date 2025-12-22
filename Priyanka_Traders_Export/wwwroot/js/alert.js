

    // Show alert function
function Notify(type, title, message) {
        // Get icon class based on type
        let iconClass;
        switch (type) {
            case 'success':
                iconClass = 'bi-check-circle-fill';
                break;
            case 'error':
                iconClass = 'bi-x-circle-fill';
                break;
            case 'warning':
                iconClass = 'bi-exclamation-triangle-fill';
                break;
            case 'info':
                iconClass = 'bi-info-circle-fill';
                break;
            default:
                iconClass = 'bi-info-circle-fill';
        }

        // Create alert element
        const alertId = 'alert-' + Date.now();
        const alertHtml = `
            <div id="${alertId}" class="custom-alert custom-alert-${type}">
                <i class="custom-alert-icon bi ${iconClass}"></i>
                <div class="custom-alert-content">
                    <div class="custom-alert-title">${title || ""}</div>
                    <div class="custom-alert-message">${message || ""}</div>
                </div>
                <button class="custom-alert-close">&times;</button>
            </div>
        `;

        // Append to container
        $('#alertContainer').append(alertHtml);

        // Show the alert with animation
        setTimeout(() => {
            $(`#${alertId}`).addClass('show');
        }, 10);

        // Auto-close after 5 seconds
        const autoClose = setTimeout(() => {
            closeAlert(alertId);
        }, 5000);

        // Close button click handler
        $(`#${alertId} .custom-alert-close`).click(function () {
            clearTimeout(autoClose);
            closeAlert(alertId);
        });
    }

    // Close alert function
    function closeAlert(id) {
        const alert = $(`#${id}`);
        alert.removeClass('show').addClass('hide');

        // Remove from DOM after animation completes
        setTimeout(() => {
            alert.remove();
        }, 300);
    }
