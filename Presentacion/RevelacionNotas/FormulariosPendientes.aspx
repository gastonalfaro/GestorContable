<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="FormulariosPendientes.aspx.cs" Inherits="Presentacion.RevelacionNotas.FormulariosPendientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>

    <script src="/Compartidas/rmm-js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        function SearchText() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="Contenido" runat="server">
    <h2>Revelaciones Pendientes de Autorización</h2>
    <div>
        <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-5">
                <asp:Label ID="lblNumero" runat="server" Text="Consecutivo:"></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtNumero" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
        <div class="col-md-6" style="margin-top: 1%;">

            <div class="col-md-5">
                <asp:Label ID="lblClaseCuentas" runat="server" Text="Clase de cuentas"></asp:Label></div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlClaseCuentas" AutoPostBack="false" class="chzn-select FormatoDropDownList" runat="server" data-placeholder="Elija grupo de cuentas" onchange="" OnSelectedIndexChanged="ddlClaseCuentas_SelectedIndexChanged1">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="">Todas</asp:ListItem>
                    <asp:ListItem>Activos</asp:ListItem>
                    <asp:ListItem>Pasivos</asp:ListItem>
                    <asp:ListItem>Patrimonio</asp:ListItem>
                    <asp:ListItem>Ingresos</asp:ListItem>
                    <asp:ListItem>Gastos</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-5">
                <asp:Label ID="lblAuxCuentas" runat="server" Text="Auxiliares de cuentas"></asp:Label></div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlAuxCuentas" class="chzn-select FormatoDropDownList" runat="server" data-placeholder="Elija auxiliar de cuentas" onchange="AuxCambia()"></asp:DropDownList></div>
        </div>
        <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-5">
                <asp:Label ID="lblPeriodoMensual" runat="server" Text="Periodo mensual"></asp:Label></div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlPeriodoMensual" runat="server" data-placeholder="Elija periodo mensual" class="chzn-select FormatoDropDownList">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                    <asp:ListItem Value="">Cualquiera</asp:ListItem>
                    <asp:ListItem>Enero</asp:ListItem>
                    <asp:ListItem>Febrero</asp:ListItem>
                    <asp:ListItem>Marzo</asp:ListItem>
                    <asp:ListItem>Abril</asp:ListItem>
                    <asp:ListItem>Mayo</asp:ListItem>
                    <asp:ListItem>Junio</asp:ListItem>
                    <asp:ListItem>Julio</asp:ListItem>
                    <asp:ListItem>Agosto</asp:ListItem>
                    <asp:ListItem>Setiembre</asp:ListItem>
                    <asp:ListItem>Octubre</asp:ListItem>
                    <asp:ListItem>Noviembre</asp:ListItem>
                    <asp:ListItem>Diciembre</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-md-6" style="margin-top: 1%;">
            <asp:Panel ID="pnlSociedad" runat="server" Visible="false">
                <div class="col-md-5">
                    <asp:Label ID="lblInstitucion" runat="server" Text="Institución"></asp:Label></div>
                <div class="col-md-7">
                    <asp:DropDownList ID="ddlInstitucion" runat="server" data-placeholder="Elija una institución" class="chzn-select FormatoDropDownList" Width="215px">
                    </asp:DropDownList>
                </div>
            </asp:Panel>
        </div>

        <div class="col-md-6" style="margin-top: 1%;">
            <div class="col-md-5">
                <asp:Label ID="lblAnnio" runat="server" Text="Periodo Anual"></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtAnnios" class="FormatoTextBox" runat="server" data-placeholder="Ingrese el año"></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align: center; margin-top: 1%;">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="ButtonNeutro" />
            <asp:Button ID="btnNuevoFormulario" runat="server" Text="Nueva" OnClick="btnNuevaRev_Click" Visible="False" CssClass="ButtonNeutro" />
        </div>
    </div>

    <div style="margin-left: auto; margin-right: auto; text-align: center;">
        <asp:Label ID="lblSinResultados" runat="server" Text="La búsqueda no produjo resultados" ForeColor="Red" Visible="False" Font-Italic="True"></asp:Label>
        <asp:GridView ID="gvFormularios" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="gvFormularios_RowCommand" OnRowEditing="gvFormularios_RowEditing" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvFormularios_PageIndexChanging1"
            CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
            <Columns>
                <asp:TemplateField AccessibleHeaderText="Numero" HeaderText="Consecutivo">
                    <ItemTemplate>
                        <asp:Label ID="lblIdRevelacionPendiente" runat="server" Text='<%# Bind("IdRevelacionPendiente") %>'></asp:Label>
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
                <asp:TemplateField AccessibleHeaderText="Institucion" HeaderText="Institución">
                    <ItemTemplate>
                        <asp:Label ID="lblNomSociedad" runat="server" Text='<%# Bind("NomSociedad") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="ClaseDeCuenta" HeaderText="Clase de cuenta">
                    <ItemTemplate>
                        <asp:Label ID="lblClsCuenta" runat="server" Text='<%# Bind("ClaseCuentas") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="AuxDeCuenta" HeaderText="Auxiliar de Cuenta">
                    <ItemTemplate>
                        <asp:Label ID="lblAuxCuenta" runat="server" Text='<%# Bind("NomCuentaContable") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Concepto" HeaderText="Concepto">
                    <ItemTemplate>
                        <asp:Label ID="lblConcepto" runat="server" Text='<%# Bind("Concepto") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Consulta" HeaderText="">
                    <ItemTemplate>
                        <asp:Image ID="imgPretension" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-search.png" Height="20px" Width="20px" />
                        <asp:LinkButton ID="lnkConsultar" runat="server" CausesValidation="False" CommandName="consulta" CommandArgument='<%#Eval("IdRevelacionPendiente")%>' Text="Consultar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Modificar" HeaderText="">
                    <ItemTemplate>
                        <asp:Image ID="mgEditar" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-document-edit.png" Height="20px" Width="20px" />
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="False" CommandName="Edit" Text="Modificar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
    </div>
    <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tamanoArreglo = 0;
        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
    </script>
</asp:Content>
