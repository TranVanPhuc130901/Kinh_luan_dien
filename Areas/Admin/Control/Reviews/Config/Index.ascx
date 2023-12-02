<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="Areas_Admin_Control_Reviews_Config_Index" %>
<%@ Import Namespace="Developer.Keyword" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li><%=ReviewsKeyword.Reviews %></li>
        <li>Cấu hình</li>
    </ul>
    <!-- END Forms General Header -->
    <form runat="server" class="form-horizontal">
        <div class="block row">
            <div class="col-md-4">
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
                    <legend>Cấu hình hình ảnh</legend>
                    <div class="form-group">
                        <label class="col-md-8 control-label">Giới hạn kích thước ảnh</label>
                        <div class="col-md-4">
                            <label class="switch switch-primary">
                                <asp:CheckBox ID="cbHanCheKichThuoc" runat="server"></asp:CheckBox><span></span></label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-8 control-label">Chiều rộng (pixel)</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="tbHanCheW" class="form-control" runat="server" TextMode="Number" min="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-8 control-label">Chiều cao (pixel)</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="tbHanCheH" class="form-control" runat="server" TextMode="Number" min="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-8 control-label">Tạo ảnh nhỏ cho ảnh đại diện</label>
                        <div class="col-md-4">
                            <label class="switch switch-primary">
                                <asp:CheckBox ID="cbTaoAnhNho" runat="server"></asp:CheckBox><span></span></label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-8 control-label">Chiều rộng (pixel)</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="tbAnhNhoW" class="form-control" runat="server" TextMode="Number" min="0"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-8 control-label">Chiều cao (pixel)</label>
                        <div class="col-md-4">
                            <asp:TextBox ID="tbAnhNhoH" class="form-control" runat="server" TextMode="Number" min="0"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="col-md-8">
                <fieldset>
                    <legend>Tối ưu cho công cụ tìm kiếm</legend>
                    <div class="form-group count-this">
                        <label class="col-md-3 control-label">Meta title <span></span></label>
                        <div class="col-md-9">
                            <asp:TextBox type="text" ID="txtMetaTitle" ClientIDMode="Static" CssClass="form-control" runat="server" placeholder="Giới hạn từ 50 đến 65 ký tự"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-3 control-label">Meta keyword <span></span></label>
                        <div class="col-md-9">
                            <asp:TextBox type="text" ID="txtMetaKeyword" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-3 control-label">Meta description <span></span></label>
                        <div class="col-md-9">
                            <asp:TextBox type="text" ID="txtMetaDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5" placeholder="Giới hạn từ 130 đến 160 ký tự"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-9 col-md-offset-3">
                            <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Lưu cài đặt" OnClientClick="unhook();" />
                        </div>
                    </div>
                    
                </fieldset>
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
