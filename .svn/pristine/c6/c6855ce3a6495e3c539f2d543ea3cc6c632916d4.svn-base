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
    public class clsEmpresasAutorizados
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdPersonaJuridica;
        public string Lstr_IdPersonaJuridica
        {
            get { return lstr_IdPersonaJuridica; }
            set { lstr_IdPersonaJuridica = value; }
        }

        private string lstr_TipoIdPersonaAutorizada;
        public string Lstr_TipoIdPersonaAutorizada
        {
            get { return lstr_TipoIdPersonaAutorizada; }
            set { lstr_TipoIdPersonaAutorizada = value; }
        }

        private string lstr_IdPersonaAutorizada;
        public string Lstr_IdPersonaAutorizada
        {
            get { return lstr_IdPersonaAutorizada; }
            set { lstr_IdPersonaAutorizada = value; }
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

        private string lstr_CtaCliente;
        public string Lstr_CtaCliente
        {
            get { return lstr_CtaCliente; }
            set { lstr_CtaCliente = value; }
        }

        private string lstr_NombrePersonaAutorizada;
        public string Lstr_NombrePersonaAutorizada
        {
            get { return lstr_NombrePersonaAutorizada; }
            set { lstr_NombrePersonaAutorizada = value; }
        }


        private string lstr_PuestoPersonaAutorizada;
        public string Lstr_PuestoPersonaAutorizada
        {
            get { return lstr_PuestoPersonaAutorizada; }
            set { lstr_PuestoPersonaAutorizada = value; }
        }

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


        public clsEmpresasAutorizados()
        { }
    }
}