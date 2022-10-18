using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.Consolidacion;

namespace LogicaNegocio.Consolidacion
{
    public class clsEstadoFinancierosDeudaPublica
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
        public DataSet BuscarEstadosFinancierosDeudaPublica(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarEstadosFinancierosDeudaPublica cls_BuscarEstadosFinancierosDeudaPublica = new clsBuscarEstadosFinancierosDeudaPublica(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarEstadosFinancierosDeudaPublica.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarEstadosFinancierosDeudaPublica.Lstr_RespuestaXML)));

                str_CodResultado = cls_BuscarEstadosFinancierosDeudaPublica.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarEstadosFinancierosDeudaPublica.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }
       
        public bool EliminarEstadoFinancieroDeudaPublica(byte bt_IdEstadoFinanciero, string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEliminarEstadosFinancierosDeudaPublica cls_EliminarEstadoFinancieroDeudaPublica = new clsEliminarEstadosFinancierosDeudaPublica(bt_IdEstadoFinanciero, str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_EliminarEstadoFinancieroDeudaPublica.Lstr_CodigoResultado;
                str_Mensaje = cls_EliminarEstadoFinancieroDeudaPublica.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }

        public DataSet ValidarEstadoFinancierosDeudaPublica(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsValidaEstadosFinancierosDeudaPublica cls_ValidaEstadosFinancierosDeudaPublica = new clsValidaEstadosFinancierosDeudaPublica(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ValidaEstadosFinancierosDeudaPublica.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ValidaEstadosFinancierosDeudaPublica.Lstr_RespuestaXML)));

                str_CodResultado = cls_ValidaEstadosFinancierosDeudaPublica.Lstr_CodigoResultado;
                str_Mensaje = cls_ValidaEstadosFinancierosDeudaPublica.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        #endregion

    }
}