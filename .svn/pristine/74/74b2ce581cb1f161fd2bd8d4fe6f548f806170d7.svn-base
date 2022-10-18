using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarBanco : clsProcedimientoAlmacenado
    {
        private string lstr_IdBanco;
        public string Lstr_IdBanco
        {
            get { return lstr_IdBanco; }
            set { lstr_IdBanco = value; }
        }


        private string lstr_NomBanco;
        public string Lstr_NomBanco
        {
            get { return lstr_NomBanco; }
            set { lstr_NomBanco = value; }
        }

        private string lstr_IdPais;
        public string Lstr_IdPais
        {
            get { return lstr_IdPais; }
            set { lstr_IdPais = value; }
        }

        private string lstr_Telefono;
        public string Lstr_Telefono
        {
            get { return lstr_Telefono; }
            set { lstr_Telefono = value; }
        }

        private string lstr_Contacto;
        public string Lstr_Contacto
        {
            get { return lstr_Contacto; }
            set { lstr_Contacto = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        private DateTime ldt_FchModifica;
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }

        public clsModificarBanco(string str_IdBanco, string str_NomBanco, string str_IdPais, string str_Telefono, string str_Contacto, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lstr_IdBanco = str_IdBanco;
            lstr_NomBanco = str_NomBanco;
            lstr_IdPais = str_IdPais;
            lstr_Telefono = str_Telefono;
            lstr_Contacto = str_Contacto;
            lstr_Estado = str_Estado;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarBanco.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}