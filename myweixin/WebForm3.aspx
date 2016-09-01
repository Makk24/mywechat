<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="myweixin.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <br/><br/>
    <div>
        <table>
            <tr>
                <td><asp:Label ID="Label2" runat="server" Text="doi:"></asp:Label>&nbsp;&nbsp;</td>
                <td><asp:TextBox ID="TextBox2" runat="server" Width="400" Height="50" TextMode="MultiLine"></asp:TextBox>（多个doi，使用英文逗号分隔，如：10.1038/480426a,10.1038/480425a）<asp:Button ID="Button1" runat="server" Text="获取" OnClick="Button1_Click" Height="32px" Width="73px" />

                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
