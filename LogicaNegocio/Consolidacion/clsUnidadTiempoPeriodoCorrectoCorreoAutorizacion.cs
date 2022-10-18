using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.Consolidacion;

namespace LogicaNegocio.Consolidacion
{
    public class clsUnidadTiempoPeriodoCorrectoCorreoAutorizacion
    {
        #region variables
        private string lstr_IdEntidad;
        private int lint_Periodo;
        private string lstr_UnidadTiempoPeriodo;
        
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
       
        #endregion

        #region procedimientos

        public DataSet ValidaCargaUnidadTiempoPeriodoCorrectoCorreoAutorizacion(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsValidaCargaUnidadTiempoPeriodoCorrectoCorreoAutorizacion cls_ValidaCargaUnidadTiempoPeriodoCorrectoCorreoAutorizacion = new clsValidaCargaUnidadTiempoPeriodoCorrectoCorreoAutorizacion(str_IdEntidad, int_Periodo, str_UnidadTiempoPeriodo, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ValidaCargaUnidadTiempoPeriodoCorrectoCorreoAutorizacion.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ValidaCargaUnidadTiempoPeriodoCorrectoCorreoAutorizacion.Lstr_RespuestaXML)));

                str_CodResultado = cls_ValidaCargaUnidadTiempoPeriodoCorrectoCorreoAutorizacion.Lstr_CodigoResultado;
                str_Mensaje = cls_ValidaCargaUnidadTiempoPeriodoCorrectoCorreoAutorizacion.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }


        #endregion

    }
}