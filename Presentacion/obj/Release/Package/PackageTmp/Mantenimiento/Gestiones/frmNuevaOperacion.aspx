<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevaOperacion.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestiones.frmNuevaOperacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"><asp:Button ID="btnVolverOperaciones" runat="server" Text="VOLVER" OnClick="btnVolverOperaciones_Click" CssClass="ButtonNeutro"/></div> 
    <div class="col-md-12" id="tblOperacion">
        <h3>OPERACIONES</h3>
        <p>Mantenimiento de Operaciones del Sistema Gestor.</p>
        <div class="col-md-8">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label> </div>
            <div class="col-md-8"><asp:TextBox ID="txtIdOperacion" runat="server" MaxLength="10" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-8">
            <div class="col-md-3"><asp:Label ID="lblm" runat="server" Text="Módulo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:DropDownList ID="ddlIdModulo" runat="server"  CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <div class="col-md-8">
            <div class="col-md-3"><asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:TextBox  ID="txtDescripcion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-8">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="ID Operación Reversa:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:TextBox ID="txtOperacionReserva" runat="server" CssClass="FormatoDropDownList"></asp:TextBox></div>
        </div>
        <div class="col-md-8">
            <div class="col-md-3"><asp:Label ID="Label5" runat="server" Text="Clase Documento:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:DropDownList ID="ddlIdClaseDoc" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <div class="col-md-8">
            <div class="col-md-3"><asp:Label ID="Label6" runat="server" Text="Estado:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-8"><asp:CheckBox ID="chkEstados" runat="server" Checked='<%# Eval("Estado") %>' /></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnCrearOperacion" runat="server" Text="CREAR" OnClick="btnCrearOperacion_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
   
</asp:Content>
