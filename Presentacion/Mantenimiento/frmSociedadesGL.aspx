<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmSociedadesGL.aspx.cs" Inherits="Presentacion.Mantenimiento.frmSociedadesGL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <%--<asp:Button ID="btnSociedadGLNuevo" runat="server" Text="NUEVO" OnClick="btnSociedadGLNuevo_Click"/>
        <asp:Button ID="btnSociedadGLGuardar" runat="server" Text="GUARDAR" OnClick="btnSociedadGLGuardar_Click" Visible="false" />--%>
        <asp:Button ID="btnVolverSociedadesGL" runat="server" Text="VOLVER" OnClick="btnVolverSociedadesGL_Click" Visible="false" CssClass="ButtonNeutro" />
                
    </div> 
    <div class="col-md-12" id="tblParametros">
        <h3>INSTITUCIONES/SOCIEDADES GL</h3>
        <p>Mantenimiento de Instituciones/Sociedades GL del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdSociedadGL" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtBusqNomSociedad" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="País:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtIdPais" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label5" runat="server" Text="Moneda:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdMoneda" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarSociedadesGL" runat="server" Text="CONSULTAR" OnClick="btnConsultarSociedadesGL_Click" CssClass="ButtonNeutro"/></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
                
        <asp:GridView ID="grdSociedadesGL" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True" onrowcommand="grdSociedadesGL_RowCommand"
            DataKeyNames="IdSociedadGL,NomSociedad"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdSociedadesGL_SelectedIndexChanged" OnRowEditing="grdSociedadesGL_RowEditing"
            OnRowUpdating="grdSociedadesGL_RowUpdating" OnRowUpdated="grdSociedadesGL_RowUpdated" OnPageIndexChanging="grdSociedadesGL_PageIndexChanging"
            OnRowCancelingEdit="grdSociedadesGL_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"   > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdSociedadGL" runat="server"  Text='<%# Bind("IdSociedadGL") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdSociedadGL" runat="server"  Text='<%# Bind("IdSociedadGL") %>' MaxLength="10" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomSociedad" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomSociedad" runat="server" Text='<%# Bind("NomSociedad") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomSociedad" runat="server" Width="50%" Text='<%# Bind("NomSociedad") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Denominación" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblDenominacion" runat="server" Text='<%# Bind("Denominacion") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertDenominacion" runat="server"  Text='<%# Bind("Denominacion") %>' MaxLength="30" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="País" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdPais" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdPais" runat="server" Text='<%# Bind("IdPais") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdPais" runat="server" Width="50%" Text='<%# Bind("IdPais") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Población" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPoblacion" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPoblacion" runat="server" Text='<%# Bind("Poblacion") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditPoblacion" runat="server" Width="50%" Text='<%# Bind("Poblacion") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Calle" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCalle" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCalle" runat="server" Text='<%# Bind("Calle") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditCalle" runat="server" Width="50%" Text='<%# Bind("Calle") %>' MaxLength="200"  />
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Moneda" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdMoneda" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdMoneda" runat="server" Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdMoneda" runat="server" Width="50%" Text='<%# Bind("IdMoneda") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Idioma" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtIdIdioma" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIdIdioma" runat="server" Text='<%# Bind("IdIdioma") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditIdIdioma" runat="server" Width="50%" Text='<%# Bind("IdIdioma") %>' MaxLength="200"  />
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
				                    <asp:TextBox ID="txtEditEstado" runat="server" Width="50%" Text='<%# Bind("Estado") %>' MaxLength="200"  />
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
