using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsActualizarRevContTotalPasivos : clsProcedimientoAlmacenado
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

        private Decimal ldec_MontoPasivos;
        public Decimal Ldec_MontoPasivos
        {
            get { return ldec_MontoPasivos; }
            set { ldec_MontoPasivos = value; }
        }

        private string lstr_CantExpPasivos;
        public string Lstr_CantExpPasivos
        {
            get { return lstr_CantExpPasivos; }
            set { lstr_CantExpPasivos = value; }
        }

        private Decimal ldec_MontoActivos;
        public Decimal Ldec_MontoActivos
        {
            get { return ldec_MontoActivos; }
            set { ldec_MontoActivos = value; }
        }

        private DateTime? lstr_FchModifica;
        public DateTime? Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }

        private Nullable<Int32> lint_Proceso;
        public Nullable<Int32> Lint_Proceso
        {
            get { return lint_Proceso; }
            set { lint_Proceso = value; }
        }


        public clsActualizarRevContTotalPasivos(string str_IdRevCont, string str_IdSociedadGL,
            string str_TipoProceso, Decimal dec_MontoPasivos, string str_CantExpPasivos, Decimal dec_MontoActivos, DateTime? str_FchModifica, Nullable<Int32> int_Proceso)
        {
            lstr_IdRevCont = str_IdRevCont;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_TipoProceso = str_TipoProceso;
            ldec_MontoPasivos = dec_MontoPasivos;
            lstr_CantExpPasivos = str_CantExpPasivos;
            ldec_MontoActivos = dec_MontoActivos;
            lstr_FchModifica = str_FchModifica;
            lint_Proceso = int_Proceso;

            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\ActualizarRevContTotalPasivos.config", this);
        }
    }
}