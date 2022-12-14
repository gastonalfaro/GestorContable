 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using System.Configuration;
//using System.Data.SqlClient;
using System.Globalization;
using Presentacion.Compartidas;
using LogicaNegocio.Contingentes;
using System.IO;
using Presentacion.Contingentes.ArchivosCO;

namespace Presentacion.Contingentes
{
    public partial class Liquidacion : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private Presentacion.wsAsientos.ServicioContable ws_ContabilizaAsientos = new Presentacion.wsAsientos.ServicioContable();
        //private static clsResoluciones lresoluciones = new clsResoluciones();

        #region viejas
        private int CXPxCXC;
        private string idResolucion;
        private string idExpediente;
        private string estadoResolucion;
        private DateTime? fechaResolucion;
        private DateTime? PosibleFecSalidaRec;
        private decimal MontoPosibleReembolso;
        private decimal MontoPosReemColones;
        private string Observacion;
        private string userCrea;
        private string userModifica;
        private string esResolFirme;
        private string EstadoResolucion;
        private DateTime? FechaResolucion;
        private DateTime PosibleFecRembolso;
        private int CxCaCxP;
        private string Moneda;
        private string EstadoTransaccion;
        private decimal MontoPrincipal;
        private decimal MontoIntereses;
        private decimal InteresesMoratorios;
        private decimal InteresesMoratoriosColones;
        private decimal MontoInteresesColones;
        private decimal MontoPrincipalColones;//tipocambio*montoprincipal
        private decimal ValorPresenteIntColones;//Formula: VP= VF/(1+i)n
        private decimal ValorPresentePrincipal;//Formula: VP= VF/(1+i)n
        private decimal ValorPresenteIntereses;
        private decimal ValorPresentePrinColones;//Formula: VP= VF/(1+i)n
        private string TipoTransaccion;
        private string IdExpediente = string.Empty;
        private decimal TipoCambio;
        private string UsrModifica;
        private decimal Costas;
        private decimal CostasColones;
        private decimal DAnnoMoral;
        private decimal DAnnoMoralColones;
        private string Observaciones = string.Empty;
        #endregion


        private String gstr_Usuario = String.Empty;
        private String gstr_IdSociedadGL = String.Empty;

        private String str_Mensaje_Error = String.Empty;

        //private String gstr_Modulo = "CT";
        protected String gstr_Modulo
        {
            get
            {
                if (ViewState["gstr_Modulo"] == null)
                    ViewState["gstr_Modulo"] = "CT";
                return (String)ViewState["gstr_Modulo"];
            }
            set
            {
                ViewState["gstr_Modulo"] = value;
            }
        }
        //private String gstr_InModuloCT = "IdModulo In ('CT')";
        protected String gstr_InModuloCT
        {
            get
            {
                if (ViewState["gstr_InModuloCT"] == null)
                    ViewState["gstr_InModuloCT"] = "IdModulo In ('CT')"; 
                return (String)ViewState["gstr_InModuloCT"];
            }
            set
            {
                ViewState["gstr_InModuloCT"] = value;
            }
        }
        //private String gstr_Transaccion = "Liquidación";
        protected String gstr_Transaccion
        {
            get
            {
                if (ViewState["gstr_Transaccion"] == null)
                    ViewState["gstr_Transaccion"] = "Liquidación";
                return (String)ViewState["gstr_Transaccion"];
            }
            set
            {
                ViewState["gstr_Transaccion"] = value;
            }
        }
        //private String gstr_Leyenda = String.Empty;
        protected String gstr_Leyenda
        {
            get
            {
                if (ViewState["gstr_Leyenda"] == null)
                    ViewState["gstr_Leyenda"] = null;
                return (String)ViewState["gstr_Leyenda"];
            }
            set
            {
                ViewState["gstr_Leyenda"] = value;
            }
        }

        //private String gstr_IdExpediente = String.Empty;
        protected String gstr_IdExpediente
        {
            get
            {
                if (ViewState["gstr_IdExpediente"] == null)
                    ViewState["gstr_IdExpediente"] = null;
                return (String)ViewState["gstr_IdExpediente"];
            }
            set
            {
                ViewState["gstr_IdExpediente"] = value;
            }
        }

        //private static String gstr_IdResolucionDictada = String.Empty;
        protected String gstr_IdResolucionDictada
        {
            get
            {
                if (ViewState["gstr_IdResolucionDictada"] == null)
                    ViewState["gstr_IdResolucionDictada"] = null;
                return (String)ViewState["gstr_IdResolucionDictada"];
            }
            set
            {
                ViewState["gstr_IdResolucionDictada"] = value;
            }
        }
        //private static String gstr_TipoExpediente = String.Empty;
        protected String gstr_TipoExpediente
        {
            get
            {
                if (ViewState["gstr_TipoExpediente"] == null)
                    ViewState["gstr_TipoExpediente"] = null;
                return (String)ViewState["gstr_TipoExpediente"];
            }
            set
            {
                ViewState["gstr_TipoExpediente"] = value;
            }
        }
        //private static String gstr_TipoProceso = String.Empty;
        protected String gstr_TipoProceso
        {
            get
            {
                if (ViewState["gstr_TipoProceso"] == null)
                    ViewState["gstr_TipoProceso"] = null;
                return (String)ViewState["gstr_TipoProceso"];
            }
            set
            {
                ViewState["gstr_TipoProceso"] = value;
            }
        }
        //private static String gstr_Observacion = String.Empty;
        protected String gstr_Observacion
        {
            get
            {
                if (ViewState["gstr_Observacion"] == null)
                    ViewState["gstr_Observacion"] = null;
                return (String)ViewState["gstr_Observacion"];
            }
            set
            {
                ViewState["gstr_Observacion"] = value;
            }
        }

        //private static String gstr_Moneda = String.Empty;
        protected String gstr_Moneda
        {
            get
            {
                if (ViewState["gstr_Moneda"] == null)
                    ViewState["gstr_Moneda"] = null;
                return (String)ViewState["gstr_Moneda"];
            }
            set
            {
                ViewState["gstr_Moneda"] = value;
            }
        }
        //private static String gstr_ValorPresente = String.Empty;
        protected String gstr_ValorPresente
        {
            get
            {
                if (ViewState["gstr_ValorPresente"] == null)
                    ViewState["gstr_ValorPresente"] = null;
                return (String)ViewState["gstr_ValorPresente"];
            }
            set
            {
                ViewState["gstr_ValorPresente"] = value;
            }
        }
        //private static String gstr_MontoPretensionCol = String.Empty;
        protected String gstr_MontoPretensionCol
        {
            get
            {
                if (ViewState["gstr_MontoPretensionCol"] == null)
                    ViewState["gstr_MontoPretensionCol"] = null;
                return (String)ViewState["gstr_MontoPretensionCol"];
            }
            set
            {
                ViewState["gstr_MontoPretensionCol"] = value;
            }
        }
        //private static String gstr_MontoPretensionAnt = String.Empty;
        protected String gstr_MontoPretensionAnt
        {
            get
            {
                if (ViewState["gstr_MontoPretensionAnt"] == null)
                    ViewState["gstr_MontoPretensionAnt"] = null;
                return (String)ViewState["gstr_MontoPretensionAnt"];
            }
            set
            {
                ViewState["gstr_MontoPretensionAnt"] = value;
            }
        }
        //private static String gstr_MontoAjustado = String.Empty;
        protected String gstr_MontoAjustado
        {
            get
            {
                if (ViewState["gstr_MontoAjustado"] == null)
                    ViewState["gstr_MontoAjustado"] = null;
                return (String)ViewState["gstr_MontoAjustado"];
            }
            set
            {
                ViewState["gstr_MontoAjustado"] = value;
            }
        }

        //private static String gsrt_Moneda = String.Empty;
        protected String gsrt_Moneda
        {
            get
            {
                if (ViewState["gsrt_Moneda"] == null)
                    ViewState["gsrt_Moneda"] = null;
                return (String)ViewState["gsrt_Moneda"];
            }
            set
            {
                ViewState["gsrt_Moneda"] = value;
            }
        }

        //private String gstr_AsientosResultado = String.Empty;
        protected String gstr_AsientosResultado
        {
            get
            {
                if (ViewState["gstr_AsientosResultado"] == null)
                    ViewState["gstr_AsientosResultado"] = null;
                return (String)ViewState["gstr_AsientosResultado"];
            }
            set
            {
                ViewState["gstr_AsientosResultado"] = value;
            }
        }

        //private static Int32 gint_Periodo;
        protected Int32 gint_Periodo
        {
            get
            {
                if (ViewState["gint_Periodo"] == null)
                    ViewState["gint_Periodo"] = 0;
                return Convert.ToInt32(ViewState["gint_Periodo"]);
            }
            set
            {
                ViewState["gint_Periodo"] = value;
            }
        }
        //private Int32 gint_IdRes;
        protected Int32 gint_IdRes
        {
            get
            {
                if (ViewState["gint_IdRes"] == null)
                    ViewState["gint_IdRes"] = 0;
                return Convert.ToInt32(ViewState["gint_IdRes"]);
            }
            set
            {
                ViewState["gint_IdRes"] = value;
            }
        }
        //private static Int32 gint_CxCaCxP;
        protected Int32 gint_CxCaCxP
        {
            get
            {
                if (ViewState["gint_CxCaCxP"] == null)
                    ViewState["gint_CxCaCxP"] = 0;
                return Convert.ToInt32(ViewState["gint_CxCaCxP"]);
            }
            set
            {
                ViewState["gint_CxCaCxP"] = value;
            }
        }
        //private static Int32 gint_mes;
        protected Int32 gint_mes
        {
            get
            {
                if (ViewState["gint_mes"] == null)
                    ViewState["gint_mes"] = 0;
                return Convert.ToInt32(ViewState["gint_mes"]);
            }
            set
            {
                ViewState["gint_mes"] = value;
            }
        }

        //private static Decimal gdec_TipoCambio;
        protected Decimal gdec_TipoCambio
        {
            get
            {
                if (ViewState["gdec_TipoCambio"] == null)
                    ViewState["gdec_TipoCambio"] = 0;
                return Convert.ToDecimal(ViewState["gdec_TipoCambio"]);
            }
            set
            {
                ViewState["gdec_TipoCambio"] = value;
            }
        }
        //private static Decimal gdec_Venta;
        protected Decimal gdec_Venta
        {
            get
            {
                if (ViewState["gdec_Venta"] == null)
                    ViewState["gdec_Venta"] = 0;
                return Convert.ToDecimal(ViewState["gdec_Venta"]);
            }
            set
            {
                ViewState["gdec_Venta"] = value;
            }
        }
        //private static Decimal gdec_Compra;
        protected Decimal gdec_Compra
        {
            get
            {
                if (ViewState["gdec_Compra"] == null)
                    ViewState["gdec_Compra"] = 0;
                return Convert.ToDecimal(ViewState["gdec_Compra"]);
            }
            set
            {
                ViewState["gdec_Compra"] = value;
            }
        }
        //private static Decimal gdec_EUR;
        protected Decimal gdec_EUR
        {
            get
            {
                if (ViewState["gdec_EUR"] == null)
                    ViewState["gdec_EUR"] = 0;
                return Convert.ToDecimal(ViewState["gdec_EUR"]);
            }
            set
            {
                ViewState["gdec_EUR"] = value;
            }
        }

        //private static Decimal gdec_Tbp;
        protected Decimal gdec_Tbp
        {
            get
            {
                if (ViewState["gdec_Tbp"] == null)
                    ViewState["gdec_Tbp"] = 0;
                return Convert.ToDecimal(ViewState["gdec_Tbp"]);
            }
            set
            {
                ViewState["gdec_Tbp"] = value;
            }
        }
        //private static Decimal gdec_Tiempo;
        protected Decimal gdec_Tiempo
        {
            get
            {
                if (ViewState["gdec_Tiempo"] == null)
                    ViewState["gdec_Tiempo"] = 0;
                return Convert.ToDecimal(ViewState["gdec_Tiempo"]);
            }
            set
            {
                ViewState["gdec_Tiempo"] = value;
            }
        }

        //private static String gstr_MonedaAnterior;
        protected String gstr_MonedaAnterior
        {
            get
            {
                if (ViewState["gstr_MonedaAnterior"] == null)
                    ViewState["gstr_MonedaAnterior"] = null;
                return (String)ViewState["gstr_MonedaAnterior"];
            }
            set
            {
                ViewState["gstr_MonedaAnterior"] = value;
            }
        }
        //private static Decimal gdec_TipoCambioAnterior;
        protected Decimal gdec_TipoCambioAnterior
        {
            get
            {
                if (ViewState["gdec_TipoCambioAnterior"] == null)
                    ViewState["gdec_TipoCambioAnterior"] = 0;
                return Convert.ToDecimal(ViewState["gdec_TipoCambioAnterior"]);
            }
            set
            {
                ViewState["gdec_TipoCambioAnterior"] = value;
            }
        }
        //private static Decimal gdec_TbpAnterior;
        protected Decimal gdec_TbpAnterior
        {
            get
            {
                if (ViewState["gdec_TbpAnterior"] == null)
                    ViewState["gdec_TbpAnterior"] = 0;
                return Convert.ToDecimal(ViewState["gdec_TbpAnterior"]);
            }
            set
            {
                ViewState["gdec_TbpAnterior"] = value;
            }
        }
        //private static Decimal gdec_TiempoAnterior;
        protected Decimal gdec_TiempoAnterior
        {
            get
            {
                if (ViewState["gdec_TiempoAnterior"] == null)
                    ViewState["gdec_TiempoAnterior"] = 0;
                return Convert.ToDecimal(ViewState["gdec_TiempoAnterior"]);
            }
            set
            {
                ViewState["gdec_TiempoAnterior"] = value;
            }
        }
        //private static String gstr_ObservacionAnterior;
        protected String gstr_ObservacionAnterior
        {
            get
            {
                if (ViewState["gstr_ObservacionAnterior"] == null)
                    ViewState["gstr_ObservacionAnterior"] = null;
                return (String)ViewState["gstr_ObservacionAnterior"];
            }
            set
            {
                ViewState["gstr_ObservacionAnterior"] = value;
            }
        }
        //private static Decimal gdec_TipoCambioCierre;
        protected Decimal gdec_TipoCambioCierre
        {
            get
            {
                if (ViewState["gdec_TipoCambioCierre"] == null)
                    ViewState["gdec_TipoCambioCierre"] = null;
                return Convert.ToDecimal(ViewState["gdec_TipoCambioCierre"]);
            }
            set
            {
                ViewState["gdec_TipoCambioCierre"] = value;
            }
        }

        //private static Decimal gdec_Intereses;
        protected Decimal gdec_Intereses
        {
            get
            {
                if (ViewState["gdec_Intereses"] == null)
                    ViewState["gdec_Intereses"] = null;
                return Convert.ToDecimal(ViewState["gdec_Intereses"]);
            }
            set
            {
                ViewState["gdec_Intereses"] = value;
            }
        }
        //private static Decimal gdec_Costas;
        protected Decimal gdec_Costas
        {
            get
            {
                if (ViewState["gdec_Costas"] == null)
                    ViewState["gdec_Costas"] = null;
                return Convert.ToDecimal(ViewState["gdec_Costas"]);
            }
            set
            {
                ViewState["gdec_Costas"] = value;
            }
        }
        //private static Decimal gdec_InteresesMoratorios;
        protected Decimal gdec_InteresesMoratorios
        {
            get
            {
                if (ViewState["gdec_InteresesMoratorios"] == null)
                    ViewState["gdec_InteresesMoratorios"] = null;
                return Convert.ToDecimal(ViewState["gdec_InteresesMoratorios"]);
            }
            set
            {
                ViewState["gdec_InteresesMoratorios"] = value;
            }
        }
        //private static Decimal gdec_DannoMoral;
        protected Decimal gdec_DannoMoral
        {
            get
            {
                if (ViewState["gdec_DannoMoral"] == null)
                    ViewState["gdec_DannoMoral"] = 0;
                return Convert.ToDecimal(ViewState["gdec_DannoMoral"]);
            }
            set
            {
                ViewState["gdec_DannoMoral"] = value;
            }
        }

        //private Decimal gdec_InteresesColonesLiq;
        protected Decimal gdec_InteresesColonesLiq
        {
            get
            {
                if (ViewState["gdec_InteresesColonesLiq"] == null)
                    ViewState["gdec_InteresesColonesLiq"] = 0;
                return Convert.ToDecimal(ViewState["gdec_InteresesColonesLiq"]);
            }
            set
            {
                ViewState["gdec_InteresesColonesLiq"] = value;
            }
        }
        //private Decimal gdec_CostasColones;
        protected Decimal gdec_CostasColones
        {
            get
            {
                if (ViewState["gdec_CostasColones"] == null)
                    ViewState["gdec_CostasColones"] = 0;
                return Convert.ToDecimal(ViewState["gdec_CostasColones"]);
            }
            set
            {
                ViewState["gdec_CostasColones"] = value;
            }
        }
        //private Decimal gdec_InteresesMoratoriosColones;
        protected Decimal gdec_InteresesMoratoriosColones
        {
            get
            {
                if (ViewState["gdec_InteresesMoratoriosColones"] == null)
                    ViewState["gdec_InteresesMoratoriosColones"] = 0;
                return Convert.ToDecimal(ViewState["gdec_InteresesMoratoriosColones"]);
            }
            set
            {
                ViewState["gdec_InteresesMoratoriosColones"] = value;
            }
        }
        //private Decimal gdec_DannoMoralColones;
        protected Decimal gdec_DannoMoralColones
        {
            get
            {
                if (ViewState["gdec_DannoMoralColones"] == null)
                    ViewState["gdec_DannoMoralColones"] = 0;
                return Convert.ToDecimal(ViewState["gdec_DannoMoralColones"]);
            }
            set
            {
                ViewState["gdec_DannoMoralColones"] = value;
            }
        }

        //private static Decimal gdec_InteresesColAnterior;
        protected Decimal gdec_InteresesColAnterior
        {
            get
            {
                if (ViewState["gdec_InteresesColAnterior"] == null)
                    ViewState["gdec_InteresesColAnterior"] = null;
                return Convert.ToDecimal(ViewState["gdec_InteresesColAnterior"]);
            }
            set
            {
                ViewState["gdec_InteresesColAnterior"] = value;
            }
        }
        //private static Decimal gdec_InteresesMoratoriosColAnterior;
        protected Decimal gdec_InteresesMoratoriosColAnterior
        {
            get
            {
                if (ViewState["gdec_InteresesMoratoriosColAnterior"] == null)
                    ViewState["gdec_InteresesMoratoriosColAnterior"] = null;
                return Convert.ToDecimal(ViewState["gdec_InteresesMoratoriosColAnterior"]);
            }
            set
            {
                ViewState["gdec_InteresesMoratoriosColAnterior"] = value;
            }
        }
        //private static Decimal gdec_CostasColAnterior;
        protected Decimal gdec_CostasColAnterior
        {
            get
            {
                if (ViewState["gdec_CostasColAnterior"] == null)
                    ViewState["gdec_CostasColAnterior"] = null;
                return Convert.ToDecimal(ViewState["gdec_CostasColAnterior"]);
            }
            set
            {
                ViewState["gdec_CostasColAnterior"] = value;
            }
        }
        //private static Decimal gdec_DannoMoralColAnterior;
        protected Decimal gdec_DannoMoralColAnterior
        {
            get
            {
                if (ViewState["gdec_DannoMoralColAnterior"] == null)
                    ViewState["gdec_DannoMoralColAnterior"] = null;
                return Convert.ToDecimal(ViewState["gdec_DannoMoralColAnterior"]);
            }
            set
            {
                ViewState["gdec_DannoMoralColAnterior"] = value;
            }
        }

        //private static Decimal gdec_InteresesAjuste;
        protected Decimal gdec_InteresesAjuste
        {
            get
            {
                if (ViewState["gdec_InteresesAjuste"] == null)
                    ViewState["gdec_InteresesAjuste"] = null;
                return Convert.ToDecimal(ViewState["gdec_InteresesAjuste"]);
            }
            set
            {
                ViewState["gdec_InteresesAjuste"] = value;
            }
        }
        //private static Decimal gdec_InteresesMoratoriosAjuste;
        protected Decimal gdec_InteresesMoratoriosAjuste
        {
            get
            {
                if (ViewState["gdec_InteresesMoratoriosAjuste"] == null)
                    ViewState["gdec_InteresesMoratoriosAjuste"] = null;
                return Convert.ToDecimal(ViewState["gdec_InteresesMoratoriosAjuste"]);
            }
            set
            {
                ViewState["gdec_InteresesMoratoriosAjuste"] = value;
            }
        }
        //private static Decimal gdec_CostasAjuste;
        protected Decimal gdec_CostasAjuste
        {
            get
            {
                if (ViewState["gdec_CostasAjuste"] == null)
                    ViewState["gdec_CostasAjuste"] = null;
                return Convert.ToDecimal(ViewState["gdec_CostasAjuste"]);
            }
            set
            {
                ViewState["gdec_CostasAjuste"] = value;
            }
        }
        //private static Decimal gdec_DannoMoralAjuste;
        protected Decimal gdec_DannoMoralAjuste
        {
            get
            {
                if (ViewState["gdec_DannoMoralAjuste"] == null)
                    ViewState["gdec_DannoMoralAjuste"] = null;
                return Convert.ToDecimal(ViewState["gdec_DannoMoralAjuste"]);
            }
            set
            {
                ViewState["gdec_DannoMoralAjuste"] = value;
            }
        }

