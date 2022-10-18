<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmReporteSaldosDeudaExt.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.Reportes.frmReporteSaldosDeudaExt" EnableEventValidation="false" %>

<%@ Register assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2" namespace="eWorld.UI" tagprefix="ew" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <script  type="text/javascript" src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
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
         <div class="col-md-12">  
             <h3>Reporte de Saldos de Deuda Externa:</h3>
        </div>
           <div class="col-md-12 divStyle">
        <div class="col-md-6 divStyle">           
                <div class="col-md-4">Número de préstamo:</div>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlNroPrestamo" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="FormatoDropDownList" OnSelectedIndexChanged="ddENroPrestamo_SelectedIndexChanged" >
                        </asp:DropDownList>
                      <%--  <asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                     DataSourceID="NumeroPrestamo" DataTextField="IdPrestamo" DataValueField="IdPrestamo" 
                    <asp:SqlDataSource ID="NumeroPrestamo" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT distinct([IdPrestamo]) FROM [cf].[Prestamos]"></asp:SqlDataSource>--%>
                </div>
         </div>
         <div class="col-md-6 divStyle">   
                <div class="col-md-4">Número de Tramo:</div>
                <div class="col-md-6">
                     <asp:DropDownList ID="ddENumeroTramo" runat="server"  CssClass="FormatoDropDownList"></asp:DropDownList>
                </div>
         </div>
         <div class="col-md-6 divStyle"> 
             <div class="col-md-4">
                 Tipo de Moneda
             </div>
                <div class="col-md-6">
                      <asp:DropDownList ID="ddlMonedas" runat="server" DataTextField="IdMoneda" DataValueField="IdMoneda" AppendDataBoundItems="True" AutoPostBack="True"  CssClass="FormatoDropDownList" >

                        <asp:ListItem Value="" Selected="True">Original</asp:ListItem>
                          <asp:ListItem>CRC</asp:ListItem>
                          <asp:ListItem>USD</asp:ListItem>
                     </asp:DropDownList>
               <%--<asp:SqlDataSource ID="TipoMoneda" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT distinct([IdMoneda]) FROM [ma].[Monedas]"></asp:SqlDataSource>--%>
                </div>
             </div>

        <div class="col-md-6 divStyle">
             <div class="col-md-4">Fecha Desde:</div>
                <div class="col-md-6"><asp:TextBox ID="txtFchDesde" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6 divStyle"> 
                 <div class="col-md-4">Fecha Hasta:</div>
                <div class="col-md-6">
                    <asp:TextBox ID="txtFchHasta" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                </div>
        </div>

        <div class="col-md-12 divStyle" style="text-align:center; margin-bottom: 2%;">
             <asp:Button ID="btnImprimir" runat="server" CssClass="ButtonNeutro" Text="Ver Reporte" Width="200px" OnClick="btnImprimir_Click"/>
        </div>
       </div>
       
        <asp:Panel ID="PanelReporteSaldosDeudaExterna" runat="server">
            <div align="center">
                <iframe height="800px" width="100%" src="../../../Compartidas/VisorReportes/VisorReportes.aspx" frameborder="0"></iframe>
            </div>
        </asp:Panel>
           </div>
</asp:Content>
