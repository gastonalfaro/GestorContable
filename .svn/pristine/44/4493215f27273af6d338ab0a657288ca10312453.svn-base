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
    public class clsAcreedor
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        #endregion

        #region Métodos

        public DataSet ConsultarAcreedor(string lstr_TipoAcreedor = null, string lstr_Pais = null, string lint_NroAcreedor = null, 
            string lstr_NomAcreedor = null, DateTime? ldt_FechaInicio = null, DateTime? ldt_FechaFin = null,string lstr_Estado = "ACT")
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaAcreedor cr_Procedimiento = new clsConsultaAcreedor(lstr_TipoAcreedor, lstr_Pais, lint_NroAcreedor, 
                    lstr_NomAcreedor, ldt_FechaInicio, ldt_FechaFin, lstr_Estado);
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

        public bool CrearAcreedor(int lint_NroAcreedor, string lstr_Cedula, string lstr_TipoIdAcreedor, string lstr_NomAcreedor, string lstr_Abreviatura,
            string lstr_Contacto, string lstr_Telefono, string lstr_Direccion, string lstr_Pais, string lstr_TipoAcreedor,
            string lstr_PaisInstitucion, string lstr_CatPersona, string lstr_TipoPersona, string lstr_IdCtaContable, string lstr_Estado,
            string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaAcreedor cr_Procedimiento = new clsCreaAcreedor(lint_NroAcreedor, lstr_Cedula, lstr_TipoIdAcreedor, lstr_NomAcreedor,
                    lstr_Abreviatura, lstr_Contacto, lstr_Telefono, lstr_Direccion, lstr_Pais, lstr_TipoAcreedor, lstr_PaisInstitucion,
                    lstr_CatPersona, lstr_TipoPersona, lstr_IdCtaContable, lstr_Estado, lstr_UsrCreacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }

        public bool ModificarAcreedor(int lint_NroAcreedor, string lstr_Cedula, string lstr_TipoIdAcreedor, string lstr_NomAcreedor, string lstr_Abreviatura, string lstr_Contacto, string lstr_Telefono,
            string lstr_Direccion, string lstr_Pais, string lstr_TipoAcreedor, string lstr_PaisInstitucion, string lstr_CatPersona, string lstr_TipoPersona, string lstr_UsrModifica,
            DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaAcreedor cr_Procedimiento = new clsModificaAcreedor(lint_NroAcreedor, lstr_Cedula, lstr_TipoIdAcreedor, lstr_NomAcreedor, lstr_Abreviatura, lstr_Contacto, lstr_Telefono,
                    lstr_Direccion, lstr_Pais, lstr_TipoAcreedor, lstr_PaisInstitucion, lstr_CatPersona, lstr_TipoPersona, lstr_UsrModifica,
                    ldt_FchModifica);
                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }

        public bool CambiarEstadoAcreedor(int lint_IdAcreedor, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCambiaEstadoAcreedor cr_Procedimiento = new clsCambiaEstadoAcreedor(lint_IdAcreedor, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }

        #endregion

        #region Constructor

        public clsAcreedor()
        { }

        #endregion
    }
}