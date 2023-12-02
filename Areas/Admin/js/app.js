/*
 *  Document   : app.js
 *  Author     : pixelcave
 *  Description: Custom scripts and plugin initializations (available to all pages)
 *
 *  Feel free to remove the plugin initilizations from uiInit() if you would like to
 *  use them only in specific pages. Also, if you remove a js plugin you won't use, make
 *  sure to remove its initialization from uiInit().
 */

var hook = false;
$("input, textarea").on("propertychange change keyup paste input", function () {
    hook = true;
});
function unhook() {
    hook = false;
}
// Đếm ký tự trong textbox
$(".count-this").each(function () {
    $(this).find("label span").text("(" + $(this).find("textarea, input").val().length + ")");
});
$(".count-this textarea, .count-this input").keyup(function () {
    $(this).parent().prev().find("span").text("(" + $(this).val().length + ")");
});
// Cảnh báo số ký tự thẻ meta
$("[id$='txtMetaTitle']").keyup(function () {
    if ($(this).val().length > 49 && $(this).val().length < 66) $(this).parent().parent().addClass("has-success").removeClass("has-warning");
    else $(this).parent().parent().removeClass("has-success").addClass("has-warning");
});
$("[id$='txtMetaTitle']").each(function () {
    if ($(this).val().length > 49 && $(this).val().length < 66) $(this).parent().parent().addClass("has-success").removeClass("has-warning");
    else $(this).parent().parent().removeClass("has-success").addClass("has-warning");
});
$("[id$='txtMetaDescription']").keyup(function () {
    if ($(this).val().length > 129 && $(this).val().length < 161) $(this).parent().parent().addClass("has-success").removeClass("has-warning");
    else $(this).parent().parent().removeClass("has-success").addClass("has-warning");
});
$("[id$='txtMetaDescription']").each(function () {
    if ($(this).val().length > 129 && $(this).val().length < 161) $(this).parent().parent().addClass("has-success").removeClass("has-warning");
    else $(this).parent().parent().removeClass("has-success").addClass("has-warning");
});
function SetLangAdmin(langId, control, action) {
    setCookie("fc4649d44a90703e1a071ea7bad0089f", langId, "", "/", "", "");
    if (action.includes("Update")) window.location = "/Admin?control=" + control + "";
    else window.location = "/Admin?control=" + control + "&action=" + action + "";
}
var App = function () {
    /* active menu */
    //$(".sidebar-nav li a.sidebar-nav-menu").each(function () {
    //    var attr = $(this).attr("href");
    //    var link = location.href;
    //    if (getParameterByName("control", attr) === getParameterByName("control", link)) $(this).parent().attr("class", "active");
    //    $(this).next().find("a").each(function() {
    //        attr = $(this).attr("href");
    //        if (getParameterByName("action", attr) === getParameterByName("action", link)) $(this).attr("class", "active");
    //    });
    //});
    $("#cm_menu_main li a.sidebar-nav-menu").each(function () {
        var attr = $(this).attr("href");
        var link = location.href;
        if (getParameterByName("control", attr) === getParameterByName("control", link)) {
            $(this).attr("class", "sidebar-nav-menu open");
            $(this).next().css("display", "block");
            $(this).next().find("a").each(function () {
                attr = $(this).attr("href");
                if (getParameterByName("action", attr) === getParameterByName("action", link) && getParameterByName("app", attr) === getParameterByName("app", link)) $(this).attr("class", "active");
            });
        }
    });
    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"), results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return "";
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }
    /* End active menu */


    /* Helper variables - set in uiInit() */
    var page, pageContent, header, footer, sidebar, sScroll, sidebarAlt, sScrollAlt;

    /* Initialization UI Code */
    var uiInit = function() {
        // Set variables - Cache some often used Jquery objects in variables */
        page            = $("#page-container");
        pageContent     = $("#page-content");
        header          = $("header");
        footer          = $("footer");

        sidebar         = $("#sidebar");
        sScroll         = $("#sidebar-scroll");

        sidebarAlt      = $("#sidebar-alt");
        sScrollAlt      = $("#sidebar-alt-scroll");

        // Initialize sidebars functionality
        handleSidebar("init");

        // Sidebar navigation functionality
        handleNav();

        // Interactive blocks functionality
        interactiveBlocks();

        // Scroll to top functionality
        scrollToTop();

        // Template Options, change features
        templateOptions();

        // Resize #page-content to fill empty space if exists (also add it to resize and orientationchange events)
        resizePageContent();
        $(window).resize(function(){ resizePageContent(); });
        $(window).bind("orientationchange", resizePageContent);

        // Add the correct copyright year at the footer
        //var yearCopy = $('#year-copy'), d = new Date();
        //if (d.getFullYear() === 2014) { yearCopy.html('2014'); } else { yearCopy.html('2014-' + d.getFullYear().toString().substr(2,2)); }

        // Initialize chat demo functionality (in sidebar)
        //chatUi();

        // Initialize tabs
        $('[data-toggle="tabs"] a, .enable-tabs a').click(function(e){ e.preventDefault(); $(this).tab("show"); });

        // Initialize Tooltips
        $('[data-toggle="tooltip"], .enable-tooltip').tooltip({container: "body", animation: false});

        // Initialize Popovers
        $('[data-toggle="popover"], .enable-popover').popover({container: "body", animation: true});

        // Initialize single image lightbox
        $('[data-toggle="lightbox-image"]').magnificPopup({type: "image", image: {titleSrc: "title"}});

        // Initialize image gallery lightbox
        $('[data-toggle="lightbox-gallery"]').each(function(){
            $(this).magnificPopup({
                delegate: "a.gallery-link",
                type: "image",
                gallery: {
                    enabled: true,
                    navigateByImgClick: true,
                    arrowMarkup: '<button type="button" class="mfp-arrow mfp-arrow-%dir%" title="%title%"></button>',
                    tPrev: "Previous",
                    tNext: "Next",
                    tCounter: '<span class="mfp-counter">%curr% of %total%</span>'
                },
                image: {titleSrc: "title"}
            });
        });

        // Initialize Typeahead - Example with countries
        var exampleTypeheadData = ["Afghanistan","Albania","Algeria","American Samoa","Andorra","Angola","Anguilla","Antarctica","Antigua and Barbuda","Argentina","Armenia","Aruba","Australia","Austria","Azerbaijan","Bahrain","Bangladesh","Barbados","Belarus","Belgium","Belize","Benin","Bermuda","Bhutan","Bolivia","Bosnia and Herzegovina","Botswana","Bouvet Island","Brazil","British Indian Ocean Territory","British Virgin Islands","Brunei","Bulgaria","Burkina Faso","Burundi","CΓ΄te d'Ivoire","Cambodia","Cameroon","Canada","Cape Verde","Cayman Islands","Central African Republic","Chad","Chile","China","Christmas Island","Cocos (Keeling) Islands","Colombia","Comoros","Congo","Cook Islands","Costa Rica","Croatia","Cuba","Cyprus","Czech Republic","Democratic Republic of the Congo","Denmark","Djibouti","Dominica","Dominican Republic","East Timor","Ecuador","Egypt","El Salvador","Equatorial Guinea","Eritrea","Estonia","Ethiopia","Faeroe Islands","Falkland Islands","Fiji","Finland","Former Yugoslav Republic of Macedonia","France","French Guiana","French Polynesia","French Southern Territories","Gabon","Georgia","Germany","Ghana","Gibraltar","Greece","Greenland","Grenada","Guadeloupe","Guam","Guatemala","Guinea","Guinea-Bissau","Guyana","Haiti","Heard Island and McDonald Islands","Honduras","Hong Kong","Hungary","Iceland","India","Indonesia","Iran","Iraq","Ireland","Israel","Italy","Jamaica","Japan","Jordan","Kazakhstan","Kenya","Kiribati","Kuwait","Kyrgyzstan","Laos","Latvia","Lebanon","Lesotho","Liberia","Libya","Liechtenstein","Lithuania","Luxembourg","Macau","Madagascar","Malawi","Malaysia","Maldives","Mali","Malta","Marshall Islands","Martinique","Mauritania","Mauritius","Mayotte","Mexico","Micronesia","Moldova","Monaco","Mongolia","Montserrat","Morocco","Mozambique","Myanmar","Namibia","Nauru","Nepal","Netherlands","Netherlands Antilles","New Caledonia","New Zealand","Nicaragua","Niger","Nigeria","Niue","Norfolk Island","North Korea","Northern Marianas","Norway","Oman","Pakistan","Palau","Panama","Papua New Guinea","Paraguay","Peru","Philippines","Pitcairn Islands","Poland","Portugal","Puerto Rico","Qatar","RΓ©union","Romania","Russia","Rwanda","SΓ£o TomΓ© and PrΓ­ncipe","Saint Helena","Saint Kitts and Nevis","Saint Lucia","Saint Pierre and Miquelon","Saint Vincent and the Grenadines","Samoa","San Marino","Saudi Arabia","Senegal","Seychelles","Sierra Leone","Singapore","Slovakia","Slovenia","Solomon Islands","Somalia","South Africa","South Georgia and the South Sandwich Islands","South Korea","Spain","Sri Lanka","Sudan","Suriname","Svalbard and Jan Mayen","Swaziland","Sweden","Switzerland","Syria","Taiwan","Tajikistan","Tanzania","Thailand","The Bahamas","The Gambia","Togo","Tokelau","Tonga","Trinidad and Tobago","Tunisia","Turkey","Turkmenistan","Turks and Caicos Islands","Tuvalu","US Virgin Islands","Uganda","Ukraine","United Arab Emirates","United Kingdom","United States","United States Minor Outlying Islands","Uruguay","Uzbekistan","Vanuatu","Vatican City","Venezuela","Vietnam","Wallis and Futuna","Western Sahara","Yemen","Yugoslavia","Zambia","Zimbabwe"];
        $(".input-typeahead").typeahead({ source: exampleTypeheadData });

        // Initialize Chosen
        $(".select-chosen").chosen({width: "100%"});

        // Initialize Select2
        $(".select-select2").select2();

        // Initialize Bootstrap Colorpicker
        $(".input-colorpicker").colorpicker({format: "hex"});
        $(".input-colorpicker-rgba").colorpicker({format: "rgba"});

        // Initialize Slider for Bootstrap
        $(".input-slider").slider();

        // Initialize Tags Input
        $(".input-tags").tagsInput({ width: "auto", height: "auto"});

        // Initialize Datepicker
        $(".input-datepicker, .input-daterange").datepicker({weekStart: 1});
        $(".input-datepicker-close").datepicker({weekStart: 1}).on("changeDate", function(e){ $(this).datepicker("hide"); });

        // Initialize Timepicker
        $(".input-timepicker").timepicker({minuteStep: 1,showSeconds: true,showMeridian: true});
        $(".input-timepicker24").timepicker({minuteStep: 1,showSeconds: true,showMeridian: false});

        // Easy Pie Chart
        $(".pie-chart").easyPieChart({
            barColor: $(this).data("bar-color") ? $(this).data("bar-color") : "#777777",
            trackColor: $(this).data("track-color") ? $(this).data("track-color") : "#eeeeee",
            lineWidth: $(this).data("line-width") ? $(this).data("line-width") : 3,
            size: $(this).data("size") ? $(this).data("size") : "80",
            animate: 800,
            scaleColor: false
        });

        // Initialize Placeholder
        $("input, textarea").placeholder();
    };

    /* Page Loading functionality */
    var pageLoading = function(){
        var pageWrapper = $("#page-wrapper");

        if (pageWrapper.hasClass("page-loading")) {
            pageWrapper.removeClass("page-loading");
        }
    };

    /* Gets window width cross browser */
    var getWindowWidth = function(){
        return window.innerWidth
                || document.documentElement.clientWidth
                || document.body.clientWidth;
    };

    /* Sidebar Navigation functionality */
    var handlePageScroll;
    var handleNav = function() {
        // Animation Speed, change the values for different results
        var upSpeed     = 0;
        var downSpeed   = 0;

        // Get all vital links
        var menuLinks       = $(".sidebar-nav-menu");
        var submenuLinks    = $(".sidebar-nav-submenu");

        // Primary Accordion functionality
        //menuLinks.click(function(){
        //    var link = $(this);

        //    if (page.hasClass("sidebar-mini") && page.hasClass("sidebar-visible-lg") && (getWindowWidth() > 991)) {
        //        if (link.hasClass("open")) {
        //            link.removeClass("open").next().removeAttr("style");
        //        }
        //        else {
        //            $(".sidebar-nav-menu.open").removeClass("open").next().removeAttr("style");
        //            link.addClass("open").next().css("display", "block");
        //        }
        //    }
        //    else if (!link.parent().hasClass("active")) {
        //        if (link.hasClass("open")) {
        //            // Chung tạm ẩn scroll khi click vào menu
        //            //link.removeClass('open').next().slideUp(upSpeed, function(){
        //            //    handlePageScroll(link, 200, 300);
        //            //});
        //            link.removeClass("open").next().slideUp(upSpeed);

        //            // Resize #page-content to fill empty space if exists
        //            setTimeout(resizePageContent, upSpeed);
        //        }
        //        else {
        //            $(".sidebar-nav-menu.open").removeClass("open").next().slideUp(upSpeed);

        //            // Chung tạm ẩn scroll khi click vào menu

        //            //link.addClass('open').next().slideDown(downSpeed, function(){
        //            //    handlePageScroll(link, 150, 600);
        //            //});
        //            link.addClass("open").next().slideDown(downSpeed);

        //            // Resize #page-content to fill empty space if exists
        //            setTimeout(resizePageContent, ((upSpeed > downSpeed) ? upSpeed : downSpeed));
        //        }
        //    }

        //    link.blur();

        //    return false;
        //});

        // Submenu Accordion functionality
        submenuLinks.click(function(){
            var link = $(this);

            if (link.parent().hasClass("active") !== true) {
                if (link.hasClass("open")) {

                    // Chung tạm ẩn scroll khi click vào menu

                    //link.removeClass('open').next().slideUp(upSpeed, function(){
                    //    handlePageScroll(link, 200, 300);
                    //});
                    link.removeClass("open").next().slideUp(upSpeed);

                    // Resize #page-content to fill empty space if exists
                    setTimeout(resizePageContent, upSpeed);
                }
                else {
                    link.closest("ul").find(".sidebar-nav-submenu.open").removeClass("open").next().slideUp(upSpeed);

                    // Chung tạm ẩn scroll khi click vào menu
                    //link.addClass('open').next().slideDown(downSpeed, function(){
                    //    handlePageScroll(link, 150, 600);
                    //});
                    link.addClass("open").next().slideDown(downSpeed);

                    // Resize #page-content to fill empty space if exists
                    setTimeout(resizePageContent, ((upSpeed > downSpeed) ? upSpeed : downSpeed));
                }
            }

            link.blur();

            return false;
        });
        $(".sidebar-mini.sidebar-visible-lg-mini.sidebar-visible-xs #sidebar .sidebar-nav .sidebar-nav-menu + ul").css("display","none!important");
    };

    /* Scrolls the page (static layout) or the sidebar scroll element (fixed header/sidebars layout) to a specific position - Used when a submenu opens */
    handlePageScroll = function(sElem, sHeightDiff, sSpeed) {
        if (!page.hasClass("disable-menu-autoscroll")) {
            var elemScrollToHeight;

            // If we have a static layout scroll the page
            if (!header.hasClass("navbar-fixed-top") && !header.hasClass("navbar-fixed-bottom")) {
                var elemOffsetTop   = sElem.offset().top;

                elemScrollToHeight  = (((elemOffsetTop - sHeightDiff) > 0) ? (elemOffsetTop - sHeightDiff) : 0);

                $("html, body").animate({scrollTop: elemScrollToHeight}, sSpeed);
            } else { // If we have a fixed header/sidebars layout scroll the sidebar scroll element
                var sContainer      = sElem.parents("#sidebar-scroll");
                var elemOffsetCon   = sElem.offset().top + Math.abs($("div:first", sContainer).offset().top);

                elemScrollToHeight = (((elemOffsetCon - sHeightDiff) > 0) ? (elemOffsetCon - sHeightDiff) : 0);
                sContainer.animate({ scrollTop: elemScrollToHeight}, sSpeed);
            }
        }
    };

    /* Sidebar Functionality */
    var handleSidebar = function(mode, extra) {
        if (mode === "init") {
            // Init sidebars scrolling functionality
            handleSidebar("sidebar-scroll");
            handleSidebar("sidebar-alt-scroll");

            // Close the other sidebar if we hover over a partial one
            // In smaller screens (the same applies to resized browsers) two visible sidebars
            // could mess up our main content (not enough space), so we hide the other one :-)
            $(".sidebar-partial #sidebar")
                .mouseenter(function(){ handleSidebar("close-sidebar-alt"); });
            $(".sidebar-alt-partial #sidebar-alt")
                .mouseenter(function(){ handleSidebar("close-sidebar"); });
        } else {
            var windowW = getWindowWidth();

            if (mode === "toggle-sidebar") {
                if ( windowW > 991) { // Toggle main sidebar in large screens (> 991px)
                    page.toggleClass("sidebar-visible-lg");
                    if ($(".sidebar-visible-lg").length) setCookie("sidebar", "sidebar-visible-lg", "", "/", "", "");
                    if (page.hasClass("sidebar-mini")) {
                        page.toggleClass("sidebar-visible-lg-mini");
                        if ($(".sidebar-visible-lg-mini").length) setCookie("sidebar", "sidebar-visible-lg-mini", "", "/", "", "");
                    }

                    if (page.hasClass("sidebar-visible-lg")) {
                        handleSidebar("close-sidebar-alt");
                    }

                    // If 'toggle-other' is set, open the alternative sidebar when we close this one
                    if (extra === "toggle-other") {
                        if (!page.hasClass("sidebar-visible-lg")) {
                            handleSidebar("open-sidebar-alt");
                        }
                    }
                } else { // Toggle main sidebar in small screens (< 992px)
                    page.toggleClass("sidebar-visible-xs");

                    if (page.hasClass("sidebar-visible-xs")) {
                        handleSidebar("close-sidebar-alt");
                    }
                }

                // Handle main sidebar scrolling functionality
                handleSidebar("sidebar-scroll");
            }
            else if (mode === "toggle-sidebar-alt") {
                if ( windowW > 991) { // Toggle alternative sidebar in large screens (> 991px)
                    page.toggleClass("sidebar-alt-visible-lg");

                    if (page.hasClass("sidebar-alt-visible-lg")) {
                        handleSidebar("close-sidebar");
                    }

                    // If 'toggle-other' is set open the main sidebar when we close the alternative
                    if (extra === "toggle-other") {
                        if (!page.hasClass("sidebar-alt-visible-lg")) {
                            handleSidebar("open-sidebar");
                        }
                    }
                } else { // Toggle alternative sidebar in small screens (< 992px)
                    page.toggleClass("sidebar-alt-visible-xs");

                    if (page.hasClass("sidebar-alt-visible-xs")) {
                        handleSidebar("close-sidebar");
                    }
                }
            }
            else if (mode === "open-sidebar") {
                if ( windowW > 991) { // Open main sidebar in large screens (> 991px)
                    if (page.hasClass("sidebar-mini")) { page.removeClass("sidebar-visible-lg-mini"); }
                    page.addClass("sidebar-visible-lg");
                } else { // Open main sidebar in small screens (< 992px)
                    page.addClass("sidebar-visible-xs");
                }

                // Close the other sidebar
                handleSidebar("close-sidebar-alt");
            }
            else if (mode === "open-sidebar-alt") {
                if ( windowW > 991) { // Open alternative sidebar in large screens (> 991px)
                    page.addClass("sidebar-alt-visible-lg");
                } else { // Open alternative sidebar in small screens (< 992px)
                    page.addClass("sidebar-alt-visible-xs");
                }

                // Close the other sidebar
                handleSidebar("close-sidebar");
            }
            else if (mode === "close-sidebar") {
                if ( windowW > 991) { // Close main sidebar in large screens (> 991px)
                    page.removeClass("sidebar-visible-lg");
                    if (page.hasClass("sidebar-mini")) { page.addClass("sidebar-visible-lg-mini"); }
                } else { // Close main sidebar in small screens (< 992px)
                    page.removeClass("sidebar-visible-xs");
                }
            }
            else if (mode === "close-sidebar-alt") {
                if ( windowW > 991) { // Close alternative sidebar in large screens (> 991px)
                    page.removeClass("sidebar-alt-visible-lg");
                } else { // Close alternative sidebar in small screens (< 992px)
                    page.removeClass("sidebar-alt-visible-xs");
                }
            }
            else if (mode === "sidebar-scroll") { // Handle main sidebar scrolling
                if (page.hasClass("sidebar-mini") && page.hasClass("sidebar-visible-lg-mini") && (windowW > 991)) { // Destroy main sidebar scrolling when in mini sidebar mode
                    if (sScroll.length && sScroll.parent(".slimScrollDiv").length) {
                        sScroll
                            .slimScroll({destroy: true});
                        sScroll
                            .attr("style", "");
                    }
                }
                else if ((page.hasClass("header-fixed-top") || page.hasClass("header-fixed-bottom"))) {
                    var sHeight = $(window).height();

                    if (sScroll.length && (!sScroll.parent(".slimScrollDiv").length)) { // If scrolling does not exist init it..
                        sScroll
                            .slimScroll({
                                height: sHeight,
                                color: "#fff",
                                size: "3px",
                                touchScrollStep: 100
                            });

                        // Handle main sidebar's scrolling functionality on resize or orientation change
                        var sScrollTimeout;

                        $(window).on("resize orientationchange", function(){
                            clearTimeout(sScrollTimeout);

                            sScrollTimeout = setTimeout(function(){
                                handleSidebar("sidebar-scroll");
                            }, 150);
                        });
                    }
                    else { // ..else resize scrolling height
                        sScroll
                            .add(sScroll.parent())
                            .css("height", sHeight);
                    }
                }
            }
            else if (mode === "sidebar-alt-scroll") { // Init alternative sidebar scrolling
                if ((page.hasClass("header-fixed-top") || page.hasClass("header-fixed-bottom"))) {
                    var sHeightAlt = $(window).height();

                    if (sScrollAlt.length && (!sScrollAlt.parent(".slimScrollDiv").length)) { // If scrolling does not exist init it..
                        sScrollAlt
                            .slimScroll({
                                height: sHeightAlt,
                                color: "#fff",
                                size: "3px",
                                touchScrollStep: 100
                            });

                        // Resize alternative sidebar scrolling height on window resize or orientation change
                        var sScrollAltTimeout;

                        $(window).on("resize orientationchange", function(){
                            clearTimeout(sScrollAltTimeout);

                            sScrollAltTimeout = setTimeout(function(){
                                handleSidebar("sidebar-alt-scroll");
                            }, 150);
                        });
                    }
                    else { // ..else resize scrolling height
                        sScrollAlt
                            .add(sScrollAlt.parent())
                            .css("height", sHeightAlt);
                    }
                }
            }
        }

        return false;
    };

    /* Resize #page-content to fill empty space if exists */
    var resizePageContent = function() {
        var windowH         = $(window).height();
        var sidebarH        = sidebar.outerHeight();
        var sidebarAltH     = sidebarAlt.outerHeight();
        var headerH         = header.outerHeight();
        var footerH         = footer.outerHeight();

        // If we have a fixed sidebar/header layout or each sidebars’ height < window height
        if (header.hasClass("navbar-fixed-top") || header.hasClass("navbar-fixed-bottom") || ((sidebarH < windowH) && (sidebarAltH < windowH))) {
            if (page.hasClass("footer-fixed")) { // if footer is fixed don't remove its height
                pageContent.css("min-height", windowH - headerH + "px");
            } else { // else if footer is static, remove its height
                pageContent.css("min-height", windowH - (headerH + footerH) + "px");
            }
        }  else { // In any other case set #page-content height the same as biggest sidebar's height
            if (page.hasClass("footer-fixed")) { // if footer is fixed don't remove its height
                pageContent.css("min-height", ((sidebarH > sidebarAltH) ? sidebarH : sidebarAltH) - headerH + "px");
            } else { // else if footer is static, remove its height
                pageContent.css("min-height", ((sidebarH > sidebarAltH) ? sidebarH : sidebarAltH) - (headerH + footerH) + "px");
            }
        }
    };

    /* Interactive blocks functionality */
    var interactiveBlocks = function() {

        // Toggle block's content
        $('[data-toggle="block-toggle-content"]').on("click", function(){
            var blockContent = $(this).closest(".block").find(".block-content");

            if ($(this).hasClass("active")) {
                blockContent.slideDown();
            } else {
                blockContent.slideUp();
            }

            $(this).toggleClass("active");
        });

        // Toggle block fullscreen
        $('[data-toggle="block-toggle-fullscreen"]').on("click", function(){
            var block = $(this).closest(".block");

            if ($(this).hasClass("active")) {
                block.removeClass("block-fullscreen");
            } else {
                block.addClass("block-fullscreen");
            }

            $(this).toggleClass("active");
        });

        // Hide block
        $('[data-toggle="block-hide"]').on("click", function(){
            $(this).closest(".block").fadeOut();
        });
    };

    /* Scroll to top functionality */
    var scrollToTop = function() {
        // Get link
        var link = $("#to-top");

        $(window).scroll(function() {
            // If the user scrolled a bit (150 pixels) show the link in large resolutions
            if (($(this).scrollTop() > 150) && (getWindowWidth() > 991)) {
                link.fadeIn(100);
            } else {
                link.fadeOut(100);
            }
        });

        // On click get to top
        link.click(function() {
            $("html, body").animate({scrollTop: 0}, 400);
            return false;
        });
    };

    /* Demo chat functionality (in sidebar) */
    var chatUi = function() {
        var chatUsers       = $(".chat-users");
        var chatTalk        = $(".chat-talk");
        var chatMessages    = $(".chat-talk-messages");
        var chatInput       = $("#sidebar-chat-message");
        var chatMsg         = "";

        // Initialize scrolling on chat talk list
        chatMessages.slimScroll({ height: 210, color: "#fff", size: "3px", position: "left", touchScrollStep: 100 });

        // If a chat user is clicked show the chat talk
        $("a", chatUsers).click(function(){
            chatUsers.slideUp();
            chatTalk.slideDown();
            chatInput.focus();

            return false;
        });

        // If chat talk close button is clicked show the chat user list
        $("#chat-talk-close-btn").click(function(){
            chatTalk.slideUp();
            chatUsers.slideDown();

            return false;
        });

        // When the chat message form is submitted
        $("#sidebar-chat-form").submit(function(e){
            // Get text from message input
            chatMsg = chatInput.val();

            // If the user typed a message
            if (chatMsg) {
                // Add it to the message list
                chatMessages.append('<li class="chat-talk-msg chat-talk-msg-highlight themed-border animation-slideLeft">' + $("<div />").text(chatMsg).html() + "</li>");

                // Scroll the message list to the bottom
                chatMessages.slimScroll({ scrollTo: chatMessages[0].scrollHeight + "px" });

                // Reset the message input
                chatInput.val("");
            }

            // Don't submit the message form
            e.preventDefault();
        });
    };

    /* Template Options, change features functionality */
    var templateOptions = function() {
        /*
         * Color Themes
         */
        var colorList   = $(".sidebar-themes");
        var themeLink   = $("#theme-link");

        var themeColor  = themeLink.length ? themeLink.attr("href") : "default";
        var cookies     = page.hasClass("enable-cookies") ? true : false;

        var themeColorCke;

        // If cookies have been enabled
        if (cookies) {
            themeColorCke = Cookies.get("optionThemeColor") ? Cookies.get("optionThemeColor") : false;

            // Update color theme
            if (themeColorCke) {
                if (themeColorCke === "default") {
                    if (themeLink.length) {
                        themeLink.remove();
                        themeLink = $("#theme-link");
                        deleteCookie("ThemeColor", "/", "/");
                    }
                } else {
                    if (themeLink.length) {
                        setCookie("ThemeColor", themeColor, "", "/", "");
                        themeLink.attr("href", themeColorCke);
                    }
                    else {
                        setCookie("ThemeColor", themeColor, "", "/", "");
                        $('link[href="/Areas/Admin/css/themes.css"]').before('<link id="theme-link" rel="stylesheet" href="' + themeColorCke + '">');
                        themeLink = $("#theme-link");
                    }
                }
            }

            themeColor = themeColorCke ? themeColorCke : themeColor;
        }

        // Set the active color theme link as active
        $('a[data-theme="' + themeColor + '"]', colorList)
            .parent("li")
            .addClass("active");

        // When a color theme link is clicked
        $("a", colorList).click(function(e){
            // Get theme name
            themeColor = $(this).data("theme");

            $("li", colorList).removeClass("active");
            $(this).parent("li").addClass("active");

            if (themeColor === "default") {
                if (themeLink.length) {
                    themeLink.remove();
                    themeLink = $("#theme-link");
                    deleteCookie("ThemeColor", "/", "");
                }
            } else {
                if (themeLink.length) {
                    setCookie("ThemeColor", themeColor, "", "/", "");
                    themeLink.attr("href", themeColor);
                } else {
                    setCookie("ThemeColor", themeColor, "", "/", "");
                    $('link[href="/Areas/Admin/css/themes.css"]').before('<link id="theme-link" rel="stylesheet" href="' + themeColor + '">');
                    themeLink = $("#theme-link");
                }
            }

            // If cookies have been enabled, save the new options
            if (cookies) {
                Cookies.set("optionThemeColor", themeColor, {expires: 7});
            }
        });

        // Prevent template options dropdown from closing on clicking options
        $(".dropdown-options a").click(function(e){ e.stopPropagation(); });

        /* Page Style */
        var optMainStyle        = $("#options-main-style");
        var optMainStyleAlt     = $("#options-main-style-alt");

        if (page.hasClass("style-alt")) {
            optMainStyleAlt.addClass("active");
        } else {
            optMainStyle.addClass("active");
        }

        optMainStyle.click(function() {
            page.removeClass("style-alt");
            $(this).addClass("active");
            optMainStyleAlt.removeClass("active");
        });

        optMainStyleAlt.click(function() {
            page.addClass("style-alt");
            $(this).addClass("active");
            optMainStyle.removeClass("active");
        });

        /* Header options */
        var optHeaderDefault    = $("#options-header-default");
        var optHeaderInverse    = $("#options-header-inverse");

        if (header.hasClass("navbar-default")) {
            optHeaderDefault.addClass("active");
        } else {
            optHeaderInverse.addClass("active");
        }

        optHeaderDefault.click(function() {
            header.removeClass("navbar-inverse").addClass("navbar-default");
            $(this).addClass("active");
            optHeaderInverse.removeClass("active");
        });

        optHeaderInverse.click(function() {
            header.removeClass("navbar-default").addClass("navbar-inverse");
            $(this).addClass("active");
            optHeaderDefault.removeClass("active");
        });
    };

    /* Datatables basic Bootstrap integration (pagination integration included under the Datatables plugin in plugins.js) */
    var dtIntegration = function() {
        $.extend(true, $.fn.dataTable.defaults, {
            "sDom": "<'row'<'col-sm-6 col-xs-5'l><'col-sm-6 col-xs-7'f>r>t<'row'<'col-sm-5 hidden-xs'i><'col-sm-7 col-xs-12 clearfix'p>>",
            "sPaginationType": "bootstrap",
            "oLanguage": {
                "sLengthMenu": "_MENU_",
                "sSearch": "<div class=\"input-group\">_INPUT_<span class=\"input-group-addon\"><i class=\"fa fa-search\"></i></span></div>",
                "sInfo": "<strong>_START_</strong>-<strong>_END_</strong> of <strong>_TOTAL_</strong>",
                "oPaginate": {
                    "sPrevious": "",
                    "sNext": ""
                }
            }
        });
        $.extend($.fn.dataTableExt.oStdClasses, {
            "sWrapper": "dataTables_wrapper form-inline",
            "sFilterInput": "form-control",
            "sLengthSelect": "form-control"
        });
    };

    /* Print functionality - Hides all sidebars, prints the page and then restores them (To fix an issue with CSS print styles in webkit browsers)  */
    var handlePrint = function() {
        // Store all #page-container classes
        var pageCls = page.prop("class");

        // Remove all classes from #page-container
        page.prop("class", "");

        // Print the page
        window.print();

        // Restore all #page-container classes
        page.prop("class", pageCls);
    };

    return {
        init: function() {
            uiInit(); // Initialize UI Code
            pageLoading(); // Initialize Page Loading
        },
        sidebar: function(mode, extra) {
            handleSidebar(mode, extra); // Handle sidebars - access functionality from everywhere
        },
        datatables: function() {
            dtIntegration(); // Datatables Bootstrap integration
        },
        pagePrint: function() {
            handlePrint(); // Print functionality
        }
    };
}();

