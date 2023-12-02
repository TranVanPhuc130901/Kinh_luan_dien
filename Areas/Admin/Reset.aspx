<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reset.aspx.cs" Inherits="Areas_Admin_Reset" %>
<%@ Register Src="~/Areas/Admin/Control/Component/LoginAltContainer.ascx" TagPrefix="uc1" TagName="LoginAltContainer" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="vi">
<head>
    <meta charset="utf-8"/>
    <title>Reset password</title>
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
                        <h1><strong>Khôi phục mật khẩu</strong></h1>
                    </div>
                    <div class="block push-bit">
                        <form id="form_reminder" class="form-horizontal">
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="gi gi-envelope"></i></span>
                                        <input id="reminder_username" class="form-control input-lg" placeholder="Username" required=""/>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12">
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="gi gi-barcode"></i></span>
                                        <input id="reminder_captcha" class="form-control input-lg" placeholder="Mã bảo vệ" required=""/>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group form-actions">
                                <div class="col-xs-12 d-flex">
                                    <img id="myimg" src="/Areas/Admin/Captcha.ashx" alt="captcha" class="cursor-pointer" onclick="reload_captcha();" style="align-self: flex-start"/>
                                    <button class="btn btn-sm btn-primary" onclick="resetPassword(event)">Xác nhận</button>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-xs-12 text-center">
                                    <small>Bạn đã nhớ mật khẩu?</small> <a href="/admin/login"><small>Đăng nhập</small></a>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
<div id="modal-terms" class="modal" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Thông báo</h4>
            </div>
            <div class="modal-body">
                <p><asp:Literal runat="server" id="ltrMes"/></p>
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
