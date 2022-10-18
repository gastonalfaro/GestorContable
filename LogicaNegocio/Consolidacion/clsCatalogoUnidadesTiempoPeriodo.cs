using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Consolidacion
{
    public class clsCatalogoUnidadesTiempoPeriodo
    {
        #region variables
        private string lstr_UnidadTiempoPeriodo;
        #endregion

        #region obtencion y asignacion
        public string Lstr_UnidadTiempoPeriodo
        {
            get { return lstr_UnidadTiempoPeriodo; }
            set { lstr_UnidadTiempoPeriodo = value; }
        }

        #endregion

        #region procedimientos
        public DataSet BuscarCatalogoUnidadesTiempoPeriodo(string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarCatalogoUnidadesTiempoPeriodo cls_BuscarCatalogoUnidadesTiempoPeriodo = new clsBuscarCatalogoUnidadesTiempoPeriodo(str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarCatalogoUnidadesTiempoPeriodo.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarCatalogoUnidadesTiempoPeriodo.Lstr_RespuestaXML)));

                str_CodResultado = cls_BuscarCatalogoUnidadesTiempoPeriodo.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarCatalogoUnidadesTiempoPeriodo.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }
        public DataSet ConsultarCatalogoUnidadesTiempoPeriodo(string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsConsultarCatalogoEtapaEstadoFinanciero cls_ConsultarCatalogoEtapaEstadoFinanciero = new clsConsultarCatalogoEtapaEstadoFinanciero(str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarCatalogoEtapaEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarCatalogoEtapaEstadoFinanciero.Lstr_RespuestaXML)));

                str_CodResultado = cls_ConsultarCatalogoEtapaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_ConsultarCatalogoEtapaEstadoFinanciero.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        #endregion

    }
}