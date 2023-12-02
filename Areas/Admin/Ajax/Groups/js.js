var UpdateOrderGroup_action;
function UpdateOrderGroup(control, typePage, id, value) {
    if (UpdateOrderGroup_action) UpdateOrderGroup_action.abort();
    UpdateOrderGroup_action = jQuery.ajax({
        url: "/Areas/Admin/Ajax/Groups/Handler.ashx",
        type: "POST",
        data: {
            "action": "UpdateOrderGroup",
            "control": control,
            "typePage": typePage,
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
            $.bootstrapGrowl("Hệ thống đang bận, vui lòng thử lại sau hoặc báo cáo sự cố tới quản trị", { type: "warning" });
            //nothing
        }
    });
}
function OnOffGroup(control, typePage, id) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Groups/Handler.ashx",
        type: "POST",
        data: {
            "action": "OnOffGroup",
            "id": id,
            "control": control,
            "typePage": typePage
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
            $.bootstrapGrowl("Hệ thống đang bận, vui lòng thử lại sau hoặc báo cáo sự cố tới quản trị", { type: "warning" });
            //nothing
        }
    });
}

function DeleteGroup(control, typePage, id, titleItem) {
    var msg = "Thao tác sẽ xóa mục <b>" + titleItem + "</b> cùng với các mục con và bài viết nằm trong <b>" + titleItem + "</b><br/>Để tiếp tục xóa chọn <b class='text-warning'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
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
                url: "/Areas/Admin/Ajax/Groups/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteGroup",
                    "control": control,
                    "typePage": typePage,
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
                    $.bootstrapGrowl("Hệ thống đang bận, vui lòng thử lại sau hoặc báo cáo sự cố tới quản trị", { type: "warning" });
                    //do nothing
                }
            });
        }
    });
}

function DeleteListGroups(control, typePage) {
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
        message: "Có phải bạn muốn xóa các mục đã chọn cùng với các mục con và bài viết trong đó?",
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
            if (result) $.post("/Areas/Admin/Ajax/Groups/Handler.ashx", { "action": "DeleteListGroups", "listId": listId, "control": control, "typePage": typePage }, function () {
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

function RestoreGroup(control, typePage, id, titleItem) {
    jQuery.ajax({
        url: "/Areas/Admin/Ajax/Groups/Handler.ashx",
        type: "POST",
        dataType: "json",
        data: {
            "action": "RestoreGroup",
            "control": control,
            "typePage": typePage,
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
                $.bootstrapGrowl("Bạn phải khôi phục mục cha của <b>" + titleItem + "</b> trước (" + res[0] + ")", { type: "warning", delay: 5000 });
            }
        },
        error: function () {
            $.bootstrapGrowl("Hệ thống đang bận, vui lòng thử lại sau hoặc báo cáo sự cố tới quản trị", { type: "warning" });
            //nothing
        }
    });
}

function DeleteRecGroup(control, app, id, titleItem, pic) {
    var msg = "Thao tác sẽ xóa vĩnh viễn mục <b>" + titleItem + "</b> cùng với các mục con và bài viết nằm trong <b>" + titleItem + "</b><br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
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
                url: "/Areas/Admin/Ajax/Groups/Handler.ashx",
                type: "POST",
                dataType: "json",
                data: {
                    "action": "DeleteRecGroup",
                    "control": control,
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
                    $.bootstrapGrowl("Hệ thống đang bận, vui lòng thử lại sau hoặc báo cáo sự cố tới quản trị", { type: "warning" });
                    //do nothing
                }
            });
        }
    });
}

function DeleteRecListGroups(control, pic) {
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
                url: "/Areas/Admin/Ajax/Groups/Handler.ashx",
                type: "POST",
                dataType: "json",
                data: {
                    "action": "DeleteRecListGroups",
                    "control": control,
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
                    $.bootstrapGrowl("Hệ thống đang bận, vui lòng thử lại sau hoặc báo cáo sự cố tới quản trị", { type: "warning" });
                    //do nothing
                }
            });
        }
    });
}