using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearNemotecnico : clsProcedimientoAlmacenado
    {
        private string lstr_IdNemotecnico;
        public string Lstr_IdNemotecnico
        {
            get { return lstr_IdNemotecnico; }
            set { lstr_IdNemotecnico = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_NomNemotecnico;
        public string Lstr_NomNemotecnico
        {
            get { return lstr_NomNemotecnico; }
            set { lstr_NomNemotecnico = value; }
        }


        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }


        private string lstr_TipoNemotecnico;
        public string Lstr_TipoNemotecnico
        {
            get { return lstr_TipoNemotecnico; }
            set { lstr_TipoNemotecnico = value; }
        }

        private string lstr_IdTasa;
        public string Lstr_IdTasa
        {
            get { return lstr_IdTasa; }
            set { lstr_IdTasa = value; }
        }

        private string lstr_ModuloSINPE;
        public string Lstr_ModuloSINPE
        {
            get { return lstr_ModuloSINPE; }
            set { lstr_ModuloSINPE = value; }
        }

        private string lstr_IdCuentaContableCP;
        public string Lstr_IdCuentaContableCP
        {
            get { return lstr_IdCuentaContableCP; }
            set { lstr_IdCuentaContableCP = value; }
        }

        private string lstr_IdCuentaContableLP;
        public string Lstr_IdCuentaContableLP
        {
            get { return lstr_IdCuentaContableLP; }
            set { lstr_IdCuentaContableLP = value; }
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

        public clsCrearNemotecnico(string str_IdNemotecnico, string str_IdSociedadFi, string str_NomNemotecnico, string str_IdMoneda, string str_TipoNemotecnico, string str_IdTasa, string str_ModuloSINPE, string str_IdCuentaContableCP, string str_IdCuentaContableLP, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdNemotecnico = str_IdNemotecnico;
            lstr_IdSociedadFi = str_IdSociedadFi;
            lstr_NomNemotecnico = str_NomNemotecnico;
            lstr_Estado = str_Estado;
            lstr_IdMoneda = str_IdMoneda;
            lstr_TipoNemotecnico = str_TipoNemotecnico;
            lstr_IdTasa = str_IdTasa;
            lstr_ModuloSINPE = str_ModuloSINPE;
            lstr_IdCuentaContableCP = str_IdCuentaContableCP;
            lstr_IdCuentaContableLP = str_IdCuentaContableLP;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearNemotecnico.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}