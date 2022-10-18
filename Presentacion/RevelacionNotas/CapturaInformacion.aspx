<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="CapturaInformacion.aspx.cs" Inherits="Presentacion.RevelacionNotas.CapturaIngresos" EnableEventValidation="false"%>
<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContenidoJS" runat="server">
        <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script> 
        <script src="Scripts/jquery.min.js" type="text/javascript"></script>
		
    <script src="/Compartidas/rmm-js/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#Contenido_ddlEntidad_chzn .chzn-single chzn-single-with-drop").append("<span>Entidad nuev</span>");
            });
            function SearchText() {
                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            }
    </script> 
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <h2>Captura de Información</h2>
    <div id="formularioRN">
        <asp:Panel ID="pnlDatosRevelacion" runat="server">        
         <table style="width: 100%;">
                <tr>
                    <td>
                        
                        <asp:Label ID="lblPAnual" runat="server" Text="Periodo Anual:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAnno" runat="server" ReadOnly="false" CssClass="FormatoTextBox"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblPMensual" runat="server" Text="Periodo Mensual: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMesAno" runat="server" CssClass="FormatoDropDownList">
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
                        <asp:Label ID="lblMsjFecha" runat="server" Text="Debe adjuntar la autorización para crear la revelación." ForeColor="#CC0000" Visible="False" Font-Italic="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNomInstitucion" runat="server" Text="Institucion:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNomIntitucion" AutoPostBack="true" runat="server" data-placeholder="Elija una institución" class="chzn-select" Style="width: 300px;" OnSelectedIndexChanged="ddlNomIntitucion_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblEntidad" runat="server" Text="Entidad:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEntidad" runat="server" data-placeholder="Elija una entidad" class="chzn-select" Style="width: 300px;"  AutoPostBack="true" OnSelectedIndexChanged="ddlEntidad_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblOficina" runat="server" Text="Oficina:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOficina" runat="server" data-placeholder="Elija una oficina" class="chzn-select" Style="width: 300px;"></asp:DropDownList>
                    </td>
                    <td>

                    </td>
                    <td>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblClaseCuentas" runat="server" Text="Clase de Cuentas:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlClaseCuentas" AutoPostBack="true" runat="server" data-placeholder="Elija un grupo de cuentas" class="chzn-select" Style="width: 300px;" onchange="" OnSelectedIndexChanged="ddlClaseCuentas_SelectedIndexChanged">
                            <asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem>Activos</asp:ListItem>
                            <asp:ListItem>Pasivos</asp:ListItem>
                            <asp:ListItem>Patrimonio</asp:ListItem>
                            <asp:ListItem>Ingresos</asp:ListItem>
                            <asp:ListItem>Gastos</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblAuxCuentas" runat="server" Text="Auxiliares de cuentas:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAuxCuentas" class="chzn-select" runat="server" data-placeholder="Elija auxiliar de cuentas" onchange="AuxCambia()" Style="width: 300px;" OnSelectedIndexChanged="ddlAuxCuentas_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>

                </tr>
                <tr>

                    <td>
                        <asp:Label ID="lblConcepto" runat="server" Text="Concepto:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtConcepto" runat="server" CssClass="FormatoTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblJustificacion" runat="server" Text="Justificacion:"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtJustificacion" runat="server" Height="50px" TextMode="MultiLine" CssClass="FormatoTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblArchivos" runat="server" Text="Adjuntos:"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fileUpload1" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="btnArchivos" runat="server" Text="Añadir" OnClick="btnArchivos_Click" CssClass="ButtonNeutro" />
                    </td>
                </tr>
             </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlArchivosSubir">
        <h3>Archivos por subir</h3>
        <asp:GridView ID="gvArchivoPorSubir" runat="server" Width="420px" AutoGenerateColumns="False" OnRowDeleting="gvArchivoPorSubir_RowDeleting"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
           
            <Columns>
                <asp:TemplateField HeaderText="Nombre de Archivo">
                    <ItemTemplate>
                        <asp:Label ID="lblNombre" runat="server"
                            Text='<%# Bind("NombreArchivo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tamaño">
                    <ItemTemplate>
                        <asp:Label ID="lblTamano" runat="server"
                            Text='<%# Bind("Tamano") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEliminar" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('¿Desea eliminar el archivo?');" Text="Eliminar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />            
        </asp:GridView>
        <asp:Label ID="lblMensajeArchSubir" runat="server" Text="No existen archivos pendientes de subir" Font-Italic="True" ForeColor="#CC0000" Visible="True"></asp:Label>
        </asp:Panel>
        <br />
        <br />
        <asp:Button ID="btnCrear" runat="server" Text="Crear" Width="90px" OnClick="btnCrear_Click" CssClass="ButtonNeutro" />
         <asp:Button ID="btnTerminar" runat="server" OnClick="btnTerminar_Click" Text="Terminar" Visible="False" CssClass="ButtonNeutro"  />
         <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="ButtonNeutro" Width="96px" />
        <asp:Button ID="btnCancelar" runat="server" Text="Atrás" OnClick="btnCancelar_Click" Width="78px" CssClass="ButtonNeutro" />
         <br />
            <asp:Label ID="lblError" runat="server" Text="Label" Font-Italic="True" ForeColor="#CC0000" Visible="False"></asp:Label>
        <br />
        <h3>Archivos de Revelación</h3>
        <asp:GridView ID="gvFiles" runat="server"  Width="420px" AutoGenerateColumns="False" OnRowDeleting="gvFiles_RowDeleting" OnRowCommand="gvFiles_RowCommand"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
           
            <Columns>
                <asp:TemplateField HeaderText="IdArchivo" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblIdArchivo" runat="server"
                            Text='<%# Bind("IdArchivo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Dato" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblDato" runat="server"
                            Text='<%# Bind("Dato") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Nombre de Archivo">
                    <ItemTemplate>
                        <asp:Label ID="lblNombre" runat="server"
                            Text='<%# Bind("NombreArchivo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tamaño">
                    <ItemTemplate>
                        <asp:Label ID="lblTamano" runat="server"
                            Text='<%# Bind("Tamano") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fecha de subida">
                    <ItemTemplate>
                        <asp:Label ID="lblFecha" runat="server"
                            Text='<%# Bind("FchCreacion") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDescargar" runat="server" CausesValidation="False" CommandName="consulta" CommandArgument='<%#Eval("IdArchivo")%>' Text="Descargar"></asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" ShowHeader="False" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEliminar" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('¿Desea eliminar el archivo?');" Text="Eliminar"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />           
        </asp:GridView>
        <br />
                 <asp:HiddenField ID="hdnEstado" runat="server" />
         
         <asp:HiddenField ID="hdnAux" runat="server" />
         <asp:HiddenField ID="hdnFecha" runat="server" />

        <asp:HiddenField ID="hdRubroCuenta" runat="server" />
        <asp:HiddenField ID="hdSubCuenta" runat="server" />
        <asp:HiddenField ID="hdSubCuentaAnexa" runat="server" />
        <asp:HiddenField ID="hdAuxiliarCuenta" runat="server" />



        <br />
    </div>
        <script src="Scripts/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        var tamanoArreglo = 0;
        $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });

        function MesElegido() {
            
        }
        function CargarAux() {
            debugger;
            var select = "#<%=ddlAuxCuentas.ClientID%>";
            select.children().remove();
            select.find('option').remove().end().append('<option value=""></option>').val('')
            var numero = tamanoArreglo;
            if (numero > 0) {
                for (var i = 0; i <= numero; i++) {
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
                        debugger;
                    }
                });
        }

        function CargarAux2() {
            debugger;
            var select = $('#<%=ddlAuxCuentas.ClientID%>');
            select.children().remove();
            select.find('option').remove().end().append('<option value=""></option>').val('')
            //var numero = tamanoArreglo;
            //if (numero > 0) {
            //    for (var i = 0; i <= numero; i++) {
            //        var nombreLI = "Contenido_ddlAuxCuentas_chzn_o_" + i.toString();
            //        $("#Contenido_ddlAuxCuentas_chzn .chzn-results #" + nombreLI).remove();
            //    }
            //}
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
                    debugger;
                }
            });
        }
        function AuxCambia() {
            debugger;
            var valorSeleccionado = $('#<%=ddlAuxCuentas.ClientID%> option:selected').val();
            $('#<%=hdnAux.ClientID%>').val(valorSeleccionado);
        }
    </script>
</asp:Content>
