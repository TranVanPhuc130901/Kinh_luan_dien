<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Breadcrumb.ascx.cs" Inherits="Areas_Display_Component_Breadcrumb" %>
<div class="breadcrumbb full-section">
    <div class="container">
        <ul class="breadcrumb-ul">
            <li><a href="/" title="Home page">Trang chủ</a></li>
            <asp:Literal runat="server" ID="ltrList"></asp:Literal>
            <%--<li class="breadcrumb_last"><a href="">Sản phẩm</a></li>--%>
        </ul>
    </div>
</div>
