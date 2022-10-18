using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Globalization;
using Presentacion.Compartidas;
using System.Web.UI.HtmlControls;
using System.Configuration;
//using System.Data.SqlClient;
using Logica.SubirArchivo;
using System.Text;
using System.IO;
using Presentacion.Contingentes.ArchivosCO;


namespace Presentacion.Contingentes
{
    public partial class Resoluciones : BASE
    {
         #region variables
        private Presentacion.wsSG.wsSistemaGestor ws_SG = new Presentacion.wsSG.wsSistemaGestor();
        private Presentacion.wsAsientos.ServicioContable ws_ContabilizaAsientos = new Presentacion.wsAsientos.ServicioContable();

        //private String gstr_Modulo = "CT";
        //private String gstr_Usuario = String.Empty;
        protected String gstr_Usuario
        {
            get
            {
                if (ViewState["gstr_Usuario"] == null)
                    ViewState["gstr_Usuario"] = null;
                return (String)ViewState["gstr_Usuario"];
            }
            set
            {
                ViewState["gstr_Usuario"] = value;
            }
        }
        private clsArchivoSubir utilidad = new clsArchivoSubir();

        //private Boolean gbool_SinLugar;
        protected Boolean gbool_SinLugar
        {
            get
            {
                if (ViewState["gbool_SinLugar"] == null)
                    ViewState["gbool_SinLugar"] = false;
                return (Boolean)ViewState["gbool_SinLugar"];
            }
            set
            {
                ViewState["gbool_SinLugar"] = value;
            }
        }
        //private Boolean gbool_Incobrable;
        protected Boolean gbool_Incobrable
        {
            get
            {
                if (ViewState["gbool_Incobrable"] == null)
                    ViewState["gbool_Incobrable"] = false;
                return (Boolean)ViewState["gbool_Incobrable"];
            }
            set
            {
                ViewState["gbool_Incobrable"] = value;
            }
        }
        //private Boolean gbool_Lleno;
        protected Boolean gbool_Lleno
        {
            get
            {
                if (ViewState["gbool_Lleno"] == null)
                    ViewState["gbool_Lleno"] = false;
                return (Boolean)ViewState["gbool_Lleno"];
            }
            set
            {
                ViewState["gbool_Lleno"] = value;
            }
        }
        //private Boolean gbool_MonedaExtrangera;
        protected Boolean gbool_MonedaExtrangera
        {
            get
            {
                if (ViewState["gbool_MonedaExtrangera"] == null)
                    ViewState["gbool_MonedaExtrangera"] = false;
                return (Boolean)ViewState["gbool_MonedaExtrangera"];
            }
            set
            {
                ViewState["gbool_MonedaExtrangera"] = value;
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
        //private String gstr_TipoExpediente = String.Empty;
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

        //private String gstr_IdResolucion = String.Empty;
        protected String gstr_IdResolucion
        {
            get
            {
                if (ViewState["gstr_IdResolucion"] == null)
                    ViewState["gstr_IdResolucion"] = null;
                return (String)ViewState["gstr_IdResolucion"];
            }
            set
            {
                ViewState["gstr_IdResolucion"] = value;
            }
        }
        //private String str_idCuentaContable = String.Empty;
        protected String str_idCuentaContable
        {
            get
            {
                if (ViewState["str_idCuentaContable"] == null)
                    ViewState["str_idCuentaContable"] = null;
                return (String)ViewState["str_idCuentaContable"];
            }
            set
            {
                ViewState["str_idCuentaContable"] = value;
            }
        }
        //private String str_idPosPre = String.Empty;
        protected String str_idPosPre
        {
            get
            {
                if (ViewState["str_idPosPre"] == null)
                    ViewState["str_idPosPre"] = null;
                return (String)ViewState["str_idPosPre"];
            }
            set
            {
                ViewState["str_idPosPre"] = value;
            }
        }

        //private String gstr_ResolucionExp = String.Empty;
        protected String gstr_ResolucionExp
        {
            get
            {
                if (ViewState["gstr_ResolucionExp"] == null)
                    ViewState["gstr_ResolucionExp"] = null;
                return (String)ViewState["gstr_ResolucionExp"];
            }
            set
            {
                ViewState["gstr_ResolucionExp"] = value;
            }
        }

        //private String gstr_Transaccion = "Normal";
        protected String gstr_Transaccion
        {
            get
            {
                if (ViewState["gstr_Transaccion"] == null)
                    ViewState["gstr_Transaccion"] = "Normal";
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
                    ViewState["gstr_Leyenda"] = String.Empty;
                return (String)ViewState["gstr_Leyenda"];
            }
            set
            {
                ViewState["gstr_Leyenda"] = value;
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
        //private String gstr_Sociedad = String.Empty;
        protected String gstr_Sociedad
        {
            get
            {
                if (ViewState["gstr_Sociedad"] == null)
                    ViewState["gstr_Sociedad"] = String.Empty;
                return (String)ViewState["gstr_Sociedad"];
            }
            set
            {
                ViewState["gstr_Sociedad"] = value;
            }
        }
        //private String gstr_UsuarioModifica = String.Empty;
        protected String gstr_UsuarioModifica
        {
            get
            {
                if (ViewState["gstr_UsuarioModifica"] == null)
                    ViewState["gstr_UsuarioModifica"] = String.Empty;
                return (String)ViewState["gstr_UsuarioModifica"];
            }
            set
            {
                ViewState["gstr_UsuarioModifica"] = value;
            }
        }
        //private String gstr_Observaciones = String.Empty;
        protected String gstr_Observaciones
        {
            get
            {
                if (ViewState["gstr_Observaciones"] == null)
                    ViewState["gstr_Observaciones"] = String.Empty;
                return (String)ViewState["gstr_Observaciones"];
            }
            set
            {
                ViewState["gstr_Observaciones"] = value;
            }
        }

        //private String gstr_FechaResolucion;
        protected String gstr_FechaResolucion
        {
            get
            {
                if (ViewState["gstr_FechaResolucion"] == null)
                    ViewState["gstr_FechaResolucion"] = String.Empty;
                return (String)ViewState["gstr_FechaResolucion"];
            }
            set
            {
                ViewState["gstr_FechaResolucion"] = value;
            }
        }
        //private String gstr_PosibleFechaSalida;
        protected String gstr_PosibleFechaSalida
        {
            get
            {
                if (ViewState["gstr_PosibleFechaSalida"] == null)
                    ViewState["gstr_PosibleFechaSalida"] = DateTime.Now.ToString("dd/MM/yyyy");
                return (String)ViewState["gstr_PosibleFechaSalida"];
            }
            set
            {
                ViewState["gstr_PosibleFechaSalida"] = value;
            }
        }

        //private String gstr_Moneda = String.Empty;
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
        //private Int32 gint_Periodo;
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
        //private Int32 gint_CxCaCxP;
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

        //private Decimal gdec_TipoCambio;
        protected Decimal gdec_TipoCambio
        {
            get
            {
                if (ViewState["gdec_TipoCambio"] == null)
                    ViewState["gdec_TipoCambio"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_TipoCambio"]);
            }
            set
            {
                ViewState["gdec_TipoCambio"] = value;
            }
        }
        //private Decimal gdec_Tbp;
        protected Decimal gdec_Tbp
        {
            get
            {
                if (ViewState["gdec_Tbp"] == null)
                    ViewState["gdec_Tbp"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_Tbp"]);
            }
            set
            {
                ViewState["gdec_Tbp"] = value;
            }
        }
        //private Decimal gdec_Tiempo;
        protected Decimal gdec_Tiempo
        {
            get
            {
                if (ViewState["gdec_Tiempo"] == null)
                    ViewState["gdec_Tiempo"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_Tiempo"]);
            }
            set
            {
                ViewState["gdec_Tiempo"] = value;
            }
        }

        //private Decimal gdec_MontoPrincipal;
        protected Decimal gdec_MontoPrincipal
        {
            get
            {
                if (ViewState["gdec_MontoPrincipal"] == null)
                    ViewState["gdec_MontoPrincipal"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoPrincipal"]);
            }
            set
            {
                ViewState["gdec_MontoPrincipal"] = value;
            }
        }
        //private Decimal gdec_MontoIntereses;
        protected Decimal gdec_MontoIntereses
        {
            get
            {
                if (ViewState["gdec_MontoIntereses"] == null)
                    ViewState["gdec_MontoIntereses"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoIntereses"]);
            }
            set
            {
                ViewState["gdec_MontoIntereses"] = value;
            }
        }
        //private Decimal gdec_MontoPosibleReembolso;
        protected Decimal gdec_MontoPosibleReembolso
        {
            get
            {
                if (ViewState["gdec_MontoPosibleReembolso"] == null)
                    ViewState["gdec_MontoPosibleReembolso"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoPosibleReembolso"]);
            }
            set
            {
                ViewState["gdec_MontoPosibleReembolso"] = value;
            }
        }

        //private Decimal gdec_MontoPrincipalColones;
        protected Decimal gdec_MontoPrincipalColones
        {
            get
            {
                if (ViewState["gdec_MontoPrincipalColones"] == null)
                    ViewState["gdec_MontoPrincipalColones"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoPrincipalColones"]);
            }
            set
            {
                ViewState["gdec_MontoPrincipalColones"] = value;
            }
        }
        //private Decimal gdec_MontoInteresesColones;
        protected Decimal gdec_MontoInteresesColones
        {
            get
            {
                if (ViewState["gdec_MontoInteresesColones"] == null)
                    ViewState["gdec_MontoInteresesColones"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoInteresesColones"]);
            }
            set
            {
                ViewState["gdec_MontoInteresesColones"] = value;
            }
        }
        //private Decimal gdec_MontoPosibleReembolsoColones;
        protected Decimal gdec_MontoPosibleReembolsoColones
        {
            get
            {
                if (ViewState["gdec_MontoPosibleReembolsoColones"] == null)
                    ViewState["gdec_MontoPosibleReembolsoColones"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoPosibleReembolsoColones"]);
            }
            set
            {
                ViewState["gdec_MontoPosibleReembolsoColones"] = value;
            }
        }
        //private Decimal gdec_MontoAjustesPrincipal;
        protected Decimal gdec_MontoAjustesPrincipal
        {
            get
            {
                if (ViewState["gdec_MontoAjustesPrincipal"] == null)
                    ViewState["gdec_MontoAjustesPrincipal"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoAjustesPrincipal"]);
            }
            set
            {
                ViewState["gdec_MontoAjustesPrincipal"] = value;
            }
        }
        //private Decimal gdec_MontoAjustesIntereses;
        protected Decimal gdec_MontoAjustesIntereses
        {
            get
            {
                if (ViewState["gdec_MontoAjustesIntereses"] == null)
                    ViewState["gdec_MontoAjustesIntereses"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoAjustesIntereses"]);
            }
            set
            {
                ViewState["gdec_MontoAjustesIntereses"] = value;
            }
        }

        //private Decimal gdec_ValorPresentePrincipal;//Formula: VP= VF/(1+i)n
        protected Decimal gdec_ValorPresentePrincipal
        {
            get
            {
                if (ViewState["gdec_ValorPresentePrincipal"] == null)
                    ViewState["gdec_ValorPresentePrincipal"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_ValorPresentePrincipal"]);
            }
            set
            {
                ViewState["gdec_ValorPresentePrincipal"] = value;
            }
        }
        //private Decimal gdec_ValorPresenteIntereses;
        protected Decimal gdec_ValorPresenteIntereses
        {
            get
            {
                if (ViewState["gdec_ValorPresenteIntereses"] == null)
                    ViewState["gdec_ValorPresenteIntereses"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_ValorPresenteIntereses"]);
            }
            set
            {
                ViewState["gdec_ValorPresenteIntereses"] = value;
            }
        }

        //private Decimal gdec_ValorPresentePrincipalColones;//Formula: VP= VF/(1+i)n
        protected Decimal gdec_ValorPresentePrincipalColones
        {
            get
            {
                if (ViewState["gdec_ValorPresentePrincipalColones"] == null)
                    ViewState["gdec_ValorPresentePrincipalColones"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_ValorPresentePrincipalColones"]);
            }
            set
            {
                ViewState["gdec_ValorPresentePrincipalColones"] = value;
            }
        }
        //private Decimal gdec_ValorPresenteInteresesColones;//Formula: VP= VF/(1+i)n
        protected Decimal gdec_ValorPresenteInteresesColones
        {
            get
            {
                if (ViewState["gdec_ValorPresenteInteresesColones"] == null)
                    ViewState["gdec_ValorPresenteInteresesColones"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_ValorPresenteInteresesColones"]);
            }
            set
            {
                ViewState["gdec_ValorPresenteInteresesColones"] = value;
            }
        }

        //private Decimal gdec_MontoPrincipalColRev;
        protected Decimal gdec_MontoPrincipalColRev
        {
            get
            {
                if (ViewState["gdec_MontoPrincipalColRev"] == null)
                    ViewState["gdec_MontoPrincipalColRev"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoPrincipalColRev"]);
            }
            set
            {
                ViewState["gdec_MontoPrincipalColRev"] = value;
            }
        }
        //private Decimal gdec_MontoInteresesColRev;
        protected Decimal gdec_MontoInteresesColRev
        {
            get
            {
                if (ViewState["gdec_MontoInteresesColRev"] == null)
                    ViewState["gdec_MontoInteresesColRev"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoInteresesColRev"]);
            }
            set
            {
                ViewState["gdec_MontoInteresesColRev"] = value;
            }
        }

        //private Decimal gdec_MontoPrincipalAnterior;
        protected Decimal gdec_MontoPrincipalAnterior
        {
            get
            {
                if (ViewState["gdec_MontoPrincipalAnterior"] == null)
                    ViewState["gdec_MontoPrincipalAnterior"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoPrincipalAnterior"]);
            }
            set
            {
                ViewState["gdec_MontoPrincipalAnterior"] = value;
            }
        }
        //private Decimal gdec_MontoInteresesAnterior;
        protected Decimal gdec_MontoInteresesAnterior
        {
            get
            {
                if (ViewState["gdec_MontoInteresesAnterior"] == null)
                    ViewState["gdec_MontoInteresesAnterior"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_MontoInteresesAnterior"]);
            }
            set
            {
                ViewState["gdec_MontoInteresesAnterior"] = value;
            }
        }
        //private String gstr_MonedaAnterior;
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
        //private Decimal gdec_TipoCambioAnterior;
        protected Decimal gdec_TipoCambioAnterior
        {
            get
            {
                if (ViewState["gdec_TipoCambioAnterior"] == null)
                    ViewState["gdec_TipoCambioAnterior"] = 0.0;
               
                return Convert.ToDecimal(ViewState["gdec_TipoCambioAnterior"].ToString());
            }
            set
            {
                ViewState["gdec_TipoCambioAnterior"] = value;
            }
        }

        //private Decimal gdec_TbpAnterior;
        protected Decimal gdec_TbpAnterior
        {
            get
            {
                if (ViewState["gdec_TbpAnterior"] == null)
                    ViewState["gdec_TbpAnterior"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_TbpAnterior"]);
            }
            set
            {
                ViewState["gdec_TbpAnterior"] = value;
            }
        }

        //private Decimal gdec_TiempoAnterior;
        protected Decimal gdec_TiempoAnterior
        {
            get
            {
                if (ViewState["gdec_TiempoAnterior"] == null)
                    ViewState["gdec_TiempoAnterior"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_TiempoAnterior"]);
            }
            set
            {
                ViewState["gdec_TiempoAnterior"] = value;
            }
        }

        //private String gstr_ObservacionesAnteriores;
        protected String gstr_ObservacionesAnteriores
        {
            get
            {
                if (ViewState["gstr_ObservacionesAnteriores"] == null)
                    ViewState["gstr_ObservacionesAnteriores"] = null;
                return (String)ViewState["gstr_ObservacionesAnteriores"];
            }
            set
            {
                ViewState["gstr_ObservacionesAnteriores"] = value;
            }
        }

        //private Decimal gdec_InteresesMoratorios;
        protected Decimal gdec_InteresesMoratorios
        {
            get
            {
                if (ViewState["gdec_InteresesMoratorios"] == null)
                    ViewState["gdec_InteresesMoratorios"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_InteresesMoratorios"]);
            }
            set
            {
                ViewState["gdec_InteresesMoratorios"] = value;
            }
        }
        //private Decimal gdec_InteresesMoratoriosColones;
        protected Decimal gdec_InteresesMoratoriosColones
        {
            get
            {
                if (ViewState["gdec_InteresesMoratoriosColones"] == null)
                    ViewState["gdec_InteresesMoratoriosColones"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_InteresesMoratoriosColones"]);
            }
            set
            {
                ViewState["gdec_InteresesMoratoriosColones"] = value;
            }
        }
        //private Decimal gdec_Costas;
        protected Decimal gdec_Costas
        {
            get
            {
                if (ViewState["gdec_Costas"] == null)
                    ViewState["gdec_Costas"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_Costas"]);
            }
            set
            {
                ViewState["gdec_Costas"] = value;
            }
        }
        //private Decimal gdec_CostasColones;
        protected Decimal gdec_CostasColones
        {
            get
            {
                if (ViewState["gdec_CostasColones"] == null)
                    ViewState["gdec_CostasColones"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_CostasColones"]);
            }
            set
            {
                ViewState["gdec_CostasColones"] = value;
            }
        }
        //private Decimal gdec_DAnnoMoral;
        protected Decimal gdec_DAnnoMoral
        {
            get
            {
                if (ViewState["gdec_DAnnoMoral"] == null)
                    ViewState["gdec_DAnnoMoral"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_DAnnoMoral"]);
            }
            set
            {
                ViewState["gdec_DAnnoMoral"] = value;
            }
        }
        //private Decimal gdec_DAnnoMoralColones;
        protected Decimal gdec_DAnnoMoralColones
        {
            get
            {
                if (ViewState["gdec_DAnnoMoralColones"] == null)
                    ViewState["gdec_DAnnoMoralColones"] = 0.0;
                return Convert.ToDecimal(ViewState["gdec_DAnnoMoralColones"]);
            }
            set
            {
                ViewState["gdec_DAnnoMoralColones"] = value;
            }
        }

        //private Int32 gint_EstadoPretension;
        protected Int32 gint_EstadoPretension
        {
            get
            {
                if (ViewState["gint_EstadoPretension"] == null)
                    ViewState["gint_EstadoPretension"] = 0.0;
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

        //Boolean gbool_TienePretensionInicial = false;
        protected Boolean gbool_TienePretensionInicial
        {
            get
            {
                if (ViewState["gbool_TienePretensionInicial"] == null)
                    ViewState["gbool_TienePretensionInicial"] = false;
                return (Boolean)ViewState["gbool_TienePretensionInicial"];
            }
            set
            {
                ViewState["gbool_TienePretensionInicial"] = value;
            }
        }
        //Boolean gbool_TieneRP1 = false;
        protected Boolean gbool_TieneRP1
        {
            get
            {
                if (ViewState["gbool_TieneRP1"] == null)
                    ViewState["gbool_TieneRP1"] = false;
                return (Boolean)ViewState["gbool_TieneRP1"];
            }
            set
            {
                ViewState["gbool_TieneRP1"] = value;
            }
        }
        //Boolean gbool_TieneRP2 = false;
        protected Boolean gbool_TieneRP2
        {
            get
            {
                if (ViewState["gbool_TieneRP2"] == null)
                    ViewState["gbool_TieneRP2"] = false;
                return (Boolean)ViewState["gbool_TieneRP2"];
            }
            set
            {
                ViewState["gbool_TieneRP2"] = value;
            }
        }
        //Boolean gbool_TieneRF = false;
        protected Boolean gbool_TieneRF
        {
            get
            {
                if (ViewState["gbool_TieneRF"] == null)
                    ViewState["gbool_TieneRF"] = false;
                return (Boolean)ViewState["gbool_TieneRF"];
            }
            set
            {
                ViewState["gbool_TieneRF"] = value;
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

        DataSet lds_ConsultarResolucion = new DataSet();

        Decimal ldec_PrincipalPrevision = 0;
        Decimal ldec_InteresesPrevision = 0;

        Decimal ldec_PrincipalCierre = 0;
        Decimal ldec_InteresesCierre = 0;

        Int32 lint_IdRes = 0;
        Int32 lint_Mes = 0;

        Boolean lbool_Prevision = false;

        Decimal ldec_TipoCambioCierre = 0;

        private String gstr_MensajeResultadoResoluciones = String.Empty;

        private Asiento asiento = new Asiento();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.lnkEliminar);

            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gstr_Sociedad = clsSesion.Current.SociedadUsr;
            gstr_IdExpediente = this.DDLExpedientes.SelectedValue;
            //System.Web.UI.ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkEliminar);
            string idResolucion = Request.QueryString["id"]; //IDExpediente
            Session["IdResol"] = idResolucion;
            ViewState["IdResolucion"] = Session["IdResol"];
            string idRes = Convert.ToString(ViewState["IdResolucion"]);


            string str_Estado = Request.QueryString["Est"];

            string Nuevo = Request.QueryString["isAdd"]; //Modificado
            Session["isAdd"] = Nuevo; 
            ViewState["Nuevo"] = Session["isAdd"];
            bool strNuevo = Convert.ToBoolean(ViewState["Nuevo"]);

            gstr_ResolucionExp = Request.QueryString["Est"]; //Estado de la resolucion

            if (String.IsNullOrEmpty(gstr_IdExpediente))
                gstr_IdExpediente = DDLExpedientes.SelectedValue;

            
            string[] tipocambio = new string[4];

            if (!IsPostBack)
            {
                CargarDDLs();
                var x = DDLMoneda.DataSource;

                gstr_TipoExpediente = ConsultarTipoExpediente(gstr_IdExpediente);
                gdec_TipoCambio = this.txtCompra.Text == "" ? 0 : Convert.ToDecimal(this.txtCompra.Text);
                if (this.DDLMoneda.SelectedItem.Value.Contains("USD"))
                {
                    if (gstr_TipoExpediente.Contains("Actor"))
                        gdec_TipoCambio = this.txtCompra.Text == "" ? 0 : Convert.ToDecimal(this.txtCompra.Text);
                    else if (gstr_TipoExpediente.Contains("Demandado"))
                        gdec_TipoCambio = this.txtVenta.Text == "" ? 0 : Convert.ToDecimal(this.txtVenta.Text);
                }
                else if (this.DDLMoneda.SelectedItem.Value.Contains("EUR"))
                {
                    if (gstr_TipoExpediente.Contains("Actor"))
                    {
                        Decimal ldec_compra = this.txtCompra.Text == "" ? 0 : Convert.ToDecimal(this.txtCompra.Text);
                        Decimal ldec_euro = this.txtEuro.Text == "" ? 0 : Convert.ToDecimal(this.txtEuro.Text);
                        gdec_TipoCambio = ldec_compra * ldec_euro;
                    }
                    else if (gstr_TipoExpediente.Contains("Demandado"))
                    {
                        Decimal ldec_venta = this.txtVenta.Text == "" ? 0 : Convert.ToDecimal(this.txtVenta.Text);
                        Decimal ldec_euro = this.txtEuro.Text == "" ? 0 : Convert.ToDecimal(this.txtEuro.Text);
                        gdec_TipoCambio = ldec_venta * ldec_euro;
                    }
                }
                else
                    gdec_TipoCambio = 1;

                gdec_TipoCambio = Math.Round(gdec_TipoCambio, 2);

                //this.DDLExpedientes.Items.Insert(0, new ListItem("", "")); //Buscar expediente combobox
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!string.IsNullOrEmpty(str_Estado))
                        if(str_Estado.Contains("Liquidacion"))
                        {
                            this.Response.Redirect("~/Contingentes/Liquidacion.aspx?id=" + idResolucion, true);
                        }

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CT"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        this.txtFechaResolucion.Text =
                        //this.txtFechSalidaRecur.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        this.txtFechSalidaRecur.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                        //CargarExpedientes();
                        tipocambio = CargarIndicadoresEco();
                        this.txtCompra.Text = tipocambio[0];
                        this.txtVenta.Text = tipocambio[1];
                        this.txtEuro.Text = tipocambio[2];
                        this.txtTBP.Text = tipocambio[3];

                        if (!strNuevo)//Viene del select del grid a modificar
                        {
                            ObtenerResolucionAModificar(idRes, gstr_ResolucionExp);
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
            //oculta las filas que hacen referencia a intereses
            this.rIntereses.Visible = false;
            this.rColonIntereses.Visible = false;
            this.rVPIntereses.Visible = false;
        }

        private void CargarDDLs()
        {
            DataSet lds_Monedas = ws_SG.uwsConsultarDinamico("SELECT rtrim(IdMoneda) as IdMoneda, rtrim (IdMoneda+' - '+NomMoneda) as Nombre from [ma].[Monedas] where IdMoneda IN ('USD', 'CRC' ,'EUR')");
            this.DDLMoneda.Items.Clear();
            if (lds_Monedas.Tables.Count > 0 && (DDLMoneda.Items.Count == 0))
            {
                DataTable ldt_Monedas = lds_Monedas.Tables["Table"];
                DDLMoneda.DataSource = ldt_Monedas;
                DDLMoneda.DataTextField = "Nombre";
                DDLMoneda.DataValueField = "IdMoneda";
                DDLMoneda.Items.Insert(0, new ListItem("--- Elegir Moneda---", "0"));
                DDLMoneda.DataBind();
                for (int i=0; i<DDLMoneda.Items.Count;i++){
                    DDLMoneda.Items[i].Value = DDLMoneda.Items[i].Value.Trim();
                }
            }
            String str_consul = "SELECT IdExpediente,IdExpediente+'-'+TipoExpediente AS NomExpediente FROM co.Expedientes where co.Expedientes.EstadoExpediente='Activo' and IdSociedadGL='" + clsSesion.Current.SociedadUsr + "'";
            DataSet lds_Expedientes = ws_SG.uwsConsultarDinamico(str_consul);
            this.DDLExpedientes.Items.Clear();
            if (lds_Expedientes.Tables.Count > 0)// && (DDLExpedientes.Items.Count == 0))
            {
                DataTable ldt_Expedientes = lds_Expedientes.Tables["Table"];
                DDLExpedientes.DataSource = ldt_Expedientes;
                DDLExpedientes.DataTextField = "NomExpediente";
                DDLExpedientes.DataValueField = "IdExpediente";
                DDLExpedientes.Items.Insert(0, new ListItem("--- Elegir Número Expediente---", "0"));
                DDLExpedientes.DataBind();
                //for (int i = 0; i < DDLMoneda.Items.Count; i++)
                //{
                //    DDLMoneda.Items[i].Value = DDLMoneda.Items[i].Value.Trim();
                //}
            }

            DataSet lds_EstadosProcesales = ws_SG.uwsConsultarDinamico("SELECT IdCatalogo, IdOpcion, ValOpcion, NomOpcion, Estado, UsrCreacion, FchCreacion, UsrModifica, FchModifica FROM ma.OpcionesCatalogos WHERE (IdCatalogo = '34') AND Estado = 'A' ");
            this.DDLEstadoProcesal.Items.Clear();
            if (lds_EstadosProcesales.Tables.Count > 0)// && (DDLExpedientes.Items.Count == 0))
            {
                DataTable ldt_EstadosProcesales = lds_EstadosProcesales.Tables["Table"];
                DDLEstadoProcesal.DataSource = ldt_EstadosProcesales;
                DDLEstadoProcesal.DataTextField = "NomOpcion";
                DDLEstadoProcesal.DataValueField = "NomOpcion";
                DDLEstadoProcesal.Items.Insert(0, new ListItem("--- Elegir Estado Procesal---", "0"));
                DDLEstadoProcesal.DataBind();
                //for (int i = 0; i < DDLMoneda.Items.Count; i++)
                //{
                //    DDLMoneda.Items[i].Value = DDLMoneda.Items[i].Value.Trim();
                //}
            }
        }

        private string[] CargarIndicadoresEco()
        {
            DataSet resultCompraUSD = new DataSet();
            DataSet resultVentaUSD = new DataSet();
            DataSet resultCompraEU = new DataSet();
            DataSet resultVentaEU = new DataSet();
            DataSet resultTBP = new DataSet();
            String[] resultado = new String[4];

            resultCompraUSD = ws_SG.uwsConsultarTiposCambio("CRCN", DateTime.Today, "3280", "N");//compra antes: 317
            resultVentaUSD = ws_SG.uwsConsultarTiposCambio("CRCN", DateTime.Today, "3140", "N");//venta antes: 318
            resultCompraEU = ws_SG.uwsConsultarTiposCambio("EUR", DateTime.Now, "333", "N");//euro
            resultTBP = ws_SG.uwsConsultarValoresIndicadoresEco("TBP", DateTime.Now, "N");//TBP

            resultado[0] = String.Format("{0:0.00}", resultCompraUSD.Tables[0].Rows.Count > 0 ? resultCompraUSD.Tables[0].Rows[0]["Valor"] : "00.00"); //condition ? first_expression : second_expression;
            resultado[1] = String.Format("{0:0.00}", resultVentaUSD.Tables[0].Rows.Count > 0 ? resultVentaUSD.Tables[0].Rows[0]["Valor"] : "00.00");
            resultado[2] = String.Format("{0:0.00}", resultCompraEU.Tables[0].Rows.Count > 0 ? resultCompraEU.Tables[0].Rows[0]["Valor"] : "00.00");
            resultado[3] = String.Format("{0:0.00}", resultTBP.Tables[0].Rows.Count > 0 ? resultTBP.Tables[0].Rows[0]["Valor"] : "00.00");
            return resultado;

        }

        protected void DDLExpedientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            gstr_IdExpediente = this.DDLExpedientes.SelectedValue;
            AjustarEtiquetas();
            LimpiarCampos();
            this.ddlEstadoResol.SelectedValue = "0";
            
            DataSet lds_ArchivosExpediente = ws_SG.uwsObtenerArchivoPorIdResolucion(this.DDLExpedientes.SelectedValue, gstr_Sociedad, ConsultarIdExpediente(this.DDLExpedientes.SelectedValue.ToString()));
            
            if (lds_ArchivosExpediente.Tables.Count > 0)
            {
                gvFiles.DataSource = lds_ArchivosExpediente;
                gvFiles.DataBind();
            }
            else
            {
                //Log.Error("Consulta no arrojó resultados, la tabla viene vacia al realizar la consulta para uwsObtenerArchivoPorIdResolucion.");
            }
            
           
          //this.up_DatosPrincipales.Update();
        }

        protected void ddlEstadoResol_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean lbool_existe = ObtenerResolucionAModificar(DDLExpedientes.SelectedValue, ddlEstadoResol.SelectedValue);
            
            if (!lbool_existe)
            {
                LimpiarCampos();
            }

            this.up_DatosMontos.Update();
            CambioTipoCambioAnterior();
        }

        protected void DDLMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            setLabels();
        }

        private void setLabels()
        {
            decimal venta = Convert.ToDecimal(this.txtVenta.Text);
            decimal compra = Convert.ToDecimal(this.txtCompra.Text);
            decimal euro = Convert.ToDecimal(this.txtEuro.Text);
            //decimal tiempoN = txtTiempo.Text.Contains("") ? 1 : Convert.ToDecimal(txtTiempo.Text);
            decimal tbp = Convert.ToDecimal(txtTBP.Text);
            decimal montoprincipal = this.txtMontoPrincipal.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoPrincipal.Text);
            decimal montointereses = txtMontoIntereses.Text == "" ? 0 : Convert.ToDecimal(txtMontoIntereses.Text);
            decimal montodesembolso = txtMontoPosDesembolso.Text == "" ? 0 : Convert.ToDecimal(txtMontoPosDesembolso.Text);

            if (!this.DDLExpedientes.SelectedItem.Value.Equals("0"))
            {
                gstr_IdExpediente = this.DDLExpedientes.SelectedItem.Value;
            }

            //}
            this.txtMontoPosDesembolso.Text = montodesembolso.ToString("N2");
            this.txtMontoPrincipal.Text = montoprincipal.ToString("N2");
            this.txtMontoIntereses.Text = montointereses.ToString("N2");

            //Segundo moneda seleccionada se da la variacion del monto
            if (this.DDLMoneda.SelectedItem.Value.Contains("USD"))
            {
                if (ConsultarTipoExpediente(gstr_IdExpediente).Contains("Actor"))
                {
                    txtMontoColonesPrincipal.Text = (montoprincipal * compra).ToString("N2");
                    gdec_MontoPrincipalColones = decimal.Parse(this.txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    txtMontoColonesIntereses.Text = (montointereses * compra).ToString("N2");
                    gdec_MontoInteresesColones = decimal.Parse(txtMontoColonesIntereses.Text, NumberStyles.AllowThousands | NumberStyles.Number);
                    gdec_TipoCambio = compra;
                }
                else if (ConsultarTipoExpediente(gstr_IdExpediente).Contains("Demandado"))
                {
                    txtMontoColonesPrincipal.Text = (montoprincipal * venta).ToString("N2");
                    gdec_MontoPrincipalColones = decimal.Parse(this.txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    txtMontoColonesIntereses.Text = (montointereses * venta).ToString("N2");
                    gdec_MontoInteresesColones = decimal.Parse(txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    gdec_TipoCambio = venta;

                }
            }
            else if (this.DDLMoneda.SelectedItem.Value.Contains("EUR"))
            {
                if (ConsultarTipoExpediente(gstr_IdExpediente).Contains("Actor"))
                {
                    //this.txtMontoPrincipal.Text = montoprincipal.ToString("N2");
                    //this.txtMontoIntereses.Text = montointereses.ToString("N2");
                    txtMontoColonesPrincipal.Text = (montoprincipal * Math.Round(compra * euro, 2)).ToString("N2");
                    gdec_MontoPrincipalColones = decimal.Parse(txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    txtMontoColonesIntereses.Text = (montointereses * Math.Round(euro * compra, 2)).ToString("N2");
                    gdec_MontoInteresesColones = decimal.Parse(txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    gdec_TipoCambio = compra * euro;

                }
                else if (ConsultarTipoExpediente(gstr_IdExpediente).Contains("Demandado"))
                {
                    //this.txtMontoPrincipal.Text = montoprincipal.ToString("N2");
                    //this.txtMontoIntereses.Text = montointereses.ToString("N2");
                    txtMontoColonesPrincipal.Text = (montoprincipal * Math.Round(euro * venta, 2)).ToString("N2");
                    gdec_MontoPrincipalColones = decimal.Parse(txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    txtMontoColonesIntereses.Text = (montointereses * Math.Round(euro * venta, 2)).ToString("N2");
                    gdec_MontoInteresesColones = decimal.Parse(txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    gdec_TipoCambio = venta * euro;

                }
            }
            else if (this.DDLMoneda.SelectedItem.Value.Contains("CRC"))
            {
                //this.txtMontoPrincipal.Text = montoprincipal.ToString("N2");
                //this.txtMontoIntereses.Text = montointereses.ToString("N2");
                txtMontoColonesPrincipal.Text = (montoprincipal * 1).ToString("N2");
                gdec_MontoPrincipalColones = decimal.Parse(txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                txtMontoColonesIntereses.Text = (montointereses * 1).ToString("N2");
                gdec_MontoInteresesColones = decimal.Parse(txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                gdec_TipoCambio = 1;
            }
            gdec_TipoCambio = Math.Round(gdec_TipoCambio, 2);
            //Sacamos el parametro de Tiempo digitado por contabilidad nacional
            DataSet dataParam = ws_SG.uwsConsultarParametros("CT_Tiempo", "", DateTime.Today, "", "");//str_IdParametro,str_IdModulo,dt_FchVigencia,str_DesParametro,str_TipoParametro
            if (dataParam.Tables.Count > 0)
            {
                DataTable dt = dataParam.Tables[0];
                DataRow campo = dt.Rows[0];
                txtTiempo.Text = campo["Valor"].ToString();
            }

            if (this.txtTiempo.Text == "0" || this.txtTiempo.Text == "")
            {
                decimal valorPresentePresentePricipal = 0;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresentePricipal.Text = valorPresentePresentePricipal.ToString("N2");
                gdec_ValorPresentePrincipalColones = decimal.Parse(txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                //Valor presente principal colones intereses
                decimal valorPresentePresenteIntereses = 0;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresenteIntereses.Text = valorPresentePresenteIntereses.ToString("N2");
                gdec_ValorPresenteInteresesColones = decimal.Parse(txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

            }
            else
            {

                //Valor presente principal colones
                double calc = Convert.ToDouble(1 + (tbp / 100));
                double tiempo = Convert.ToDouble(txtTiempo.Text);
                double divMonto = Math.Pow(calc, tiempo);

                double valorPresentePresentePricipal = this.txtMontoColonesPrincipal.Text.Equals("") ? 0 : Convert.ToDouble(txtMontoColonesPrincipal.Text) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresentePricipal.Text = valorPresentePresentePricipal.ToString("N2");
                gdec_ValorPresentePrincipalColones = decimal.Parse(txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                //Valor presente principal colones intereses
                double valorPresentePresenteIntereses = this.txtMontoColonesIntereses.Text.Equals("") ? 0 : Convert.ToDouble(txtMontoColonesIntereses.Text) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresenteIntereses.Text = valorPresentePresenteIntereses.ToString("N2");
                gdec_ValorPresenteInteresesColones = decimal.Parse(txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

            }
            this.up_DatosPrincipales.Update();
        }

        private void setValorPresente()
        {
            decimal venta = Convert.ToDecimal(this.txtVenta.Text);
            decimal compra = Convert.ToDecimal(this.txtCompra.Text);
            decimal euro = Convert.ToDecimal(this.txtEuro.Text);
            //decimal tiempoN = txtTiempo.Text.Contains("") ? 1 : Convert.ToDecimal(txtTiempo.Text);
            decimal tbp = Convert.ToDecimal(txtTBP.Text);
            decimal montoprincipal = this.txtMontoPrincipal.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoPrincipal.Text);
            decimal montointereses = txtMontoIntereses.Text == "" ? 0 : Convert.ToDecimal(txtMontoIntereses.Text);
            decimal montodesembolso = txtMontoPosDesembolso.Text == "" ? 0 : Convert.ToDecimal(txtMontoPosDesembolso.Text);

            Decimal monto_principal_colones;
            Decimal monto_principal_intereses;
            Decimal tipo_cambio = 1;

            if (ConsultarTipoExpediente(gstr_IdExpediente).Contains("Actor") && !this.DDLMoneda.SelectedItem.Value.Contains("CRC"))
            {
                tipo_cambio = compra;
            }
            else if (ConsultarTipoExpediente(gstr_IdExpediente).Contains("Demandado") && !this.DDLMoneda.SelectedItem.Value.Contains("CRC"))
            {
                tipo_cambio = venta;
            }

            if (this.DDLMoneda.SelectedItem.Value.Contains("EUR"))
            {
                tipo_cambio = Math.Round(tipo_cambio * euro, 2);
            }

            monto_principal_colones = (montoprincipal * tipo_cambio);
            monto_principal_intereses = (montointereses * tipo_cambio);
            
            //Sacamos el parametro de Tiempo digitado por contabilidad nacional
            DataSet dataParam = ws_SG.uwsConsultarParametros("CT_Tiempo", "", DateTime.Today, "", "");//str_IdParametro,str_IdModulo,dt_FchVigencia,str_DesParametro,str_TipoParametro
            if (dataParam.Tables.Count > 0)
            {
                DataTable dt = dataParam.Tables[0];
                DataRow campo = dt.Rows[0];
                txtTiempo.Text = campo["Valor"].ToString();
            }

            if (this.txtTiempo.Text == "0" || this.txtTiempo.Text == "")
            {
                decimal valorPresentePresentePricipal = 0;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresentePricipal.Text = valorPresentePresentePricipal.ToString("N2");
                gdec_ValorPresentePrincipalColones = decimal.Parse(txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                //Valor presente principal colones intereses
                decimal valorPresentePresenteIntereses = 0;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresenteIntereses.Text = valorPresentePresenteIntereses.ToString("N2");
                gdec_ValorPresenteInteresesColones = decimal.Parse(txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

            }
            else
            {

                //Valor presente principal colones
                double calc = Convert.ToDouble(1 + (tbp / 100));
                double tiempo = Convert.ToDouble(txtTiempo.Text);
                double divMonto = Math.Pow(calc, tiempo);

                double valorPresentePresentePricipal = Convert.ToDouble(monto_principal_colones) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresentePricipal.Text = valorPresentePresentePricipal.ToString("N2");
                gdec_ValorPresentePrincipalColones = decimal.Parse(txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                //Valor presente principal colones intereses
                double valorPresentePresenteIntereses = Convert.ToDouble(monto_principal_intereses) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresenteIntereses.Text = valorPresentePresenteIntereses.ToString("N2");
                gdec_ValorPresenteInteresesColones = decimal.Parse(txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            }
            this.up_DatosPrincipales.Update();
        }

        private void AjustarEtiquetas()
        {
            //if (gstr_IdExpediente != "0")
            //{
            //    gstr_IdExpediente = this.DDLExpedientes.SelectedValue;
            //}
            //else { MessageBox.Show("Selecciona un expediente del desplegable."); }
            gstr_IdExpediente = this.DDLExpedientes.SelectedItem.Value;

            gstr_TipoExpediente = ConsultarTipoExpediente(gstr_IdExpediente);

            if (gstr_TipoExpediente.Contains("Actor"))
            {
                // this.lblFechaSalidaEntrada.Text = "Posible fecha de entrada de recursos";
                gstr_Estado = "Activo";
                this.lblFechaSalidaEntrada.Text = "Posible fecha de entrada de recursos";
                this.div_Desembolso.Visible = false;
            }
            else if (gstr_TipoExpediente.Contains("Demandado"))
            {
                gstr_Estado = "Pasivo";
                this.lblFechaSalidaEntrada.Text = "Posible fecha de salida de recursos";
                this.div_Desembolso.Visible = true;

            }

        }

        private void ConvertirMontos()
        {
            decimal dec_venta = Convert.ToDecimal(this.txtVenta.Text);
            decimal dec_compra = Convert.ToDecimal(this.txtCompra.Text);
            decimal dec_euro = Convert.ToDecimal(this.txtEuro.Text);
            decimal dec_tbp = Convert.ToDecimal(txtTBP.Text);

            decimal montoprincipal = this.txtMontoPrincipal.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoPrincipal.Text);
            decimal montointereses = txtMontoIntereses.Text == "" ? 0 : Convert.ToDecimal(txtMontoIntereses.Text);
            decimal montodesembolso = txtMontoPosDesembolso.Text == "" ? 0 : Convert.ToDecimal(txtMontoPosDesembolso.Text);

            if (!this.DDLExpedientes.SelectedItem.Value.Equals("0"))
            {
                gstr_IdExpediente = this.DDLExpedientes.SelectedItem.Value;
            }

            this.txtMontoPosDesembolso.Text = montodesembolso.ToString("N2");
            this.txtMontoPrincipal.Text = montoprincipal.ToString("N2");
            this.txtMontoIntereses.Text = montointereses.ToString("N2");

            //Segundo moneda seleccionada se da la variacion del monto
            if (this.DDLMoneda.SelectedItem.Value.Contains("USD"))
            {
                if (ConsultarTipoExpediente(gstr_IdExpediente).Equals("Actor"))
                {
                    txtMontoColonesPrincipal.Text = (montoprincipal * dec_compra).ToString("N2");
                    gdec_MontoPrincipalColones = decimal.Parse(this.txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    txtMontoColonesIntereses.Text = (montointereses * dec_compra).ToString("N2");
                    gdec_MontoInteresesColones = decimal.Parse(txtMontoColonesIntereses.Text, NumberStyles.AllowThousands | NumberStyles.Number);
                    gdec_TipoCambio = dec_compra;
                }
                else if (ConsultarTipoExpediente(gstr_IdExpediente).Equals("Demandado"))
                {
                    txtMontoColonesPrincipal.Text = (montoprincipal * dec_venta).ToString("N2");
                    gdec_MontoPrincipalColones = decimal.Parse(this.txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    txtMontoColonesIntereses.Text = (montointereses * dec_venta).ToString("N2");
                    gdec_MontoInteresesColones = decimal.Parse(txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    gdec_TipoCambio = dec_venta;
                }
            }
            else if (this.DDLMoneda.SelectedItem.Value.Contains("EUR"))
            {
                if (ConsultarTipoExpediente(gstr_IdExpediente).Equals("Actor"))
                {
                    txtMontoColonesPrincipal.Text = (montoprincipal * Math.Round(dec_compra * dec_euro,2)).ToString("N2");
                    gdec_MontoPrincipalColones = decimal.Parse(txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    txtMontoColonesIntereses.Text = (montointereses * Math.Round(dec_euro * dec_compra,2)).ToString("N2");
                    gdec_MontoInteresesColones = decimal.Parse(txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    gdec_TipoCambio = dec_compra * dec_euro;

                    
                }
                else if (ConsultarTipoExpediente(gstr_IdExpediente).Equals("Demandado"))
                {
                    txtMontoColonesPrincipal.Text = (montoprincipal * Math.Round(dec_euro * dec_venta,2)).ToString("N2");
                    gdec_MontoPrincipalColones = decimal.Parse(txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    txtMontoColonesIntereses.Text = (montointereses * Math.Round(dec_euro * dec_venta,2)).ToString("N2");
                    gdec_MontoInteresesColones = decimal.Parse(txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                    gdec_TipoCambio = dec_venta * dec_euro;
                }
            }
            else if (this.DDLMoneda.SelectedItem.Value.Contains("CRC"))
            {
                txtMontoColonesPrincipal.Text = (montoprincipal * 1).ToString("N2");
                gdec_MontoPrincipalColones = decimal.Parse(txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                txtMontoColonesIntereses.Text = (montointereses * 1).ToString("N2");
                gdec_MontoInteresesColones = decimal.Parse(txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
                gdec_TipoCambio = 1;
            }
            //Sacamos el parametro de Tiempo digitado por contabilidad nacional
            DataSet dataParam = ws_SG.uwsConsultarParametros("CT_Tiempo", "", DateTime.Today, "", "");//str_IdParametro,str_IdModulo,dt_FchVigencia,str_DesParametro,str_TipoParametro
            if (dataParam.Tables.Count > 0)
            {
                DataTable dt = dataParam.Tables[0];
                DataRow campo = dt.Rows[0];
                txtTiempo.Text = campo["Valor"].ToString();
            }

            if (this.txtTiempo.Text == "0" || this.txtTiempo.Text == "")
            {
                decimal valorPresentePresentePricipal = 0;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresentePricipal.Text = valorPresentePresentePricipal.ToString("N2");
                gdec_ValorPresentePrincipal = decimal.Parse(txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                //Valor presente principal colones intereses
                decimal valorPresentePresenteIntereses = 0;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresenteIntereses.Text = valorPresentePresenteIntereses.ToString("N2");
                gdec_ValorPresenteInteresesColones = decimal.Parse(txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            }
            else
            {
                //Valor presente principal colones
                double calc = Convert.ToDouble(1 + (dec_tbp/100));
                double tiempo = Convert.ToDouble(txtTiempo.Text);
                double divMonto = Math.Pow(calc, tiempo);
                double valorPresentePresentePricipal = Convert.ToDouble(txtMontoColonesPrincipal.Text) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresentePricipal.Text = valorPresentePresentePricipal.ToString("N2");
                gdec_ValorPresentePrincipal = decimal.Parse(txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                //Valor presente principal colones intereses
                double valorPresentePresenteIntereses = Convert.ToDouble(txtMontoColonesIntereses.Text) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresenteIntereses.Text = valorPresentePresenteIntereses.ToString("N2");
                gdec_ValorPresenteInteresesColones = decimal.Parse(txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            }
        }
        
        private void ConvertirMontosColones()
        {
            decimal dec_venta = Convert.ToDecimal(this.txtVenta.Text);
            decimal dec_compra = Convert.ToDecimal(this.txtCompra.Text);
            decimal dec_euro = Convert.ToDecimal(this.txtEuro.Text);

            decimal dec_tbp = Convert.ToDecimal(txtTBP.Text);

            decimal montoprincipal = this.txtMontoPrincipal.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoPrincipal.Text);
            decimal montointereses = txtMontoIntereses.Text == "" ? 0 : Convert.ToDecimal(txtMontoIntereses.Text);
            decimal montodesembolso = txtMontoPosDesembolso.Text == "" ? 0 : Convert.ToDecimal(txtMontoPosDesembolso.Text);

            gstr_IdExpediente = this.DDLExpedientes.SelectedItem.Value;

            this.txtMontoPosDesembolso.Text = montodesembolso.ToString("N2");
            this.txtMontoPrincipal.Text = montoprincipal.ToString("N2");
            this.txtMontoIntereses.Text = montointereses.ToString("N2");

            //Sacamos el parametro de Tiempo digitado por contabilidad nacional
            DataSet dataParam = ws_SG.uwsConsultarParametros("CT_Tiempo", "", DateTime.Today, "", "");
            if (dataParam.Tables.Count > 0)
            {
                DataTable dt = dataParam.Tables[0];
                DataRow campo = dt.Rows[0];
                txtTiempo.Text = campo["Valor"].ToString();
            }

            if (this.txtTiempo.Text == "0" || this.txtTiempo.Text == "")
            {
                decimal valorPresentePresentePricipal = 0;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresentePricipal.Text = valorPresentePresentePricipal.ToString("N2");
                gdec_ValorPresentePrincipal = decimal.Parse(txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                //Valor presente principal colones intereses
                decimal valorPresentePresenteIntereses = 0;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresenteIntereses.Text = valorPresentePresenteIntereses.ToString("N2");
                gdec_ValorPresenteInteresesColones = decimal.Parse(txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            }
            else
            {
                //Valor presente principal colones
                double calc = Convert.ToDouble(1 + (dec_tbp/100));
                double tiempo = Convert.ToDouble(txtTiempo.Text);
                double divMonto = Math.Pow(calc, tiempo);
                double valorPresentePresentePricipal = Convert.ToDouble(txtMontoColonesPrincipal.Text) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresentePricipal.Text = valorPresentePresentePricipal.ToString("N2");
                gdec_ValorPresentePrincipal = decimal.Parse(txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                //Valor presente principal colones intereses
                double valorPresentePresenteIntereses = Convert.ToDouble(txtMontoColonesIntereses.Text) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresenteIntereses.Text = valorPresentePresenteIntereses.ToString("N2");
                gdec_ValorPresenteInteresesColones = decimal.Parse(txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
           
            }
        }

        public void BloqueaCampos(bool pEstado)
        {
            this.btnGuardar.Visible =
            this.btnGuardar.Enabled =
            this.CKEditorObservaciones.Enabled =
            this.DDLEstadoProcesal.Enabled =
            this.txtResolucionNum.Enabled =
            this.txtFechaResolucion.Enabled =
            this.txtFechSalidaRecur.Enabled =
            this.txtMontoPosDesembolso.Enabled =
            this.txtMontoPrincipal.Enabled =
            this.txtMontoIntereses.Enabled =
            this.DDLMoneda.Enabled =
            this.txtMontoColonesPrincipal.Enabled =
            this.txtValorPresentePricipal.Enabled =
            this.txtMontoColonesIntereses.Enabled =
            this.txtValorPresenteIntereses.Enabled = pEstado;
        }

        public void LimpiarCampos()
        {
            this.DDLEstadoProcesal.SelectedValue = "0";

            //DDLMoneda.Items.Clear();
            //CargarDDLs();
            this.DDLMoneda.SelectedValue = "0";

            this.txtResolucionNum.Text = "";
            this.txtMontoPosDesembolso.Text = "";
            this.txtMontoPrincipal.Text = "";
            this.txtMontoIntereses.Text = "";
            this.txtMontoColonesPrincipal.Text = "";
            this.txtValorPresentePricipal.Text = "";
            this.txtMontoColonesIntereses.Text = "";
            this.txtValorPresenteIntereses.Text = "";
            this.CKEditorObservaciones.Text = string.Empty;
        }


        private void CambioTipoCambioAnterior()
        {
            gdec_TipoCambio = (gdec_TipoCambioAnterior == 0) ? gdec_TipoCambio : gdec_TipoCambioAnterior;
            gdec_TipoCambio = Math.Round(gdec_TipoCambio, 2);
            gstr_Moneda = (gstr_MonedaAnterior=="")?DDLMoneda.SelectedValue:gstr_MonedaAnterior ;
            gdec_Tbp = (gdec_TbpAnterior==0)? gdec_Tbp:gdec_TbpAnterior ;
            gdec_Tiempo = (gdec_TiempoAnterior == 0) ? gdec_Tiempo : gdec_TiempoAnterior;

            gdec_MontoPrincipal = this.txtMontoPrincipal.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoPrincipal.Text);
            gdec_MontoIntereses = txtMontoIntereses.Text == "" ? 0 : Convert.ToDecimal(txtMontoIntereses.Text);

            gdec_MontoPrincipalColones = gdec_MontoPrincipal * gdec_TipoCambio;
            gdec_MontoInteresesColones = gdec_MontoIntereses * gdec_TipoCambio;

            txtMontoColonesPrincipal.Text = gdec_MontoPrincipalColones.ToString("N2");
            txtMontoColonesIntereses.Text = gdec_MontoInteresesColones.ToString("N2");

            //gdec_Tbp = Convert.ToDecimal(txtTBP.Text);

            double calc = Convert.ToDouble(1 + (gdec_Tbp / 100));
            double tiempo = Convert.ToDouble(gdec_Tiempo);
            double divMonto = Math.Pow(calc, tiempo);

            double valorPresentePresentePricipal = Convert.ToDouble(gdec_MontoPrincipalColones) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
            double valorPresentePresenteIntereses = Convert.ToDouble(gdec_MontoInteresesColones) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
            
            txtValorPresentePricipal.Text = valorPresentePresentePricipal.ToString("N2");
            txtValorPresenteIntereses.Text = valorPresentePresenteIntereses.ToString("N2");

            gdec_ValorPresenteIntereses = decimal.Parse(txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            gdec_ValorPresentePrincipal = decimal.Parse(txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            gdec_ValorPresenteInteresesColones = decimal.Parse(txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);
            gdec_ValorPresentePrincipalColones = decimal.Parse(txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);


        }

        private void InicializarVariables()
        {
            gstr_IdExpediente = this.DDLExpedientes.SelectedValue;
            gstr_TipoExpediente = ConsultarTipoExpediente(gstr_IdExpediente); // Actor o Demandado
            gstr_IdResolucion = this.txtResolucionNum.Text;
            gstr_EstadoResolucion = this.ddlEstadoResol.SelectedItem.Value; // P1, P2 o RF
            gstr_EstadoProcesal = this.DDLEstadoProcesal.SelectedItem.Value; // # de Estado
            gstr_EstadoTransaccion = "Vigente";
            gstr_UsuarioModifica = (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario;//usuaario loggeado va aqui
            gstr_Observaciones = this.CKEditorObservaciones.Text;
            gstr_Moneda = this.DDLMoneda.SelectedValue;

            gdec_Tbp = txtTBP.Text == "" ? 0 : Convert.ToDecimal(this.txtTBP.Text);
            gdec_Tiempo = txtTiempo.Text == "" ? 0 : Convert.ToDecimal(txtTiempo.Text);
            
            if (!gstr_Moneda.Contains("CRC"))
                gbool_MonedaExtrangera = true;
            else
                gbool_MonedaExtrangera = false;

            gbool_SinLugar = this.chkCxPCaC.Checked;
            gbool_Incobrable = this.ckbIncobrable.Checked;

            gbool_CambioMes = this.ckbNuevoMes.Checked;
            gbool_CambioAno = this.ckbNuevoAno.Checked;

            if (verificarMontos().Equals("Lleno"))
                gbool_Lleno = true;
            else
                gbool_Lleno = false;

            //gstr_FechaResolucion = this.calFechaResolucion.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);// Convert.ToDateTime(this.txtFechaResolucion.Text);
            gstr_FechaResolucion = Convert.ToDateTime(this.txtFechaResolucion.Text).ToString();
            //gstr_PosibleFechaSalida = this.calFechSalidaRecur.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture); //Convert.ToDateTime(this.txtSalidaRecursosFecha.Text);
            if (!String.IsNullOrEmpty(this.txtFechSalidaRecur.Text))
            {
                gstr_PosibleFechaSalida = Convert.ToDateTime(this.txtFechSalidaRecur.Text).ToString();
            }
            
            if (gstr_TipoExpediente.Equals("Actor"))
            {
                gstr_TipoTransaccion = "Cobro";
                gint_CxCaCxP = 1; // CxCobrar
            }
            else if (gstr_TipoExpediente.Equals("Demandado"))
            {
                gstr_TipoTransaccion = "Pago";
                gint_CxCaCxP = 0; // CxCobrar
            }


            gdec_MontoPosibleReembolso = this.txtMontoPosDesembolso.Text.Equals("") ? 0 : Convert.ToDecimal(this.txtMontoPosDesembolso.Text);
            gdec_ValorPresentePrincipal = this.txtValorPresentePricipal.Text == "" ? 0 : Convert.ToDecimal(this.txtValorPresentePricipal.Text);//Formula: VP= VF/(1+i)n
            gdec_ValorPresenteIntereses = this.txtValorPresenteIntereses.Text == "" ? 0 : Convert.ToDecimal(this.txtValorPresenteIntereses.Text);//Formula: VP= VF/(1+i)n

            gdec_MontoPrincipal = this.txtMontoPrincipal.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoPrincipal.Text);
            gdec_MontoIntereses = this.txtMontoIntereses.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoIntereses.Text);

            gdec_MontoPrincipalColones = this.txtMontoColonesPrincipal.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoColonesPrincipal.Text);//tipocambio*montoprincipal
            gdec_MontoInteresesColones = this.txtMontoColonesIntereses.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoColonesIntereses.Text);
            gdec_MontoPosibleReembolsoColones = this.txtMontoPosDesembolso.Text.Equals("") ? 0 : Convert.ToDecimal(this.txtMontoPosDesembolso.Text);
            

            gdec_ValorPresentePrincipalColones = gdec_ValorPresentePrincipal;
            gdec_ValorPresenteInteresesColones = gdec_ValorPresenteIntereses;
        }

        private Boolean ObtenerResolucionAModificar(String idResolucion, String EstadoResolucion)
        {
            String lstr_resoluciones = String.Empty;
            Boolean lbool_existe = false;
            this.chkCxPCaC.Enabled = true;
            //CargarDDLs();

            String lstr_Codigo = String.Empty;
            String lstr_Mensaje = String.Empty;

            string lstr_query = "SELECT * FROM co.Expedientes exp " +
                "INNER JOIN co.Resoluciones res " + 
                "ON exp.IdExp = res.IdExp " +
                "LEFT OUTER JOIN co.CobrosPagos cp "+
                "ON cp.IdRes = res.IdRes "+
                "WHERE exp.IdExpediente ='" + idResolucion + "' " +
                "AND exp.IdSociedadGL ='" + gstr_Sociedad + "' " +
                "AND exp.EstadoExpediente = 'Activo' "+
                "AND cp.TipoTransaccion != 'tipotra' ";

            DataTable dt_Resoluciones = GetData(lstr_query);

            TieneResolucion(idResolucion);

            if ((gbool_TieneRF || gbool_TieneRP2) && EstadoResolucion.Contains("Provisional 1"))
            {
                BloqueaCampos(false);
            }
            else if (gbool_TieneRF && EstadoResolucion.Contains("Provisional 2"))
            {
                BloqueaCampos(false);
            }
            else
                BloqueaCampos(true);

            if (dt_Resoluciones.Rows.Count > 0)
            {
                lbool_existe = true;
                
                foreach (DataRow dr_Resolucion in dt_Resoluciones.Rows)
                {
                    if ((dr_Resolucion["EstadoResolucion"].ToString().Contains("Provisional 1") && EstadoResolucion.Contains("Provisional 1"))
                        || (dr_Resolucion["EstadoResolucion"].ToString().Contains("Provisional 2") && EstadoResolucion.Contains("Provisional 2"))
                        || (dr_Resolucion["EstadoResolucion"].ToString().Contains("En Firme") && EstadoResolucion.Contains("En Firme")))
                    {
                        ViewState["idRes"] = Convert.ToInt32(dr_Resolucion["IdRes"].ToString());
                        if (!string.IsNullOrEmpty(dr_Resolucion["IdCobroPagoResolucion"].ToString()))
                        ViewState["idCobroP"] = Convert.ToInt32(dr_Resolucion["IdCobroPagoResolucion"].ToString());
                        this.DDLExpedientes.SelectedValue = dr_Resolucion["IdExpedienteFK"].ToString();
                        this.ddlEstadoResol.SelectedValue = dr_Resolucion["EstadoResolucion"].ToString();
                        this.DDLEstadoProcesal.SelectedValue = dr_Resolucion["EstadoProcesal1"].ToString().Trim();
                        this.txtResolucionNum.Text = dr_Resolucion["IdResolucion"].ToString();
                        //this.calFechSalidaRecur.SelectedValue = (DateTime)campo["PosibleFechSalidaRecursos"];
                        // this.txtSalidaRecursosFecha.Text = //laFechaS.ToString("dd/MM/yyyy");

                        if (!String.IsNullOrEmpty(dr_Resolucion["PosibleFechSalidaRecursos"].ToString()))
                        {
                            this.txtFechSalidaRecur.Text = ((DateTime)dr_Resolucion["PosibleFechSalidaRecursos"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        
                        this.txtFechaResolucion.Text = ((DateTime)dr_Resolucion["FechResolucion"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //this.txtFechaResolucion.Text = //laFechad.ToString("dd/MM/yyyy");
                        this.txtMontoPosDesembolso.Text = dr_Resolucion["MontoPosibleReembolsoColones"].ToString();

                        if (!string.IsNullOrEmpty(dr_Resolucion["Moneda"].ToString()))
                        this.DDLMoneda.SelectedValue = dr_Resolucion["Moneda"].ToString().Trim();
                        if (!string.IsNullOrEmpty(dr_Resolucion["MontoIntereses"].ToString()))
                        this.txtMontoIntereses.Text = Convert.ToDecimal(dr_Resolucion["MontoIntereses"].ToString()).ToString("N2");
                        if (!string.IsNullOrEmpty(dr_Resolucion["MontoPrincipal"].ToString()))
                        this.txtMontoPrincipal.Text = Convert.ToDecimal(dr_Resolucion["MontoPrincipal"].ToString()).ToString("N2");
                        if (!string.IsNullOrEmpty(dr_Resolucion["MontoInteresesColones"].ToString()))
                        this.txtMontoColonesIntereses.Text = Convert.ToDecimal(dr_Resolucion["MontoInteresesColones"].ToString()).ToString("N2");
                        if (!string.IsNullOrEmpty(dr_Resolucion["MontoPrincipalColones"].ToString()))
                        this.txtMontoColonesPrincipal.Text = Convert.ToDecimal(dr_Resolucion["MontoPrincipalColones"].ToString()).ToString("N2");
                        if (!string.IsNullOrEmpty(dr_Resolucion["ValorPresentePrinColones"].ToString()))
                        this.txtValorPresentePricipal.Text = Convert.ToDecimal(dr_Resolucion["ValorPresentePrinColones"].ToString()).ToString("N2");
                        if (!string.IsNullOrEmpty(dr_Resolucion["ValorPresenteInteresColones"].ToString()))
                        this.txtValorPresenteIntereses.Text = Convert.ToDecimal(dr_Resolucion["ValorPresenteInteresColones"].ToString()).ToString("N2");
                        this.CKEditorObservaciones.Text = dr_Resolucion["Observacion"].ToString();
                        //this.chkCxPCaC.Checked = Convert.ToBoolean(campo["AplicarCuentaComo"]);

                        String lstr_tipoExp = ConsultarTipoExpediente(idResolucion);

                        if (lstr_tipoExp.Contains("Actor"))
                        {
                            gint_EstadoPretension = 1;
                            this.div_Desembolso.Visible = false;
                            if (EstadoResolucion.Contains("En Firme"))
                            {
                                this.ckbIncobrable.Visible = true;
                                this.Label1.Visible = true;
                            }
                            else
                            {
                                this.ckbIncobrable.Visible = false;
                                this.Label1.Visible = false;
                            }
                        }
                        else
                        {
                            gint_EstadoPretension = 0;
                            
                            this.ckbIncobrable.Visible = false;
                            this.Label1.Visible = false;
                            
                        }
                        break;
                    }
                    else if (dr_Resolucion["EstadoResolucion"].ToString().Contains("Provisional 2") && (gstr_ResolucionExp != null) && gstr_ResolucionExp.Contains("Provisiona l"))
                    {
                        //LimpiarCampos();
                        BloqueaCampos(true);
                    }
                    else
                    {
                        LimpiarCampos();
                    }

                }

                

                AjustarEtiquetas();
                CambioTipoCambioAnterior();
                //this.btnModificar.Visible = true;
                //this.btnGuardar.Visible = false;
            }

            DataSet fileList = ws_SG.uwsObtenerArchivoPorIdResolucion(idResolucion, gstr_Sociedad,ConsultarIdExpediente(idResolucion));
            if (fileList.Tables.Count > 0)
            {
                gvFiles.DataSource = fileList;
                gvFiles.DataBind();
            }
            else
            {
                //Log.Error("Consulta no arrojó resultados, la tabla viene vacia al realizar la consulta para uwsObtenerArchivoPorIdResolucion.");
            }


            return lbool_existe;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            InicializarVariables();

            GuardarResolucion();
        }

/*      |||||||||||||||||||||||||||||||| */
        private void GuardarResolucion()
        {
            #region Variables

            string lstr_CodAsiento = "";
            String lstr_mensj = string.Empty;
            String lstr_Resultado = String.Empty;
            String lstr_ResEnviarRev = String.Empty;

            String[] larrstr_ResultResolucion = new string[3];
            String[] larrstr_ResultadoModificacion = new String[2];
            String[] larrstr_ResultadoConsResolucion = new String[2];

            String lstr_Codigo = String.Empty;
            String lstr_Mensaje = String.Empty;

            String lstr_Monto = String.Empty;
            String lstr_MontoReversar = String.Empty;

            Boolean lbool_Reinicio = false;
            

            
            DataSet lds_ConsultarExpediente = new DataSet();
            
            DataRow ldr_ConsultarExpediente = null;



            gdec_MontoAjustesPrincipal = 0;
            gstr_Estado = gstr_TipoExpediente;
            gstr_AsientosResultado = "";

            gdec_TipoCambio = Math.Round(gdec_TipoCambio, 2);

            asiento.definirExpediente(gstr_IdExpediente,gstr_Sociedad,gstr_Usuario);

            #endregion


            try
            {
                #region existe Pretención Inicial??
                lds_ConsultarExpediente = ws_SG.uwsConsultarExpedienteXNumero(gstr_IdExpediente, gstr_Sociedad);
                if ((lds_ConsultarExpediente.Tables["Table"] != null) && (lds_ConsultarExpediente.Tables.Count > 0))
                {
                    ldr_ConsultarExpediente = lds_ConsultarExpediente.Tables["Table"].Rows[0];
                    if (!String.IsNullOrEmpty(ldr_ConsultarExpediente["MonedaPretension"].ToString()))
                    {
                        gbool_TienePretensionInicial = true; // Sí existe Pretension Inicial [ePI]
                        gdec_MontoPrincipalColRev = Convert.ToDecimal(ldr_ConsultarExpediente["MontoPretensionColones"]);
                    }
                    else
                    {
                        gbool_TienePretensionInicial = false; // No existe Pretension Inicial [nePI]
                    }
                }
                #endregion

                TieneResolucion();

                
                if (((gdec_MontoInteresesColones == 0) && (gdec_MontoPrincipalColones != 0))
                    || ((gdec_MontoInteresesColones != 0) && (gdec_MontoPrincipalColones == 0)))
                    gint_CantidadLineasAsiento = 2;
                else
                    gint_CantidadLineasAsiento = 4; 

                String go = String.Empty;
                #region Actor
                if (gstr_TipoExpediente.Contains("Actor"))
                {
                    switch (gstr_EstadoResolucion) // RP1, RP2, RF
                    {
                        #region Actor Provisional 1
                        case "Provisional 1":
                            if (!gbool_SinLugar) // NORMAL
                            {
                                #region AC RP1 Normal Con Sin Monto
                                if (!gbool_TienePretensionInicial)
                                    gint_EstadoPretension = 5; // 1: activo contingente con monto 0.00 en PI
                                else
                                    gint_EstadoPretension = 1; // 1: activo contingente

                                    if (!gbool_TieneRP1)// No tiene RP1
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                    }
                                    else
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                            gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                            gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                    }

                                    if (larrstr_ResultResolucion[0].Contains("00"))
                                    {
                                        lstr_Resultado = "exito";
                                        asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);

                                        if ((gbool_TienePretensionInicial) || (gbool_TieneRP1))
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 1, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); // suma monto ajustado
                                        else
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); // suma monto y cant

                                        if (lstr_ResEnviarRev.Contains("exito"))
                                        {
                                            lstr_Resultado = "exito";
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo2";
                                        }
                                    }
                                    else
                                    {
                                        go = larrstr_ResultResolucion[1];
                                        lstr_Resultado = "fallo1";
                                    }
                                #endregion
                            }
                            else // SIN LUGAR
                            {
                                gint_CxCaCxP = 0;
                                gstr_Estado = "Demandado";
                                gstr_Observaciones = "Reversion de Cuenta";

                                #region AC RP1 SinLugar ConMonto
                                
                                if (gbool_Lleno) 
                                {
                                    gint_EstadoPretension = 2; //Demandado contabilizado

                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT01", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                          
                                   
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        if (!gbool_TieneRP1)
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                        }
                                        else
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                        }

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_Resultado = "exito";
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            if (gbool_TienePretensionInicial || gbool_TieneRP1)
                                            {
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 6, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //resta

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                            else
                                                lstr_Resultado = "exito";
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo1";
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo";
                                    }

                                }
                                #endregion

                                #region AC RP1 SinLugar SinMonto
                                else // sin Monto
                                {
                                    gint_EstadoPretension = 0;
                                    if (!gbool_TieneRP1)
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                    }
                                    else
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                            gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                            gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                    }

                                    if (larrstr_ResultResolucion[0].Contains("00"))
                                    {
                                        lstr_Resultado = "exito";
                                        asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                        if ((gbool_TienePretensionInicial) || (gbool_TieneRP1))
                                        {
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 6, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //resta

                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                gdec_MontoPrincipal = 0;

                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //suma

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }
                                        else
                                        {
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //suma

                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo1";
                                    }
                                    
                                }

                                lbool_Reinicio = true;

                                #endregion
                            }
                                
                            break;
                        #endregion

                        #region Actor Provisional 2
                        case "Provisional 2":
                            if (!gbool_SinLugar) // NORMAL
                            {
                                #region AC RP2 Normal Con y Sin Monto
                                if (!gbool_TienePretensionInicial) // No existe Resolucion
                                    gint_EstadoPretension = 5; // 1: activo contingente con monto 0.00 en PI
                                else
                                    gint_EstadoPretension = 1; // 1: activo contingente

                                if (gbool_TieneRP2)
                                {
                                    larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                        gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                        gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                }
                                else
                                {
                                    larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                        gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                        gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                        gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                }

                                if (larrstr_ResultResolucion[0].Contains("00"))
                                {
                                    lstr_Resultado = "exito";
                                    asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);

                                    if (gbool_TieneRP1 || gbool_TienePretensionInicial || gbool_TieneRP2)
                                        lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 1, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); // suma monto ajustado
                                    else
                                        lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); // suma monto y cant

                                    if (lstr_ResEnviarRev.Contains("exito"))
                                    {
                                        lstr_Resultado = "exito";
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo2";
                                    }
                                }
                                else
                                {
                                    go = larrstr_ResultResolucion[1];
                                    lstr_Resultado = "fallo1";
                                }
                                #endregion
                            }
                            else // SIN LUGAR
                            {
                                gint_CxCaCxP = 0;
                                gstr_Estado = "Demandado";
                                gstr_Observaciones = "Reversion de Cuenta";


                                #region AC RP2 SinLugar ConMonto
                                if (gbool_Lleno) // Con Monto
                                {
                                    gint_EstadoPretension = 2; //Demandado contabilizado


                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT01", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);

                                   
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            if (!gbool_TieneRP2)
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                    Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                    gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                    gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                    gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                    gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                            }
                                            else
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                    Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                    gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                    gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                    gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                            }

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                                if (gbool_TienePretensionInicial || gbool_TieneRP1)
                                                {
                                                    lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 6, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //resta

                                                    if (lstr_ResEnviarRev.Contains("exito"))
                                                    {
                                                        lstr_Resultado = "exito";
                                                    }
                                                    else
                                                    {
                                                        lstr_Resultado = "fallo3";
                                                    }
                                                }
                                                else
                                                    lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo2";
                                        }
                                }
                                #endregion

                                #region AC RP2 SinLugar SinMonto
                                else // Sin Monto
                                {
                                    // Paso a Demandado
                                    gint_EstadoPretension = 0;
                                    if (!gbool_TieneRF)
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                    }
                                    else
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                            gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                            gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                    }

                                    if (larrstr_ResultResolucion[0].Contains("00"))
                                    {
                                        lstr_Resultado = "exito";
                                        asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                        if (gbool_TienePretensionInicial || gbool_TieneRP1 || gbool_TieneRP2)
                                        {
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 6, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //resta

                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                gdec_MontoPrincipal = 0;

                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //suma

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo3";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo3";
                                            }
                                        }
                                        else
                                        {
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //suma

                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo3";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo";
                                    }
                                }//else sin monto

                                lbool_Reinicio = true;
                                #endregion
                            }
                            
                            break;
                        #endregion

                        #region Actor Resolucion Firme
                        case "En Firme":
                            if (gbool_Incobrable)
                            {
                                if (gbool_Lleno)//con monto
                                {
                                    garrdec_Montos = new Decimal[15];
                                    gint_CantidadLineasAsiento = 20;
                                    gint_EstadoPretension = 3;

                                    if (lbool_Prevision)
                                    {
                                        // aca debe ir la diferencia del monto con la prevision de incobrabilidad 08/09/2016
                                        garrdec_Montos[0] = ldec_PrincipalPrevision;
                                        garrdec_Montos[1] = ldec_InteresesPrevision;
                                        //garrdec_Montos[1] = gdec_MontoInteresesColRev - ldec_InteresesPrevision;

                                        garrdec_Montos[5] = ldec_PrincipalCierre - ldec_PrincipalPrevision; 
                                        garrdec_Montos[6] = ldec_InteresesCierre - ldec_InteresesPrevision;

                                        if ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno)
                                        {
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT15", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, ldec_PrincipalPrevision, ldec_PrincipalPrevision, out lstr_CodAsiento);
                                              }
                                        else
                                        {
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT14", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, ldec_PrincipalPrevision, ldec_PrincipalPrevision, out lstr_CodAsiento);
                                        }
                                            
                                    }
                                    else
                                    {
                                        garrdec_Montos[0] = gdec_MontoPrincipalColRev;
                                        garrdec_Montos[1] = gdec_MontoInteresesColRev;

                                        if ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno)
                                        {
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT17", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColRev, gdec_MontoInteresesColRev, out lstr_CodAsiento);
                                         
                                        }
                                        else 
                                        {
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT16", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColRev, gdec_MontoInteresesColRev, out lstr_CodAsiento);
                                  
                                           
                                        }
                                    }
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {

                                        larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_Resultado = "exito";
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                        }
                                    }
                                }
                                else
                                { }
                            }
                            else if (!gbool_SinLugar) // NORMAL
                            {
                                #region AC EnFirme Normal Con Monto
                                if (gbool_Lleno)
                                {
                                    gint_EstadoPretension = 3;
                                    if (!gbool_TienePretensionInicial && !gbool_TieneRP1 && !gbool_TieneRP2 && !gbool_TieneRF)
                                    {
                                        #region Asiento-> Registro
                                        //lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT06", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT10", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                      
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            string estado = "Actor";
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, estado, // gstr_Estado,
                                                    Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                    gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                    gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                    gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                    gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        #endregion
                                    }
                                    else if (gbool_TienePretensionInicial || gbool_TieneRP1 || gbool_TieneRP2 || gbool_TieneRF)
                                    {
                                        #region Asiento-> Registro-> Revelacion

                                        if (gbool_TieneRF)
                                        {
                                            #region Principal

                                            #region Actual > registro
                                            gint_CantidadLineasAsiento = 2;
                                            CambioTipoCambioAnterior();
                                            glbool_cambioMonto = false;
                                            if (gdec_MontoPrincipalColones > gdec_MontoPrincipalColRev)
                                            {
                                                gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - gdec_MontoPrincipalColRev;
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT10", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                               
                                                if (((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && (ldec_TipoCambioCierre != 0) && !gstr_MonedaAnterior.Contains("CRC"))//((lint_Mes < DateTime.Today.Month) && (ldec_TipoCambioCierre != 0))
                                                {
                                                    gdec_MontoAjustesPrincipal = gdec_MontoPrincipal - gdec_MontoPrincipalAnterior;
                                                    gdec_MontoPrincipalAnterior = (gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre) - (gdec_MontoAjustesPrincipal * gdec_TipoCambioAnterior);

                                                    if (lstr_Resultado.Contains("Contabilizado"))
                                                    {
                                                        gint_CantidadLineasAsiento = 4;
                                                        garrdec_Montos = new Decimal[15];
                                                        //garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                        garrdec_Montos[1] = 0;

                                                        if (gdec_MontoPrincipalAnterior < 0)
                                                        {
                                                            gdec_MontoPrincipalAnterior = gdec_MontoPrincipalAnterior * -1;
                                                            garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT25", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                           
                                                        }
                                                        else
                                                        {
                                                            garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT24", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                           
                                                        }
                                                    }
                                                }

                                            }
                                            #endregion

                                            #region Actual < registro
                                            else if (gdec_MontoPrincipalColones < gdec_MontoPrincipalColRev)
                                            {
                                                //reversión provisión
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT73", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColRev, 0, out lstr_CodAsiento);
                                                //registro RF
                                               
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT10", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, 0, out lstr_CodAsiento);

                                                if (((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && (ldec_TipoCambioCierre != 0) && !gstr_MonedaAnterior.Contains("CRC"))//((lint_Mes < DateTime.Today.Month) && (ldec_TipoCambioCierre != 0))
                                                {
                                                    Decimal ldec_AjusteDiferencial = 0;
                                                    ldec_AjusteDiferencial = (gdec_MontoPrincipalAnterior * ldec_TipoCambioCierre) - (gdec_MontoPrincipalAnterior * gdec_TipoCambioAnterior);

                                                    if (ldec_AjusteDiferencial < 0)
                                                    {
                                                        ldec_AjusteDiferencial = ldec_AjusteDiferencial * -1;
                                                        gint_CantidadLineasAsiento = 4;
                                                        garrdec_Montos = new Decimal[15];
                                                        garrdec_Montos[0] = ldec_AjusteDiferencial;
                                                        garrdec_Montos[1] = 0;

                                                        if (gbool_CambioAno)
                                                        {
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT92", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, ldec_AjusteDiferencial, 0, out lstr_CodAsiento);

                                                           
                                                        }
                                                        else
                                                        {
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT91", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, ldec_AjusteDiferencial, 0, out lstr_CodAsiento);
                                                          
                                                        }
                                                    }
                                                    else
                                                    {
                                                        garrdec_Montos = new Decimal[15];
                                                        gint_CantidadLineasAsiento = 4;
                                                        garrdec_Montos[0] = ldec_AjusteDiferencial;
                                                        garrdec_Montos[1] = 0;

                                                        if (gbool_CambioAno)
                                                        {
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT90", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, ldec_AjusteDiferencial, 0, out lstr_CodAsiento);
                                                             }
                                                        else
                                                        {
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT89", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, ldec_AjusteDiferencial, 0, out lstr_CodAsiento);
                                                            }
                                                    }

                                                    //gdec_MontoAjustesPrincipal = gdec_MontoPrincipalAnterior - gdec_MontoPrincipal;
                                                    //gdec_MontoPrincipalAnterior = (gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre) - (gdec_MontoAjustesPrincipal * gdec_TipoCambioAnterior);

                                                    //monto total al cierre
                                                    //gdec_MontoPrincipalAnterior = gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre;

                                                    gdec_MontoAjustesPrincipal = gdec_MontoPrincipal * (ldec_TipoCambioCierre - gdec_TipoCambioAnterior);

                                                    if (lstr_Resultado.Contains("Contabilizado"))
                                                    {
                                                        lstr_Resultado = "exito";
                                                        asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                                        gint_CantidadLineasAsiento = 4;
                                                        garrdec_Montos = new Decimal[15];
                                                        garrdec_Montos[1] = 0;

                                                        if (gdec_MontoAjustesPrincipal < 0)
                                                        {
                                                            gdec_MontoAjustesPrincipal = gdec_MontoAjustesPrincipal * -1;
                                                            garrdec_Montos[0] = gdec_MontoAjustesPrincipal;
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT25", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                                             }
                                                        else
                                                        {
                                                            garrdec_Montos[0] = gdec_MontoAjustesPrincipal;
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT24", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                                          }
                                                    }
                                                }
                                            }
                                            #endregion

                                            else
                                                lstr_Resultado = "Contabilizado";
                                            #endregion

                                            #region Intereses

                                            #region Actual > registro
                                            if (gdec_MontoInteresesColones > gdec_MontoInteresesColRev)
                                            {
                                                gdec_MontoAjustesIntereses = gdec_MontoInteresesColones - gdec_MontoInteresesColRev;
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT10", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                                if (((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && (ldec_TipoCambioCierre != 0) && !gstr_MonedaAnterior.Contains("CRC"))//((lint_Mes < DateTime.Today.Month) && (ldec_TipoCambioCierre != 0))
                                                {
                                                    gdec_MontoAjustesIntereses = gdec_MontoIntereses - gdec_MontoInteresesAnterior;

                                                    gdec_MontoInteresesAnterior = (gdec_MontoAjustesIntereses * ldec_TipoCambioCierre) - (gdec_MontoAjustesIntereses * gdec_TipoCambioAnterior);

                                                    if (lstr_Resultado.Contains("Contabilizado"))
                                                    {
                                                        gint_CantidadLineasAsiento = 4;
                                                        garrdec_Montos = new Decimal[15];
                                                        garrdec_Montos[0] = 0;
                                                        //garrdec_Montos[1] = gdec_MontoInteresesAnterior;

                                                        if (gdec_MontoInteresesAnterior < 0)
                                                        {
                                                            gdec_MontoInteresesAnterior = gdec_MontoInteresesAnterior * -1;
                                                            garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT25", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                           }
                                                        else
                                                        {
                                                            garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT24", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                            }
                                                    }
                                                }

                                            }
                                            #endregion

                                            #region Actual < registro
                                            else if (gdec_MontoInteresesColones < gdec_MontoInteresesColRev)
                                            {
                                                //reversion
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT73", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesColRev, out lstr_CodAsiento);

                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT10", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesColones, out lstr_CodAsiento);

                                                if (((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && (ldec_TipoCambioCierre != 0) && !gstr_MonedaAnterior.Contains("CRC"))//((lint_Mes < DateTime.Today.Month) && (ldec_TipoCambioCierre != 0))
                                                {
                                                    Decimal ldec_AjusteDiferencial = 0;
                                                    ldec_AjusteDiferencial = (gdec_MontoInteresesAnterior * ldec_TipoCambioCierre) - (gdec_MontoInteresesAnterior * gdec_TipoCambioAnterior);

                                                    if (ldec_AjusteDiferencial < 0)
                                                    {
                                                        ldec_AjusteDiferencial = ldec_AjusteDiferencial * -1;
                                                        gint_CantidadLineasAsiento = 4;
                                                        garrdec_Montos = new Decimal[15];
                                                        garrdec_Montos[0] = 0;
                                                        garrdec_Montos[1] = ldec_AjusteDiferencial;

                                                        if (gbool_CambioAno)
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT92", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, ldec_AjusteDiferencial, out lstr_CodAsiento);
                                                        else
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT91", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, ldec_AjusteDiferencial, out lstr_CodAsiento);

                                                    }
                                                    else
                                                    {
                                                        gint_CantidadLineasAsiento = 4;
                                                        garrdec_Montos = new Decimal[15];
                                                        garrdec_Montos[0] = 0;
                                                        garrdec_Montos[1] = ldec_AjusteDiferencial;

                                                        if (gbool_CambioAno)
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT90", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, ldec_AjusteDiferencial, out lstr_CodAsiento);
                                                        else
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT89", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, ldec_AjusteDiferencial, out lstr_CodAsiento);

                                                    }

                                                    //gdec_MontoAjustesIntereses = gdec_MontoInteresesAnterior - gdec_MontoIntereses;
                                                    //gdec_MontoInteresesAnterior = (gdec_MontoAjustesIntereses * ldec_TipoCambioCierre) - (gdec_MontoAjustesIntereses * gdec_TipoCambioAnterior);

                                                    //Nuevomonto * diferencia de TC
                                                    gdec_MontoAjustesIntereses = gdec_MontoIntereses * (ldec_TipoCambioCierre - gdec_TipoCambioAnterior);
                                                    //gdec_MontoInteresesAnterior = gdec_MontoAjustesIntereses * ldec_TipoCambioCierre;

                                                    if (lstr_Resultado.Contains("Contabilizado"))
                                                    {
                                                        gint_CantidadLineasAsiento = 4;
                                                        garrdec_Montos = new Decimal[15];
                                                        garrdec_Montos[0] = 0;
                                                        //garrdec_Montos[1] = gdec_MontoInteresesAnterior;

                                                        if (gdec_MontoAjustesIntereses < 0)
                                                        {
                                                            gdec_MontoAjustesIntereses = gdec_MontoAjustesIntereses * -1;
                                                            garrdec_Montos[1] = gdec_MontoAjustesIntereses;
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT25", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                                        }
                                                        else
                                                        {
                                                            garrdec_Montos[1] = gdec_MontoAjustesIntereses;
                                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT24", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                                        }
                                                    }
                                                }
                                            }
                                            #endregion

                                            else
                                                lstr_Resultado = "Contabilizado";

                                            #endregion
                                        }
                                        else
                                        {
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT10", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                        }
                                        
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            string estado = "Actor";
                                            if (!gbool_TieneRF) //No existe Resolucion
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,estado,//gstr_Estado,
                                                    Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                    gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                    gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                    gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                    gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                            }
                                            else
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, estado, //gstr_Estado,
                                                    Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                    gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                    gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                    gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                            }

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                                if (!gbool_TieneRF)
                                                {
                                                    lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                                }
                                                else
                                                    lstr_ResEnviarRev = "exito";

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }
                                    else if (gbool_TieneRP1 || gbool_TieneRP2)
                                    {
                                        #region

                                        #region Monto Principal
                                        //if (gdec_MontoPrincipalColRev == gdec_MontoPrincipalColones)
                                        //{
                                        //    garrdec_Montos = new Decimal[3];
                                        //    garrdec_Montos[0] = gdec_MontoPrincipalColRev;
                                        //    garrdec_Montos[1] = gdec_MontoPrincipalColones;

                                        //    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT05", gstr_Transaccion, true, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, 0);
                                        //}
                                        //else if (gdec_MontoPrincipalColRev > gdec_MontoPrincipalColones)
                                        //{
                                        //    gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColRev - gdec_MontoPrincipalColones;
                                        //    garrdec_Montos = new Decimal[3];
                                        //    garrdec_Montos[0] = gdec_MontoPrincipalColRev;
                                        //    garrdec_Montos[1] = gdec_MontoPrincipalColones;
                                        //    garrdec_Montos[2] = gdec_MontoAjustesPrincipal;

                                        //    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT07", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, 0, gdec_MontoAjustesPrincipal, null, null, null);
                                        //}
                                        //else if ((gdec_MontoPrincipalColRev == gdec_MontoPrincipalColones)
                                        //    && (gint_Periodo < DateTime.Today.Year))
                                        //{
                                        //    garrdec_Montos = new Decimal[3];
                                        //    garrdec_Montos[0] = gdec_MontoPrincipalColRev;
                                        //    garrdec_Montos[1] = gdec_MontoPrincipalColones;
                                        //    garrdec_Montos[2] = gdec_MontoAjustesPrincipal;

                                        //    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT08", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, 0);
                                        //}
                                        //else if (gdec_MontoPrincipalColRev < gdec_MontoPrincipalColones)
                                        //{
                                        //    garrdec_Montos = new Decimal[3];
                                        //    gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - gdec_MontoPrincipalColRev;
                                        //    garrdec_Montos[0] = gdec_MontoPrincipalColRev;
                                        //    garrdec_Montos[1] = gdec_MontoPrincipalColones;
                                        //    garrdec_Montos[2] = gdec_MontoAjustesPrincipal;
                                        //    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT09", gstr_Transaccion, true, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, 0);
                                        //}
                                        #endregion
                                        
                                        #region Monto Intereses
                                        //if (gdec_MontoInteresesColRev == gdec_MontoInteresesColones)
                                        //{
                                        //    garrdec_Montos = new Decimal[3];
                                        //    garrdec_Montos[0] = gdec_MontoPrincipalColRev;
                                        //    garrdec_Montos[1] = gdec_MontoPrincipalColones;

                                        //    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT05", gstr_Transaccion, true, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesColones);
                                        //}
                                        //else if (gdec_MontoInteresesColRev > gdec_MontoInteresesColones)
                                        //{
                                        //    gdec_MontoAjustesPrincipal = gdec_MontoInteresesColRev - gdec_MontoInteresesColones;
                                        //    garrdec_Montos = new Decimal[3];
                                        //    garrdec_Montos[0] = gdec_MontoInteresesColRev;
                                        //    garrdec_Montos[1] = gdec_MontoInteresesColones;
                                        //    garrdec_Montos[2] = gdec_MontoAjustesPrincipal;

                                        //    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT07", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesColones, gdec_MontoAjustesPrincipal, null, null, null);
                                        //}
                                        //else if ((gdec_MontoInteresesColRev == gdec_MontoInteresesColones)
                                        //    && (gint_Periodo < DateTime.Today.Year))
                                        //{
                                        //    garrdec_Montos = new Decimal[3];
                                        //    garrdec_Montos[0] = gdec_MontoInteresesColRev;
                                        //    garrdec_Montos[1] = gdec_MontoInteresesColones;
                                        //    garrdec_Montos[2] = gdec_MontoAjustesPrincipal;

                                        //    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT08", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesColones);
                                        //}
                                        //else if (gdec_MontoInteresesColRev < gdec_MontoInteresesColones)
                                        //{
                                        //    garrdec_Montos = new Decimal[3];
                                        //    gdec_MontoAjustesPrincipal = gdec_MontoInteresesColones - gdec_MontoInteresesColRev;
                                        //    garrdec_Montos[0] = gdec_MontoInteresesColRev;
                                        //    garrdec_Montos[1] = gdec_MontoInteresesColones;
                                        //    garrdec_Montos[2] = gdec_MontoAjustesPrincipal;
                                        //    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT09", gstr_Transaccion, true, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesColones);
                                        //}
                                        #endregion

                                        gint_EstadoPretension = 3;
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT10", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                     
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            if (!gbool_TieneRF) //No existe Resolucion
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                    Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                    gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                    gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                    gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                    gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                            }
                                            else
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                    Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                    gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                    gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                    gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                            }

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo";
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion

                                #region AC EnFirme Normal Sin Monto
                                else
                                {
                                    gdec_MontoInteresesColones = 0;
                                    gdec_MontoPrincipalColones = 0;
                                    gdec_ValorPresentePrincipal = 0;
                                    gdec_ValorPresenteIntereses = 0;
                                    gint_EstadoPretension = 1; //Activo con monto 0

                                    // Registra Resolución.
                                    if (!gbool_TieneRF) //No existe Resolucion
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                    }
                                    else 
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                            gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                            gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                    }

                                    if (larrstr_ResultResolucion[0].Contains("00"))
                                    {
                                        lstr_Resultado = "exito";
                                        asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                        if ((gbool_TienePretensionInicial) || (gbool_TieneRP1) || gbool_TieneRP2)
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 1, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); // suma monto ajustado
                                        else
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); // suma monto y cant
                                        
                                        if (lstr_ResEnviarRev.Contains("exito"))
                                        {
                                            lstr_Resultado = "exito";
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo2";
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo1";
                                    }

                                }
                                #endregion
                            }
                            else // SIN LUGAR
                            {
                                #region AC EnFirme Sin Lugar Con Monto
                                
                                gint_CxCaCxP = 0;
                                gstr_Estado = "Demandado";
                                gstr_Observaciones = "SinLugar";

                                if (gbool_Lleno) // Con Monto
                                {
                                    gint_EstadoPretension = 2; //Demandado Contabilizado

                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT06", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);//Plantilla Nº 10 Registrar cuenta por cobrar de un activo  

                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        if (!gbool_TieneRF)
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                        }
                                        else
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                        }

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_Resultado = "exito";
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            if (gbool_TienePretensionInicial || gbool_TieneRP1 || gbool_TieneRP2)
                                            {
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 6, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //resta

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                            else
                                                lstr_Resultado = "exito";
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo1";
                                        }
                                        
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo";
                                    }
                                   
                                }
                                #endregion

                                #region AC EnFirme Sin Lugar Sin Monto
                                else
                                {
                                    // Paso a Demandado
                                    gint_EstadoPretension = 0;

                                    if (!gbool_TieneRF)
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                    }
                                    else
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                            gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                            gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                    }

                                    if (larrstr_ResultResolucion[0].Contains("00"))
                                    {
                                        lstr_Resultado = "exito";
                                        asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                        if (gbool_TienePretensionInicial || gbool_TieneRP1 || gbool_TieneRP2)
                                        {
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 6, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //resta

                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //suma

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }
                                        else
                                        {
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //suma

                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo1";
                                    }
                                }
                                #endregion
                            }
                            break;
                        #endregion

                        default:
                            break;
                    }
                }
                #endregion

                #region Demandado
                else if (gstr_TipoExpediente.Contains("Demandado"))
                {
                    switch (gstr_EstadoResolucion) // RP1, RP2, RF
                    {
                        #region Demandado Provisional 1
                        case "Provisional 1":
                            if (!gbool_SinLugar) // NORMAL
                            {
                                gint_EstadoPretension = 2; //Pasivo

                                #region PC RP1 Normal ConMonto
                                if (gbool_Lleno)
                                {
                                    if (gbool_TieneRP1)
                                    {
                                        // 
                                        CambioTipoCambioAnterior();
                                        String lstr_ResultadoTemporal = String.Empty;
                                        gint_CantidadLineasAsiento = 2;

                                        #region Asientos Monto Principal

                                        #region Registro > Actual Diferente Periodo + Ajuste Diferencial Cambiario
                                        if ((gdec_MontoPrincipalColRev > gdec_MontoPrincipalColones) &&
                                            ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno))
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColRev - gdec_MontoPrincipalColones;

                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT04", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalAnterior - gdec_MontoPrincipal;

                                            gdec_MontoPrincipalAnterior = (gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre) - (gdec_MontoAjustesPrincipal * gdec_TipoCambioAnterior);

                                            if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC") && ldec_TipoCambioCierre != 0)
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                if (gdec_MontoPrincipalAnterior < 0)
                                                {
                                                    gdec_MontoPrincipalAnterior = gdec_MontoPrincipalAnterior * -1;
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT98", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                 }
                                                else
                                                {
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT96", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                   
                                                }
                                            }
                                        }

                                            
                                        #endregion
                                        
                                        #region Registo > Actual + Ajuste Diferencial Cambiario
                                        else if ((gdec_MontoPrincipalColRev > gdec_MontoPrincipalColones) &&
                                            (gbool_CambioMes || (lint_Mes < DateTime.Today.Month)))
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColRev - gdec_MontoPrincipalColones;
                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                           gdec_MontoAjustesPrincipal = gdec_MontoPrincipalAnterior - gdec_MontoPrincipal;

                                            gdec_MontoPrincipalAnterior = (gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre) - (gdec_MontoAjustesPrincipal * gdec_TipoCambioAnterior);

                                            if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC"))
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                if (gdec_MontoPrincipalAnterior < 0)
                                                {
                                                    gdec_MontoPrincipalAnterior = gdec_MontoPrincipalAnterior * -1;
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT97", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                }
                                                else
                                                {
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT95", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);

                                                }
                                            }
                                        }
                                        #endregion

                                        #region Actual > Registro + Ajuste Diferencial Cambiario
                                        else if ((gdec_MontoPrincipalColRev < gdec_MontoPrincipalColones) &&
                                            ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes))//(lint_Mes < DateTime.Today.Month))
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - gdec_MontoPrincipalColRev;
                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT02", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                           gdec_MontoAjustesPrincipal = gdec_MontoPrincipal - gdec_MontoPrincipalAnterior;

                                            gdec_MontoPrincipalAnterior = (gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre) - (gdec_MontoAjustesPrincipal * gdec_TipoCambioAnterior);

                                            if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC"))
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                if (gdec_MontoPrincipalAnterior < 0)
                                                {
                                                    gdec_MontoPrincipalAnterior = gdec_MontoPrincipalAnterior * -1;
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT95", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                }
                                                else
                                                {
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT97", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                }
                                            }

                                        }
                                        #endregion
                                        
                                        #region Registro > Actual
                                        else if (gdec_MontoPrincipalColRev > gdec_MontoPrincipalColones)
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColRev - gdec_MontoPrincipalColones;
                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                         }
                                        #endregion

                                        #region Actual > Registro
                                        else if (gdec_MontoPrincipalColRev < gdec_MontoPrincipalColones)
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - gdec_MontoPrincipalColRev;
                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT02", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                        }
                                        #endregion
                                        else
                                        {
                                            lstr_ResultadoTemporal = "Contabilizado";
                                        }
                                        #endregion

                                        #region Asientos Monto Intereses
                                        gdec_MontoAjustesIntereses = 0;

                                        #region Registro > Actual Diferente Periodo + Ajuste Diferencial Cambiario
                                        if ((gdec_MontoInteresesColRev > gdec_MontoInteresesColones) &&
                                            ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno))
                                        {
                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesColRev - gdec_MontoInteresesColones;

                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT04", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);

                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesAnterior - gdec_MontoIntereses;

                                            gdec_MontoInteresesAnterior = (gdec_MontoAjustesIntereses * ldec_TipoCambioCierre) - (gdec_MontoAjustesIntereses * gdec_TipoCambioAnterior);

                                            if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC") && ldec_TipoCambioCierre != 0)
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                if (gdec_MontoInteresesAnterior < 0)
                                                {
                                                    gdec_MontoInteresesAnterior = gdec_MontoInteresesAnterior * -1;
                                                    garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT98", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                                }
                                                else
                                                {
                                                    garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT96", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                                }
                                            }

                                        }
                                        #endregion

                                        #region Registo > Actual + Ajuste Diferencial Cambiario
                                        else if ((gdec_MontoInteresesColRev > gdec_MontoInteresesColones) &&
                                            ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes))//(lint_Mes < DateTime.Today.Month))
                                        {
                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesColRev - gdec_MontoInteresesColones;
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);

                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesAnterior - gdec_MontoIntereses;

                                            gdec_MontoInteresesAnterior = (gdec_MontoAjustesIntereses * ldec_TipoCambioCierre) - (gdec_MontoAjustesIntereses * gdec_TipoCambioAnterior);

                                            if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC"))
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                if (gdec_MontoInteresesAnterior < 0)
                                                {
                                                    gdec_MontoInteresesAnterior = gdec_MontoInteresesAnterior * -1;
                                                    garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT97", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                                }
                                                else
                                                {
                                                    garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT95", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Actual > Registro + Ajuste Diferencial Cambiario
                                        else if ((gdec_MontoInteresesColRev < gdec_MontoInteresesColones) &&
                                            ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes))//(lint_Mes < DateTime.Today.Month))
                                        {
                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesColones - gdec_MontoInteresesColRev;
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT02", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                            //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                            gdec_MontoAjustesIntereses = gdec_MontoIntereses - gdec_MontoInteresesAnterior;

                                            gdec_MontoInteresesAnterior = (gdec_MontoAjustesIntereses * ldec_TipoCambioCierre) - (gdec_MontoAjustesIntereses * gdec_TipoCambioAnterior);

                                            if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC"))
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                if (gdec_MontoInteresesAnterior < 0)
                                                {
                                                    gdec_MontoInteresesAnterior = gdec_MontoInteresesAnterior * -1;
                                                    garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT95", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                                }
                                                else
                                                {
                                                    garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT97", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Registo > Actual
                                        else if (gdec_MontoInteresesColRev > gdec_MontoInteresesColones)
                                        {
                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesColRev - gdec_MontoInteresesColones;
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                        }
                                        #endregion

                                        #region Registo > Actual
                                        else if (gdec_MontoInteresesColRev < gdec_MontoInteresesColones)
                                        {
                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesColones - gdec_MontoInteresesColRev;
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT02", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                        }
                                        #endregion

                                        else
                                        {
                                            lstr_Resultado = "Contabilizado";
                                        }
                                        #endregion

                                        #region Registro
                                        if (lstr_Resultado.Contains("Contabilizado") && lstr_ResultadoTemporal.Contains("Contabilizado"))
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                            gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                            gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas,gdec_DAnnoMoral,gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones,gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones,gdec_ValorPresenteInteresesColones,gstr_TipoTransaccion,gstr_EstadoTransaccion);

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                                if (gdec_MontoPrincipalColRev == 0)
                                                {
                                                    lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColRev, gdec_MontoPrincipalColRev, gbool_CambioMes); //borrar

                                                    if (lstr_ResEnviarRev.Contains("exito"))
                                                    {
                                                        lstr_Resultado = "exito";
                                                    }
                                                    else
                                                    {
                                                        lstr_Resultado = "fallo2";
                                                    }
                                                }
                                                else
                                                    lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }
                                    else if (gbool_TienePretensionInicial)
                                    {
                                        #region tiene PI -> Asiento-> Registro -> Revelación
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT01", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);

                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                //ws_SG.uwsModificarCodigoAsientoCo(0, Convert.ToInt32(gstr_IdResolucion), gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColRev, gdec_MontoPrincipalColRev, gbool_CambioMes); //borrar

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Asiento-> Registro
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT01", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                        //lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT01", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones);
                                      
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                
                                               //int x =  Convert.ToInt32(GetData("SELECT [IdCobroPagoResolucion] FROM [co].[CobrosPagos] where idexpedientefk='" + gstr_IdExpediente + "' and idresolucionfk = '" + gstr_IdResolucion + "'").Rows[0]["IdCobroPagoResolucion"]);

                                               asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                               //String[] res = ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, x, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                                    lstr_Resultado = "exito";
                                                

                                              
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }

                                }
                                #endregion

                                #region PC RP1 Normal SinMonto
                                else
                                {
                                    gint_EstadoPretension = 0; //Pasivo

                                    if (!gbool_TieneRP1)
                                    {
                                        #region No tiene RP1 Registro-> Revelación

                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                        gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                        gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                        gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_Resultado = "exito";
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            if (gbool_TienePretensionInicial)
                                            {
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 1, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); 

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                            else
                                            {
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); 

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo1";
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                        gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                        gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_Resultado = "exito";
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo1";
                                        }

                                    }
                                }
                                #endregion
                            }
                            else // SIN LUGAR
                            {
                                gint_EstadoPretension = 1; //Activo 
                                gint_CxCaCxP = 1;
                                gstr_Estado = "Activo";
                                gstr_TipoExpediente = "Actor";
                                gstr_Observaciones = "Reversion de Cuenta";

                                #region PC RP1 SinLugar ConSinMonto

                                if (gbool_TieneRP1)
                                {
                                    #region tiene RP1 AsientoRev-> Registro-> Revelación
                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT60", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                  
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_Resultado = "exito";
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo1";
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo";
                                    }
                                    #endregion
                                }
                                else if (gbool_TienePretensionInicial)
                                {
                                    #region tiene PI Revelación-> Registro-> Revelación
                                    lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //borrar monto cant

                                    if (lstr_ResEnviarRev.Contains("exito"))
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //crea monto cant

                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo1";
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo2";
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region  Registro-> Revelación AC 0
                                    larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                        gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                        gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                        gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                    if (larrstr_ResultResolucion[0].Contains("00"))
                                    {
                                       // ws_SG.uwsModificarCodigoAsientoCo(0, Convert.ToInt32(gstr_IdResolucion), gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                        
                                        lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes); //borrar

                                        if (lstr_ResEnviarRev.Contains("exito"))
                                        {
                                            lstr_Resultado = "exito";
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo2";
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo1";
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                            
                            break;
                        #endregion

                        #region Demandado Provisional 2
                        case "Provisional 2":
                            if (!gbool_SinLugar) // NORMAL
                            {
                                #region PC RP2 Normal conMonto
                                if(gbool_Lleno) // CON MONTO
                                {
                                    String lstr_ResultadoTemporal = String.Empty;
                                    gint_EstadoPretension = 2; //Pasivo

                                    if (gbool_TieneRP2)
                                    {
                                        gint_CantidadLineasAsiento = 2;
                                        CambioTipoCambioAnterior();

                                        #region Asientos Monto Principal

                                        #region Registro > Actual + Cam.Ano + Dif.Camb
                                        if ((gdec_MontoPrincipalColRev > gdec_MontoPrincipalColones) &&
                                            ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) )
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColRev - gdec_MontoPrincipalColones;
                                            //gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - gdec_MontoPrincipalColones;
                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT04", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalAnterior - gdec_MontoPrincipal;

                                            gdec_MontoPrincipalAnterior = (gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre) - (gdec_MontoAjustesPrincipal * gdec_TipoCambioAnterior);

                                            if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC") && ldec_TipoCambioCierre != 0)
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                if (gdec_MontoPrincipalAnterior < 0)
                                                {
                                                    gdec_MontoPrincipalAnterior = gdec_MontoPrincipalAnterior * -1;
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT98", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                }
                                                else
                                                {
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT96", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                              
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Registo > Actual + Ajuste Diferencial Cambiario
                                        else if ((gdec_MontoPrincipalColRev > gdec_MontoPrincipalColones) &&
                                            ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes))
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColRev - gdec_MontoPrincipalColones;
                                            //gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - gdec_MontoPrincipalColones;
                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);

                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalAnterior - gdec_MontoPrincipal;

                                            gdec_MontoPrincipalAnterior = (gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre) - (gdec_MontoAjustesPrincipal * gdec_TipoCambioAnterior);


                                            if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC") && ldec_TipoCambioCierre != 0)
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                if (gdec_MontoPrincipalAnterior < 0)
                                                {
                                                    gdec_MontoPrincipalAnterior = gdec_MontoPrincipalAnterior * -1;
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT97", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                }
                                                else
                                                {
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT95", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Actual > Registro + Ajuste Diferencial Cambiario
                                        else if ((gdec_MontoPrincipalColRev < gdec_MontoPrincipalColones) &&
                                            ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - gdec_MontoPrincipalColRev;
                                            //gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - (gdec_MontoPrincipalAnterior * gdec_TipoCambio);

                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT02", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);

                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipal - gdec_MontoPrincipalAnterior;

                                            gdec_MontoPrincipalAnterior = (gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre) - (gdec_MontoAjustesPrincipal * gdec_TipoCambioAnterior);

                                            if (lstr_ResultadoTemporal.Contains("Contabilizado"))
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                if (gdec_MontoPrincipalAnterior < 0)
                                                {
                                                    gdec_MontoPrincipalAnterior = gdec_MontoPrincipalAnterior * -1;
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT95", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                }
                                                else
                                                {
                                                    garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT97", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                }
                                            }
                                        }
                                        #endregion

                                        else if (gdec_MontoPrincipalColRev > gdec_MontoPrincipalColones)
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColRev - gdec_MontoPrincipalColones;
                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                          
                                        }
                                        else if (gdec_MontoPrincipalColRev < gdec_MontoPrincipalColones)
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - gdec_MontoPrincipalColRev;
                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT02", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                           
                                        }
                                        else
                                        {
                                            lstr_ResultadoTemporal = "Contabilizado";
                                        }
                                        #endregion

                                        #region Asientos Monto Intereses
                                        gdec_MontoAjustesPrincipal = 0;

                                        #region Reg > Actual + camb.Ano + Dif.camb
                                        if ((gdec_MontoInteresesColRev > gdec_MontoInteresesColones) &&
                                            ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno))
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoInteresesColRev - gdec_MontoInteresesColones;
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT04", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesPrincipal, out lstr_CodAsiento);
                                         
                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesAnterior - gdec_MontoIntereses;

                                            gdec_MontoInteresesAnterior = (gdec_MontoAjustesIntereses * ldec_TipoCambioCierre) - (gdec_MontoAjustesIntereses * gdec_TipoCambioAnterior);

                                            if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC") && ldec_TipoCambioCierre != 0)
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                if (gdec_MontoInteresesAnterior < 0)
                                                {                                                    
                                                    gdec_MontoInteresesAnterior = gdec_MontoInteresesAnterior * -1;
                                                    garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT98", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                            
                                                }
                                                else
                                                {
                                                    garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                    lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT96", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                  
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Registo > Actual + Ajuste Diferencial Cambiario
                                        else if ((gdec_MontoInteresesColRev > gdec_MontoInteresesColones) &&
                                            ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes))
                                        {
                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesColRev - gdec_MontoInteresesColones;
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                         
                                            if (ldec_TipoCambioCierre != 0)
                                            {
                                                gdec_MontoAjustesIntereses = gdec_MontoInteresesAnterior - gdec_MontoIntereses;

                                                gdec_MontoInteresesAnterior = (gdec_MontoAjustesIntereses * ldec_TipoCambioCierre) - (gdec_MontoAjustesIntereses * gdec_TipoCambioAnterior);

                                                if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC"))
                                                {
                                                    garrdec_Montos = new Decimal[15];
                                                    if (gdec_MontoInteresesAnterior < 0)
                                                    {
                                                        garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                        gdec_MontoInteresesAnterior = gdec_MontoInteresesAnterior * -1;
                                                        lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT97", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);

                                                    }
                                                    else
                                                    {
                                                        garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                        lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT95", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Actual > Registro + Ajuste Diferencial Cambiario
                                        else if ((gdec_MontoInteresesColRev < gdec_MontoInteresesColones) &&
                                            ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes))
                                        {
                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesColones - gdec_MontoInteresesColRev;
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT02", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                            
                                            if (ldec_TipoCambioCierre != 0)
                                            {
                                                gdec_MontoAjustesIntereses = gdec_MontoIntereses - gdec_MontoInteresesAnterior;

                                                gdec_MontoInteresesAnterior = (gdec_MontoAjustesIntereses * ldec_TipoCambioCierre) - (gdec_MontoAjustesIntereses * gdec_TipoCambioAnterior);


                                                if (lstr_ResultadoTemporal.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC"))
                                                {
                                                    garrdec_Montos = new Decimal[15];
                                                    if (gdec_MontoInteresesAnterior < 0)
                                                    {
                                                        gdec_MontoInteresesAnterior = gdec_MontoInteresesAnterior * -1;
                                                        garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                        lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT95", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);

                                                    }
                                                    else
                                                    {
                                                        garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                        lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT97", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                    }
                                                }

                                            }
                                        }
                                        #endregion

                                        else if (gdec_MontoInteresesColRev > gdec_MontoInteresesColones)
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoInteresesColRev - gdec_MontoInteresesColones;
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesPrincipal, out lstr_CodAsiento);
                                          
                                        }
                                        else if (gdec_MontoInteresesColRev < gdec_MontoInteresesColones)
                                        {
                                            gdec_MontoAjustesPrincipal = gdec_MontoInteresesColones - gdec_MontoInteresesColRev;
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT02", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesPrincipal, out lstr_CodAsiento);
                                           
                                        }
                                        else
                                        {
                                            lstr_Resultado = "Contabilizado";
                                        }
                                        #endregion

                                        #region Registro
                                        if (lstr_Resultado.Contains("Contabilizado") && lstr_ResultadoTemporal.Contains("Contabilizado"))
                                        {
                                            if (!gbool_TieneRP2)
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                            }
                                            else
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                    Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                    gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                    gdec_ValorPresentePrincipal, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                    gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                            }
                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo2";
                                        }
                                        #endregion
                                    }
                                    else if(gbool_TieneRP1)
                                    {
                                        gint_CantidadLineasAsiento = 2;

                                        #region Asientos Monto Principal
                                        #region Registo > Actual + Ajuste Dif.Cam
                                        if (gdec_MontoPrincipalColRev > gdec_MontoPrincipalColones) 
                                        {
                                            //if ((ldec_TipoCambioCierre != 0) && ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                            //{
                                            //    gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - ldec_PrincipalCierre;
                                            //    if (gdec_MontoAjustesPrincipal < 0)
                                            //    {
                                            //        gdec_MontoAjustesPrincipal = gdec_MontoAjustesPrincipal * -1;
                                            //        lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT29", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0);
                                            //    }
                                            //    else
                                            //        lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT28", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0);
                                            //}
                                            if ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno)
                                            {
                                                //CambioTipoCambioAnterior();
                                                //gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColRev - (gdec_MontoPrincipal * gdec_TipoCambio);
                                                gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - gdec_MontoPrincipalColones;

                                                lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT04", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                           
                                            }
                                            else
                                            {
                                                //CambioTipoCambioAnterior();

                                                //gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - gdec_MontoPrincipalColones;

                                                gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - gdec_MontoPrincipalColones;
                                          
                                                //gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColRev - (gdec_MontoPrincipal * gdec_TipoCambio);
                                                lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                               
                                            }
                                        }
                                        #endregion

                                        #region Actual > Registro + Ajuste Diferencial Cambiario
                                        else if (gdec_MontoPrincipalColRev < gdec_MontoPrincipalColones)
                                        {
                                            //if ((ldec_TipoCambioCierre != 0) && ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                            //{
                                            //    gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - ldec_PrincipalCierre;
                                            //    if (gdec_MontoAjustesPrincipal < 0)
                                            //    {
                                            //        gdec_MontoAjustesPrincipal = gdec_MontoAjustesPrincipal * -1;
                                            //        lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT29", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0);
                                            //    }
                                            //    else
                                            //        lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT28", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0);
                                            //}
                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - (gdec_MontoPrincipalAnterior * gdec_TipoCambio);
                                          

                                            
                                            //gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - (gdec_MontoPrincipalAnterior * gdec_TipoCambio);
                                            lstr_ResultadoTemporal = asiento.enviar(gstr_InModuloCT, "CT02", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                          
                                        }
                                        #endregion
                                        
                                        else
                                        {
                                            lstr_ResultadoTemporal = "Contabilizado";
                                        }
                                        #endregion

                                        #region Asientos Monto Intereses
                                        gdec_MontoAjustesPrincipal = 0;
                                        gdec_MontoAjustesIntereses = 0;

                                        #region Registro > Actual
                                        if (gdec_MontoInteresesColRev > gdec_MontoInteresesColones) 
                                        {
                                        //    if ((ldec_TipoCambioCierre != 0) && ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                        //    {
                                        //        gdec_MontoAjustesIntereses = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - ldec_InteresesCierre;
                                        //        if (gdec_MontoAjustesIntereses < 0)
                                        //        {
                                        //            gdec_MontoAjustesIntereses = gdec_MontoAjustesIntereses * -1;
                                        //            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT29", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses);
                                        //        }
                                        //        else
                                        //            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT28", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses);
                                        //    }
                                            if ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno)
                                            {
                                               // gdec_MontoAjustesIntereses = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - gdec_MontoInteresesColones;
                                                gdec_MontoAjustesIntereses = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - gdec_MontoInteresesColones;

                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT04", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                                
                                            }
                                            else
                                            {
                                                //gdec_MontoAjustesIntereses = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - gdec_MontoInteresesColones;

                                                gdec_MontoAjustesIntereses = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - gdec_MontoInteresesColones;
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                               
                                            }
                                        }
                                        #endregion

                                        #region Actual > Registro + Ajuste Diferencial Cambiario
                                        else if (gdec_MontoInteresesColRev < gdec_MontoInteresesColones)
                                        {
                                            //if ((ldec_TipoCambioCierre != 0) && ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                            //{
                                            //    gdec_MontoAjustesPrincipal = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - ldec_InteresesCierre;
                                            //    if (gdec_MontoAjustesPrincipal < 0)
                                            //    {
                                            //        gdec_MontoAjustesPrincipal = gdec_MontoAjustesPrincipal * -1;
                                            //        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT29", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0);
                                            //    }
                                            //    else
                                            //        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT28", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0);
                                            //}

                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesColones - (gdec_MontoInteresesAnterior * gdec_TipoCambio);
                                          
                                            //gdec_MontoAjustesPrincipal = (gdec_MontoIntereses * gdec_TipoCambio) - gdec_MontoInteresesColRev;
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT02", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                         
                                        }
                                        #endregion

                                        else
                                        {
                                            lstr_Resultado = "Contabilizado";
                                        }
                                        #endregion

                                        #region Registro
                                        if (lstr_Resultado.Contains("Contabilizado") && lstr_ResultadoTemporal.Contains("Contabilizado"))
                                        {
                                            if (!gbool_TieneRP2)
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                            }
                                            else
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                    Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                    gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                    gdec_ValorPresentePrincipal, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                    gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                            }
                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo2";
                                        }
                                        #endregion
                                    }
                                    else if (gbool_TienePretensionInicial)
                                    {
                                        #region tiene PI Asiento-> Registo-> Revelación
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT01", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                          
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {

                                            if (!gbool_TieneRP2)
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);
                                            }
                                            else
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                    Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                    gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                    gdec_ValorPresentePrincipal, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                    gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                            }

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColRev, gdec_MontoPrincipalColRev, gbool_CambioMes); //borrar
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Asiento-> Registro
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT01", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                         
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            
                                #region PC RP2 Normal SinMonto
                                else//SIN MONTO
                                {
                                    gint_EstadoPretension = 0; //Pasivo
                                    if (gbool_TieneRP2 || gbool_TieneRP1)// PC SM conRP1 o conPI
                                    {
                                        #region tiene RP -> Asiento-> Registro-> Revelación
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT03", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                         
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                            gdec_ValorPresentePrincipal, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                            gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                                if (gdec_MontoPrincipalColRev == 0)
                                                {
                                                    lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColRev, gdec_MontoPrincipalColRev, gbool_CambioMes); //borrar

                                                    if (lstr_ResEnviarRev.Contains("exito"))
                                                    {
                                                        lstr_Resultado = "exito";
                                                    }
                                                    else
                                                    {
                                                        lstr_Resultado = "fallo2";
                                                    }
                                                }
                                                else
                                                    lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }
                                    else // PC SM sinPI o sin RPI
                                    {
                                        #region Registro-> Revelación PC 0
                                        gint_EstadoPretension = 0; //Pasivo

                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            //ws_SG.uwsModificarCodigoAsientoCo(0, Convert.ToInt32(gstr_IdResolucion), gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            if (gbool_TienePretensionInicial)
                                            {
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 1, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);

                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo3";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "exito";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }
                                }
                                #endregion
                            }
                            else // Declarar sin lugar
                            {
                                gint_EstadoPretension = 1; //Activo 
                                gint_CxCaCxP = 1;
                                gstr_Estado = "Activo";
                                gstr_TipoExpediente = "Actor";
                                gstr_Observaciones = "Reversion de Cuenta";

                                #region PC RP2 SinLugar ConSinMonto
                                if (gbool_TieneRP1 || gbool_TieneRP2)
                                {
                                    #region AsientoRev-> Registro-> Revelación
                                    if ((gdec_MontoPrincipalColRev == 0) || (gdec_MontoInteresesColRev == 0))
                                        gint_CantidadLineasAsiento = 2;
                                    
                                    if ((gdec_MontoPrincipalColRev == 0) && (gdec_MontoInteresesColRev == 0))
                                        lstr_Resultado = "Contabilizado";
                                    else
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT60", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColRev, gdec_MontoInteresesColRev, out lstr_CodAsiento);
                                          
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        if (gdec_MontoPrincipalColRev == 0)
                                        {
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);

                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }

                                        if(gbool_TieneRP2)
                                            larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                        else
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_Resultado = "exito";
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo1";
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo";
                                    }
                                    #endregion
                                }
                                else if (gbool_TienePretensionInicial)
                                {
                                    #region Revelación-> Registro-> Revelación AC
                                    //lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                    lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);

                                    if (lstr_ResEnviarRev.Contains("exito"))
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_Resultado = "exito";
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo1";
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo2";
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region Registro-> Revelación AC
                                    larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                        gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                        gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                        gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                    if (larrstr_ResultResolucion[0].Contains("00"))
                                    {
                                        lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                        //ws_SG.uwsModificarCodigoAsientoCo(0, Convert.ToInt32(gstr_IdResolucion), gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                        asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                        if (lstr_ResEnviarRev.Contains("exito"))
                                        {
                                            lstr_Resultado = "exito";
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo3";
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo";
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            break;
                        #endregion

                        #region Demandado En Firme
                        case "En Firme":
                            if (!gbool_SinLugar) // NORMAL
                            {
                                #region PC EnFirme Normal con Monto
                                if (gbool_Lleno)
                                {
                                    gint_EstadoPretension = 2; //Pasivo
                                    //if (gbool_TieneRF)
                                        
                                    if (gbool_TieneRF)
                                    {
                                        glbool_cambioMonto = false;
                                        
                                        #region Monto Principal

                                        #region Actual > Registro

                                        if (gdec_MontoPrincipalColones > gdec_MontoPrincipalColRev)
                                        {
                                            gdec_MontoAjustesPrincipal = (gdec_MontoPrincipal * gdec_TipoCambioAnterior) - (gdec_MontoPrincipalAnterior * gdec_TipoCambioAnterior);

                                            garrdec_Montos = new Decimal[15];
                                            garrdec_Montos[0] = gdec_MontoAjustesPrincipal;
                                            garrdec_Montos[1] = gdec_MontoAjustesPrincipal;

                                            gint_CantidadLineasAsiento = 4;

                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT06", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                         
                                            if (lstr_Resultado.Contains("Contabilizado") && ldec_TipoCambioCierre != 0 &&!gstr_MonedaAnterior.Contains("CRC") && 
                                                (((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) || ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes)))
                                            {
                                                gdec_MontoAjustesPrincipal = gdec_MontoPrincipal - gdec_MontoPrincipalAnterior;

                                                gdec_MontoPrincipalAnterior = (gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre) - (gdec_MontoAjustesPrincipal * gdec_TipoCambioAnterior);

                                                garrdec_Montos = new Decimal[15];
                                                garrdec_Montos[0] = gdec_MontoPrincipalAnterior;

                                                if (gdec_MontoPrincipalAnterior < 0)
                                                {
                                                    gdec_MontoPrincipalAnterior = gdec_MontoPrincipalAnterior * -1;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT22", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                   
                                                }
                                                else
                                                {
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT23", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                                }
                                            }
                                        }

                                        #endregion

                                        #region Registro > Actual

                                        else if (gdec_MontoPrincipalColRev > gdec_MontoPrincipalColones)
                                        {
                                            gdec_MontoAjustesPrincipal = Math.Abs((gdec_MontoPrincipal * gdec_TipoCambioAnterior) - (gdec_MontoPrincipalAnterior * gdec_TipoCambioAnterior));
                                            //gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - gdec_MontoPrincipalColones;
                                            String CT = ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) ? "CT68" : "CT67";
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, CT, gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                           gdec_MontoAjustesPrincipal = gdec_MontoPrincipalAnterior - gdec_MontoPrincipal;

                                            gdec_MontoPrincipalAnterior = (gdec_MontoAjustesPrincipal * ldec_TipoCambioCierre) - (gdec_MontoAjustesPrincipal * gdec_TipoCambioAnterior);

                                            if (lstr_Resultado.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC") && ldec_TipoCambioCierre != 0
                                                && (((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) || ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes)))
                                            {
                                                garrdec_Montos = new Decimal[15];
                                                String CT_diferencial = gdec_MontoPrincipalAnterior < 0 ? "CT98" : "CT95";
                                                gdec_MontoPrincipalAnterior = Math.Abs(gdec_MontoPrincipalAnterior);
                                                garrdec_Montos[0] = gdec_MontoPrincipalAnterior;
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, CT_diferencial, gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalAnterior, 0, out lstr_CodAsiento);
                                               }
                                        }
                                        #endregion

                                        #endregion

                                        #region Intereses

                                        #region Actual > Registro
                                        if (gdec_MontoInteresesColones > gdec_MontoInteresesColRev)
                                        {
                                            gdec_MontoAjustesIntereses = (gdec_MontoIntereses * gdec_TipoCambioAnterior) - (gdec_MontoInteresesAnterior * gdec_TipoCambioAnterior);

                                            garrdec_Montos = new Decimal[15];
                                            garrdec_Montos[2] = gdec_MontoAjustesIntereses;
                                            garrdec_Montos[3] = gdec_MontoAjustesIntereses;

                                            gint_CantidadLineasAsiento = 4;

                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT06", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                           
                                            if (lstr_Resultado.Contains("Contabilizado") && ldec_TipoCambioCierre != 0 && !gstr_MonedaAnterior.Contains("CRC") &&
                                                (((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) || ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes)))
                                            {
                                                gdec_MontoAjustesIntereses = gdec_MontoIntereses - gdec_MontoInteresesAnterior;
                                                gdec_MontoInteresesAnterior = (gdec_MontoAjustesIntereses * ldec_TipoCambioCierre) - (gdec_MontoAjustesIntereses * gdec_TipoCambioAnterior);

                                                garrdec_Montos = new Decimal[15];
                                                garrdec_Montos[1] = gdec_MontoInteresesAnterior;

                                                if (gdec_MontoPrincipalAnterior < 0)
                                                {
                                                    gdec_MontoInteresesAnterior = gdec_MontoInteresesAnterior * -1;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT22", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);

                                                }
                                                else
                                                {
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT23", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);

                                                }
                                                
                                            }
                                        }
                                        #endregion

                                        #region Registro > Actual 
                                        if (gdec_MontoInteresesColRev > gdec_MontoInteresesColones)
                                        {
                                            gdec_MontoAjustesPrincipal = Math.Abs((gdec_MontoIntereses * gdec_TipoCambioAnterior) - (gdec_MontoInteresesAnterior * gdec_TipoCambioAnterior));
                                            String CT = ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) ? "CT68" : "CT67";

                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, CT, gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesPrincipal, out lstr_CodAsiento);
                                            //ws_SG.uwsModificarCodigoAsientoCo(0, Convert.ToInt32(gstr_IdResolucion), gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                    
                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesAnterior - gdec_MontoIntereses;

                                            gdec_MontoInteresesAnterior = (gdec_MontoAjustesIntereses * ldec_TipoCambioCierre) - (gdec_MontoAjustesIntereses * gdec_TipoCambioAnterior);

                                            if (lstr_Resultado.Contains("Contabilizado") && !gstr_MonedaAnterior.Contains("CRC") && ldec_TipoCambioCierre != 0
                                                && (((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno) || ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes)))
                                            {

                                                garrdec_Montos = new Decimal[15];
                                                String CT_diferencial = gdec_MontoPrincipalAnterior < 0 ? "CT98" : "CT95";
                                                gdec_MontoPrincipalAnterior = Math.Abs(gdec_MontoPrincipalAnterior);
                                                garrdec_Montos[1] = gdec_MontoInteresesAnterior;
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, CT_diferencial, gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesAnterior, out lstr_CodAsiento);
                                                //ws_SG.uwsModificarCodigoAsientoCo(0, Convert.ToInt32(gstr_IdResolucion), gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                    
                                            }
                                        }
                                        #endregion

                                        #endregion

          #endregion





                                        //else if (gdec_MontoInteresesColRev < gdec_MontoInteresesColones)
                                        //{
                                        //    gdec_MontoAjustesPrincipal = gdec_MontoInteresesColones - gdec_MontoInteresesColRev;

                                        //    garrdec_Montos = new Decimal[2];
                                        //    garrdec_Montos[0] = gdec_MontoAjustesPrincipal;
                                        //    garrdec_Montos[1] = gdec_MontoAjustesPrincipal;

                                        //    gint_CantidadLineasAsiento = 2;

                                        //    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT06", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesPrincipal);

                                        //}
                                        //else if (!lstr_Resultado.Contains("Contabilizado"))
                                        //    lstr_Resultado = "exito";

                                        
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            if (gbool_TieneRF)
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                            gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                            gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                            }
                                            else
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                        gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                        gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                        gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                            }

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else if (lstr_Resultado.Contains("exito"))
                                        {
                                            lstr_Resultado = "texto1";
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        
                                    }
                                    else if (gbool_TieneRP1 || gbool_TieneRP2)
                                    {
                                        //CambioTipoCambioAnterior();

                                        gint_CantidadLineasAsiento = 4;
                                        garrdec_Montos = new Decimal[15];

                                        #region Asientos Monto Principal

                                        #region Registro = Actual
                                        if (gdec_MontoPrincipalColRev == gdec_MontoPrincipalColones)
                                        {
                                            if (((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                            {
                                                gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - (gdec_MontoPrincipalAnterior * ldec_TipoCambioCierre);

                                                if (gdec_MontoAjustesPrincipal < 0)
                                                {
                                                    gdec_MontoAjustesPrincipal = gdec_MontoAjustesPrincipal * -1;
                                                    garrdec_Montos[0] = gdec_MontoAjustesPrincipal;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT29", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                                  
                                                }
                                                else
                                                {
                                                    garrdec_Montos[0] = gdec_MontoAjustesPrincipal;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT28", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                                   
                                                }
                                            }

                                            gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - gdec_MontoPrincipalColones;
                                            garrdec_Montos[0] = gdec_MontoPrincipalAnterior * gdec_TipoCambio;
                                            garrdec_Montos[1] = gdec_MontoPrincipalColones;
                                            garrdec_Montos[2] = gdec_MontoAjustesPrincipal;

                                            if ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno)
                                            {
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT08", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColRev, 0, out lstr_CodAsiento);           
                                                 
                                            } 
                                            else
                                            {
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT05", gstr_Transaccion, true, null, gint_CantidadLineasAsiento, gdec_MontoPrincipalColRev, 0, out lstr_CodAsiento);
                                                
                                            }
                                                 }
                                        #endregion

                                        #region Registro > Actual
                                        else if (gdec_MontoPrincipalColRev > gdec_MontoPrincipalColones)
                                        {
                                            if (((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                            {
                                                gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - (gdec_MontoPrincipalAnterior * ldec_TipoCambioCierre);
                                                //gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - (gdec_MontoPrincipalAnterior * gdec_TipoCambio);

                                                if (gdec_MontoAjustesPrincipal < 0)
                                                {
                                                    gdec_MontoAjustesPrincipal = gdec_MontoAjustesPrincipal * -1;
                                                    garrdec_Montos[0] = gdec_MontoAjustesPrincipal;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT29", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                                   
                                                }
                                                else
                                                {
                                                    garrdec_Montos[0] = gdec_MontoAjustesPrincipal;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT28", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                                    
                                                }
                                            }

                                            //gdec_MontoAjustesPrincipal = (ldec_PrincipalCierre + gdec_MontoPrincipalAnterior) - gdec_MontoPrincipalColones;
                                            gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - gdec_MontoPrincipalColones;
                                            garrdec_Montos[0] = gdec_MontoPrincipalAnterior * gdec_TipoCambio;
                                            garrdec_Montos[1] = gdec_MontoPrincipalColones;
                                            garrdec_Montos[2] = gdec_MontoAjustesPrincipal;

                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT07", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, 0, out lstr_CodAsiento);
                                          
                                        }
                                        #endregion

                                        #region  Registro < Actual
                                        else if (gdec_MontoPrincipalColRev < gdec_MontoPrincipalColones)
                                        {
                                            if (((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                            {
                                                gdec_MontoAjustesPrincipal = (gdec_MontoPrincipalAnterior * gdec_TipoCambio) - (gdec_MontoPrincipalAnterior * ldec_TipoCambioCierre);
                                               // gdec_MontoAjustesPrincipal = (gdec_MontoPrincipal * gdec_TipoCambioAnterior) - (gdec_MontoPrincipal * gdec_TipoCambio);

                                                if (gdec_MontoAjustesPrincipal < 0)
                                                {
                                                    gdec_MontoAjustesPrincipal = gdec_MontoAjustesPrincipal * -1;
                                                    garrdec_Montos[0] = gdec_MontoAjustesPrincipal;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT29", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                                   
                                                } 
                                                else
                                                {
                                                    garrdec_Montos[0] = gdec_MontoAjustesPrincipal;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT28", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoAjustesPrincipal, 0, out lstr_CodAsiento);
                                                   
                                                }
                                            }

                                            gdec_MontoAjustesPrincipal = gdec_MontoPrincipalColones - (gdec_MontoPrincipalAnterior * gdec_TipoCambio);
                                            garrdec_Montos[0] = gdec_MontoPrincipalAnterior * gdec_TipoCambio;
                                            garrdec_Montos[1] = gdec_MontoPrincipalColones;
                                            garrdec_Montos[2] = gdec_MontoAjustesPrincipal;

                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT09", gstr_Transaccion, true, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, 0, out lstr_CodAsiento);
                                            
                                        }
                                        #endregion
                                        #endregion

                                        #region Asientos Monto Intereses

                                        #region Registro = Actual
                                        if (gdec_MontoInteresesColRev == gdec_MontoInteresesColones)
                                        {
                                            if (((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                            {
                                                //gdec_MontoInteresesAnterior = (gdec_MontoIntereses * ldec_TipoCambioCierre) - (gdec_MontoIntereses * gdec_TipoCambioAnterior);
                                                gdec_MontoAjustesIntereses = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - (gdec_MontoInteresesAnterior * ldec_TipoCambioCierre);

                                                if (gdec_MontoAjustesIntereses < 0)
                                                {
                                                    gdec_MontoAjustesIntereses = gdec_MontoAjustesIntereses * -1;
                                                    garrdec_Montos[1] = gdec_MontoAjustesIntereses;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT29", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                                    
                                                }
                                                else
                                                {
                                                    garrdec_Montos[1] = gdec_MontoAjustesIntereses;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT28", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                                   
                                                }
                                            }

                                            gdec_MontoAjustesIntereses = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - gdec_MontoInteresesColones;
                                            garrdec_Montos[0] = gdec_MontoInteresesAnterior * gdec_TipoCambio;
                                            garrdec_Montos[1] = gdec_MontoInteresesColones;
                                            garrdec_Montos[2] = gdec_MontoAjustesIntereses;


                                            if ((gint_Periodo < DateTime.Today.Year) || gbool_CambioAno)
                                            {
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT08", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                                }
                                            else
                                            {
                                                lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT05", gstr_Transaccion, true, null, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                                }
                                        }
                                        #endregion

                                        #region Registro > Actual
                                        else if (gdec_MontoInteresesColRev > gdec_MontoInteresesColones)
                                        {
                                            if( ((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                            {
                                                gdec_MontoAjustesIntereses = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - (gdec_MontoInteresesAnterior * ldec_TipoCambioCierre);

                                                if (gdec_MontoAjustesIntereses < 0)
                                                {
                                                    gdec_MontoAjustesIntereses = gdec_MontoAjustesIntereses * -1;
                                                    garrdec_Montos[1] = gdec_MontoAjustesIntereses;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT29", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                                   
                                                }
                                                else
                                                {
                                                    garrdec_Montos[1] = gdec_MontoAjustesIntereses;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT28", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                                   
                                                }
                                            }

                                            //gdec_MontoAjustesIntereses = (ldec_InteresesCierre + gdec_MontoInteresesAnterior) - gdec_MontoInteresesColones;
                                            gdec_MontoAjustesIntereses = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - gdec_MontoInteresesColones;
                                            garrdec_Montos[0] = gdec_MontoInteresesAnterior * gdec_TipoCambio;
                                            garrdec_Montos[1] = gdec_MontoInteresesColones;
                                            garrdec_Montos[2] = gdec_MontoAjustesIntereses;

                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT07", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                           
                                        }
                                        #endregion

                                        #region Registro < Actual
                                        else if (gdec_MontoInteresesColRev < gdec_MontoInteresesColones)
                                        {
                                            if (((lint_Mes < DateTime.Today.Month) || gbool_CambioMes) && !gstr_MonedaAnterior.Contains("CRC"))
                                            {
                                                gdec_MontoAjustesIntereses = (gdec_MontoInteresesAnterior * gdec_TipoCambio) - (gdec_MontoInteresesAnterior * ldec_TipoCambioCierre);

                                                if (gdec_MontoAjustesIntereses < 0)
                                                {
                                                    gdec_MontoAjustesIntereses = gdec_MontoAjustesIntereses * -1;
                                                    garrdec_Montos[1] = gdec_MontoAjustesIntereses;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT29", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                                  
                                                }
                                                else
                                                {
                                                    garrdec_Montos[1] = gdec_MontoAjustesIntereses;
                                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT28", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoAjustesIntereses, out lstr_CodAsiento);
                                                   
                                                }
                                            }

                                            gdec_MontoAjustesIntereses = gdec_MontoInteresesColones - (gdec_MontoInteresesAnterior * gdec_TipoCambio);
                                            garrdec_Montos[0] = gdec_MontoInteresesAnterior * gdec_TipoCambio;
                                            garrdec_Montos[1] = gdec_MontoInteresesColones;
                                            garrdec_Montos[2] = gdec_MontoAjustesIntereses;

                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT09", gstr_Transaccion, true, garrdec_Montos, gint_CantidadLineasAsiento, 0, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                            //ws_SG.uwsModificarCodigoAsientoCo(0, Convert.ToInt32(gstr_IdResolucion), gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                    
                                        }
                                        #endregion

                                        #region Registro
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            if (gbool_TieneRF)
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                            gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                            gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                            }
                                            else
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                        gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                        gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                        gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                            }

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion

                                        #endregion
                                    }
                                    else if (gbool_TienePretensionInicial)
                                    {
                                        #region Asiento-> Revelación-> Registro
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT06", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                         
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);

                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                        gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                        gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                        gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                                if (larrstr_ResultResolucion[0].Contains("00"))
                                                {
                                                    lstr_Resultado = "exito";
                                                    asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo1";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Asiento-> Registro
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT06", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);

                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }

                                }
                                #endregion

                        #region PC EnFirme Normal Sin Monto
                                else
                                {
                                    gint_EstadoPretension = 0; //Pasivo
                                    if (gbool_TieneRP1 || gbool_TieneRP2)
                                    {
                                        #region AsientoRev-> Registro-> Revelación PC 0
                                        lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT60", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColRev, gdec_MontoInteresesColRev, out lstr_CodAsiento);
                                          
                                        if (lstr_Resultado.Contains("Contabilizado"))
                                        {
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }
                                    else if (gbool_TienePretensionInicial)
                                    {
                                        #region Registro-> Revelación PC 0
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                        gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                        gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                        gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_Resultado = "exito";
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 1, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo3";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Registro-> Revelación PC 0
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                        Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                        gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                        gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                        gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                        gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                            lstr_Resultado = "exito";
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            if (lstr_ResEnviarRev.Contains("exito"))
                                            {
                                                lstr_Resultado = "exito";
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo2";
                                            }
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo1";
                                        }
                                        #endregion
                                    }

                                }
                                #endregion
                            }
                            else // SIN LUGAR
                            {
                                gint_EstadoPretension = 1; //Activo sin revel
                                gint_CxCaCxP = 1;
                                gstr_Estado = "Activo";
                                gstr_TipoExpediente = "Actor";
                                gstr_Observaciones = "Reversion de Cuenta";

                                #region PC EnFirme SinLugar ConSinMonto
                                if (gbool_TieneRP1 || gbool_TieneRP2)
                                {
                                    #region AsientoRev-> Registro-> Revelación
                                    if ((gdec_MontoPrincipalColRev == 0) || (gdec_MontoInteresesColRev == 0))
                                        gint_CantidadLineasAsiento = 2;

                                    //lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT60", gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColRev, gdec_MontoInteresesColRev, out lstr_CodAsiento);

                                    #region Reversar asientos diferencial cambiario

                                    String lstr_query = "SELECT IdExp FROM co.Expedientes exp " +
                                                        "WHERE exp.IdExpediente ='" + gstr_IdExpediente + "' " +
                                                        "AND exp.IdSociedadGL ='" + gstr_Sociedad + "' " +
                                                        "AND exp.EstadoExpediente = 'Activo'";
                                    DataTable dt_Resoluciones = GetData(lstr_query);

                                    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                    try
                                    {
                                        Int32 lint_IdExp = 0;

                                        foreach (DataRow dr_Resolucion in dt_Resoluciones.Rows)
                                        {
                                            lint_IdExp = Convert.ToInt32(dr_Resolucion["IdExp"]);
                                        }

                                        DataTable ldt_CobrosPagos = ConsultarCobrosPagos(gstr_IdExpediente, lint_IdExp); // Falta el id Res

                                        foreach (DataRow dr_CobrosPagos in ldt_CobrosPagos.Rows)
                                        {
                                            String lstr_IdOperacion = dr_CobrosPagos["IdResolucionFK"].ToString();

                                            Decimal lstr_Monto1 = Convert.ToDecimal(dr_CobrosPagos["MontoPrincipal"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto2 = Convert.ToDecimal(dr_CobrosPagos["MontoPrincipalColones"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto3 = Convert.ToDecimal(dr_CobrosPagos["MontoPrincipalColonesCierre"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto4 = Convert.ToDecimal(dr_CobrosPagos["MontoIntereses"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto5 = Convert.ToDecimal(dr_CobrosPagos["MontoInteresesColones"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto6 = Convert.ToDecimal(dr_CobrosPagos["MontoInteresesColonesCierre"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto7 = Convert.ToDecimal(dr_CobrosPagos["ValorPresentePrincipal"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto8 = Convert.ToDecimal(dr_CobrosPagos["ValorPresentePrinColones"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto9 = Convert.ToDecimal(dr_CobrosPagos["ValorPresentePrinColonesCierre"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto10 = Convert.ToDecimal(dr_CobrosPagos["ValorPresenteIntereses"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto11 = Convert.ToDecimal(dr_CobrosPagos["ValorPresenteInteresColones"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto12 = Convert.ToDecimal(dr_CobrosPagos["ValorPresenteInteresColonesCierre"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto13 = Convert.ToDecimal(dr_CobrosPagos["Intereses"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto14 = Convert.ToDecimal(dr_CobrosPagos["InteresesColones"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto15 = Convert.ToDecimal(dr_CobrosPagos["InteresesColonesCierre"], CultureInfo.InvariantCulture);
                                            Decimal lstr_Monto16 = Convert.ToDecimal(dr_CobrosPagos["InteresesMoratorios"], CultureInfo.InvariantCulture);

                                            gstr_InModuloCT = "IdModulo In ('CT')";
                                            gstr_Transaccion = "Reversion";
                                            gstr_Leyenda = String.Empty;
                                            glbool_cambioMonto = false;
                                            gint_CantidadLineasAsiento = 16;

                                            garrdec_Montos = new Decimal[16];
                                            garrdec_Montos[0] = lstr_Monto1;
                                            garrdec_Montos[1] = lstr_Monto2;
                                            garrdec_Montos[2] = lstr_Monto3;
                                            garrdec_Montos[3] = lstr_Monto4;
                                            garrdec_Montos[4] = lstr_Monto5;
                                            garrdec_Montos[5] = lstr_Monto6;
                                            garrdec_Montos[6] = lstr_Monto7;
                                            garrdec_Montos[7] = lstr_Monto8;
                                            garrdec_Montos[8] = lstr_Monto9;
                                            garrdec_Montos[9] = lstr_Monto10;
                                            garrdec_Montos[10] = lstr_Monto11;
                                            garrdec_Montos[11] = lstr_Monto12;

                                            String lstr_Operacion = RetornaAsientos(lstr_IdOperacion);
                                            asiento.definirExpediente(gstr_IdExpediente, gstr_Sociedad, gstr_Usuario);
                                            lstr_Resultado = asiento.enviar(gstr_InModuloCT, lstr_Operacion, gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, lstr_Monto2, lstr_Monto4, out lstr_CodAsiento);
                                            //lstr_Resultado = EnviarAsientos(gstr_InModuloCT, lstr_Operacion, gstr_IdExpediente, gstr_Transaccion, gstr_Leyenda, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, 0, 0, null, 0, null);
                                        }
                                    }
                                    catch (Exception exp)
                                    { }

#endregion


                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT10", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                          
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        if (gbool_TieneRF)
                                            larrstr_ResultResolucion = ws_SG.uwsModificarResolucionDeta(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas, gdec_DAnnoMoral, gdec_InteresesMoratoriosColones,
                                            gdec_CostasColones, gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gstr_TipoTransaccion, gstr_EstadoTransaccion);
                                        else
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            lstr_Resultado = "exito";
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo1";
                                        }
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo";
                                    }
                                    #endregion
                                }
                                else if (gbool_TienePretensionInicial)
                                {
                                    #region Revelación-> Registro-> Revelación AC
                                    lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);


                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT10", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                          
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        //if (larrstr_ResultResolucion[0].Contains("00"))
                                        //{
                                            larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                                Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                                gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                                gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                                gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                            if (larrstr_ResultResolucion[0].Contains("00"))
                                            {
                                                lstr_Resultado = "exito";
                                                lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev, gbool_CambioMes);
                                                asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                                if (lstr_ResEnviarRev.Contains("exito"))
                                                {
                                                    lstr_Resultado = "exito";
                                                }
                                                else
                                                {
                                                    lstr_Resultado = "fallo2";
                                                }
                                            }
                                            else
                                            {
                                                lstr_Resultado = "fallo1";
                                            }
                                        //}
                                    }
                                    else
                                    {
                                        lstr_Resultado = "fallo2";
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region Registro-> Revelación AC


                                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, "CT10", gstr_Transaccion, false, garrdec_Montos, gint_CantidadLineasAsiento, gdec_MontoPrincipalColones, gdec_MontoInteresesColones, out lstr_CodAsiento);
                                    //ws_SG.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_EstadoResolucion, gstr_IdExpediente, gstr_Sociedad, lstr_CodAsiento, gstr_Usuario);
                                          
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        larrstr_ResultResolucion = ws_SG.uwsRegistrarResolucion(gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion, gstr_Estado,
                                            Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                            gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Usuario, gstr_Moneda, gstr_EstadoTransaccion, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal,
                                            gdec_MontoIntereses, gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones, gdec_MontoInteresesColones, gdec_MontoPrincipalColones,
                                            gdec_ValorPresenteInteresesColones, gdec_ValorPresentePrincipal, gdec_ValorPresenteIntereses, gdec_ValorPresentePrincipalColones, gdec_Costas, gdec_CostasColones,
                                            gdec_DAnnoMoral, gdec_DAnnoMoralColones, gstr_TipoTransaccion, gint_EstadoPretension, gstr_EstadoProcesal);

                                        if (larrstr_ResultResolucion[0].Contains("00"))
                                        {
                                            //lstr_ResEnviarRev = EnviarRevelacion(gstr_IdExpediente, 0, gdec_MontoPrincipalColones, gdec_MontoPrincipalColRev);
                                            asiento.modificarAsiento(lint_IdRes, gstr_IdResolucion, lstr_CodAsiento);
                                            //if (lstr_ResEnviarRev.Contains("exito"))
                                            //{
                                                lstr_Resultado = "exito";
                                            //}
                                            //else
                                            //{
                                            //    lstr_Resultado = "fallo3";
                                            //}
                                        }
                                        else
                                        {
                                            lstr_Resultado = "fallo";
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            break;

                        #endregion

                        default:
                            break;
                    }
                }

                if (lbool_Reinicio)
                {
                    LimpiarCampos();
                }

                #region Respuesta

                if (lstr_Resultado.Contains("exito"))
                {
                    MessageBox.Show("La creación de resolución fue satisfactoria."+
                        "\n" + asiento.getAsientosResultado());
                    this.LimpiarCampos();
                }
                else if (lstr_Resultado.Equals("fallo") || lstr_Resultado.Equals("error"))
                {
                    MessageBox.Show("Falló la creación de resolución. [Registro Asiento Sig@f]");
                }
                else if (lstr_Resultado.Equals("fallo1"))
                {
                    try
                    {
                        gstr_MensajeResultadoResoluciones = asiento.getMensajeResultadoResoluciones() + Environment.NewLine + larrstr_ResultResolucion[1];
                    }
                    catch (Exception exp) { }

                    MessageBox.Show("Falló la creación de resolución. [Registro Resolución]" +  go);
                }
                else if (lstr_Resultado.Equals("fallo2"))
                {
                    MessageBox.Show("Falló la creación de resolución. [Registro Revelación]");
                }
                else if (lstr_Resultado.Equals("fallo3"))
                {
                    MessageBox.Show(
                        "Falló el envio de revelación. \n" +
                        "Falló la creación de la resolución.");
                }
                else if (lstr_Resultado.Equals("texto1"))
                {
                    MessageBox.Show("No existe proceso de disminución de RF");
                }
                else
                {
                    MessageBox.Show(lstr_Resultado);
                }
                #endregion
            }
            catch (Exception err)
            {
                ws_SG.uwsRegistrarAccionBitacoraCo("CT", gstr_Usuario, "GuardarResolucion", "Error: " + err.ToString(),
                    gstr_IdExpediente, "", gstr_Sociedad);

                MessageBox.Show("La creación de la resolución no fue satisfactoria.");
            }
        }

        private void TieneResolucion(string str_IdExp="") 
        {
            String lstr_Mensaje = string.Empty;
            String lstr_Codigo = String.Empty;

            str_IdExp = (string.IsNullOrEmpty(str_IdExp)) ? gstr_IdExpediente : str_IdExp;

            lds_ConsultarResolucion = ws_SG.uwsConsultarResolucion("", str_IdExp, gstr_Sociedad, out lstr_Codigo, out lstr_Mensaje);

            gbool_TieneRP1 = false;
            gbool_TieneRP2 = false;
            gbool_TieneRF = false;

            foreach (DataRow ldr_ConsultarResolucion in lds_ConsultarResolucion.Tables["Table"].Rows)
            {
                if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Contains("En Firme"))
                {
                    gbool_TieneRF = true;
                }
                if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Contains("Provisional 2"))
                {
                    gbool_TieneRP2 = true;
                    if (gbool_TieneRF)
                    {
                        break;
                    }
                }
                if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Contains("Provisional 1"))
                {
                    gbool_TieneRP1 = true;
                    if (gbool_TieneRP2 || gbool_TieneRF)
                    {
                        break;
                    }
                }
                if (gbool_TieneRP2 || gbool_TieneRF || gbool_TieneRP1)
                {
                    if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["MontoPrincipalColones"].ToString()))
                    gdec_MontoPrincipalColRev = Convert.ToDecimal(ldr_ConsultarResolucion["MontoPrincipalColones"]);
                    if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["MontoInteresesColones"].ToString()))
                    gdec_MontoInteresesColRev = Convert.ToDecimal(ldr_ConsultarResolucion["MontoInteresesColones"]);
                    gint_Periodo = Convert.ToDateTime(ldr_ConsultarResolucion["FchCreacion"].ToString()).Year;

                    if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["FchModifica"].ToString()))
                        lint_Mes = Convert.ToDateTime(ldr_ConsultarResolucion["FchModifica"].ToString()).Month;


                    if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["TipoCambio1"].ToString()))
                    gdec_TipoCambioAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["TipoCambio1"]);
                    else
                        gdec_TipoCambioAnterior = gdec_TipoCambio;
                    if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["Tbp"].ToString()))
                    gdec_TbpAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["Tbp"]);
                    if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["Tiempo"].ToString()))
                    gdec_TiempoAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["Tiempo"]);
                    ldec_TipoCambioCierre = ldr_ConsultarResolucion["TipoCambioCierre"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["TipoCambioCierre"]);

                    gstr_MonedaAnterior = ldr_ConsultarResolucion["Moneda"].ToString();
                    if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["MontoPrincipal"].ToString()))
                    gdec_MontoPrincipalAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["MontoPrincipal"]);
                    if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["MontoIntereses"].ToString()))
                    gdec_MontoInteresesAnterior = Convert.ToDecimal(ldr_ConsultarResolucion["MontoIntereses"]);

                    gstr_ObservacionesAnteriores = ldr_ConsultarResolucion["Observacion"].ToString();

                    if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["MontoPrincipalAnterior"].ToString()))
                    ldec_PrincipalPrevision = ldr_ConsultarResolucion["MontoPrincipalAnterior"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["MontoPrincipalAnterior"]);

                    if (!string.IsNullOrEmpty(ldr_ConsultarResolucion["MontoInteresesAnterior"].ToString()))
                    ldec_InteresesPrevision = ldr_ConsultarResolucion["MontoInteresesAnterior"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["MontoInteresesAnterior"]);
                    lint_IdRes = ldr_ConsultarResolucion["IdRes"].ToString() == "" ? 0 : Convert.ToInt32(ldr_ConsultarResolucion["IdRes"].ToString());

                    if ((ldec_PrincipalPrevision != 0) || (ldec_InteresesPrevision != 0))
                        lbool_Prevision = true;

                    ldec_PrincipalCierre = ldr_ConsultarResolucion["MontoPrincipalColonesCierre"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["MontoPrincipalColonesCierre"]);
                    ldec_InteresesCierre = ldr_ConsultarResolucion["MontoInteresesColonesCierre"].ToString() == "" ? 0 : Convert.ToDecimal(ldr_ConsultarResolucion["MontoInteresesColonesCierre"]);
                }
            }
        
        }

        private string EnviarRevelacion(String str_NumExpediente, int int_proceso,
            Decimal dec_Monto, Decimal dec_MontoReversar, bool bool_CambioMes)
        {
            #region Variables
            string lstr_Respuesta = string.Empty;
            string lstr_ErrorConsolidado = string.Empty;
            String lstr_Montos = String.Empty;

            string lstr_MontoTotalColones = string.Empty;
            string lstr_ValorPresente = string.Empty;
            string lstr_TipoProceso = string.Empty;

            string lstr_IdTipoProceso = string.Empty;
            string lstr_Ministerio = string.Empty;

            string[] larrstr_RevelacionResultado = new string[2];

            Decimal ldec_MontoAjuste = 0;
            string lstr_MontoAjuste = string.Empty;
            string lstr_Monto = string.Empty;
            string lstr_MontoReversar= string.Empty;
            #endregion

            try
            {
                #region PreEnvio
                string lstr_TipoExpediente = ConsultarTipoExpediente(str_NumExpediente);

                #region existe Pretención Inicial??
                DataSet lds_ConsultarExpediente = new DataSet();
                DataRow ldr_ConsultarExpediente = null;

                lds_ConsultarExpediente = ws_SG.uwsConsultarExpedienteXNumero(gstr_IdExpediente, gstr_Sociedad);

                if ((lds_ConsultarExpediente.Tables["Table"] != null) && (lds_ConsultarExpediente.Tables.Count > 0))
                {
                    ldr_ConsultarExpediente = lds_ConsultarExpediente.Tables["Table"].Rows[0];
                    //if (!String.IsNullOrEmpty(ldr_ConsultarExpediente["MontoPretensionColones"].ToString()))
                    //{
                        lstr_TipoProceso = ldr_ConsultarExpediente["TipoProcesoExpediente"].ToString();
                        lstr_ValorPresente = ldr_ConsultarExpediente["MontoValorPresente"].ToString();
                        lstr_MontoTotalColones = ldr_ConsultarExpediente["MontoPretensionColones"].ToString();

                        if (String.IsNullOrEmpty(lstr_MontoTotalColones))
                            lstr_MontoTotalColones = "0.00";
                        //}
                }
                #endregion

                // Se Obtiene el  Id de TipoProceso que tiene el Expediente
                string lstr_ConsultaTP = "SELECT IdCatalogo,oc.IdOpcion,oc.NomOpcion FROM ma.OpcionesCatalogos oc WHERE oc.IdCatalogo='30' AND Estado = 'A' AND oc.NomOpcion='" + lstr_TipoProceso + "'";
                DataTable ldt_TipoProceso = GetData(lstr_ConsultaTP);//Sacamos el tipo de proceso
                DataRow ldr_TipoProceso = (ldt_TipoProceso.Rows.Count > 0) ? ldt_TipoProceso.Rows[0] : ldt_TipoProceso.NewRow();//Asignamos y validamos dataTables
                lstr_IdTipoProceso = ldr_TipoProceso["IdOpcion"].ToString();
                lstr_Ministerio = (clsSesion.Current.SociedadUsr == null) ? "Ministerio desconocido." : clsSesion.Current.SociedadUsr;

                // Se verifica la existencia de periodo en Revelación y Notas
                DateTime ldt_FechaActual = DateTime.Now;
                DateTime ldt_FchRevelacion = new DateTime();

                if(gbool_CambioMes)
                {

                }

                DataSet lds_RevelacionInfo = ws_SG.uwsConsultarRevelacionContingente("", Convert.ToString(ldt_FechaActual.Year), Convert.ToString(ldt_FechaActual.Month));
                DataRow ldr_RevelacionInfo = null;

                if (lds_RevelacionInfo.Tables.Count > 0)
                {
                    ldr_RevelacionInfo = lds_RevelacionInfo.Tables[0].Rows[0];
                    ldt_FchRevelacion = Convert.ToDateTime(ldr_RevelacionInfo["FchModifica"].ToString());
                }
                else
                {
                    lstr_ErrorConsolidado = "No se encontró Periódo de Revelación en Nota. ";
                }

                //Insertamos en BD Revelacion
                string lstr_FchModificaRevelacion = ldt_FchRevelacion.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                #endregion

                #region Procesos
                if ((int_proceso == 0) || (int_proceso == 9)) //nuevo suma monto cantidad
                {
                    lstr_MontoAjuste = ((double)dec_Monto).ToString();

                    if (lstr_TipoExpediente.Contains("Actor")
                        || (lstr_TipoExpediente.Contains("Demandado") && (int_proceso == 9)))
                    {
                        larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 1); //suma monto cant
                    }
                    else if (lstr_TipoExpediente.Contains("Demandado")
                    || (lstr_TipoExpediente.Contains("Actor") && (int_proceso == 9)))
                    {
                        larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 1);
                    }
                    lstr_Montos = lstr_Montos + "Suma Monto: " + lstr_MontoAjuste;
                }
                else if (int_proceso == 1) // Ajustar monto
                {
                    if (dec_MontoReversar > dec_Monto)
                    {
                        ldec_MontoAjuste = dec_MontoReversar - dec_Monto;
                        lstr_MontoAjuste = ((double)ldec_MontoAjuste).ToString();

                        if (!bool_CambioMes)
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 5); //resta monto
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 5);
                        }
                        else
                        {
                            lstr_MontoAjuste = ((double)dec_Monto).ToString();

                            if (lstr_TipoExpediente.Contains("Actor"))
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 1); //suma monto cant
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 1);
                       
                        }
                        lstr_Montos = lstr_Montos + "Ajustar Monto: " + lstr_MontoAjuste;
                    }
                    else if (dec_MontoReversar < dec_Monto)
                    {
                        ldec_MontoAjuste = dec_Monto - dec_MontoReversar;
                        lstr_MontoAjuste = ((double)ldec_MontoAjuste).ToString();
                        if (!bool_CambioMes)
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 4); //suma monto
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 4);
                        }
                        else
                        {
                            lstr_MontoAjuste = ((double)dec_Monto).ToString();

                            if (lstr_TipoExpediente.Contains("Actor"))
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 1); //suma monto cant
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 1);

                        }
                        lstr_Montos = lstr_Montos + "Sumar Monto: " + lstr_MontoAjuste;
                    }
                    else
                    {
                        larrstr_RevelacionResultado[0] = "00";
                    }
                }
                if (int_proceso == 2) // Intercambio de monto
                {
                    lstr_Monto = ((double)dec_Monto).ToString();
                    lstr_MontoReversar = ((double)dec_MontoReversar).ToString();
                    if (!bool_CambioMes)
                    {
                        if (lstr_TipoExpediente.Contains("Actor"))
                        {
                            larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_Monto, "1", lstr_MontoReversar, ldt_FchRevelacion, 2); // resta, suma : monto, cant
                        }
                        else if (lstr_TipoExpediente.Contains("Demandado"))
                        {
                            larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_Monto, "1", lstr_MontoReversar, ldt_FchRevelacion, 2); // resta, suma : monto, cant
                        }
                    }
                    else
                    {
                        lstr_MontoAjuste = ((double)dec_Monto).ToString();

                        if (lstr_TipoExpediente.Contains("Actor"))
                            larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 1); //suma monto cant
                        else if (lstr_TipoExpediente.Contains("Demandado"))
                            larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 1);

                    }
                }
                if ((int_proceso == 3) || (int_proceso == 6)) // Resta monto y cant, PI
                {
                    lstr_MontoTotalColones = String.Empty;
                    lstr_MontoTotalColones = ((double)gdec_MontoPrincipalColRev).ToString();
                    if (!bool_CambioMes)
                    {
                        if ( lstr_TipoExpediente.Contains("Actor")
                            || ( (lstr_TipoExpediente.Contains("Demandado")) && (int_proceso == 6)))
                        {
                            larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoTotalColones, "1", "0.00", ldt_FchRevelacion, 3);
                        }
                        else if (lstr_TipoExpediente.Contains("Demandado")
                            || ((lstr_TipoExpediente.Contains("Actor")) && (int_proceso == 6)))
                        {
                            larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoTotalColones, "1", "0.00", ldt_FchRevelacion, 3);
                        }
                    }
                    else
                    {
                        lstr_MontoAjuste = ((double)dec_Monto).ToString();

                        //if (lstr_TipoExpediente.Contains("Actor"))
                        //    larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", lstr_FchModificaRevelacion, 1); //suma monto cant
                        //else if (lstr_TipoExpediente.Contains("Demandado"))
                        //    larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", lstr_FchModificaRevelacion, 1);
                    }
                    lstr_Montos = lstr_Montos + "Restar Monto: " + lstr_MontoTotalColones;
                }
                if (int_proceso == 4) // Resta monto y cant, ajuste RP1
                {
                    lstr_MontoTotalColones = String.Empty;
                    if (!bool_CambioMes)
                    {
                        if (gdec_MontoPrincipalColones > gdec_MontoPrincipalColRev)
                        {
                            Decimal ldec_AjusteMonto = gdec_MontoPrincipalColones - gdec_MontoPrincipalColRev;
                            lstr_MontoTotalColones = ((double)ldec_AjusteMonto).ToString();

                            if (lstr_TipoExpediente.Contains("Actor"))
                            {
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoTotalColones, "1", "0.00", ldt_FchRevelacion, 4);
                            }
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                            {
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoTotalColones, "1", "0.00", ldt_FchRevelacion, 4);
                            }
                            lstr_Montos = lstr_Montos + "Ajustar Monto: " + lstr_MontoTotalColones;
                        }
                    }
                    else
                    {
                        if (gdec_MontoPrincipalColones > gdec_MontoPrincipalColRev)
                        {
                            Decimal ldec_AjusteMonto = gdec_MontoPrincipalColones - gdec_MontoPrincipalColRev;
                            lstr_MontoTotalColones = ((double)ldec_AjusteMonto).ToString();

                            if (lstr_TipoExpediente.Contains("Actor"))
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoTotalColones, "1", "0.00", ldt_FchRevelacion, 1); //suma monto cant
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoTotalColones, "1", "0.00", ldt_FchRevelacion, 1);
                        }
                    }
                }
                else if (int_proceso == 5)
                {
                    if (!bool_CambioMes)
                    {
                        if (lstr_TipoExpediente.Contains("Actor"))
                        {
                            larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_IdTipoProceso, "1", "0.00", ldt_FchRevelacion, 5);

                        }
                        else if (lstr_TipoExpediente.Contains("Demandado"))
                        {
                            larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_IdTipoProceso, "1", "0.00", ldt_FchRevelacion, 5);
                        }
                    }
                    else
                    {
                        lstr_MontoAjuste = ((double)dec_Monto).ToString();

                        if (lstr_TipoExpediente.Contains("Actor"))
                            larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, "0.00", "1", "0.00", ldt_FchRevelacion, 1); //suma monto cant
                        else if (lstr_TipoExpediente.Contains("Demandado"))
                            larrstr_RevelacionResultado = ws_SG.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, "0.00", "1", "0.00", ldt_FchRevelacion, 1);

                    }
                }
                else
                {
                }
                #endregion

                //Validamos resultado para desplegar mensaje al usuario
                if (!String.IsNullOrEmpty(larrstr_RevelacionResultado[0]))
                {
                    //Crea la revelacion en  nota del expediente despues de crear la pretension
                    if (larrstr_RevelacionResultado[0].Contains("00"))
                    {
                        ws_SG.uwsRegistrarAccionBitacoraCo("CT", gstr_Usuario, "Envio Revelación.", str_NumExpediente + ":" + gstr_Sociedad + "\n" +
                            //"Monto Actual: " + dec_Monto + " Monto Anterior: " + dec_MontoReversar + 
                            lstr_Montos + "\n" + 
                            "Respuesta: Satisfactorio.",
                            gstr_IdExpediente, "", gstr_Sociedad);
                        //lstr_Respuesta = "Se generó, la revelación en nota del expediente satisfactoriamente.";
                        lstr_Respuesta = "exito";
                    }
                    else if (larrstr_RevelacionResultado[0].Contains("99"))
                    {
                         //lstr_Respuesta = "No se generó, la revelación en nota del expediente. Falló la comunicación con el módulo de Revelación en Nota.";
                        lstr_Respuesta = "fallo";
                        ws_SG.uwsRegistrarAccionBitacoraCo("CT", gstr_Usuario, "Envio Revelación.", str_NumExpediente + ":" + gstr_Sociedad + "\n" +
                            "Monto Actual: " + dec_Monto + " Monto Anterior: " + dec_MontoReversar + "\nError: " + lstr_ErrorConsolidado + " | " + larrstr_RevelacionResultado[0] + ": " + larrstr_RevelacionResultado[1],
                            gstr_IdExpediente, "", gstr_Sociedad);
                    }
                }
                else
                {
                    lstr_Respuesta = "fallo";

                    ws_SG.uwsRegistrarAccionBitacoraCo("CT", gstr_Usuario, "Envio Revelación.", str_NumExpediente + ":" + gstr_Sociedad + "\n" +
                            "Monto Actual: " + dec_Monto + " Monto Anterior: " + dec_MontoReversar + "\nError: " + lstr_ErrorConsolidado + " | " + larrstr_RevelacionResultado[0] + " " + larrstr_RevelacionResultado[1],
                            gstr_IdExpediente, "", gstr_Sociedad);
                    
                }
            }
            catch (Exception err)
            {
                lstr_Respuesta = "fallo";

                ws_SG.uwsRegistrarAccionBitacoraCo("CT", gstr_Usuario, "Envio Revelación.", str_NumExpediente + ":" + gstr_Sociedad + "\n" +
                            "Monto Actual: " + dec_Monto + " Monto Anterior: " + dec_MontoReversar + "\nError: " + larrstr_RevelacionResultado + " : " + err.Message,
                            gstr_IdExpediente, "", gstr_Sociedad);
                
            }

            return lstr_Respuesta;
        }

        private String[] ModificarMontoExpedientes(Decimal pstr_MontoPretension, Decimal pstr_MontoPretensionColones)
        {
            String[] larrstr_ResultResolucion = new String[2];
            DataSet lds_Expediente = new DataSet();
            DataRow ldr_Expediente = null;

            try
            {
                lds_Expediente = ws_SG.uwsConsultarExpedienteXNumero(gstr_IdExpediente, gstr_Sociedad);

                if ((lds_Expediente.Tables["Table"] != null) && (lds_Expediente.Tables.Count > 0))
                {
                    ldr_Expediente = lds_Expediente.Tables["Table"].Rows[0];

                    String lstr_IdExpediente = ldr_Expediente["IdExpediente"].ToString();
                    String str_MonedaPretension = ldr_Expediente["MonedaPretension"].ToString();
                    Decimal ldec_TipoCambio = Convert.ToDecimal(ldr_Expediente["TipoCambio"]);
                    Decimal ldec_MontoPretension = pstr_MontoPretension; //Convert.ToDecimal(ldr_Expediente["MontoPretension"]);
                    Decimal ldec_MontoPretColones = pstr_MontoPretensionColones; //Convert.ToDecimal(ldr_Expediente["MontoPretensionColones"]);
                    Int32 lint_EstadoPretension = Convert.ToInt32(ldr_Expediente["EstadoPretension"]);
                    DateTime ldt_PosibleFecEntRec = Convert.ToDateTime(ldr_Expediente["PosibleFechEntradaRecursos"]);
                    Decimal ldec_ValorPresente = Convert.ToDecimal(ldr_Expediente["MontoValorPresente"]);
                    String lstr_ObservacionesPretension = ldr_Expediente["ObservacionesPretension"].ToString();
                    String lstr_TipoProceso = ldr_Expediente["TipoProcesoExpediente"].ToString();

                    String lstr_Sociedad = gstr_Sociedad;
                    Decimal ldec_MontoPosibleReembolso = Convert.ToDecimal("0.00");
                    String lstr_UsrModifica = gstr_Usuario;

                    larrstr_ResultResolucion = ws_SG.uwsRegistrarPretensionInicial(lstr_IdExpediente, lstr_Sociedad, lstr_TipoProceso, str_MonedaPretension,
                        ldec_TipoCambio, ldec_MontoPretension, ldec_MontoPretColones, ldec_MontoPosibleReembolso,
                        lint_EstadoPretension, ldt_PosibleFecEntRec, ldec_ValorPresente,
                        lstr_ObservacionesPretension, lstr_UsrModifica);

                }
            }
            catch (Exception exc) 
            {
                larrstr_ResultResolucion[0] = "99";
                larrstr_ResultResolucion[1] = "Error al modificar montos.";
                ws_SG.uwsRegistrarAccionBitacoraCo("CT", gstr_Usuario, "ModificarMontosResolucion", "Exp:" + gstr_IdExpediente + " Error: " + exc.ToString(),
                    gstr_IdExpediente, "", gstr_Sociedad);
               
            }
            return larrstr_ResultResolucion;
        }

        private bool verificarExisteResolucionExpediente(string idexp, string tipoResolucion)
        {
            bool result;
            string consulta = "Select * from co.Expedientes as e inner join co.Resoluciones as r on e.IdExpediente=r.IdExpedienteFK where e.EstadoExpediente='Activo' and IdSociedadGL='" + clsSesion.Current.SociedadUsr + "' and e.IdExpediente='" + idexp + "' and r.EstadoResolucion='" + tipoResolucion + "'";

            DataTable dt = GetData(consulta);
            if (dt.Rows.Count > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        protected String DeclararSinLugar(string idExpediente, string tipoExpediente, string estadoResolucion, int CxCxCxP, string Sociedad, string userCrea)
        {
            string lstr_respuesta = "Sin lugar";
            string[] lstr_respuestas = new string[2];


            lstr_respuestas = ws_SG.uwsDeclararSinLugarResolucion(idExpediente, tipoExpediente, estadoResolucion, CxCxCxP, Sociedad, userCrea);
            //Validamos resultado para desplegar mensaje al usuario
            if (lstr_respuestas[0].Contains("00"))
            {
                lstr_respuesta = "exito";
                MessageBox.Show("Se declaró sin lugar la resolución con número de expediente " + idExpediente);

            }
            else if (lstr_respuestas[0].Contains("99") || lstr_respuestas[0].Contains("Codigo :-") || lstr_respuestas[0].Contains("Codigo:") || lstr_respuestas[0].Contains("Codigo :") || lstr_respuestas[0].Contains("Codigo :-6"))
            {
                lstr_respuesta = "fallo";
                MessageBox.Show("No se pudo declarar sin lugar la resolución, con numero de expediente " + idExpediente);
            
            }
            return lstr_respuesta;
        }
       
        private string ConsultarTipoExpediente(string idexpediente)
        {
            string str_consul = "Select TipoExpediente from co.Expedientes where IdExpediente='" + idexpediente + "' and IdSociedadGL='" + clsSesion.Current.SociedadUsr + "' and EstadoExpediente='Activo'";
            string tipoExp = string.Empty;
            //Consultar Expedientes
            DataTable exped = GetData(str_consul);
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                tipoExp=campo["TipoExpediente"].ToString();
            }

            return tipoExp;
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
            ds = ws_SG.uwsConsultarDinamico(lstr_query);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables["Table"];
            }
            return null;
        }

        private string[] ModificarResolucionData(int idRes,int idCobroP)
        {
            
            string[] resultModificar=new string[2];
            string fechResolucion = this.txtFechaResolucion.Text;
            // string fechSalidaRecur = this.calFechSalidaRecur.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string fechSalidaRecur = this.txtFechSalidaRecur.Text;
            
            /*string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
            //string fechResolucion = this.calFechaResolucion.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            
            using (SqlConnection con = new SqlConnection(lstr_ConnString))
            {
                con.Open();  
                using (SqlCommand StoredProcedureCommand = new SqlCommand("co.uspModificarResolucion", con))
                {
                    StoredProcedureCommand.CommandType = CommandType.StoredProcedure;
                    StoredProcedureCommand.Parameters.AddWithValue("@pIdRes", idRes);
                    StoredProcedureCommand.Parameters.AddWithValue("@pIdCobroPagoResolucion", idCobroP);
                    StoredProcedureCommand.Parameters.AddWithValue("@pIdResolucion", this.txtResolucionNum.Text);
                    StoredProcedureCommand.Parameters.AddWithValue("@pIdExpediente", DDLExpedientes.SelectedValue);
                    StoredProcedureCommand.Parameters.AddWithValue("@pEstadoResolucion", this.ddlEstadoResol.SelectedItem.Value);
                    StoredProcedureCommand.Parameters.AddWithValue("@pFechaResolucion", Convert.ToDateTime(fechResolucion));
                    StoredProcedureCommand.Parameters.AddWithValue("@pPosibleFecSalidaRec", Convert.ToDateTime(fechSalidaRecur));
                    StoredProcedureCommand.Parameters.AddWithValue("@pMontoPosibleReembolso", Convert.ToDecimal(this.txtMontoPosDesembolso.Text));
                    StoredProcedureCommand.Parameters.AddWithValue("@pMontoPosReemColones", Convert.ToDecimal(this.txtMontoPosDesembolso.Text));
                    StoredProcedureCommand.Parameters.AddWithValue("@pObservacion", this.CKEditorObservaciones.Text);
                    StoredProcedureCommand.Parameters.AddWithValue("@pCxCaCxP", Convert.ToInt32(this.chkCxPCaC.Checked));
                    StoredProcedureCommand.Parameters.AddWithValue("@pMoneda", this.DDLMoneda.SelectedItem.Value);
                    StoredProcedureCommand.Parameters.AddWithValue("@pTipoCambio", gdec_TipoCambio);
                    StoredProcedureCommand.Parameters.AddWithValue("@pMontoPrincipal", Convert.ToDecimal(this.txtMontoPrincipal.Text));
                    StoredProcedureCommand.Parameters.AddWithValue("@pMontoIntereses", Convert.ToDecimal(this.txtMontoIntereses.Text));
                    StoredProcedureCommand.Parameters.AddWithValue("@pValorPresentePrincipal", decimal.Parse(this.txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number)); 
                    StoredProcedureCommand.Parameters.AddWithValue("@pValorPresenteIntereses", decimal.Parse(this.txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
                    StoredProcedureCommand.Parameters.AddWithValue("@pMontoPrincipalColones", decimal.Parse(this.txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
                    StoredProcedureCommand.Parameters.AddWithValue("@pMontoInteresesColones", decimal.Parse(this.txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
                    StoredProcedureCommand.Parameters.AddWithValue("@pUsrModifica", (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
                    StoredProcedureCommand.Parameters.AddWithValue("@pEstadoProcesal", this.DDLEstadoProcesal.SelectedItem.Value);
                    StoredProcedureCommand.Parameters.AddWithValue("@pEstadoPretension", gint_EstadoPretension);

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
                    if (Codigo.Contains("00"))
                        MessageBox.Show("Resolución modificada satisfactoriamente.");
                    else
                        MessageBox.Show("La Resolución no fue modificada." + resultModificar[1]);
                    
                    con.Close();
                }
            }*/
           

            resultModificar = ws_SG.uwsModificarResolucion(idRes, idCobroP, this.txtResolucionNum.Text, DDLExpedientes.SelectedValue, gstr_Sociedad, this.ddlEstadoResol.SelectedItem.Value, "", Convert.ToDateTime(fechResolucion),
                FechaValida(fechSalidaRecur), Convert.ToDecimal(this.txtMontoPosDesembolso.Text), Convert.ToDecimal(this.txtMontoPosDesembolso.Text), "",
                Convert.ToInt32(this.chkCxPCaC.Checked), this.DDLMoneda.SelectedItem.Value, gdec_TipoCambio, 0, 0, Convert.ToDecimal(this.txtMontoPrincipal.Text),
                Convert.ToDecimal(this.txtMontoIntereses.Text), decimal.Parse(this.txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number),
                decimal.Parse(this.txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number),
                decimal.Parse(this.txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number),
                decimal.Parse(this.txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number),
                this.DDLEstadoProcesal.SelectedItem.Value, gint_EstadoPretension, (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);


            return resultModificar;    
        }
        
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            String[] lstr_ResultadoModificarReserva = new String[2];

            gint_IdRes = Convert.ToInt32(ViewState["idRes"]);
            int idCobroP = Convert.ToInt32(ViewState["idCobroP"]);
            ModificarResolucionData(gint_IdRes, idCobroP);

            string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
            string[] resultModificar = new string[2];
           // string fechResolucion = this.calFechaResolucion.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string fechResolucion = this.txtFechaResolucion.Text;
//            string fechSalidaRecur = this.calFechSalidaRecur.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string fechSalidaRecur = this.txtFechSalidaRecur.Text;

            //lstr_ResultadoModificarReserva = ws_SG.uwsModificarResolucionDeta(gint_IdRes, idCobroP, gstr_IdResolucion, gstr_IdExpediente, gstr_EstadoResolucion,
            //    Convert.ToDateTime(str_fechaResolucion), Convert.ToDateTime(str_PosibleFecSalidaRec), MontoPosibleReembolso,
            //    MontoPosReemColones, Observacion, CxCaCxP, Moneda, TipoCambio, MontoPrincipal, MontoIntereses,
            //    gdec_ValorPresenteInteresesColones, ValorPresentePrincipal, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
            //    EstadoProcesal, gstr_Usuario, gdec_InteresesMoratorios, gdec_Costas,gdec_DAnnoMoral,gdec_InteresesMoratoriosColones,
                                            //gdec_CostasColones,gdec_DAnnoMoralColones, gdec_ValorPresentePrincipalColones,gdec_ValorPresenteInteresesColones,gstr_TipoTransaccion,gstr_EstadoTransaccion);

            //StoredProcedureCommand.Parameters.AddWithValue("@pIdResolucion", this.txtResolucionNum.Text);
            ////StoredProcedureCommand.Parameters.AddWithValue("@pIdExpedienteFK", "");
            //StoredProcedureCommand.Parameters.AddWithValue("@pEstadoResolucion", this.ddlEstadoResol.SelectedItem.Value);
            //StoredProcedureCommand.Parameters.AddWithValue("@pFechaResolucion", Convert.ToDateTime(fechResolucion));
            //StoredProcedureCommand.Parameters.AddWithValue("@pPosibleFecSalidaRec", Convert.ToDateTime(fechSalidaRecur));
            //StoredProcedureCommand.Parameters.AddWithValue("@pMontoPosibleReembolso", Convert.ToDecimal(this.txtMontoPosDesembolso.Text));
            //StoredProcedureCommand.Parameters.AddWithValue("@pMontoPosReemColones", Convert.ToDecimal(this.txtMontoPosDesembolso.Text));
            //StoredProcedureCommand.Parameters.AddWithValue("@pObservacion", this.CKEditorObservaciones.Text);
            //StoredProcedureCommand.Parameters.AddWithValue("@pCxCaCxP", Convert.ToInt32(this.chkCxPCaC.Checked));
            //StoredProcedureCommand.Parameters.AddWithValue("@pMoneda", this.DDLMoneda.SelectedItem.Value);
            //StoredProcedureCommand.Parameters.AddWithValue("@pTipoCambio", TipoCambio);
            //StoredProcedureCommand.Parameters.AddWithValue("@pMontoPrincipal", Convert.ToDecimal(this.txtMontoPrincipal.Text));
            //StoredProcedureCommand.Parameters.AddWithValue("@pMontoIntereses", Convert.ToDecimal(this.txtMontoIntereses.Text));
            //StoredProcedureCommand.Parameters.AddWithValue("@pValorPresentePrincipal", decimal.Parse(this.txtValorPresentePricipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
            //StoredProcedureCommand.Parameters.AddWithValue("@pValorPresenteIntereses", decimal.Parse(this.txtValorPresenteIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
            //StoredProcedureCommand.Parameters.AddWithValue("@pMontoPrincipalColones", decimal.Parse(this.txtMontoColonesPrincipal.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
            //StoredProcedureCommand.Parameters.AddWithValue("@pMontoInteresesColones", decimal.Parse(this.txtMontoColonesIntereses.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number));
            //StoredProcedureCommand.Parameters.AddWithValue("@pUsrModifica", (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
            //StoredProcedureCommand.Parameters.AddWithValue("@pEstadoProcesal", this.DDLEstadoProcesal.SelectedItem.Value);
                        

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DDLExpedientes.Enabled = false;
            //ObtenerResolucionAModificar();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.DDLExpedientes.Enabled = false;
            //this.ddlExpResolModif.Enabled = true;
        }

        protected void btnUpload_Click1(object sender, EventArgs e)
        {
            Request.ContentType = "multipart/form-data";
            HttpFileCollection files = HttpContext.Current.Request.Files;

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

                    if (this.txtResolucionNum.Text == "")
                    {
                        MessageBox.Show("Requerido adjuntar una resolución antes de subir el archivo a una resolución.");
                    }
                    else
                    {
                        //ws_SG.uwsGuardarArchivo(lstr_nombre, tipoContenido, lint_tamano, archivoDato, 0, this.DDLExpedientes.SelectedValue, 0, "",0,"",0, (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
                        ws_SG.uwsGuardarArchivoContingente(lstr_nombre, tipoContenido, lint_tamano, archivoDato, 0, this.ddlEstadoResol.SelectedValue, gstr_Sociedad, ConsultarIdExpediente(gstr_IdExpediente), gstr_IdExpediente, 0, "", 0, (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
                        MessageBox.Show("Se adjunto un nuevo archivo a la resolución" + gstr_IdExpediente + ".");
                    }
                    
                }
            }

            DataSet fileList = ws_SG.uwsObtenerArchivoPorIdResolucion(gstr_IdExpediente, gstr_Sociedad,ConsultarIdExpediente(gstr_IdExpediente));
            if (fileList.Tables.Count > 0)
            {
             gvFiles.DataSource = fileList;
             gvFiles.DataBind();
            }
            else
            {
                //Log.Error("Consulta no arrojo resultados, la tabla viene vacia al realizar la consulta para uwsObtenerArchivoPorIdResolucion.");
            }
           
        }

        private void verificarMontoDesembolso(string idExpediente)
        {
            this.txtMontoPosDesembolso.Enabled = true;
        }
        
        protected void chkCxPCaC_CheckedChanged(object sender, EventArgs e)
        {
            //string str_numExpediente=string.Empty;
            //string consult1=string.Empty;
            //string conslt2=string.Empty;
            //DataTable dt=new DataTable();//Consulta
            //DataRow campo;//fila obtenida

            //if (this.ddlExpResolModif.Enabled=true)
            //{

            //  str_numExpediente = this.ddlExpResolModif.SelectedValue;
            //  consult1 = "Select TipoExpediente,r.IdResolucion, r.IdExpedienteFK as IdExpediente from co.Expedientes AS e INNER JOIN co.Resoluciones AS r ON e.IdExpediente=r.IdExpedienteFK where r.IdResolucion='" + str_numExpediente + "'";

            //}
            //else if (this.DDLExpedientes.Enabled = true)
            //{
            //    str_numExpediente = this.DDLExpedientes.SelectedValue;
            //    consult1 = "Select TipoExpediente,IdExpediente from co.Expedientes where co.Expedientes.IdExpediente='" + str_numExpediente + "'";

            //}
            
            
            //if (this.DDLExpedientes.SelectedValue=="0")
            //{
            //    //MessageBox.Show("Debe elegir un expediente en el combo deplegable.");
            //    this.chkCxPCaC.Checked = false;
            //}
            //else
            //{

            //    str_numExpediente = this.DDLExpedientes.SelectedValue;
            //}
            //string consl = "Select TipoExpediente,IdExpediente from co.Expedientes where co.Expedientes.IdExpediente='" + str_numExpediente + "' and IdSociedadGL='"+clsSesion.Current.SociedadUsr+"'";
            //string estadoRes=string.Empty;
            //dt = GetData(consl);//Consulta
            
            //if(dt.Rows.Count>0){

            //    campo = dt.Rows[0];//fila obtenida
                
            //    if (this.ddlEstadoResol.SelectedItem.Value!="0")
            //    {

            //        estadoRes=this.ddlEstadoResol.SelectedItem.Value;
            //        //verificarResoluciones(estadoRes, campo["TipoExpediente"].ToString());
            //        if (!verificarExisteResolucionExpediente(str_numExpediente, estadoRes))
            //       {
            //            if (campo["TipoExpediente"].ToString().Contains("Actor"))
            //            {
            //                gstr_IdExpediente = campo["IdExpediente"].ToString();
            //               // DeclararSinLugar(idExpediente, "Demandado", "Activo", 0, clsSesion.Current.SociedadUsr,(clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);//se reversa a cuenta por pagar

            //            }
            //            else if (campo["TipoExpediente"].ToString().Contains("Demandado"))
            //            {
            //                gstr_IdExpediente = campo["IdExpediente"].ToString();
            //                //DeclararSinLugar(idExpediente, "Actor", "Activo", 1,clsSesion.Current.SociedadUsr, (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);//Se reversa a cuenta por cobrar
            //      }
            //        }
            //    }
            //    else
            //    {
            //        //MessageBox.Show("Debe de seleccionar en el desplegable, un tipo de provisión para asignar.");
            //    }
            //}
        }

        protected void ckbIncobrable_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void gvFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string[] lstr_ResEliminacion = new string[2];
   
            if (gvFiles.Rows.Count > 0)
            {
                string str_idArchivo = e.Keys["IdArchivo"].ToString();
                string str_idResolucion = this.DDLExpedientes.SelectedValue;
                int int_indice = Convert.ToInt32(e.RowIndex);
                Label lblFchModificacion = (Label)gvFiles.Rows[int_indice].Cells[4].FindControl("lblFchModifica"); 
                LinkButton lblIdArchivo = (LinkButton)gvFiles.Rows[int_indice].Cells[0].FindControl("lnkEliminar");

                string lstr_fecha = String.Empty;
                lstr_fecha = Convert.ToDateTime(lblFchModificacion.Text).ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                lstr_ResEliminacion = ws_SG.uwsEliminarArchivo(str_idArchivo, lstr_fecha);
                if (lstr_ResEliminacion[0] == "00")//Request.QueryString["Rev"] != null && 
                {
                    //    string lst_IdRevelacion = Request.QueryString["Rev"];

                    DataSet fileList = ws_SG.uwsObtenerArchivoPorIdResolucion(str_idResolucion, gstr_Sociedad,ConsultarIdExpediente(str_idResolucion));
                    gvFiles.DataSource = fileList;
                   // this.upArchivos.Update();
                    gvFiles.DataBind();
                    this.up_DatosPrincipales.Update();
                    MessageBox.Show("El archivo fue eliminado satisfactoriamente.");
                    
                }
                else MessageBox.Show(lstr_ResEliminacion[1]);
            }           

        }

        protected void btnGenerarAsientos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El sistema no posee, parámetros válidos de cuentas, para enviar asientos a SIG@F.Configurelos en el Módulo de Mantenimiento previamente.");
        }

        protected void txtMontoPosDesembolso_TextChanged(object sender, EventArgs e)
        {            
            decimal montodesembolso = this.txtMontoPosDesembolso.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoPosDesembolso.Text);
            this.txtMontoPosDesembolso.Text = montodesembolso.ToString("N2");
            if (!this.DDLMoneda.SelectedValue.Equals("0"))
                DDLMoneda_SelectedIndexChanged(sender, e);
            this.up_DatosMontos.Update();
        }

        protected void txtMontoPrincipal_TextChanged(object sender, EventArgs e)
        {            
            decimal montoprincipal = this.txtMontoPrincipal.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoPrincipal.Text);
            this.txtMontoPrincipal.Text = montoprincipal.ToString("N2");
            if (!this.DDLMoneda.SelectedValue.Equals("0"))
                DDLMoneda_SelectedIndexChanged(sender, e);
            this.up_DatosMontos.Update();
        }

        protected void txtMontoIntereses_TextChanged(object sender, EventArgs e)
        {
            decimal montointereses = this.txtMontoIntereses.Text == "" ? 0 : Convert.ToDecimal(this.txtMontoIntereses.Text);
            this.txtMontoIntereses.Text = montointereses.ToString("N2");
            if (!this.DDLMoneda.SelectedValue.Equals("0"))
                DDLMoneda_SelectedIndexChanged(sender, e);
            this.up_DatosMontos.Update();
        }

        private string  verificarMontos()
        {
            string result;

            if ( ( this.txtMontoPrincipal.Text.Equals("") || this.txtMontoPrincipal.Text.Equals("0.00"))
                && (this.txtMontoIntereses.Text.Equals("") || this.txtMontoIntereses.Text.Equals("0.00"))
                && (this.txtMontoColonesPrincipal.Text.Equals("") || this.txtMontoColonesPrincipal.Text.Equals("0.00"))
                && (this.txtMontoColonesIntereses.Text.Equals("") || this.txtMontoColonesIntereses.Text.Equals("0.00")) ) 
            {
                if (this.txtMontoColonesPrincipal.Text.Equals(""))
                    gdec_MontoPrincipalColones = Convert.ToDecimal("0,00");

                result="Vacio";
                return result;
            }
            return result = "Lleno";
        }

        private DataRow ObtenerResolucion(string tipoResolucion)
        {
            DataRow result=null;
            string consulta = "SELECT * FROM co.CobrosPagos INNER JOIN co.Resoluciones ON co.CobrosPagos.IdResolucionFK=co.Resoluciones.IdResolucion and co.Resoluciones.EstadoResolucion='" + tipoResolucion + "'";

            DataTable dt = GetData(consulta);
            if (dt.Rows.Count > 0)
            {
                result = dt.Rows[0];//unica fila que coicide con el filtro
            }
            else
            {

                result = null;
            }
            return result;
        }

        #region Consultas
        private void CargarExpedientes()
        {
            String str_consul = "SELECT IdExpediente, IdExpediente+' : '+TipoExpediente AS NomExpediente FROM co.Expedientes " +
                "WHERE co.Expedientes.EstadoExpediente='Activo' and IdSociedadGL='" + gstr_Sociedad + "'";
            DataTable exped = GetData(str_consul);
            this.DDLExpedientes.Items.Clear();
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                this.DDLExpedientes.DataSource = exped;
                this.DDLExpedientes.DataTextField = "NomExpediente";
                this.DDLExpedientes.DataValueField = "IdExpediente";
                this.DDLExpedientes.Items.Insert(0, new ListItem("--- Elegir Expediente---", "0"));
                this.DDLExpedientes.DataBind();
            }
            else
            {
                //MessageBox.Show("No hay Expedientes disponibles.");
            }
        }
       
        #endregion

        public void GuardarResultadoResoluciones()
        {
            string path = @"C:\inetpub\wwwroot\SistemaGestor\Logs\LogResoluciones.txt";

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                File.WriteAllText(path, asiento.getMensajeResultadoResoluciones());
            }
            string readText = File.ReadAllText(path);
            //Console.WriteLine(readText);

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

        /// <summary>
        /// Si no tiene formato de fecha valida retorna la fecha vigente
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        private DateTime FechaValida(String fecha)
        {
            try
            {
                 return DateTime.Parse(fecha);
            }
            catch
            {
                return DateTime.Now;
            }
        }


        protected void ckbNuevoAno_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void ckbNuevoMes_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void DDLEstadoProcesal_SelectedIndexChanged(object sender, EventArgs e)
        {
            gstr_EstadoProcesal = this.DDLEstadoProcesal.SelectedItem.Value;
        }

        private String RetornaAsientos(string str_Asiento)
        {
            #region Reversiones
            string lstr_AsientoFinal = string.Empty;
            gbool_CambioAno = (gint_Periodo < DateTime.Today.Year) || this.ckbNuevoAno.Checked;
            switch (str_Asiento)
            {
                case "CT01":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT61" : "CT60";
                    }
                    break;
                case "CT01P":
                    {
                        lstr_AsientoFinal = "CT61";
                    }
                    break;
                case "CT02":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT63" : "CT62";
                    }
                    break;
                case "CT02P":
                    {
                        lstr_AsientoFinal = "CT63";
                    }
                    break;
                case "CT03":
                    {
                        lstr_AsientoFinal = "CT64";
                    }
                    break;
                case "CT04":
                    {
                        lstr_AsientoFinal = "CT65";
                    }
                    break;
                case "CT05":
                    {
                        lstr_AsientoFinal = "CT66";
                    }
                    break;
                case "CT06":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT68" : "CT67";
                    }
                    break;
                case "CT06P":
                    {
                        lstr_AsientoFinal = "CT68";
                    }
                    break;
                case "CT07":
                    {
                        lstr_AsientoFinal = "CT69";
                    }
                    break;
                case "CT08":
                    {
                        lstr_AsientoFinal = "CT70";
                    }
                    break;
                case "CT09":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT72" : "CT71";
                    }
                    break;
                case "CT09P":
                    {
                        lstr_AsientoFinal = "CT72";
                    }
                    break;
                case "CT10":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT74" : "CT73";
                    }
                    break;
                case "CT10P":
                    {
                        lstr_AsientoFinal = "CT74";
                    }
                    break;
                case "CT13":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT76" : "CT75";
                    }
                    break;
                case "CT13P":
                    {
                        lstr_AsientoFinal = "CT76";
                    }
                    break;
                case "CT14":
                    {
                        lstr_AsientoFinal = "CT77";
                    }
                    break;
                case "CT15":
                    {
                        lstr_AsientoFinal = "CT78";
                    }
                    break;
                case "CT16":
                    {
                        lstr_AsientoFinal = "CT79";
                    }
                    break;
                case "CT17":
                    {
                        lstr_AsientoFinal = "CT80";
                    }
                    break;
                case "CT18":
                    {
                        lstr_AsientoFinal = "CT81";
                    }
                    break;
                case "CT19":
                    {
                        lstr_AsientoFinal = "CT82";
                    }
                    break;
                case "CT20":
                    {
                        lstr_AsientoFinal = "CT83";
                    }
                    break;
                case "CT21":
                    {
                        lstr_AsientoFinal = "CT84";
                    }
                    break;
                case "CT22":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT86" : "CT85";
                    }
                    break;
                case "CT22P":
                    {
                        lstr_AsientoFinal = "CT86";
                    }
                    break;
                case "CT23":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT88" : "CT87";
                    }
                    break;
                case "CT23P":
                    {
                        lstr_AsientoFinal = "CT88";
                    }
                    break;
                case "CT24":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT90" : "CT89";
                    }
                    break;
                case "CT24P":
                    {
                        lstr_AsientoFinal = "CT90";
                    }
                    break;
                case "CT25":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT92" : "CT91";
                    }
                    break;
                case "CT25P":
                    {
                        lstr_AsientoFinal = "CT92";
                    }
                    break;
                case "CT26":
                    {
                        lstr_AsientoFinal = "CT93";
                    }
                    break;
                case "CT27":
                    {
                        lstr_AsientoFinal = "CT94";
                    }
                    break;
                case "CT28":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT96" : "CT95";
                    }
                    break;
                case "CT28P":
                    {
                        lstr_AsientoFinal = "CT96";
                    }
                    break;
                case "CT29":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT98" : "CT97";
                    }
                    break;
                case "CT29P":
                    {
                        lstr_AsientoFinal = "CT98";
                    }
                    break;
                case "CT30":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT100" : "CT99";
                    }
                    break;
                case "CT30P":
                    {
                        lstr_AsientoFinal = "CT100";
                    }
                    break;
                case "CT32":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT102" : "CT101";
                    }
                    break;
                case "CT32P":
                    {
                        lstr_AsientoFinal = "CT102";
                    }
                    break;
                case "CT34":
                    {
                        lstr_AsientoFinal = "CT103";
                    }
                    break;
                case "CT35":
                    {
                        lstr_AsientoFinal = "CT104";
                    }
                    break;
                case "CT36":
                    {
                        lstr_AsientoFinal = "CT105";
                    }
                    break;
                case "CT38":
                    {
                        lstr_AsientoFinal = "CT106";
                    }
                    break;
                case "CT39":
                    {
                        lstr_AsientoFinal = "CT107";
                    }
                    break;

            }
            #endregion
            return lstr_AsientoFinal;
        }

        private DataTable ConsultarCobrosPagos(string str_IdExpedienteFK, int int_IdRes)
        {
            return GetData("SELECT * FROM co.CobrosPagos where IdExpedienteFK = '" + str_IdExpedienteFK + "' and IdRes = " + int_IdRes + " AND TipoTransaccion!= 'tipotra'");
        }
    }
}
