<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditLink.ascx.cs" Inherits="Areas_Admin_Control_Redirect_Link_AddEditLink" %>
<%@ Import Namespace="RevosJsc.RedirectsControl" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li>Chuyển hướng 301</li>
        <li>Thêm mới link</li>
    </ul>
    <!-- END Forms General Header -->

    <div class="row">
        <div class="col-md-12">
            <!-- Horizontal Form Block -->
            <div class="block">
<%--                <div class="block-title">
                    <h2>Thêm mới <strong>link</strong></h2>
                </div>--%>
                <!-- Horizontal Form Content -->
                <form runat="server" id="f_redirect" class="form-horizontal">
                    <asp:HiddenField runat="server" id="hdOldLink"/>
                    <div class="form-group">
                        <label class="control-label col-md-2">Link chuyển hướng</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtLink" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Link đích</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtLinkDestination" CssClass="form-control" required="required"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Ngày tạo</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDate" CssClass="form-control" Enabled="False"></asp:TextBox>
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
                    <div class="form-group form-actions">
                        <div class="col-md-10 col-md-offset-2">
                            <a href="<%=Link.LnkRedirect() %>" class="btn btn-warning">Hủy</a>
                            <asp:Button runat="server" id="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Thêm mới"/>
                        </div>
                    </div>
                </form>
                <!-- END Horizontal Form Content -->
            </div>
            <!-- END Horizontal Form Block -->
        </div>
    </div>
</div>
