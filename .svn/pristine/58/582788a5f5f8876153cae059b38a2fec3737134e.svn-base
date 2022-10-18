<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Politicas.aspx.cs" Inherits="Presentacion.Seguridad.Politicas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 175px;
        }
        .auto-style2 {
            width: 141px;
        }
        .auto-style3 {
            width: 143px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12"><h2>Gestión de Políticas</h2></div>
    <div  class="col-md-6">       
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblTmpOcio" runat="server" Text="Tiempo máximo ocio (minutos)"></asp:Label></div>
            <div class="col-md-7">
                  <asp:TextBox ID="txtTmpOcio" runat="server" TextMode="Number" CssClass="FormatoTextBox"></asp:TextBox>
                        <asp:RangeValidator runat="server" ID="RangeValidator1" ControlToValidate="txtTmpOcio" Type="Integer"
                            MinimumValue="0" ErrorMessage="El valor debe ser mayor o igual 0" Display="Dynamic" ForeColor="#CC0000" MaximumValue="100000000" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-5"> <asp:Label ID="lblVigenciaClave" runat="server" Text="Días de vigencia de contraseña:"></asp:Label></div>
            <div class="col-md-7">    <asp:TextBox ID="txtVigenciaClave" runat="server" TextMode="Number"  CssClass="FormatoTextBox"></asp:TextBox>
                        <asp:RangeValidator runat="server" ID="RangeValidator4" Type="Integer" ForeColor="#CC0000"  ControlToValidate="txtVigenciaClave"
                            MinimumValue="0" MaximumValue="100000000" ErrorMessage="El valor debe ser mayor o igual 0" Display="Dynamic" />
                </div>
        </div>
        <div class="row">
            <div class="col-md-5"> <asp:Label ID="lblTiempoBloqueo" runat="server" Text="Duración de bloqueo de contraseña(minutos)"></asp:Label></div>
            <div class="col-md-7">      <asp:TextBox ID="txtBloqueoClave" runat="server" TextMode="Number"  CssClass="FormatoTextBox"></asp:TextBox>
                        <asp:RangeValidator runat="server" ID="RangeValidator5" Type="Integer" ForeColor="#CC0000"  ControlToValidate="txtBloqueoClave"
                            MinimumValue="0" MaximumValue="100000000" ErrorMessage="El valor debe ser mayor o igual 0" Display="Dynamic" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-5"> <asp:Label ID="lblCantNumeros" runat="server" Text="Cantidad de números en clave"></asp:Label></div>
            <div class="col-md-7">    <asp:TextBox ID="txtCantNumeros" runat="server" TextMode="Number"  CssClass="FormatoTextBox"></asp:TextBox>
                        <asp:RangeValidator runat="server" ID="RangeValidator9" Type="Integer" ForeColor="#CC0000"  ControlToValidate="txtCantNumeros"
                            MinimumValue="0" MaximumValue="100000000"  ErrorMessage="El valor debe ser mayor o igual 0" Display="Dynamic" />
            </div>
        </div>
    </div>
     <div  class="col-md-6">       
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblMaxIntentosFallidos" runat="server" Text="Límite de intentos fallidos de iniciar sesión"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtMaxIntentosFallidos" runat="server" TextMode="Number" CssClass="FormatoTextBox"></asp:TextBox>
                        <asp:RangeValidator runat="server" ID="RangeValidator3" Type="Integer" ForeColor="#CC0000" ControlToValidate="txtMaxIntentosFallidos"
                            MinimumValue="0" MaximumValue="100000000" ErrorMessage="El valor debe ser mayor o igual 0" Display="Dynamic" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">  <asp:Label ID="Label1" runat="server" Text="Reutilización de clave vieja, posterior a “n” claves nuevas"></asp:Label>
                   </div>
            <div class="col-md-7"> <asp:TextBox ID="txtReutilizacionClave" runat="server" TextMode="Number" CssClass="FormatoTextBox"></asp:TextBox>
                        <asp:RangeValidator runat="server" ID="RangeValidator7" Type="Integer" ForeColor="#CC0000" ControlToValidate="txtReutilizacionClave"
                            MinimumValue="0" MaximumValue="100000000"  ErrorMessage="El valor debe ser mayor o igual 0" Display="Dynamic" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-5"> <asp:Label ID="lblCantLetrasClave" runat="server" Text="Cantidad de letras en contraseña"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtCantLetrasClave" runat="server" TextMode="Number" CssClass="FormatoTextBox"></asp:TextBox>
                        <asp:RangeValidator runat="server" ID="RangeValidator6" Type="Integer" ForeColor="#CC0000"  ControlToValidate="txtCantLetrasClave"
                            MinimumValue="0"  MaximumValue="100000000"  ErrorMessage="El valor debe ser mayor o igual 0" Display="Dynamic" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblCantCaracteres" runat="server" Text="Cantidad de símbolos en contraseña"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCantCaracteres" runat="server" TextMode="Number" CssClass="FormatoTextBox"></asp:TextBox>
                        <asp:RangeValidator runat="server" ID="RangeValidator8" Type="Integer" ForeColor="#CC0000"  ControlToValidate="txtCantCaracteres"
                            MinimumValue="0" MaximumValue="100000000"  ErrorMessage="El valor debe ser mayor o igual 0" Display="Dynamic" />
            </div>
        </div>
    </div> 

    <br />
               
    <div class="col-md-12">
        <div style="text-align:center">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="ButtonNeutro" />
            <asp:Button ID="btnAtras" runat="server" Text="Atrás" OnClick="btnAtras_Click" CssClass="ButtonNeutro" />
        </div>
    </div>
</asp:Content>
