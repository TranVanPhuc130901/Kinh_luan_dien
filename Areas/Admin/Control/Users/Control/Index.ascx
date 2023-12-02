<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="Areas_Admin_Control_Users_Control_Index" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li>Tài khoản quản trị</li>
        <li>Danh sách tài khoản</li>
    </ul>
    <!-- END Forms General Header -->
    <form runat="server" class="block full">
        <div class="block-title pt10 pb10 pl20 pr20">
            <a href="<%=RevosJsc.UsersControl.Link.LnkUsersCreate() %>" title="Add"><i class="fa fa-plus"></i> Thêm mới tài khoản</a>&nbsp;&nbsp;&nbsp;
            <a class="text-warning" href="javascript:DeleteListUsers();"><i class="fa fa-trash-o"></i> Xóa các mục đang chọn</a>
        </div>
        <div class="row">
            <div class="form-group col-md-3">
                <asp:TextBox runat="server" id="tbUsername" CssClass="form-control" Width="100%" placeholder="username"></asp:TextBox>
            </div>
            <div class="form-group col-md-3">
                <asp:TextBox runat="server" id="tbPhone" CssClass="form-control" Width="100%" placeholder="số điện thoại"></asp:TextBox>
            </div>
            <div class="form-group col-md-3">
                <asp:TextBox runat="server" id="tbEmail" CssClass="form-control" Width="100%" placeholder="email"></asp:TextBox>
            </div>
            <div class="form-group col-xs-6 col-md-1">
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
        <div id="ecom-products_wrapper" class="dataTables_wrapper form-inline table-responsive">
            <table id="ecom-products" class="table table-bordered table-striped table-vcenter dataTable no-footer" role="grid" aria-describedby="ecom-products_info">
                <thead>
                    <tr role="row">
                        <th class="text-center"><input id="checkAll" type="checkbox" onchange="checkAllCheckBox('cb',this)" class="cursor-pointer"/></th>
                        <th class="text-center">Tên tài khoản</th>
                        <th class="text-center">Ngày tạo</th>
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
