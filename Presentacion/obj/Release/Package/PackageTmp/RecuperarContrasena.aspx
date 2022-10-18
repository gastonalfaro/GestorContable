<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="RecuperarContrasena.aspx.cs" Inherits="Presentacion.RecuperarContrasena" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
     <link rel="stylesheet" href="Compartidas/rmm-css/Login.css"/>
     <style type="text/css">           
        .login { width:415px;}
        select { width:300px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <asp:Panel ID="pnlCorreo" runat="server" Visible="True">
         <div class="login">   
        <table class="asd">
            <tr>
                <td colspan="3" class="titulotabla">Recuperar contraseña</td>
            </tr>
            <tr>
                <td class="auto-style2">Tipo Identificación:&nbsp;
                </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="ddlOrigen" runat="server" onchange="cargAnnombre()">
                        <asp:ListItem Value="Fisico">Físico</asp:ListItem>
                        <asp:ListItem Value="Juridico">Jurídico</asp:ListItem>
                        <asp:ListItem Value="DIMEX">DIMEX</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblIdUsuario" runat="server" Text="Número Identificación: "></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
             <tr>
                <td class="auto-style2">
                    <asp:Label ID="lblCorreo" runat="server" Text="Correo electrónico: "></asp:Label>
                </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtCorreo" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" CssClass="ButtonNeutro" Width="40%"/>
        <asp:Button ID="btnAtras" runat="server" Text="Atrás" OnClick="btnAtras_Click" CssClass="ButtonNeutro"  Width="40%"/>
        <br />
        <asp:Label id="lblMensajeConf" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
       </div>
    </asp:Panel>
</asp:Content>
