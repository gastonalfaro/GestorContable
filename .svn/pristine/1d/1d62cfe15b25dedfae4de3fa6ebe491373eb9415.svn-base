using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Consolidacion
{
    public class clsEntidadesDeUnAmbito
    {
        #region variables
        private string lstr_IdAmbitoConsolidacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdAmbitoConsolidacion
        {
            get { return lstr_IdAmbitoConsolidacion; }
            set { lstr_IdAmbitoConsolidacion = value; }
        }
       
        #endregion

        #region procedimientos
        public DataSet BuscarEntidadesDeUnAmbito(string str_IdAmbitoConsolidacion, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsBuscarEntidadesDeUnAmbito cls_BuscarEntidadesDeUnAmbito = new clsBuscarEntidadesDeUnAmbito(str_IdAmbitoConsolidacion, str_Estado, str_UsrCreacion);
                lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarEntidadesDeUnAmbito.Lstr_RespuestaSchema)));
                lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_BuscarEntidadesDeUnAmbito.Lstr_RespuestaXML)));

                str_CodResultado = cls_BuscarEntidadesDeUnAmbito.Lstr_CodigoResultado;
                str_Mensaje = cls_BuscarEntidadesDeUnAmbito.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return lds_TablaConsulta;
        }
        #endregion

    }
}