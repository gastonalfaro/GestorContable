using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaAcreedor : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NroAcreedor;
        private string lstr_Cedula;
        private string lstr_TipoIdAcreedor;
        private string lstr_NomAcreedor;
        private string lstr_Abreviatura;
        private string lstr_Contacto;
        private string lstr_Telefono;
        private string lstr_Direccion;
        private string lstr_Pais;
        private string lstr_TipoAcreedor;
        private string lstr_PaisInstitucion;
        private string lstr_CatPersona;
        private string lstr_TipoPersona;
        private string lstr_IdCtaContable;
        private string lstr_Estado;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public int Lint_NroAcreedor
        {
            get { return lint_NroAcreedor; }
            set { lint_NroAcreedor = value; }
        }
        public string Lstr_Cedula
        {
            get { return lstr_Cedula; }
            set { lstr_Cedula = value; }
        }
        public string Lstr_TipoIdAcreedor
        {
            get { return lstr_TipoIdAcreedor; }
            set { lstr_TipoIdAcreedor = value; }
        }
        public string Lstr_NomAcreedor
        {
            get { return lstr_NomAcreedor; }
            set { lstr_NomAcreedor = value; }
        }
        public string Lstr_Abreviatura
        {
            get { return lstr_Abreviatura; }
            set { lstr_Abreviatura = value; }
        }
        public string Lstr_Contacto
        {
            get { return lstr_Contacto; }
            set { lstr_Contacto = value; }
        }
        public string Lstr_Telefono
        {
            get { return lstr_Telefono; }
            set { lstr_Telefono = value; }
        }
        public string Lstr_Direccion
        {
            get { return lstr_Direccion; }
            set { lstr_Direccion = value; }
        }
        public string Lstr_Pais
        {
            get { return lstr_Pais; }
            set { lstr_Pais = value; }
        }
        public string Lstr_TipoAcreedor
        {
            get { return lstr_TipoAcreedor; }
            set { lstr_TipoAcreedor = value; }
        }
        public string Lstr_PaisInstitucion
        {
            get { return lstr_PaisInstitucion; }
            set { lstr_PaisInstitucion = value; }
        }
        public string Lstr_CatPersona
        {
            get { return lstr_CatPersona; }
            set { lstr_CatPersona = value; }
        }
        public string Lstr_TipoPersona
        {
            get { return lstr_TipoPersona; }
            set { lstr_TipoPersona = value; }
        }
        public string Lstr_IdCtaContable
        {
            get { return lstr_IdCtaContable; }
            set { lstr_IdCtaContable = value; }
        }
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaAcreedor(int lint_NroAcreedor, string lstr_Cedula, string lstr_TipoIdAcreedor, string lstr_NomAcreedor, string lstr_Abreviatura, string lstr_Contacto, string lstr_Telefono,
            string lstr_Direccion, string lstr_Pais, string lstr_TipoAcreedor, string lstr_PaisInstitucion, string lstr_CatPersona, string lstr_TipoPersona, string lstr_IdCtaContable, string lstr_Estado,
            string lstr_UsrCreacion)
        {
            this.lint_NroAcreedor = lint_NroAcreedor;
            this.lstr_Cedula = lstr_Cedula;
            this.lstr_TipoIdAcreedor = lstr_TipoIdAcreedor;
            this.lstr_NomAcreedor = lstr_NomAcreedor;
            this.lstr_Abreviatura = lstr_Abreviatura;
            this.lstr_Contacto = lstr_Contacto;
            this.lstr_Telefono = lstr_Telefono;
            this.lstr_Direccion = lstr_Direccion;
            this.lstr_Pais = lstr_Pais;
            this.lstr_TipoAcreedor = lstr_TipoAcreedor;
            this.lstr_PaisInstitucion = lstr_PaisInstitucion;
            this.lstr_CatPersona = lstr_CatPersona;
            this.lstr_TipoPersona = lstr_TipoPersona;
            this.lstr_IdCtaContable = lstr_IdCtaContable;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearAcreedor.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}