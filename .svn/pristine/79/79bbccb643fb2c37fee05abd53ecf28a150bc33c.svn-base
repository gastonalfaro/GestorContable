using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.Consolidacion;

namespace LogicaNegocio.Consolidacion
{
    public class clsUnidadesTiempoPeriodoEnCursoPorFecha
    {
        #region variables
        private DateTime ldt_Fecha;
        #endregion

        #region obtencion y asignacion
        
        public DateTime Ldt_Fecha
        {
            get { return ldt_Fecha; }
            set { ldt_Fecha = value; }
        }

        #endregion

        #region procedimientos

        public DataSet BuscarUnidadesTiempoPeriodoEnCursoPorFecha(DateTime dt_Fecha, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarUnidadesTiempoPeriodoEnCursoPorFecha cls_ValidaCargaUnidadTiempoPeriodoCorrectoCorreoAutorizacion = new clsBuscarUnidadesTiempoPeriodoEnCursoPorFecha(dt_Fecha, str_Estado, str_UsrCreacion);
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