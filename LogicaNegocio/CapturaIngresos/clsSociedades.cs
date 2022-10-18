using System;
using System.Collections.Generic;
using Datos.ConexionSQL.Procedimientos.CapturaIngresos;
using Datos.ConexionSQL;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;


namespace LogicaNegocio.CapturaIngresos
{
    public class clsSociedades
    {
        #region variables
        #endregion

        #region Métodos
        public DataSet ConsultaSociedad(string lstr_estado = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarSociedades cr_Procedimiento = new clsConsultarSociedades(lstr_estado);
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            { }
            return lds_TablasConsulta;
        }

        #endregion

        #region Constructor

        public clsSociedades()
        { }

        #endregion

    }
}