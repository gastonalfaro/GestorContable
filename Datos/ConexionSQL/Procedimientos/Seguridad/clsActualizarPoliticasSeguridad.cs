using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsActualizarPoliticasSeguridad : clsProcedimientoAlmacenado
    {
        private string lint_TiempoOcio;
        public string Lint_TiempoOcio
        {
            get { return lint_TiempoOcio; }
            set { lint_TiempoOcio = value; }
        }

        private string lint_MaxSesionesUsuario;
        public string Lint_MaxSesionesUsuario
        {
            get { return lint_MaxSesionesUsuario; }
            set { lint_MaxSesionesUsuario = value; }
        }

        private string lint_MaxNroIntentosFallidos;
        public string Lint_MaxNroIntentosFallidos
        {
            get { return lint_MaxNroIntentosFallidos; }
            set { lint_MaxNroIntentosFallidos = value; }
        }

        private string lint_MaxVigenciaClave;
        public string Lint_MaxVigenciaClave
        {
            get { return lint_MaxVigenciaClave; }
            set { lint_MaxVigenciaClave = value; }
        }

        private string lint_TiempoBloqueoClave;
        public string Lint_TiempoBloqueoClave
        {
            get { return lint_TiempoBloqueoClave; }
            set { lint_TiempoBloqueoClave = value; }
        }

        private string lint_MinTamanoClave;
        public string Lint_MinTamanoClave
        {
            get { return lint_MinTamanoClave; }
            set { lint_MinTamanoClave = value; }
        }

        private string lint_MinLetrasClave;
        public string Lint_MinLetrasClave
        {
            get { return lint_MinLetrasClave; }
            set { lint_MinLetrasClave = value; }
        }

        private string lint_MinNumerosClave;
        public string Lint_MinNumerosClave
        {
            get { return lint_MinNumerosClave; }
            set { lint_MinNumerosClave = value; }
        }

        private string lint_MinCaracteresClave;
        public string Lint_MinCaracteresClave
        {
            get { return lint_MinCaracteresClave; }
            set { lint_MinCaracteresClave = value; }
        }

        private string lint_NroReutilizacionUltimasClaves;
        public string Lint_NroReutilizacionUltimasClaves
        {
            get { return lint_NroReutilizacionUltimasClaves; }
            set { lint_NroReutilizacionUltimasClaves = value; }
        }

        private string lint_AntiguedadRegistrosBitacora;
        public string Lint_AntiguedadRegistrosBitacora
        {
            get { return lint_AntiguedadRegistrosBitacora; }
            set { lint_AntiguedadRegistrosBitacora = value; }
        }

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        private string ldat_FchModifica;
        public string Ldat_FchModifica
        {
            get { return ldat_FchModifica; }
            set { ldat_FchModifica = value; }
        }

        public clsActualizarPoliticasSeguridad(string int_TiempoOcio, string int_MaxSesionesUsuario, string int_MaxNroIntentosFallidos,
            string int_MaxVigenciaClave, string int_TiempoBloqueoClave, string int_MinTamanoClave, string int_MinLetrasClave,
            string int_MinNumerosClave, string int_MinCaracteresClave, string int_NroReutilizacionUltimasClaves, string int_AntiguedadBitacora,
            string str_UsrModifica, string dat_FchModfica)
        {
            lint_TiempoOcio = int_TiempoOcio;
            lint_MaxSesionesUsuario = int_MaxSesionesUsuario;
            lint_MaxNroIntentosFallidos = int_MaxNroIntentosFallidos;
            lint_MaxVigenciaClave = int_MaxVigenciaClave;
            lint_TiempoBloqueoClave = int_TiempoBloqueoClave;
            lint_MinTamanoClave = int_MinTamanoClave;
            lint_MinLetrasClave = int_MinLetrasClave;
            lint_MinNumerosClave = int_MinNumerosClave;
            lint_MinCaracteresClave = int_MinCaracteresClave;
            lint_NroReutilizacionUltimasClaves = int_NroReutilizacionUltimasClaves;
            lint_AntiguedadRegistrosBitacora = int_AntiguedadBitacora;
            lstr_UsrModifica = str_UsrModifica;
            ldat_FchModifica = dat_FchModfica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ActualizarPoliticasSeguridad.config", this);
        }
    }
}