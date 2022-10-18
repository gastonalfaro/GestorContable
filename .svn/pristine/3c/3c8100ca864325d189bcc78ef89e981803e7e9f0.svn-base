<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmTasaVariableTitulos_cp.aspx.cs" Inherits="Presentacion.Mantenimiento.frmTasaVariableTitulos_cp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"></div> 
    <div class="col-md-12" id="tblTasaVariableTitulos">
        <h3>Tasa Variable Títulos</h3>
        <p>Mantenimiento de Tasa Variable de Títulos del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Número Valor:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBuscarNroValor" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"> <asp:Label ID="Label2" runat="server" Text="Nemotécnico:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqNomTasaVariableTitulo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"> <asp:Button ID="btnTasaVariableTituloConsultar" runat="server" Text="CONSULTAR" OnClick="btnTasaVariableTituloConsultar_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
          <asp:GridView ID="grdvTasaVariableTitulos" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvTasaVariableTitulos_SelectedIndexChanged" OnRowEditing="grdvTasaVariableTitulos_RowEditing"
            OnRowUpdating="grdvTasaVariableTitulos_RowUpdating" OnPageIndexChanging="grdvTasaVariableTitulos_PageIndexChanging"
            OnRowCancelingEdit="grdvTasaVariableTitulos_RowCancelingEdit" OnRowDataBound="grdvTasaVariableTitulos_RowDataBound" PageSize="20" AllowPaging="True">
                        <Columns>

                            <asp:TemplateField HeaderText="Número Valor"  > 
                                <ItemTemplate>
                                    <asp:Label ID="lblNroValor" runat="server" Text='<%# Bind("NroValor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nemotécnico" >
                                <ItemTemplate>
                                    <asp:Label ID="lblNemotecnico" runat="server" Text='<%# Bind("Nemotecnico") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tasa Variable" >
                                <ItemTemplate>
                                    <asp:Label ID="lblTasaVariable" runat="server" Text='<%# Bind("TasaVariable") %>'></asp:Label>
                                    
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <%--<asp:TextBox ID="txtEditEstado" runat="server" Width="90%" Text='<%# Bind("Estado") %>' MaxLength="100"  />--%>
                                     <asp:Label ID="lblTasaVariable" runat="server" Text='<%# Eval("TasaVariable")%>' Visible = "false"></asp:Label>
                                    <asp:DropDownList ID="ddlTasaVariable" runat="server" OnSelectedIndexChanged="ddlTasaVariable_SelectedIndexChanged"></asp:DropDownList>
                                </EditItemTemplate> 
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Tasa Variable Valor" >
                                <ItemTemplate>
                                    <asp:Label ID="lblTasaVariableValor" runat="server" Text='<%# Bind("TasaVariableValor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Margen" >
                                <ItemTemplate>
                                    <asp:Label ID="lblMargen" runat="server" Text='<%# Bind("Margen") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="FchModifica" Visible="false" >
                                <ItemTemplate>
                                    <asp:Label ID="lblFchModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label>
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
                            </asp:TemplateField>

                        </Columns>    

                        <EditRowStyle BackColor="#999999" />

            </asp:GridView>
    </div>

</asp:Content>
