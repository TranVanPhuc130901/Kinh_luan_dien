<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Promotion.ascx.cs" Inherits="Areas_Display_Component_MenuOther" %>
<asp:Literal ID="ltrPromotion" runat="server"></asp:Literal>
<style>
    
    .promotionHomePage  {
        background: #000;
        display: flex;
        justify-content: center;
        align-items: center;
        max-height: 46px;
    }

.promotionHomePage .image {
        max-height: 100%;
        display: flex;
    }

    .promotionHomePage img {
        height: 100%;
        width: 100%;
        object-fit: contain;
        object-position: center;
    }

</style>

