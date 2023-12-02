<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Areas_Admin_Index" ValidateRequest="false" %>
<%@ Register Src="~/Areas/Admin/RenderCss.ascx" TagPrefix="uc1" TagName="RenderCss" %>
<%@ Register Src="~/Areas/Admin/RenderJs.ascx" TagPrefix="uc1" TagName="RenderJs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="vi">
<head runat="server">
    <title>Khu vực quản trị website</title>
    <meta name="description" content="Revos CMS 1.3" />
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
    <uc1:RenderCss runat="server" ID="RenderCss" />
    <link rel="stylesheet" href="/Areas/Admin/css/themes.css"/>
    <%--<script src="/Areas/Admin/js/vendor/modernizr.min.js"></script>--%>
</head>
<body>
    <asp:PlaceHolder runat="server" id="plLoadControl"></asp:PlaceHolder>
    <a href="#" id="to-top"><i class="fa fa-angle-double-up"></i></a>
    <script src="/Areas/Admin/js/vendor/jquery.min.js"></script>
    <script src="/Areas/Admin/js/vendor/bootstrap.min.js"></script>
    <script src="/Areas/Admin/js/vendor/bootbox.min.js"></script>
    <script src="/Areas/Admin/js/plugins.js"></script>
    <%--<script src="/Areas/Admin/js/app.min.js"></script>--%>
    <uc1:RenderJs runat="server" ID="RenderJs" />
    <script src="/Areas/Admin/Ajax/Groups/js.min.js"></script>
    <script src="/Areas/Admin/Ajax/Items/js.min.js"></script>
    <script src="/Areas/Admin/Ajax/Filter/js.js"></script>
</body>
</html>