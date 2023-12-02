<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddVariantsToItems.aspx.cs" Inherits="Areas_Admin_PopUp_Items_AddVariantsToItems" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></title>
    <meta name="robots" content="noindex, nofollow" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=0" />
    <link rel="shortcut icon" href="/Areas/admin/img/favicon.png" />
    <link rel="stylesheet" href="/Areas/Admin/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/Areas/Admin/css/plugins.css" />
    <link rel="stylesheet" href="/Areas/Admin/css/main.css" />
    <link rel="stylesheet" href="/Areas/Admin/css/themes.css" />
</head>
<body>
    <div id="page-content">
        <form id="form" runat="server" class="form-horizontal">
            <asp:HiddenField runat="server" ID="hdImage" />
            <asp:HiddenField runat="server" ID="hdCreateDate" />
            <asp:HiddenField runat="server" ID="HdPromotionStartDate" />
            <asp:HiddenField runat="server" ID="HdPromotionEndDate" />

            <div class="block row">
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Thêm mới biến thể</legend>
                        <div class="form-group">
                            <label class="control-label col-md-2">Tên sản phẩm</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                        <asp:Repeater runat="server" ID="rptOption">
                                <itemtemplate>
                                    <label class="control-label col-md-2"><asp:Literal runat="server" ID="ltrTitle"/></label>
                                    <div class="col-md-4">
                                        <asp:DropDownList runat="server" id="ddlOption" CssClass="form-control"></asp:DropDownList>
                                        <%--<asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>--%>
                                    </div>
                            </itemtemplate>
                        </asp:Repeater>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Ảnh</label>
                            <div class="col-md-10">
                                <asp:Literal ID="ltimg" runat="server"></asp:Literal>
                                <asp:Literal runat="server" ID="ltrNote"></asp:Literal>
                                <asp:FileUpload ID="flimg" AllowMultiple="True" runat="server" CssClass="flimg" accept="image/gif, image/jpeg, image/png, image/svg+xml" />
                                <asp:CustomValidator runat="server" ID="ctUpload" ErrorMessage="" ControlToValidate="flimg" ClientValidationFunction="setUploadButtonState();"></asp:CustomValidator>
                                <asp:LinkButton ID="lnk_delete_Image_current" runat="server" Visible="false" OnClick="lnk_delete_Image_current_Click">Xóa hình ảnh hiện tại</asp:LinkButton>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">SKU</label>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtCode" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="control-label col-md-2">Giá niêm yết</label>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtPrice" TextMode="Number" CssClass="form-control" onkeyup="HienThiGia(this,'giaNiemYet');"></asp:TextBox>
                                    <div id="giaNiemYet" class="input-group-addon">
                                        <asp:Literal runat="server" ID="ltrPriceOld" /></div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group d-none">
                            <label class="control-label col-md-2">Giá bán</label>
                            <div class="col-md-10">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtPrice2" TextMode="Number" CssClass="form-control" onkeyup="HienThiGia(this,'giaBan');"></asp:TextBox>
                                    <div id="giaBan" class="input-group-addon">
                                        <asp:Literal runat="server" ID="ltrPriceNew" /></div>
                                </div>
                            </div>
                        </div>
                        <%--                        <div class="form-group">
                            <label class="control-label col-md-2">Hình ảnh</label>
                            <div class="col-md-10">
                                <div class="input-group-checkbox">
                                    <asp:Literal runat="server" id="ltrAddImage"></asp:Literal>
                                </div>
                            </div>
                        </div>--%>
                        <%--                        <div class="form-group">
                            <label class="control-label col-md-2 col-sm-3">Thứ tự</label>
                            <div class="col-md-10 col-sm-9">
                                <asp:TextBox runat="server" ID="txtOrder" TextMode="Number" Text="1" min="0" CssClass="not-reset form-control" Width="80"></asp:TextBox>
                            </div>
                        </div>--%>
                        <div class="form-group">
                            <label class="control-label col-md-2 col-sm-3">Tồn kho</label>
                            <div class="col-md-2 col-sm-9">
                                <asp:TextBox runat="server" ID="txtQuantity" TextMode="Number" Text="100" min="0" CssClass="not-reset form-control" Width="80"></asp:TextBox>
                            </div>
                            <label class="control-label col-md-2 col-sm-3">Trạng thái</label>
                            <div class="col-md-2 col-sm-9">
                                <label class="switch switch-primary">
                                    <asp:CheckBox runat="server" ID="cbStatus" Checked="True" /><span></span></label>
                            </div>
                            <label class="control-label col-md-3 col-sm-3">Tiếp tục tạo mục khác</label>
                            <div class="col-md-1 col-sm-9">
                                <label class="switch switch-primary">
                                    <asp:CheckBox runat="server" ID="cbContiue" /><span></span></label>
                            </div>
                        </div>
                    </fieldset>
                    <div class="form-group">
                        <div class="col-md-10 col-md-offset-2">
                            <a runat="server" id="btnCancel" class="btn btn-default">Hủy</a>
                            <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Thêm mới" OnClientClick="unhook();" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <fieldset>
                        <div class="text-danger uploadError"></div>
                        <legend>Danh sách biến thể</legend>
                        <asp:Literal runat="server" ID="ltrList" />
                    </fieldset>
                    <asp:Literal runat="server" ID="ltrPaging" />
                </div>
            </div>
        </form>
    </div>
    <script src="/Areas/Admin/js/vendor/jquery.min.js"></script>
    <script src="/Areas/Admin/js/vendor/bootstrap.min.js"></script>
    <script src="/Areas/Admin/js/vendor/bootbox.min.js"></script>
    <script src="/Areas/Admin/js/plugins.js"></script>
    <script src="/Areas/Admin/js/app.js"></script>
    <script src="/Areas/Admin/Ajax/Items/js.min.js"></script>

</body>
</html>
<script>
    //window.onbeforeunload = function (e) {
    //    if (hook) {
    //        //e = e || window.event;
    //        // For IE and Firefox prior to version 4
    //        if (e) { e.returnValue = 'Sure?'; }
    //        // For Safari
    //        return 'Sure?';
    //    }
    //    return null;
    //};

    function HienThiGia(idTextBoxGia, idHienThi) {
        var gia = idTextBoxGia.value;
        gia = DinhDangGia(gia);
        document.getElementById(idHienThi).innerHTML = gia;
    }
    function DinhDangGia(number) {
        if (isNaN(number)) return "<span class='text-danger'>Giá nhập sai định dạng!</span>";
        var str = new String(number);

        var indexOfdot = str.indexOf(".", 0);
        var phanThapPhan;
        if (indexOfdot > -1) {
            phanThapPhan = "," + str.substring(indexOfdot + 1, len);
            str = str.substring(0, indexOfdot);
        }

        var result = "", len = str.length;
        for (var i = len - 1; i >= 0; i--) {
            if ((i + 1) % 3 == 0 && i + 1 != len) result += ".";
            result += str.charAt(len - 1 - i);
        }

        if (indexOfdot > -1)
            result += phanThapPhan;

        return result;
    }

    function UpdateEnableItemNew(iid, iienable) {
        $.post("/cms/admin/Ajax/Items/UpdateEnableItem.aspx", { "iid": iid, "iienable": iienable }, function (result) {
            //
        });
    }

</script>
