<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditCategory.ascx.cs" Inherits="Areas_Admin_Control_Product_Color_AddEditCategory" %>
<%@ Import Namespace="Developer.Keyword" %>
<%@ Register Src="~/Areas/Admin/Control/Component/UploadImage.ascx" TagPrefix="uc1" TagName="UploadImage" %>

<div id="page-content">
    <ul class="breadcrumb breadcrumb-top">
        <li><%=ProductKeyword.Product %></li>
        <li><asp:Literal runat="server" id="ltrTitle"/></li>
    </ul>
    <form id="form" runat="server" class="form-horizontal">
        <asp:HiddenField runat="server" ID="HdParentId" />
        <asp:HiddenField runat="server" ID="HdTitle" />
        <asp:HiddenField ID="HdOldContent" runat="server" />
        <asp:HiddenField ID="HdOldContent2" runat="server" />
        <asp:HiddenField ID="HdTotalView" runat="server" />
        <div class="block row">
            <div class="col-md-12">
                <fieldset>
                    <legend>Thông tin cơ bản</legend>
                    <div class="form-group d-none">
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
                    <uc1:UploadImage runat="server" ID="UploadImage" Visible="False" />
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Mã màu <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtDesc" TextMode="Color" required></asp:TextBox> (Click vào màu để thay đổi màu sắc)
                        </div>
                    </div>
                    <div class="form-group d-none">
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

                <fieldset class="d-none">
                    <legend>Tối ưu cho công cụ tìm kiếm</legend>
                    <div class="form-group count-this">
                        <label class="col-md-2 control-label">URL <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtUrl" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-2 control-label">Meta title <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtMetaTitle" ClientIDMode="Static" CssClass="form-control" runat="server" placeholder="Giới hạn từ 50 đến 65 ký tự"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-2 control-label">Meta keyword <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtMetaKeyword" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-2 control-label">Meta description <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtMetaDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5" placeholder="Giới hạn từ 130 đến 160 ký tự"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-2 control-label">Tag <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtTag" CssClass="form-control input-tags" placeholder="Add tag" runat="server"></asp:TextBox>
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
