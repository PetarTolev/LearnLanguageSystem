function del(id, value) {
    $(`#${id}`).val(value);
}

function deleteConfirm(id, route) {
    var id = $(`#${id}`).val();
    var token = $("input[name=__RequestVerificationToken]").val();

    $.ajax({
        url: `${route}?id=${id}`,
        contentType: "application/json; charset=utf-8",
        headers: { 'X-CSRF-TOKEN': token },
        success: function() {
            $(`#Container_${id}`).remove();
            $('#deleteModal').modal('hide');

            var count = $("[id^='Container_']").length;

            if (count < 1) {
                $('#emptyNotification').show();
            }

            showAddButton();
        }
    });
}

function changeAvatar(input) {
    if (input.files && input.files[0]) {
        var fr = new FileReader();

        fr.onload = function(e) {
            $('#avatar')
                .css("background-image", `url("${e.target.result}")`);
        };

        fr.readAsDataURL(input.files[0]);
    }
}