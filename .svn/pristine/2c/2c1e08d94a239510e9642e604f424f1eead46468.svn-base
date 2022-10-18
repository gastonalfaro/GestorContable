<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmFondos.aspx.cs" Inherits="Presentacion.Mantenimiento.frmFondos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <%--  <asp:Button ID="btnNuevosFondos" runat="server" Text="NUEVO" OnClick="btnNuevosFondos_Click" />
            <asp:Button ID="btnGuardarFondos" runat="server" Text="GUARDAR" OnClick="btnGuardarFondos_Click" Visible="false" />
            <asp:Button ID="btnVolverFondos" runat="server" Text="VOLVER" OnClick="btnVolverFondos_Click" Visible="false" />
        --%>
    </div> 
    <div class="col-md-12" id="tblFondos">
         <h3>FONDOS/FUENTES FINANCIAMIENTO</h3>
        <p>Consulta de Fuentes de Financiamiento (Fondos) del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblCodigo" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaCodigo" runat="server" Enabled="true" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaIdEntidadCP" runat="server" Text="Entidad CP:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdEntidadCP" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblBusquedaNomFondo" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaNomFondo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnConsultarFondo" runat="server" Text="CONSULTAR" OnClick="btnConsultarFondo_Click" CssClass="ButtonNeutro" /></div>
        <div class="col-md-12" style="text-align:center;"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
      
             
                 <asp:GridView ID="grdFondos" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
                      CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        Width="100%" OnSelectedIndexChanged="grdFondos_SelectedIndexChanged" OnRowEditing="grdFondos_RowEditing"
                        OnRowUpdating="grdFondos_RowUpdating" OnPageIndexChanging="grdFondos_PageIndexChanging"
                        OnRowCancelingEdit="grdFondos_RowCancelingEdit" AllowPaging="true" PageSize="20">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Código"  > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdFondo" runat="server" Text='<%# Bind("IdFondo") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarIdFondo" runat="server" Text='<%# Bind("IdFondo") %>' MaxLength="2" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descripción" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdNomFondo" runat="server"  Text='<%# Bind("NomFondo") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarNomFondo" runat="server"  Text='<%# Bind("NomFondo") %>' MaxLength="18" />
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Entidad CP" > 
                                            <ItemTemplate>
                                                <asp:Label ID="lblIdEntidadCP" runat="server"  Text='<%# Bind("IdEntidadCP") %>'></asp:Label>
                                            </ItemTemplate>                           
                                            <FooterTemplate>
				                                <asp:TextBox ID="txtInsertarIdEntidadCP" runat="server"  Text='<%# Bind("IdEntidadCP") %>' MaxLength="18" />
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
