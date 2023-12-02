function OnOffLanguage(id) {
    $.post("/Areas/Admin/Ajax/Language/Handler.ashx", { "action": "OnOffLanguage", "id": id }, function () {
        $.bootstrapGrowl("Cập nhật thành công", { type: "success" });
    });
}
function DeleteKeyword(id, titleItem) {
    var msg = "Thao tác sẽ xóa từ khóa <b>" + titleItem + "</b><br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
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
                url: "/Areas/Admin/Ajax/Language/Handler.ashx",
                type: "POST",
                data: {
                    "action": "DeleteKeyword",
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

function DeleteListKeyword() {
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
                label: 'OK',
                className: 'btn-danger'
            },
            cancel: {
                label: 'Cancel'
            }
        },
        callback: function (result) {
            if (result) $.post("/Areas/Admin/Ajax/Language/Handler.ashx", { "action": "DeleteListKeyword", "listId": listId }, function () {
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
