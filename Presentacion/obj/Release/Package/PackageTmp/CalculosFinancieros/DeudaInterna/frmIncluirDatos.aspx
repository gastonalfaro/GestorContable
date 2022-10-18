<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmIncluirDatos.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmIncluirDatos" %>

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
        <h3>Cargar información:Títulos</h3>
        <table style="width: 100%;">
            <tr>
                <td colspan="3">
                    <asp:HyperLink ID="hlDescargarFormato" runat="server">Descargar Formato de archivo</asp:HyperLink>

                    <br />
                    <b>Nota Importante:</b> Al descargar el archivo, reemplace (en el nombre del archivo) la palabra plantilla, por su número de cédula. Incluya ceros sin espacios. Ej: Titulos_0109990888&quot;</td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
                <td align="center">&nbsp;<asp:Button runat="server" ID="btnSubirArchivo" Text="Subir Archivo" OnClick="UploadButton_Click" Width="200px" CssClass="ButtonNeutro"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:FileUpload ID="fucCargaArchivo" runat="server" CssClass="FormatoTextBox"/>
                    <br />
                    <asp:Label runat="server" ID="lblEstatus" Text="Estatus de carga: " Font-Bold="True" />
                    <br />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" align="center">

                    <asp:Button ID="btnCargarInfo" runat="server" OnClientClick="return confirm('Se cargarán los datos visualizados. ¿Seguro que desea continuar?');" OnClick="Button2_Click" Text="Cargar Información" Width="200px" CssClass="ButtonNeutro"/>

                    <br />
                    <asp:Label ID="lblMensaje" runat="server" Text="Estado de la carga: " Font-Bold="True"></asp:Label>

                </td>
            </tr>

        </table>
        <br />
        <div style="overflow-x: auto; width: auto" align="center">
            <asp:GridView ID="grvCCSS" runat="server" CellPadding="4" ForeColor="#333333" AllowPaging="True" OnPageIndexChanging="grvCCSS_PageIndexChanging" Font-Size="X-Small" PageSize="20"
                  CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                <EditRowStyle BackColor="#999999" />
            </asp:GridView>
        </div>
        <br />
    </div>

    <div style="background-color:lightgray;width:100%;text-align:center;height:50px;">
        <asp:Button ID="btnContabilizar" runat="server" CssClass="ButtonNeutro" Text="Contabilizar" Style="margin-top:5px;" OnClick="btnContabilizar_Click"/>
    </div>
</asp:Content>
