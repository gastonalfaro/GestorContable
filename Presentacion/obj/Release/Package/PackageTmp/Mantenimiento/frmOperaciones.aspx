<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmOperaciones.aspx.cs" Inherits="Presentacion.Mantenimiento.frmOperaciones" %>
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
        <asp:Button ID="btnOperacionNuevo" runat="server" Text="NUEVO" OnClick="btnOperacionNuevo_Click" CssClass="ButtonNeutro" />
        <asp:Button ID="btnOperacionGuardar" runat="server" Text="GUARDAR" OnClick="btnOperacionGuardar_Click" Visible="false" CssClass="ButtonNeutro" />
        <asp:Button ID="btnOperacionVolver" runat="server" Text="VOLVER" OnClick="btnOperacionVolver_Click" Visible="false"  CssClass="ButtonNeutro"/>
    </div> 
    <div class="col-md-12" id="tblOperaciones">
        <h3>OPERACIONES</h3>
        <p>Mantenimiento de Operaciones del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaIdOperacion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusquedaNomOperacion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblbusqModulo" runat="server" Text="Módulo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:DropDownList ID="ddlBusquedaModulo" runat="server" CssClass="FormatoTextBox"></asp:DropDownList></div>
        </div>
        
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnOperacionConsultar" runat="server" Text="CONSULTAR" OnClick="btnOperacionConsultar_Click" CssClass="ButtonNeutro"/></div>
        <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
                    
          <asp:GridView ID="grdOperaciones" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdOperaciones_SelectedIndexChanged" OnRowEditing="grdOperaciones_RowEditing"
            OnRowDataBound="grdOperaciones_RowDataBound" OnRowUpdating="grdOperaciones_RowUpdating" OnPageIndexChanging="grdOperaciones_PageIndexChanging" 
            OnDataBound="grdOperaciones_DataBound"
            OnRowCancelingEdit="grdOperaciones_RowCancelingEdit" PageSize="20" AllowPaging="True" ShowFooter="true">
                        <Columns>
                            <asp:TemplateField HeaderText="Código"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdOperacion" runat="server" Text='<%# Bind("IdOperacion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Módulo">
                                <ItemTemplate>
                                    <asp:Label ID="lblModulo" runat="server" Text='<%# Bind("IdModulo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Bind("NomOperacion") %>'></asp:Label>
                                
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarNomOperacion" runat="server" Text='<%# Bind("NomOperacion") %>' />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Operación Reversa" >
                                <ItemTemplate>
                                    <asp:Label ID="lblIdOperacionReversa" runat="server" Text='<%# Bind("IdOperacionReversa") %>'></asp:Label>
                                
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarIdOperacionReversa" runat="server" Text='<%# Bind("IdOperacionReversa") %>' />
                                </EditItemTemplate>
                               
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Clase Documento" >
                                <ItemTemplate>
                                    <asp:Label ID="lblClaseDocumento" runat="server" Text='<%# Bind("IdClaseDoc") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblClaseDocumento" runat="server" Text='<%# Eval("IdClaseDoc")%>' Visible = "false"></asp:Label>
                                    <asp:DropDownList ID="ddlEditarClaseDocumentos" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Estado" >
                                <ItemTemplate>
                                    <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditarEstado" runat="server" Text='<%# Bind("Estado") %>' />
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
                                    <asp:LinkButton ID="lnkActualizarDir" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                                    <asp:LinkButton ID="lnkCancelarDir" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                                </EditItemTemplate> 
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtEditarDireccion" runat="server" CausesValidation="False" Text="Editar" CommandName="Edit" ></asp:LinkButton>
                                </ItemTemplate>
<%--                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtAgregarDireccion" runat="server" CausesValidation="False" Text="Agregar" CommandName="Select" ></asp:LinkButton>
                                </FooterTemplate>--%>
                            </asp:TemplateField>
                             <asp:TemplateField>
															 <ItemTemplate>
																 <asp:HyperLink ID="lnkConsultarTiposAsientos" runat="server" Text="Tipos Asientos"
																	  NavigateUrl='<%# Eval("IdOperacion", @"~/Mantenimiento/frmTiposAsiento.aspx?IdOperacion={0}")%>'></asp:HyperLink>
															 </ItemTemplate>
                             </asp:TemplateField>
                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
    </div>
</asp:Content>

