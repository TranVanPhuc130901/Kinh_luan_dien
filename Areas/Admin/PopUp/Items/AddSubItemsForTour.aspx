<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSubItemsForTour.aspx.cs" Inherits="Areas_Admin_PopUp_Items_AddSubItemsForTour" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal runat="server" ID="ltrTitle"></asp:Literal></title>
    <meta name="description" content="Revos CMS 1.1" />
    <meta name="author" content="Revos JSC" />
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
                            <label class="control-label col-md-2">Mô tả</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Số sao</label>
                            <div class="col-md-10">
                                <asp:DropDownList runat="server" id="ddlStar" CssClass="form-control">
                                    <asp:ListItem Value="6" Text="6 sao"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5 sao"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4 sao"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3 sao"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2 sao"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1 sao"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Ảnh</label>
                            <div class="col-md-10">
                                <asp:Literal ID="ltimg" runat="server"></asp:Literal>
                                <asp:Literal runat="server" ID="ltrNote"></asp:Literal>
                                <asp:FileUpload ID="flimg" AllowMultiple="True" runat="server" CssClass="flimg" accept="image/gif, image/jpeg, image/png, image/svg+xml" required />
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
                        <div class="form-group">
                            <label class="control-label col-md-2">Thứ tự</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtOrder" TextMode="Number" Text="1" min="0" CssClass="not-reset form-control" Width="80"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Lưu cấu hình ảnh</label>
                            <div class="col-md-10">
                                <label class="switch switch-primary">
                                    <asp:CheckBox runat="server" ID="cbSaveConfig" /><span></span></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Trạng thái</label>
                            <div class="col-md-10">
                                <label class="switch switch-primary">
                                    <asp:CheckBox runat="server" ID="cbStatus" Checked="True" /><span></span></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Tiếp tục tạo mục khác</label>
                            <div class="col-md-10">
                                <label class="switch switch-primary">
                                    <asp:CheckBox runat="server" ID="cbContiue" /><span></span></label>
                            </div>
                        </div>
                    </fieldset>
                    <div class="form-group">
                        <div class="col-md-10 col-md-offset-2">
                            <div class="text-danger uploadError"></div>
                            <a runat="server" id="btnCancel" class="btn btn-default">Hủy</a>
                            <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Thêm mới" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Danh sách hình ảnh</legend>
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
    <script src="/Areas/Admin/Ajax/Items/js.js"></script>
</body>
</html>