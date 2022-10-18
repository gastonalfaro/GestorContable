<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="ConsultarNotas.aspx.cs" Inherits="Presentacion.RevelacionNotas.Contingencias.ConsultarNotas" %>
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
        <h2>Consulta de Revelaciones y Notas</h2>

        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblNumero" runat="server" Text="Consecutivo:" ></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtNumero" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblAnnoBusqueda" runat="server" Text="Año: "></asp:Label></div>
            <div class="col-md-7"> <asp:TextBox ID="txtAnno" runat="server" onkeypress="return AceptarSoloNumeros(event)"  CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-5"><asp:Label ID="lblPeriodoMensual" runat="server" Text="Mes: "></asp:Label></div>
            <div class="col-md-7">
                  <asp:DropDownList ID="ddlPeriodoMensual" runat="server" data-placeholder="Elija periodo mensual" class="chzn-select FormatoDropDownList" >
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                    <asp:ListItem Value=""> Cualquiera</asp:ListItem>
                    <asp:ListItem Value="1"> Enero</asp:ListItem>
                    <asp:ListItem Value="2"> Febrero</asp:ListItem>
                    <asp:ListItem Value="3"> Marzo</asp:ListItem>
                    <asp:ListItem Value="4"> Abril</asp:ListItem>
                    <asp:ListItem Value="5"> Mayo</asp:ListItem>
                    <asp:ListItem Value="6"> Junio</asp:ListItem>
                    <asp:ListItem Value="7"> Julio</asp:ListItem>
                    <asp:ListItem Value="8"> Agosto</asp:ListItem>
                    <asp:ListItem Value="9"> Setiembre</asp:ListItem>
                    <asp:ListItem Value="10"> Octubre</asp:ListItem>
                    <asp:ListItem Value="11"> Noviembre</asp:ListItem>
                    <asp:ListItem Value="12"> Diciembre</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="ButtonNeutro" /></div>
    </div>    
    <br />
    <br />
    <br />
    <asp:Label ID="lblSinResultados" runat="server" Text="La búsqueda no produjo resultados" ForeColor="Red" Visible="False" Font-Italic="True"></asp:Label>
    <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center"  PageSize="10" AllowPaging="True" OnRowCommand="gvNotas_RowCommand"
          CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" OnPageIndexChanging="gvNotas_PageIndexChanging">
        <Columns>
            <asp:TemplateField AccessibleHeaderText="Numero" HeaderText="Consecutivo" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblIdRevCont" runat="server" Text='<%# Bind("IdRevCont") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="Periodo Anual" HeaderText="Periodo Anual">
                <ItemTemplate>
                    <asp:Label ID="lblAnno" runat="server" Text='<%# Bind("PeriodoAnual") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="PeriodoMensual" HeaderText="Periodo Mensual">
                <ItemTemplate>
                    <asp:Label ID="lblMes" runat="server" Text='<%# Bind("PeriodoMensual") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField AccessibleHeaderText="Consulta" HeaderText="">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkConsultar" runat="server" CausesValidation="False" CommandName="consulta" CommandArgument='<%#Eval("IdRevCont")%>' Text="Consultar"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#999999" />
    </asp:GridView>
</asp:Content>
