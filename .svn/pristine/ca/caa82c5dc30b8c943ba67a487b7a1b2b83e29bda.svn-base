using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Mantenimiento;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.Mantenimiento
{
    public class clsDinamico
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_SQL;
        public string Lstr_SQL
        {
            get { return lstr_SQL; }
            set { lstr_SQL = value; }
        }

      
        public DataSet ConsultarDinamico(string str_SQL)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarDinamico cr_Procedimiento = new clsConsultarDinamico(str_SQL);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch(Exception ex)
            { }
            return lds_TablasConsulta;
        }

    
        public clsDinamico()
        { }
    }
}