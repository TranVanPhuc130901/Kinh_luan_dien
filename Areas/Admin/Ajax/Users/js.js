function OnOffUsers(id) {
    $.post("/Areas/Admin/Ajax/Users/Handler.ashx", { "action": "OnOffUsers", "id": id }, function () {
        $.bootstrapGrowl("Cập nhật thành công", { type: "success" });
    });
}

function DeleteUsers(id, account) {
    bootbox.confirm({
        animate: false,
        message: "Có phải bạn muốn xóa tài khoản <b>" + account + "</b>?",
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
            if (result) $.post("/Areas/Admin/Ajax/Users/Handler.ashx", { "action": "DeleteUsers", "id": id, "account": account }, function () {
                $("#item" + id).fadeOut(300, function () { $(this).remove(); });
                $.bootstrapGrowl("Xóa thành công: " + account, { type: "success" });
            });
        }
    });
}
function RestoreUsers(id, account) {
    $.post("/Areas/Admin/Ajax/Users/Handler.ashx", { "action": "RestoreUsers", "id": id, "account": account }, function () {
        $("#item" + id).fadeOut(300, function () { $(this).remove(); });
        $.bootstrapGrowl("Đã khôi phục: " + account, { type: "success" });
    });
}
function DeleteListUsers() {
    var listirid = "";
    jQuery("input[name=tick]").each(function () {
        if (this.checked) {
            var id = this.id.substring(this.id.lastIndexOf("-") + 1);
            listirid += id + ",";
        }
    });
    if (listirid.length < 1) return;
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
            if (result) $.post("/Areas/Admin/Ajax/Users/Handler.ashx", { "action": "DeleteListUsers", "listId": listirid }, function () {
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
function RestoreListUsers() {
    var listirid = "";
    jQuery("input[name=tick]").each(function () {
        if (this.checked) {
            var id = this.id.substring(this.id.lastIndexOf("-") + 1);
            listirid += id + ",";
        }
    });
    if (listirid.length < 1) return;
    $.post("/Areas/Admin/Ajax/Users/Handler.ashx", { "action": "RestoreListUsers", "listId": listirid }, function () {
        jQuery("input[name=tick]").each(function () {
            if (this.checked) {
                var id = this.id.substring(this.id.lastIndexOf("-") + 1);
                $("#item" + id).fadeOut(300, function () { $(this).remove(); });
            }
        });
        $.bootstrapGrowl("Khôi phục thành công các mục đã chọn", { type: "success" });
    });
}
function DeleteRecUsers(id, account) {
    var msg = "Thao tác sẽ xóa tài khoản <b>" + account + "</b> khỏi thùng rác và không thể khôi phục sau đó<br/>Để tiếp tục xóa chọn <b class='text-danger'>OK</b><br/>Để hủy thao tác chọn <b>Cancel</b>";
    var msgSuccess = "Bạn đã xóa thành công tài khoản <b>" + account + "</b>";
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
            if (result) $.post("/Areas/Admin/Ajax/Users/Handler.ashx", { "action": "DeleteRecUsers", "id": id, "account": account }, function () {
                $("#item" + id).fadeOut(300, function () { $(this).remove(); });
                $.bootstrapGrowl(msgSuccess, { type: "success" });
            });
        }
    });
}
function DeleteRecListUsers() {
    var listirid = "";
    jQuery("input[name=tick]").each(function () {
        if (this.checked) {
            var id = this.id.substring(this.id.lastIndexOf("-") + 1);
            listirid += id + ",";
        }
    });
    if (listirid.length < 1) return;
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
            if (result) $.post("/Areas/Admin/Ajax/Users/Handler.ashx", { "action": "DeleteRecListUsers", "listId": listirid }, function () {
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