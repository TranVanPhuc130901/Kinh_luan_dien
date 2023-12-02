function loading(status) {
    if (!document.getElementById("AjaxLoading")) {
        const ajaxLoading = '<div id="AjaxLoading" onclick="loading(false)" style="display:none;"><div class="loading"><div></div><div></div><div></div></div></div>';
        $("body").append(ajaxLoading);
    }

    if (typeof status == "undefined" || status) {
        $("#AjaxLoading").show();
    } else {
        $("#AjaxLoading").fadeOut();
    }
}
$(".navigation_expanded").on("click", function() {
    openMenu();
});
$(".navigation_close").on("click", function ()
{
    closeMenu();
});
function openMenu() {
    $(".navigation_primary").css({ right: 0, opacity: 1, visibility: "visible" });
    $(".navigation_close").addClass("active");
}
function closeMenu() {
    $(".navigation_primary").css({ right: "100%", opacity: 0, visibility: "hidden" });
    $(".navigation_close").removeClass("active");
}


//#region Search
// ẩn hiện box Search
$(".navigation_search").click(function () {
    if ($(".search-area").hasClass('active')) {
        return
    } else {
        $(".search-area").addClass("active");
        $(".search-area .search-inner input[name='keyword']").focus();
    }
});

$(".search-close-btn").click(function () {
    if ($(".search-area").hasClass('active')) $(".search-area").removeClass("active");
    return;
});

$(".search-area .search-inner form .prev_search").click(function () {
    if ($(".search-area").hasClass('active')) $(".search-area").removeClass("active");
    return;
});

// hiện ô tìm kiếm khi có giá trị
$(function () {
    // Xử lý khi có thay đổi giá trị trong input
    $(".search-area .search-inner input[name='keyword']").on("input", function () {
        var valueSearch = $(this).val();
        if (valueSearch === "") {
            // Nếu giá trị rỗng, xóa CSS cho button
            $(".search-area .search-inner button").css({
                cursor: "",
                background: "",
                transition: "all 0.3s ease"
            });
        } else {
            // Nếu có giá trị, thêm CSS cho button
            $(".search-area .search-inner button").css({
                cursor: "pointer",
                background: "#000",
                transition: "all 0.3s ease"
            });
        }
    });

    // Xử lý khi nhấn chọn item-popular
    //$(".item-popular").on("click", function () {
    //    var selectedValue = $(this).text(); // Lấy giá trị của item-popular đã chọn
    //    $(".search-area .search-inner input[name='keyword']").val(selectedValue); // Đưa giá trị vào input
    //});
});
//$('.navigation_cart').on('click',
//    function () {
//        location.href = "/gio-hang.htm";
//    });

// lấy giá trị của popular searches gán vào value khi chọn




//#endregion
//// I know that the code could be better.
//// If you have some tips or improvement, please let me know.

//$('.img-parallax').each(function () {
//    var img = $(this);
//    var imgParent = $(this).parent();
//    function parallaxImg() {
//        var speed = img.data('speed');
//        var imgY = imgParent.offset().top;
//        var winY = $(this).scrollTop();
//        var winH = $(this).height();
//        var parentH = imgParent.innerHeight();


//        // The next pixel to show on screen      
//        var winBottom = winY + winH;

//        // If block is shown on screen
//        if (winBottom > imgY && winY < imgY + parentH) {
//            // Number of pixels shown after block appear
//            var imgBottom = ((winBottom - imgY) * speed);
//            // Max number of pixels until block disappear
//            var imgTop = winH + parentH;
//            // Porcentage between start showing until disappearing
//            var imgPercent = ((imgBottom / imgTop) * 100) + (50 - (speed * 50));
//        }
//        img.css({
//            top: imgPercent + '%',
//            transform: 'translateY(-' + imgPercent + '%)'
//        });
//    }
//    $(document).on({
//        scroll: function () {
//            parallaxImg();
//        }, ready: function () {
//            parallaxImg();
//        }
//    });
//});
function CountItemsInCart() {
    jQuery.ajax({
        url: "/Areas/Display/Ajax/Product.aspx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "CountItemsInCart"
        },
        success: function (res) {
            $("#minicart-quantity").html(res[0]);
        },
        error: function () {
            thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
        }
    });
}
$(window).on('load', function () {
    CountItemsInCart();

});


