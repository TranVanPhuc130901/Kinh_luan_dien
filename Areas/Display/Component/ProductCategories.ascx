<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductCategories.ascx.cs" Inherits="Areas_Display_Component_ProductCategories" %>
<div class="productCate">
    <span class="openCate" onclick="toggleCate()">Danh mục sản phẩm</span>
    <ul id="cateSP" class="cateHead <%=Rewrite.Length > 0 ? "":"active" %>">
        <asp:Literal runat="server" ID="ltrList"/>
    </ul>
</div>