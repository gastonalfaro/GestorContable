<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmReporteMovimientosDE.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.Reportes.frmReporteMovimientosDE" EnableEventValidation="false"%>

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
    <style type="text/css">
        .divStyle {margin-top:1%; }
    </style>

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
             <h3>Reporte de Movimientos con su relación presupuestaria de Deuda Externa:</h3>
                Criterios de búsqueda:<br />
        </div>
        <div class="col-md-12 divStyle">
            <div class="col-md-6 divStyle">
                <div class="col-md-4">Número de préstamo:</div>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddmNumeroPrestamo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"  CssClass="FormatoDropDownList" OnSelectedIndexChanged="ddmNumeroPrestamo_SelectedIndexChanged" >
                    </asp:DropDownList>
                       <%-- <asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                     DataSourceID="NumeroPrestamo" DataTextField="IdPrestamo" DataValueField="IdPrestamo"

                     <asp:SqlDataSource ID="NumeroPrestamo" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT distinct([IdPrestamo]) FROM [cf].[Prestamos]"></asp:SqlDataSource>--%>

                </div>
               </div>
              <div class="col-md-6 divStyle">
                <div class="col-md-4">Número de Tramo:</div>
                <div class="col-md-6">
                                    <asp:DropDownList ID="ddmNumeroTramo" runat="server"  CssClass="FormatoDropDownList">
                </asp:DropDownList></div>
     </div>

         <div class="col-md-6 divStyle">
         
             <div class="col-md-4">Fecha Desde:</div>
                <div class="col-md-6"><asp:TextBox ID="txtFchDesde" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
             </div>
              <div class="col-md-6 divStyle">
                 <div class="col-md-4">Fecha Hasta:</div>
                <div class="col-md-6"><asp:TextBox ID="txtFchHasta" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>

        </div>

        <div class="col-md-12 divStyle" style="text-align:center; margin-bottom:2%;">
             <asp:Button ID="btnImprimir" runat="server" CssClass="ButtonNeutro" Text="Ver Reporte" Width="200px" OnClick="btnImprimir_Click"/>
        </div>
       </div>

        <asp:Panel ID="PanelReporteMovimientosDE" runat="server">
            <div align="center">
                <iframe height="800px" width="100%" src="../../../Compartidas/VisorReportes/VisorReportes.aspx" frameborder="0"></iframe>
            </div>
        </asp:Panel>
        <br />
    </div>
</asp:Content>
