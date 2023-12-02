$(".product_categories").on("click", ".label", function() {
    $(this).toggleClass("active");
    $(this).next().slideToggle("fast");
});

$(".product_filters").on("change", "input[type=checkbox]", function() {
    var gId = $("#hd_group_id").val();
    var rawUrl = $("#hd_raw_url").val();

    var sList = "";
    $(".product_filters input[type=checkbox]").each(function() {
        if (this.checked) sList += $(this).val() + ",";
    });
    if (sList.length > 0) sList = sList.substring(0, sList.length - 1);
    var link = "/" + rawUrl + ".htm?filter=" + sList;
    $.ajax({
        url: "/Areas/Display/Ajax/Product.aspx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "FilterProduct",
            "filter": sList,
            "gId": gId,
            "rawUrl": rawUrl
        },
        beforeSend: function() {
            loading(true);
        },
        complete: function() {
            loading(false);
        },
        success: function(res) {
            $(".gothiar_shop_right").html(res[0]);
            history.pushState({}, null, link);
            //$([document.documentElement, document.body]).animate({
            //    scrollTop: $("#Breadcrumb").offset().top
            //}, 500);
        },
        error: function() {
            //do nothing
        }
    });
});



if ($(".gothiar_shop_gallery").length > 0) $(".gothiar_shop_gallery").owlCarousel({
    loop: true,
    nav: true,
    items: 2,
    lazyLoad: true,
    center: true,
    navText: ['<svg width="16" version="1.1" viewBox="0 0 32 32" xml:space="preserve" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"><path d="M7.701,14.276l9.586-9.585c0.879-0.878,2.317-0.878,3.195,0l0.801,0.8c0.878,0.877,0.878,2.316,0,3.194  L13.968,16l7.315,7.315c0.878,0.878,0.878,2.317,0,3.194l-0.801,0.8c-0.878,0.879-2.316,0.879-3.195,0l-9.586-9.587  C7.229,17.252,7.02,16.62,7.054,16C7.02,15.38,7.229,14.748,7.701,14.276z"/></svg>', '<svg width="16" version="1.1" viewBox="0 0 32 32" xml:space="preserve" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"><path d="M7.701,14.276l9.586-9.585c0.879-0.878,2.317-0.878,3.195,0l0.801,0.8c0.878,0.877,0.878,2.316,0,3.194  L13.968,16l7.315,7.315c0.878,0.878,0.878,2.317,0,3.194l-0.801,0.8c-0.878,0.879-2.316,0.879-3.195,0l-9.586-9.587  C7.229,17.252,7.02,16.62,7.054,16C7.02,15.38,7.229,14.748,7.701,14.276z"/></svg>']
});



function thongBao(miliSecondDelay, contentPopup) {
    taoThongBao();
    $("#divNoiDungThongBao").html(contentPopup);
    miliSecondDelay = parseInt(miliSecondDelay) / 1000;
    var int = self.setInterval(
        function() {
            miliSecondDelay--;
            if (miliSecondDelay < 0) {
                window.clearInterval(int);
                huyThongBao();
            }
        }, 1000);
}

function taoThongBao() {
    var textForm = "<div id='divThongBao'><div id='divKhungThongBao'><div id='divNoiDungThongBao'><!----></div></div></div>";
    $("body").append(textForm);
    $("#divThongBao").attr("style", "background:#daf2e4;position:fixed;_position:absolute;top:10px;right:10px;z-index:9999;display:none;width:250px;max-width:calc(100% - 20px)");
    $("#divKhungThongBao").attr("style", "position:relative;padding:15px");
    $("#divNoiDungThongBao").attr("style", "color:#61c257");
    $("#divThongBao").show();
}

function huyThongBao() {
    $("#divThongBao").hide();
    $("#divThongBao").remove();
}


