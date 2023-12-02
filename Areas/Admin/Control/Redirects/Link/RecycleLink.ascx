<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecycleLink.ascx.cs" Inherits="Areas_Admin_Control_Redirect_Link_RecycleLink" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li>Chuyển hướng 301</li>
        <li>Danh sách link đã xóa</li>
    </ul>
    <!-- END Forms General Header -->
    <form runat="server" class="block full">
        <div class="block-title pt10 pb10 pl20 pr20">
            <a class="text-success" href="javascript:RestoreListRedirect();" title="Add"><i class="fa fa-refresh"></i> Khôi phục các mục đang chọn</a>&nbsp;&nbsp;&nbsp;
            <a class="text-danger" href="javascript:DeleteRecListRedirect();"><i class="fa fa-trash"></i> Xóa vĩnh viễn các mục đang chọn</a>
        </div>
        <div class="row">
            <div class="form-group col-md-4">
                <asp:TextBox runat="server" id="tbLink" CssClass="form-control" Width="100%" placeholder="Link chuyển hướng"></asp:TextBox>
            </div>
            <div class="form-group col-md-4">
                <asp:TextBox runat="server" id="tbDes" CssClass="form-control" Width="100%" placeholder="Link đích"></asp:TextBox>
            </div>
            <div class="form-group col-xs-6 col-md-2">
                <asp:Button runat="server" id="btSearch" CssClass="btn btn-primary" OnClick="btSearch_OnClick" Text="Tìm kiếm"/>
            </div>
            <div class="form-group col-xs-6 col-md-2">
                <asp:DropDownList runat="server" id="ddlShowNumber" AutoPostBack="True" OnSelectedIndexChanged="ddlShowNumber_OnSelectedIndexChanged" CssClass="form-control">
                    <asp:ListItem Value="10" Text="Hiển thị: 10 item"></asp:ListItem>
                    <asp:ListItem Value="20" Text="Hiển thị: 20 item"></asp:ListItem>
                    <asp:ListItem Value="50" Text="Hiển thị: 50 item"></asp:ListItem>
                    <asp:ListItem Value="100" Text="Hiển thị: 100 item"></asp:ListItem>
                </asp:DropDownList>
            </div>
            
        </div>
        <div id="ecom-products_wrapper" class="dataTables_wrapper form-inline no-footer">        
            <table id="ecom-products" class="table table-bordered table-striped table-vcenter dataTable no-footer" role="grid" aria-describedby="ecom-products_info">
                <thead>
                <tr role="row">
                    <th class="text-center"><input id="checkAll" class="cursor-pointer" type="checkbox" onchange="checkAllCheckBox('cb',this)"/></th>
                    <th class="text-center">Link</th>
                    <th class="text-center">Trạng thái</th>
                    <th class="text-center">Công cụ</th>
                </tr>
                </thead>
                <asp:Literal runat="server" ID="ltrList" />
            </table>
            <div class="row">
                <asp:Literal runat="server" ID="ltrPaging" />
            </div>
        </div>
    </form>
</div>