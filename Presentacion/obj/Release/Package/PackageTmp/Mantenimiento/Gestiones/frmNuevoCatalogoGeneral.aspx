<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevoCatalogoGeneral.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestiones.frmNuevoCatalogoGeneral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div style="width: 100%; height: 100%; overflow: auto">
        <asp:Table ID="tblCatalogos" runat="server" Width="100%">

            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <h3>CATÁLOGOS GENERALES</h3>
                </asp:TableCell>
                
            </asp:TableRow>

            <asp:TableRow>      
                <asp:TableCell ColumnSpan="3">Mantenimiento de Catálogos Generales del Sistema Gestor.</asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" ColumnSpan="3">
                    <asp:Button ID="btnCatalogoVolver" runat="server" Text="VOLVER" OnClick="btnCatalogoVolver_Click" CssClass="ButtonNeutro" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell >
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label1" runat="server" Text="Código:" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtNuevoIdCatalogo" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:TableCell>

                <asp:TableCell >
                    <asp:Table runat="server">
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:TextBox ID="txtNuevoNomCatalogo" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    </asp:TableCell>

                <asp:TableCell >
                    <asp:Button ID="btnCatalogoGuardar" runat="server" Text="CREAR" OnClick="btnCatalogoGuardar_Click" CssClass="ButtonNeutro" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableHeaderCell><br /></asp:TableHeaderCell>
            </asp:TableRow> 

            <asp:TableRow>
                <asp:TableCell ColumnSpan="3">
                    <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label>
                </asp:TableCell>
                <asp:TableHeaderCell><br /></asp:TableHeaderCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