function buyNow() {
    var mi = $("#id_pro").val();
    var i = $(".size-item.active").attr("i");
    var color = "";
    var image = "";
    var imageSrc = "";
    size = $(".listSize .size-item.active").text();
    color = $(".listColor .color-item.active").attr("data-color");
    image = $(".listColor .color-item.active").find("img");
    var quantity = $(".box_choosenumber .choosenumber_details .hdQuantity_details").val();
    let inventory = $(".inventoryProduct span").text();
    var randomNumber = Math.floor(Math.random() * (10000 - 1 + 1)) + 1;
    if (image.length > 0) {
        imageSrc = image.attr("src");
    }

    if (dataVariant.length > 0) {
        if (color === undefined || color === "") {
            thongBao(3000, "Mời bạn chọn màu sắc");
            return;
        } else if (size === '' || size === undefined) {
            thongBao(3000, "Mời bạn chọn size");
            return;
        } else if (quantity === "0") {
            thongBao(3000, "Sản phẩm này đã hết");
            return;
        }
    }

    jQuery.ajax({
        url: "/Areas/Display/Ajax/Product.aspx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "BuyNow",
            "id": mi,
            "num": quantity,
            "mi" : i,
            "inventory": inventory
        },
        beforeSend: function() {
            loading(true);
        },
        complete: function() {
            loading(false);
        },
        success: function(res) {
            if (res[0] === "Success") {
                CountItemsInCart();
                location.href = "/gio-hang.htm";
            } else {
                thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
            }
        },
        error: function() {
            thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
        }
    });

}

function AddToCart() {
    var id = $("#id_pro").val();
    var size = "";
    var color = "";
    var image = "";
    var imageSrc = "";
    var quantity = $(".box_choosenumber .choosenumber_details .hdQuantity_details").val();
    size = $(".listSize .size-item.active").text();
    color = $(".listColor .color-item.active").attr('data-color');
    image = $(".listColor .color-item.active").find("img");
    var randomNumber = Math.floor(Math.random() * (10000 - 1 + 1)) + 1;
    if (image.length > 0) {
        imageSrc = image.attr("src");
    }

    if (dataVariant.length > 0) {
        if (color === undefined || color === '') {
            thongBao(3000, "Mời bạn chọn màu sắc");
            return;
        } else if (size === '') {
            thongBao(3000, "Mời bạn chọn size");
            return;
        }
    }

    jQuery.ajax({
        url: "/Areas/Display/Ajax/Product.aspx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "AddToCart",
            "id": id,
            "num": quantity,
            "size": size,
            "color": color,
            "image": imageSrc,
            "randomNumber": randomNumber
        },
        beforeSend: function() {
            loading(true);
        },
        complete: function() {
            loading(false);
        },
        success: function(res) {
            if (res[0] === "Success") {
                CountItemsInCart();
            } else {
                thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
            }
        },
        error: function() {
            thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
        }
    });

}

function RemoveItemInCart(id, size, color, image, randomNumber) {
    jQuery.ajax({
        url: "/Areas/Display/Ajax/Product.aspx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "RemoveItemInCart",
            "id": id,
            "size": size,
            "color": color,
            "image": image,
            "randomNumber": randomNumber
        },
        beforeSend: function() {
            loading(true);
        },
        complete: function() {
            loading(false);
        },
        success: function(res) {
            if (!res[0]) location.href = "/gio-hang-trong.htm";
            thongBao(3000, "Cập nhật thành công.");
            $("#list_items").html(res[0]);
            updateQuantityCart();
            CountItemsInCart();
        },
        error: function() {
            thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
        }
    });
}

function UpdateCart(mi, newNumber, id ) {
    jQuery.ajax({
        url: "/Areas/Display/Ajax/Product.aspx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "UpdateCart",
            "mi": mi,
            //"newSize": newSize,
            //"newColor": newColor,
            "newNumber": newNumber,
            "id" : id
            //"newImage": newImage,
            //"randomNumber": randomNumber
        },
        beforeSend: function() {
            loading(true);
        },
        complete: function() {
            loading(false);
        },
        success: function (res) {
            thongBao(3000, "Cập nhật thành công.");
            $(".area_total").html(res[0]);
            // Kiểm tra xem phần tử có tồn tại hay không

            // Lặp qua các phần tử trong giỏ hàng
            $("#list_items .colinfo-left .info").each(function() {
                var dataIdCart = $(this).attr("data-idcart");

                if (dataIdCart === id) {
                    // Xóa tất cả html bên trong
                    $(this).html("");

                    // Gán html mới cho phần tử
                    $(this).html(res[1]);
                }
            });
            CountItemsInCart();
            UpdateImageCart();
            //location.href = '/gio-hang.htm';
        },
        error: function() {
            thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
        }
    });
}

function GetInventoryProduct(id) {
    jQuery.ajax({
        url: "/Areas/Display/Ajax/Product.aspx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "GetInventoryProduct",
            "id": id
        },
        beforeSend: function () {
            loading(true);
        },
        complete: function () {
            loading(false);
        },
        success: function (res) {
            console.log(res[0]);
        },
        error: function () {
            thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
        }
    });
}

