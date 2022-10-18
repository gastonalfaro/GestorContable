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
    public class tJerarquiaPeriodo
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


        private string lstr_IdAmbitoConsolidacion;
        public string Lstr_IdAmbitoConsolidacion
        {
            get { return lstr_IdAmbitoConsolidacion; }
            set { lstr_IdAmbitoConsolidacion = value; }
        }

        public DataSet ConsultarJerarquiasPeriodo(string str_Vista, string str_IdJerarquia, string str_IdEjercicio, string str_IdPeriodo, string str_IdAmbitoConsolidacion)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarJerarquiasPeriodo cr_Procedimiento = new clsConsultarJerarquiasPeriodo(str_Vista, str_IdJerarquia, str_IdEjercicio, str_IdPeriodo, str_IdAmbitoConsolidacion);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearJerarquiaPeriodo(string str_Vista, string str_IdJerarquia, string str_IdEjercicio, string str_IdPeriodo, string str_IdAmbitoConsolidacion, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearJerarquiaPeriodo cls_ProcCrearJerarquiaPeriodo = new clsCrearJerarquiaPeriodo(str_Vista, str_IdJerarquia, str_IdEjercicio, str_IdPeriodo, str_IdAmbitoConsolidacion, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearJerarquiaPeriodo.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearJerarquiaPeriodo.Lstr_MensajeRespuesta;
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