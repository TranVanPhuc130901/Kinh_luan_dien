<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Article.ascx.cs" Inherits="Areas_Display_Search_Control_Article" %>
<%@ Register Src="~/Areas/Display/Component/Breadcrumb.ascx" TagPrefix="uc1" TagName="Breadcrumb" %>
<%@ Register Src="~/Areas/Display/News/SubControl/SubNewsCate.ascx" TagPrefix="uc1" TagName="SubNewsCate" %>
<%@ Register Src="~/Areas/Display/News/SubControl/SubNewsRight.ascx" TagPrefix="uc1" TagName="SubNewsRight" %>
<%@ Register Src="~/Areas/Display/News/SubControl/SubNewsSearchBox.ascx" TagPrefix="uc1" TagName="SubNewsSearchBox" %>
<uc1:Breadcrumb runat="server" ID="Breadcrumb" />
<div id="content" class="container">
    <div class="col_right">
        <asp:Literal runat="server" ID="ltrList"></asp:Literal>
    </div>
    <div class="col_left">
        <uc1:SubNewsSearchBox runat="server"/>
        <uc1:SubNewsCate runat="server"/>
        <uc1:SubNewsRight runat="server"/>
    </div>
</div>
