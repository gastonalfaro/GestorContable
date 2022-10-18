<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevoPropietario.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestiones.frmNuevoPropietario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"> <asp:Button ID="btnPropietarioVolver" runat="server" Text="VOLVER" OnClick="btnPropietarioVolver_Click" CssClass="ButtonNeutro" /></div> 
    <div class="col-md-12" id="tblPropietarios">
        <h3>Propietarios</h3>
        <p> Mantenimiento de Propietarios del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:TextBox Width="100%" ID="txtIdPropietario" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:TextBox Width="100%"  ID="txtDesPropietario" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6" style="visibility:hidden;">
            <div class="col-md-3"><asp:Label ID="Label5" runat="server" Text="SocicedadGL:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:DropDownList ID="ddlIdSociedadGL" runat="server"  CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <div class="col-md-6" style="visibility:hidden;">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="SociedadFi:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:DropDownList ID="ddlIdSociedadFi" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
         <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label4" runat="server" Text="Estado:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:CheckBox ID="cbEstado" runat="server" /></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnCrearPropietario" runat="server" Text="CREAR" OnClick="btnCrearPropietario_Click" CssClass="ButtonNeutro"/></div>
    </div>
    <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>

</asp:Content>
