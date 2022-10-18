using Datos.ConexionSQL.Procedimientos.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Seguridad
{
    public class tBitacora
    {

        private DateTime? lstr_FechaInicio;
        public DateTime? Lstr_FechaInicio
        {
            get { return lstr_FechaInicio; }
            set { lstr_FechaInicio = value; }
        }

        private DateTime? lstr_FechaFinal;
        public DateTime? Lstr_FechaFinal
        {
            get { return lstr_FechaFinal; }
            set { lstr_FechaFinal = value; }
        }

        private string lstr_IdRegistro;
        public string Lstr_IdRegistro
        {
            get { return lstr_IdRegistro; }
            set { lstr_IdRegistro = value; }
        }

        private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        public DataSet ufnConsultarBitacoraErrores(DateTime? str_FechaInicio, DateTime? str_FechaFinal, string str_IdRegistro,
            string str_IdUsuario, string str_IdModulo, string str_Accion)
        {
            DataSet lds_TablasBitacora = new DataSet();
            try
            {
                clsConsultarBitacoraErrores lcbe_ConsultarBitacora = new clsConsultarBitacoraErrores(str_FechaInicio, str_FechaFinal,
                    str_IdRegistro, str_IdUsuario, str_IdModulo,str_Accion);
                lds_TablasBitacora.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcbe_ConsultarBitacora.Lstr_RespuestaSchema)));
                lds_TablasBitacora.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcbe_ConsultarBitacora.Lstr_RespuestaXML)));
            }
            catch (Exception ex)
            { }
            return lds_TablasBitacora;
        }

        public DataSet ufnConsultarBitacoraAsientos(string str_FechaInicio, string str_FechaFinal,
            string str_IdOperacion, string str_IdSociedadGL, string str_IdTransaccion, string str_IdModulo)
        {
            DataSet lds_TablasBitacora = new DataSet();
            try
            {
                clsConsultarBitacoraAsientos lcbe_ConsultarBitacora = new clsConsultarBitacoraAsientos(str_FechaInicio, str_FechaFinal,
                    str_IdOperacion, str_IdSociedadGL, str_IdTransaccion, str_IdModulo);
                lds_TablasBitacora.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lcbe_ConsultarBitacora.Lstr_RespuestaSchema)));
                lds_TablasBitacora.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lcbe_ConsultarBitacora.Lstr_RespuestaXML)));
            }
            catch (Exception ex)
            { }
            return lds_TablasBitacora;
        }

        public string ufnRegistrarAccionBitacora(string str_IdModulo, string str_IdSesionUsuario, string str_Accion, string str_Detalle, string str_IdOperacion = null, string str_IdTransaccion = null, string str_IdSociedadGL = null)
        {
            string lstr_Respuesta = String.Empty;
            string lstr_PlantillaCorreo = String.Empty;
            try
            {
                clsRegistrarAccionBitacora cru_RegistrarAccionBitacora = new clsRegistrarAccionBitacora(str_IdModulo, str_IdSesionUsuario, str_Accion, str_Detalle, str_IdOperacion , str_IdTransaccion , str_IdSociedadGL );
                if (cru_RegistrarAccionBitacora.Lstr_CodigoResultado != "00")
                {
                    lstr_Respuesta = cru_RegistrarAccionBitacora.Lstr_MensajeRespuesta;
                }
                else
                {
                    DataSet lds_RegistrarAccionBitacora = new DataSet();
                    lds_RegistrarAccionBitacora.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cru_RegistrarAccionBitacora.Lstr_RespuestaSchema)));
                    lds_RegistrarAccionBitacora.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cru_RegistrarAccionBitacora.Lstr_RespuestaXML)));
                    lstr_Respuesta = cru_RegistrarAccionBitacora.Lstr_MensajeRespuesta;
                }
            }
            catch (Exception ex)
            { }
            return lstr_Respuesta;
        }


    }
}