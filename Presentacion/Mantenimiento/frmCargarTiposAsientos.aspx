<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCargarTiposAsientos.aspx.cs" Inherits="Presentacion.Mantenimiento.frmIncluirDatos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div>
        <h3>Cargar información:Tipos de Asiento</h3>
        <asp:HyperLink ID="hlDescargarFormato" runat="server">Descargar Formato de archivo</asp:HyperLink>
        <br />
        <div style="width:100%;text-align:center">            
            <asp:FileUpload ID="fucCargaArchivo" runat="server" />
            <asp:Button runat="server" ID="btnSubirArchivo" Text="Subir Archivo" OnClick="UploadButton_Click" Width="200px" CssClass="ButtonNeutro" />
            <br />
            <asp:Label runat="server" ID="lblEstatus" Text="Estatus de carga: " Font-Bold="True" />
            <br />
            <asp:Button ID="btnCargarInfo" runat="server" OnClientClick="return confirm('Se cargarán los datos visualizados. ¿Seguro que desea continuar?');" OnClick="Button2_Click" Text="Cargar Información" CssClass="ButtonNeutro" Width="200px" />
            <br />
            <asp:Label ID="lblMensaje" runat="server" Text="Estado de la carga: " Font-Bold="True"></asp:Label>
        </div>
        <br />
        <div style="overflow-x: auto; width: auto" align="center">
            <asp:GridView ID="grvCCSS" runat="server" CellPadding="4" ForeColor="#333333" AllowPaging="True"
                 CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                 OnPageIndexChanging="grvCCSS_PageIndexChanging" Font-Size="X-Small" PageSize="20">
                <EditRowStyle BackColor="#999999" />
            </asp:GridView>
        </div>
        <br />
        
        <asp:TextBox ID="txtError" runat="server" TextMode="multiline" Width="825" rows="10" ForeColor="#FF3300" Enabled="false" Visible="true"></asp:TextBox>
    </div>
</asp:Content>
