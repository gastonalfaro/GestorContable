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
    public class tElementoPEP
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdElementoPEP;
        public string Lstr_IdElementoPEP
        {
            get { return lstr_IdElementoPEP; }
            set { lstr_IdElementoPEP = value; }
        }

        private string lstr_NomElementoPEP;
        public string Lstr_NomElementoPEP
        {
            get { return lstr_NomElementoPEP; }
            set { lstr_NomElementoPEP = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarElementosPEP(string str_IdElementoPEP, string str_NomElementoPEP)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarElementosPEP cr_Procedimiento = new clsConsultarElementosPEP(str_IdElementoPEP, str_NomElementoPEP);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearElementoPEP(string str_IdElementoPEP, string str_NomElementoPEP, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearElementoPEP cls_ProcCrearElementoPEP = new clsCrearElementoPEP(str_IdElementoPEP, str_NomElementoPEP, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearElementoPEP.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearElementoPEP.Lstr_MensajeRespuesta;

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