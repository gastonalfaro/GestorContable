<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmPrestamos.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.frmPrestamos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
    <style type="text/css">
        .divStyle {
            margin-top: 1%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12">
        <h3>Préstamos</h3>
    </div>
    <div class="col-md-12">Consulta de Préstamos del Sistema Gestor.</div>

    <div class="col-md-12 divStyle">
        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="Label1" runat="server" Text="Id Préstamo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdPrestamo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="FechaInicioLabel" runat="server" Text="Fecha Inicio: "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="FechaFinLabel" runat="server" Text="Fecha Fin: "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtFechaFin" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="lblFuente" runat="server" Text="Fuente:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtFuente" runat="server" MaxLength="50" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="lblSituacion" runat="server" Text="Situación:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtSituacion" runat="server" MaxLength="50" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="lblPlazo" runat="server" Text="Plazo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtPlazo" runat="server" MaxLength="50" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="30" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="lblNombreAcreedor" runat="server" Text="Nombre Acreedor:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtNombreAcreedor" runat="server" MaxLength="100" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="lblCatAcreedor" runat="server" Text="Categoría Acreedor:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtCatAcreedor" runat="server" MaxLength="100" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="lblTipoAcreedor" runat="server" Text="Tipo Acreedor:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtTipoAcreedor" runat="server" MaxLength="100" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="lblNombreDeudor" runat="server" Text="Nombre Deudor:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtNombreDeudor" runat="server" MaxLength="100" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="lblCatDeudor" runat="server" Text="Categoría Deudor:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtCatDuedor" runat="server" MaxLength="100" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="lblTipoPrestamo" runat="server" Text="Tipo Préstamo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtTipoPrestamo" runat="server" MaxLength="20" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-12" style="margin-bottom:2%; margin-top:2%;">
            <div style="text-align: center;">
                <asp:Button ID="btnFormulariosConsultar" runat="server" Text="CONSULTAR" OnClick="btnFormulariosConsultar_Click" CssClass="ButtonNeutro" />
            </div>
        </div>

        <div class="col-md-12 divStyle">
            <asp:Label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true"></asp:Label>
        </div>
    </div>

    <div style="width: 100%; height: 100%; overflow: auto">
        <div class="divStyle">
            <asp:GridView ID="grdvFormularios" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                Width="100%" CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                PageSize="15" AllowPaging="True" OnPageIndexChanging="grdvFormularios_PageIndexChanging">
                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <Columns>

                    <asp:TemplateField HeaderText="Id Préstamo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIdPrestamo" runat="server" Text='<%# Bind("IdPrestamo") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtInsertIdPrestamo" runat="server" Text='<%# Bind("IdPrestamo") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fuente" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblFuente" runat="server" Text='<%# Bind("Fuente") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtFuente" runat="server" Text='<%# Bind("Fuente") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Situación" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSituacion" runat="server" Text='<%# Bind("Situacion") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtSituacion" runat="server" Text='<%# Bind("Situacion") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Plazo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPlazo" runat="server" Text='<%# Bind("Plazo") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtPlazo" runat="server" Text='<%# Bind("Plazo") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nombre" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNombre" runat="server" Text='<%# Bind("Nombre") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Firmado" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtFchFirmado" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFchFirmado" runat="server" Text='<%# Eval("FchFirmado").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Límite Giro" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtLimiteGiro" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLimiteGiro" runat="server" Text='<%# (Eval("LimiteGiro")??"").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Límite Efectivo" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtLimiteEfectivo" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblLimiteEfectivo" runat="server" Text='<%# (Eval("LimiteEfectivo")??"").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Efectivo" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtEfectivo" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEfectivo" runat="server" Text='<%# (Eval("Efectivo")??"").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <%--           <asp:TemplateField HeaderText="Monto" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtInsertMonto" runat="server" Text='<%# Bind("Monto") %>' MaxLength="32" />
                                </FooterTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>--%>

                    <asp:BoundField DataField="Monto" DataFormatString="{0:N}" HeaderText="Monto">

                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIdMoneda" runat="server" Text='<%# Bind("IdMoneda") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtInsertIdMoneda" runat="server" Text='<%# Bind("IdMoneda") %>' MaxLength="3" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo Tramo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblTipoTramo" runat="server" Text='<%# Bind("TipoTramo") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtInsertTipoTramo" runat="server" Text='<%# Bind("TipoTramo") %>' MaxLength="50" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Propósito" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblProposito" runat="server" Text='<%# Bind("Proposito") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtInsertProposito" runat="server" Text='<%# Bind("Proposito") %>' MaxLength="100" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Garantía Pública" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblGarantiaPublica" runat="server" Text='<%# Bind("GarantiaPublica") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtInsertGarantiaPublica" runat="server" Text='<%# Bind("GarantiaPublica") %>' MaxLength="50" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Origen Deuda" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblOrigenDeuda" runat="server" Text='<%# Bind("OrigenDeuda") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtInsertOrigenDeuda" runat="server" Text='<%# Bind("OrigenDeuda") %>' MaxLength="50" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nombre Acreedor" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNbrAcreedor" runat="server" Text='<%# Bind("NbrAcreedor") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNbrAcreedor" runat="server" Text='<%# Bind("NbrAcreedor") %>' MaxLength="100" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Categoría Acreedor" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCatAcreedor" runat="server" Text='<%# Bind("CatAcreedor") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCatAcreedor" runat="server" Text='<%# Bind("CatAcreedor") %>' MaxLength="100" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo Acreedor" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblTipoAcreedor" runat="server" Text='<%# Bind("TipoAcreedor") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTipoAcreedor" runat="server" Text='<%# Bind("TipoAcreedor") %>' MaxLength="100" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nombre Deudor" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblNbrDeudor" runat="server" Text='<%# Bind("NbrDeudor") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtNbrDeudor" runat="server" Text='<%# Bind("NbrDeudor") %>' MaxLength="100" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Categoría Deudor" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCatDeudor" runat="server" Text='<%# Bind("CatDeudor") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCatDeudor" runat="server" Text='<%# Bind("CatDeudor") %>' MaxLength="100" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo Préstamo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblTipoPrestamo" runat="server" Text='<%# Bind("TipoPrestamo") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtTipoPrestamo" runat="server" Text='<%# Bind("TipoPrestamo") %>' MaxLength="100" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEstado" runat="server" Text='<%# Bind("Estado") %>' MaxLength="100" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <%--------------------------------------------------------------------------------------------------------------------------------------------%>


                    <asp:TemplateField HeaderText="Condición Prestamo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCondicionPrestamo" runat="server" Text='<%# Bind("CondicionPrestamo") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCondicionPrestamo" runat="server" Text='<%# Bind("CondicionPrestamo") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Existe Obligacion" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblExisteObligacion" runat="server" Text='<%# Bind("ExisteObligacion") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtExisteObligacion" runat="server" Text='<%# Bind("ExisteObligacion") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Condición Motivo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCondicionMotivo" runat="server" Text='<%# Bind("CondicionMotivo") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCondicionMotivo" runat="server" Text='<%# Bind("CondicionMotivo") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <%-------------------------------------------------------------------------------------------------------------------------------------------------%>

                    <asp:TemplateField HeaderText="Condición Tasa" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCondicionTasa" runat="server" Text='<%# Bind("CondicionTasa") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCondicionTasa" runat="server" Text='<%# Bind("CondicionTasa") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Condición Monto" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCondicionMonto" runat="server" Text='<%# Bind("CondicionMonto") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCondicionMonto" runat="server" Text='<%# Bind("CondicionMonto") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Condición Fecha Inicio" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCondicionFchInicio" runat="server" Text='<%# Bind("CondicionFchInicio") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCondicionFchInicio" runat="server" Text='<%# Bind("CondicionFchInicio") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Condición Fecha Fin" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblCondicionFchFin" runat="server" Text='<%# Bind("CondicionFchFin") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtCondicionFchFin" runat="server" Text='<%# Bind("CondicionFchFin") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>

                <EditRowStyle BackColor="#999999" />

                <PagerStyle CssClass="pgr"></PagerStyle>
            </asp:GridView>

        </div>
    </div>
</asp:Content>
