using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System.Data;

namespace LogicaNegocio.Consolidacion
{
    public class clsDTSX
    {

        #region variables
        private string lstr_DTSXPaqueteURL;
        private string lstr_DTSXPaqueteNombre;
        private string lstr_DTSXPaqueteVariable;
        private bool lstr_bEjecutar64Bit;
        #endregion

        #region obtencion y asignacion
        public string Lstr_DTSXPaqueteURL
        {
            get { return lstr_DTSXPaqueteURL; }
            set { lstr_DTSXPaqueteURL = value; }
        }

        public string Lstr_DTSXPaqueteNombre
        {
            get { return lstr_DTSXPaqueteNombre; }
            set { lstr_DTSXPaqueteNombre = value; }
        }

        public string Lstr_DTSXPaqueteVariable
        {
            get { return lstr_DTSXPaqueteVariable; }
            set { lstr_DTSXPaqueteVariable = value; }
        }

        public bool Lstr_bEjecutar64Bit
        {
            get { return lstr_bEjecutar64Bit; }
            set { lstr_bEjecutar64Bit = value; }
        }

        #endregion

        #region metodos
        public bool EjecutarDTSX(string str_DTSXPaqueteURL, string str_DTSXPaqueteNombre, string str_DTSXPaqueteVariable, bool str_bEjecutar64Bit, string str_Ruta32Bit, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEjecutarDTSX cls_ProcEjecutarDTSX = new clsEjecutarDTSX(str_DTSXPaqueteURL, str_DTSXPaqueteNombre, str_DTSXPaqueteVariable, str_bEjecutar64Bit, str_Ruta32Bit, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcEjecutarDTSX.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcEjecutarDTSX.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool EjecutarDTSXBD(string str_DTSXPaqueteURL, string str_DTSXPaqueteNombre, string str_DTSXPaqueteVariable, bool str_bEjecutar64Bit, string str_Ruta32Bit, string str_Estado, string str_UsrCreacion, string str_DTSXFolderName, string str_DTSXProyecto, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEjecutarDTSXBD cls_ProcEjecutarDTSXBD = new clsEjecutarDTSXBD(str_DTSXPaqueteURL, str_DTSXPaqueteNombre, str_DTSXPaqueteVariable, str_bEjecutar64Bit, str_Ruta32Bit, str_Estado, str_UsrCreacion, str_DTSXFolderName, str_DTSXProyecto);
                str_CodResultado = cls_ProcEjecutarDTSXBD.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcEjecutarDTSXBD.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public DataSet EjecutarDTSXPrueba( string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            DataSet lds_TablaConsulta = new DataSet();
            bool bool_ResCreacion = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEjecutarDTSXPrueba cls_ProcEjecutarDTSX = new clsEjecutarDTSXPrueba(str_Estado, str_UsrCreacion);
                //lds_TablaConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcEjecutarDTSX.Lstr_RespuestaSchema)));
                //lds_TablaConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcEjecutarDTSX.Lstr_RespuestaXML)));


                str_CodResultado = cls_ProcEjecutarDTSX.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcEjecutarDTSX.Lstr_MensajeRespuesta;
              
            }
            catch (Exception ex)
            {
                str_Mensaje = ex.ToString();
            }
            return lds_TablaConsulta;
        }

        #endregion

    }
}