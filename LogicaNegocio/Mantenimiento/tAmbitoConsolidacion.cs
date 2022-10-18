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
    public class tAmbitoConsolidacion
    {
        private string lstr_Vista;
        public string Lstr_Vista
        {
            get { return lstr_Vista; }
            set { lstr_Vista = value; }
        }

        private string lstr_IdAmbitoConsolidacion;
        public string Lstr_IdAmbitoConsolidacion
        {
            get { return lstr_IdAmbitoConsolidacion; }
            set { lstr_IdAmbitoConsolidacion = value; }
        }

        private string lstr_NomCorto;
        public string Lstr_NomCorto
        {
            get { return lstr_NomCorto; }
            set { lstr_NomCorto = value; }
        }

        private string lstr_NomAmbito;
        public string Lstr_NomAmbito
        {
            get { return lstr_NomAmbito; }
            set { lstr_NomAmbito = value; }
        }


        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarAmbitosConsolidacion(string str_Vista, string str_IdAmbitoConsolidacion, string str_NomCorto, string str_NomAmbito)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarAmbitosConsolidacion cr_Procedimiento = new clsConsultarAmbitosConsolidacion(str_Vista, str_IdAmbitoConsolidacion, str_NomCorto, str_NomAmbito);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearAmbitoConsolidacion(string str_Vista, string str_IdAmbitoConsolidacion, string str_NomCorto, string str_NomAmbito, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearAmbitoConsolidacion cls_ProcCrearAmbitoConsolidacion = new clsCrearAmbitoConsolidacion(str_Vista, str_IdAmbitoConsolidacion, str_NomCorto, str_NomAmbito, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearAmbitoConsolidacion.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearAmbitoConsolidacion.Lstr_MensajeRespuesta;
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