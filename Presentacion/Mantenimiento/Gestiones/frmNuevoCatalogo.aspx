<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevoCatalogo.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestiones.frmNuevoCatalogo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"> <asp:Button ID="btnVolverOperaciones" runat="server" Text="VOLVER" OnClick="btnVolverOperaciones_Click1" CssClass="ButtonNeutro"/></div> 
    <div class="col-md-12">
        <h3>CATÁLOGOS GENERALES</h3>
        <p> Mantenimiento de Catálogos Generales del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label>    </div>
            <div class="col-md-8"><asp:TextBox Width="100%" ID="txtIdOperacion" runat="server" MaxLength="10"  CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblm" runat="server" Text="Módulo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:DropDownList ID="ddlIdModulo" runat="server"  CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:TextBox Width="100%"  ID="txtDescripcion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label6" runat="server" Text="Estado:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Eval("Estado") %>' Text="Activo" /></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnCrearCatalogo" runat="server" Text="CREAR" OnClick="btnCrearCatalogo_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    
</asp:Content>
