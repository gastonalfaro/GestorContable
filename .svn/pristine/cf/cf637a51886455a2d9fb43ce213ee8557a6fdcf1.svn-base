using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsBuscarRevelacionContingente : clsProcedimientoAlmacenado
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
        public clsBuscarRevelacionContingente(string str_IdRevCont, string str_PeriodoAnual, string str_PeriodoMensual)
        {
            lstr_IdRevCont = str_IdRevCont;
            lstr_PeriodoAnual = str_PeriodoAnual;
            lstr_PeriodoMensual = str_PeriodoMensual;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\BuscarRevelacionContingente.config", this);
        }
    }
}