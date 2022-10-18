<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Prestamos.aspx.cs" Inherits="LogicaNegocio.CalculosFinancieros.DeudaExterna.Prestamos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="div_busqueda">
                <table style="width: 300px;">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Buscar:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="opcion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="opcion_SelectedIndexChanged1" Style="width: 100%;">
                                <asp:ListItem Value="1">Por identificación</asp:ListItem>
                                <asp:ListItem Value="2">Por fuente</asp:ListItem>
                                <asp:ListItem Value="3">Por situación</asp:ListItem>
                                <asp:ListItem Value="4">Por plazo</asp:ListItem>
                                <asp:ListItem Value="5">Por nombre</asp:ListItem>
                                <asp:ListItem Value="6">Por fecha</asp:ListItem>
                                <asp:ListItem Value="7">Por acreedor</asp:ListItem>
                                <asp:ListItem Value="8">Por deudor</asp:ListItem>
                                <asp:ListItem Value="9" Selected="True">Por tipo</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblValor1" runat="server" Text="Valor 1:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="valor1" runat="server" Style="width: 100%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblValor2" runat="server" Text="Valor 2:" Visible="False"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="valor2" runat="server" Visible="False" Style="width: 100%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" />
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" style="height: 26px" Text="Button" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="div_Prestamos" style="width: 100%; height: 300px; overflow: auto">
                <asp:GridView ID="gv" runat="server" OnRowCommand="gv_RowCommand" AutoGenerateColumns="False" OnSelectedIndexChanged="gv_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:Button ID="btnConsultar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Editar" runat="server" Text="Editar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Eliminar">
                            <ItemTemplate>
                                <asp:Button ID="btnEliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Eliminar" runat="server" Text="Eliminar" OnClientClick="return confirm('¿Seguro que desea eliminar el nuevo préstamo?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IdPrestamo" HeaderText="Id de Préstamo" />
                        <asp:BoundField DataField="Fuente" HeaderText="Fuente" />
                        <asp:BoundField DataField="Situacion" HeaderText="Situación" />
                        <asp:BoundField DataField="Plazo" HeaderText="Plazo" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="FchFirmado" HeaderText="Fecha de Firmado" />
                        <asp:BoundField DataField="LimiteGiro" HeaderText="Fecha Límite de Giro" />
                        <asp:BoundField DataField="LimiteEfectivo" HeaderText="Fecha Límite de Efectivo" />
                        <asp:BoundField DataField="Efectivo" HeaderText="Fecha de Efectivo" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto" />
                        <asp:BoundField DataField="IdMoneda" HeaderText="Moneda" />
                        <asp:BoundField DataField="TpoTramo" HeaderText="Tipo de Tramo" />
                        <asp:BoundField DataField="Proposito" HeaderText="Propósito" />
                        <asp:BoundField DataField="GarantiaPublica" HeaderText="Garantía Pública" />
                        <asp:BoundField DataField="OrigenDeuda" HeaderText="Origen de la Deuda" />
                        <asp:BoundField DataField="IdAcreedor" HeaderText="Acreedor" />
                        <asp:BoundField DataField="IdDeudor" HeaderText="Deudor" />
                        <asp:BoundField DataField="TpoPrestamo" HeaderText="Tipo de Préstamo" />
                        <asp:BoundField DataField="Tasa" HeaderText="Tasa" />
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            </asp:GridView>
            <br />
            <br />
            Datos del préstamo<br />
            <br />
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:Label ID="lblIdPrestamo" runat="server" Text="Id de préstamo"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdPrestamo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblFuente" runat="server" Text="Fuente"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFuente" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSituacion" runat="server" Text="Situación"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSituacion" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblPlazo" runat="server" Text="Plazo"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPlazo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblFechaFirmado" runat="server" Text="Fecha de firmado"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFechaFirmado" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLimiteGiro" runat="server" Text="Fecha límite giro"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLimiteGiro" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblLimiteEfectivo" runat="server" Text="Fecha límite efectivo"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLimiteEfectivo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEfectivo" runat="server" Text="Fecha efectivo"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEfectivo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblMonto" runat="server" Text="Monto"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIdMoneda" runat="server" Text="Moneda"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdMoneda" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblTpoTramo" runat="server" Text="Tipo de tramo"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTpoTramo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProposito" runat="server" Text="Propósito"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProposito" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblGarantiaPublica" runat="server" Text="Garantía pública"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGarantiaPublica" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOrigenDeuda" runat="server" Text="Origen de la deuda"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrigenDeuda" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblIdAcreedor" runat="server" Text="Acreedor"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdAcreedor" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblIdDeudor" runat="server" Text="Deudor"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtIdDeudor" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblTpoPrestamo" runat="server" Text="Tipo de préstamo"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTpoPrestamo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTasa" runat="server" Text="Tasa"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTasa" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar Cambios" OnClick="btnGuardarCambios_Click" OnClientClick="return confirm('¿Seguro que desea guardar los cambios?');" />
                        <asp:Button ID="btnCrearPrestamo" runat="server" Text="Guardar Prestamo" OnClientClick="return confirm('¿Seguro que desea almacenar el nuevo préstamo?');" OnClick="btnCrearPrestamo_Click" />

                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:TextBox ID="TextBox1" runat="server" Height="86px" TextMode="MultiLine" Width="254px"></asp:TextBox>
            <br />
            <br />
        </div>
    </form>
</body>
</html>
