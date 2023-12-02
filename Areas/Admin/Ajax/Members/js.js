function UpdateOrderMember(id, value) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Members/Handler.ashx",
        type: "POST",
        data: {
            "action": "UpdateOrderMember",
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
function OnOffMember(id) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Members/Handler.ashx",
        type: "POST",
        data: {
            "action": "OnOffMember",
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

function DeleteMember(page, id, titleItem) {
    var msg = "Thao tác sẽ xóa <b>" + titleItem + "</b>.<br/>Để tiếp tục xóa chọn <b class='text-warning'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
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
                url: "/Areas/Admin/Ajax/Members/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteMember",
                    "page": page,
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
function DeleteListMembers(page) {
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
        message: "Có phải bạn muốn xóa các mục đã chọn?",
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
            if (result) $.post("/Areas/Admin/Ajax/Members/Handler.ashx", { "action": "DeleteListMembers", "page": page, "listId": listId }, function () {
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
function RestoreMember(page, id, titleItem) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Members/Handler.ashx",
        type: "POST",
        data: {
            "action": "RestoreMember",
            "page": page,
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
function DeleteRecMember(page, id, titleItem, pic) {
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
                url: "/Areas/Admin/Ajax/Members/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteRecMember",
                    "page": page,
                    "id": id,
                    "pic": pic
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

function DeleteRecListMembers(page, pic) {
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
                url: "/Areas/Admin/Ajax/Members/Handler.ashx",
                type: "POST",
                dataType: "json",
                data: {
                    "action": "DeleteRecListMembers",
                    "page": page,
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
