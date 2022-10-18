using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearPlanCuenta : clsProcedimientoAlmacenado
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

        public clsCrearPlanCuenta(string str_IdPlanCuenta, string str_NomPlanCuenta, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdPlanCuenta = str_IdPlanCuenta;
            lstr_NomPlanCuenta = str_NomPlanCuenta;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearPlanCuenta.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}