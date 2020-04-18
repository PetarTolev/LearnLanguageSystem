$(document).ready(function(){
 
    $('#addQuestion').click(function(){
        var name = $('.question-card:last .form-control:last').attr('name');
        var previousIndex = Number(name.match(/\d+/));

        var currentIndex = previousIndex + 1;

        var card = $('.question-card:last').clone(true);

        $(card).find('h3#title')[0].innerText = `Question ${currentIndex + 1}`;

        $(card).find(`input#Questions_${previousIndex}__Content`).val('');
        $(card).find(`input#Questions_${previousIndex}__Content`).attr("name", `Questions[${currentIndex}].Content`);
        $(card).find(`input#Questions_${previousIndex}__Content`).attr("id", `Questions_${currentIndex}__Content`);

        $(card).find(`input#Questions_${previousIndex}__One_Content`).val('');
        $(card).find(`input#Questions_${previousIndex}__One_Content`).attr("name", `Questions[${currentIndex}].One.Content`);
        $(card).find(`input#Questions_${previousIndex}__One_Content`).attr("id", `Questions_${currentIndex}__One_Content`);
        $(card).find(`input#Questions_${previousIndex}__One_IsRight`).prop('checked', false);
        $(card).find(`input#Questions_${previousIndex}__One_IsRight`).attr("name", `Questions[${currentIndex}].One.IsRight`);
        $(card).find(`input#Questions_${previousIndex}__One_IsRight`).attr("id", `Questions_${currentIndex}__One_IsRight`);

        $(card).find(`input#Questions_${previousIndex}__Two_Content`).val('');
        $(card).find(`input#Questions_${previousIndex}__Two_Content`).attr("name", `Questions[${currentIndex}].Two.Content`);
        $(card).find(`input#Questions_${previousIndex}__Two_Content`).attr("id", `Questions_${currentIndex}__Two_Content`);
        $(card).find(`input#Questions_${previousIndex}__Two_IsRight`).prop('checked', false);
        $(card).find(`input#Questions_${previousIndex}__Two_IsRight`).attr("name", `Questions[${currentIndex}].Two.IsRight`);
        $(card).find(`input#Questions_${previousIndex}__Two_IsRight`).attr("id", `Questions_${currentIndex}__Two_IsRight`);

        $(card).find(`input#Questions_${previousIndex}__Three_Content`).val('');
        $(card).find(`input#Questions_${previousIndex}__Three_Content`).attr("name", `Questions[${currentIndex}].Three.Content`);
        $(card).find(`input#Questions_${previousIndex}__Three_Content`).attr("id", `Questions_${currentIndex}__Three_Content`);
        $(card).find(`input#Questions_${previousIndex}__Three_IsRight`).prop('checked', false);
        $(card).find(`input#Questions_${previousIndex}__Three_IsRight`).attr("name", `Questions[${currentIndex}].Three.IsRight`);
        $(card).find(`input#Questions_${previousIndex}__Three_IsRight`).attr("id", `Questions_${currentIndex}__Three_IsRight`);
        
        $(card).find(`input#Questions_${previousIndex}__Four_Content`).val('');
        $(card).find(`input#Questions_${previousIndex}__Four_Content`).attr("name", `Questions[${currentIndex}].Four.Content`);
        $(card).find(`input#Questions_${previousIndex}__Four_Content`).attr("id", `Questions_${currentIndex}__Four_Content`);
        $(card).find(`input#Questions_${previousIndex}__Four_IsRight`).prop('checked', false);
        $(card).find(`input#Questions_${previousIndex}__Four_IsRight`).attr("name", `Questions[${currentIndex}].Four.IsRight`);
        $(card).find(`input#Questions_${previousIndex}__Four_IsRight`).attr("id", `Questions_${currentIndex}__Four_IsRight`);

        $(card).insertBefore('.button-group');
        $('form').append("</br>");
    });
});

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
        success: function () {
            $(`#Container_${id}`).remove();
            $('#deleteModal').modal('hide');

            var count = $("id^='Container_'").length;

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