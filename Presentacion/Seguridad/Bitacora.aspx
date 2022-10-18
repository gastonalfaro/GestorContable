<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Bitacora.aspx.cs" Inherits="Presentacion.Seguridad.Bitacora" %>
<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
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
     <div class="col-md-12"><h2>Bitácora de Acciones</h2></div>
    <div  class="col-md-6">       
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblFechaDesde" runat="server" Text="Desde: "></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtFechaDesde" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5"> <asp:Label ID="lblUsuario" runat="server" Text="Cédula Usuario: "></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtUsuario" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5"><asp:Label ID="lblAccion" runat="server" Text="Acción"></asp:Label></div>
            <div class="col-md-7">   
                <asp:DropDownList ID="ddlAccion" runat="server"  CssClass="FormatoDropDownList">
                   <%-- <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem>Creación</asp:ListItem>
                    <asp:ListItem>Actualización</asp:ListItem>
                    <asp:ListItem>Eliminación</asp:ListItem>
                    <asp:ListItem>Consulta</asp:ListItem>
                    <asp:ListItem>Impresión</asp:ListItem>--%>
                </asp:DropDownList></div>
        </div>
    </div>
    <div  class="col-md-6">       
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblFechaHasta" runat="server" Text="Hasta: "></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtFechaHasta" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5"><asp:Label ID="lblModulo" runat="server" Text="Módulo"></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlModulo" runat="server"  CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
    </div>

   
    <br />
    <div class="col-md-12">
        <div style="text-align:center">
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" CssClass="ButtonNeutro"/>
        </div>
    </div>
    <br />
    <br />
    <asp:GridView ID="gvBitacora" runat="server" AutoGenerateColumns="False" ShowFooter="True" ForeColor="#333333" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvBitacora_PageIndexChanging"
         CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
        <Columns>
            <asp:TemplateField HeaderText="IdRegistro" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblIdRegistro" runat="server" Text='<%# Bind("IdRegistro") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Fecha" Visible="true">
                <ItemTemplate>
                    <asp:Label ID="lblFchAccion" runat="server" Text='<%# Bind("FchAccion") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Usuario">
                <ItemTemplate>
                    <asp:Label ID="lblIdUsuario" runat="server" Text='<%# Bind("IdUsuario") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="SesionUsuario">
                <ItemTemplate>
                    <asp:Label ID="lblIdSesionUsuario" runat="server" Text='<%# Bind("IdSesionUsuario") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Dirección IP">
                <ItemTemplate>
                    <asp:Label ID="lblIPMaquina" runat="server" Text='<%# Bind("IPMaquina") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Módulo">
                <ItemTemplate>
                    <asp:Label ID="lblModulo" runat="server" Text='<%# Bind("NomModulo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Accion">
                <ItemTemplate>
                    <asp:Label ID="lblAccion" runat="server" Text='<%# Bind("Accion") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Detalle">
                <ItemTemplate>
                    <asp:Label ID="lblDetalle" runat="server" Text='<%# Bind("Detalle") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
    </asp:GridView>

     <div class="col-md-12">
        <asp:Label ID="lblSinResultados" runat="server" Text="La búsqueda no produjo resultados" ForeColor="Red" Visible="False" Font-Italic="True"></asp:Label>
    </div>
</asp:Content>
