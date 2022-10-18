<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="DetallesUsuario.aspx.cs" Inherits="Presentacion.Seguridad.GestionUsuarios.DetallesUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
     <style type="text/css">         
        .FormatoTextBox, .FormatoDropDownList { width:100%!important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    
    <div class="col-md-12"><h2>Detalles de Usuario</h2></div>
    <div  class="col-md-6">       
        <div class="row">
            <div class="col-md-5"> <asp:Label ID="lblTipoIdentificacion" runat="server" Text="Tipo Identificación" ></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtTipoIDentificacion" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblNombre" runat="server" Text="Nombre de Usuario"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtNombreUsuario" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblCorreo" runat="server" Text="Correo electrónico"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtCorreoUsr" runat="server" ReadOnly="True"  CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
    </div>
      <div  class="col-md-6">       
        <div class="row">
            <div class="col-md-5"> <asp:Label ID="lblCedula" runat="server" Text="Cédula" ></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtIdUsuario" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblInstitucion" runat="server" Text="Institución"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtInstitucion" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-7"><asp:CheckBox ID="chkAdmin" runat="server" Text="Administrador" TextAlign="Left" Enabled="False" /></div>
        </div>
    </div>
    <br />
    <br />
      <div runat="server" id="divBitacora">
        <br />
     <div  class="col-md-6">       
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblFchRegistro" runat="server" Text="Fecha de registro"></asp:Label> </div>
            <div class="col-md-7"><asp:TextBox ID="txtFchRegistro" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox> </div>
        </div>
        <div class="row">
            <div class="col-md-5"> <asp:Label ID="lblCambioClave" runat="server" Text="Fecha último cambio de clave"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtFchCambioClave" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
          <div class="row">
            <div class="col-md-5"> </div>
            <div class="col-md-7">  <asp:CheckBox ID="chkActivo" runat="server" Text="Activo" TextAlign="Left" Enabled="False" /></div>
        </div>
    </div>
      <div  class="col-md-6">       
        <div class="row">
            <div class="col-md-5"><asp:Label ID="lblUltimaSesion" runat="server" Text="Fecha de última sesión"></asp:Label> </div>
            <div class="col-md-7"> <asp:TextBox ID="txtFchUltimaSesion" runat="server" ReadOnly="True" CssClass="FormatoTextBox"></asp:TextBox> </div>
        </div>
        <div class="row">
            <div class="col-md-5"> </div>
            <div class="col-md-7">  <asp:CheckBox ID="chkCtaHabilitada" runat="server" Text="Cuenta habilitada" TextAlign="Left" Enabled="False" /></div>
        </div>
      
    </div>
        <br />
  
       
        </div>
            <br />
         <div class="col-md-12"><h3>Roles</h3></div>
        <div class="col-md-12"><asp:Label ID="lblExistenRoles" runat="server" Text="Label" Visible="False"></asp:Label></div>
            <br />
            
            <br />
             <asp:UpdatePanel ID="uplRoles" runat="server">
                 <ContentTemplate>
                     <asp:GridView ID="gvRolesUsuario" runat="server" AutoGenerateColumns="False" ShowFooter="True"  ForeColor="#333333" PageSize="10" AllowPaging="True"
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
                 <Triggers>
                 </Triggers>
    </asp:UpdatePanel>
            <br />
            <br />
           
        <div class="col-md-12">
            <div style="position:absolute;right:15px;">
                  <asp:Button ID="btnCancelar" runat="server" Text="Atrás" Width="133px" OnClick="btnCancelar_Click" CssClass="ButtonNeutro"/>                 
             </div>
        </div>
    </asp:Content>
