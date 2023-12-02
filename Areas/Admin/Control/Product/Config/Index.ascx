<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="Areas_Admin_Control_Product_Config_Index" %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET, Version=3.6.6.2, Culture=neutral, PublicKeyToken=e379cdf2f8354999" %>
<%@ Import Namespace="Developer.Keyword" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li><%=ProductKeyword.Product %></li>
        <li>Cấu hình</li>
    </ul>
    <!-- END Forms General Header -->
    <form runat="server" class="form-horizontal">
        <div class="block row">
            <div class="col-md-6">
                <fieldset>
                    <legend>Cấu hình số lượng</legend>
                    <div class="form-group">
                        <label class="col-md-8 control-label">Số bài trên trang chính</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtIndex" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-8 control-label">Số bài trên trang danh mục</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtCategory" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-8 control-label">Số bài khác trên 1 trang</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtOther" class="form-control" runat="server" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>Nội dung đầu trang sản phẩm</legend>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:TextBox ID="txt_content4" CssClass="form-control" runat="server" placeholder="Tiêu đề"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <CKEditor:CKEditorControl ID="txt_content5" Toolbar="Basic" Height="150" runat="server" placeholder="Nội dung"></CKEditor:CKEditorControl>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-6">
                <fieldset>
                    <legend>Tối ưu cho công cụ tìm kiếm</legend>
                    <div class="form-group count-this">
                        <label class="col-md-3 control-label">Meta title <span></span></label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtMetaTitle" ClientIDMode="Static" CssClass="form-control" runat="server" placeholder="Giới hạn từ 50 đến 65 ký tự"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-3 control-label">Meta keyword <span></span></label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtMetaKeyword" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-3 control-label">Meta description <span></span></label>
                        <div class="col-md-9">
                            <asp:TextBox ID="txtMetaDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="1" placeholder="Giới hạn từ 130 đến 160 ký tự"></asp:TextBox>
                        </div>
                    </div>
                    
                </fieldset>
                
                <fieldset>
                    <legend>Nội dung trang hoàn thành đặt mua</legend>
                    <div class="form-group">
                        <div class="col-md-12">
                            <CKEditor:CKEditorControl ID="txt_content3" Height="150" runat="server"></CKEditor:CKEditorControl>
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
    function HienThiGia(idTextBoxGia, idHienThi) {
        var gia = idTextBoxGia.value;
        gia = DinhDangGia(gia);
        document.getElementById(idHienThi).innerHTML = gia;
    }
    function DinhDangGia(number) {
        if (isNaN(number)) return "<span class='text-danger'>Giá nhập sai định dạng!</span>";
        var str = new String(number);

        var indexOfdot = str.indexOf(".", 0);
        var phanThapPhan;
        if (indexOfdot > -1) {
            phanThapPhan = "," + str.substring(indexOfdot + 1, len);
            str = str.substring(0, indexOfdot);
        }

        var result = "", len = str.length;
        for (var i = len - 1; i >= 0; i--) {
            if ((i + 1) % 3 == 0 && i + 1 != len) result += ".";
            result += str.charAt(len - 1 - i);
        }

        if (indexOfdot > -1)
            result += phanThapPhan;

        return result;
    }
</script>
