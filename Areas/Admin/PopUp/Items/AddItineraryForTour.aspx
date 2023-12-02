<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddItineraryForTour.aspx.cs" Inherits="Areas_Admin_PopUp_Items_AddItineraryForTour" %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET, Version=3.6.6.2, Culture=neutral, PublicKeyToken=e379cdf2f8354999" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal runat="server" id="ltrTitle"></asp:Literal></title>
    <meta name="description" content="Revos CMS 1.1" />
    <meta name="author" content="Revos JSC" />
    <meta name="robots" content="noindex, nofollow" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,user-scalable=0" />
    <link rel="shortcut icon" href="/Areas/Admin/img/favicon.png" />
    <!-- Stylesheets -->
    <link rel="stylesheet" href="/Areas/Admin/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/Areas/Admin/css/plugins.css" />
    <link rel="stylesheet" href="/Areas/Admin/css/main.css" />
    <link rel="stylesheet" href="/Areas/Admin/css/themes.css" />
    <!-- END Stylesheets -->
</head>
<body>
    <div id="page-content">
        <!-- Forms General Header -->
        <!-- END Forms General Header -->
        <form id="form" runat="server" class="form-horizontal">
            <asp:HiddenField runat="server" ID="hdImage" />
            <div class="block row">
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Thêm mới lịch trình</legend>
                        <div class="form-group">
                            <label class="control-label col-md-2">Tiêu đề</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-md-2">Ngày</label>
                            <div class="col-md-10">
                                <asp:TextBox runat="server" ID="txtDate" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 col-sm-4 text-left">Thứ tự</label>
                            <div class="col-md-10 col-sm-8">
                                <asp:TextBox runat="server" ID="txtOrder" TextMode="Number" Text="1" min="0" CssClass="not-reset form-control" Width="80"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 col-sm-4 text-left">Trạng thái</label>
                            <div class="col-md-10 col-sm-8">
                                <label class="switch switch-primary">
                                    <asp:CheckBox runat="server" ID="cbStatus" Checked="True" /><span></span></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-2 col-sm-4 text-left">Tiếp tục tạo mục khác</label>
                            <div class="col-md-10 col-sm-8">
                                <label class="switch switch-primary">
                                    <asp:CheckBox runat="server" ID="cbContiue" /><span></span></label>
                            </div>
                        </div>
                    </fieldset>                
                    <fieldset>
                        <legend>Nội dung chi tiết</legend>
                        <div class="form-group">
                            <div class="col-md-12">
                                <CKEditor:CKEditorControl ID="txt_content" runat="server" FilebrowserImageBrowseUrl="ckeditor/ckfinder/ckfinder.aspx?type=Images"></CKEditor:CKEditorControl>
                            </div>
                        </div>
                    </fieldset>
                    <div class="form-group">
                        <div class="col-md-10 col-md-offset-2 text-right">
                            <a runat="server" id="btnCancel" class="btn btn-default">Hủy</a>
                            <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Thêm mới" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <fieldset>
                        <legend>Danh sách lịch trình</legend>
                        <asp:Literal runat="server" id="ltrList"/>
                    </fieldset>
                    <asp:Literal runat="server" ID="ltrPaging" />
                </div>
            </div>
        </form>
    </div>
    <script src="/Areas/Admin/js/vendor/jquery.min.js"></script>
    <script src="/Areas/Admin/js/vendor/bootstrap.min.js"></script>
    <script src="/Areas/Admin/js/vendor/bootbox.min.js"></script>
    <script src="/Areas/Admin/js/plugins.js"></script>
    <script src="/Areas/Admin/js/app.js"></script>
    <script src="/Areas/Admin/Ajax/Items/js.js"></script>
</body>
</html>