        //private static Decimal gdec_InteresesAnterior;
        protected Decimal gdec_InteresesAnterior
        {
            get
            {
                if (ViewState["gdec_InteresesAnterior"] == null)
                    ViewState["gdec_InteresesAnterior"] = null;
                return Convert.ToDecimal(ViewState["gdec_InteresesAnterior"]);
            }
            set
            {
                ViewState["gdec_InteresesAnterior"] = value;
            }
        }
        //private static Decimal gdec_InteresesMoratoriosAnterior;
        protected Decimal gdec_InteresesMoratoriosAnterior
        {
            get
            {
                if (ViewState["gdec_InteresesMoratoriosAnterior"] == null)
                    ViewState["gdec_InteresesMoratoriosAnterior"] = null;
                return Convert.ToDecimal(ViewState["gdec_InteresesMoratoriosAnterior"]);
            }
            set
            {
                ViewState["gdec_InteresesMoratoriosAnterior"] = value;
            }
        }
        //private static Decimal gdec_CostasAnterior;
        protected Decimal gdec_CostasAnterior
        {
            get
            {
                if (ViewState["gdec_CostasAnterior"] == null)
                    ViewState["gdec_CostasAnterior"] = null;
                return Convert.ToDecimal(ViewState["gdec_CostasAnterior"]);
            }
            set
            {
                ViewState["gdec_CostasAnterior"] = value;
            }
        }
        //private static Decimal gdec_DannoMoralAnterior;
        protected Decimal gdec_DannoMoralAnterior
        {
            get
            {
                if (ViewState["gdec_DannoMoralAnterior"] == null)
                    ViewState["gdec_DannoMoralAnterior"] = 0;
                return Convert.ToDecimal(ViewState["gdec_DannoMoralAnterior"]);
            }
            set
            {
                ViewState["gdec_DannoMoralAnterior"] = value;
            }
        }

        //private String gstr_TipoTransaccion = String.Empty;
        protected String gstr_TipoTransaccion
        {
            get
            {
                if (ViewState["gstr_TipoTransaccion"] == null)
                    ViewState["gstr_TipoTransaccion"] = null;
                return (String)ViewState["gstr_TipoTransaccion"];
            }
            set
            {
                ViewState["gstr_TipoTransaccion"] = value;
            }
        }

        //private Int32 gint_EstadoPretension;
        protected Int32 gint_EstadoPretension
        {
            get
            {
                if (ViewState["gint_EstadoPretension"] == null)
                    ViewState["gint_EstadoPretension"] = 0;
                return Convert.ToInt32(ViewState["gint_EstadoPretension"]);
            }
            set
            {
                ViewState["gint_EstadoPretension"] = value;
            }
        }

        private Decimal[] garrdec_Montos;
        //private Boolean glbool_cambioMonto;
        protected Boolean glbool_cambioMonto
        {
            get
            {
                if (ViewState["glbool_cambioMonto"] == null)
                    ViewState["glbool_cambioMonto"] = false;
                return (Boolean)ViewState["glbool_cambioMonto"];
            }
            set
            {
                ViewState["glbool_cambioMonto"] = value;
            }
        }

        //private String gstr_EstadoResolucion = String.Empty;
        protected String gstr_EstadoResolucion
        {
            get
            {
                if (ViewState["gstr_EstadoResolucion"] == null)
                    ViewState["gstr_EstadoResolucion"] = null;
                return (String)ViewState["gstr_EstadoResolucion"];
            }
            set
            {
                ViewState["gstr_EstadoResolucion"] = value;
            }
        }
        //private String gstr_NumResolucion = String.Empty;
        protected String gstr_NumResolucion
        {
            get
            {
                if (ViewState["gstr_NumResolucion"] == null)
                    ViewState["gstr_NumResolucion"] = null;
                return (String)ViewState["gstr_NumResolucion"];
            }
            set
            {
                ViewState["gstr_NumResolucion"] = value;
            }
        }
        //private String gstr_TipoTransaccion = String.Empty;
        //private String gstr_EstadoTransaccion = String.Empty;
        protected String gstr_EstadoTransaccion
        {
            get
            {
                if (ViewState["gstr_EstadoTransaccion"] == null)
                    ViewState["gstr_EstadoTransaccion"] = null;
                return (String)ViewState["gstr_EstadoTransaccion"];
            }
            set
            {
                ViewState["gstr_EstadoTransaccion"] = value;
            }
        }
        //private String gstr_EstadoProcesal = String.Empty;
        protected String gstr_EstadoProcesal
        {
            get
            {
                if (ViewState["gstr_EstadoProcesal"] == null)
                    ViewState["gstr_EstadoProcesal"] = null;
                return (String)ViewState["gstr_EstadoProcesal"];
            }
            set
            {
                ViewState["gstr_EstadoProcesal"] = value;
            }
        }
        //private String gstr_Estado = String.Empty;
        protected String gstr_Estado
        {
            get
            {
                if (ViewState["gstr_Estado"] == null)
                    ViewState["gstr_Estado"] = null;
                return (String)ViewState["gstr_Estado"];
            }
            set
            {
                ViewState["gstr_Estado"] = value;
            }
        }

        private DateTime? gdt_FechaResolucion;
        private DateTime? gdt_PosibleFechaSalida;
        //private static String gstr_FechaResolucion;
        protected String gstr_FechaResolucion
        {
            get
            {
                if (ViewState["gstr_FechaResolucion"] == null)
                    ViewState["gstr_FechaResolucion"] = null;
                return (String)ViewState["gstr_FechaResolucion"];
            }
            set
            {
                ViewState["gstr_FechaResolucion"] = value;
            }
        }
        //private static String gstr_PosibleFechaSalida;
        protected String gstr_PosibleFechaSalida
        {
            get
            {
                if (ViewState["gstr_PosibleFechaSalida"] == null)
                    ViewState["gstr_PosibleFechaSalida"] = null;
                return (String)ViewState["gstr_PosibleFechaSalida"];
            }
            set
            {
                ViewState["gstr_PosibleFechaSalida"] = value;
            }
        }

        //private Boolean gbool_TieneLiq;
        protected Boolean gbool_TieneLiq
        {
            get
            {
                if (ViewState["gbool_TieneLiq"] == null)
                    ViewState["gbool_TieneLiq"] = false;
                return (Boolean)ViewState["gbool_TieneLiq"];
            }
            set
            {
                ViewState["gbool_TieneLiq"] = value;
            }
        }
        //private Int32 gint_CantidadLineasAsiento;
        protected Int32 gint_CantidadLineasAsiento
        {
            get
            {
                if (ViewState["gint_CantidadLineasAsiento"] == null)
                    ViewState["gint_CantidadLineasAsiento"] = 0;
                return Convert.ToInt32(ViewState["gint_CantidadLineasAsiento"]);
            }
            set
            {
                ViewState["gint_CantidadLineasAsiento"] = value;
            }
        }

        //private String gstr_MensajeError;
        protected String gstr_MensajeError
        {
            get
            {
                if (ViewState["gstr_MensajeError"] == null)
                    ViewState["gstr_MensajeError"] = null;
                return (String)ViewState["gstr_MensajeError"];
            }
            set
            {
                ViewState["gstr_MensajeError"] = value;
            }
        }


        //private Boolean gbool_CambioMes = false;
        protected Boolean gbool_CambioMes
        {
            get
            {
                if (ViewState["gbool_CambioMes"] == null)
                    ViewState["gbool_CambioMes"] = false;
                return (Boolean)ViewState["gbool_CambioMes"];
            }
            set
            {
                ViewState["gbool_CambioMes"] = value;
            }
        }
        //private Boolean gbool_CambioAno = false;
        protected Boolean gbool_CambioAno
        {
            get
            {
                if (ViewState["gbool_CambioAno"] == null)
                    ViewState["gbool_CambioAno"] = false;
                return (Boolean)ViewState["gbool_CambioAno"];
            }
            set
            {
                ViewState["gbool_CambioAno"] = value;
            }
        }

        private Asiento asiento = new Asiento();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gstr_IdSociedadGL = clsSesion.Current.SociedadUsr;
            gstr_IdExpediente = this.ddlIdExpediente.SelectedValue;


            string[] tipocambio = new string[4];
            string idExpediente = Request.QueryString["id"];//idRresolucion
            string Nuevo = Request.QueryString["isAdd"];//Modificado
            Session["isAdd"] = Nuevo;
            Session["IdExp"] = idExpediente;
            ViewState["Nuevo"] = Session["isAdd"];
            ViewState["IdExpediente"] = Session["IdExp"];
            bool strNuevo = Convert.ToBoolean(ViewState["Nuevo"]);
            String idExp = Convert.ToString(ViewState["IdExpediente"]);

            //CargarMonedas();
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CT"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        CargarMonedas();
                        CargarExpedientes();

                        tipocambio = CargarIndicadoresEco();
                        //cargar valored de tipo de cambio e indicadores economicos
                        this.txtCompra.Text = tipocambio[0];
                        this.txtVenta.Text = tipocambio[1];
                        this.txtEuro.Text = tipocambio[2];
                        // this.txtTBP.Text = tipocambio[3];

