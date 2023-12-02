<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditUser.ascx.cs" Inherits="Areas_Admin_Control_Users_Control_AddEditUser" %>
<%@ Import Namespace="RevosJsc.UsersControl" %>
<%@ Register Src="~/Areas/Admin/Control/Component/UploadImageUsers.ascx" TagPrefix="uc1" TagName="UploadImageUsers" %>
<div id="page-content">
    <ul class="breadcrumb breadcrumb-top">
        <li>Tài khoản quản trị</li>
        <li>Thêm mới tài khoản</li>
    </ul>
    <form runat="server" id="f_users" class="row">
        <div class="col-md-12">
            <div class="block row">
                <div class="form-horizontal col-md-6">
                    <asp:HiddenField runat="server" id="hdUsername"/>
                    <asp:HiddenField runat="server" id="hdId"/>
                    <asp:HiddenField runat="server" id="hdRole"/>
                    <asp:HiddenField runat="server" id="hdExpiration"/>
                    <div class="form-group">
                        <label class="control-label col-md-3">Tên tài khoản</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtUsername" ClientIDMode="Static" CssClass="form-control" minlength="5" MaxLength="30" required="required" pattern="[a-z0-9]{5,30}"></asp:TextBox>
                            <div class="text-warning">Lưu ý: Tên tài khoản chỉ nên bao gồm chữ thường và số, độ dài từ 5 đến 30 ký tự</div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Mật khẩu</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtPassword" ClientIDMode="Static" CssClass="form-control" TextMode="Password" minlength="5" autocomplete="new-password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Nhập lại mật khẩu</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtPassword2" ClientIDMode="Static" CssClass="form-control" TextMode="Password" minlength="5"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Họ</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Tên</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Email</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Điện thoại</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Địa chỉ</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtAdd" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Ngày tạo</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtDate" CssClass="form-control" Enabled="False"></asp:TextBox>
                        </div>
                    </div>
                    
                </div>
                <div class="form-horizontal col-md-6">
                    <uc1:UploadImageUsers runat="server" ID="UploadImageUsers" />
                    <div class="form-group">
                        <label class="control-label col-md-3">Ghi chú thêm</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" CssClass="form-control" Rows="8"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Phân quyền</label>
                        <div class="col-md-9">
                            <div class="wrap-control">
                                <button type="button" class="btn btn-sm btn-primary" data-toggle="modal" data-target="#modal-user-settings">
                                    Mở bảng phân quyền
                                </button>
                            </div>
                        </div>
                    </div>
                    <div id="modal-user-settings" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <!-- Modal Body -->
                                <div class="modal-body">
                                    <label class="cursor-pointer"><input id="checkAll" type="checkbox" onchange="checkAllCheckBox('cb',this)"/>Chọn/Bỏ chọn tất cả</label>
                                    <asp:CheckBoxList runat="server" id="cblRole"/>
                                </div>
                                <div class="form-group form-actions">
                                    <div class="col-xs-12 text-center">
                                        <button type="button" class="btn btn-sm btn-primary" data-dismiss="modal">Xong</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-3">Trạng thái</label>
                        <div class="col-md-3">
                            <label class="switch switch-primary"><asp:CheckBox runat="server" ID="chkStatus" Checked="True" /><span></span></label>
                        </div>
                        <label class="control-label col-md-3">Tiếp tục tạo mục khác</label>
                        <div class="col-md-3">
                            <label class="switch switch-primary"><asp:CheckBox runat="server" ID="chkContiue" /><span></span></label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-9 col-md-offset-3">
                            <a href="<%=Link.LnkUsers() %>" class="btn btn-warning">Hủy</a>
                            <asp:Button runat="server" id="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Thêm mới" OnClientClick="unhook();"/>
                        </div>
                    </div>
                </div>
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
    var input = document.getElementById('txtUsername');
    input.oninvalid = function (event) {
        event.target.setCustomValidity('Tên tài khoản chỉ nên bao gồm chữ thường và số, độ dài từ 5 đến 30 ký tự');
    }
    var password = document.getElementById("txtPassword")
        , confirm_password = document.getElementById("txtPassword2");

    function validatePassword() {
        if (password.value != confirm_password.value) {
            confirm_password.setCustomValidity("Nhập lại mật khẩu không chính xác");
        } else {
            confirm_password.setCustomValidity('');
        }
    }
    password.onchange = validatePassword;
    confirm_password.onkeyup = validatePassword;
</script>