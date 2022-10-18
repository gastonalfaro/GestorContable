<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmReporteHistorialTituloValor.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmReporteHistorialTituloValor" EnableEventValidation="false" %>

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
    <style type="text/css"> .contenido { width:90%!important; } </style>
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
          <div style="display:inline-block;">
            <h3>Reporte de Historial de Títulos:</h3>
            Criterios de búsqueda:<br />
               <div class="col-md-12" style="margin-top: 2%;">
            <div class="col-md-6" style="margin-top: 1%;">
                <div class="col-md-4">Nemotécnico:</div>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="ddlNemotecnico" AutoPostBack="True"  CssClass="chzn-select FormatoDropDownList" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlNemotecnico_SelectedIndexChanged">
                    <asp:ListItem Value="">-- Seleccione Opcion --</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6" style="margin-top: 1%;">
                <div class="col-md-4">Número de Valor:</div>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="ddlNumValor"  CssClass="chzn-select FormatoDropDownList"  AppendDataBoundItems="True" AutoPostBack="True">
                    <asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
          </div>
            <div class="col-md-12" id="ImprimirRep"  style="text-align:center; margin-top:3%;">
                <asp:Button ID="btnImprimir" runat="server" Text="Ver Reporte" Width="200px" OnClick="btnImprimir_Click" CssClass="ButtonNeutro" />
            </div>
        </div>   
        <br />
        <asp:Panel ID="PanelReporte" runat="server">
            <div align="center">
                <iframe height="800px" width="100%" src="../../Compartidas/VisorReportes/VisorReportes.aspx"   frameborder="0"></iframe>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
