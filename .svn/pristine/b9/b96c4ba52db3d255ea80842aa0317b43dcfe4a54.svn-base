using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaTitulosCanjeSubasta : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_NroEmisionSerie;
        private string lstr_TituloCompraEmision;
        private int lint_NumValor;
        private string lstr_Nemotecnico;
        private DateTime ldt_FchCanje;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public string Lstr_NroEmisionSerie {
            get { return lstr_NroEmisionSerie; }
            set { lstr_NroEmisionSerie = value; }
        }

        public string Lstr_TituloCompraEmision
        {
            get { return lstr_TituloCompraEmision; }
            set { lstr_TituloCompraEmision = value; }
        }

         public int Lint_NumValor {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
        public string Lstr_Nemotecnico{
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }

        public DateTime Ldt_FchCanje {
            get { return ldt_FchCanje; }
            set { ldt_FchCanje = value; }
        }
        public string Lstr_UsrCreacion {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaTitulosCanjeSubasta(string lstr_NroEmisionSerie, int lint_NumValor, string lstr_Nemotecnico, DateTime ldt_FchCanje, string lstr_TituloCompraEmision, string lstr_UsrCreacion)
        {
            this.lstr_NroEmisionSerie = lstr_NroEmisionSerie;
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.ldt_FchCanje = ldt_FchCanje;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            this.lstr_TituloCompraEmision = lstr_TituloCompraEmision;
            try
            {
              
                
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearTitulosCanjeSubasta.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}