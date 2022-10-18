<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevoBanco.aspx.cs" Inherits="Presentacion.Mantenimiento.Gestiones.frmNuevoBanco" %>
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
        <asp:Table ID="tblModulos" runat="server" Width="100%">

            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left" ColumnSpan="2">
                    <h3>BANCOS</h3>
                </asp:TableCell>
                
            </asp:TableRow>

            <asp:TableRow><asp:TableCell><br /></asp:TableCell></asp:TableRow>
            
            <asp:TableRow>      
                <asp:TableCell ColumnSpan="2">
                    Mantenimiento de Bancos del Sistema Gestor.
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Right" ColumnSpan="2">
                    <asp:Button ID="btnBancoVolver" runat="server" Text="VOLVER" OnClick="btnBancoVolver_Click" CssClass="ButtonNeutro" />
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow><asp:TableCell><br /></asp:TableCell></asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2">
                    <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label>
                </asp:TableCell>
                <asp:TableHeaderCell><br /></asp:TableHeaderCell>
            </asp:TableRow>
            
            <asp:TableRow><asp:TableCell><br /></asp:TableCell></asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell Width="20%">
                    <asp:Label ID="lblIdBanco" runat="server" Text="Código Banco:" Font-Bold="true"></asp:Label>    
                </asp:TableCell>
                <asp:TableCell Width="60%"><asp:TextBox Width="100%" ID="txtIdBanco" runat="server" CssClass="FormatoTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="Label2" runat="server" Text="Descripción:" Font-Bold="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell><asp:TextBox Width="100%"  ID="txtNomModulo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <%--<asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblIdPais" runat="server" Text="Pais:" Font-Bold="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell><asp:TextBox Width="100%"  ID="txtIdPais" runat="server"></asp:TextBox></asp:TableCell>
            </asp:TableRow>--%>

            <asp:TableRow ID="tr_Pais">
                <asp:TableCell Width="20%">
                    <asp:Label ID="lblIdPais" runat="server" Text="País:" Font-Bold="true"></asp:Label>    
                </asp:TableCell>
                
                <asp:TableCell Width="60%">
                    <asp:DropDownList ID="ddlIdPais" runat="server" AppendDataBoundItems="true" AutoPostBack="true" TextMode="Text" CssClass="FormatoDropDownList">
                     <Items>
                        <asp:ListItem Text="-- Selecionar opción --" Value="--" Selected="True" />
                    </Items>
                    </asp:DropDownList>

                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblTelefono" runat="server" Text="Telefono:" Font-Bold="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell><asp:TextBox Width="100%"  ID="txtTelefono" runat="server" CssClass="FormatoTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblContacto" runat="server" Text="Contacto:" Font-Bold="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell><asp:TextBox Width="100%"  ID="txtContacto" runat="server" CssClass="FormatoTextBox"></asp:TextBox></asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblEstado" runat="server" Text="Estado:" Font-Bold="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell><asp:CheckBox ID="chkCrearEstado" runat="server" OnCheckedChanged="chkCrearEstado_CheckedChanged"></asp:CheckBox></asp:TableCell>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell Width="20%"></asp:TableCell>
                <asp:TableCell Width="20%" ColumnSpan="1"  HorizontalAlign="Right">
                    <asp:Button ID="btnCrearBanco" runat="server" Text="CREAR" OnClick="btnCrearBanco_Click" CssClass="ButtonNeutro" />
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table>
    </div>
</asp:Content>
