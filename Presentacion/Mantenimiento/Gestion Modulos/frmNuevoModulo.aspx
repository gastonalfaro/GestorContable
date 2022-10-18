<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevoModulo.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestion_Modulos.frmNuevoModulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"><asp:Button ID="btnModuloVolver" runat="server" Text="VOLVER" OnClick="btnModuloVolver_Click" CssClass="ButtonNeutro"/></div> 
    <div class="col-md-12">
        <h3>MÓDULOS</h3>
        <p> Mantenimiento de Módulos del Sistema Gestor.</p>
    </div>
    <div class="col-md-12">
        <div class="col-md-8">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label> </div>
            <div class="col-md-8"><asp:TextBox Width="100%" ID="txtIdModulo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
    </div>
    <div class="col-md-12">
        <div class="col-md-8">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:TextBox Width="100%"  ID="txtNomModulo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
    </div>
     <div class="col-md-12">
        <div class="col-md-8">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="Estado:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:CheckBox ID="chkCrearEstado" runat="server" OnCheckedChanged="chkCrearEstado_CheckedChanged" Text="Activo" ></asp:CheckBox></div>
        </div>
    </div>
    <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnCrearModulo" runat="server" Text="CREAR" OnClick="btnCrearModulo_Click" CssClass="ButtonNeutro" /></div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
 </asp:Content>
