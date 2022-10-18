<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmTasaVariableTitulos.aspx.cs" Inherits="Presentacion.Mantenimiento.frmTasaVariableTitulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <div class="FormatoBotones"></div> 
    <div class="col-md-12" id="tblNemotecnicos">
            <h3>EMISIONES</h3>
            <p>Mantenimiento de Emisiones.</p>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblNroValor" runat="server" Text="Número de Emisión:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtNroValor" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblNemotecnico" runat="server" Text="Nemotécnico:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:DropDownList ID="ddlNemotecnico" runat="server" CssClass="FormatoDropDownList"></asp:DropDownList></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblIndicadorEconomico" runat="server" Text="Indicador Económico:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:DropDownList ID="ddlInidcadorEconomico" runat="server" CssClass="FormatoDropDownList" OnSelectedIndexChanged="ddlInidcadorEconomico_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></div>
            </div>
            <div class="col-md-6">
                <div class="col-md-3"><asp:Label ID="lblValorIndicadorEco" runat="server" Text="Valor de Indicador:" Font-Bold="true"></asp:Label></div>
                <div class="col-md-5"><asp:TextBox ID="txtValorIndicadorEco" runat="server" CssClass="FormatoTextBox" ReadOnly="True">0</asp:TextBox></div>
            </div>
            
            <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnGuardarIndicadorTitulo" runat="server" Text="GUARDAR" OnClick="btnConsultarNemotecnicos_Click" CssClass="ButtonNeutro" /></div>
             <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div> 
    </div>
    <div style="width: 100%; height: 100%; overflow: auto">
        <asp:GridView ID="gvIndicadorTitulo" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="FormatoGrid" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="NroValor" HeaderText="Número de Valor" />
                <asp:BoundField DataField="Nemotecnico" HeaderText="Nemotécnico" />
                <asp:BoundField DataField="IdIndicadorEco" HeaderText="Indicador Económico" />
                <asp:BoundField DataField="ValorIndicador" HeaderText="Valor de Indicador" />
                <asp:BoundField DataField="FchReferencia" HeaderText="Fecha de Referencia" />
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

    </div>
</asp:Content>
