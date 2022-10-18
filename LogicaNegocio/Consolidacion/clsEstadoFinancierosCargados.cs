using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Consolidacion
{
    public class clsEstadoFinancierosCargados
    {
        #region variables

        private string lstr_IdEntidad;
        private int lint_Periodo;
        private string lstr_UnidadTiempoPeriodo;
        #endregion

        #region obtencion y asignacion

        public string Lstr_IdEntidad
        {
            get { return lstr_IdEntidad; }
            set { lstr_IdEntidad = value; }
        }

        public int Lint_Periodo
        {
            get { return lint_Periodo; }
            set { lint_Periodo = value; }
        }

        public string Lstr_UnidadTiempoPeriodo
        {
            get { return lstr_UnidadTiempoPeriodo; }
            set { lstr_UnidadTiempoPeriodo = value; }
        }
        #endregion

        #region procedimientos
        public DataSet ConsultarEstadoFinancierosCargados(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsConsultarEstadosFinancierosCargados cls_ConsultarEstadosFinancierosCargados = new clsConsultarEstadosFinancierosCargados(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarEstadosFinancierosCargados.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarEstadosFinancierosCargados.Lstr_RespuestaXML)));

                str_CodResultado = cls_ConsultarEstadosFinancierosCargados.Lstr_CodigoResultado;
                str_Mensaje = cls_ConsultarEstadosFinancierosCargados.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public DataSet ConsultarCorreosAutorizacionEstadosFinancierosCargados(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsConsultarCorreosAutorizacionEstadosFinancierosCargados cls_ConsultarEstadosFinancierosCargados = new clsConsultarCorreosAutorizacionEstadosFinancierosCargados(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ConsultarEstadosFinancierosCargados.Lstr_CodigoResultado;
                str_Mensaje = cls_ConsultarEstadosFinancierosCargados.Lstr_MensajeRespuesta;

                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarEstadosFinancierosCargados.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarEstadosFinancierosCargados.Lstr_RespuestaXML)));

                
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }
        #endregion
    }
}