function updateQuantityCart() {
    var inv = $(".choosenumber").attr("inv");
    $(".choosenumber").each(function() {
        var spinner = jQuery(this),
            input = spinner.find('input[type="number"]'),
            btnUp = spinner.find(".augment"),
            btnDown = spinner.find(".abate"),
            min = input.attr("min"),
            max = inv;
        var newVal;
        btnUp.on("click", function() {
            var oldValue = parseFloat(input.val());
            if (oldValue >= max) {
                newVal = oldValue;
            } else {
                newVal = oldValue + 1;
            }
            spinner.find("input").val(newVal);
            spinner.find("input").trigger("change");
            btnDown.addClass("active");
        });
        btnDown.on("click", function() {
            var oldValue = parseFloat(input.val());
            if (oldValue <= min) {
                newVal = oldValue;
            } else {
                newVal = oldValue - 1;
            }
            spinner.find("input").val(newVal);
            spinner.find("input").trigger("change");
            if (newVal == 1) {
                btnDown.removeClass("active");
            }
        });
    });
}

function CountItemsInCart() {
    jQuery.ajax({
        url: "/Areas/Display/Ajax/Product.aspx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "CountItemsInCart"
        },
        success: function(res) {
            $("#minicart-quantity").html(res[0]);
            //console.log(res[1]);
        },
        error: function () {
            thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
        }
    });
}

/**
 * Cập nhật ảnh sản phẩm khi chọn trong select
 * @param {srcImage} e 
 * @returns {} 
 */
function UpdateImageCart() {
    $("#list_items .listorder li").each(function() {
        var divImage = $(this).find('.colimg');
        var image = $(this).find('#select-color option:selected').attr('imagesrc');
        divImage.find('img').attr('src', image);
    });
}

function SubmitCart(e) {
    e.preventDefault();
    if (!$("#FormDH")[0].checkValidity()) {
        // ReSharper disable once PossiblyUnassignedProperty
        $("#FormDH")[0].reportValidity();
        return;
    }
    //var gender = $("input[name=gender]:checked").val();
    var name = $("#tbName").val();
    var phone = $("#tbPhone").val();
    var email = $("#tbEmail").val();
    var address = $("#tbAddress").val();
    var message = $("#tbMessage").val();
    //var payMethod = $("input[name=PaymentMethod]:checked").val();
    jQuery.ajax({
        url: "/Areas/Display/Ajax/Product.aspx",
        type: "POST",
        //dataType: "json",
        data: {
            "action": "SubmitCart",
            //"gender": gender,
            "name": name,
            "phone": phone,
            "email": email,
            "address": address,
            "message": message,
            //"payMethod": payMethod === null ? "" : payMethod
        },
        beforeSend: function() {
            loading(true);
            $("#FormDH button[type=submit]").attr("disabled", true);
        },
        complete: function() {
            loading(false);
            $("#FormDH button[type=submit]").attr("disabled", true);
        },
        success: function() {
            location.href = "/dat-hang-thanh-cong.htm";
        },
        error: function() {
            thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
        }
    });
}

