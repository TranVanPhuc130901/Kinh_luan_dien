<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditBillDetails.aspx.cs" Inherits="Areas_Admin_PopUp_Bill_EditBillDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal runat="server" ID="ltrTitle"></asp:Literal></title>
    <link rel="stylesheet" href="/Areas/Admin/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="/Areas/Admin/css/plugins.css"/>
    <link rel="stylesheet" href="/Areas/Admin/css/main.css"/>
</head>
<body>
    <div id="page-content">
        <form id="form" runat="server" class="form-horizontal">
            <div class="block row">
                <div class="col-md-12">
                    <fieldset>
                        <div class="form-group">
                            <label class="control-label col-md-2">Link theo dõi đơn hàng</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtLink" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-10 col-md-offset-2">
                                <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Cập nhật" OnClientClick="unhook();" />
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
