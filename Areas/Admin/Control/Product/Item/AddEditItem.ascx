<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddEditItem.ascx.cs" Inherits="Areas_Admin_Control_Product_Item_AddEditItem" %>
<%@ Register TagPrefix="CKEditor" Namespace="CKEditor.NET" Assembly="CKEditor.NET, Version=3.6.6.2, Culture=neutral, PublicKeyToken=e379cdf2f8354999" %>
<%@ Register Src="~/Areas/Admin/Control/Component/UploadImage.ascx" TagPrefix="uc1" TagName="UploadImage" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="Developer.Keyword" %>
<%@ Import Namespace="RevosJsc.Columns" %>
<div id="page-content">
    <!-- Forms General Header -->
    <ul class="breadcrumb breadcrumb-top">
        <li><%=ProductKeyword.Product %></li>
        <li><asp:Literal runat="server" ID="ltrTitle" /></li>
    </ul>
    <!-- END Forms General Header -->
    <form id="form" runat="server" class="form-horizontal">
        <asp:HiddenField runat="server" ID="HdOldIgId" />
        <asp:HiddenField runat="server" ID="HdOldAuthor" />
        <asp:HiddenField runat="server" ID="HdTitle" />
        <asp:HiddenField runat="server" ID="HdVariant" />
        <asp:HiddenField runat="server" ID="HdWholesale" />
        <asp:HiddenField runat="server" ID="HdPromotionStartDate" />
        <asp:HiddenField runat="server" ID="HdPromotionEndDate" />
        <asp:HiddenField ID="HdOldContent" runat="server" />
        <asp:HiddenField ID="HdTotalView" runat="server" />
        <div class="block row">
            <div class="col-md-12">
                <fieldset>
                    <legend>Thông tin cơ bản</legend>
                    <div class="form-group">
                        <label class="control-label col-md-2">Danh mục cha</label>
                        <div class="col-md-10">
                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control select-chosen" required />
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
                        <label class="control-label col-md-2">SKU</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtCode" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label class="control-label col-md-2">Giá niêm yết</label>
                        <div class="col-md-2">
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtPriceOld" TextMode="Number" CssClass="form-control" onkeyup="HienThiGia(this,'giaNY');"></asp:TextBox>
                                <div id="giaNY" class="input-group-addon">
                                    <asp:Literal runat="server" ID="ltrPriceOld" />
                                </div>
                            </div>
                        </div>
                        <label class="control-label col-md-2 d-none">Giá bán</label>
                        <div class="col-md-2 d-none">
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtPriceNew" TextMode="Number" CssClass="form-control" onkeyup="HienThiGia(this,'giaBan');"></asp:TextBox>
                                <div id="giaBan" class="input-group-addon">
                                    <asp:Literal runat="server" ID="ltrPriceNew" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group d-none">
                        <label class="control-label col-md-2">Tùy chọn nâng cấp</label>
                        <div class="col-md-10">
                            <asp:CheckBoxList runat="server" id="cblOption" RepeatColumns="4" CssClass="table table-vcenter table-borderless table-condensed"/> 
                        </div>
                    </div>
                    <uc1:UploadImage runat="server" ID="UploadImage" />
                    <uc1:UploadImage runat="server" ID="UploadImage1" />
                    <div class="form-group">
                        <label class="control-label col-md-2">Ngày đăng</label>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtDate" TextMode="DateTimeLocal" CssClass="form-control"></asp:TextBox>
                        </div>
                        <label class="control-label col-md-1">Thứ tự</label>
                        <div class="col-md-2">
                            <asp:TextBox runat="server" ID="txtOrder" TextMode="Number" Text="1" CssClass="not-reset form-control" Width="80"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-2">Trạng thái</label>
                        <div class="col-md-2">
                            <label class="switch switch-primary">
                                <asp:CheckBox runat="server" ID="cbStatus" Checked="True" /><span></span></label>
                        </div>
                        <label class="control-label col-md-2">Ngừng kinh doanh</label>
                        <div class="col-md-2">
                            <label class="switch switch-primary">
                                <asp:CheckBox runat="server" ID="cbInventory" Checked="false" /><span></span></label>
                        </div>
                        <label class="control-label col-md-3">Tiếp tục tạo mục khác</label>
                        <div class="col-md-1">
                            <label class="switch switch-primary">
                                <asp:CheckBox runat="server" ID="cbContiue" /><span></span></label>
                        </div>
                    </div>
                </fieldset>
                <asp:ScriptManager runat="server" ID="ScriptManager1" />
                <asp:UpdatePanel runat="server" ID="UpdatePanel">
                    <ContentTemplate>
                        <fieldset>
                            <legend>Nhóm phân loại</legend>
                            <asp:Repeater runat="server" ID="rptOption" OnItemCommand="rptOption_OnItemCommand">
                                <ItemTemplate>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <asp:TextBox type="text" ID="tbGroupName" CssClass="form-control" runat="server" placeholder="Tên nhóm phân loại"></asp:TextBox>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox type="text" ID="tbClass" CssClass="form-control" runat="server" placeholder="Có thể nhập nhiều loại, mỗi loại cách nhau bằng dấu phẩy (,)"></asp:TextBox>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton id="lbtDelete" Text="Xóa" runat="server" CssClass="btn btn-warning" CommandName="delete" CommandArgument="1"/>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="form-group">
                                <div class="col-md-3 col-md-offset-3">
                                    <asp:Button id="btAddOption" runat="server" Text="Thêm mới phân loại" OnClick="btAddOption_Click" CssClass="btn btn-default" CommandArgument="1" />
                                </div>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>
                        <fieldset>
                            <legend>Nhóm bán giá sỉ</legend>
                            <asp:Repeater runat="server" ID="rptWholesale" OnItemCommand="rptWholesale_OnItemCommand">
                                <ItemTemplate>
                                    <div class="form-group">
                                        <div class="col-md-2">
                                            <asp:TextBox runat="server" type="text" ID="tbGroupWholesale" placeholder="Số lượng" CssClass="form-control" ></asp:TextBox>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox type="text" ID="tbClassWholesale" CssClass="form-control" data-index='<%#  Container.ItemIndex %>' runat="server" placeholder="Giá bán" onkeyup="HienThiGiaBanLe(this, document.getElementById(this.getAttribute('id')).getAttribute('data-index'))"></asp:TextBox>
                                            <div id='<%# "giaSi_" + Container.ItemIndex %>' class="input-group-addon">
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:LinkButton id="lbtDelete" Text="Xóa" runat="server" CssClass="btn btn-warning" CommandName="delete" CommandArgument="1"/>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="form-group">
                                <div class="col-md-3 col-md-offset-3">
                                    <asp:Button id="btAddOptionWholesale" runat="server" Text="Thêm mới phân loại" OnClick="btAddOptionWholesale_click" CssClass="btn btn-default" CommandArgument="1" />
                                </div>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                    <legend>Mô tả chi tiết</legend>
                    <div class="form-group">
                        <div class="col-md-12">
                            <CKEditor:CKEditorControl ID="txt_content" runat="server"></CKEditor:CKEditorControl>
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
                    <div class="form-group count-this">
                        <label class="col-md-2 control-label">Tag <span></span></label>
                        <div class="col-md-10">
                            <asp:TextBox type="text" ID="txtTag" CssClass="form-control input-tags" placeholder="Add tag" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
                <div class="form-group">
                    <div class="col-md-10 col-md-offset-2">
                        <asp:Button runat="server" ID="btSubmit" CssClass="btn btn-primary" OnClick="btSubmit_OnClick" Text="Thêm mới" OnClientClick="unhook();" />
                    </div>
                </div>
                <asp:Button runat="server" ID="btSubmit2" OnClick="btSubmit_OnClick" CssClass="quick-submit btn btn-primary" Text="Save" OnClientClick="unhook();" />
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

    function HienThiGiaBanLe(idTextBoxGia, index) {
        var gia = idTextBoxGia.value;
        gia = DinhDangGia(gia);
        var giaSiElement = document.getElementById("giaSi_" + index);
        console.log(index);

        if (giaSiElement) {
            giaSiElement.innerHTML = gia;
        }
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