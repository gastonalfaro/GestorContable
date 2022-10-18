using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarAcreedores : clsProcedimientoAlmacenado
    {
        private Nullable<Int32> lint_NroAcreedor;
        public Nullable<Int32> Lint_NroAcreedor
        {
            get { return lint_NroAcreedor; }
            set { lint_NroAcreedor = value; }
        }

        private string lstr_NomAcreedor;
        public string Lstr_NomAcreedor
        {
            get { return lstr_NomAcreedor; }
            set { lstr_NomAcreedor = value; }
        }


        public clsConsultarAcreedores(Nullable<Int32> int_NroAcreedor, string str_NomAcreedor)
        {
            lint_NroAcreedor = int_NroAcreedor;
            lstr_NomAcreedor = str_NomAcreedor;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarAcreedores.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}