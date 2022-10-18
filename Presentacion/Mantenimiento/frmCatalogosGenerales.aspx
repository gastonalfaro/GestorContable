<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCatalogosGenerales.aspx.cs" Inherits="Presentacion.Mantenimiento.frmCatalogosGenerales" %>
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
        <asp:Button ID="btnCatalogoNuevo" runat="server" Text="NUEVO" OnClick="btnCatalogoNuevo_Click" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnCatalogoGuardar" runat="server" Text="GUARDAR" OnClick="btnCatalogoGuardar_Click" Visible="false" CssClass="ButtonNeutro" />
        <asp:Button ID="btnCatalogoVolver" runat="server" Text="VOLVER" OnClick="btnCatalogoVolver_Click" Visible="false" CssClass="ButtonNeutro" />
    </div>
     <div class="col-md-12" id="tblCatalogos">
         <h3>CATÁLOGOS GENERALES</h3>
         <p>Mantenimiento de Catálogos Generales del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqIdCatalogo" runat="server" CssClass="FormatoTextBox" onkeypress="return AceptarSoloNumeros(event)" ></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqNomCatalogo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnCatalogoConsultar" runat="server" Text="CONSULTAR" OnClick="btnCatalogoConsultar_Click" CssClass="ButtonNeutro" /></div>
            <div class="col-md-12" style="text-align:center;"> <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>   
     </div>
    <div style="width: 100%;  overflow: auto">
        <asp:GridView ID="grdvCatalogos" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvCatalogos_SelectedIndexChanged" OnRowEditing="grdvCatalogos_RowEditing"
            OnRowUpdating="grdvCatalogos_RowUpdating" OnPageIndexChanging="grdvCatalogos_PageIndexChanging"
            OnRowCancelingEdit="grdvCatalogos_RowCancelingEdit" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Código"  > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdCatalogo" runat="server" Text='<%# Bind("IdCatalogo") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdCatalogo" runat="server"  Text='<%# Bind("IdCatalogo") %>' MaxLength="4" />
                                </FooterTemplate>
                        
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoCatalogo" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNombre" runat="server" Width="90%" Text='<%# Bind("Nombre") %>' MaxLength="100"  />
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
