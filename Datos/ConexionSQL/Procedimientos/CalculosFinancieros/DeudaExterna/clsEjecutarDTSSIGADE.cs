using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsEjecutarDTSSIGADE : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_pack_name;
        private string lstr_proj_name;

        #endregion

        #region Obtención y asignación

        public string Lstr_pack_name
        {
            get { return lstr_pack_name; }
            set { lstr_pack_name = value; }
        }
        public string Lstr_proj_name
        {
            get { return lstr_proj_name; }
            set { lstr_proj_name = value; }
        }
       
        #endregion 

        #region Constructor

        public clsEjecutarDTSSIGADE(string lstr_pack_name = "Ejecuta_DTS.dtsx", string lstr_proj_name = "SIGADE")
        {
            /*if (!string.IsNullOrEmpty(lstr_pack_name))
                this.lstr_pack_name = lstr_pack_name;
            if (!string.IsNullOrEmpty(lstr_proj_name))
                this.lstr_proj_name = lstr_proj_name;*/
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\EjecutarDTSSIGADE.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}