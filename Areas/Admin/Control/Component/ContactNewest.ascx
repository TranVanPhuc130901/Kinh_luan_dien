<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactNewest.ascx.cs" Inherits="Areas_Admin_Control_Component_ContactNewest" %>
<style>
    #ContactNewest .cot1 { width: 30px }
    #ContactNewest .cot2 { width: calc((100% - 30px - 150px - 100px - 150px) / 2);min-width: 250px }
    #ContactNewest .cot3 { width: 150px }
    #ContactNewest .cot6 { width: 100px }
    #ContactNewest .cot7 { width: 150px }
</style>
<div id="ContactNewest" class="block full">
    <div class="block-title pt10 pb10 pl20 pr20">
        LIÊN HỆ MỚI NHẤT
    </div>
    <div class="list-category">
        <div class="head item home">
            <div class="text-center cot1"><input id="checkAll" class="cursor-pointer" type="checkbox" onchange="checkAllCheckBox('cb',this)"/></div>
            <div class="cot2">Họ tên</div>
            <div class="cot2">Nội dung</div>
            <div class="text-center cot3">Ngày gửi</div>
            <div class="text-center cot6">Trạng thái</div>
            <div class="text-center cot7">Công cụ</div>
        </div>
        <asp:Literal runat="server" ID="ltrList" />
    </div>
</div>
<script src="/Areas/Admin/Ajax/Contact/js.js" defer></script>