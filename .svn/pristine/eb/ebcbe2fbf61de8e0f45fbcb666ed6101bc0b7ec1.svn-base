<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmAnularTituloValor.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmAnularTituloValor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div style="display:inline-block;">
        <div class="col-md-6">
            <div class="col-md-3">Nemotécnico:</div>
            <div class="col-md-7">
                    <asp:DropDownList runat="server" ID="ddlNemotecnico" AutoPostBack="True" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlNemotecnico_SelectedIndexChanged" CssClass="FormatoDropDownList">
                    <asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3">Número de Valor:</div>
                <div class="col-md-7">
                        <asp:DropDownList ID="ddlNumValor" runat="server" AppendDataBoundItems="True" AutoPostBack="True" CssClass="FormatoDropDownList">
                    <asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                    </asp:DropDownList>

                </div>
        </div>
        <asp:TextBox ID="txtFecha" runat="server" CssClass="FormatoTextBox" Visible="false"></asp:TextBox>
    <div class="col-md-12"><asp:Button ID="btnConsultar" CssClass="ButtonNeutro" runat="server" Text="Consultar" OnClick="btnConsultar_Click"/> </div>
    </div>
    <div style="overflow:auto;">
         <asp:GridView ID="grdvTitulosValores" runat="server" 
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">           
            <EditRowStyle BackColor="#2461BF" />           
        </asp:GridView>
     </div>
       <div class="col-md-12"><asp:Button ID="btnAnular" CssClass="ButtonNeutro" runat="server" Text="Anular" OnClick="btnAnular_Click" Visible="false"/> </div>
</asp:Content>
