using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsEliminarTipoAsiento : clsProcedimientoAlmacenado
    {
        private string lstr_Codigo;
        public string Lstr_Codigo
        {
            get { return lstr_Codigo; }
            set { lstr_Codigo = value; }
        }

        private string lstr_CodigoAuxiliar;
        public string Lstr_CodigoAuxiliar
        {
            get { return lstr_CodigoAuxiliar; }
            set { lstr_CodigoAuxiliar = value; }
        }

        private string lstr_CodigoAuxiliar2;
        public string Lstr_CodigoAuxiliar2
        {
            get { return lstr_CodigoAuxiliar2; }
            set { lstr_CodigoAuxiliar2 = value; }
        }

        private string lstr_CodigoAuxiliar3;
        public string Lstr_CodigoAuxiliar3
        {
            get { return lstr_CodigoAuxiliar3; }
            set { lstr_CodigoAuxiliar3 = value; }
        }

        private string lstr_CodigoAuxiliar4;
        public string Lstr_CodigoAuxiliar4
        {
            get { return lstr_CodigoAuxiliar4; }
            set { lstr_CodigoAuxiliar4 = value; }
        }

        private string lstr_CodigoAuxiliar5;
        public string Lstr_CodigoAuxiliar5
        {
            get { return lstr_CodigoAuxiliar5; }
            set { lstr_CodigoAuxiliar5 = value; }
        }
        private string lstr_CodigoAuxiliar6;
        public string Lstr_CodigoAuxiliar6
        {
            get { return lstr_CodigoAuxiliar6; }
            set { lstr_CodigoAuxiliar6 = value; }
        }
        private int? lint_Secuencia;
        public int? Lint_Secuencia
        {
            get { return lint_Secuencia; }
            set { lint_Secuencia = value; }
        }
        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        private string lstr_IdOperacion;
        public string Lstr_IdOperacion
        {
            get { return lstr_IdOperacion; }
            set { lstr_IdOperacion = value; }
        }


        public clsEliminarTipoAsiento(string str_IdModulo, string str_IdOperacion, string str_Codigo,
            string str_CodigoAuxiliar, string str_CodigoAuxiliar2, string str_CodigoAuxiliar3,
            string str_CodigoAuxiliar4, string str_CodigoAuxiliar5 = "", string str_CodigoAuxiliar6 = "",
            int? int_Secuencia = 1)
        {
            lstr_Codigo = str_Codigo;
            lstr_CodigoAuxiliar = str_CodigoAuxiliar;
            lstr_CodigoAuxiliar2 = str_CodigoAuxiliar2;
            lstr_CodigoAuxiliar3 = str_CodigoAuxiliar3;
            lstr_CodigoAuxiliar4 = str_CodigoAuxiliar4;
            lstr_CodigoAuxiliar5 = str_CodigoAuxiliar5;
            lstr_CodigoAuxiliar6 = str_CodigoAuxiliar6;
            lint_Secuencia = int_Secuencia;
            lstr_IdModulo = str_IdModulo;
            lstr_IdOperacion = str_IdOperacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\EliminarTipoAsiento.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}