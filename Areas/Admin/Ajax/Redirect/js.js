function OnOffRedirect(id) {
    $.post("/Areas/Admin/Ajax/Redirect/Handler.ashx", { "action": "OnOffRedirect", "id": id }, function () {
        $.bootstrapGrowl("Cập nhật thành công", { type: "success" });
    });
}
function DeleteRedirect(id, link) {
    bootbox.confirm({
        animate: false,
        message: "Có phải bạn muốn xóa: " + link + "?",
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
            if (result) $.post("/Areas/Admin/Ajax/Redirect/Handler.ashx", { "action": "DeleteRedirect", "id": id, "link": link }, function () {
                $("#item" + id).fadeOut(300, function () { $(this).remove(); });
                $.bootstrapGrowl("Xóa thành công: " + link, { type: "success" });
            });
        }
    });
}
function RestoreRedirect(id, link) {
    $.post("/Areas/Admin/Ajax/Redirect/Handler.ashx", { "action": "RestoreRedirect", "id": id, "link": link }, function () {
        $("#item" + id).fadeOut(300, function () { $(this).remove(); });
        $.bootstrapGrowl("Đã khôi phục: " + link, { type: "success" });
    });
}
function DeleteListRedirect() {
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
            if (result) $.post("/Areas/Admin/Ajax/Redirect/Handler.ashx", { "action": "DeleteListRedirect", "listId": listirid }, function () {
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
function RestoreListRedirect() {
    var listirid = "";
    jQuery("input[name=tick]").each(function () {
        if (this.checked) {
            var id = this.id.substring(this.id.lastIndexOf("-") + 1);
            listirid += id + ",";
        }
    });
    if (listirid.length < 1) return;
    $.post("/Areas/Admin/Ajax/Redirect/Handler.ashx", { "action": "RestoreListRedirect", "listId": listirid }, function () {
        jQuery("input[name=tick]").each(function () {
            if (this.checked) {
                var id = this.id.substring(this.id.lastIndexOf("-") + 1);
                $("#item" + id).fadeOut(300, function () { $(this).remove(); });
            }
        });
        $.bootstrapGrowl("Khôi phục thành công các mục đã chọn", { type: "success" });
    });
}
function DeleteRecRedirect(id, link) {
    bootbox.confirm({
        animate: false,
        message: "Có phải bạn muốn xóa vĩnh viễn: " + link + "?",
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
            if (result) $.post("/Areas/Admin/Ajax/Redirect/Handler.ashx", { "action": "DeleteRecRedirect", "id": id, "link": link }, function () {
                $("#item" + id).fadeOut(300, function () { $(this).remove(); });
                $.bootstrapGrowl("Xóa thành công: " + link, { type: "success" });
            });
        }
    });
}
function DeleteRecListRedirect() {
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
        message: "Có phải bạn muốn xóa vĩnh viễn các mục đã chọn?",
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
            if (result) $.post("/Areas/Admin/Ajax/Redirect/Handler.ashx", { "action": "DeleteRecListRedirect", "listId": listirid }, function () {
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