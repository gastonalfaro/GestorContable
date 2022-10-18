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
    public class tCentroBeneficio
    {
        private string lstr_IdCentroBeneficio;
        public string Lstr_IdCentroBeneficio
        {
            get { return lstr_IdCentroBeneficio; }
            set { lstr_IdCentroBeneficio = value; }
        }

        private DateTime ldt_FchVigencia;
        public DateTime Ldt_FchVigencia
        {
            get { return ldt_FchVigencia; }
            set { ldt_FchVigencia = value; }
        }

        private DateTime ldt_FchVigenciaHasta;
        public DateTime Ldt_FchVigenciaHasta
        {
            get { return ldt_FchVigenciaHasta; }
            set { ldt_FchVigenciaHasta = value; }
        }

        private string lstr_IdSociedadCo;
        public string Lstr_IdSociedadCo
        {
            get { return lstr_IdSociedadCo; }
            set { lstr_IdSociedadCo = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_Denominacion;
        public string Lstr_Denominacion
        {
            get { return lstr_Denominacion; }
            set { lstr_Denominacion = value; }
        }

        private string lstr_NomCentroBeneficio;
        public string Lstr_NomCentroBeneficio
        {
            get { return lstr_NomCentroBeneficio; }
            set { lstr_NomCentroBeneficio = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarCentrosBeneficio(string str_IdCentroBeneficio, DateTime dt_FchVigenciaHasta, string str_IdSociedadCo, string str_IdSociedadFi, DateTime dt_FchConsulta, string str_Denominacion, string str_NomCentroBeneficio)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCentrosBeneficio cr_Procedimiento = new clsConsultarCentrosBeneficio(str_IdCentroBeneficio, dt_FchVigenciaHasta, str_IdSociedadCo, str_IdSociedadFi, dt_FchConsulta, str_Denominacion, str_NomCentroBeneficio);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearCentroBeneficio(string str_IdCentroBeneficio, DateTime? dt_FchVigencia, DateTime? dt_FchVigenciaHasta, string str_IdSociedadCo, string str_IdSociedadFi, string str_Denominacion, string str_NomCentroBeneficio, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearCentroBeneficio cls_ProcCrearCentroBeneficio = new clsCrearCentroBeneficio(str_IdCentroBeneficio, dt_FchVigencia, dt_FchVigenciaHasta, str_IdSociedadCo, str_IdSociedadFi, str_Denominacion, str_NomCentroBeneficio, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearCentroBeneficio.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearCentroBeneficio.Lstr_MensajeRespuesta;
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