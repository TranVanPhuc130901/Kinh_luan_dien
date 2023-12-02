<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Category.ascx.cs" Inherits="Areas_Display_News_Control_Detail" %>
<%@ Register Src="~/Areas/Display/AboutUs/SubControl/SubAboutUsCate.ascx" TagPrefix="uc1" TagName="SubAboutUsCate" %>

<section class="gothiar_about_us">
    <div class="wrp">
        <div class="c_top">
            <asp:Literal ID="ltrInfo" runat="server"></asp:Literal>
        </div>
        <div class="c_cleft">
            <div class="contentView">
                <asp:Literal ID="ltrContent" runat="server"></asp:Literal>
            </div>
        </div>
        <div class="c_right">
            <uc1:SubAboutUsCate runat="server" ID="SubAboutUsCate" />
        </div>
    </div>
</section>