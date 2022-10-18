<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmModulos.aspx.cs" Inherits="Presentacion.Mantenimiento.frmModulos" %>
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
        <asp:Button ID="btnModuloNuevo" runat="server" Text="NUEVO" OnClick="btnModuloNuevo_Click" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnModuloGuardar" runat="server" Text="GUARDAR" OnClick="btnModuloGuardar_Click" Visible="false" CssClass="ButtonNeutro"/>
        <asp:Button ID="btnModuloVolver" runat="server" Text="VOLVER" OnClick="btnModuloVolver_Click" Visible="false" CssClass="ButtonNeutro"/>
    </div> 
    <div class="col-md-12" id="tblModulos">
            <h3>MÓDULOS</h3>
            <p>Mantenimiento de Módulos del Sistema Gestor.</p>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqIdModulo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtBusqNomModulo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnModuloConsultar" runat="server" Text="CONSULTAR" OnClick="btnModuloConsultar_Click" CssClass="ButtonNeutro" /></div>
            <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
        </div>
    <div style="width: 100%; height: 100%; overflow: auto">
                <asp:GridView ID="grdvModulos" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True" ForeColor="#333333"
                        CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
                        Width="100%" OnSelectedIndexChanged="grdvModulos_SelectedIndexChanged" OnRowEditing="grdvModulos_RowEditing"
                        OnRowUpdating="grdvModulos_RowUpdating" OnRowUpdated="grdvModulos_RowUpdated" OnPageIndexChanging="grdvModulos_PageIndexChanging"
                        OnRowCancelingEdit="grdvModulos_RowCancelingEdit" AllowPaging="true" PageSize="20">
                        <Columns>

                            <asp:TemplateField HeaderText="Código" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblIdModulo" runat="server" Text='<%# Bind("IdModulo") %>'></asp:Label>
                                </ItemTemplate>                           
                                <FooterTemplate>
				                    <asp:TextBox ID="txtInsertIdModulo" runat="server"  Text='<%# Bind("IdModulo") %>' MaxLength="4" />
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Descripción" >
                                <FooterTemplate>
                                    <asp:TextBox ID="txtNomNuevoModulo" runat="server"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNomModulo" runat="server" Text='<%# Bind("NomModulo") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
				                    <asp:TextBox ID="txtEditNomModulo" runat="server" Width="90%" Text='<%# Bind("NomModulo") %>' MaxLength="100"  />
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

                        </Columns>  

                        <EditRowStyle BackColor="#999999" />

                    </asp:GridView>
        </div>
</asp:Content>
