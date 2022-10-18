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
    public class tAsignacionACUC
    {
        private string lstr_Vista;
        public string Lstr_Vista
        {
            get { return lstr_Vista; }
            set { lstr_Vista = value; }
        }

        private string lstr_Version;
        public string Lstr_Version
        {
            get { return lstr_Version; }
            set { lstr_Version = value; }
        }


        private string lstr_IdAmbitoConsolidacion;
        public string Lstr_IdAmbitoConsolidacion
        {
            get { return lstr_IdAmbitoConsolidacion; }
            set { lstr_IdAmbitoConsolidacion = value; }
        }


        private string lstr_IdUnidadConsolidacion;
        public string Lstr_IdUnidadConsolidacion
        {
            get { return lstr_IdUnidadConsolidacion; }
            set { lstr_IdUnidadConsolidacion = value; }
        }

        private string lstr_IdEjercicio;
        public string Lstr_IdEjercicio
        {
            get { return lstr_IdEjercicio; }
            set { lstr_IdEjercicio = value; }
        }

        private string lstr_IdPeriodo;
        public string Lstr_IdPeriodo
        {
            get { return lstr_IdPeriodo; }
            set { lstr_IdPeriodo = value; }
        }

        private Boolean lbln_EsUnidad;
        public Boolean Lbln_EsUnidad
        {
            get { return lbln_EsUnidad; }
            set { lbln_EsUnidad = value; }
        }

        public DataSet ConsultarAsignacionesACUC(string str_Vista, string str_Version, string str_IdAmbitoConsolidacion, string str_IdUnidadConsolidacion, string str_IdEjercicio, string str_IdPeriodo, Boolean bln_EsUnidad)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarAsignacionesACUC cr_Procedimiento = new clsConsultarAsignacionesACUC(str_Vista, str_Version, str_IdAmbitoConsolidacion, str_IdUnidadConsolidacion, str_IdEjercicio, str_IdPeriodo, bln_EsUnidad);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearAsignacionACUC(string str_Vista, string str_Version, string str_IdAmbitoConsolidacion, string str_IdUnidadConsolidacion, string str_IdEjercicio, string str_IdPeriodo, Boolean bln_EsUnidad, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearAsignacionACUC cls_ProcCrearAsignacionACUC = new clsCrearAsignacionACUC(str_Vista, str_Version, str_IdAmbitoConsolidacion, str_IdUnidadConsolidacion, str_IdEjercicio, str_IdPeriodo, bln_EsUnidad, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearAsignacionACUC.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearAsignacionACUC.Lstr_MensajeRespuesta;
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