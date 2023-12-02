<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditItem.ascx.cs" Inherits="Areas_Admin_Control_Member_Item_AddEditItem" %>
<%@ Register Src="~/Areas/Admin/Control/Component/UploadImage.ascx" TagPrefix="uc1" TagName="UploadImage" %>
<%@ Import Namespace="Developer.Keyword" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li><%=NewsKeyword.News %></li>
        <li><asp:Literal runat="server" id="ltrTitle"/></li>
    </ul>
    <!-- END Forms General Header -->
    <form id="form" runat="server" class="form-horizontal">
        <asp:HiddenField runat="server" ID="HdAccount" />
        <asp:HiddenField ID="HdPassword" runat="server" />
        <asp:HiddenField ID="HdEmail" runat="server" />
        <div class="block row">
            <div class="col-md-12">
                <fieldset>
                    <legend>Thông tin cơ bản</legend>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Tài khoản <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtAccount" ClientIDMode="Static" CssClass="form-control" minlength="5" MaxLength="30" required="required" pattern="[a-z0-9]{1,30}"></asp:TextBox>
                            <div class="text-warning">Lưu ý: Tên tài khoản chỉ nên bao gồm chữ thường và số, độ dài từ 5 đến 30 ký tự</div>
                            <asp:Literal runat="server" id="ltrNote1"></asp:Literal>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Mật khẩu <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtPassword" ClientIDMode="Static" TextMode="Password" CssClass="form-control" minlength="5" autocomplete="new-password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Nhập lại mật khẩu <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtPassword2" ClientIDMode="Static" TextMode="Password" CssClass="form-control" minlength="5"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">First name <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Last name <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <uc1:UploadImage runat="server" id="UploadImage" />
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Địa chỉ <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtAdd" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Điện thoại</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Email</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" CssClass="form-control"></asp:TextBox>
                            <asp:Literal runat="server" id="ltrNote2"></asp:Literal>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Ngày sinh</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtBirthDay" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Ngày tạo</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDate" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
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

    var input = document.getElementById('txtAccount');
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