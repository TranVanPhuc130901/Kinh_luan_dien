function UpdateOrderMenu(id, value) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Menus/Handler.ashx",
        type: "POST",
        data: {
            "action": "UpdateOrderMenu",
            "id": id,
            "value": value
        },
        beforeSend: function () {
            loading(true);
        },
        complete: function () {
            loading(false);
        },
        success: function () {
            $.bootstrapGrowl("Cập nhật thành công", { type: "success" });
        },
        error: function () {
            $.bootstrapGrowl("Hệ thống đang bận", { type: "warning" });
            //nothing
        }
    });
}

function OnOffMenu(id) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Menus/Handler.ashx",
        type: "POST",
        data: {
            "action": "OnOffMenu",
            "id": id
        },
        beforeSend: function () {
            loading(true);
        },
        complete: function () {
            loading(false);
        },
        success: function () {
            $.bootstrapGrowl("Cập nhật thành công", { type: "success" });
        },
        error: function () {
            $.bootstrapGrowl("Hệ thống đang bận", { type: "warning" });
            //nothing
        }
    });
}

function DeleteMenu(app, id, titleItem) {
    var msg = "Thao tác sẽ xóa mục <b>" + titleItem + "</b> cùng với các mục con nằm trong <b>" + titleItem + "</b><br/>Để tiếp tục xóa chọn <b class='text-warning'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
    var msgSuccess = "Bạn đã xóa thành công <b>" + titleItem + "</b>";
    bootbox.confirm({
        animate: false,
        message: msg,
        buttons: {
            confirm: {
                label: 'OK',
                className: 'btn-warning'
            },
            cancel: {
                label: 'Cancel'
            }
        },
        callback: function (result) {
            if (result) jQuery.ajax({
                url: "/Areas/Admin/Ajax/Menus/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteMenu",
                    "id": id,
                    "app": app,
                    "titleItem": titleItem
                },
                beforeSend: function () {
                    loading(true);
                },
                complete: function () {
                    loading(false);
                },
                success: function () {
                    $.bootstrapGrowl(msgSuccess, { type: "success" });
                    $("#item-" + id).fadeOut(300, function () { $(this).remove(); });
                },
                error: function () {
                    $.bootstrapGrowl("Hệ thống đang bận", { type: "warning" });
                    //do nothing
                }
            });
        }
    });
}

function DeleteListMenus(app) {
    var listId = "";
    jQuery("input[name=tick]").each(function () {
        if (this.checked) {
            var id = this.id.substring(this.id.lastIndexOf("-") + 1);
            listId += id + ",";
        }
    });
    if (listId.length < 1) return;
    bootbox.confirm({
        animate: false,
        message: "Có phải bạn muốn xóa các mục đã chọn cùng với các mục con trong đó?",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-warning'
            },
            cancel: {
                label: 'No'
            }
        },
        callback: function (result) {
            if (result) $.post("/Areas/Admin/Ajax/Menus/Handler.ashx", { "action": "DeleteListMenus", "listId": listId, "app": app }, function () {
                jQuery("input[name=tick]").each(function () {
                    if (this.checked) {
                        var id = this.id.substring(this.id.lastIndexOf("-") + 1);
                        //$("#" + id).fadeOut(300, function () { $(this).remove(); });
                        $("#item-" + id).fadeOut(300, function () { $(this).remove(); });
                    }
                });
                $.bootstrapGrowl("Xóa thành công các mục đã chọn", { type: "success" });
            });
        }
    });
}

function RestoreMenu(app, id, titleItem) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Menus/Handler.ashx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "RestoreMenu",
            "app": app,
            "id": id,
            "titleItem": titleItem
        },
        beforeSend: function () {
            loading(true);
        },
        complete: function () {
            loading(false);
        },
        success: function (res) {
            if (res[0] === "Success") {
                $.bootstrapGrowl("Khôi phục thành công " + titleItem, { type: "success" });
                $("#item-" + id).fadeOut(300, function () { $(this).remove(); });
            }
            else {
                $.bootstrapGrowl("Bạn phải khôi phục menu cha của <b>" + titleItem + "</b> trước (" + res[0] + ")", { type: "warning", delay: 5000 });
            }
        },
        error: function () {
            $.bootstrapGrowl("Hệ thống đang bận", { type: "warning" });
            //nothing
        }
    });
}

function DeleteRecMenu(app, id, titleItem, pic) {
    var msg = "Thao tác sẽ xóa vĩnh viễn mục <b>" + titleItem + "</b> cùng với các mục con nằm trong <b>" + titleItem + "</b><br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
    var msgSuccess = "Bạn đã xóa thành công <b>" + titleItem + "</b>";
    bootbox.confirm({
        animate: false,
        message: msg,
        buttons: {
            confirm: {
                label: 'OK',
                className: 'btn-danger'
            },
            cancel: {
                label: 'Cancel'
            }
        },
        callback: function (result) {
            if (result) jQuery.ajax({
                url: "/Areas/Admin/Ajax/Menus/Handler.ashx",
                type: "POST",
                dataType: "json",
                data: {
                    "action": "DeleteRecMenu",
                    "app": app,
                    "id": id,
                    "titleItem": titleItem,
                    "pic": pic
                },
                beforeSend: function () {
                    loading(true);
                },
                complete: function () {
                    loading(false);
                },
                success: function (res) {
                    $.bootstrapGrowl(msgSuccess, { type: "success" });
                    var mySplitResult = res[0].split(",");
                    for (var i = 0; i < mySplitResult.length; i++) {
                        if (mySplitResult[i].length > 0) $("#item-" + mySplitResult[i]).fadeOut(300, function () { $(this).remove(); });
                    }
                },
                error: function () {
                    $.bootstrapGrowl("Hệ thống đang bận", { type: "warning" });
                    //do nothing
                }
            });
        }
    });
}

function DeleteRecListMenus(pic) {
    var listId = "";
    jQuery("input[name=tick]").each(function () {
        if (this.checked) {
            var id = this.id.substring(this.id.lastIndexOf("-") + 1);
            listId += id + ",";
        }
    });
    if (listId.length < 1) return;
    var msg = "Thao tác sẽ xóa vĩnh viễn các mục đã chọn cùng với các mục con và bài viết nằm trong các mục đó<br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
    var msgSuccess = "Xóa thành công các mục đã chọn";
    bootbox.confirm({
        animate: false,
        message: msg,
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-danger'
            },
            cancel: {
                label: 'No'
            }
        },
        callback: function (result) {
            if (result) jQuery.ajax({
                url: "/Areas/Admin/Ajax/Menus/Handler.ashx",
                type: "POST",
                dataType: "json",
                data: {
                    "action": "DeleteRecListMenus",
                    "listId": listId,
                    "pic": pic
                },
                beforeSend: function () {
                    loading(true);
                },
                complete: function () {
                    loading(false);
                },
                success: function (res) {
                    $.bootstrapGrowl(msgSuccess, { type: "success" });
                    var mySplitResult = res[0].split(",");
                    for (var i = 0; i < mySplitResult.length; i++) {
                        if (mySplitResult[i].length > 0) $("#item-" + mySplitResult[i]).fadeOut(300, function () { $(this).remove(); });
                    }
                },
                error: function () {
                    $.bootstrapGrowl("Hệ thống đang bận", { type: "warning" });
                    //do nothing
                }
            });
        }
    });
}