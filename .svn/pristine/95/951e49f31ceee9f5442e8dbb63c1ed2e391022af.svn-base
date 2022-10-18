using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsBuscarFormulario:clsProcedimientoAlmacenado
    {
        private string lstr_IdRevelacion;
        public string Lstr_IdRevelacion
        {
            get { return lstr_IdRevelacion; }
            set { lstr_IdRevelacion = value; }
        }

        private string lstr_PeriodoMensual;
        public string Lstr_PeriodoMensual
        {
            get { return lstr_PeriodoMensual; }
            set { lstr_PeriodoMensual = value; }
        }

        private string lstr_GrupoCuentas;
        public string Lstr_GrupoCuentas
        {
            get { return lstr_GrupoCuentas; }
            set { lstr_GrupoCuentas = value; }
        }

        private string lstr_Cuentas;
        public string Lstr_Cuentas
        {
            get { return lstr_Cuentas; }
            set { lstr_Cuentas = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        private string lstr_Institucion;
        public string Lstr_Institucion
        {
            get { return lstr_Institucion; }
            set { lstr_Institucion = value; }
        }

        private string lstr_PeriodoAnual;
        public string Lstr_PeriodoAnual
        {
            get { return lstr_PeriodoAnual; }
            set { lstr_PeriodoAnual = value; }
        }


        public clsBuscarFormulario(string str_IdRevelacion, string str_PeriodoMensual, string str_GrupoCuentas, string str_Cuentas, string str_UsrCreacion,string str_Institucion,string str_Annio)
        {
            lstr_IdRevelacion = str_IdRevelacion;
            lstr_PeriodoMensual = str_PeriodoMensual;
            lstr_GrupoCuentas = str_GrupoCuentas;
            lstr_Cuentas = str_Cuentas;
            lstr_UsrCreacion = str_UsrCreacion;
            lstr_Institucion = str_Institucion;
            lstr_PeriodoAnual = str_Annio;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\BuscarFormulario.config", this);
        }

    }
}