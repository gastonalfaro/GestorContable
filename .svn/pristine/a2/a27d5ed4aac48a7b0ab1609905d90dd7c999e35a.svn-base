using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsConsultarRevelacionContSoc : clsProcedimientoAlmacenado
    {
                private string lstr_IdRevCont;
        public string Lstr_IdRevCont
        {
            get { return lstr_IdRevCont; }
            set { lstr_IdRevCont = value; }
        }

        private string lstr_PeriodoAnual;
        public string Lstr_PeriodoAnual
        {
            get { return lstr_PeriodoAnual; }
            set { lstr_PeriodoAnual = value; }
        }

        private string lstr_PeriodoMensual;
        public string Lstr_PeriodoMensual
        {
            get { return lstr_PeriodoMensual; }
            set { lstr_PeriodoMensual = value; }
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

        public clsConsultarRevelacionContSoc(string str_IdRevCont, string str_PeriodoAnual, string str_PeriodoMensual,
            string str_IdSociedadGL, string str_TipoProceso)
        {
            lstr_IdRevCont = str_IdRevCont;
            lstr_PeriodoAnual = str_PeriodoAnual;
            lstr_PeriodoMensual = str_PeriodoMensual;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_TipoProceso = str_TipoProceso;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\ConsultarRevelacionesContSoc.config", this);
        }

    }

}