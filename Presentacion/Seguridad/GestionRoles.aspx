<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="GestionRoles.aspx.cs" Inherits="Presentacion.Seguridad.GestionRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
		 <div class="col-md-12">
        <h3>Gestión de Roles</h3>
        <p>Consulta de Roles del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqIdRol" runat="server" Text="Código:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtBusqIdRol" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqNomRol" runat="server" Text="Nombre:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtBusqNomRol" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;">
          <asp:Button ID="btnRolesConsultar" runat="server" Text="CONSULTAR" CssClass="ButtonNeutro" OnClick="btnRolesConsultar_Click" />
        </div>
    </div>
    <br />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvRoles" runat="server" OnRowEditing="gvRoles_EditarRol" AutoGenerateColumns="False"  ShowFooter="True" ShowHeaderWhenEmpty="True" OnRowDeleting="gvRoles_RowDeleting" OnSelectedIndexChanged="gvRoles_SelectedIndexChanged" PageSize="20" AllowPaging="True" OnPageIndexChanging="gvRoles_PageIndexChanging" 
                                CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                            <Columns>
                                <asp:TemplateField HeaderText="FchModificacion" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFchModificacion" runat="server" Text='<%# Bind("FchModifica") %>' Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdRol" runat="server" Text='<%# Bind("IdRol") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nombre">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtDescNuevoRol" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescRol" runat="server" Text='<%# Bind("DescRol") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Estado">
                                    <ItemTemplate>
                                        <%# (!String.IsNullOrEmpty(Eval("Habilitado").ToString()) && Boolean.Parse(Eval("Habilitado").ToString()))  ? "Habilitado" : "Deshabilitado" %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left"> 
                                    <ItemTemplate> 
                                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar"></asp:LinkButton> 
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkAgregar" runat="server" CausesValidation="False" CommandName="Select" Text="Agregar"></asp:LinkButton> 
                                    </FooterTemplate> 
                                </asp:TemplateField>
                            </Columns>
                           </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
       <asp:HiddenField ID="hdnDescRol" runat="server" value="" />
     </asp:Content>
