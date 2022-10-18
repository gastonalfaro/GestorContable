<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmTitulosGarantia.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmTitulosGarantia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
        <div id="datos">
        <div style="display: inline-block;">
    <h3>Ingreso de Títulos en Garantía: </h3>
    <br />
    Ingrese los títulos en garantía en el Sistema:<br />
    <br />
     <div class="col-md-12" style="margin-top: 2%;">
        <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-4">Nemotécnico:</div>
            <div class="col-md-6">
                 <asp:DropDownList runat="server" ID="ddlNemotecnico" AppendDataBoundItems="True" AutoPostBack="True" CssClass="chzn-select FormatoDropDownList" OnSelectedIndexChanged="CambiarNemotecnico">
                    <asp:ListItem Value="0">-- Seleccione Opción --</asp:ListItem>
                    <asp:ListItem Value="-">Sin Valor</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-4">Número de Valor:</div>
            <div class="col-md-6">
                <asp:DropDownList ID="ddlNroValor" runat="server" CssClass="FormatoDropDownList" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlNroValor_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                <br />
                <asp:TextBox ID="txtNroValor1" runat="server" Visible="False"  CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>
        <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-4">Indicador:</div>
            <div class="col-md-6">
                <asp:TextBox ID="txtIndicador" runat="server" ReadOnly="True" CssClass="FormatoTextBox">En Garantía</asp:TextBox>
            </div>
        </div>
        </div>
        <div class="col-md-12" style="text-align:center; margin-top:3%;">
            <asp:Button ID="btnTituloGar" runat="server" OnClick="btnTituloGar_Click" Text="Añadir título en Garantía" CssClass="ButtonNeutro" Width="200px"/>
        </div>
    </div>

    <br />
    <asp:Label ID="lblEstadoTrans" runat="server" Text="Estado de la transacción: "></asp:Label>
    
    <br />
    <br />
    Títulos en Garantía:
    <div style="overflow-x: auto; width: auto" align="center">
    <asp:GridView ID="grvTituloGar" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
        DataKeyNames="NroValor, Nemotecnico"  
        CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnRowCommand="grvTituloGar_RowCommand" >
        <Columns>
            <asp:BoundField DataField="NroValor" HeaderText="Número Valor" />
            <asp:BoundField DataField="Nemotecnico" HeaderText="Nemotécnico" />
            <asp:BoundField DataField="IndicadorGarantia" HeaderText="Indicador de Garantía" />
            <asp:BoundField DataField="FchCreacionT" HeaderText="Fecha de Creación" DataFormatString="{0:d}" />
            <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtEliminar" runat="server" CausesValidation="False" Text="Eliminar" CommandName="Elimina" ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
    </asp:GridView>
        </div>
    </div>
</asp:Content>