                        if (!strNuevo)//Viene del select del grid a modificar
                        {

                            CargarLiquidacion(idExp);
                            CambioTipoCambioAnterior();
                            //Cargar Archivos en tabla tabla
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sesión de usuario finalizó.");
                    Response.Redirect("~/Login.aspx", true);
                }
            }
            else
            {

                if (String.IsNullOrEmpty(gstr_Usuario))
                {
                    MessageBox.Show("Sesión de usuario finalizó.");
                    Response.Redirect("~/Login.aspx", true);
                }
            }

        }

        protected void ddlIdExpediente_SelectedIndexChanged(object sender, EventArgs e)
        {
            gstr_IdExpediente = this.ddlIdExpediente.SelectedValue;
            this.CargarLiquidacion(this.ddlIdExpediente.SelectedValue);
            CambioTipoCambioAnterior();
        }

        protected void DDLMoneda_SelectedIndexChanged(object sender, EventArgs e) { ActualizaMontos(); }

        private void ActualizaMontos()
        {
            gdec_Venta = Convert.ToDecimal(this.txtVenta.Text);
            gdec_Compra = Convert.ToDecimal(this.txtCompra.Text);
            gdec_EUR = Convert.ToDecimal(this.txtEuro.Text);

            gdec_Intereses = txtIntereses.Text == "" ? 0 : Convert.ToDecimal(txtIntereses.Text);
            gdec_Costas = txtCostas.Text == "" ? 0 : Convert.ToDecimal(txtCostas.Text);
            gdec_InteresesMoratorios = txtInteresesMoratorios.Text == "" ? 0 : Convert.ToDecimal(txtInteresesMoratorios.Text);
            gdec_DannoMoral = txtDannoMoral.Text == "" ? 0 : Convert.ToDecimal(txtDannoMoral.Text);

            if (!this.ddlIdExpediente.SelectedItem.Value.Equals("0"))
            {
                gstr_IdExpediente = this.ddlIdExpediente.SelectedItem.Value;
            }

            this.txtIntereses.Text = gdec_Intereses.ToString("N2");
            this.txtCostas.Text = gdec_Costas.ToString("N2");
            this.txtInteresesMoratorios.Text = gdec_InteresesMoratorios.ToString("N2");
            this.txtDannoMoral.Text = gdec_DannoMoral.ToString("N2");

            if (this.DDLMoneda.SelectedValue.Contains("USD"))
            {
                if (ConsultarTipoExpediente(gstr_IdExpediente).Equals("Actor"))
                {
                    txtInteresesColones.Text = (gdec_Intereses * gdec_Compra).ToString("N2");
                    gdec_InteresesColonesLiq = decimal.Parse(this.txtInteresesColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                    txtCostasColones.Text = (gdec_Costas * gdec_Compra).ToString("N2");
                    gdec_CostasColones = decimal.Parse(txtCostasColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    txtInteresesMoratoriosColones.Text = (gdec_InteresesMoratorios * gdec_Compra).ToString("N2");
                    gdec_InteresesMoratoriosColones = decimal.Parse(txtInteresesMoratoriosColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    txtDannoMoralColones.Text = (gdec_DannoMoral * gdec_Compra).ToString("N2");
                    gdec_DannoMoralColones = decimal.Parse(txtDannoMoralColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    gdec_TipoCambio = gdec_Compra;

                }
                else if (ConsultarTipoExpediente(gstr_IdExpediente).Equals("Demandado"))
                {
                    txtInteresesColones.Text = (gdec_Intereses * gdec_Venta).ToString("N2");
                    gdec_InteresesColonesLiq = decimal.Parse(this.txtInteresesColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                    txtCostasColones.Text = (gdec_Costas * gdec_Venta).ToString("N2");
                    gdec_CostasColones = decimal.Parse(txtCostasColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    txtInteresesMoratoriosColones.Text = (gdec_InteresesMoratorios * gdec_Venta).ToString("N2");
                    gdec_InteresesMoratoriosColones = decimal.Parse(txtInteresesMoratoriosColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    txtDannoMoralColones.Text = (gdec_DannoMoral * gdec_Venta).ToString("N2");
                    gdec_DannoMoralColones = decimal.Parse(txtDannoMoralColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    gdec_TipoCambio = gdec_Venta;
                }
            }
            else if (this.DDLMoneda.SelectedValue.Contains("EUR"))
            {
                if (ConsultarTipoExpediente(gstr_IdExpediente).Equals("Actor"))
                {
                    txtInteresesColones.Text = (gdec_Intereses * Math.Round(gdec_Compra * gdec_EUR,2)).ToString("N2");
                    gdec_InteresesColonesLiq = decimal.Parse(this.txtInteresesColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                    txtCostasColones.Text = (gdec_Costas * Math.Round(gdec_Compra * gdec_EUR,2)).ToString("N2");
                    gdec_CostasColones = decimal.Parse(txtCostasColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    txtInteresesMoratoriosColones.Text = (gdec_InteresesMoratorios * Math.Round(gdec_Compra * gdec_EUR,2)).ToString("N2");
                    gdec_InteresesMoratoriosColones = decimal.Parse(txtInteresesMoratoriosColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    txtDannoMoralColones.Text = (gdec_DannoMoral * Math.Round(gdec_Compra * gdec_EUR,2)).ToString("N2");
                    gdec_DannoMoralColones = decimal.Parse(txtDannoMoralColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    gdec_TipoCambio = Math.Round(gdec_Compra * gdec_EUR,2);
                }
                else if (ConsultarTipoExpediente(gstr_IdExpediente).Equals("Demandado"))
                {
                    txtInteresesColones.Text = (gdec_Intereses * Math.Round(gdec_Venta * gdec_EUR,2)).ToString("N2");
                    gdec_InteresesColonesLiq = decimal.Parse(this.txtInteresesColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                    txtCostasColones.Text = (gdec_Costas * Math.Round(gdec_Venta * gdec_EUR,2)).ToString("N2");
                    gdec_CostasColones = decimal.Parse(txtCostasColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    txtInteresesMoratoriosColones.Text = (gdec_InteresesMoratorios * Math.Round(gdec_Venta * gdec_EUR,2)).ToString("N2");
                    gdec_InteresesMoratoriosColones = decimal.Parse(txtInteresesMoratoriosColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    txtDannoMoralColones.Text = (gdec_DannoMoral * Math.Round(gdec_Venta * gdec_EUR,2)).ToString("N2");
                    gdec_DannoMoralColones = decimal.Parse(txtDannoMoralColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                    gdec_TipoCambio = Math.Round(gdec_Venta * gdec_EUR,2);
                }
            }
            else if (this.DDLMoneda.SelectedValue.Contains("CRC"))
            {
                txtInteresesColones.Text = (gdec_Intereses * 1).ToString("N2");
                gdec_InteresesColonesLiq = decimal.Parse(this.txtInteresesColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                txtCostasColones.Text = (gdec_Costas * 1).ToString("N2");
                gdec_CostasColones = decimal.Parse(txtCostasColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                txtInteresesMoratoriosColones.Text = (gdec_InteresesMoratorios * 1).ToString("N2");
                gdec_InteresesMoratoriosColones = decimal.Parse(txtInteresesMoratoriosColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                txtDannoMoralColones.Text = (gdec_DannoMoral * 1).ToString("N2");
                gdec_DannoMoralColones = decimal.Parse(txtDannoMoralColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);

                gdec_TipoCambio = 1;
            }
            gdec_TipoCambio = Math.Round(gdec_TipoCambio, 2);
            if (ConsultarTipoExpediente(gstr_IdExpediente).Equals("Actor"))
            {
                EstadoTransaccion = "Cobro";
                CxCaCxP = 1;
            }
            else if (ConsultarTipoExpediente(gstr_IdExpediente).Equals("Actor"))
            {
                EstadoTransaccion = "Pago";
                CxCaCxP = 0;
            }
            //this.upMontos.Update();
        }

        private void CambioTipoCambioAnterior()
        {
            tieneLiq();
            gdec_TipoCambio = (gdec_TipoCambioAnterior == 0) ? gdec_TipoCambio : gdec_TipoCambioAnterior;
            gstr_Moneda = gstr_MonedaAnterior;
            gdec_TipoCambio = Math.Round(gdec_TipoCambio, 2);
            gdec_InteresesColonesLiq = gdec_Intereses * gdec_TipoCambio;
            gdec_InteresesMoratoriosColones = gdec_InteresesMoratorios * gdec_TipoCambio;
            gdec_CostasColones = gdec_Costas * gdec_TipoCambio;
            gdec_DannoMoralColones = gdec_DannoMoral * gdec_TipoCambio;

            txtInteresesColones.Text = gdec_InteresesColonesLiq.ToString("N2");
            txtInteresesMoratoriosColones.Text = gdec_InteresesMoratoriosColones.ToString("N2");
            txtCostasColones.Text = gdec_CostasColones.ToString("N2");
            txtDannoMoralColones.Text = gdec_DannoMoralColones.ToString("N2");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarResolucion();
        }

        private void tieneLiq()
        {
            string lstr_CodAsiento = string.Empty;
            string[] larrstr_ResultadoLiquidacion = new string[3];
            string lstr_mensj = string.Empty;
            String lstr_Resultado = String.Empty;
            String lstr_ResEnviarRev = String.Empty;

            Boolean lbool_Incobrable = this.ckbIncobrable.Checked;

            DataSet lds_ConsultarResolucion = new DataSet();
            DataSet lds_ConsultarExpediente = new DataSet();
            string resultado = string.Empty;
            String[] larrstr_ResultadoModificacion = new String[2];
            Decimal[] dec_MontosActual = new decimal[15];

            Decimal[] arrdec_montosProv1 = new decimal[15];

            string lstr_Monto = string.Empty;
            string lstr_MontoReversar = string.Empty;

            String[] larrstr_ResultadoConsResolucion = new String[2];

            String lstr_Codigo = String.Empty;
            String lstr_Mensaje = String.Empty;
            DataRow ldr_ConsultarResolucion = null;

            gbool_CambioMes = this.ckbNuevoMes.Checked;
            gbool_CambioAno = this.ckbNuevoAno.Checked;


            #region existe Liquidación/RF?
            lds_ConsultarResolucion = ws_SGService.uwsConsultarResolucion("", gstr_IdExpediente, gstr_IdSociedadGL, out lstr_Codigo, out lstr_Mensaje);
            if (lstr_Codigo.Contains("00"))
            {
                for (int i = 0; i < lds_ConsultarResolucion.Tables["Table"].Rows.Count; i++)
                {
                    ldr_ConsultarResolucion = lds_ConsultarResolucion.Tables["Table"].Rows[i];
                    if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Equals("Liquidacion"))
                    {
                        if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["TipoCambio1"].ToString()))//ggarcia
                            gdec_TipoCambioAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["TipoCambio1"].ToString());
                        else
                            gdec_TipoCambioAnterior = 0;
                        gdec_TipoCambioCierre = ldr_ConsultarResolucion["TipoCambioCierre"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["TipoCambioCierre"].ToString());

                        gstr_MonedaAnterior = ldr_ConsultarResolucion["Moneda"].ToString();

                    }
                }

            }
            #endregion
        }

        // |||||||||||||||||||||||||||||||||||||||||||||
        private void GuardarResolucion()
        {
            #region Variables
            string lstr_CodAsiento = string.Empty;
            string[] larrstr_ResultadoLiquidacion = new string[3];
            string lstr_mensj = string.Empty;
            String lstr_Resultado = String.Empty;
            String lstr_ResEnviarRev = String.Empty;

            Boolean lbool_TienePretensionInicial = false;
            Boolean lbool_TieneRP1 = false;
            Boolean lbool_TieneRP2 = false;
            Boolean lbool_TieneRF = false;
            Boolean lbool_Reinicio = false;
            Boolean lbool_Prevision = false;
            Boolean lbool_Incobrable = this.ckbIncobrable.Checked;

            DataSet lds_ConsultarResolucion = new DataSet();
            DataSet lds_ConsultarExpediente = new DataSet();

            Decimal ldec_MontoAjuste = 0;

            int periodo = 0;

            string idOperacion;
            string resultado = string.Empty;
            String[] larrstr_ResultadoModificacion = new String[2];
            Decimal[] dec_MontosActual = new decimal[15];

            Decimal[] arrdec_montosProv1 = new decimal[15];
            Decimal dec_montoPrincipalProv1 = 0;
            Decimal dec_montoInteresProv1 = 0;

            Decimal[] arrdec_montosDiferencia = new decimal[15];
            Decimal ldec_montoPrincipalDiferencia = 0;
            Decimal ldec_montoInteresesDiferencia = 0;

            Decimal[] arrdec_montosProv2 = new decimal[15];
            Decimal dec_montoPrincipalProv2 = 0;
            Decimal dec_montoInteresProv2 = 0;

            Decimal ldec_InteresesPrevision = 0;
            Decimal ldec_InteresesMoratoriosPrevision = 0;
            Decimal ldec_CostasPrevision = 0;
            Decimal ldec_DannoMoralPrevision = 0;

            Decimal ldec_InteresesCierre = 0;
            Decimal ldec_InteresesMoratoriosCierre = 0;
            Decimal ldec_CostasCierre = 0;
            Decimal ldec_DannoMoralCierre = 0;

            string lstr_Monto = string.Empty;
            string lstr_MontoReversar = string.Empty;

            String[] larrstr_ResultadoConsResolucion = new String[2];

            String lstr_Codigo = String.Empty;
            String lstr_Mensaje = String.Empty;
            DataRow ldr_ConsultarResolucion = null;

            DataRow ldr_ConsultarExpediente = null;

            Int32 lint_IdRes = 0;
            Int32 lint_IdCobroPagoResolucion;

            gbool_CambioMes = this.ckbNuevoMes.Checked;
            gbool_CambioAno = this.ckbNuevoAno.Checked;

            asiento.definirExpediente(gstr_IdExpediente, gstr_IdSociedadGL, gstr_Usuario);
            ActualizaMontos();

            #endregion

            try
            {
                gstr_AsientosResultado = "";
                gstr_IdExpediente = this.ddlIdExpediente.SelectedItem.Value;
                gstr_TipoExpediente = ConsultarTipoExpediente(gstr_IdExpediente);
                gstr_EstadoResolucion = "Liquidacion";
                gstr_IdResolucionDictada = this.txtNumResol.Text;
                gstr_Observacion = this.CKEditorObservaciones.Text;

                #region existe Pretención Inicial??
                lds_ConsultarExpediente = ws_SGService.uwsConsultarExpedienteXNumero(gstr_IdExpediente, gstr_IdSociedadGL);
                if ((lds_ConsultarExpediente.Tables["Table"] != null) && (lds_ConsultarExpediente.Tables.Count > 0))
                //&& (!lbool_TieneRP1))
                {
                    ldr_ConsultarExpediente = lds_ConsultarExpediente.Tables["Table"].Rows[0];
                    if (!String.IsNullOrEmpty(ldr_ConsultarExpediente["MonedaPretension"].ToString()))
                    {
                        lbool_TienePretensionInicial = true; // Sí existe Pretension Inicial [ePI]
                    }
                    else
                    {
                        lbool_TienePretensionInicial = false; // No existe Pretension Inicial [nePI]
                    }
                }
                #endregion

                #region existe Liquidación/RF?
                lds_ConsultarResolucion = ws_SGService.uwsConsultarResolucion("", gstr_IdExpediente, gstr_IdSociedadGL, out lstr_Codigo, out lstr_Mensaje);
                if (lstr_Codigo.Contains("00"))
                {
                    for (int i = 0; i < lds_ConsultarResolucion.Tables["Table"].Rows.Count; i++)
                    {
                        ldr_ConsultarResolucion = lds_ConsultarResolucion.Tables["Table"].Rows[i];
                        if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Equals("En Firme"))
                        {
                            lbool_TieneRF = true;
                        }
                        else if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Equals("Liquidacion"))
                        {
                            gbool_TieneLiq = true;
                            gdec_InteresesColAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["InteresesColones"]);
                            gdec_InteresesMoratoriosColAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["InteresesMoratoriosColones"]);
                            gdec_CostasColAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["CostasColones"]);
                            gdec_DannoMoralColAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["DanoMoralColones"]);

                            gint_Periodo = Convert.ToDateTime(ldr_ConsultarResolucion["FchModifica"].ToString()).Year;
                            gint_mes = ldr_ConsultarResolucion["FchModifica2"].ToString() == "" ? DateTime.Today.Month : Convert.ToDateTime(ldr_ConsultarResolucion["FchModifica2"].ToString()).Month;

                            if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["TipoCambio1"].ToString()))//ggarcia
                                gdec_TipoCambioAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["TipoCambio1"].ToString());
                            else
                                gdec_TipoCambioAnterior = 0;
                            gdec_TipoCambioCierre = ldr_ConsultarResolucion["TipoCambioCierre"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["TipoCambioCierre"].ToString());

                            gdec_InteresesAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["Intereses"]);
                            gdec_InteresesMoratoriosAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["InteresesMoratorios"]);
                            gdec_CostasAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["Costas"]);
                            gdec_DannoMoralAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["DanoMoral"]);

                            ldec_InteresesPrevision = ldr_ConsultarResolucion["InteresesAnterior"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["InteresesAnterior"]);
                            ldec_InteresesMoratoriosPrevision = ldr_ConsultarResolucion["InteresesMoratoriosAnterior"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["InteresesMoratoriosAnterior"]);
                            ldec_CostasPrevision = ldr_ConsultarResolucion["CostasAnterior"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["CostasAnterior"]);
                            ldec_DannoMoralPrevision = ldr_ConsultarResolucion["DanoMoralAnterior"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["DanoMoralAnterior"]);

                            ldec_InteresesCierre = ldr_ConsultarResolucion["InteresesColonesCierre"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["InteresesColonesCierre"]);
                            ldec_InteresesMoratoriosCierre = ldr_ConsultarResolucion["InteresesMoratoriosColonesCierre"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["InteresesMoratoriosColonesCierre"]);
                            ldec_CostasCierre = ldr_ConsultarResolucion["CostasColonesCierre"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["CostasColonesCierre"]);
                            ldec_DannoMoralCierre = ldr_ConsultarResolucion["DanoMoralColonesCierre"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["DanoMoralColonesCierre"]);

                            if ((ldec_InteresesPrevision != 0) || (ldec_InteresesMoratoriosPrevision != 0))
                                lbool_Prevision = true;

                            gstr_MonedaAnterior = ldr_ConsultarResolucion["Moneda"].ToString();
                            gstr_ObservacionAnterior = ldr_ConsultarResolucion["Observacion"].ToString();

                        }
                    }

                }
                else
                {
                    lbool_TieneRF = false;
                    gbool_TieneLiq = false;
                }
                #endregion

                if (lbool_TieneRF)
                {

                    garrdec_Montos = new Decimal[15];
                    gint_CantidadLineasAsiento = 20;

                    #region Enviar


                    if (lbool_Incobrable)
                    {
                        #region Incobrable
                        garrdec_Montos = new Decimal[15];
                        gint_CantidadLineasAsiento = 20;

                        if (lbool_Prevision)
                        {
                            // 08/09/2016 preguntar Christian
                            garrdec_Montos[1] = ldec_InteresesPrevision;
                            garrdec_Montos[2] = ldec_InteresesMoratoriosPrevision;
                            garrdec_Montos[3] = ldec_CostasPrevision;
                            garrdec_Montos[4] = ldec_DannoMoralPrevision;

                            // 08/09/2016 si faltase el monto actual

                            garrdec_Montos[6] = ldec_InteresesCierre - ldec_InteresesPrevision;
                            garrdec_Montos[7] = ldec_InteresesMoratoriosCierre - ldec_InteresesMoratoriosPrevision;
                            garrdec_Montos[8] = ldec_CostasCierre - ldec_CostasPrevision;
                            garrdec_Montos[9] = ldec_DannoMoralCierre - ldec_DannoMoralPrevision;

                            ///
                            if ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno)
                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT15", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                            else
                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT14", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);

                        }
                        else
                        {
                            garrdec_Montos[1] = gdec_InteresesColAnterior;
                            garrdec_Montos[2] = gdec_InteresesMoratoriosColAnterior;
                            garrdec_Montos[3] = gdec_CostasColAnterior;
                            garrdec_Montos[4] = gdec_DannoMoralColAnterior;

                            if ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno)
                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT17", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                            else
                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT16", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);

                        }

                        if (lstr_Resultado.Contains("Contabilizado"))
                        {
                            lstr_Resultado = "exito";

                            gstr_EstadoTransaccion = "Vigente";
                            gstr_EstadoResolucion = "Liquidacion";
                            gstr_Estado = gstr_TipoExpediente;
                            string lstr_Moneda = this.DDLMoneda.SelectedItem.Value;

                            if (!String.IsNullOrEmpty(this.txtFechaResolucion.Text))
                            {
                                gstr_PosibleFechaSalida = Convert.ToDateTime(this.txtFechaResolucion.Text).ToString();
                                gdt_PosibleFechaSalida = Convert.ToDateTime(gstr_PosibleFechaSalida);
                            }
                            else
                            {
                                gdt_PosibleFechaSalida = null;
                            }

                            gstr_FechaResolucion = DateTime.Today.ToString();
                            gdt_FechaResolucion = Convert.ToDateTime(gstr_FechaResolucion);

                            // gstr_ObservacionAnterior = gstr_ObservacionAnterior + gstr_Observacion;

                            larrstr_ResultadoLiquidacion = ws_SGService.uwsModificarLiquidacion(
                                gstr_IdExpediente, gstr_IdSociedadGL, gstr_EstadoResolucion,

                                gdt_FechaResolucion,
                                gdt_PosibleFechaSalida,

                                gstr_IdResolucionDictada,
                                gint_CxCaCxP, lstr_Moneda, gdec_TipoCambio,

                                gdec_Intereses, gdec_InteresesColonesLiq,
                                gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones,
                                gdec_Costas, gdec_CostasColones,
                                gdec_DannoMoral, gdec_DannoMoralColones,
                                gstr_Observacion,
                                gstr_Estado,
                                gstr_Usuario,
                                gstr_EstadoProcesal,
                                gstr_TipoTransaccion, gstr_EstadoTransaccion, gstr_Usuario);


                            if (larrstr_ResultadoLiquidacion[0].Contains("00"))
                            {

                              

                             //   ws_SGService.uwsModificarCodigoAsientoCo(lint_IdRes, 0, idResolucion, gstr_IdExpediente, gstr_IdSociedadGL, lstr_CodAsiento, gstr_Usuario);
                                    
                                MessageBox.Show("La creación de liquidación fue satisfactoria.\n" + asiento.getAsientosResultado());
                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucionDictada, lstr_CodAsiento);
                            }
                            else
                            {
                                lstr_Resultado = "fallo";
                                MessageBox.Show("La creación de liquidación no fue satisfactoria.\n" +
                                    larrstr_ResultadoLiquidacion[1].ToString() + "\n");
                            }
                        }
                                            #endregion

                    }
                else if (gbool_TieneLiq)
                {
                    CambioTipoCambioAnterior();
                    gint_CantidadLineasAsiento = 10;

                        #region Diferencia de Montos
                        Int32 lint_ContLineasArriba = 0;
                        Decimal[] larrdec_MontosArriba = new decimal[15];

                        Int32 lint_ContLineasAbajo = 0;
                        Decimal[] larrdec_MontosAbajo = new decimal[15];

                        Int32 lint_ContLineasDiferencialArriba = 0;
                        Decimal[] larrdec_MontosDiferencialArriba = new Decimal[15];

                        Int32 lint_ContLineasDiferencialAbajo = 0;
                        Decimal[] larrdec_MontosDiferencialAbajo = new Decimal[15];

                        if (gdec_TipoCambioCierre == 0)
                            gdec_TipoCambioCierre = gdec_TipoCambio;

                        #region Intereses
                        if (gdec_InteresesColAnterior < gdec_InteresesColonesLiq)
                        {
                            gdec_InteresesAjuste = gdec_InteresesColonesLiq - gdec_InteresesColAnterior;
                            larrdec_MontosArriba[0] = gdec_InteresesAjuste;

                            if ((gint_mes < DateTime.Today.Month) || gbool_CambioMes)
                            {
                                gdec_InteresesAjuste = gdec_Intereses - gdec_InteresesAnterior;
                                gdec_InteresesColAnterior = (gdec_InteresesAjuste * gdec_TipoCambioCierre) - (gdec_InteresesAjuste * gdec_TipoCambioAnterior);

                                if (gdec_InteresesColAnterior < 0)
                                {
                                    gdec_InteresesColAnterior = gdec_InteresesColAnterior * -1;
                                    larrdec_MontosDiferencialAbajo[1] = gdec_InteresesColAnterior;
                                }
                                else
                                    larrdec_MontosDiferencialArriba[1] = gdec_InteresesColAnterior;
                            }
                        }
                        else if (gdec_InteresesColAnterior > gdec_InteresesColonesLiq)
                        {
                            gdec_InteresesAjuste = gdec_InteresesColAnterior - gdec_InteresesColonesLiq;
                            larrdec_MontosAbajo[0] = gdec_InteresesAjuste;

                            if ((gint_mes < DateTime.Today.Month) || gbool_CambioMes)
                            {
                                gdec_InteresesAjuste = gdec_InteresesAnterior - gdec_Intereses;
                                gdec_InteresesColAnterior = (gdec_InteresesAjuste * gdec_TipoCambioCierre) - (gdec_InteresesAjuste * gdec_TipoCambioAnterior);

                                if (gdec_InteresesColAnterior < 0)
                                {
                                    gdec_InteresesColAnterior = gdec_InteresesColAnterior * -1;
                                    larrdec_MontosDiferencialAbajo[1] = gdec_InteresesColAnterior;
                                }
                                else
                                    larrdec_MontosDiferencialArriba[1] = gdec_InteresesColAnterior;
                            }
                        }
                        else
                        {
                            lstr_Resultado = "exito";
                        }
                        #endregion

                        #region  Intereses Moratorios
                        if (gdec_InteresesMoratoriosColAnterior < gdec_InteresesMoratoriosColones)
                        {
                            gdec_InteresesMoratoriosAjuste = gdec_InteresesMoratoriosColones - gdec_InteresesMoratoriosColAnterior;
                            larrdec_MontosArriba[1] = gdec_InteresesMoratoriosAjuste;

                            if ((gint_mes < DateTime.Today.Month) || gbool_CambioMes)
                            {
                                gdec_InteresesMoratoriosAjuste = gdec_InteresesMoratorios - gdec_InteresesMoratoriosAnterior;
                                gdec_InteresesMoratoriosColAnterior = (gdec_InteresesMoratoriosAjuste * gdec_TipoCambioCierre) - (gdec_InteresesMoratoriosAjuste * gdec_TipoCambioAnterior);

                                if (gdec_InteresesMoratoriosColAnterior < 0)
                                {
                                    gdec_InteresesMoratoriosColAnterior = gdec_InteresesMoratoriosColAnterior * -1;
                                    larrdec_MontosDiferencialAbajo[2] = gdec_InteresesMoratoriosColAnterior;
                                }
                                else
                                    larrdec_MontosDiferencialArriba[2] = gdec_InteresesMoratoriosColAnterior;
                            }
                        }
                        else if (gdec_InteresesMoratoriosColAnterior > gdec_InteresesMoratoriosColones)
                        {
                            gdec_InteresesMoratoriosAjuste = gdec_InteresesMoratoriosColAnterior - gdec_InteresesMoratoriosColones;
                            larrdec_MontosAbajo[1] = gdec_InteresesMoratoriosAjuste;

                            if ((gint_mes < DateTime.Today.Month) || gbool_CambioMes)
                            {
                                gdec_InteresesMoratoriosAjuste = gdec_InteresesMoratoriosAnterior - gdec_InteresesMoratorios;
                                gdec_InteresesMoratoriosColAnterior = (gdec_InteresesMoratoriosAjuste * gdec_TipoCambioCierre) - (gdec_InteresesMoratoriosAjuste * gdec_TipoCambioAnterior);

                                if (gdec_InteresesMoratoriosColAnterior < 0)
                                {
                                    gdec_InteresesMoratoriosColAnterior = gdec_InteresesMoratoriosColAnterior * -1;
                                    larrdec_MontosDiferencialAbajo[2] = gdec_InteresesMoratoriosColAnterior;

                                }
                                else
                                    larrdec_MontosDiferencialArriba[2] = gdec_InteresesMoratoriosColAnterior;
                            }
                        }
                        else
                        {
                            lstr_Resultado = "exito";
                        }
                        #endregion

                        #region Costas
                        if (gdec_CostasColAnterior < gdec_CostasColones)
                        {
                            gdec_CostasAjuste = gdec_CostasColones - gdec_CostasColAnterior;
                            larrdec_MontosArriba[2] = gdec_CostasAjuste;

                            if ((gint_mes < DateTime.Today.Month) || gbool_CambioMes)
                            {
                                gdec_CostasAjuste = gdec_Costas - gdec_CostasAnterior;
                                gdec_CostasColAnterior = (gdec_CostasAjuste * gdec_TipoCambioCierre) - (gdec_CostasAjuste * gdec_TipoCambioAnterior);

                                if (gdec_CostasColAnterior < 0)
                                {
                                    gdec_CostasColAnterior = gdec_CostasColAnterior * -1;
                                    larrdec_MontosDiferencialAbajo[3] = gdec_CostasColAnterior;
                                }
                                else
                                    larrdec_MontosDiferencialArriba[3] = gdec_CostasColAnterior;
                            }
                        }
                        else if (gdec_CostasColAnterior > gdec_CostasColones)
                        {
                            gdec_CostasAjuste = gdec_CostasColAnterior - gdec_CostasColones;
                            larrdec_MontosAbajo[2] = gdec_CostasAjuste;

                            if ((gint_mes < DateTime.Today.Month) || gbool_CambioMes)
                            {
                                gdec_CostasAjuste = gdec_CostasAnterior - gdec_Costas;
                                gdec_CostasColAnterior = (gdec_CostasAjuste * gdec_TipoCambioCierre) - (gdec_CostasAjuste * gdec_TipoCambioAnterior);

                                if (gdec_CostasColAnterior < 0)
                                {
                                    gdec_CostasColAnterior = gdec_CostasColAnterior * -1;
                                    larrdec_MontosDiferencialAbajo[3] = gdec_CostasColAnterior;
                                }
                                else
                                    larrdec_MontosDiferencialArriba[3] = gdec_CostasColAnterior;
                            }
                        }
                        else
                        {
                            lstr_Resultado = "exito";
                        }
                        #endregion

                        #region Danno Moral
                        if (gdec_DannoMoralColAnterior < gdec_DannoMoralColones)
                        {
                            gdec_DannoMoralAjuste = gdec_DannoMoralColones - gdec_DannoMoralColAnterior;
                            larrdec_MontosArriba[3] = gdec_DannoMoralAjuste;

                            if ((gint_mes < DateTime.Today.Month) || gbool_CambioMes)
                            {
                                gdec_DannoMoralAjuste = gdec_DannoMoral - gdec_DannoMoralAnterior;
                                gdec_DannoMoralColAnterior = (gdec_DannoMoralAjuste * gdec_TipoCambioCierre) - (gdec_DannoMoralAjuste * gdec_TipoCambioAnterior);

                                if (gdec_DannoMoralColAnterior < 0)
                                {
                                    gdec_DannoMoralColAnterior = gdec_DannoMoralColAnterior * -1;
                                    larrdec_MontosDiferencialAbajo[4] = gdec_DannoMoralColAnterior;
                                }
                                else
                                    larrdec_MontosDiferencialArriba[4] = gdec_DannoMoralColAnterior;
                            }
                        }
                        else if (gdec_DannoMoralColAnterior > gdec_DannoMoralColones)
                        {
                            gdec_DannoMoralAjuste = gdec_DannoMoralColAnterior - gdec_DannoMoralColones;
                            larrdec_MontosAbajo[3] = gdec_DannoMoralAjuste;

                            if ((gint_mes < DateTime.Today.Month) || gbool_CambioMes)
                            {

                                gdec_DannoMoralAjuste = gdec_DannoMoralAnterior - gdec_DannoMoral;
                                gdec_DannoMoralColAnterior = (gdec_DannoMoralAjuste * gdec_TipoCambioCierre) - (gdec_DannoMoralAjuste * gdec_TipoCambioAnterior);

                                if (gdec_DannoMoralColAnterior < 0)
                                {
                                    gdec_DannoMoralColAnterior = gdec_DannoMoralColAnterior * -1;
                                    larrdec_MontosDiferencialAbajo[4] = gdec_DannoMoralColAnterior;
                                }
                                else
                                    larrdec_MontosDiferencialArriba[4] = gdec_DannoMoralColAnterior;
                            }
                        
                        }
                        else
                        {
                            lstr_Resultado = "exito";
                        }
                        #endregion

                        #endregion

                        glbool_cambioMonto = false;

                        Boolean lbool_arriba = false;
                        Boolean lbool_abajo = false;
                        Boolean lbool_dif_arriba = false;
                        Boolean lbool_dif_abajo = false;

                        for (int i = 0; i < larrdec_MontosArriba.Count(); i++)
                            if (larrdec_MontosArriba[i] > 0)
                                lbool_arriba = true;

                        for (int j = 0; j < larrdec_MontosAbajo.Count(); j++)
                            if (larrdec_MontosAbajo[j] > 0)
                                lbool_abajo = true;

                        for (int i = 0; i < larrdec_MontosDiferencialArriba.Count(); i++)
                            if (larrdec_MontosDiferencialArriba[i] > 0)
                                lbool_dif_arriba = true;

                        for (int j = 0; j < larrdec_MontosDiferencialAbajo.Count(); j++)
                            if (larrdec_MontosDiferencialAbajo[j] > 0)
                                lbool_dif_abajo = true;

                        if (gstr_TipoExpediente.Equals("Demandado"))
                        {
                            gstr_TipoTransaccion = "Cobro";
                            gint_CxCaCxP = 1;

                            if (lbool_abajo)
                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT99", gstr_Transaccion, glbool_cambioMonto, larrdec_MontosAbajo, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                            
                            if (lbool_arriba)
                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT30", gstr_Transaccion, glbool_cambioMonto, larrdec_MontosArriba, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                            
                            if ((gint_mes < DateTime.Today.Month) || gbool_CambioMes)
                            {
                                if (lbool_dif_arriba)
                                {
                                    String CT = ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) ? "CT86" : "CT85";
                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, CT, gstr_Transaccion, glbool_cambioMonto, larrdec_MontosDiferencialArriba, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                                }


                                if (lbool_dif_abajo)
                                {
                                    String CT = ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) ? "CT88" : "CT87";
                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, CT, gstr_Transaccion, glbool_cambioMonto, larrdec_MontosDiferencialAbajo, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                                }
                                    
                            }
                        }
                        else
                        {
                            gstr_TipoTransaccion = "Pago";
                            gint_CxCaCxP = 0;

                            if (lbool_abajo)
                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT101", gstr_Transaccion, glbool_cambioMonto, larrdec_MontosAbajo, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);

                            if (lbool_arriba)
                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT32", gstr_Transaccion, glbool_cambioMonto, larrdec_MontosArriba, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                            
                            if ((gint_mes < DateTime.Today.Month) || gbool_CambioMes)
                            {
                                if (lbool_dif_arriba)
                                {
                                    String CT = ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) ? "CT90" : "CT89";
                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, CT, gstr_Transaccion, glbool_cambioMonto, larrdec_MontosDiferencialArriba, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                                }
                                    

                                if (lbool_dif_abajo)
                                {
                                    String CT = ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) ? "CT92" : "CT91";
                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, CT, gstr_Transaccion, glbool_cambioMonto, larrdec_MontosDiferencialAbajo, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                                }
                                    
                            }
                        }

                        #region registra
                        if (lstr_Resultado.Equals("Contabilizado") || lstr_Resultado.Equals("exito"))//ggarcia, agrego exito para casos que no generan asiento
                        {

                            lstr_Resultado = "exito";


                            gstr_EstadoTransaccion = "Vigente";
                            gstr_EstadoResolucion = "Liquidacion";
                            gstr_Estado = gstr_TipoExpediente;
                            string lstr_Moneda = this.DDLMoneda.SelectedItem.Value;



                            if (!String.IsNullOrEmpty(this.txtFechaResolucion.Text))
                            {
                                gstr_PosibleFechaSalida = Convert.ToDateTime(this.txtFechaResolucion.Text).ToString();
                                gdt_PosibleFechaSalida = Convert.ToDateTime(gstr_PosibleFechaSalida);
                                gstr_FechaResolucion = gstr_PosibleFechaSalida;//ggarcia
                                gdt_FechaResolucion = gdt_PosibleFechaSalida;//ggarcia
                            }
                            else
                            {
                                gdt_PosibleFechaSalida = null;
                                gstr_FechaResolucion = DateTime.Today.ToString();//ggarcia
                                gdt_FechaResolucion = Convert.ToDateTime(gstr_FechaResolucion);//ggarcia
                            }
                            //ggarcia, se comenta y se pasa para arriba
                            //gstr_FechaResolucion = DateTime.Today.ToString();
                            //gdt_FechaResolucion = Convert.ToDateTime(gstr_FechaResolucion);

                            // gstr_ObservacionAnterior = gstr_ObservacionAnterior + gstr_Observacion;

                            larrstr_ResultadoLiquidacion = ws_SGService.uwsModificarLiquidacion(
                                gstr_IdExpediente, gstr_IdSociedadGL, gstr_EstadoResolucion,

                                gdt_FechaResolucion,
                                gdt_PosibleFechaSalida,

                                gstr_IdResolucionDictada,
                                gint_CxCaCxP, lstr_Moneda, gdec_TipoCambio,

                                gdec_Intereses, gdec_InteresesColonesLiq,
                                gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones,
                                gdec_Costas, gdec_CostasColones,
                                gdec_DannoMoral, gdec_DannoMoralColones,
                                gstr_Observacion,
                                gstr_Estado,
                                gstr_Usuario,
                                gstr_EstadoProcesal,
                                gstr_TipoTransaccion, gstr_EstadoTransaccion, gstr_Usuario);


                            if (larrstr_ResultadoLiquidacion[0].Contains("00"))
                            {
                                MessageBox.Show("La creación de liquidación fue satisfactoria.\n" + asiento.getAsientosResultado());
                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucionDictada, lstr_CodAsiento);
                            }
                            else
                            {
                                lstr_Resultado = "fallo";
                                MessageBox.Show("La creación de liquidación no fue satisfactoria.\n" +
                                    larrstr_ResultadoLiquidacion[1].ToString() + "\n");
                            }
                        }
                        #endregion

                    }
                    else
                    {
                        #region simple
                        garrdec_Montos = new decimal[15];
                        garrdec_Montos[0] = gdec_InteresesColonesLiq;
                        garrdec_Montos[1] = gdec_InteresesMoratoriosColones;
                        garrdec_Montos[2] = gdec_CostasColones;
                        garrdec_Montos[3] = gdec_DannoMoralColones;

                        gint_CantidadLineasAsiento = 8;
                        glbool_cambioMonto = false;

                        if (gstr_TipoExpediente.Equals("Demandado"))
                        {
                            gstr_TipoTransaccion = "Cobro";
                            gint_CxCaCxP = 1;
                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT30", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                        }
                        else
                        {
                            gstr_TipoTransaccion = "Pago";
                            gint_CxCaCxP = 0;
                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT32", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, 0, out lstr_CodAsiento);
                        }
                        #endregion


                        #region registra
                        if (lstr_Resultado.Equals("Contabilizado"))
                        {


                            gstr_EstadoTransaccion = "Vigente";
                            gstr_EstadoResolucion = "Liquidacion";
                            gstr_Estado = gstr_TipoExpediente;

                            string lstr_Moneda = this.DDLMoneda.SelectedItem.Value;

                            if (!String.IsNullOrEmpty(this.txtFechaResolucion.Text))
                            {
                                gstr_PosibleFechaSalida = Convert.ToDateTime(this.txtFechaResolucion.Text).ToString();
                                gdt_PosibleFechaSalida = Convert.ToDateTime(gstr_PosibleFechaSalida);
                            }
                            else
                            {
                                gdt_PosibleFechaSalida = null;
                            }

                            gstr_FechaResolucion = DateTime.Today.ToString();
                            gdt_FechaResolucion = Convert.ToDateTime(gstr_FechaResolucion);

                            larrstr_ResultadoLiquidacion = ws_SGService.uwsRegistrarLiquidacion(
                                gstr_IdExpediente, gstr_IdSociedadGL, gstr_EstadoResolucion,

                                gdt_FechaResolucion,
                                gdt_PosibleFechaSalida,
                                gstr_IdResolucionDictada,

                                gint_CxCaCxP, gstr_EstadoProcesal,
                                gstr_Estado, lstr_Moneda, gdec_TipoCambio,

                                gdec_Intereses, gdec_InteresesColonesLiq,
                                gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones,
                                gdec_Costas, gdec_CostasColones,
                                gdec_DannoMoral, gdec_DannoMoralColones,

                                gstr_TipoTransaccion, gstr_EstadoTransaccion,
                                gstr_Observacion,
                                gstr_Usuario);

                            if (larrstr_ResultadoLiquidacion[0].Contains("00"))
                            {
                                MessageBox.Show("La creación de liquidación fue satisfactoria.\n" + asiento.getAsientosResultado());

                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucionDictada, lstr_CodAsiento);

                                lstr_Resultado = "exito";
                            }
                            else
                            {
                                lstr_Resultado = "fallo";
                                MessageBox.Show("La creación de liquidación no fue satisfactoria.\n" +
                                    larrstr_ResultadoLiquidacion[1].ToString() + "\n");
                            }
                        }
                        #endregion
                        else
                        {
                            MessageBox.Show("La creación de liquidación no fue satisfactoria.\n" + lstr_Resultado);
                        }
                    }

                    #endregion
                }
                else
                {
                    MessageBox.Show("Para registrar la liquidación el expediente debe tener una resolución en firme registrada. \n");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("La creación de la liquidación no fue satisfactoria. \n" + e.Message);
            }
            #region old
            // string[] result_save = new string[3];
           // string fechaR;
           // idResolucion = txtNumResol.Text; //this.ddlIdExpediente.SelectedItem.Value;
           // IdExpediente = this.ddlIdExpediente.SelectedItem.Value;
           // string str_resultado = string.Empty;
           // string idModulo = "IdModulo In ('CT')";
           //// EstadoTransaccion = "En proceso";
           // TipoTransaccion = this.lblEstadoResolucion.Text;
           //  fechaR= this.txtFechaResolucion.Text;
           //  FechaResolucion = null;
           //  try
           //  {
           //      FechaResolucion = Convert.ToDateTime(fechaR);
           //  }
           //  catch 
           //  { 
           //  }
           // PosibleFecSalidaRec = FechaResolucion;
           // MontoPosibleReembolso = 0;
           // MontoPrincipal = 0;
           // MontoIntereses = 0;
           // MontoInteresesColones = 0;
           // MontoPrincipalColones = 0;//tipocambio*montoprincipal
           // ValorPresenteIntColones = 0;//Formula: VP= VF/(1+i)n
           // ValorPresentePrincipal = 0;//Formula: VP= VF/(1+i)n
           // ValorPresenteIntereses = 0;
           // MontoIntereses = this.txtIntereses.Text==""?0:Convert.ToDecimal(this.txtIntereses.Text);
           // InteresesMoratorios = this.txtInteresesMoratorios.Text==""? 0 : Convert.ToDecimal(this.txtInteresesMoratorios.Text);
           // InteresesMoratoriosColones =this.txtInteresesMoratoriosColones.Text==""?0:decimal.Parse(this.txtInteresesMoratoriosColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
           // MontoInteresesColones =this.txtInteresesColones.Text==""?0: decimal.Parse(this.txtInteresesColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
           // Costas = this.txtCostas.Text==""?0:Convert.ToDecimal(this.txtCostas.Text);
           // CostasColones = this.txtCostasColones.Text==""?0:decimal.Parse(this.txtCostasColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
           // DAnnoMoral = this.txtDannoMoral.Text==""?0:Convert.ToDecimal(this.txtDannoMoral.Text);
           // DAnnoMoralColones =this.txtDannoMoralColones.Text==""? 0: decimal.Parse(this.txtDannoMoralColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
           // Observacion = "Es una Liquidación de Resolución en Firme dictada";
           // userCrea = clsSesion.Current.LoginUsuario;
           // Moneda = this.DDLMoneda.SelectedItem.Value;
           // EstadoResolucion = this.lblEstadoResolucion.Text;
           // //Moneda = this.DDLMoneda.SelectedItem.Value; 
           // UsrModifica = clsSesion.Current.LoginUsuario;//usuaario loggeado va aqui
           // Observaciones = this.CKEditorObservaciones.Text;

           // if (ConsultarTipoExpediente(IdExpediente).Equals("Actor"))
           // {
           //         //Disparar asiento de Liqudiacion
           //     str_resultado = asiento.enviar(idModulo, "CT32", IdExpediente, MontoInteresesColones, InteresesMoratoriosColones, CostasColones, DAnnoMoralColones);
           //     MessageBox.Show("El expediente " + IdExpediente + " generará el registro de la cuenta por cobrar.");
                
           // }else if (ConsultarTipoExpediente(IdExpediente).Equals("Demandado")){

           //     //Disparar asiento de Liqudiacion
           //     str_resultado = asiento.enviar(idModulo, "CT30", IdExpediente, MontoInteresesColones, InteresesMoratoriosColones, CostasColones, DAnnoMoralColones);
           //     MessageBox.Show("El expediente " + IdExpediente + " generará el registro de la cuenta por pagar.");
           // }
           //     //Insertamos en BD
           // result_save = ws_SGService.uwsRegistrarCobrosPagos(idResolucion, IdExpediente, Moneda,
           //                             TipoCambio, MontoPrincipal, MontoIntereses, InteresesMoratorios,
           //                             InteresesMoratoriosColones, MontoInteresesColones, MontoPrincipalColones, ValorPresenteIntColones,
           //                             ValorPresentePrincipal, ValorPresenteIntereses, ValorPresentePrinColones, Costas, CostasColones, DAnnoMoral, DAnnoMoralColones, TipoTransaccion, EstadoTransaccion, FechaResolucion, Observaciones, userCrea);
                
           // //Validamos resultado para desplegar mensaje al usuario
           // if (result_save[0].Contains("00"))
           // {

           //     MessageBox.Show("Se ingreso satisfactoriamente la liquidación para el número '" + idResolucion + "' de resolución.");
           // }
           // else if (result_save[0].Contains("99") || result_save[0].Contains("Codigo:-") || result_save[0].Contains("Codigo:00-") || result_save[0].Contains("Codigo:") || result_save[0].Contains("Codigo :-6"))
           // {
           //     MessageBox.Show("Error en el proceso de inserción de la liquidación de la resolución, no se pudo registrar la nueva resolución.");
           // }
            // LimpiarCampos();
            #endregion
        }

        private string[] CargarIndicadoresEco()
        {
            DataSet resultCompraUSD = new DataSet();
            DataSet resultVentaUSD = new DataSet();
            DataSet resultCompraEU = new DataSet();
            DataSet resultVentaEU = new DataSet();
            DataSet resultTBP = new DataSet();
            string[] resultado = new string[4];
            //invoacion a servicios
            resultCompraUSD = ws_SGService.uwsConsultarTiposCambio("CRCN", DateTime.Today, "3280", "N");//compra antes: 317
            resultVentaUSD = ws_SGService.uwsConsultarTiposCambio("CRCN", DateTime.Today, "3140", "N");//venta antes: 318
            resultCompraEU = ws_SGService.uwsConsultarTiposCambio("EUR", DateTime.Now, "333", "N");//euro
            resultTBP = ws_SGService.uwsConsultarValoresIndicadoresEco("TBP", DateTime.Now, "N");//TBP
            //seteo de resultados en array de strings
            ////DataSet1.Tables["Tu_Tabla"].Rows[0]["Nombre_Columna_1"].ToString(); obetenemos los datos del dataset de resultado
            resultado[0] = String.Format("{0:0.00}", resultCompraUSD.Tables[0].Rows.Count > 0 ? resultCompraUSD.Tables[0].Rows[0]["Valor"] : "00.00"); //condition ? first_expression : second_expression;
            resultado[1] = String.Format("{0:0.00}", resultVentaUSD.Tables[0].Rows.Count > 0 ? resultVentaUSD.Tables[0].Rows[0]["Valor"] : "00.00");
            resultado[2] = String.Format("{0:0.00}", resultCompraEU.Tables[0].Rows.Count > 0 ? resultCompraEU.Tables[0].Rows[0]["Valor"] : "00.00");
            resultado[3] = String.Format("{0:0.00}", resultTBP.Tables[0].Rows.Count > 0 ? resultTBP.Tables[0].Rows[0]["Valor"] : "00.00");
            return resultado;

        }
        
        private DataTable GetData(string lstr_query)
        {
            /*string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(lstr_ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = lstr_query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }*/
            DataSet ds = new DataSet();
            ds = ws_SGService.uwsConsultarDinamico(lstr_query);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables["Table"];
            }
            return null;
        }

        private void guardarPrevision(Decimal IdRes, String IdExp,int Dias,float Monto,float total,float porcentaje,String usuario)
        {
            string[] str_resul = ws_SGService.uwsCrearAntiguedadDeSaldos(null, null, IdRes.ToString(), IdExp, null, Dias, null, Convert.ToDecimal( Monto), Convert.ToDecimal( total), Convert.ToDecimal( porcentaje), null, usuario);
           /* String query = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
                            "BEGIN TRANSACTION;" +

                            "UPDATE [co].[AntiguedadDeSaldos]" +
                               "SET [DiasDeCuenta] = " + Dias +
                                  ",[MontoIncobrable] = " + Monto +
                                  ",[DiferenciaAjustar] = " + total +
                                  ",[PorcentajeIncobrable] = " + porcentaje +
                                  ",[UsrModificacion] = '" + usuario + "'" +
                                  ",[FchModificacion] = getdate()" +
                             " WHERE [IdResolucion] = '" + IdRes + "' and [IdExpediente] = '" + IdExp + "'" +

                            "IF @@ROWCOUNT = 0" +
                            "BEGIN " +
                              "INSERT INTO [co].[AntiguedadDeSaldos] " +
                                       "([IdResolucion]" +
                                       ",[IdExpediente]" +
                                       ",[DiasDeCuenta]" +
                                       ",[MontoIncobrable]" +
                                       ",[DiferenciaAjustar]" +
                                       ",[PorcentajeIncobrable]" +
                                       ",[UsrCreacion]" +
                                       ",[FchCreacion]" +
                                       ",[UsrModificacion]" +
                                       ",[FchModificacion])" +
                                 "VALUES" +
                                       "('" + IdRes + "','" + IdExp + "'," + Dias + "," + Monto + "," + total + "," + porcentaje + ",'" + usuario + "',getdate(),'" + usuario + "',getdate())" +
                            "END " +
                            "COMMIT TRANSACTION;";
            
            string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(lstr_ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        
                    }
                    //cmd.ExecuteNonQuery(); 
                }
            }
             */
        }
        
        private void CargarLiquidacion(string idExpediente)
        {
            LimpiarCampos();
            CargarMonedas();
            
            string lstr_query =
                "SELECT * FROM co.Expedientes exp " +
                "INNER JOIN co.Resoluciones res " +
                "ON exp.IdExp = res.IdExp " +
                "LEFT OUTER JOIN  co.CobrosPagos cp " + //"INNER JOIN  co.CobrosPagos cp " +  GGARCIA CAMBIO A LEFT OUTER PORQUE NO MOSTRABA
                "ON res.IdRes = cp.IdRes " +
                "WHERE exp.IdExpediente ='" + idExpediente + "' " +
                "AND exp.IdSociedadGL ='" + gstr_IdSociedadGL + "' " +
                "AND res.EstadoResolucion = 'Liquidacion' " +
                "AND exp.EstadoExpediente = 'Activo'";

            DataTable dt_Resoluciones = GetData(lstr_query);
            
            if (dt_Resoluciones.Rows.Count > 0)
            {
                //MessageBox.Show("El Expediente " + idExpediente + " ya posee una << Liquidación >> , esta será cargada para su modificación.");
                //this.btnModificar.Visible = true;
                //this.btnGuardar.Visible = false;
                foreach (DataRow dr_Resolucion in dt_Resoluciones.Rows)
                {
                    if (dr_Resolucion["EstadoResolucion"].ToString().Equals("Liquidacion"))
                    {
                        this.ddlIdExpediente.SelectedValue = dr_Resolucion["IdExpedienteFK"].ToString();
                        this.txtNumResol.Text = dr_Resolucion["IdResolucion"].ToString();
                        this.txtFechaResolucion.Text = dr_Resolucion["FechResolucion"].ToString();//["FechFalloResol"].ToString();//ggarcia
                        
                        if (!String.IsNullOrEmpty(dr_Resolucion["PosibleFechSalidaRecursos"].ToString()))
                        {
                            this.txtFechaResolucion.Text = ((DateTime)dr_Resolucion["PosibleFechSalidaRecursos"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        
                        this.DDLMoneda.SelectedItem.Value = "0";
                        this.DDLMoneda.SelectedValue = dr_Resolucion["Moneda"].ToString();
                        if (!string.IsNullOrEmpty(dr_Resolucion["Intereses"].ToString()))
                        this.txtIntereses.Text = Convert.ToDecimal(dr_Resolucion["Intereses"].ToString()).ToString("N2");
                        this.txtInteresesColones.Text = dr_Resolucion["InteresesColones"].ToString();
                        if (!string.IsNullOrEmpty(dr_Resolucion["Costas"].ToString()))
                            this.txtCostas.Text = Convert.ToDecimal(dr_Resolucion["Costas"].ToString()).ToString("N2");
                        if (!string.IsNullOrEmpty(dr_Resolucion["CostasColones"].ToString()))
                            this.txtCostasColones.Text = Convert.ToDecimal(dr_Resolucion["CostasColones"].ToString()).ToString("N2");
                        if (!string.IsNullOrEmpty(dr_Resolucion["InteresesMoratoriosColones"].ToString()))
                            this.txtInteresesMoratoriosColones.Text = Convert.ToDecimal(dr_Resolucion["InteresesMoratoriosColones"].ToString()).ToString("N2");
                        if (!string.IsNullOrEmpty(dr_Resolucion["InteresesMoratorios"].ToString()))
                            this.txtInteresesMoratorios.Text = Convert.ToDecimal(dr_Resolucion["InteresesMoratorios"].ToString()).ToString("N2");
                        if (!string.IsNullOrEmpty(dr_Resolucion["DanoMoral"].ToString()))
                        this.txtDannoMoral.Text = Convert.ToDecimal(dr_Resolucion["DanoMoral"].ToString()).ToString("N2");
                        this.txtDannoMoralColones.Text = dr_Resolucion["DanoMoralColones"].ToString();
                        this.CKEditorObservaciones.Text = dr_Resolucion["Observacion"].ToString();

                        if (dr_Resolucion["TipoExpediente"].ToString().Equals("Actor"))
                        {
                            this.ckbIncobrable.Visible = true;
                            this.Label1.Visible = true;
                        }
                        else
                        {
                            this.ckbIncobrable.Visible = false;
                            this.Label1.Visible = false;
                        }

                        ActualizaMontos();
                    }

                   
                    //this.upDatosPrincipales.Update();
                }

            }
            else
            {

                //MessageBox.Show("El Expediente no posee una liquidaci'on ingresada.");
            }

            DataSet lds_Archivos = ws_SGService.uwsObtenerArchivoPorIdResolucion(gstr_IdExpediente, gstr_IdSociedadGL, ConsultarIdExpediente(gstr_IdExpediente));

            if (lds_Archivos.Tables.Count > 0)
            {
                gvFiles.DataSource = lds_Archivos;
                gvFiles.DataBind();
            }
            else  { }

        }

        private void CargarMonedas()
        {
            string str_consul = "SELECT IdMoneda,IdMoneda+' - '+NomMoneda as Nombre FROM [ma].[Monedas] WHERE IdMoneda IN ('USD', 'CRC' ,'EUR')";
            DataTable exped = GetData(str_consul);

            this.DDLMoneda.Items.Clear();

            if (exped.Rows.Count > 0 && (DDLMoneda.Items.Count == 0))
            {
                this.DDLMoneda.Items.Add(new ListItem("---- Elegir Moneda ----", "0"));//Insert(0, "--Seleccione--");
                DataRow campo = exped.Rows[0];
                this.DDLMoneda.DataSource = exped;
                this.DDLMoneda.DataTextField = "Nombre";
                this.DDLMoneda.DataValueField = "IdMoneda";
                this.DDLMoneda.DataBind();
                for (int i = 0; i < DDLMoneda.Items.Count; i++)
                {
                    DDLMoneda.Items[i].Value = DDLMoneda.Items[i].Value.Trim();
                }
            }
        }

        private string[] ModificarLiquidacionData(string idexpediente, string idresolucion)
        {
            
            string[] resultModificar = new string[2];
            resultModificar = ws_SGService.uwsModificarLiquidacion(idexpediente, gstr_IdSociedadGL, "", DateTime.Parse( this.txtFechaResolucion.Text), DateTime.Parse(this.txtFechaResolucion.Text), "",
                0, this.DDLMoneda.SelectedItem.Value, TipoCambio, Convert.ToDecimal(this.txtIntereses.Text), decimal.Parse(this.txtInteresesColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number),
                Convert.ToDecimal(this.txtInteresesMoratorios.Text), decimal.Parse(this.txtInteresesMoratoriosColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number),
                Convert.ToDecimal(this.txtCostas.Text), decimal.Parse(this.txtCostasColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number),
                Convert.ToDecimal(this.txtDannoMoral.Text), decimal.Parse(this.txtDannoMoralColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number),
                this.CKEditorObservaciones.Text, "", (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario,
                "", "", "", (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
            MessageBox.Show("Liquidación modificada satisfactoriamente.");
            /*
            string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
            //string fechResolucion = this.calFechaResolucion.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //string fechSalidaRecur = this.calFechSalidaRecur.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);


            using (SqlConnection con = new SqlConnection(lstr_ConnString))
            {
                con.Open();
                using (SqlCommand StoredProcedureCommand = new SqlCommand("co.uspModificarLiquidacion", con))
                {
                    StoredProcedureCommand.CommandType = CommandType.StoredProcedure;
                    StoredProcedureCommand.Parameters.AddWithValue("@pIdResolucion", idresolucion);
                    StoredProcedureCommand.Parameters.AddWithValue("@pIdExpedienteFK", idexpediente);
                    StoredProcedureCommand.Parameters.AddWithValue("@pMoneda", this.DDLMoneda.SelectedItem.Value);
                    StoredProcedureCommand.Parameters.AddWithValue("@pTipoCambio", TipoCambio);
                    StoredProcedureCommand.Parameters.AddWithValue("@pMontoIntereses", Convert.ToDecimal(this.txtIntereses.Text));
                    StoredProcedureCommand.Parameters.AddWithValue("@pMontoInteresesColones", decimal.Parse(this.txtInteresesColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
                    StoredProcedureCommand.Parameters.AddWithValue("@pInteresesMoratorios", Convert.ToDecimal(this.txtInteresesMoratorios.Text));
                    StoredProcedureCommand.Parameters.AddWithValue("@pInteresesMoratoriosColones", decimal.Parse(this.txtInteresesMoratoriosColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
                    StoredProcedureCommand.Parameters.AddWithValue("@pCostas", Convert.ToDecimal(this.txtCostas.Text));
                    StoredProcedureCommand.Parameters.AddWithValue("@pCostasColones", decimal.Parse(this.txtCostasColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
                    StoredProcedureCommand.Parameters.AddWithValue("@pDanoMoral", Convert.ToDecimal(this.txtDannoMoral.Text));
                    StoredProcedureCommand.Parameters.AddWithValue("@pDanoMoralColones", decimal.Parse(this.txtDannoMoralColones.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
                    StoredProcedureCommand.Parameters.AddWithValue("@pFechFalloResol", this.txtFechaResolucion.Text);
                    StoredProcedureCommand.Parameters.AddWithValue("@pObservaciones", this.CKEditorObservaciones.Text);
                    StoredProcedureCommand.Parameters.AddWithValue("@pUsrModifica", (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
                       
                    StoredProcedureCommand.Parameters.Add("@pResultado", SqlDbType.VarChar, 2);
                    StoredProcedureCommand.Parameters["@pResultado"].Direction = ParameterDirection.Output;
                    StoredProcedureCommand.Parameters.Add("@pMensaje", SqlDbType.VarChar, 500);
                    StoredProcedureCommand.Parameters["@pMensaje"].Direction = ParameterDirection.Output;

                    StoredProcedureCommand.Connection = con;
                    StoredProcedureCommand.ExecuteNonQuery();

                    string Codigo = (string)StoredProcedureCommand.Parameters["@pResultado"].Value;
                    string Resultado = (string)StoredProcedureCommand.Parameters["@pMensaje"].Value;
                    resultModificar[0] = Codigo;
                    resultModificar[1] = Resultado;
                    //string strMsg = "Se modifico satisfactoriamente la resolucion.";
                    //Response.Write("<script>alert('"+strMsg +"')</script>");
                    MessageBox.Show("Liquidación modificada satisfactoriamente.");
                    con.Close();
                }
            }*/

            return resultModificar;
        }
        
        protected void btnGenerarAsientos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El sistema no posee, parámetros válidos de cuentas, para enviar asientos a SIG@F.Configurelos en el Módulo de Mantenimiento previamente.");
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string lstr_idexpediente;
            string lstr_idresolucion=this.txtNumResol.Text;
            
            if(this.lblNumExpediente.Text!="")
            {
                lstr_idexpediente = this.lblNumExpediente.Text;
            }
            else
            {
                lstr_idexpediente=this.ddlIdExpediente.SelectedValue;
            }
            
            
            ModificarLiquidacionData(lstr_idexpediente, lstr_idresolucion);
        }

        protected void btnSubirArhivo_Click(object sender, EventArgs e)
        {
            Request.ContentType = "multipart/form-data";
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string expedienteid = this.ddlIdExpediente.SelectedValue;
            foreach (string fileTagName in files)
            {
                HttpPostedFile file = Request.Files[fileTagName];
                if (file.ContentLength > 0)
                {
                    // Due to the limit of the max for a int type, the largest file can be
                    // uploaded is 2147483647, which is very large anyway.
                    int lint_tamano = file.ContentLength;
                    
                    string lstr_nombre = file.FileName;
                    int position = lstr_nombre.LastIndexOf("\\");
                    lstr_nombre = lstr_nombre.Substring(position + 1);
                    string tipoContenido = file.ContentType;
                    byte[] archivoDato = new byte[lint_tamano];
                    file.InputStream.Read(archivoDato, 0, lint_tamano);
                    if (this.lblNumExpediente.Text != "")
                    {
                        expedienteid = this.lblNumExpediente.Text;
                    }
                    else if (this.ddlIdExpediente.SelectedValue != "0")
                    {
                        expedienteid = this.ddlIdExpediente.SelectedValue;
                    }

                               
                        ws_SGService.uwsGuardarArchivoContingente(lstr_nombre, tipoContenido, lint_tamano, archivoDato, 0, "Liquidacion", gstr_IdSociedadGL, ConsultarIdExpediente(expedienteid),expedienteid,0, "", 0, (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
                          //  ws_SG.uwsGuardarArchivoContingente(lstr_nombre, tipoContenido, lint_tamano, archivoDato, 0, this.ddlEstadoResol.SelectedValue, gstr_Sociedad, 0, gstr_IdExpediente, 0, "", 0, (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
                    
                    MessageBox.Show("Se adjunto un nuevo archivo a la resolución <<" + expedienteid + ">>");

                }
            }

            //string archivosDato = "Select * from rn.Archivos where IdCobroPago='" + expedienteid + "'";
            //DataTable listaArchivos = GetData(archivosDato);

            DataSet lds_Archivos = ws_SGService.uwsObtenerArchivoPorIdResolucion(gstr_IdExpediente, gstr_IdSociedadGL, ConsultarIdExpediente(gstr_IdExpediente));
            if (lds_Archivos.Tables.Count > 0)
            {

                gvFiles.DataSource = lds_Archivos;
                gvFiles.DataBind();
            }
                   
        }

        protected void gvFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string[] lstr_ResEliminacion = new string[2];

            if (gvFiles.Rows.Count > 0)
            {

                string str_idArchivo = e.Keys["IdArchivo"].ToString();
                string str_idExp = string.Empty;// this.txtResolucionNum.Text;
                int int_indice = Convert.ToInt32(e.RowIndex);
                Label lblFchModificacion = (Label)gvFiles.Rows[int_indice].Cells[4].FindControl("lblFchModifica");
                LinkButton lblIdArchivo = (LinkButton)gvFiles.Rows[int_indice].Cells[0].FindControl("lnkEliminar");
                //string lblIdArchivo = lblIdArchivo.
                //DateTime lstr_FechaModificado = DateTime.Now.Date;
                string lstr_fecha = String.Empty;
                lstr_fecha = Convert.ToDateTime(lblFchModificacion.Text).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                lstr_ResEliminacion = ws_SGService.uwsEliminarArchivo(str_idArchivo, lstr_fecha);
                if (lstr_ResEliminacion[0] == "00")//Request.QueryString["Rev"] != null && 
                {
                    //    string lst_IdRevelacion = Request.QueryString["Rev"];

                    if (this.lblNumExpediente.Text != "")
                    {
                        str_idExp = this.lblNumExpediente.Text;
                    }
                    else if (this.ddlIdExpediente.SelectedValue != "0")
                    {
                        str_idExp = this.ddlIdExpediente.SelectedValue;
                    }
                    //string archivosDato = "Select * from rn.Archivos where IdCobroPago='" + str_idExp + "'";
                    //DataTable listaArchivos = GetData(archivosDato);

                    DataSet lds_Archivos = ws_SGService.uwsObtenerArchivoPorIdResolucion(gstr_IdExpediente, gstr_IdSociedadGL, ConsultarIdExpediente(gstr_IdExpediente));
                    if (lds_Archivos.Tables.Count > 0)
                    {

                        gvFiles.DataSource = lds_Archivos;
                        gvFiles.DataBind();
                    }
                    else
                    {
                        gvFiles.DataSource = null;
                        gvFiles.DataBind();
                    }
                    MessageBox.Show("El archivo fue eliminado satisfactoriamente.");


                }
                else
                {

                }
            }
          
        }

        private void CargarExpedientes()
        {

            string str_consul = "SELECT IdExpediente,IdExpediente+'-'+TipoExpediente AS NomExpediente FROM co.Expedientes where co.Expedientes.EstadoExpediente='Activo' and IdSociedadGL='" + gstr_IdSociedadGL + "'";
            DataTable exped = GetData(str_consul);
            this.ddlIdExpediente.Items.Clear();
            if (exped.Rows.Count > 0) {
            DataRow campo = exped.Rows[0];
            this.ddlIdExpediente.DataSource = exped;
            this.ddlIdExpediente.DataTextField = "NomExpediente";
            this.ddlIdExpediente.DataValueField = "IdExpediente";
            this.ddlIdExpediente.Items.Insert(0, new ListItem("--- Elegir Expediente---", "0"));
            this.ddlIdExpediente.DataBind();
            }
        }
        
        private void LimpiarCampos()
        {
            this.txtInteresesMoratoriosColones.Text = "";
            this.txtCostas.Text = "";
            this.txtCostasColones.Text = "";
            this.txtDannoMoral.Text = "";
            this.txtIntereses.Text = "";
            this.txtDannoMoralColones.Text = "";
            this.txtInteresesColones.Text = "";
            this.txtInteresesMoratorios.Text = "";
           // this.DDLMoneda.SelectedItem.Value="0";
            this.txtNumResol.Text = "";
            this.CKEditorObservaciones.Text = "";
            this.txtFechaResolucion.Text = "";
            gvFiles.DataSource = null;
            gvFiles.DataBind();
        }

        private string ConsultarTipoExpediente(string idexpediente)
        {
            string str_consul = "Select TipoExpediente from co.Expedientes where IdExpediente='" + idexpediente + "' and IdSociedadGL='" + gstr_IdSociedadGL + "' AND EstadoExpediente = 'Activo'";
            string tipoExp = string.Empty;
            //Consultar Expedientes
            DataTable exped = GetData(str_consul);
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                tipoExp = campo["TipoExpediente"].ToString();
            }
            else
            {

                tipoExp = "No hay valores encontrados";
            }

            return tipoExp;
        }

        private DataSet ConsultarTiposAsientos(string str_modulo, string str_operacion, string str_tipoProcesoExpediente)
        {
            DataSet ds;
            string consult = "SELECT IdSociedadFi from ma.SociedadesFinancieras where IdSociedadGL='" + gstr_IdSociedadGL + "'";
            DataTable dt2 = GetData(consult);
            DataRow campo = null;
            string sociedadFi = string.Empty;
            if (dt2.Rows.Count > 0)
            {
                campo = dt2.Rows[0];
                sociedadFi = campo["IdSociedadFi"].ToString();
            }

            ds = ws_SGService.uwsConsultarTiposAsiento(sociedadFi, str_modulo, str_operacion, "", "", "CRC", str_tipoProcesoExpediente, null, null);

            return ds;
        }
        
        private string ConsultarTipoProcesoExpediente(string idExpediente)
        {
            string ds = string.Empty;
            string consult = "SELECT TipoProcesoExpediente FROM co.Expedientes where IdExpediente='" + idExpediente + "' AND IdSociedadGL='" + gstr_IdSociedadGL + "' AND EstadoExpediente = 'Activo'";
            DataTable dt2 = GetData(consult);
            DataRow campo = null;
            string tipoProceso = string.Empty;
            if (dt2.Rows.Count > 0)
            {
                campo = dt2.Rows[0];
                tipoProceso = campo["TipoProcesoExpediente"].ToString();
            }

            return tipoProceso;
        }
        
        private string ConsultarOpcionesCatalogos(string tipoProcesoExpediente)
        {
            string ds = string.Empty;
            string consult = "SELECT ValOpcion,NomOpcion FROM ma.OpcionesCatalogos where IdCatalogo='30' AND Estado = 'A' and NomOpcion='" + tipoProcesoExpediente + "'";
            DataTable dt2 = GetData(consult);
            DataRow campo = null;
            string tipoProceso = string.Empty;
            if (dt2.Rows.Count > 0)
            {
                campo = dt2.Rows[0];
                tipoProceso = campo["ValOpcion"].ToString();
            }

            return tipoProceso;
        }

        protected void txtIntereses_TextChanged(object sender, EventArgs e) { ActualizaMontos(); }

        protected void txtCostas_TextChanged(object sender, EventArgs e) { ActualizaMontos(); }
        
        protected void txtInteresesMoratorios_TextChanged(object sender, EventArgs e) { ActualizaMontos(); }

        protected void txtDannoMoral_TextChanged(object sender, EventArgs e) { ActualizaMontos(); }

        private string ConsultarClaseDocumento(string str_modulo, string str_operacion)
        {
            DataSet ds;
            string consult = "SELECT  IdClaseDoc FROM [ma].[Operaciones] where IdModulo='CT' and IdOperacion='" + str_operacion + "'";
            DataTable dt2 = GetData(consult);
            DataRow campo = null;
            string clasDoc = string.Empty;
            if (dt2.Rows.Count > 0)
            {
                campo = dt2.Rows[0];
                clasDoc = campo["IdClaseDoc"].ToString();
            }

            return clasDoc;

        }
        
        private int ConsultarIdExpediente(string idexpediente)
        {
            string str_consul = "SELECT IdExp FROM co.Expedientes where IdExpediente='" + idexpediente + "' and IdSociedadGL='" + clsSesion.Current.SociedadUsr + "' AND EstadoExpediente = 'Activo'";
            int IdExp = 0;
            //Consultar ID Expedientes 
            DataTable exped = GetData(str_consul);
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                IdExp = Convert.ToInt32(campo["IdExp"].ToString());
            }

            return IdExp;
        }


        String str_Mensaje = String.Empty;
        Boolean lbool_cambio = true;


        protected void btn_Simulacion_Click(object sender, EventArgs e)
        {
            try
            {
                CalculoDiferencialCambiario();
            }
            catch
            {
                GuardarResultadoResoluciones();
            }
            
        }

        private void CalculoDiferencialCambiario()
        {
            #region variables
            DataTable ldt_MontosExpedientes = new DataTable();
            DataSet lds_MontosExpedientes = new DataSet();

            DateTime? ldt_FechaActual = DateTime.Today;//new DateTime();
            DateTime? ldt_FchInicio = Convert.ToDateTime(Convert.ToString(DateTime.Today.Year) + "-" + Convert.ToString(DateTime.Today.Month) + "-01");

            DataRow[] ldar_Temporal;

            string[] tipocambio = new string[4];

            tipocambio = CargarIndicadoresEco();

            decimal ldec_CompraActual = tipocambio[0] == "" ? 0 : Convert.ToDecimal(tipocambio[0]);
            decimal ldec_VentaActual = tipocambio[1] == "" ? 0 : Convert.ToDecimal(tipocambio[1]);
            decimal ldec_EuroActual = tipocambio[2] == "" ? 0 : Convert.ToDecimal(tipocambio[2]);
            decimal ldec_TBPActual = tipocambio[3] == "" ? 0 : Convert.ToDecimal(tipocambio[3]);
            decimal ldec_TipoCambioActual = 0;

            String[] lstr_Resultado = new String[3]{"","",""};
            String[] lstr_ResultadoRegistro = new String[2];

            string lstr_IdModulo = "IdModulo In ('CT')";
            string lstr_Operacion = String.Empty;
            string lstr_TipoExpediente = string.Empty;
            int lint_CantidadLineasAsiento;
            bool lbool_cambioMonto = false;
            string lstr_Leyenda = String.Empty;
            string lstr_Transaccion = "Diferencial Cambiario";
            decimal[] larrdec_Montos;
            decimal[] larrdec_MontosArriba;
            decimal[] larrdec_MontosAbajo;
            String lstr_AsientosResultado = String.Empty;
            #endregion
            string lstr_CodAsiento = "";

            lds_MontosExpedientes = ConsultarMontosExpedientes("", "", 0, 0, "", ldt_FchInicio, ldt_FechaActual);
            ldt_MontosExpedientes = lds_MontosExpedientes.Tables["Table"];

            if ((ldt_MontosExpedientes != null) && (ldt_MontosExpedientes.Rows.Count > 0))
            {
                foreach (DataRow dr_FilaExpediente in ldt_MontosExpedientes.Rows)
                {
                    #region Inicial
                    
                    string resultado = String.Empty;

                    decimal ldec_DifMontoPrincipal = 0;
                    decimal ldec_DifMontoIntereses = 0;

                    decimal ldec_DifIntereses = 0;
                    decimal ldec_DifInteresesMoratorios = 0;
                    decimal ldec_DifCostas = 0;
                    decimal ldec_DifDanoMoral = 0;

                    string lstr_IdExpediente = dr_FilaExpediente["IdExpedienteFK"].ToString();
                    string lstr_IdSociedad = dr_FilaExpediente["IdSociedadGL"].ToString();
                    string lstr_EstadoResolucion = dr_FilaExpediente["EstadoResolucion"].ToString();
                    String lstr_Moneda = dr_FilaExpediente["Moneda"].ToString();
                    lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);
                    #endregion

                    if ((lstr_IdSociedad.Contains(gstr_IdSociedadGL)))
                    // && (lstr_IdExpediente.Contains("01 - INC - LIQ")))
                    {
                        str_Mensaje += Environment.NewLine + Environment.NewLine;
                        str_Mensaje += "______________________________________________" + Environment.NewLine;
                        str_Mensaje += "Id Expediente: ";
                        str_Mensaje += lstr_IdExpediente + Environment.NewLine;

                        #region carga data
                        decimal ldec_TipoCambio = Convert.ToDecimal(dr_FilaExpediente["TipoCambio"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["TipoCambio"]);
                        decimal ldec_Tbp = Convert.ToDecimal(dr_FilaExpediente["Tbp"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tbp"]);
                        decimal ldec_Tiempo = Convert.ToDecimal(dr_FilaExpediente["Tiempo"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tiempo"]);
                        decimal ldec_TipoCambioCierre = Convert.ToDecimal(dr_FilaExpediente["TipoCambioCierre"].ToString().Equals("") ? ldec_TipoCambio : dr_FilaExpediente["TipoCambioCierre"]);
                        
                        decimal ldec_MontoPrincipal = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipal"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipal"]);
                        decimal ldec_MontoPrincipalColones = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColones"]);
                        decimal ldec_MontoPrincipalCierre = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColonesCierre"]);

                        decimal ldec_MontoIntereses = Convert.ToDecimal(dr_FilaExpediente["MontoIntereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoIntereses"]);
                        decimal ldec_MontoInteresesColones = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColones"]);
                        decimal ldec_MontoInteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColonesCierre"]);

                        decimal ldec_InteresesMoratorios = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratorios"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratorios"]);
                        decimal ldec_InteresesMoratoriosColones = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColones"]);
                        decimal ldec_InteresesMoratoriosColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColonesCierre"]);

                        decimal ldec_Intereses = Convert.ToDecimal(dr_FilaExpediente["Intereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Intereses"]);
                        decimal ldec_InteresesColones = Convert.ToDecimal(dr_FilaExpediente["InteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColones"]);
                        decimal ldec_InteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColonesCierre"]);

                        decimal ldec_Costas = Convert.ToDecimal(dr_FilaExpediente["Costas"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Costas"]);
                        decimal ldec_CostasColones = Convert.ToDecimal(dr_FilaExpediente["CostasColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColones"]);
                        decimal ldec_CostasColonesCierre = Convert.ToDecimal(dr_FilaExpediente["CostasColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColonesCierre"]);

                        decimal ldec_DanoMoral = Convert.ToDecimal(dr_FilaExpediente["DanoMoral"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoral"]);
                        decimal ldec_DanoMoralColones = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColones"]);
                        decimal ldec_DanoMoralColonesCierre = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColonesCierre"]);

                        Decimal ldec_MontoPrincipalAnterior = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalAnterior"]);
                        Decimal ldec_MontoInteresesAnterior = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesAnterior"]);
                        Decimal ldec_InteresesAnterior = Convert.ToDecimal(dr_FilaExpediente["InteresesAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesAnterior"]);
                        Decimal ldec_InteresesMoratoriosAnterior = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosAnterior"]);
                        Decimal ldec_CostasAnterior = Convert.ToDecimal(dr_FilaExpediente["CostasAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasAnterior"]);
                        Decimal ldec_DanoMoralAnterior = Convert.ToDecimal(dr_FilaExpediente["DanoMoralAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralAnterior"]);
                        #endregion

                        #region tipos cambio
                        lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);

                        str_Mensaje += lstr_TipoExpediente + " : " + lstr_EstadoResolucion + Environment.NewLine;
                        
                        if (lstr_Moneda.Contains("CRC"))
                            ldec_TipoCambioActual = 1;
                        else if (lstr_Moneda.Contains("USD"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual;
                        }
                        else if (lstr_Moneda.Contains("EUR"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual * ldec_EuroActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual * ldec_EuroActual;
                        }

                        ldec_TipoCambioActual = Math.Round(ldec_TipoCambioActual, 2);
                        #endregion

                        #region Calculo Diferencial

                        larrdec_MontosArriba = new Decimal[15];
                        larrdec_MontosAbajo = new Decimal[15];

                        if ((ldec_MontoPrincipalCierre == 0) && (ldec_MontoInteresesColonesCierre == 0))
                        {
                            ldec_DifMontoPrincipal = (ldec_MontoPrincipal * ldec_TipoCambioActual) - (ldec_MontoPrincipal * ldec_TipoCambio);
                            ldec_DifMontoIntereses = (ldec_MontoIntereses * ldec_TipoCambioActual) - (ldec_MontoIntereses * ldec_TipoCambio);

                            ldec_MontoPrincipalCierre = ldec_MontoPrincipal * ldec_TipoCambioActual;
                            ldec_MontoInteresesColonesCierre = ldec_MontoIntereses * ldec_TipoCambioActual;
                        }
                        else
                        {
                            ldec_DifMontoPrincipal = (ldec_MontoPrincipal * ldec_TipoCambioActual) - (ldec_MontoPrincipal * ldec_TipoCambioCierre);
                            ldec_DifMontoIntereses = (ldec_MontoIntereses * ldec_TipoCambioActual) - (ldec_MontoIntereses * ldec_TipoCambioCierre);

                            ldec_MontoPrincipalCierre = ldec_MontoPrincipal * ldec_TipoCambioActual;
                            ldec_MontoInteresesColonesCierre = ldec_MontoIntereses * ldec_TipoCambioActual;
                        }

                        if ((ldec_InteresesColonesCierre == 0) || (ldec_InteresesMoratoriosColonesCierre == 0) ||
                            (ldec_CostasColonesCierre == 0) || (ldec_DanoMoralColonesCierre == 0))
                        {
                            ldec_DifIntereses = (ldec_Intereses * ldec_TipoCambioActual) - (ldec_Intereses * ldec_TipoCambio);
                            ldec_DifInteresesMoratorios = (ldec_InteresesMoratorios * ldec_TipoCambioActual) - (ldec_InteresesMoratorios * ldec_TipoCambio);
                            ldec_DifCostas = (ldec_Costas * ldec_TipoCambioActual) - (ldec_Costas * ldec_TipoCambio);
                            ldec_DifDanoMoral = (ldec_DanoMoral * ldec_TipoCambioActual) - (ldec_DanoMoral * ldec_TipoCambio);

                            ldec_InteresesColonesCierre = ldec_Intereses * ldec_TipoCambioActual;
                            ldec_InteresesMoratoriosColonesCierre = ldec_InteresesMoratorios * ldec_TipoCambioActual;
                            ldec_CostasColonesCierre = ldec_Costas * ldec_TipoCambioActual;
                            ldec_DanoMoralColonesCierre = ldec_DanoMoral * ldec_TipoCambioActual;
                        }
                        else
                        {
                            ldec_DifIntereses = (ldec_Intereses * ldec_TipoCambioActual) - (ldec_Intereses * ldec_TipoCambioCierre);
                            ldec_DifInteresesMoratorios = (ldec_InteresesMoratorios * ldec_TipoCambioActual) - (ldec_InteresesMoratorios * ldec_TipoCambioCierre);
                            ldec_DifCostas = (ldec_Costas * ldec_TipoCambioActual) - (ldec_Costas * ldec_TipoCambioCierre);
                            ldec_DifDanoMoral = (ldec_DanoMoral * ldec_TipoCambioActual) - (ldec_DanoMoral * ldec_TipoCambioCierre);

                            ldec_InteresesColonesCierre = ldec_Intereses * ldec_TipoCambioActual;
                            ldec_InteresesMoratoriosColonesCierre = ldec_InteresesMoratorios * ldec_TipoCambioActual;
                            ldec_CostasColonesCierre = ldec_Costas * ldec_TipoCambioActual;
                            ldec_DanoMoralColonesCierre = ldec_DanoMoral * ldec_TipoCambioActual;
                        }

                        if (ldec_DifMontoPrincipal > 0)
                            larrdec_MontosArriba[0] = ldec_DifMontoPrincipal;
                        else if (ldec_DifMontoPrincipal < 0)
                            larrdec_MontosAbajo[0] = ldec_DifMontoPrincipal * -1;

                        if (ldec_DifMontoIntereses > 0)
                            larrdec_MontosArriba[1] = ldec_DifMontoIntereses;
                        else if (ldec_DifMontoIntereses < 0)
                            larrdec_MontosAbajo[1] = ldec_DifMontoIntereses * -1;

                        if (ldec_DifIntereses > 0)
                            larrdec_MontosArriba[1] = ldec_DifIntereses;
                        else if (ldec_DifIntereses < 0)
                            larrdec_MontosAbajo[1] = ldec_DifIntereses * -1;

                        if (ldec_DifInteresesMoratorios > 0)
                            larrdec_MontosArriba[2] = ldec_DifInteresesMoratorios;
                        else if (ldec_DifInteresesMoratorios < 0)
                            larrdec_MontosAbajo[2] = ldec_DifInteresesMoratorios * -1;

                        if (ldec_DifCostas > 0)
                            larrdec_MontosArriba[3] = ldec_DifCostas;
                        else if (ldec_DifCostas < 0)
                            larrdec_MontosAbajo[3] = ldec_DifCostas * -1;

                        if (ldec_DifDanoMoral > 0)
                            larrdec_MontosArriba[4] = ldec_DifDanoMoral;
                        else if (ldec_DifDanoMoral < 0)
                            larrdec_MontosAbajo[4] = ldec_DifDanoMoral * -1;

                        Boolean lbool_continuar = false;
                        for (int j = 0; j < larrdec_MontosArriba.Count(); j++)
                        {
                            if (larrdec_MontosArriba[j] > 0)
                            {
                                lbool_continuar = true;
                            }
                        }
                        #endregion

                        if ((lstr_Moneda.Contains("USD") || lstr_Moneda.Contains("EUR")))
                        {
                            #region envio asientos
                            if ((((ldec_DifMontoPrincipal > 0 || ldec_DifMontoIntereses > 0) ||
                                (ldec_DifIntereses > 0 || ldec_DifInteresesMoratorios > 0 || ldec_DifCostas > 0 || ldec_DifDanoMoral > 0))
                                && lbool_continuar) ||

                                (((ldec_DifMontoPrincipal < 0 || ldec_DifMontoIntereses < 0) ||
                                (ldec_DifIntereses < 0 || ldec_DifInteresesMoratorios < 0 || ldec_DifCostas < 0 || ldec_DifDanoMoral < 0))
                                && !lbool_continuar))
                            {
                                #region diferencial
                                if (ldec_TipoCambioActual>ldec_TipoCambioCierre){
                                    lbool_continuar = true;
                                }
                                else
                                {
                                    lbool_continuar = false;
                                }
                                
                                if (lstr_TipoExpediente.Contains("Demandado") &&
                                    (lstr_EstadoResolucion.Contains("En Firme") || lstr_EstadoResolucion.Contains("Liquidacion")))
                                {
                                    #region Demandado RF Liq
                                    lint_CantidadLineasAsiento = 12;

                                    if (lbool_continuar)
                                    {
                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT22", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    else
                                    {
                                        lstr_AsientosResultado = String.Empty;
                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT23", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        lstr_Resultado[0] = "exito";
                                    }
                                    else
                                    {
                                        lstr_Resultado[0] = "fallo";
                                    }
                                    #endregion
                                }
                                else if (lstr_TipoExpediente.Contains("Demandado"))
                                {
                                    #region Demandado
                                    lint_CantidadLineasAsiento = 4;

                                    if (lbool_continuar)
                                    {
                                        lstr_AsientosResultado = String.Empty;
                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT28", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    else
                                    {
                                        lstr_AsientosResultado = String.Empty;
                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT29", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        lstr_Resultado[0] = "exito";
                                    }
                                    else
                                    {
                                        lstr_Resultado[0] = "fallo";
                                    }
                                    #endregion
                                }
                                else if (lstr_TipoExpediente.Contains("Actor") &&
                                    (lstr_EstadoResolucion.Contains("En Firme") || lstr_EstadoResolucion.Contains("Liquidacion")))
                                {
                                    #region Actor RF Liq
                                    lint_CantidadLineasAsiento = 12;
                                    if (lbool_continuar)
                                    {
                                        lstr_AsientosResultado = String.Empty;
                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT24", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    else
                                    {
                                        lstr_AsientosResultado = String.Empty;

                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT25", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        lstr_Resultado[0] = "exito";
                                    }
                                    else
                                    {
                                        lstr_Resultado[0] = "fallo";
                                    }
                                    #endregion
                                }

                                
                                #endregion

                                str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                            }
                            #endregion
                        }

                        Int32 lint_IdRes = ConsultarIdRes(lstr_IdExpediente, lstr_IdSociedad, lstr_EstadoResolucion);

                        if (lstr_Resultado[0].Contains("exito") || lstr_Moneda.Contains("CRC"))
                        {
                            try
                            {
                                #region Registro
                                lstr_ResultadoRegistro = ws_SGService.uwsModificarCobrosPagos(
                                    lstr_IdExpediente, lstr_IdSociedad, lint_IdRes,
                                    lstr_Moneda, ldec_TipoCambio, ldec_Tbp, ldec_Tiempo, ldec_TipoCambioActual,

                                    ldec_MontoPrincipal, ldec_MontoPrincipalColones, ldec_MontoPrincipalCierre,
                                    ldec_MontoIntereses, ldec_MontoInteresesColones, ldec_MontoInteresesColonesCierre,
                                    0, 0, 0,
                                    0, 0, 0,
                                    ldec_Intereses, ldec_InteresesColones, ldec_InteresesColonesCierre,
                                    ldec_InteresesMoratorios, ldec_InteresesMoratoriosColones, ldec_InteresesMoratoriosColonesCierre,
                                    ldec_Costas, ldec_CostasColones, ldec_CostasColonesCierre,
                                    ldec_DanoMoral, ldec_DanoMoralColones, ldec_DanoMoralColonesCierre,
                                    ldec_MontoPrincipalAnterior, ldec_MontoInteresesAnterior,
                                    ldec_InteresesAnterior, ldec_CostasAnterior,
                                    ldec_InteresesMoratoriosAnterior, ldec_DanoMoralAnterior,
                                    "Diferencial", "Diferencial");

                                if (lstr_ResultadoRegistro[0].Contains("00"))
                                {
                                    lstr_Resultado[0] = "exito";
                                    str_Mensaje += "Expediente " + lstr_IdExpediente + " modificado con éxito." + Environment.NewLine;
                                }
                                else
                                {
                                    lstr_Resultado[0] = "fallo";
                                    str_Mensaje += "Fallo al modificar expediente " + lstr_IdExpediente + "." + Environment.NewLine +
                                        lstr_ResultadoRegistro[0] + ": " + lstr_ResultadoRegistro[1] + Environment.NewLine;
                                }


                                #endregion
                            }
                            catch (Exception ex)
                            {
                                str_Mensaje += "Fallo al modificar expediente " + lstr_IdExpediente + "." + Environment.NewLine +
                                "Error: " + ex.Message + Environment.NewLine;

                            }
                        }
                    }

                }

                str_Mensaje += "Fin de Proceso\n";
                str_Mensaje += "------------------------------------------------" + Environment.NewLine;

                GuardarResultadoContabilizacion();
            }
        }

        public void GuardarResultadoContabilizacion()
        {
            string path = @"C:\inetpub\wwwroot\SistemaGestor\Logs\LogResoluciones " +
                DateTime.Now.Year + "." +
                DateTime.Now.Month + "." +
                DateTime.Now.Day + " " +
                DateTime.Now.Hour + "." +
                DateTime.Now.Minute + "." +
                DateTime.Now.Second + ".txt";
            //string path = @"C:\Logs\DiferencialCambiarioContingentes\LogDifCambiario.txt";

            // This text is added only once to the file.
            //if (!File.Exists(path))
            //{
                // Create a file to write to.
                File.WriteAllText(path, str_Mensaje);
            //}
            string readText = File.ReadAllText(path);

        }

        public void GuardarResultadoResoluciones()
        {
            string path = @"C:\inetpub\wwwroot\SistemaGestor\Logs\LogResoluciones " +
                DateTime.Now.Year + "." +
                DateTime.Now.Month + "." +
                DateTime.Now.Day + " " +
                DateTime.Now.Hour + "." +
                DateTime.Now.Minute + "." +
                DateTime.Now.Second + ".txt";

            // This text is added only once to the file.
            //if (!File.Exists(path))
            //{
                // Create a file to write to.
                File.WriteAllText(path, str_Mensaje);
            //}
            string readText = File.ReadAllText(path);

        }

        private Int32 ConsultarIdRes(string str_idexpediente, String str_IdSociedad, String str_EstadoResolucion)
        {
            String str_consul = "SELECT res.IdRes FROM co.Expedientes exp " +
                "INNER JOIN co.Resoluciones res " +
                "ON exp.IdExp = res.IdExp " +
                "WHERE exp.IdExpediente = '" + str_idexpediente + "' " +
                "AND exp.IdSociedadGL = '" + str_IdSociedad + "' " +
                "AND exp.EstadoExpediente = 'Activo' " +
                "AND res.EstadoResolucion = '" + str_EstadoResolucion + "'";

            Int32 lint_IdRes;

            DataTable exped = GetData(str_consul);
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                lint_IdRes = Convert.ToInt32(campo["IdRes"].ToString());
            }
            else
            {

                lint_IdRes = 0;
            }

            return lint_IdRes;
        }


        private DataSet ConsultarMontosExpedientes(string lstr_IdExpediente,
            string lstr_IdSociedadGL,
            int lint_IdExp,
            int lint_IdRes,
            string lstr_EstadoResolucion,
            DateTime? ldt_FchInicio,
            DateTime? ldt_FchFin)
        {
            DataSet ds_CobrosPagos = new DataSet();
            ds_CobrosPagos = ws_SGService.uwsConsultarCobrosPagos(lstr_IdExpediente, lstr_IdSociedadGL, lint_IdExp, lint_IdRes, lstr_EstadoResolucion, ldt_FchInicio, ldt_FchFin);

            return ds_CobrosPagos;
        }

        private String[] EnviarAsientos2(String str_IdExpediente, String str_Sociedad, String str_AsientosResultado,
            String str_IdModulo, String str_IdOperacion,
            String str_Trasaccion, String str_Leyenda, Boolean lbool_cambio, Decimal[] arrdec_Montos,
            Int32 int_CantidadLineasAsiento,
            Decimal MontoPricipalColones, Decimal MontoInteresColones, out String CodAsiento)
        {
            #region Variables
            Presentacion.wsAsientos.ZfiAsiento item_asiento = new Presentacion.wsAsientos.ZfiAsiento();
            Presentacion.wsAsientos.ZfiAsiento item_asiento2 = new Presentacion.wsAsientos.ZfiAsiento();
            Presentacion.wsAsientos.ZfiAsiento[] tabla_asientos = new Presentacion.wsAsientos.ZfiAsiento[int_CantidadLineasAsiento];

            String[] item_resAsientosLog = new String[10];
            String lstr_logAsiento = String.Empty;
            String[] lstr_Resultado = new String[3]{"","",""};
            String lstr_Montos = String.Empty;

            String lstr_TipoProcesoTexto = String.Empty;
            String lstr_TipoProceso_CodAux2 = String.Empty;

            String lstr_idTira_CodAux3 = String.Empty;
            String lstr_clsDocumento_CodAux4 = String.Empty;

            String lstr_ClaveContable = String.Empty;
            String lstr_ClaveContable2 = String.Empty;

            Int32 lint_cantLineasAsiento = 0;
            Int32 lint_Contador = 0;
            Int32 lint_cantTiras = 0;
            Int32 lint_contMonto = 0;

            Boolean bool_diferencial = false;

            DateTime ldt_FechaContabilizacion = DateTime.Now;

            Decimal ldec_Monto = 0;
            String str_Usuario = "0000001";

            #endregion
            CodAsiento = "";

            Boolean lbool_continuar = false;
            for (int j = 0; j < arrdec_Montos.Count(); j++)
            {
                if (arrdec_Montos[j] > 0)
                {
                    lbool_continuar = true;
                }
            }

            if (arrdec_Montos != null)
            {
                for (int i = 0; i < arrdec_Montos.Count(); i++)
                {
                    if (arrdec_Montos[i] != null)
                        arrdec_Montos[i] = Math.Round(arrdec_Montos[i], 2);
                }
            }

            if (str_IdOperacion.Contains("CT28") || str_IdOperacion.Contains("CT29"))
            {
                bool_diferencial = true;
            }

            if (lbool_continuar)
            {
                //Tipo de proceso
                lstr_TipoProcesoTexto = ConsultarTipoProcesoExpediente(str_IdExpediente);
                lstr_TipoProceso_CodAux2 = ConsultarOpcionesCatalogos(lstr_TipoProcesoTexto);
                lstr_clsDocumento_CodAux4 = ConsultarClaseDocumento(str_IdModulo, str_IdOperacion);

                //Obtenemos tira de asientos configuradas en el gestor
                DataSet lds_TirasAsientos = ConsultarTiposAsientos2(str_Sociedad, str_IdModulo, str_IdOperacion, lstr_TipoProceso_CodAux2);
                DataTable ldt_TirasAsiento = null;

                lint_cantTiras = lds_TirasAsientos.Tables[0].Rows.Count;

                if (lint_cantTiras > 0)
                {
                    ldt_TirasAsiento = lds_TirasAsientos.Tables[0];

                    //Sacar datos de tiras asientos
                    if ((lint_cantTiras == 2) && !lbool_cambio && !bool_diferencial)
                    {
                        Int32 lint_cont = 0;

                        #region caso simple
                        foreach (DataRow ldr_TiraAsiento in ldt_TirasAsiento.Rows)
                        {
                            //Segun monto a enviar a SIGAF para contabilizar asiento de provision 
                            lstr_idTira_CodAux3 = ldr_TiraAsiento["CodigoAuxiliar3"].ToString();
                            switch (lstr_idTira_CodAux3.Trim())
                            {
                                case "1"://Monto Principal
                                    if (MontoPricipalColones != 0)
                                    {
                                        ////Llenamos los asientos
                                        item_asiento = new wsAsientos.ZfiAsiento();
                                        String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                        if (lstr_info.Length > 15)
                                            lstr_info = lstr_info.Substring(0, 15);
                                        item_asiento.Xblnr = lstr_info;//REF
                                        item_asiento.Bktxt = "Texto_Cabecera";
                                        item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                        item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                        item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                        item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                        item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                        item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT10-AG operacion+codigoprocesal expediente


                                        item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                        item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                        item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                        item_asiento.Wrbtr = MontoPricipalColones;//Importe o monto en colones a contabilizar 

                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 40: " + MontoPricipalColones + "\n";

                                        item_asiento.Zuonr = "Asig_1";
                                        item_asiento.Sgtxt = "SG-Liquidacion";
                                        item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                        item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                        item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                        item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                        item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
                                        item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                        item_asiento.Fkber = "";
                                        item_asiento.Xref2 = "";
                                        tabla_asientos[0] = item_asiento;
                                        ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
                                        item_asiento2 = new wsAsientos.ZfiAsiento();
                                        item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                        item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                        item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                        item_asiento2.Wrbtr = MontoPricipalColones;//Importe o monto en colones a contabilizar
                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 50: " + MontoPricipalColones + "\n";

                                        item_asiento2.Zuonr = "";
                                        item_asiento2.Sgtxt = "SG-Provision diario";
                                        item_asiento2.Fipex = ldr_TiraAsiento["IdPosPre2"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento2.Kostl = ldr_TiraAsiento["IdCentroCosto2"].ToString();
                                        item_asiento2.Fistl = ldr_TiraAsiento["IdCentroGestor2"].ToString();
                                        item_asiento2.Prctr = ldr_TiraAsiento["IdCentroBeneficio2"].ToString();
                                        item_asiento2.Geber = ldr_TiraAsiento["IdFondo2"].ToString().Trim();//Fondo
                                        item_asiento2.Fkber = "";
                                        item_asiento2.Xref2 = "xref2";
                                        tabla_asientos[1] = item_asiento2;
                                    }
                                    break;
                                case "2"://Monto Intereses
                                    if (MontoInteresColones != 0)
                                    {
                                        item_asiento = new wsAsientos.ZfiAsiento();
                                        ///***************************************************Cargar cuenta 40 HABER*****************************************************/
                                        if (MontoPricipalColones == 0)
                                        {
                                            String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                            if (lstr_info.Length > 15)
                                                lstr_info = lstr_info.Substring(0, 15);
                                            item_asiento.Xblnr = lstr_info;//REF
                                            item_asiento.Bktxt = "Texto_Cabecera";
                                            item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                            item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                            item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                            item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                            item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 


                                            item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                            item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente

                                        }

                                        item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                        item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                        item_asiento.Wrbtr = MontoInteresColones;//Importe o monto en colones a contabilizar 

                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 40: " + MontoInteresColones + "\n";

                                        item_asiento.Zuonr = "Asig_1";
                                        item_asiento.Sgtxt = "SG-Provision";
                                        item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                        item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                        item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                        item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                        item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
                                        item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                        item_asiento.Fkber = "";
                                        item_asiento.Xref2 = "";
                                        if (MontoPricipalColones == 0)
                                        {
                                            tabla_asientos[0] = item_asiento;
                                        }
                                        else
                                            tabla_asientos[2] = item_asiento;
                                        ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
                                        item_asiento2 = new wsAsientos.ZfiAsiento();
                                        item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                        item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                        item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                        item_asiento2.Wrbtr = MontoInteresColones;//Importe o monto en colones a contabilizar
                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 50: " + MontoInteresColones + "\n";


                                        item_asiento2.Projk = ldr_TiraAsiento["IdElementoPEP2"].ToString().TrimEnd();
                                        item_asiento2.Fipex = ldr_TiraAsiento["IdPosPre2"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento2.Kostl = ldr_TiraAsiento["IdCentroCosto2"].ToString();
                                        item_asiento2.Fistl = ldr_TiraAsiento["IdCentroGestor2"].ToString();
                                        item_asiento2.Prctr = ldr_TiraAsiento["IdCentroBeneficio2"].ToString();
                                        item_asiento2.Measure = ldr_TiraAsiento["IdPrograma2"].ToString().TrimEnd();//Programa presupuestario
                                        item_asiento2.Zuonr = "Asig_2";
                                        item_asiento2.Sgtxt = "SG-Liquidacion";//char 50
                                        item_asiento2.Geber = ldr_TiraAsiento["IdFondo2"].ToString().Trim();//Fondo
                                        item_asiento2.Fkber = "";
                                        item_asiento2.Xref2 = "xref2";
                                        if (MontoPricipalColones == 0)
                                        {
                                            tabla_asientos[1] = item_asiento2;
                                        }
                                        else
                                            tabla_asientos[3] = item_asiento2;
                                    }
                                    break;
                            }
                        }
                        #endregion
                    }
                    else if (lint_cantTiras >= 2)
                    {
                        lint_Contador = 0;
                        Int32 lint_index = 0;

                        #region casos Complicados
                        foreach (DataRow ldr_TiraAsiento in ldt_TirasAsiento.Rows)
                        {
                            lint_index = ldt_TirasAsiento.Rows.IndexOf(ldr_TiraAsiento);

                            lstr_idTira_CodAux3 = ldr_TiraAsiento["CodigoAuxiliar3"].ToString();
                            lstr_ClaveContable = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();
                            lstr_ClaveContable2 = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();

                            if ((lint_Contador == 0) && (arrdec_Montos[lint_contMonto] == 0))
                            {
                                lint_contMonto++;
                                continue;
                            }
                            if (lint_Contador == int_CantidadLineasAsiento)
                                break;
                            //if (lint_cantTiras == lint_Contador)
                            //    break;
                            else if ((lint_cantTiras == 4) && (MontoPricipalColones != 0) && (MontoInteresColones == 0))
                            {
                                if ((lint_index == 1) || (lint_index == 3))
                                    continue;
                            }
                            else if ((lint_cantTiras == 4) && (MontoPricipalColones == 0) && (MontoInteresColones != 0))
                            {
                                if ((lint_index == 0) || (lint_index == 2))
                                    continue;
                            }
                            else if ((lint_cantTiras == 6) && (MontoPricipalColones != 0) && (MontoInteresColones == 0))
                            {
                                if ((lint_index == 1) || (lint_index == 3) || (lint_index == 5))
                                    continue;
                            }
                            else if ((lint_cantTiras == 6) && (MontoPricipalColones == 0) && (MontoInteresColones != 0))
                            {
                                if ((lint_index == 0) || (lint_index == 2) || (lint_index == 4))
                                    continue;
                            }

                            ldec_Monto = arrdec_Montos[lint_contMonto++];

                            if ((lstr_ClaveContable.Equals("40") && lstr_ClaveContable2.Equals("50")))
                            {
                                item_asiento = new wsAsientos.ZfiAsiento();
                                #region cabecera
                                if (lint_Contador == 0)
                                {
                                    item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                    item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                    item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                    item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                    String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                    if (lstr_info.Length > 15)
                                        lstr_info = lstr_info.Substring(0, 15);
                                    item_asiento.Xblnr = lstr_info;//REF
                                    item_asiento.Bktxt = "Texto_Cabecera";
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region debe 40
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 40: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "SG-Liquidacion";
                                item_asiento.Zuonr = "Asig_1";
                                item_asiento.Fkber = "";
                                item_asiento.Xref2 = "";
                                item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();
                                if (lint_Contador == 0)
                                {
                                    tabla_asientos[lint_Contador] = item_asiento;
                                    lint_Contador++;
                                }
                                else
                                    tabla_asientos[lint_Contador++] = item_asiento;

                                #endregion

                                item_asiento2 = new wsAsientos.ZfiAsiento();
                                #region 50 haber
                                //if (lint_cantTiras == lint_Contador)
                                //    break;
                                if (str_IdOperacion.Contains("CT09") && (lint_Contador == 3))
                                    break;
                                if ((lbool_cambio) && (lint_Contador < 2))
                                {
                                    ldec_Monto = arrdec_Montos[lint_contMonto++];
                                }
                                item_asiento2.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 50: " + ldec_Monto + "\n";
                                item_asiento2.Sgtxt = "SG-Provision diario";
                                item_asiento2.Zuonr = "";
                                item_asiento2.Fkber = "";
                                item_asiento2.Xref2 = "xref2";
                                item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                item_asiento2.Kostl = ldr_TiraAsiento["IdCentroCosto2"].ToString();
                                item_asiento2.Fistl = ldr_TiraAsiento["IdCentroGestor2"].ToString();
                                item_asiento2.Prctr = ldr_TiraAsiento["IdCentroBeneficio2"].ToString();
                                item_asiento2.Geber = ldr_TiraAsiento["IdFondo2"].ToString().Trim();//Fondo
                                item_asiento2.Projk = ldr_TiraAsiento["IdElementoPEP2"].ToString().TrimEnd();
                                item_asiento2.Fipex = ldr_TiraAsiento["IdPosPre2"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento2.Measure = ldr_TiraAsiento["IdPrograma2"].ToString().TrimEnd();
                                tabla_asientos[lint_Contador++] = item_asiento2;
                                #endregion
                            }
                            else if (lstr_ClaveContable.Equals("40"))
                            {
                                item_asiento = new wsAsientos.ZfiAsiento();
                                #region cabecera
                                if (lint_Contador == 0)
                                {
                                    item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                    item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                    item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                    item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                    String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                    if (lstr_info.Length > 15)
                                        lstr_info = lstr_info.Substring(0, 15);
                                    item_asiento.Xblnr = lstr_info;//REF
                                    item_asiento.Bktxt = "Texto_Cabecera";
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region debe 40
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 40: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "SG-Liquidacion";
                                item_asiento.Zuonr = "Asig_1";
                                item_asiento.Fkber = "";
                                item_asiento.Xref2 = "";
                                item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();
                                if (lint_Contador == 0)
                                {
                                    tabla_asientos[lint_Contador] = item_asiento;
                                    lint_Contador++;
                                }
                                else
                                    tabla_asientos[lint_Contador++] = item_asiento;
                                #endregion

                            }
                            else if (lstr_ClaveContable.Equals("50"))
                            {
                                item_asiento = new wsAsientos.ZfiAsiento();
                                #region cabecera
                                if (lint_Contador == 0)
                                {
                                    item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                    item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                    item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                    item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                    String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                    if (lstr_info.Length > 15)
                                        lstr_info = lstr_info.Substring(0, 15);
                                    item_asiento.Xblnr = lstr_info;//REF
                                    item_asiento.Bktxt = "Texto_Cabecera";
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region haber 50
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 50: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "SG-Liquidacion";
                                item_asiento.Zuonr = "Asig_1";
                                item_asiento.Fkber = "";
                                item_asiento.Xref2 = "";
                                item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();
                                if (lint_Contador == 0)
                                {
                                    tabla_asientos[lint_Contador] = item_asiento;
                                    lint_Contador++;
                                }
                                else
                                    tabla_asientos[lint_Contador++] = item_asiento;
                                #endregion

                            }
                        }
                        #endregion
                    }
                    //Cargar de Asientos 
                    string[] concatenado = new string[8];
                    //envio de asiento mediante servicio web hacia SIGAF
                    try
                    {
                        //item_resAsientosLog = ws_ContabilizaAsientos.EnviarAsientos(tabla_asientos);  *cucurucho
                        item_resAsientosLog = ws_ContabilizaAsientos.EnviarAsientos(tabla_asientos, "");
                        Int32 lint_Length = 0;
                        for (int j = 0; j < item_resAsientosLog.Count(); j++)
                        {
                            if (item_resAsientosLog[j].Contains("[E]"))
                                lstr_Resultado[0] = "error";
                            else if (item_resAsientosLog[j].Contains("[S]"))
                            {
                                lint_Length = item_resAsientosLog[j].Length;
                                try
                                {
                                    str_AsientosResultado = str_AsientosResultado + "\n" + item_resAsientosLog[j].ToString().Substring(58, 10);
                                }
                                catch { }
                                lstr_Resultado[0] = "Contabilizado";
                                lstr_Resultado[2] = str_AsientosResultado;

                                try
                                {
                                    ws_SGService.uwsRegistrarBitacoraMovimientosCuentasExpedientes(str_IdExpediente, "CT", str_Sociedad, str_IdOperacion, "", MontoPricipalColones, MontoInteresColones, 0, 0, "Provisión Monto Principal Colones- ", str_Usuario);
                                    ws_SGService.uwsRegistrarBitacoraMovimientosCuentasExpedientes(str_IdExpediente, "CT", str_Sociedad, str_IdOperacion, "", MontoPricipalColones, MontoInteresColones, 0, 0, "Provisión Monto Interes Colones - ", str_Usuario);

                                    //ws_SG.uwsRegistrarCobrosPagos(gstr_IdExpediente, gstr_IdExpediente)
                                }
                                catch { }
                            }
                            else if (item_resAsientosLog[j].Contains("[I]"))
                                lstr_Resultado[0] = "info";

                            lstr_logAsiento += "\n" + (j + 1) + ": " + item_resAsientosLog[j];

                            lstr_Resultado[1] = lstr_logAsiento;
                        }

                        //str_Mensaje += lstr_logAsiento;

                        ws_SGService.uwsRegistrarAccionBitacoraCo("CT", str_Usuario, "Enviar Asiento", str_IdExpediente + ":" + str_Sociedad +
                            " Operación: " + str_IdOperacion + 
                            "Resultado: " + lstr_logAsiento,
                            str_IdExpediente, str_Trasaccion, str_Sociedad);

                        try
                        {

                            String[] lstr_AsientosResultado = new String[3];
                            Int32 lint_IdExp = 0;

                            String lstr_query = "SELECT IdExp FROM co.Expedientes exp " +
                                "WHERE exp.IdExpediente ='" + str_IdExpediente + "' " +
                                "AND exp.IdSociedadGL ='" + str_Sociedad + "' " +
                                "AND exp.EstadoExpediente = 'Activo'"; 
                                //"AND cp.EstadoTransaccion != 'estadotran'";

                            DataTable dt_Resoluciones = GetData(lstr_query);

                            foreach (DataRow dr_Resolucion in dt_Resoluciones.Rows)
                            {
                                lint_IdExp = Convert.ToInt32(dr_Resolucion["IdExp"]);
                            }
                            if (lstr_Resultado[0].Contains("Contabilizado"))
                            {
                                lstr_AsientosResultado = ws_SGService.uwsRegistrarCobrosPagos(str_IdOperacion,
                                    str_IdExpediente, lint_IdExp, "REV", 0, 0,
                                    0, 0,
                                    arrdec_Montos == null ? MontoPricipalColones : arrdec_Montos[0],
                                    arrdec_Montos == null ? MontoPricipalColones : arrdec_Montos[1],//Monto Pr
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 2 ? arrdec_Montos[2] : 0),
                                    arrdec_Montos == null ? MontoInteresColones : (arrdec_Montos.Count() >= 3 ? arrdec_Montos[3] : 0),
                                    arrdec_Montos == null ? MontoInteresColones : (arrdec_Montos.Count() >= 4 ? arrdec_Montos[4] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 5 ? arrdec_Montos[5] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 6 ? arrdec_Montos[6] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 7 ? arrdec_Montos[7] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 8 ? arrdec_Montos[8] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 9 ? arrdec_Montos[9] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 10 ? arrdec_Montos[10] : 0),
                                    0,
                                    0, 0, 0,//Intereses
                                    0, 0, 0,
                                    0, 0, 0,
                                    0, 0, 0,
                                    0, 0, 0, 0,//Anteriores

                                    "tipotra", "estadotran", DateTime.Today, "Reversion", str_Usuario);
                            }
                        }
                        catch (Exception ex)
                        {
                            str_Mensaje += ex.Message + Environment.NewLine;
                                
                        }
                    }

                    catch (Exception ex)
                    {
                        lstr_Resultado[0] = "error";
                        lstr_Resultado[1] = lstr_logAsiento + "\n" + ex.Message;

                        ws_SGService.uwsRegistrarAccionBitacoraCo("CT", str_Usuario, "Enviar Asiento", str_IdExpediente + ":" + str_Sociedad +
                            " Operación: " + str_IdOperacion + "\n" + str_Leyenda + "\n" + lstr_Montos +
                           "\nResultado: " + lstr_Resultado,
                           str_IdExpediente, str_Trasaccion, str_Sociedad);

                        return lstr_Resultado;
                    }
                }
                else
                {
                    lstr_Resultado[0] = "error";
                    lstr_Resultado[1] = "Error: Los datos de consulta del asiento, no fue encontrada en la configuracion del Sistema Gestor.";
                }
            }
            return lstr_Resultado;

        }

        private DataSet ConsultarTiposAsientos2(String str_Sociedad, string str_modulo, string str_operacion, string str_tipoProcesoExpediente)
        {
            DataSet ds;
            DataSet lds_TirasAsientos;
            String lstr_SociedadFi = string.Empty;

            String lstr_ConsultaSociedades = "SELECT IdSociedadFi from ma.SociedadesFinancieras " +
            "WHERE IdSociedadGL='" + str_Sociedad + "'";

            DataTable lds_NombreSociedades = GetData(lstr_ConsultaSociedades);
            DataRow ldr_NombreSociedad = null;

            if (lds_NombreSociedades.Rows.Count > 0)
            {
                ldr_NombreSociedad = lds_NombreSociedades.Rows[0];
                lstr_SociedadFi = ldr_NombreSociedad["IdSociedadFi"].ToString();
            }


            lds_TirasAsientos = ws_SGService.uwsConsultarTiposAsiento(lstr_SociedadFi, str_modulo, str_operacion, "", "", "CRC", str_tipoProcesoExpediente, null, null);

            return lds_TirasAsientos;

        }


        protected void btn_Incobrabilidad_Click(object sender, EventArgs e)
        {
            try
            {
                CalculoIncobrabilidad();
            }
            catch
            {
                //();
            }
        }

        private void CalculoIncobrabilidad()
        {
            #region variables
            DataTable ldt_MontosExpedientes = new DataTable();
            DataSet lds_MontosExpedientes = new DataSet();

            DateTime? ldt_FechaActual = DateTime.Today;//new DateTime();
            DateTime? ldt_FchInicio = Convert.ToDateTime(Convert.ToString(DateTime.Today.Year) + "-" + Convert.ToString(DateTime.Today.Month) + "-01");

            DataRow[] ldar_Temporal;

            string[] tipocambio = new string[4];

            tipocambio = CargarIndicadoresEco();
            tipocambio = CargarIndicadoresEco();

            decimal ldec_CompraActual = tipocambio[0] == "" ? 0 : Convert.ToDecimal(tipocambio[0]);
            decimal ldec_VentaActual = tipocambio[1] == "" ? 0 : Convert.ToDecimal(tipocambio[1]);
            decimal ldec_EuroActual = tipocambio[2] == "" ? 0 : Convert.ToDecimal(tipocambio[2]);
            decimal ldec_TBPActual = tipocambio[3] == "" ? 0 : Convert.ToDecimal(tipocambio[3]);
            decimal ldec_TipoCambioActual = 0;

            String[] lstr_Resultado = new String[3]{"","",""};
            String[] lstr_ResultadoRegistro = new String[2];

            string lstr_IdModulo = "IdModulo In ('CT')";
            string lstr_Operacion = String.Empty;
            string lstr_TipoExpediente = string.Empty;
            int lint_CantidadLineasAsiento;
            bool lbool_cambioMonto = false;
            string lstr_Leyenda = String.Empty;
            string lstr_Transaccion = "Diferencial Cambiario";
            decimal[] larrdec_Montos;
            decimal[] larrdec_MontosArriba;
            decimal[] larrdec_MontosAbajo;
            String lstr_AsientosResultado = String.Empty;

            gbool_CambioMes = this.ckbNuevoMes.Checked;
            gbool_CambioAno = this.ckbNuevoAno.Checked;

            #endregion
            string lstr_CodAsiento = string.Empty;

            lds_MontosExpedientes = ConsultarMontosExpedientes("", "", 0, 0, "", ldt_FchInicio, ldt_FechaActual);
            ldt_MontosExpedientes = lds_MontosExpedientes.Tables["Table"];

            if (ldt_MontosExpedientes.Rows.Count > 0)
            {
                foreach (DataRow dr_FilaExpediente in ldt_MontosExpedientes.Rows)
                {
                    #region Inicial

                    str_Mensaje += "______________________________________________" + Environment.NewLine;
                    str_Mensaje += "Id Expediente: " + Environment.NewLine;
                    str_Mensaje += dr_FilaExpediente["IdExpedienteFK"].ToString() + Environment.NewLine;
                    str_Mensaje += "______________________________________________" + Environment.NewLine;

                    string resultado = String.Empty;

                    decimal ldec_DifMontoPrincipal = 0;
                    decimal ldec_DifMontoIntereses = 0;

                    decimal ldec_DifIntereses = 0;
                    decimal ldec_DifInteresesMoratorios = 0;
                    decimal ldec_DifCostas = 0;
                    decimal ldec_DifDanoMoral = 0;

                    string lstr_IdExpediente = dr_FilaExpediente["IdExpedienteFK"].ToString();
                    string lstr_IdSociedad = dr_FilaExpediente["IdSociedadGL"].ToString();
                    string lstr_EstadoResolucion = dr_FilaExpediente["EstadoResolucion"].ToString();
                    String lstr_Moneda = dr_FilaExpediente["Moneda"].ToString();
                    lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);

                    DateTime ldt_FchModifica = Convert.ToDateTime(dr_FilaExpediente["FchModifica"].ToString());
                    DateTime ldt_FchCreacion = Convert.ToDateTime(dr_FilaExpediente["FchCreacion"].ToString());
                    #endregion

                    if (lstr_Moneda.Contains("USD") || lstr_Moneda.Contains("EUR") || (lstr_Moneda.Contains("CRC")))
                    {
                        #region carga data
                        Decimal ldec_TipoCambio = Convert.ToDecimal(dr_FilaExpediente["TipoCambio"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["TipoCambio"].ToString());
                        Decimal ldec_Tbp = Convert.ToDecimal(dr_FilaExpediente["Tbp"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tbp"].ToString());
                        Decimal ldec_Tiempo = Convert.ToDecimal(dr_FilaExpediente["Tiempo"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tiempo"].ToString());
                        Decimal ldec_TipoCambioCierre = Convert.ToDecimal(dr_FilaExpediente["TipoCambioCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["TipoCambioCierre"].ToString());

                        Decimal ldec_MontoPrincipal = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipal"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipal"]);
                        Decimal ldec_MontoPrincipalColones = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColones"]);
                        Decimal ldec_MontoPrincipalCierre = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColonesCierre"]);

                        Decimal ldec_MontoIntereses = Convert.ToDecimal(dr_FilaExpediente["MontoIntereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoIntereses"]);
                        Decimal ldec_MontoInteresesColones = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColones"]);
                        Decimal ldec_MontoInteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColonesCierre"]);

                        Decimal ldec_InteresesMoratorios = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratorios"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratorios"]);
                        Decimal ldec_InteresesMoratoriosColones = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColones"]);
                        Decimal ldec_InteresesMoratoriosColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColonesCierre"]);

                        Decimal ldec_Intereses = Convert.ToDecimal(dr_FilaExpediente["Intereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Intereses"]);
                        Decimal ldec_InteresesColones = Convert.ToDecimal(dr_FilaExpediente["InteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColones"]);
                        Decimal ldec_InteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColonesCierre"]);

                        Decimal ldec_Costas = Convert.ToDecimal(dr_FilaExpediente["Costas"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Costas"]);
                        Decimal ldec_CostasColones = Convert.ToDecimal(dr_FilaExpediente["CostasColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColones"]);
                        Decimal ldec_CostasColonesCierre = Convert.ToDecimal(dr_FilaExpediente["CostasColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColonesCierre"]);

                        Decimal ldec_DanoMoral = Convert.ToDecimal(dr_FilaExpediente["DanoMoral"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoral"]);
                        Decimal ldec_DanoMoralColones = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColones"]);
                        Decimal ldec_DanoMoralColonesCierre = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColonesCierre"]);

                        Decimal ldec_MontoPrincipalAnterior = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalAnterior"]);
                        Decimal ldec_MontoInteresesAnterior = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesAnterior"]);
                        Decimal ldec_InteresesAnterior = Convert.ToDecimal(dr_FilaExpediente["InteresesAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesAnterior"]);
                        Decimal ldec_InteresesMoratoriosAnterior = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosAnterior"]);
                        Decimal ldec_CostasAnterior = Convert.ToDecimal(dr_FilaExpediente["CostasAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasAnterior"]);
                        Decimal ldec_DanoMoralAnterior = Convert.ToDecimal(dr_FilaExpediente["DanoMoralAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralAnterior"]);
                        #endregion

                        #region tipos cambio
                        lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);
                        str_Mensaje += lstr_TipoExpediente + Environment.NewLine;

                        if (lstr_Moneda.Contains("CRC"))
                            ldec_TipoCambioActual = 1;
                        else if (lstr_Moneda.Contains("USD"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual;
                        }
                        else if (lstr_Moneda.Contains("EUR"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual * ldec_EuroActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual * ldec_EuroActual;
                        }
                        #endregion

                        #region Cálculo Incobrabilidad

                        larrdec_MontosArriba = new Decimal[5];
                        larrdec_MontosAbajo = new Decimal[5];

                        TimeSpan lts_DiferenciaTiempo = DateTime.Today - ldt_FchModifica;

                        int lint_DiferenciaDias = int.Parse(txtbox_cantdias.Text);//lts_DiferenciaTiempo.Days;
                        double lint_Porcentaje = 0;

                        #region Porcentajes Previsiones
                        if (lint_DiferenciaDias >= 1440)
                        {
                            lint_Porcentaje = 1;
                        }
                        else if (lint_DiferenciaDias >= 1260)
                        {
                            lint_Porcentaje = 0.875;
                        }
                        else if (lint_DiferenciaDias >= 1080)
                        {
                            lint_Porcentaje = 0.75;
                        }
                        else if (lint_DiferenciaDias >= 900)
                        {
                            lint_Porcentaje = 0.675;
                        }
                        else if (lint_DiferenciaDias >= 720)
                        {
                            lint_Porcentaje = 0.5;
                        }
                        else if (lint_DiferenciaDias >= 540)
                        {
                            lint_Porcentaje = 0.375;
                        }
                        else if (lint_DiferenciaDias >= 360)
                        {
                            lint_Porcentaje = 0.25;
                        }
                        else if (lint_DiferenciaDias >= 180)
                        {
                            lint_Porcentaje = 0.1;
                        }
                        else if (lint_DiferenciaDias >= 45)
                        {
                            lint_Porcentaje = 0.05;
                        }
                        else if (lint_DiferenciaDias >= 30)
                        {
                            lint_Porcentaje = 0.03;
                        }
                        #endregion

                        if (lstr_TipoExpediente.Contains("Actor") && (lint_DiferenciaDias >= 30) && lstr_IdSociedad.Contains(gstr_IdSociedadGL) &&
                            (lstr_EstadoResolucion.Contains("En Firme") || lstr_EstadoResolucion.Contains("Liquidacion")))
                        {
                            #region Actor RF Liq
                            lint_CantidadLineasAsiento = 8;

                            if (lstr_EstadoResolucion.Contains("En Firme"))
                            {
                                ldec_MontoPrincipalAnterior = (ldec_MontoPrincipalCierre * Convert.ToDecimal(lint_Porcentaje)) - ldec_MontoPrincipalAnterior;
                                ldec_MontoInteresesAnterior = (ldec_MontoInteresesColonesCierre * Convert.ToDecimal(lint_Porcentaje)) - ldec_MontoInteresesAnterior;

                                if (ldec_MontoPrincipalAnterior < 0)
                                {
                                    ldec_MontoPrincipalAnterior = ldec_MontoPrincipalAnterior * -1;
                                    larrdec_MontosAbajo[0] = ldec_MontoPrincipalAnterior;
                                }
                                else
                                    larrdec_MontosArriba[0] = ldec_MontoPrincipalAnterior;

                                if (ldec_MontoInteresesAnterior < 0)
                                {
                                    ldec_MontoInteresesAnterior = ldec_MontoInteresesAnterior * -1;
                                    larrdec_MontosAbajo[1] = ldec_MontoInteresesAnterior;
                                }
                                else
                                    larrdec_MontosArriba[1] = ldec_MontoInteresesAnterior;

                            }
                            else
                            {
                                ldec_InteresesAnterior = ((ldec_Intereses * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_InteresesAnterior;
                                ldec_InteresesMoratoriosAnterior = ((ldec_InteresesMoratorios * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_InteresesMoratoriosAnterior;
                                ldec_CostasAnterior = ((ldec_Costas * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_CostasAnterior;
                                ldec_DanoMoralAnterior = ((ldec_DanoMoral * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_DanoMoralAnterior;

                                if (ldec_InteresesAnterior < 0)
                                {
                                    ldec_InteresesAnterior = ldec_InteresesAnterior * -1;
                                    larrdec_MontosAbajo[1] = ldec_InteresesAnterior;
                                }
                                else
                                    larrdec_MontosArriba[1] = ldec_InteresesAnterior;

                                if (ldec_InteresesMoratoriosAnterior < 0)
                                {
                                    ldec_InteresesMoratoriosAnterior = ldec_InteresesMoratoriosAnterior * -1;
                                    larrdec_MontosAbajo[2] = ldec_InteresesMoratoriosAnterior;
                                }
                                else
                                    larrdec_MontosArriba[2] = ldec_InteresesMoratoriosAnterior;

                                if (ldec_CostasAnterior < 0)
                                {
                                    ldec_CostasAnterior = ldec_CostasAnterior * -1;
                                    larrdec_MontosAbajo[3] = ldec_CostasAnterior;
                                }
                                else
                                    larrdec_MontosArriba[3] = ldec_CostasAnterior;

                                if (ldec_DanoMoralAnterior < 0)
                                {
                                    ldec_DanoMoralAnterior = ldec_DanoMoralAnterior * -1;
                                    larrdec_MontosAbajo[4] = ldec_DanoMoralAnterior;
                                }
                                else
                                    larrdec_MontosArriba[4] = ldec_DanoMoralAnterior;

                            }

                            

                            Int32 lint_IdExp = 0;

                            String lstr_query = "SELECT IdExp FROM co.Expedientes exp " +
                                "WHERE exp.IdExpediente ='" + lstr_IdExpediente + "' " +
                                "AND exp.IdSociedadGL ='" + lstr_IdSociedad + "' " +
                                "AND exp.EstadoExpediente = 'Activo'";
                            //"AND cp.EstadoTransaccion != 'estadotran'";

                            DataTable dt_Resoluciones = GetData(lstr_query);

                            foreach (DataRow dr_Resolucion in dt_Resoluciones.Rows)
                            {
                                lint_IdExp = Convert.ToInt32(dr_Resolucion["IdExp"]);
                            }

                            if (larrdec_MontosAbajo != null)
                            {
                                for (int i = 0; i < larrdec_MontosAbajo.Count(); i++)
                                {
                                    if (larrdec_MontosAbajo[i] != null)
                                        larrdec_MontosAbajo[i] = Math.Round(larrdec_MontosAbajo[i], 2);
                                }
                            }
                            String CT = ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) ? "CT35" : "CT34";
                            lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, CT, lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                            if (lstr_Resultado.Contains("Contabilizado"))
                            {
                                ws_SGService.uwsRegistrarCobrosPagos(CT,
                                lstr_IdExpediente, lint_IdExp, "REV", 0, 0,
                                0, 0,
                                larrdec_MontosAbajo == null ? 0 : larrdec_MontosAbajo[0],
                                larrdec_MontosAbajo == null ? 0 : larrdec_MontosAbajo[1],
                                larrdec_MontosAbajo == null ? 0 : larrdec_MontosAbajo[2],
                                larrdec_MontosAbajo == null ? 0 : larrdec_MontosAbajo[3],
                                larrdec_MontosAbajo == null ? 0 : larrdec_MontosAbajo[4],
                                0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0,//Intereses
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0, 0,//Anteriores

                                "tipotra", "estadotran", DateTime.Today, "Reversion", null);
                            }

                            if (larrdec_MontosArriba != null)
                            {
                                for (int i = 0; i < larrdec_MontosArriba.Count(); i++)
                                {
                                    if (larrdec_MontosArriba[i] != null)
                                        larrdec_MontosArriba[i] = Math.Round(larrdec_MontosArriba[i], 2);
                                }
                            }

                            lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT13", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);
                            
                            if (lstr_Resultado.Contains("Contabilizado"))
                            {
                                ws_SGService.uwsRegistrarCobrosPagos("CT13",
                                lstr_IdExpediente, lint_IdExp, "REV", 0, 0,
                                0, 0,
                                larrdec_MontosArriba == null ? 0 : larrdec_MontosArriba[0],
                                larrdec_MontosArriba == null ? 0 : larrdec_MontosArriba[1],
                                larrdec_MontosArriba == null ? 0 : larrdec_MontosArriba[2],
                                larrdec_MontosArriba == null ? 0 : larrdec_MontosArriba[3],
                                larrdec_MontosArriba == null ? 0 : larrdec_MontosArriba[4],
                                0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0,//Intereses
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0, 0,//Anteriores

                                "tipotra", "estadotran", DateTime.Today, "Reversion", null);
                            }
                            
                            #endregion

                            Int32 lint_IdRes = ConsultarIdRes(lstr_IdExpediente, lstr_IdSociedad, lstr_EstadoResolucion);

                            ldec_MontoPrincipalAnterior = Math.Round((ldec_MontoPrincipalCierre * Convert.ToDecimal(lint_Porcentaje)),2);
                            ldec_MontoInteresesAnterior = Math.Round((ldec_MontoInteresesColonesCierre * Convert.ToDecimal(lint_Porcentaje)),2);
                            ldec_InteresesAnterior = Math.Round(((ldec_Intereses * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)),2);
                            ldec_InteresesMoratoriosAnterior = Math.Round(((ldec_InteresesMoratorios * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)), 2);
                            ldec_CostasAnterior = Math.Round(((ldec_Costas * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)),2);
                            ldec_DanoMoralAnterior = Math.Round(((ldec_DanoMoral * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)),2);

                            try
                            {
                                #region Registro
                                lstr_ResultadoRegistro = ws_SGService.uwsModificarCobrosPagos(
                                    lstr_IdExpediente, lstr_IdSociedad, lint_IdRes, lstr_Moneda, ldec_TipoCambioActual,
                                    ldec_Tbp, ldec_Tiempo, ldec_TipoCambioCierre,//cambio de posicion 
                                    ldec_MontoPrincipal, ldec_MontoPrincipalColones, ldec_MontoPrincipalCierre,
                                    ldec_MontoIntereses, ldec_MontoInteresesColones, ldec_MontoInteresesColonesCierre,
                                    0, 0, 0,
                                    0, 0, 0,
                                    ldec_Intereses, ldec_InteresesColones, ldec_InteresesColonesCierre,
                                    ldec_InteresesMoratorios, ldec_InteresesMoratoriosColones, ldec_InteresesMoratoriosColonesCierre,
                                    ldec_Costas, ldec_CostasColones, ldec_CostasColonesCierre,
                                    ldec_DanoMoral, ldec_DanoMoralColones, ldec_DanoMoralColonesCierre,
                                    ldec_MontoPrincipalAnterior, ldec_MontoInteresesAnterior,
                                    ldec_InteresesAnterior, ldec_CostasAnterior,
                                    ldec_InteresesMoratoriosAnterior, ldec_DanoMoralAnterior,
                                    "d", "g");
                                guardarPrevision(lint_IdRes,lstr_IdExpediente,lint_DiferenciaDias,(float)(ldec_MontoPrincipalAnterior+
                                    ldec_MontoInteresesAnterior+ldec_InteresesAnterior+ldec_InteresesMoratoriosAnterior+
                                    ldec_CostasAnterior + ldec_DanoMoralAnterior),(float)( ldec_MontoPrincipalCierre + ldec_MontoInteresesColonesCierre
                                    + (ldec_Intereses * ldec_TipoCambioCierre) + (ldec_InteresesMoratorios * ldec_TipoCambioCierre) +
                                    (ldec_Costas * ldec_TipoCambioCierre) + (ldec_DanoMoral * ldec_TipoCambioCierre))
                                    , (float)(lint_Porcentaje * 100), gstr_Usuario);


                                if (lstr_ResultadoRegistro[0].Contains("00"))
                                {
                                    lstr_Resultado[0] = "exito";
                                    str_Mensaje += "Expediente " + lstr_IdExpediente + " modificado con éxito." + Environment.NewLine;
                                }
                                else
                                {
                                    lstr_Resultado[0] = "fallo";
                                    str_Mensaje += "Fallo al modificar expediente " + lstr_IdExpediente + ". \n" + lstr_ResultadoRegistro[1] + Environment.NewLine;
                                }
                                #endregion
                            }
                            catch { }
                            str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                        }

                        #endregion
                    }
                }

                str_Mensaje += "Fin de Proceso\n";
                str_Mensaje += "------------------------------------------------" + Environment.NewLine;

                GuardarResultadoContabilizacion();

            }
        } 

    }

}