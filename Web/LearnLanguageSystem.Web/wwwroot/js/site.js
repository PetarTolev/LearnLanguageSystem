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

        $(card).find(`input#Questions_${previousIndex}__QuestionOne`).val('');
        $(card).find(`input#Questions_${previousIndex}__QuestionOne`).attr("name", `Questions[${currentIndex}].QuestionOne`);
        $(card).find(`input#Questions_${previousIndex}__QuestionOne`).attr("id", `Questions_${currentIndex}__QuestionOne`);
        
        $(card).find(`input#Questions_${previousIndex}__QuestionTwo`).val('');
        $(card).find(`input#Questions_${previousIndex}__QuestionTwo`).attr("name", `Questions[${currentIndex}].QuestionTwo`);
        $(card).find(`input#Questions_${previousIndex}__QuestionTwo`).attr("id", `Questions_${currentIndex}__QuestionTwo`);
        
        $(card).find(`input#Questions_${previousIndex}__QuestionThree`).val('');
        $(card).find(`input#Questions_${previousIndex}__QuestionThree`).attr("name", `Questions[${currentIndex}].QuestionThree`);
        $(card).find(`input#Questions_${previousIndex}__QuestionThree`).attr("id", `Questions_${currentIndex}__QuestionThree`);
        
        $(card).find(`input#Questions_${previousIndex}__QuestionFour`).val('');
        $(card).find(`input#Questions_${previousIndex}__QuestionFour`).attr("name", `Questions[${currentIndex}].QuestionFour`);
        $(card).find(`input#Questions_${previousIndex}__QuestionFour`).attr("id", `Questions_${currentIndex}__QuestionFour`);

        $(card).insertBefore('.button-group');
        //$('form').append(card);
        $('form').append("</br>");
    });
});