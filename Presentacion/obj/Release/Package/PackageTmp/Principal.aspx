<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Presentacion.Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 618px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<%--<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>--%>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div align="Center">
        <img alt="Bienvenido" class="auto-style1" longdesc="Bienvenido Portal" src="Compartidas/imagenes/portal.png" />
    </div>
</asp:Content>
