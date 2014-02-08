
//$(document).on("mobileinit", function () {
//});


var sliderFields = $("[data-ref='slider']");
sliderFields.each(function (index, el) {
    var span = $(el).prev("span");
    var spanId = span[0].id;
    var fieldId = spanId.substring(0, spanId.length - 5);
    var field = document.getElementById(fieldId);
    $(el).val(field.value);
});


$(function () {    
    $("[data-ref='slider']").slider({
        create: function (event, ui) {
        },
        stop: function (event, ui) {
            var sliderValue = this.value;
            var span = $(this).parent().parent().prev("span");
            var spanId = span[0].id;
            var fieldId = spanId.substring(0, spanId.length - 5);
            var field = document.getElementById(fieldId);
            span[0].innerText = sliderValue;
            field.value = sliderValue;
            Sitecore.PageModes.PageEditor.setModified(true);
        }
    });
});

var selectUpdate = function () {
    var selectValue = this.value;
    var span = $(this).parent().parent().prev("span");
    var spanId = span[0].id;
    var fieldId = spanId.substring(0, spanId.length - 5);
    var field = document.getElementById(fieldId);
    span[0].innerText = selectValue;
    field.value = selectValue;
    Sitecore.PageModes.PageEditor.setModified(true);
};


var selectFields = $("[data-ref='select']").not("[data-role='slider']");
selectFields.each(function (index, el) {
    var span = $(el).prev("span");
    var spanId = span[0].id;
    var fieldId = spanId.substring(0, spanId.length - 5);
    var field = document.getElementById(fieldId);
    $(el).val(field.value);
    $(el).on("change", selectUpdate);
    span.hide();
});

var flipUpdate = function () {
    var selectValue = this.value;
    var span = $(this).prev("span");
    var spanId = span[0].id;
    var fieldId = spanId.substring(0, spanId.length - 5);
    var field = document.getElementById(fieldId);
    span[0].innerText = selectValue;
    field.value = selectValue;
    Sitecore.PageModes.PageEditor.setModified(true);
};

var flipSwitchFields = $("[data-ref='select'][data-role='slider']");
flipSwitchFields.each(function (index, el) {
    var span = $(el).prev("span");
    var spanId = span[0].id;
    var fieldId = spanId.substring(0, spanId.length - 5);
    var field = document.getElementById(fieldId);
    $(el).val(field.value);
    $(el).on("change", flipUpdate);
    span.hide();
});

