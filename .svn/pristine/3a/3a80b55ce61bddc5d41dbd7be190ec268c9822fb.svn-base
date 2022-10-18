using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using LogicaNegocio.Ejemplo;
using LogicaNegocio.Seguridad;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Contingentes;
using log4net;
using log4net.Config;

namespace WebServiceConnecionSAP
{
    /// <summary>
    /// Summary description for wsConeccionSAP
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsConeccionSAP : System.Web.Services.WebService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(wsConeccionSAP));

        #region TablasSAP

        [WebMethod]
        public String[] uwsCrearCentroCostos(string str_IdCentroCosto, string str_IdSociedadCO, string str_DenominacionGn, string str_Descripcion, string str_CnBeneficio, DateTime str_FinValidez, DateTime str_ValidoD, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tCentroCosto ltro_CentroCosto = new tCentroCosto();
                bool_ResCreacion = ltro_CentroCosto.CrearCentroCosto(str_IdCentroCosto, str_ValidoD, str_FinValidez, str_IdSociedadCO, str_CnBeneficio, str_DenominacionGn, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {  
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();
            
            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearAreaFuncional(string str_IdAreaFuncional, string str_NbrAreaFuncional, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {
                tAreaFuncional ltro_AreaFuncional = new tAreaFuncional();
                bool_ResCreacion = ltro_AreaFuncional.CrearAreaFuncional(str_IdAreaFuncional, str_NbrAreaFuncional, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearCentroBeneficio(string str_CodigoCB, string str_SociedadCO, string str_DenominacionGn, string str_Descripcion, DateTime str_FinValidez, DateTime str_ValidoD, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {
                tCentroBeneficio ltro_CentroBeneficio = new tCentroBeneficio();
                bool_ResCreacion = ltro_CentroBeneficio.CrearCentroBeneficio(str_CodigoCB, str_ValidoD, str_FinValidez, str_SociedadCO, str_DenominacionGn, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearCentroGestor(string str_CodigoCG, string str_EntidadCP, string str_DenominacionGn, string str_Descripcion, DateTime str_FinValidez, DateTime str_ValidoD, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {
                tCentroGestor ltro_CentroBeneficio = new tCentroGestor();
                bool_ResCreacion = ltro_CentroBeneficio.CrearCentroGestor(str_CodigoCG, str_ValidoD, str_FinValidez, str_EntidadCP, str_DenominacionGn, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearClaseDocumento(string str_CodigoCD, string str_NbrClaseDocumento, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {
                tClaseDocumento ltro_AreaFuncional = new tClaseDocumento();
                bool_ResCreacion = ltro_AreaFuncional.CrearClaseDocumento(str_CodigoCD, str_NbrClaseDocumento, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearCuentaContablePlanCuenta(string str_IdCuentaMayor, string str_IdPlanCuenta, string str_GrupoCuentas, string str_TxtBreve, string str_TxtDescriptivo, string str_CuentaGrupo, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tCuentaContable ltro_CuentaContable  = new tCuentaContable();
                bool_ResCreacion = ltro_CuentaContable.CrearCuentaContable(str_IdCuentaMayor, str_IdPlanCuenta, str_GrupoCuentas, str_TxtBreve, str_TxtDescriptivo, str_CuentaGrupo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearCuentaContableSociedad(string str_IdCuentaMayor, string str_Sociedad, string str_MonedaCuenta, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tCuentaContable ltro_CuentaContable = new tCuentaContable();
                bool_ResCreacion = ltro_CuentaContable.CrearCuentaSociedad(str_IdCuentaMayor, str_Sociedad, str_MonedaCuenta, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearElementoPEP(string str_IdElementoPEP, string str_Descripcion, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tElementoPEP ltro_ElementoPEP = new tElementoPEP();
                bool_ResCreacion = ltro_ElementoPEP.CrearElementoPEP(str_IdElementoPEP, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearEntidadesCP(string str_IdEntidadCP, string str_TxtEntidad, string str_Moneda, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tEntidadCP ltro_EntidadCP = new tEntidadCP();
                bool_ResCreacion = ltro_EntidadCP.CrearEntidadCP(str_IdEntidadCP, str_TxtEntidad, str_Moneda, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearFondo(string str_IdFondo, string str_EntidadCP, string str_Denominacion, string str_Descripcion, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tFondo ltro_Fondo = new tFondo();
                bool_ResCreacion = ltro_Fondo.CrearFondo(str_IdFondo, str_EntidadCP, str_Denominacion, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearGrupoCuentaContable(string str_IdGrupoCuenta, string str_IdPlanCuenta, string str_Significado,string str_Desde, string str_Hasta, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tGrupoCuenta ltro_GrupoCuenta = new tGrupoCuenta();
                bool_ResCreacion = ltro_GrupoCuenta.CrearGrupoCuenta(str_IdGrupoCuenta, str_IdPlanCuenta, str_Desde, str_Hasta, str_Significado, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearPosicionPresupuestaria(string str_IdPosicionPre, string str_IdEntidadCP, string str_IdEjercico, string str_Denominacion, string str_Descripcion, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tPosicionPresupuestaria ltro_PosicionPresupuestaria = new tPosicionPresupuestaria();
                bool_ResCreacion = ltro_PosicionPresupuestaria.CrearPosicionPresupuestaria(str_IdPosicionPre, str_IdEntidadCP, str_IdEjercico, str_Denominacion, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearProgramaPresupuestario(string str_IdProgramaPre, string str_IdEntidadCP, string str_Denominacion, string str_Descripcion, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tPrograma ltro_Programa  = new tPrograma();
                bool_ResCreacion = ltro_Programa.CrearPrograma(str_IdProgramaPre, str_IdEntidadCP, str_Denominacion, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearSociedadCosto(string str_IdSociedad, string str_Descripcion, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tSociedadCosto ltro_SociedadCosto = new tSociedadCosto();
                bool_ResCreacion = ltro_SociedadCosto.CrearSociedadCosto(str_IdSociedad, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearSociedadFinaciera(string str_IdSociedadFin, string str_Denominacion, string str_ClavePais, string str_Poblacion, string str_ClaveMoneda, string str_ClaveIdioma, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tSociedadFinanciera ltro_SociedadFinaciera = new tSociedadFinanciera();
                bool_ResCreacion = ltro_SociedadFinaciera.CrearSociedadFinanciera(str_IdSociedadFin, str_Denominacion, str_Denominacion, str_Poblacion, str_ClaveMoneda, str_ClaveIdioma, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearSociedadGL(string str_IdSociedadGL, string str_Denominacion, string str_ClavePais, string str_Poblacion, string str_Calle, string str_ClaveMoneda, string str_ClaveIdioma, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tSociedadGL ltro_SociedadGL = new tSociedadGL();
                bool_ResCreacion = ltro_SociedadGL.CrearSociedadGL(str_IdSociedadGL, str_Denominacion, str_Denominacion, str_Poblacion, str_Calle, str_ClaveMoneda, str_ClaveIdioma, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearUnidadConsolidacion(string str_IdVista, string str_IdUnidadCons, string str_TxtBreve, string str_TxtMedio, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tUnidadConsolidacion ltro_UnidadConsolidacion = new tUnidadConsolidacion();
                bool_ResCreacion = ltro_UnidadConsolidacion.CrearUnidadConsolidacion(str_IdVista, str_IdUnidadCons, str_TxtBreve, str_TxtMedio, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearAmbitoConsolidacion(string str_IdVista, string str_IdAmbitoCons, string str_TxtBreve, string str_TxtMedio, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tAmbitoConsolidacion ltro_AmbitoConsolidacion  = new tAmbitoConsolidacion();
                bool_ResCreacion = ltro_AmbitoConsolidacion.CrearAmbitoConsolidacion(str_IdVista, str_IdAmbitoCons, str_TxtBreve, str_TxtMedio, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearJerarquia(string str_IdVista, string str_IdJerarquia, string str_TxtBreve, string str_TxtMedio, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tJerarquia ltro_Jerarquia = new tJerarquia();
                bool_ResCreacion = ltro_Jerarquia.CrearJerarquia(str_IdVista, str_IdJerarquia, str_TxtBreve, str_TxtMedio, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearJerarquiaPeriodo(string str_IdVista, string str_IdJerarquiaPer, string str_IdEjercicio, string str_IdPeriodo, string str_AmbitoCons, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tJerarquiaPeriodo ltro_JerarquiaPeriodo = new tJerarquiaPeriodo();
                bool_ResCreacion = ltro_JerarquiaPeriodo.CrearJerarquiaPeriodo(str_IdVista, str_IdJerarquiaPer, str_IdEjercicio, str_IdPeriodo, str_AmbitoCons, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }


        [WebMethod]
        public String[] uwsCrearAsignacionACUC(string str_IdVista, string str_IdVersion, string str_IdAmbitoCons, string str_IdUnidadCons, string str_IdEjercico, string str_IdPeriodo, bool str_NodoUC, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {

                tAsignacionACUC ltro_AsignacionACUC = new tAsignacionACUC();
                bool_ResCreacion = ltro_AsignacionACUC.CrearAsignacionACUC(str_IdVista, str_IdVersion, str_IdAmbitoCons, str_IdUnidadCons, str_IdEjercico, str_IdPeriodo, str_NodoUC, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

       
        #endregion

    }
}
