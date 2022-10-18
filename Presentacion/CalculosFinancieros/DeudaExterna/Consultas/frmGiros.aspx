<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmGiros.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.frmGiros" %>

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
        <h3>Giros</h3>
    </div>
    <div class="col-md-12">Consulta de Giros del Sistema Gestor.</div>

    <div class="col-md-12 divStyle">
        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="Label1" runat="server" Text="Id Préstamo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdPrestamo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="Label2" runat="server" Text="Id Tramo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdTramo" runat="server" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="lblFechaEstimada" runat="server" Text="Fecha Estimada:  "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtFechaEstimada" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-4">
                <asp:Label ID="lblFecha" runat="server" Text="Fecha Desembolso:  "></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtFecha" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-12" style="margin-bottom:2%; margin-top:2%;">
            <div style="text-align: center;">
                <asp:Button ID="btnFormulariosConsultar" runat="server" Text="CONSULTAR" OnClick="btnFormulariosConsultar_Click" CssClass="ButtonNeutro" />
            </div>
        </div>

        <div class="col-md-12 ">
            <asp:Label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true"></asp:Label>
        </div>
    </div>

    <div class="col-md-12 divStyle">
    <div style="width: 100%; height: 100%; overflow: auto">
        
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

                <asp:TemplateField HeaderText="Fecha Estimada">
                    <FooterTemplate>
                        <asp:TextBox ID="txtFchEstimada" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblFchEstimada" runat="server" Text='<%# Eval("FchEstimada").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditFchEstimada" runat="server" Width="90%" Text='<%# Eval("FchEstimada").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="50" />
                    </EditItemTemplate>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Fecha Desembolso" ItemStyle-HorizontalAlign="Center">
                    <FooterTemplate>
                        <asp:TextBox ID="txtFchDesembolso" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblFchDesembolso" runat="server" Text='<%# Eval("FchDesembolso").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditFchDesembolso" runat="server" Width="90%" Text='<%# Eval("FchDesembolso").ToString().Replace(" ","").Replace("12:00:00a.m.","") %>' MaxLength="50" />
                    </EditItemTemplate>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="Monto" ItemStyle-HorizontalAlign="Center">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMonto" runat="server" Width="90%" Text='<%# Bind("Monto") %>' MaxLength="100" />
                                </EditItemTemplate>

                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>--%>

                <asp:BoundField DataField="Monto" DataFormatString="{0:N}" HeaderText="Monto">

                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center">
                    <FooterTemplate>
                        <asp:TextBox ID="txtIdMoneda" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdMoneda" runat="server" Text='<%# Bind("Moneda") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditIdMoneda" runat="server" Width="90%" Text='<%# Bind("Moneda") %>' MaxLength="100" />
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

                <asp:TemplateField HeaderText="Tipo Desembolso " ItemStyle-HorizontalAlign="Center">
                    <FooterTemplate>
                        <asp:TextBox ID="txtTipoDesembolso" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTipoDesembolso" runat="server" Text='<%# Bind("TipoDesembolso") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditTipoDesembolso" runat="server" Width="90%" Text='<%# Bind("TipoDesembolso") %>' MaxLength="100" />
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




                <%--   <asp:TemplateField HeaderText="Ver" ShowHeader="True" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarFormulario" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarFormulario" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                
                                    <asp:LinkButton ID="lbtIraFormulario" runat="server" CausesValidation="False" CommandName="Edit" Text="Ver"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
            </Columns>

            <EditRowStyle BackColor="#999999" />

            <PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>
        </div>
    </div>
</asp:Content>

