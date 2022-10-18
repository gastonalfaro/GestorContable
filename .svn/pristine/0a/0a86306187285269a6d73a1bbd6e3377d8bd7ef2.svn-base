using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.Seguridad;

namespace LogicaNegocio.Seguridad
{
    public class Tipoliticas
    {
        /// <summary>
        /// Tiempo, en minutos, que permanece en ocio la aplicacion antes de cerrar sesion
        /// </summary>
        private int lint_TiempoOcio;
        public int Lint_TiempoOcio
        { 
            get { return lint_TiempoOcio; }
            set { lint_TiempoOcio = value; }
        }

        /// <summary>
        /// Limite de sesiones de usuarios abiertas al mismo tiempo
        /// </summary>
        private int lint_MaxSesionesUsuario;
        public int Lint_MaxSesionesUsuario
        {
            get { return lint_MaxSesionesUsuario; }
            set { lint_MaxSesionesUsuario = value; }
        }

        /// <summary>
        /// Cantidad limite de intentos de iniciar sesion
        /// </summary>
        private int lint_MaxIntentosFallidos;
        public int Lint_MaxIntentosFallidos
        {
            get { return lint_MaxIntentosFallidos; }
            set { lint_MaxIntentosFallidos = value; }
        }

        /// <summary>
        /// Dias de vigencia de la clave
        /// </summary>
        private int lint_DiasVigenciaClave;
        public int Lint_DiasVigenciaClave
        {
            get { return lint_DiasVigenciaClave; }
            set { lint_DiasVigenciaClave = value; }
        }

        /// <summary>
        /// Minutos que permanece bloqueada la clave luego de fallar al autenticar
        /// </summary>
        private int lint_MinutosBloqueoClave;
        public int Lint_MinutosBloqueoClave
        {
            get { return lint_MinutosBloqueoClave; }
            set { lint_MinutosBloqueoClave = value; }
        }

        /// <summary>
        /// Minimo de largo de contrasena
        /// </summary>
        private int lint_MinTamanoClave;
        public int Lint_MinTamanoClave
        {
            get { return lint_MinTamanoClave; }
            set { lint_MinTamanoClave = value; }
        }

        /// <summary>
        /// Minimo de cantidad de letras de clave
        /// </summary>
        private int lint_MinCantidadLetrasClave;
        public int Lint_MinCantidadLetrasClave
        {
            get { return lint_MinCantidadLetrasClave; }
            set { lint_MinCantidadLetrasClave = value; }
        }

        /// <summary>
        /// Minima cantidad de numeros en la contrasena
        /// </summary>
        private int lint_MinCantidadNumerosClave;
        public int Lint_MinCantidadNumerosClave
        {
            get { return lint_MinCantidadNumerosClave; }
            set { lint_MinCantidadNumerosClave = value; }
        }

        /// <summary>
        /// Minima cantidad de simbolos en la contrasena
        /// </summary>
        private int lint_MinCantidadSimbolosClave;
        public int Lint_MinCantidadSimbolosClave
        {
            get { return lint_MinCantidadSimbolosClave; }
            set { lint_MinCantidadSimbolosClave = value; }
        }

        /// <summary>
        /// Cantidad minima de claves 
        /// </summary>
        private int lint_CantClavesParaReutilizacion;
        public int Lint_CantClavesParaReutilizacion
        {
            get { return lint_CantClavesParaReutilizacion; }
            set { lint_CantClavesParaReutilizacion = value; }
        }

        private int lint_AntiguedadRegistrosBitacora;
        public int Lint_AntiguedadRegistrosBitacora
        {
            get { return lint_AntiguedadRegistrosBitacora; }
            set { lint_AntiguedadRegistrosBitacora = value; }
        }

        public DataSet ConsultarPoliticas(string int_IdPolitica, string dat_FchVigencia)
        {
            DataSet lds_TablasPoliticas = new DataSet();
            clsConsultarPoliticas cr_ProcedimientoConsulta = new clsConsultarPoliticas(int_IdPolitica, dat_FchVigencia);
            lds_TablasPoliticas.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_ProcedimientoConsulta.Lstr_RespuestaSchema)));
            lds_TablasPoliticas.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_ProcedimientoConsulta.Lstr_RespuestaXML)));
            if (String.Equals(cr_ProcedimientoConsulta.Lstr_CodigoResultado, "00"))
            {
                //Verifica que los datos otorgados sean correctos
                if (lds_TablasPoliticas.Tables["Table"].Rows.Count > 0)
                {
                    lint_TiempoOcio = Convert.ToInt32(lds_TablasPoliticas.Tables["Table"].Rows[0]["TiempoOcio"].ToString());
                }
            }
            return lds_TablasPoliticas;

        }

        public void ActualizarPoliticas(string int_TiempoOcio, string int_MaxSesionesUsuario, string int_MaxNroIntentosFallidos,
            string int_MaxVigenciaClave, string int_TiempoBloqueoClave, string int_MinTamanoClave, string int_MinLetrasClave,
            string int_MinNumerosClave, string int_MinCaracteresClave, string int_NroReutilizacionUltimasClaves, string int_AntiguedadBitacora,
            string str_UsrModifica, string dat_FchModfica)
        {
            try
            {
                clsActualizarPoliticasSeguridad cap_ProcActualizarPoliticas = new clsActualizarPoliticasSeguridad(int_TiempoOcio,
                 int_MaxSesionesUsuario, int_MaxNroIntentosFallidos, int_MaxVigenciaClave,
                 int_TiempoBloqueoClave, int_MinTamanoClave, int_MinLetrasClave,
                 int_MinNumerosClave, int_MinCaracteresClave, int_NroReutilizacionUltimasClaves,
                 int_AntiguedadBitacora, str_UsrModifica, dat_FchModfica);
            }
            catch (Exception ex)
            { }
        }

        public Tipoliticas()
        { }
    }
}