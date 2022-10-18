<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevoAcreedor.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestiones.frmNuevoAcreedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"><asp:Button ID="btnVolverAcreedores" runat="server" Text="VOLVER" OnClick="btnVolverAcreedores_Click" CssClass="ButtonNeutro"/></div> 
    <div class="col-md-12">
        <h3>ACREEDORES</h3>
        <p> Mantenimiento de Acreedores del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"> <asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label>    </div>
            <div class="col-md-5"> <asp:TextBox  ID="txtNumAcreedor" runat="server" MaxLength="10" CssClass="FormatoTextBox" onkeypress="return AceptarSoloNumeros(event)" ></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox  ID="txtDescripcion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label9" runat="server" Text="Abreviatura:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox  ID="txtAbreviatura" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="Contacto:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtContacto" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label4" runat="server" Text="Telefono:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txttelefono" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"> <asp:Label ID="Label7" runat="server" Text="Dirección:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtDireccion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label5" runat="server" Text="País:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtPais" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
          <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label8" runat="server" Text="País Institución:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtPaisInstitucion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
          <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label10" runat="server" Text="Tipo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtTipo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label6" runat="server" Text="Estado:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:CheckBox ID="chkEstado" runat="server" Checked='<%# Eval("Estado") %>' Text="Activo" /></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnCrearAcreedor" runat="server" Text="CREAR" OnClick="btnCrearAcreedor_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
 </asp:Content>