/* Initialize app when page loads */

$(function () { App.init(); });

/* https://github.com/ifightcrime/bootstrap-growl */
(function (e) { e.bootstrapGrowl = function (t, n) { var n = e.extend({}, e.bootstrapGrowl.default_options, n), r = e("<div>"); r.attr("class", "bootstrap-growl alert"), n.type && r.addClass("alert-" + n.type), n.allow_dismiss && r.append('<a class="close" data-dismiss="alert" href="#">&times;</a>'), r.append(t), n.top_offset && (n.offset = { from: "top", amount: n.top_offset }); var i = e(".bootstrap-growl", n.ele); offsetAmount = n.offset.amount, e.each(i, function () { offsetAmount = offsetAmount + e(this).outerHeight() + n.stackup_spacing }), css = { position: "fixed", margin: 0, "z-index": "9999", display: "none" }, css[n.offset.from] = offsetAmount + "px", r.css(css), n.width !== "auto" && r.css("width", n.width + "px"), e(n.ele).append(r); switch (n.align) { case "center": r.css({ left: "50%", "margin-left": "-" + r.outerWidth() / 2 + "px" }); break; case "left": r.css("left", "20px"); break; default: r.css("right", "20px") } r.fadeIn(), n.delay >= 0 && r.delay(n.delay).fadeOut("slow", function () { e(this).remove() }) }, e.bootstrapGrowl.default_options = { ele: "body", type: null, offset: { from: "top", amount: 20 }, align: "right", width: 250, delay: 4e3, allow_dismiss: !0, stackup_spacing: 10 } })(jQuery);

