<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="ImpresionFormulario.aspx.cs" Inherits="Presentacion.RevelacionNotas.ImpresionFormulario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">
    <asp:Panel ID="CambioPatrimonioNetoAgregadoPanel" HorizontalAlign="Left" runat="Server">
        <asp:MultiView ID="ReportesContigentesMultiView" ActiveViewIndex="0" runat="Server">
            <asp:View ID="View1" runat="Server">
                <div>
                    <iframe height="800px" width="100%" src="../Compartidas/VisorReportes/VisorReportes.aspx"   frameborder="0"></iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
    <br />
    <br />
    <asp:Button ID="btnAtras" runat="server" Text="Atrás" OnClick="btnAtras_Click" style="text-align: center" Width="77px" CssClass="ButtonNeutro" />
</asp:Content>
