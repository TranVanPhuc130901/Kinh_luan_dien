<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="Areas_Admin_Control_Product_Item_Index" %>
<%@ Import Namespace="Developer.Keyword" %>
<style>
    .cot1 { width: 30px }
    .cot2 { width: calc(100% - 30px - 100px - 100px - 65px - 100px - 150px) }
    .cot3 { width: 100px }
    .cot4 { width: 100px }
    .cot5 { width: 65px }
    .cot6 { width: 100px }
    .cot7 { width: 150px }
</style>
<div id="page-content">
    <ul class="breadcrumb breadcrumb-top">
        <li><%=ProductKeyword.Product %></li>
        <li><%=ProductKeyword.DanhSachBaiViet %></li>
    </ul>
    <form runat="server" class="block full">
        <div class="block-title pt10 pb10 pl20 pr20">
            <a href="<%=RevosJsc.ProductControl.Link.LnkMnProductItemCreate() %>" title="Add"><i class="fa fa-plus"></i> <%=ProductKeyword.ThemMoiBaiViet %></a>&nbsp;&nbsp;&nbsp;
            <a class="text-warning" href="javascript:DeleteListItems('<%=Control %>','<%=Pic %>');"><i class="fa fa-trash-o"></i> Xóa các mục đang chọn</a>
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
                <div class="cot2"><asp:LinkButton runat="server" ID="lbtTitle" OnClick="lbtTitle_Click" ToolTip="Click để sắp xếp theo tiêu đề">Tiêu đề <i class="fa fa-sort"></i></asp:LinkButton></div>
                <div class="text-center cot3"><asp:LinkButton runat="server" ID="lbtCreateDate" OnClick="lbtCreateDate_Click" ToolTip="Click để sắp xếp theo ngày đăng">Ngày đăng <i class="fa fa-sort"></i></asp:LinkButton></div>
                <div class="text-center cot4"><asp:LinkButton runat="server" ID="lbtTotalview" OnClick="lbtTotalview_Click" ToolTip="Click để sắp xếp theo lượt xem">Lượt xem <i class="fa fa-sort"></i></asp:LinkButton></div>
                <div class="text-center cot5"><asp:LinkButton runat="server" ID="lbtOrder" OnClick="lbtOrder_Click" ToolTip="Click để sắp xếp theo lượt xem">STT <i class="fa fa-sort"></i></asp:LinkButton></div>
                <div class="text-center cot6"><asp:LinkButton runat="server" ID="lbtStatus" OnClick="lbtStatus_Click" ToolTip="Click để sắp xếp theo trạng thái">Trạng thái <i class="fa fa-sort"></i></asp:LinkButton></div>
                <div class="text-center cot7">Công cụ</div>
            </div>
            <asp:Literal runat="server" ID="ltrList" />
        </div>
        <div class="row">
            <asp:Literal runat="server" ID="ltrPaging" />
        </div>
    </form>
</div>