// Lấy ảnh sản phẩm và đổi ảnh sản phẩm khi nhấn vào ảnh màu sản phẩm
function GetImageByIdToColor(id) {
    // const id = $(".color-item.active").attr("dti");
    jQuery.ajax({
        url: "/Areas/Display/Ajax/Product.aspx",
        type: "POST",

        data: {
            "action": "GetImageProductById",
            "id": id
        },
        beforeSend: function() {
            loading(true);
        },
        complete: function() {
            loading(false);
        },
        success: function(response) {
            var imagePast = JSON.parse(response);
            //console.log(imagePast)
            if (Array.isArray(imagePast) && imagePast.length > 0 && imagePast[0] !== '') {
                var box = $(".box_gothiarGallery");
                var boxGallery = $(".box_gothiarGallery .gothiar_shop_gallery");
                if (boxGallery.length > 0) {
                    boxGallery.hide();
                }
                box.html(imagePast[0]);
                var boxGallery1 = $(".box_gothiarGallery .gothiar_shop_gallery1");
                boxGallery1.addClass('owl-carousel');
                boxGallery1.owlCarousel('destroy'); // Destroy old owl-carousel
                boxGallery1.owlCarousel({
                    loop: true,
                    nav: true,
                    items: 2,
                    lazyLoad: true,
                    center: true,
                    navText: [
                        '<svg width="16" version="1.1" viewBox="0 0 32 32" xml:space="preserve" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"><path d="M7.701,14.276l9.586-9.585c0.879-0.878,2.317-0.878,3.195,0l0.801,0.8c0.878,0.877,0.878,2.316,0,3.194  L13.968,16l7.315,7.315c0.878,0.878,0.878,2.317,0,3.194l-0.801,0.8c-0.878,0.879-2.316,0.879-3.195,0l-9.586-9.587  C7.229,17.252,7.02,16.62,7.054,16C7.02,15.38,7.229,14.748,7.701,14.276z"/></svg>',
                        '<svg width="16" version="1.1" viewBox="0 0 32 32" xml:space="preserve" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"><path d="M7.701,14.276l9.586-9.585c0.879-0.878,2.317-0.878,3.195,0l0.801,0.8c0.878,0.877,0.878,2.316,0,3.194  L13.968,16l7.315,7.315c0.878,0.878,0.878,2.317,0,3.194l-0.801,0.8c-0.878,0.879-2.316,0.879-3.195,0l-9.586-9.587  C7.229,17.252,7.02,16.62,7.054,16C7.02,15.38,7.229,14.748,7.701,14.276z"/></svg>'
                    ]
                });
                //if(dataVariant[0])
                var sizeToColor = [];
                var inventory = "";
                for (var i = 0; i < dataVariant.length; i ++) {
                    var colorProduct = $(".color-item.active").attr("data-color");
                        for (var j = 0; j < dataVariant.length; j++) {
                            if (colorProduct === dataVariant[j].color) {
                                var size = dataVariant[j].size;
                                sizeToColor.push(size);
                                inventory = dataVariant[j].inventory;
                            }
                    }
                    
                }
                if (sizeToColor.length > 0) {
                    // Gọi hàm displaySize với các tham số cần thiết
                    displaySize(sizeToColor, ['S', 'M', 'L', 'XL', 'XXL']);
                }
                //$(".inventoryProduct span").text(inventory);
            }
        },
        error: function() {
            thongBao(3000, "Hệ thống đang bận, bạn vui lòng thử lại sau nhé!");
        }
    });
};

//function displaySize(parameters, size) {
//    $(".size-item").each(function() {
//        var currentSize = $(this).text().trim();
//        if (!parameters.includes(currentSize)) {
//            $(this).addClass("hide");
//        } else {
//            $(this).removeClass("hide");
//        }
//    });
//}

function displaySize(parameters, size) {
    $(".size-item").each(function () {
        var currentSize = $(this).text().trim();
        var isMatch = false;
        for (var i = 0; i < parameters.length; i++) {
            if (parameters[i] === currentSize) {
                isMatch = true;
                break;
            }
        }
        if (!isMatch && size.includes(currentSize)) {
            $(this).addClass("hide");
        } else {
            $(this).removeClass("hide");
        }
    });
}

// xử lý dữ liệu của biến thể và hiển thị ra giao diện
if (typeof dataVariant !== "undefined")
    document.addEventListener("DOMContentLoaded", function() {
        if (dataVariant.length > 0) {
            var listColor = document.querySelector(".listColor");
            var listSize = document.querySelector(".listSize");


            var colorSet = new Set();
            var sizeSet = new Set();

            

            for (var i = 0; i < dataVariant.length; i++) {
                var color = dataVariant[i].color;
                var size = dataVariant[i].size;
                var image = dataVariant[i].image;
                var id = dataVariant[i].id;
                var colorReplace = dataVariant[i].colorReplace;
                var mi = dataVariant[i].mi;

                if (!colorSet.has(color)) {
                    var colorHTML = `<button class="color-item" dti="${id}" onclick="GetImageByIdToColor(${id})" data-color="${color}">${image}</button>`;
                    listColor.innerHTML += colorHTML;
                    colorSet.add(color);
                }

                if (!sizeSet.has(size)) {
                    var sizeHTML = `<div class="size-item" mi="${mi}" i="${id}" dtSize='${size}'>${size}</div>`;
                    listSize.innerHTML += sizeHTML;
                    sizeSet.add(size);
                }
            }
        } else {
            var boxColor = document.querySelector(".box-color");
            var boxSize = document.querySelector(".box-size");

            boxColor.remove();
            boxSize.remove();
        }
    });

