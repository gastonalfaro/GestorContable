using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsModificarFormulario : clsProcedimientoAlmacenado
    {
        private string lstr_IdRevelacion;
        public string Lstr_IdRevelacion
        {
            get { return lstr_IdRevelacion; }
            set { lstr_IdRevelacion = value; }
        }

        private string lstr_FechaFinal;
        public string Lstr_FechaFinal
        {
            get { return lstr_FechaFinal; }
            set { lstr_FechaFinal = value; }
        }

        private string lstr_Institucion;
        public string Lstr_Institucion
        {
            get { return lstr_Institucion; }
            set { lstr_Institucion = value; }
        }
        private string lstr_Entidad;
        public string Lstr_Entidad
        {
            get { return lstr_Entidad; }
            set { lstr_Entidad = value; }
        }

        private string lstr_IdOficina;
        public string Lstr_IdOficina
        {
            get { return lstr_IdOficina; }
            set { lstr_IdOficina = value; }
        }

        private string lstr_GrupoCuentas;
        public string Lstr_GrupoCuentas
        {
            get { return lstr_GrupoCuentas; }
            set { lstr_GrupoCuentas = value; }
        }
        private string lstr_Cuentas;
        public string Lstr_Cuentas
        {
            get { return lstr_Cuentas; }
            set { lstr_Cuentas = value; }
        }

        private string lstr_Concepto;
        public string Lstr_Concepto
        {
            get { return lstr_Concepto; }
            set { lstr_Concepto = value; }
        }
        private string lstr_Justificacion;
        public string Lstr_Justificacion
        {
            get { return lstr_Justificacion; }
            set { lstr_Justificacion = value; }
        }

        private string lstr_EstadoRevelacion;
        public string Lstr_EstadoRevelacion
        {
            get { return lstr_EstadoRevelacion; }
            set { lstr_EstadoRevelacion = value; }
        }

        private DateTime lstr_FchModifica;
        public DateTime Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        //.......................campos nuevos...............................

        private string lstr_RubroCuenta;
        public string Lstr_RubroCuenta
        {
            get { return lstr_RubroCuenta; }
            set { lstr_RubroCuenta = value; }
        }


        private string lstr_SubCuenta;
        public string Lstr_SubCuenta
        {
            get { return lstr_SubCuenta; }
            set { lstr_SubCuenta = value; }
        }


        private string lstr_SubCuentaAnexa;
        public string Lstr_SubCuentaAnexa
        {
            get { return lstr_SubCuentaAnexa; }
            set { lstr_SubCuentaAnexa = value; }
        }


        private string lstr_AuxiliarCuenta;
        public string Lstr_AuxiliarCuenta
        {
            get { return lstr_AuxiliarCuenta; }
            set { lstr_AuxiliarCuenta = value; }
        }


        public clsModificarFormulario(string str_IdRevelacion, string str_Institucion, string str_Entidad, string str_IdOficina,
            string str_GrupoCuentas, string str_Cuentas, string str_Concepto, string str_Justificacion,
            string str_EstadoRevelacion, string str_FchModifica, string str_UsrModifica,
            string str_RubroCuenta, string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
            
        {
            lstr_IdRevelacion = str_IdRevelacion;
            lstr_Institucion = str_Institucion;
            lstr_Entidad = str_Entidad;
            lstr_IdOficina = str_IdOficina;
            lstr_GrupoCuentas = str_GrupoCuentas;
            lstr_Cuentas = str_Cuentas;
            lstr_Concepto = str_Concepto;
            lstr_Justificacion = str_Justificacion;
            lstr_EstadoRevelacion = str_EstadoRevelacion;
            lstr_FchModifica = Convert.ToDateTime(str_FchModifica);
            lstr_UsrModifica = str_UsrModifica;
            //---------------------Campos agregados     ---------------
            lstr_RubroCuenta = str_RubroCuenta;
            lstr_SubCuenta = str_SubCuenta;
            lstr_SubCuentaAnexa = str_SubCuentaAnexa;
            lstr_AuxiliarCuenta = str_AuxiliarCuenta;
            //---------------------------              -----------------

            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\ModificarFormulario.config", this);
        }
    }
}