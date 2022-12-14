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
using System.Globalization;

namespace WebServiceConexionSAP
{
    public class wsConexionSAP : System.Web.Services.WebService
    {
        
        #region TablasSAP

        #region Bancos

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

                //Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                //Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }
        
        [WebMethod]
        public String[] uwsModificarBanco(string str_IdBanco, string str_NomBanco, string str_IdPais, string str_Telefono, 
            string str_Contacto, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModifica;
            bool bool_ResModifica = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsBancos ltro_Banco = new clsBancos();
                bool_ResModifica = ltro_Banco.ModificarBanco(str_IdBanco, str_NomBanco, str_IdPais, str_Telefono, str_Contacto, str_Estado, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResModifica = new String[2];
                arr_ResModifica[0] = bool_ResModifica.ToString();
                arr_ResModifica[1] = ex.ToString();

            }
            return arr_ResModifica;
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

                //Log.Info(str_Mensaje);

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                //Log.Error(ex.ToString());

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }


        [WebMethod]
        public String[] uwsModificarBancoCuenta(string str_IdBanco, string str_IdCuentaBancaria, string str_IdCuentaContable, 
            string str_IdSociedadGL, string str_TipoCuenta, string str_UsrModifica, DateTime dt_FchModifica)
        {
            String[] arr_ResModificado;
            bool bool_ResModificado = false;
            string str_CodResultado = string.Empty;
            string str_Mensaje = string.Empty;
            try
            {
                clsBancos ltro_Banco = new clsBancos();
                bool_ResModificado = ltro_Banco.ModificarBancoCuenta(str_IdBanco, str_IdCuentaBancaria, str_IdCuentaContable, str_IdSociedadGL, str_TipoCuenta, str_UsrModifica, dt_FchModifica, out str_CodResultado, out str_Mensaje);

                

                arr_ResModificado = new String[2];
                arr_ResModificado[0] = bool_ResModificado.ToString();
                arr_ResModificado[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResModificado = new String[2];
                arr_ResModificado[0] = bool_ResModificado.ToString();
                arr_ResModificado[1] = ex.ToString();

            }
            return arr_ResModificado;
        }
        #endregion Bancos

        [WebMethod]
        public String[] uwsCrearCentroCostos(string str_IdCentroCosto, string str_IdSociedadCO, string str_IdSociedadFi, 
            string str_Denominacion, string str_Descripcion, string str_CnBeneficio, string str_FinValidez, string str_ValidoD, 
            string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            
            try
            {
                DateTime? dt_FinValidez; 
                DateTime? dt_ValidoD;
                if (string.IsNullOrEmpty(str_FinValidez))
                    dt_FinValidez = null;
                else 
                    dt_FinValidez = DateTime.ParseExact(str_FinValidez, "dd.MM.yyyy", CultureInfo.CurrentCulture);
                if (string.IsNullOrEmpty(str_ValidoD))
                    dt_ValidoD = null;
                else
                    dt_ValidoD = DateTime.ParseExact(str_ValidoD, "dd.MM.yyyy", CultureInfo.CurrentCulture);


                tCentroCosto ltro_CentroCosto = new tCentroCosto();
                bool_ResCreacion = ltro_CentroCosto.CrearCentroCosto(str_IdCentroCosto, dt_ValidoD, dt_FinValidez, str_IdSociedadCO, str_IdSociedadFi, str_CnBeneficio, str_Denominacion, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearAreaFuncional(string str_IdAreaFuncional, string str_NomAreaFuncional, string str_Estado = null, 
            string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {
                tAreaFuncional ltro_AreaFuncional = new tAreaFuncional();
                bool_ResCreacion = ltro_AreaFuncional.CrearAreaFuncional(str_IdAreaFuncional, str_NomAreaFuncional, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearCentroBeneficio(string str_CodigoCB, string str_SociedadCO, string str_IdSociedadFi, 
            string str_Denominacion, string str_Descripcion, string str_FinValidez, string str_ValidoD, string str_Estado = null, 
            string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            
            try
            {
                //DateTime dt_FinValidez = Convert.ToDateTime(str_FinValidez);
                DateTime? dt_FinValidez;
                DateTime? dt_ValidoD;
                if (string.IsNullOrEmpty(str_FinValidez))
                    dt_FinValidez = null;
                else
                    dt_FinValidez = DateTime.ParseExact(str_FinValidez, "dd.MM.yyyy", CultureInfo.CurrentCulture);
                if (string.IsNullOrEmpty(str_ValidoD))
                    dt_ValidoD = null;
                else
                    dt_ValidoD = DateTime.ParseExact(str_ValidoD, "dd.MM.yyyy", CultureInfo.CurrentCulture);

                tCentroBeneficio ltro_CentroBeneficio = new tCentroBeneficio();
                bool_ResCreacion = ltro_CentroBeneficio.CrearCentroBeneficio(str_CodigoCB, dt_ValidoD, dt_FinValidez, str_SociedadCO, str_IdSociedadFi, str_Denominacion, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearCentroGestor(string str_CodigoCG, string str_EntidadCP, string str_IdSociedadFi, string str_Denominacion, 
            string str_Descripcion, string str_FinValidez, string str_ValidoD, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {
                DateTime? dt_FinValidez;
                DateTime? dt_ValidoD;
                if (string.IsNullOrEmpty(str_FinValidez))
                    dt_FinValidez = null;
                else
                    dt_FinValidez = DateTime.ParseExact(str_FinValidez, "dd.MM.yyyy", CultureInfo.CurrentCulture);
                if (string.IsNullOrEmpty(str_ValidoD))
                    dt_ValidoD = null;
                else
                    dt_ValidoD = DateTime.ParseExact(str_ValidoD, "dd.MM.yyyy", CultureInfo.CurrentCulture);


                tCentroGestor ltro_CentroBeneficio = new tCentroGestor();
                bool_ResCreacion = ltro_CentroBeneficio.CrearCentroGestor(str_CodigoCG, dt_ValidoD, dt_FinValidez, str_EntidadCP, str_IdSociedadFi, str_Denominacion, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearClaseDocumento(string str_CodigoCD, string str_NomClaseDocumento, string str_Estado = null, 
            string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {
                tClaseDocumento ltro_AreaFuncional = new tClaseDocumento();
                bool_ResCreacion = ltro_AreaFuncional.CrearClaseDocumento(str_CodigoCD, str_NomClaseDocumento, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearCuentaContablePlanCuenta(string str_IdCuentaMayor, string str_IdPlanCuenta, string str_GrupoCuentas, 
            string str_TxtBreve, string str_TxtDescriptivo, string str_CuentaGrupo, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tCuentaContable ltro_CuentaContable = new tCuentaContable();
                bool_ResCreacion = ltro_CuentaContable.CrearCuentaContable(str_IdCuentaMayor, str_IdPlanCuenta, str_GrupoCuentas, str_TxtBreve, str_TxtDescriptivo, str_CuentaGrupo, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearCuentaContableConsolida(tCuentaConsolida[] arr_Cuentas)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            Int16 i = 0;
            try
            {

                arr_ResCreacion = new String[arr_Cuentas.Count() + 1];
                arr_ResCreacion[0] = "True";
                foreach (tCuentaConsolida x in arr_Cuentas)
                {
                    bool_ResCreacion = x.CrearCuentaContableConsolida(x.Lstr_IdCuentaContable, x.Lstr_IdPlanCuenta, x.Lstr_NomCuentaContable, x.Lstr_IndTipoCuenta, x.Lstr_IndNaturaleza, x.Lstr_Estado, x.Lstr_UsrCreacion, out str_CodResultado, out str_Mensaje);
                    i++;
                    if (bool_ResCreacion == false)
                    {
                        arr_ResCreacion[0] = bool_ResCreacion.ToString();
                    }
                    arr_ResCreacion[i] = x.Lstr_IdCuentaContable + str_Mensaje;

                }

                //arr_ResCreacion = new String[2];
                //arr_ResCreacion[0] = bool_ResCreacion.ToString();
                //arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {


                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }
        
        [WebMethod]
        public String[] uwsCrearCuentaContableSociedad(string str_IdCuentaMayor, string str_Sociedad, string str_MonedaCuenta, 
            string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tCuentaContable ltro_CuentaContable = new tCuentaContable();
                bool_ResCreacion = ltro_CuentaContable.CrearCuentaSociedad(str_IdCuentaMayor, str_Sociedad, str_MonedaCuenta, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearElementoPEP(string str_IdElementoPEP, string str_Descripcion, string str_Estado = null, 
            string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tElementoPEP ltro_ElementoPEP = new tElementoPEP();
                bool_ResCreacion = ltro_ElementoPEP.CrearElementoPEP(str_IdElementoPEP, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearEntidadesCP(string str_IdEntidadCP, string str_TxtEntidad, string str_Moneda, string str_Estado = null, 
            string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tEntidadCP ltro_EntidadCP = new tEntidadCP();
                bool_ResCreacion = ltro_EntidadCP.CrearEntidadCP(str_IdEntidadCP, str_TxtEntidad, str_Moneda, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearFondo(string str_IdFondo, string str_EntidadCP, string str_Denominacion, string str_Descripcion, 
            string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tFondo ltro_Fondo = new tFondo();
                bool_ResCreacion = ltro_Fondo.CrearFondo(str_IdFondo, str_EntidadCP, str_Denominacion, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearGrupoCuentaContable(string str_IdGrupoCuenta, string str_IdPlanCuenta, string str_Significado, 
            string str_Desde, string str_Hasta, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tGrupoCuenta ltro_GrupoCuenta = new tGrupoCuenta();
                bool_ResCreacion = ltro_GrupoCuenta.CrearGrupoCuenta(str_IdGrupoCuenta, str_IdPlanCuenta, str_Desde, str_Hasta, str_Significado, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearPosicionPresupuestaria(string str_IdPosicionPre, string str_IdEntidadCP, string str_IdEjercico, 
            string str_Denominacion, string str_Descripcion, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                TiposicionPresupuestaria ltro_PosicionPresupuestaria = new TiposicionPresupuestaria();
                bool_ResCreacion = ltro_PosicionPresupuestaria.CrearPosicionPresupuestaria(str_IdPosicionPre, str_IdEntidadCP, str_IdEjercico, str_Denominacion, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearProgramaPresupuestario(string str_IdProgramaPre, string str_IdEntidadCP, string str_Denominacion, 
            string str_Descripcion, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tPrograma ltro_Programa = new tPrograma();
                bool_ResCreacion = ltro_Programa.CrearPrograma(str_IdProgramaPre, str_IdEntidadCP, str_Denominacion, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearSociedadCosto(string str_IdSociedad, string str_Descripcion, string str_Estado = null, 
            string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tSociedadCosto ltro_SociedadCosto = new tSociedadCosto();
                bool_ResCreacion = ltro_SociedadCosto.CrearSociedadCosto(str_IdSociedad, str_Descripcion, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearSociedadFinaciera(string str_IdSociedadFin, string str_IdSociedadGL, string str_Denominacion, 
            string str_ClavePais, string str_Poblacion, string str_ClaveMoneda, string str_ClaveIdioma, string str_Estado = null, 
            string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tSociedadFinanciera ltro_SociedadFinaciera = new tSociedadFinanciera();
                bool_ResCreacion = ltro_SociedadFinaciera.CrearSociedadFinanciera(str_IdSociedadFin, str_IdSociedadGL, str_Denominacion, str_Denominacion, str_ClavePais, str_Poblacion, str_ClaveMoneda, str_ClaveIdioma, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearSociedadGL(string str_IdSociedadGL, string str_NomSociedad, string str_ClavePais, string str_Poblacion, 
            string str_Calle, string str_ClaveMoneda, string str_ClaveIdioma, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {
                tSociedadGL ltro_SociedadGL = new tSociedadGL();
                bool_ResCreacion = ltro_SociedadGL.CrearSociedadGL(str_IdSociedadGL, str_NomSociedad, str_ClavePais, str_Poblacion, str_Calle, str_ClaveMoneda, str_ClaveIdioma, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearUnidadConsolidacion(string str_IdVista, string str_IdUnidadCons, string str_TxtBreve, string str_TxtMedio, 
            string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tUnidadConsolidacion ltro_UnidadConsolidacion = new tUnidadConsolidacion();
                bool_ResCreacion = ltro_UnidadConsolidacion.CrearUnidadConsolidacion(str_IdVista, str_IdUnidadCons, str_TxtBreve, str_TxtMedio, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearAmbitoConsolidacion(string str_IdVista, string str_IdAmbitoCons, string str_TxtBreve, string str_TxtMedio, 
            string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tAmbitoConsolidacion ltro_AmbitoConsolidacion = new tAmbitoConsolidacion();
                bool_ResCreacion = ltro_AmbitoConsolidacion.CrearAmbitoConsolidacion(str_IdVista, str_IdAmbitoCons, str_TxtBreve, str_TxtMedio, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearJerarquia(string str_IdVista, string str_IdJerarquia, string str_TxtBreve, string str_TxtMedio, 
            string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tJerarquia ltro_Jerarquia = new tJerarquia();
                bool_ResCreacion = ltro_Jerarquia.CrearJerarquia(str_IdVista, str_IdJerarquia, str_TxtBreve, str_TxtMedio, str_Estado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearJerarquiaPeriodo(string str_IdVista, string str_IdJerarquiaPer, string str_IdEjercicio, string str_IdPeriodo, 
            string str_AmbitoCons, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tJerarquiaPeriodo ltro_JerarquiaPeriodo = new tJerarquiaPeriodo();
                bool_ResCreacion = ltro_JerarquiaPeriodo.CrearJerarquiaPeriodo(str_IdVista, str_IdJerarquiaPer, str_IdEjercicio, str_IdPeriodo, str_AmbitoCons, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearAsignacionACUC(string str_IdVista, string str_IdVersion, string str_IdAmbitoCons, string str_IdUnidadCons, 
            string str_IdEjercico, string str_IdPeriodo, bool str_NodoUC, string str_Estado = null, string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                tAsignacionACUC ltro_AsignacionACUC = new tAsignacionACUC();
                bool_ResCreacion = ltro_AsignacionACUC.CrearAsignacionACUC(str_IdVista, str_IdVersion, str_IdAmbitoCons, str_IdUnidadCons, str_IdEjercico, str_IdPeriodo, str_NodoUC, str_UsrCreacion, out str_CodResultado, out str_Mensaje);
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {
                

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        [WebMethod]
        public String[] uwsCrearReserva(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdClaseDocPsto,
            string str_IdMoneda, string str_NomReserva, string str_Estado, string str_Bloqueado, clsReservasDetalle[] arr_Detalle,
            string str_UsrCreacion = null)
        {
            String[] arr_ResCreacion;
            bool bool_ResCreacion = false;
            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {

                clsReservas ltro_Reserva = new clsReservas();
                bool_ResCreacion = ltro_Reserva.CrearReserva(str_IdReserva, str_IdEntidadCP, str_IdSociedadFi, str_IdClaseDocPsto, str_IdMoneda, str_NomReserva, str_Estado, str_Bloqueado, str_UsrCreacion, out str_CodResultado, out str_Mensaje);

                if (bool_ResCreacion)
                {
                    foreach (clsReservasDetalle x in arr_Detalle)
                    {
                        bool_ResCreacion = ltro_Reserva.CrearReservaDetalle(x.Lstr_IdReserva, x.Lstr_Posicion, x.Lstr_Detalle, x.Lstr_IdPosPre, x.Lstr_IdCentroGestor, x.Lstr_IdFondo, x.Lstr_Segmento, x.Lstr_IdPrograma, x.Lstr_IdCuentaContable, x.Lstr_IdCentroCosto, x.Lstr_IdElementoPEP, x.Lstr_IdMoneda, x.Ldec_Monto, x.Lstr_Estado, x.Lstr_Bloqueado, x.Lstr_UsrCreacion, out str_CodResultado, out str_Mensaje);
                    }
                }

                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = str_Mensaje;

            }
            catch (Exception ex)
            {


                arr_ResCreacion = new String[2];
                arr_ResCreacion[0] = bool_ResCreacion.ToString();
                arr_ResCreacion[1] = ex.ToString();

            }
            return arr_ResCreacion;
        }

        #endregion

        #region Bitacora
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
                str_ResCreacion = "99 : " + ex.Message.ToString();
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
                str_ResCreacion = "99 : " + ex.Message.ToString();
            }
            return str_ResCreacion;
        }
        #endregion Bitacora
    }
}
