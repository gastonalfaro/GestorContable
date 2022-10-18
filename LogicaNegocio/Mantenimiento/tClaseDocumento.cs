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
    public class tClaseDocumento
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdClaseDocumento;
        public string Lstr_IdClaseDocumento
        {
            get { return lstr_IdClaseDocumento; }
            set { lstr_IdClaseDocumento = value; }
        }

        private string lstr_NomClaseDocumento;
        public string Lstr_NomClaseDocumento
        {
            get { return lstr_NomClaseDocumento; }
            set { lstr_NomClaseDocumento = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarClasesDocumento(string str_IdClaseDocumento, string str_NomClaseDocumento)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarClasesDocumento cr_Procedimiento = new clsConsultarClasesDocumento(str_IdClaseDocumento, str_NomClaseDocumento);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearClaseDocumento(string str_IdClaseDocumento, string str_NomClaseDocumento, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearClaseDocumento cls_ProcCrearClaseDocumento = new clsCrearClaseDocumento(str_IdClaseDocumento, str_NomClaseDocumento, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearClaseDocumento.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearClaseDocumento.Lstr_MensajeRespuesta;

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