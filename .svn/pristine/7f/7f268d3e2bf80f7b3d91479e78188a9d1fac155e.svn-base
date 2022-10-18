using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearIndicadorEconomico : clsProcedimientoAlmacenado
    {
        private string lstr_IdIndicadorEconomico;
        public string Lstr_IdIndicadorEconomico
        {
            get { return lstr_IdIndicadorEconomico; }
            set { lstr_IdIndicadorEconomico = value; }
        }

        private string lstr_Transaccion;
        public string Lstr_Transaccion
        {
            get { return lstr_Transaccion; }
            set { lstr_Transaccion = value; }
        }

        private string lstr_NomIndicadorEconomico;
        public string Lstr_NomIndicadorEconomico
        {
            get { return lstr_NomIndicadorEconomico; }
            set { lstr_NomIndicadorEconomico = value; }
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

        public clsCrearIndicadorEconomico(string str_IdIndicadorEconomico, string str_Transaccion, string str_NomIndicadorEconomico, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdIndicadorEconomico = str_IdIndicadorEconomico;
            lstr_Transaccion = str_Transaccion;
            lstr_NomIndicadorEconomico = str_NomIndicadorEconomico;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearIndicadorEconomico.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}