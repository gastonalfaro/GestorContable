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
    public class clsEmpresas
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdPersonaJuridica;
        public string Lstr_IdPersonaJuridica
        {
            get { return lstr_IdPersonaJuridica; }
            set { lstr_IdPersonaJuridica = value; }
        }

        private string lstr_Nombre;
        public string Lstr_Nombre
        {
            get { return lstr_Nombre; }
            set { lstr_Nombre = value; }
        }

        private string lstr_CorreoEmpresa;
        public string Lstr_CorreoEmpresa
        {
            get { return lstr_CorreoEmpresa; }
            set { lstr_CorreoEmpresa = value; }
        }

        private string lstr_TelefonoEmpresa;
        public string Lstr_TelefonoEmpresa
        {
            get { return lstr_TelefonoEmpresa; }
            set { lstr_TelefonoEmpresa = value; }
        }

        private string lstr_TipoIdPersonaAutoriza;
        public string Lstr_TipoIdPersonaAutoriza
        {
            get { return lstr_TipoIdPersonaAutoriza; }
            set { lstr_TipoIdPersonaAutoriza = value; }
        }

        private string lstr_IdPersonaAutoriza;
        public string Lstr_IdPersonaAutoriza
        {
            get { return lstr_IdPersonaAutoriza; }
            set { lstr_IdPersonaAutoriza = value; }
        }


        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public DataSet ConsultarEmpresasAutorizados(string str_IdPersonaJuridica, string str_TipoIdPersonaAutorizada, string str_IdPersonaAutorizada, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza, string str_CtaCliente, string str_Estado)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarEmpresasAutorizados cr_Procedimiento = new clsConsultarEmpresasAutorizados(str_IdPersonaJuridica, str_TipoIdPersonaAutorizada, str_IdPersonaAutorizada, str_TipoIdPersonaAutoriza, str_IdPersonaAutoriza, str_CtaCliente, str_Estado);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearEmpresaAutorizado(string str_IdPersonaJuridica, string str_TipoIdPersonaAutorizada, string str_IdPersonaAutorizada, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza, string str_CtaCliente, string str_NombrePersonaAutorizada, string str_PuestoPersonaAutorizada, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearEmpresaAutorizado cls_ProcCrearEmpresaAutorizado = new clsCrearEmpresaAutorizado(str_IdPersonaJuridica, str_TipoIdPersonaAutorizada, str_IdPersonaAutorizada, str_TipoIdPersonaAutoriza, str_IdPersonaAutoriza, str_CtaCliente, str_NombrePersonaAutorizada, str_PuestoPersonaAutorizada, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearEmpresaAutorizado.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearEmpresaAutorizado.Lstr_MensajeRespuesta;

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

        public DataSet ConsultarEmpresas(string str_IdPersonaJuridica, string str_Nombre, string str_IdPersonaAutorizada, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarEmpresas cr_Procedimiento = new clsConsultarEmpresas(str_IdPersonaJuridica, str_Nombre, str_IdPersonaAutorizada, str_TipoIdPersonaAutoriza, str_IdPersonaAutoriza);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearEmpresa(string str_IdPersonaJuridica, string str_Nombre, string str_CorreoEmpresa, string str_TelefonoEmpresa, string str_TipoIdPersonaAutoriza, string str_IdPersonaAutoriza, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearEmpresa cls_ProcCrearEmpresa = new clsCrearEmpresa(str_IdPersonaJuridica, str_Nombre, str_CorreoEmpresa, str_TelefonoEmpresa, str_TipoIdPersonaAutoriza, str_IdPersonaAutoriza, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearEmpresa.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearEmpresa.Lstr_MensajeRespuesta;

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

        public clsEmpresas()
        { }
    }
}