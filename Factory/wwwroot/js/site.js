// Utilizing AJAX to asynchronously delete a stylist after user-confirmation.
const deleteLinks = document.querySelectorAll('.delete-link');

// Create click handler for every deleteLink element.
deleteLinks.forEach((deleteLink) => {
    deleteLink.addEventListener('click', (e) => {
        e.preventDefault(); 

        // Grab the Id from the data-id attribute.
        let id = deleteLink.getAttribute('data-id');
        let url = deleteLink.getAttribute('data-url')
        
        // Ask user for confirmation.
        if (confirm('Are you sure you want to delete?')) {
            // Initiates an AJAX request on confirmation.
            $.ajax({
                // Route and type of request.
                url: url,
                type: 'POST',
                // Delete route requires an Id.
                data: { id: id },
                // The controller sends back Ok() if successful.
                success: function(result) {
                    location.replace("/");
                },
                error: function(result) {
                    alert("Error while deleting. Please try again later.");
                }
            });
        }
    });
});
