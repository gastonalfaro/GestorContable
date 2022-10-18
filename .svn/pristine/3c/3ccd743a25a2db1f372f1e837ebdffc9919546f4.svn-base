using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Consolidacion
{
    public class clsCatalogoEtapaEstadoFinanciero
    {
        #region variables
        private int lint_IdEtapaEstadoFinanciero;
        private string lstr_DescripEtapaEstadoFinanciero;
        private string lstr_Usuario;
        #endregion

        #region obtencion y asignacion
        public int Lint_IdEtapaEstadoFinanciero
        {
            get { return lint_IdEtapaEstadoFinanciero; }
            set { lint_IdEtapaEstadoFinanciero = value; }
        }

        public string Lstr_DescripEtapaEstadoFinanciero
        {
            get { return lstr_DescripEtapaEstadoFinanciero; }
            set { lstr_DescripEtapaEstadoFinanciero = value; }
        }

        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }
        #endregion

        #region procedimientos
        public DataSet BuscarCatalogoEtapaEstadoFinanciero(int int_IdEtapaEstadoFinanciero, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarCatalogoEtapaEstadoFinanciero cls_BuscarCatalogoEtapaEstadoFinanciero = new clsBuscarCatalogoEtapaEstadoFinanciero(int_IdEtapaEstadoFinanciero, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarCatalogoEtapaEstadoFinanciero.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarCatalogoEtapaEstadoFinanciero.Lstr_RespuestaXML)));

                str_CodResultado = cls_BuscarCatalogoEtapaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarCatalogoEtapaEstadoFinanciero.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }
        
        public DataSet ConsultarCatalogoEtapaEstadoFinanciero(string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
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

        public bool EliminarCatalogoEtapaEstadoFinanciero(int int_IdEtapaEstadoFinanciero, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEliminarCatalogoEtapaEstadoFinanciero cls_EliminarCatalogoEtapaEstadoFinanciero = new clsEliminarCatalogoEtapaEstadoFinanciero(int_IdEtapaEstadoFinanciero, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_EliminarCatalogoEtapaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_EliminarCatalogoEtapaEstadoFinanciero.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }

        public bool InsertarCatalogoEtapaEstadoFinanciero(int int_IdEtapaEstadoFinanciero, string str_DescripEtapaEstadoFinanciero, string str_Usuario, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsInsertarCatalogoEtapaEstadoFinanciero cls_InsertarCatalogoEtapaEstadoFinanciero = new clsInsertarCatalogoEtapaEstadoFinanciero(int_IdEtapaEstadoFinanciero,str_DescripEtapaEstadoFinanciero, str_Usuario, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_InsertarCatalogoEtapaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_InsertarCatalogoEtapaEstadoFinanciero.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_Resultado = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_Resultado;
        }

        public bool ModificarCatalogoEtapaEstadoFinanciero(int int_IdEtapaEstadoFinanciero, string str_DescripEtapaEstadoFinanciero, string str_Usuario, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_Resultado = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsModificarCatalogoEtapaEstadoFinanciero cls_ModificarCatalogoEtapaEstadoFinanciero = new clsModificarCatalogoEtapaEstadoFinanciero(int_IdEtapaEstadoFinanciero, str_DescripEtapaEstadoFinanciero, str_Usuario, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ModificarCatalogoEtapaEstadoFinanciero.Lstr_CodigoResultado;
                str_Mensaje = cls_ModificarCatalogoEtapaEstadoFinanciero.Lstr_MensajeRespuesta;
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