<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Information.ascx.cs" Inherits="Areas_Admin_Control_SystemWebsite_Action_Information" %>
<div id="page-content">
    <!-- Forms General Header -->
    <div class="content-header">
        <div class="header-section">
            <h1>
                <i class="gi gi-circle_info"></i>Thông tin website<br>
                <small>Cấu hình thông tin email, hotline, mạng xã hội ...</small>
            </h1>
        </div>
    </div>
    <ul class="breadcrumb breadcrumb-top">
        <li>Hệ thống</li>
        <li>Thông tin website</li>
    </ul>
    <!-- END Forms General Header -->

    <div class="row">
        <div class="col-md-12">
            <!-- Horizontal Form Block -->
            <div class="block">
                <!-- Horizontal Form Content -->
                <form runat="server" class="form-horizontal">
                    <asp:HiddenField ID="hd_img1" runat="server" />
                    <asp:HiddenField ID="hd_img2" runat="server" />
                    <div class="form-group">
                        <label class="control-label col-md-3">Brand name</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtBrand" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Hotline</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtHotline" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Điện thoại</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Email</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group d-none">
                        <label class="control-label col-md-3">Địa chỉ</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtAdd" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group d-none">
                        <label class="control-label col-md-3">Instagram Access Token</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtOpenTime" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Zalo</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtHotline2" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Fanpage Facebook</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtFanpageFb" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Instagram</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtInstagram" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Twitter</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtFanpageTw" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group d-none">
                        <label class="control-label col-md-3">Pinterest</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtPinterest" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Youtube chanel</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtYoutube" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group d-none">
                        <label class="control-label col-md-3">Skype</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtRss" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">LinkedIn</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtLinkedIn" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Bản quyền chân trang</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtCpr" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="control-label col-md-3">Ảnh khi share trang chủ trên mạng xã hội</label>
                        <div class="col-md-9">
                            <div><asp:Literal ID="ltrimgShare" runat="server"></asp:Literal></div>
                            <div><asp:FileUpload ID="flimgShare" runat="server" accept="image/gif, image/jpeg, image/png"/></div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Icon trên thanh địa chỉ trình duyệt (favicon.ico, 16x16 px)</label>
                        <div class="col-md-9">
                            <div><asp:Literal ID="ltrimgFavicon" runat="server"></asp:Literal></div>
                            <div><asp:FileUpload ID="flimgFavicon" runat="server" accept="image/x-icon, image/png"/></div>
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
