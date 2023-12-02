<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="Areas_Admin_Control_Product_Config_Index" %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET, Version=3.6.6.2, Culture=neutral, PublicKeyToken=e379cdf2f8354999" %>
<%@ Import Namespace="Developer.Keyword" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li><%=ContactKeyword.Contact %></li>
        <li>Cấu hình</li>
    </ul>
    <!-- END Forms General Header -->
    <form runat="server" class="form-horizontal">
        <div class="block row">
            <div class="col-md-12">
                <fieldset class="d-none">
                    <legend>Nội dung trang liên hệ</legend>
                    <div class="form-group">
                        <div class="col-md-12">
                            <CKEditor:CKEditorControl ID="txt_contentx" runat="server"></CKEditor:CKEditorControl>
                        </div>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>Thông báo hoàn thành gửi liên hệ</legend>
                    <div class="form-group">
                        <div class="col-md-12">
                            <CKEditor:CKEditorControl ID="txt_content" runat="server"></CKEditor:CKEditorControl>
                        </div>
                    </div>
                </fieldset>
                <div class="form-group">
                    <div class="col-md-12">
                        <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Lưu cài đặt" OnClientClick="unhook();" />
                    </div>
                    <asp:Button runat="server" ID="btSubmit2" OnClick="btSubmit_OnClick" CssClass="quick-submit btn btn-primary" Text="Save" OnClientClick="unhook();"/>
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
</script>
