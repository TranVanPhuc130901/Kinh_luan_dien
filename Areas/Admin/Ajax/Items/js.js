function ClonePro(igid, iid, app) {
    $.ajax({
        url: "/Areas/Admin/Ajax/Items/CloneProduct.aspx",
        type: "POST",
        dataType: "json",
        data: {
            "igid": igid,
            "iid": iid,
            "app": app
        },
        beforeSend: function () {
            loading(true);
        },
        complete: function () {
            loading(false);
        },
        success: function (result) {
            $.bootstrapGrowl(result[0], { type: "success" });
            window.open(result[1], "_blank");
        },
        error: function () {
            //nothing
        }
    });
}
var UpdateOrderItems_action;
function UpdateOrderItems(id, value) {
    if (UpdateOrderItems_action) UpdateOrderItems_action.abort();
    UpdateOrderItems_action = jQuery.ajax({
        url: "/Areas/Admin/Ajax/Items/Handler.ashx",
        type: "POST",
        data: {
            "action": "UpdateOrderItems",
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
function OnOffItems(id) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Items/Handler.ashx",
        type: "POST",
        data: {
            "action": "OnOffItems",
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

function DeleteItem(control, id, titleItem) {
    var msg = "Có phải bạn muốn xóa <b>" + titleItem + "</b>?";
    var msgSuccess = "Bạn đã xóa thành công <b>" + titleItem + "</b>";
    bootbox.confirm({
        animate: false,
        message: msg,
        buttons: {
            confirm: {
                label: "Yes",
                className: "btn-warning"
            },
            cancel: {
                label: "No"
            }
        },
        callback: function (result) {
            if (result) jQuery.ajax({
                url: "/Areas/Admin/Ajax/Items/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteItem",
                    "control": control,
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

function DeleteListItems(control) {
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
                label: "Yes",
                className: "btn-warning"
            },
            cancel: {
                label: "No"
            }
        },
        callback: function (result) {
            if (result) $.post("/Areas/Admin/Ajax/Items/Handler.ashx", { "action": "DeleteListItems", "listId": listId, "control": control }, function () {
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

function RestoreItem(control, id, titleItem) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Items/Handler.ashx",
        type: "POST",
        data: {
            "action": "RestoreItem",
            "control": control,
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
            $("#item" + id).fadeOut(300, function () { $(this).remove(); });
        },
        error: function () {
            $.bootstrapGrowl("Hệ thống đang bận", { type: "warning" });
            //do nothing
        }
    });
}

function DeleteRecItem(control, pic, id, titleItem) {
    var msg = "Thao tác sẽ xóa <b>" + titleItem + "</b> khỏi thùng rác và không thể khôi phục sau đó<br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
    var msgSuccess = "Bạn đã xóa thành công <b>" + titleItem + "</b>";
    bootbox.confirm({
        animate: false,
        message: msg,
        buttons: {
            confirm: {
                label: "OK",
                className: "btn-danger"
            },
            cancel: {
                label: "Cancel"
            }
        },
        callback: function (result) {
            if (result) jQuery.ajax({
                url: "/Areas/Admin/Ajax/Items/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteRecItem",
                    "control": control,
                    "pic": pic,
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

function DeleteRecListItems(control, pic) {
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
        message: "Thao tác sẽ xóa các mục đã chọn, không thể khôi phục sau khi xóa.<br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>",
        buttons: {
            confirm: {
                label: "OK",
                className: "btn-danger"
            },
            cancel: {
                label: "Cancel"
            }
        },
        callback: function (result) {
            if (result) $.post("/Areas/Admin/Ajax/Items/Handler.ashx", { "action": "DeleteRecListItems", "listId": listId, "control": control, "pic": pic }, function () {
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

function OnOffSubItems(id) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Items/Handler.ashx",
        type: "POST",
        data: {
            "action": "OnOffSubItems",
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
function DeleteSubItem(link, pic, id, titleItem) {
    var msg = "Thao tác sẽ xóa <b>" + titleItem + "</b> và không thể khôi phục sau đó<br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
    var msgSuccess = "Bạn đã xóa thành công <b>" + titleItem + "</b>";
    bootbox.confirm({
        animate: false,
        message: msg,
        buttons: {
            confirm: {
                label: "OK",
                className: "btn-danger"
            },
            cancel: {
                label: "Cancel"
            }
        },
        callback: function (result) {
            if (result) jQuery.ajax({
                url: "/Areas/Admin/Ajax/Items/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteSubItem",
                    "link": link,
                    "pic": pic,
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

function DeleteRecListSubItems(control, pic) {
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
        message: "Thao tác sẽ xóa các mục đã chọn, không thể khôi phục sau khi xóa.<br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>",
        buttons: {
            confirm: {
                label: "OK",
                className: "btn-danger"
            },
            cancel: {
                label: "Cancel"
            }
        },
        callback: function (result) {
            if (result) $.post("/Areas/Admin/Ajax/Items/Handler.ashx", { "action": "DeleteRecListSubItems", "listId": listId, "control": control, "pic": pic }, function () {
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
