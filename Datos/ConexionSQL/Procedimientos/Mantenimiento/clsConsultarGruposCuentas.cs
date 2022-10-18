using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarGruposCuentas : clsProcedimientoAlmacenado
    {
        private string lstr_IdGrupoCuenta;
        public string Lstr_IdGrupoCuenta
        {
            get { return lstr_IdGrupoCuenta; }
            set { lstr_IdGrupoCuenta = value; }
        }

        private string lstr_IdPlanCuenta;
        public string Lstr_IdPlanCuenta
        {
            get { return lstr_IdPlanCuenta; }
            set { lstr_IdPlanCuenta = value; }
        }

        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }


        private string lstr_NomGrupoCuenta;
        public string Lstr_NomGrupoCuenta
        {
            get { return lstr_NomGrupoCuenta; }
            set { lstr_NomGrupoCuenta = value; }
        }


        public clsConsultarGruposCuentas(string str_IdGrupoCuenta, string str_IdPlanCuenta, string str_IdCuentaContable, string str_NomGrupoCuenta)
        {
            lstr_IdGrupoCuenta = str_IdGrupoCuenta;
            lstr_NomGrupoCuenta = str_NomGrupoCuenta;
            lstr_IdPlanCuenta = str_IdPlanCuenta;
            lstr_IdCuentaContable = str_IdCuentaContable;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarGruposCuentas.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}