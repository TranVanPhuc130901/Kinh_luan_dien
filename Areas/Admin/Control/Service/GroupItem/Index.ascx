<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="Areas_Admin_Control_Service_GroupItem_Index" %>
<%@ Import Namespace="Developer.Keyword" %>
<style>
    .cot1 { width: 30px }
    .cot2 { width: calc(100% - 30px - 65px - 65px - 100px - 120px) }
    .cot3 { width: 65px }
    .cot5 { width: 65px }
    .cot6 { width: 100px }
    .cot7 { width: 120px }
</style>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li><%=ServiceKeyword.Service %></li>
        <li><%=ServiceKeyword.DanhSachNhom %></li>
    </ul>
    <!-- END Forms General Header -->
    <form runat="server" class="block full">
        <div class="block-title pt10 pb10 pl20 pr20">
            <a href="<%=RevosJsc.ServiceControl.Link.LnkMnServiceGroupItemCreate() %>" title="Add"><i class="fa fa-plus"></i> <%=ServiceKeyword.ThemMoiNhom %></a>&nbsp;&nbsp;&nbsp;
            <a class="text-warning" href="javascript:DeleteListGroups('<%=Control %>','<%=Action %>');"><i class="fa fa-trash-o"></i> Xóa các mục đang chọn</a>
        </div>
        <div class="list-category">
            <div class="head item">
                <div class="text-center cot1">
                    <input type="checkbox" onclick="checkAllCheckBox('cb_group', this)" class="cursor-pointer"/>
                </div>
                <div class="cot2">Tiêu đề</div>
                <div class="text-center cot3">Bài viết</div>
                <div class="text-center cot5">STT</div>
                <div class="text-center cot6">Trạng thái</div>
                <div class="text-center cot7">Công cụ</div>
            </div>
            <asp:Literal runat="server" ID="ltrList"/>
        </div>
    </form>
</div>
