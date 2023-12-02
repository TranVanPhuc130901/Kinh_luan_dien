<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditFilter.ascx.cs" Inherits="Areas_Admin_Control_Tour_Filter_AddEditFilter" %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET, Version=3.6.6.2, Culture=neutral, PublicKeyToken=e379cdf2f8354999" %>
<%@ Register Src="~/Areas/Admin/Control/Component/UploadImage.ascx" TagPrefix="uc1" TagName="UploadImage" %>
<%@ Import Namespace="Developer.Keyword" %>
<div id="page-content">
    <ul class="breadcrumb breadcrumb-top">
        <li><%=TourKeyword.Tour %></li>
        <li><asp:Literal runat="server" id="ltrTitle"/></li>
    </ul>
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
                        <label class="control-label col-md-2">Tên danh mục <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Mô tả <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" CssClass="form-control" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group d-none">
                        <label class="control-label col-md-2">Loại thuộc tính</label>
                        <div class="col-md-10">
                            <asp:DropDownList runat="server" ID="ddlType" CssClass="form-control">
                                <asp:ListItem Value="1" Text="Chọn nhiều thuộc tính / sản phẩm"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Chọn 1 thuộc tính / sản phẩm"></asp:ListItem>
                            </asp:DropDownList>
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
                        <label class="control-label col-md-2">Thứ tự</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtOrder" TextMode="Number" Text="1" min="0" CssClass="not-reset form-control" Width="80"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Trạng thái</label>
                        <div class="col-md-10">
                            <label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbStatus" Checked="True" /><span></span></label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Tiếp tục tạo mục khác</label>
                        <div class="col-md-10">
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
