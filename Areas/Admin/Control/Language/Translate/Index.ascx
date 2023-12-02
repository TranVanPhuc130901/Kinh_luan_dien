<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Index.ascx.cs" Inherits="Areas_Admin_Control_Language_Translate_Index" %>
<%@ Import Namespace="Developer.Keyword" %>
<%@ Import Namespace="RevosJsc.Columns" %>
<style>
    .cot2 { width: calc((100% - 150px) / 2) }
    .cot7 { width: 150px }
</style>
<div id="page-content">
    <ul class="breadcrumb breadcrumb-top">
        <li><%=LanguageKeyword.Language %></li>
        <li><%=LanguageKeyword.DichTuKhoa %></li>
    </ul>
    <form runat="server" class="block full">
        <div class="block-title pt10 pb10 pl20 pr20">
            <a href="<%=RevosJsc.LanguageControl.Link.LnkMnKeywordCreate() %>" title="Add"><i class="fa fa-plus"></i> <%=LanguageKeyword.ThemMoiTuKhoa %></a>
        </div>
        <div class="form-horizontal form-bordered">
            <div class="form-group">
                <div class="col-md-5 pt7">TỪ KHÓA</div>
                <div class="col-md-6">DỊCH</div>
                <div class="col-md-1 text-center">CÔNG CỤ</div>
            </div>
            <asp:Repeater ID="RpListLanguageNationals" runat="server" OnItemCommand="RpListLanguageNationals_ItemCommand">
                <ItemTemplate>
                    <div class="form-group">
                        <label class="col-md-5 pt7"><%#Eval(KeywordsColumns.VkTitle).ToString() %></label>
                        <div class="col-md-6"><asp:TextBox ID="txtTranslate" runat="server" ToolTip="<%#Eval(KeywordsColumns.IkId).ToString() %>" Text="<%#TranslateKeyword(Eval(KeywordsColumns.IkId).ToString()) %>" CssClass="form-control"></asp:TextBox></div>
                        <label class="col-md-1 text-center"><asp:LinkButton ID="LnkUpdate" CssClass="btn btn-success" runat="server" CommandName="edit" CommandArgument='<%#Eval(KeywordsColumns.IkId) + "," + Eval(KeywordsColumns.VkTitle) %>' ToolTip="Click vào để thay đổi từ khóa này">Save</asp:LinkButton></label>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="form-group">
                <div class="col-md-6 col-md-offset-5">
                    <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Save all" OnClientClick="unhook();" />
                </div>
            </div>
            <asp:Button runat="server" ID="btSubmit2" OnClick="btSubmit_OnClick" CssClass="quick-submit btn btn-primary" Text="Save all" OnClientClick="unhook();"/>
        </div>
    </form>
</div>