/*// Xử lý active khi chọn color và size
//$(document).ready(function () {
//    GetImageByIdToColor($(".color-item.active").attr("dti"));
//    $(".size-item:first-child").addClass('active');
//    $(".color-item:first-child").addClass('active');
//    $(".size-item").click(function () {
//        let colorActive = $(".color-item.active").attr("data-color");
//        let sizeActive = $(this).attr("dtsize");
//        if ($(this).hasClass('hide')) return;
//        // Xóa class "active" từ tất cả các phần tử "size-item"
//        $(".size-item").removeClass("active");

//        // Thêm class "active" vào phần tử được nhấp
//        $(this).addClass("active");


//        for (let v = 0; v < dataVariant.length; v ++) {
//            if (dataVariant[v].size === sizeActive && dataVariant[v].color === colorActive) {
//                let inventoryActive = dataVariant[v].inventory;
//                $(".inventoryProduct span").text(inventoryActive);
//            } else {
//                $(".inventoryProduct span").text();
//            }
//        }

//    });
//    $(".color-item").click(function () {
//        let colorActive = $(this).attr("data-color");
//        let sizeActive = $(".size-item.active").attr("dtsize");
//        if (typeof dataVariant !== "undefined") {
//            var colorItemClick = $(this).attr('data-color');
//            var filteredSizes = dataVariant.filter(item => item.color === colorItemClick).map(item => item.size);

//            // Kiểm tra filteredSizes có dữ liệu hay không
//            if (filteredSizes.length > 0) {
//                // Gọi hàm displaySize với các tham số cần thiết
//                displaySize(filteredSizes, ['S', 'M', 'L', 'XL', 'XXL']);
//            }
//        }
//        //if ($(".size-item").hasClass('hide')) { $(".size-item").removeClass('active'); }
//        // Xóa class "active" từ tất cả các phần tử "color-item"
//        $(".color-item").removeClass("active");

//        // Thêm class "active" vào phần tử được nhấp
//        $(this).addClass("active");

//        for (let v = 0; v < dataVariant.length; v++) {
//            if (dataVariant[v].size === sizeActive && dataVariant[v].color === colorActive) {
//                let inventoryActive = dataVariant[v].inventory;
//                $(".inventoryProduct span").text(inventoryActive);
//            } else {
//                $(".inventoryProduct span").text();
//            }
//        }

//    });

//    // Lấy phần tử input
//    const inputQuantity = $(".box_choosenumber .hdQuantity_details");
//    var colorActive = $(".color-item.active").attr("data-color");
//    var sizeActive = $(".size-item.active").attr("dtsize");
//    var inventoryProduct = "";

//    $(".color-item").click(function () {
//        // Cập nhật giá trị của colorActive khi người dùng nhấn vào phần tử màu
//        colorActive = $(this).attr("data-color");
//        updateInventoryProduct();
//    });

//    $(".size-item").click(function () {
//        // Cập nhật giá trị của sizeActive khi người dùng nhấn vào phần tử kích thước
//        sizeActive = $(this).attr("dtsize");
//        updateInventoryProduct();
//    });

//    function updateInventoryProduct() {
//        for (let i = 0; i < dataVariant.length; i++) {
//            if (dataVariant[i].size === sizeActive && dataVariant[i].color === colorActive) {
//                inventoryProduct = dataVariant[i].inventory;
//            }
//        }
//    }

//    // Tạo một hàm để cập nhật số lượng
//    function updateQuantity(quantity) {
//        if (quantity >= 1) {
//            if (quantity > inventoryProduct) {
//                inputQuantity.val(inventoryProduct);
//            } else {
//                inputQuantity.val(quantity);
//            }
//        } else {
//            inputQuantity.val(inputQuantity.val());
//        }
//    }

//    inputQuantity.on("change",
//        function() {
//            updateQuantity(inputQuantity.val());
//        });

//    // Gán sự kiện click cho nút giảm
//    $(".box_choosenumber .abate_details").click(function() {
//        // Lấy giá trị hiện tại của input
//        const currentQuantity = inputQuantity.val();


//        // Giảm số lượng đi 1
//        const newQuantity = currentQuantity - 1;


//        // Cập nhật số lượng
//        updateQuantity(newQuantity);
//    });

//    // Gán sự kiện click cho nút tăng
//    $(".box_choosenumber .augment_details").click(function() {
//        // Lấy giá trị hiện tại của input
//        const currentQuantity = parseInt(inputQuantity.val());

//        // Tăng số lượng lên 1
//        let newQuantity = currentQuantity + 1;

//        if (newQuantity > inventoryProduct) newQuantity = inventoryProduct;

//        // Cập nhật số lượng
//        updateQuantity(newQuantity);
//    });

//    // đóng mở popup khuyến mãi khi có nhiều hơn 3 ưu đãi
//    var boxTableWholesale = $(".tb_wholesale");
//    var btnMoreWholesale = $(".btn-moreWholesale");
//    var closeWholesale = $(".close-box_wholesale");
//    // Hiệu ứng khi nhấn vào btnMoreWholesale
//    btnMoreWholesale.click(function() {
//        // Ẩn boxTableWholesale nếu đang hiển thị
//        if (boxTableWholesale.is(":visible")) {
//            boxTableWholesale.hide("slow");
//        } else {
//            // Hiển thị boxTableWholesale
//            boxTableWholesale.show("slow");
//        }
//    });

//    // Hiệu ứng khi nhấn vào closeWholesale
//    closeWholesale.click(function() {
//        // Ẩn boxTableWholesale
//        boxTableWholesale.hide("slow");
//    });
//});*/
$(document).ready(function () {
    let id = $(".color-item:first-child").attr("dti");
    GetImageByIdToColor(id);
    $(".size-item:first-child").addClass("active");
    $(".color-item:first-child").addClass("active");
    if (typeof dataVariant !== "undefined" && dataVariant.length > 0) {
        $(".inventoryProduct span").text(dataVariant[0].inventory);
    }

    $(".size-item").click(function () {
        let colorActive = $(".color-item.active").attr("data-color");
        let sizeActive = $(this).attr("dtsize");
        if ($(this).hasClass("hide")) return;

        $(".size-item").removeClass("active");
        $(this).addClass("active");

        updateInventoryProduct(colorActive, sizeActive);
    });

    $(".color-item").click(function () {
        let colorActive = $(this).attr("data-color");
        let sizeActive = $(".size-item.active").attr("dtsize");

        if (typeof dataVariant !== "undefined") {
            let filteredSizes = dataVariant
              .filter(item => item.color === colorActive)
              .map(item => item.size);

            if (filteredSizes.length > 0) {
                displaySize(filteredSizes, ["S", "M", "L", "XL", "XXL"]);
            }
        }

        $(".color-item").removeClass("active");
        $(this).addClass("active");

        updateInventoryProduct(colorActive, sizeActive);
    });

    const inputQuantity = $(".box_choosenumber .hdQuantity_details");
    let colorActive = $(".color-item.active").attr("data-color");
    let sizeActive = $(".size-item.active").attr("dtsize");
    let inventoryProduct = "";

    $(".color-item").click(function () {
        colorActive = $(this).attr("data-color");
        updateInventoryProduct(colorActive, sizeActive);
    });

    $(".size-item").click(function () {
        sizeActive = $(this).attr("dtsize");
        updateInventoryProduct(colorActive, sizeActive);
    });

    function updateInventoryProduct(color, size) {
        for (let i = 0; i < dataVariant.length; i++) {
            if (dataVariant[i].size === size && dataVariant[i].color === color) {
                inventoryProduct = dataVariant[i].inventory;
                break;
            }
        }
        $(".inventoryProduct span").text(inventoryProduct);
        updateQuantity(inputQuantity.val());
    }

    function updateQuantity(quantity) {
        if (quantity >= 1) {
            if (quantity > inventoryProduct) {
                inputQuantity.val(inventoryProduct);
            } else {
                inputQuantity.val(quantity);
            }
        } else {
            inputQuantity.val(inputQuantity.val());
        }
    }

    inputQuantity.on("change", function () {
        updateQuantity(inputQuantity.val());
    });

    $(".box_choosenumber .abate_details").click(function () {
        const currentQuantity = inputQuantity.val();
        const newQuantity = currentQuantity - 1;
        updateQuantity(newQuantity);
    });

    $(".box_choosenumber .augment_details").click(function () {
        const currentQuantity = parseInt(inputQuantity.val());
        let newQuantity = currentQuantity + 1;
        if (newQuantity > inventoryProduct) newQuantity = inventoryProduct;
        updateQuantity(newQuantity);
    });

    const boxTableWholesale = $(".tb_wholesale");
    const btnMoreWholesale = $(".btn-moreWholesale");
    const closeWholesale = $(".close-box_wholesale");

    btnMoreWholesale.click(function () {
        if (boxTableWholesale.is(":visible")) {
            boxTableWholesale.hide("slow");
        } else {
            boxTableWholesale.show("slow");
        }
    });

    closeWholesale.click(function () {
        boxTableWholesale.hide("slow");
    });
});

//$(window).on('load', function () {
//    CountItemsInCart();
//});