<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmIntereses.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.Consultas.frmIntereses" %>

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
        <h3>Intereses </h3>
    </div>
    <div class="col-md-12">Consulta de Intereses del Sistema Gestor.</div>

    <div class="col-md-12 divStyle" style="margin-bottom: 4%;">
        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="Label1" runat="server" Text="Id Préstamo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdPrestamo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>


        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="IdTramoLabel" runat="server" Text="Id Tramo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdTramo" runat="server" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="FchAPartirLabel" runat="server" Text="Fecha a Partir: "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqFchPagoAPartir" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="FchTasaAPartirLabel" runat="server" Text="Fecha Tasa a Partir: "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqFchTasaAPartir" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>


        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="SecuenciaLabel" runat="server" Text="Id Tramo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqSecuencia" runat="server" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-12" style="margin-bottom: 2%; margin-top: 2%;">
            <div style="text-align: center;">
                <asp:Button ID="btnFormulariosConsultar" runat="server" Text="CONSULTAR" OnClick="btnFormulariosConsultar_Click" CssClass="ButtonNeutro" />
            </div>
        </div>

        <div class="col-md-12">
            <asp:Label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true"></asp:Label>
        </div>
    </div>

    <div style="width: 100%; height: 100%; overflow: auto">
        <div style="margin-bottom: 2%;">
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
                            <asp:TextBox ID="txtInsertIdPrestamo" runat="server" Text='<%# Bind("IdPrestamo") %>' MaxLength="15" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Id Tramo">
                        <FooterTemplate>
                            <asp:TextBox ID="txtIdTramo" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblIdTramo" runat="server" Text='<%# Bind("IdTramo") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditIdTramo" runat="server" Width="10%" Text='<%# Bind("IdTramo") %>' MaxLength="32" />
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Pago a Partir" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtFchPagoAPartir" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFchPagoAPartir" runat="server" Text='<%# Eval("FchPagoAPartir").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditFchPagoAPartir" runat="server" Width="90%" Text='<%# Eval("FchPagoAPartir").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="50" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha Tasa a Partir" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtFchTasaAPartir" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFchTasaAPartir" runat="server" Text='<%# Eval("FchTasaAPartir").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditFchTasaAPartir" runat="server" Width="90%" Text='<%# Eval("FchTasaAPartir").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="50" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Secuencia " ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtSecuencia" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSecuencia" runat="server" Text='<%# Bind("Secuencia") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditSecuencia" runat="server" Width="90%" Text='<%# Bind("Secuencia") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tasa" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtTasa" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTasa" runat="server" Text='<%# Bind("Tasa") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditTasa" runat="server" Width="90%" Text='<%# Bind("Tasa") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Tasa Margen" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtTasaMargen" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTasaMargen" runat="server" Text='<%# Bind("TasaMargen") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditTasaMargen" runat="server" Width="90%" Text='<%# Bind("TasaMargen") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Año" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtAnno" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAnno" runat="server" Text='<%# Bind("Anno") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditAnno" runat="server" Width="90%" Text='<%# Bind("Anno") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mes" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtMes" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblMes" runat="server" Text='<%# Bind("Mes") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditMes" runat="server" Width="90%" Text='<%# Bind("Mes") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Factor Conversion" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtFactorConversion" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFactorConversion" runat="server" Text='<%# Bind("FactorConversion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditFactorConversion" runat="server" Width="90%" Text='<%# Bind("FactorConversion") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fcha Pago Hasta" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtFchPagoHasta" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFchPagoHasta" runat="server" Text='<%# Eval("FchPagoHasta").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditFchPagoHasta" runat="server" Width="90%" Text='<%# Eval("FchPagoHasta").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Período" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtPeriodo" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodo" runat="server" Text='<%# Bind("Periodo") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditPeriodo" runat="server" Width="90%" Text='<%# Bind("Periodo") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Período Días" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtPeriodoDias" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodoDias" runat="server" Text='<%# Bind("PeriodoDias") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPeriodoDias" runat="server" Width="90%" Text='<%# Bind("PeriodoDias") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <%--              <asp:TemplateField HeaderText="Monto" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditMonto" runat="server" Width="90%" Text='<%# Bind("Monto") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>--%>

                    <asp:BoundField DataField="Monto" DataFormatString="{0:N}" HeaderText="Monto">

                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Días Gracia" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtDiasGracia" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDiasGracia" runat="server" Text='<%# Bind("DiasGracia") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditDiasGracia" runat="server" Width="90%" Text='<%# Bind("DiasGracia") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tasa Punitiva" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtTasaPunitiva" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTasaPunitiva" runat="server" Text='<%# Bind("TasaPunitiva") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditTasaPunitiva" runat="server" Width="90%" Text='<%# Bind("TasaPunitiva") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                </Columns>

                <EditRowStyle BackColor="#999999" />

                <PagerStyle CssClass="pgr"></PagerStyle>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
