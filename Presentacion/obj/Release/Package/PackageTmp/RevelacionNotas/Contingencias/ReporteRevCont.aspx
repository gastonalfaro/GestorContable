<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="ReporteRevCont.aspx.cs" Inherits="Presentacion.RevelacionNotas.Contingencias.ReporteRevCont" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">
     <asp:Panel ID="CambioPatrimonioNetoAgregadoPanel" HorizontalAlign="Left" runat="Server">
        <asp:MultiView ID="ReportesContigentesMultiView" ActiveViewIndex="0" runat="Server">
            <asp:View ID="View1" runat="Server">
                <div>
                    <iframe height="800px" width="100%" src="../../Compartidas/VisorReportes/VisorReportes.aspx"   frameborder="0"></iframe>
                </div>
            </asp:View>
        </asp:MultiView>
    </asp:Panel>
    <br />
    <br />
    <asp:Button ID="btnAtras" runat="server" Text="Atrás" style="text-align: center" CssClass="ButtonNeutro" OnClick="btnAtras_Click" />
</asp:Content>
