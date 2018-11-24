// Show loading overlay the element
function showLoading($element) {
    if (!$element) {
        $element = $("body");
    } else {
        $element = $($element);
    }

    if (!$element.hasClass("box"))
    {
        $element.addClass("box");
    }

    if ($element.children(".overlay").length === 0)
    {
        $element.append('<div class="overlay"><i class="fa fa-spinner fa-spin"></i></div>');
    }
}

// hide loading overlay the element
function hideLoading($element) {
    $element = $($element);
    if ($element.hasClass("box")) {
        $element.removeClass("box");
    }

    if ($element.children(".overlay").length === 0) {
        $element.children(".overlay").remove();
    }
}

$(function () {
    var $alert = $(".alert-dismissible#statusMessage");
    if ($alert.length > 0) {
        console.log("asdasd");
        setTimeout(function () {
            $alert.remove();
        }, $alert.data("auto-dismiss"));
    }
});