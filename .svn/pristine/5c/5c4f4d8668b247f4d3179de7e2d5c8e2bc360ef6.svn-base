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
    public class TiposicionPresupuestaria
    {
        private string lstr_IdPosPre;
        public string Lstr_IdPosPre
        {
            get { return lstr_IdPosPre; }
            set { lstr_IdPosPre = value; }
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

        private string lstr_Denominacion;
        public string Lstr_Denominacion
        {
            get { return lstr_Denominacion; }
            set { lstr_Denominacion = value; }
        }

        private string lstr_NomPosPre;
        public string Lstr_NomPosPre
        {
            get { return lstr_NomPosPre; }
            set { lstr_NomPosPre = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public DataSet ConsultarPosicionesPresupuestarias(string str_IdPosPre, string str_IdEntidadCP, string str_IdEjercicio, string str_Denominacion, string str_NomPosPre)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarPosicionesPresupuestarias cr_Procedimiento = new clsConsultarPosicionesPresupuestarias(str_IdPosPre, str_IdEntidadCP, str_IdEjercicio, str_Denominacion, str_NomPosPre);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearPosicionPresupuestaria(string str_IdPosPre, string str_IdEntidadCP, string str_IdEjercicio, string str_Denominacion, string str_NomPosPre, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearPosicionPresupuestaria cls_ProcCrearPosicionPresupuestaria = new clsCrearPosicionPresupuestaria(str_IdPosPre, str_IdEntidadCP, str_IdEjercicio, str_Denominacion, str_NomPosPre, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearPosicionPresupuestaria.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearPosicionPresupuestaria.Lstr_MensajeRespuesta;
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