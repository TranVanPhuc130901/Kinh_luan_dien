<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReplyComment.aspx.cs" Inherits="Areas_Admin_PopUp_Items_ReplyComment" %>
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
                <div class="col-md-12">
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:Literal runat="server" ID="ltrInfo"/>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Trả lời bình luận</legend>
                        <div class="form-group">
                            <label class="control-label col-md-2">Họ và tên</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Email</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Số điện thoại</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Nội dung</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtContent" CssClass="form-control" Rows="3" required></asp:TextBox>
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
                        <legend>Danh sách thảo luận</legend>
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