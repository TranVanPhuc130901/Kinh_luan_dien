<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImportExcel.ascx.cs" Inherits="Areas_Admin_Control_Redirect_Tool_ImportExcel" %>
<div id="page-content">
    <!-- Forms General Header -->
    <div class="content-header">
        <div class="header-section">
            <h1>
                <i class="fi fi-xls"></i>Redirect 301<br>
                <small>Chuyển hướng link</small>
            </h1>
        </div>
    </div>
    <ul class="breadcrumb breadcrumb-top">
        <li>Chuyển hướng 301</li>
        <li>Import từ excel</li>
    </ul>
    <!-- END Forms General Header -->
    <div class="row">
        <div class="col-md-12">
            <!-- Horizontal Form Block -->
            <div class="block">
                <form runat="server" class="form-horizontal">
                    <div class="form-group">
                        <div class="col-md-6 col-md-offset-3">
                            Tải tệp Excel mẫu tại <a href="/Areas/Admin/Control/Redirects/Tool/redirect301.xlsx" title="Tải file mẫu">đây</a>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6 col-md-offset-3 text-warning">
                            Lưu ý: Những link chuyển hướng đã tồn tại trùng với link trong bảng Excel sẽ được update link đích theo bảng Excel.
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-6 col-md-offset-3">
                            <asp:FileUpload ID="fuExcel" runat="server" Width="222px" accept="application/vnd.ms-excel,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"/>
                        </div>
                    </div>
                    <div class="form-group form-actions">
                        <div class="col-md-6 col-md-offset-3">
                            <asp:Button runat="server" id="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Import"/>
                        </div>
                    </div>
                </form>
                
            </div>
        </div>
    </div>
</div>