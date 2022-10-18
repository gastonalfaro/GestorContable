using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsModificarRevelacionPendiente : clsProcedimientoAlmacenado
    {
        private string lstr_IdRevelacionPendiente;
        public string Lstr_IdRevelacionPendiente
        {
            get { return lstr_IdRevelacionPendiente; }
            set { lstr_IdRevelacionPendiente = value; }
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

        private string lstr_PlanCuentas;
        public string Lstr_PlanCuentas
        {
            get { return lstr_PlanCuentas; }
            set { lstr_PlanCuentas = value; }
        }

        private string lstr_ClaseCuentas;
        public string Lstr_ClaseCuentas
        {
            get { return lstr_ClaseCuentas; }
            set { lstr_ClaseCuentas = value; }
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

        private string lstr_FchModifica;
        public string Lstr_FchModifica
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

        public clsModificarRevelacionPendiente(string str_IdRevelacionPendiente, string str_Institucion,
            string str_Entidad, string str_IdOficina, string str_PlanCuentas, string str_ClaseCuenta, string str_Cuentas,
            string str_Concepto, string str_Justificacion, string str_EstadoRevelacion, string str_FchModifica, string str_UsrModifica,
            string str_RubroCuenta, string str_SubCuenta, string str_SubCuentaAnexa, string str_AuxiliarCuenta)
        {
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            lstr_IdRevelacionPendiente = str_IdRevelacionPendiente;
            lstr_Institucion = str_Institucion;
            lstr_Entidad = str_Entidad;
            lstr_IdOficina = str_IdOficina;
            lstr_PlanCuentas = str_PlanCuentas;
            lstr_ClaseCuentas = str_ClaseCuenta;
            lstr_Cuentas = str_Cuentas;
            lstr_Concepto = str_Concepto;
            lstr_Justificacion = str_Justificacion;
            lstr_EstadoRevelacion = str_EstadoRevelacion;
            lstr_FchModifica = str_FchModifica;
            lstr_UsrModifica = str_UsrModifica;
            //---------------------Campos agregados     ---------------
            lstr_RubroCuenta = str_RubroCuenta;
            lstr_SubCuenta = str_SubCuenta;
            lstr_SubCuentaAnexa = str_SubCuentaAnexa;
            lstr_AuxiliarCuenta = str_AuxiliarCuenta;
            //---------------------------              -----------------
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\ModificarRevelacionPendiente.config", this);
        }
    }
}