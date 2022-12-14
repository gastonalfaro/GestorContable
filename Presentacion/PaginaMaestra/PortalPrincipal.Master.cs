using System;
using System.Web;
using System.Linq;
using System.Web.UI;
using System.Web.Security;
using Presentacion.Compartidas;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Data;

namespace Presentacion.PaginaMaestra
{
    public partial class PortalPrincipal : System.Web.UI.MasterPage
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (!string.IsNullOrEmpty(clsSesion.Current.LoginUsuario))
                {
                    this.lblBienvenido.Text = "Bienvenido(a) " + clsSesion.Current.NomUsuario + " ";

                    clsSeguridadVistas.MostrarElementos(clsSesion.Current.LoginUsuario, this, "");
                    if (clsSesion.Current.PermisosModulos != null)
                        CargaMenu(clsSesion.Current.PermisosModulos);
                }
                else
                    this.lblBienvenido.Text = string.Empty;
            }
        }

        protected void lnkCerrarSesion_Click(object sender, EventArgs e)
        {
            string ResCerrarSesion = ws_SGService.uwsCerrarSesionUsuario(clsSesion.Current.LoginUsuario, clsSesion.Current.IdSesion);
            
            FormsAuthentication.SignOut();
            clsSesion.Current.BorrarDatosSesion();
            Response.Redirect("~/Login.aspx", true);
        }

