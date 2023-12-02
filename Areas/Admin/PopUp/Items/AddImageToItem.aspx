<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddImageToItem.aspx.cs" Inherits="Areas_Admin_PopUp_Items_AddImageToItem" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal runat="server" id="ltrTitle"></asp:Literal></title>
    <meta name="robots" content="noindex, nofollow" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=0" />
    <link rel="shortcut icon" href="/Areas/Admin/img/favicon.png" />
    <link rel="stylesheet" href="/Areas/Admin/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/Areas/Admin/css/plugins.css" />
    <link rel="stylesheet" href="/Areas/Admin/css/main.css" />
    <link rel="stylesheet" href="/Areas/Admin/css/themes.css" />
</head>
<body>
    <div id="page-content">
        <form id="form" runat="server" class="form-horizontal">
            <asp:HiddenField runat="server" ID="hdImage" />
            <div class="block row">
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Thêm mới hình ảnh</legend>
                        <div class="form-group">
                            <label class="control-label col-md-2">Tiêu đề</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Ảnh</label>
                            <div class="col-md-10">
                                <asp:Literal ID="ltimg" runat="server"></asp:Literal>
                                <asp:Literal runat="server" id="ltrNote"></asp:Literal>
                                <asp:FileUpload ID="flimg" AllowMultiple="True" runat="server" CssClass="flimg" accept="image/gif, image/jpeg, image/png, image/svg+xml" required/>
                                <asp:CustomValidator runat="server" id="ctUpload" ErrorMessage="" ControlToValidate="flimg" ClientValidationFunction="setUploadButtonState();"></asp:CustomValidator>
                                <div class="row">
                                    <asp:Panel runat="server" ID="pnHanCheKichThuoc" CssClass="col-md-6">
                                        <div class="checkbox">
                                            <asp:CheckBox ID="cbHanCheKichThuoc" runat="server" Text="Hạn chế kích thước ảnh" />
                                        </div>
                                        <div class="mt5">
                                            <asp:TextBox ID="tbHanCheW" placeholder="Width" Width="80" runat="server" ToolTip="Chiều rộng lớn nhất có thể của ảnh đại diện, nếu ảnh có kích thước lớn hơn nó sẽ tự co lại" CssClass="form-control not-reset d-inline-block text-center" TextMode="Number"></asp:TextBox>&nbsp;&nbsp;&nbsp;x&nbsp;&nbsp;&nbsp;
                                            <asp:TextBox ID="tbHanCheH" placeholder="Height" Width="80" runat="server" ToolTip="Chiều cao lớn nhất có thể của ảnh đại diện, nếu ảnh có kích thước lớn hơn nó sẽ tự co lại" CssClass="form-control not-reset d-inline-block text-center" TextMode="Number"></asp:TextBox>&nbsp;px
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnTaoAnhNho" CssClass="col-md-6">
                                        <div class="checkbox">
                                            <asp:CheckBox ID="cbTaoAnhNho" runat="server" Text="Tạo ảnh nhỏ" />
                                        </div>
                                        <div class="mt5">
                                            <asp:TextBox ID="tbAnhNhoW" placeholder="Width" Width="80" runat="server" ToolTip="Chiều rộng của ảnh nhỏ. Ảnh nhỏ dùng để hiển thị thay thế cho ảnh đại diện nhằm giảm tải dữ liệu phải tải về máy khách khi hiển thị" CssClass="form-control not-reset d-inline-block text-center" TextMode="Number"></asp:TextBox>&nbsp;&nbsp;&nbsp;x&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="tbAnhNhoH" placeholder="Height" Width="80" runat="server" ToolTip="Chiều cao của ảnh nhỏ. Ảnh nhỏ dùng để hiển thị thay thế cho ảnh đại diện nhằm giảm tải dữ liệu phải tải về máy khách khi hiển thị" CssClass="form-control not-reset d-inline-block text-center" TextMode="Number"></asp:TextBox>&nbsp;px
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="form-group d-none">
                            <label class="control-label col-md-2 col-sm-3">Lưu cấu hình ảnh</label>
                            <div class="col-md-10 col-sm-9"><label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbSaveConfig" /><span></span></label>
                            </div>
                        </div>
                        <div class="form-group d-none">
                            <label class="control-label col-md-2">Màu sắc</label>
                            <div class="col-md-10">
                                <asp:DropDownList runat="server" ID="ddlColor" CssClass="form-control select-chosen"/>
                            </div>
                        </div>
                        <div class="form-group d-none">
                            <label class="control-label col-md-2">Giá niêm yết</label>
                            <div class="col-md-10">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtPriceOld" TextMode="Number" CssClass="form-control" onkeyup="HienThiGia(this,'giaNiemYet');"></asp:TextBox>
                                    <div id="giaNiemYet" class="input-group-addon"><asp:Literal runat="server" id="ltrPriceOld"/></div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group d-none">
                            <label class="control-label col-md-2">Giá bán</label>
                            <div class="col-md-10">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtPriceNew" TextMode="Number" CssClass="form-control" onkeyup="HienThiGia(this,'giaBan');"></asp:TextBox>
                                    <div id="giaBan" class="input-group-addon"><asp:Literal runat="server" id="ltrPriceNew"/></div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group d-none">
                            <label class="control-label col-md-2">Link video Youtube</label>
                            <div class="col-md-10">
                                Bạn có thể nhập link video: <code>https://www.youtube.com/watch?v=wJnBTPUQS5A</code> <br/>
                                hoặc link rút gọn: <code>https://youtu.be/wJnBTPUQS5A</code> <br/>
                                hoặc mã video: <code>wJnBTPUQS5A</code> <br/>
                                hoặc mã nhúng: <code>&lt;iframe width="560" height="315" src="http://www.youtube.com/embed/wJnBTPUQS5A" frameborder="0" allowfullscreen&gt;&lt;/iframe&gt;</code>
                                <asp:TextBox runat="server" ID="txtEmbed" TextMode="MultiLine" CssClass="form-control" Rows="3"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group d-none">
                            <label class="control-label col-md-2 col-sm-3">Ảnh phụ kiện</label>
                            <div class="col-md-10 col-sm-9"><label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbPk" /><span></span></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2 col-sm-3">Thứ tự</label>
                            <div class="col-md-2 col-sm-9">
                                <asp:TextBox runat="server" ID="txtOrder" TextMode="Number" Text="1" min="0" CssClass="not-reset form-control" Width="80"></asp:TextBox>
                            </div>
                            <label class="control-label col-md-2 col-sm-3">Trạng thái</label>
                            <div class="col-md-2 col-sm-9">
                                <label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbStatus" Checked="True" /><span></span></label>
                            </div>
                            <label class="control-label col-md-3 col-sm-3">Tiếp tục tạo mục khác</label>
                            <div class="col-md-1 col-sm-9">
                                <label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbContiue" /><span></span></label>
                            </div>
                        </div>
                    </fieldset>
                    <div class="form-group">
                        <div class="col-md-10 col-md-offset-2">
                            <a runat="server" id="btnCancel" class="btn btn-default">Hủy</a>
                            <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Thêm mới" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <fieldset>
                        <div class="text-danger uploadError"></div>
                        <legend>Danh sách hình ảnh</legend>
                        <asp:Literal runat="server" id="ltrList"/>
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
    <script src="/Areas/Admin/Ajax/Items/js.js"></script>
</body>
</html>

<script>
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
</script>