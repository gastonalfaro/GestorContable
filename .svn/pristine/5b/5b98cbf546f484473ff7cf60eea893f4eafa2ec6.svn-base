using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsModificarCodigosAsientoCo : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdResolucion;
        private int? lint_IdCobroPagoResolucion;
        private int ? lint_IdRes;
        private string  lstr_IdSociedadGL;
        private string lstr_IdExpediente;
        private string lstr_Tabla;
        private string lstr_CodAsiento;
        private string lstr_UsrModifica;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdResolucion
        {
            get { return lstr_IdResolucion; }
            set { lstr_IdResolucion = value; }
        }
        public int? Lint_IdCobroPagoResolucion
        {
            get { return lint_IdCobroPagoResolucion; }
            set { lint_IdCobroPagoResolucion = value; }
        }
        public int? Lint_IdRes
        {
            get { return lint_IdRes; }
            set { lint_IdRes = value; }
        }
        public string  Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        public string Lstr_Tabla
        {
            get { return lstr_Tabla; }
            set { lstr_Tabla = value; }
        }
        public string Lstr_CodAsiento
        {
            get { return lstr_CodAsiento; }
            set { lstr_CodAsiento = value; }
        }
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        #endregion

        #region Constructor

        public clsModificarCodigosAsientoCo(int? lint_IdRes, int? lint_IdCobroPagoResolucion, string lstr_IdResolucion, string lstr_IdExpediente, string lstr_IdSociedadGL,
            string lstr_Tabla, string lstr_CodAsiento, string lstr_UsrModifica)
        {
            this.lstr_IdResolucion = lstr_IdResolucion;
            this.lint_IdCobroPagoResolucion = lint_IdCobroPagoResolucion;
            this.lint_IdRes = lint_IdRes;
            this.lstr_IdSociedadGL = lstr_IdSociedadGL;
            this.lstr_IdExpediente = lstr_IdExpediente;
            this.lstr_Tabla = lstr_Tabla;
            this.lstr_CodAsiento = lstr_CodAsiento;
            this.lstr_UsrModifica = lstr_UsrModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "Contigentes\\ModificarCodigosAsiento.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}