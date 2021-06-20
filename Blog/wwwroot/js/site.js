// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
 
    //for form in cateogdry index
    $("form#addCategory").submit(function () {
        event.preventDefault();

        console.log("before ajax");
        $.ajax({
            url: '/Category/Create',
            type: 'POST',
            dataType: 'text',
            data: $('form#addCategory').serialize(),
            success: function (data) {

                $("#catTable").html(data);
                $('form#addCategory').trigger("reset");
                
            },
            error: function (err) {
                console.log(err);
            }
        });
    });

    // cmt form in detail of  a post
    $("form#cmtForm").submit(function () {
        event.preventDefault();
        $.ajax({
            url: '/Comment/CreateComment',
            type: 'POST',
            dataType: 'text',
            data: $('form#cmtForm').serialize(),
            success: function (data) {

                $("#cmtList").html(data);
                $('form#cmtForm').trigger("reset");

            },
            error: function (err) {
                console.log(err);
            }
        });
    });

    //text Editor 
    $('#textEditor').trumbowyg();
    $('#cmtbody').trumbowyg();

 });


