$(document).ready(function () {
    $('.jq-delete').on('click', function () {
        var btn = $(this);
        var gameId = $(this).data('id');
        const confirmationButton = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger",
                cancelButton: "btn btn-info me-2"
            },
            buttonsStyling: false
        });
        confirmationButton.fire({
            title: "Are you sure you need to delete the game ?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                confirmationButton.fire({
                    title: "Deleted!",
                    text: "Your file has been deleted.",
                    icon: "success"
                });
                $.ajax({
                    method: "delete",
                    url: `/games/delete/${gameid}`,
                    success: function () {
                        alert(`the game with id ${gameid} deleted successfully`)
                    }
                });
                btn.parents('tr').fadeOut();
            } else  {
                swalWithBootstrapButtons.fire({
                    title: "Cancelled",
                    text: "Your imaginary file is safe :)",
                    icon: "error"
                });
            }
        });
        
    })
})