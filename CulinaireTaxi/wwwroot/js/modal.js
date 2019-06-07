
var current_modal = null;

function initializeModal(modal) {
    //Ensure modal is hidden.
    modal.style.display = "none";

    var closeModal_button = modal.getElementsByClassName("modal-close")[0];
    closeModal_button.onclick = function () { closeModal(); };

    //Close the modal when the user clicks outside of it.
    window.onclick = function (event) {
        if (event.target == current_modal) {
            closeModal();
        }
    };
}

function openModal(modal) {
    if (current_modal != null) {
        return;
    }

    current_modal = modal;
    current_modal.style.display = "block";
}

function closeModal() {
    current_modal.style.display = "none";
    current_modal = null;
}
