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
    public class clsNemotecnicos
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private string lstr_IdNemotecnico;
        public string Lstr_IdNemotecnico
        {
            get { return lstr_IdNemotecnico; }
            set { lstr_IdNemotecnico = value; }
        }


        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_NomNemotecnico;
        public string Lstr_NomNemotecnico
        {
            get { return lstr_NomNemotecnico; }
            set { lstr_NomNemotecnico = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_TipoNemotecnico;
        public string Lstr_TipoNemotecnico
        {
            get { return lstr_TipoNemotecnico; }
            set { lstr_TipoNemotecnico = value; }
        }

        private string lstr_IdTasa;
        public string Lstr_IdTasa
        {
            get { return lstr_IdTasa; }
            set { lstr_IdTasa = value; }
        }

        private string lstr_ModuloSINPE;
        public string Lstr_ModuloSINPE
        {
            get { return lstr_ModuloSINPE; }
            set { lstr_ModuloSINPE = value; }
        }

        private string lstr_IdCuentaContableCP;
        public string Lstr_IdCuentaContableCP
        {
            get { return lstr_IdCuentaContableCP; }
            set { lstr_IdCuentaContableCP = value; }
        }

        private string lstr_IdCuentaContableLP;
        public string Lstr_IdCuentaContableLP
        {
            get { return lstr_IdCuentaContableLP; }
            set { lstr_IdCuentaContableLP = value; }
        }

        public DataSet ConsultarNemotecnicos(string str_IdNemotecnico, string str_IdSociedadFi, string str_NomNemotecnico, string str_IdMoneda, string str_TipoNemotecnico)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarNemotecnicos cr_Procedimiento = new clsConsultarNemotecnicos(str_IdNemotecnico, str_IdSociedadFi, str_NomNemotecnico, str_IdMoneda, str_TipoNemotecnico);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearNemotecnico(string str_IdNemotecnico, string str_IdSociedadFi, string str_NomNemotecnico, string str_IdMoneda, string str_TipoNemotecnico, string str_IdTasa, string str_ModuloSINPE, string str_IdCuentaContableCP, string str_IdCuentaContableLP, string str_Estado, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearNemotecnico cls_ProcCrearNemotecnico = new clsCrearNemotecnico(str_IdNemotecnico, str_IdSociedadFi, str_NomNemotecnico, str_IdMoneda, str_TipoNemotecnico, str_IdTasa, str_ModuloSINPE, str_IdCuentaContableCP, str_IdCuentaContableLP, str_Estado, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearNemotecnico.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearNemotecnico.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool ModificarNemotecnico(string str_IdNemotecnico, string str_IdSociedadFi, string str_NomNemotecnico, string str_IdMoneda, string str_TipoNemotecnico, string str_IdTasa, string str_ModuloSINPE, string str_IdCuentaContableCP, string str_IdCuentaContableLP, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarNemotecnico cls_ProcModificarNemotecnico = new clsModificarNemotecnico(str_IdNemotecnico, str_IdSociedadFi, str_NomNemotecnico, str_IdMoneda, str_TipoNemotecnico, str_IdTasa, str_ModuloSINPE, str_IdCuentaContableCP, str_IdCuentaContableLP, str_Estado, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarNemotecnico.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarNemotecnico.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public clsNemotecnicos()
        { }
    }
}