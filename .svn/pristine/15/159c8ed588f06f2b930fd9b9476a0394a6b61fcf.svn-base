using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Seguridad;
using System.Data;

namespace LogicaNegocio.Seguridad
{
    public class tRol
    {
        private int lint_IdRol;
        public int Lint_IdRol
        {
            get { return lint_IdRol; }
            set { lint_IdRol = value; }
        }

        private string lstr_DescripcionRol;
        public string Lstr_DescripcionRol
        {
            get { return lstr_DescripcionRol; }
            set { lstr_DescripcionRol = value; }
        }

        /// <summary>
        /// Consultar roles que existen en el sistema
        /// </summary>
        /// <param name="int_IdRol">Identificador de Rol</param>
        /// <returns>Dataset con datos del rol</returns>
        public DataSet ConsultarRolSP(string int_IdRol, string str_DescRol, out string str_MensajeSalida)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarRol cr_Procedimiento = new clsConsultarRol(int_IdRol, str_DescRol);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                str_MensajeSalida = cr_Procedimiento.Lstr_MensajeRespuesta;
            }
            catch
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }

            return lds_TablasConsulta;
        }

        /// <summary>
        /// Creacion de un rol en el sistema
        /// </summary>
        /// <param name="str_DescRol">Descripcion del rol</param>
        /// <param name="str_UsrCreacion">Usuario que crea el rol</param>
        /// <returns>Indica si la creacion fue exitosa</returns>
        public bool CrearRol(string str_DescRol, string str_IdSesionUsuario, string str_UsrCreacion, out string str_MensajeSalida)
        {
            bool bool_ResCreacion = false;
            try
            {
                clsCrearRol cls_ProcCrearRol = new clsCrearRol(str_DescRol, str_IdSesionUsuario, str_UsrCreacion);
                if (String.Equals(cls_ProcCrearRol.Lstr_CodigoResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
                str_MensajeSalida = cls_ProcCrearRol.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }

            return bool_ResCreacion;
        }

        /// <summary>
        /// Actualiza el rol indicado
        /// </summary>
        /// <param name="int_IdRol">Identificador de rol a actualizar</param>
        /// <param name="str_DescRol">Descripcion de rol</param>
        /// <param name="str_Usuario">Usuario que actualiza el rol</param>
        /// <param name="dat_FchModifica">Fecha de modificacion</param>
        /// <returns>Resultado de actualizacion</returns>
        public bool ActualizarRol(string int_IdRol, string str_DescRol, string str_IdSesionUsuario, string str_Habilitado, string str_Usuario, string dat_FchModifica)
        {

            bool bool_ResultadoActualizacion = false;
            try
            {
                clsActualizarRol cls_ProcActualizarRol = new clsActualizarRol(int_IdRol, str_DescRol, str_IdSesionUsuario, str_Habilitado, str_Usuario, dat_FchModifica);
                if (String.Equals(cls_ProcActualizarRol.Lstr_CodigoResultado, "00"))
                {
                    bool_ResultadoActualizacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResultadoActualizacion;
        }

        /// <summary>
        /// Funcion para eliminacion de roles
        /// </summary>
        /// <param name="int_IdRol">Identificador de rol</param>
        /// <param name="dat_FchModifica">Fecha de consulta del rol</param>
        /// <param name="str_MensajeSalida">Mensaje de salida obtenido de la base de datos</param>
        /// <returns>True en cado de que la eliminacion sea exitosa</returns>
        public bool ufnEliminarRol(string int_IdRol, string dat_FchModifica, out string str_MensajeSalida)
        {
            bool lboo_ResultadoEliminacion = false;
            try
            {
                clsEliminarRol cla_EliminarRol = new clsEliminarRol(int_IdRol, dat_FchModifica);
                if (String.Equals(cla_EliminarRol.Lstr_CodigoResultado, "00"))
                {
                    lboo_ResultadoEliminacion = true;
                }
                str_MensajeSalida = cla_EliminarRol.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }
            return lboo_ResultadoEliminacion;
        }

        /// <summary>
        /// Asigna un rol a un usuario
        /// </summary>
        /// <param name="str_IdUsuario">Identificador de usuario</param>
        /// <param name="int_IdRol">Identificador de rol</param>
        /// <param name="str_UsuarioAdmin">Usuario que asigna el rol</param>
        /// <returns>Indica si se pudo</returns>
        public bool CrearRolUsuario(string str_IdSesionUsuario, string str_IdUsuario, string int_IdRol, string str_UsuarioAdmin, out string str_MensajeSalida)
        {
            bool lboo_CodResultado = false;
            try
            {
                clsCrearRolUsuario ccru_CrearRolUsuario = new clsCrearRolUsuario(str_IdSesionUsuario, str_IdUsuario, int_IdRol, str_UsuarioAdmin);
                if (String.Equals(ccru_CrearRolUsuario.Lstr_CodigoResultado, "00"))
                {
                    lboo_CodResultado = true;
                }
                str_MensajeSalida = ccru_CrearRolUsuario.Lstr_MensajeRespuesta;
            }
            catch
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }

            return lboo_CodResultado;
        }

        /// <summary>
        /// Consulta de roles de usuario
        /// </summary>
        /// <param name="str_IdRol">Identificador de rol</param>
        /// <param name="str_IdUsuario">Identificador de usuario</param>
        /// <returns>Dataset con roles del usuario</returns>
        public DataSet ConsultarRolesUsuario(string str_IdRol, string str_IdUsuario, out string str_MensajeSalida)
        {
            DataSet lds_RolesUsuario = new DataSet();
            try
            {
                clsConsultarRolesUsuarios lccru_ProcConsultarRolesUsr = new clsConsultarRolesUsuarios(str_IdRol, str_IdUsuario);
                lds_RolesUsuario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lccru_ProcConsultarRolesUsr.Lstr_RespuestaSchema)));
                lds_RolesUsuario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lccru_ProcConsultarRolesUsr.Lstr_RespuestaXML)));
                str_MensajeSalida = lccru_ProcConsultarRolesUsr.Lstr_MensajeRespuesta;
            }
            catch
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }

            return lds_RolesUsuario;
        }



        /// <summary>
        /// Elimina uno de los roles que posee un usuario
        /// </summary>
        /// <param name="str_IdUsuario">Identificador de usuario</param>
        /// <param name="int_IdRol">Identificador de rol</param>
        /// <param name="str_FchModifica">Fecha de consulta</param>
        /// <param name="str_MensajeSalida">Mensaje que se obtiene desde la base de datos</param>
        /// <returns>En caso de que la eliminacion sea exitosa se devuelve true</returns>
        public bool ufnEliminarRolUsuario(string str_IdUsuario, string int_IdRol, string str_FchModifica,
            out string str_MensajeSalida)
        {
            bool lboo_ResultadoEliminacion = false;
            try
            {
                clsEliminarRolUsuario cla_EliminarRolUsuario = new clsEliminarRolUsuario(str_IdUsuario, int_IdRol, str_FchModifica);
                if (String.Equals(cla_EliminarRolUsuario.Lstr_CodigoResultado, "00"))
                {
                    lboo_ResultadoEliminacion = true;
                }
                str_MensajeSalida = cla_EliminarRolUsuario.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }
            return lboo_ResultadoEliminacion;
        }

        public tRol()
        { }
    }
}