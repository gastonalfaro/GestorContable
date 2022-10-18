using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CapturaIngresos
{
    public class clsCambiarEstadoFormularioCapturaIngresos : clsProcedimientoAlmacenado
    {

        #region Parámetros

        private int lint_IdFormulario;
        private int lint_Anno;   
        private string lstr_EstadoActual;
        private string lstr_EstadoNuevo;
        private string lstr_ReferenciaDTR;
        private string lstr_Usuario;
        

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

        public string Lstr_EstadoActual
        {
            get { return lstr_EstadoActual; }
            set { lstr_EstadoActual = value; }
        }

        public string Lstr_EstadoNuevo
        {
            get { return lstr_EstadoNuevo; }
            set { lstr_EstadoNuevo = value; }
        }


        public string Lstr_ReferenciaDTR
        {
            get { return lstr_ReferenciaDTR; }
            set { lstr_ReferenciaDTR = value; }
        }
                
        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }



        #endregion

        #region Constructor

        public clsCambiarEstadoFormularioCapturaIngresos(int lint_IdFormulario,
                                                 int lint_Anno,                                                
                                                 string lstr_EstadoActual,
                                                 string lstr_EstadoNuevo,
                                                 string lstr_ReferenciaDTR,
                                                 string lstr_Usuario)
        {
            this.lint_IdFormulario = lint_IdFormulario;
            this.lint_Anno = lint_Anno;
            this.lstr_EstadoActual = lstr_EstadoActual;
            this.lstr_EstadoNuevo = lstr_EstadoNuevo;
            this.lstr_Usuario = lstr_Usuario;
            this.lstr_ReferenciaDTR = lstr_ReferenciaDTR;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CapturaIngresos\\CambiarEstadoFormularioCapturaIngresos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }

            //EjecucionSP("C:\\Users\\anajera\\Documents\\Visual Studio 2013\\Projects\\SistemaGestorV3\\Datos\\ConexionSQL\\Configs\\CapturaIngresos\\InsertarPagosFormularioCaptura.config", this);
            //EjecucionSP("C:\\Versiones\\SistemaGestor\\ConexionSQL\\Configs\\Mantenimiento\\EstadoGiroEstimado.config", this);
        }

        #endregion

    }
}