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
    public class tSociedadFinanciera
    {
        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_Denominacion;
        public string Lstr_Denominacion
        {
            get { return lstr_Denominacion; }
            set { lstr_Denominacion = value; }
        }

        private string lstr_NomSociedad;
        public string Lstr_NomSociedad
        {
            get { return lstr_NomSociedad; }
            set { lstr_NomSociedad = value; }
        }

        private string lstr_IdPais;
        public string Lstr_IdPais
        {
            get { return lstr_IdPais; }
            set { lstr_IdPais = value; }
        }

        private string lstr_Poblacion;
        public string Lstr_Poblacion
        {
            get { return lstr_Poblacion; }
            set { lstr_Poblacion = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_IdIdioma;
        public string Lstr_IdIdioma
        {
            get { return lstr_IdIdioma; }
            set { lstr_IdIdioma = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarSociedadesFinancieras(string str_IdSociedadFi, string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_IdMoneda)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarSociedadesFinancieras cr_Procedimiento = new clsConsultarSociedadesFinancieras(str_IdSociedadFi, str_IdSociedadGL, str_Denominacion, str_NomSociedad, str_IdPais, str_IdMoneda);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearSociedadFinanciera(string str_IdSociedadFi, string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_Poblacion, string str_IdMoneda, string str_IdIdioma, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearSociedadFinanciera cls_ProcCrearSociedadFinanciera = new clsCrearSociedadFinanciera(str_IdSociedadFi, str_IdSociedadGL, str_Denominacion, str_NomSociedad, str_IdPais, str_Poblacion, str_IdMoneda, str_IdIdioma, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearSociedadFinanciera.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearSociedadFinanciera.Lstr_MensajeRespuesta;
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