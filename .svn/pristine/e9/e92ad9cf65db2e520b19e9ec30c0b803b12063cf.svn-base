using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsDTSSIGADE
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion
        #region Parámetros

        private string lstr_pack_name;
        private string lstr_proj_name;

        #endregion

        #region Obtención y asignación

        public string Lstr_pack_name
        {
            get { return lstr_pack_name; }
            set { lstr_pack_name = value; }
        }
        public string Lstr_proj_name
        {
            get { return lstr_proj_name; }
            set { lstr_proj_name = value; }
        }
       
        #endregion 

        #region Métodos

        public DataSet EjecutarDTSSIGADE(string lstr_pack_name, string lstr_proj_name, out string str_CodResultado, out string str_Mensaje)
        {

            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsEjecutarDTSSIGADE cr_Procedimiento = new clsEjecutarDTSSIGADE(lstr_pack_name, lstr_proj_name);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString(); 
            }
            return lds_TablasConsulta;
        }

        #endregion

        #region Constructor

        public clsDTSSIGADE()
        { }

        #endregion
    }
}