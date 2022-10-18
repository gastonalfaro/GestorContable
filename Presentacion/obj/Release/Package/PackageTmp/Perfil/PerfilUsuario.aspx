<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="PerfilUsuario.aspx.cs" Inherits="Presentacion.Perfil.PerfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
     <style type="text/css">
        .FormatoTextBox, .FormatoDropDownList { width:100%!important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div id="Datos" style="display:inline-block;">
    <div class="col-md-12"><h2>Perfil de Usuario</h2></div>
    
     <div class="col-md-6">					
        <div class="row">
            <div class="col-md-3"><asp:Label ID="lblTipoIdentificacion" runat="server" Text="Tipo Identificación"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtTipoIDentificacion" runat="server" ReadOnly="True"  CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
          <div class="row">
            <div class="col-md-3"><asp:Label ID="lblNombre" runat="server" Text="Nombre de Usuario"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtNombreUsuario" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
          <div class="row">
            <div class="col-md-3"><asp:Label ID="lblCorreo" runat="server" Text="Correo electrónico"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCorreoUsr" runat="server"  CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
          <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-7"><asp:CheckBox ID="chkAdmin" runat="server" Text="Administrador" TextAlign="Left" Enabled="False" /></div>
        </div>
    </div>
    <div class="col-md-6">					
        <div class="row">
            <div class="col-md-3"><asp:Label ID="lblCedula" runat="server" Text="Cédula"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtIdUsuario" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
          <div class="row">
            <div class="col-md-3"><asp:Label ID="lblInstitucion" runat="server" Text="Institución"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtInstitucion" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
          <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-7"><asp:CheckBox ID="chkActivo" runat="server" Text="Activo" TextAlign="Left" Enabled="False" /></div>
        </div>
          <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-7"> <asp:CheckBox ID="chkCtaHabilitada" runat="server" Text="Cuenta habilitada" TextAlign="Left" Enabled="False" /></div>
        </div>
    </div>
    <br />
    <div runat="server" id="divBitacora"  class="col-md-12" style="text-align:center;">
        <asp:Button ID="btnGuardarPerfil" runat="server" Text="Guardar" OnClick="btnGuardarPerfil_Click" CssClass="ButtonNeutro"/>
        <br />
    </div>
    <br />
    
    <div  class="col-md-12" style="text-align:center;">
        <h3>Roles</h3>
        <br />
        <asp:Label ID="lblExistenRoles" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:UpdatePanel ID="uplRoles" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvRolesUsuario" runat="server" AutoGenerateColumns="False" ShowFooter="True" ForeColor="#333333" PageSize="10" AllowPaging="True"
                      CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnPageIndexChanging="gvRolesUsuario_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="FchModifica" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblFchModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IdRol" Visible="false">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtIdRol" runat="server" Text='<%# Bind("IdRol") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIdRol" runat="server" Text='<%# Bind("IdRol") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Descripción">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDescRol" runat="server" Text='<%# Bind("DescRol") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDescRol" runat="server" Text='<%# Bind("DescRol") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <%# (!String.IsNullOrEmpty(Eval("Habilitado").ToString()) && Boolean.Parse(Eval("Habilitado").ToString()))  ? "Habilitado" : "Deshabilitado" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
    </div>

    </div>
</asp:Content>
