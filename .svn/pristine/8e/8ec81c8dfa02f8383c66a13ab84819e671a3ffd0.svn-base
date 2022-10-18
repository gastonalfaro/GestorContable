using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Mantenimiento;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.Mantenimiento
{
    public class tCuentaConsolida
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }

        private string lstr_IdPlanCuenta;
        public string Lstr_IdPlanCuenta
        {
            get { return lstr_IdPlanCuenta; }
            set { lstr_IdPlanCuenta = value; }
        }

        //private string lstr_IdGrupoCuenta;
        //public string Lstr_IdGrupoCuenta
        //{
        //    get { return lstr_IdGrupoCuenta; }
        //    set { lstr_IdGrupoCuenta = value; }
        //}

        //private string lstr_NomCorto;
        //public string Lstr_NomCorto
        //{
        //    get { return lstr_NomCorto; }
        //    set { lstr_NomCorto = value; }
        //}

        private string lstr_NomCuentaContable;
        public string Lstr_NomCuentaContable
        {
            get { return lstr_NomCuentaContable; }
            set { lstr_NomCuentaContable = value; }
        }

        //private string lstr_CuentaGrupo;
        //public string Lstr_CuentaGrupo
        //{
        //    get { return lstr_CuentaGrupo; }
        //    set { lstr_CuentaGrupo = value; }
        //}

        private string lstr_IndTipoCuenta;
        public string Lstr_IndTipoCuenta
        {
            get { return lstr_IndTipoCuenta; }
            set { lstr_IndTipoCuenta = value; }
        }

        private string lstr_IndNaturaleza;
        public string Lstr_IndNaturaleza
        {
            get { return lstr_IndNaturaleza; }
            set { lstr_IndNaturaleza = value; }
        }

        //private string lstr_IndConsolidacion;
        //public string Lstr_IndConsolidacion
        //{
        //    get { return lstr_IndConsolidacion; }
        //    set { lstr_IndConsolidacion = value; }
        //}

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

        public bool CrearCuentaContableConsolida(string str_IdCuentaContable, string str_IdPlanCuenta, string str_NomCuentaContable, string str_IndTipoCuenta, string str_IndNaturaleza, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = string.Empty;
            str_Mensaje = string.Empty;
            try
            {
                clsCrearCuentaConsolida cls_ProcCrearCuentaConsolida = new clsCrearCuentaConsolida(str_IdCuentaContable, str_IdPlanCuenta, str_NomCuentaContable, str_IndTipoCuenta, str_IndNaturaleza, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearCuentaConsolida.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearCuentaConsolida.Lstr_MensajeRespuesta;

                //Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

   

    }
}