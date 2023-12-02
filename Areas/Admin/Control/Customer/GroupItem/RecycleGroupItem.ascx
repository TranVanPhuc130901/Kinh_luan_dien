<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecycleGroupItem.ascx.cs" Inherits="Areas_Admin_Control_Customer_GroupItem_RecycleGroupItem" %>
<%@ Import Namespace="Developer.Keyword" %>
<style>
    .cot1 { width: 30px }
    .cot2 { width: calc((100% - 30px - 120px - 120px)) }
    .cot4 { width: 120px }
    .cot5 { width: 120px }
</style>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li><%=CustomerKeyword.Customer %></li>
        <li><%=CustomerKeyword.DanhSachNhomDaXoa %></li>
    </ul>
    <!-- END Forms General Header -->
    <form runat="server" class="block full">
        <div class="block-title pt10 pb10 pl20 pr20">
            <a class="text-danger" href="javascript:DeleteRecListGroups('<%=Control %>','<%=Pic %>');"><i class="fa fa-trash-o"></i> Xóa các mục đang chọn</a>
        </div>
        <div class="list-category">
            <div class="head item">
                <div class="text-center cot1">
                    <input type="checkbox" onclick="checkAllCheckBox('cb_group', this)" class="cursor-pointer"/>
                </div>
                <div class="cot2">Tiêu đề</div>
                <div class="text-center cot4">Bài viết</div>
                <div class="text-center cot5">Công cụ</div>
            </div>
            <asp:Literal runat="server" ID="ltrList"/>
        </div>
    </form>
</div>