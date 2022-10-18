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
    public class tJerarquia
    {
        private string lstr_Vista;
        public string Lstr_Vista
        {
            get { return lstr_Vista; }
            set { lstr_Vista = value; }
        }

        private string lstr_IdJerarquia;
        public string Lstr_IdJerarquia
        {
            get { return lstr_IdJerarquia; }
            set { lstr_IdJerarquia = value; }
        }

        private string lstr_NomCorto;
        public string Lstr_NomCorto
        {
            get { return lstr_NomCorto; }
            set { lstr_NomCorto = value; }
        }

        private string lstr_NomJerarquia;
        public string Lstr_NomJerarquia
        {
            get { return lstr_NomJerarquia; }
            set { lstr_NomJerarquia = value; }
        }


        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarJerarquias(string str_Vista, string str_IdJerarquia, string str_NomCorto, string str_NomJerarquia)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarJerarquias cr_Procedimiento = new clsConsultarJerarquias(str_Vista, str_IdJerarquia, str_NomCorto, str_NomJerarquia);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearJerarquia(string str_Vista, string str_IdJerarquia, string str_NomCorto, string str_NomJerarquia, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearJerarquia cls_ProcCrearJerarquia = new clsCrearJerarquia(str_Vista, str_IdJerarquia, str_NomCorto, str_NomJerarquia, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearJerarquia.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearJerarquia.Lstr_MensajeRespuesta;
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