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
    public class tAreaFuncional
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdAreaFuncional;
        public string Lstr_IdAreaFuncional
        {
            get { return lstr_IdAreaFuncional; }
            set { lstr_IdAreaFuncional = value; }
        }

        private string lstr_NomAreaFuncional;
        public string Lstr_NomAreaFuncional
        {
            get { return lstr_NomAreaFuncional; }
            set { lstr_NomAreaFuncional = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarAreasFuncionales(string str_IdAreaFuncional = null, string str_NomAreaFuncional = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarAreasFuncionales cr_Procedimiento = new clsConsultarAreasFuncionales(str_IdAreaFuncional, str_NomAreaFuncional);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));                
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearAreaFuncional(string str_IdAreaFuncional, string str_NomAreaFuncional, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearAreaFuncional cls_ProcCrearAreaFuncional = new clsCrearAreaFuncional(str_IdAreaFuncional, str_NomAreaFuncional, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearAreaFuncional.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearAreaFuncional.Lstr_MensajeRespuesta;

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