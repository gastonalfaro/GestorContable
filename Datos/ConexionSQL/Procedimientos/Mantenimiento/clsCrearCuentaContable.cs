using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearCuentaContable : clsProcedimientoAlmacenado
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


        private string lstr_NomCorto;
        public string Lstr_NomCorto
        {
            get { return lstr_NomCorto; }
            set { lstr_NomCorto = value; }
        }

        private string lstr_NomCuentaContable;
        public string Lstr_NomCuentaContable
        {
            get { return lstr_NomCuentaContable; }
            set { lstr_NomCuentaContable = value; }
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

       
        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearCuentaContable(string str_IdCuentaContable, string str_IdPlanCuenta, string str_IdGrupoCuenta, string str_NomCorto, string str_NomCuentaContable, string str_CuentaGrupo, string str_IndTotales, string str_IndConsolidacion, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdCuentaContable = str_IdCuentaContable;
            lstr_IdPlanCuenta = str_IdPlanCuenta;
            lstr_IdGrupoCuenta = str_IdGrupoCuenta;
            lstr_NomCorto = str_NomCorto;            
            lstr_NomCuentaContable = str_NomCuentaContable;
            lstr_CuentaGrupo = str_CuentaGrupo;
            lstr_IndTotales = str_IndTotales;
            lstr_IndConsolidacion = str_IndConsolidacion;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearCuentaContable.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}