<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmPlanesCuentas.aspx.cs" Inherits="Presentacion.Mantenimiento.frmPlanesCuentas" %>
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
        <asp:Button ID="btnNuevosPlanesCuentas" runat="server" Text="NUEVO" OnClick="btnNuevosPlanesCuentas_Click" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnGuardarPlanesCuentas" runat="server" Text="GUARDAR" OnClick="btnGuardarPlanesCuentas_Click" Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnVolverPlanesCuentas" runat="server" Text="VOLVER" OnClick="btnVolverPlanesCuentas_Click" Visible="false" CssClass="ButtonNeutro"/>
             
    </div> 
    <div class="col-md-12" id="tblPlanesCuentas">
         <h3>PlanesCuentas</h3>
        <p>Mantenimiento de PlanesCuentas del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblInstitucion" runat="server" Text="Institución:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-4"><asp:TextBox ID="txtInstitucion" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
            <div class="col-md-4"><asp:TextBox ID="txtNomInstitucion" runat="server" Enabled="false" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaPlanCuenta" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdPlanCuenta" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaNomPlanCuenta" runat="server" Text="Nombre:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaNomPlanCuenta" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"> <asp:Label ID="lblBusquedaIdMoneda" runat="server" Text="Moneda:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtBusquedaIdMoneda" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarPlanCuenta" runat="server" Text="CONSULTAR" OnClick="btnConsultarPlanCuenta_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
 
               <asp:GridView ID="grdPlanesCuentas" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
                        CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        Width="100%" OnSelectedIndexChanged="grdPlanesCuentas_SelectedIndexChanged" OnRowEditing="grdPlanesCuentas_RowEditing"
                        OnRowUpdating="grdPlanesCuentas_RowUpdating" OnRowUpdated="grdPlanesCuentas_RowUpdated" OnPageIndexChanging="grdPlanesCuentas_PageIndexChanging"
                        OnRowCancelingEdit="grdPlanesCuentas_RowCancelingEdit" AllowPaging="true" PageSize="20">
                                    <Columns>

                                        <asp:CommandField ShowEditButton="False" />

                                        <asp:TemplateField HeaderText="Dirección:" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdPlanCuenta" runat="server" Text='<%# Bind("IdPlanCuenta") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarIdPlanCuenta" runat="server" Text='<%# Bind("IdPlanCuenta") %>' MaxLength="2" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nombre" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNomPlanCuenta" runat="server"  Text='<%# Bind("NomPlanCuenta") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarNomPlanCuenta" runat="server"  Text='<%# Bind("NomPlanCuenta") %>' MaxLength="18" />
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
