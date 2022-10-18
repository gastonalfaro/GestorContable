<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevoPrevisionesIncobrables.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestiones.frmNuevoPrevisionesIncobrables" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"> <asp:Button ID="btnVolverOperaciones" runat="server" Text="VOLVER" OnClick="btnVolverPrevisionesIncobrables_Click1" CssClass="ButtonNeutro"/></div> 
    <div class="col-md-12">
        <h3>PREVISIONES INCOBRABLES</h3>
        <p> Mantenimiento de Previsiones Incobrables del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblDiasMorosidad" runat="server" Text="Días Morosidad:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtDiasMorosidad" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblPorcEstimacion" runat="server" Text="Porcentaje de Estimación:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtPorcEstimacion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtDescripcion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnCrearPrevisionIncobrable" runat="server" Text="CREAR" OnClick="btnCrearPrevisionIncobrable_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    
</asp:Content>
