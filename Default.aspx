<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Src="~/Areas/Display/DisplayLoadControl.ascx" TagPrefix="uc1" TagName="DisplayLoadControl" %>
<%@ Register Src="~/RenderJs.ascx" TagPrefix="uc1" TagName="RenderJs" %>
<%@ Register Src="~/RenderCss.ascx" TagPrefix="uc1" TagName="RenderCss" %>
<!DOCTYPE html>
<html lang="vi">
<head runat="server">
    <title><asp:Literal ID="ltrTitle" runat="server"></asp:Literal></title>
    <asp:Literal ID="ltrMetaOther" runat="server"></asp:Literal>
    <asp:Literal ID="ltrMetaShare" runat="server"></asp:Literal>
    <asp:Literal ID="ltrFavicon" runat="server"></asp:Literal>
    <asp:Literal ID="ltrCodeOnHead" runat="server"></asp:Literal>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" name="viewport" />
    <%--<script type="text/javascript">if (document.URL.indexOf("www.") > -1) window.location = document.URL.replace("www.", "");</script>--%>
    <uc1:RenderCss runat="server" ID="RenderCss" />
</head>
<body>
    <asp:Literal runat="server" ID="ltrCodeOnBody" />
    <uc1:DisplayLoadControl runat="server" />
    <asp:Literal runat="server" ID="ltrCodeUnderBody" />
    <script src="/js/lazysizes.min.js"></script>
<%--<script src="/js/jquery-3.6.0.min.js"></script>
<script src="js/owl-carousel/owl.carousel.min.js"></script>--%>
    <%--<script src="/js/Index.js"></script>--%>
    <uc1:RenderJs runat="server" ID="RenderJs" />
</body>
</html>
