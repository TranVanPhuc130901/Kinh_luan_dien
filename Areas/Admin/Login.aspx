<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Areas_Admin_Authentication" %>
<%@ Register Src="~/Areas/Admin/Control/Component/LoginAltContainer.ascx" TagPrefix="uc1" TagName="LoginAltContainer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="vi">
<head>
    <meta charset="utf-8"/>
    <title>Login</title>
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
    <div class="container">
        <div class="row">
            <div class="col-md-5 col-md-offset-1">
                <uc1:LoginAltContainer runat="server" ID="LoginAltContainer" />
            </div>
            <div class="col-md-5">
                <div id="login-container">
                    <div class="login-title text-center">
                        <h1><strong>Đăng nhập hệ thống</strong></h1>
                    </div>
                    <div class="block push-bit">
                        <form runat="server" id="form_login" clientidmode="Static" class="form-horizontal">
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <asp:Literal runat="server" id="ltrMes"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="gi gi-user"></i></span>
                                        <asp:TextBox runat="server" id="login_name" CssClass="form-control input-lg" placeholder="Username"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="gi gi-lock"></i></span>
                                        <asp:TextBox runat="server" id="login_password" TextMode="Password" CssClass="form-control input-lg" placeholder="Password"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group form-actions">
                                <div class="col-xs-4">
                                    <label class="switch switch-primary" data-toggle="tooltip" title="Ghi nhớ đăng nhập?">
                                        <asp:CheckBox runat="server" id="login_remember_me" Checked="True" />
                                        <span></span>
                                    </label>
                                </div>
                                <div class="col-xs-8 text-right">
                                    <asp:Button runat="server" id="btn_login" OnClick="btn_login_OnClick" CssClass="btn btn-sm btn-primary" Text="Đăng nhập"/>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12 text-center">
                                    <a href="/admin/reset"><small>Bạn quên mật khẩu?</small></a>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="/Areas/Admin/js/vendor/jquery.min.js"></script>
    <script src="/Areas/Admin/js/vendor/bootstrap.min.js"></script>
    <script src="/Areas/Admin/js/plugins.js"></script>
    <script src="/Areas/Admin/js/app.js"></script>
    <script src="/Areas/Admin/js/pages/login.js"></script>
    <script>$(function () { Login.init(); });</script>
</body>
</html>
