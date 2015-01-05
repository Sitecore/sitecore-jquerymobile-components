
//$(document).on("mobileinit", function () {
//});


var sliderFields = jQuery("[data-ref='slider']");
if (sliderFields)
{
    sliderFields.each(function (index, el) {
        var span = jQuery(el).prev("span");
        var spanId = span[0].id;
        var fieldId = spanId.substring(0, spanId.length - 5);
        var field = document.getElementById(fieldId);
        jQuery(el).val(field.value);
    });
}


jQuery(function () {
    jQuery("[data-ref='slider']").slider({
        create: function (event, ui) {
        },
        stop: function (event, ui) {
            var sliderValue = this.value;
            var span = jQuery(this).parent().parent().prev("span");
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
    var span = jQuery(this).parent().parent().prev("span");
    var spanId = span[0].id;
    var fieldId = spanId.substring(0, spanId.length - 5);
    var field = document.getElementById(fieldId);
    span[0].innerText = selectValue;
    field.value = selectValue;
    Sitecore.PageModes.PageEditor.setModified(true);
};


var selectFields = jQuery("[data-ref='select']").not("[data-role='slider']");
if (selectFields)
{
    selectFields.each(function (index, el) {
        var span = jQuery(el).prev("span");
        var spanId = span[0].id;
        var fieldId = spanId.substring(0, spanId.length - 5);
        var field = document.getElementById(fieldId);
        jQuery(el).val(field.value);
        jQuery(el).on("change", selectUpdate);
        span.hide();
    });
}

var flipUpdate = function () {
    var selectValue = this.value;
    var span = jQuery(this).prev("span");
    var spanId = span[0].id;
    var fieldId = spanId.substring(0, spanId.length - 5);
    var field = document.getElementById(fieldId);
    span[0].innerText = selectValue;
    field.value = selectValue;
    Sitecore.PageModes.PageEditor.setModified(true);
};

var flipSwitchFields = jQuery("[data-ref='select'][data-role='slider']");
if (flipSwitchFields) {
    flipSwitchFields.each(function (index, el) {
        var span = jQuery(el).prev("span");
        var spanId = span[0].id;
        var fieldId = spanId.substring(0, spanId.length - 5);
        var field = document.getElementById(fieldId);
        jQuery(el).val(field.value);
        jQuery(el).on("change", flipUpdate);
        span.hide();
    });
}
