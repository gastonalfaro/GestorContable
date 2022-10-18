<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="NuevoUsuario.aspx.cs" Inherits="Presentacion.Seguridad.GestionUsuarios.NuevoUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
         <style type="text/css">
        .FormatoTextBox, .FormatoDropDownList { width:100%!important; }
             </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div  class="col-md-6">       
        <div class="row">
            <div class="col-md-5">Tipo Identificación:</div>
            <div class="col-md-7">  
                <asp:DropDownList ID="ddlOrigen" runat="server" onchange="cargaNombre()" CssClass="FormatoDropDownList">
                    <asp:ListItem Value="F">Físico</asp:ListItem>
                    <asp:ListItem Value="J">Jurídico</asp:ListItem>
                    <asp:ListItem Value="E">DIMEX</asp:ListItem>
                </asp:DropDownList></div>
        </div>
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblNombre" runat="server" Text="Nombre de Usuario"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtNombreUsuario" runat="server"  CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="row">
            <div class="col-md-5"> <asp:Label ID="lblCorreo" runat="server" Text="Correo electrónico"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCorreoUsr" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
    </div>
     <div  class="col-md-6">       
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblCedula" runat="server" Text="Cédula"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIdUsuario" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblSociedad" runat="server" Text="Institución"></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlSociedad" runat="server"  CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-7"><asp:CheckBox ID="chkAdmin" runat="server" Text="Administrador" TextAlign="Left" /></div>
        </div>
    </div>
 
    <br />
     <div class="col-md-12">
        <div style="position:absolute;right:15px;">
            <asp:Button ID="btnCrearUsuario" runat="server" Text="Guardar" CssClass="ButtonNeutro" Width="106px" OnClick="btnCrearUsuario_Click" Height="28px" />
            <asp:Button ID="btnCancelar" runat="server" Text="Atrás" OnClick="btnCancelar_Click" Width="98px" CssClass="ButtonNeutro" Height="28px"/>
        </div>
    </div>
</asp:Content>

