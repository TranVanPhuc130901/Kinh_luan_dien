<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Email.ascx.cs" Inherits="Areas_Admin_Control_SystemWebsite_Action_Email" %>
<div id="page-content">
    <!-- Forms General Header -->
    <div class="content-header">
        <div class="header-section">
            <h1>
                <i class="gi gi-envelope"></i>Email hệ thống<br>
                <small>Cấu hình email gửi và nhận thông báo</small>
            </h1>
        </div>
    </div>
    <ul class="breadcrumb breadcrumb-top">
        <li>Hệ thống</li>
        <li>Email hệ thống</li>
    </ul>
    <!-- END Forms General Header -->

    <div class="row">
        <div class="col-md-12">
            <!-- Horizontal Form Block -->
            <div class="block">
                <!-- Horizontal Form Content -->
                <form runat="server" class="form-horizontal">
                    <asp:HiddenField runat="server" id="hdOldPass"/>
                    <div class="form-group">
                        <div class="col-md-9 col-sm-offset-3">Email hệ thống được dùng để gửi các thông tin từ hệ thống tới khách hoặc quản trị viên.</div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-9 col-sm-offset-3 text-danger">Lưu ý: Không nên dùng tài khoản quan trọng của bạn làm email hệ thống vì có thể bị Google đưa vào dạng spam.</div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label" for="tbEmail">Email hệ thống</label>
                        <div class="col-md-7">
                            <asp:TextBox id="tbEmail" ClientIDMode="Static" TextMode="Email" CssClass="form-control" placeholder="Nhập email.." runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label" for="tbPassword">Mật khẩu email hệ thống</label>
                        <div class="col-md-7">
                            <asp:TextBox id="tbPassword" ClientIDMode="Static" TextMode="Password" CssClass="form-control" placeholder="Nhập mật khẩu.." runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-9 col-sm-offset-3">Email nhận thông báo từ hệ thống, mỗi email ngăn cách bởi dấu phẩy (,) và không có khoảng trống.</div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label" for="tbEmailOther">Danh sách email nhận thông báo</label>
                        <div class="col-md-7">
                            <asp:TextBox id="tbEmailOther" ClientIDMode="Static" TextMode="MultiLine" Rows="5" CssClass="form-control input-tags" placeholder="Add email" runat="server"></asp:TextBox>
                            <div class="text-danger"><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Danh sách email nhận thông báo không hợp lệ!" ControlToValidate="tbEmailOther" Display="Dynamic" SetFocusOnError="True" ValidationExpression="(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*(,)?)*"></asp:RegularExpressionValidator></div>
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
