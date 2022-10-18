using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearAcreedor : clsProcedimientoAlmacenado
    {
        private Int32 lint_NroAcreedor;
        public Int32 Lint_NroAcreedor
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

        private string lstr_Abreviatura;
        public string Lstr_Abreviatura
        {
            get { return lstr_Abreviatura; }
            set { lstr_Abreviatura = value; }
        }

        private string lstr_Contacto;
        public string Lstr_Contacto
        {
            get { return lstr_Contacto; }
            set { lstr_Contacto = value; }
        }

        private string lstr_Telefono;
        public string Lstr_Telefono
        {
            get { return lstr_Telefono; }
            set { lstr_Telefono = value; }
        }

        private string lstr_Direccion;
        public string Lstr_Direccion
        {
            get { return lstr_Direccion; }
            set { lstr_Direccion = value; }
        }

        private string lstr_Pais;
        public string Lstr_Pais
        {
            get { return lstr_Pais; }
            set { lstr_Pais = value; }
        }

        private string lstr_TipoAcreedor;
        public string Lstr_TipoAcreedor
        {
            get { return lstr_TipoAcreedor; }
            set { lstr_TipoAcreedor = value; }
        }

        private string lstr_PaisInstitucion;
        public string Lstr_PaisInstitucion
        {
            get { return lstr_PaisInstitucion; }
            set { lstr_PaisInstitucion = value; }
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

        public clsCrearAcreedor(int int_NroAcreedor, string str_NomAcreedor, string str_Abreviatura, string str_Contacto, string str_Telefono, string str_Direccion, string str_Pais, string str_TipoAcreedor, string str_PaisInstitucion, string str_Estado, string str_UsrCreacion)
        {
            lint_NroAcreedor = int_NroAcreedor;
            lstr_NomAcreedor = str_NomAcreedor;
            lstr_Abreviatura = str_Abreviatura;
            lstr_Contacto = str_Contacto;
            lstr_Telefono = str_Telefono;
            lstr_Direccion = str_Direccion;
            lstr_Pais = str_Pais;
            lstr_TipoAcreedor = str_TipoAcreedor;
            lstr_PaisInstitucion = str_PaisInstitucion;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearAcreedor.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

    }
}