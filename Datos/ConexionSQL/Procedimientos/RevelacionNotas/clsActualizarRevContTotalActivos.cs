using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsActualizarRevContTotalActivos : clsProcedimientoAlmacenado
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

        private Decimal ldec_MontoActivos;
        public Decimal Ldec_MontoActivos
        {
            get { return ldec_MontoActivos; }
            set { ldec_MontoActivos = value; }
        }

        private string lstr_CantExpActivos;
        public string Lstr_CantExpActivos
        {
            get { return lstr_CantExpActivos; }
            set { lstr_CantExpActivos = value; }
        }

        private Decimal ldec_MontoPasivos;
        public Decimal Ldec_MontoPasivos
        {
            get { return ldec_MontoPasivos; }
            set { ldec_MontoPasivos = value; }
        }

        private DateTime ? lstr_FchModifica;
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


        public clsActualizarRevContTotalActivos(string str_IdRevCont, string str_IdSociedadGL,
            string str_TipoProceso, Decimal dec_MontoActivos, string str_CantExpActivos, Decimal dec_MontoPasivos, DateTime? str_FchModifica, Nullable<Int32> int_Proceso)
        {
            lstr_IdRevCont = str_IdRevCont;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_TipoProceso = str_TipoProceso;
            ldec_MontoActivos = dec_MontoActivos;
            lstr_CantExpActivos = str_CantExpActivos;
            ldec_MontoPasivos = dec_MontoPasivos;
            lstr_FchModifica = str_FchModifica;
            lint_Proceso = int_Proceso;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\ActualizarRevContTotalActivos.config", this);
        }
    }
}