<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFile.ascx.cs" Inherits="Areas_Admin_Control_Component_UploadFile" %>
<asp:HiddenField ID="hdFile" runat="server" />
<div class="form-group">
    <label class="control-label col-md-2"><asp:Literal runat="server" id="ltrText"/></label>
    <div class="col-md-10">
        <asp:Literal ID="ltimg" runat="server"></asp:Literal>
        <div><asp:LinkButton ID="btnDeleteCurrentFile" runat="server" Visible="false" OnClick="btnDeleteCurrentFile_Click" CssClass="btn btn-xs btn-danger">Xóa tệp hiện tại</asp:LinkButton></div>
        Dịnh dạng hợp lệ: .pdf .xls .xlsx .doc .docx .ppt .pptx .pps .ppsx .zip .rar .7zip .txt .xps .gif .pjp .jpg .pjpeg .jpeg .jfif .png .svgz .svg .bmp .mp4 .webm<br/>
        Dung lượng tối đa 4Mb
        <asp:FileUpload ID="flFile" runat="server" />
        <span class="help-block">Hoặc nhập đường link download tệp đính kèm này</span>
        <asp:TextBox runat="server" ID="txtLink" CssClass="form-control"></asp:TextBox>
    </div>
</div>