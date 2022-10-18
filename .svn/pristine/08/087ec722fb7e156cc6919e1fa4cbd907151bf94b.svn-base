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
    public class tFondo
    {
        private string lstr_IdFondo;
        public string Lstr_IdFondo
        {
            get { return lstr_IdFondo; }
            set { lstr_IdFondo = value; }
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

        private string lstr_NomFondo;
        public string Lstr_NomFondo
        {
            get { return lstr_NomFondo; }
            set { lstr_NomFondo = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarFondos(string str_IdFondo, string str_IdEntidadCP, string str_Denominacion, string str_NomFondo)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarFondos cr_Procedimiento = new clsConsultarFondos(str_IdFondo, str_IdEntidadCP, str_Denominacion, str_NomFondo);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearFondo(string str_IdFondo, string str_IdEntidadCP, string str_Denominacion, string str_NomFondo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearFondo cls_ProcCrearFondo = new clsCrearFondo(str_IdFondo, str_IdEntidadCP, str_Denominacion, str_NomFondo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearFondo.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearFondo.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch 
            {
            }

            return bool_ResCreacion;
        }


    }
}