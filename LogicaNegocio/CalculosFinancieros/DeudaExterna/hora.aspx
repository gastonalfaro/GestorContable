<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hora.aspx.cs" Inherits="LogicaNegocio.CalculosFinancieros.DeudaExterna.hora" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <br />
        <br />
        Moneda<asp:TextBox ID="ind" runat="server"></asp:TextBox>
        <br />
        fecha<asp:TextBox ID="fch" runat="server"></asp:TextBox>
        <br />
        monto<asp:TextBox ID="mnt" runat="server"></asp:TextBox>
        <br />
        <br />
        monto dolares<asp:TextBox ID="mntd" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
        <br />
        IRR = <asp:TextBox ID="irr" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
