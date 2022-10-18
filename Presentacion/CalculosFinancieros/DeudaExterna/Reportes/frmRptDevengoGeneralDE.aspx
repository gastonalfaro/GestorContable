<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmRptDevengoGeneralDE.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.Reportes.frmRptDevengoGeneralDE" %>
<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 82px;
        }
        .auto-style3 {
            width: 211px;
        }
    </style>
    <style type="text/css">
        .divStyle {margin-top:1%; }
    </style>

    <style type="text/css"> .contenido { width:90%!important; } </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
        <div id="datos">
            <h3>Reporte Devengo General Deuda Externa:</h3>
            Criterios de búsqueda :<br />
            <div class="col-md-12 divStyle">
                <div class="col-md-6 divStyle">
                    <div class="col-md-3">Fecha hasta:</div>
                    <div class="col-md-6"><asp:TextBox ID="txtFechaFin" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
                </div>
              
                <div class="col-md-12 divStyle" style="text-align:center; margin-bottom:2%;">
                    <asp:Button ID="btnImprimir" runat="server" Text="Ver Reporte" Width="200px" OnClick="btnImprimir_Click" CssClass="ButtonNeutro" />
                </div>
            </div>
       
        <asp:Panel ID="PanelReporte" runat="server">
            <div align="center">
                <iframe height="800px" width="100%" src="../../../Compartidas/VisorReportes/VisorReportes.aspx"   frameborder="0"></iframe>
            </div>
        </asp:Panel>

    </div>
</asp:Content>