// Js Update

var customerItems = document.querySelectorAll('.customerItem');
var sku;
var imageSrc;
// Lặp qua từng phần tử và thêm sự kiện onclick
if (customerItems) {
    customerItems.forEach(function (item) {
        item.addEventListener('click', function () {
            var popupCustomer = document.querySelector('.customerPopUp');
            popupCustomer.classList.add('active');
            var imageElement = item.querySelector('.image');
            imageSrc = imageElement.querySelector('img').getAttribute('data-src');
            // Thực hiện các tác vụ khi click vào ảnh
            sku = imageElement.getAttribute('data-sku');
            GetImageBySku(sku);
            GetImage(imageSrc);
            // Thêm code xử lý tại đây
        });
    });
}


document.addEventListener("DOMContentLoaded", function () {
    var customerPopUp = document.querySelector(".customerPopUp");
    if (customerPopUp) {
        var customerLight = customerPopUp.querySelector(".customerLight");
    }

    var widthScreen = screen.width;
    var homeHeader = document.querySelector('.WeHero.home');

    var promotionHeight;
    var promotion = document.querySelector('.promotionHomePage');

    if (widthScreen > 1200 && promotion) {
        if (homeHeader) {
            homeHeader.style.height = `calc(100vh - ${promotion.offsetHeight}px)`;
        }
    };

    if (promotion) {
        var header = document.querySelector('header');
        promotionHeight = promotion.offsetHeight;
        header.style.top = `${promotionHeight}px`;
    }


    // Lấy tất cả item ảnh khách hàng có trên trang
    var itemCustomer = document.querySelectorAll(".customerItem");

    var viewAll = document.querySelector(".gothiar_customer .viewAll");
    // Kiểm tra nếu có nhiều hơn 12 item thì chỉ hiện 12 item còn lại sẽ ẩn
    if (itemCustomer.length > 12) {
        for (var i = 12; i < 24; i++) {
            // kiểm tra có giá trị itemCustommer thứ i không
            if (itemCustomer[i]) {
                itemCustomer[i].style.display = 'none'
            }
        }
    }

    if (viewAll) {
        viewAll.addEventListener("click",
            function () {
                for (var i = 12; i < itemCustomer.length; i++) {
                    // kiểm tra có giá trị itemCustommer thứ i không
                    if (itemCustomer[i]) {
                        itemCustomer[i].style.display = 'block';
                    }
                }
                viewAll.style.display = 'none'; // Ẩn nút "Xem thêm" sau khi nhấn vào
            });
    }


    // Thêm sự kiện click vào phần nền customerLight
    if (customerLight) {
        customerLight.addEventListener("click", function (event) {
            // Kiểm tra xem sự kiện click có được thực hiện trên customerLight hay không
            if (event.target === this) {
                // Ẩn customerPopUp khi click ra ngoài cùng của nó
                customerPopUp.classList.remove('active');
            }
        });
    }
});
var closePopupCustomer = document.querySelector('.closePopupcustomer');
if (closePopupCustomer) {
    closePopupCustomer.addEventListener('click', function () {
        var popupCustomer = document.querySelector('.customerPopUp');
        console.log('phuc');
        popupCustomer.classList.remove('active');
    });
}

// lấy đường dẫn ảnh khách hàng đưa vào popup
function GetImage(imageSrc) {
    var image = document.querySelector('.customerPopupLeft img');
    image.setAttribute('src', imageSrc);
    image.setAttribute('alt', imageSrc);
}
// ajax lấy ảnh sản phẩm theo sku
function GetImageBySku(sku) {
    jQuery.ajax({
        url: "/Areas/Display/Ajax/Customer.aspx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "GetImageBySku",
            "sku": sku
        },
        beforeSend: function () {
            loading(true);
        },
        complete: function () {
            loading(false);
        },
        success: function (res) {
            var customerPopupRight = document.querySelector('.customerPopupRight');
            customerPopupRight.innerHTML = res;
        },
        error: function (err) {
            console.log('loi');
            var customerPopupRight = document.querySelector('.customerPopupRight');
            customerPopupRight.innerHTML = '';
            //thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
        }
    });
}