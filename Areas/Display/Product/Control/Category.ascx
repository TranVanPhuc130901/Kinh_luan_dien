<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Category.ascx.cs" Inherits="Areas_Display_Product_Control_Category" %>
<%@ Register Src="~/Areas/Display/Product/SubControl/SubProductCategories.ascx" TagPrefix="uc1" TagName="SubProductCategories" %>
<%@ Register Src="~/Areas/Display/Product/SubControl/SubProductFilter.ascx" TagPrefix="uc1" TagName="SubProductFilter" %>
<%--<%@ Register Src="~/Areas/Display/Adv/AdvSlide.ascx" TagPrefix="uc1" TagName="AdvSlide" %>
<uc1:AdvSlide runat="server" ID="AdvSlide" />--%>
<section class="gothiar_shop_category">
    <div class="wrp">
        <div class="gothiar_shop_top">
            <asp:Literal ID="ltrTop" runat="server"></asp:Literal>
        </div>
        <div class="gothiar_shop_left">
            <uc1:SubProductCategories runat="server" ID="SubProductCategories" />
            <uc1:SubProductFilter runat="server" ID="SubProductFilter" />
        </div>
        <div class="gothiar_shop_right">
            <asp:Literal ID="ltrList" runat="server"></asp:Literal>
        </div>
    </div>
</section>