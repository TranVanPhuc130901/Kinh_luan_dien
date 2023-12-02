<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Sitemap.ascx.cs" Inherits="Areas_Admin_Control_SystemWebsite_Action_Sitemap" %>
<div id="page-content">
    <!-- Forms General Header -->
    <div class="content-header">
        <div class="header-section">
            <h1>
                <i class="fi fi-xml"></i>Tạo sitemap.xml và robots.txt<br>
                <small>Cần tạo sitemap sau khi có bài viết mới.</small>
            </h1>
        </div>
    </div>
    <ul class="breadcrumb breadcrumb-top">
        <li>Hệ thống</li>
        <li>Tạo sitemap.xml và robots.txt</li>
    </ul>
    <!-- END Forms General Header -->

    <div class="row">
        <div class="col-md-6 col-md-offset-3">
            <!-- Horizontal Form Block -->
            <div class="block">
                <!-- Horizontal Form Content -->
                <form runat="server" class="form-horizontal">
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:CheckBox runat="server" id="cbSitemap" ClientIDMode="Static" Checked="True"/>
                            <label for="cbSitemap">Tạo file sitemap.xml</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:CheckBox runat="server" id="cbRobots" ClientIDMode="Static" Checked="False" AutoPostBack="True" OnCheckedChanged="cbRobots_OnCheckedChanged"/>
                            <label for="cbRobots">Tạo file robots.txt</label>
                        </div>
                    </div>
                    <asp:Panel runat="server" id="pnRobots" Visible="False">
                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:DropDownList runat="server" id="ddlIndex" ClientIDMode="Static" required="">
                                    <asp:ListItem Value="0" Text="Chặn index"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Cho phép index"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="form-group form-actions">
                        <div class="col-md-12">
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
