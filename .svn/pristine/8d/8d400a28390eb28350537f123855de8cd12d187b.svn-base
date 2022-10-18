<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmReporteTitulosGarantia.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmReporteTitulosGarantia" EnableEventValidation="false" %>

<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>
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
        <h3>Reporte de Títulos en Garantía:</h3>
        Criterios de búsqueda:<br />
        <div class="col-md-12" style="margin-top: 2%;">
        <div class="col-md-6"  style="margin-top: 1%;">
            <div class="col-md-4">Moneda:</div>
            <div class="col-md-6">   
                <asp:DropDownList ID="ddlMoneda" runat="server" AppendDataBoundItems="True" AutoPostBack="True"  CssClass="FormatoDropDownList">
                        <asp:ListItem Value="">-- Seleccione Opcion --</asp:ListItem>
                        <asp:ListItem Value="CRCN">Colones (CRC)</asp:ListItem>
                        <asp:ListItem Value="USD">Dólares (USD)</asp:ListItem>
                        <asp:ListItem Value="UDE">Unidades De Desarrollo (UDE)</asp:ListItem>
                    </asp:DropDownList>
            </div>
        </div>
         <div class="col-md-6"  style="margin-top: 1%;">
            <div class="col-md-4">Fecha hasta:</div>
            <div class="col-md-6">
                <asp:TextBox ID="txtFchFin" runat="server" placeholder="dd/mm/aaaa" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
        </div>
        </div>
        <div class="col-md-6"  style="margin-top: 1%;">
            <div class="col-md-4">Fecha desde:</div>
            <div class="col-md-6">
                <asp:TextBox ID="txtFchInicio" runat="server" placeholder="dd/mm/aaaa" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>
    </div>
        <div class="col-md-12" style="text-align:center; margin-top:3%;">
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
