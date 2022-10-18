<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmTramos.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.frmTramos" %>

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
        <h3>Tramos</h3>
    </div>
    <div class="col-md-12">Consulta de Tramos del Sistema Gestor.</div>

    <div class="col-md-12 divStyle">
        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="Label2" runat="server" Text="Id Tramo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdTramo" runat="server" MaxLength="4" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="Label1" runat="server" Text="Id Préstamo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtBusqIdPrestamo" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="Label3" runat="server" Text="Tipo Acuerdo:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlTipoAcuerdo" runat="server" TextMode="Text" AutoPostBack="true" CssClass="FormatoDropDownList">
                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                    <asp:ListItem Value="Acuerdo Estandar">Acuerdo Estándar</asp:ListItem>
                    <asp:ListItem Value="Acuerdo Reescalonamiento">Acuerdo Reescalonamiento</asp:ListItem>
                    <asp:ListItem Value="Emision del Gobierno">Emisión del Gobierno</asp:ListItem>
                    <asp:ListItem Value="Otros">Otros</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-md-6 divStyle">
            <div class="col-md-5">
                <asp:Label ID="Label4" runat="server" Text="Tipo Financiamiento:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlTipoFinanciamiento" runat="server" TextMode="Text" AutoPostBack="true" CssClass="FormatoDropDownList">
                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                    <asp:ListItem Value="Bonos Brady">Bonos Brady</asp:ListItem>
                    <asp:ListItem Value="Bonos Globales">Bonos Globales</asp:ListItem>
                    <asp:ListItem Value="Bonos/Obligaciones del tesoro">Bonos / Obligaciones del Tesoro</asp:ListItem>
                    <asp:ListItem Value="Eurobonos">Eurobonos</asp:ListItem>
                    <asp:ListItem Value="Linea de Credito">Línea de Crédito</asp:ListItem>
                    <asp:ListItem Value="Otros">Otros</asp:ListItem>
                    <asp:ListItem Value="Pagares">Pagares</asp:ListItem>
                    <asp:ListItem Value="Prestamos">Préstamos</asp:ListItem>
                </asp:DropDownList>
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
        <div style="margin-top: 2%;">
            <asp:GridView ID="grdvFormularios" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                Width="100%" CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                PageSize="15" AllowPaging="True" OnPageIndexChanging="grdvFormularios_PageIndexChanging">
                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <Columns>

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
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Id Préstamo" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblIdPrestamo" runat="server" Text='<%# Bind("IdPrestamo") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtInsertIdPrestamo" runat="server" Text='<%# Bind("IdPrestamo") %>' MaxLength="32" />
                        </FooterTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo Acuerdo" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtTipoAcuerdo" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTipoAcuerdo" runat="server" Text='<%# Bind("TipoAcuerdo") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditTipoAcuerdo" runat="server" Width="90%" Text='<%# Bind("TipoAcuerdo") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tipo Financiamiento" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtTipoFinanciamiento" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTipoFinanciamiento" runat="server" Text='<%# Bind("TipoFinanciamiento") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditTipoFinanciamiento" runat="server" Width="90%" Text='<%# Bind("TipoFinanciamiento") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Término Crédito" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtTerminoCredito" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTerminoCredito" runat="server" Text='<%# Bind("TerminoCredito") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditTerminoCredito" runat="server" Width="90%" Text='<%# Bind("TerminoCredito") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Reorganización" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtReorganizacion" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblReorganizacion" runat="server" Text='<%# Bind("Reorganizacion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditReorganizacion" runat="server" Width="90%" Text='<%# Bind("Reorganizacion") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Términos Reorganizados" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtTerminoReorganizado" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTerminoReorganizado" runat="server" Text='<%# Bind("TerminoReorganizado") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditTerminoReorganizado" runat="server" Width="90%" Text='<%# Bind("TerminoReorganizado") %>' MaxLength="100" />
                        </EditItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>

                    <%--     <asp:TemplateField HeaderText="Monto" ItemStyle-HorizontalAlign="Center">
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
                            </asp:TemplateField>  --%>

                    <asp:BoundField DataField="Monto" DataFormatString="{0:N}" HeaderText="Monto">

                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Moneda" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtIdMoneda" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblIdMoneda" runat="server" Text='<%# Bind("IdMoneda") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEditIdMoneda" runat="server" Width="90%" Text='<%# Bind("IdMoneda") %>' MaxLength="100" />
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
