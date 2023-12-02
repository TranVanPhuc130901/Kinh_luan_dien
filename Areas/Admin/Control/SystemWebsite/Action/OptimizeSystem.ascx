<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OptimizeSystem.ascx.cs" Inherits="Areas_Admin_Control_SystemWebsite_Action_OptimizeSystem" %>
<div id="page-content">
    <!-- Forms General Header -->
    <div class="content-header">
        <div class="header-section">
            <h1>
                <i class="gi gi-embed_close"></i>Tối ưu công cụ tìm kiếm, thống kê<br>
                <small>Cấu hình phục vụ SEO, nhúng mã</small>
            </h1>
        </div>
    </div>
    <ul class="breadcrumb breadcrumb-top">
        <li>Hệ thống</li>
        <li>Tối ưu công cụ tìm kiếm</li>
    </ul>
    <!-- END Forms General Header -->

    <div class="row">
        <div class="col-md-12">
            <!-- Horizontal Form Block -->
            <div class="block">
                <!-- Horizontal Form Content -->
                <form runat="server" class="form-horizontal">
                    <div class="form-group">
                        <label class="control-label col-md-2">Thẻ meta title</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="textTagTitle" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Thẻ meta keywords</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="textTagKeyword" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Thẻ meta description</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="textTagDescription" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Mã nhúng thẻ head</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtHead" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Mã nhúng đầu thẻ body</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtBody" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Mã nhúng cuối thẻ body</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtBodyBottom" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group form-actions">
                        <div class="col-md-9 col-md-offset-3">
                            <asp:Button runat="server" id="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Lưu"/>
                        </div>
                    </div>
                </form>
                <!-- END Horizontal Form Content -->
            </div>
            <!-- END Horizontal Form Block -->
        </div>
    </div>
</div>