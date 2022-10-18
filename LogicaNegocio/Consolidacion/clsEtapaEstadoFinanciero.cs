using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.Consolidacion;

namespace LogicaNegocio.Consolidacion
{
    public class clsEtapaEstadoFinanciero
    {
        #region variables

        private string lstr_IdEntidad;
        private int lint_Periodo;
        private string lstr_UnidadTiempoPeriodo;
        private DateTime ldt_FechaDeEtapaEstado;

        private int lint_IdEtapaEstadoFinanciero;
        private string lstr_NotaRazon;
        private string lstr_UsuarioEtapaEstado;
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

        public DateTime Ldt_FechaDeEtapaEstado
        {
            get { return ldt_FechaDeEtapaEstado; }
            set { ldt_FechaDeEtapaEstado = value; }
        }

        public int Lint_IdEtapaEstadoFinanciero
        {
            get { return lint_IdEtapaEstadoFinanciero; }
            set { lint_IdEtapaEstadoFinanciero = value; }
        }

        public string Lstr_NotaRazon
        {
            get { return lstr_NotaRazon; }
            set { lstr_NotaRazon = value; }
        }

        public string Lstr_UsuarioEtapaEstado
        {
            get { return lstr_UsuarioEtapaEstado; }
            set { lstr_UsuarioEtapaEstado = value; }
        }
       
        #endregion

        #region procedimientos
        public DataSet BuscarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, DateTime dt_FechaDeEtapaEstado, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarEtapaEstadoFinanciero cls_BuscarEtapaEstadoFinanciero = new clsBuscarEtapaEstadoFinanciero(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, dt_FechaDeEtapaEstado, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarEtapaEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarEtapaEstadoFinanciero.Lstr_RespuestaXML)));

                str_CodResultado = cls_BuscarEtapaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarEtapaEstadoFinanciero.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public DataSet ConsultarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsConsultarEtapaEstadoFinanciero cls_ConsultarEtapaEstadoFinanciero = new clsConsultarEtapaEstadoFinanciero(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ConsultarEtapaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_ConsultarEtapaEstadoFinanciero.Lstr_MensajeRespuesta;

                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarEtapaEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarEtapaEstadoFinanciero.Lstr_RespuestaXML)));

            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public bool EliminarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, DateTime dt_FechaDeEtapaEstado, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEliminarEtapaEstadoFinanciero cls_EliminarEtapaEstadoFinanciero = new clsEliminarEtapaEstadoFinanciero(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, dt_FechaDeEtapaEstado, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_EliminarEtapaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_EliminarEtapaEstadoFinanciero.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }
       
        public bool InsertarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, int int_IdEtapasEstadoFinanciero, string str_NotaRazon, string str_UsuarioEtapaEstado, DateTime dt_FechaDeEtapaEstado, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsInsertarEtapaEstadoFinanciero cls_InsertarEtapaEstadoFinanciero = new clsInsertarEtapaEstadoFinanciero(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, int_IdEtapasEstadoFinanciero, str_NotaRazon, str_UsuarioEtapaEstado, dt_FechaDeEtapaEstado, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_InsertarEtapaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_InsertarEtapaEstadoFinanciero.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00") || String.Equals(str_CodResultado, "True"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }

        public bool ModificarEtapaEstadoFinanciero(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, int int_IdEtapaEstadoFinanciero, string str_NotaRazon, string str_UsuarioEtapaEstado, DateTime dt_FechaDeEtapaEstado, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsModificarEtapaEstadoFinanciero cls_ModificarEstadoFinanciero = new clsModificarEtapaEstadoFinanciero(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, dt_FechaDeEtapaEstado, int_IdEtapaEstadoFinanciero, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ModificarEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_ModificarEstadoFinanciero.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }
        #endregion
    }
}