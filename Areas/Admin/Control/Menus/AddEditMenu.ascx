<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditMenu.ascx.cs" Inherits="Areas_Admin_Control_Menu_AddEditMenu" %>
<%@ Register Src="~/Areas/Admin/Control/Component/UploadImage.ascx" TagPrefix="uc1" TagName="UploadImage" %>
<%@ Import Namespace="Developer.Keyword" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li><%=MenusKeyword.Menus %></li>
        <li><asp:Literal runat="server" id="ltrTitle"/></li>
    </ul>
    <!-- END Forms General Header -->
    <form id="form" runat="server" class="form-horizontal">
        <asp:HiddenField runat="server" ID="HdParentId" />
        <asp:HiddenField runat="server" ID="HdTitle" />
        <div class="block row">
            <div class="col-md-12">
                <fieldset>
                    <legend>Thông tin cơ bản</legend>
                    <div class="form-group">
                        <label class="control-label col-md-2">Menu cha</label>
                        <div class="col-md-10">
                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control select-chosen"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Chọn trang có sẵn</label>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="ddlControl" AutoPostBack="True" OnSelectedIndexChanged="ddlControl_OnSelectedIndexChanged" CssClass="form-control select-chosen" />
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="ddlControlCate" AutoPostBack="True" OnSelectedIndexChanged="ddlControlCate_OnSelectedIndexChanged" CssClass="form-control select-chosen" />
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Tên memu <span></span></label>
                        <div class="col-md-5">
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList runat="server" ID="ddlOpenOption" CssClass="form-control">
                                <asp:ListItem Value="0">Mở trang tại của sổ hiện tại</asp:ListItem>
                                <asp:ListItem Value="1">Mở trang tại của sổ mới</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Đường dẫn <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtLink" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <uc1:UploadImage runat="server" id="UploadImage" />
                    <div class="form-group">
                        <label class="control-label col-md-2">Ngày đăng</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDate" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Thứ tạo</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtOrder" TextMode="Number" Text="1" min="0" CssClass="not-reset form-control" Width="80"></asp:TextBox>
                        </div>
                        <label class="control-label col-md-2">Trạng thái</label>
                        <div class="col-md-2">
                            <label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbStatus" Checked="True" /><span></span></label>
                        </div>
                        <label class="control-label col-md-3">Tiếp tục tạo mục khác</label>
                        <div class="col-md-1">
                            <label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbContiue" /><span></span></label>
                        </div>
                    </div>
                </fieldset>
                
                <div class="form-group">
                    <div class="col-md-10 col-md-offset-2">
                        <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Thêm mới" />
                    </div>
                </div>
                <asp:Button runat="server" ID="btSubmit2" OnClick="btSubmit_OnClick" CssClass="quick-submit btn btn-primary" Text="Save"/>
            </div>
        </div>
    </form>
</div>
