<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Presentacion.Seguridad.Usuarios" %>
<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContenidoJS" runat="server">
				<script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script> 
        <script src="Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript">
    </script> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server" >
		<div class="col-md-12">
        <h3>Gestión de Usuarios</h3>
        <p>Consulta de Usuarios del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusIdUsuario" runat="server" Text="Cédula:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtBusqIdUsuario" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqNomUsuario" runat="server" Text="Nombre:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtBusqNomUsuario" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;">
          <asp:Button ID="btnUsuarioConsultar" runat="server" Text="CONSULTAR" CssClass="ButtonNeutro" OnClick="btnUsuarioConsultar_Click" />
        </div>
    </div>
    <br />
    <div style="width:100%;overflow-x: scroll;">
    <asp:GridView ID="gvUsuarios" runat="server" AutoGenerateColumns="False" OnRowEditing="gvUsuarios_Edicion" ShowHeaderWhenEmpty="True" ForeColor="#333333" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvUsuarios_PageIndexChanging" OnRowCommand="gvUsuarios_RowCommand"
          CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
        <Columns>
            <asp:TemplateField HeaderText="Cédula">
                <ItemTemplate> 
                    <asp:Label ID="lblIdUsuario" runat="server" Text='<%# Bind("IdUsuario") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Nombre">
                <ItemTemplate> 
                    <asp:Label ID="lblNomUsuario" runat="server" Text='<%# Bind("NomUsuario") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Correo electrónico">
                <ItemTemplate> 
                    <asp:Label ID="lblCorreoUsuario" runat="server" Text='<%# Bind("CorreoUsuario") %>'></asp:Label> 
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Habilitado">
                <ItemTemplate>
                    <asp:CheckBox ID="chkConsultar" runat="server" Checked='<%# Bind("CtaHabilitada") %>' Enabled="false" /> 
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Administrador">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAdministrador" runat="server" Checked='<%# Bind("Administrador") %>' Enabled="false" /> 
                </ItemTemplate> 
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Activo">
                <ItemTemplate>
                    <asp:CheckBox ID="chkActivo" runat="server" Checked='<%# Bind("Activo") %>' Enabled="false" /> 
                </ItemTemplate> 
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="Consulta" HeaderText="Consultar">
                <ItemTemplate>
                    <asp:Image ID="imgPretension" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-search.png" Height="20px" Width="20px"/>
                    <asp:LinkButton ID="lnkConsultar" runat="server" CausesValidation="False" CommandName="consulta" CommandArgument='<%#Eval("IdUsuario")%>' Text="Consultar"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Modificar" ShowHeader="False" HeaderStyle-HorizontalAlign="Left"> 
                <ItemTemplate>
                    <asp:Image ID="mgEditar" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-document-edit.png" Height="20px" Width="20px"/> 
                    <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="False" CommandName="Edit" Text="Modificar"></asp:LinkButton> 
                </ItemTemplate> 
           </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
    </asp:GridView>
    </div>
    <br />
    <br />
    <br />
    <div id="dialog-confirm"></div>
    </asp:Content>

