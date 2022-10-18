using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System.Data;

namespace LogicaNegocio.Consolidacion
{
    public class clsArchivoAnexoEstadoFinanciero
    {
        #region variables
        private string lstr_IdEstadoFinancieroArchivoAnexo;

        private string lstr_IdEntidad;
        private int lint_IdEstadoFinanciero;
        private int lint_Periodo;
        private string lstr_UnidadTiempoPeriodo;
        private string lstr_NombreArchivo;
        private string lstr_TipoArchivo;
        private int lint_TamanoByteArchivo;
        private DateTime ldt_FechaArchivo;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEstadoFinancieroArchivoAnexo
        {
            get { return lstr_IdEstadoFinancieroArchivoAnexo; }
            set { lstr_IdEstadoFinancieroArchivoAnexo = value; }
        }

        public string Lstr_IdEntidad
        {
            get { return lstr_IdEntidad; }
            set { lstr_IdEntidad = value; }
        }

        public int Lint_IdEstadoFinanciero
        {
            get { return lint_IdEstadoFinanciero; }
            set { lint_IdEstadoFinanciero = value; }
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

        public string Lstr_NombreArchivo
        {
            get { return lstr_NombreArchivo; }
            set { lstr_NombreArchivo = value; }
        }

        public string Lstr_TipoArchivo
        {
            get { return lstr_TipoArchivo; }
            set { lstr_TipoArchivo = value; }
        }

        public int Lint_TamanoByteArchivo
        {
            get { return lint_TamanoByteArchivo; }
            set { lint_TamanoByteArchivo = value; }
        }

        public DateTime Ldt_FechaArchivo
        {
            get { return ldt_FechaArchivo; }
            set { ldt_FechaArchivo = value; }
        }
       
        #endregion

        #region metodos
        public DataSet BuscarArchivoAnexoEstadoFinanciero(string str_IdEstadoFinancieroArchivoAnexo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;

            try
            {
                clsBuscarArchivoAnexoEstadoFinancieroFilestream cls_BuscarArchivoAnexoEstadoFinanciero = new clsBuscarArchivoAnexoEstadoFinancieroFilestream(str_IdEstadoFinancieroArchivoAnexo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_BuscarArchivoAnexoEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarArchivoAnexoEstadoFinanciero.Lstr_MensajeRespuesta;
                
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarArchivoAnexoEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarArchivoAnexoEstadoFinanciero.Lstr_RespuestaXML)));

                
            }
            catch (Exception ex)
            { }

            return lds_TablaConsulta;
        }

        public DataSet BuscarArchivoAnexoEstadoFinancieroFileStream(byte[] bt_Buffer, string str_IdEstadoFinancieroArchivoAnexo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;

            try
            {
                clsBuscarArchivoAnexoEstadoFinancieroFilestream cls_BuscarArchivoAnexoEstadoFinanciero = new clsBuscarArchivoAnexoEstadoFinancieroFilestream(str_IdEstadoFinancieroArchivoAnexo, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarArchivoAnexoEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarArchivoAnexoEstadoFinanciero.Lstr_RespuestaXML)));

                str_CodResultado = cls_BuscarArchivoAnexoEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarArchivoAnexoEstadoFinanciero.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }

            return lds_TablaConsulta;
        }

        public bool EliminarArchivoAnexoEstadoFinanciero(string str_IdEstadoFinancieroArchivoAnexo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEliminarArchivoAnexoEstadoFinanciero cls_EliminarArchivoAnexoEstadoFinanciero = new clsEliminarArchivoAnexoEstadoFinanciero(str_IdEstadoFinancieroArchivoAnexo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_EliminarArchivoAnexoEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_EliminarArchivoAnexoEstadoFinanciero.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }

        public DataSet InsertarArchivoAnexoEstadoFinanciero(string str_IdEntidad, int int_IdEstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo, 
            int int_TamanoByteArchivo, DateTime dt_FechaArchivo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsInsertarArchivoAnexoEstadoFinanciero cls_InsertarArchivoAnexoEstadoFinanciero = new clsInsertarArchivoAnexoEstadoFinanciero(str_IdEntidad, int_IdEstadoFinanciero, int_Periodo, str_UnidadTiempoPeriodo, str_NombreArchivo, str_TipoArchivo, int_TamanoByteArchivo, dt_FechaArchivo, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_RespuestaXML)));

                str_CodResultado = cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_MensajeRespuesta;
           
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public DataSet InsertarArchivoAnexoEstadoFinancieroFilestream(byte[] Buffer, string str_IdEntidad, int int_IdEstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo,
            int int_TamanoByteArchivo, DateTime dt_FechaArchivo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            bool bool_Resultado = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsInsertarArchivoAnexoEstadoFinancieroFileStream cls_InsertarArchivoAnexoEstadoFinanciero = new clsInsertarArchivoAnexoEstadoFinancieroFileStream(Buffer, str_IdEntidad, int_IdEstadoFinanciero, int_Periodo, str_UnidadTiempoPeriodo, str_NombreArchivo, str_TipoArchivo, int_TamanoByteArchivo, dt_FechaArchivo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_MensajeRespuesta;
                
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_RespuestaXML)));

                if (String.Equals(str_CodResultado, "00") || String.Equals(str_CodResultado, "01"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public DataSet ConsultarArchivosAnexosEstadosFinancierosCargados(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsConsultarArchivosAnexosEstadosFinancierosCargados cls_InsertarArchivoAnexoEstadoFinanciero = new clsConsultarArchivosAnexosEstadosFinancierosCargados(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_RespuestaXML)));

                str_CodResultado = cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_InsertarArchivoAnexoEstadoFinanciero.Lstr_MensajeRespuesta;
           
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        #endregion



    }
}