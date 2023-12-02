<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerImage.ascx.cs" Inherits="Areas_Display_Adv_CustomerImage" %>
<section class="gothiar_customer">
    <div class="wrp">
        <asp:Literal runat="server" ID="ltrCustomer"></asp:Literal>
    </div>
</section>
<div class="customerPopUp">
    <div class="customerLight"></div>
    <div class="customerModal">
        <div class="customerPopupHeader">
                <div class="closePopupcustomer"></div>
            </div>
            <div class="customerPopupBody">
                <div class="customerPopupLeft">
                    <img src="" alt=""/>
                </div>
                <div class="customerPopupRight">
                    <img src="" alt=""/>
                </div>
            </div>
    </div>
</div>

