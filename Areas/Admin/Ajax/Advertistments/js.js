function UpdateOrderAdvertistmentPosition(id, value) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
        type: "POST",
        data: {
            "action": "UpdateOrderAdvertistmentPosition",
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
function OnOffAdvertistmentPosition(id) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
        type: "POST",
        data: {
            "action": "OnOffAdvertistmentPosition",
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
function DeleteAdvertistmentPosition(id, titleItem) {
    var msg = "Thao tác sẽ xóa mục <b>" + titleItem + "</b> cùng với các mục con của <b>" + titleItem + "</b><br/>Để tiếp tục xóa chọn <b class='text-warning'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
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
                url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteAdvertistmentPosition",
                    "id": id,
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
function DeleteListAdvertistmentPositions() {
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
            if (result) $.post("/Areas/Admin/Ajax/Advertistments/Handler.ashx", { "action": "DeleteListAdvertistmentPositions", "listId": listId }, function () {
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
function RestoreAdvertistmentPosition(id, titleItem) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
        type: "POST",
        data: {
            "action": "RestoreAdvertistmentPosition",
            "id": id,
            "titleItem": titleItem
        },
        beforeSend: function () {
            loading(true);
        },
        complete: function () {
            loading(false);
        },
        success: function () {
            $.bootstrapGrowl("Khôi phục thành công " + titleItem, { type: "success" });
            $("#item-" + id).fadeOut(300, function () { $(this).remove(); });
        },
        error: function () {
            $.bootstrapGrowl("Hệ thống đang bận", { type: "warning" });
            //nothing
        }
    });
}
function DeleteRecAdvertistmentPosition(id, titleItem) {
    var msg = "Thao tác sẽ xóa vĩnh viễn mục <b>" + titleItem + "</b> cùng với các mục con của <b>" + titleItem + "</b><br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
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
                url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteRecAdvertistmentPosition",
                    "id": id
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
function DeleteRecListAdvertistmentPositions() {
    var listId = "";
    jQuery("input[name=tick]").each(function () {
        if (this.checked) {
            var id = this.id.substring(this.id.lastIndexOf("-") + 1);
            listId += id + ",";
        }
    });
    if (listId.length < 1) return;
    var msg = "Thao tác sẽ xóa vĩnh viễn các mục đã chọn cùng với các mục con của các mục đó<br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
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
                url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
                type: "POST",
                dataType: "json",
                data: {
                    "action": "DeleteRecListAdvertistmentPositions",
                    "listId": listId
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
function DeleteAdvertistment(id, titleItem) {
    var msg = "Thao tác sẽ xóa <b>" + titleItem + "</b><br/>Để tiếp tục xóa chọn <b class='text-warning'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
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
                url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteAdvertistment",
                    "id": id,
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
                    $("#item" + id).fadeOut(300, function () { $(this).remove(); });
                },
                error: function () {
                    $.bootstrapGrowl("Hệ thống đang bận", { type: "warning" });
                    //nothing
                }
            });
        }
    });
}
function DeleteListAdvertistments() {
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
        message: "Thao tác sẽ xóa các mục đã chọn.<br/>Để tiếp tục xóa chọn <b class='text-warning'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>",
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
            if (result) $.post("/Areas/Admin/Ajax/Advertistments/Handler.ashx", { "action": "DeleteListAdvertistments", "listId": listId }, function () {
                jQuery("input[name=tick]").each(function () {
                    if (this.checked) {
                        var id = this.id.substring(this.id.lastIndexOf("-") + 1);
                        $("#item" + id).fadeOut(300, function () { $(this).remove(); });
                    }
                });
                $.bootstrapGrowl("Xóa thành công các mục đã chọn", { type: "success" });
            });
        }
    });
}
function RestoreAdvertistment(id, titleItem) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
        type: "POST",
        data: {
            "action": "RestoreAdvertistment",
            "id": id,
            "titleItem": titleItem
        },
        beforeSend: function () {
            loading(true);
        },
        complete: function () {
            loading(false);
        },
        success: function () {
            $.bootstrapGrowl("Khôi phục thành công " + titleItem, { type: "success" });
            $("#item-" + id).fadeOut(300, function () { $(this).remove(); });
        },
        error: function () {
            $.bootstrapGrowl("Hệ thống đang bận", { type: "warning" });
            //nothing
        }
    });
}
function DeleteRecAdvertistment(id, titleItem) {
    var msg = "Thao tác sẽ xóa <b>" + titleItem + " và không thể khôi phục</b><br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
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
                url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteRecAdvertistment",
                    "id": id,
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
                    //nothing
                }
            });
        }
    });
}
function DeleteRecListAdvertistments() {
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
        message: "Thao tác sẽ xóa các mục đã chọn và không thể khôi phục.<br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>",
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
            if (result) $.post("/Areas/Admin/Ajax/Advertistments/Handler.ashx", { "action": "DeleteRecListAdvertistments", "listId": listId }, function () {
                jQuery("input[name=tick]").each(function () {
                    if (this.checked) {
                        var id = this.id.substring(this.id.lastIndexOf("-") + 1);
                        $("#item-" + id).fadeOut(300, function () { $(this).remove(); });
                    }
                });
                $.bootstrapGrowl("Xóa thành công các mục đã chọn", { type: "success" });
            });
        }
    });
}
function UpdateOrderAdvertistment(id, value) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
        type: "POST",
        data: {
            "action": "UpdateOrderAdvertistment",
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
function OnOffAdvertistment(id) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Advertistments/Handler.ashx",
        type: "POST",
        data: {
            "action": "OnOffAdvertistment",
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