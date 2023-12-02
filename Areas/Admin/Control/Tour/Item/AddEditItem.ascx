<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditItem.ascx.cs" Inherits="Areas_Admin_Control_Tour_Item_AddEditItem" %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET, Version=3.6.6.2, Culture=neutral, PublicKeyToken=e379cdf2f8354999" %>
<%@ Register Src="~/Areas/Admin/Control/Component/UploadImage.ascx" TagPrefix="uc1" TagName="UploadImage" %>
<%@ Register Src="~/Areas/Admin/Control/Component/UploadFile.ascx" TagPrefix="uc1" TagName="UploadFile" %>

<%@ Import Namespace="Developer.Keyword" %>
<%@ Import Namespace="RevosJsc.Columns" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li><%=TourKeyword.Tour %></li>
        <li><asp:Literal runat="server" id="ltrTitle"/></li>
    </ul>
    <!-- END Forms General Header -->
    <form id="form" runat="server" class="form-horizontal">
        <asp:HiddenField runat="server" ID="HdOldIgId" />
        <asp:HiddenField runat="server" ID="HdTitle" />
        <asp:HiddenField ID="HdOldContent" runat="server" />
        <asp:HiddenField ID="HdTotalView" runat="server" />
        <asp:HiddenField ID="HdOldHotel" runat="server" />
        <asp:HiddenField ID="HdOldCruises" runat="server" />
        <asp:HiddenField ID="HdOldVehicles" runat="server" />
        <div class="block row">
            <div class="col-md-12">
                <fieldset>
                    <legend>Thông tin cơ bản</legend>
                    <div class="form-group">
                        <label class="control-label col-md-2">Danh mục cha</label>
                        <div class="col-md-10">
                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control select-chosen" required/>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Tên bài viết <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtTitle" CssClass="form-control" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Mô tả <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtDesc" TextMode="MultiLine" CssClass="form-control" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-2 control-label">Điểm đến sẽ qua</label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtPlace" CssClass="form-control input-tags" placeholder="Add" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Link video Youtube</label>
                        <div class="col-md-10">
                            Bạn có thể nhập link video: <code>https://www.youtube.com/watch?v=wJnBTPUQS5A</code> <br/>
                            hoặc link rút gọn: <code>https://youtu.be/wJnBTPUQS5A</code> <br/>
                            hoặc mã video: <code>wJnBTPUQS5A</code> <br/>
                            hoặc mã nhúng: <code>&lt;iframe width="560" height="315" src="http://www.youtube.com/embed/wJnBTPUQS5A" frameborder="0" allowfullscreen&gt;&lt;/iframe&gt;</code>
                            <asp:TextBox runat="server" ID="txtEmbed" TextMode="MultiLine" CssClass="form-control" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    <uc1:UploadImage runat="server" id="UploadImage" />
                    <uc1:UploadImage runat="server" ID="UploadImage1" />
                    <uc1:UploadImage runat="server" ID="UploadImage2" />
                    <uc1:UploadFile runat="server" ID="UploadFile" />
                    <div class="form-group">
                        <label class="control-label col-md-2">Giá từ</label>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtPriceOld" TextMode="Number" CssClass="form-control" onkeyup="HienThiGia(this,'giaNY');"></asp:TextBox>
                                <div id="giaNY" class="input-group-addon"><asp:Literal runat="server" id="ltrPriceOld"/></div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group d-none">
                        <label class="control-label col-md-2">Giá khuyến mại</label>
                        <div class="col-md-3">
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtPriceNew" TextMode="Number" CssClass="form-control" onkeyup="HienThiGia(this,'giaBan');"></asp:TextBox>
                                <div id="giaBan" class="input-group-addon"><asp:Literal runat="server" id="ltrPriceNew"/></div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Mô tả giá <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtPriceDescription" CssClass="form-control" placeholder="vd: Giá phụ thuộc vào thời điểm ..."></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="control-label col-md-2">Ghi chú số người, khách sạn <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtRoomDescription" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Ngày đăng</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDate" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Thứ tự</label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtOrder" TextMode="Number" Text="1" min="0" CssClass="not-reset form-control" Width="80"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Trạng thái</label>
                        <div class="col-md-10">
                            <label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbStatus" Checked="True" /><span></span></label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Tiếp tục tạo mục khác</label>
                        <div class="col-md-10">
                            <label class="switch switch-primary"><asp:CheckBox runat="server" ID="cbContiue" /><span></span></label>
                        </div>
                    </div>
                </fieldset>
                <fieldset runat="server" id="fsFilter" class="fsFilter">
                    <legend>Thuộc tính lọc</legend>
                    <div class="form-group">
                        <asp:Repeater ID="rptParentFilter" runat="server">
                            <ItemTemplate>
                                <div class="col-sm-6 col-md-4 col-lg-3">
                                    <div class="fwb"><%#Eval(FilterColumns.VfName).ToString() %></div>
                                    <div class="list">
                                        <asp:RadioButtonList ID="rdlAnswer" runat="server" DataSource='<%#GetSubFilter(Eval(FilterColumns.IfId).ToString(), Eval(FilterColumns.IfType).ToString(), "0") %>' DataTextField="<%#FilterColumns.VfName%>" DataValueField="<%#FilterColumns.IfId %>" />
                                        <asp:CheckBoxList ID="cblAnswer" runat="server" DataSource='<%#GetSubFilter(Eval(FilterColumns.IfId).ToString(), Eval(FilterColumns.IfType).ToString(), "1") %>' DataTextField="<%#FilterColumns.VfName%>" DataValueField="<%#FilterColumns.IfId %>" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </fieldset>

                <fieldset>
                    <legend>Nội dung chi tiết</legend>
                    <div class="form-group">
                        <div class="col-md-12">
                            <CKEditor:CKEditorControl ID="txt_content" Height="150" runat="server"></CKEditor:CKEditorControl>
                        </div>
                    </div>
                </fieldset>
                <div class="row">
                    <div class="col-md-6">
                        <fieldset>
                            <legend>Giá bao gồm</legend>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <CKEditor:CKEditorControl ID="txt_content2" Height="150" Toolbar="Basic" runat="server"></CKEditor:CKEditorControl>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-md-6">
                        <fieldset>
                            <legend>Giá không bao gồm</legend>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <CKEditor:CKEditorControl ID="txt_content3" Height="150" Toolbar="Basic" runat="server"></CKEditor:CKEditorControl>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <fieldset>
                    <legend>Thông tin cần thiết cho chuyến đi</legend>
                    <div class="form-group">
                        <div class="col-md-12">
                            <CKEditor:CKEditorControl ID="txt_content4" Height="150" runat="server"></CKEditor:CKEditorControl>
                        </div>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>Tối ưu cho công cụ tìm kiếm</legend>
                    <div class="form-group count-this">
                        <label class="col-md-2 control-label">URL <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtUrl" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-2 control-label">Meta title <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtMetaTitle" ClientIDMode="Static" CssClass="form-control" runat="server" placeholder="Giới hạn từ 50 đến 65 ký tự"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-2 control-label">Meta keyword <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtMetaKeyword" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this">
                        <label class="col-md-2 control-label">Meta description <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtMetaDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5" placeholder="Giới hạn từ 130 đến 160 ký tự"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group count-this d-none">
                        <label class="col-md-2 control-label">Tag <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtTag" CssClass="form-control input-tags" placeholder="Add tag" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-10 col-md-offset-2">
                            <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Thêm mới" OnClientClick="unhook();" />
                        </div>
                    </div>
                    <asp:Button runat="server" ID="btSubmit2" OnClick="btSubmit_OnClick" CssClass="quick-submit btn btn-primary" Text="Save" OnClientClick="unhook();"/>
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

        if (indexOfdot > -1) result += phanThapPhan;

        return result + " €";
    }
</script>