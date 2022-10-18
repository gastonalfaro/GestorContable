using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsActualizarFormularioExp : clsProcedimientoAlmacenado
    {

        private string lstr_NumExpediente;
        public string Lstr_NumExpediente
        {
            get { return lstr_NumExpediente; }
            set { lstr_NumExpediente = value; }
        }
        private string lstr_HabilitadaPretencion;
        public string Lstr_HabilitadaPretencion
        {
            get { return lstr_HabilitadaPretencion; }
            set { lstr_HabilitadaPretencion = value; }
        }

        private string lstr_EstadoRevelacion;
        public string Lstr_EstadoRevelacion
        {
            get { return lstr_EstadoRevelacion; }
            set { lstr_EstadoRevelacion = value; }
        }

        private string lstr_FchModifica;
        public string Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }



        public clsActualizarFormularioExp(string str_NumExpediente, string str_HabilitadaPretencion, 
            string str_EstadoRevelacion, string str_FchModifica, string str_UsrModifica)
        {
            lstr_NumExpediente = str_NumExpediente;
            lstr_HabilitadaPretencion = str_HabilitadaPretencion;
            lstr_EstadoRevelacion = str_EstadoRevelacion;
            lstr_FchModifica = str_FchModifica;
            lstr_UsrModifica = str_UsrModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\ActualizarFormularioExp.config", this);
        }
    }
}