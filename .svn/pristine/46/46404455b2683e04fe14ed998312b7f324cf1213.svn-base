using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarServicio : clsProcedimientoAlmacenado
    {
        private string lstr_IdServicio;
        public string Lstr_IdServicio
        {
            get { return lstr_IdServicio; }
            set { lstr_IdServicio = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_IdOficina;
        public string Lstr_IdOficina
        {
            get { return lstr_IdOficina; }
            set { lstr_IdOficina = value; }
        }

        private string lstr_NomServicio;
        public string Lstr_NomServicio
        {
            get { return lstr_NomServicio; }
            set { lstr_NomServicio = value; }
        }

        private decimal? ldec_Monto;
        public decimal? Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }

        private string lstr_PermiteReserva;
        public string Lstr_PermiteReserva
        {
            get { return lstr_PermiteReserva; }
            set { lstr_PermiteReserva = value; }
        }

        private string lstr_CtaContableDebeActualDev;
        public string Lstr_CtaContableDebeActualDev
        {
            get { return lstr_CtaContableDebeActualDev; }
            set { lstr_CtaContableDebeActualDev = value; }
        }

        private string lstr_CtaContableHaberActualDev;
        public string Lstr_CtaContableHaberActualDev
        {
            get { return lstr_CtaContableHaberActualDev; }
            set { lstr_CtaContableHaberActualDev = value; }
        }

        private string lstr_IdPosPreActualDev;
        public string Lstr_IdPosPreActualDev
        {
            get { return lstr_IdPosPreActualDev; }
            set { lstr_IdPosPreActualDev = value; }
        }


        private string lstr_CtaContableDebeActualPer;
        public string Lstr_CtaContableDebeActualPer
        {
            get { return lstr_CtaContableDebeActualPer; }
            set { lstr_CtaContableDebeActualPer = value; }
        }

        private string lstr_CtaContableHaberActualPer;
        public string Lstr_CtaContableHaberActualPer
        {
            get { return lstr_CtaContableHaberActualPer; }
            set { lstr_CtaContableHaberActualPer = value; }
        }

        private string lstr_IdPosPreActualPer;
        public string Lstr_IdPosPreActualPer
        {
            get { return lstr_IdPosPreActualPer; }
            set { lstr_IdPosPreActualPer = value; }
        }

        private string lstr_CtaContableDebeVencidoDev;
        public string Lstr_CtaContableDebeVencidoDev
        {
            get { return lstr_CtaContableDebeVencidoDev; }
            set { lstr_CtaContableDebeVencidoDev = value; }
        }


        private string lstr_CtaContableHaberVencidoDev;
        public string Lstr_CtaContableHaberVencidoDev
        {
            get { return lstr_CtaContableHaberVencidoDev; }
            set { lstr_CtaContableHaberVencidoDev = value; }
        }

        private string lstr_IdPosPreVencidoDev;
        public string Lstr_IdPosPreVencidoDev
        {
            get { return lstr_IdPosPreVencidoDev; }
            set { lstr_IdPosPreVencidoDev = value; }
        }

        private string lstr_CtaContableDebeVencidoPer;
        public string Lstr_CtaContableDebeVencidoPer
        {
            get { return lstr_CtaContableDebeVencidoDev; }
            set { lstr_CtaContableDebeVencidoDev = value; }
        }
        private string lstr_CtaContableHaberVencidoPer;
        public string Lstr_CtaContableHaberVencidoPer
        {
            get { return lstr_CtaContableHaberVencidoPer; }
            set { lstr_CtaContableHaberVencidoPer = value; }
        }

        private string lstr_IdPosPreVencidoPer;
        public string Lstr_IdPosPreVencidoPer
        {
            get { return lstr_IdPosPreVencidoPer; }
            set { lstr_IdPosPreVencidoPer = value; }
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

        public clsModificarServicio(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, string str_NomServicio, decimal? dec_Monto, string str_PermiteReserva,
            string str_CtaContableDebeActualDev, string str_CtaContableHaberActualDev, string str_IdPosPreActualDev,
            string str_CtaContableDebeActualPer, string str_CtaContableHaberActualPer, string str_IdPosPreActualPer,
            string str_CtaContableDebeVencidoDev, string str_CtaContableHaberVencidoDev, string str_IdPosPreVencidoDev, 
            string str_CtaContableDebeVencidoPer, string str_CtaContableHaberVencidoPer, string str_IdPosPreVencidoPer, 
            string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lstr_IdServicio = str_IdServicio;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_IdOficina = str_IdOficina;
            lstr_NomServicio = str_NomServicio;
            lstr_Estado = str_Estado;
            ldec_Monto = dec_Monto;
            lstr_PermiteReserva = str_PermiteReserva;
            lstr_CtaContableDebeActualDev = str_CtaContableDebeActualDev;
            lstr_CtaContableHaberActualDev = str_CtaContableHaberActualDev;
            lstr_IdPosPreActualDev = str_IdPosPreActualDev;
            lstr_CtaContableDebeActualPer = str_CtaContableDebeActualPer;
            lstr_CtaContableHaberActualPer = str_CtaContableHaberActualPer;
            lstr_IdPosPreActualPer = str_IdPosPreActualPer;
            lstr_CtaContableDebeVencidoDev = str_CtaContableDebeVencidoDev;
            lstr_CtaContableHaberVencidoDev = str_CtaContableHaberVencidoDev;
            lstr_IdPosPreVencidoDev = str_IdPosPreVencidoDev;
            lstr_CtaContableDebeVencidoPer = str_CtaContableDebeVencidoPer;
            lstr_CtaContableHaberVencidoPer = str_CtaContableHaberVencidoPer;
            lstr_IdPosPreVencidoPer = str_IdPosPreVencidoPer;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarServicio.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}