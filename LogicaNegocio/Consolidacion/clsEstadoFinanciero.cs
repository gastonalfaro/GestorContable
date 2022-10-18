using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Consolidacion
{
    public class clsEstadoFinanciero
    {
        #region variables
        private byte lbt_IdEstadoFinanciero;

        private string lstr_NombreEstadoFinanciero;
        private string lstr_DescripcionEstadoFinanciero;
        private string lstr_Usuario;
        #endregion

        #region obtencion y asignacion
        public byte Lbt_IdEstadoFinanciero
        {
            get { return lbt_IdEstadoFinanciero; }
            set { lbt_IdEstadoFinanciero = value; }
        }


        public string Lstr_NombreEstadoFinanciero
        {
            get { return lstr_NombreEstadoFinanciero; }
            set { lstr_NombreEstadoFinanciero = value; }
        }

        public string Lstr_DescripcionEstadoFinanciero
        {
            get { return lstr_DescripcionEstadoFinanciero; }
            set { lstr_DescripcionEstadoFinanciero = value; }
        }

        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }
        #endregion

        #region procedimientos
        public DataSet BuscarEstadoFinanciero(byte bt_IdEstadoFinanciero, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarEstadoFinanciero cls_BuscarEstadoFinanciero = new clsBuscarEstadoFinanciero(bt_IdEstadoFinanciero, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarEstadoFinanciero.Lstr_RespuestaXML)));

                str_CodResultado = cls_BuscarEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarEstadoFinanciero.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public DataSet ConsultarEstadoFinanciero(string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsConsultarEstadoFinanciero cls_ConsultarEstadoFinanciero = new clsConsultarEstadoFinanciero(str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarEstadoFinanciero.Lstr_RespuestaXML)));

                str_CodResultado = cls_ConsultarEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_ConsultarEstadoFinanciero.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }

        public bool EliminarEstadoFinanciero(byte bt_IdEstadoFinanciero, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEliminarEstadoFinanciero cls_EliminarEstadoFinanciero = new clsEliminarEstadoFinanciero(bt_IdEstadoFinanciero, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_EliminarEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_EliminarEstadoFinanciero.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }

        public bool InsertarEstadoFinanciero(byte bt_EstadoFinanciero, string str_NombreEstadoFinanciero, string str_DescripcionEstadoFinanciero, string str_Usuario, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsInsertarEstadoFinanciero cls_InsertarEstadoFinanciero = new clsInsertarEstadoFinanciero(bt_EstadoFinanciero, str_NombreEstadoFinanciero, str_DescripcionEstadoFinanciero, str_Usuario, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_InsertarEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_InsertarEstadoFinanciero.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }

        public bool ModificarEstadoFinanciero(byte bt_EstadoFinanciero, string str_NombreEstadoFinanciero, string str_DescripcionEstadoFinanciero, string str_Usuario, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsModificarEstadoFinanciero cls_ModificarEstadoFinanciero = new clsModificarEstadoFinanciero(bt_EstadoFinanciero, str_NombreEstadoFinanciero, str_DescripcionEstadoFinanciero, str_Usuario, str_Estado, str_UsrCreacion);
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