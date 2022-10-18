<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalSistemaGestor.Master" AutoEventWireup="true" CodeBehind="PerfilUsuario.aspx.cs" Inherits="Presentacion.Perfil.PerfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
    <div class="rmm">
        <ul runat="server">
            <li id="liOBJ_CI" runat="server" visible="false"><a href='~/CapturaIngresos/frmCapturaIngresos.aspx' runat="server">Captura de Ingresos</a></li>
            <li id="liOBJ_RN" runat="server" visible="false"><a href="~/RevelacionNotas/Formularios.aspx" runat="server">Revelación Notas</a></li>
            <li id="liOBJ_PC" runat="server" visible="false"><a href='' runat="server">Plan.Consolidación</a></li>
            <li id="liOBJ_CF" runat="server" visible="false"><a href='' runat="server">Cálculos Financieros</a></li>
            <li id="liOBJ_CO" runat="server" visible="false"><a href='~/Contingentes/Pretenciones.aspx' runat="server">Contingentes</a></li>
            <li id="liOBJ_MA" runat="server" visible="false"><a href='~/Mantenimiento/frmModulos.aspx' runat="server">Mantenimiento</a></li>
            <li id="liOBJ_SG" runat="server" visible="true"><a href="~/Seguridad/Usuarios.aspx" runat="server">Seguridad</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <link href="../../Compartidas/EstiloPagina.css" rel="stylesheet" type="text/css" />
    <h2>Perfil de Usuario</h2>
    <table style="width: 100%;">
        <tr>
            <td style="width: 117px">
                <asp:Label ID="lblTipoIdentificacion" runat="server" Text="Tipo Identificación"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTipoIDentificacion" runat="server" ReadOnly="True"></asp:TextBox>

            </td>
            <td>
                <asp:Label ID="lblCedula" runat="server" Text="Cédula"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtIdUsuario" runat="server" ReadOnly="True"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td style="width: 117px">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre de Usuario"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNombreUsuario" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="lblInstitucion" runat="server" Text="Institución"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtInstitucion" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 117px">
                <asp:Label ID="lblCorreo" runat="server" Text="Correo electrónico"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCorreoUsr" runat="server" ></asp:TextBox>
            </td>
            <td>
                <asp:CheckBox ID="chkAdmin" runat="server" Text="Administrador" TextAlign="Left" Enabled="False" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 174px">
                <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" TextAlign="Left" Enabled="False" />
            </td>
            <td>&nbsp;</td>
            <td>
                <asp:CheckBox ID="chkCtaHabilitada" runat="server" Text="Cuenta habilitada" TextAlign="Left" Enabled="False" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    <div runat="server" id="divBitacora">
        <asp:Button ID="btnGuardarPerfil" runat="server" Text="Guardar" OnClick="btnGuardarPerfil_Click" />
        <br />
    </div>
    <br />
    <h3>Roles</h3>
    <br />
    <asp:Label ID="lblExistenRoles" runat="server" Text="Label" Visible="False"></asp:Label>
    <br />
    <asp:UpdatePanel ID="uplRoles" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvRolesUsuario" runat="server" AutoGenerateColumns="False" ShowFooter="True" ForeColor="#333333" PageSize="10" AllowPaging="True">
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
                            <%# (Boolean.Parse(Eval("Habilitado").ToString())) ? "Habilitado" : "Deshabilitado" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <br />
    <br />
    <table>
        <tr>
            <td style="width: 243px">&nbsp;</td>
            <td>&nbsp;</td>
            <td></td>
        </tr>
    </table>
</asp:Content>
