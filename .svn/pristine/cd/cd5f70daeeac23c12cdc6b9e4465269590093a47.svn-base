<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmPropietarios.aspx.cs" Inherits="Presentacion.Mantenimiento.frmPropietarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones">
        <asp:Button ID="btnPropietarioNuevo" runat="server" Text="NUEVO" OnClick="btnPropietarioNuevo_Click" CssClass="ButtonNeutro"/>
        <%--<asp:Button ID="btnPropietarioGuardar" runat="server" Text="GUARDAR" OnClick="btnPropietarioGuardar_Click" Visible="false" />--%>
        <asp:Button ID="btnPropietariosVolver" runat="server" Text="VOLVER" OnClick="btnPropietariosVolver_Click" Visible="false" CssClass="ButtonNeutro" />
               
    </div> 
    <div class="col-md-12" id="tblParametros">
        <h3>Propietarios</h3>
        <p>Consulta de Propietarios del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"> <asp:TextBox ID="txtBusqIdPropietario" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-5"><asp:TextBox ID="txtBusqNomPropietario" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnPropietariosConsultar" runat="server" Text="CONSULTAR" OnClick="btnPropietariosConsultar_Click" CssClass="ButtonNeutro" /></div>
    </div>
    <div class="col-md-12" ><asp:Label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true"></asp:Label></div>
    <div style="width: 100%; height: 100%; overflow: auto">
        
        <asp:GridView ID="grdvPropietarios" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
             CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            Width="100%" OnSelectedIndexChanged="grdvPropietarios_SelectedIndexChanged" OnRowEditing="grdvPropietarios_RowEditing"
            OnRowUpdating="grdvPropietarios_RowUpdating" OnRowUpdated="grdvPropietarios_RowUpdated" OnPageIndexChanging="grdvPropietarios_PageIndexChanging"
            OnRowCancelingEdit="grdvPropietarios_RowCancelingEdit" AllowPaging="true" PageSize="20">
            <Columns>

                <asp:TemplateField HeaderText="Código">
                    <ItemTemplate>
                        <asp:Label ID="lblIdPropietario" runat="server" Text='<%# Bind("IdPropietario") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtInsertIdPropietario" runat="server" Text='<%# Bind("IdPropietario") %>' />
                    </FooterTemplate>

                </asp:TemplateField>
                  <asp:TemplateField HeaderText="FechaModifica" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblFchModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Sociedad" Visible="false">
                    <FooterTemplate>
                        <asp:TextBox ID="txtSociedadGL" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdSociedadGL" runat="server" Text='<%# Bind("IdSociedadGL") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditarIdSociedadGL" runat="server" Text='<%# Bind("IdSociedadGL") %>'  />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Sociedad Fi" Visible="false">
                    <FooterTemplate>
                        <asp:TextBox ID="txtSociedadFi" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdSociedadFi" runat="server" Text='<%# Bind("IdSociedadFi") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditarIdSociedadFi" runat="server" Text='<%# Bind("IdSociedadFi") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Descripción">
                    <FooterTemplate>
                        <asp:TextBox ID="txtNomPropietario" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNomPropietario" runat="server" Text='<%# Bind("NomPropietario") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditarNomPropietario" runat="server" Text='<%# Bind("NomPropietario") %>' />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Estado">
                    <FooterTemplate>
                        <asp:TextBox ID="txtEstado" runat="server"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("Estado") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditarEstado" runat="server"  Text='<%# Bind("Estado") %>'/>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                    <EditItemTemplate> 
                        <asp:LinkButton ID="lnkActualizarDir" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                        <asp:LinkButton ID="lnkCancelarDir" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                    </EditItemTemplate> 
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtEditarDireccion" runat="server" CausesValidation="False" Text="Editar" CommandName="Edit" ></asp:LinkButton>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="lbtAgregarDireccion" runat="server" CausesValidation="False" Text="Agregar" CommandName="Select" ></asp:LinkButton>
                    </FooterTemplate>
                </asp:TemplateField>

            </Columns>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
    </div>
</asp:Content>
