<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminLoadControl.ascx.cs" Inherits="Areas_Admin_Control_AdminLoadControl" %>
<%@ Register Src="~/Areas/Admin/Control/Component/SidebarNav.ascx" TagPrefix="uc1" TagName="SidebarNav" %>
<%@ Register Src="~/Areas/Admin/Control/Component/LanguageFlag.ascx" TagPrefix="uc1" TagName="LanguageFlag" %>
<%@ Register Src="~/Areas/Admin/Control/Component/ThemeColor.ascx" TagPrefix="uc1" TagName="ThemeColor" %>
<div id="page-wrapper">
    <div id="page-container" class="sidebar-mini sidebar-no-animations <%=Classname %>">
        <div id="sidebar">
            <div id="sidebar-scroll">
                <div class="sidebar-content">
                    <a href="/admin" class="sidebar-brand"><i class="gi gi-dashboard"></i> <span>Revos CMS</span></a>
                    <uc1:SidebarNav runat="server" ID="SidebarNav" />
                    <uc1:ThemeColor runat="server" ID="ThemeColor" />
                </div>
            </div>
        </div>
        <div id="main-container">
            <header class="navbar navbar-default">
                <ul class="nav navbar-nav-custom">
                    <li><a href="javascript:void(0)" onclick="App.sidebar('toggle-sidebar');this.blur();"><i class="fa fa-bars fa-fw"></i></a></li>
                    <uc1:LanguageFlag runat="server" ID="LanguageFlag" />
                    <li><a href="/" target="_blank" title="Trang hiển thị"><i class="fa fa-home"></i></a></li>
                </ul>
                <div class="nav_right pull-right">
                    <a href="javascript:void(0)">Hi <%=Username %></a> <a href="/admin?control=Users&action=LogOut">[Đăng xuất]</a>
                </div>
            </header>
            <asp:PlaceHolder runat="server" ID="plLoadControl"></asp:PlaceHolder>
            <footer class="clearfix">
                <div class="pull-right">Revos Viet Nam ., JSC</div>
                <div class="pull-left"><a href="javascript:void(0);" target="_blank">Version 1.3</a></div>
            </footer>
        </div>
    </div>
</div>
<%--<a target="_blank" href="/" class="quick-submit2 btn btn-primary" title="Xem website của bạn"><i class="fa fa-home"></i></a>--%>