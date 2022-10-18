<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="EditarRol.aspx.cs" Inherits="Presentacion.Seguridad.GestRoles.EditarRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <h2>Edición de Roles</h2>
    <br />
    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción: "></asp:Label>
    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
    <asp:Label ID="lblHabilitado" runat="server" Text="Habilitado: "></asp:Label>
    <asp:CheckBox ID="chkHabilitado" runat="server" />
    <br />
    <br />
    <div id="Permisos">
      <div class="col-md-12">
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqNomObjeto" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtBusqNomObjeto" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;">
          <asp:Button ID="btnObjetoConsultar" runat="server" Text="CONSULTAR" CssClass="ButtonNeutro" OnClick="btnObjetoConsultar_Click" />
        </div>
      </div>
        <asp:GridView ID="gvPermisosRol" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowCancelingEdit="gvPermisos_CancelarEdicion" OnRowEditing="gvPermisosRol_EdicionPerm" OnRowUpdating="gvPermisosRol_ActualizaPerm" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvPermisosRol_PageIndexChanging"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
            <Columns>
                <asp:TemplateField HeaderText="FchModifica" Visible="false">
                    <EditItemTemplate> 
                        <asp:Label ID="lblFchModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label>
                    </EditItemTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lblFchModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label> 
                    </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="IdObjeto" Visible="false">
                    <EditItemTemplate> 
                        <asp:Label ID="lblIdObjeto" runat="server" Text='<%# Bind("IdObjeto") %>'></asp:Label>
                    </EditItemTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lblIdObjeto" runat="server" Text='<%# Bind("IdObjeto") %>'></asp:Label> 
                    </ItemTemplate> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Módulo">
                    <EditItemTemplate>
                        <asp:Label ID="lblModulo" runat="server" Text='<%# Bind("NomModulo") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate> 
                        <asp:Label ID="lblModulo" runat="server" Text='<%# Bind("NomModulo") %>'></asp:Label>
                    </ItemTemplate> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Objeto">
                    <EditItemTemplate> 
                        <asp:Label ID="lblDescObjeto" runat="server" Text='<%# Bind("DescObjeto") %>'></asp:Label>
                    </EditItemTemplate> 
                    <ItemTemplate> 
                        <asp:Label ID="lblDescObjeto" runat="server" Text='<%# Bind("DescObjeto") %>'></asp:Label> 
                    </ItemTemplate> 
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Consultar">
                    <EditItemTemplate> 
                        <asp:CheckBox ID="chkEdicionConsultar" runat="server" Checked='<%# Eval("Consultar") %>' />
                    </EditItemTemplate> 
                    <ItemTemplate>
                        <asp:CheckBox ID="chkConsultar" runat="server" Checked='<%# Eval("Consultar") %>' Enabled="false" /> 
                    </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Insertar">
                    <EditItemTemplate> 
                        <asp:CheckBox ID="chkEdicionInsertar" runat="server" Checked='<%# Eval("Insertar") %>' />
                    </EditItemTemplate> 
                    <ItemTemplate>
                        <asp:CheckBox ID="chkInsertar" runat="server" Checked='<%# Eval("Insertar") %>' Enabled="false" /> 
                    </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Borrar">
                    <EditItemTemplate> 
                        <asp:CheckBox ID="chkEdicionBorrar" runat="server" Checked='<%# Eval("Borrar") %>' />
                    </EditItemTemplate> 
                    <ItemTemplate>
                        <asp:CheckBox ID="chkBorrar" runat="server" Checked='<%# Eval("Borrar") %>' Enabled="false" /> 
                    </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Actualizar">
                    <EditItemTemplate> 
                        <asp:CheckBox ID="chkEdicionActualizar" runat="server" Checked='<%# Eval("Actualizar") %>' />
                    </EditItemTemplate> 
                    <ItemTemplate>
                        <asp:CheckBox ID="chkActualizar" runat="server" Checked='<%# Eval("Actualizar") %>' Enabled="false" /> 
                    </ItemTemplate> 
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Exportar">
                    <EditItemTemplate> 
                        <asp:CheckBox ID="chkEdicionExportar" runat="server" Checked='<%# Eval("Exportar") %>' />
                    </EditItemTemplate> 
                    <ItemTemplate>
                        <asp:CheckBox ID="chkExportar" runat="server" Checked='<%# Eval("Exportar") %>' Enabled="false" /> 
                    </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="Imprimir">
                    <EditItemTemplate> 
                        <asp:CheckBox ID="chkEdicionImprimir" runat="server" Checked='<%# Eval("Imprimir") %>' />
                    </EditItemTemplate> 
                    <ItemTemplate>
                        <asp:CheckBox ID="chkImprimir" runat="server" Checked='<%# Eval("Imprimir") %>' Enabled="false" /> 
                    </ItemTemplate> 
                </asp:TemplateField> 
                <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left"> 
                    <EditItemTemplate> 
                        <asp:LinkButton ID="lnkActualizar" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton> 
                        <asp:LinkButton ID="lnkCancelar" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton> 
                    </EditItemTemplate> 
                    <ItemTemplate> 
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar"></asp:LinkButton> 
                    </ItemTemplate> 
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" HorizontalAlign="Center" />
        </asp:GridView>
    </div>
     <div class="col-md-12" style="text-align:center;">
        <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar" OnClick="btnGuardarCambios_Click" CssClass="ButtonNeutro" />
        <asp:Button ID="btnCancelar" runat="server" Text="Atrás" OnClick="btnCancelar_Click" CssClass="ButtonNeutro" />                 
    </div>
</asp:Content>
