<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmOpcionesCatalogo.aspx.cs" Inherits="Presentacion.Mantenimiento.frmOpcionesCatalogo" %>
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
        <asp:Button ID="btnVolverCatalogos" runat="server" Text="VOLVER" OnClick="btnVolverCatalogos_Click" CssClass="ButtonNeutro"/>
    </div> 
    <div class="col-md-12" id="tblCatalogo">
        <h3>CATÁLOGO GENERAL</h3>
        <p>Consulta de Opciones de Catalogo General.</p>
        <div class="col-md-4">
            <div class="col-md-5"><asp:Label ID="lblCatalogo" runat="server" Text="Catálogo General:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCatalogo" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-4">
            <div class="col-md-3"><asp:Label ID="lblNomCatalogo" runat="server" Text="Nombre:" Font-Bold="true" ></asp:Label></div>
            <div class="col-md-9"><asp:TextBox ID="txtNomCatalogo" runat="server"  Width="100%"  CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-2">
            <asp:CheckBox ID="ckbActivo" runat ="server" Text="Activo"/>
        </div>
        <div class="col-md-2">
            <asp:Button ID="btnActualizar" Text="Actualizar" runat="server" CssClass="ButtonNeutro" OnClick="btnActualizar_Click" />
        </div>
        <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
       <asp:GridView ID="grvOpciones" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grvOpciones_SelectedIndexChanged" OnRowEditing="grvOpciones_RowEditing"
            OnRowUpdating="grvOpciones_RowUpdating" OnPageIndexChanging="grvOpciones_PageIndexChanging"
            OnRowCancelingEdit="grvOpciones_RowCancelingEdit" PageSize="20" AllowPaging="True" ShowFooter="true">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"  > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdOpcion" runat="server" Text='<%# Bind("IdOpcion") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertarIdOpcion" runat="server"  Text='<%# Bind("IdOpcion") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Valor" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtInsertarValOpcion" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblValOpcion" runat="server" Text='<%# Bind("ValOpcion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarValOpcion" runat="server" Width="90%" Text='<%# Bind("ValOpcion") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtInsertarNomOpcion" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomOpcion" runat="server" Text='<%# Bind("NomOpcion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarNomOpcion" runat="server" Width="90%" Text='<%# Bind("NomOpcion") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtInsertarEstado" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FchModificacion" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblFchModificacion" runat="server" Text='<%# Bind("FchModifica") %>' Visible="False"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <EditItemTemplate> 
                                    <asp:LinkButton ID="lnkActualizarParametro" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarParametro" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarParametro" runat="server" CausesValidation="False" Text="Editar" CommandName="Edit" ></asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtAgregarOpcion" runat="server" CausesValidation="False" Text="Agregar" CommandName="Select" ></asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
    </div>
</asp:Content>
