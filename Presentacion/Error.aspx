<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Presentacion.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
     <div id="TIng" style="width:60%;margin: 10% auto auto auto; text-align:center;" >
        <div style="background:rgba(231, 231, 232, 0.6);padding:30px;text-align:center;" >
            <img src="Compartidas/imagenes/Warning.png" class="Img256" alt="Error" style="width:35%;padding-bottom:15px;"/>
            <asp:Label ID="lblMensaje" runat="server" style="display:block;font-weight:bold;">Disculpe tenemos problemas con la página, vuelva a intentarlo más tarde.</asp:Label>
        </div>
    </div>
</asp:Content>
