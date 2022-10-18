<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmServicios.aspx.cs" Inherits="Presentacion.Mantenimiento.frmServicios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <asp:Button ID="btnNuevoServicio" runat="server" Text="NUEVO" OnClick="btnNuevoServicio_Click" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnGuardarServicio" runat="server" Text="GUARDAR" OnClick="btnGuardarServicio_Click" Visible="false" CssClass="ButtonNeutro" />
        <asp:Button ID="btnVolverServicios" runat="server" Text="VOLVER" OnClick="btnVolverServicios_Click" Visible="false" CssClass="ButtonNeutro"/>
              
    </div> 
    <div class="col-md-12" id="tblServicios">
        <h3>Servicios</h3>
        <p>Mantenimiento de Servicios del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdServicio" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Nombre:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaNomServicio" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label4" runat="server" Text="Sociedad:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtBusquedaSociedad" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="Cuenta Contable:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtCuentaContable" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label5" runat="server" Text="PosPre:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtPosPre" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarServicio" runat="server" Text="CONSULTAR" OnClick="btnConsultarServicio_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
        
        <asp:GridView ID="grdServicios" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"  OnRowDataBound="grdServicios_RowDataBound"
            OnSelectedIndexChanged="grdServicios_SelectedIndexChanged" OnRowEditing="grdServicios_RowEditing"
            OnRowUpdating="grdServicios_RowUpdating" OnRowUpdated="grdServicios_RowUpdated" OnPageIndexChanging="grdServicios_PageIndexChanging"
            OnRowCancelingEdit="grdServicios_RowCancelingEdit" PageSize="10" AllowPaging="True" 
            OnRowCreated="grdServicios_RowCreated">
                        <Columns>
                            <asp:TemplateField HeaderText="Código" ControlStyle-Width="60"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdServicio" runat="server"  Text='<%# Bind("IdServicio") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertarIdServicio" runat="server"  Text='<%# Bind("IdServicio") %>' MaxLength="10" />
                                </FooterTemplate>
                            </asp:TemplateField>

                        <asp:TemplateField HeaderText="FechaModifica" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblFechaModifica" runat="server"  Text='<%# Bind("FchModifica") %>'></asp:Label>
                                </ItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sociedad" ControlStyle-Width="60"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdSociedad" runat="server"  Text='<%# Bind("IdSociedadGL") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertarIdSociedad" runat="server"  Text='<%# Bind("IdSociedadGL") %>' MaxLength="10" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Dependencia" ControlStyle-Width="60"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdOficina" runat="server"  Text='<%# Bind("IdOficina") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertarIdOficina" runat="server"  Text='<%# Bind("IdOficina") %>' MaxLength="10" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" ControlStyle-Width="150">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomServicio" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomServicio" runat="server" Text='<%# Bind("NomServicio") %>' ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarNomServicio" runat="server" Text='<%# Bind("NomServicio") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Monto" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblMonto" runat="server" Text='<%# Bind("Monto") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertMonto"  TextMode="Number"  runat="server"  Text='<%# Bind("Monto") %>' MaxLength="30" />
                                </FooterTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarMonto" runat="server" Text='<%# Bind("Monto") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Perm.Reserva" ControlStyle-Width="30" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblReserva" runat="server" Text='<%# Bind("PermiteReserva") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:CheckBox ID="cbInsertReserva"  TextMode="Number"  runat="server"  Text='<%# Bind("PermiteReserva") %>' MaxLength="30" />
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblReserva" runat="server" Text='<%# Bind("PermiteReserva") %>' Visible="false"></asp:Label>
				                    <asp:CheckBox ID="cbEditarReserva" runat="server" />
                                    <%--<asp:CheckBox ID="ckbReserva" runat="server" />--%>
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CC Debe Dev" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCtaContableDebeActualDev" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCtaContableDebeActualDev" runat="server" Text='<%# Bind("CtaContableDebeActualDev") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarCtaContableDebeActualDev" runat="server" Text='<%# Bind("CtaContableDebeActualDev") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CC Haber Dev" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCtaContableHaberActualDev" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCtaContableHaberActualDev" runat="server" Text='<%# Bind("CtaContableHaberActualDev") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarCtaContableHaberActualDev" runat="server" Text='<%# Bind("CtaContableHaberActualDev") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PosPre Dev" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdPosPreActualDev" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPosPreActualDev" runat="server" Text='<%# Bind("IdPosPreActualDev") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdPosPreActualDev" runat="server" Text='<%# Bind("IdPosPreActualDev") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CC Debe Per" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCtaContableDebeActualPer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCtaContableDebeActualPer" runat="server" Text='<%# Bind("CtaContableDebeActualPer") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarCtaContableDebeActualPer" runat="server" Text='<%# Bind("CtaContableDebeActualPer") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CC Haber Per" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCtaContableHaberActualPer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCtaContableHaberActualPer" runat="server" Text='<%# Bind("CtaContableHaberActualPer") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarCtaContableHaberActualPer" runat="server" Text='<%# Bind("CtaContableHaberActualPer") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PosPre Per" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdPosPreActualPer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPosPreActualPer" runat="server" Text='<%# Bind("IdPosPreActualPer") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdPosPreActualPer" runat="server" Text='<%# Bind("IdPosPreActualPer") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CC Debe Dev" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCtaContableDebeVencidoDev" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCtaContableDebeVencidoDev" runat="server" Text='<%# Bind("CtaContableDebeVencidoDev") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarCtaContableDebeVencidoDev" runat="server" Text='<%# Bind("CtaContableDebeVencidoDev") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CC Haber Dev" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCtaContableHaberVencidoDev" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCtaContableHaberVencidolDev" runat="server" Text='<%# Bind("CtaContableHaberVencidoDev") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarCtaContableHaberVencidoDev" runat="server" Text='<%# Bind("CtaContableHaberVencidoDev") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PosPre Dev" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdPosPreVencidoDev" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPosPreVencidoDev" runat="server" Text='<%# Bind("IdPosPreVencidoDev") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdPosPreVencidoDev" runat="server" Text='<%# Bind("IdPosPreVencidoDev") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CC Debe Per" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCtaContableDebeVencidoPer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCtaContableDebeVencidoPer" runat="server" Text='<%# Bind("CtaContableDebeVencidoPer") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarCtaContableDebeVencidoPer" runat="server" Text='<%# Bind("CtaContableDebeVencidoPer") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="CC Haber Per" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCtaContableHaberVencidoPer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCtaContableHaberVencidoPer" runat="server" Text='<%# Bind("CtaContableHaberVencidoPer") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarCtaContableHaberVencidoPer" runat="server" Text='<%# Bind("CtaContableHaberVencidoPer") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PosPre Per" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdPosPreVencidoPer" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPosPreVencidoPer" runat="server" Text='<%# Bind("IdPosPreVencidoPer") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdPosPreVencidoPer" runat="server" Text='<%# Bind("IdPosPreVencidoPer") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" ControlStyle-Width="30">
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarEstado" runat="server" Text='<%# Bind("Estado") %>' />
                                    <%--<asp:CheckBox ID="ckbEstado" runat="server" />--%>
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarParametro" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarParametro" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarParametro" runat="server" CausesValidation="False" Text="Editar" CommandName="Edit" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>  
            <EditRowStyle BackColor="#999999" />
                    </asp:GridView>
    </div>
</asp:Content>
