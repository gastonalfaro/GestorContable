using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Consolidacion
{
    public class clsArchivoPlantillaEstadoFinanciero
    {
        #region variables

        private string lstr_IdEstadoFinancieroArchivoPlantilla;

        private int lint_IdEstadoFinanciero;
        private string lstr_NombreArchivo;
        private string lstr_TipoArchivo;
        private DateTime ldt_FechaArchivo;
        private string lstr_Usuario;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEstadoFinancieroArchivoPlantilla
        {
            get { return lstr_IdEstadoFinancieroArchivoPlantilla; }
            set { lstr_IdEstadoFinancieroArchivoPlantilla = value; }
        }

        public int Lint_IdEstadoFinanciero
        {
            get { return lint_IdEstadoFinanciero; }
            set { lint_IdEstadoFinanciero = value; }
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
        public DateTime Ldt_FechaArchivo
        {
            get { return ldt_FechaArchivo; }
            set { ldt_FechaArchivo = value; }
        }

        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }
        #endregion

        #region metodos
        public DataSet BuscarArchivoPlantillaEstadoFinanciero(string str_IdEstadoFinancieroArchivoPlantilla, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarArchivoPlantillaEstadoFinancieroFilestream cls_BuscarArchivoPlantillaEstadoFinanciero = new clsBuscarArchivoPlantillaEstadoFinancieroFilestream(str_IdEstadoFinancieroArchivoPlantilla, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_BuscarArchivoPlantillaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarArchivoPlantillaEstadoFinanciero.Lstr_MensajeRespuesta;

                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarArchivoPlantillaEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarArchivoPlantillaEstadoFinanciero.Lstr_RespuestaXML)));

            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public bool EliminarArchivoPlantillaEstadoFinanciero(string str_IdEstadoFinancieroArchivoPlantilla, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEliminarArchivoPlantillaEstadoFinanciero cls_EliminarArchivoPlantillaEstadoFinanciero = new clsEliminarArchivoPlantillaEstadoFinanciero(str_IdEstadoFinancieroArchivoPlantilla, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_EliminarArchivoPlantillaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_EliminarArchivoPlantillaEstadoFinanciero.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }

        public DataSet InsertarArchivoPlantillaEstadoFinanciero(int int_IdEstadoFinanciero, string str_NombreArchivo, string str_TipoArchivo, DateTime dt_FechaArchivo, 
            string str_Usuario, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsInsertarArchivoPlantillaEstadoFinanciero cls_InsertarArchivoPlantillaEstadoFinanciero = new clsInsertarArchivoPlantillaEstadoFinanciero(int_IdEstadoFinanciero,str_NombreArchivo,str_TipoArchivo,dt_FechaArchivo, str_Usuario, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_MensajeRespuesta;

                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_RespuestaXML)));


                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString(); 
            }
            return lds_TablaConsulta;
        }

        public DataSet InsertarArchivoPlantillaEstadoFinancieroFilestream(byte[] Buffer, int int_IdEstadoFinanciero, string str_NombreArchivo, string str_TipoArchivo, DateTime dt_FechaArchivo, 
            string str_Usuario, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsInsertarArchivoPlantillaEstadoFinancieroFileStream cls_InsertarArchivoPlantillaEstadoFinanciero = new clsInsertarArchivoPlantillaEstadoFinancieroFileStream(Buffer, int_IdEstadoFinanciero, str_NombreArchivo, str_TipoArchivo, dt_FechaArchivo, str_Usuario, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_RespuestaXML)));
                
                str_CodResultado = cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_MensajeRespuesta;

                if (String.Equals(str_CodResultado, "01"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return lds_TablaConsulta;
        }

        public DataSet ConsultarArchivosPlantillasEstadosFinancierosCargados(string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsConsultarArchivosPlantillasEstadosFinancierosCargados cls_InsertarArchivoPlantillaEstadoFinanciero = new clsConsultarArchivosPlantillasEstadosFinancierosCargados(str_Estado, str_UsrCreacion);
                str_CodResultado = cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_MensajeRespuesta;

                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_InsertarArchivoPlantillaEstadoFinanciero.Lstr_RespuestaXML)));


                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        #endregion

    }
}