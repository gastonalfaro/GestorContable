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
    public class tSociedadCosto
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdSociedadCo;
        public string Lstr_IdSociedadCo
        {
            get { return lstr_IdSociedadCo; }
            set { lstr_IdSociedadCo = value; }
        }

        private string lstr_NomSociedad;
        public string Lstr_NomSociedad
        {
            get { return lstr_NomSociedad; }
            set { lstr_NomSociedad = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarSociedadesCosto(string str_IdSociedadCo, string str_NomSociedad)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarSociedadesCosto cr_Procedimiento = new clsConsultarSociedadesCosto(str_IdSociedadCo, str_NomSociedad);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearSociedadCosto(string str_IdSociedadCo, string str_NomSociedad, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearSociedadCosto cls_ProcCrearSociedadCosto = new clsCrearSociedadCosto(str_IdSociedadCo, str_NomSociedad, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearSociedadCosto.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearSociedadCosto.Lstr_MensajeRespuesta;

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