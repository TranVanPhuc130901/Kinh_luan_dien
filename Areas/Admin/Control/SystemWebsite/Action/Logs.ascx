<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Logs.ascx.cs" Inherits="Areas_Admin_Control_SystemWebsite_Action_Logs" %>
<div id="page-content">
    <!-- Forms General Header -->
    <div class="content-header">
        <div class="header-section">
            <h1>
                <i class="fi fi-log"></i>Logs<br>
                <small>Nhật ký hệ thống</small>
            </h1>
        </div>
    </div>
    <ul class="breadcrumb breadcrumb-top">
        <li>Hệ thống</li>
        <li>Logs</li>
    </ul>
    <!-- END Forms General Header -->
    <div class="block full">
        <div id="ecom-products_wrapper" class="dataTables_wrapper form-inline no-footer">
            <div class="row">
                <asp:Literal runat="server" ID="ltrPaging2" />
            </div>
            <table id="ecom-products" class="table table-bordered table-striped table-vcenter dataTable no-footer" role="grid" aria-describedby="ecom-products_info">
                <asp:Literal runat="server" ID="ltrList" />
            </table>
            <div class="row">
                <asp:Literal runat="server" ID="ltrPaging" />
            </div>
        </div>
    </div>
    
</div>