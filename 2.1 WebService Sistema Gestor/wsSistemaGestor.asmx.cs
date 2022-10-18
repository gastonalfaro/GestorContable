using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using LogicaNegocio.Ejemplo;
using LogicaNegocio.Seguridad;
using LogicaNegocio.CapturaIngresos;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Contingentes;
using LogicaNegocio.RevelacionNotas;
using log4net;
using log4net.Config;
using System.Net.Mail;
using System.Text;
using Logica.SubirArchivo;
using LogicaNegocio.CalculosFinancieros;
using LogicaNegocio.CalculosFinancieros.DeudaExterna;
using LogicaNegocio.Seguridad;
using LogicaNegocio.Mantenimiento;
using System.Globalization;
using System.Configuration;

//Log4Net inicializa en WebApplication
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace wsSistemaGestor
{

    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsSistemaGestor : System.Web.Services.WebService
    {
        /// log4Net variable 
        private static readonly ILog log = LogManager.GetLogger(typeof(wsSistemaGestor));
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private tSeguridad gcls_Seguridad = new tSeguridad();

        //para enviar el email
        private tUsuario usr_Envio = new tUsuario();
        //para sacar a quién se debe enviar la notificación
        private tSociedadGL soc_Consulta = new tSociedadGL();
        //Para sacar la sociedad gl del formulario de captura
        private clsFormulariosCapturaIngresos fci_Formulario = new clsFormulariosCapturaIngresos();

        private static string lstr_formato_fecha = "dd/MM/yyyy";

        private string[] DatosConexion()
        {
            string[] str_DatosConn = new string[5];
            str_DatosConn[0] = ConfigurationManager.AppSettings["Puerto"];
            str_DatosConn[1] = ConfigurationManager.AppSettings["Host"];
            str_DatosConn[2] = ConfigurationManager.AppSettings["UsuarioSistema"];
            str_DatosConn[3] = ConfigurationManager.AppSettings["CredencialUsuario"];
            str_DatosConn[4] = ConfigurationManager.AppSettings["CredencialContrasena"];
            return str_DatosConn;
        }

        #region Mantenimiento
        #region AreasFuncionales
        [WebMethod]
        public bool uwsCrearAreaFuncional(string str_IdAreaFuncional, string str_NomAreaFuncional, string str_Estado = null, string str_UsrCreacion = null)
        {
            bool bool_ResCreacion = false;
            string str_CodResultado = null;
            string str_Mensaje = null;
            try
            {
                tAreaFuncional ltro_AreaFuncional = new tAreaFuncional();
                bool_ResCreacion = ltro_AreaFuncional.CrearAreaFuncional(str_IdAreaFuncional, str_NomAreaFuncional, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarAreasFuncionales(string str_IdAreaFuncional, string str_NomAreaFuncional)
        {
            DataSet lds_AreasFuncionales = new DataSet();
            try
            {
                tAreaFuncional tus_AreaFuncional = new tAreaFuncional();
                lds_AreasFuncionales = tus_AreaFuncional.ConsultarAreasFuncionales(str_IdAreaFuncional, str_NomAreaFuncional);
            }
            catch
            { }
            return lds_AreasFuncionales;
        }
        #endregion AreasFuncionales

        #region Ambito Consolidacion
        [WebMethod]
        public bool uwsCrearAmbitoConsolidacion(string str_Vista, string str_IdAmbitoConsolidacion, string str_NomCorto, string str_NomAmbito, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tAmbitoConsolidacion ltro_AmbitoConsolidacion = new tAmbitoConsolidacion();
                bool_ResCreacion = ltro_AmbitoConsolidacion.CrearAmbitoConsolidacion(str_Vista, str_IdAmbitoConsolidacion, str_NomCorto, str_NomAmbito, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarAmbitosConsolidacion(string str_Vista, string str_IdAmbitoConsolidacion, string str_NomCorto, string str_NomAmbito)
        {
            DataSet lds_AmbitosConsolidacion = new DataSet();
            try
            {
                tAmbitoConsolidacion tus_AmbitoConsolidacion = new tAmbitoConsolidacion();
                lds_AmbitosConsolidacion = tus_AmbitoConsolidacion.ConsultarAmbitosConsolidacion(str_Vista, str_IdAmbitoConsolidacion, str_NomCorto, str_NomAmbito);
            }
            catch
            { }
            return lds_AmbitosConsolidacion;
        }
        #endregion Ambito Consolidacion

        #region AsignacionACUC
        [WebMethod]
        public bool uwsCrearAsignacionACUC(string str_Vista, string str_Version, string str_IdAmbitoConsolidacion, string str_IdUnidadConsolidacion, string str_IdEjercicio, string str_IdPeriodo, Boolean bln_EsUnidad, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tAsignacionACUC ltro_AsignacionACUC = new tAsignacionACUC();
                bool_ResCreacion = ltro_AsignacionACUC.CrearAsignacionACUC(str_Vista, str_Version, str_IdAmbitoConsolidacion, str_IdUnidadConsolidacion, str_IdEjercicio, str_IdPeriodo, bln_EsUnidad, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarAsignacionesACUC(string str_Vista, string str_Version, string str_IdAmbitoConsolidacion, string str_IdUnidadConsolidacion, string str_IdEjercicio, string str_IdPeriodo, Boolean bln_EsUnidad)
        {
            DataSet lds_AsignacionesACUC = new DataSet();
            try
            {
                tAsignacionACUC tus_AmbitoConsolidacion = new tAsignacionACUC();
                lds_AsignacionesACUC = tus_AmbitoConsolidacion.ConsultarAsignacionesACUC(str_Vista, str_Version, str_IdAmbitoConsolidacion, str_IdUnidadConsolidacion, str_IdEjercicio, str_IdPeriodo, bln_EsUnidad);
            }
            catch
            { }
            return lds_AsignacionesACUC;
        }
        #endregion AsignacionACUC

        #region Centro Beneficio
        [WebMethod]
        public bool uwsCrearCentroBeneficio(string str_IdCentroBeneficio, DateTime dt_FchVigencia, DateTime dt_FchVigenciaHasta, string str_IdSociedadCo, string str_IdSociedadFi, string str_Denominacion, string str_NomCentroBeneficio, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tCentroBeneficio ltro_CentroBeneficio = new tCentroBeneficio();
                bool_ResCreacion = ltro_CentroBeneficio.CrearCentroBeneficio(str_IdCentroBeneficio, dt_FchVigencia, dt_FchVigenciaHasta, str_IdSociedadCo, str_IdSociedadFi, str_Denominacion, str_NomCentroBeneficio, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }


        [WebMethod]
        public DataSet uwsConsultarCentrosBeneficio(string str_IdCentroBeneficio, DateTime dt_FchVigenciaHasta, string str_IdSociedadCo, string str_IdSociedadFi, DateTime dt_FchConsulta, string str_Denominacion, string str_NomCentroBeneficio)
        {
            DataSet lds_CentrosBeneficio = new DataSet();
            try
            {
                tCentroBeneficio tus_CentroBeneficio = new tCentroBeneficio();
                lds_CentrosBeneficio = tus_CentroBeneficio.ConsultarCentrosBeneficio(str_IdCentroBeneficio, dt_FchVigenciaHasta, str_IdSociedadCo, str_IdSociedadFi, dt_FchConsulta, str_Denominacion, str_NomCentroBeneficio);
            }
            catch
            { }
            return lds_CentrosBeneficio;
        }
        #endregion Centro Beneficio

        #region Centro Costo
        [WebMethod]
        public bool uwsCrearCentroCosto(string str_IdCentroCosto, DateTime dt_FchVigencia, DateTime dt_FchVigenciaHasta, string str_IdSociedadCo, string str_IdSociedadFi, string str_IdCentroBeneficio, string str_Denominacion, string str_NomCentroCosto, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tCentroCosto ltro_CentroCosto = new tCentroCosto();
                bool_ResCreacion = ltro_CentroCosto.CrearCentroCosto(str_IdCentroCosto, dt_FchVigencia, dt_FchVigenciaHasta, str_IdSociedadCo, str_IdSociedadFi, str_IdCentroBeneficio, str_Denominacion, str_NomCentroCosto, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }
        [WebMethod]
        public DataSet uwsConsultarCentrosCosto(string str_IdCentroCosto, DateTime dt_FchVigenciaHasta, string str_IdSociedadCo, string str_IdSociedadFi, DateTime dt_FchConsulta, string str_Denominacion, string str_NomCentroCosto)
        {
            DataSet lds_CentrosCosto = new DataSet();
            try
            {
                tCentroCosto tus_CentroCosto = new tCentroCosto();
                lds_CentrosCosto = tus_CentroCosto.ConsultarCentrosCosto(str_IdCentroCosto, dt_FchVigenciaHasta, str_IdSociedadCo, str_IdSociedadFi, dt_FchConsulta, str_Denominacion, str_NomCentroCosto);
            }
            catch
            { }
            return lds_CentrosCosto;
        }
        #endregion Centro Costo

        #region Centro Gestor
        [WebMethod]
        public bool uwsCrearCentroGestor(string str_IdCentroGestor, DateTime dt_FchVigencia, DateTime dt_FchVigenciaHasta, string str_IdEntidadCP, string str_IdSociedadFi, string str_Denominacion, string str_NomCentroGestor, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tCentroGestor ltro_CentroGestor = new tCentroGestor();
                bool_ResCreacion = ltro_CentroGestor.CrearCentroGestor(str_IdCentroGestor, dt_FchVigencia, dt_FchVigenciaHasta, str_IdEntidadCP, str_IdSociedadFi, str_Denominacion, str_NomCentroGestor, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }
        [WebMethod]
        public DataSet uwsConsultarCentrosGestores(string str_IdCentroGestor, DateTime dt_FchVigenciaHasta, string str_IdEntidadCP, string str_IdSociedadFi, DateTime dt_FchConsulta, string str_Denominacion, string str_NomCentroGestor)
        {
            DataSet lds_CentrosGestor = new DataSet();
            try
            {
                tCentroGestor tus_CentroGestor = new tCentroGestor();
                lds_CentrosGestor = tus_CentroGestor.ConsultarCentrosGestores(str_IdCentroGestor, dt_FchVigenciaHasta, str_IdEntidadCP, str_IdSociedadFi, dt_FchConsulta, str_Denominacion, str_NomCentroGestor);
            }
            catch
            { }
            return lds_CentrosGestor;
        }
        #endregion Centro Gestor

        #region Clase Documento
        public bool uwsCrearClaseDocumento(string str_IdClaseDocumento, string str_NomClaseDocumento, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tClaseDocumento ltro_ClaseDocumento = new tClaseDocumento();

                bool_ResCreacion = ltro_ClaseDocumento.CrearClaseDocumento(str_IdClaseDocumento, str_NomClaseDocumento, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarClasesDocumento(string str_IdClaseDocumento, string str_NomClaseDocumento)
        {
            DataSet lds_ClasesDocumento = new DataSet();
            try
            {
                tClaseDocumento tus_ClaseDocumento = new tClaseDocumento();
                lds_ClasesDocumento = tus_ClaseDocumento.ConsultarClasesDocumento(str_IdClaseDocumento, str_NomClaseDocumento);
            }
            catch
            { }
            return lds_ClasesDocumento;
        }
        #endregion Clase Documento

        #region Cuenta Contable
        //[WebMethod]
        //public bool uwsCrearCuentaContable(string str_IdCuentaContable, string str_IdPlanCuenta, string str_IdGrupoCuenta, string str_NomCorto, string str_NomCuentaContable, string str_CuentaGrupo, string str_IndTotales, string str_IndConsolidacion, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        //{
        //    bool bool_ResCreacion = false;
        //    str_CodResultado = null;
        //    str_Mensaje = null;
        //    try
        //    {
        //        tCuentaContable ltro_CuentaContable = new tCuentaContable();
        //        bool_ResCreacion = ltro_CuentaContable.CrearCuentaContable(str_IdCuentaContable, str_IdPlanCuenta, str_IdGrupoCuenta, str_NomCorto, str_NomCuentaContable, str_CuentaGrupo, str_IndTotales, str_IndConsolidacion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

        //        Log.Info(str_Mensaje);
        //    }
        //    catch (Exception ex)
        //    { Log.Error(ex.ToString()); }
        //    return bool_ResCreacion;
        //}
        [WebMethod]
        public DataSet uwsConsultarCuentasContables(string str_IdCuentaContable, string str_IdPlanCuenta, string str_IdGrupoCuenta, string str_NomCuenta, string str_CuentaGrupo, string str_IndTotales, string str_IndConsolidacion, string str_IdSociedadFi)
        {
            DataSet lds_CuentasContables = new DataSet();
            try
            {
                tCuentaContable tus_CuentaContable = new tCuentaContable();
                lds_CuentasContables = tus_CuentaContable.ConsultarCuentasContables(str_IdCuentaContable, str_IdPlanCuenta, str_IdGrupoCuenta, str_NomCuenta, str_CuentaGrupo, str_IndTotales, str_IndConsolidacion, str_IdSociedadFi);
            }
            catch
            { }
            return lds_CuentasContables;
        }

        [WebMethod]
        public DataSet uwsConsultarCuentasContablesTipo(string str_GrupoCuentas)
        {
            DataSet lds_CuentasContables = new DataSet();
            try
            {
                tCuentaContable tus_CuentaContable = new tCuentaContable();
                lds_CuentasContables = tus_CuentaContable.ConsultarCuentasContablesTipo(str_GrupoCuentas);
            }
            catch
            { }
            return lds_CuentasContables;
        }
        #endregion Cuenta Contable

        #region Cuenta Sociedad
        [WebMethod]
        public bool uwsCrearCuentaSociedad(string str_IdCuentaContable, string str_IdSocieadadFi, string str_IdMoneda, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tCuentaContable ltro_CuentaSociedad = new tCuentaContable();
                bool_ResCreacion = ltro_CuentaSociedad.CrearCuentaSociedad(str_IdCuentaContable, str_IdSocieadadFi, str_IdMoneda, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }


        [WebMethod]
        public DataSet uwsConsultarCuentasSociedades(string str_IdCuentaContable, string str_IdSociedadFi, string str_IdMoneda)
        {
            DataSet lds_CuentasContables = new DataSet();
            try
            {
                tCuentaContable tus_CuentaContable = new tCuentaContable();
                lds_CuentasContables = tus_CuentaContable.ConsultarCuentasSociedades(str_IdCuentaContable, str_IdSociedadFi, str_IdMoneda);
            }
            catch
            { }
            return lds_CuentasContables;
        }


        #endregion Cuenta Sociedad

        #region ElementoPEP
        [WebMethod]
        public bool uwsCrearElementoPEP(string str_IdElementoPEP, string str_NomElementoPEP, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tElementoPEP ltro_ElementoPEP = new tElementoPEP();
                bool_ResCreacion = ltro_ElementoPEP.CrearElementoPEP(str_IdElementoPEP, str_NomElementoPEP, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarElementosPEP(string str_IdElementoPEP, string str_NomElementoPEP)
        {
            DataSet lds_ElementosPEP = new DataSet();
            try
            {
                tElementoPEP tus_ElementoPEP = new tElementoPEP();
                lds_ElementosPEP = tus_ElementoPEP.ConsultarElementosPEP(str_IdElementoPEP, str_NomElementoPEP);
            }
            catch
            { }
            return lds_ElementosPEP;
        }
        #endregion ElementoPEP

        #region Empresas
        [WebMethod]
        public bool uwsCrearEmpresa(string str_IdPersonaJuridica, string str_Nombre, string str_CorreoEmpresa, string str_TelefonoEmpresa, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsEmpresas ltro_Empresas = new clsEmpresas();
                bool_ResCreacion = ltro_Empresas.CrearEmpresa(str_IdPersonaJuridica, str_Nombre, str_CorreoEmpresa, str_TelefonoEmpresa, str_TipoIdPersonaAutoriza, str_IdPersonaAutoriza, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarEmpresas(string str_IdPersonaJuridica, string str_Nombre, string str_IdPersonaAutorizada, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza)
        {
            DataSet lds_Empresas = new DataSet();
            try
            {
                clsEmpresas tus_Empresas = new clsEmpresas();
                lds_Empresas = tus_Empresas.ConsultarEmpresas(str_IdPersonaJuridica, str_Nombre, str_IdPersonaAutorizada, str_TipoIdPersonaAutoriza, str_IdPersonaAutoriza);
            }
            catch
            { }
            return lds_Empresas;
        }
        #endregion Empresas

        #region EmpresasAutorizados
        [WebMethod]
        public String[] uwsCrearEmpresaAutorizado(string str_IdPersonaJuridica, string str_TipoIdPersonaAutorizada, string str_IdPersonaAutorizada, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza, string str_CtaCliente, string str_NombrePersonaAutorizada, string str_PuestoPersonaAutorizada, string str_Estado, string str_UsrCreacion)
        {
            String[] arr_Resultado;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {
                clsEmpresasAutorizados ltro_EmpresasAutorizados = new clsEmpresasAutorizados();
                bool_ResCreacion = ltro_EmpresasAutorizados.CrearEmpresaAutorizado(str_IdPersonaJuridica, str_TipoIdPersonaAutorizada, str_IdPersonaAutorizada, str_TipoIdPersonaAutoriza, str_IdPersonaAutoriza, str_CtaCliente, str_NombrePersonaAutorizada, str_PuestoPersonaAutorizada, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);

                arr_Resultado = new String[2];
                arr_Resultado[0] = bool_ResCreacion.ToString();
                arr_Resultado[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_Resultado = new String[2];
                arr_Resultado[0] = bool_ResCreacion.ToString();
                arr_Resultado[1] = ex.ToString();

            }
            return arr_Resultado;
        }

        [WebMethod]
        public DataSet uwsConsultarEmpresasAutorizados(string str_IdPersonaJuridica, string str_TipoIdPersonaAutorizada, string str_IdPersonaAutorizada, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza, string str_CtaCliente, string str_Estado)
        {
            DataSet lds_EmpresasAutorizados = new DataSet();
            try
            {
                clsEmpresasAutorizados tus_EmpresasAutorizados = new clsEmpresasAutorizados();
                lds_EmpresasAutorizados = tus_EmpresasAutorizados.ConsultarEmpresasAutorizados(str_IdPersonaJuridica, str_TipoIdPersonaAutorizada, str_IdPersonaAutorizada, str_TipoIdPersonaAutoriza, str_IdPersonaAutoriza, str_CtaCliente, str_Estado);
            }
            catch
            { }
            return lds_EmpresasAutorizados;
        }
        #endregion EmpresasAutorizados

        #region EntidadCP
        [WebMethod]
        public bool uwsCrearEntidadCP(string str_IdEntidadCP, string str_NomEntidadCP, string str_IdMoneda, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tEntidadCP ltro_EntidadCP = new tEntidadCP();
                bool_ResCreacion = ltro_EntidadCP.CrearEntidadCP(str_IdEntidadCP, str_NomEntidadCP, str_IdMoneda, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarEntidadesCP(string str_IdEntidadCP, string str_NomEntidadCP, string str_IdMoneda)
        {
            DataSet lds_EntidadesCP = new DataSet();
            try
            {
                tEntidadCP tus_EntidadCP = new tEntidadCP();
                lds_EntidadesCP = tus_EntidadCP.ConsultarEntidadesCP(str_IdEntidadCP, str_NomEntidadCP, str_IdMoneda);
            }
            catch
            { }
            return lds_EntidadesCP;
        }
        #endregion EntidadCP

        #region Fondo
        [WebMethod]
        public bool uwsCrearFondo(string str_IdFondo, string str_IdEntidadCP, string str_Denominacion, string str_NomFondo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;

            try
            {
                tFondo ltro_Fondo = new tFondo();
                bool_ResCreacion = ltro_Fondo.CrearFondo(str_IdFondo, str_IdEntidadCP, str_Denominacion, str_NomFondo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarFondos(string str_IdFondo, string str_IdEntidadCP, string str_Denominacion, string str_NomFondo)
        {
            DataSet lds_Fondos = new DataSet();
            try
            {
                tFondo tus_Fondo = new tFondo();
                lds_Fondos = tus_Fondo.ConsultarFondos(str_IdFondo, str_IdEntidadCP, str_Denominacion, str_NomFondo);
            }
            catch
            { }
            return lds_Fondos;
        }
        #endregion Fondo

        #region Grupo Cuenta
        [WebMethod]
        public bool uwsCrearGrupoCuenta(string str_IdGrupoCuenta, string str_IdPlanCuenta, string str_CuentaDesde, string str_CuentaHasta, string str_NomGrupoCuenta, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tGrupoCuenta ltro_GrupoCuenta = new tGrupoCuenta();
                bool_ResCreacion = ltro_GrupoCuenta.CrearGrupoCuenta(str_IdGrupoCuenta, str_IdPlanCuenta, str_CuentaDesde, str_CuentaHasta, str_NomGrupoCuenta, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarGruposCuentas(string str_IdGrupoCuenta, string str_IdPlanCuenta, string str_IdCuentaContable, string str_NomGrupoCuenta)
        {
            DataSet lds_GruposCuenta = new DataSet();
            try
            {
                tGrupoCuenta tus_GrupoCuenta = new tGrupoCuenta();
                lds_GruposCuenta = tus_GrupoCuenta.ConsultarGruposCuentas(str_IdGrupoCuenta, str_IdPlanCuenta, str_IdCuentaContable, str_NomGrupoCuenta);
            }
            catch
            { }
            return lds_GruposCuenta;
        }
        #endregion Grupo Cuenta

        #region Jerarquia
        [WebMethod]
        public bool uwsCrearJerarquia(string str_Vista, string str_IdJerarquia, string str_NomCorto, string str_NomJerarquia, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tJerarquia cls_ProcCrearJerarquia = new tJerarquia();
                bool_ResCreacion = cls_ProcCrearJerarquia.CrearJerarquia(str_Vista, str_IdJerarquia, str_NomCorto, str_NomJerarquia, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarJerarquias(string str_Vista, string str_IdJerarquia, string str_NomCorto, string str_NomJerarquia)
        {
            DataSet lds_Jerarquias = new DataSet();
            try
            {
                tJerarquia tus_Jerarquia = new tJerarquia();
                lds_Jerarquias = tus_Jerarquia.ConsultarJerarquias(str_Vista, str_IdJerarquia, str_NomCorto, str_NomJerarquia);
            }
            catch
            { }
            return lds_Jerarquias;
        }
        #endregion Jerarquia

        #region Jerarquia Periodo
        [WebMethod]
        public bool uwsCrearJerarquiaPeriodo(string str_Vista, string str_IdJerarquia, string str_IdEjercicio, string str_IdPeriodo, string str_IdAmbitoConsolidacion, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tJerarquiaPeriodo ltro_JerarquiaPeriodo = new tJerarquiaPeriodo();
                bool_ResCreacion = ltro_JerarquiaPeriodo.CrearJerarquiaPeriodo(str_Vista, str_IdJerarquia, str_IdEjercicio, str_IdPeriodo, str_IdAmbitoConsolidacion, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarJerarquiasPeriodo(string str_Vista, string str_IdJerarquia, string str_IdEjercicio, string str_IdPeriodo, string str_IdAmbitoConsolidacion)
        {
            DataSet lds_JerarquiasPeriodo = new DataSet();
            try
            {
                tJerarquiaPeriodo tus_JerarquiaPeriodo = new tJerarquiaPeriodo();
                lds_JerarquiasPeriodo = tus_JerarquiaPeriodo.ConsultarJerarquiasPeriodo(str_Vista, str_IdJerarquia, str_IdEjercicio, str_IdPeriodo, str_IdAmbitoConsolidacion);
            }
            catch
            { }
            return lds_JerarquiasPeriodo;
        }
        #endregion Jerarquia Periodo

        #region Modulo
        [WebMethod]
        public DataSet uwsConsultarModulos(string str_IdModulo = null, string str_NomModulo = null)
        {
            DataSet lds_Modulos = new DataSet();
            try
            {
                tModulo tus_Modulo = new tModulo();
                lds_Modulos = tus_Modulo.ConsultarModulos(str_IdModulo, str_NomModulo);
            }
            catch
            { }
            return lds_Modulos;
        }

        [WebMethod]
        public String[] uwsCrearModulo(string str_IdModulo, string str_NomModulo, string str_Estado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                tModulo ltro_Modulo = new tModulo();
                bool_ResCreacion = ltro_Modulo.CrearModulo(str_IdModulo, str_NomModulo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarModulo(string str_IdModulo, string str_NomModulo, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                tModulo ltro_Modulo = new tModulo();
                bool_ResModifica = ltro_Modulo.ModificarModulo(str_IdModulo, str_NomModulo, str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = str_CodResultado;
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[3];
                arr_ResModifica[0] = str_CodResultado;
                arr_ResModifica[1] = str_Mensaje;
                arr_ResModifica[2] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion Modulo

        #region Moneda
        [WebMethod]
        public DataSet uwsConsultarMonedas(string str_IdMoneda = null, string str_NomMoneda = null)
        {
            DataSet lds_Monedas = new DataSet();
            try
            {
                tMoneda tus_Moneda = new tMoneda();
                lds_Monedas = tus_Moneda.ConsultarMonedas(str_IdMoneda, str_NomMoneda);
            }
            catch
            { }
            return lds_Monedas;
        }
        [WebMethod]
        public String[] uwsCrearMoneda(string str_IdMoneda, string str_NomMoneda, string str_Estado, string str_UsrCreacion, char str_ConversionUSD = '/')
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                tMoneda ltro_Moneda = new tMoneda();
                bool_ResCreacion = ltro_Moneda.CrearMoneda(str_IdMoneda, str_NomMoneda, str_Estado, str_UsrCreacion, str_ConversionUSD, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarMoneda(string str_IdMoneda, string str_NomMoneda, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, char str_ConversionUSD = '/')
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                tMoneda ltro_Moneda = new tMoneda();
                bool_ResModifica = ltro_Moneda.ModificarMoneda(str_IdMoneda, str_NomMoneda, str_Estado, str_UsrModifica, dt_FchModifica, str_ConversionUSD, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion Moneda

        #region Parametro
        [WebMethod]
        public DataSet uwsConsultarParametros(string str_IdParametro, string str_IdModulo, DateTime dt_FchVigencia, string str_DesParametro, string str_TipoParametro)
        {
            DataSet lds_Parametros = new DataSet();
            try
            {
                tParametro tus_Parametro = new tParametro();
                lds_Parametros = tus_Parametro.ConsultarParametros(str_IdParametro, str_IdModulo, dt_FchVigencia, str_DesParametro, str_TipoParametro);
            }
            catch
            { }
            return lds_Parametros;
        }
        [WebMethod]
        public String[] uwsCrearParametro(string str_IdParametro, DateTime dt_FchVigencia, string str_IdModulo, string str_DesParametro, string str_TipoParametro, string str_Valor, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                tParametro ltro_Parametro = new tParametro();
                bool_ResCreacion = ltro_Parametro.CrearParametro(str_IdParametro, dt_FchVigencia, str_IdModulo, str_DesParametro, str_TipoParametro, str_Valor, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarParametro(string str_IdParametro, DateTime dt_FchVigencia, string str_IdModulo, string str_DesParametro, string str_TipoParametro, string str_Valor, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                tParametro ltro_Parametro = new tParametro();
                bool_ResModifica = ltro_Parametro.ModificarParametro(str_IdParametro, dt_FchVigencia, str_IdModulo, str_DesParametro, str_TipoParametro, str_Valor, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion Parametro

        #region PlanCuenta
        public bool uwsCrearPlanCuenta(string str_IdPlanCuenta, string str_NomPlanCuenta, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tPlanCuenta ltro_PlanCuenta = new tPlanCuenta();

                bool_ResCreacion = ltro_PlanCuenta.CrearPlanCuenta(str_IdPlanCuenta, str_NomPlanCuenta, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarPlanesCuentas(string str_IdPlanCuenta, string str_NomPlanCuenta)
        {
            DataSet lds_PlanesCuentas = new DataSet();
            try
            {
                tPlanCuenta tus_PlanCuenta = new tPlanCuenta();
                lds_PlanesCuentas = tus_PlanCuenta.ConsultarPlanesCuentas(str_IdPlanCuenta, str_NomPlanCuenta);
            }
            catch
            { }
            return lds_PlanesCuentas;
        }
        #endregion PlanCuenta

        #region PosicionPresupuestaria
        public bool uwsCrearPosicionPresupuestaria(string str_IdPosPre, string str_IdEntidadCP, string str_IdEjercicio, string str_Denominacion, string str_NomPosPre, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                TiposicionPresupuestaria ltro_PosicionPresupuestaria = new TiposicionPresupuestaria();

                bool_ResCreacion = ltro_PosicionPresupuestaria.CrearPosicionPresupuestaria(str_IdPosPre, str_IdEntidadCP, str_IdEjercicio, str_Denominacion, str_NomPosPre, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarPosicionesPresupuestarias(string str_IdPosPre, string str_IdEntidadCP, string str_IdEjercicio, string str_Denominacion, string str_NomPosPre)
        {
            DataSet lds_PosicionesPresupuestarias = new DataSet();
            try
            {
                TiposicionPresupuestaria tus_PosicionPresupuestaria = new TiposicionPresupuestaria();
                lds_PosicionesPresupuestarias = tus_PosicionPresupuestaria.ConsultarPosicionesPresupuestarias(str_IdPosPre, str_IdEntidadCP, str_IdEjercicio, str_Denominacion, str_NomPosPre);
            }
            catch
            { }
            return lds_PosicionesPresupuestarias;
        }
        #endregion PosicionPresupuestaria

        #region Programa
        public bool uwsCrearPrograma(string str_IdPrograma, string str_IdEntidadCP, string str_Denominacion, string str_NomPrograma, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tPrograma ltro_Programa = new tPrograma();

                bool_ResCreacion = ltro_Programa.CrearPrograma(str_IdPrograma, str_IdEntidadCP, str_Denominacion, str_NomPrograma, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarProgramas(string str_IdPrograma, string str_IdEntidadCP, string str_Denominacion, string str_NomPrograma)
        {
            DataSet lds_Programas = new DataSet();
            try
            {
                tPrograma tus_Programa = new tPrograma();
                lds_Programas = tus_Programa.ConsultarProgramas(str_IdPrograma, str_IdEntidadCP, str_Denominacion, str_NomPrograma);
            }
            catch
            { }
            return lds_Programas;
        }
        #endregion Programa

        #region SociedadCosto
        public bool uwsCrearSociedadCosto(string str_IdSociedadCo, string str_NomSociedad, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tSociedadCosto ltro_SociedadCosto = new tSociedadCosto();

                bool_ResCreacion = ltro_SociedadCosto.CrearSociedadCosto(str_IdSociedadCo, str_NomSociedad, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarSociedadesCosto(string str_IdSociedadCosto, string str_NomSociedadCosto)
        {
            DataSet lds_SociedadesCosto = new DataSet();
            try
            {
                tSociedadCosto tus_SociedadCosto = new tSociedadCosto();
                lds_SociedadesCosto = tus_SociedadCosto.ConsultarSociedadesCosto(str_IdSociedadCosto, str_NomSociedadCosto);
            }
            catch
            { }
            return lds_SociedadesCosto;
        }
        #endregion SociedadCosto

        #region SociedadFinanciera
        public bool uwsCrearSociedadFinanciera(string str_IdSociedadFi, string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_Poblacion, string str_IdMoneda, string str_IdIdioma, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tSociedadFinanciera ltro_SociedadFinanciera = new tSociedadFinanciera();

                bool_ResCreacion = ltro_SociedadFinanciera.CrearSociedadFinanciera(str_IdSociedadFi, str_IdSociedadGL, str_Denominacion, str_NomSociedad, str_IdPais, str_Poblacion, str_IdMoneda, str_IdIdioma, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarSociedadesFinancieras(string str_IdSociedadFi, string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_IdMoneda)
        {
            DataSet lds_SociedadesFinancieras = new DataSet();
            try
            {
                tSociedadFinanciera tus_SociedadFinanciera = new tSociedadFinanciera();
                lds_SociedadesFinancieras = tus_SociedadFinanciera.ConsultarSociedadesFinancieras(str_IdSociedadFi, str_IdSociedadGL, str_Denominacion, str_NomSociedad, str_IdPais, str_IdMoneda);
            }
            catch
            { }
            return lds_SociedadesFinancieras;
        }
        #endregion SociedadFinanciera

        #region SociedadGL
        [WebMethod]
        public bool uwsCrearSociedadGL(string str_IdSociedadGL, string str_NomSociedad, string str_IdPais, string str_Poblacion, string str_Calle, string str_IdMoneda, string str_IdIdioma, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tSociedadGL ltro_SociedadGL = new tSociedadGL();

                bool_ResCreacion = ltro_SociedadGL.CrearSociedadGL(str_IdSociedadGL, str_NomSociedad, str_IdPais, str_Poblacion, str_Calle, str_IdMoneda, str_IdIdioma, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public bool uwsModificarSociedadGL(string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_Poblacion, string str_Calle, string str_IdMoneda, string str_IdIdioma, string str_CorreoNotifica, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResModificacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tSociedadGL ltro_SociedadGL = new tSociedadGL();

                bool_ResModificacion = ltro_SociedadGL.ModificarSociedadGL(str_IdSociedadGL, str_Denominacion, str_NomSociedad, str_IdPais, str_Poblacion, str_Calle, str_IdMoneda, str_IdIdioma, str_CorreoNotifica, str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResModificacion;
        }


        [WebMethod]
        public DataSet uwsConsultarSociedadesGL(string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_IdMoneda)
        {
            DataSet lds_SociedadesGL = new DataSet();
            try
            {
                tSociedadGL tus_SociedadGL = new tSociedadGL();
                lds_SociedadesGL = tus_SociedadGL.ConsultarSociedadesGL(str_IdSociedadGL, str_NomSociedad, str_IdPais, str_IdMoneda);
            }
            catch
            { }
            return lds_SociedadesGL;
        }
        #endregion SociedadGL

        #region SociedadesGLSociedadesFi
        [WebMethod]
        public DataSet uwsConsultarSociedadesGLSociedadesFi(string str_IdSociedadGL, string str_IdModulo, string str_IdSociedadFi)
        {
            DataSet lds_SociedadesGLSociedadesFi = new DataSet();
            try
            {
                tSociedadGL tus_SociedadGL = new tSociedadGL();
                lds_SociedadesGLSociedadesFi = tus_SociedadGL.ConsultarSociedadesGLSociedadesFi(str_IdSociedadGL, str_IdModulo, str_IdSociedadFi);
            }
            catch
            { }
            return lds_SociedadesGLSociedadesFi;
        }

        [WebMethod]
        public bool uwsCrearSociedadGlSociedadFi(string str_IdSociedadGL, string str_IdModulo, string str_IdSociedadFi, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tSociedadGL ltro_SociedadGL = new tSociedadGL();

                bool_ResCreacion = ltro_SociedadGL.CrearSociedadGLSociedadFi(str_IdSociedadGL, str_IdModulo, str_IdSociedadFi, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public bool uwsModificarSociedadGlSociedadFi(string str_IdSociedadGL, string str_IdModulo, string str_IdSociedadFi, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResModifica = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tSociedadGL ltro_SociedadGL = new tSociedadGL();

                bool_ResModifica = ltro_SociedadGL.ModificarSociedadGLSociedadFi(str_IdSociedadGL, str_IdModulo, str_IdSociedadFi, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResModifica;
        }
        #endregion

        #region UnidadConsolidacion
        [WebMethod]
        public bool uwsCrearUnidadConsolidacion(string str_Vista, string str_IdUnidadConsolidacion, string str_NomCorto, string str_NomUnidad, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                tUnidadConsolidacion ltro_UnidadConsolidacion = new tUnidadConsolidacion();

                bool_ResCreacion = ltro_UnidadConsolidacion.CrearUnidadConsolidacion(str_Vista, str_IdUnidadConsolidacion, str_NomCorto, str_NomUnidad, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                Log.Info(str_Mensaje);
            }
            catch (Exception ex)
            { Log.Error(ex.ToString()); }
            return bool_ResCreacion;
        }

        [WebMethod]
        public DataSet uwsConsultarUnidadesConsolidacion(string str_Vista, string str_IdUnidadConsolidacion, string str_NomCorto, string str_NomUnidad)
        {
            DataSet lds_UnidadesConsolidacion = new DataSet();
            try
            {
                tUnidadConsolidacion tus_UnidadConsolidacion = new tUnidadConsolidacion();
                lds_UnidadesConsolidacion = tus_UnidadConsolidacion.ConsultarUnidadesConsolidacion(str_Vista, str_IdUnidadConsolidacion, str_NomCorto, str_NomUnidad);
            }
            catch
            { }
            return lds_UnidadesConsolidacion;
        }
        #endregion UnidadConsolidacion

        #region Acreedores
        [WebMethod]
        public DataSet uwsConsultarAcreedores(Nullable<Int32> int_NumAcreedor, string str_NomAcreedor)
        {
            DataSet lds_Acreedores = new DataSet();
            try
            {
                clsAcreedores tus_Acreedor = new clsAcreedores();
                lds_Acreedores = tus_Acreedor.ConsultarAcreedores(int_NumAcreedor, str_NomAcreedor);
            }
            catch
            { }
            return lds_Acreedores;
        }

        [WebMethod]
        public String[] uwsCrearAcreedor(Int32 int_NumAcreedor, string str_NomAcreedor, string str_Abreviatura, string str_Contacto, string str_Telefono, string str_Direccion, string str_Pais, string str_TipoAcreedor, string str_PaisInstitucion, string str_Estado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsAcreedores ltro_Acreedor = new clsAcreedores();
                bool_ResCreacion = ltro_Acreedor.CrearAcreedor(int_NumAcreedor, str_NomAcreedor, str_Abreviatura, str_Contacto, str_Telefono, str_Direccion, str_Pais, str_TipoAcreedor, str_PaisInstitucion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarAcreedor(Int32 int_NumAcreedor, string str_NomAcreedor, string str_Abreviatura, string str_Contacto, string str_Telefono, string str_Direccion, string str_Pais, string str_TipoAcreedor, string str_PaisInstitucion, string str_IdCtaContable, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResCreacion;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsAcreedores ltro_Acreedor = new clsAcreedores();
                bool_ResModifica = ltro_Acreedor.ModificarAcreedor(int_NumAcreedor, str_NomAcreedor, str_Abreviatura, str_Contacto, str_Telefono, str_Direccion, str_Pais, str_TipoAcreedor, str_PaisInstitucion, str_IdCtaContable, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResModifica.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResModifica.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }
        #endregion Acreedores

        #region Bancos
        [WebMethod]
        public DataSet uwsConsultarBancos(string str_IdBanco = null, string str_IdBancoPropio = null, string str_IdSociedadFi = null, string str_NomBanco = null)
        {
            DataSet lds_Bancos = new DataSet();
            try
            {
                clsBancos tus_Banco = new clsBancos();
                lds_Bancos = tus_Banco.ConsultarBancos(str_IdBanco, str_IdBancoPropio, str_IdSociedadFi, str_NomBanco);
            }
            catch
            { }
            return lds_Bancos;
        }
        [WebMethod]
        public String[] uwsCrearBanco(string str_IdBanco, string str_IdBancoPropio, string str_IdSociedadFi, string str_NomBanco, string str_IdPais, string str_Telefono, string str_Contacto, string str_Estado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsBancos ltro_Banco = new clsBancos();
                bool_ResCreacion = ltro_Banco.CrearBanco(str_IdBanco, str_IdBancoPropio, str_IdSociedadFi, str_NomBanco, str_IdPais, str_Telefono, str_Contacto, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarBanco(string str_IdBanco, string str_NomBanco, string str_IdPais, string str_Telefono, string str_Contacto, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsBancos ltro_Banco = new clsBancos();
                bool_ResModifica = ltro_Banco.ModificarBanco(str_IdBanco, str_NomBanco, str_IdPais, str_Telefono, str_Contacto, str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        [WebMethod]
        public DataSet uwsConsultarBancosServicios(string str_IdBanco = null, string str_IdServicio = null, string str_IdSociedadGL = null)
        {
            DataSet lds_BancosServicios = new DataSet();
            try
            {
                clsBancos tus_Banco = new clsBancos();
                lds_BancosServicios = tus_Banco.ConsultarBancosServicios(str_IdBanco, str_IdServicio, str_IdSociedadGL);
            }
            catch
            { }
            return lds_BancosServicios;
        }

        [WebMethod]
        public String[] uwsCrearBancoServicio(string str_IdBanco, string str_IdServicio, string str_IdSociedadGL, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsBancos ltro_Banco = new clsBancos();
                bool_ResCreacion = ltro_Banco.CrearBancoServicio(str_IdBanco, str_IdServicio, str_IdSociedadGL, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsBorrarBancoServicio(string str_IdBanco, string str_IdServicio, string str_IdSociedadGL)
        {
            String[] arr_ResBorrado;
            bool bool_ResBorrado = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsBancos ltro_Banco = new clsBancos();
                bool_ResBorrado = ltro_Banco.BorrarBancoServicio(str_IdBanco, str_IdServicio, str_IdSociedadGL, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResBorrado = new String[2];
                arr_ResBorrado[0] = bool_ResBorrado.ToString();
                arr_ResBorrado[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResBorrado = new String[2];
                arr_ResBorrado[0] = bool_ResBorrado.ToString();
                arr_ResBorrado[1] = ex.ToString();

            }
            return arr_ResBorrado;
        }

        [WebMethod]
        public DataSet uwsConsultarBancosCuentas(string str_IdBanco = null, string str_IdBancoPropio = null, string str_IdCuentaBancaria = null, string str_CuentaBancaria = null, string str_IdCuentaContable = null, string str_IdSociedadFi = null)
        {
            DataSet lds_BancosCuentas = new DataSet();
            try
            {
                clsBancos tus_Banco = new clsBancos();
                lds_BancosCuentas = tus_Banco.ConsultarBancosCuentas(str_IdBanco, str_IdBancoPropio, str_IdCuentaBancaria, str_CuentaBancaria, str_IdCuentaContable, str_IdSociedadFi);
            }
            catch
            { }
            return lds_BancosCuentas;
        }

        [WebMethod]
        public String[] uwsCrearBancoCuenta(string str_IdBanco, string str_IdBancoPropio, string str_IdCuentaBancaria, string str_CuentaBancaria, string str_IdCuentaContable, string str_IdSociedadGL, string str_TipoCuenta, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsBancos ltro_Banco = new clsBancos();
                bool_ResCreacion = ltro_Banco.CrearBancoCuenta(str_IdBanco, str_IdBancoPropio, str_IdCuentaBancaria, str_CuentaBancaria, str_IdCuentaContable, str_IdSociedadGL, str_TipoCuenta, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarBancoCuenta(string str_IdBanco, string str_IdCuentaBancaria, string str_IdCuentaContable, string str_IdSociedadGL, string str_TipoCuenta, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModificado;
            bool bool_ResModificado = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsBancos ltro_Banco = new clsBancos();
                bool_ResModificado = ltro_Banco.ModificarBancoCuenta(str_IdBanco, str_IdCuentaBancaria, str_IdCuentaContable, str_IdSociedadGL, str_TipoCuenta, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModificado = new String[2];
                arr_ResModificado[0] = bool_ResModificado.ToString();
                arr_ResModificado[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModificado = new String[2];
                arr_ResModificado[0] = bool_ResModificado.ToString();
                arr_ResModificado[1] = ex.ToString();

            }
            return arr_ResModificado;
        }
        #endregion Bancos

        #region CodigoSegmento
        [WebMethod]
        public string uwsCrearActualizarCodSegmento(string pAccion, string pIdEntidad, string pIdCodSegmento, string pIdUsuario)
        {
            string respuesta = string.Empty;
            try
            {
                LogicaNegocio.Mantenimiento.clsCodigoSegmento objCodSeg = new LogicaNegocio.Mantenimiento.clsCodigoSegmento();
                respuesta = objCodSeg.CrearActualizarCodSegmento(pAccion, pIdEntidad, pIdCodSegmento, pIdUsuario);
            }
            catch(Exception e)
            {
                return "Error en la ejecución del servicio - " + e.Message.ToString();
            }
            return respuesta;
        }

        [WebMethod]
        public DataSet uwsGetCodigosSegmento(string pIdEntidad, string pBuscar)
        {
            DataSet respuesta = null;
            try
            {
                LogicaNegocio.Mantenimiento.clsCodigoSegmento objCodSeg = new LogicaNegocio.Mantenimiento.clsCodigoSegmento();
                respuesta = objCodSeg.GetCodigosSegmento(pIdEntidad,pBuscar);
            }
            catch (Exception e)
            {
                string err = e.Message.ToString();
            }
            return respuesta;
        }

        #endregion CodigoSegmento

        #region Catalogos
        [WebMethod]
        public DataSet uwsConsultarCatalogos(Nullable<int> int_IdCatalogo, string str_AbrevCatalogo, string str_NomCatalogo, string str_IdModulo)
        {
            DataSet lds_Catalogos = new DataSet();
            try
            {
                clsCatalogosGenerales tus_Catalogo = new clsCatalogosGenerales();
                lds_Catalogos = tus_Catalogo.ConsultarCatalogos(int_IdCatalogo, str_AbrevCatalogo, str_NomCatalogo, str_IdModulo);
            }
            catch
            { }
            return lds_Catalogos;
        }

        [WebMethod]
        public String[] uwsCrearCatalogo(string str_AbrevCatalogo, string str_NomCatalogo, string str_IdModulo, string str_Estado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsCatalogosGenerales ltro_Catalogo = new clsCatalogosGenerales();
                bool_ResCreacion = ltro_Catalogo.CrearCatalogo(str_AbrevCatalogo, str_NomCatalogo, str_IdModulo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarCatalogo(Int32 int_IdCatalogo, string str_AbrevCatalogo, string str_NomCatalogo, string str_IdModulo, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsCatalogosGenerales ltro_Catalogo = new clsCatalogosGenerales();
                bool_ResModifica = ltro_Catalogo.ModificarCatalogo(int_IdCatalogo, str_AbrevCatalogo, str_NomCatalogo, str_IdModulo, str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion Catalogos

        #region OpcionesCatalogo
        [WebMethod]
        public DataSet uwsConsultarOpcionesCatalogo(string str_IdCatalogo, string str_AbrevCatalogo, string str_IdOpcion, string str_NomOpcion)
        {
            DataSet lds_OpcionesCatalogo = new DataSet();
            try
            {
                int int_IdCatalogo;
                int int_IdOpcion;
                if (string.IsNullOrEmpty(str_IdCatalogo))
                {
                    int_IdCatalogo = 0;
                }
                else
                {
                    int_IdCatalogo = Convert.ToInt32(str_IdCatalogo);
                }
                if (string.IsNullOrEmpty(str_IdOpcion))
                {
                    int_IdOpcion = 0;
                }
                else
                {
                    int_IdOpcion = Convert.ToInt32(str_IdOpcion);
                }
                clsOpcionesCatalogo tus_Catalogo = new clsOpcionesCatalogo();
                lds_OpcionesCatalogo = tus_Catalogo.ConsultarOpcionesCatalogo(int_IdCatalogo, str_AbrevCatalogo, int_IdOpcion, str_NomOpcion);
            }
            catch
            { }
            return lds_OpcionesCatalogo;
        }
        [WebMethod]
        public String[] uwsCrearOpcionCatalogo(Int32 int_IdCatalogo, Int32 int_IdOpcion, string str_ValOpcion, string str_NomOpcion, string str_Estado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsOpcionesCatalogo ltro_Catalogo = new clsOpcionesCatalogo();
                bool_ResCreacion = ltro_Catalogo.CrearOpcionCatalogo(int_IdCatalogo, int_IdOpcion, str_ValOpcion, str_NomOpcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarOpcionCatalogo(Int32 int_IdCatalogo, Int32 int_IdOpcion, string str_ValOpcion, string str_NomOpcion, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsOpcionesCatalogo ltro_Catalogo = new clsOpcionesCatalogo();
                bool_ResModifica = ltro_Catalogo.ModificarOpcionCatalogo(int_IdCatalogo, int_IdOpcion, str_ValOpcion, str_NomOpcion, str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion OpcionesCatalogo

        #region Direccion
        [WebMethod]
        public DataSet uwsConsultarDirecciones(string str_IdDireccion, string str_IdSociedadGL, string str_NomDireccion)
        {
            DataSet lds_Direcciones = new DataSet();
            try
            {
                clsDirecciones tus_Direccion = new clsDirecciones();
                lds_Direcciones = tus_Direccion.ConsultarDirecciones(str_IdDireccion, str_IdSociedadGL, str_NomDireccion);
            }
            catch
            { }
            return lds_Direcciones;
        }
        [WebMethod]
        public String[] uwsCrearDireccion(string str_IdDireccion, string str_IdSociedadGL, string str_NomDireccion, string str_Estado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsDirecciones ltro_Direccion = new clsDirecciones();
                bool_ResCreacion = ltro_Direccion.CrearDireccion(str_IdDireccion, str_IdSociedadGL, str_NomDireccion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarDireccion(string str_IdDireccion, string str_IdSociedadGL, string str_NomDireccion, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsDirecciones ltro_Direccion = new clsDirecciones();
                bool_ResModifica = ltro_Direccion.ModificarDireccion(str_IdDireccion, str_IdSociedadGL, str_NomDireccion, str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion Direccion

        #region IndicadoresEconomicos
        [WebMethod]
        public DataSet uwsConsultarIndicadoresEconomicos(string str_IdIndicadorEco, string str_Transaccion, string str_NomIndicadorEco)
        {
            DataSet lds_IndicadoresEconomicos = new DataSet();
            try
            {
                clsIndicadoresEconomicos tus_IndicadorEco = new clsIndicadoresEconomicos();
                lds_IndicadoresEconomicos = tus_IndicadorEco.ConsultarIndicadoresEconomicos(str_IdIndicadorEco, str_Transaccion, str_NomIndicadorEco);
            }
            catch
            { }
            return lds_IndicadoresEconomicos;
        }
        [WebMethod]
        public String[] uwsCrearIndicadorEconomico(string str_IdIndicadorEco, string str_Transaccion, string str_NomIndicadorEco, string str_Estado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsIndicadoresEconomicos ltro_IndicadorEco = new clsIndicadoresEconomicos();
                bool_ResCreacion = ltro_IndicadorEco.CrearIndicadorEconomico(str_IdIndicadorEco, str_Transaccion, str_NomIndicadorEco, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarIndicadorEconomico(string str_IdIndicadorEco, string str_Transaccion, string str_NomIndicadorEco, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsIndicadoresEconomicos ltro_IndicadorEco = new clsIndicadoresEconomicos();
                bool_ResModifica = ltro_IndicadorEco.ModificarIndicadorEconomico(str_IdIndicadorEco, str_Transaccion, str_NomIndicadorEco, str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion IndicadoresEconomicos

        #region Nemotecnico
        [WebMethod]
        public DataSet uwsConsultarNemotecnicos(string str_IdNemotecnico, string str_IdSociedadFi, string str_NomNemotecnico, string str_IdMoneda, string str_TipoNemotecnico)
        {
            DataSet lds_Nemotecnicos = new DataSet();
            try
            {
                clsNemotecnicos tus_Nemotecnico = new clsNemotecnicos();
                lds_Nemotecnicos = tus_Nemotecnico.ConsultarNemotecnicos(str_IdNemotecnico, str_IdSociedadFi, str_NomNemotecnico, str_IdMoneda, str_TipoNemotecnico);
            }
            catch
            { }
            return lds_Nemotecnicos;
        }
        [WebMethod]
        public String[] uwsCrearNemotecnico(string str_IdNemotecnico, string str_IdSociedadFi, string str_NomNemotecnico, string str_IdMoneda, string str_TipoNemotecnico, string str_IdTasa, string str_ModuloSINPE, string str_IdCuentaContableCP, string str_IdCuentaContableLP, string str_Estado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsNemotecnicos ltro_Nemotecnico = new clsNemotecnicos();
                bool_ResCreacion = ltro_Nemotecnico.CrearNemotecnico(str_IdNemotecnico, str_IdSociedadFi, str_NomNemotecnico, str_IdMoneda, str_TipoNemotecnico, str_IdTasa, str_ModuloSINPE, str_IdCuentaContableCP, str_IdCuentaContableLP, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarNemotecnico(string str_IdNemotecnico, string str_IdSociedadFi, string str_NomNemotecnico, string str_IdMoneda, string str_TipoNemotecnico, string str_IdTasa, string str_ModuloSINPE, string str_IdCuentaContableCP, string str_IdCuentaContableLP, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsNemotecnicos ltro_Nemotecnico = new clsNemotecnicos();
                bool_ResModifica = ltro_Nemotecnico.ModificarNemotecnico(str_IdNemotecnico, str_IdSociedadFi, str_NomNemotecnico, str_IdMoneda, str_TipoNemotecnico, str_IdTasa, str_ModuloSINPE, str_IdCuentaContableCP, str_IdCuentaContableLP, str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion Nemotecnico

        #region Oficina
        [WebMethod]
        public DataSet uwsConsultarOficinas(string str_IdOficina, string str_IdSociedadGL, string str_IdDireccion, string str_NomOficina)
        {
            DataSet lds_Oficinas = new DataSet();
            try
            {
                clsOficinas tus_Oficina = new clsOficinas();
                lds_Oficinas = tus_Oficina.ConsultarOficinas(str_IdOficina, str_IdSociedadGL, str_IdDireccion, str_NomOficina);
            }
            catch
            { }
            return lds_Oficinas;
        }
        [WebMethod]
        public String[] uwsCrearOficina(string str_IdOficina, string str_IdSociedadGL, string str_NomOficina, string str_IdDireccion, string str_Estado, string str_UsrCreacion, string str_CorreoNotifica, char str_UsaExpediente)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsOficinas ltro_Oficina = new clsOficinas();
                bool_ResCreacion = ltro_Oficina.CrearOficina(str_IdOficina, str_IdSociedadGL, str_NomOficina, str_IdDireccion, str_Estado, str_UsrCreacion, str_CorreoNotifica, str_UsaExpediente, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarOficina(string str_IdOficina, string str_IdSociedadGL, string str_NomOficina, string str_IdDireccion, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, string str_CorreoNotifica, char str_UsaExpediente)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsOficinas ltro_Oficina = new clsOficinas();
                bool_ResModifica = ltro_Oficina.ModificarOficina(str_IdOficina, str_IdSociedadGL, str_NomOficina, str_IdDireccion, str_Estado, str_UsrModifica, dt_FchModifica, str_CorreoNotifica, str_UsaExpediente, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion Oficina

        #region OficinaCeBe
        [WebMethod]
        public DataSet uwsConsultarOficinasCeBes(string str_IdOficina, string str_IdSociedadGL, string str_IdModulo, string str_IdCentroBeneficio)
        {
            DataSet lds_Oficinas = new DataSet();
            try
            {
                clsOficinas tus_Oficina = new clsOficinas();
                lds_Oficinas = tus_Oficina.ConsultarOficinasCeBe(str_IdOficina, str_IdSociedadGL, str_IdModulo, str_IdCentroBeneficio);
            }
            catch
            { }
            return lds_Oficinas;
        }
        [WebMethod]
        public String[] uwsCrearOficinaCeBe(string str_IdOficina, string str_IdSociedadGL, string str_IdModulo, string str_IdCentroBeneficio, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsOficinas ltro_Oficina = new clsOficinas();
                bool_ResCreacion = ltro_Oficina.CrearOficinaCeBe(str_IdOficina, str_IdSociedadGL, str_IdModulo, str_IdCentroBeneficio, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsBorrarOficinaCeBe(string str_IdOficina, string str_IdSociedadGL, string str_IdModulo, string str_IdCentroBeneficio)
        {
            String[] arr_ResBorra;
            bool bool_ResBorra = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsOficinas ltro_Oficina = new clsOficinas();
                bool_ResBorra = ltro_Oficina.BorrarOficinaCeBe(str_IdOficina, str_IdSociedadGL, str_IdModulo, str_IdCentroBeneficio, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResBorra = new String[2];
                arr_ResBorra[0] = bool_ResBorra.ToString();
                arr_ResBorra[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResBorra = new String[2];
                arr_ResBorra[0] = bool_ResBorra.ToString();
                arr_ResBorra[1] = ex.ToString();

            }
            return arr_ResBorra;
        }
        #endregion OficinaCeBe

        #region Operacion
        [WebMethod]
        public DataSet uwsConsultarOperaciones(string str_IdOperacion, string str_IdModulo, string str_NomOperacion)
        {
            DataSet lds_Operaciones = new DataSet();
            try
            {
                clsOperaciones tus_Operacion = new clsOperaciones();
                lds_Operaciones = tus_Operacion.ConsultarOperaciones(str_IdOperacion, str_IdModulo, str_NomOperacion);
            }
            catch
            { }
            return lds_Operaciones;
        }
        [WebMethod]
        public String[] uwsCrearOperacion(string str_IdOperacion, string str_IdModulo, string str_NomOperacion, string str_IdClaseDoc, string str_Estado, string str_IdOperacionReversa, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsOperaciones ltro_Operacion = new clsOperaciones();
                bool_ResCreacion = ltro_Operacion.CrearOperacion(str_IdOperacion, str_IdModulo, str_NomOperacion, str_IdClaseDoc, str_Estado, str_IdOperacionReversa, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarOperacion(string str_IdOperacion, string str_IdModulo, string str_NomOperacion, string str_IdClaseDoc, string str_Estado, string str_IdOperacionReversa, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsOperaciones ltro_Operacion = new clsOperaciones();
                bool_ResModifica = ltro_Operacion.ModificarOperacion(str_IdOperacion, str_IdModulo, str_NomOperacion, str_IdClaseDoc, str_Estado, str_IdOperacionReversa, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion Operacion

        #region Pais
        [WebMethod]
        public DataSet uwsConsultarPaises(string str_IdPais, string str_NomPais)
        {
            DataSet lds_Paises = new DataSet();
            try
            {
                clsPaises tus_Pais = new clsPaises();
                lds_Paises = tus_Pais.ConsultarPaises(str_IdPais, str_NomPais);
            }
            catch
            { }
            return lds_Paises;
        }
        [WebMethod]
        public String[] uwsCrearPais(string str_IdPais, string str_NomPais, string str_Nacionalidad, string str_IdMoneda, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsPaises ltro_Pais = new clsPaises();
                bool_ResCreacion = ltro_Pais.CrearPais(str_IdPais, str_NomPais, str_Nacionalidad, str_IdMoneda, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarPais(string str_IdPais, string str_NomPais, string str_Nacionalidad, string str_IdMoneda, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsPaises ltro_Pais = new clsPaises();
                bool_ResModifica = ltro_Pais.ModificarPais(str_IdPais, str_NomPais, str_Nacionalidad, str_IdMoneda, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion Pais

        #region PrevisionIncobrable
        [WebMethod]
        public DataSet uwsConsultarPrevisionesIncobrables(Nullable<int> int_DiasMorosidad, Nullable<Decimal> dec_PorcEstimacion, string str_Descripcion)
        {
            DataSet lds_PrevisionesIncobrables = new DataSet();
            try
            {
                clsPrevisionesIncobrables tus_PrevisionIncobrable = new clsPrevisionesIncobrables();
                lds_PrevisionesIncobrables = tus_PrevisionIncobrable.ConsultarPrevisionesIncobrables(int_DiasMorosidad, dec_PorcEstimacion, str_Descripcion);
            }
            catch
            { }
            return lds_PrevisionesIncobrables;
        }
        [WebMethod]
        public String[] uwsCrearPrevisionIncobrable(Int32 int_DiasMorosidad, Decimal dec_PorcEstimacion, string str_Descripcion, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsPrevisionesIncobrables ltro_PrevisionIncobrable = new clsPrevisionesIncobrables();
                bool_ResCreacion = ltro_PrevisionIncobrable.CrearPrevisionIncobrable(int_DiasMorosidad, dec_PorcEstimacion, str_Descripcion, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarPrevisionIncobrable(Int32 int_DiasMorosidad, Decimal dec_PorcEstimacion, string str_Descripcion, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsPrevisionesIncobrables ltro_PrevisionIncobrable = new clsPrevisionesIncobrables();
                bool_ResModifica = ltro_PrevisionIncobrable.ModificarPrevisionIncobrable(int_DiasMorosidad, dec_PorcEstimacion, str_Descripcion, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion PrevisionIncobrable

        #region Propietario
        [WebMethod]
        public DataSet uwsConsultarPropietarios(string str_IdPropietario, string str_IdSociedadGL, string str_IdSociedadFi, string str_NomPropietario)
        {
            DataSet lds_Propietarios = new DataSet();
            try
            {
                clsPropietarios tus_Propietario = new clsPropietarios();
                lds_Propietarios = tus_Propietario.ConsultarPropietarios(str_IdPropietario, str_IdSociedadGL, str_IdSociedadFi, str_NomPropietario);
            }
            catch
            { }
            return lds_Propietarios;
        }
        [WebMethod]
        public String[] uwsCrearPropietario(string str_IdPropietario, string str_IdSociedadGL, string str_IdSociedadFi, string str_NomPropietario, string str_Estado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsPropietarios ltro_Propietario = new clsPropietarios();
                bool_ResCreacion = ltro_Propietario.CrearPropietario(str_IdPropietario, str_IdSociedadGL, str_IdSociedadFi, str_NomPropietario, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarPropietario(string str_IdPropietario, string str_IdSociedadGL, string str_IdSociedadFi, string str_NomPropietario, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsPropietarios ltro_Propietario = new clsPropietarios();
                bool_ResModifica = ltro_Propietario.ModificarPropietario(str_IdPropietario, str_IdSociedadGL, str_IdSociedadFi, str_NomPropietario, str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion Propietario

        #region Servicio
        [WebMethod]
        public DataSet uwsConsultarServicios(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, string str_NomServicio, string str_IdCuentaContable, string str_IdPosPre)
        {
            DataSet lds_Servicios = new DataSet();
            try
            {
                clsServicios tus_Servicio = new clsServicios();
                lds_Servicios = tus_Servicio.ConsultarServicios(str_IdServicio, str_IdSociedadGL, str_IdOficina, str_NomServicio, str_IdCuentaContable, str_IdPosPre);
            }
            catch
            { }
            return lds_Servicios;
        }
        [WebMethod]
        public String[] uwsCrearServicio(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, string str_NomServicio, Nullable<decimal> dec_Monto, string str_PermiteReserva,
            string str_CtaContableDebeActualDev, string str_CtaContableHaberActualDev, string str_IdPosPreActualDev,
            string str_CtaContableDebeActualPer, string str_CtaContableHaberActualPer, string str_IdPosPreActualPer,
            string str_CtaContableDebeVencidoDev, string str_CtaContableHaberVencidoDev, string str_IdPosPreVencidoDev,
            string str_CtaContableDebeVencidoPer, string str_CtaContableHaberVencidoPer, string str_IdPosPreVencidoPer,
            string str_Estado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsServicios ltro_Servicio = new clsServicios();
                bool_ResCreacion = ltro_Servicio.CrearServicio(str_IdServicio, str_IdSociedadGL, str_IdOficina, str_NomServicio, dec_Monto, str_PermiteReserva,
                    str_CtaContableDebeActualDev, str_CtaContableHaberActualDev, str_IdPosPreActualDev,
                    str_CtaContableDebeActualPer, str_CtaContableHaberActualPer, str_IdPosPreActualPer,
                    str_CtaContableDebeVencidoDev, str_CtaContableHaberVencidoDev, str_IdPosPreVencidoDev,
                    str_CtaContableDebeVencidoPer, str_CtaContableHaberVencidoPer, str_IdPosPreVencidoPer,
                    str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarServicio(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, string str_NomServicio, string dec_Monto, string str_PermiteReserva,
            string str_CtaContableDebeActualDev, string str_CtaContableHaberActualDev, string str_IdPosPreActualDev,
            string str_CtaContableDebeActualPer, string str_CtaContableHaberActualPer, string str_IdPosPreActualPer,
            string str_CtaContableDebeVencidoDev, string str_CtaContableHaberVencidoDev, string str_IdPosPreVencidoDev,
            string str_CtaContableDebeVencidoPer, string str_CtaContableHaberVencidoPer, string str_IdPosPreVencidoPer,
            string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica = new String[2];
            bool bool_ResModifica = true;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            decimal? Monto = null;
            try
            {
                try
                {

                    if (!string.IsNullOrEmpty(dec_Monto))
                        Monto = Convert.ToDecimal(dec_Monto);
                }
                catch (Exception ex)
                {
                    arr_ResModifica[0] = "-1";
                    arr_ResModifica[1] = "Formato Incorrecto decimal";
                    bool_ResModifica = false;
                }
                if (bool_ResModifica)
                {
                    clsServicios ltro_Servicio = new clsServicios();
                    bool_ResModifica = ltro_Servicio.ModificarServicio(str_IdServicio, str_IdSociedadGL, str_IdOficina, str_NomServicio, Monto, str_PermiteReserva,
                        str_CtaContableDebeActualDev, str_CtaContableHaberActualDev, str_IdPosPreActualDev,
                        str_CtaContableDebeActualPer, str_CtaContableHaberActualPer, str_IdPosPreActualPer,
                        str_CtaContableDebeVencidoDev, str_CtaContableHaberVencidoDev, str_IdPosPreVencidoDev,
                        str_CtaContableDebeVencidoPer, str_CtaContableHaberVencidoPer, str_IdPosPreVencidoPer,
                        str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                    Log.Info(str_Mensaje);

                    arr_ResModifica[0] = bool_ResModifica.ToString();
                    arr_ResModifica[1] = str_Mensaje;
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }

        [WebMethod]
        public DataSet uwsConsultarServiciosOficinas(string str_IdServicio = null, string str_IdSociedadGL = null, string str_IdOficina = null)
        {
            DataSet lds_ServiciosOficinas = new DataSet();
            try
            {
                clsServicios tus_Servicios = new clsServicios();
                lds_ServiciosOficinas = tus_Servicios.ConsultarServiciosOficinas(str_IdServicio, str_IdSociedadGL, str_IdOficina);
            }
            catch
            { }
            return lds_ServiciosOficinas;
        }

        [WebMethod]
        public String[] uwsCrearServicioOficina(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsServicios ltro_Servicio = new clsServicios();
                bool_ResCreacion = ltro_Servicio.CrearServicioOficina(str_IdServicio, str_IdSociedadGL, str_IdOficina, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsBorrarServicioOficina(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina)
        {
            String[] arr_ResBorrado;
            bool bool_ResBorrado = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsServicios ltro_Servicio = new clsServicios();
                bool_ResBorrado = ltro_Servicio.BorrarServicioOficina(str_IdServicio, str_IdSociedadGL, str_IdOficina, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResBorrado = new String[2];
                arr_ResBorrado[0] = bool_ResBorrado.ToString();
                arr_ResBorrado[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResBorrado = new String[2];
                arr_ResBorrado[0] = bool_ResBorrado.ToString();
                arr_ResBorrado[1] = ex.ToString();

            }
            return arr_ResBorrado;
        }

        #endregion Servicio

        #region TipoAsiento
        [WebMethod]
        public DataSet uwsConsultarTiposAsiento(string str_Codigo, string str_IdModulo, string str_IdOperacion, string str_IdCuentaContable, string str_IdPosPre, string CodigoAuxiliar, string CodigoAuxiliar2, string str_CodigoAuxiliar3, string str_CodigoAuxiliar4)
        {
            DataSet lds_TiposAsiento = new DataSet();
            try
            {
                clsTiposAsiento tus_TipoAsiento = new clsTiposAsiento();
                lds_TiposAsiento = tus_TipoAsiento.ConsultarTiposAsiento(str_Codigo, str_IdModulo, str_IdOperacion, str_IdCuentaContable, str_IdPosPre, CodigoAuxiliar, CodigoAuxiliar2, str_CodigoAuxiliar3, str_CodigoAuxiliar4);
            }
            catch
            { }
            return lds_TiposAsiento;
        }
        [WebMethod]
        public DataSet uwsConsultarTiposAsientoDetalle(string str_Codigo, string str_IdModulo, string str_IdOperacion, string str_IdCuentaContable, string str_IdPosPre, string CodigoAuxiliar, string CodigoAuxiliar2, string str_CodigoAuxiliar3, string str_CodigoAuxiliar4, string str_CodigoAuxiliar5, string str_CodigoAuxiliar6, string int_Secuencia, string str_OrderBy, string str_Exacto)
        {
            DataSet lds_TiposAsiento = new DataSet();
            try
            {
                int? secuencia = null;
                if (!string.IsNullOrEmpty(int_Secuencia))
                    secuencia = Convert.ToInt32(int_Secuencia);
                string str_Orden = (str_OrderBy == ""|| str_OrderBy == null)? " ORDER BY Codigo, IdModulo, Secuencia":str_OrderBy;
                clsTiposAsiento tus_TipoAsiento = new clsTiposAsiento();
                lds_TiposAsiento = tus_TipoAsiento.ConsultarTiposAsiento(str_Codigo, str_IdModulo, str_IdOperacion, str_IdCuentaContable, str_IdPosPre, CodigoAuxiliar, str_CodigoAuxiliar3, str_CodigoAuxiliar6, str_CodigoAuxiliar4, str_CodigoAuxiliar5, CodigoAuxiliar2, secuencia, str_Orden, str_Exacto);
            }
            catch
            { }
            return lds_TiposAsiento;
        }
        [WebMethod]
        public String[] uwsCrearTipoAsiento(string str_IdModulo, string str_IdOperacion, string str_Codigo, string str_CodigoAuxiliar, string str_CodigoAuxiliar2, string str_CodigoAuxiliar3, string str_CodigoAuxiliar4, string str_IdClaveContable, string str_IdCuentaContable, string str_IdCentroCosto, string str_IdCentroBeneficio, string str_IdElementoPEP, string str_IdPosPre, string str_IdCentroGestor, string str_IdPrograma, string str_IdFondo, string str_DocPresupuestario, string str_PosDocPresupuestario, string str_FlujoEfectivo, string str_NICSP24,
            string str_IdClaveContable2, string str_IdCuentaContable2, string str_IdCentroCosto2, string str_IdCentroBeneficio2, string str_IdElementoPEP2, string str_IdPosPre2, string str_IdCentroGestor2, string str_IdPrograma2, string str_IdFondo2, string str_DocPresupuestario2, string str_PosDocPresupuestario2, string str_FlujoEfectivo2, string str_NICSP242, string str_Estado, string str_UsrCreacion, string str_CodigoAuxiliar5, string str_CodigoAuxiliar6, string int_Secuencia)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                int? secuencia = 1;

                if (!string.IsNullOrEmpty(int_Secuencia))
                    secuencia = Convert.ToInt32(int_Secuencia);
                clsTiposAsiento ltro_TipoAsiento = new clsTiposAsiento();
                bool_ResCreacion = ltro_TipoAsiento.CrearTipoAsiento(str_IdModulo, str_IdOperacion, str_Codigo, str_CodigoAuxiliar, str_CodigoAuxiliar2, str_CodigoAuxiliar3, str_CodigoAuxiliar4, str_IdClaveContable, str_IdCuentaContable, str_IdCentroCosto, str_IdCentroBeneficio, str_IdElementoPEP, str_IdPosPre, str_IdCentroGestor, str_IdPrograma, str_IdFondo, str_DocPresupuestario, str_PosDocPresupuestario, str_FlujoEfectivo, str_NICSP24,
            str_IdClaveContable2, str_IdCuentaContable2, str_IdCentroCosto2, str_IdCentroBeneficio2, str_IdElementoPEP2, str_IdPosPre2, str_IdCentroGestor2, str_IdPrograma2, str_IdFondo2, str_DocPresupuestario2, str_PosDocPresupuestario2, str_FlujoEfectivo2, str_NICSP242, str_Estado, str_UsrCreacion, str_CodigoAuxiliar5, str_CodigoAuxiliar6, secuencia, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarTipoAsiento(string str_IdModulo, string str_IdOperacion, string str_Codigo, string str_CodigoAuxiliar, string str_CodigoAuxiliar2, string str_CodigoAuxiliar3, string str_CodigoAuxiliar4, string str_IdClaveContable, string str_IdCuentaContable, string str_IdCentroCosto, string str_IdCentroBeneficio, string str_IdElementoPEP, string str_IdPosPre, string str_IdCentroGestor, string str_IdPrograma, string str_IdFondo, string str_DocPresupuestario, string str_PosDocPresupuestario, string str_FlujoEfectivo, string str_NICSP24,
            string str_IdClaveContable2, string str_IdCuentaContable2, string str_IdCentroCosto2, string str_IdCentroBeneficio2, string str_IdElementoPEP2, string str_IdPosPre2, string str_IdCentroGestor2, string str_IdPrograma2, string str_IdFondo2, string str_DocPresupuestario2, string str_PosDocPresupuestario2, string str_FlujoEfectivo2, string str_NICSP242, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, string str_CodigoAuxiliar5, string str_CodigoAuxiliar6, string int_Secuencia)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                int? secuencia = 1;

                if (!string.IsNullOrEmpty(int_Secuencia))
                    secuencia = Convert.ToInt32(int_Secuencia);

                clsTiposAsiento ltro_TipoAsiento = new clsTiposAsiento();
                bool_ResModifica = ltro_TipoAsiento.ModificarTipoAsiento(str_IdModulo, str_IdOperacion, str_Codigo, str_CodigoAuxiliar, str_CodigoAuxiliar2, str_CodigoAuxiliar3, str_CodigoAuxiliar4, str_IdClaveContable, str_IdCuentaContable, str_IdCentroCosto, str_IdCentroBeneficio, str_IdElementoPEP, str_IdPosPre, str_IdCentroGestor, str_IdPrograma, str_IdFondo, str_DocPresupuestario, str_PosDocPresupuestario, str_FlujoEfectivo, str_NICSP24,
            str_IdClaveContable2, str_IdCuentaContable2, str_IdCentroCosto2, str_IdCentroBeneficio2, str_IdElementoPEP2, str_IdPosPre2, str_IdCentroGestor2, str_IdPrograma2, str_IdFondo2, str_DocPresupuestario2, str_PosDocPresupuestario2, str_FlujoEfectivo2, str_NICSP242, str_Estado, str_UsrModifica, dt_FchModifica, str_CodigoAuxiliar5, str_CodigoAuxiliar6, secuencia, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }

        [WebMethod]
        public String[] uwsEliminarTipoAsiento(string str_IdModulo, string str_IdOperacion, string str_Codigo,
            string str_CodigoAuxiliar, string str_CodigoAuxiliar2, string str_CodigoAuxiliar3, string str_CodigoAuxiliar4,
            string str_CodigoAuxiliar5, string str_CodigoAuxiliar6, string int_Secuencia)
        {
            String[] arr_ResElimina;
            bool bool_ResElimina = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;

            try
            {
                int? secuencia = 1;

                if (!string.IsNullOrEmpty(int_Secuencia))
                    secuencia = Convert.ToInt32(int_Secuencia);

                clsTiposAsiento ltro_TipoAsiento = new clsTiposAsiento();
                bool_ResElimina = ltro_TipoAsiento.EliminarTipoAsiento(str_IdModulo, str_IdOperacion, str_Codigo,
            str_CodigoAuxiliar, str_CodigoAuxiliar2, str_CodigoAuxiliar3, str_CodigoAuxiliar4,
            str_CodigoAuxiliar5, str_CodigoAuxiliar6, secuencia, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResElimina = new String[2];
                arr_ResElimina[0] = bool_ResElimina.ToString();
                arr_ResElimina[1] = str_Mensaje;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResElimina = new String[2];
                arr_ResElimina[0] = bool_ResElimina.ToString();
                arr_ResElimina[1] = ex.ToString();
            }

            return arr_ResElimina;
        }
        #endregion TipoAsiento

        #region TipoCambio
        [WebMethod]
        public DataSet uwsConsultarTiposCambio(string str_IdMoneda, DateTime dt_FchReferencia, string str_TipoTransaccion, string str_ExactaFecha = "S")
        {
            DataSet lds_TiposCambio = new DataSet();
            try
            {
                clsTiposCambio tus_TipoCambio = new clsTiposCambio();
                lds_TiposCambio = tus_TipoCambio.ConsultarTiposCambio(str_IdMoneda, dt_FchReferencia, str_TipoTransaccion, str_ExactaFecha);
            }
            catch
            { }
            return lds_TiposCambio;
        }
        [WebMethod]
        public String[] uwsCrearTipoCambio(string str_IdMoneda, DateTime dt_FchReferencia, string str_TipoTransaccion, decimal dec_Valor, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsTiposCambio ltro_TipoCambio = new clsTiposCambio();
                bool_ResCreacion = ltro_TipoCambio.CrearTipoCambio(str_IdMoneda, dt_FchReferencia, str_TipoTransaccion, dec_Valor, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarTipoCambio(string str_IdMoneda, DateTime dt_FchReferencia, string str_TipoTransaccion, decimal dec_Valor, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsTiposCambio ltro_TipoCambio = new clsTiposCambio();
                bool_ResModifica = ltro_TipoCambio.ModificarTipoCambio(str_IdMoneda, dt_FchReferencia, str_TipoTransaccion, dec_Valor, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        [WebMethod]
        public string[] CargarTiposCambio(string ldt_FchInicio)
        {
            clsTiposCambio ltro_TipoCambio = new clsTiposCambio();
            return ltro_TipoCambio.CargarTiposCambio(ldt_FchInicio);
        }
        [WebMethod]
        public string[] ActualizarTiposCambio()
        {
            clsTiposCambio ltro_TipoCambio = new clsTiposCambio();
            return ltro_TipoCambio.ActualizarTiposCambio();
        }

        #endregion TipoCambio

        #region ValoresIndicadoresEco
        [WebMethod]
        public DataSet uwsConsultarValoresIndicadoresEco(string str_IdIndicadorEco, DateTime dt_FchReferencia, string str_ExactaFecha = "S")
        {
            DataSet lds_ValoresIndicadoresEco = new DataSet();
            try
            {
                clsValoresIndicadoresEco tus_ValoresIndicadoresEco = new clsValoresIndicadoresEco();
                lds_ValoresIndicadoresEco = tus_ValoresIndicadoresEco.ConsultarValoresIndicadoresEco(str_IdIndicadorEco, dt_FchReferencia, str_ExactaFecha);
            }
            catch
            { }
            return lds_ValoresIndicadoresEco;
        }
        [WebMethod]
        public String[] uwsCrearValorIndicadorEco(string str_IdIndicadorEco, DateTime dt_FchReferencia, decimal dec_Valor, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsValoresIndicadoresEco ltro_ValoresIndicadoresEco = new clsValoresIndicadoresEco();
                bool_ResCreacion = ltro_ValoresIndicadoresEco.CrearValorIndicadorEco(str_IdIndicadorEco, dt_FchReferencia, dec_Valor, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarValorIndicadorEco(string str_IdIndicadorEco, DateTime dt_FchReferencia, decimal dec_Valor, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsValoresIndicadoresEco ltro_ValoresIndicadoresEco = new clsValoresIndicadoresEco();
                bool_ResModifica = ltro_ValoresIndicadoresEco.ModificarValorIndicadorEco(str_IdIndicadorEco, dt_FchReferencia, dec_Valor, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        [WebMethod]
        public string[] CargarIndicadoresEco(string ldt_FchInicio)
        {
            clsValoresIndicadoresEco ltro_IndicadoresEco = new clsValoresIndicadoresEco();
            return ltro_IndicadoresEco.CargarIndicadoresEco(ldt_FchInicio);
        }
        [WebMethod]
        public string[] ActualizarIndicadoresEco()
        {
            clsValoresIndicadoresEco ltro_IndicadoresEco = new clsValoresIndicadoresEco();
            return ltro_IndicadoresEco.ActualizarIndicadoresEco();
        }

        #endregion ValoresIndicadoresEco

        #region Reservas
        [WebMethod]
        public DataSet uwsConsultarReservas(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdMoneda, string str_NomReserva, string str_OrderBy)
        {
            DataSet lds_TiposCambio = new DataSet();
            try
            {
                clsReservas cls_Reservas = new clsReservas();
                lds_TiposCambio = cls_Reservas.ConsultarReservas(str_IdReserva, str_IdEntidadCP, str_IdSociedadFi, str_IdMoneda, str_NomReserva, str_OrderBy);
            }
            catch
            { }
            return lds_TiposCambio;
        }

        [WebMethod]
        public DataSet uwsConsultarReservasDetallado(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdMoneda, string str_NomReserva, string str_OrderBy)
        {
            DataSet lds_TiposCambio = new DataSet();
            try
            {
                clsReservas cls_Reservas = new clsReservas();
                lds_TiposCambio = cls_Reservas.ConsultarReservasDetallado(str_IdReserva, str_IdEntidadCP, str_IdSociedadFi, str_IdMoneda, str_NomReserva, str_OrderBy);
            }
            catch
            { }
            return lds_TiposCambio;
        }

        [WebMethod]
        public String[] uwsCrearReservas(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdClaseDocPsto,
                    string str_IdMoneda, string str_NomReserva, string str_Estado, string str_Bloqueado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsReservas cls_Reservas = new clsReservas();
                bool_ResCreacion = cls_Reservas.CrearReserva(str_IdReserva, str_IdEntidadCP, str_IdSociedadFi, str_IdClaseDocPsto,
                    str_IdMoneda, str_NomReserva, str_Estado, str_Bloqueado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarReserva(string str_IdReserva, Int32 int_OrdenContingentes, Int32 int_OrdenDeudaInterna, Int32 int_OrdenDeudaExterna, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsReservas cls_Reservas = new clsReservas();
                bool_ResModifica = cls_Reservas.ModificarReserva(str_IdReserva, int_OrdenContingentes, int_OrdenDeudaInterna, int_OrdenDeudaExterna, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }
        #endregion

        #region Reservas Detalle
        [WebMethod]
        public DataSet uwsConsultarReservaDetalle(string str_IdReserva)
        {
            DataSet lds_TiposCambio = new DataSet();
            try
            {
                clsReservasDetalle cls_ReservasDetalle = new clsReservasDetalle();
                lds_TiposCambio = cls_ReservasDetalle.ConsultarReservasDetalle(str_IdReserva, null, null, null, null, null, null, null, null, null, null);
            }
            catch
            { }
            return lds_TiposCambio;
        }

        [WebMethod]
        public DataSet uwsConsultarReservaDetallado(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdMoneda,
            string str_NomReserva, string str_IdCentroGestor, string str_IdCentroCosto, string str_IdCuentaContable,
            string str_IdElementoPEP, string str_IdPosPre, string str_SoloDE, string str_SoloDI, string str_SoloCT, string str_OrderBy)
        {
            DataSet lds_TiposCambio = new DataSet();
            try
            {
                clsReservasDetalle cls_ReservasDetalle = new clsReservasDetalle();
                lds_TiposCambio = cls_ReservasDetalle.ConsultarReservasDetallado(str_IdReserva, str_IdEntidadCP, str_IdSociedadFi, str_IdMoneda,
                    str_NomReserva, str_IdCentroGestor, str_IdCentroCosto, str_IdCuentaContable,
                    str_IdElementoPEP, str_IdPosPre, str_SoloDE, str_SoloDI, str_SoloCT, str_OrderBy);
            }
            catch
            { }
            return lds_TiposCambio;
        }
        [WebMethod]
        public String[] uwsCrearReservaDetalle(string str_IdReserva, string str_Posicion, string str_Detalle, string str_IdPosPre,
                     string str_IdCentroGestor, string str_IdFondo, string str_Segmento, string str_IdPrograma, string str_IdCuentaContable, string str_IdCentroCosto,
                     string str_IdElementoPEP, string str_IdMoneda, decimal dc_Monto, string str_Estado, string str_Bloqueado, string str_UsrCreacion)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsReservasDetalle cls_ReservasDetalle = new clsReservasDetalle();
                bool_ResCreacion = cls_ReservasDetalle.CrearReservaDetalle(str_IdReserva, str_Posicion, str_Detalle, str_IdPosPre,
                    str_IdCentroGestor, str_IdFondo, str_Segmento, str_IdPrograma, str_IdCuentaContable, str_IdCentroCosto,
                    str_IdElementoPEP, str_IdMoneda, dc_Monto, str_Estado, str_Bloqueado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

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
        public String[] uwsModificarReservaDetalle(string str_IdReserva, string str_Posicion, Int32 int_OrdenContingentes, Int32 int_OrdenDeudaInterna, Int32 int_OrdenDeudaExterna, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsReservasDetalle cls_Reservas = new clsReservasDetalle();
                bool_ResModifica = cls_Reservas.ModificarReservaDetalle(str_IdReserva, str_Posicion, int_OrdenContingentes, int_OrdenDeudaInterna, int_OrdenDeudaExterna, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                Log.Info(str_Mensaje);

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
        }

        #endregion

        #endregion Mantenimiento

        #region FirmaDigital
        #endregion

        #region Seguridad
        #region Gestion de usuarios

        /// <summary>
        /// Metodo web para iniciar sesion en el sistema
        /// </summary>
        /// <param name="str_IdUsuario">Identificacion del usuario</param>
        /// <param name="str_Clave">Clave o contrasena</param>
        /// <returns>BooleAnno indicando si se tuvo exito al realizar la operacion</returns>
        [WebMethod]
        public string[] uwsLoguearUsuario(string str_IdUsuario, string str_Clave, string str_IpUsuario)
        {
            string[] lastr_Resultado = new string[8];
            string lstr_CodResultado = "99";
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lastr_Resultado = tus_Usuario.ufnLoguearUsuario(str_IdUsuario, str_Clave, str_IpUsuario, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return lastr_Resultado;
        }

        [WebMethod]
        public string[] uwsLoguearUsuarioFirma(string str_IdUsuario, string str_NomUsuario, string str_IpUsuario)
        {
            string lstr_CodResultado = "99";
            string[] lastr_Resultado = new string[8];
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lastr_Resultado = tus_Usuario.ufnLoguearUsuarioFirma(str_IdUsuario, str_NomUsuario, str_IpUsuario, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                lastr_Resultado[0] = lstr_CodResultado;
                log.Error(ex.Message);
            }
            return lastr_Resultado;
        }

        [WebMethod]
        public string[] uwsValidarUsuario(string str_IdUsuario, string str_Clave, string str_IpUsuario)
        {
            string[] lastr_Resultado = new string[8];
            string lstr_CodResultado = "99";
            string lstr_Mensaje = String.Empty;
            if ((null == str_IdUsuario) || (0 == str_IdUsuario.Length))
            {
                System.Diagnostics.Trace.WriteLine("[ValidarUsuario] Fallo la validacion del usuario.");
            }

            if ((null == str_Clave) || (0 == str_Clave.Length))
            {
                System.Diagnostics.Trace.WriteLine("[ValidarUsuario] No se puede validar la contrasena");
            }

            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lastr_Resultado = tus_Usuario.ufnLoguearUsuario(str_IdUsuario, str_Clave, str_IpUsuario, out lstr_Mensaje);
                log.Info(lstr_Mensaje);
            }
            catch (Exception ex)
            {
                lastr_Resultado[0] = lstr_CodResultado;
                lastr_Resultado[1] = ex.Message;
                System.Diagnostics.Trace.WriteLine("[ValidarUsuario] Exception " + ex.Message);
            }
            lstr_CodResultado = lastr_Resultado[0];
            switch (lstr_CodResultado)
            {
                case "-1":
                    {
                        System.Diagnostics.Trace.WriteLine("[ValidarUsuario] El usuario no existe");
                        break;
                    }
                case "-2":
                    {
                        System.Diagnostics.Trace.WriteLine("[ValidarUsuario] La cuenta no ha sido activada");
                        break;
                    }
                case "-3":
                    {
                        System.Diagnostics.Trace.WriteLine("[ValidarUsuario] La cuenta se encuentra bloqueada");
                        break;
                    }
                case "-4":
                    {
                        System.Diagnostics.Trace.WriteLine("[ValidarContrasena] Contrasena incorrecta");
                        break;
                    }
                case "99":
                    {
                        System.Diagnostics.Trace.WriteLine("[Procedimiento] Error ejecutando el procedimiento");
                        break;
                    }
                case "00":
                    {
                        break;
                    }
                default:
                    {
                        System.Diagnostics.Trace.WriteLine("[ValidarUsuario] Se ha presentado un error");
                        break;
                    }
            }
            return lastr_Resultado;
        }


        /// <summary>
        /// Obtiene el codigo de activación de un usuario del sistema
        /// </summary>
        /// <param name="str_Cedula">Identificador de usuario</param>
        /// <returns>Codigo del usuario a registrar</returns>
        [WebMethod]
        public string uwsConsultarCodigo(string str_Cedula)
        {
            string lstr_Respuesta = String.Empty;
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lstr_Respuesta = tus_Usuario.ufnConsultarCodigoActivacion(DatosConexion(), str_Cedula);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine("[ValidarUsuario] Exception " + ex.Message);
            }
            return lstr_Respuesta;
        }//fin

        /// <summary>
        /// Registra un usuario en el sistema
        /// </summary>
        /// <param name="str_Cedula">Identificador de usuario</param>
        /// <param name="str_Contrasena">Contrasena de usuario</param>
        /// <param name="str_Correo">Correo de usuario</param>
        /// <returns>Mensaje de registro de usuario</returns>
        [WebMethod]
        public string uwsRegistrarUsuario(string str_Cedula, string str_TipoID, string str_Nombre, string str_Contrasena, string str_Correo)
        {
            string lstr_Respuesta = String.Empty;
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lstr_Respuesta = tus_Usuario.ufnRegistrarUsuario(DatosConexion(), str_Cedula, str_TipoID, str_Nombre, str_Contrasena, str_Correo);
            }
            catch (Exception ex)
            { }
            return lstr_Respuesta;
        }

        [WebMethod]
        public string uwsRegistrarUsuarioFirma(string str_Cedula, string str_TipoID, string str_Nombre, string str_Contrasena, string str_Correo)
        {
            string lstr_Respuesta = String.Empty;
            string lstr_Mensaje = String.Empty;
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lstr_Respuesta = tus_Usuario.ufnRegistrarUsuarioFirma(DatosConexion(), str_Cedula, str_TipoID, str_Nombre, str_Contrasena, str_Correo, out lstr_Mensaje);
                log.Info(lstr_Mensaje);
            }
            catch (Exception ex)
            {
                log.Error("Error al ejecutar webservice de RegistrarUsuarioFirma" + ex.ToString());
            }
            return lstr_Respuesta;
        }

        /// <summary>
        /// Metodo web para la recuperacion de la contrasena de usuario
        /// </summary>
        /// <param name="str_Cedula"></param>
        /// <param name="str_Correo"></param>
        /// <returns></returns>
        [WebMethod]
        public string uwsRecuperarContrasena(string str_Cedula, string str_Correo)
        {
            string lstr_Respuesta = String.Empty;
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lstr_Respuesta = tus_Usuario.ufnRecuperarContrasena(DatosConexion(), str_Cedula, str_Correo);
            }
            catch (Exception ex)
            { }
            return lstr_Respuesta;
        }

        /// <summary>
        /// Metodo web de confirmacion de usuario
        /// </summary>
        /// <param name="str_IdUsuario">Identificador de usuario</param>
        /// <param name="str_Clave">Contrasena de usuario</param>
        /// <param name="str_CodActivacion">Codigo de activacion de usuario</param>
        /// <param name="dat_FchModifica">Fecha de modificacion</param>
        /// <returns>Codigo de resultado</returns>
        [WebMethod]
        public string[] uwsConfirmarUsuario(string str_IdUsuario, string str_Clave, string str_CodActivacion, string dat_FchModifica, string str_IPMaquina)
        {
            string lstr_CodResultado = "99";
            string[] lastr_Resultado = new string[9];
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lastr_Resultado = tus_Usuario.ufnConfirmarUsuario(str_IdUsuario, str_Clave, str_CodActivacion, dat_FchModifica, str_IPMaquina);
            }
            catch
            {
                lastr_Resultado[0] = lstr_CodResultado;
                lastr_Resultado[8] = "Error en WS";
            }
            return lastr_Resultado;
        }

        [WebMethod]
        public string uwsCerrarSesionUsuario(string str_IdUsuario, string str_IdSesionUsuario)
        {
            string lstr_CodResultado = "99";
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lstr_CodResultado = tus_Usuario.ufnCerrarSesionUsuario(str_IdUsuario, str_IdSesionUsuario);
            }
            catch
            { }
            return lstr_CodResultado;
        }

        [WebMethod]
        public string uwsCerrarSesionesActivas(string str_IdUsuario)
        {
            string lstr_CodResultado = "99";
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lstr_CodResultado = tus_Usuario.ufnCerrarSesionesActivas(str_IdUsuario);
            }
            catch
            { }
            return lstr_CodResultado;
        }

        [WebMethod]
        public string[] uwsActualizarUsuario(string str_IdUsuario, string str_TipoIdUsuario, string str_NomUsuario, string str_CorreoUsuario,
            string boo_Activo, string boo_Administrador, string boo_CtaHabilitada, string str_IdSociedadGL,
            string str_UsrModifica, string str_FchModifica)
        {
            string[] ResultadoActualizacion = new string[2];
            ResultadoActualizacion[0] = "99";
            ResultadoActualizacion[1] = "Error al realizar la actualizacion";
            try
            {
                tUsuario ltu_Usuario = new tUsuario();
                ResultadoActualizacion = ltu_Usuario.ufnActualizarUsuario(str_IdUsuario, str_TipoIdUsuario, str_NomUsuario, str_CorreoUsuario,
                    boo_Activo, boo_Administrador, boo_CtaHabilitada, str_IdSociedadGL, str_UsrModifica, str_FchModifica);
            }
            catch (Exception ex)
            { }
            return ResultadoActualizacion;
        }

        [WebMethod]
        public string[] uwsActualizarPerfilUsuario(string str_CedUsuario, string str_ClaveActual, string str_NuevaClave,
            string str_Usuario, string str_FchModifica)
        {
            string[] lstr_ResultadoActualizacion = new string[2];
            try
            {
                tUsuario ltu_Usuario = new tUsuario();
                lstr_ResultadoActualizacion = ltu_Usuario.ufnActualizarPerfilUsuario(str_CedUsuario, str_ClaveActual, str_NuevaClave,
                    str_Usuario, str_FchModifica);
            }
            catch (Exception ex)
            {
                lstr_ResultadoActualizacion[0] = "99";
                lstr_ResultadoActualizacion[1] = "Error en WS";
            }
            return lstr_ResultadoActualizacion;
        }

        /// <summary>
        /// Metodo web de consulta de permisos de usuario
        /// </summary>
        /// <param name="str_IdUsuario">Identificador de usuario</param>
        /// <param name="str_IdObjeto">Identificador de objeto</param>
        /// <returns>Dataset con permisos de usuario</returns>
        [WebMethod]
        public DataSet uwsConsultarPermisosUsuarios(string str_IdUsuario, string str_IdObjeto)
        {
            DataSet lds_PermisosUsuario = new DataSet();
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lds_PermisosUsuario = tus_Usuario.ufnConsultarPermisosUsuarios(str_IdUsuario, str_IdObjeto);
            }
            catch
            { }
            return lds_PermisosUsuario;
        }

        [WebMethod]
        public bool uwsUsuarioPoseePermisos(string str_IdUsuario, string str_IdObjeto, string str_Permiso)
        {
            DataSet lds_PermisosUsuario = new DataSet();
            bool lboo_TienePermiso = false;
            try
            {
                tUsuario tus_Usuario = new tUsuario();
                lds_PermisosUsuario = tus_Usuario.ufnConsultarPermisosUsuarios(str_IdUsuario, str_IdObjeto);
                DataTable ldt_PermisosObjeto = lds_PermisosUsuario.Tables["Table"];
                if (ldt_PermisosObjeto.Rows.Count > 0)
                {
                    for (int lint_numFila = 0; lint_numFila < ldt_PermisosObjeto.Rows.Count; lint_numFila++)
                    {
                        if (ldt_PermisosObjeto.Rows[lint_numFila][str_Permiso].ToString() == "True")
                        {
                            lboo_TienePermiso = true;
                            break;
                        }
                    }
                }
            }
            catch
            { }
            return lboo_TienePermiso;
        }


        /// <summary>
        /// Metodo web para la consulta de los usuarios existentes
        /// </summary>
        /// <param name="str_IdUsuario">Identificador del usuario</param>
        /// <returns>Dataset con informacion de usuarios</returns>
        [WebMethod]
        public DataSet uwsConsultarUsuarios(string str_IdUsuario, string str_TipoIdUsuario, string str_IdSociedadGL)
        {
            return uwsConsultaUsuarios(str_IdUsuario, str_TipoIdUsuario, str_IdSociedadGL, "");
        }

        [WebMethod]
        public DataSet uwsConsultaUsuarios(string str_IdUsuario, string str_TipoIdUsuario, string str_IdSociedadGL, string str_NomUsuario)
        {
            DataSet lds_Usuario = new DataSet();
            if (String.IsNullOrEmpty(str_IdUsuario.Trim()))
            {
                str_IdUsuario = null;
            }
            try
            {
                tUsuario ltu_Usuario = new tUsuario();
                lds_Usuario = ltu_Usuario.ConsultarUsuarios(str_IdUsuario, str_TipoIdUsuario, str_IdSociedadGL, str_NomUsuario);
            }
            catch
            { }
            return lds_Usuario;
        }

        #endregion

        #region Gestion Objetos

        /// <summary>
        /// Metodo web que permite crear un nuevo objeto
        /// </summary>
        /// <param name="str_IdObjeto">Identificador de Objeto</param>
        /// <param name="str_IdModulo">Identificador de modulo</param>
        /// <param name="str_TipoObjeto">Tipo de objeto</param>
        /// <param name="str_DescObjeto">Descripcion de objeto</param>
        /// <param name="str_UsrCreacion">Usuario que crea el objeto</param>
        /// <returns>BooleAnno indicando si la operacion tuvo exito</returns>
        [WebMethod]
        public string uwsCrearObjeto(string str_IdObjeto, string str_IdModulo, string str_TipoObjeto,
            string str_DescObjeto, string str_UsrCreacion)
        {
            string str_ResultadoOperacion = "99";
            string lstr_Mensaje = String.Empty;
            try
            {
                tObjeto tob_Objeto = new tObjeto();
                str_ResultadoOperacion = tob_Objeto.CrearObjeto(str_IdObjeto, str_IdModulo, str_TipoObjeto,
                    str_DescObjeto, str_UsrCreacion, out lstr_Mensaje);
                log.Info(lstr_Mensaje);
            }
            catch
            { }
            return str_ResultadoOperacion;

        }

        /// <summary>
        /// Metodo web de consulta los objetos
        /// </summary>
        /// <param name="str_IdObjeto">Identificador del objeto</param>
        /// <param name="str_IdModulo">Identificador del modulo</param>
        /// <returns>Dataset con objetos que existen en el sistema</returns>
        [WebMethod]
        public DataSet uwsConsultarObjetos(string str_IdObjeto, string str_IdModulo)
        {
            return uwsConsultaObjetos(str_IdObjeto, str_IdModulo, "", "");
        }

        [WebMethod]
        public DataSet uwsConsultaObjetos(string str_IdObjeto, string str_IdModulo, string str_DescObjeto, string str_TipoObjeto)
        {
            DataSet lds_TablasObjetos = new DataSet();
            string lstr_MensajeSalida = String.Empty;
            if (String.IsNullOrEmpty(str_IdObjeto.Trim()))
            {
                str_IdObjeto = null;
            }
            if (String.IsNullOrEmpty(str_IdModulo.Trim()))
            {
                str_IdModulo = null;
            }
            try
            {
                tObjeto tob_Objeto = new tObjeto();
                lds_TablasObjetos = tob_Objeto.ConsultarObjetos(str_IdObjeto, str_IdModulo, str_DescObjeto, str_TipoObjeto, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return lds_TablasObjetos;
        }

        /// <summary>
        /// Define los permisos para un rol en un permiso especifico
        /// </summary>
        /// <param name="int_IdRol">Identificador de rol</param>
        /// <param name="str_IdObjeto">Identificador de objeto</param>
        /// <param name="boo_Consultar">Se da permiso de consulta sobre el objeto</param>
        /// <param name="boo_Insertar">Se da permiso de insercion sobre el objeto</param>
        /// <param name="boo_Borrar">Se da permiso de eliminacion sobre el objeto</param>
        /// <param name="boo_Actualizar">Se da permiso de actualizacion sobre el objeto</param>
        /// <param name="boo_Exportar">Se da permiso de exportacion sobre el objeto</param>
        /// <param name="boo_Imprimir">Se da permiso de impresion sobre el objeto</param>
        /// <param name="str_UsrCreacion">Usuario que asigna los permisos</param>
        /// <returns>BooleAnno indicando si la operacion fue exitosa</returns>
        [WebMethod]
        public bool uwsCrearRolObjeto(string int_IdRol, string str_IdObjeto, string boo_Consultar, string boo_Insertar,
            string boo_Borrar, string boo_Actualizar, string boo_Exportar, string boo_Imprimir, string str_UsrCreacion)
        {
            bool bool_ResultadoOperacion = false;
            try
            {
                tPermisosObjeto tob_PemisosObjeto = new tPermisosObjeto();
                bool_ResultadoOperacion = tob_PemisosObjeto.CrearRolObjeto(int_IdRol, str_IdObjeto, boo_Consultar,
                    boo_Insertar, boo_Borrar, boo_Actualizar, boo_Exportar, boo_Imprimir, str_UsrCreacion);
            }
            catch (Exception ex)
            { }
            return bool_ResultadoOperacion;
        }

        /// <summary>
        /// Metodo web que consulta los objetos y sus permisos en un rol
        /// </summary>
        /// <param name="int_IdRol">Identificador de rol</param>
        /// <param name="str_IdObjeto">Identificador de objeto</param>
        /// <param name="str_DescObjeto">Descripción de objeto</param>
        /// <returns>Dataset con tabla que contiene los objetos y permisos del rol</returns>
        [WebMethod]
        public DataSet uwsConsultarRolesObjeto(string int_IdRol, string str_IdObjeto, string str_DescObjeto)
        {
            DataSet lds_Respuesta = new DataSet();
            try
            {
                tPermisosObjeto ltp_PermisoObjeto = new tPermisosObjeto();
                lds_Respuesta = ltp_PermisoObjeto.ConsultarRolesObjetos(int_IdRol, str_IdObjeto, str_DescObjeto);
            }
            catch (Exception ex)
            { }
            return lds_Respuesta;
        }

        [WebMethod]
        public bool uwsActualizarRolObjeto(string int_IdRol, string str_IdObjeto, string boo_Consultar, string boo_Insertar,
            string boo_Borrar, string boo_Actualizar, string boo_Exportar, string boo_Imprimir, string str_Usuario, string str_FchModifica)
        {
            string lstr_Mensaje = String.Empty;
            bool bool_ResultadoOperacion = false;
            try
            {
                tPermisosObjeto tob_PermisosObjeto = new tPermisosObjeto();
                bool_ResultadoOperacion = tob_PermisosObjeto.ActualizarRolObjeto(int_IdRol, str_IdObjeto,
                 boo_Consultar, boo_Insertar, boo_Borrar, boo_Actualizar, boo_Exportar,
                 boo_Imprimir, str_Usuario, str_FchModifica, out lstr_Mensaje);
                Log.Info(lstr_Mensaje);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return bool_ResultadoOperacion;
        }

        [WebMethod]
        public bool uwsActualizarObjeto(string str_IdObjeto, string str_IdModulo, string str_Habilitado, string str_DescObjeto,
            string str_UsrModificacion)
        {
            bool lboo_ResActualizacion = false;
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tObjeto ltob_Objeto = new tObjeto();
                lboo_ResActualizacion = ltob_Objeto.ActualizarObjeto(str_IdObjeto, str_IdModulo, str_Habilitado, str_DescObjeto,
                    str_UsrModificacion, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return lboo_ResActualizacion;
        }

        /// <summary>
        /// Metodo para eliminar un objeto del sistema
        /// </summary>
        /// <param name="str_IdObjeto">Identificador del objeto</param>
        /// <param name="str_IdModulo">Identificador del modulo</param>
        /// <param name="str_FchModificacion">Fecha de consulta del objeto</param>
        /// <returns>BoolenAnno indicando el resultado de la eliminacion</returns>
        [WebMethod]
        public bool uwsEliminarObjeto(string str_IdObjeto, string str_IdModulo, string str_FchModificacion)
        {
            string lstr_Mensaje = String.Empty;
            bool bool_ResultadoOperacion = false;
            try
            {
                tObjeto tob_Objeto = new tObjeto();
                bool_ResultadoOperacion = tob_Objeto.ufnEliminarObjeto(str_IdObjeto, str_IdModulo, str_FchModificacion, out lstr_Mensaje);
                Log.Info(lstr_Mensaje);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return bool_ResultadoOperacion;
        }
        #endregion

        #region Gestion Roles
        /// <summary>
        /// Metodo web para consulta de roles
        /// </summary>
        /// <param name="int_IdRol">Identificador de rol</param>
        /// <param name="str_DescRol">Descripción del rol</param>
        /// <returns>Dataset con roles segun parametros de entrada</returns>
        [WebMethod]
        public DataSet uwsConsultarRoles(string int_IdRol)
        {
            return uwsConsultaRoles(int_IdRol, "");
        }
        [WebMethod]
        public DataSet uwsConsultaRoles(string int_IdRol, string str_DescRol)
        {
            string lstr_Mensaje = String.Empty;
            DataSet lds_Respuesta = new DataSet();
            try
            {
                tRol rol_RolUSuario = new tRol();
                lds_Respuesta = rol_RolUSuario.ConsultarRolSP(int_IdRol, str_DescRol, out lstr_Mensaje);
                Log.Info(lstr_Mensaje);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return lds_Respuesta;
        }

        /// <summary>
        /// Metodo web de actualizacion de roles
        /// </summary>
        /// <param name="int_IdRol">Identificador de rol a actualizar</param>
        /// <param name="str_DescRol">Descripcion de rol</param>
        /// <param name="str_Usuario">Usuario que realiza la actualizacion</param>
        /// <param name="dat_FchModifica">Fecha de modificacion</param>
        /// <returns></returns>
        [WebMethod]
        public bool uwsActualizarRol(string int_IdRol, string str_DescRol, string str_IdSesionUsuario, string str_Habilitado, string str_Usuario, string dat_FchModifica)
        {
            bool lboo_ResultadoActualizacion = false;
            try
            {
                tRol ltro_Rol = new tRol();
                lboo_ResultadoActualizacion = ltro_Rol.ActualizarRol(int_IdRol, str_DescRol, str_IdSesionUsuario, str_Habilitado, str_Usuario, dat_FchModifica);
            }
            catch (Exception ex)
            { }
            return lboo_ResultadoActualizacion;
        }

        /// <summary>
        /// Metodo web para creacion de roles
        /// </summary>
        /// <param name="str_DescRol">Descripcion del rol</param>
        /// <param name="str_Usuario">Usuario que crea el rol</param>
        /// <returns>Resultado de creacion de rol</returns>
        [WebMethod]
        public bool uwsCrearRol(string str_DescRol, string str_IdSesionUsuario, string str_Usuario)
        {
            string lstr_MensajeSalida = String.Empty;
            bool bool_ResCreacion = false;
            try
            {
                tRol ltro_Rol = new tRol();
                bool_ResCreacion = ltro_Rol.CrearRol(str_DescRol, str_IdSesionUsuario, str_Usuario, out lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return bool_ResCreacion;
        }

        [WebMethod]
        public bool uwsEliminarRol(string int_IdRol, string dat_FchModifica)
        {
            string lstr_MensajeSalida = String.Empty;
            bool lboo_ResultadoEliminacion = false;
            try
            {
                tRol lrol_RolUsuario = new tRol();
                lboo_ResultadoEliminacion = lrol_RolUsuario.ufnEliminarRol(int_IdRol, dat_FchModifica, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return lboo_ResultadoEliminacion;
        }

        [WebMethod]
        public DataSet uwsConsultarRolesUsuario(string str_IdRol, string str_IdUsuario)
        {
            string lstr_MensajeSalida = String.Empty;
            DataSet lds_Respuesta = new DataSet();
            try
            {
                tRol lrol_RolUsuario = new tRol();
                lds_Respuesta = lrol_RolUsuario.ConsultarRolesUsuario(str_IdRol, str_IdUsuario, out lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return lds_Respuesta;
        }

        [WebMethod]
        public Boolean uwsCrearRolUsuario(string str_IdSesionUsuario, string str_IdRol, string str_IdUsuario, string str_UsuarioAdmin)
        {
            string lstr_MensajeSalida = String.Empty;
            bool lboo_ResCreacion = false;
            try
            {
                tRol lrol_RolUsuario = new tRol();
                lboo_ResCreacion = lrol_RolUsuario.CrearRolUsuario(str_IdSesionUsuario, str_IdUsuario, str_IdRol, str_UsuarioAdmin, out lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return lboo_ResCreacion;
        }

        [WebMethod]
        public bool uwsEliminarRolUsuario(string str_IdUsuario, string int_IdRol, string dat_FchModifica)
        {
            string lstr_MensajeSalida = String.Empty;
            bool lboo_ResultadoEliminacion = false;
            try
            {
                tRol lrol_RolUsuario = new tRol();
                lboo_ResultadoEliminacion = lrol_RolUsuario.ufnEliminarRolUsuario(str_IdUsuario, int_IdRol, dat_FchModifica, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return lboo_ResultadoEliminacion;
        }

        #endregion

        #region Politicas
        /// <summary>
        /// Metodo web para consultar las politicas existentes
        /// </summary>
        /// <param name="int_IdPolitica">Identificador de politica</param>
        /// <returns></returns>
        [WebMethod]
        public DataSet uwsConsultarPoliticas(string int_IdPolitica, string dat_FchVigencia)
        {
            DataSet lds_Politicas = new DataSet();
            try
            {
                Tipoliticas Tipo_Politicas = new Tipoliticas();
                lds_Politicas = Tipo_Politicas.ConsultarPoliticas(int_IdPolitica, dat_FchVigencia);
            }
            catch (Exception ex)
            { }
            return lds_Politicas;
        }


        /// <summary>
        /// Metodo web que actualiza las politicas de seguridad del sistema
        /// </summary>
        /// <param name="int_TiempoOcio">Tiempo (en minutos) que tarda el sistema en cerrar sesion automaticamente al no detectar accion alguna</param>
        /// <param name="int_MaxSesionesUsuario">Maximo numero de sesione abiertas por un mismo usuario al mismo tiempo</param>
        /// <param name="int_MaxNroIntentos">Cantidad maxima de intentos de iniciar sesion</param>
        /// <param name="int_MaxVigenciaClave">Duracion de la contrasena, en dias, antes de perder validez</param>
        /// <param name="int_TiempoBloqueoClave">Duracion, en minutos, que permanece la clave bloqueada </param>
        /// <param name="int_MinTamAnnoClave">Longitud minima de contrasena</param>
        /// <param name="int_MinLetrasClave">Cantidad minima de letras en contrasena</param>
        /// <param name="int_MinNumerosClave">Cantidad minima de numeros en contrasena</param>
        /// <param name="int_MinCaracteresClave">Cantidad minima de caracteres, simbolos, en contrasena</param>
        /// <param name="int_NroReutilizacionClave">Este numero indica cuantas nuevas contrasenas tienen que haber 
        ///  sido registradas para volver a utilizar una contrasena antigua</param>
        /// <param name="int_AntiguedadBitacora">Antiguedad, en dias, de los registros que permanecen almacenados en la bitacora</param>
        /// <param name="str_UsrModifica">Identificador de usuario que realiza los cambios</param>
        /// <param name="dat_FchModfica">Fecha en que se realizan los cambios</param>
        /// <returns>BooleAnno indicando si se tuvo exito al realizar la operacion</returns>
        [WebMethod]
        public bool uwsActualizarPoliticasSeguridad(string int_TiempoOcio, string int_MaxSesionesUsuario, string int_MaxNroIntentosFallidos,
            string int_MaxVigenciaClave, string int_TiempoBloqueoClave, string int_MinTamanoClave, string int_MinLetrasClave,
            string int_MinNumerosClave, string int_MinCaracteresClave, string int_NroReutilizacionUltimasClaves, string int_AntiguedadBitacora,
            string str_UsrModifica, string dat_FchModfica)
        {
            bool lboo_TransaccionExitosa = false;
            try
            {
                Tipoliticas lTipo_Politicas = new Tipoliticas();
                lTipo_Politicas.ActualizarPoliticas(int_TiempoOcio, int_MaxSesionesUsuario, int_MaxNroIntentosFallidos,
                int_MaxVigenciaClave, int_TiempoBloqueoClave, int_MinTamanoClave, int_MinLetrasClave,
                int_MinNumerosClave, int_MinCaracteresClave, int_NroReutilizacionUltimasClaves, int_AntiguedadBitacora,
                str_UsrModifica, dat_FchModfica);
                lboo_TransaccionExitosa = true;
            }
            catch (Exception ex)
            { }
            return lboo_TransaccionExitosa;
        }

        #endregion

        #region Bitacora
        [WebMethod]
        public DataSet uwsConsultarBitacoras(string str_FechaInicio, string str_FechaFinal, string str_IdRegistro,
            string str_IdUsuario, string str_IdModulo, string str_Accion)
        {
            DataSet lds_Bitacora = new DataSet();
            DateTime? FechaInicio = null;
            DateTime? FechaFinal = null;
            //try
            //{

            if (!string.IsNullOrEmpty(str_FechaInicio))
                FechaInicio = DateTime.ParseExact(str_FechaInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(str_FechaFinal))
                FechaFinal = DateTime.ParseExact(str_FechaFinal, lstr_formato_fecha, CultureInfo.InvariantCulture);


            tBitacora ldb_Bitacora = new tBitacora();
            lds_Bitacora = ldb_Bitacora.ufnConsultarBitacoraErrores(FechaInicio, FechaFinal, str_IdRegistro,
            str_IdUsuario, str_IdModulo, str_Accion);
            //}
            //catch (Exception ex)
            //{ }                     
            return lds_Bitacora;
        }

        [WebMethod]
        public DataSet uwsConsultarBitacorasAsientos(string str_FechaInicio, string str_FechaFinal,
            string str_IdOperacion, string str_IdSociedadGL, string str_IdTransaccion, string str_IdModulo)
        {
            DataSet lds_Bitacora = new DataSet();
            try
            {
                tBitacora ldb_Bitacora = new tBitacora();
                lds_Bitacora = ldb_Bitacora.ufnConsultarBitacoraAsientos(str_FechaInicio, str_FechaFinal,
                    str_IdOperacion, str_IdSociedadGL, str_IdTransaccion, str_IdModulo);
            }
            catch (Exception ex)
            { }
            return lds_Bitacora;
        }

        [WebMethod]
        public String uwsRegistrarAccionBitacora(string str_IdModulo, string str_IdSesionUsuario, string str_Accion, string str_Detalle)
        {
            string lstr_MensajeSalida = String.Empty;
            string str_ResCreacion = String.Empty;
            try
            {
                tBitacora ltro_Bitacora = new tBitacora();
                str_ResCreacion = ltro_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, str_IdSesionUsuario, str_Accion, str_Detalle);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return str_ResCreacion;
        }


        [WebMethod]
        public String uwsRegistrarAccionBitacoraCo(string str_IdModulo, string str_IdSesionUsuario, string str_Accion, string str_Detalle, string str_IdOperacion, string str_IdTransaccion, string str_IdSociedadGL)
        {
            string lstr_MensajeSalida = String.Empty;
            string str_ResCreacion = String.Empty;
            try
            {
                tBitacora ltro_Bitacora = new tBitacora();
                str_ResCreacion = ltro_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, str_IdSesionUsuario, str_Accion, str_Detalle, str_IdOperacion, str_IdTransaccion, str_IdSociedadGL);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message.ToString());
            }
            return str_ResCreacion;
        }

        #endregion

        #endregion

        #region Contingentes
        #region Metodos
        [WebMethod]
        public string RegistrarExpedienteReporte(string lstr_IdExpediente,
                                    string lstr_IdSociedadGL,
                                    string lstr_NomDemandado,
                                    decimal ldec_MontoPrincipal,
                                    decimal ldec_MontoInteres,
                                    decimal ldec_MontoCostas,
                                    decimal ldec_MontoIntMoratorios,
                                    decimal ldec_MontoDannosPerj,
                                    string lstr_UsrCreacion)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            string periodo = String.Empty;

            try
            {
                clsExpedientes lcls_Expedientes = new clsExpedientes();
                lcls_Expedientes.CrearExpedientesReportes(lstr_IdExpediente,
                                    lstr_IdSociedadGL,
                                    lstr_NomDemandado,
                                    ldec_MontoPrincipal,
                                    ldec_MontoInteres,
                                    ldec_MontoCostas,
                                    ldec_MontoIntMoratorios,
                                    ldec_MontoDannosPerj,
                                    lstr_UsrCreacion,
                                    out codSalida,
                                    out txtSalida);

                mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }

            return mensaje;
        }

        /// <summary>
        /// Registrar expediente 
        /// </summary>
        /// <param name="str_IdExpediente"></param>
        /// <param name="int_NumExOrigen"></param>
        /// <param name="str_TipoExpediente"></param>
        /// <param name="str_EstadoExpediente"></param>
        /// <param name="dt_FechaDemanda"></param>
        /// <param name="str_TipoProceso"></param>
        /// <param name="str_MotivoDemanda"></param>
        /// <param name="str_MonedaPretension"></param>
        /// <param name="dec_TipoCambio"></param>
        /// <param name="dec_MontoPretension"></param>
        /// <param name="dec_MontoPretColones"></param>
        /// <param name="int_EstadoPretension"></param>
        /// <param name="dt_PosibleFecEntRec"></param>
        /// <param name="dec_ValorPresente"></param>
        /// <param name="str_EstadoProcesal"></param>
        /// <param name="str_UsrCreacion"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsRegistrarExpedientes(string IdExpediente, string str_IdSociedadGL, string NumExOrigen, string TipoExpediente, string EstadoExpediente, DateTime FechaDemanda, string TipoProceso, string MotivoDemanda, string EstadoProcesal, string UsuarioCreacion, string CedulaActor, string CedulaDemandado, string NombreActor, string NombreDemandado, string TipoEntidadPersona, string Porcentaje)
        {
            clsExpedientes reg_Expedientes = new clsExpedientes();

            string[] lstr_respuesta = new string[2];
            try
            {

                lstr_respuesta = reg_Expedientes.RegsitrarExpedientes(IdExpediente, str_IdSociedadGL, NumExOrigen, TipoExpediente, EstadoExpediente, FechaDemanda, TipoProceso, MotivoDemanda, EstadoProcesal, UsuarioCreacion, CedulaActor, CedulaDemandado, NombreActor, NombreDemandado, TipoEntidadPersona, Porcentaje);
                log.Info("Resultado: " + lstr_respuesta);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            return lstr_respuesta;
        }
        /// <summary>
        /// Modificar expedientes 
        /// </summary>
        /// <param name="str_IdExpediente"></param>
        /// <param name="int_NumExOrigen"></param>
        /// <param name="str_TipoExpediente"></param>
        /// <param name="str_EstadoExpediente"></param>
        /// <param name="dt_FechaDemanda"></param>
        /// <param name="str_TipoProceso"></param>
        /// <param name="str_MotivoDemanda"></param>
        /// <param name="str_MonedaPretension"></param>
        /// <param name="dec_TipoCambio"></param>
        /// <param name="dec_MontoPretension"></param>
        /// <param name="dec_MontoPretColones"></param>
        /// <param name="int_EstadoPretension"></param>
        /// <param name="dt_PosibleFecEntRec"></param>
        /// <param name="dec_ValorPresente"></param>
        /// <param name="str_EstadoProcesal"></param>
        /// <param name="str_UsrCreacion"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsModificarExpediente(string IdExpediente, string str_IdSociedadGL, string NumExOrigen, string TipoExpediente, string EstadoExpediente,
            DateTime FechaDemanda, string TipoProceso, string MotivoDemanda, string EstadoProcesal,
            string UsuarioModificacion, string CedulaActor, string CedulaDemandado, string NombreActor,
            string NombreDemandado, string TipoEntidadPersona, string Porcentaje)
        {
            clsExpedientes reg_Expedientes = new clsExpedientes();

            string[] lstr_respuesta = new string[2];
            try
            {
                //Modificar Expediente
                lstr_respuesta = reg_Expedientes.ModificarExpedientes(IdExpediente, str_IdSociedadGL, NumExOrigen, TipoExpediente, EstadoExpediente, FechaDemanda, TipoProceso, MotivoDemanda, EstadoProcesal, UsuarioModificacion, CedulaActor, CedulaDemandado, NombreActor, NombreDemandado, TipoEntidadPersona, Porcentaje);

            }
            catch (Exception ex)
            {


            }

            return lstr_respuesta;

        }
        /// <summary>
        /// Eliminar Expediente
        /// </summary>
        /// <param name="IdExpediente"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsEliminarExpediente(string IdExpediente)
        {

            string[] lstr_result = new string[2];

            return lstr_result;

        }
        /// <summary>
        /// Regitra una pretension Inicial del expediente
        /// </summary>
        /// <param name="NumExpediente"></param>
        /// <param name="MonedaPretension"></param>
        /// <param name="TipoCambio"></param>
        /// <param name="MontoPretension"></param>
        /// <param name="MontoPretColones"></param>
        /// <param name="EstadoPretension"></param>
        /// <param name="PosibleFecEntRec"></param>
        /// <param name="ValorPresente"></param>
        /// <param name="UsrModifica"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsRegistrarPretensionInicial(string str_IdExpediente, string str_Sociedad,
            string str_TipoProceso, string str_MonedaPretension, decimal dec_TipoCambio, decimal dec_MontoPretension, decimal dec_MontoPretColones, decimal dec_MontoPosibleReembolso, int int_EstadoPretension, DateTime? dt_PosibleFecEntRec, decimal dec_ValorPresente, string str_ObservacionesPretension, string str_UsrModifica)
        {

            clsExpedientes reg_ExpedientesIni = new clsExpedientes();
            string[] lstr_respuesta = new string[2];

            try
            {

                lstr_respuesta = reg_ExpedientesIni.RegistrarPretensionInicial(str_IdExpediente, str_Sociedad, str_TipoProceso, str_MonedaPretension, dec_TipoCambio, dec_MontoPretension, dec_MontoPretColones, dec_MontoPosibleReembolso, int_EstadoPretension, dt_PosibleFecEntRec, dec_ValorPresente, str_ObservacionesPretension, str_UsrModifica);

            }
            catch (Exception ex)
            {


            }

            return lstr_respuesta;
        }
        /// <summary>
        /// Registrar Resoluciones
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsRegistrarResolucion(string IdResolucion, string IdExpedienteFK, string IdSociedadGL, string EstadoResolucion, string Estado, DateTime FechaResolucion,
            DateTime? PosibleFecSalidaRec, decimal MontoPosibleReembolso, decimal MontoPosReemColones, string Observacion, int CxCaCxP, string UsrCreacion, string Moneda,
            string EstadoTransaccion, decimal TipoCambio, Decimal Tbp, Decimal Tiempo,
            decimal MontoPrincipal, decimal MontoIntereses, decimal InteresesMoratorios,
            decimal InteresesMoratoriosColones, decimal MontoInteresesColones, decimal MontoPrincipalColones, decimal ValorPresenteIntColones,
            decimal ValorPresentePrincipal, decimal ValorPresenteIntereses, decimal ValorPresentePrinColones, decimal Costas, decimal CostasColones, decimal DanoMoral,
            decimal DanoMoralColones, string TipoTransaccion, Nullable<Int32> int_EstadoPretension, string EstadoProcesal)
        {
            clsResoluciones reg_Resoluciones = new clsResoluciones();
            string[] lstr_result = new string[2];
            try
            {
                //ultimo codigo modificado 05/11/2015
                lstr_result = reg_Resoluciones.RegistrarResolucion(IdResolucion, IdExpedienteFK, IdSociedadGL, EstadoResolucion, Estado,
                    FechaResolucion, PosibleFecSalidaRec, MontoPosibleReembolso, MontoPosReemColones, Observacion, CxCaCxP, UsrCreacion,
                    Moneda, EstadoTransaccion, TipoCambio, Tbp, Tiempo,
                    MontoPrincipal, MontoIntereses, InteresesMoratorios, InteresesMoratoriosColones,
                    MontoInteresesColones, MontoPrincipalColones, ValorPresenteIntColones, ValorPresentePrincipal, ValorPresenteIntereses,
                    ValorPresentePrinColones, Costas, CostasColones, DanoMoral, DanoMoralColones, TipoTransaccion, int_EstadoPretension, EstadoProcesal);

            }
            catch (Exception err)
            {
                lstr_result[0] = err.Message;

            }
            return lstr_result;
        }

        /// <summary>
        /// Modificar Resolucion 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsModificarResolucion(int IdRes, int IdCobroPagoResolucion, string IdResolucion, string IdExpediente, string IdSociedadGL, string EstadoResolucion, string Estado, DateTime FechaResolucion,
            DateTime? PosibleFecSalidaRec, decimal MontoPosibleReembolso, decimal MontoPosReemColones, string Observacion, int CxCaCxP, string Moneda,
            decimal TipoCambio, Decimal Tbp, Decimal Tiempo,
            decimal MontoPrincipal, decimal MontoIntereses, decimal ValorPresentePrincipal, decimal ValorPresenteIntereses,
            decimal MontoPrincipalColones, decimal MontoInteresesColones, string EstadoProcesal, Nullable<Int32> int_EstadoPretension, string UsrModifica)
        {
            clsResoluciones reg_Resoluciones = new clsResoluciones();
            string[] lstr_result = new string[2];
            try
            {
                lstr_result = reg_Resoluciones.ModificarResolucion(
                    IdRes, IdCobroPagoResolucion, IdResolucion, IdExpediente,
                IdSociedadGL, EstadoResolucion, Estado, FechaResolucion, PosibleFecSalidaRec,
                MontoPosibleReembolso, MontoPosReemColones, Observacion, CxCaCxP, Moneda, TipoCambio,
                Tbp, Tiempo, MontoPrincipal, MontoIntereses, ValorPresentePrincipal,
                    ValorPresenteIntereses, MontoPrincipalColones, MontoInteresesColones, EstadoProcesal, int_EstadoPretension, UsrModifica);
            }
            catch (Exception exc)
            {
                lstr_result[0] = "99";
                lstr_result[1] = "Erorr: " + exc.Message;
            }
            return lstr_result;
        }

        /// <summary>
        /// Modificar Resolucion 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsModificarResolucionDeta(int IdRes, int IdCobroPagoResolucion, string IdResolucion, string IdExpediente, string IdSociedadGL, string EstadoResolucion, string Estado, DateTime FechaResolucion,
            DateTime? PosibleFecSalidaRec, decimal MontoPosibleReembolso, decimal MontoPosReemColones, string Observacion, int CxCaCxP, string Moneda,
            decimal TipoCambio, Decimal Tbp, Decimal Tiempo,
            decimal MontoPrincipal, decimal MontoIntereses, decimal ValorPresentePrincipal, decimal ValorPresenteIntereses,
            decimal MontoPrincipalColones, decimal MontoInteresesColones, string EstadoProcesal, Nullable<Int32> int_EstadoPretension, string UsrModifica, decimal? InteresesMoratorios = null, decimal? Costas = null, decimal? DanoMoral = null,
                                            decimal? InteresesMoratoriosColones = null, decimal? CostasColones = null, decimal? DanoMoralColones = null, decimal? ValorPresentePrinColones = null, decimal? ValorPresenteIntColones = null,
                                            string TipoTransaccion = null, string EstadoTransaccion = null)
        {
            clsResoluciones reg_Resoluciones = new clsResoluciones();
            string[] lstr_result = new string[2];
            try
            {
                lstr_result = reg_Resoluciones.ModificarResolucion(//Deta(
                    IdRes, IdCobroPagoResolucion, IdResolucion, IdExpediente,
                IdSociedadGL, EstadoResolucion, Estado, FechaResolucion, PosibleFecSalidaRec,
                MontoPosibleReembolso, MontoPosReemColones, Observacion, CxCaCxP, Moneda, TipoCambio,
                Tbp, Tiempo, MontoPrincipal, MontoIntereses, ValorPresentePrincipal,
                    ValorPresenteIntereses, MontoPrincipalColones, MontoInteresesColones, EstadoProcesal, int_EstadoPretension, UsrModifica);//, 
                    //InteresesMoratorios,Costas ,DanoMoral,
                     //                       InteresesMoratoriosColones ,CostasColones,DanoMoralColones ,ValorPresentePrinColones ,ValorPresenteIntColones  ,
                     //                       TipoTransaccion, EstadoTransaccion);
            }
            catch (Exception exc)
            {
                lstr_result[0] = "99";
                lstr_result[1] = "Erorr: " + exc.Message;
            }
            return lstr_result;
        }

        [WebMethod]
        public DataSet uwsConsultarResolucion(string IdResolucion, string IdExpediente, string IdSociedadGL, out string str_Codigo, out string str_Mensaje)
        {
            DataSet lds_ConsultaResoluciones = new DataSet();
            clsResoluciones reg_Resoluciones = new clsResoluciones();
            str_Codigo = String.Empty;
            str_Mensaje = String.Empty;

            try
            {
                //ultimo codigo modificado 05/11/2015
                lds_ConsultaResoluciones = reg_Resoluciones.ConsultarResolucion(IdResolucion, IdExpediente, IdSociedadGL, out str_Codigo, out str_Mensaje);
            }
            catch (Exception exc)
            {
                str_Codigo = str_Codigo + "99";
                str_Mensaje = str_Mensaje + exc.Message;
            }
            return lds_ConsultaResoluciones;
        }

        [WebMethod]
        public DataSet uwsConsultarExpendientesResoluciones(string NumExpediente, string IdSociedadGL, string IdResolucion, out string str_Codigo, out string str_Mensaje)
        {
            DataSet lds_ConsultaResoluciones = new DataSet();
            clsResoluciones reg_Resoluciones = new clsResoluciones();
            str_Codigo = String.Empty;
            str_Mensaje = String.Empty;

            try
            {
                //ultimo codigo modificado 05/11/2015
                lds_ConsultaResoluciones = reg_Resoluciones.ConsultarExpendientesResoluciones(NumExpediente, IdSociedadGL, IdResolucion, out str_Codigo, out str_Mensaje);
            }
            catch (Exception exc)
            {
                str_Codigo = str_Codigo + "99";
                str_Mensaje = str_Mensaje + exc.Message;
            }
            return lds_ConsultaResoluciones;
        }

        /// <summary>
        /// Anular Expediente
        /// </summary>
        /// <param name="IdExpediente"></param>
        /// <param name="EstadoExpediente"></param>
        /// <returns></returns>
        /// <summary>
        /// Anular Expediente
        /// </summary>
        /// <param name="str_IdExpediente"></param>
        /// <param name="str_EstadoExpediente"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsAnularExpediente(string str_IdExpediente, string str_EstadoExpediente, string str_Sociedad)
        {

            clsExpedientes reg_Expedientes = new clsExpedientes();

            string[] lstr_respuesta = new string[2];
            try
            {

                lstr_respuesta = reg_Expedientes.AnularExpediente(str_IdExpediente, str_EstadoExpediente, str_Sociedad);
                log.Info("Resultado: " + lstr_respuesta);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }

            return lstr_respuesta;

        }

        /// <summary>
        /// Cerrar una revelacion
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsDeclararSinLugarResolucion(string str_IdExpediente, string str_TipoExpediente, string str_EstadoResolucion, int int_CxCaCxP, string str_Sociedad, string str_UsrCreacion)
        {
            clsResoluciones rev_Resol = new clsResoluciones();
            string[] lstr_respuesta = new string[2]; ;
            try
            {
                lstr_respuesta = rev_Resol.DeclararsinLugar(str_IdExpediente, str_TipoExpediente, str_EstadoResolucion, int_CxCaCxP, str_Sociedad, str_UsrCreacion);
                log.Info("Resultado: " + lstr_respuesta);
            }
            catch (Exception ex)
            {
                log.Error("Error: " + ex);
            }
            return lstr_respuesta;
        }

        [WebMethod]
        public string[] uwsRegistrarCobrosPagos(
            string IdResolucion, string IdExpedienteFK, int IDRes,
            string Moneda, decimal TipoCambio, decimal Tbp, decimal Tiempo, decimal TipoCambioCierre,

            decimal MontoPrincipal, decimal MontoPrincipalColones, decimal MontoPrincipalCierre,
            decimal MontoIntereses, decimal MontoInteresesColones, decimal MontoInteresesCierre,

            decimal ValorPresentePrincipal, decimal ValorPresentePrinColones, decimal ValorPresentePrinCierre,
            decimal ValorPresenteIntereses, decimal ValorPresenteIntColones, decimal ValorPresenteIntCierre,

            decimal Intereses, decimal InteresesColones, decimal InteresesCierre,
            decimal InteresesMoratorios, decimal InteresesMoratoriosColones, decimal InteresesMoratoriosCierre,
            decimal Costas, decimal CostasColones, decimal CostasCierre,
            decimal DanoMoral, decimal DanoMoralColones, decimal DanoMoralCierre,

            Decimal? dec_InteresesAnterior, Decimal? dec_CostasAnterior,
            Decimal? dec_InteresesMoratoriosAnterior, Decimal? dec_DanoMoralAnterior,

            string TipoTransaccion, string EstadoTransaccion, DateTime? FechFalloResol,
            string Observaciones, string UsrCreacion)
        {

            clsCobrosPagos reg_Resoluciones = new clsCobrosPagos();
            string[] lstr_result = new string[2];
            try
            {

                lstr_result = reg_Resoluciones.RegistrarCobrosPagos(
                    IdResolucion, IdExpedienteFK, IDRes,
                    Moneda, TipoCambio, Tbp, Tiempo, TipoCambioCierre,

                    MontoPrincipal, MontoPrincipalColones, MontoPrincipalCierre,
                    MontoIntereses, MontoInteresesColones, MontoInteresesCierre,
                    ValorPresentePrincipal, ValorPresentePrinColones, ValorPresentePrinCierre,
                    ValorPresenteIntereses, ValorPresenteIntColones, ValorPresenteIntCierre,
                    Intereses, InteresesColones, InteresesCierre,
                    InteresesMoratorios, InteresesMoratoriosColones, InteresesMoratoriosCierre,
                    Costas, CostasColones, CostasCierre,
                    DanoMoral, DanoMoralColones, DanoMoralCierre,

                    dec_InteresesAnterior, dec_InteresesMoratoriosAnterior,
                    dec_CostasAnterior, dec_DanoMoralAnterior,

                    TipoTransaccion, EstadoTransaccion, FechFalloResol,
                    Observaciones, UsrCreacion);

            }
            catch (Exception err)
            {
                lstr_result[0] = err.Message;

            }
            return lstr_result;


        }        
        /// <summary>
        /// Modificar Cobros Pagos para Archivo 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsModificarCobrosPagosArchivoD(
             int lint_IdRes,
         int lint_IdCobroPagoResolucion,
         string lstr_IdResolucion,//Identificador único de la resolución dictada en los tribunales de justicia
         string lstr_IdExpediente,//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
         string lstr_IdSociedadGL,
         string lstr_EstadoResolucion,//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
         string lstr_Moneda,//La moneda en la cual se recibe el cobro. Campo obligatorio
         decimal ldec_TipoCambio,//El tipo de cambio al momento de incluirlo en el sistema.
         decimal ldec_MontoPrincipal,//Es el monto principal a cobrar/pagar
         decimal ldec_MontoPrincipalColones,//Monto principal a cobrar/pagar en colones
         decimal ldec_Intereses,
         decimal ldec_InteresesColones,
         decimal ldec_InteresesMoratorios,
         decimal ldec_InteresesMoratoriosColones,
         decimal ldec_Costas,
         decimal ldec_CostasColones,
         decimal ldec_DanoMoral,
         decimal ldec_DanoMoralColones,
         string lstr_UsrModifica, 
            string lstr_Origen = "Judicial")
        {
            clsCobrosPagos reg_Resoluciones = new clsCobrosPagos();
            string[] lstr_result = new string[2];
            try
            {
                lstr_result = reg_Resoluciones.ModificarCobrosPagosArchivo(
                                 lint_IdRes,
                                 lint_IdCobroPagoResolucion,
                                 lstr_IdResolucion,//Identificador único de la resolución dictada en los tribunales de justicia
                                 lstr_IdExpediente,//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
                                 lstr_IdSociedadGL,
                                 lstr_EstadoResolucion,//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
                                 lstr_Moneda,//La moneda en la cual se recibe el cobro. Campo obligatorio
                                 ldec_TipoCambio,//El tipo de cambio al momento de incluirlo en el sistema.
                                 ldec_MontoPrincipal,//Es el monto principal a cobrar/pagar
                                 ldec_MontoPrincipalColones,//Monto principal a cobrar/pagar en colones
                                 ldec_Intereses,
                                 ldec_InteresesColones,
                                 ldec_InteresesMoratorios,
                                 ldec_InteresesMoratoriosColones,
                                 ldec_Costas,
                                 ldec_CostasColones,
                                 ldec_DanoMoral,
                                 ldec_DanoMoralColones,
                                 lstr_UsrModifica);//Realizamos el mappeo en la BD
            }
            catch (Exception exc)
            {
                lstr_result[0] = "99";
                lstr_result[1] = "Erorr: " + exc.Message;
            }
            return lstr_result;
        }

        /// <summary>
        /// Modificar Cobros Pagos para Archivo 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsModificarCobrosPagosArchivo(
             int? lint_IdRes,
         int? lint_IdCobroPagoResolucion,
         string lstr_IdResolucion,//Identificador único de la resolución dictada en los tribunales de justicia
         string lstr_IdExpediente,//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
         string lstr_IdSociedadGL,
         string lstr_EstadoResolucion,//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
         string lstr_Moneda,//La moneda en la cual se recibe el cobro. Campo obligatorio
         decimal? ldec_TipoCambio,//El tipo de cambio al momento de incluirlo en el sistema.
         decimal? ldec_MontoPrincipal,//Es el monto principal a cobrar/pagar
         decimal? ldec_MontoPrincipalColones,//Monto principal a cobrar/pagar en colones
         decimal? ldec_Intereses,
         decimal? ldec_InteresesColones,
         decimal? ldec_InteresesMoratorios,
         decimal? ldec_InteresesMoratoriosColones,
         decimal? ldec_Costas,
         decimal? ldec_CostasColones,
         decimal? ldec_DanoMoral,
         decimal? ldec_DanoMoralColones,
         string lstr_UsrModifica)
        {
            clsCobrosPagos reg_Resoluciones = new clsCobrosPagos();
            string[] lstr_result = new string[2];
            try
            {
                lstr_result = reg_Resoluciones.ModificarCobrosPagosArchivo(
                                 lint_IdRes,
                                 lint_IdCobroPagoResolucion,
                                 lstr_IdResolucion,//Identificador único de la resolución dictada en los tribunales de justicia
                                 lstr_IdExpediente,//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
                                 lstr_IdSociedadGL,
                                 lstr_EstadoResolucion,//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
                                 lstr_Moneda,//La moneda en la cual se recibe el cobro. Campo obligatorio
                                 ldec_TipoCambio,//El tipo de cambio al momento de incluirlo en el sistema.
                                 ldec_MontoPrincipal,//Es el monto principal a cobrar/pagar
                                 ldec_MontoPrincipalColones,//Monto principal a cobrar/pagar en colones
                                 ldec_Intereses,
                                 ldec_InteresesColones,
                                 ldec_InteresesMoratorios,
                                 ldec_InteresesMoratoriosColones,
                                 ldec_Costas,
                                 ldec_CostasColones,
                                 ldec_DanoMoral,
                                 ldec_DanoMoralColones,
                                 lstr_UsrModifica);//Realizamos el mappeo en la BD
            }
            catch (Exception exc)
            {
                lstr_result[0] = "99";
                lstr_result[1] = "Erorr: " + exc.Message;
            }
            return lstr_result;
        }

        [WebMethod]
        public String[] uwsModificarCobrosPagos(String str_IdExpediente, String str_IdSociedadGL, Int32 int_IdRes,
            String str_Moneda, Decimal dec_TipoCambio, Decimal dec_Tbp, Decimal dec_Tiempo, Decimal dec_TipoCambioCierre,
            Decimal? dec_MontoPrincipal, Decimal? dec_MontoPrincipalColones, Decimal? dec_MontoPrincipalCierre,
            Decimal? dec_MontoIntereses, Decimal? dec_MontoInteresesColones, Decimal? dec_MontoInteresesCierre,
            Decimal? dec_ValorPresentePrincipal, Decimal? dec_ValorPresentePrincipalColones, Decimal? dec_ValorPresentePrincipalCierre,
            Decimal? dec_ValorPresenteIntereses, Decimal? dec_ValorPresenteInteresesColones, Decimal? dec_ValorPresenteInteresesCierre,
            Decimal? dec_Intereses, Decimal? dec_InteresesColones, Decimal? dec_InteresesCierre,
            Decimal? dec_InteresesMoratorios, Decimal? dec_InteresesMoratoriosColones, Decimal? dec_InteresesMoratoriosCierre,
            Decimal? dec_Costas, Decimal? dec_CostasColones, Decimal? dec_CostasCierre,
            Decimal? dec_DanoMoral, Decimal? dec_DanoMoralColones, Decimal? dec_DanoMoralCierre,
            Decimal? dec_MontoPrincipalAnterior, Decimal? dec_MontoInteresesAnterior, Decimal? dec_InteresesAnterior, Decimal? dec_CostasAnterior, Decimal? dec_InteresesMoratoriosAnterior, Decimal? dec_DanoMoralAnterior,
            String str_UsrModifica, String str_EstadoProcesal
            )
        {
            String[] lstr_resultado = new string[2];
            clsCobrosPagos reg_clsCobrosPagos = new clsCobrosPagos();

            try
            {

                lstr_resultado = reg_clsCobrosPagos.ModificarCobrosPagos
                    (str_IdExpediente, str_IdSociedadGL, int_IdRes,
                    str_Moneda, dec_TipoCambio, dec_Tbp, dec_Tiempo, dec_TipoCambioCierre,
                    dec_MontoPrincipal, dec_MontoPrincipalColones, dec_MontoPrincipalCierre,
                    dec_MontoIntereses, dec_MontoInteresesColones, dec_MontoInteresesCierre,
                    dec_ValorPresentePrincipal, dec_ValorPresentePrincipalColones, dec_ValorPresentePrincipalCierre,
                    dec_ValorPresenteIntereses, dec_ValorPresenteInteresesColones, dec_ValorPresenteInteresesCierre,
                    dec_Intereses, dec_InteresesColones, dec_InteresesCierre,
                    dec_InteresesMoratorios, dec_InteresesMoratoriosColones, dec_InteresesMoratoriosCierre,
                    dec_Costas, dec_CostasColones, dec_CostasCierre,
                    dec_DanoMoral, dec_DanoMoralColones, dec_DanoMoralCierre,
                    dec_MontoPrincipalAnterior, dec_MontoInteresesAnterior, dec_InteresesAnterior, dec_CostasAnterior, dec_InteresesMoratoriosAnterior, dec_DanoMoralAnterior,
                    str_UsrModifica, str_EstadoProcesal);

            }
            catch (Exception err)
            {
                lstr_resultado[0] = "99";
                lstr_resultado[1] = err.Message;

            }
            return lstr_resultado;
        }

        [WebMethod]
        public String[] uwsModificarCodigoAsientoCoD(int lint_IdRes, int lint_IdCobroPagoResolucion, string lstr_IdResolucion, string lstr_IdExpediente, string lstr_IdSociedadGL,
            string lstr_CodAsiento, string lstr_UsrModifica
            )
        {
            return this.uwsModificarCodigoAsientoCo(lint_IdRes, lint_IdCobroPagoResolucion, lstr_IdResolucion, lstr_IdExpediente, lstr_IdSociedadGL,
            lstr_CodAsiento, lstr_UsrModifica);
        }

        [WebMethod]
        public String[] uwsModificarCodigoAsientoCo(int? lint_IdRes, int? lint_IdCobroPagoResolucion, string lstr_IdResolucion, string lstr_IdExpediente, string lstr_IdSociedadGL,
            string lstr_CodAsiento, string lstr_UsrModifica
            )
        {
            String[] lstr_resultado = new string[2];
            bool lbln_res = true;
            clsCobrosPagos reg_clsCobrosPagos = new clsCobrosPagos();
            
            try
            {

                lbln_res = reg_clsCobrosPagos.ModificarCodigoAsiento(lint_IdRes, lint_IdCobroPagoResolucion, lstr_IdResolucion, lstr_IdExpediente, lstr_IdSociedadGL,
                                 lstr_CodAsiento, lstr_UsrModifica,out lstr_resultado[0],out lstr_resultado[1]);

            }
            catch (Exception err)
            {
                lstr_resultado[0] = "99";
                lstr_resultado[1] = err.Message;

            }
            return lstr_resultado;
        }

        [WebMethod]
        public DataSet uwsConsultarCobrosPagos(
            String str_IdExpediente,
            String str_IdSociedadGL,
            Int32 int_IdExp,
            Int32 int_IdRes,
            string str_EstadoResolucion,
            DateTime? dt_FchInicio,
            DateTime? dt_FchFin)
        {
            String[] lstr_resultado = new string[2];
            DataSet lstr_result = new DataSet();
            clsCobrosPagos reg_clsCobrosPagos = new clsCobrosPagos();

            try
            {
                lstr_result = reg_clsCobrosPagos.ConsultarCobrosPagos
                    (str_IdExpediente, str_IdSociedadGL, int_IdExp, int_IdRes, str_EstadoResolucion, dt_FchInicio, dt_FchFin);

            }
            catch (Exception err)
            {
                lstr_resultado[0] = err.Message;
                lstr_result = null;
                log.Error("Error " + err.Message);

            }
            return lstr_result;
        }
        /// <summary>
        /// PErmite realizar consultas generales para el modulo de contingentes
        /// </summary>
        /// <param name="str_consulta"></param>
        /// <returns></returns>
        [WebMethod]
        public DataTable uwsConsultasGeneralesExpedientes(string str_consulta)
        {

            clsExpedientes reg_ConsultExp = new clsExpedientes();
            DataTable lstr_result = new DataTable();
            try
            {

                lstr_result = reg_ConsultExp.ConsultasGeneralesExpedientes(str_consulta);

            }
            catch (Exception err)
            {
                lstr_result = null;
                log.Error("Error " + err.Message);

            }
            return lstr_result;
        }

        [WebMethod]
        public decimal[] uwsObtenerMontosProvision(string str_idExpediente, string str_estadopProvision)
        {

            clsResoluciones resol_Exp = new clsResoluciones();
            decimal[] lstr_result = new decimal[3];
            try
            {

                lstr_result = resol_Exp.ObtenerMontoResolucion(str_idExpediente, str_estadopProvision);

            }
            catch (Exception err)
            {
                lstr_result = null;
                log.Error("Error " + err.Message);

            }
            return lstr_result;
        }

        [WebMethod]
        public string[] uwsRegistrarBitacoraMovimientosCuentasExpedientes(string str_IdExpediente, string str_IdModulo, string str_Sociedad, string str_NroAsiento, string lstr_TipoCuenta, decimal dec_Debe, decimal dec_Haber, decimal dec_Monto, decimal dec_SaldoAcumulado, string str_DetalleTransaccion, string str_UsrCreacion)
        {

            clsBitacoraDeMovimientosCuentasExpedientes reg_Bitacora = new clsBitacoraDeMovimientosCuentasExpedientes();
            string[] lstr_result = new string[2];
            try
            {

                lstr_result = reg_Bitacora.RegistrarBitacoraDeMovimientosCuentasExpedientes(str_IdExpediente, str_IdModulo, str_Sociedad, str_NroAsiento, lstr_TipoCuenta, dec_Debe, dec_Haber, dec_Monto, dec_SaldoAcumulado, str_DetalleTransaccion, str_UsrCreacion);

            }
            catch (Exception err)
            {
                lstr_result[0] = err.Message;

            }
            return lstr_result;
        }

        [WebMethod]
        public string[] uwsCrearAntiguedadDeSaldos(
            int? lint_IdAntiguedadSaldos, int? lint_IdPrevisionIncobrables, string lstr_IdResolucion, string lstr_IdExpediente,
            string lstr_DescripcionVencimiento, int? lint_DiasDeCuenta, int? lint_MesesDeCuenta, decimal? ldec_MontoIncobrable, decimal? ldec_DiferenciaAjustar,
            decimal? ldec_PorcentajeIncobrable, string lstr_Estado, string lstr_Usuario
            )
        {
            clsAntiguedadDeSaldos reg_Antiguedad = new clsAntiguedadDeSaldos();
            string[] lstr_result = new string[3];
            Boolean res = false;
            int? int_TmpIdAntiguedadSaldos = null;
            try
            {
                //ultimo codigo modificado 05/11/2015
                res = reg_Antiguedad.CrearAntiguedadDeSaldos(lint_IdAntiguedadSaldos, lint_IdPrevisionIncobrables, lstr_IdResolucion, lstr_IdExpediente,
            lstr_DescripcionVencimiento, lint_DiasDeCuenta, lint_MesesDeCuenta, ldec_MontoIncobrable, ldec_DiferenciaAjustar,
            ldec_PorcentajeIncobrable, lstr_Estado, lstr_Usuario, out lstr_result[0], out lstr_result[1], out int_TmpIdAntiguedadSaldos);

                lstr_result[2] = int_TmpIdAntiguedadSaldos.ToString();
            }
            catch (Exception err)
            {
                lstr_result[0] = "99";
                lstr_result[1] = "Error en WS: " + err.Message;

            }
            return lstr_result;
        }

        [WebMethod]
        public string[] uwsRegistrarLiquidacion(
            string str_IdExpediente, string str_IdSociedadGL, string str_EstadoResolucion,
            DateTime? dt_FchResolucion,
            DateTime? dt_FchFallo,
            string str_ResolucionDictada,
            int int_CxCaCxP,
            string str_EstadoProcesal, string str_Estado,
            string srt_Moneda, decimal dec_TipoCambio,
            decimal dec_Intereses, decimal dec_InteresesColones,
            decimal dec_InteresesMoratorios, decimal dec_InteresesMoratoriosColones,
            decimal dec_Costas, decimal dec_CostasColones,
            decimal dec_DannoMoral, decimal dec_DannoMoralColones,
            string str_TipoTransaccion, string str_EstadoTransaccion,
            string str_Observacion,
            string str_UsrCreacion
            )
        {

            clsLiquidacion reg_Resoluciones = new clsLiquidacion();
            string[] lstr_result = new string[2];
            try
            {
                //ultimo codigo modificado 05/11/2015
                lstr_result = reg_Resoluciones.RegistrarLiquidacion(str_IdExpediente, str_IdSociedadGL, str_EstadoResolucion,
            dt_FchResolucion,
            dt_FchFallo,
            str_ResolucionDictada,
            int_CxCaCxP,
            str_EstadoProcesal, str_Estado,
            srt_Moneda, dec_TipoCambio,
            dec_Intereses, dec_InteresesColones,
            dec_InteresesMoratorios, dec_InteresesMoratoriosColones,
            dec_Costas, dec_CostasColones,
            dec_DannoMoral, dec_DannoMoralColones,
            str_TipoTransaccion, str_EstadoTransaccion,
            str_Observacion,
            str_UsrCreacion
            );

            }
            catch (Exception err)
            {
                lstr_result[0] = "99";
                lstr_result[1] = "Error en WS: " + err.Message;

            }
            return lstr_result;
        }

        [WebMethod]
        public string[] uwsModificarLiquidacion(
            string str_IdExpediente, string str_IdSociedadGL, string str_EstadoResolucion,
            DateTime? dt_FchResolucion,
            DateTime? dt_FchFallo,
            string str_ResolucionDictada,
            int int_CxCaCxP,
            string srt_Moneda, decimal dec_TipoCambio,
            decimal dec_Intereses, decimal dec_InteresesColones,
            decimal dec_InteresesMoratorios, decimal dec_InteresesMoratoriosColones,
            decimal dec_Costas, decimal dec_CostasColones,
            decimal dec_DannoMoral, decimal dec_DannoMoralColones,
            string str_ObservacionesLiq, string str_Estado, string str_UsrModifica,
            string str_EstadoProcesal, string str_TipoTransaccion, string str_EstadoTransaccion,
            string str_UsrCreacion
            )
        {

            clsLiquidacion reg_Resoluciones = new clsLiquidacion();
            string[] lstr_result = new string[2];
            try
            {
                //ultimo codigo modificado 05/11/2015
                lstr_result = reg_Resoluciones.ModificarLiquidacion(
                    str_IdExpediente, str_IdSociedadGL, str_EstadoResolucion,
                    dt_FchResolucion,
                    dt_FchFallo,
                    str_ResolucionDictada,
                    int_CxCaCxP,
                    srt_Moneda, dec_TipoCambio,
                    dec_Intereses, dec_InteresesColones,
                    dec_InteresesMoratorios, dec_InteresesMoratoriosColones,
                    dec_Costas, dec_CostasColones,
                    dec_DannoMoral, dec_DannoMoralColones,
                    str_ObservacionesLiq, str_Estado, str_UsrModifica,
                    str_EstadoProcesal, str_TipoTransaccion, str_EstadoTransaccion,
                    str_UsrCreacion);

            }
            catch (Exception err)
            {
                lstr_result[0] = err.Message;

            }
            return lstr_result;
        }


        #endregion

        #region Consultas

        /// <summary>
        /// consultar expedientes por numero
        /// </summary>
        /// <param name="NumeroExpediente"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet uwsConsultarExpedienteXNumero(string NumeroExpediente, string sociedad)
        {
            DataSet lstr_result = new DataSet();
            clsExpedientes rev_Exp = new clsExpedientes();
            try
            {

                lstr_result = rev_Exp.ConsultarExpedienteXNumExp(NumeroExpediente, sociedad);//Con parametro trae el filtro
            }
            catch (Exception err)
            {
                lstr_result = new DataSet();
            }
            return lstr_result;

        }

        [WebMethod]
        public string uwsConsultarExistenciaProvision(string estadoProvision)
        {
            string lstr_result = string.Empty;
            clsResoluciones resol = new clsResoluciones();
            try
            {

                lstr_result = resol.VerificarExisteProvision(estadoProvision);//Con parametro trae el filtro
            }
            catch (Exception err)
            {
                lstr_result = "Error :" + err.Message;
            }
            return lstr_result;

        }

        /// <summary>
        /// consultar expedientes por fecha de creacion
        /// </summary>
        /// <param name="NumeroExpediente"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet uwsConsultarExpedienteXFecha(string FechaInicio, string FechaFin, string sociedad)
        {
            DataSet lstr_result = new DataSet();
            clsExpedientes rev_Exp = new clsExpedientes();
            try
            {

                lstr_result = rev_Exp.ConsultarExpedienteXFecha(FechaInicio, FechaFin, sociedad);//Con parametro trae el filtro
            }
            catch (Exception err)
            {
                lstr_result = new DataSet();
            }
            return lstr_result;

        }

        /// <summary>
        /// Obtiene todos los expedientes 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataSet uwsConsultarExpediente(string sociedad)
        {
            DataSet lstr_result = new DataSet();
            clsExpedientes con_Expediente = new clsExpedientes();
            try
            {
                lstr_result = con_Expediente.ConsultarExpedientesEntidadesPersonas(sociedad);
            }
            catch (Exception exc)
            {
                lstr_result = new DataSet();
                log.Error("Error mensaje en metodo de Consultas Expedientes");
            }


            return lstr_result;
        }
        #endregion
        #endregion

        #region Archivos
        [WebMethod]
        public string[] uwsEnviarCorreoAttach(string str_CorreoDestino, string str_CuerpoCorreo, string str_Asunto, string str_NombreArch, byte[] b_data, string str_Usuario)
        {
            string[] lstr_Resultado = new string[2];
            clsArchivoSubir l_archivo = new clsArchivoSubir();

            try
            {

                clsMailAttachment[] Arr_ma = new clsMailAttachment[1];
                clsMailAttachment ma = new clsMailAttachment(b_data, str_NombreArch);

                Arr_ma[0] = ma;
                usr_Envio.EnviarCorreoAttach(DatosConexion(), str_CorreoDestino, str_CuerpoCorreo, str_Asunto, Arr_ma);
                //Log.Info("Comprobante de pago de formulario " + Convert.ToString(int_Anno) + "." + Convert.ToString(int_IdFormulario) + " Enviado a " + ds_Sociedad.Tables["Table"].Rows[0]["CorreoNotifica"].ToString());

            }
            catch (Exception ex)
            {
                this.uwsRegistrarAccionBitacora("SG", str_Usuario, str_Asunto, ex.ToString());
                gcls_Seguridad.SaveError(ex);
            }
            return lstr_Resultado;

        }

        [WebMethod]
        public string[] uwsGuardarArchivo(string str_nombre, string str_tipoContenido,
            int int_size, byte[] b_data, long str_IdRevelacion, string str_IdResolucion, int int_IdFormulario, string str_IdExpediente, long lg_IdRevelacionPediente, string str_IdCobroPago, Int16 int_Anno, string str_userCreacion)
        {
            string[] lstr_Resultado = new string[2];
            clsArchivoSubir l_archivo = new clsArchivoSubir();

            try
            {

                lstr_Resultado = l_archivo.ufnGuardarArchivo(str_nombre, str_tipoContenido,
            int_size, b_data, str_IdRevelacion, str_IdResolucion, int_IdFormulario, str_IdExpediente, lg_IdRevelacionPediente, str_IdCobroPago, int_Anno, str_userCreacion);

            }
            catch (Exception ex)
            {
                this.uwsRegistrarAccionBitacora("SG", str_userCreacion, "Guardar Archivo", ex.ToString());
                gcls_Seguridad.SaveError(ex);
            }
            return lstr_Resultado;

        }

        [WebMethod]
        public string[] uwsGuardarArchivoContingente(string str_nombre, string str_tipoContenido,
            int int_size, byte[] b_data, long str_IdRevelacion, string str_IdResolucion, string str_IdSociedadGL, int int_IdFormulario, string str_IdExpediente, long lg_IdRevelacionPediente, string str_IdCobroPago, Int16 int_Anno, string str_userCreacion)
        {
            string[] lstr_Resultado = new string[2];
            clsArchivoSubir l_archivo = new clsArchivoSubir();

            try
            {
                lstr_Resultado = l_archivo.ufnGuardarArchivoContingente(str_nombre, str_tipoContenido,
            int_size, b_data, str_IdRevelacion, str_IdResolucion, str_IdSociedadGL, int_IdFormulario, str_IdExpediente, lg_IdRevelacionPediente, str_IdCobroPago, int_Anno, str_userCreacion);

            }
            catch (Exception ex)
            { }
            return lstr_Resultado;

        }

        [WebMethod]
        public string[] uwsGuardarArchivos(string str_nombre, string str_tipoContenido,
            int int_size, byte[] b_data, long str_IdRevelacion, string str_IdResolucion, int int_IdFormulario, string str_IdExpediente, long lg_IdRevelacionPediente, long lg_IdArchivoDeuda, string str_IdCobroPago, Int16 int_Anno, string str_userCreacion)
        {
            string[] lstr_Resultado = new string[2];
            clsArchivoSubir l_archivo = new clsArchivoSubir();

            try
            {

                lstr_Resultado = l_archivo.ufnGuardarArchivos(str_nombre, str_tipoContenido,
            int_size, b_data, str_IdRevelacion, str_IdResolucion, int_IdFormulario, str_IdExpediente, lg_IdRevelacionPediente, lg_IdArchivoDeuda, str_IdCobroPago, int_Anno, str_userCreacion);
                if (int_IdFormulario > 0 && int_Anno > 0 && lstr_Resultado[0] == "00")
                {
                    ////para enviar el email
                    //tUsuario usr_Envio = new tUsuario();
                    ////para sacar a quién se debe enviar la notificación
                    //tSociedadGL soc_Consulta = new tSociedadGL();
                    ////Para sacar la sociedad gl del formulario de captura
                    //clsFormulariosCapturaIngresos fci_Formulario = new clsFormulariosCapturaIngresos();
                    DataSet ds_Formulario = new DataSet();
                    DataSet ds_Sociedad = new DataSet();

                    ds_Formulario = fci_Formulario.ConsultarFormulariosCapturaIngresos(int_IdFormulario, int_Anno, "", "", "", "", "", "", "", "", "", "");

                    ds_Sociedad = soc_Consulta.ConsultarSociedadesGL(ds_Formulario.Tables["Table"].Rows[0]["IdSociedadGL"].ToString(), "", "", "");

                    if (lstr_Resultado[0] == "00")
                        usr_Envio.EnviarCorreo(DatosConexion(), ds_Sociedad.Tables["Table"].Rows[0]["CorreoNotifica"].ToString(), "Se ha recibido el comprobante de pago del formulario " + Convert.ToString(int_IdFormulario) + " del Año " + Convert.ToString(int_Anno), "Comprobante de Pago cargado al Sistema Gestor");
                    //else
                    //    usr_Envio.EnviarCorreo(ds_Sociedad.Tables["Table"].Rows[0]["CorreoNotifica"].ToString(), "Se ha recibido el comprobante de pago del formulario " + Convert.ToString(int_IdFormulario) + " del Año " + Convert.ToString(int_Anno), "Comprobante de Pago no pudo ser cargado al Sistema Gestor");
                }

            }
            catch (Exception ex)
            { }
            return lstr_Resultado;

        }

        [WebMethod]
        public DataSet uwsObtenerListaArchivos()
        {
            DataSet lstr_Resultado = new DataSet();
            clsArchivoSubir l_archivo = new clsArchivoSubir();
            try
            {
                lstr_Resultado = l_archivo.ufnObtenerListaArchivo();
            }
            catch (Exception ex)
            { }
            return lstr_Resultado;

        }

        [WebMethod]
        public DataSet uwsObtenerArchivoPorIdResolucion(String str_IdExpediente, String str_IdSociedad, int int_IdFormulario)
        {
            DataSet lstr_Resultado = new DataSet("ArchivoResolucion");

            clsArchivoSubir l_archivo = new clsArchivoSubir();
            try
            {

                lstr_Resultado = l_archivo.ufnObtenerArchivoPorIdResolucion(str_IdExpediente, str_IdSociedad, int_IdFormulario);
            }
            catch (Exception ex)
            {
                //lstr_Resultado[0] = Convert.ToChar(ex.Message);

            }
            return lstr_Resultado;

        }

        [WebMethod]
        public string[] uwsEliminarArchivo(string str_IdArchivo, string dat_FchModifica)
        {
            string[] lstr_ResEliminacion = new string[2];
            lstr_ResEliminacion[0] = "99";
            lstr_ResEliminacion[1] = "Error ejecutando WS";
            try
            {
                clsArchivoSubir lcls_Archivos = new clsArchivoSubir();
                lstr_ResEliminacion = lcls_Archivos.ufnEliminarArchivo(str_IdArchivo, dat_FchModifica);
            }
            catch (Exception ex)
            { }
            return lstr_ResEliminacion;
        }

        [WebMethod]
        public DataSet uwsObtenerArchivoPorIdRvelacion(string str_IdRevelacion)
        {
            DataSet lstr_Resultado = new DataSet("ArchivoResolucion");

            clsArchivoSubir l_archivo = new clsArchivoSubir();
            try
            {

                lstr_Resultado = l_archivo.ufnObtenerArchivoPorIdRevelacion(str_IdRevelacion);
            }
            catch (Exception ex)
            {
                //lstr_Resultado[0] = Convert.ToChar(ex.Message);

            }
            return lstr_Resultado;

        }

        [WebMethod]
        public DataSet uwsObtenerArchivoPorIdRvelacionPendiente(string str_IdRevelacionPendiente)
        {
            DataSet lstr_Resultado = new DataSet("ArchivoResolucion");

            clsArchivoSubir l_archivo = new clsArchivoSubir();
            try
            {

                lstr_Resultado = l_archivo.ufnObtenerArchivoPorIdRevelacionPendiente(str_IdRevelacionPendiente);
            }
            catch (Exception ex)
            {
                //lstr_Resultado[0] = Convert.ToChar(ex.Message);

            }
            return lstr_Resultado;

        }


        [WebMethod]
        public DataSet uwsObtenerArchivoPorId(string str_IdArchivo)
        {
            DataSet lstr_Resultado = new DataSet("ArchivoResolucion");

            clsArchivoSubir l_archivo = new clsArchivoSubir();
            try
            {

                lstr_Resultado = l_archivo.ufnObtenerArchivoPorId(str_IdArchivo);
            }
            catch (Exception ex)
            {
                //lstr_Resultado[0] = Convert.ToChar(ex.Message);

            }
            return lstr_Resultado;

        }

        [WebMethod]
        public DataSet uwsObtenerArchivoPorIdArchivoDeuda(string str_IdArchivoDeuda)
        {
            DataSet lstr_Resultado = new DataSet("ArchivoResolucion");

            clsArchivoSubir l_archivo = new clsArchivoSubir();
            try
            {

                lstr_Resultado = l_archivo.ufnObtenerArchivoPorIdArchivoDeuda(str_IdArchivoDeuda);
            }
            catch (Exception ex)
            {
                //lstr_Resultado[0] = Convert.ToChar(ex.Message);

            }
            return lstr_Resultado;

        }

        [WebMethod]
        public DataSet uwsObtenerArchivoCapturaIngresos(int? int_IdFormulario, Int16? int_Anno)
        {
            DataSet lstr_Resultado = new DataSet("ArchivoCapturaIngresos");

            clsArchivoSubir l_archivo = new clsArchivoSubir();
            try
            {
                lstr_Resultado = l_archivo.ufnObtenerArchivoCapturaIngresos(int_IdFormulario, int_Anno);
            }
            catch (Exception ex)
            {
                //lstr_Resultado[0] = Convert.ToChar(ex.Message);

            }
            return lstr_Resultado;

        }
        #endregion

        #region Revelación y Notas

        [WebMethod]
        public DataSet uwsConsultarArchivosDeuda(string lint_IdArchivoDeuda, string lint_Anno, string lint_Mes, string lstr_Categoria)
        {
            DataSet lds_ArchivosDeuda = new DataSet();
            string lstr_MensajeSalida = String.Empty;
            string mensaje = "";
            Int64? IdArchivoDeuda = null;
            Int16? Anno = null;
            Int16? Mes = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdArchivoDeuda))
                        IdArchivoDeuda = Convert.ToInt64(lint_IdArchivoDeuda);
                    if (!string.IsNullOrEmpty(lint_Anno))
                        Anno = Convert.ToInt16(lint_Anno);

                    if (!string.IsNullOrEmpty(lint_Mes))
                        Mes = Convert.ToInt16(lint_Mes);
                }
                catch (Exception ex)
                {
                    mensaje = "Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsNotasCalculosFinancieros lcls_CalculosFinancieros = new clsNotasCalculosFinancieros();
                    lds_ArchivosDeuda = lcls_CalculosFinancieros.ConsultarArchivosDeuda(IdArchivoDeuda, Anno, Mes, lstr_Categoria);
                }

            }
            catch (Exception ex)
            {

            }
            return lds_ArchivosDeuda;
        }

        [WebMethod]
        public DataSet uwsConsultarCategoriasNotas()
        {
            DataSet lds_CategoriasNotas = new DataSet();
            string lstr_MensajeSalida = String.Empty;
            try
            {
                clsNotasCalculosFinancieros lcls_CalculosFinancieros = new clsNotasCalculosFinancieros();
                lds_CategoriasNotas = lcls_CalculosFinancieros.ConsultarCategoriasNotas();
            }
            catch (Exception ex)
            {

            }
            return lds_CategoriasNotas;
        }

        [WebMethod]
        public string[] uwsCrearArchivoDeuda(string lstr_IdModulo, string lint_Mes, string lint_Anno, string lint_IdOpcionCategoria, string lstr_Usuario)
        {
            string[] str_Resultado = new string[2];
            str_Resultado[0] = "false";
            string lstr_CodigoSalida = String.Empty;
            string lstr_MensajeSalida = String.Empty;
            int lint_IdArchivoTemporal;
            DateTime ldt_Fecha = DateTime.Now;
            try
            {
                clsNotasCalculosFinancieros lcls_NotasCalculosFinancieros = new clsNotasCalculosFinancieros();
                lcls_NotasCalculosFinancieros.CrearArchivoDeuda(lstr_IdModulo, Convert.ToInt16(lint_Mes), Convert.ToInt16(lint_Anno),
                    Convert.ToInt32(lint_IdOpcionCategoria),
                    lstr_Usuario, out lstr_CodigoSalida, out lstr_MensajeSalida, out lint_IdArchivoTemporal);
                log.Info(lstr_MensajeSalida);
                str_Resultado[0] = "true";
                str_Resultado[1] = lint_IdArchivoTemporal.ToString();
            }
            catch (Exception ex)
            {

            }
            return str_Resultado;
        }

        /// <summary>
        /// Consulta de revelacion
        /// </summary>
        /// <param name="str_IdRevelacion">Identificador de revelacion</param>
        /// <returns>Dataset con datos de formularios</returns>
        [WebMethod]
        public DataSet uwsConsultarFormulario(string str_IdRevelacion)
        {
            DataSet lds_Formularios = new DataSet();
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tRevelacionNotas ltr_RevelacionNotas = new tRevelacionNotas();
                lds_Formularios = ltr_RevelacionNotas.ufnConsultarFormulario(str_IdRevelacion, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return lds_Formularios;
        }

        /// <summary>
        /// Busqueda de formularios segun criterios indicados por el usuario
        /// </summary>
        /// <param name="str_IdRevelacion">Identificador de revelacion</param>
        /// <param name="str_PeriodoMensual">Periodo comprendido entre el 1 al 30 o 31 de  cada mes</param>
        /// <param name="str_GrupoCuentas">Grupo de cuentas del Plan de Cuentas Contables</param>
        /// <param name="str_Cuentas">Detalle de subcuentas anexas que integran la subcuenta anexa del Plan de Cuentas Contables</param>
        /// <returns></returns>
        [WebMethod]
        public DataSet uwsBuscarFormulario(string str_IdRevelacion, string str_PeriodoMensual, string str_GrupoCuentas,
                string str_Cuentas, string str_UsrCreacion, string str_Institucion, string str_Annio)
        {
            DataSet lds_Formulario = new DataSet();
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tRevelacionNotas ltr_RevelacionNotas = new tRevelacionNotas();
                lds_Formulario = ltr_RevelacionNotas.ufnBuscarFormulario(str_IdRevelacion.Trim(), str_PeriodoMensual.Trim(),
                    str_GrupoCuentas.Trim(), str_Cuentas, str_UsrCreacion, str_Institucion, str_Annio, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message.ToString());
            }
            return lds_Formulario;
        }

        [WebMethod]
        public DataSet uwsConsultarRevelacionPendiente(string str_IdRevelacionPendiente, string str_PeriodoMensual, string str_GrupoCuentas,
            string str_Cuentas, string str_UsrCreacion, string str_Institucion, string str_PeriodoAnual)
        {
            DataSet lds_Formulario = new DataSet();
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tRevelacionNotas ltr_RevelacionNotas = new tRevelacionNotas();
                lds_Formulario = ltr_RevelacionNotas.ufnConsultarRevelacionPendiente(str_IdRevelacionPendiente, str_PeriodoMensual,
                    str_GrupoCuentas, str_Cuentas, str_UsrCreacion, str_Institucion, str_PeriodoAnual, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message.ToString());
            }
            return lds_Formulario;
        }

        [WebMethod]
        public DataSet uwsConsultarRevelacionContingente(string str_IdRevCont, string str_PeriodoAnual, string str_PeriodoMensual)
        {
            DataSet lds_Formulario = new DataSet();
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tRevelacionNotas ltr_RevelacionNotas = new tRevelacionNotas();
                lds_Formulario = ltr_RevelacionNotas.ufnConsultarRevelacionContingente(str_IdRevCont, str_PeriodoAnual,
                    str_PeriodoMensual, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message.ToString());
            }
            return lds_Formulario;
        }

        [WebMethod]
        public DataSet uwsConsultarRevelacionContSoc(string str_IdRevCont, string str_PeriodoAnual, string str_PeriodoMensual,
            string str_IdSociedadGL, string str_TipoProceso)
        {
            DataSet lds_Formulario = new DataSet();
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tRevelacionNotas ltr_RevelacionNotas = new tRevelacionNotas();
                lds_Formulario = ltr_RevelacionNotas.ufnConsultarRevelacionContSoc(str_IdRevCont, str_PeriodoAnual,
                    str_PeriodoMensual, str_IdSociedadGL, str_TipoProceso, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message.ToString());
            }
            return lds_Formulario;
        }

        [WebMethod]
        public DataSet uwsBuscarRevelacionContingente(string str_IdRevCont, string str_PeriodoAnual, string str_PeriodoMensual)
        {
            DataSet lds_Formulario = new DataSet();
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tRevelacionNotas ltr_RevelacionNotas = new tRevelacionNotas();
                lds_Formulario = ltr_RevelacionNotas.ufnBuscarRevelacionContingente(str_IdRevCont, str_PeriodoAnual,
                    str_PeriodoMensual, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message.ToString());
            }
            return lds_Formulario;
        }

        [WebMethod]
        public string[] uwsActualizarObservacionesRevCont(string str_IdRevCont, string str_IdSociedadGL,
            string str_TipoProceso, string str_Observacion, string str_UsrModifica, string str_FchModifica)
        {
            string[] str_Resultado = new string[2];
            try
            {
                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                str_Resultado = ltrn_RevelacionNotas.ufnActualizarObservacionesRevCont(str_IdRevCont, str_IdSociedadGL,
                    str_TipoProceso, str_Observacion, str_UsrModifica, str_FchModifica);
                log.Info(str_Resultado[1]);
            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                log.Error("Error en Web service");
            }
            return str_Resultado;
        }

        [WebMethod]
        public string[] uwsActualizarRevConTotalPasivos(string str_IdRevCont, string str_IdSociedadGL,
            string str_TipoProceso, string str_MontoPasivos, string str_CantExpPasivos, string str_MontoActivos, DateTime? str_FchModifica, Nullable<Int32> int_Proceso)
        {
            string[] str_Resultado = new string[2];
            try
            {
                if (String.IsNullOrEmpty(str_MontoActivos))
                    str_MontoActivos = "0";
                if (String.IsNullOrEmpty(str_MontoPasivos))
                    str_MontoActivos = "0";

                Decimal dec_MontosActivos = Convert.ToDecimal(str_MontoActivos, CultureInfo.InvariantCulture);
                Decimal dec_MontosPasivos = Convert.ToDecimal(str_MontoPasivos, CultureInfo.InvariantCulture);

                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                str_Resultado = ltrn_RevelacionNotas.ufnActualizarRevConTotalPasivos(str_IdRevCont, str_IdSociedadGL,
                    str_TipoProceso, dec_MontosPasivos, str_CantExpPasivos, dec_MontosActivos, str_FchModifica, int_Proceso);
                log.Info(str_Resultado[1]);
            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                str_Resultado[1] = str_Resultado[1] + ex.Message;
                log.Error("Error en Web service");
            }
            return str_Resultado;
        }

        [WebMethod]
        public string[] uwsCrearFormularioAnterior(string str_Anno, string str_Mes, string str_Institucion, string str_Entidad, string str_IdOficina, string str_PlanCuentas, string str_ClaseCuenta,
            string str_Cuentas, string str_Concepto, string str_Justificacion, string str_NumExpediente, string str_HabilitadaPretencion, string str_EstadoRevelacion,
            string str_UsrCreacion, string str_RubroCuenta, string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
        {
            string[] str_Resultado = new string[2];
            string lstr_MensajeSalida = String.Empty;
            DateTime ldt_Fecha = DateTime.Now;
            try
            {
                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                str_Resultado = ltrn_RevelacionNotas.ufnCrearFormulario(str_Anno, str_Mes,
                    str_Institucion, str_Entidad, str_IdOficina, str_PlanCuentas, str_ClaseCuenta, str_Cuentas, str_Concepto, str_Justificacion,
                    str_NumExpediente, str_HabilitadaPretencion, str_EstadoRevelacion, str_UsrCreacion, out lstr_MensajeSalida,
                    str_RubroCuenta, str_SubCuenta, str_SubCuentaAnexa, str_AuxiliarCuenta);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                log.Error(lstr_MensajeSalida);
            }
            return str_Resultado;
        }

        [WebMethod]
        public string[] uwsActualizarRevConTotalActivos(string str_IdRevCont, string str_IdSociedadGL,
            string str_TipoProceso, string str_MontoActivos, string str_CantExpActivos, string str_MontoPasivos, DateTime? str_FchModifica, Nullable<Int32> int_Proceso)
        {
            string[] str_Resultado = new string[2];
            try
            {
                if (String.IsNullOrEmpty(str_MontoActivos))
                    str_MontoActivos = "0";
                if (String.IsNullOrEmpty(str_MontoPasivos))
                    str_MontoActivos = "0";

                Decimal dec_MontosActivos = Convert.ToDecimal(str_MontoActivos, CultureInfo.InvariantCulture);
                Decimal dec_MontosPasivos = Convert.ToDecimal(str_MontoPasivos, CultureInfo.InvariantCulture);

                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                str_Resultado = ltrn_RevelacionNotas.ufnActualizarRevConTotalActivos(str_IdRevCont, str_IdSociedadGL,
                    str_TipoProceso, dec_MontosActivos, str_CantExpActivos, dec_MontosPasivos, str_FchModifica, int_Proceso);
                log.Info(str_Resultado[1]);
            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                str_Resultado[1] = str_Resultado[1] + ex.Message;
                log.Error("Error en Web service");
            }
            return str_Resultado;
        }

        /// <summary>
        /// Metodo web encargado de la creacion de una nueva revelacion
        /// </summary>
        /// <param name="str_IdRevelacion">Identificador de revelacion</param>
        /// <param name="str_PeriodoAnual"></param>
        /// <param name="str_PeriodoMensual">Periodo comprendido entre el 1 al 30 o 31 de  cada mes</param>
        /// <param name="str_Institucion">Unidad Primaria de Registro</param>
        /// <param name="str_Entidad">Nombre de la Unidad Primaria de Registro</param>
        /// <param name="str_GrupoCuentas">Grupo de cuentas del Plan de Cuentas Contables</param>
        /// <param name="str_Cuentas">Detalle de subcuentas anexas que integran la subcuenta anexa del Plan de Cuentas Contables</param>
        /// <param name="str_Concepto">Explicación general del concepto </param>
        /// <param name="str_Justificacion">Explicación específica del registro o revelación con mayor detalle</param>
        /// <param name="str_NumExpediente"></param>
        /// <param name="str_HabilitadaPretencion"></param>
        /// <param name="str_EstadoRevelacion"></param>
        /// <param name="str_UsrCreacion"></param>
        /// <returns></returns>
        [WebMethod]
        public string[] uwsCrearFormulario(string str_Institucion, string str_Entidad, string str_IdOficina, string str_PlanCuentas, string str_ClaseCuenta,
            string str_Cuentas, string str_Concepto, string str_Justificacion, string str_NumExpediente, string str_HabilitadaPretencion, string str_EstadoRevelacion,
            string str_UsrCreacion, string str_RubroCuenta, string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
        {
            string[] str_Resultado = new string[2];
            string lstr_MensajeSalida = String.Empty;
            DateTime ldt_Fecha = DateTime.Now;
            try
            {
                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                str_Resultado = ltrn_RevelacionNotas.ufnCrearFormulario(Convert.ToString(ldt_Fecha.Year), ObtenerMesAnno(ldt_Fecha),
                    str_Institucion, str_Entidad, str_IdOficina, str_PlanCuentas, str_ClaseCuenta, str_Cuentas, str_Concepto, str_Justificacion,
                    str_NumExpediente, str_HabilitadaPretencion, str_EstadoRevelacion, str_UsrCreacion, out lstr_MensajeSalida,
                     str_RubroCuenta,  str_SubCuenta,  str_SubCuentaAnexa,  str_AuxiliarCuenta);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                log.Error(lstr_MensajeSalida);
            }
            return str_Resultado;
        }

        [WebMethod]
        public string[] uwsCrearRevelacionPendiente(string str_PeriodoAnual, string str_PeriodoMensual, string str_Institucion,
            string str_Entidad, string str_IdOficina, string str_PlanCuentas, string str_ClaseCuenta, string str_Cuentas,
            string str_Concepto, string str_Justificacion, string str_EstadoRevelacion, string str_UsrCreacion,
               string str_RubroCuenta, string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
        {
            string[] str_Resultado = new string[2];
            string lstr_MensajeSalida = String.Empty;
            DateTime ldt_Fecha = DateTime.Now;
            try
            {
                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                str_Resultado = ltrn_RevelacionNotas.ufnCrearRevelacionPendiente(str_PeriodoAnual, str_PeriodoMensual,
                 str_Institucion, str_Entidad, str_IdOficina, str_PlanCuentas, str_ClaseCuenta, str_Cuentas,
                 str_Concepto, str_Justificacion, str_EstadoRevelacion,
                 str_UsrCreacion, out lstr_MensajeSalida, str_RubroCuenta, str_SubCuenta, str_SubCuentaAnexa, str_AuxiliarCuenta);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                log.Error(lstr_MensajeSalida);
            }
            return str_Resultado;
        }


        [WebMethod]
        public string[] uwsModificarRevelacionPendiente(string str_IdRevelacionPendiente, string str_Institucion,
            string str_Entidad, string str_IdOficina, string str_PlanCuentas, string str_ClaseCuenta, string str_Cuentas,
            string str_Concepto, string str_Justificacion, string str_EstadoRevelacion,
            string str_FchModifica, string str_UsrModifica,string str_RubroCuenta,string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
        {
            string[] str_Resultado = new string[2];
            string lstr_MensajeSalida = String.Empty;
            DateTime ldt_Fecha = DateTime.Now;
            try
            {
                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                str_Resultado = ltrn_RevelacionNotas.ufnModificarRevelacionPendiente(str_IdRevelacionPendiente, str_Institucion,
                 str_Entidad, str_IdOficina, str_PlanCuentas, str_ClaseCuenta, str_Cuentas,
                 str_Concepto, str_Justificacion, str_EstadoRevelacion,
                 str_FchModifica, str_UsrModifica, out lstr_MensajeSalida, str_RubroCuenta, str_SubCuenta, str_SubCuentaAnexa, str_AuxiliarCuenta);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                log.Error(lstr_MensajeSalida);
            }
            return str_Resultado;
        }

        [WebMethod]
        public string[] uwsEliminarRevelacionPendiente(string str_IdRevelacionPendiente)
        {
            string[] str_Resultado = new string[2];
            string lstr_MensajeSalida = String.Empty;
            DateTime ldt_Fecha = DateTime.Now;
            try
            {
                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                str_Resultado = ltrn_RevelacionNotas.ufnEliminarRevelacionPendiente(str_IdRevelacionPendiente,
                    out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                log.Error(lstr_MensajeSalida);
            }
            return str_Resultado;
        }

        [WebMethod]
        public string[] uwsAutorizarRevelacionPendiente(string str_IdRevelacionPendiente)
        {
            string[] str_Resultado = new string[2];
            string lstr_MensajeSalida = String.Empty;
            DateTime ldt_Fecha = DateTime.Now;
            try
            {
                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                str_Resultado = ltrn_RevelacionNotas.ufnAutorizarRevelacionPendiente(str_IdRevelacionPendiente,
                    out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                log.Error(lstr_MensajeSalida);
            }
            return str_Resultado;
        }

        /// <summary>
        /// Metodo web que permite modificar la informacion existente en un formulario de revelacion
        /// </summary>
        /// <param name="str_IdRevelacion">Identificador de revelacion</param>
        /// <param name="str_Institucion">Unidad Primaria de Registro</param>
        /// <param name="str_Entidad">Nombre de la Unidad Primaria de Registro</param>
        /// <param name="str_GrupoCuentas">Grupo de cuentas del Plan de Cuentas Contables </param>
        /// <param name="str_Cuentas">Cuentas del Plan de Cuentas Contables</param>
        /// <param name="str_Concepto">Explicación general del concepto </param>
        /// <param name="str_Justificacion">Explicación específica del registro o revelación con mayor detalle</param>
        /// <param name="str_NumExpediente"></param>
        /// <param name="str_HabilitadaPretencion"></param>
        /// <param name="str_EstadoRevelacion"></param>
        /// <param name="str_FchModifica"></param>
        /// <param name="str_UsrModifica"></param>
        /// <returns></returns>
        [WebMethod]
        public Boolean uwsModificarFormulario(string str_IdRevelacion, string str_Institucion,
            string str_Entidad, string str_IdOficina, string str_GrupoCuentas, string str_Cuentas, string str_Concepto, string str_Justificacion,
            string str_EstadoRevelacion, string str_FchModifica, string str_UsrModifica,
            string str_RubroCuenta, string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
        {
            Boolean lboo_ResModificacion = false;
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tRevelacionNotas lrn_RevelacionNotas = new tRevelacionNotas();
                lboo_ResModificacion = lrn_RevelacionNotas.ufnModificarFormulario(str_IdRevelacion, str_Institucion, str_Entidad,
                    str_IdOficina, str_GrupoCuentas, str_Cuentas, str_Concepto, str_Justificacion,
                    str_EstadoRevelacion, str_FchModifica, str_UsrModifica, out lstr_MensajeSalida,
                    str_RubroCuenta, str_SubCuenta, str_SubCuentaAnexa, str_AuxiliarCuenta);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                lstr_MensajeSalida = "Error de aplicacion";
                log.Error("Error de aplicacion");
            }
            return lboo_ResModificacion;
            //return lstr_MensajeSalida;
        }

        [WebMethod]
        public string uwsActualizarFormularioExp(string str_NumExpediente, string str_HabilitadaPretencion, string str_EstadoRevelacion,
            string str_FchModifica, string str_UsrModifica)
        {
            string lstr_CodigoSalida = "99";
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tRevelacionNotas lrn_RevelacionNotas = new tRevelacionNotas();
                lstr_CodigoSalida = lrn_RevelacionNotas.ufnActualizarFormularioExp(str_NumExpediente, str_HabilitadaPretencion,
                str_EstadoRevelacion, str_FchModifica, str_UsrModifica, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                log.Error("Error accediendo a base de datos");
            }
            return lstr_CodigoSalida;
        }

        [WebMethod]
        public string uwsAutorizarCambiosRevelacion(string str_NumExpediente, DateTime str_UltimoDiaMod,
            DateTime str_FchModifica, string str_UsrModifica)
        {
            string lstr_CodigoSalida = "99";
            string lstr_MensajeSalida = String.Empty;
            try
            {
                tRevelacionNotas lrn_RevelacionNotas = new tRevelacionNotas();
                lstr_CodigoSalida = lrn_RevelacionNotas.ufnAutorizarCambiosRevelacion(str_NumExpediente, str_UltimoDiaMod,
                    str_FchModifica, str_UsrModifica, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);
            }
            catch (Exception ex)
            {
                log.Error("Error en servicio web");
            }
            return lstr_CodigoSalida;
        }

        /// <summary>
        /// Metodo web utilizado en contingentes para crear una revelacion
        /// </summary>
        /// <param name="str_NumMinisterio">Identificador del ministerio</param>
        /// <param name="str_Ministerio">Nombre de ministerio</param>
        /// <param name="str_TipoProceso">Tipo de proceso</param>
        /// <param name="str_NumExpediente">Numero de expediente</param>
        /// <param name="str_TotalExpedientes">Total de expedientes</param>
        /// <param name="str_MontoTotalColones">Monto en colones</param>
        /// <param name="str_ValorPresente">Valor</param>
        /// <param name="str_Usuario">Usuario que crea la revelacion</param>
        /// <returns></returns>
        [WebMethod]
        public Boolean uwsCrearRevelacionContingente(string str_NumMinisterio, string str_Ministerio, string str_TipoProceso,
            string str_NumExpediente, string str_TotalExpedientes, string str_MontoTotalColones, string str_ValorPresente
            , string str_Usuario)
        {
            Boolean lboo_ResCreacion = false;
            string lstr_MensajeSalida = String.Empty;
            try
            {
                string lstr_Concepto = String.Format("Observaciones para {0} de {1}", str_NumExpediente, str_Ministerio);
                string lstr_Justificacion = String.Format("Observaciones para {0} de {1} de tipo {2} para un" +
                "total de {3} expedientes con un monto de {4} colones y valor de {5}", str_NumExpediente, str_Ministerio, str_TipoProceso,
                str_TotalExpedientes, str_MontoTotalColones, str_ValorPresente);
                DateTime ldt_Fecha = DateTime.Now;
                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                lboo_ResCreacion = ltrn_RevelacionNotas.ufnCrearFormularioExp(Convert.ToString(ldt_Fecha.Year), ObtenerMesAnno(ldt_Fecha),
                    str_NumMinisterio, "OPER", lstr_Concepto, lstr_Justificacion, str_NumExpediente, "true", "Creada",
                    str_Usuario, out lstr_MensajeSalida);
                log.Info(lstr_MensajeSalida);

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return lboo_ResCreacion;
        }

        /// <summary>
        /// Metodo que devuelve el mes de la fecha otorgado a manera de string, en espAnnol
        /// </summary>
        /// <param name="dt_Fecha">Fecha</param>
        /// <returns></returns>
        private string ObtenerMesAnno(DateTime dt_Fecha)
        {
            string lstr_MesAnno = string.Empty;
            int lint_NumMes = dt_Fecha.Month;
            switch (lint_NumMes)
            {
                case 1:
                    lstr_MesAnno = "Enero";
                    break;
                case 2:
                    lstr_MesAnno = "Febrero";
                    break;
                case 3:
                    lstr_MesAnno = "Marzo";
                    break;
                case 4:
                    lstr_MesAnno = "Abril";
                    break;
                case 5:
                    lstr_MesAnno = "Mayo";
                    break;
                case 6:
                    lstr_MesAnno = "Junio";
                    break;
                case 7:
                    lstr_MesAnno = "Julio";
                    break;
                case 8:
                    lstr_MesAnno = "Agosto";
                    break;
                case 9:
                    lstr_MesAnno = "Setiembre";
                    break;
                case 10:
                    lstr_MesAnno = "Octubre";
                    break;
                case 11:
                    lstr_MesAnno = "Noviembre";
                    break;
                default:
                    lstr_MesAnno = "Diciembre";
                    break;
            }
            return lstr_MesAnno;
        }

        [WebMethod]
        public string uwsSubirArchivoRevelacion(string str_NombreArchivo, string str_Tipo, byte[] bytes)
        {
            string lstr_Resultado = String.Empty;
            try
            {
                tRevelacionNotas ltrn_RevelacionNotas = new tRevelacionNotas();
                lstr_Resultado = ltrn_RevelacionNotas.ufnSubirArchivoRevelacion(str_NombreArchivo, str_Tipo, bytes, 6, "1221");
            }
            catch (Exception ex)
            { }
            return lstr_Resultado;

        }
        //[WebMethod]
        //public static List<string> GetEmployeeName(string empName)
        //{
        //    List<string> empResult = new List<string>();
        //    Database db = DatabaseFactory.CreateDatabase();
        //    using (SqlConnection con = new SqlConnection(@"Data Source=SARSHA\SqlServer2k8;Integrated Security=true;Initial Catalog=Test"))
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.CommandText = "select Top 10 EmployeeName from Employee where EmployeeName LIKE ''+@SearchEmpName+'%'";
        //            cmd.Connection = con;
        //            con.Open();
        //            cmd.Parameters.AddWithValue("@SearchEmpName", empName);
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                empResult.Add(dr["EmployeeName"].ToString());
        //            }
        //            con.Close();
        //            return empResult;
        //        }
        //    }
        //}

        #endregion

        #region Asientos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_Anno"></param>
        /// <param name="lint_Idformulario"></param>
        [WebMethod]
        public string EnviarAsientosCI(string lint_Anno, string lint_Idformulario)
        {
            try
            {
                Boolean vResultado = true;
                Int16? int_Anno = null;
                int int_Idformulario = -1;
                if (!string.IsNullOrEmpty(lint_Anno))
                    int_Anno = Convert.ToInt16(lint_Anno);
                if (!string.IsNullOrEmpty(lint_Idformulario))
                    int_Idformulario = Convert.ToInt32(lint_Idformulario);
                int res = 0;
                int res1 = 0;
                clsTiposAsiento ta = new clsTiposAsiento();

                res = ta.EnviarAsientosCI(int_Anno, int_Idformulario);
                res1 = ta.EnviarAsientosCICT(int_Anno, int_Idformulario);
                return res.ToString();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return "-1";
            }
        }
        #endregion

        #region dinamico
        [WebMethod]
        public DataSet uwsConsultarDinamico(string str_SQL)
        {
            DataSet lds_Dina = new DataSet();
            try
            {
                clsDinamico tus_Dina = new clsDinamico();
                lds_Dina = tus_Dina.ConsultarDinamico(str_SQL);
            }
            catch
            { }
            return lds_Dina;
        }
        #endregion

        #region Procesos
        [WebMethod]
        public string Reclasificar(string lstr_IdPrestamo = "", string lint_IdTramo = "", string ldt_Fecha = "", char lchr_Xtir = '1')
        {
            string txtSalida = "";
            string codSalida = "00";
            string mensaje = "";
            int? IdTramo = null;

            DateTime? Fecha = null;
            try
            {
                if (!string.IsNullOrEmpty(ldt_Fecha))
                    Fecha = DateTime.ParseExact(ldt_Fecha, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
            }
            try
            {
                if (!string.IsNullOrEmpty(lint_IdTramo))
                    IdTramo = Convert.ToInt32(lint_IdTramo);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: Formato incorrecto de campo ";
            }
            if (mensaje == "")
            {
                try
                {

                    clsCalculosDeudaExterna lcls_DeudaExterna = new clsCalculosDeudaExterna();
                    lcls_DeudaExterna.Reclasificar(out mensaje, lstr_IdPrestamo, IdTramo, Fecha, lchr_Xtir);


                    codSalida = ((codSalida == "00") || (codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + mensaje;
                }
                catch (Exception e)
                {
                    mensaje = "Código 99: " + e.ToString();
                }
            }
            return mensaje;
        }
        [WebMethod]
        public string CalculaDevengoDE(string lstr_IdPrestamo = "", string lint_IdTramo = "", string ldt_FechaHasta = "", char lchr_Xtir = '1')
        {
            DateTime? FechaHasta = null;
            int? IdTramo = null;
            string mensaje = "";
            try
            {
                if (!string.IsNullOrEmpty(ldt_FechaHasta))
                    FechaHasta = DateTime.ParseExact(ldt_FechaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
            }
            try
            {
                if (!string.IsNullOrEmpty(lint_IdTramo))
                    IdTramo = Convert.ToInt32(lint_IdTramo);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: Formato incorrecto de campo ";
            }
            if (mensaje == "")
            {
                clsCalculosDeudaExterna lcls_DeudaExterna = new clsCalculosDeudaExterna();

                lcls_DeudaExterna.DevengoDE(out mensaje, lstr_IdPrestamo, IdTramo, FechaHasta, lchr_Xtir);

                return mensaje;
            }
            return null;

        }

        [WebMethod]
        public string ContabilizaDevengoDE(string lstr_IdPrestamo = "", string lint_IdTramo = "", string ldt_FechaHasta = "")
        {
            DateTime? FechaHasta = null;
            int? IdTramo = null;
            string mensaje = "";
            try
            {
                if (!string.IsNullOrEmpty(ldt_FechaHasta))
                    FechaHasta = DateTime.ParseExact(ldt_FechaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
            }
            try
            {
                if (!string.IsNullOrEmpty(lint_IdTramo))
                    IdTramo = Convert.ToInt32(lint_IdTramo);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: Formato incorrecto de campo ";
            }
            if (mensaje == "")
            {
                clsCalculosDeudaExterna lcls_DeudaExterna = new clsCalculosDeudaExterna();

                lcls_DeudaExterna.ContabilizarDevengoDE(out mensaje, lstr_IdPrestamo, IdTramo, FechaHasta);

                return mensaje;
            }
            return null;

        }
        [WebMethod]
        public string DiferencialCambiario(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchFin)
        {

            string mensaje = "";
            DateTime? FechaHasta = DateTime.Today;
            int? IdTramo = null;
            clsDiferencialCamb clsDiferencialCamb = new clsDiferencialCamb();
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                try
                { 
                    if (!string.IsNullOrEmpty(ldt_FchFin))
                        FechaHasta = DateTime.ParseExact(ldt_FchFin, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }

                if (mensaje == "")
                {
                    mensaje = "Proceso Finalizado! Verifique bitácora.";
                    mensaje = clsDiferencialCamb.Diferencial(lstr_IdPrestamo, IdTramo, Convert.ToDateTime(FechaHasta));
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();

            }
            return mensaje;
        }
        
        [WebMethod]
        public string ReversaDevengoDE(string lstr_IdPrestamo = "", string lint_IdTramo = "", string ldt_FechaHasta = "")
        {
            DateTime? FechaHasta = null;
            int? IdTramo = null;
            string mensaje = "";
            try
            {
                if (!string.IsNullOrEmpty(ldt_FechaHasta))
                    FechaHasta = DateTime.ParseExact(ldt_FechaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
            }
            if (mensaje == "")
            {
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsCalculosDeudaExterna lcls_DeudaExterna = new clsCalculosDeudaExterna();

                    lcls_DeudaExterna.ReversarDevengoDE(out mensaje, lstr_IdPrestamo, IdTramo, FechaHasta);

                    return mensaje;
                }
            }
            return null;

        }
       
        [WebMethod]
        public DataSet ConsultaSaldoDeudaExt(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FechaDesde, string ldt_FechaHasta)
        {
            DataSet ldas_SaldoDeudaExt = new DataSet();
            string mensaje = "";

            DateTime? FechaDesde = null;
            DateTime? FechaHasta = null;
            int? IdTramo = null;

            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(ldt_FechaDesde))
                        FechaDesde = DateTime.ParseExact(ldt_FechaDesde, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FechaHasta))
                        FechaHasta = DateTime.ParseExact(ldt_FechaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }

                if (mensaje == "")
                {
                    clsSaldoDeudaExt lcls_SaldoDeudaExt = new clsSaldoDeudaExt();
                    ldas_SaldoDeudaExt = lcls_SaldoDeudaExt.ConsultarSaldosDeudaExt(lstr_IdPrestamo, IdTramo, FechaDesde, FechaHasta);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();

            }

            return ldas_SaldoDeudaExt;
        }

        [WebMethod]
        public string uwsReversarAsiento(int Consecutivo, string CodAsiento, string ldt_FechaHasta = "")
        {
            string CorResultado = string.Empty;
            string Mensaje = string.Empty;
            bool Resultado = false;
            DateTime? FechaHasta = null;
            try
            {
                if (!string.IsNullOrEmpty(ldt_FechaHasta))
                    FechaHasta = DateTime.ParseExact(ldt_FechaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
            }
            if (Mensaje == "")
            {
                try
                {
                    clsCalculosDeudaExterna lcls_DeudaExterna = new clsCalculosDeudaExterna();
                    Resultado = lcls_DeudaExterna.ReversarAsiento(Consecutivo, "", Convert.ToDateTime(FechaHasta), out CorResultado, out Mensaje);
                }
                catch (Exception e)
                {
                    Mensaje = "Error al reversar asiento " + e.ToString();
                }
            }
            return Mensaje;
        }
        #endregion Procesos

        /// <summary>
        /// Modificar Cobros Pagos para Archivo 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string debugPago(
     string texto          )
        {
         int lint_IdRes=4075;
         int lint_IdCobroPagoResolucion= 10845;
         string lstr_IdResolucion="1020";//Identificador único de la resolución dictada en los tribunales de justicia
         string lstr_IdExpediente = "45-454545-4545-CA";//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
         string lstr_IdSociedadGL="11215";
         string lstr_EstadoResolucion ="En Firme";//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
         string lstr_Moneda="CRC";//La moneda en la cual se recibe el cobro. Campo obligatorio
         decimal? ldec_TipoCambio=1;//El tipo de cambio al momento de incluirlo en el sistema.
         decimal? ldec_MontoPrincipal=15000;//Es el monto principal a cobrar/pagar
         decimal? ldec_MontoPrincipalColones=15000;//Monto principal a cobrar/pagar en colones
         decimal? ldec_Intereses=0;
         decimal? ldec_InteresesColones=0;
         decimal? ldec_InteresesMoratorios=0;
         decimal? ldec_InteresesMoratoriosColones=0;
         decimal? ldec_Costas=0;
         decimal? ldec_CostasColones=0;
         decimal? ldec_DanoMoral= 0;
         decimal? ldec_DanoMoralColones= 0;
         string lstr_UsrModifica = "0110370132";

            clsCobrosPagos reg_Resoluciones = new clsCobrosPagos();
            string[] lstr_result = new string[2];
            try
            {
                lstr_result = reg_Resoluciones.ModificarCobrosPagosArchivo(
                                 lint_IdRes,
                                 lint_IdCobroPagoResolucion,
                                 lstr_IdResolucion,//Identificador único de la resolución dictada en los tribunales de justicia
                                 lstr_IdExpediente,//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
                                 lstr_IdSociedadGL,
                                 lstr_EstadoResolucion,//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
                                 lstr_Moneda,//La moneda en la cual se recibe el cobro. Campo obligatorio
                                 ldec_TipoCambio,//El tipo de cambio al momento de incluirlo en el sistema.
                                 ldec_MontoPrincipal,//Es el monto principal a cobrar/pagar
                                 ldec_MontoPrincipalColones,//Monto principal a cobrar/pagar en colones
                                 ldec_Intereses,
                                 ldec_InteresesColones,
                                 ldec_InteresesMoratorios,
                                 ldec_InteresesMoratoriosColones,
                                 ldec_Costas,
                                 ldec_CostasColones,
                                 ldec_DanoMoral,
                                 ldec_DanoMoralColones,
                                 lstr_UsrModifica);//Realizamos el mappeo en la BD
            }
            catch (Exception exc)
            {
                lstr_result[0] = "99";
                lstr_result[1] = "Erorr: " + exc.Message;
            }
            return texto;
        }
    }
}
