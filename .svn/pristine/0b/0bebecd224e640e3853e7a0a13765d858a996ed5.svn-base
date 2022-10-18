using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarCuentaContablesTipo : clsProcedimientoAlmacenado
    {
        private string lstr_GrupoCuentas;
        public string Lstr_GrupoCuentas
        {
            get { return lstr_GrupoCuentas; }
            set { lstr_GrupoCuentas = value; }
        }


        public clsConsultarCuentaContablesTipo(string str_GrupoCuentas)
        {
            lstr_GrupoCuentas = str_GrupoCuentas;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarCuentasContablesTipo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}