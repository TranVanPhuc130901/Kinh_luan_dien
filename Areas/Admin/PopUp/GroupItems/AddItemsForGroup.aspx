<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddItemsForGroup.aspx.cs" Inherits="Areas_Admin_PopUp_GroupItems_AddItemsForGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add items to group</title>
    <meta name="robots" content="noindex, nofollow" />
    <link rel="shortcut icon" href="/Areas/Admin/img/favicon.png"/>
    <style>
        #PopupAddItem{ font: normal 12px Tahoma}
        #PopupAddItem .TitleItem{font:bold 12px Tahoma;color:#444;display:block;padding:5px 0 8px 0}
        #PopupAddItem .TitleListBox{ display: flex;align-items: center;justify-content: space-between;}
        #PopupAddItem .pdButtonGet{padding-top:30px;padding-bottom:25px}
        #PopupAddItem .MesText{color:Red;padding-bottom:5px}
        #PopupAddItem .NoteText{color:Red;font-weight:bold}
        #PopupAddItem .cb10{ clear: both;height:10px}
        input[type=submit]{cursor: pointer}
        select{ cursor: pointer;padding: 3px 5px}
        input{padding: 3px 5px}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="PopupAddItem">
                    <table style="width: 100%; border: 0;border-spacing: 0;border-collapse: collapse;">
                        <tr>
                            <td style="width: 45%">
                                <div class="TitleListBox">
                                    <b><asp:Literal ID="lt_cate_name" runat="server"></asp:Literal></b>
                                    <input type="text" id="searchBox2" onkeyup="filter(this.value, '#lstadded');" placeholder="Lọc theo tiêu đề"/>
                                </div>
                                <div class="cb10"></div>
                                <div><asp:ListBox ID="lstadded" runat="server" Width="100%" Height="350px" SelectionMode="Multiple"></asp:ListBox></div>
                            </td>
                            <td style="width: 10%;text-align: center">
                                <div class="pdButtonGet"><asp:Button ID="btnadd" Width="30px" runat="server" Text="<<" OnClick="btnadd_Click" /></div>
                                <div><asp:Button ID="btnremove" Width="30px" runat="server" Text=">>" OnClick="btnremove_Click" /></div>
                            </td>
                            <td style="width: 45%">
                                <div class="TitleListBox">
                                    <input type="text" id="searchBox" onkeyup="filter(this.value, '#lstnotadded');" placeholder="Lọc theo tiêu đề"/>
                                    <asp:DropDownList ID="ddl_groups" runat="server" Width="230px" AutoPostBack="true" OnSelectedIndexChanged="ddl_groups_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                                <div class="cb10"></div>
                                <div><asp:ListBox ID="lstnotadded" runat="server" Width="100%" Height="350px" SelectionMode="Multiple"></asp:ListBox></div>
                            </td>
                        </tr>
                    </table>
                    <div class="cb10"></div>
                    <div class="cRed">Chú ý: Có thể chọn nhiều mục cùng lúc bằng cách giữ phím <span class="NoteText">Shift</span> hoặc <span class="NoteText">Ctrl</span> khi chọn!</div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script>
        function filter(input, list) {
            // Declare variables
            input = xoa_dau(input.toString()).toLowerCase().trim();
            var label = document.querySelectorAll(list + " option");
            //alert("Bọt Tắm Gội Cho Bé Mamamy - Hương Blueberry 200ml".toLowerCase().indexOf("b"));
            // Loop through all list items, and hide those who don't match the search query
            for (var i = 0; i < label.length; i++) {
                var a = label[i];
                if (xoa_dau(a.text).toLowerCase().indexOf(input) > -1) {
                    label[i].style.display = "";
                } else {
                    label[i].style.display = "none";
                }
            }
        };
        function xoa_dau(str) {
            str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
            str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
            str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
            str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
            str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
            str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
            str = str.replace(/đ/g, "d");
            str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
            str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
            str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
            str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
            str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
            str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
            str = str.replace(/Đ/g, "D");
            str = str.replace(/[^0-9a-zàáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ\s]/gi, '');
            str = str.replace(/\s+/g, ' ');
            return str.trim();
        }
    </script>
</body>
</html>
