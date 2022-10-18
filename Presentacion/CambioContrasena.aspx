<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalSistemaGestor.Master" AutoEventWireup="true" CodeBehind="CambioContrasena.aspx.cs" Inherits="Presentacion.Perfil.CambioContrasena" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <h2>Cambio de Contraseña</h2>
    <table class="asd">         
        <tr>
            <td class="auto-style2">
                <asp:Label ID="lblContrasenaActual" runat="server" Text="Contraseña actual: "></asp:Label>
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="txtContrasenaActual" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="lblNuevaContrasena" runat="server" Text="Nueva contraseña: "></asp:Label>
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="txtNuevaContrasena" runat="server" TextMode="Password"></asp:TextBox>
                 <asp:Label id="label1" ToolTip="Los símbolos permitidos son: !,#,%,&,*,-,_,?,@" runat="server" Font-Bold="True">?</asp:Label>
            </td>
            <td><asp:RegularExpressionValidator Display = "Dynamic" ForeColor="Red" ControlToValidate = "txtNuevaContrasena" ValidationExpression = "^[\s\S]{8,}$" runat="server" ErrorMessage="La contraseña debe tener mínimo 8 caracteres."></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="lblConfirmacion" runat="server" Text="Confirme la contraseña: "></asp:Label>
            </td>
            <td class="auto-style1">
                <asp:TextBox ID="txtConfirmacion" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                <asp:CompareValidator ErrorMessage="Las contraseñas no concuerdan." ForeColor="Red" ControlToCompare="txtNuevaContrasena"
                    ControlToValidate="txtConfirmacion" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
    <br />
</asp:Content>
