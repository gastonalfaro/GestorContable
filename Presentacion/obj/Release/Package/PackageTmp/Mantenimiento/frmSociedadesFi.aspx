<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmSociedadesFi.aspx.cs" Inherits="Presentacion.Mantenimiento.frmSociedadesFi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <%--<asp:Button ID="btnSociedadFiNuevo" runat="server" Text="NUEVO" OnClick="btnSociedadFiNuevo_Click"/>
        <asp:Button ID="btnSociedadFiGuardar" runat="server" Text="GUARDAR" OnClick="btnSociedadFiGuardar_Click" Visible="false" />--%>
        <asp:Button ID="btnSociedadesFiVolver" runat="server" Text="VOLVER" OnClick="btnSociedadesFiVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
               
    </div> 
    <div class="col-md-12">
        <h3>SOCIEDADES FINANCIERAS</h3>
        <p>Consulta de Sociedades Financieras del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdSociedadFi" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label4" runat="server" Text="Sociedad GL:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdSociedadGL" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqNomSociedad" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label3" runat="server" Text="País:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtIdPais" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label5" runat="server" Text="Moneda:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqIdMoneda" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnSociedadFiConsultar" runat="server" Text="CONSULTAR" OnClick="btnSociedadFiConsultar_Click" CssClass="ButtonNeutro"/></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
    
        <asp:GridView ID="gvpSociedadesFi" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="gvpSociedadesFi_SelectedIndexChanged" OnRowEditing="gvpSociedadesFi_RowEditing"
            OnRowUpdating="gvpSociedadesFi_RowUpdating" OnRowUpdated="gvpSociedadesFi_RowUpdated" OnPageIndexChanging="gvpSociedadesFi_PageIndexChanging"
            OnRowCancelingEdit="gvpSociedadesFi_RowCancelingEdit" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdSociedadFi" runat="server"  Text='<%# Bind("IdSociedadFi") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdSociedadFi" runat="server"  Text='<%# Bind("IdSociedadFi") %>' MaxLength="10" />
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

                            <asp:TemplateField HeaderText="Sociedad GL" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblDenominacion" runat="server" Text='<%# Bind("IdSociedadGL") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertDenominacion" runat="server"  Text='<%# Bind("IdSociedadGL") %>' MaxLength="30" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="País" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPais" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblPais" runat="server" Text='<%# Bind("IdPais") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditPais" runat="server" Width="50%" Text='<%# Bind("IdPais") %>' MaxLength="200"  />
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Moneda" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMoneda" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblMoneda" runat="server" Text='<%# Bind("IdMoneda") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditMoneda" runat="server" Width="50%" Text='<%# Bind("IdMoneda") %>' MaxLength="200"  />
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

                        </Columns>  

            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
    </div>
</asp:Content>
