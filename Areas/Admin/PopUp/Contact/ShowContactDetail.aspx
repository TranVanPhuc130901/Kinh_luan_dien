<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowContactDetail.aspx.cs" Inherits="Areas_Admin_PopUp_Contact_ShowContactDetail" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Chi tiết liên hệ</title>
    <style>
        table {border-collapse: collapse;width: 100%}
        table, th, td {border: 1px solid black;}
        td{padding: 5px}
        tr:nth-child(even){background: #f2f2f2}
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div style="font-family: Arial">
        <asp:Literal runat="server" id="ltrList"/>
    </div>
</form>
</body>
</html>
