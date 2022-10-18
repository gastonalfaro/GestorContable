<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmReporteDevengoGeneral.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmReporteDevengoGeneral" EnableEventValidation="false" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" Namespace="eWorld.UI" TagPrefix="ew" %>

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
    <style type="text/css">
        .contenido {
            width: 90% !important;
        }
    </style>
    <script type="text/javascript">

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
         <div style="display: inline-block;">
        <h3>Reporte de Devengo General</h3>
        Criterios de búsqueda:<br />

        <div class="col-md-12" style="margin-top: 2%;">
            <div class="col-md-6" style="margin-top: 1%;">
                <div class="col-md-4">Nemotécnico:</div>
                <div class="col-md-6">
                    <asp:DropDownList runat="server" ID="ddlNemotecnico" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlNemotecnico_SelectedIndexChanged" CssClass="chzn-select FormatoDropDownList">
                        <asp:ListItem Value="">-- Seleccione Opcion --</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6" style="margin-top: 1%;">
                <div class="col-md-4">Número de Valor:</div>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlNumValor" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="FormatoDropDownList">
                        <asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6" style="margin-top: 1%;">
                <div class="col-md-4">Tipo:</div>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlTipo" runat="server" AppendDataBoundItems="True" CssClass="FormatoDropDownList" OnSelectedIndexChanged="ddlTipo_SelectedIndexChanged">
                    </asp:DropDownList>
                    <%--<asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                    DataSourceID="TipoValor" DataTextField="NomOpcion" DataValueField="NomOpcion" 
                    <asp:SqlDataSource ID="TipoValor" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT * FROM [cf].[vTiposValores]"></asp:SqlDataSource>--%>
                </div>
            </div>
            <div class="col-md-6" style="margin-top: 1%;">
                <div class="col-md-4">Moneda:</div>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlMoneda" runat="server" AppendDataBoundItems="True" CssClass="FormatoDropDownList">
                    </asp:DropDownList>
                    <%--<asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem> DataSourceID="Monedas" DataTextField="NomMoneda" DataValueField="IdMoneda"
                     <asp:SqlDataSource ID="Monedas" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT [NomMoneda], rtrim(ltrim([IdMoneda])) as IdMoneda FROM [ma].[Monedas]
                    where [IdMoneda] in ('JPY', 'CRCN', 'USD', 'EUR','UDE')
                    order by [NomMoneda]"></asp:SqlDataSource>--%>
                </div>
            </div>
            <div class="col-md-6" style="margin-top: 1%;">
                <div class="col-md-4">Fecha de Inicio:</div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtFchInicio" runat="server" placeholder="dd/mm/aaaa" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6" style="margin-top: 1%;">
                <div class="col-md-4">Fecha de Fin:</div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtFchFin" runat="server" placeholder="dd/mm/aaaa" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="col-md-12" style="text-align: center; margin-top: 2%;">
            <asp:Button ID="btnImprimir" runat="server" Text="Ver Reporte" Width="200px" OnClick="btnImprimir_Click" CssClass="ButtonNeutro" />
        </div>
       <br />
          </div>
        <asp:Panel ID="PanelReporte" runat="server">
            <div style="overflow: auto; width: auto" align="center">
                <iframe height="800px" width="100%" style="display: block; overflow: auto; white-space: nowrap" src="../../Compartidas/VisorReportes/VisorReportes.aspx" frameborder="0"></iframe>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
