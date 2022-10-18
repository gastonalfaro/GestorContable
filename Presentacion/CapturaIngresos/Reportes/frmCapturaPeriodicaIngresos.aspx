<%@ Page Title="" Language="C#" MasterPageFile="~/PaginaMaestra/PortalPrincipal.Master" AutoEventWireup="true" CodeBehind="frmCapturaPeriodicaIngresos.aspx.cs" Inherits="Presentacion.CapturaIngresos.Reportes.frmCapturaPeriodicaIngresos" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>

<%@ Register Assembly="eWorld.UI, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContenidoJS" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="Contenido" runat="server">
     <div class="col-md-12">
          <asp:LinkButton ID="ViewEntregaATiempoLinkButton" runat="server" OnClick="ViewEntregaATiempoLinkButton_Click">Parámetros</asp:LinkButton>
          <asp:Label ID="SeparadorEntregaATiempoReporteLabel" runat="server" Text="|" Visible="false"></asp:Label>
         <asp:LinkButton ID="ViewReporteLinkButton" runat="server" OnClick="ViewReporteLinkButton_Click">Reporte</asp:LinkButton> 
         <br />
         <h2><asp:Label ID="Label1" runat="server" Text="Reporte de Captura de Ingresos."></asp:Label></h2>
    </div>
    <asp:Panel id="EntregaATiempoPanel" HorizontalAlign="Left" runat="Server">  
        <asp:MultiView id="EntregaATiempoMultiView" ActiveViewIndex="0" runat="Server">
            <asp:View id="EntregaATiempoView" runat="Server">                                                                               
                <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="FechaInicioLabel" runat="server" Text="Fecha Desde: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtFechaInicio" runat="server" required="true" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                           <asp:RequiredFieldValidator id="rfvFechaInicio" runat="server"
                          ControlToValidate="txtFechaInicio"
                          ErrorMessage="Campo Requerido."
                          ForeColor="Red">
                      </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="FechaFinLabel" runat="server" Text="Fecha Hasta: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtFechaFin" runat="server" required="true" CssClass="js-date-picker FormatoTextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator id="rfvFechaFin" runat="server"
                          ControlToValidate="txtFechaFin"
                          ErrorMessage="Campo Requerido."
                          ForeColor="Red">
                      </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                 <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="Periodo" runat="server" Text="Periódo: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtPeriodo" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>
                </div>

                  <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="Anno" runat="server" Text="Año: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtAnno" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>
                </div>

                <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="Formulario" runat="server" Text="Formulario: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtFormulario" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>
                </div>

              

                <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="Pago" runat="server" Text="Pago: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtPago" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>
                </div>

                  <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="Moneda" runat="server" Text="Moneda: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtMoneda" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>
                </div>

                 <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="Institucion" runat="server" Text="Institución: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtInstitucion" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>
                </div>
               	
                <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="Oficina" runat="server" Text="Dependencia: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtOficina" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>
                </div>	

                 <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="Servicio" runat="server" Text="Servicio: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtServicio" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>
                </div>	

                 <div class="col-md-6">					
                    <div class="row">
                        <div class="col-md-3"><asp:Label ID="Estado" runat="server" Text="Estado: "></asp:Label></div>
                        <div class="col-md-7"><asp:TextBox ID="txtEstado" runat="server" CssClass="FormatoTextBox"></asp:TextBox></div>
                    </div>
                </div>	
                
                
                  	
       
                <div class="col-md-12" style="text-align:center;">
                      <asp:Button ID="VerReporteButton" runat="server" Text="Ver Reporte" ValidationGroup="ValidacionGeneral" OnClick="VerReporteButton_Click" CssClass="ButtonNeutro" Width="200px"/>
                </div>
                          
                <br />                                                                                                                                                                                                                                                         
            </asp:View>            
                    
            <asp:View ID="ReporteView" runat="Server">
                <div>
                    <iframe height="800px" width="100%" src="../../Compartidas/VisorReportes/VisorReportes.aspx"   frameborder="0">
                    </iframe>
                </div> 
            </asp:View>   
        </asp:MultiView>
        
        <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
    </asp:Panel> 

</asp:Content>
