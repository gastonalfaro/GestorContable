<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmRptPagosExpedientes.aspx.cs" Inherits="Presentacion.CapturaIngresos.Reportes.frmRptPagosExpedientes" %>
<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    <script>

        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=500,toolbar=0,scrollbars=0,status=0,dir=ltr');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div id="datos">
        <div class="col-md-12">
            <h3>Reporte de Pagos de Contingencias:</h3>
            Criterios de búsqueda:<br /><br />
        </div>
     

       <div class="col-md-6">					
            <div class="row">
                <div class="col-md-3">Fecha Inicio:</div>
                <div class="col-md-7"><asp:TextBox ID="txtFechaInicio" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
            </div>
        </div>
        <div class="col-md-6">					
            <div class="row">
                <div class="col-md-3">Fecha Final:</div>
                <div class="col-md-7"><asp:TextBox ID="txtFechaFin" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
            </div>
        </div>
       
        <div class="col-md-6">					
            <div class="row">
                 <br />
         <br />
                <div class="col-md-3">Consulta:</div>
                <div class="col-md-7">
                <asp:DropDownList ID="ddlSinExpediente" runat="server"  TextMode="Text" CssClass="FormatoDropDownList">
                    <asp:ListItem Value="N">Sin Expediente</asp:ListItem>
                    <asp:ListItem Value="S">Con Expediente</asp:ListItem>
                    <asp:ListItem Value="T">Todos</asp:ListItem>
                </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-md-12" style="text-align:center;">
             <asp:Button ID="btnImprimir" runat="server" Text="Ver Reporte" Width="200px" OnClick="btnImprimir_Click" CssClass="ButtonNeutro" />
        </div>
        <br />
        <asp:Panel ID="PanelReporte" runat="server" Visible="false">
            <div align="center">
                <iframe height="800px" width="100%" src="../../Compartidas/VisorReportes/VisorReportes.aspx"   frameborder="0"></iframe>
            </div>
        </asp:Panel>

    </div>
</asp:Content>

