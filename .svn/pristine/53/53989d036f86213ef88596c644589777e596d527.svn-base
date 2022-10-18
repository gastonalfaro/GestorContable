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
    public class tUnidadConsolidacion
    {
        private string lstr_Vista;
        public string Lstr_Vista
        {
            get { return lstr_Vista; }
            set { lstr_Vista = value; }
        }

        private string lstr_IdUnidadConsolidacion;
        public string Lstr_IdUnidadConsolidacion
        {
            get { return lstr_IdUnidadConsolidacion; }
            set { lstr_IdUnidadConsolidacion = value; }
        }

        private string lstr_NomCorto;
        public string Lstr_NomCorto
        {
            get { return lstr_NomCorto; }
            set { lstr_NomCorto = value; }
        }

        private string lstr_NomUnidad;
        public string Lstr_NomUnidad
        {
            get { return lstr_NomUnidad; }
            set { lstr_NomUnidad = value; }
        }
        

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarUnidadesConsolidacion(string str_Vista, string str_IdUnidadConsolidacion, string str_NomCorto, string str_NomUnidad)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarUnidadesConsolidacion cr_Procedimiento = new clsConsultarUnidadesConsolidacion(str_Vista, str_IdUnidadConsolidacion, str_NomCorto, str_NomUnidad);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearUnidadConsolidacion(string str_Vista, string str_IdUnidadConsolidacion, string str_NomCorto, string str_NomUnidad, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearUnidadConsolidacion cls_ProcCrearUnidadConsolidacion = new clsCrearUnidadConsolidacion(str_Vista, str_IdUnidadConsolidacion, str_NomCorto, str_NomUnidad, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearUnidadConsolidacion.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearUnidadConsolidacion.Lstr_MensajeRespuesta;
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