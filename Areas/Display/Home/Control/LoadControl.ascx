<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoadControl.ascx.cs" Inherits="Areas_Display_Home_Control_LoadControl" %>
<%@ Register Src="~/Areas/Display/Adv/AdvSlide.ascx" TagPrefix="uc1" TagName="AdvSlide" %>
<%@ Register Src="~/Areas/Display/Adv/AdvProductCategories.ascx" TagPrefix="uc1" TagName="AdvProductCategories" %>
<%@ Register Src="~/Areas/Display/Adv/AdvMidHomePage.ascx" TagPrefix="uc1" TagName="AdvMidHomePage" %>
<%@ Register Src="~/Areas/Display/News/SubControl/SubNewsHome.ascx" TagPrefix="uc1" TagName="SubNewsHome" %>
<%@ Register Src="~/Areas/Display/Adv/CustomerImage.ascx" TagPrefix="uc1" TagName="CustomerImage" %>
<uc1:AdvSlide runat="server" ID="AdvSlide" />
<uc1:AdvProductCategories runat="server" ID="AdvProductCategories" />
<uc1:AdvMidHomePage runat="server" ID="AdvMidHomePage" />
<uc1:SubNewsHome runat="server" ID="SubNewsHome" />
<uc1:CustomerImage runat="server" id="CustomerImage" />
