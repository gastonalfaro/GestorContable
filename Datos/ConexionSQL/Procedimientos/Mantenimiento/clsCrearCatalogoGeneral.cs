using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearCatalogoGeneral : clsProcedimientoAlmacenado
    {
        private string lstr_AbrevCatalogo;
        public string Lstr_AbrevCatalogo
        {
            get { return lstr_AbrevCatalogo; }
            set { lstr_AbrevCatalogo = value; }
        }


        private string lstr_Nombre;
        public string Lstr_Nombre
        {
            get { return lstr_Nombre; }
            set { lstr_Nombre = value; }
        }
        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }
        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearCatalogoGeneral(string str_AbrevCatalogo, string str_Nombre, string str_IdModulo, string str_Estado, string str_UsrCreacion)
        {
            lstr_AbrevCatalogo = str_AbrevCatalogo;
            lstr_Nombre = str_Nombre;
            lstr_IdModulo = str_IdModulo;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearCatalogoGeneral.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}