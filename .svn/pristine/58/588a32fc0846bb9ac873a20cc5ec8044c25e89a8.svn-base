<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmReservaDetalle.aspx.cs" Inherits="Presentacion.Mantenimiento.frmReservaDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <asp:Button ID="btnVolverReservas" runat="server" Text="VOLVER" OnClick="btnVolverReservas_Click" CssClass="ButtonNeutro" />
    </div> 
    <div class="col-md-12" id="tblDetalle">
        <h3>Reservas</h3>
        <p>Consulta de Detalles de Reserva del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblNomReserva" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-9"><asp:TextBox ID="txtNomReserva" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblIdReserva" runat="server" Text="Código Reserva:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtIdReserva" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
         
       <asp:GridView ID="grdDetalles" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnPageIndexChanging="grdDetalles_PageIndexChanging" AllowPaging="true" PageSize="20" AllowSorting="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Posicion" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblPosicion" runat="server"  Text='<%# Bind("Posicion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Detalle" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblDetalle" runat="server"  Text='<%# Bind("Detalle") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarDetalle" runat="server" Text='<%# Bind("Detalle") %>' MaxLength="200"  />
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Código PosPre" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPosPre" runat="server"  Text='<%# Bind("IdPosPre") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdPosPre" runat="server" Text='<%# Bind("IdPosPre") %>' MaxLength="200"  />
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Código Centro Gestor" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCentroGestor" runat="server" Text='<%# Bind("IdCentroGestor") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdCentroGestor" runat="server" Width="50%" Text='<%# Bind("IdCentroGestor") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Código Fondo" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdFondo" runat="server" Text='<%# Bind("IdFondo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdFondo" runat="server" Text='<%# Bind("IdFondo") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Segmento" >
                                <ItemTemplate>
                                    <asp:Label ID="lblSegmento" runat="server" Text='<%# Bind("Segmento") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarSegmento" runat="server" Text='<%# Bind("Segmento") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Código Programa" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPrograma" runat="server" Text='<%# Bind("IdPrograma") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdPrograma" runat="server" Width="50%" Text='<%# Bind("IdPrograma") %>'  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Código Cuenta Contable" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCuentaContable" runat="server" Text='<%# Bind("IdCuentaContable") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdCuentaContable" runat="server" Width="50%" Text='<%# Bind("IdCuentaContable") %>'  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Código Centro Costo" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCentroCosto" runat="server" Text='<%# Bind("IdCentroCosto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdCentroCosto" runat="server" Width="50%" Text='<%# Bind("IdCentroCosto") %>'  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Código Elemento PEP" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdElementoPEP" runat="server" Text='<%# Bind("IdElementoPEP") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdElementoPEP" runat="server" Width="50%" Text='<%# Bind("IdElementoPEP") %>'  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Código Moneda" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdMoneda" runat="server" Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdMoneda" runat="server" Width="50%" Text='<%# Bind("IdMoneda") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Monto" >
                                <ItemTemplate>
                                    <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtMonto" runat="server" Width="50%" Text='<%# Bind("Monto") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bloqueado" >
                                <ItemTemplate>
                                    <asp:Label ID="lblBloqueado" runat="server" Text='<%# Bind("Bloqueado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtBloqueado" runat="server" Width="50%" Text='<%# Bind("Bloqueado") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" >
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditEstado" runat="server" Width="50%" Text='<%# Bind("Estado") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>
<%--
                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarReserva" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarReserva" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarReserva" runat="server" ReservaCausesValidation="False" CommandName="Edit" Text="Editar"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
    --%>
                        </Columns>  
            <EditRowStyle BackColor="#999999" />
                    </asp:GridView>
    </div>
</asp:Content>
