<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="Formularios.aspx.cs" Inherits="Presentacion.RevelacionNotas.Formularios" EnableEventValidation="false" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">
    <h2>Creación y consulta de formularios</h2>
    <div>
        <div class="col-md-6" style="margin-top:1%;">
            <div class="col-md-5"><asp:Label ID="lblNumero" runat="server" Text="Consecutivo:"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtNumero" runat="server" CssClass="FormatoTextBox" ></asp:TextBox></div>
        </div>
        <div class="col-md-6"  style="margin-top:1%;">
            <div class="col-md-5"> <asp:Label ID="lblPeriodoMensual" runat="server" Text="Periodo mensual"></asp:Label></div>
            <div class="col-md-7"> <asp:DropDownList ID="ddlPeriodoMensual" runat="server" data-placeholder="Elija periodo mensual" class="chzn-select FormatoDropDownList" >
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
        <div class="col-md-6" id="pnlSociedad" visible="false"  style="margin-top:1%;">
            <div class="col-md-5"><asp:Label ID="lblInstitucion" runat="server" Text="Institución"></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlInstitucion" runat="server" data-placeholder="Elija una institución" class="chzn-select FormatoDropDownList" Width="215px">
                </asp:DropDownList></div>
        </div>
        <div class="col-md-6"  style="margin-top:1%;">
            <div class="col-md-5"><asp:Label ID="lblClaseCuentas" runat="server" Text="Clase de cuentas"></asp:Label></div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlClaseCuentas" AutoPostBack="false" class="chzn-select FormatoDropDownList" runat="server" data-placeholder="Elija grupo de cuentas"  onchange="" OnSelectedIndexChanged="ddlClaseCuentas_SelectedIndexChanged1" >
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
        <div class="col-md-6"  style="margin-top:1%;">
            <div class="col-md-5"><asp:Label ID="lblAuxCuentas" runat="server" Text="Auxiliares de cuentas"></asp:Label></div>
            <div class="col-md-7"><asp:DropDownList ID="ddlAuxCuentas" class="chzn-select FormatoDropDownList" runat="server" data-placeholder="Elija auxiliar de cuentas" onchange="AuxCambia()"></asp:DropDownList></div>
        </div>
        <div class="col-md-6"  style="margin-top:1%;">
            <div class="col-md-5"><asp:Label ID="lblAnnio" runat="server" Text="Periodo Anual"></asp:Label></div>
            <div class="col-md-7"><asp:TextBox ID="txtAnnios" class="FormatoTextBox" runat="server" data-placeholder="Ingrese el año"  ></asp:TextBox></div>
        </div>
        <div class="col-md-12" style="text-align:center;margin-top:1%;">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="ButtonNeutro" />
            <asp:Button ID="btnNuevoFormulario" runat="server" OnClick="btnNuevoForm_OnClick" Text="Nueva" Visible="False" CssClass="ButtonNeutro" />
            <asp:HiddenField ID="hdnAux" runat="server" />
       </div>
    </div>
    <br />
    <br />
    <br />
    <div style="margin-left: auto; margin-right: auto; text-align: center;">
        <asp:Label ID="lblSinResultados" runat="server" Text="La búsqueda no produjo resultados" ForeColor="Red" Visible="False" Font-Italic="True"></asp:Label>
        <asp:GridView ID="gvFormularios" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="gvFormularios_RowCommand" OnRowEditing="gvFormularios_RowEditing" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvFormularios_PageIndexChanging1" OnRowDataBound="gvFormularios_RowDataBound"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
            <Columns>
                <asp:TemplateField AccessibleHeaderText="Numero" HeaderText="Consecutivo">
                    <ItemTemplate>
                        <asp:Label ID="lblIdRevelacion" runat="server" Text='<%# Bind("IdRevelacion") %>'></asp:Label>
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
                        <asp:Image ID="imgPretension" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-search.png" Height="20px" Width="20px"/>
                        <asp:LinkButton ID="lnkConsultar" runat="server" CausesValidation="False" CommandName="consulta" CommandArgument='<%#Eval("IdRevelacion")%>' Text="Consultar"></asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Modificar" HeaderText="" >
                    <ItemTemplate>
                        <asp:Image ID="mgEditar" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-document-edit.png" Height="20px" Width="20px"/>  
                        <asp:LinkButton ID="lnkEditar" runat="server" CausesValidation="False" CommandName="Edit" Text="Modificar"></asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="UltimoDiaModificacion" HeaderText="UltimoDiaModificacion" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblUltimoDiaModificacion" runat="server" Text='<%# Bind("UltimoDiaModificacion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
    </div>
    <div id="Content">
    </div>
        <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tamanoArreglo = 0;
        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        function CargarAux() {
            var select = $('#<%=ddlAuxCuentas.ClientID%>');
                select.children().remove();
                select.find('option').remove().end().append('<option value=""></option>').val('')
                var numero = tamanoArreglo;
                if (numero > 0)
                {
                    for(var i=0; i<=numero; i++)
                    {
                        var nombreLI = "Contenido_ddlAuxCuentas_chzn_o_" + i.toString();
                        $("#Contenido_ddlAuxCuentas_chzn .chzn-results #" + nombreLI).remove();
                    }
                }
                var urlPagina = 'Formularios.aspx/ConsultarAuxiliares';
                var grupoCuentas = $('#<%=ddlClaseCuentas.ClientID%> option:selected').val();
		        grupoCuentas = jQuery.trim(grupoCuentas);
		        var par = JSON.stringify({ 'str_GrupoCuentas': grupoCuentas });
		        $.ajax({
                    //debugger
		            url: urlPagina,
		            type: 'POST',
		            data: par,
		            contentType: "application/json; charset=utf-8",
		            dataType: "json",
		            success: function (data) {
		                console.log(data.d);
		                select.children().remove();
		                if (data.d) {
		                    tamanoArreglo = data.d.length;
		                    $(data.d).each(function (index, item) {
		                        select.append($("<option>").val(item.Value).text(item.Text));
		                        var nombre = "Contenido_ddlAuxCuentas_chzn_o_" + index;
		                        $("#Contenido_ddlAuxCuentas_chzn .chzn-results").append("<li id=" + nombre + ' class="active-result" style="">' + item.Text + '</li>');
		                    });
		                }
		            },
		            error: function (XMLHttpRequest, textStatus, errorThrown) {
		            }
		        });
        }
        function CargarAux2() {
            var select = $('#<%=ddlAuxCuentas.ClientID%>');
            select.children().remove();
            select.find('option').remove().end().append('<option value=""></option>').val('')
            var urlPagina = 'Formularios.aspx/ConsultarAuxiliares';
            var grupoCuentas = $('#<%=ddlClaseCuentas.ClientID%> option:selected').val();
            grupoCuentas = jQuery.trim(grupoCuentas);
            var par = JSON.stringify({ 'str_GrupoCuentas': grupoCuentas });
            $.ajax({
                //debugger
                url: urlPagina,
                type: 'POST',
                data: par,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    console.log(data.d);
                    select.children().remove();
                    if (data.d) {
                        tamanoArreglo = data.d.length;
                        $(data.d).each(function (index, item) {
                            select.append($("<option>").val(item.Value).text(item.Text));
                            //var nombre = "Contenido_ddlAuxCuentas_chzn_o_" + index;
                            //$("#Contenido_ddlAuxCuentas_chzn .chzn-results").append("<li id=" + nombre + ' class="active-result" style="">' + item.Text + '</li>');
                        });
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                }
            });
        }
        function AuxCambia() {
            var valorSeleccionado = $('#<%=ddlAuxCuentas.ClientID%> option:selected').val();
            $('#<%=hdnAux.ClientID%>').val(valorSeleccionado);
        }
    </script>
</asp:Content>
