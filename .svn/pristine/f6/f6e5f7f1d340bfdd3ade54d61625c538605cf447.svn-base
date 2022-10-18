using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Consolidacion;

namespace LogicaNegocio.Consolidacion
{
    public class clsArchivoEstadoFinanciero
    {

        #region variables
        private string lstr_IdEstadoFinancieroArchivo;

        private string lstr_IdEntidad;
        private int lint_IdEstadoFinanciero;
        private int lint_Periodo;
        private string lstr_UnidadTiempoPeriodo;
        private string lstr_NombreArchivo;
        private string lstr_TipoArchivo;
        private int lint_TamanoByteArchivo;
        private DateTime ldt_FechaArchivo;
        private string lstr_Usuario;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEstadoFinancieroArchivoPlantilla
        {
            get { return lstr_IdEstadoFinancieroArchivo; }
            set { lstr_IdEstadoFinancieroArchivo = value; }
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
        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }
        public DateTime Ldt_FechaArchivo
        {
            get { return ldt_FechaArchivo; }
            set { ldt_FechaArchivo = value; }
        }
        #endregion

        #region metodos
        public DataSet BuscarArchivoEstadoFinanciero(string str_IdEstadoFinancieroArchivo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarArchivoEstadoFinancieroFilestream cls_BuscarArchivoEstadoFinanciero = new clsBuscarArchivoEstadoFinancieroFilestream(str_IdEstadoFinancieroArchivo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_BuscarArchivoEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarArchivoEstadoFinanciero.Lstr_MensajeRespuesta; 
                
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarArchivoEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarArchivoEstadoFinanciero.Lstr_RespuestaXML)));

                
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public bool EliminarArchivoEstadoFinanciero(string str_IdEstadoFinancieroArchivo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEliminarArchivoEstadoFinanciero cls_EliminarArchivoEstadoFinanciero = new clsEliminarArchivoEstadoFinanciero(str_IdEstadoFinancieroArchivo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_EliminarArchivoEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_EliminarArchivoEstadoFinanciero.Lstr_MensajeRespuesta;

                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }

        public DataSet InsertarArchivoEstadoFinanciero(string str_IdEntidad, int int_EstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo, 
            int int_TamanoByteArchivo, DateTime dt_FechaArchivo, string str_Usuario, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsInsertarArchivoEstadoFinanciero cls_InsertarArchivoEstadoFinanciero = new clsInsertarArchivoEstadoFinanciero(str_IdEntidad, int_EstadoFinanciero, int_Periodo, str_UnidadTiempoPeriodo, str_NombreArchivo, str_TipoArchivo, int_TamanoByteArchivo, dt_FechaArchivo, str_Usuario, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoEstadoFinanciero.Lstr_RespuestaXML)));
                
                str_CodResultado = cls_InsertarArchivoEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_InsertarArchivoEstadoFinanciero.Lstr_MensajeRespuesta;

                if ( String.Equals(str_CodResultado, "00") || String.Equals(str_CodResultado, "01"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public DataSet InsertarArchivoEstadoFinancieroFileStream(byte[] Buffer, string str_IdEntidad, int int_EstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo,
            int int_TamanoByteArchivo, DateTime dt_FechaArchivo, string str_Usuario, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            bool bool_Resultado = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsInsertarArchivoEstadoFinancieroFileStream cls_InsertarArchivoEstadoFinanciero = new clsInsertarArchivoEstadoFinancieroFileStream(Buffer, str_IdEntidad, int_EstadoFinanciero, int_Periodo, str_UnidadTiempoPeriodo, str_NombreArchivo, str_TipoArchivo, int_TamanoByteArchivo, dt_FechaArchivo, str_Usuario, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoEstadoFinanciero.Lstr_RespuestaXML)));

                str_Mensaje = cls_InsertarArchivoEstadoFinanciero.Lstr_MensajeRespuesta; 
                str_CodResultado = cls_InsertarArchivoEstadoFinanciero.Lstr_CodigoResultado;

                if ((String.Equals(str_CodResultado, "01"))||(String.Equals(str_CodResultado, "True")))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            {
            }
            return lds_TablaConsulta;
        }

        public DataSet BuscarArchivoEstadoFinancieroFilestream(string str_IdEstadoFinancieroArchivo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarArchivoEstadoFinancieroFilestream cls_BuscarArchivoEstadoFinanciero = new clsBuscarArchivoEstadoFinancieroFilestream(str_IdEstadoFinancieroArchivo, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_BuscarArchivoEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarArchivoEstadoFinanciero.Lstr_MensajeRespuesta;

                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarArchivoEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarArchivoEstadoFinanciero.Lstr_RespuestaXML)));


            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        #endregion

    }
}