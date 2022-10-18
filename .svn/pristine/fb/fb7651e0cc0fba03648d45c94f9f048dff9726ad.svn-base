using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarPlanesCuentas : clsProcedimientoAlmacenado
    {
        private string lstr_IdPlanCuenta;
        public string Lstr_IdPlanCuenta
        {
            get { return lstr_IdPlanCuenta; }
            set { lstr_IdPlanCuenta = value; }
        }

        private string lstr_NomPlanCuenta;
        public string Lstr_NomPlanCuenta
        {
            get { return lstr_NomPlanCuenta; }
            set { lstr_NomPlanCuenta = value; }
        }


        public clsConsultarPlanesCuentas(string str_IdPlanCuenta, string str_NomPlanCuenta)
        {
            lstr_IdPlanCuenta = str_IdPlanCuenta;
            lstr_NomPlanCuenta = str_NomPlanCuenta;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarPlanesCuentas.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}