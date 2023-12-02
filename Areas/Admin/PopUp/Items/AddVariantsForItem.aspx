<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddVariantsForItem.aspx.cs" Inherits="Areas_Admin_PopUp_Items_AddVariantsForItem" %>
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
            <div class="block row">
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Thêm mới biến thể</legend>
                        <div class="form-group">
                            <label class="control-label col-md-2">Tiêu đề</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Mô tả</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Giá niêm yết</label>
                            <div class="col-md-10">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtPriceOld" TextMode="Number" CssClass="form-control" onkeyup="HienThiGia(this,'giaNiemYet');"></asp:TextBox>
                                    <div id="giaNiemYet" class="input-group-addon"><asp:Literal runat="server" id="ltrPriceOld"/></div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Giá bán</label>
                            <div class="col-md-10">
                                <div class="input-group">
                                    <asp:TextBox runat="server" ID="txtPriceNew" TextMode="Number" CssClass="form-control" onkeyup="HienThiGia(this,'giaBan');"></asp:TextBox>
                                    <div id="giaBan" class="input-group-addon"><asp:Literal runat="server" id="ltrPriceNew"/></div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2 col-sm-3">Thứ tự</label>
                            <div class="col-md-10 col-sm-9">
                                <asp:TextBox runat="server" ID="txtOrder" TextMode="Number" Text="1" min="0" CssClass="not-reset form-control" Width="80"></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset>
                        <legend>Cấu hình</legend>
                        <div class="form-group">
                            <label class="control-label col-md-2">CPU</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBox1" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">RAM</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">HDD</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBox3" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Monitors</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBox4" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">VGA</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBox5" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Tình trạng</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBox6" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Bảo Hành</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="TextBox7" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </fieldset>
                    <div class="form-group">
                        <label class="control-label col-md-2 col-sm-3">Trạng thái</label>
                        <div class="col-md-10 col-sm-9">
                            <label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbStatus" Checked="True" /><span></span></label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2 col-sm-3">Tiếp tục tạo mục khác</label>
                        <div class="col-md-10 col-sm-9">
                            <label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbContiue" /><span></span></label>
                        </div>
                    </div>
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
                        <legend>Danh sách biến thể</legend>
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