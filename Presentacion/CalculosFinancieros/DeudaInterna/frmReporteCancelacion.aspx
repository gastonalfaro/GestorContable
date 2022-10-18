<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmReporteCancelacion.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmReporteCancelacion" EnableEventValidation="false" %>

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
        <div>
            <h3>Reporte de Cancelaciones:</h3>
            Criterios de búsqueda:<br />

            <div class="col-md-12" style="margin-top: 2%;">
                <div class="col-md-6" style="margin-top: 1%;">
                    <div class="col-md-4">Nemotécnico:</div>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" ID="ddlNemotecnico" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlNemotecnico_SelectedIndexChanged" CssClass="chzn-select FormatoDropDownList">
                            <asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
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
                    <div class="col-md-4">Tipo de Valor:</div>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlTipoValor" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="FormatoDropDownList">
                        </asp:DropDownList>
                        <%--<asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem> DataSourceID="TipoValor" DataTextField="NomOpcion" DataValueField="ValOpcion"  
                    <asp:SqlDataSource ID="TipoValor" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT [ValOpcion], [NomOpcion] FROM [cf].[vTiposValores]"></asp:SqlDataSource></div>--%>
                    </div>
                </div>
                <div class="col-md-6" style="margin-top: 1%;">
                    <div class="col-md-4">Tipo de Negociación:</div>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlTipoNegociacion" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="FormatoDropDownList">
                        </asp:DropDownList>
                        <%--<asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                    DataSourceID="TipoNegociacion" DataTextField="NomOpcion" DataValueField="ValOpcion" 
                    <asp:SqlDataSource ID="TipoNegociacion" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT [NomOpcion], [ValOpcion] FROM [cf].[vTiposNegociacion]"></asp:SqlDataSource>--%>
                    </div>
                </div>

                <div class="col-md-6" style="margin-top: 1%;">
                    <div class="col-md-4">Moneda:</div>
                    <div class="col-md-6">
                        <asp:DropDownList ID="ddlMoneda" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="FormatoDropDownList">
                        </asp:DropDownList>
                        <%--<asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                    DataSourceID="Moneda" DataTextField="IdMoneda" DataValueField="IdMoneda" 
                    <asp:SqlDataSource ID="Moneda" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT [IdMoneda] FROM [ma].[Monedas] WHERE [IdMoneda] IN ('USD','CRC','UDE','EUR') "></asp:SqlDataSource>
                        --%>
                    </div>
                </div>
                <div class="col-md-6" style="margin-top: 1%;">
                    <div class="col-md-4">Cuenta Contable:</div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtCuenta" runat="server" AutoPostBack="True" CssClass="FormatoTextBox">
                        </asp:TextBox>
                    </div>
                </div>

                <div class="col-md-6" style="margin-top: 1%;">
                    <div class="col-md-4">Fecha desde:</div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFchInicio" runat="server" placeholder="dd/mm/aaaa" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-6" style="margin-top: 1%;">
                    <div class="col-md-4">Fecha hasta:</div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFchFin" runat="server" placeholder="dd/mm/aaaa" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="text-align: center; margin-top: 2%;" id="ImprimirRep">
                <asp:Button ID="btnImprimir" runat="server" Text="Ver Reporte" OnClick="btnImprimir_Click" CssClass="ButtonNeutro" />
            </div>
        </div>

        <br />
        <asp:Panel ID="PanelReporte" runat="server">
            <div>
                <iframe height="800px" width="100%" src="../../Compartidas/VisorReportes/VisorReportes.aspx" frameborder="0"></iframe>
            </div>
        </asp:Panel>
        <br />
    </div>
</asp:Content>
