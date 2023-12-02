<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Areas_Display_Product_Control_Detail" %>
<%@ Register Src="~/Areas/Display/Product/SubControl/SubProductRecently.ascx" TagPrefix="uc1" TagName="SubProductRecently" %>
<%@ Register Src="~/Areas/Display/Product/SubControl/SubProductOther.ascx" TagPrefix="uc1" TagName="SubProductOther" %>

<div class="gothiar_shop_detail">
    <div class="wrp">
        <div class="gothiar_shop_top">
            <asp:Literal ID="ltrTop" runat="server"></asp:Literal>
        </div>
    </div>
    <div class="box_gothiarGallery">
        <asp:Literal ID="ltrGallery" runat="server"></asp:Literal>
    </div>
    <div class="wrp">
        <div class="contentView_pro info_product">
            <asp:Literal runat="server" ID="ltrInfo"></asp:Literal>
            <asp:Literal runat="server" ID="ltrWholesale"></asp:Literal>
            <div class="box-color">
               <div class="top-color">
                   <div class="color-title">Màu Sắc:</div>
                   <div class="color-name"></div>
               </div>
               <div class="listColor"></div>
           </div>
            <div class="box-size">
                <div class="size-title">Kích cỡ:</div>
                <div class="listSize"></div>
            </div>
            <asp:Literal runat="server" ID="ltrData"></asp:Literal>
            <div class="inventoryProduct"><span>100</span> sản phẩm có sẵn </div>
            <div class="box_choosenumber">
                <div class="choosenumber_details">
                    <div class="abate_details active"></div>
                    <input class="hdQuantity_details number" type="number" value="1" min="1">
                    <div class="augment_details"></div>
                </div>
            </div>
            <div class="checkout">
                <a href="javascript:void(0);" onclick="AddToCart();" class="buy-btn add_cart-btn">Thêm vào giỏ hàng</a>
                <a href="javascript:void(0);" onclick="buyNow();" class="buy-btn">Mua Ngay</a>
            </div>
        </div>
        
    </div>
    
    <div class="wrp">
        <div class="contentView contentView_pro">
            <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
        </div>
        <uc1:SubProductOther runat="server" ID="SubProductOther" />
        <uc1:SubProductRecently runat="server" ID="SubProductRecently" />
    </div>
</div>

<script>
    document.addEventListener("DOMContentLoaded", function (event) {
        //GetListCart();
        //GetAllLocal();
        updateQuantityCart();
    });
</script>