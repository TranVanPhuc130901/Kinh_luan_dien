<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="Areas_Display_News_Control_Index" %>
<%@ Register Src="~/Areas/Display/Adv/AdvSlide.ascx" TagPrefix="uc1" TagName="AdvSlide" %>
<uc1:AdvSlide runat="server" ID="AdvSlide" />
<section class="latest_stories">
    <div class="wrp">
        <asp:Literal runat="server" ID="ltrList"></asp:Literal>
    </div>
</section>
