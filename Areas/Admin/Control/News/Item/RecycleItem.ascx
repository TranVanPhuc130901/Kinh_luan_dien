<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecycleItem.ascx.cs" Inherits="Areas_Admin_Control_News_Item_RecycleItem" %>
<%@ Import Namespace="Developer.Keyword" %>
<style>
    .cot1 { width: 30px }
    .cot2 { width: calc(100% - 30px - 100px - 100px - 120px) }
    .cot3 { width: 100px }
    .cot4 { width: 100px }
    .cot5 { width: 120px }
</style>
<div id="page-content">
    <ul class="breadcrumb breadcrumb-top">
        <li><%=NewsKeyword.News %></li>
        <li><%=NewsKeyword.DanhSachBaiVietDaXoa %></li>
    </ul>
    <form runat="server" class="block full">
        <div class="block-title pt10 pb10 pl20 pr20">
            <a class="text-danger" href="javascript:DeleteRecListItems('<%=Control %>','<%=Pic %>');"><i class="fa fa-trash-o"></i> Xóa các mục đang chọn</a>
        </div>
        <div class="row">
            <div class="form-group col-md-4">
                <asp:TextBox runat="server" id="txtTitle" CssClass="form-control" placeholder="Tên bài viết"></asp:TextBox>
            </div>
            <div class="form-group col-md-4">
                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control select-chosen" AutoPostBack="True" OnSelectedIndexChanged="btSearch_OnClick" />
            </div>
            <div class="form-group col-xs-6 col-md-2">
                <asp:Button runat="server" id="btSearch" CssClass="btn btn-primary" OnClick="btSearch_OnClick" Text="Tìm kiếm"/>
            </div>
            <div class="form-group col-xs-6 col-md-2">
                <asp:DropDownList runat="server" id="ddlShowNumber" AutoPostBack="True" OnSelectedIndexChanged="btSearch_OnClick" CssClass="form-control">
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
                <div class="cot2">Tiêu đề</div>
                <div class="text-center cot3">Ngày đăng</div>
                <div class="text-center cot4">Lượt xem</div>
                <div class="text-center cot5">Công cụ</div>
            </div>
            <asp:Literal runat="server" ID="ltrList" />
        </div>
        <div class="row">
            <asp:Literal runat="server" ID="ltrPaging" />
        </div>
    </form>
</div>