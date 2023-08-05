// Utilizing AJAX to asynchronously delete a stylist after user-confirmation.
const deleteLinks = document.querySelectorAll('.delete-link');

// Create click handler for every deleteLink element.
deleteLinks.forEach((deleteLink) => {
    deleteLink.addEventListener('click', (e) => {
        e.preventDefault(); 

        // Grab the engineerId from the data-id attribute.
        let engineerId = deleteLink.getAttribute('data-id');
        let url = "/Engineers/Delete/" + engineerId;
        
        // Ask user for confirmation.
        if (confirm('Are you sure you want to delete this engineer?')) {
            // Initiates an AJAX request on confirmation.
            $.ajax({
                // Route and type of request.
                url: url,
                type: 'POST',
                // Delete route requires an Id.
                data: { id: engineerId },
                // The controller sends back Ok() if successful.
                success: function(result) {
                    location.reload();
                },
                error: function(result) {
                    alert("Error deleting engineer. Please try again later.");
                }
            });
        }
    });
});
