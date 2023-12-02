<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="Areas_Admin_Control_Language_Keyword_Index" %>
<%@ Import Namespace="Developer.Keyword" %>
<style>
    .cot1 { width: 30px }
    .cot2 { width: calc(100% - 30px - 150px) }
    .cot7 { width: 150px }
</style>
<div id="page-content">
    <ul class="breadcrumb breadcrumb-top">
        <li><%=LanguageKeyword.Language %></li>
        <li><%=LanguageKeyword.DanhSachTuKhoa %></li>
    </ul>
    <form runat="server" class="block full">
        <div class="block-title pt10 pb10 pl20 pr20">
            <a href="<%=RevosJsc.LanguageControl.Link.LnkMnKeywordCreate() %>" title="Add"><i class="fa fa-plus"></i> <%=LanguageKeyword.ThemMoiTuKhoa %></a>&nbsp;&nbsp;&nbsp;
            <a class="text-danger" href="javascript:DeleteListKeyword();"><i class="fa fa-trash-o"></i> Xóa các mục đang chọn</a>
        </div>
        <div class="row">
            <div class="form-group col-md-4">
                <asp:TextBox runat="server" id="txtTitle" CssClass="form-control" placeholder="Từ khóa"></asp:TextBox>
            </div>
            <div class="form-group col-md-4">
                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control select-chosen" AutoPostBack="True" OnSelectedIndexChanged="btSearch_OnClick" Enabled="False"/>
            </div>
            <div class="form-group col-xs-6 col-md-2">
                <asp:Button runat="server" id="btSearch" CssClass="btn btn-primary" OnClick="btSearch_OnClick" Text="Tìm kiếm"/>
            </div>
            <div class="form-group col-xs-6 col-md-2">
                <asp:DropDownList runat="server" id="ddlShowNumber" AutoPostBack="True" OnSelectedIndexChanged="btSearch_OnClick" CssClass="form-control" Enabled="False">
                    <asp:ListItem Value="10" Text="Hiển thị: 10 item"></asp:ListItem>
                    <asp:ListItem Value="20" Text="Hiển thị: 20 item"></asp:ListItem>
                    <asp:ListItem Value="50" Text="Hiển thị: 50 item"></asp:ListItem>
                    <asp:ListItem Value="100" Text="Hiển thị: 100 item"></asp:ListItem>
                </asp:DropDownList>
            </div>
            
        </div>
        <div class="list-category">
            <div class="head item">
                <div class="text-center cot1"><input id="checkAll" class="cursor-pointer" type="checkbox" onchange="checkAllCheckBox('cb',this)"/></div>
                <div class="cot2"><asp:LinkButton runat="server" ID="lbtTitle" OnClick="lbtTitle_Click" ToolTip="Click để sắp xếp theo tiêu đề">Tiêu đề <i class="fa fa-sort"></i></asp:LinkButton></div>
                <div class="text-center cot7">Công cụ</div>
            </div>
            <asp:Literal runat="server" ID="ltrList" />
        </div>
        <div class="row">
            <asp:Literal runat="server" ID="ltrPaging" />
        </div>
    </form>
</div>
