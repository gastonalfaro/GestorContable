<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmReporteReversionesDevengoCanc.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmReporteReversionesDevengoCanc" EnableEventValidation="false" %>

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
        <h3>Reporte de Reversiones de Devengo de Cancelaciones:</h3>
        Criterios de búsqueda:<br />
        <table style="width: 100%;">
            <tr>
                <td>Nemotécnico:</td>
                <td>
                    <asp:DropDownList runat="server" Width="200px" ID="ddlNemotecnico"  CssClass="chzn-select FormatoDropDownList" AutoPostBack="True"  AppendDataBoundItems="True" OnSelectedIndexChanged="ddlNemotecnico_SelectedIndexChanged">
                        <asp:ListItem Value="">-- Seleccione Opcion --</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>Número de Valor:</td>
                <td>
                <asp:DropDownList ID="ddlNumValor" runat="server" CssClass="FormatoDropDownList" Width="200px" AppendDataBoundItems="True" AutoPostBack="True">
                    <asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td id="ImprimirRep" colspan="5" align="center">
                    <asp:Button ID="btnImprimir" runat="server" Text="Ver Reporte" Width="200px" OnClick="btnImprimir_Click" CssClass="ButtonNeutro" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Panel ID="PanelReporte" runat="server">
            <div style="overflow-x: auto; width: auto" align="center">
                <iframe height="800px" width="100%" src="../../Compartidas/VisorReportes/VisorReportes.aspx"   frameborder="0"></iframe>
            </div>
        </asp:Panel>

    </div>
</asp:Content>
