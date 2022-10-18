<%@ Page Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmFormularioEdit.aspx.cs" Inherits="Presentacion.CapturaIngresos.frmFormularioEdit" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ContenidoJS" runat="server">
        <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
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
            $(document).ready(function () {

            });
            function cargaNombre() {
                debugger;
                var ced = $('#<%=txtIdPersona.ClientID %>').val();
                var ddlReport = document.getElementById("<%=ddlTipoPersona.ClientID%>");
                var tipo = ddlReport.options[ddlReport.selectedIndex].text;
                if (ced.length >= 10) {
                    $('#<%=lblNombre.ClientID %>').val('');
                    $.getJSON('http://www.hacienda.go.cr/ldap/buscar_persona3.php', { cedula: ced, origen: tipo }, function (datos) {   //ESTE USA UN SERVICIO
                        if (datos["primer apellido"] == undefined && datos["segundo apellido"] == undefined)
                        { var html = datos["nombre"]; }
                        else if (datos["segundo apellido"] == undefined)
                        { var html = datos["nombre"] + ' ' + datos["primer apellido"]; }
                        else
                        { var html = datos["nombre"] + ' ' + datos["primer apellido"] + ' ' + datos["segundo apellido"]; }
                        $('#<%=lblNombre.ClientID %>').val(html);
                });
            }
            else {
                $("input[id$='Contenido_lblNombre']").val('');
            }
        }
    </script>
        <style type="text/css">
            .auto-style2 {
                font-weight: 700;
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
			<br />
			<asp:Label ID="Label1" runat="server" Text="Tipo de Cambio (Colones)"></asp:Label>
			<br />
			</h2>
			<b><asp:Label ID="lblCompraDolar" runat="server" Text="Compra Dólares: "></asp:Label></b>
			<asp:Label ID="lblCompraDol" runat="server" Text="0.0"></asp:Label>
			&nbsp;&nbsp;&nbsp;&nbsp;
			<b><asp:Label ID="lblVentaDolar" runat="server" Text="Venta Dólares: "></asp:Label></b>
			<asp:Label ID="lblVentaDol" runat="server" Text="0.0"></asp:Label>
			&nbsp;&nbsp;&nbsp;&nbsp;
			<b><asp:Label ID="lblMontoEuro" runat="server" Text="Euros: "></asp:Label></b>
			<asp:Label ID="lblEuro" runat="server" Text="0.0"></asp:Label>
			<br />
			<br />
		</div>
     
   <div id="datos" style="width:100%;display:inline-block;">
            <asp:UpdatePanel runat="server">
				<ContentTemplate>
				<div class="col-md-12"><h2>Información del formulario</h2></div>
				
				<div class="col-md-12"> <asp:label ID="lblMensaje" runat="server" Visible="false" Font-Bold="true" ></asp:label></div>
                <div class="col-md-6">
					
                    <div class="row">
                        <div class="col-md-3"><label style="font-weight: 700">Año</label></div>
                        <div class="col-md-7">
							<asp:TextBox  ID="txtAnno" MaxLength="4"  runat="server" onkeypress="return AceptarSoloNumeros(event)" ></asp:TextBox>
							<asp:label ID="lblAnno" runat="server"  ></asp:label>
						</div>
                    </div>
					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="lblListaFormularios" runat="server" Text="Formulario:" style="font-weight: 700"></asp:Label></div>
                        <div class="col-md-7">
							 <asp:DropDownList ID="ddlListaFormularios" runat="server" AppendDataBoundItems="True" AutoPostBack="True" TextMode="Text" OnLoad="ddlListaFormularios_Load" DataSourceID="SqlDataSource4" DataTextField="IdFormulario" DataValueField="IdFormulario" OnSelectedIndexChanged="ddlListaFormularios_SelectedIndexChanged" Width="200px">
								<Items>
									<asp:ListItem Text="-- Selecionar opción --" Value="0" Selected="True" />
								</Items>
							</asp:DropDownList>
							<asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString %>" SelectCommand="SELECT * FROM ci.FormulariosCapturasIngresos WHERE Anno = @pAnno AND IdPersona = @pIdPersona AND TipoIdPersona = @pTipoIdPersona;">
								<SelectParameters>
									<asp:ControlParameter ControlID="txtAnno" Name="pAnno" PropertyName="Text" />
									<asp:ControlParameter ControlID="txtIdPersona" Name="pIdPersona" PropertyName="Text" />
									<asp:ControlParameter ControlID="ddlTipoPersona" Name="pTipoIdPersona" PropertyName="SelectedValue" />
								</SelectParameters>
							</asp:SqlDataSource>
							<asp:Label ID="lblNroFormulario" runat="server" Text=""></asp:Label>
						</div>
                    </div>
                </div>
                <div class="col-md-6">
					
                    <div class="row">
                        <div class="col-md-3"><label style="font-weight: 700">Fecha</label></div>
                        <div class="col-md-7"><asp:label ID="lblFechaIngreso" runat="server" ></asp:label></div>
                    </div>
					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="Label2" runat="server" Text="Estado" style="font-weight: 700"></asp:Label></div>
                        <div class="col-md-7"><asp:Label ID="lblNomEstadoFormulario" runat="server" Width="100">Creado</asp:Label><asp:Label ID="lblEdoFormulario" visible ="false" runat="server" Width="100">PEN</asp:Label></div>
                    </div>
                </div>

                <div class="col-md-12">
					
                    <div class="row">
                        <div class="col-md-1" style="width:11%;"></div>
                        <div class="col-md-9">
                            <div class="col-md-4"><label style="font-weight: 700">Tipo Ide<strong>n</strong>tificación </label></div>
                            <div class="col-md-4"><label style="font-weight: 700">Identificación</label></div>
                            <div class="col-md-4"><label style="font-weight: 700">Nombre</label></div>
                        </div>
                    </div>
                    <div class="row">
						
                        <div class="col-md-1" style="width:11%;"><label style="font-weight: 700">Nombre</label></div>
                        <div class="col-md-9">
                            <div class="col-md-4">
								<asp:DropDownList Width="100" ID="ddlTipoPersona" runat="server" TextMode="Text" OnSelectedIndexChanged="ddlTipoPersona_SelectedIndexChanged" AutoPostBack="true">
									<asp:ListItem Value="F">Fisico</asp:ListItem>
									<asp:ListItem Value="J">Juridico</asp:ListItem>
									<asp:ListItem Value="DI">DIMEX</asp:ListItem>
								</asp:DropDownList>
                            </div>
                            <div class="col-md-4"><asp:TextBox Width="100" ID="txtIdPersona" runat="server" MaxLength="12"  AutoPostBack="true" OnTextChanged="txtIdPersona_TextChanged" ></asp:TextBox></div>
                            <div class="col-md-4"><asp:Label ID="lblNombre" runat="server" TextMode="SingleLine" Width="200"></asp:Label></div>
                        </div>
                    </div>
                    <div class="row">
						
                        <div class="col-md-1"  style="width:11%;"><label style="font-weight: 700">Tramitado Por</label></div>
                        <div class="col-md-9">
                            <div class="col-md-4">                               
								<asp:DropDownList Width="100" ID="ddlTipoPersonaTramite" runat="server" TextMode="Text" OnSelectedIndexChanged="ddlTipoPersonaTramite_SelectedIndexChanged" AutoPostBack="true">
									<asp:ListItem Value="F">Fisico</asp:ListItem>
									<asp:ListItem Value="J">Juridico</asp:ListItem>
									<asp:ListItem Value="DI">DIMEX</asp:ListItem>
								</asp:DropDownList>
                            </div>
                            <div class="col-md-4"><asp:TextBox Width="100" ID="txtIdPersonaTramite" runat="server" MaxLength="12"  AutoPostBack="true" OnTextChanged="txtIdPersonaTramite_TextChanged" ></asp:TextBox></div>
                            <div class="col-md-4"><asp:Label ID="lblNombreTramite" runat="server" TextMode="SingleLine" Width="200"></asp:Label></div>
                        </div>
                    </div>
                </div>

                <div class="col-md-6">
					
                    <div class="row">
                        <div class="col-md-3"><label style="font-weight: 700">Correo Electrónico</label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtCorreo" runat="server" TextMode="Email"></asp:TextBox></div>
                    </div>
                </div>
                <div class="col-md-6">
					
                    <div class="row">
                        <div class="col-md-3"><label style="font-weight: 700">Dirección</label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtDireccion" runat="server" ></asp:TextBox></div>
                    </div>
                </div>

				 <div class="col-md-12">
					
					<div class="row">
						<div class="col-md-3"> <asp:Label ID="lblCtaCliente" runat="server" Text="Cuenta Cliente" style="font-weight: 700"></asp:Label></div>
						<div class="col-md-7"><asp:TextBox ID="txtCuentaCliente" runat="server" AutoPostBack="true" OnTextChanged="txtCuentaCliente_TextChanged"></asp:TextBox></div>
					</div>
					
					 <div class="row">
						<div class="col-md-3"><asp:Label ID="lblBanco" runat="server" Visible="False" style="font-weight: 700">Banco</asp:Label></div>
						<div class="col-md-7"> 
							<asp:DropDownList  class="chzn-select" Width="100" ID="ddlBanco" runat="server" AppendDataBoundItems="true" AutoPostBack="True" TextMode="Text" DataSourceID="SqlDataSource6" DataTextField="NomBanco" DataValueField="IdBanco" Visible="False">
								<Items>
								<asp:ListItem Text="-- Selecionar opción --" Value="0" />
								</Items>
							</asp:DropDownList>
							<asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString2 %>" SelectCommand="SELECT b.NomBanco, b.IdBanco FROM ma.Bancos b ">
					<%--            <SelectParameters>
									<asp:ControlParameter ControlID="ddlServicios" DefaultValue="0" Name="IdServicio" PropertyName="SelectedValue" />
								</SelectParameters>--%>
							</asp:SqlDataSource>
						</div>
					</div>
				</div>

				<div class="col-md-12">
					
					<div class="row">
						<div class="col-md-3" style="width:12.5%;"><label style="font-weight: 700">Institución UPR</label></div>
						<div class="col-md-7">
							<asp:DropDownList  class="chzn-select" width="550" AppendDataBoundItems="True" ID="ddlInstUPR" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource5" DataTextField="NomSociedad" DataValueField="IdSociedadGL" OnSelectedIndexChanged="ddlInstUPR_SelectedIndexChanged1">
							  <Items>
								<asp:ListItem Text="-- Selecionar opción --" Value="0" />
							  </Items>
							</asp:DropDownList>
							<asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString2 %>" SelectCommand="SELECT [IdSociedadGL], [NomSociedad] FROM [ma].[SociedadesGL]"></asp:SqlDataSource>
						</div>
					</div>
				</div>
            
				<div class="col-md-6">
					
					<div class="row">
						<div class="col-md-3"><label style="font-weight: 700">Dependencia</label></div>
						<div class="col-md-7">
							<asp:DropDownList  class="chzn-select" Width="300px" AutoPostBack="True" AppendDataBoundItems="True" ID="ddlOficinas" runat="server" TextMode="Text" DataSourceID="SqlDataSource1" DataTextField="NomOficina" DataValueField="IdOficina" OnSelectedIndexChanged="ddlOficinas_SelectedIndexChanged">
							  <Items>
								<asp:ListItem Text="-- Selecionar opción --" Value="0" />
							  </Items>
							</asp:DropDownList>
							<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString2 %>" SelectCommand="SELECT [IdOficina], [NomOficina] FROM [Oficinas] WHERE (([IdSociedadGL] = @IdSociedadGL) AND ([Estado] = @Estado))">
								<SelectParameters>
									<asp:ControlParameter ControlID="ddlInstUPR" Name="IdSociedadGL" PropertyName="SelectedValue" Type="String" />
									<asp:Parameter DefaultValue="A" Name="Estado" Type="String" />
								</SelectParameters>
							</asp:SqlDataSource>
						</div>
					</div>
					
					<div class="row">
						<div class="col-md-3"><label style="font-weight: 700">Descripción</label></div>
						<div class="col-md-7"><asp:TextBox ID="txtDescripcion" runat="server"  AutoPostBack="True" OnTextChanged="txtDescripcion_TextChanged"></asp:TextBox></div>
					</div>
				</div>
           
				<div class="col-md-6">
					
					<div class="row">
						<div class="col-md-3"><label style="font-weight: 700">Expediente</label></div>
						<div class="col-md-7"><asp:TextBox ID="txtExpediente" runat="server" Width="100"></asp:TextBox></div>
					</div>     
								
					 <div class="row">
						<div class="col-md-3">Moneda</div>
						<div class="col-md-7">
							<asp:DropDownList ID="ddlMoneda" runat="server" TextMode="Text"  Width="100" AppendDataBoundItems="True">
								<asp:ListItem Value="0">-- Selecionar opción --</asp:ListItem>
								<asp:ListItem Value="CRC">Colones</asp:ListItem>
								<asp:ListItem Value="USD">Dólares</asp:ListItem>
							</asp:DropDownList>
						</div>
					</div>
				</div>
				<div class="col-md-12">
					
					 <div class="row">
						<div class="col-md-3"><label style="font-weight: 700">En Colones</label></div>
						<div class="col-md-7">
							<asp:label ID="lblTotalColones" runat="server" AutoPostBack="True" Width="100" ReadOnly="True"></asp:label>
							<asp:Label ID="lblLetrasColones" runat="server" Width="100%"></asp:Label>
						</div>
					</div>
					
					 <div class="row">
						<div class="col-md-3"><label style="font-weight: 700">En Dólares</label></div>
						<div class="col-md-7">
							<asp:label ID="lblMontoDolares" runat="server" AutoPostBack="True" ReadOnly="True"></asp:label></td>
							<asp:Label ID="lblLetrasDolares" runat="server"></asp:Label>							
						</div>
					</div>
				</div>
				<div id="divFormularioPago">
					<div class="col-md-12"><h2 style="text-align: center"><asp:Label ID="lblFormPagos" runat="server">Información de Pagos</asp:Label></h2></div>
					<div class="col-md-12">
						
						 <div class="row">
							<div class="col-md-2" style="width:12.5%;"><asp:Label ID="lblServicio" runat="server" style="font-weight: 700">Servicio</asp:Label></div>
							<div class="col-md-7">
								<asp:DropDownList  class="chzn-select" Width="550px" AutoPostBack="True" AppendDataBoundItems="True" ID="ddlServicios" runat="server" OnSelectedIndexChanged="ddlServicios_SelectedIndexChanged" TextMode="Text" DataSourceID="SqlDataSource3" DataTextField="NomServicio" DataValueField="IdServicio" >
                                  <Items>
                                    <asp:ListItem Text="-- Selecionar opción --" Value="0" />
                                  </Items>
                                    </asp:DropDownList>
             
             
								  <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:GestNICSPDEVConnectionString2 %>" 
										SelectCommand="SELECT s.IdServicio, s.NomServicio
										FROM ma.Servicios s 
										INNER JOIN  ma.ServiciosOficinas OS 
											 ON  OS.idSociedadGL = s.IdSociedadGL
											AND OS.IdServicio = s.IdServicio
										WHERE  (s.IdSociedadGL = @IdSociedadGL) 
										AND (OS.IdOficina = @IdOficina) ">
										<SelectParameters>
											<asp:ControlParameter ControlID="ddlInstUPR" Name="IdSociedadGL" PropertyName="SelectedValue" Type="String" />
										</SelectParameters>
										<SelectParameters>
											<asp:ControlParameter ControlID="ddlOficinas" Name="IdOficina" PropertyName="SelectedValue" Type="String"  />
										</SelectParameters>
									</asp:SqlDataSource>
             
                       
							</div>
						</div>
					</div>
					<div class="col-md-6">
						
						 <div class="row">
							<div class="col-md-3"><asp:Label ID="lblPeriodo" runat="server" style="font-weight: 700">Periodo</asp:Label></div>
							<div class="col-md-7"> <asp:DropDownList  ID="ddlPeriodo" TextMode="Number" OnTextChanged="ddlPeriodo_TextChanged" runat="server"></asp:DropDownList></div>
						</div>
						
						<div class="row">
							<div class="col-md-3"><asp:Label ID="lblMonto" runat="server" style="font-weight: 700">Monto</asp:Label></div>
							<div class="col-md-7"><asp:TextBox ID="txtMonto" Width="100" runat="server" onclick="Formateo_Monto(this)" onchange="Formateo_Monto(this)" OnTextChanged="txtMonto_TextChanged" >0.00</asp:TextBox></div>
						</div>
					</div>
					<div class="col-md-6">
						
						 <div class="row">
							<div class="col-md-3"><asp:Label ID="lblReserva" runat="server" style="font-weight: 700">Reserva Presupuestaria</asp:Label></div>
							<div class="col-md-7"><asp:TextBox ID="txtReserva" runat="server" MinLength ="12" MaxLength="15" OnTextChanged="txtReserva_TextChanged"></asp:TextBox></div>
						</div>
						<div class="row">
							<div class="col-md-3"></div>
							<div class="col-md-7"></div>
						</div>
					</div>
				</div>

				
				<div class="col-md-12" style="text-align:center;">               
					<asp:Button ID="btnAgregarPago" runat="server" Text="Agregar Pago" OnClick="btnAgregarPago_Click" Width="200px"  CssClass="ButtonNeutro"/>  
					<asp:Button ID="btnImprimir" visible="false" runat="server" Text="Imprimir Formulario" onclientclick="PrintElem('#datos')" OnClick="btnImprimir_Click" Width="200px"  CssClass="ButtonNeutro"/>    
					<asp:Button ID="btnPrepararImprimir" runat="server" Visible="false" Text="Preparar Impresión" OnClick="btnPrepararImprimir_Click" Width="200px" CssClass="ButtonNeutro" />    
					<asp:Button ID="btnInsertarFormulario" runat="server" Text="Guardar Formulario" Width="200px" OnClick="btnInsertarFormulario_Click"  CssClass="ButtonNeutro"/>            
					<asp:Button ID="btnPagoDTR" runat="server" Text="Pagar"  Width="200px" OnClick="btnPagoDTR_Click" Visible="False"  CssClass="ButtonNeutro"/>              
				</div>

				
				<div class="col-md-12"><h2  style="text-align: center">Listado de Pagos&nbsp;</h2></div>
                <div class="col-md-12">
                   //GridView
				    <asp:GridView ID="grdDatos" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="BorrarRubro" AutoGenerateColumns="False"
                          CssClass="FormatoGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr">
                    <Columns>
                        <%--<asp:BoundField DataField="Sociedad" HeaderText="Sociedad" />
                        <asp:BoundField DataField="Oficina" HeaderText="Oficina" />--%>
                        <asp:BoundField DataField="Servicio" HeaderText="Servicio" />
                        <%--<asp:BoundField DataField="Moneda" HeaderText="Moneda" />--%>
                        <%--<asp:BoundField DataField="Monto" HeaderText="Monto" />--%>
                        <asp:BoundField DataField="Monto" DataFormatString="{0:N}" HeaderText="Monto" NullDisplayText="0" />
                        <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                        <asp:TemplateField HeaderText="Borrar" HeaderImageUrl="~/Compartidas/imagenes/1444297028_Delete.png">
                            <ItemTemplate>
                                <asp:Button ID="btnBorrar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Si elimina la fila, debe de incluirla nuevamente. ¿Seguro que desea eliminar la línea?');" CommandName="Borrar" runat="server" Text="Borrar" BackColor="#3366CC" ForeColor="White" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                 </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>   
    
</asp:Content>
