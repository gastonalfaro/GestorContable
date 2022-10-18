using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsConsultarCobrosPagos : clsProcedimientoAlmacenado
    {
        #region Parámetros


        private string lstr_IdResolucion;//Identificador único de la resolución dictada en los tribunales de justicia
        private string lstr_IdExpediente;//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
        private string lstr_IdSociedadGL;
        private DateTime? ldt_FchInicio;
        private DateTime? ldt_FchFin;
        private int lint_IdExp;
        private int lint_IdRes;
        private string lstr_EstadoResolucion;

        #endregion

        #region Obtención y asignación

        
        public string Lstr_IdResolucion
        {
            get { return lstr_IdResolucion; }
            set { lstr_IdResolucion = value; }
        }
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }

        public int Lint_IdExp
        {
            get { return lint_IdExp; }
            set { lint_IdExp = value; }
        }
        public int Lint_IdRes
        {
            get { return lint_IdRes; }
            set { lint_IdRes = value; }
        }
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }
        public string Lstr_EstadoResolucion
        {
            get { return lstr_EstadoResolucion; }
            set { lstr_EstadoResolucion = value; }
        }
        public DateTime? Ldt_FchInicio
        {
            get { return ldt_FchInicio; }
            set { ldt_FchInicio = value; }
        }
        public DateTime? Ldt_FchFin
        {
            get { return ldt_FchFin; }
            set { ldt_FchFin = value; }
        }
        #endregion

        #region Constructor
        public clsConsultarCobrosPagos() { }

        public clsConsultarCobrosPagos(
            string lstr_IdExpediente,
            string lstr_IdSociedadGL,
            int lint_IdExp,
            int lint_IdRes,
            string lstr_EstadoResolucion,
            DateTime? ldt_FchInicio,
            DateTime? ldt_FchFin)
        {
            this.lstr_IdExpediente = lstr_IdExpediente;
            this.lstr_IdSociedadGL = lstr_IdSociedadGL;
            this.lint_IdExp = lint_IdExp;
            this.lint_IdRes = lint_IdRes;
            this.lstr_EstadoResolucion = lstr_EstadoResolucion;
            this.ldt_FchInicio = ldt_FchInicio;
            this.ldt_FchFin = ldt_FchFin;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "Contigentes\\CobrosPagos\\ConsultarCobrosPagos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }



        public bool ConsultarCobrosPagos()
        {
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Contigentes\\CobrosPagos\\ConsultarCobrosPagos.config", this);
                resultFlag = true;
            }
            catch (Exception ex)
            {
                resultFlag = false;
                this.Lstr_MensajeRespuesta = ex.ToString();
            }


            return resultFlag;
        }

        #endregion
    }
}