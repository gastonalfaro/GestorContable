<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmOficinas.aspx.cs" Inherits="Presentacion.Mantenimiento.frmOficinas" %>
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
        <asp:Button ID="btnNuevosFondos" runat="server" Text="NUEVO" OnClick="btnNuevosFondos_Click" CssClass="ButtonNeutro" />
        <asp:Button ID="btnGuardarFondos" runat="server" Text="GUARDAR" OnClick="btnGuardarFondos_Click" Visible="false" CssClass="ButtonNeutro" />
        <asp:Button ID="btnVolverFondos" runat="server" Text="VOLVER" OnClick="btnVolverFondos_Click" Visible="false"  CssClass="ButtonNeutro"/>
    </div> 
    <div class="col-md-12" id="tblFondos">
        <h3>Fondos</h3>
        <p>Mantenimiento de Fondos del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblInstitucion" runat="server" Text="Institución:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-4"><asp:TextBox ID="txtInstitucion" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
            <div class="col-md-4"> <asp:TextBox ID="txtNomInstitucion" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaFondo" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdFondo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaNomFondo" runat="server" Text="Nombre:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaNomFondo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaIdMoneda" runat="server" Text="Moneda:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdMoneda" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarFondo" runat="server" Text="CONSULTAR" OnClick="btnConsultarFondo_Click" CssClass="ButtonNeutro" /></div>
        <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
                  <asp:GridView ID="grdFondos" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
                        Width="100%" OnSelectedIndexChanged="grdFondos_SelectedIndexChanged" OnRowEditing="grdFondos_RowEditing"
                        CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        OnRowUpdating="grdFondos_RowUpdating" OnRowUpdated="grdFondos_RowUpdated" OnPageIndexChanging="grdFondos_PageIndexChanging"
                        OnRowCancelingEdit="grdFondos_RowCancelingEdit" AllowPaging="true" PageSize="20">
                                    <Columns>

                                        <asp:CommandField ShowEditButton="False" />

                                        <asp:TemplateField HeaderText="Dirección:" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdFondo" runat="server" Text='<%# Bind("IdFondo") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarIdFondo" runat="server" Text='<%# Bind("IdFondo") %>' MaxLength="2" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nombre" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNomFondo" runat="server"  Text='<%# Bind("NomFondo") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarNomFondo" runat="server"  Text='<%# Bind("NomFondo") %>' MaxLength="18" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Estado" >
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstado" runat="server"   TextMode="Number"  Text='<%# Bind("Estado") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
				                                <asp:TextBox ID="txtEditarEstado" runat="server" Width="50%"   TextMode="Number"  Text='<%# Bind("Estado") %>' MaxLength="100"  />
                                            </EditItemTemplate> 
                                        </asp:TemplateField>

                                    </Columns>  

                                    <EditRowStyle BackColor="#999999" />

                                </asp:GridView>
    </div>
</asp:Content>
