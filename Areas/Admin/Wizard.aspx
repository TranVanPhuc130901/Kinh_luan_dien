<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Wizard.aspx.cs" Inherits="Areas_Admin_Wizard" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="vi">
<head>
    <meta charset="utf-8"/>
    <title>Wizard</title>
    <meta name="author" content="Revos JSC"/>
    <meta name="robots" content="noindex, nofollow"/>
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=0"/>
    <link rel="shortcut icon" href="/Areas/Admin/img/favicon.png"/>
    <link rel="apple-touch-icon" href="/Areas/Admin/img/icon57.png" sizes="57x57"/>
    <link rel="apple-touch-icon" href="/Areas/Admin/img/icon72.png" sizes="72x72"/>
    <link rel="apple-touch-icon" href="/Areas/Admin/img/icon76.png" sizes="76x76"/>
    <link rel="apple-touch-icon" href="/Areas/Admin/img/icon114.png" sizes="114x114"/>
    <link rel="apple-touch-icon" href="/Areas/Admin/img/icon120.png" sizes="120x120"/>
    <link rel="apple-touch-icon" href="/Areas/Admin/img/icon144.png" sizes="144x144"/>
    <link rel="apple-touch-icon" href="/Areas/Admin/img/icon152.png" sizes="152x152"/>
    <link rel="apple-touch-icon" href="/Areas/Admin/img/icon180.png" sizes="180x180"/>
    <link rel="stylesheet" href="/Areas/Admin/css/bootstrap.min.css"/>
    <link rel="stylesheet" href="/Areas/Admin/css/plugins.css"/>
    <link rel="stylesheet" href="/Areas/Admin/css/main.css"/>
    <link rel="stylesheet" href="/Areas/Admin/css/themes.css"/>
    <script src="/Areas/Admin/js/vendor/modernizr.min.js"></script>
</head>
<body>
    <div id="page-content">
        <div class="content-header">
            <div class="header-section">
                <h1>
                    <i class="fa fa-magic"></i>Khởi tạo website<br/>
                    <asp:Literal runat="server" id="ltrTitle"/>
                </h1>
            </div>
        </div>
        <div class="block">
            <div class="row">
                <div class="col-sm-6 col-sm-offset-3">
                    <form runat="server" id="progress_wizard" class="form-horizontal">
                        <div class="form-group">
                            <label class="col-md-4 control-label">Tên ngôn ngữ mặc định của website</label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" id="txtLanguage" CssClass="form-control" placeholder="vd: Tiếng Việt" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Cờ quốc gia (nếu có)</label>
                            <div class="col-md-8">
                                <asp:FileUpload runat="server" id="flFlag" CssClass="form-control" accept="image/gif, image/jpeg, image/png"/>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Tên đăng nhập</label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" id="txtUsername" CssClass="form-control" Text="admin" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Mật khẩu</label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" id="txtPassword" CssClass="form-control" minlength="5" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 control-label">Email khôi phục mật khẩu</label>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" id="txtEmail" TextMode="Email" CssClass="form-control" minlength="5" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-actions">
                            <div class="col-md-8 col-md-offset-4">
                                <asp:Button runat="server" id="next3" ClientIDMode="Predictable" OnClick="next3_OnClick" CssClass="btn btn-sm btn-primary" Text="Hoàn thành"/>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script src="/Areas/Admin/js/vendor/jquery.min.js"></script>
    <script src="/Areas/Admin/js/vendor/bootstrap.min.js"></script>
    <script src="/Areas/Admin/js/plugins.js"></script>
    <script src="/Areas/Admin/js/app.js"></script>
</body>
</html>