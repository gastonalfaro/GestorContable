<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmAcreedores.aspx.cs" Inherits="Presentacion.Mantenimiento.frmAcreedores" %>
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
        <asp:Button ID="btnAcreedorNuevo" runat="server" Text="NUEVO" OnClick="btnAcreedorNuevo_Click" Visible="false" CssClass="ButtonNeutro" />
        <asp:Button ID="btnAcreedorGuardar" runat="server" Text="GUARDAR" OnClick="btnAcreedorNuevo_Click" Visible="false"  CssClass="ButtonNeutro"/>
        <asp:Button ID="btnAcreedorVolver" runat="server" Text="VOLVER" OnClick="btnAcreedorVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
    </div>
    <div class="col-md-12">
        <h3>ACREEDORES</h3>
        <p>Mantenimiento de Acreedores del Sistema Gestor.</p>

        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtBusqIdAcreedor" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqNomAcreedor" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        
       <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnAcreedorConsultar" runat="server" Text="CONSULTAR" OnClick="btnAcreedorConsultar_Click" CssClass="ButtonNeutro"/></div>
       <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
                    <asp:GridView ID="grdvAcreedores" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True" ForeColor="#333333"
                        Width="100%" OnSelectedIndexChanged="grdvAcreedores_SelectedIndexChanged" OnRowEditing="grdvAcreedores_RowEditing"
                        OnRowDataBound="grdvAcreedores_RowDataBound"  CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        OnRowUpdating="grdvAcreedores_RowUpdating" OnRowUpdated="grdvAcreedores_RowUpdated" OnPageIndexChanging="grdvAcreedores_PageIndexChanging"
                        OnDataBound="grdvAcreedores_DataBound" OnRowCancelingEdit="grdvAcreedores_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>
                            <%--<asp:CommandField ShowEditButton="true"/>--%>

                            <asp:TemplateField HeaderText="Código" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdAcreedor" runat="server" Text='<%# Bind("NroAcreedor") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdAcreedor" runat="server"  Text='<%# Bind("NroAcreedor") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevoAcreedor" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomAcreedor" runat="server" Text='<%# Bind("NomAcreedor") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomAcreedor" runat="server" Width="90%" Text='<%# Bind("NomAcreedor") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Abreviatura" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevaAbreviatura" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAbreviatura" runat="server" Text='<%# Bind("Abreviatura") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditAbreviatura" runat="server" Width="90%" Text='<%# Bind("Abreviatura") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Contacto" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevoContacto" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblContacto" runat="server" Text='<%# Bind("Contacto") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditContacto" runat="server" Width="90%" Text='<%# Bind("Contacto") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Telefonos" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevaTelefonos" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTelefonos" runat="server" Text='<%# Bind("Telefono") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditTelefonos" runat="server" Width="90%" Text='<%# Bind("Telefono") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Direccion" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevaDireccion" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDireccion" runat="server" Text='<%# Bind("Direccion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditDireccion" runat="server" Width="90%" Text='<%# Bind("Direccion") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="País" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevoPais" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPais" runat="server" Text='<%# Bind("Pais") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditPais" runat="server" Width="90%" Text='<%# Bind("Pais") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="País Institución" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevoPaisInstitucion" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPaisInstitucion" runat="server" Text='<%# Bind("PaisInstitucion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditPaisInstitucion" runat="server" Width="90%" Text='<%# Bind("PaisInstitucion") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tipo" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevoTipoAcreedor" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTipoAcreedor" runat="server" Text='<%# Bind("TipoAcreedor") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditTipoAcreedor" runat="server" Width="90%" Text='<%# Bind("TipoAcreedor") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNuevoEstado" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Cuenta Contable" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCtaContable" runat="server" Text='<%# Bind("IdCtaContable") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblIdCtaContable" runat="server" Text='<%# Eval("IdCtaContable")%>' Visible = "false"></asp:Label>
                                    <asp:DropDownList ID="ddlEditarIdCtaContable" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="FchModifica" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIFchModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertFchModifica" runat="server"  Text='<%# Bind("FchModifica") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarAcr" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarAcr" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarAcreedor" runat="server" CausesValidation="False" Text="Editar" CommandName="Edit" ></asp:LinkButton>
                                </ItemTemplate>
<%--                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtAgregarAcreedor" runat="server" CausesValidation="False" Text="Agregar" CommandName="Select" ></asp:LinkButton>
                                </FooterTemplate>--%>
                            </asp:TemplateField>
                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
    </div>
</asp:Content>

