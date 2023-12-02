<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadImage.ascx.cs" Inherits="Areas_Admin_Control_Component_UploadImage" %>
<asp:HiddenField ID="hdImage" runat="server" />
<div class="form-group">
    <label class="control-label col-md-2"><asp:Literal runat="server" id="ltrText"/></label>
    <div class="col-md-10">
        <asp:Literal ID="ltimg" runat="server"></asp:Literal>
        <div><asp:LinkButton ID="btnDeleteCurrentImage" runat="server" Visible="false" OnClick="btnDeleteCurrentImage_Click" CssClass="btn btn-xs btn-danger">Xóa hình ảnh hiện tại</asp:LinkButton></div>
        <asp:FileUpload ID="flimg" runat="server" CssClass="flimg" accept="image/gif, image/jpeg, image/png, image/svg+xml" />
        <asp:Panel runat="server" id="pnLayAnhTuNoiDung" CssClass="checkbox">
            <asp:CheckBox ID="cbLayAnhTuNoiDung" runat="server" Checked="True" Text="Lấy ảnh đầu tiên trong nội dung" />
        </asp:Panel>
        <div class="row">
            <asp:Panel runat="server" id="pnHanCheKichThuoc" CssClass="col-md-6" Visible="false">
                <div class="checkbox">
                    <asp:CheckBox ID="cbHanCheKichThuoc" runat="server" Text="Hạn chế kích thước ảnh" />
                </div>
                <div class="mt5">
                    width
                    <asp:TextBox ID="tbHanCheW" Width="80" runat="server" ToolTip="Chiều rộng lớn nhất có thể của ảnh đại diện, nếu ảnh có kích thước lớn hơn nó sẽ tự co lại" CssClass="form-control d-inline-block text-center" TextMode="Number" min="0"></asp:TextBox>&nbsp;px&nbsp;&nbsp;&nbsp;
                    height
                    <asp:TextBox ID="tbHanCheH" Width="80" runat="server" ToolTip="Chiều cao lớn nhất có thể của ảnh đại diện, nếu ảnh có kích thước lớn hơn nó sẽ tự co lại" CssClass="form-control d-inline-block text-center" TextMode="Number" min="0"></asp:TextBox>&nbsp;px
                </div>
            </asp:Panel>
            <asp:Panel runat="server" id="pnTaoAnhNho" CssClass="col-md-6" Visible="false">
                <div class="checkbox">
                    <asp:CheckBox ID="cbTaoAnhNho" runat="server" Text="Tạo ảnh nhỏ" />
                </div>
                <div class="mt5">
                    width
                    <asp:TextBox ID="tbAnhNhoW" Width="80" runat="server" ToolTip="Chiều rộng của ảnh nhỏ. Ảnh nhỏ dùng để hiển thị thay thế cho ảnh đại diện nhằm giảm tải dữ liệu phải tải về máy khách khi hiển thị" CssClass="form-control d-inline-block text-center" TextMode="Number" min="0"></asp:TextBox>&nbsp;px&nbsp;&nbsp;&nbsp;
                    height
                    <asp:TextBox ID="tbAnhNhoH" Width="80" runat="server" ToolTip="Chiều cao của ảnh nhỏ. Ảnh nhỏ dùng để hiển thị thay thế cho ảnh đại diện nhằm giảm tải dữ liệu phải tải về máy khách khi hiển thị" CssClass="form-control d-inline-block text-center" TextMode="Number" min="0"></asp:TextBox>&nbsp;px
                </div>
            </asp:Panel>
        </div>
    </div>
</div>