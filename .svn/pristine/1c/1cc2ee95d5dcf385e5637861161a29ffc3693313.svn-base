<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPaginaReportes.Master" AutoEventWireup="true" CodeBehind="frmReporteCancelaciones.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmReporteCancelaciones" EnableEventValidation="false" %>

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
        <h3>Reporte de Cancelaciones:</h3>
        Criterios de búsqueda:<br />
        <table style="width: 100%;">
            <tr>
                <td>Descripción:</td>
                <td>
                    <asp:TextBox ID="txtDescripcion" runat="server" ReadOnly="True" Width="200px">Cancelacion</asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>Tipo de Negociación:</td>
                <td>
                    <asp:DropDownList ID="ddlTipoNegociacion" runat="server" AppendDataBoundItems="True" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlTipoNegociacion_SelectedIndexChanged">
                      </asp:DropDownList>
                      <%--<asp:ListItem Value="0">-- Seleccione Opcion --</asp:ListItem>
                    DataSourceID="TipoNegociacion" DataTextField="NomOpcion" DataValueField="ValOpcion" 
                    <asp:SqlDataSource ID="TipoNegociacion" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT [NomOpcion], [ValOpcion] FROM [cf].[vTipoNegociacionValores]"></asp:SqlDataSource>--%>
                </td>
            </tr>
            <tr>
                <td>Tipo de Valor:</td>
                <td>
                    <asp:DropDownList ID="ddlTipoValor" runat="server" AutoPostBack="True"  Width="200px" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlTipoValor_SelectedIndexChanged">
                    </asp:DropDownList>
                      <%--  <asp:ListItem Value="0">-- Seleccione Opcion --</asp:ListItem>DataSourceID="TiposValores" DataTextField="NomOpcion" DataValueField="ValOpcion"
                    <asp:SqlDataSource ID="TiposValores" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT [ValOpcion], [NomOpcion] FROM [cf].[vTiposValores]"></asp:SqlDataSource>--%>
                </td>
                <td>&nbsp;</td>
                <td>Nemotécnico</td>
                <td>
                    <asp:DropDownList runat="server" Width="200px" ID="ddlNemotecnico" AutoPostBack="True" OnSelectedIndexChanged="ddlNemotecnico_SelectedIndexChanged" AppendDataBoundItems="True">
                     </asp:DropDownList>
                     <%-- <asp:ListItem Value="0">-- Seleccione Opcion --</asp:ListItem>DataSourceID="Nemotecnicos" DataTextField="NomNemotecnico" DataValueField="IdNemotecnico" 
                     <asp:SqlDataSource ID="Nemotecnicos" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT [IdNemotecnico], CONCAT ([NomNemotecnico], ' (', REPLACE([IdNemotecnico],' ', ''), ')') AS [NomNemotecnico]FROM [ma].[Nemotecnicos]