function NewWindow_(url, name, w, h, scrollbar, resizable) {
    var leftPosition = screen.width / 2 - w / 2;
    var topPosition = screen.height / 2 - h / 2 - 100 + 50;
    //    if (TopPosition < 1) top = 1;    
    var settings = "width=" + w + ",height=" + h + ",scrollbars=" + scrollbar + ",resizable=" + resizable + ",screenX=0,screenY=200'";
    var mywindow = window.open(url, name, settings);
    mywindow.moveTo(leftPosition, topPosition);
}

function checkAllCheckBox(nameregex, parrentControl) {
    var checked = parrentControl.checked;
    var re;
    var elm;
    if (checked) {
        re = new RegExp(nameregex);
        for (var i = 1; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i];
            if (elm.type === "checkbox") {
                if (re.test(elm.id)) {
                    elm.checked = true;

                }
            }
        }
    } else {
        re = new RegExp(nameregex);
        for (var i = 1; i < document.forms[0].elements.length; i++) {
            elm = document.forms[0].elements[i];
            if (elm.type === "checkbox") {
                if (re.test(elm.id)) {
                    elm.checked = false;
                }
            }
        }
    }
}

function loading(status) {
    if (!document.getElementById("AjaxLoading")) {
        var ajaxLoading = '<div id="AjaxLoading" onclick="loading(false)" style="display:none;position:fixed;_position:absolute;top:50% ;left:50%;z-index:99999999;-moz-transform: translate(-50%, -50%); -ms-transform: translate(-50%, -50%); -o-transform: translate(-50%, -50%); -webkit-transform: translate(-50%, -50%); transform: translate(-50%, -50%);"><i class="fa fa-spinner fa-4x fa-spin"></i></div>';
        jQuery("body").append(ajaxLoading);
    }

    if (typeof status == "undefined" || status) {
        jQuery("#AjaxLoading").show();
    } else {
        jQuery("#AjaxLoading").fadeOut();
    }
}