//agregado por Gaston cucurucho ******************************************
//para solucionar problemas de lentitud
        protected DataTable gdat_TiposCambio1
        {
            get
            {
                if (ViewState["gdat_TiposCambio1"] == null)
                    ViewState["gdat_TiposCambio1"] = new DataTable();
                return (DataTable)ViewState["gdat_TiposCambio1"];
            }
            set
            {
                ViewState["gdat_TiposCambio1"] = value;
            }
        }
        protected DateTime gdt_FechaActual1
        {
            get
            {
                if (ViewState["gdt_FechaActual1"] == null)
                    ViewState["gdt_FechaActual1"] = new DateTime();
                return Convert.ToDateTime(ViewState["gdt_FechaActual1"]);
            }
            set
            {
                ViewState["gdt_FechaActual1"] = value;
            }
        }
        private void CargarTiposCambio()
        {
            gdt_FechaActual1 = DateTime.Today;
            wsSG.wsSistemaGestor wsSistemaGestor = new wsSG.wsSistemaGestor();
            if ((string.IsNullOrEmpty(clsSesion.Current.TpoCambioEUR)) && (string.IsNullOrEmpty(clsSesion.Current.TpoCambioCompra)) && (string.IsNullOrEmpty(clsSesion.Current.TpoCambioVenta)))
            {            
                gdat_TiposCambio1 = wsSistemaGestor.uwsConsultarTiposCambio(null, gdt_FechaActual1, null, "N").Tables[0];                
                clsSesion.Current.TpoCambioEUR = gdat_TiposCambio1.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString();
                clsSesion.Current.TpoCambioCompra = gdat_TiposCambio1.Select("TipoTransaccion = '317'")[0]["Valor"].ToString();
                clsSesion.Current.TpoCambioVenta = gdat_TiposCambio1.Select("TipoTransaccion = '318'")[0]["Valor"].ToString();
            }
        }
 //agregado por Gaston cucurucho ************************************************************************************
        private void CargaMenu(string[] pPermisos)
        {
            string vMenu = string.Empty;
            if (!string.IsNullOrEmpty(clsSesion.Current.MenuUsuario))
            {
                vMenu = clsSesion.Current.MenuUsuario;
            }
            else
            {
                vMenu = "<div id=\"cssmenu\">" +
                    "<ul>" +
                        "<li><a href=\"/Principal.aspx\" runat=\"server\">Inicio</a></li>";

                //Permisos de Captura de Ingresos
                if (pPermisos.Contains("CI"))
                {
                    CargarTiposCambio(); //agregado por Gaston cucurucho para solucionar problemas de lentitud
                    vMenu += "<li id=\"liOBJ_CI\" class=\"active\" runat=\"server\"><a href=\"#\"><span>Captura Ingresos</span></a>" +
                               "<ul>" +
                                "<li style=\"" + MenuVisible("CI_frmFormulariosCaptura", "") + "\" ><a href=\"/CapturaIngresos/frmFormulariosCaptura.aspx\" runat=\"server\">Consultar Formularios</a></li>" +
                                   "<li style=\"" + MenuVisible("CI_frmNuevoFormulario", "") + "\" ><a href=\"/CapturaIngresos/frmNuevoFormulario.aspx\" runat=\"server\">Crear Formulario</a></li>" +
                                   "<li style=\"" + MenuVisible("CI_frmCapturaIngresos", "") + "\" ><a href=\"/CapturaIngresos/frmCapturaIngresos.aspx\" runat=\"server\">Modificar Formulario</a></li>" +
                                   "<li style=\"" + MenuVisible("CI_frmRealizarPago", "") + "\" ><a href=\"/CapturaIngresos/frmRealizarPago.aspx\" runat=\"server\">Registrar Pago</a></li>" +
                                   "<li style=\"" + MenuVisible("CI_frmFormulariosComprobantes", "") + "\" ><a href=\"/CapturaIngresos/frmFormulariosComprobantes.aspx\" runat=\"server\">Comprobantes de Pago</a></li>" +
                                   "<li style=\"" + MenuVisible("CI_frmAnularFormulario", "") + "\" ><a href=\"/CapturaIngresos/frmAnularFormulario.aspx\" runat=\"server\">Anular Formulario</a></li>" +
                                   "<li id=\"Li3\" runat=\"server\"><a href=\"#\"  >Reportes</a>" +
                                       "<ul>" +
                                           "<li style=\"" + MenuVisible("CI_frmCapturaPeriodicaIngresos", "") + "\" ><a href=\"/CapturaIngresos/Reportes/frmCapturaPeriodicaIngresos.aspx\" runat=\"server\">Captura Periódica Ingresos</a></li>" +
                                           "<li style=\"" + MenuVisible("CI_frmCapturaPeriodicaIngresosUPR", "") + "\" ><a href=\"/CapturaIngresos/Reportes/frmCapturaPeriodicaIngresosUPR.aspx\" runat=\"server\" style=\"padding-top:4px;\">Captura Periódica Ingresos de la Institución</a></li>" +
                                           "<li style=\"" + MenuVisible("CI_frmRptPagosExpedientes", "") + "\" ><a href=\"/CapturaIngresos/Reportes/frmRptPagosExpedientes.aspx\" runat=\"server\">Pagos de Contingentes</a></li>" +
                                           "<li style=\"" + MenuVisible("CI_frmReporteBitacoraCI", "") + "\" ><a href =\"/CapturaIngresos/Reportes/frmReporteBitacoraCI.aspx\" runat=\"server\">Reporte de Bitácora CI</a></li>" +
                                        "</ul>" +
                                   "</li> " +
                               "</ul>" +
                           "</li>";
                }
            //Permisos de Deuda Externa
            if (pPermisos.Contains("DE"))
                vMenu += "<li id=\"liOBJ_DE\" runat=\"server\" class=\"active\" visibility =\"hidden\"><a href =\"#\" runat=\"server\" class=\"\">Deuda Externa</a>" +
                           "<ul>" +
                               //"<li><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/frmAsientosReversion.aspx\" runat=\"server\">Asientos Reversion</a></li>" +
                               //"<li><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/frmReporteDevengoDE.aspx\" runat=\"server\">Reporte de Devengos</a></li>" +
                               //"<li><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/frmReporteSaldosDE.aspx\" runat=\"server\">Reporte de Saldos</a></li>" +
                               //"<li><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/frmReporteBitacoraDE.aspx\" runat=\"server\">Reporte de Bitácora DE</a></li>" +
                               "<li style=\"" + MenuVisible("frmAsientosReversion", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/frmAsientosReversion.aspx\" runat=\"server\">Asientos Reversion</a></li>" +
                               "<li style=\"" + MenuVisible("frmDevengo", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/frmDevengo.aspx\" runat=\"server\">Procesos de Cierre</a></li>" +
                               "<li id=\"Li3\" runat=\"server\"><a href=\"#\"  >Consultas</a>" +
                                   "<ul>" +
                                       "<li style=\"" + MenuVisible("frmAmortizacionesDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmAmortizaciones.aspx\" runat=\"server\">Consulta Amortizaciones</a></li>" +
                                       "<li style=\"" + MenuVisible("frmComisionesDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmComisiones.aspx\" runat=\"server\">Consulta Comisiones</a></li>" +
                                       "<li style=\"" + MenuVisible("frmComisionesPagosDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmComisionesPagos.aspx\" runat=\"server\">Consulta Comisiones Pagos</a></li>" +
                                       "<li style=\"" + MenuVisible("frmGirosDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmGiros.aspx\" runat=\"server\">Consulta Giros</a></li>" +
                                       "<li style=\"" + MenuVisible("frmGirosEstimadosDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmGirosEstimados.aspx\" runat=\"server\">Consulta Giros Estimados</a></li>" +
                                       "<li style=\"" + MenuVisible("frmInteresesDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmIntereses.aspx\" runat=\"server\">Consulta Intereses</a></li>" +
                                       "<li style=\"" + MenuVisible("frmInteresesPagosDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmInteresesPagos.aspx\" runat=\"server\">Consulta Intereses Pagos</a></li>" +
                                       "<li style=\"" + MenuVisible("frmInteresePunitivosPagosDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmInteresePunitivosPagos.aspx\" runat=\"server\">Consulta Intereses Punitivos Pagos</a></li>" +
                                       "<li style=\"" + MenuVisible("frmPrestamosDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmPrestamos.aspx\" runat=\"server\">Consulta Préstamos</a></li>" +
                                       "<li style=\"" + MenuVisible("frmTasasFlotantesDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmTasasFlotantes.aspx\" runat=\"server\">Consulta Tasas Flotantes</a></li>" +
                                       "<li style=\"" + MenuVisible("frmTramosDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Consultas/frmTramos.aspx\" runat=\"server\">Consulta Tramos</a></li>" +
                                   "</ul>" +
                               "</li> " +
                               "<li id=\"Li3\" runat=\"server\"><a href=\"#\"  >Reportes</a>" +
                                   "<ul>" +
                                    "<li style=\"" + MenuVisible("frmReporteDevengoDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Reportes/frmReporteDevengoDE.aspx\" runat=\"server\">Reporte de Devengos</a></li>" +
                                    "<li style=\"" + MenuVisible("frmRptDevengoGeneralDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Reportes/frmRptDevengoGeneralDE.aspx\" runat=\"server\">Reporte General de Devengo </a></li>" +
                                    "<li style=\"" + MenuVisible("frmReporteSaldosDeudaExt", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Reportes/frmReporteSaldosDeudaExt.aspx\" runat=\"server\">Reporte de Saldos Deuda Externa</a></li>" +
                                    "<li style=\"" + MenuVisible("frmReporteMovimientosDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Reportes/frmReporteMovimientosDE.aspx\" runat=\"server\">Reporte de Movimientos con su Relación Presupuestaria</a></li>" +
                                    "<li style=\"" + MenuVisible("frmReporteSaldoDeudaExt", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Reportes/frmReporteSaldoDeudaExt.aspx\" runat=\"server\">Reporte de Movimientos</a></li>" +
                                   "<li style=\"" + MenuVisible("frmReporteBitacoraDE", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaExterna/Reportes/frmReporteBitacoraDE.aspx\" runat=\"server\">Reporte de Bitácora DE</a></li>" +
                                   "</ul>" +
                               "</li> " +
                            "</ul>" +
                       "</li> ";
            //Permisos de Deuda Interna
            if (pPermisos.Contains("DI"))
                vMenu += "<li id =\"liOBJ_DI\" runat=\"server\" class=\"active\"><a href =\"#\" runat=\"server\" class=\"\">Deuda Interna</a>" +
                    "<ul>" +
                        "<li style=\"" + MenuVisible("frmCostoTransaccion", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmCostoTransaccion.aspx\" runat=\"server\">Agregar Costos de Transaccion</a></li>" +
                        "<li style=\"" + MenuVisible("frmTitulosGarantia", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmTitulosGarantia.aspx\" runat=\"server\">Agregar Título en Garantía</a></li>" +
                        "<li style=\"" + MenuVisible("frmCargarDatosRDs", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmCargarDatosRDs.aspx\" runat=\"server\">Cargar datos de RDI-RDE-RDD</a></li>" +
                        "<li style=\"" + MenuVisible("frmPagosCCSS", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmPagosCCSS.aspx\" runat=\"server\">Cargar Pagos de CCSS</a></li>" +
                        "<li style=\"" + MenuVisible("frmTrasladosMagisterio", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmTrasladosMagisterio.aspx\" runat=\"server\">Cargar traslados a Magisterio</a></li>" +
                        "<li style=\"" + MenuVisible("frmIncluirDatos", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmIncluirDatos.aspx\" runat=\"server\">Cargar Títulos Manualmente</a></li>" +
                        "<li style=\"" + MenuVisible("frmCrearTitulosCanjeSubasta", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmCrearTitulosCanjeSubasta.aspx\" runat=\"server\">Crear Títulos Canje, Subasta</a></li>" +
                        "<li style=\"" + MenuVisible("frmCancelacionAnticipada", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmCancelacionAnticipada.aspx\" runat=\"server\">Cargar Cancelaciones Anticipadas</a></li>" +
                        "<li style=\"" + MenuVisible("frmAnularTituloValor", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmAnularTituloValor.aspx\" runat=\"server\">Anulación Titulos Valores</a></li>" +
                        "<li style=\"" + MenuVisible("frmProcesosManuales", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmProcesosManuales.aspx\" runat=\"server\">Procesos Manuales</a></li>" +

                        "<li><a href =\"#\" class=\"\">Reportes</a>" +
                            "<ul>" +
                                "<li style=\"" + MenuVisible("frmReporteCancelacion", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteCancelacion.aspx\" runat=\"server\">Reporte de Cancelacion</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteColocaciones", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteColocaciones.aspx\" runat=\"server\">Reporte Colocaciones</a></li>" +
                                //"<li style=\"" + MenuVisible("frmReporteCuponesPagados", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteCuponesPagados.aspx\" runat=\"server\">Reporte Cupones Pagados</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteDevensInt", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteDevengosInt.aspx\" runat=\"server\">Reporte de Devengo</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteAuxiliarContable", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteAuxiliarContable.aspx\" runat=\"server\">Reporte de Auxiliar Contable</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteDevenGeneral", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteDevengoGeneral.aspx\" runat=\"server\">Reporte General de Devengo</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteCanjeEmision", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteCanjeEmision.aspx\" runat=\"server\">Reporte de Emisión de Canjes</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteSubastaEmision", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteSubastaEmision.aspx\" runat=\"server\">Reporte de Emisión de Subasta Inversa</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteNemotecnicosCPLP", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteNemotecnicosCPLP.aspx\" runat=\"server\">Reporte por Plazos</a></li>" +
                                "<li style=\"" + MenuVisible("frmRptConciliaSaldos", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmRptConciliaSaldos.aspx\" runat=\"server\">Reporte de Conciliación de Saldos</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteOperacionesEspeciales", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteOperacionesEspeciales.aspx\" runat=\"server\">Reporte de Operaciones Especiales</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteSaldosDI", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteSaldosDI.aspx\" runat=\"server\">Reporte de Saldos DI</a></li>" +

                                "<li style=\"" + MenuVisible("frmReporteReverDevengo", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteReverDevengo.aspx\" runat=\"server\">- Reporte de Reversión del Devengo</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteReclasLPCP", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteReclasLPCP.aspx\" runat=\"server\">- Reporte de Reclasificación LP/CP</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteCuponesPagados", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteCuponesPagados.aspx\" runat=\"server\">- Reporte de Cupones Pagados</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteValora", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteValora.aspx\" runat=\"server\">- Reporte de Valoración de Moneda</a></li>" +

                                "<li style=\"" + MenuVisible("frmReporteTitulosGarantia", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteTitulosGarantia.aspx\" runat=\"server\">Reporte de Titulos en Garantia</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteHistorialTituloValor", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteHistorialTituloValor.aspx\" runat=\"server\">Reporte de Historial de Títulos</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteDevenCanjeSubasta", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteDevengoCanjeSubasta.aspx\" runat=\"server\">Reporte de Devengo Canje Subasta</a></li>" +
                                //"<li style=\"" + MenuVisible("frmReporteReversionesDevenCanc", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteReversionesDevengoCanc.aspx\" style=\"padding-top:4px;\" runat=\"server\">Reporte de Reversión de Devengo Cancelacion</a></li>" +
                                //"<li style=\"" + MenuVisible("frmReporteTitulosReclasLPCP", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteTitulosReclasLPCP.aspx\" runat=\"server\">Reporte de Títulos Reclasificados</a></li>" +
                                //"<li style=\"" + MenuVisible("frmReporteConciliacionSaldos", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteConciliacionSaldos.aspx\" runat=\"server\">Reporte de Conciliación de Saldos</a></li>" +
                                "<li style=\"" + MenuVisible("frmReporteBitacoraDI", "") + "\" ><a class=\"\" href =\"/CalculosFinancieros/DeudaInterna/frmReporteBitacoraDI.aspx\" runat=\"server\">- Reporte de Bitácora DI</a></li>" +
                            "</ul>" +
                        "</li>   " +
                    "</ul>" +
                "</li> ";
            //Permisos de Contingente
            if (pPermisos.Contains("CT"))
            {
                vMenu += "<li id =\"liOBJ_CT\" runat=\"server\" class=\"active\" visible =\"false\"><a href=\"#\" runat=\"server\"><span>Contingentes</span></a>" +
                   " <ul class=\"\">" +
                        "<li runat=\"server\" id =\"liCT_Registro\"><a href =\"#\" runat=\"server\" class=\"\">Registrar</a>" +
                            "<ul>" +
                                "<li id =\"liCT_rgNuevoExpediente\" style=\"" + MenuVisible("CT_rgNuevoExpediente", "") + "\" ><a href=\"/Contingentes/NuevoExpediente.aspx?isAdd=true\" runat=\"server\">Registrar Expediente</a></li> " +
                                "<li id =\"liCT_rgPretensiones\" style=\"" + MenuVisible("CT_rgPretenciones", "") + "\" ><a href=\"/Contingentes/Pretenciones.aspx?isAdd=true\" runat=\"server\">Registrar Pretensión Inicial</a></li>" +
                                "<li id =\"liCT_rgResoluciones\" style=\"" + MenuVisible("CT_rgResoluciones", "") + "\" ><a href=\"/Contingentes/Resoluciones.aspx?isAdd=true\" runat=\"server\">Registrar Resolución</a></li> " +
                                "<li id =\"liCT_rgLiquidacion\" style=\"" + MenuVisible("CT_rgLiquidacion", "") + "\" ><a href =\"/Contingentes/Liquidacion.aspx?isAdd=true\" runat=\"server\">Registrar Liquidación</a></li> " +
                                "<li id =\"liCT_rgCargaCuentaCobrar\" style=\"" + MenuVisible("CT_rgCargaCuentaCobrar", "") + "\" ><a href =\"/Contingentes/CargaCuentaCobrar.aspx?isAdd=true\" runat=\"server\">Cancelacion cuentas por cobrar</a></li> " +
                                "<li id =\"liCT_rgCargaCuentaPagar\" style=\"" + MenuVisible("CT_rgCargaCuentaPagar", "") + "\" ><a href =\"/Contingentes/CargaCuentaPagar.aspx?isAdd=true\" runat=\"server\">Cancelación cuentar por pagar</a></li> " +
                            "</ul>" +
                        "</li>" +
                        "<li class=\"\" id =\"liCT_Reportes\" style=\"" + MenuVisible("CT_Reportes", "") + "\" ><a class=\"\" href =\"#\" runat=\"server\">Reportes</a>" +
                            "<ul>" +
                                "<li id =\"liCT_rptActivos\"  style=\"" + MenuVisible("CT_rptActivos", "") + "\"  runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=Activo\" runat=\"server\">Reporte de Activos Contingentes</a></li>" +
                                "<li id =\"liCT_rptPasivos\"  style=\"" + MenuVisible("CT_rptPasivos", "") + "\" runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=Pasivo\" runat=\"server\">Reporte de Pasivos Contingentes</a></li>" +
                                "<li id =\"liCT_rptProvisiones\" style=\"" + MenuVisible("CT_rptProvisiones", "") + "\" runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=Provision\" runat=\"server\">Reporte de Provisiones</a></li>" +
                                "<li id =\"liCT_rptAnulados\" style=\"" + MenuVisible("CT_rptAnulados", "") + "\" runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=Anulado\" style=\"padding-top:4px; runat=\"server\">Reporte de Expedientes Anulados</a></li>" +
                                "<li id =\"liCT_rptPagados\" style=\"" + MenuVisible("CT_rptPagados", "") + "\" runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=PAGADO\" style=\"padding-top:4px; runat=\"server\">Reporte de Expedientes Pagados</a></li>" +
                                "<li id =\"liCT_rptDuplicados\"  style=\"" + MenuVisible("CT_rptDuplicados", "") + "\"  runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=Duplicado\" runat=\"server\">Reporte de Expedientes Duplicados</a></li>" +
                                "<li id =\"liCT_rptxCruce\" style=\"" + MenuVisible("CT_rptxCruce", "") + "\"  runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=XCruce\" runat=\"server\">Reporte por Cruce de Variables</a></li> " +
                                "<li id =\"liCT_rptPrevisiones\"  style=\"" + MenuVisible("CT_rptPrevisiones", "") + "\"  runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=Previsiones\"  style=\"padding-top:4px;\" runat=\"server\">Reportes de Antiguedad de Saldos de Previsiones</a></li>" +
                                "<li id =\"liCT_rptPrevisionesMinisterios\"  style=\"" + MenuVisible("CT_rptPrevisionesMinisterios", "") + "\"  runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=PrevisionesMinisterios\"  style=\"padding-top:4px;\" runat=\"server\">Reportes de Antiguedad de Saldos de Previsiones Por Ministerio</a></li>" +
                                "<li id =\"liCT_rptCxC\"  style=\"" + MenuVisible("CT_rptCxC", "") + "\"  runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=CXC\" runat=\"server\">Informe de Cuentas Por Cobrar</a></li>" +
                                "<li id =\"liCT_rptCxP\" style=\"" + MenuVisible("CT_rptCxP", "") + "\"  runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=CXP\" runat=\"server\">Informe de Cuentas Por Pagar</a></li>" +
                                "<li id =\"liCT_rptCIC\" visible =\"false\" runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=CIC\" runat=\"server\">Reporte de Pagos de Contingentes</a></li>" +
                                "<li id =\"liCT_rptCxCxP\" style=\"" + MenuVisible("CT_rptCxCxP", "") + "\" runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=BITCON\" runat=\"server\">Reporte Bitácora Contingentes</a></li>" +
                                "<li id =\"liCT_rptExpG\" style=\"" + MenuVisible("CT_rptExpG", "") + "\" runat=\"server\"><a href=\"/Contingentes/ReportesContingentes.aspx?rept=GENERAL\" runat=\"server\">Reporte General de Expedientes</a></li>" +
                            "</ul>" +
                        "</li>" +
                        "<li class=\"\" id =\"liCT_Consultas\"><a class=\"\" href =\"#\" runat=\"server\">Consultas</a>" +
                            "<ul>" +
                                "<li id =\"liCT_csResoluciones\"><a href=\"/Contingentes/ResolucionesConsultar.aspx\" runat=\"server\">Consultar Resoluciones</a></li>" +
                                "<li id =\"liCT_csExpedientes\"><a href=\"/Contingentes/ConsultarExpedientes.aspx\" runat=\"\">Consultar Expedientes</a></li>" +
                            "</ul>" +
                        "</li>" +
                    "</ul>" +
                "</li>";
            }
            //Permisos de Plan de consolidación
            if (pPermisos.Contains("PC"))
                vMenu += "<li id =\"liOBJ_PC\" runat=\"server\" class=\"active\" visible =\"false\"><a href=\"#\" runat=\"server\"><span>Plan.Consolidación</span></a>" +
                     "<ul runat=\"server\">" +
                         "<li id =\"liPC_frmCargaEntidad\" style=\"" + MenuVisible("PC_frmCargaEntidad", "") + "\"  runat=\"server\"><a href =\"/Consolidacion/frmCargaEntidad.aspx\" class=\"\" runat=\"server\">Cargar Entidad</a></li>" +
                         "<li id =\"liPC_frmRevisionEntidad\" style=\"" + MenuVisible("PC_frmRevisionEntidad", "") + "\"  runat=\"server\"><a href =\"/Consolidacion/frmRevisionEntidad.aspx\" class=\"\" runat=\"server\">Revisión Entidad</a></li>" +
                         "<li id =\"liPC_frmRevisionAnalista\" style=\"" + MenuVisible("PC_frmRevisionAnalista", "") + "\" runat=\"server\" class=\"\"><a href =\"/Consolidacion/frmRevisionAnalista.aspx\" class=\"\" runat=\"server\">Revisión Analista</a></li>" +
                         "<li id =\"liPC_Reportes\" runat=\"server\" style=\"" + MenuVisible("PC_Reportes", "") + "\"><a href =\"#\" class=\"\" runat=\"server\">Reportes</a>" +
                             "<ul runat=\"server\">" +
                                 "<li id =\"liPC_rptBitacora\" runat=\"server\" style=\"" + MenuVisible("PC_rptBitacora", "") + "\"><a href =\"/Consolidacion/Reportes/BitacoraErroresPorFechaProcesoDTSX.aspx\" class=\"\" runat=\"server\">Bitácora</a></li>" +
                                 "<li id =\"liPC_rptEntidadTiempo\" runat=\"server\" style=\"" + MenuVisible("PC_rptEntidadTiempo", "") + "\"><a href =\"/Consolidacion/Reportes/EntidadesEntregaATiempoEstadosFinancieros.aspx\" class=\"\" runat=\"server\">Entidades a Tiempo</a></li>" +
                                 "<li id =\"liPC_rptEntidadRechazo\" runat=\"server\" style=\"" + MenuVisible("PC_rptEntidadRechazo", "") + "\"><a href =\"/Consolidacion/Reportes/EntidadesEntregaRechazadaEstadosFinancieros.aspx\" class=\"\" runat=\"server\">Entidades Rechazadas</a></li>" +
                                 "<li id =\"liPC_rptEntidadTarde\" runat=\"server\" style=\"" + MenuVisible("PC_rptEntidadTarde", "") + "\"><a href =\"/Consolidacion/Reportes/EntidadesEntregaTardeEstadosFinancieros.aspx\" class=\"\" runat=\"server\">Entidades Tardías</a></li>" +
                                 "<li id =\"liPC_rptEstadoDeuda\" runat=\"server\" style=\"" + MenuVisible("PC_rptEstadoDeuda", "") + "\"><a href =\"/Consolidacion/Reportes/EstadoFinancieroDeudaPublicaAgregado.aspx\" class=\"\" runat=\"server\">Deuda Pública Agregado</a></li>" +
                                 "<li id =\"liPC_rptEstadoDeudaDes\" runat=\"server\" style=\"" + MenuVisible("PC_rptEstadoDeudaDes", "") + "\"><a href =\"/Consolidacion/Reportes/EstadoFinancieroDeudaPublicaDesagregado.aspx\" class=\"\" runat=\"server\">Deuda Pública Desagregado</a></li>" +
                                 "<li id =\"liPC_rptEstadoDeudaDesAmb\" runat=\"server\" style=\"" + MenuVisible("PC_rptEstadoDeudaDesAmb", "") + "\"><a href =\"/Consolidacion/Reportes/EstadoFinancieroDeudaPublicaDesagregadoPorAmbitoConsolidacionNumConsecutivo.aspx\" style=\"padding-top:4px;\" runat=\"server\">Deuda Pública Desagregado por Ámbito</a></li>" +
                                 "<li id =\"liPC_rptEstadoNeto\" runat=\"server\" style=\"" + MenuVisible("PC_rptEstadoNeto", "") + "\"><a href =\"/Consolidacion/Reportes/EstadoFinancieroCambioPatrimonioNetoAgregado.aspx\" class=\"\" runat=\"server\">Patrimonio Agregado</a></li>" +
                                 "<li id =\"liPC_rptEstadoNetoDes\" runat=\"server\" style=\"" + MenuVisible("PC_rptEstadoNetoDes", "") + "\"><a href =\"/Consolidacion/Reportes/EstadoFinancieroCambioPatrimonioNetoDesagregado.aspx\" class=\"\" runat=\"server\">Patrimonio Desagregado</a></li>" +
                                 "<li id =\"liPC_rptEstadoNetoDesAmb\" runat=\"server\" style=\"" + MenuVisible("PC_rptEstadoNetoDesAmb", "") + "\"><a href =\"/Consolidacion/Reportes/EstadoFinancieroCambioPatrimonioNetoDesagregadoPorAmbitoConsolidacionNumCuenta.aspx\" class=\"\" runat=\"server\">Patrimonio Desagregado por Ámbito</a></li>" +
                                 "<li id =\"liPC_rptEstadoFlujo\" runat=\"server\" style=\"" + MenuVisible("PC_rptEstadoFlujo", "") + "\"><a href =\"/Consolidacion/Reportes/EstadoFinancieroFlujoEfectivoAgregado.aspx\" class=\"\" runat=\"server\">Flujo Efectivo</a></li>" +
                                 "<li id =\"liPC_rptEstadoFlujoDes\" runat=\"server\" style=\"" + MenuVisible("PC_rptEstadoFlujoDes", "") + "\"><a href =\"/Consolidacion/Reportes/EstadoFinancieroFlujoEfectivoDesagregado.aspx\" class=\"\" runat=\"server\">Flujo Efectivo Desagregado</a></li>" +
                                 "<li id =\"liPC_rptEstadoFlujoDesAmb\" runat=\"server\" style=\"" + MenuVisible("PC_rptEstadoFlujoDesAmb", "") + "\"><a href =\"/Consolidacion/Reportes/EstadoFinancieroFlujoEfectivoDesagregadoPorAmbitoConsolidacionNumCuenta.aspx\" class=\"\" runat=\"server\">Flujo Efectivo Desagregado por Ámbito</a></li>" +
                             "</ul>" +
                         "</li>" +
                     "</ul>" +
                 "</li>";
            //Permisos de Revelación de notas
            if (pPermisos.Contains("RN"))
            {
                vMenu += "<li id =\"liOBJ_RN\" runat=\"server\" class=\"active\"><a href =\"#\" runat=\"server\"><span>Revelación Notas</span></a>" +
                     "<ul>";
                if (pPermisos.Contains("DI") || pPermisos.Contains("DE")) {
                    vMenu += "<li  style=\"" + MenuVisible("CapturaInformacionDeudas", "") + "\"><a href =\"/RevelacionNotas/CapturaInformacionDeudas.aspx\" class=\"\" runat=\"server\">Revelaciones de la Deuda Pública</a></li>";
                    vMenu += "<li  style=\"" + MenuVisible("FormulariosDeuda", "") + "\"><a href =\"/RevelacionNotas/FormularioCatalogoNotas.aspx\" class=\"\" runat=\"server\">Formularios de deuda</a></li>";

                }
                vMenu += "<li><a href =\"/RevelacionNotas/Formularios.aspx\" class=\"\" runat=\"server\">Creación y consulta de formularios</a></li>" +
                    "<li><a href =\"/RevelacionNotas/Contingencias/ConsultarNotas.aspx\" class=\"\" runat=\"server\">Notas de Contingencias</a></li>" +
                    "<li><a href =\"/RevelacionNotas/FormulariosPendientes.aspx\" class=\"\" runat=\"server\">Revelaciones Pendiente de Aprobación</a></li>";


                vMenu += "</ul>" +
            "</li>";
            }
            //Permisos de seguridad
            if (pPermisos.Contains("SG"))
                vMenu += "<li id =\"liOBJ_SG\" runat=\"server\" class=\"active\" ><a href =\"#\" runat=\"server\"><span>Seguridad</span></a>" +
                     "<ul>" +
                         "<li><a href =\"/Seguridad/Usuarios.aspx\" class=\"\" runat=\"server\">Gestión de Usuarios</a></li>" +
                         "<li><a href =\"/Seguridad/GestionRoles.aspx\" class=\"\" runat=\"server\">Gestión de Roles</a></li>" +
                         "<li><a href =\"/Seguridad/GestionObjetos.aspx\" class=\"\" runat=\"server\">Gestión de Objetos</a></li>" +
                         "<li><a href =\"/Seguridad/Politicas.aspx\" class=\"\" runat=\"server\">Políticas</a></li>" +
                         "<li><a href =\"/Seguridad/Bitacora.aspx\" class=\"\" runat=\"server\">Bitácora</a></li>" +
                     "</ul>" +
                 "</li>";
            //Permisos de Mantenimiento
            if (pPermisos.Contains("MA"))
                vMenu += "<li id =\"liOBJ_MA\" runat=\"server\" class=\"active\" ><a href =\"#\" runat=\"server\"><span>Mantenimiento</span></a>" +
                     "<ul>" +
                         "<li id =\"lifrmCodSegmento\"  style=\"" + MenuVisible("frmCodigoSegmento", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmCodigoSegmento.aspx\" runat=\"server\">Código Segmento</a></li>" +
                         "<li id =\"lifrmModulos\"  style=\"" + MenuVisible("frmModulos", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmModulos.aspx\" runat=\"server\">Módulos</a></li>" +
                         "<li id =\"lifrmParametros\" style=\"" + MenuVisible("frmParametros", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmParametros.aspx\" class=\"\" runat=\"server\">Parámetros</a></li>" +
                         "<li id =\"lifrmCatalogosGenerales\" style=\"" + MenuVisible("frmCatalogosGenerales", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmCatalogosGenerales.aspx\" class=\"\" runat=\"server\"><span>Catálogos Generales</span></a></li>" +
                         "<li id =\"lifrmSociedadesGL\" style=\"" + MenuVisible("frmSociedadesGL", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmSociedadesGL.aspx\" class=\"\" runat=\"server\">Instituciones</a></li>" +
                         "<li id =\"lifrmBancos\" style=\"" + MenuVisible("frmBancos", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmBancos.aspx\" class=\"vinculoizquierda\" runat=\"server\">Bancos</a></li>" +

                         "<li id =\"lifrmMonedas\"  style=\"" + MenuVisible("frmMonedas", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmMonedas.aspx\" class=\"\" runat=\"server\">Monedas</a></li>" +
                         "<li id =\"lifrmTiposCambio\"  style=\"" + MenuVisible("frmTiposCambio", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmTiposCambio.aspx\" class=\"\" runat=\"server\">Tipos de Cambio</a></li>" +
                         "<li id =\"lifrmIndicadoresEconomicos\" style=\"" + MenuVisible("frmIndicadoresEconomicos", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmIndicadoresEconomicos.aspx\" class=\"\" runat=\"server\">Indicadores Económicos</a></li>" +
                         "<li id =\"lifrmValoresIndicadoresEco\" style=\"" + MenuVisible("frmValoresIndicadoresEco", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmValoresIndicadoresEco.aspx\" class=\"\" runat=\"server\">Valores Indicadores</a></li>" +
                         "<li id =\"lifrmPaises\" style=\"" + MenuVisible("frmPaises", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmPaises.aspx\" class=\"\" runat=\"server\">Países</a></li>" +
                         "<li id =\"lifrmSociedadesFi\" style=\"" + MenuVisible("frmSociedadesFi", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmSociedadesFi.aspx\" class=\"\" runat=\"server\">Sociedades Financieras</a></li>" +
                         "<li id =\"lifrmSociedadesCo\" style=\"" + MenuVisible("frmSociedadesCo", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmSociedadesCo.aspx\" class=\"\" runat=\"server\">Sociedades Costos</a></li>" +

                         "<li id =\"lifrmEntidadesCP\" style=\"" + MenuVisible("frmEntidadesCP", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmEntidadesCP.aspx\" class=\"\" runat=\"server\">Entidades Control</a></li>" +
                         "<li id =\"lifrmAreasFuncionales\" style=\"" + MenuVisible("frmAreasFuncionales", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmAreasFuncionales.aspx\" class=\"\" runat=\"server\">Áreas Funcionales</a></li>" +
                         "<li id =\"lifrmFondos\" style=\"" + MenuVisible("frmFondos", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmFondos.aspx\" class=\"\" runat=\"server\">Fondos</a></li>" +
                         "<li id =\"lifrmCentrosGestores\" style=\"" + MenuVisible("frmCentrosGestores", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmCentrosGestores.aspx\" class=\"\" runat=\"server\">Centros Gestores</a></li>" +
                         "<li id =\"lifrmProgramas\" style=\"" + MenuVisible("frmProgramas", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmProgramas.aspx\" class=\"\" runat=\"server\">Programas</a></li>" +
                         "<li id =\"lifrmPosicionesPresupuestarias\" style=\"" + MenuVisible("frmPosicionesPresupuestarias", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmPosicionesPresupuestarias.aspx\" class=\"\" runat=\"server\">Posiciones Presupuestarias</a></li>" +
                         "<li id =\"lifrmElementosPEP\" style=\"" + MenuVisible("frmElementosPEP", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmElementosPEP.aspx\" class=\"\" runat=\"server\">Elementos PEP</a></li>" +
                         "<li id =\"lifrmCentrosCosto\" style=\"" + MenuVisible("frmCentrosCosto", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmCentrosCosto.aspx\" class=\"\" runat=\"server\">Centros Costo</a></li>" +
                         "<li id =\"lifrmCentrosBeneficio\" style=\"" + MenuVisible("frmCentrosBeneficio", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmCentrosBeneficio.aspx\" class=\"\" runat=\"server\">Centros Beneficios</a></li>" +
                         "<li id =\"lifrmClasesDocumento\" style=\"" + MenuVisible("frmClasesDocumento", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmClasesDocumento.aspx\" class=\"\" runat=\"server\">Clases Documentos</a></li>" +
                         "<li id =\"lifrmCuentasContables\" style=\"" + MenuVisible("frmCuentasContables", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmCuentasContables.aspx\" class=\"\" runat=\"server\">Cuentas Contables</a></li>" +

                         "<li id =\"lifrmReservas\" style=\"" + MenuVisible("frmReservas", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmReservas.aspx\" class=\"\" runat=\"server\">Reservas</a></li>" +

                         "<li id =\"lifrmAcreedores\" style=\"" + MenuVisible("frmAcreedores", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmAcreedores.aspx\" class=\"\" runat=\"server\">Acreedores</a></li>" +
                         "<li id =\"lifrmNemotecnicos\" style=\"" + MenuVisible("frmNemotecnicos", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmNemotecnicos.aspx\" class=\"\" runat=\"server\">Nemotécnicos</a></li>" +
                         "<li id =\"lifrmOperaciones\" style=\"" + MenuVisible("frmOperaciones", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmOperaciones.aspx\" class=\"\" runat=\"server\">Operaciones</a></li>" +
                         "<li id =\"lifrmPropietarios\" style=\"" + MenuVisible("frmPropietarios", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmPropietarios.aspx\" class=\"\" runat=\"server\">Propietarios</a></li>" +
                         "<li id =\"lifrmServicios\" style=\"" + MenuVisible("frmServicios", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmServicios.aspx\" class=\"\" runat=\"server\">Servicios</a></li>" +
                         "<li id =\"lifrmTiposAsiento\" style=\"" + MenuVisible("frmTiposAsiento", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmTiposAsiento.aspx\" class=\"\" runat=\"server\">Tipos Asiento</a></li>" +
                         "<li id =\"lifrmCargarTiposAsientos\" style=\"" + MenuVisible("frmCargarTiposAsientos", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmCargarTiposAsientos.aspx\" class=\"\" runat=\"server\">Carga de Tipos Asiento</a></li>" +
                         //"<li id =\"lifrmTasaVariableTitulos\" style=\"" + MenuVisible("frmTasaVariableTitulos", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmEmisiones.aspx\" class=\"\" runat=\"server\">Emisiones</a></li>" +
                         "<li id =\"lifrmPrevisionesIncobrables\" style=\"" + MenuVisible("frmPrevisionesIncobrables", "") + "\" runat=\"server\"><a href =\"/Mantenimiento/frmPrevisionesIncobrables.aspx\" class=\"\" runat=\"server\">Previsiones Incobrables</a></li>" +

                     "</ul>" +
                 "</li>";

            //Todos los usuarios pueden editar el perfil
            vMenu += "<li id =\"liOBJ_PE\" runat=\"server\" class=\"active\"><a href =\"#\" runat=\"server\"><span>Perfil</span></a>" +
               " <ul>" +
                    "<li><a href =\"/Perfil/CambioContrasena.aspx\" class=\"\" runat=\"server\">Cambio de Contraseña</a></li>" +
                    "<li><a href =\"/Perfil/PerfilUsuario.aspx\" class=\"\" runat=\"server\">Perfil de Usuario</a></li>" +
                    "<li><a href =\"/Perfil/Empresas.aspx\" class=\"\" runat=\"server\">Registro de Empresas y Autorizados</a></li>" +
                "</ul>" +
            "</li>" +
        "</ul>" +
    "</div>";
                clsSesion.Current.MenuUsuario = vMenu;
            }  //gaston eliminar
          this.litMenu.Text = vMenu;
        }

        private string MenuVisible(string pIDObjeto, string pModulo)
        {
            string pVariable = "visibility: hidden;position: absolute;";
            List<string> vPermisos = new List<string>();
            //Carga los permisos que tiene el usuario
            DataSet ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(clsSesion.Current.LoginUsuario, pIDObjeto);
            
            for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
              //  if (ldt_PermisosUsuario.Tables["Table"].Rows[i]["Actualizar"].ToString() == "True")
                    vPermisos.Add(ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString());

            if (vPermisos.Contains(pIDObjeto) || pIDObjeto.Equals("frmReporteSaldoDeudaExt"))
                pVariable = "visibility:visible;";

            return pVariable;
        }
    }
}