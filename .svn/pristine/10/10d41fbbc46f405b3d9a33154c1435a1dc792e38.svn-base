<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCrearTitulosCanjeSubasta.aspx.cs" Inherits="Presentacion.CalculosFinancieros.DeudaExterna.frmCrearTitulosCanjeSubasta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div class="col-md-12">
        <h3>Insertar Títulos de Canje, Subasta</h3>
    </div>

    <br />
    <br />


    <div class="col-md-6">
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="NroEmisionCompraLabel" runat="server" Text="Fecha de Canje:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlNumSerie" runat="server" CssClass="FormatoDropDownList" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlNumSerie_SelectedIndexChanged">
                    <asp:ListItem Value="">-- Seleccione Opción --</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>

    <div class="col-md-6">
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="NroEmisionLabel" runat="server" Text="Número de emisión:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtNroEmision" runat="server" MaxLength="15" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
    </div>

    <div class="col-md-6">
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="NumeroValorLabel" runat="server" Text="Número Valor:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtNumeroValor" runat="server" onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="col-md-6">
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="NemotecnicoLabel" runat="server" Text="Nemo Técnico:" Font-Bold="true"></asp:Label>
            </div>
            <div class="col-md-7">
                <asp:TextBox ID="txtNemotecnico" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
            </div>
        </div>
    </div>

    <%--    <div class="col-md-6">
        <div class="row">
            <div class="col-md-3">
                <asp:Label ID="FechaCanjeLabel" runat="server" Text="Fecha Canje:" Font-Bold="true"></asp:Label></div>
            <div class="col-md-7">
                <asp:TextBox ID="txtFechaCanje" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
    </div> --%>




    <div class="col-md-12">
        <div style="text-align: center;">
            <asp:Button ID="btnInsertarTituloCanjeSubasta" runat="server" Text="INSERTAR" OnClick="btnInsertarTituloCanjeSubasta_Click" CssClass="ButtonNeutro" Style="width: 200px;" />

            <asp:Button ID="btnContabilizarCanje" runat="server" Text="CONTABILIZAR CANJE" CssClass="ButtonNeutro" Style="width: 200px;" OnClick="btnContabilizarCanje_Click" />

            <asp:Button ID="btnContabilizarSubasta" runat="server" Text="CONTABILIZAR SUBASTA" CssClass="ButtonNeutro" Style="width: 200px;" />
        </div>
    </div>

    <br />

    <br />

    <div style="width: 100%; height: 100%; overflow: auto">

        <asp:GridView ID="grdvTitulos" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
            Width="100%" CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr"
            PageSize="15" AllowPaging="True" OnPageIndexChanging="grdvFormularios_PageIndexChanging"
              DataKeyNames="NroEmisionSerie, NroValor, Nemotecnico, FchCanje"
            >
            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            <Columns>

                <asp:TemplateField HeaderText="Número Emisión" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblNroEmisionSerie" runat="server" Text='<%# Bind("NroEmisionSerie") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNroEmisionSerie" runat="server" Text='<%# Bind("NroEmisionSerie") %>' MaxLength="15" />
                    </FooterTemplate>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Número Valor" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblNroValor" runat="server" Text='<%# Bind("NroValor") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNroValor" runat="server" Text='<%# Bind("NroValor") %>' MaxLength="15" />
                    </FooterTemplate>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Nemo Técnico" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblNemotecnico" runat="server" Text='<%# Bind("Nemotecnico") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNemotecnico" runat="server" Text='<%# Bind("Nemotecnico") %>' MaxLength="15" />
                    </FooterTemplate>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Fecha de Canje " ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblFchCanje" runat="server" Text='<%# Bind("FchCanje") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtFchCanje" runat="server" Text='<%# Bind("FchCanje") %>' MaxLength="15" />
                    </FooterTemplate>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtEditarDireccion" runat="server" CausesValidation="False" Text="Borrar" OnClick="lbtEditarDireccion_Click" OnClientClick="return confirmCheckIn(this);"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>




            </Columns>

            <EditRowStyle BackColor="#999999" />

            <PagerStyle CssClass="pgr"></PagerStyle>
        </asp:GridView>

    </div>
    <div class="col-md-12">
        <asp:Label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true"></asp:Label></div>

     <%-- Muestra el div para el cuadro de dialogo de eliminar o no un archivo del sistema --%>
    <script type="text/javascript">
        var _confirm = false;
        function confirmCheckIn(button) {
            if (_confirm == false) {
                jQuery('<div>')
                .html("<p>¿Está seguro que desea eliminar este registro?</p>")
                .dialog({
                    autoOpen: true,
                    draggable: false,
                    resizable: false,
                    modal: true,
                    title: "Confirmacion ",
                    width: 350,
                    height: 180,
                    show: "slide",
                    hide: "puff",
                    buttons: {
                        "Si": function () {
                            jQuery(this).dialog("close");
                            _confirm = true;
                            button.click();
                        },

                        "No": function () {
                            jQuery(this).dialog("close");
                        }
                    },

                    close: function () {
                        jQuery(this).remove();
                    }
                });
            }
            return _confirm;
        }
    </script>
</asp:Content>
