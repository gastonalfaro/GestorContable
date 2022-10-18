using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Compartidas
{
    public class clsSesion
    {
        // private constructor
        private clsSesion()
        {
            chr_MensajeError = '1';
            chr_MensajeExito = '0';
        }

        // Gets the current session.
        public static clsSesion Current
        {
            get
            {

                clsSesion sesion;
                try
                {
                    sesion =
                    (clsSesion)HttpContext.Current.Session["__clsSesion__"];
                }
                catch (Exception ex)
                {

                    sesion = null;

                }
                if (sesion == null)
                {
                    sesion = new clsSesion();
                    HttpContext.Current.Session["__clsSesion__"] = sesion;
                }
                return sesion;
            }
        }

        public void BorrarDatosSesion() 
        {
            LoginUsuario = String.Empty;
            TipoIdUsuario = String.Empty;
            NomUsuario = String.Empty;
            CorreoUsuario = String.Empty;
            IdSesion = String.Empty;
            IdSesion = String.Empty;
            SociedadUsr = String.Empty;
            NomSociedadGL = String.Empty;
            NombreSociedadGL = String.Empty;
            UsaFirmaDigital = false;
            String[] PermisosModulos = new String[0];
            RolesUsuario = new List<string>();

            //agregado por gasto cucurucho para solucionar lentitud
            MenuUsuario = String.Empty;
            TpoCambioCompra = String.Empty;
            TpoCambioEUR = String.Empty;
            TpoCambioVenta = String.Empty;
        }
        // Propiedades utilizadas en la sesion

        /// <summary>
        /// Identificador de usuario
        /// </summary>
        public string LoginUsuario { get; set; }

        /// <summary>
        /// Tipo de Identificador de usuario
        /// </summary>
        public string TipoIdUsuario { get; set; }

        private List<string> l_Roles = new List<string>();
        public List<string> RolesUsuario 
        { 
            get{return l_Roles;} 
            set{l_Roles = value;} 
        }


        //agregado por gaston cucurucho para solucionar lentitud ******************
        /// <summary>
        /// Menu de usuario
        /// </summary>   
        public string MenuUsuario { get; set; }
        /// <summary>
        /// Tipo de cambio del euro
        /// </summary>
        public string TpoCambioEUR { get; set; }
        /// <summary>
        /// tipo cambio de compra del dolar
        /// </summary>
        public string TpoCambioCompra { get; set; }
        /// <summary>
        /// tipo de cambio de venta del dolar
        /// </summary>
        public string TpoCambioVenta { get; set; }
        //agregado por gaston cucurucho para solucionar lentitud ******************


        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string NomUsuario { get; set; }

        /// <summary>
        /// Correo de usuario
        /// </summary>
        public string CorreoUsuario { get; set; }
        /// <summary>
        /// Identificador de sesion activa
        /// </summary>
        public string IdSesion { get; set; }

        /// <summary>
        /// Identificador de Direccion IP activa
        /// </summary>
        public string IPSesion { get; set; }
        /// <summary>
        /// Identificador de SociedadGL de usuario
        /// </summary>
        public string SociedadUsr { get; set; }

        /// <summary>
        /// Nombre de SociedadGL de usuario
        /// </summary>
        public string NombreSociedadGL { get; set; }

        public bool UsaFirmaDigital { get; set; }

        /// <summary>
        /// Fecha ultima consulta de datos
        /// </summary>
        public string FechaUltimaConsulta { get; set; }
        public char chr_MensajeError { get; set; }
        public char chr_MensajeExito { get; set; }
        public string DescripcionRol { get; set; }
        public string gstr_ModuloActual { get; set; }
        public bool gbol_FirmaDigital { get; set; }
        public int gint_CantNumContrasena { get; set; }
        public int gint_CantLetrasContrasena { get; set; }
        public int gint_CantSimbolosContrasena { get; set; }

        #region Variables Contingentes
        public String IdResolucion { get; set; }
        public String IdExpediente { get; set; }
        #endregion

        #region Variables Mantenimiento

        public string NomSociedadGL { get; set; }

        public string IdSociedadGL { get; set; }

        public String[] PermisosModulos { get; set; }

        public string IdCatalogo { get; set; }

        public string NomCatalogo { get; set; }

        public string IdReserva { get; set; }

        public string IdServicio { get; set; }

        public Boolean gbool_Permisos { get; set; }

        #region variales IndicadoresEconómicos
        public string IdIndicadorEco { get; set; } //creo manriq, para mantenimiento IndicadoresEconómicos
        public string Transaccion { get; set; } //creo manriq, para mantenimiento IndicadoresEconómicos
        public string NomIndicador { get; set; } //creo manriq, para mantenimiento IndicadoresEconómicos
        #endregion  

        #endregion

        #region VariablesCaptura
        public string IdFormularioCI { get; set; }
        public string AnnoFormularioCI { get; set; }
        public string Letras { get; set; }
        #endregion VariablesCaptura

        #region Variables de Politicas
        public int gint_MaxIntentosFallidos { get; set; }
        public int gint_TiempoBloqueoClave { get; set; }
        public int gint_CantLetrasClave { get; set; }
        public int gint_CantCaracteresClave { get; set; }
        public int gint_CantNumerosClave { get; set; }
        public int gint_TiempoOcio { get; set; }
        public int gint_VigenciaClave { get; set; }
        public int gint_ReutilizacionClave{ get; set; }

        #endregion

        public string gstr_NomParametro;

        public string NomParametro
        {
            get { return gstr_NomParametro; }
            set { gstr_NomParametro = value; }
        }
    }  
}