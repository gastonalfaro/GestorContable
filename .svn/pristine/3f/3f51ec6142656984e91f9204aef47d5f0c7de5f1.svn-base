using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarCuentasContables : clsProcedimientoAlmacenado
    {
        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }

        private string lstr_IdPlanCuenta;
        public string Lstr_IdPlanCuenta
        {
            get { return lstr_IdPlanCuenta; }
            set { lstr_IdPlanCuenta = value; }
        }

        private string lstr_IdGrupoCuenta;
        public string Lstr_IdGrupoCuenta
        {
            get { return lstr_IdGrupoCuenta; }
            set { lstr_IdGrupoCuenta = value; }
        }

        private string lstr_NomCuenta;
        public string Lstr_NomCuenta
        {
            get { return lstr_NomCuenta; }
            set { lstr_NomCuenta = value; }
        }

        private string lstr_CuentaGrupo;
        public string Lstr_CuentaGrupo
        {
            get { return lstr_CuentaGrupo; }
            set { lstr_CuentaGrupo = value; }
        }
        private string lstr_IndTotales;
        public string Lstr_IndTotales
        {
            get { return lstr_IndTotales; }
            set { lstr_IndTotales = value; }
        }

        private string lstr_IndConsolidacion;
        public string Lstr_IndConsolidacion
        {
            get { return lstr_IndConsolidacion; }
            set { lstr_IndConsolidacion = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        public clsConsultarCuentasContables(string str_IdCuentaContable, string str_IdPlanCuenta, string str_IdGrupoCuenta, string str_NomCuenta, string str_CuentaGrupo, string str_IndTotales, string str_IndConsolidacion, string str_IdSociedadFi)
        {
            lstr_IdCuentaContable = str_IdCuentaContable;
            lstr_IdPlanCuenta = str_IdPlanCuenta;
            lstr_IdGrupoCuenta = str_IdGrupoCuenta;
            lstr_NomCuenta = str_NomCuenta;
            lstr_CuentaGrupo = str_CuentaGrupo; 
            lstr_IndTotales = str_IndTotales;
            lstr_IndConsolidacion = str_IndConsolidacion;
            lstr_IdSociedadFi = str_IdSociedadFi;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarCuentasContables.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}