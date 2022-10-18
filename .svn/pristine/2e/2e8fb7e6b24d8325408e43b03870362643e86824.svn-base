<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmReservas.aspx.cs" Inherits="Presentacion.Mantenimiento.frmReservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"></div> 
    <div class="col-md-12" id="tblParametros">
        <h3>Reservas</h3>
        <p>Gestión de Reservas del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Reserva:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdReserva" runat="server" CssClass="FormatoTextBox" onkeypress="return AceptarSoloNumeros(event)"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="Posición Presupuestaria:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdPosPre" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label4" runat="server" Text="Centro Costo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdCentroCosto" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label5" runat="server" Text="Elemento PEP:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdElementoPEP" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label6" runat="server" Text="Moneda:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdMoneda" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqNomReserva" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarReserva" runat="server" Text="CONSULTAR" OnClick="btnConsultarReserva_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
      
        <asp:GridView ID="grdReservas" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdReservas_SelectedIndexChanged" OnRowEditing="grdReservas_RowEditing"
            OnRowUpdating="grdReservas_RowUpdating" OnRowUpdated="grdReservas_RowUpdated" OnPageIndexChanging="grdReservas_PageIndexChanging"
            OnSorting="grdReservas_Sorting" OnRowCancelingEdit="grdReservas_RowCancelingEdit" AllowPaging="true" PageSize="20" AllowSorting="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Reserva" SortExpression="IdReserva"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdReserva" runat="server"  Text='<%# Bind("IdReserva") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Posición" SortExpression="Posicion"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblPosicion" runat="server"  Text='<%# Bind("Posicion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Posición Presupuestaria" SortExpression="PosPre"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblPosPre" runat="server"  Text='<%# Bind("IdPosPre") %>'></asp:Label>
                                </ItemTemplate>
<%--                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdEntidadCP" runat="server" Width="50%" Text='<%# Bind("IdEntidadCP") %>' MaxLength="200"  />
                                </EditItemTemplate>--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Centro Gestor" SortExpression="IdCentroGestor" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblCentroGestor" runat="server"  Text='<%# Bind("IdCentroGestor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Fondo" SortExpression="IdFondo"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblFondo" runat="server"  Text='<%# Bind("IdFondo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Segmento" SortExpression="Segmento" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblSegmento" runat="server"  Text='<%# Bind("Segmento") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Código Programa" SortExpression="IdPrograma" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPrograma" runat="server"  Text='<%# Bind("IdPrograma") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Centro Costo" SortExpression="IdCentroCosto">
       
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCentroCosto" runat="server" Text='<%# Bind("IdCentroCosto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Elemento PEP" SortExpression="IdElementoPEP"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdElementoPEP" runat="server"  Text='<%# Bind("IdElementoPEP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Descripcion" SortExpression="NomReserva"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblNomReserva" runat="server"  Text='<%# Bind("NomReserva") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                              
                            <asp:TemplateField HeaderText="FchModifica" SortExpression="FchModifica" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblFchModifica" runat="server"  Text='<%# Bind("FchModifica") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Orden Prioridad Contingentes" SortExpression="OrdenContingentes"  Visible="false">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtOrdenContingentes" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOrdenContingentes" runat="server" Text='<%# Bind("OrdenContingentes") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarOrdenContingentes" runat="server" Width="50%" Text='<%# Bind("OrdenContingentes") %>'  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Orden Prioridad Deuda Interna" SortExpression="OrdenDeudaInterna">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtOrdenDeudaInterna" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOrdenDeudaInterna" runat="server" Text='<%# Bind("OrdenDeudaInterna") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarOrdenDeudaInterna" runat="server" Width="50%" Text='<%# Bind("OrdenDeudaInterna") %>'  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Orden Prioridad Deuda Externa" SortExpression="OrdenDeudaExterna">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtOrdenDeudaExterna" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblOrdenDeudaExterna" runat="server" Text='<%# Bind("OrdenDeudaExterna") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarOrdenDeudaExterna" runat="server" Width="50%" Text='<%# Bind("OrdenDeudaExterna") %>'  />
                                </EditItemTemplate> 
                            </asp:TemplateField>
                             
                            <asp:TemplateField HeaderText="Moneda" SortExpression="IdMoneda">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdMoneda" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdMoneda" runat="server" Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Monto" SortExpression="Monto">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarReserva" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarReserva" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarReserva" runat="server" ReservaCausesValidation="False" CommandName="Edit" Text="Editar" OnClick="lbtEditarReserva_Click"></asp:LinkButton>
                               <%-- <asp:LinkButton ID="lbtIraReservaDetalle" runat="server" ReservaCausesValidation="False" CommandName="Edit" Text="Ver" OnClick="lbtIraReservaDetalle_Click"></asp:LinkButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>  
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
    
    </div>
</asp:Content>
