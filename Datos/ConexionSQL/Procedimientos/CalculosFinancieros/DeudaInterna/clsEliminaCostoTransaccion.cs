using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsEliminaCostoTransaccion : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_IdCostoTransaccion;
        private string lstr_Usuario;

        #endregion

        #region Obtención y asignación

        public int Lint_IdCostoTransaccion {
            get { return lint_IdCostoTransaccion; }
            set { lint_IdCostoTransaccion = value; }
        }
        public string Lstr_Usuario {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }
        
        #endregion 

        #region Constructor

        public clsEliminaCostoTransaccion(int lint_IdCostoTransaccion, string lstr_Usuario)
        {
            this.lint_IdCostoTransaccion = lint_IdCostoTransaccion;
            this.lstr_Usuario = lstr_Usuario;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\EliminarCostoTransaccion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}