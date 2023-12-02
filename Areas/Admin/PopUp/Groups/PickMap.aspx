<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PickMap.aspx.cs" Inherits="Areas_Admin_PopUp_Groups_PickMap" %>
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
    <style>
        .pick_map img:hover{cursor: crosshair}
        /*.p_relative{position: relative}
        .p_relative svg{ position: absolute;top: 0;left: 0;cursor: crosshair;width: 100%;height: 100%}
        polygon.hover_able{fill: transparent;stroke-width: 1;cursor: pointer;stroke:black;}*/
    </style>
</head>
<body>
    <div id="page-content">
        <form id="form" runat="server" class="form-horizontal">
            <asp:HiddenField runat="server" ID="hdImage" />
            <div class="block row">
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Đánh dấu địa điểm: <asp:Literal runat="server" id="ltrHead"></asp:Literal></legend>
                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group pick_map">
                            <div class="col-md-12">
                                <asp:Literal ID="ltrImg" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </fieldset>
                    <div class="form-group">
                        <div class="col-md-10 col-md-offset-2">
                            <a runat="server" id="btnCancel" class="btn btn-default">Hủy</a>
                            <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Visible="False" Text="Cập nhật" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
    <script src="/Areas/Admin/js/vendor/jquery.min.js"></script>
    <script src="/Areas/Admin/js/vendor/bootstrap.min.js"></script>
    <script src="/Areas/Admin/js/vendor/bootbox.min.js"></script>
    <script src="/Areas/Admin/js/plugins.js"></script>
    <script src="/Areas/Admin/js/app.js"></script>
    <script>
        $(".pick_map img").on("click", function (event) {
            var x = event.pageX - $(this).offset().left;
            var y = event.pageY - $(this).offset().top;
            var oldText = $("input[name=txtTitle]").val();
            var newText = x + "," + y + ",";
            $("input[name=txtTitle]").val(oldText + newText);
            //$("polygon").attr("points", oldText + newText)
        })
    </script>
</body>
</html>
