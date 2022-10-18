<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="FormulariosDeuda.aspx.cs" Inherits="Presentacion.RevelacionNotas.FormulariosDeuda" EnableEventValidation="false" %>

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
    <div style="display:inline-block">
         <h2>Consulta de Revelaciones y Notas</h2>
        <div class="col-md-6">
            <div class="col-md-3"><asp:Label ID="lblNumero" runat="server" Text="Consecutivo:"></asp:Label></div>
            <div class="col-md-8"><asp:TextBox ID="txtNumero" runat="server" CssClass="FormatoTextBox" ></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3">Año</div>

            <div class="col-md-8">
                <asp:TextBox ID="txtAnno" runat="server" CssClass="FormatoTextBox"  MaxLength="4"  onkeypress="return AceptarSoloNumeros(event)"></asp:TextBox></div>
        </div>
        <div class="col-md-6">
            <div class="col-md-3">Mes</div>
            <div class="col-md-8"><asp:TextBox ID="txtMes" runat="server" CssClass="FormatoTextBox"  MaxLength="2"  onkeypress="return AceptarSoloNumeros(event)"></asp:TextBox></div>
        </div>
        <div class="col-md-6" id="pnlSociedad"  style="visibility:hidden;">
            <div class="col-md-3"><asp:Label ID="lblInstitucion" runat="server" Text="Institución"></asp:Label></div>
            <div class="col-md-8"><asp:DropDownList ID="ddlInstitucion" runat="server" data-placeholder="Elija una institución" class="chzn-select FormatoDropDownList" ></asp:DropDownList></div>
        </div>
        <div class="col-md-12" style="text-align:center;">
             <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" CssClass="ButtonNeutro" />
            <asp:Button ID="btnNuevoFormulario" runat="server" OnClick="btnNuevoForm_OnClick" Text="Nueva" Visible="False" CssClass="ButtonNeutro" />
        </div>
    </div>
   
     <asp:HiddenField ID="hdnAux" runat="server" />
    <br />
    <br />
    <br />
    <div style="width:100%;display:inline-block;">
        <asp:Label ID="lblSinResultados" runat="server" Text="La búsqueda no produjo resultados" ForeColor="Red" Visible="False" Font-Italic="True"></asp:Label>
                      
            <asp:GridView ID="gvFormularios" runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" OnRowCommand="gvFormularios_RowCommand" 
                OnRowEditing="gvFormularios_RowEditing" PageSize="10" AllowPaging="True" OnPageIndexChanging="gvFormularios_PageIndexChanging1" OnRowDataBound="gvFormularios_RowDataBound"
                CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
            <Columns>
                <asp:TemplateField AccessibleHeaderText="NumeroNota" HeaderText="Número de Nota">
                    <ItemTemplate>
                        <asp:Label ID="lblIdArchivoDeuda" runat="server" Text='<%# Bind("IdArchivoDeuda") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="NomModulo" HeaderText="Tipo de Deuda">
                    <ItemTemplate>
                        <asp:Label ID="lblNomModulo" runat="server" Text='<%# Bind("NomModulo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="NomOpcion" HeaderText="Categoría">
                    <ItemTemplate>
                        <asp:Label ID="lblNomOpcion" runat="server" Text='<%# Bind("NomOpcion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="FechaCreacion" HeaderText="Fecha Creación">
                    <ItemTemplate>
                        <asp:Label ID="lblFchCreacion" runat="server" Text='<%# Bind("FchCreacion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="UsrCreacion" HeaderText="Usario de Creación">
                    <ItemTemplate>
                        <asp:Label ID="lblUsrCreacion" runat="server" Text='<%# Bind("UsrCreacion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Anno" HeaderText="Año">
                    <ItemTemplate>
                        <asp:Label ID="lblAnno" runat="server" Text='<%# Bind("Anno") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField AccessibleHeaderText="Mes" HeaderText="Mes">
                    <ItemTemplate>
                        <asp:Label ID="lblMes" runat="server" Text='<%# Bind("Mes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>         
                <asp:TemplateField AccessibleHeaderText="Consulta" HeaderText="">
                    <ItemTemplate>                                
                        <asp:Image ID="imgPretension" runat="server" ImageUrl="~/Compartidas/imagenes/24x24-search.png" Height="20px" Width="20px"/>
                        <asp:LinkButton ID="lnkConsultar" runat="server" CausesValidation="False" CommandName="consulta" CommandArgument='<%#Eval("IdArchivoDeuda")%>' Text="Consultar"></asp:LinkButton> 
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
                        <asp:Label ID="lblUltimoDiaModificacion" runat="server" Text='<%# Bind("FchModifica") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
        </asp:GridView>
        
    </div>
        <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
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
    </script>--%>
</asp:Content>
