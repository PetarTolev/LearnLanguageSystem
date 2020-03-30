// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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

        $(card).find(`input#Questions_${previousIndex}__One`).val('');
        $(card).find(`input#Questions_${previousIndex}__One`).attr("name", `Questions[${currentIndex}].One`);
        $(card).find(`input#Questions_${previousIndex}__One`).attr("id", `Questions_${currentIndex}__One`);
        
        $(card).find(`input#Questions_${previousIndex}__Two`).val('');
        $(card).find(`input#Questions_${previousIndex}__Two`).attr("name", `Questions[${currentIndex}].Two`);
        $(card).find(`input#Questions_${previousIndex}__Two`).attr("id", `Questions_${currentIndex}__Two`);
        
        $(card).find(`input#Questions_${previousIndex}__Three`).val('');
        $(card).find(`input#Questions_${previousIndex}__Three`).attr("name", `Questions[${currentIndex}].Three`);
        $(card).find(`input#Questions_${previousIndex}__Three`).attr("id", `Questions_${currentIndex}__Three`);
        
        $(card).find(`input#Questions_${previousIndex}__Four`).val('');
        $(card).find(`input#Questions_${previousIndex}__Four`).attr("name", `Questions[${currentIndex}].Four`);
        $(card).find(`input#Questions_${previousIndex}__Four`).attr("id", `Questions_${currentIndex}__Four`);

        $(card).insertBefore('.button-group');
        //$('form').append(card);
        $('form').append("</br>");
    });
});