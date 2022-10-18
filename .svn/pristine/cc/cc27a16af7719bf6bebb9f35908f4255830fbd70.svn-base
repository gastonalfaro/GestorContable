<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmReporteDevengo.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmReporteDevengo" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
            $("#bt1").click(function () {
                $("#prueba").hide();
            });
            $(".btn2").click(function () {
                $("#prueba").show();
            });
        });
        function SearchText() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    <script>
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=500,toolbar=0,scrollbars=0,status=0,dir=ltr,size=landscape');
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
        <h3>Reporte de Devengo:</h3>
        Criterios de búsqueda:<br />
        <table style="width: 100%;">
            <tr>
                <td>Nemotécnicos:</td>
                <td>
                    <asp:DropDownList ID="ddlNemotecnico" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlNemotecnico_SelectedIndexChanged" Width="200px">
                     </asp:DropDownList>
                       <%--<asp:ListItem>-- Seleccione Valor --</asp:ListItem> DataSourceID="Nemotecnicos" DataTextField="NomNemotecnico" DataValueField="IdNemotecnico" 
                    <asp:SqlDataSource ID="Nemotecnicos" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT CONCAT([NomNemotecnico], ' (',rtrim(ltrim([IdNemotecnico])),')') as NomNemotecnico, rtrim(ltrim([IdNemotecnico])) as IdNemotecnico
FROM [ma].[Nemotecnicos] as A
inner join (select distinct(Nemotecnico) from cf.titulosvalores) as B
on a.IdNemotecnico = b.Nemotecnico;"></asp:SqlDataSource>--%>
                </td>
                <td>&nbsp;</td>
                <td>Número de Valor:</td>
                <td>
                    <asp:DropDownList ID="ddlNroValor" runat="server" AppendDataBoundItems="True" AutoPostBack="True"  OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" Width="200px">
                    </asp:DropDownList>
                       <%-- <asp:ListItem>-- Seleccione Valor --</asp:ListItem>DataSourceID="NumeroValor" DataTextField="NroValor" DataValueField="NroValor"
                    <asp:SqlDataSource ID="NumeroValor" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT [NroValor] FROM [cf].[TitulosValores]
where [Nemotecnico] = @Nemotecnico">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlNemotecnico" Name="Nemotecnico" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>--%>
                </td>
            </tr>
            <tr>
                <td id="ImprimirRep" colspan="5" align="center">
                    <asp:Button ID="btnReporte" runat="server" BackColor="#3366CC" ForeColor="White" OnClick="btnReporte_Click" Text="Consultar" Width="200px" />
                    <asp:Button ID="btnImprimir" runat="server" BackColor="#3366CC" ForeColor="White" Text="Imprimir Reporte" Width="200px" OnClientClick="CallPrint('datos')" OnClick="btnImprimir_Click" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <br />
        Cupón Corrido:
        <asp:Label ID="lblCuponCorrido" runat="server" Text="0.0"></asp:Label>
        <br />
        <br />
        Flujo de Efectivo:
        <div style="overflow-x: auto; width: auto" align="center">
        <asp:GridView ID="grvFlujo" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" Font-Size="X-Small" EmptyDataText="No se encontraron registros para la búsqueda generada" OnSorting="grvOperacionesEspeciales_Sorted">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Periodo" HeaderText="Periodo" SortExpression="Periodo" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="NroValor" HeaderText="Número Valor" SortExpression="NroValor" />
                    <asp:BoundField DataField="Nemotecnico" HeaderText="Nemotécnico" SortExpression="Nemotecnico" />
                    <asp:BoundField DataField="TasaInteres" HeaderText="Tasa Interes" SortExpression="TasaInteres" DataFormatString="{0:P1}" />
                    <asp:BoundField DataField="Intereses" HeaderText="Intereses" SortExpression="Intereses" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="FlujoEfectivo" HeaderText="Flujo de Efectivo" SortExpression="FlujoEfectivo" DataFormatString="{0:N}" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
                    <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Text="Flujo a Excel" Width="200px" BackColor="#3366CC" ForeColor="White" />
            </div>
        <br />
        Devengo: <div style="overflow-x: auto; width: auto" align="center">
            <asp:GridView ID="grvDevengo" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" Font-Size="X-Small" EmptyDataText="No se encontraron registros para la búsqueda generada" OnSorting="grvOperacionesEspeciales_Sorted">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Anno" HeaderText="Periodo" SortExpression="Anno" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="NroValor" HeaderText="Número Valor" SortExpression="NroValor" />
                    <asp:BoundField DataField="Nemotecnico" HeaderText="Nemotécnico" SortExpression="Nemotecnico" />
                    <asp:BoundField DataField="CostoAmortizacionInicial" HeaderText="Amortización Inicial" SortExpression="CostoAmortizacionInicial" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="Intereses" HeaderText="Intereses" SortExpression="Intereses" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="Pago" HeaderText="Pago" SortExpression="Pago" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="CostoAmortizacionFinal" HeaderText="Amortización Final" SortExpression="CostoAmortizacionFinal" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="DescuentoDevengado" HeaderText="Monto Devengado" SortExpression="DescuentoDevengado" DataFormatString="{0:N}" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>

            <asp:Button ID="btnExcelDevengo" runat="server" BackColor="#3366CC" ForeColor="White" OnClick="btnExcelDevengo_Click" Text="Devengo a Excel" Width="200px" />

        </div>
    </div>
    <br />
    Devengo Mensual:<div style="overflow-x: auto; width: auto" align="center">
            <asp:GridView ID="grvDevengoMens" runat="server" CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" Font-Size="X-Small" EmptyDataText="No se encontraron registros para la búsqueda generada" OnSorting="grvOperacionesEspeciales_Sorted">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Anno" HeaderText="Periodo" SortExpression="Anno" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="NroValor" HeaderText="Número Valor" SortExpression="NroValor" />
                    <asp:BoundField DataField="Nemotecnico" HeaderText="Nemotécnico" SortExpression="Nemotecnico" />
                    <asp:BoundField DataField="CostoAmortizacionInicial" HeaderText="Amortización Inicial" SortExpression="CostoAmortizacionInicial" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="Intereses" HeaderText="Intereses" SortExpression="Intereses" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="Pago" HeaderText="Pago" SortExpression="Pago" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="CostoAmortizacionFinal" HeaderText="Amortización Final" SortExpression="CostoAmortizacionFinal" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="DescuentoDevengado" HeaderText="Monto Devengado" SortExpression="DescuentoDevengado" DataFormatString="{0:N}" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>

            <asp:Button ID="btnExcelDevengoMens" runat="server" BackColor="#3366CC" ForeColor="White" OnClick="btnExcelDevengoMens_Click" Text="Devengo Mensual a Excel" Width="200px" />

        </div>
    <br />
</asp:Content>
