<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmIndicadoresEconomicos.aspx.cs" Inherits="Presentacion.Mantenimiento.frmIndicadoresEconomicos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
     <div class="FormatoBotones">
        <asp:Button ID="btnCatalogoNuevo" runat="server" Text="NUEVO" OnClick="btnCatalogoNuevo_Click" CssClass="ButtonNeutro"/>
        <%--<asp:Button ID="btnCatalogoGuardar" runat="server" Text="GUARDAR" OnClick="btnCatalogoGuardar_Click" Visible="false" CssClass="ButtonNeutro" />--%>
        <%--<asp:Button ID="btnCatalogoVolver" runat="server" Text="VOLVER" OnClick="btnCatalogoVolver_Click" Visible="false" CssClass="ButtonNeutro" />--%>
    </div>
    <div class="col-md-12" id="tblIndicadoress">
        <h3>INDICADORES ECONÓMICOS</h3>
        <p>Consultas de Indicadores Económicos del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqIdIndicadores" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"> <asp:Label ID="Label3" runat="server" Text="Transacción BCCR:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqTransaccion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqNomIndicadores" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-12" style="text-align:center;">
                <asp:Button ID="btnIndicadoresConsultar" runat="server" Text="CONSULTAR" OnClick="btnIndicadoresConsultar_Click" CssClass="ButtonNeutro" />
                &nbsp<asp:Button ID="btnIndicadoresActualizar" runat="server" Text="Actualizar" OnClick="btnIndicadoresActualizar_Click" CssClass="ButtonNeutro" />                
            </div>
            <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
        </div>
    <div style="width: 100%; height: 100%; overflow: auto">
        
        <asp:GridView ID="gvpIndicadoresEconomicos" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="gvpIndicadoresEconomicos_SelectedIndexChanged" OnRowEditing="gvpIndicadoresEconomicos_RowEditing"
            OnRowUpdating="gvpIndicadoresEconomicos_RowUpdating" OnRowUpdated="gvpIndicadoresEconomicos_RowUpdated" OnPageIndexChanging="gvpIndicadoresEconomicos_PageIndexChanging"
            OnRowCancelingEdit="gvpIndicadoresEconomicos_RowCancelingEdit" AllowPaging="true" PageSize="20">
                <Columns>

                    <asp:TemplateField HeaderText="Código" ItemStyle-HorizontalAlign="Center"> 
                        <ItemTemplate>
                            <asp:Label ID="lblIdIndicadores" runat="server" Text='<%# Bind("IdIndicadorEco") %>'></asp:Label>
                        </ItemTemplate>                           
                        <FooterTemplate>
				            <asp:TextBox ID="txtInsertIdIndicadores" runat="server"  Text='<%# Bind("IdIndicadorEco") %>' MaxLength="4" />
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Descripción" >
                        <FooterTemplate>
                            <asp:TextBox ID="txtNomNuevoIndicadores" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNomIndicadores" runat="server" Text='<%# Bind("NomIndicador") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
				            <asp:TextBox ID="txtEditNomIndicadores" runat="server" Width="90%" Text='<%# Bind("NomIndicador") %>' MaxLength="100"  />
                        </EditItemTemplate> 
                    </asp:TemplateField>

<%--                    <asp:TemplateField HeaderText="Transacción" > 
                        <ItemTemplate>
                            <asp:Label ID="lblTransacion" runat="server" Text='<%# Bind("Transaccion") %>'></asp:Label>
                        </ItemTemplate>                           
                        <FooterTemplate>
				            <asp:TextBox ID="txtInsertTransaccion" runat="server"  Text='<%# Bind("Transaccion") %>' MaxLength="10" />
                        </FooterTemplate>
                    </asp:TemplateField>--%>

                      <asp:TemplateField HeaderText="Transacción" >
                        <FooterTemplate>
                            <asp:TextBox ID="txtNuevoTransaccion" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTransaccion" runat="server" Text='<%# Bind("Transaccion") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
				            <asp:TextBox ID="txtEditTransaccion" runat="server" Width="90%" Text='<%# Bind("Transaccion") %>' MaxLength="1"  />
                        </EditItemTemplate> 
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                        <FooterTemplate>
                            <asp:TextBox ID="txtNuevoEstado" runat="server"></asp:TextBox>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
				            <asp:TextBox ID="txtEditarEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100"  />
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

