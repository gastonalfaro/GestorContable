using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsModificarCodigosAsiento : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private Int64 ? lint_Secuencia;
        private DateTime? ldt_FchProgramada;
        private string lstr_IdMoneda;
        private string lstr_Tabla;
        private string lstr_CodAsiento;
        private string lstr_UsrModifica;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public int? Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }
        public Int64? Lint_Secuencia
        {
            get { return lint_Secuencia; }
            set { lint_Secuencia = value; }
        }
        public DateTime? Ldt_FchProgramada
        {
            get { return ldt_FchProgramada; }
            set { ldt_FchProgramada = value; }
        }
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
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

        public clsModificarCodigosAsiento(string lstr_IdPrestamo, int? lint_IdTramo, Int64? lint_Secuencia, DateTime? ldt_FchProgramada,
            string lstr_IdMoneda, string lstr_Tabla, string lstr_CodAsiento, string lstr_UsrModifica)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lint_Secuencia = lint_Secuencia;
            this.ldt_FchProgramada = ldt_FchProgramada;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.lstr_Tabla = lstr_Tabla;
            this.lstr_CodAsiento = lstr_CodAsiento;
            this.lstr_UsrModifica = lstr_UsrModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ModificarCodigosAsiento.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}