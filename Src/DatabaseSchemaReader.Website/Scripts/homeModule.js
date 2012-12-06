define(["jquery", "easing", "flowplayer", "validate", "validateUnobtrusive", "unobtrusiveAjax"], function ($) {

    $(document).ready(function () {
        animateBoxes();
        addScrollingEffectToMenuLinks();
    });

    function addScrollingEffectToMenuLinks() {
        var elements = new Array("ul.nav a", "#signInTrigger", "#registerTrigger", ".trigger");

        $(elements).each(function (index, value) {
            $(function () {
                $(value).bind('click', function (event) {
                    var $anchor = $(this);
                    $('html, body').stop().animate({
                        scrollLeft: $($anchor.attr('href')).offset().left
                    }, 1500, 'easeInOutExpo');
                    event.preventDefault();
                });
            });
        });
    }

    function animateBoxes() {
        $("#animateRedWebApi").animate({
            opacity: 0.5,
            left: "960px",
            borderWidth: "10px"
        }, 1500);
        $("#animateGrayWebApi").animate({
            opacity: 0.5,
            top: "330px",
            borderWidth: "10px"
        }, 1500);
        $("#animateYellowWebApi").animate({
            opacity: 0.5,
            left: "980px",
            borderWidth: "10px"
        }, 1500);
        $("#animateBlueWebApi").animate({
            top: "375px",
            borderWidth: "10px"
        }, 1500);

        $("#animateBlueExplorer").animate({
            left: "100px",
            borderWidth: "10px"
        }, 1500);

        $("#animateBlueQueryBuilder").animate({
            top: "375px",
            borderWidth: "10px"
        }, 1500);

        $("#animateYellowExplorer").animate({
            opacity: 0.5,
            left: "30px",
            borderWidth: "10px"
        }, 1500);

        $("#animateRedExplorer").animate({
            opacity: 0.5,
            top: "330px",
            borderWidth: "10px"
        }, 1500);

        $("#animateGrayQueryBuilder").animate({
            opacity: 0.5,
            top: "330px",
            borderWidth: "10px"
        }, 1500);

        $("#animateGrayExplorer").animate({
            opacity: 0.5,
            left: "75px",
            borderWidth: "10px"
        }, 1500);

        $("#animateYellowQueryBuilder").animate({
            opacity: 0.5,
            left: "520px",
            borderWidth: "10px"
        }, 1500);

        $("#animateRedQueryBuilder").animate({
            opacity: 0.5,
            left: "530px",
            borderWidth: "10px"
        }, 1500);
    }
});

