<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="EdicionUsuario.aspx.cs" Inherits="Presentacion.Seguridad.GestionUsuarios.EdicionUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
     <style type="text/css">         
        .FormatoTextBox, .FormatoDropDownList { width:100%!important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
     <div class="col-md-12"><h2>Edición de Usuarios</h2></div>
    <div class="col-md-12"> <asp:Label runat="server"  ID="lblTitulosDatos" CssClass="titulo2" Text="Datos Personales"></asp:Label></div>
     <div  class="col-md-6">       
         <div class="row">
            <div class="col-md-5"><asp:Label ID="lblTipoIdentificacion" runat="server" Text="Tipo Identificación" ></asp:Label></div>
            <div class="col-md-7">   <asp:TextBox ID="txtTipoIDentificacion" runat="server" ReadOnly="True" CssClass="FormatoTextBox" ></asp:TextBox></div>  
        </div>
         <div class="row">
            <div class="col-md-5"><asp:Label ID="lblNombre" runat="server" Text="Nombre de Usuario"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtNombreUsuario" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox></div>  
        </div>
          <div class="row">
            <div class="col-md-5"><asp:Label ID="lblCorreo" runat="server" Text="Correo electrónico"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCorreoUsr" runat="server" CssClass="FormatoTextBox"></asp:TextBox><br />
                 <asp:RegularExpressionValidator runat="server" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ControlToValidate="txtCorreoUsr" ForeColor="Red" ErrorMessage="Dirección de correo electrónico inválida." /></div>  
        </div>
    </div>
      <div  class="col-md-6">       
         <div class="row">
            <div class="col-md-5"> <asp:Label ID="lblCedula" runat="server" Text="Cédula" ></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtIdUsuario" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox></div>  
        </div>
         <div class="row">
            <div class="col-md-5"><asp:Label ID="lblInstitucion" runat="server" Text="Institución"></asp:Label></div>
            <div class="col-md-7"> <asp:DropDownList ID="ddlInstitucion" runat="server"  CssClass="FormatoDropDownList"></asp:DropDownList></div>  
        </div>
          <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-7"><asp:CheckBox ID="chkAdmin" runat="server" Text="Administrador" TextAlign="Left" /></div>  
        </div>
    </div>
    
    <br />
   
    <div runat="server" id="divBitacora">
         <div  class="col-md-6">       
             <div class="row">
                <div class="col-md-5"><asp:Label ID="lblFchRegistro" runat="server" Text="Fecha de registro"></asp:Label></div>
                <div class="col-md-7"><asp:TextBox ID="txtFchRegistro" runat="server" ReadOnly="True"  CssClass="FormatoTextBox"></asp:TextBox></div>  
            </div>
              <div class="row">
                <div class="col-md-5"><asp:Label ID="lblCambioClave" runat="server" Text="Fecha último cambio de clave"></asp:Label></div>
                <div class="col-md-7"><asp:TextBox ID="txtFchCambioClave" runat="server" ReadOnly="True"  CssClass="FormatoTextBox"></asp:TextBox></div>  
            </div>
              <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-7"><asp:CheckBox ID="chkActivo" runat="server" Text="Activo" TextAlign="Left" Enabled="False" /></div>  
            </div>
        </div>
           <div  class="col-md-6">       
             <div class="row">
                <div class="col-md-5"><asp:Label ID="lblUltimaSesion" runat="server" Text="Fecha de última sesión"></asp:Label></div>
                <div class="col-md-7"><asp:TextBox ID="txtFchUltimaSesion" runat="server" ReadOnly="True"  CssClass="FormatoTextBox"></asp:TextBox></div>  
            </div>
              <div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-7"><asp:CheckBox ID="chkCtaHabilitada" runat="server" Text="Cuenta habilitada" TextAlign="Left" /></div>  
            </div>              
        </div>
        <br />
        </div>
            <br />
            <br />
            <div class="col-md-12"><h3>Roles de Usuario</h3></div>
            <div class="col-md-12"><asp:Label ID="lblExistenRoles" runat="server" Text="No existen roles" Visible="False"></asp:Label></div>
            <br />
        <div style="margin:auto;width:100%;">
             <asp:UpdatePanel ID="uplRoles" runat="server">
                 <ContentTemplate>
                     <asp:GridView ID="gvRolesUsuario" runat="server" AutoGenerateColumns="False" ShowFooter="True" ShowHeaderWhenEmpty="True" OnDataBound="gvRolesUsuarioDataBound" OnSelectedIndexChanged="gvRolesUsuario_SelectedIndexChanged" OnRowDeleting="gvRolesUsuario_EliminarRol"
                           CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
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
                                 <FooterTemplate>
                                     <asp:DropDownList ID="ddlRoles" runat="server" DataTextField="&quot;DescRol&quot;" DataValueField="&quot;IdRol&quot;">
                                     </asp:DropDownList>
                                 </FooterTemplate>
                                 <ItemTemplate>
                                     <asp:Label ID="lblDescRol" runat="server" Text='<%# Bind("DescRol") %>'></asp:Label>
                                 </ItemTemplate>
                             </asp:TemplateField>
                             <asp:TemplateField HeaderText="Estado">
                                 <ItemTemplate>
                                     <%# (!String.IsNullOrEmpty(Eval("Habilitado").ToString()) && Boolean.Parse(Eval("Habilitado").ToString())) ? "Habilitado" : "Deshabilitado" %>
                                 </ItemTemplate>
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Image ID="imgAnular" runat="server" ImageUrl="~/Compartidas/imagenes/anular.png" Height="20px" Width="20px"/> 
                                    <asp:LinkButton ID="lnkEliminar" runat="server" CausesValidation="False" CommandName="Delete" Text="Eliminar"></asp:LinkButton> 
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Image ID="imgAnular" runat="server" ImageUrl="~/Compartidas/imagenes/plus.png" Height="20px" Width="20px"/> 
                                    <asp:LinkButton ID="lnkAgregar" runat="server" CausesValidation="False" CommandName="Select" Text="Agregar"></asp:LinkButton> 
                                </FooterTemplate>  
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                         </Columns>
                         <EditRowStyle BackColor="#999999" />
                     </asp:GridView>
                     <asp:Label ID="lblMensaje" runat="server" Text="Label" Font-Italic="True" ForeColor="#CC0000" Visible="False"></asp:Label>
                     <asp:HiddenField ID="hdnRoles" runat="server" value="" />
                 </ContentTemplate>
                 <Triggers>
                 </Triggers>
    </asp:UpdatePanel>
        </div>
            <br />
            <br />

        <div class="col-md-12">
            <div style="position:absolute;right:15px;">
                 <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar" OnClick="btnGuardarCambios_Click" CssClass="ButtonNeutro" />
                 <asp:Button ID="btnCancelar" runat="server" Text="Atrás" OnClick="btnCancelar_Click" CssClass="ButtonNeutro" />  
            </div>
        </div>
    <asp:HiddenField ID="hdnFchModificacion" runat="server" value="" />
    </asp:Content>
