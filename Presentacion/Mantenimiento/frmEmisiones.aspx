<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmEmisiones.aspx.cs" Inherits="Presentacion.Mantenimiento.frmEmisiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
         <div class="col-md-12">
        <h3>Gestión de Emisiones</h3>
        <p>Consulta de Emisiones del Sistema Gestor.</p>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqNroValor" runat="server" Text="Emisión:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtBusqNroValor" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqNemotecnico" runat="server" Text="Nemotécnico:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtBusqNemotecnico" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblBusqIdIndicadorEco" runat="server" Text="Indicador Económico:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlBusqIndicadorEco" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
        </div>
        <%--<div class="col-md-6">
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
        </div>--%>
        <div class="col-md-12" style="text-align:center;">
          <asp:Button ID="btnObjetoConsultar" runat="server" Text="CONSULTAR" CssClass="ButtonNeutro" OnClick="btnObjetoConsultar_Click" />
        </div>
    </div>
    <br />
    <div style="width: 100%; overflow: auto;">

        <asp:GridView ID="gvEmisiones" runat="server" FooterStyle-HorizontalAlign="Right" AutoGenerateColumns="False" ShowFooter="True" ShowHeaderWhenEmpty="True" OnSelectedIndexChanged="gvEmisiones_SelectedIndexChanging" OnRowDeleting="gvEmisiones_EliminarObjeto" OnRowCancelingEdit="gvEmisiones_CancActualizacion" OnRowUpdating="gvEmisiones_ActualizarObj" OnRowDataBound="gvEmisiones_RowDataBound" OnRowEditing="gvEmisiones_RowEditing1" OnDataBound="gvEmisiones_DataBound" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvEmisiones_PageIndexChanging"
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
                <asp:TemplateField HeaderText="IdIndicadorEco" Visible="False">
                    <EditItemTemplate>
                        <asp:Label ID="lblIdIndicadorEco" runat="server" Text='<%# Bind("IdIndicadorEco") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIdIndicadorEco" runat="server" Text='<%# Bind("IdIndicadorEco") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Indicador Eco.">
                    <EditItemTemplate>
                        <asp:Label ID="lblIndicadorEco" runat="server" Text='<%# Bind("NomIndicadorEco") %>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="ddlIndicadorEco" runat="server">
                        </asp:DropDownList>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIndicadorEco" runat="server" Text='<%# Bind("NomIndicadorEco") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Tipo">
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
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Emisión" Visible="True">
                    <EditItemTemplate>
                        <asp:Label ID="lblNroValor" runat="server" Text='<%# Bind("NroValor") %>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNroValor" runat="server" Width="95%"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNroValor" runat="server" Text='<%# Bind("NroValor") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nemotécnico">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditNemotecnico" runat="server" Text='<%# Bind("Nemotecnico") %>' Width="95%"></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNemotecnico" runat="server" Width="95%"></asp:TextBox>
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNemotecnico" runat="server" Text='<%# Bind("Nemotecnico") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Estado">
                    <EditItemTemplate>
                        <asp:CheckBox ID="chkHabilitado" runat="server" Checked='<%# Eval("Habilitado") %>' />
                    </EditItemTemplate>
                    <ItemTemplate>
                         <%# (Eval("Habilitado").ToString() == "1") ? "Habilitado" : "Deshabilitado" %>
                    </ItemTemplate>
                </asp:TemplateField>--%>
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
