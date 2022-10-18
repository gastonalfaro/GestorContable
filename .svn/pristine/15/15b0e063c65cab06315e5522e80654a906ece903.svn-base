using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarIndicadoresEconomicos : clsProcedimientoAlmacenado
    {
        private string lstr_IdIndicadorEco;
        public string Lstr_IdIndicadorEco
        {
            get { return lstr_IdIndicadorEco; }
            set { lstr_IdIndicadorEco = value; }
        }

        private string lstr_Transaccion;
        public string Lstr_Transaccion
        {
            get { return lstr_Transaccion; }
            set { lstr_Transaccion = value; }
        }

        private string lstr_NomIndicadorEco;
        public string Lstr_NomIndicadorEco
        {
            get { return lstr_NomIndicadorEco; }
            set { lstr_NomIndicadorEco = value; }
        }


        public clsConsultarIndicadoresEconomicos(string str_IdIndicadorEco, string str_Transaccion, string str_NomIndicadorEco)
        {
            lstr_IdIndicadorEco = str_IdIndicadorEco;
            lstr_Transaccion = str_Transaccion;
            lstr_NomIndicadorEco = str_NomIndicadorEco;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarIndicadoresEconomicos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}