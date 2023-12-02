<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DisplayLoadControl.ascx.cs" Inherits="Areas_Display_DisplayLoadControl" %>
<%@ Register Src="~/Areas/Display/Component/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/Areas/Display/Component/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<uc1:Header runat="server" ID="Header" />
<asp:PlaceHolder runat="server" ID="plLoadControl"></asp:PlaceHolder>
<uc1:Footer runat="server" ID="Footer" />