<%@ Page Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmRealizarPago.aspx.cs" Inherits="Presentacion.CapturaIngresos.frmRealizarPago" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server"></asp:Content>
<asp:Content ID="ContentScripts" ContentPlaceHolderID="ContenidoJS" runat="server">
    <!--RAMSES-->
    <script type="text/javascript">
        function input_box() {
            if (confirm('¿ Desea adjuntar más de un documento ?')) {
                document.getElementById("Contenido_respuesta_user").value = "true";
            } else {
                document.getElementById("Contenido_respuesta_user>").value = "false";
            }
        }//FUNCION
    </script>

    <script src="../../Compartidas/rmm-js/chosen.jquery.js"  type="text/javascript"></script>		
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script> 
     
    <script type="text/javascript">

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
        <script type="text/javascript">
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
        .Calendario input { border: 1px solid #436EB3;width: 184px;}
    </style>
    </asp:Content> 

<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">
     <div class="col-md-12">
        <h2>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Tipo de Cambio (Colones)"></asp:Label>
        <br />
        </h2>
     </div>
    <div class="col-md-12">          
        <b><asp:Label ID="lblCompraDolar" runat="server" Text="Compra Dólares: "></asp:Label></b>
        <asp:Label ID="lblCompraDol" runat="server" Text="0.00"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <b><asp:Label ID="lblVentaDolar" runat="server" Text="Venta Dólares: "></asp:Label></b>
        <asp:Label ID="lblVentaDol" runat="server" Text="0.00"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <b><asp:Label ID="lblMontoEuro" runat="server" Text="Euros: "></asp:Label></b>
        <asp:Label ID="lblEuro" runat="server" Text="0.00"></asp:Label>
        <br />
        <br />
    </div>

    <div  class="col-md-6">
        <div class="row">
            <div class="col-md-5">Año del Formulario</div>
            <div class="col-md-7"><asp:TextBox ID="txtAnno" runat="server"  AutoPostBack="true" TextMode="Number" OnTextChanged="txtAnno_TextChanged" MaxLength="4"  onkeypress="return AceptarSoloNumeros(event)" CssClass="FormatoTextBox" ></asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5">Tipo de Identificación1</div>
            <div class="col-md-7">
                <asp:DropDownList  ID="ddlTipoPersona" runat="server" TextMode="Text" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged" AutoPostBack="true" CssClass="FormatoDropDownList">
                    <asp:ListItem Value="F">Fisico</asp:ListItem>
                    <asp:ListItem Value="J">Juridico</asp:ListItem>
                    <asp:ListItem Value="DI">DIMEX</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
         <div class="row">
            <div class="col-md-5">Número de Identificación</div>
            <div class="col-md-7"><asp:TextBox ID="txtIdPersona" runat="server" MaxLength="12"  AutoPostBack="true" OnTextChanged="txtIdPersona_TextChanged" AutoCompleteType="Disabled" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5">Nombre Completo</div>
            <div class="col-md-7"><asp:Label ID="lblNombre" runat="server"   TextMode="SingleLine" ></asp:Label></div>
        </div>
         <div class="row">
            <div class="col-md-5">Formulario</div>
            <div class="col-md-7">
                 <asp:DropDownList ID="ddlListaFormularios" runat="server" AutoPostBack="true" TextMode="Text" OnSelectedIndexChanged="ddlListaFormularios_SelectedIndexChanged" CssClass="FormatoDropDownList" ></asp:DropDownList>
               <%-- <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT * FROM ci.formulariosCapturasIngresos
                                            WHERE IdPersona = @pIdPersona
                                              AND TipoIdPersona = @pTipoIdPersona
                                              AND Anno = @pAnno
                                              AND Estado IN ('IMP');">
                    <SelectParameters>DataSourceID="SqlDataSource4" DataTextField="IdFormulario" DataValueField="IdFormulario" 
                        <asp:ControlParameter ControlID="txtAnno" Name="pAnno" PropertyName="Text" />
                        <asp:ControlParameter ControlID="txtIdPersona" Name="pIdPersona" PropertyName="Text" />
                        <asp:ControlParameter ControlID="ddlTipoPersona" Name="pTipoIdPersona" PropertyName="SelectedValue" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
                <asp:Label ID="lblFchModifica" visible ="false" runat="server"></asp:Label>
            </div>
        </div>
         <div class="row">
            <div class="col-md-5">Estado del Formulario</div>
            <div class="col-md-7"><asp:Label ID="lblNomEstadoFormulario" runat="server" ></asp:Label><asp:Label ID="lblEdoFormulario" visible ="false" runat="server" ></asp:Label></div>
        </div>
        <div class="row">
            <div class="col-md-5">Moneda Formulario</div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlMonedaFormulario" runat="server" TextMode="Text"  AppendDataBoundItems="True" CssClass="FormatoDropDownList" Enabled="False">
                    <asp:ListItem Value="0">-- Selecionar opción --</asp:ListItem>
                    <asp:ListItem Value="CRC">Colones</asp:ListItem>
                    <asp:ListItem Value="USD">Dolares</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

         <div class="row">
            <div class="col-md-5">Monto Colones</div>
            <div class="col-md-7"><asp:label ID="lblTotalColones" runat="server" ></asp:label></div>
        </div>
         <div class="row">
            <div class="col-md-5">Monto Dólares</div>
            <div class="col-md-7"><asp:label ID="lblTotalDolares" runat="server" ></asp:label></div>
        </div>
    </div>
    <div  class="col-md-6">
        <div class="row">
            <div class="col-md-5">Número Comprobante</div>
            <div class="col-md-7"><asp:TextBox ID="txtComprbante" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5">Fecha Comprobante</div>
            <div class="col-md-7"><asp:TextBox ID="txtFecha" runat="server" CssClass="js-date-picker FormatoTextBox"></asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5">Banco</div>
            <div class="col-md-7">
                 <asp:DropDownList  class="chzn-select FormatoDropDownList" ID="ddlBanco" runat="server" AppendDataBoundItems="true" TextMode="Text" Width="220px" >
                  </asp:DropDownList>
                   <%--    <Items>DataSourceID="SqlDataSource6" DataTextField="NomBanco" DataValueField="IdBanco" 
                    <asp:ListItem Text="-- Selecionar opción --" Value="0" />
                    </Items>
                
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString2 %>" SelectCommand="SELECT DISTINCT b.NomBanco, b.IdBanco FROM ma.Bancos b ">       
                </asp:SqlDataSource>  --%>
            </div>
        </div>
         <div class="row">
            <div class="col-md-5">Moneda Pago</div>
            <div class="col-md-7">
                <asp:DropDownList ID="ddlMoneda" runat="server" TextMode="Text"  autopostback="true"  AppendDataBoundItems="True" CssClass="FormatoDropDownList" OnSelectedIndexChanged="ddlMoneda_SelectedIndexChanged">
                    <asp:ListItem Value="0">-- Selecionar opción --</asp:ListItem>
                    <asp:ListItem Value="CRC">Colones</asp:ListItem>
                    <asp:ListItem Value="USD">Dolares</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
         <div class="row">
            <div class="col-md-5">Monto</div>
            <div class="col-md-7"><asp:TextBox ID="txtMonto" runat="server" autopostback="true" onkeypress="return AceptarSoloNumerosMonto(event)" onclick="Formateo_Monto(this)" onchange="Formateo_Monto(this)"  OnTextChanged="txtMonto_TextChanged" CausesValidation="True" CssClass="FormatoTextBox" >0.00</asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5">Observaciones</div>
            <div class="col-md-7"><asp:TextBox ID="txtObservaciones" colspan="2" runat="server" CssClass="FormatoTextBox"  ></asp:TextBox></div>
        </div>
         <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-7"> <asp:FileUpload ID="fu_SeleccionarComprobante" runat="server" ToolTip="Seleccionar el comprobante" Font-Bold="True" CssClass="FormatoTextBox" /></div>

        </div>
         <div class="row">
            <div class="col-md-5">
                <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*Advertencia: Capacidad máxima es de 5MB"></asp:Label>
                <ipp:br />
             </div>
            <div class="col-md-7">
                <asp:Button ID="btnAdjuntar" runat="server" Text="Adjuntar Comprobante" OnClick="btnAdjuntar_Click" CssClass="ButtonNeutro" OnClientClick="input_box()"/> 
                &nbsp<asp:Button ID="btnRealizarPago" runat="server" Text="Registrar Pago" OnClick="btnRealizarPago_Click" Visible="False" CssClass="ButtonNeutro" />
                &nbsp<asp:Button ID="btnEnviarAsiento" runat="server" Text="Enviar Asiento" OnClick="btnEnviarAsiento_Click" Visible="False" CssClass="ButtonNeutro" />
                <asp:HiddenField ID="respuesta_user" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-7"> </div>
        </div>
    </div>
    <div class="col-md-12"><asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
    <div class="col-md-12"><h2>Listado de Pagos&nbsp;</h2></div>
    <div style="width:100%;overflow-y:auto;">       
        <asp:GridView ID="grdDatos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
              CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
            <Columns>

                <asp:BoundField DataField="Servicio" HeaderText="Servicio" />
                <asp:BoundField DataField="Moneda" HeaderText="Moneda" />
                <%--<asp:BoundField DataField="Monto" HeaderText="Monto" />--%>
                <asp:BoundField DataField="Monto" DataFormatString="{0:N}" HeaderText="Monto" NullDisplayText="0" />
                <asp:BoundField DataField="Periodo" HeaderText="Periodo" />

                         
            </Columns>
            <EditRowStyle BackColor="#999999" />
            </asp:GridView>
    </div>
</asp:Content>