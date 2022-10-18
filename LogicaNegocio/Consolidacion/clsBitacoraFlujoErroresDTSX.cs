using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Consolidacion
{
    public class clsBitacoraFlujoErroresDTSX
    {
        #region variables

        private string lstr_NombreProceso;
        private DateTime ldt_FechaDe;
        private DateTime ldt_FechaHasta;

        private string lstr_Estado; 
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_NombreProceso
        {
            get { return lstr_NombreProceso; }
            set { lstr_NombreProceso = value; }
        }

        public DateTime Lstr_FechaDe
        {
            get { return ldt_FechaDe; }
            set { ldt_FechaDe = value; }
        }

        public DateTime Lstr_FechaHasta
        {
            get { return ldt_FechaHasta; }
            set { ldt_FechaHasta = value; }
        }
       
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        #endregion

        #region procedimientos
        public DataSet BuscarBitacoraFlujoErroresDTSX(string str_NombreProceso, DateTime dt_FechaDe, DateTime dt_FechaHasta, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarBitacoraFlujoErroresDTSX cls_BuscarBitacoraFlujoErroresDTSX = new clsBuscarBitacoraFlujoErroresDTSX(str_NombreProceso, dt_FechaDe, dt_FechaHasta, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarBitacoraFlujoErroresDTSX.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarBitacoraFlujoErroresDTSX.Lstr_RespuestaXML)));

                str_CodResultado = cls_BuscarBitacoraFlujoErroresDTSX.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarBitacoraFlujoErroresDTSX.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public DataSet ConsultarBitacoraFlujoErroresDTSX( string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsConsultarBitacoraFlujoErroresDTSX cls_ConsultarBitacoraFlujoErroresDTSX = new clsConsultarBitacoraFlujoErroresDTSX(str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarBitacoraFlujoErroresDTSX.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarBitacoraFlujoErroresDTSX.Lstr_RespuestaXML)));

                str_CodResultado = cls_ConsultarBitacoraFlujoErroresDTSX.Lstr_CodigoResultado;
                str_Mensaje = cls_ConsultarBitacoraFlujoErroresDTSX.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }
        #endregion

    }
}