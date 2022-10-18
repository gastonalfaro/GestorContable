<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmOpciones.aspx.cs" Inherits="Presentacion.Mantenimiento.frmOpciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <asp:Button ID="btnCatalogoNuevo" runat="server" Text="NUEVO" OnClick="btnCatalogoNuevo_Click" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnCatalogoGuardar" runat="server" Text="GUARDAR" OnClick="btnCatalogoGuardar_Click" Visible="false" CssClass="ButtonNeutro" />
        <asp:Button ID="btnCatalogoVolver" runat="server" Text="VOLVER" OnClick="btnCatalogoVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
    </div> 
    <div class="col-md-12" id="tblCatalogos">
            <h3>OPCIONES CATÁLOGOS GENERALES</h3>
            <p>Mantenimiento de Opciones de Catálogos Generales del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label id="lblCatalogo" runat="server" Text="Catalogo:" Font-Bold="true"> </asp:Label></div>
                <div class="col-md-5"><asp:TextBox runat="server" ID="txtCatalogo" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
            
        </div>
    <div style="width: 100%; height: 100%; overflow: auto">
        <asp:GridView ID="grdvOpciones" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvOpciones_SelectedIndexChanged" OnRowEditing="grdvOpciones_RowEditing"
            OnRowUpdating="grdvOpciones_RowUpdating" OnPageIndexChanging="grdvOpciones_PageIndexChanging"
            OnRowCancelingEdit="grdvOpciones_RowCancelingEdit" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"  > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdOpcion" runat="server" Text='<%# Bind("IdOpcion") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdOpcion" runat="server"  Text='<%# Bind("IdOpcion") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nombre" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoOpcion" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNombreOpcion" runat="server" Text='<%# Bind("NomOpcion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNombreOpcion" runat="server" Width="90%" Text='<%# Bind("NomOpcion") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Valor" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoValor" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblValor" runat="server" Text='<%# Bind("ValOpcion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditValor" runat="server" Width="90%" Text='<%# Bind("ValOpcion") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100"  />
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
