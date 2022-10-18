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
    public class tPrograma
    {
        private string lstr_IdPrograma;
        public string Lstr_IdPrograma
        {
            get { return lstr_IdPrograma; }
            set { lstr_IdPrograma = value; }
        }

        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
        }

        private string lstr_Denominacion;
        public string Lstr_Denominacion
        {
            get { return lstr_Denominacion; }
            set { lstr_Denominacion = value; }
        }

        private string lstr_NomPrograma;
        public string Lstr_NomPrograma
        {
            get { return lstr_NomPrograma; }
            set { lstr_NomPrograma = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarProgramas(string str_IdPrograma, string str_IdEntidadCP, string str_Denominacion, string str_NomPrograma)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarProgramas cr_Procedimiento = new clsConsultarProgramas(str_IdPrograma, str_IdEntidadCP, str_Denominacion, str_NomPrograma);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearPrograma(string str_IdPrograma, string str_IdEntidadCP, string str_Denominacion, string str_NomPrograma, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearPrograma cls_ProcCrearPrograma = new clsCrearPrograma(str_IdPrograma, str_IdEntidadCP, str_Denominacion, str_NomPrograma, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearPrograma.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearPrograma.Lstr_MensajeRespuesta;
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