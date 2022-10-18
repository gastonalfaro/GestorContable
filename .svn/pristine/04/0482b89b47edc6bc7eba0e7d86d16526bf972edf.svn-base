using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CapturaIngresos
{
    public class clsDeshabilitarPago : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_IdFormulario;
        private int lint_Anno;
        private int lint_IdPago;
        private string lstr_Estado;
        private string lstr_UsrModifica;
        private DateTime ldt_FchModifica;

        #endregion

        #region Obtención y asignación

        public int Lint_IdFormulario
        {
            get { return lint_IdFormulario; }
            set { lint_IdFormulario = value; }
        }

        public int Lint_Anno
        {
            get { return lint_Anno; }
            set { lint_Anno = value; }
        }
                
        public int Lint_IdPago
        {
            get { return lint_IdPago; }
            set { lint_IdPago = value; }
        }
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }

        #endregion

        #region Constructor

        public clsDeshabilitarPago(int lint_IdFormulario, int lint_Anno, int lint_IdPago, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            this.lint_IdFormulario = lint_IdFormulario;
            this.lint_Anno = lint_Anno;
            this.lint_IdPago = lint_IdPago;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CapturaIngresos\\EstadoPagoFormulario.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}