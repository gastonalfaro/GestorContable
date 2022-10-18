<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmGruposCuentas.aspx.cs" Inherits="Presentacion.Mantenimiento.frmGruposCuentas" %>
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
        <asp:Button ID="btnNuevosGruposCuentas" runat="server" Text="NUEVO" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnGuardarGruposCuentas" runat="server" Text="GUARDAR"  Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnVolverGruposCuentas" runat="server" Text="VOLVER"  Visible="false" CssClass="ButtonNeutro"/>     
    </div> 
    <div class="col-md-12" id="tblGruposCuentas">
        <h3>GruposCuentas</h3>
        <p>Mantenimiento de GruposCuentas del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblInstitucion" runat="server" Text="Institución:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtInstitucion" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
            <div class="col-md-5"><asp:TextBox ID="txtNomInstitucion" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaGrupoCuenta" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdGrupoCuenta" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaNomGrupoCuenta" runat="server" Text="Nombre:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaNomGrupoCuenta" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"> <asp:Label ID="lblBusquedaIdMoneda" runat="server" Text="Moneda:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtBusquedaIdMoneda" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"> <asp:Button ID="btnConsultarGrupoCuenta" runat="server" Text="CONSULTAR"  CssClass="ButtonNeutro"/></div>
         <div class="col-md-12" style="text-align:center;"> <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
       
                    <asp:GridView ID="grdGruposCuentas" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
                         CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        Width="100%" 
                       OnSelectedIndexChanged="grdGruposCuentas_SelectedIndexChanged" OnRowEditing="grdGruposCuentas_RowEditing"
                        OnRowUpdating="grdGruposCuentas_RowUpdating" OnRowUpdated="grdGruposCuentas_RowUpdated" OnPageIndexChanging="grdGruposCuentas_PageIndexChanging"
                        OnRowCancelingEdit="grdGruposCuentas_RowCancelingEdit" 
                        AllowPaging="true" PageSize="20">
                                    <Columns>

                                        <asp:CommandField ShowEditButton="False" />

                                        <asp:TemplateField HeaderText="Dirección:" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdGrupoCuenta" runat="server" Text='<%# Bind("IdGrupoCuenta") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarIdGrupoCuenta" runat="server" Text='<%# Bind("IdGrupoCuenta") %>' MaxLength="2" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nombre" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNomGrupoCuenta" runat="server"  Text='<%# Bind("NomGrupoCuenta") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarNomGrupoCuenta" runat="server"  Text='<%# Bind("NomGrupoCuenta") %>' MaxLength="18" />
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
