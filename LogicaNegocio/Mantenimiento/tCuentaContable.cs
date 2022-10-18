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
    public class tCuentaContable
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

        private string lstr_IdGrupoCuenta;
        public string Lstr_IdGrupoCuenta
        {
            get { return lstr_IdGrupoCuenta; }
            set { lstr_IdGrupoCuenta = value; }
        }

        private string lstr_NomCorto;
        public string Lstr_NomCorto
        {
            get { return lstr_NomCorto; }
            set { lstr_NomCorto = value; }
        }

        private string lstr_NomCuentaContable;
        public string Lstr_NomCuentaContable
        {
            get { return lstr_NomCuentaContable; }
            set { lstr_NomCuentaContable = value; }
        }

        private string lstr_CuentaGrupo;
        public string Lstr_CuentaGrupo
        {
            get { return lstr_CuentaGrupo; }
            set { lstr_CuentaGrupo = value; }
        }

        private string lstr_IndTotales;
        public string Lstr_IndTotales
        {
            get { return lstr_IndTotales; }
            set { lstr_IndTotales = value; }
        }

        private string lstr_IndConsolidacion;
        public string Lstr_IndConsolidacion
        {
            get { return lstr_IndConsolidacion; }
            set { lstr_IndConsolidacion = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarCuentasContables(string str_IdCuentaContable, string str_IdPlanCuenta, string str_IdGrupoCuenta, string str_NomCuenta, string str_CuentaGrupo, string str_IndTotales, string str_IndConsolidacion, string str_IdSociedadFi)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCuentasContables cr_Procedimiento = new clsConsultarCuentasContables(str_IdCuentaContable, str_IdPlanCuenta, str_IdGrupoCuenta, str_NomCuenta, str_CuentaGrupo, str_IndTotales, str_IndConsolidacion, str_IdSociedadFi);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearCuentaContable(string str_IdCuentaContable, string str_IdPlanCuenta, string str_IdGrupoCuenta, string str_NomCorto, string str_NomCuentaContable, string str_CuentaGrupo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = string.Empty;
            str_Mensaje = string.Empty;
            try
            {
                clsCrearCuentaContable cls_ProcCrearCuentaContable = new clsCrearCuentaContable(str_IdCuentaContable, str_IdPlanCuenta, str_IdGrupoCuenta, str_NomCorto, str_NomCuentaContable, str_CuentaGrupo, string.Empty, "S", str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearCuentaContable.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearCuentaContable.Lstr_MensajeRespuesta;

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

        public bool CrearCuentaContableConsolida(string str_IdCuentaContable, string str_IdPlanCuenta, string str_NomCuentaContable, string str_IndTotales, string str_IndConsolidacion, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = string.Empty;
            str_Mensaje = string.Empty;
            try
            {
                clsCrearCuentaContable cls_ProcCrearCuentaContable = new clsCrearCuentaContable(str_IdCuentaContable, str_IdPlanCuenta, string.Empty, string.Empty, str_NomCuentaContable, string.Empty, str_IndTotales, str_IndConsolidacion, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearCuentaContable.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearCuentaContable.Lstr_MensajeRespuesta;

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

        public bool CrearCuentaSociedad(string str_IdCuentaContable, string str_IdSocieadadFi, string str_IdMoneda, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearCuentaSociedad cls_ProcCrearCuentaSociedad = new clsCrearCuentaSociedad(str_IdCuentaContable, str_IdSocieadadFi, str_IdMoneda, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearCuentaSociedad.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearCuentaSociedad.Lstr_MensajeRespuesta;

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

        public DataSet ConsultarCuentasSociedades(string str_IdCuentaContable, string str_IdSociedadFi, string str_IdMoneda)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCuentasSociedades cr_Procedimiento = new clsConsultarCuentasSociedades(str_IdCuentaContable, str_IdSociedadFi, str_IdMoneda);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public DataSet ConsultarCuentasContablesTipo (string str_GrupoCuentas)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCuentaContablesTipo cr_Procedimiento = new clsConsultarCuentaContablesTipo(str_GrupoCuentas);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

    }
}