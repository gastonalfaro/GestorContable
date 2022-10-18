<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCargarDatosRDs.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaInterna.frmCargarDatosRDs" %>

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
    <div class="col-md-12">
        <h3>Carga de Valores RDI-RDE-RDD</h3>
        <p>Seleccione la fecha para la cual desea cargar los datos del módulo RDI y RDE</p>
        <div class="col-md-6">
            <div class="col-md-7"><asp:TextBox ID="txtFechaInicio" runat="server" placeholder="Inicio: dd/mm/yyyy"  CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-7"><asp:TextBox ID="txtFechaFin" runat="server" placeholder="Fin: dd/mm/yyyy"  CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnCargarRDs" runat="server" OnClick="btnCargarRDs_Click" Text="Cargar Información RDI RDE RDD" CssClass="ButtonNeutro" Width="250px"/></div>
    </div>
    <div class="col-md-12" >
         Resultados de la carga:
        <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
    </div>
   
    <br />
    <div align="center" style="overflow-y:auto;display:inline-block;width:100%;">
        <asp:GridView ID="grvRDs" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="true"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" AllowPaging="True" OnPageIndexChanging="grvRDs_PageIndexChanging">           
            <EditRowStyle BackColor="#2461BF" />           
        </asp:GridView>
    </div>
</asp:Content>
