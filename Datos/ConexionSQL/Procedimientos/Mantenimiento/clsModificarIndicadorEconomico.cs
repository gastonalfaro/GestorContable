using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarIndicadorEconomico : clsProcedimientoAlmacenado
    {
        private string lstr_IdIndicadorEconomico;
        public string Lstr_IdIndicadorEconomico
        {
            get { return lstr_IdIndicadorEconomico; }
            set { lstr_IdIndicadorEconomico = value; }
        }

        private string lstr_Transaccion;
        public string Lstr_Transaccion
        {
            get { return lstr_Transaccion; }
            set { lstr_Transaccion = value; }
        }

        private string lstr_NomIndicadorEconomico;
        public string Lstr_NomIndicadorEconomico
        {
            get { return lstr_NomIndicadorEconomico; }
            set { lstr_NomIndicadorEconomico = value; }
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

        public clsModificarIndicadorEconomico(string str_IdIndicadorEconomico, string str_Transaccion, string str_NomIndicadorEconomico, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lstr_IdIndicadorEconomico = str_IdIndicadorEconomico;
            lstr_Transaccion = str_Transaccion;
            lstr_NomIndicadorEconomico = str_NomIndicadorEconomico;
            lstr_Estado = str_Estado;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarIndicadorEconomico.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}