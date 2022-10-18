<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="GestionObjetos.aspx.cs" Inherits="Presentacion.Seguridad.GestionObjetos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
		 <div class="col-md-12">
        <h3>Gestión de Objetos</h3>
        <p>Consulta de Objetos del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqIdObjeto" runat="server" Text="Nombre:" Font-Bold="True"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtBusqIdObjeto" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqNomObjeto" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtBusqNomObjeto" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqIdModulo" runat="server" Text="Módulo:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlBusqModulo" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqTipoObjeto" runat="server" Text="Tipo Objeto:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
              <asp:DropDownList ID="ddlBusqTipoObjeto" runat="server" CssClass="FormatoDropDownList">
                <asp:ListItem Value="" Selected="True"></asp:ListItem>
                <asp:ListItem Value="Menus">Menús</asp:ListItem>
                <asp:ListItem Value="Opciones de Menu">Opciones de Menú</asp:ListItem>
                <asp:ListItem>Pantallas</asp:ListItem>
                <asp:ListItem>Formularios</asp:ListItem>
                <asp:ListItem>Contenedores</asp:ListItem>
                <asp:ListItem>Campos</asp:ListItem>
                <asp:ListItem>Botones</asp:ListItem>
                <asp:ListItem>Otros</asp:ListItem>
              </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-12" style="text-align:center;">
          <asp:Button ID="btnObjetoConsultar" runat="server" Text="CONSULTAR" CssClass="ButtonNeutro" OnClick="btnObjetoConsultar_Click" />
        </div>
    </div>
    <br />
    <div style="width: 100%; overflow: auto;">

        <asp:GridView ID="gvObjetos" runat="server" FooterStyle-HorizontalAlign="Right" AutoGenerateColumns="False" ShowFooter="True" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvObjetos_SelectedIndexChanging" OnRowDeleting="gvObjetos_EliminarObjeto" OnRowCancelingEdit="gvObjetos_CancActualizacion" OnRowUpdating="gvObjetos_ActualizarObj" OnRowDataBound="gvObjetos_RowDataBound" OnRowEditing="gvObjetos_RowEditing1" OnDataBound="gvObjetos_DataBound" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvObjetos_PageIndexChanging"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" >
            <Columns>
                <asp:TemplateField HeaderText="FchModifica" Visible="False">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFchModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblFchModifica" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Código" Visible="False">
                    <EditItemTemplate>
                        <asp:Label ID="lblIdModulo" runat="server" Text='<%# Bind("IdModulo") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdModulo" runat="server" Text='<%# Bind("IdModulo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Módulo">
                    <EditItemTemplate>
                        <asp:Label ID="lblModulo" runat="server" Text='<%# Bind("NomModulo") %>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlModulo" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModulo" runat="server" Text='<%# Bind("NomModulo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo">
                    <EditItemTemplate>
                        <asp:Label ID="lblTpoObjeto" runat="server" Text='<%# Bind("TipoObjeto") %>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlTipoObjeto" runat="server">
                            <asp:ListItem Value="Menus">Menús</asp:ListItem>
                            <asp:ListItem Value="Opciones de Menu">Opciones de Menú</asp:ListItem>
                            <asp:ListItem>Pantallas</asp:ListItem>
                            <asp:ListItem>Formularios</asp:ListItem>
                            <asp:ListItem>Contenedores</asp:ListItem>
                            <asp:ListItem>Campos</asp:ListItem>
                            <asp:ListItem>Botones</asp:ListItem>
                            <asp:ListItem>Otros</asp:ListItem>
                        </asp:DropDownList>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTpoObjeto" runat="server" Text='<%# Bind("TipoObjeto") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre" Visible="True">
                    <EditItemTemplate>
                        <asp:Label ID="lblIdObjeto" runat="server" Text='<%# Bind("IdObjeto") %>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtIdObjeto" runat="server" Width="95%"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdObjeto" runat="server" Text='<%# Bind("IdObjeto") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripción">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditDescObjeto" runat="server" Text='<%# Bind("DescObjeto") %>' Width="95%"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtDescObjeto" runat="server" Width="95%"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDescObjeto" runat="server" Text='<%# Bind("DescObjeto") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Estado">
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkHabilitado" runat="server" Checked='<%# Eval("Habilitado") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                         <%# (!String.IsNullOrEmpty(Eval("Habilitado").ToString()) && Boolean.Parse(Eval("Habilitado").ToString())) ? "Habilitado" : "Deshabilitado" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                    <EditItemTemplate>
                        <asp:LinkButton ID="lnkActualizar" runat="server" CausesValidation="True" CommandName="Update" Text="Actualizar"></asp:LinkButton>
                        <asp:LinkButton ID="lnkCancelar" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"></asp:LinkButton>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="lnkAgregar" runat="server" CausesValidation="False" CommandName="Select" Text="Agregar"></asp:LinkButton>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="False" CommandName="Edit" Text="Editar"></asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" HorizontalAlign="Center" />            
        </asp:GridView>
        <asp:Label ID="lblResultado" runat="server" Text="" ForeColor="Red" Visible="False" Font-Italic="True"></asp:Label>
    </div>
</asp:Content>
