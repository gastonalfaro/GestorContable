<%@ Page Culture="es-MX" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmNuevoFormulario.aspx.cs" Inherits="Presentacion.CapturaIngresos.frmNuevoFormulario" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="ContenidoJS" runat="server">
    <script src="https://www.java.com/js/deployJava.js" type="text/javascript"></script>
    <script type="text/javascript">
        var XML_STR_FILE_SIGNED = "";
        var NUM_CEDULA = "";

        function get_str_xml() {
            return xml_string;
        }

        function write_xml_signed_on_input() {
            document.getElementById("Contenido_h_str_signed_form").value = XML_STR_FILE_SIGNED;
            document.getElementById("Contenido_h_str_form").value = NUM_CEDULA;
            document.getElementById("Contenido_h_listen_firma").value = "C#234?9$#1$9238478rTXK";
        }

        function get_xml_str() {
            var str = document.getElementById("Contenido_h_str_form").value;
            return str;
        }

        function firmar() {
            document.appletFirma.proceso_firma_digita();
        }

    </script>
    <style type="text/css">
        .div_applet {
            text-align: center;
        }
    </style>


    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>
    <script src="../../Compartidas/rmm-js/jquery.min.js" type="text/javascript"></script>
    <script src="../../Compartidas/rmm-js/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });

        function SearchText() {
            $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });
        }
    </script>
    <script type="text/javascript">
        function doPostBack(pIDArchivo, pEvento) {
            document.getElementById('hidIDArchivo').value = pIDArchivo;

            var vForm = window.document.forms[0];
            if (vForm.onsubmit) vForm.onsubmit(); else vForm.submit();
        }
        function PrintElem(elem) {
            Popup($(elem).html());
            //Popup($('<div/>').append($(elem).clone()).html());
        }
        function Popup(data) {
            var mywindow = window.open('', 'datos', 'height=400,width=1000');
            mywindow.document.write('<html><head><title>Formulario Captura Ingresos</title>');

            mywindow.document.write('<link rel=\"stylesheet\" href=\"/Compartidas/rmm-css/Imprimir.css\" type=\"text/css\" media=\"print\"/>');
            mywindow.document.write('<style type=\"text/css\">html, body { width: 768px; } body { margin: 0 auto; } .row { margin-right: -15px; margin-left: -15px; }  .col-md-1, .col-md-10, .col-md-11, .col-md-12, .col-md-2, .col-md-4, .col-md-4, .col-md-5, .col-md-6, .col-md-7, .col-md-8, .col-md-9 { position: relative; min-height: 1px; padding-right: 15px; padding-left: 15px; float: left; display: inline-block; }' + 
                '.col-md-12 { width: 100%; } .col-md-11 { width: 91.66666667%; } .col-md-10 { width: 83.33333333%; }' +
                '.col-md-9 { width: 75%; } .col-md-8 { width: 66.66666667%; } .col-md-7 { width: 58.33333333%; } .col-md-6 { width: 49%; }' +
                '.col-md-5 { width: 41.66666667%; } .col-md-4 { width: 33.33333333%; } .col-md-4 { width: 25%; }' +
                '.col-md-2 { width: 16.66666667%; } .col-md-1 { width: 8.33333333%; }' +
                '.FormatoTextBox, .FormatoDropDownList { border: 1px solid #436EB3; padding: 3px; }' +
                '.FormatoBotones { text-align: right; }' +
                'select:disabled { background: lightgray !important; border: 1px solid #436EB3 !important; }</style>');

            mywindow.document.write('</head><body >');
            mywindow.document.write(data);
            mywindow.document.write('</body></html>');

            mywindow.document.close();
            mywindow.focus();

            mywindow.print();
            mywindow.close();
           
            return true;
        }
    </script>
    <style type="text/css">
        .auto-style2 {
            font-weight: 700;
        }

        .FormatoTextBox, .FormatoDropDownList {
            width: 100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Enlaces" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Contenido" runat="server">

    <div class="col-md-12">
        <h2>
            <asp:Label ID="Label1" runat="server" Text="Tipo de Cambio (Colones)"></asp:Label><br />
        </h2>
        <b>
            <asp:Label ID="lblCompraDolar" runat="server" Text="Compra D??lares: "></asp:Label></b>
        <asp:Label ID="lblCompraDol" runat="server" Text="0.00"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;
            <b>
                <asp:Label ID="lblVentaDolar" runat="server" Text="Venta D??lares: "></asp:Label></b>
        <asp:Label ID="lblVentaDol" runat="server" Text="0.00"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;
            <b>
                <asp:Label ID="lblMontoEuro" runat="server" Text="Euros: "></asp:Label></b>
        <asp:Label ID="lblEuro" runat="server" Text="0.00"></asp:Label>
        <br />
        <br />
    </div>


    <!--<div id="datos" >-->
    <!--<div id="Section" style="width:100%;display:inline-block;">-->
    <asp:UpdatePanel ID="upPersona1" runat="server">
        <ContentTemplate>
              </ContentTemplate>
    </asp:UpdatePanel>
      
            <div id="Section" style="width: 100%; display: inline-block;">
                <div class="col-md-12">
                    <h2 style="text-align: center">Informaci??n del formulario</h2>
                </div>
               
                <div class="col-md-12">
                    <div class="col-md-12">
                        <h5 style="text-align: center">
                            <asp:Label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true"></asp:Label></h5>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="font-weight: 700">A??o</label></div>
                    <div class="col-md-4">
                        <asp:Label ID="lblAnno" runat="server" ReadOnly="True"></asp:Label></div>
                    <div class="col-md-2">
                        <label style="font-weight: 700">Fecha</label></div>
                    <div class="col-md-4" style="min-height: 24px;">
                        <asp:Label ID="lblFechaIngreso" runat="server"></asp:Label></div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="font-weight: 700">Formulario</label></div>
                    <div class="col-md-4">
                        <asp:Label ID="lblNroFormulario" runat="server" Text=""></asp:Label></div>
                    <div class="col-md-2">
                        <label style="font-weight: 700">Estado</label></div>
                    <div class="col-md-4" style="min-height: 24px;">
                        <asp:Label ID="lblNomEstadoFormulario" runat="server" Style="text-align: left">Creado</asp:Label><asp:Label ID="lblEdoFormulario" Visible="false" runat="server" Style="text-align: left">PEN</asp:Label></div>

                </div>
                <div class="col-md-12">
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <label style="font-weight: 700">Tipo</label></div>
                    <div class="col-md-2">
                        <label style="font-weight: 700">Identificaci??n</label></div>
                    <div class="col-md-6">
                        <label style="font-weight: 700">Nombre</label></div>
                </div>

                
                        <div class="col-md-12">
                            <div class="col-md-2">
                                <label style="font-weight: 700; text-align: right">A Nombre</label>
                            </div>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlTipoPersona" runat="server" TextMode="Text" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged" CssClass="FormatoDropDownList">
                                    <asp:ListItem Value="F">Fisico</asp:ListItem>
                                    <asp:ListItem Value="J">Juridico</asp:ListItem>
                                    <asp:ListItem Value="D">DIMEX</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtIdPersona" runat="server" MaxLength="12" AutoPostBack="true" OnTextChanged="txtIdPersona_TextChanged" CssClass="FormatoTextBox"></asp:TextBox>
                            </div>
                            <div class="col-md-6" style="min-height: 24px;">
                                <asp:Label ID="lblNombre" runat="server" TextMode="SingleLine"></asp:Label>
                            </div>
                        </div>
                 

                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="font-weight: 700; text-align: right">Tramita:</label></div>
                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlTipoPersonaTramite" runat="server" TextMode="Text" OnSelectedIndexChanged="ddlTipoPersonaTramite_SelectedIndexChanged" AutoPostBack="false" CssClass="FormatoDropDownList">
                            <asp:ListItem Value="F">Fisico</asp:ListItem>
                            <asp:ListItem Value="J">Juridico</asp:ListItem>
                            <asp:ListItem Value="DI">DIMEX</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtIdPersonaTramite" runat="server" MaxLength="12" AutoPostBack="true" OnTextChanged="txtIdPersonaTramite_TextChanged" CssClass="FormatoTextBox" Width="150px"></asp:TextBox></div>
                    <div class="col-md-6" style="min-height: 24px;">
                        <asp:Label ID="lblNombreTramite" runat="server" TextMode="SingleLine"></asp:Label></div>

                </div>

                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="font-weight: 700">Correo E.</label></div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCorreo" runat="server" TextMode="Email" CssClass="FormatoTextBox"></asp:TextBox></div>

                    <div class="col-md-2">
                        <asp:Label ID="lblCtaCliente" runat="server" Text="Cuenta IBAN" Style="font-weight: 700"></asp:Label></div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCuentaCliente" runat="server" OnTextChanged="txtCuentaCliente_TextChanged" CssClass="FormatoTextBox" AutoPostBack="True"></asp:TextBox></div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="font-weight: 700">Direcci??n</label></div>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    <%-- <div class="col-md-2"><asp:Label ID="lblBanco" runat="server" Visible="False" style="font-weight: 700">Banco</asp:Label></div>
            <div class="col-md-10">
                <asp:DropDownList  class="chzn-select"  ID="ddlBanco" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TextMode="Text" DataSourceID="SqlDataSource6" DataTextField="NomBanco" DataValueField="IdBanco" Visible="False" CssClass="FormatoDropDownList">
                    <Items>
                    <asp:ListItem Text="-- Selecionar opci??n --" Value="0" />
                    </Items>
                </asp:DropDownList>
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString2 %>" SelectCommand="SELECT b.NomBanco, b.IdBanco FROM ma.Bancos b ">--%>
                    <%--            <SelectParameters>
                        <asp:ControlParameter ControlID="ddlServicios" DefaultValue="0" Name="IdServicio" PropertyName="SelectedValue" />
                    </SelectParameters>--%>
                    <%-- </asp:SqlDataSource>
            </div>--%>
                </div>

                <div class="col-md-12">
                    <div class="col-md-2" style="padding-right: 0px;">
                        <label style="font-weight: 700">Instituci??n UPR</label></div>
                    <div class="col-md-10">
                        <asp:DropDownList class="chzn-select FormatoDropDownList" AppendDataBoundItems="True" ID="ddlInstUPR" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInstUPR_SelectedIndexChanged1">
                        </asp:DropDownList>
                        <%--    <Items>DataSourceID="SqlDataSource5" DataTextField="NomSociedad" DataValueField="IdSociedadGL" 
                    <asp:ListItem Text="-- Selecionar opci??n --" Value="0" />
                    </Items>
                
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString2 %>" SelectCommand="SELECT IdSociedadGL, NomSociedad FROM ma.SociedadesGL So
                WHERE EXISTS (SELECT 1 FROM ma.Servicios Se where Se.IdSociedadGL = So.IdSociedadGL)
                "></asp:SqlDataSource>--%>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="font-weight: 700">Dependencia</label></div>
                    <div class="col-md-10">
                        <asp:DropDownList class="chzn-select FormatoDropDownList" AutoPostBack="True" AppendDataBoundItems="true" ID="ddlOficinas" runat="server" TextMode="Text" OnSelectedIndexChanged="ddlOficinas_SelectedIndexChanged">
                        </asp:DropDownList>
                        <%--      <Items>DataSourceID="SqlDataSource1" DataTextField="NomOficina" DataValueField="IdOficina" 
                    <asp:ListItem Text="-- Selecionar opci??n --" Value="0" />
                    </Items>
                
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString2 %>" SelectCommand="SELECT [IdOficina], [NomOficina] FROM [ma].[Oficinas] WHERE (([IdSociedadGL] = @IdSociedadGL) AND ([Estado] = @Estado))">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlInstUPR" Name="IdSociedadGL" PropertyName="SelectedValue" Type="String" />
                        <asp:Parameter DefaultValue="A" Name="Estado" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>--%>
                    </div>
                </div>

                   
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="font-weight: 700">Descripci??n</label></div>
                    <div class="col-md-10">
                        <asp:TextBox ID="txtDescripcion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>

                </div>
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="font-weight: 700">Expediente</label></div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtExpediente" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    <div class="col-md-2">
                        <asp:Label ID="lblMoneda" runat="server" Style="font-weight: 700">Moneda</asp:Label></div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlMoneda" runat="server" TextMode="Text" CssClass="FormatoDropDownList" AppendDataBoundItems="True">
                            <asp:ListItem Value="0">-- Selecionar opci??n --</asp:ListItem>
                            <asp:ListItem Value="CRC">Colones</asp:ListItem>
                            <asp:ListItem Value="USD">D??lares</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="font-weight: 700">En Colones</label></div>
                    <div class="col-md-2" style="min-height: 24px;">
                        <asp:Label ID="lblTotalColones" runat="server" AutoPostBack="True" ReadOnly="True"></asp:Label>
                    </div>

                    <div class="col-md-8" style="min-height: 24px;">
                        <asp:Label ID="lblLetrasColones" runat="server"></asp:Label>
                    </div>

                </div>
                <div class="col-md-12">
                    <div class="col-md-2">
                        <label style="font-weight: 700">En D??lares</label></div>
                    <div class="col-md-2" style="min-height: 24px;">
                        <asp:Label ID="lblMontoDolares" runat="server" AutoPostBack="True" ReadOnly="True"></asp:Label>
                    </div>
                    <div class="col-md-8" style="min-height: 24px;">
                        <asp:Label ID="lblLetrasDolares" runat="server"></asp:Label>
                    </div>
                </div>
                <div id="divFormularioPago">
                    <div class="col-md-12">
                        <h2 style="text-align: center">
                            <asp:Label ID="lblFormPagos" runat="server">Informaci??n de Pagos</asp:Label></h2>
                    </div>

                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblServicio" runat="server" Style="font-weight: 700">Servicio</asp:Label></div>
                        <div class="col-md-10">
                            <asp:DropDownList class="chzn-select FormatoDropDownList" AppendDataBoundItems="True" ID="ddlServicios" runat="server" OnSelectedIndexChanged="ddlServicios_SelectedIndexChanged" TextMode="Text">
                            </asp:DropDownList>
                            <%--     <Items>DataSourceID="SqlDataSource3" DataTextField="NomServicio" DataValueField="IdServicio" 
                                <asp:ListItem Text="-- Selecionar opci??n --" Value="0" />
                                </Items>
                                           
             
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString2 %>" 
                            SelectCommand="SELECT distinct s.IdServicio, s.NomServicio
                            FROM ma.Servicios s 
                            INNER JOIN  ma.ServiciosOficinas OS 
	                                ON  OS.idSociedadGL = s.IdSociedadGL
	                            AND OS.IdServicio = s.IdServicio
                            WHERE  (s.IdSociedadGL = @IdSociedadGL) 
                            AND (OS.IdOficina = @IdOficina  OR os.IdOficina='') ">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlInstUPR" Name="IdSociedadGL" PropertyName="SelectedValue" Type="String" />
                            </SelectParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlOficinas" Name="IdOficina" PropertyName="SelectedValue" Type="String"  />
                            </SelectParameters>
                        </asp:SqlDataSource>--%>
                        </div>

                    </div>
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="lblPeriodo" runat="server" Style="font-weight: 700">Periodo</asp:Label></div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlPeriodo" TextMode="Number" CssClass="FormatoDropDownList" AutoPostBack="True" runat="server" OnSelectedIndexChanged="ddlPeriodo_SelectedIndexChanged"></asp:DropDownList></div>
                        <div class="col-md-2">
                            <asp:Label ID="lblMonto" runat="server" Style="font-weight: 700">Monto</asp:Label></div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtMonto" runat="server" onkeypress="return AceptarSoloNumerosMonto(event)" onclick="Formateo_Monto(this)" CssClass="FormatoTextBox" onchange="Formateo_Monto(this)" OnTextChanged="txtMonto_TextChanged">0.00</asp:TextBox></div>

                    </div>
                    <div class="col-md-12" id="divReserva" runat="server">
                        <div class="col-md-2">
                            <asp:Label ID="lblReserva" runat="server" Style="font-weight: 700">Reserva Presupuestaria</asp:Label></div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtReserva" runat="server" MinLength="12" MaxLength="15" CssClass="FormatoTextBox" OnTextChanged="txtReserva_TextChanged"></asp:TextBox></div>


                    </div>
                </div>

    <div class="col-md-12">
        <div class="col-md-12" style="text-align: center;">

            <asp:Button ID="btnAgregarPago" runat="server" Text="Agregar Pago" OnClick="btnAgregarPago_Click" CssClass="ButtonNeutro" Style="width: 200px;" />
            <asp:Button ID="btnImprimir" runat="server" Visible="false" Text="Imprimir Formulario" OnClick="btnImprimir_Click1" CssClass="ButtonNeutro" Style="width: 200px;" />
            <%--<asp:Button ID="Button1" runat="server" Visible="false" Text="Imprimir Formulario" onclientclick="PrintElem('#datos')"  OnClick="btnImprimir_Click1" CssClass="ButtonNeutro" Style="width:200px;"/>--%>
            <asp:Button ID="btnPrepararImprimir" runat="server" Visible="false" Text="Preparar Impresi??n" OnClick="btnPrepararImprimir_Click" CssClass="ButtonNeutro" Style="width: 200px;" />
            <asp:Button ID="btnInsertarFormulario" runat="server" Text="Guardar Formulario" OnClick="btnInsertarFormulario_Click" CssClass="ButtonNeutro" Style="width: 200px;" />
            <asp:Button ID="btnPagoDTR" runat="server" Text="Pagar" OnClick="btnPagoDTR_Click" CssClass="ButtonNeutro" Style="width: 200px;" Visible="False" />
            <asp:Button ID="btnPagoDTR_Con_Firma_Digital" runat="server" Text="Pagar" OnClientClick="firmar()" CssClass="ButtonNeutro" Style="width: 200px;" Visible="False" OnClick="btnPagoDTR_Con_Firma_Digital_Click" />
            <!--<asp:Button ID="btn_Testing" runat="server" Text="Testing-Pagar"  OnClientClick="firmar()" CssClass="ButtonNeutro" Style="width:200px;" Visible="true"/>-->

            <!--_APPLET_-->
            <div id="div_applet" visible="true" runat="server">
                <div id="applet_box" runat="server">
                    <script type="text/javascript">
                            var attributes = {
                                id: 'appletFirma',
                                code: 'Vista.AppletTesting.class',
                                archive: 'Applet_TEST_TEST.jar',
                                width: 220, height: 100
                            };
                            var parameters = { fontSize: 16 };
                            var version = '1.6';
                            deployJava.runApplet(attributes, parameters, version);
                    </script>
                </div>
                <asp:HiddenField ID="h_str_form" runat="server" />
                <asp:HiddenField ID="h_str_signed_form" runat="server" />
                <asp:HiddenField ID="h_file_name" runat="server" />
                <asp:HiddenField ID="h_listen_firma" runat="server" Value="" />
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <h2 style="text-align: center">Listado de Pagos&nbsp;</h2>
    </div>
    <div class="col-md-12">
        <div class="col-md-2"></div>
        <div class="col-md-10">
            <asp:GridView ID="grdDatos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="BorrarRubro" AutoGenerateColumns="False"
                CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                <Columns>
                    <%--<asp:BoundField DataField="Sociedad" HeaderText="Sociedad" />
                        <asp:BoundField DataField="Oficina" HeaderText="Oficina" />--%>
                    <%--<asp:BoundField DataField="Servicio" HeaderText="Servicio" />--%>
                    <asp:BoundField DataField="Servicio" HeaderText="Servicio" />
                    <%-- <asp:TemplateField AccessibleHeaderText="Servicio" HeaderText="Servicio">
                    <ItemTemplate>

                        <asp:Label ID="lblServicio" runat="server" Text='<%# Bind("Servicio") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                    <%--<asp:BoundField DataField="Moneda" HeaderText="Moneda" />--%>
                    <%--<asp:BoundField DataField="Monto" HeaderText="Monto" />--%>
                    <asp:BoundField DataField="Monto" DataFormatString="{0:N}" HeaderText="Monto" NullDisplayText="0" />
                    <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                    <asp:BoundField DataField="Reserva" HeaderText="Reserva" />
                    <asp:TemplateField HeaderText="Borrar" HeaderImageUrl="~/Compartidas/imagenes/1444297028_Delete.png">
                        <ItemTemplate>
                            <asp:Button ID="btnBorrar" EnableViewState="true" OnClientClick="return confirm('Si elimina la fila, debe de incluirla nuevamente. ??Seguro que desea eliminar la l??nea?');" CommandName="Borrar" runat="server" Text="Borrar" BackColor="#3366CC" ForeColor="White" CommandArgument='<%# Container.DataItemIndex %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
            </asp:GridView>
        </div>
    </div>
    </div>    
    <script type="text/javascript">
        function tipoCedula(ddl)
        {
            var valor = ddl.value;
            if (valor == 'F') {//10  field = document.getElementById('titre');

            } else if (valor == 'D') {//12

            } else {// 20
            }
            alert(id.value);
        }
    </script>
</asp:Content>