function ShowHideGroup(idList) {
    var val = document.getElementById("item-" + idList).getAttribute("data-show");
    ChangeShowHideGroup(idList, val);
}

function ChangeShowHideGroup(idList, val) {
    if (val == 0) {
        $("#" + idList).slideDown();
        $("#item-" + idList).attr("data-show", "1");
        $("#showhide" + idList).html("<i class='hi hi-minus'></i>");
        SaveShowHideGroupStatus(idList, 1);
    } else {
        $("#" + idList).slideUp();
        $("#item-" + idList).attr("data-show", "0");
        $("#showhide" + idList).html("<i class='hi hi-plus'></i>");
        SaveShowHideGroupStatus(idList, 0);
    }
}

function SaveShowHideGroupStatus(idList, status) {
    var value = getCookie("ShowHideGroup");
    if (!value) value = "&";
    if (value.indexOf("&" + idList + "=1" + "&") < 0 && value.indexOf("&" + idList + "=0" + "&") < 0) value = value + idList + "=" + status + "&";
    else {
        value = value.replace("&" + idList + "=1" + "&", "&" + idList + "=" + status + "&");
        value = value.replace("&" + idList + "=0" + "&", "&" + idList + "=" + status + "&");
    }
    setCookie("ShowHideGroup", value, "30", "/", "", "");
}
function InitShowHideGroup() {
    var value = getCookie("ShowHideGroup");
    if (!value) value = "&";
    value = value.split("&");
    for (var i = 0; i < value.length; i++) {
        var idList = value[i].split("=")[0];
        var status = value[i].split("=")[1];
        if (idList && status) {
            if (status == 0) status = 1;
            else status = 0;
            ChangeShowHideGroup(idList, status);
        }
    }
}
/*
* Sets a Cookie with the given name and value.
*
* name Name of the cookie
* value Value of the cookie
* [expires] Expiration date of the cookie (default: end of current session) - điền số ngày tính từ ngày hiện tại
* [path] Path where the cookie is valid (default: path of calling document) - điền / để chạy khi có link rewrite
* [domain] Domain where the cookie is valid (default: domain of calling document)
* [secure] Boolean value indicating if the cookie transmission requires a secure transmission
*/
function setCookie(name, value, expires, path, domain, secure) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + parseInt(expires));
    document.cookie = name + "=" + escape(value) +
        ((expires) ? "; expires=" + exdate.toGMTString() : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
}
/**
* Gets the value of the specified cookie.
*
* name Name of the desired cookie.
*
* Returns a string containing value of specified cookie,
* or null if cookie does not exist.
*/
function getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1) {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else {
        begin += 2;
    }
    var end = document.cookie.indexOf(";", begin);
    if (end == -1) {
        end = dc.length;
    }
    return unescape(dc.substring(begin + prefix.length, end));
}

/**
* Deletes the specified cookie.
*
* name name of the cookie
* [path] path of the cookie (must be same as path used to create cookie)
* [domain] domain of the cookie (must be same as domain used to create cookie)
*/
function deleteCookie(name, path, domain) {
    if (getCookie(name)) { document.cookie = name + "=" + ((path) ? "; path=" + path : "") + ((domain) ? "; domain=" + domain : "") + "; expires=Thu, 01-Jan-70 00:00:01 GMT";}
}

function setUploadButtonState() {
    var maxFileSize = 4194304; // 4MB -> 4 * 1024 * 1024
    var fileUpload = $(".flimg");
    if (fileUpload.val() === "") {
        return false;
    }
    else {
        if (fileUpload[0].files[0].size < maxFileSize) {
            //$('.flimg').prop('disabled', false);
            $("input[type='submit']").show();
            $(".uploadError").text("");
            return true;
        } else {
            $("input[type='submit']").hide();
            $(".uploadError").text("Bạn đã tải lên tệp quá lớn, vui lòng chọn tệp khác có dung lượng nhỏ hơn 4Mb");
            return false;
        }
    }
}