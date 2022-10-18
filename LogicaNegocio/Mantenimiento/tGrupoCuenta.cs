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
    public class tGrupoCuenta
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdGrupoCuenta;
        public string Lstr_IdGrupoCuenta
        {
            get { return lstr_IdGrupoCuenta; }
            set { lstr_IdGrupoCuenta = value; }
        }

        private string lstr_IdPlanCuenta;
        public string Lstr_IdPlanCuenta
        {
            get { return lstr_IdPlanCuenta; }
            set { lstr_IdPlanCuenta = value; }
        }

        private string lstr_CuentaDesde;
        public string Lstr_CuentaDesde
        {
            get { return lstr_CuentaDesde; }
            set { lstr_CuentaDesde = value; }
        }

        private string lstr_CuentaHasta;
        public string Lstr_CuentaHasta
        {
            get { return lstr_CuentaHasta; }
            set { lstr_CuentaHasta = value; }
        }
        private string lstr_NomGrupoCuenta;
        public string Lstr_NomGrupoCuenta
        {
            get { return lstr_NomGrupoCuenta; }
            set { lstr_NomGrupoCuenta = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarGruposCuentas(string str_IdGrupoCuenta, string str_IdPlanCuenta, string str_IdCuentaContable, string str_NomGrupoCuenta)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarGruposCuentas cr_Procedimiento = new clsConsultarGruposCuentas(str_IdGrupoCuenta, str_IdPlanCuenta, str_IdCuentaContable, str_NomGrupoCuenta);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearGrupoCuenta(string str_IdGrupoCuenta, string str_IdPlanCuenta, string str_CuentaDesde, string str_CuentaHasta, string str_NomGrupoCuenta, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearGrupoCuenta cls_ProcCrearGrupoCuenta = new clsCrearGrupoCuenta(str_IdGrupoCuenta, str_IdPlanCuenta, str_CuentaDesde, str_CuentaHasta, str_NomGrupoCuenta, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearGrupoCuenta.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearGrupoCuenta.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
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