using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Consolidacion
{
    public class clsEstadoFinancieroBalanceComprobacion
    {
        #region variables
        private byte lbt_IdEstadoFinanciero;

        private string lstr_IdEntidad;
        private int lint_Periodo;
        private string lstr_UnidadTiempoPeriodo;
        #endregion

        #region obtencion y asignacion

        public byte Lbt_IdEstadoFinanciero
        {
            get { return lbt_IdEstadoFinanciero; }
            set { lbt_IdEstadoFinanciero = value; }
        }

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
        public DataSet BuscarEstadoFinancieroBalanceComprobacion(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarEstadoFinancieroBalanceComprobacion cls_BuscarEstadoFinancieroBalanceComprobacion = new clsBuscarEstadoFinancieroBalanceComprobacion(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarEstadoFinancieroBalanceComprobacion.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarEstadoFinancieroBalanceComprobacion.Lstr_RespuestaXML)));

                str_CodResultado = cls_BuscarEstadoFinancieroBalanceComprobacion.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarEstadoFinancieroBalanceComprobacion.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public bool EliminarEstadoFinancieroBalanceComprobacion(byte bt_IdEstadoFinanciero, string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEliminarEstadoFinancieroBalanceComprobacion cls_EliminarEstadoFinancieroBalanceComprobacion = new clsEliminarEstadoFinancieroBalanceComprobacion(bt_IdEstadoFinanciero, str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_EliminarEstadoFinancieroBalanceComprobacion.Lstr_CodigoResultado;
                str_Mensaje = cls_EliminarEstadoFinancieroBalanceComprobacion.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }

        public DataSet ValidarEstadoFinancieroBalanceComprobacion(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsValidaEstadoFinancieroBalanceComprobacion cls_ValidaEstadoFinancieroBalanceComprobacion = new clsValidaEstadoFinancieroBalanceComprobacion(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ValidaEstadoFinancieroBalanceComprobacion.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ValidaEstadoFinancieroBalanceComprobacion.Lstr_RespuestaXML)));

                str_CodResultado = cls_ValidaEstadoFinancieroBalanceComprobacion.Lstr_CodigoResultado;
                str_Mensaje = cls_ValidaEstadoFinancieroBalanceComprobacion.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        #endregion

    }
}