WHERE [TipoNemotecnico] = @TipoNemotecnico">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlTipoValor" DefaultValue="CC" Name="TipoNemotecnico" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>--%>
                </td>
            </tr>
            <tr>
                <td>Fecha desde:</td>
                <td>
                    <asp:TextBox ID="txtFchDesde" runat="server" Width="200px" placeholder="dd/mm/yyyy" AutoPostBack="True" OnTextChanged="txtFchDesde_TextChanged" TextMode="Date"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>Fecha hasta:</td>
                <td>
                    <asp:TextBox ID="txtFchHasta" runat="server" Width="200px" placeholder="dd/mm/yyyy" AutoPostBack="True" OnTextChanged="txtFchHasta_TextChanged" TextMode="Date"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td id="ImprimirRep" colspan="5" align="center">
                    <asp:Button ID="btnImprimir" runat="server" BackColor="#3366CC" ForeColor="White" Text="Imprimir Reporte" Width="200px" OnClientClick="CallPrint('datos')" OnClick="btnImprimir_Click" />
                    <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Text="Exportar a Excel" Width="200px" BackColor="#3366CC" ForeColor="White" />
                </td>
            </tr>
        </table>
        <br />
        Columnas Disponibles:
        <table id="ColumnasDisponibles" style="width: 100%;">
            <tr>
                <td>
                    <asp:CheckBox ID="chkNroValor" runat="server" AutoPostBack="True" Text="Número Valor" OnCheckedChanged="chkNroValor_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkValorFacial" runat="server" AutoPostBack="True" Text="Valor Facial" OnCheckedChanged="chkValorFacial_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkFchValor" runat="server" AutoPostBack="True" Text="Fecha Valor" OnCheckedChanged="chkFchValor_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkFchColocacion" runat="server" AutoPostBack="True" Text="Fecha Colocación" OnCheckedChanged="chkFchColocacion_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkFchCancelacion" runat="server" AutoPostBack="True" Text="Fecha Cancelacion" OnCheckedChanged="chkFchCancelacion_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkNemotecnico" runat="server" AutoPostBack="True" Text="Nemotécnico" OnCheckedChanged="chkNemotecnico_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkMoneda" runat="server" AutoPostBack="True" Text="Moneda" OnCheckedChanged="chkMoneda_CheckedChanged" Checked="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkPropiedad" runat="server" AutoPostBack="True" Text="Propiedad" OnCheckedChanged="chkPropiedad_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkDescuento" runat="server" AutoPostBack="True" Text="Descuento" OnCheckedChanged="chkDescuento_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkPremio" runat="server" AutoPostBack="True" Text="Premio" OnCheckedChanged="chkPremio_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkMargen" runat="server" AutoPostBack="True" Text="Margen" OnCheckedChanged="chkTMargen_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkTransadoBruto" runat="server" AutoPostBack="True" Text="Transado Bruto" OnCheckedChanged="chkTransadoBruto_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkTransadoNeto" runat="server" AutoPostBack="True" Text="Transado Neto" OnCheckedChanged="chkTransadoNeto_CheckedChanged" Checked="True" />
                </td>
                <td>
                    <asp:CheckBox ID="chkTasaBruta" runat="server" AutoPostBack="True" Text="Tasa Bruta" OnCheckedChanged="chkTasaBruta_CheckedChanged" Checked="True" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td></td>
                <td>
                    <asp:CheckBox ID="chkTasaNeta" runat="server" AutoPostBack="True" Text="Tasa Neta" OnCheckedChanged="chkTasaNeta_CheckedChanged" Checked="True" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td></td>
            </tr>
        </table>
        <div style="overflow-x: auto; width: auto" align="center">
            <asp:GridView ID="grvCancelaciones" runat="server" CellPadding="4" ForeColor="#333333" Font-Size="X-Small" AutoGenerateColumns="False" AllowSorting="True" EmptyDataText="No se encontraron registros para la búsqueda generada" OnSorting="grvCancelaciones_Sorting" OnPageIndexChanging="grvCancelaciones_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="NroAsiento" HeaderText="Número Asiento" SortExpression="NroAsiento" />
                    <asp:BoundField DataField="FchContabilizacion" DataFormatString="{0:d}" HeaderText="Fecha Contabilización" SortExpression="FchContabilizacion" />
                    <asp:BoundField DataField="NroValor" HeaderText="Número Valor" SortExpression="NroValor" />
                    <asp:BoundField DataField="ValorFacial" HeaderText="Valor Facial" SortExpression="ValorFacial" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="FchValor" HeaderText="Fecha Valor" SortExpression="FchValor" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="FchColocacion" HeaderText="Fecha Colocación" SortExpression="FchColocacion" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="FchCancelacion" HeaderText="Fecha Cancelación" SortExpression="FchCancelacion" DataFormatString="{0:d}" />
                    <asp:BoundField DataField="FchVencimiento" DataFormatString="{0:d}" HeaderText="Fecha Vencimiento" SortExpression="FchVencimiento" />
                    <asp:BoundField DataField="Nemotecnico" HeaderText="Nemotécnico" SortExpression="Nemotecnico" />
                    <asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="Moneda" />
                    <asp:BoundField DataField="Propiedad" HeaderText="Propiedad" SortExpression="Propiedad" />
                    <asp:BoundField DataField="RendimientoPorDescuento" HeaderText="Descuento" SortExpression="RendimientoPorDescuento" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="Premio" HeaderText="Premio" SortExpression="Premio" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="Margen" HeaderText="Margen" SortExpression="Margen" />
                    <asp:BoundField DataField="ValorTransadoBruto" HeaderText="Transado Bruto" SortExpression="ValorTransadoBruto" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="ValorTransadoNeto" HeaderText="Transado Neto" SortExpression="ValorTransadoNeto" DataFormatString="{0:N}" />
                    <asp:BoundField DataField="TasaBruta" HeaderText="Tasa Bruta" SortExpression="TasaBruta" />
                    <asp:BoundField DataField="TasaNeta" HeaderText="Tasa Neta" SortExpression="TasaNeta" />
                    <asp:BoundField DataField="Plazo" HeaderText="Plazo" SortExpression="Plazo" />
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
        </div>
        <table style="width: 100%;">
            <tr>
                <td>Total Valor Facial: 
                    <asp:Label ID="lblFacial" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                Total Descuento:
                    <asp:Label ID="lblDescuento" runat="server"></asp:Label>
                &nbsp;</td>
            </tr>
            <tr>
                <td>Total Transado Bruto: 
                    <asp:Label ID="lblBruto" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Total Transado Neto:
                    <asp:Label ID="lblNeto" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
