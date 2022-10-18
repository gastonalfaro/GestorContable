using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Consolidacion;
using System.Data;

namespace LogicaNegocio.Consolidacion
{
    public class clsEliminarArchivoExcel
    {

        #region variables
        private string lstr_DTSXPaqueteURL;
        private string lstr_DTSXPaqueteVariable;
        #endregion

        #region obtencion y asignacion
        public string Lstr_DTSXPaqueteURL
        {
            get { return lstr_DTSXPaqueteURL; }
            set { lstr_DTSXPaqueteURL = value; }
        }

        public string Lstr_DTSXPaqueteVariable
        {
            get { return lstr_DTSXPaqueteVariable; }
            set { lstr_DTSXPaqueteVariable = value; }
        }
        #endregion

        #region metodos
        public bool EliminarArchivoExcel(string str_DTSXPaqueteURL, string str_DTSXPaqueteVariable, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = null;
            str_Mensaje = null;
            try
            {
                clsEliminarArchivo cls_EliminarArchivo = new clsEliminarArchivo(str_DTSXPaqueteURL, str_DTSXPaqueteVariable);
                str_CodResultado = cls_EliminarArchivo.Lstr_CodigoResultado;
                str_Mensaje = cls_EliminarArchivo.Lstr_MensajeRespuesta;
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }


        #endregion
    }
}
