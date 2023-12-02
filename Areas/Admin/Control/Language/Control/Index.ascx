<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="Areas_Admin_Control_Language_Control_Index" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li>Quản lý ngôn ngữ</li>
        <li>Danh sách ngôn ngữ</li>
    </ul>
    <!-- END Forms General Header -->
    <form runat="server" class="block full">
        <div class="block-title pt10 pb10 pl20 pr20">
            <a href="<%=RevosJsc.LanguageControl.Link.LnkMnLanguageNationalCreate() %>" title="Add"><i class="fa fa-plus"></i> Thêm mới ngôn ngữ</a>
        </div>

        <div id="ecom-products_wrapper" class="dataTables_wrapper form-inline table-responsive">
            <table id="ecom-products" class="table table-bordered table-striped table-vcenter dataTable no-footer" role="grid" aria-describedby="ecom-products_info">
                <thead>
                    <tr role="row">
                        <th class="text-center"><input id="checkAll" type="checkbox" onchange="checkAllCheckBox('cb',this)" class="cursor-pointer"/></th>
                        <th class="text-center">Ngôn ngữ</th>
                        <th class="text-center">Số thứ tự</th>
                        <th class="text-center">Trạng thái</th>
                        <th class="text-center">Công cụ</th>
                    </tr>
                </thead>
                <asp:Literal runat="server" ID="ltrList" />
            </table>
            <div class="row">
                <asp:Literal runat="server" ID="ltrPaging" />
            </div>
        </div>
    </form>
</div>
