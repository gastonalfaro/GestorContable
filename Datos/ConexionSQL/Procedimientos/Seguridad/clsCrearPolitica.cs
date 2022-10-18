using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsCrearPolitica : clsProcedimientoAlmacenado
    {
        private int lint_TiempoOcio;
        public int Lint_TiempoOcio
        {
            get { return lint_TiempoOcio; }
            set { lint_TiempoOcio = value; }
        }

        private int lint_MaxSesionesUsuario;
        public int Lint_MaxSesionesUsuario
        {
            get { return lint_MaxSesionesUsuario; }
            set { lint_MaxSesionesUsuario = value; }
        }

        private int lint_MaxNroIntentosFallidos;
        public int Lint_MaxNroIntentosFallidos
        {
            get { return lint_MaxNroIntentosFallidos; }
            set { lint_MaxNroIntentosFallidos = value; }
        }

        private int lint_MaxVigenciaClave;
        public int Lint_MaxVigenciaClave
        {
            get { return lint_MaxVigenciaClave; }
            set { lint_MaxVigenciaClave = value; }
        }

        private int lint_TiempoBloqueoClave;
        public int Lint_TiempoBloqueoClave
        {
            get { return lint_TiempoBloqueoClave; }
            set { lint_TiempoBloqueoClave = value; }
        }

        private int lint_MinTamanoClave;
        public int Lint_MinTamanoClave
        {
            get { return lint_MinTamanoClave; }
            set { lint_MinTamanoClave = value; }
        }

        private int lint_MinLetrasClave;
        public int Lint_MinLetrasClave
        {
            get { return lint_MinLetrasClave; }
            set { lint_MinLetrasClave = value; }
        }

        private int lint_MinNumerosClave;
        public int Lint_MinNumerosClave
        {
            get { return lint_MinNumerosClave; }
            set { lint_MinNumerosClave = value; }
        }

        private int lint_MinCaracteresClave;
        public int Lint_MinCaracteresClave
        {
            get { return lint_MinCaracteresClave; }
            set { lint_MinCaracteresClave = value; }
        }

        private int lint_NroReutilizacionUltimasClaves;
        public int Lint_NroReutilizacionUltimasClaves
        {
            get { return lint_NroReutilizacionUltimasClaves; }
            set { lint_NroReutilizacionUltimasClaves = value; }
        }

        private int lint_AntiguedadRegistrosBitacora;
        public int Lint_AntiguedadRegistrosBitacora
        {
            get { return lint_AntiguedadRegistrosBitacora; }
            set { lint_AntiguedadRegistrosBitacora = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearPolitica(int int_TiempoOcio, int int_MaxSesionesUsuario, int int_MaxNroIntentosFallidos, 
            int int_MaxVigenciaClave, int int_TiempoBloqueoClave, int int_MinTamanoClave, int int_MinLetrasClave,
            int int_MinNumerosClave, int int_MinCaracteresClave, int int_NroReutilizacionClaves, 
            int int_AntiguedadRegistrosBitacora) 
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
            lint_NroReutilizacionUltimasClaves = int_NroReutilizacionClaves;
            lint_AntiguedadRegistrosBitacora = int_AntiguedadRegistrosBitacora;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\CrearPolitica.config", this);
        }

    }
}