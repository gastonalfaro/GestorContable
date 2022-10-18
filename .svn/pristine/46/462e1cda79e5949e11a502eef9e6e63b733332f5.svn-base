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
    public class tCentroGestor
    {
        private string lstr_IdCentroGestor;
        public string Lstr_IdCentroGestor
        {
            get { return lstr_IdCentroGestor; }
            set { lstr_IdCentroGestor = value; }
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

        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
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

        private string lstr_NomCentroGestor;
        public string Lstr_NomCentroGestor
        {
            get { return lstr_NomCentroGestor; }
            set { lstr_NomCentroGestor = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarCentrosGestores(string str_IdCentroGestor, DateTime dt_FchVigenciaHasta, string str_IdEntidadCP, string str_IdSociedadFi, DateTime dt_FchConsulta, string str_Denominacion, string str_NomCentroGestor)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCentrosGestores cr_Procedimiento = new clsConsultarCentrosGestores(str_IdCentroGestor, dt_FchVigenciaHasta, str_IdEntidadCP, str_IdSociedadFi, dt_FchConsulta, str_Denominacion, str_NomCentroGestor);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearCentroGestor(string str_IdCentroGestor, DateTime? dt_FchVigencia, DateTime? dt_FchVigenciaHasta, string str_IdEntidadCP, string str_IdSociedadFi, string str_Denominacion, string str_NomCentroGestor, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearCentroGestor cls_ProcCrearCentroGestor = new clsCrearCentroGestor(str_IdCentroGestor, dt_FchVigencia, dt_FchVigenciaHasta, str_IdEntidadCP, str_IdSociedadFi, str_Denominacion, str_NomCentroGestor, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearCentroGestor.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearCentroGestor.Lstr_MensajeRespuesta;
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