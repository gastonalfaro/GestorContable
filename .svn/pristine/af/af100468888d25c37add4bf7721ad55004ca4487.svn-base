<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="FormularioCatalogoNotas.aspx.cs" Inherits="Presentacion.RevelacionNotas.FormularioCatalogoNotas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div style="display:inline-block;width:100%">
        <h2>Consulta de notas de deuda</h2>
            <asp:GridView ID="gvFormularios" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="gvFormularios_RowCommand" OnRowEditing="gvFormularios_RowEditing" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvFormularios_PageIndexChanging1" DataKeyNames="NomOpcion"
                CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
            <Columns>
                <asp:TemplateField AccessibleHeaderText="Categoria" HeaderText="Categoría">
                    <ItemTemplate>
                        <asp:Label ID="lblCategoria" runat="server" Text='<%# Bind("NomOpcion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="CantAdjuntos" HeaderText="Cantidad de adjuntos">
                    <ItemTemplate>
                        <asp:Label ID="lblCantAdjuntos" runat="server" Text='<%# Bind("CantidadAdjuntos") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField AccessibleHeaderText="Mostrar Detalle" HeaderText="">
                    <ItemTemplate>                                
                        <asp:Image ID="imgPretension" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-search.png" Height="20px" Width="20px"/>
                        <asp:LinkButton ID="lnkConsultar" runat="server" CausesValidation="False" CommandName="consulta" CommandArgument='<%#Eval("IdOpcionCategoria")%>' Text="Mostrar Detalle"></asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>

    </div>
</asp:Content>
