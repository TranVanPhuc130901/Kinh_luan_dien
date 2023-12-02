<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditCategory.ascx.cs" Inherits="Areas_Admin_Control_Contact_Category_AddEditCategory" %>
<%@ Register Src="~/Areas/Admin/Control/Component/UploadImage.ascx" TagPrefix="uc1" TagName="UploadImage" %>
<%@ Import Namespace="Developer.Keyword" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li><%=ContactKeyword.Contact %></li>
        <li><asp:Literal runat="server" id="ltrTitle"/></li>
    </ul>
    <!-- END Forms General Header -->
    <form id="form" runat="server" class="form-horizontal">
        <asp:HiddenField runat="server" ID="HdParentId" />
        <asp:HiddenField runat="server" ID="HdTitle" />
        <asp:HiddenField ID="HdOldContent" runat="server" />
        <asp:HiddenField ID="HdTotalView" runat="server" />
        <div class="block row">
            <div class="col-md-12">
                <fieldset>
                    <legend>Thông tin cơ bản</legend>
                    <div class="form-group">
                        <label class="control-label col-md-2">Danh mục cha</label>
                        <div class="col-md-10">
                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control select-chosen"/>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Tiêu đề <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Địa chỉ</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Điện thoại</label>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label class="control-label col-md-2">Hotline</label>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtHotline" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Email</label>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label class="control-label col-md-2">Giờ mở cửa</label>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtParam" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Mã nhúng bản đồ</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtMap" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <uc1:UploadImage runat="server" id="UploadImage" />
                    <div class="form-group">
                        <label class="control-label col-md-2">Ngày tạo</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDate" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Thứ tự</label>
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
                        <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Thêm mới" OnClientClick="unhook();" />
                    </div>
                </div>
                <asp:Button runat="server" ID="btSubmit2" OnClick="btSubmit_OnClick" CssClass="quick-submit btn btn-primary" Text="Save" OnClientClick="unhook();"/>
            </div>
        </div>
    </form>
</div>
<script>
    window.onbeforeunload = function (e) {
        if (hook) {
            //e = e || window.event;
            // For IE and Firefox prior to version 4
            if (e) { e.returnValue = 'Sure?'; }
            // For Safari
            return 'Sure?';
        }
        return null;
    };
</script>
