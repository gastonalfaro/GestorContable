<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="CambioContrasena.aspx.cs" Inherits="Presentacion.Perfil.CambioContrasena" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <style type="text/css">
        .FormatoTextBox, .FormatoDropDownList { width:100%!important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12">
        <h2>Cambio de Contraseña</h2>       
    </div>
    <div class="col-md-6">					
        <div class="row">
            <div class="col-md-3"><asp:Label ID="lblContrasenaActual" runat="server" Text="Contraseña actual: "></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtContrasenaActual" runat="server" TextMode="Password" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="row">
            <div class="col-md-3"><asp:Label ID="lblNuevaContrasena" runat="server" Text="Nueva contraseña: "></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtNuevaContrasena" runat="server" TextMode="Password" CssClass="FormatoTextBox"></asp:TextBox>
                 <asp:Label id="label1" ToolTip="Los símbolos permitidos son: !,#,%,&,*,-,_,?,@" runat="server" Font-Bold="True">?</asp:Label>
                <asp:RegularExpressionValidator Display = "Dynamic" ForeColor="Red" ControlToValidate = "txtNuevaContrasena" ValidationExpression = "^[\s\S]{8,}$" runat="server" ErrorMessage="La contraseña debe tener mínimo 8 caracteres."></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3"><asp:Label ID="lblConfirmacion" runat="server" Text="Confirme la contraseña: "></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtConfirmacion" runat="server" TextMode="Password" CssClass="FormatoTextBox"></asp:TextBox>
                <asp:CompareValidator ErrorMessage="Las contraseñas no concuerdan." ForeColor="Red" ControlToCompare="txtNuevaContrasena"
                    ControlToValidate="txtConfirmacion" runat="server" />
            </div>
        </div>
    </div>
     <div class="col-md-12" style="text-align:center;">
        <div class="col-md-7">
          <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click"  CssClass="ButtonNeutro"/>
        </div>
     </div>

    <div class="col-md-12" style="text-align:center;">
        <div class="col-md-7">
          <asp:Button ID="btnComandoXX" runat="server" Text="ComandoX" OnClick="btnComandoXX_Click"  CssClass="ButtonNeutro"/>
        </div>
     </div>

    <br />
</asp:Content>
