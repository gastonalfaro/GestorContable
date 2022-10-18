<%@ Page Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmAnularFormulario.aspx.cs" Inherits="Presentacion.CapturaIngresos.frmAnularFormulario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContenidoJS" runat="server">
        <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
        <script src="../../Compartidas/rmm-js/jquery.min.js" type="text/javascript"></script>
		<script src="../../Compartidas/rmm-js/chosen.jquery.js"  type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                SearchText();
            });
            function SearchText() {
                $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
            }
    </script> 
    <script>

        function PrintElem(elem) {

            Popup($(elem).html());
        }
        function Popup(data) {
            var mywindow = window.open('', 'datos', 'height=400,width=600');
            mywindow.document.write('<html><head><title>Formulario Captura Ingresos</title>');
            /*optional stylesheet*/ //mywindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');

            mywindow.print();
            mywindow.close();

            return true;
        }
    </script>
        <script>
            function cargaNombre() {
                debugger;
                var ced = $('#<%=txtIdPersona.ClientID %>').val();
                var ddlReport = document.getElementById("<%=ddlTipoPersona.ClientID%>");
                var tipo = ddlReport.options[ddlReport.selectedIndex].text;
                if (ced.length >= 10) {
                    $('#<%=lblNombre.ClientID %>').val('');
                $.getJSON('https://www.hacienda.go.cr/ldap/buscar_persona3.php', { cedula: ced, origen: tipo }, function (datos) {   //ESTE USA UN SERVICIO
                    if (datos["primer apellido"] == undefined && datos["segundo apellido"] == undefined)
                    { var html = datos["nombre"]; }
                    else if (datos["segundo apellido"] == undefined)
                    { var html = datos["nombre"] + ' ' + datos["primer apellido"]; }
                    else
                    { var html = datos["nombre"] + ' ' + datos["primer apellido"] + ' ' + datos["segundo apellido"]; }
                    $('#<%=lblNombre.ClientID %>').val(html);
                });
            }
            else
            {
                $("input[id$='Contenido_lblNombre']").val('');
            }
        }
    </script>
    
        <style type="text/css">
            .FormatoTextBox, .FormatoDropDownList { width:100%!important; }
        </style>
    </asp:Content> 

<asp:Content ContentPlaceHolderID="encabezado" runat="server">&nbsp;
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
    <div  class="col-md-6">
        <div class="row">
            <div class="col-md-5">Año del Formulario</div>
            <div class="col-md-7"><asp:TextBox ID="txtAnno" runat="server" TextMode="Number" OnTextChanged="txtAnno_TextChanged" MaxLength="4"  onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox" ></asp:TextBox></div>
        </div>
        <div class="row">
            <div class="col-md-5">Tipo de Identificación</div>
            <div class="col-md-7">  
                <asp:DropDownList ID="ddlTipoPersona" runat="server" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged" TextMode="Text"  AutoPostBack="true" CssClass="FormatoDropDownList" Width="171px">
                    <asp:ListItem Value="F">Fisico</asp:ListItem>
                    <asp:ListItem Value="J">Juridico</asp:ListItem>
                    <asp:ListItem Value="DI">DIMEX</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">Cedula</div>
            <div class="col-md-7"><asp:TextBox ID="txtIdPersona" runat="server" MaxLength="12" OnTextChanged="txtIdPersona_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
    </div>
    <div  class="col-md-6">
        <div class="row">
            <div class="col-md-5">Nombre Completo</div>
            <div class="col-md-7"><asp:Label ID="lblNombre" runat="server" TextMode="SingleLine" ></asp:Label></div>
        </div>
        <div class="row">
            <div class="col-md-5">Lista de Formularios</div>
            <div class="col-md-7">
                  <asp:DropDownList ID="ddlListaFormularios" runat="server" AutoPostBack="true" TextMode="Text" OnLoad="ddlListaFormularios_Load" OnSelectedIndexChanged="ddlListaFormularios_SelectedIndexChanged"  CssClass="FormatoDropDownList" Width="171px" ></asp:DropDownList>
                <%--<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT * FROM ci.formulariosCapturasIngresos
                                            WHERE IdPersona = @pIdPersona
                                              AND TipoIdPersona = @pTipoIdPersona
                                              AND Anno = @pAnno
                                              AND Estado IN ('PEN','IMP');">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtAnno" Name="pAnno" PropertyName="Text" />
                        <asp:ControlParameter ControlID="txtIdPersona" Name="pIdPersona" PropertyName="Text" />
                        <asp:ControlParameter ControlID="ddlTipoPersona" Name="pTipoIdPersona" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>DataSourceID="SqlDataSource4" DataTextField="IdFormulario" DataValueField="IdFormulario" --%>
                **Solo se permite Anular Formularios en Estado Creado o Impreso
            </div>
        </div>
        <div class="row">
            <div class="col-md-5">Estado del Formulario</div>
            <div class="col-md-7"><asp:Label ID="lblNomEstadoFormulario" runat="server" Width="200px"></asp:Label><asp:Label ID="lblEdoFormulario" visible ="false" runat="server" Width="200px"></asp:Label></div>
        </div>
    </div>
    <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div class="col-md-12" style="text-align:center;"><asp:Button ID="btnAnular" runat="server" Text="Anular" CssClass="ButtonNeutro"  OnClick ="btnAnular_Click" /></div>
       
    
</asp:Content>