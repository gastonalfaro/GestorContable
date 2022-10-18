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
    public class tEntidadCP
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
        }

        private string lstr_NomEntidadCP;
        public string Lstr_NomEntidadCP
        {
            get { return lstr_NomEntidadCP; }
            set { lstr_NomEntidadCP = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarEntidadesCP(string str_IdEntidadCP, string str_NomEntidadCP, string str_Moneda)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarEntidadesCP cr_Procedimiento = new clsConsultarEntidadesCP(str_IdEntidadCP, str_NomEntidadCP, str_Moneda);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearEntidadCP(string str_IdEntidadCP, string str_NomEntidadCP, string str_IdMoneda, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearEntidadCP cls_ProcCrearEntidadCP = new clsCrearEntidadCP(str_IdEntidadCP, str_NomEntidadCP, str_IdMoneda, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearEntidadCP.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearEntidadCP.Lstr_MensajeRespuesta;

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