using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsActualizarObservacionesRevCont : clsProcedimientoAlmacenado
    {
                private string lstr_IdRevCont;
        public string Lstr_IdRevCont
        {
            get { return lstr_IdRevCont; }
            set { lstr_IdRevCont = value; }
        }
        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_TipoProceso;
        public string Lstr_TipoProceso
        {
            get { return lstr_TipoProceso; }
            set { lstr_TipoProceso = value; }
        }

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        private string lstr_Observacion;
        public string Lstr_Observacion
        {
            get { return lstr_Observacion; }
            set { lstr_Observacion = value; }
        }

        private string lstr_FchModifica;
        public string Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }


        public clsActualizarObservacionesRevCont(string str_IdRevCont, string str_IdSociedadGL,
            string str_TipoProceso, string str_Observacion, string str_UsrModifica, string str_FchModifica)
        {
            lstr_IdRevCont = str_IdRevCont;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_TipoProceso = str_TipoProceso;
            lstr_Observacion = str_Observacion;
            lstr_UsrModifica = str_UsrModifica;
            lstr_FchModifica = str_FchModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\ActualizarObservacionesRevCont.config", this);
        }
    }
}