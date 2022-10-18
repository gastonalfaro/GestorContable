using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;
using Datos.ConexionSQL.Procedimientos.Seguridad;
using System.Configuration;
using LogicaNegocio.Seguridad;
using Logica.SubirArchivo;

namespace LogicaNegocio.Seguridad
{

    public class tUsuario
    {
        private tSeguridad gcls_Seguridad = new tSeguridad();

        //Identificador del usuario
        private string lstr_Cedula;
        public string LStr_Cedula
        {
            get { return lstr_Cedula; }
            set { lstr_Cedula = value; }
        }

        //Nombre del usuario
        private string lstr_Nombre;
        public string LStr_Nombre
        {
            get { return lstr_Nombre; }
            set { lstr_Nombre = value; }
        }

        //Correo electronico del usuario
        private string lstr_CorreoUsuario;
        public string Lstr_CorreoUsuario
        {
            get { return lstr_CorreoUsuario; }
            set { lstr_CorreoUsuario = value; }
        }

        //Tipo de Identificacion del usuario
        private string lstr_TipoIdUsuario;
        public string Lstr_TipoIdUsuario
        {
            get { return lstr_TipoIdUsuario; }
            set { lstr_TipoIdUsuario = value; }
        }

        //Indica si la cuenta se encuentra habilitada
        private bool lboo_Activa = false;
        private bool Lboo_Activa
        {
            get { return lboo_Activa; }
            set { lboo_Activa = value; }
        }

        //Indica si la cuenta se encuentra habilitada
        private bool lboo_CtaHabilitada = false;
        private bool Lboo_CtaHabilitada
        {
            get { return lboo_CtaHabilitada; }
            set { lboo_CtaHabilitada = value; }
        }

        //Identificador de sesion de usuario
        private string lstr_IdSesionUsr;
        public string Lstr_IdSesionUsr
        {
            get { return lstr_IdSesionUsr; }
            set { lstr_IdSesionUsr = value; }
        }

        /// <summary>
        /// Lista de roles del usuario
        /// </summary>
        private List<tRol> ltrol_Roles = new List<tRol>();
        public List<tRol> Ltrol_Roles
        {
            get { return ltrol_Roles; }
            set { ltrol_Roles = value; }
        }

        /// <summary>
        /// Resgistra un usuario nuevo en el sistema
        /// </summary>
        /// <param name="str_Cedula">Identificador de usuario</param>
        /// <param name="str_Contrasena">Contrasena de usuario</param>
        /// <param name="str_Correo">Correo electronico de usuario</param>
        /// <returns>Codigo de respuesta obtenido desde la BD</returns>
        public string ufnRegistrarUsuario(string[] str_DatosConn, string str_Cedula, string str_TipoID, string str_Nombre, string str_Contrasena, string str_Correo)
        {
            string lstr_Respuesta = String.Empty;
           //string lstr_PlantillaCorreo = String.Empty;
            try
            {
                clsRegistrarUsuario cru_RegistroUsuario = new clsRegistrarUsuario(str_Cedula, str_TipoID, str_Nombre, str_Contrasena, str_Correo);
                if (cru_RegistroUsuario.Lstr_CodigoResultado != "00")
                {
                    lstr_Respuesta = cru_RegistroUsuario.Lstr_CodigoResultado;
                }
                else
                {
                    DataSet lds_Usuario = new DataSet();
                    lds_Usuario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cru_RegistroUsuario.Lstr_RespuestaSchema)));
                    lds_Usuario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cru_RegistroUsuario.Lstr_RespuestaXML)));
                    lstr_Respuesta = lds_Usuario.Tables["Table"].Rows[0]["CodActivacion"].ToString();
                    CorreoActivacion(str_DatosConn, lstr_Respuesta, str_Correo);
                    //lstr_PlantillaCorreo = CargarPlantillaCorreo(lstr_Respuesta, "Registro");
                    //EnviarCorreo(str_DatosConn, str_Correo, lstr_PlantillaCorreo, "Codigo de activación");
                }
            }
            catch (Exception ex)
            { }
            return lstr_Respuesta;
        }//fin

        /// <summary>
        /// Envía en un correo electrónico el código de activación de la cuenta al usuario
        /// Recibe los parametros de conexion, el correo, la clave de activacion
        /// </summary>
        public void CorreoActivacion(string[] str_DatosConn, string CodActivacion,string str_Correo) 
        {
            string lstr_PlantillaCorreo = String.Empty;
            lstr_PlantillaCorreo = CargarPlantillaCorreo(CodActivacion, "Registro");
            EnviarCorreo(str_DatosConn, str_Correo, lstr_PlantillaCorreo, "Codigo de activación");
        }//fin 

        /// <summary>
        /// Consulta el código de activacion del usuario
        /// </summary>
        /// <param name="str_Cedula">Identificador de usuario</param>
        /// <returns>String del codigo de usuario </returns>
        public string ufnConsultarCodigoActivacion(string[] str_DatosConn, string str_Cedula)
        {
            string lstr_Respuesta = String.Empty;
            string str_Correo = String.Empty;
            try
            {
                clsConsultarCodigo cls_Codigo = new clsConsultarCodigo(str_Cedula);
                if (String.Equals(cls_Codigo.Lstr_CodigoResultado, "00"))
                {
                    DataSet lds_Usuario = new DataSet();
                    lds_Usuario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_Codigo.Lstr_RespuestaSchema)));
                    lds_Usuario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_Codigo.Lstr_RespuestaXML)));
                    lstr_Respuesta = lds_Usuario.Tables["Table"].Rows[0]["CodActivacion"].ToString();
                    str_Correo = lds_Usuario.Tables["Table"].Rows[0]["CorreoUsuario"].ToString();
                    CorreoActivacion(str_DatosConn, lstr_Respuesta, str_Correo);
                }//if
            }//try
            catch (Exception ex)
            {
                lstr_Respuesta = ex.ToString();
            }//catch
            return lstr_Respuesta;
        }//fin


        public string ufnRegistrarUsuarioFirma(string[] str_DatosConn, string str_Cedula, string str_TipoID, string str_Nombre, string str_Contrasena,
            string str_Correo, out string str_MensajeRespuesta)
        {
            string pass = gcls_Seguridad.DescifrarTextoAES(str_Contrasena).Replace(str_Cedula, "");
            string lstr_Respuesta = "99";
            string lstr_PlantillaCorreo = String.Empty;
            try
            {
                clsRegistrarUsuarioFirma cru_RegistroUsuario = new clsRegistrarUsuarioFirma(str_Cedula, str_TipoID, str_Nombre, str_Contrasena, str_Correo);
                lstr_Respuesta = cru_RegistroUsuario.Lstr_CodigoResultado;
                str_MensajeRespuesta = cru_RegistroUsuario.Lstr_MensajeRespuesta;
                if (lstr_Respuesta == "00")
                {
                    DataSet lds_Usuario = new DataSet();
                    lds_Usuario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cru_RegistroUsuario.Lstr_RespuestaSchema)));
                    lds_Usuario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cru_RegistroUsuario.Lstr_RespuestaXML)));
                    string lstr_CodResultado = pass;// gcls_Seguridad.DescifrarTextoAES(lds_Usuario.Tables["Table"].Rows[0]["Clave"].ToString()).Replace(str_Cedula, "");//lds_Usuario.Tables["Table"].Rows[0]["Clave"].ToString();
                    lstr_PlantillaCorreo = CargarPlantillaCorreo(lstr_CodResultado, "Firma");
                    EnviarCorreo(str_DatosConn, str_Correo, lstr_PlantillaCorreo, "Recuperación de contraseña");
                }
            }
            catch (Exception ex)
            {
                str_MensajeRespuesta = "Error en logica de negocios";
            }
            return lstr_Respuesta;
        }

        /// <summary>
        /// Consulta los permisos de los usuarios
        /// </summary>
        /// <param name="str_IdUsuario">Identificador de usuario</param>
        /// <param name="str_IdObjeto">Identificador de objeto</param>
        /// <returns>Dataset con permisos de usuario</returns>
        public DataSet ufnConsultarPermisosUsuarios(string str_IdUsuario, string str_IdObjeto)
        {
            DataSet lds_PermisosUsuario = new DataSet();
            try
            {
                clsConsultarPermisosUsuarios cru_PermisosUsuario = new clsConsultarPermisosUsuarios(str_IdObjeto, str_IdUsuario);
                if (String.Equals(cru_PermisosUsuario.Lstr_CodigoResultado, "00"))
                {
                    lds_PermisosUsuario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cru_PermisosUsuario.Lstr_RespuestaSchema)));
                    lds_PermisosUsuario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cru_PermisosUsuario.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            {

            }
            return lds_PermisosUsuario;
        }

        /// <summary>
        /// Inicia la sesion del usuario en el el portal
        /// </summary>
        /// <param name="str_IdUsuario">Identificador de usuario</param>
        /// <param name="str_Clave">Contrasena de usuario</param>
        /// <returns>Codigo con el resultado de la ejecucion</returns>
        public string[] ufnLoguearUsuario(string str_IdUsuario, string str_Clave, string str_IpUsuario, out string str_MensajeSalida)
        {
            string[] lastr_Resultado = new string[8];
            DataSet lds_Usuario = new DataSet();
            string lstr_CodigoMensaje = "99";
            string lstr_SociedadUsr = String.Empty;
            string lstr_NomSociedad = String.Empty;
            string lstr_TipoIdUsuario = String.Empty;
            try
            {
                clsLoguearUsuario cls_ProcLoguearUsuario = new clsLoguearUsuario(str_IdUsuario, str_Clave, str_IpUsuario);
                lds_Usuario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcLoguearUsuario.Lstr_RespuestaSchema)));
                lds_Usuario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcLoguearUsuario.Lstr_RespuestaXML)));

                lstr_CodigoMensaje = cls_ProcLoguearUsuario.Lstr_CodigoResultado;

                str_MensajeSalida = cls_ProcLoguearUsuario.Lstr_MensajeRespuesta;
                //Verifica que no hubo problema al ejecutar el procedimiento
                if (!String.Equals(lstr_CodigoMensaje, "99"))
                {
                    //Verifica que los datos otorgados sean correctos
                    if (lstr_CodigoMensaje == "00")
                    {
                        //Establece los valores de las propiedades del usuario
                        lstr_Nombre = lds_Usuario.Tables["Table"].Rows[0]["Nombre"].ToString();
                        lstr_CorreoUsuario = lds_Usuario.Tables["Table"].Rows[0]["Correo"].ToString();
                        lstr_IdSesionUsr = lds_Usuario.Tables["Table"].Rows[0]["IdSesionUsuario"].ToString();
                        lstr_Cedula = str_IdUsuario;
                        lstr_SociedadUsr = lds_Usuario.Tables["Table"].Rows[0]["IdSociedadGL"].ToString();
                        lstr_NomSociedad = lds_Usuario.Tables["Table"].Rows[0]["NomSociedad"].ToString();
                        lstr_TipoIdUsuario = lds_Usuario.Tables["Table"].Rows[0]["TipoIdUsuario"].ToString();
                        lastr_Resultado[1] = lstr_Nombre;
                        lastr_Resultado[2] = lstr_CorreoUsuario;
                        lastr_Resultado[3] = lstr_IdSesionUsr;
                        lastr_Resultado[4] = lstr_Cedula;
                        lastr_Resultado[5] = lstr_SociedadUsr.Trim();
                        lastr_Resultado[6] = lstr_NomSociedad.Trim();
                        lastr_Resultado[7] = lstr_TipoIdUsuario.Trim();
                        //Anade los roles del usuario a la lista
                        DataTable ldt_TablaRoles = lds_Usuario.Tables["Table1"];
                        int lint_TamanoTabla = ldt_TablaRoles.Rows.Count;
                        for (int lint_CantFilas = 0; lint_CantFilas < lint_TamanoTabla; lint_CantFilas++)
                        {
                            tRol trol_RolUsuario = new tRol();
                            string lstr_IdRol = lds_Usuario.Tables["Table1"].Rows[lint_CantFilas]["IdRol"].ToString();
                            trol_RolUsuario.Lint_IdRol = Convert.ToInt32(lstr_IdRol);
                            ltrol_Roles.Add(trol_RolUsuario);
                        }
                    }
                }
                else
                {
                    lastr_Resultado[1] = str_MensajeSalida;
                }
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "Error al comunicar con base de datos";

                lastr_Resultado[1] = str_MensajeSalida;
            }
            lastr_Resultado[0] = lstr_CodigoMensaje;
            return lastr_Resultado;
        }

        /// <summary>
        /// Inicia la sesion del usuario en el el portal
        /// </summary>
        /// <param name="str_IdUsuario">Identificador de usuario</param>
        /// <returns>Codigo con el resultado de la ejecucion</returns>
        public string[] ufnLoguearUsuarioFirma(string str_IdUsuario, string str_NomUsuario, string str_IpUsuario, out string str_MensajeSalida)
        {
            string[] lastr_Resultado = new string[8];
            DataSet lds_Usuario = new DataSet();
            string lstr_CodigoMensaje = "99";
            string lstr_SociedadUsr = String.Empty;
            string lstr_NomSociedad = String.Empty;
            string lstr_TipoIdUsuario = String.Empty;
            try
            {
                clsLoguearUsuarioFirma cls_ProcLoguearUsuario = new clsLoguearUsuarioFirma(str_IdUsuario, str_NomUsuario, str_IpUsuario);
                lds_Usuario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcLoguearUsuario.Lstr_RespuestaSchema)));
                lds_Usuario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcLoguearUsuario.Lstr_RespuestaXML)));

                lstr_CodigoMensaje = cls_ProcLoguearUsuario.Lstr_CodigoResultado;
                str_MensajeSalida = cls_ProcLoguearUsuario.Lstr_MensajeRespuesta;
                //Verifica que no hubo problema al ejecutar el procedimiento
                if (!String.Equals(lstr_CodigoMensaje, "99"))
                {
                    //Verifica que los datos otorgados sean correctos
                    if (lstr_CodigoMensaje == "00")
                    {
                        //Establece los valores de las propiedades del usuario
                        lstr_Nombre = lds_Usuario.Tables["Table"].Rows[0]["Nombre"].ToString();
                        lstr_CorreoUsuario = lds_Usuario.Tables["Table"].Rows[0]["Correo"].ToString();
                        lstr_IdSesionUsr = lds_Usuario.Tables["Table"].Rows[0]["IdSesionUsuario"].ToString();
                        lstr_Cedula = str_IdUsuario;
                        lstr_SociedadUsr = lds_Usuario.Tables["Table"].Rows[0]["IdSociedadGL"].ToString();
                        lstr_NomSociedad = lds_Usuario.Tables["Table"].Rows[0]["NomSociedad"].ToString();
                        lstr_TipoIdUsuario = lds_Usuario.Tables["Table"].Rows[0]["TipoIdUsuario"].ToString();
                        lastr_Resultado[1] = lstr_Nombre;
                        lastr_Resultado[2] = lstr_CorreoUsuario;
                        lastr_Resultado[3] = lstr_IdSesionUsr;
                        lastr_Resultado[4] = lstr_Cedula;
                        lastr_Resultado[5] = lstr_SociedadUsr.Trim();
                        lastr_Resultado[6] = lstr_NomSociedad.Trim();
                        lastr_Resultado[7] = lstr_TipoIdUsuario.Trim();
                        //Anade los roles del usuario a la lista
                        DataTable ldt_TablaRoles = lds_Usuario.Tables["Table1"];
                        int lint_TamanoTabla = ldt_TablaRoles.Rows.Count;
                        for (int lint_CantFilas = 0; lint_CantFilas < lint_TamanoTabla; lint_CantFilas++)
                        {
                            tRol trol_RolUsuario = new tRol();
                            string lstr_IdRol = lds_Usuario.Tables["Table1"].Rows[lint_CantFilas]["IdRol"].ToString();
                            trol_RolUsuario.Lint_IdRol = Convert.ToInt32(lstr_IdRol);
                            ltrol_Roles.Add(trol_RolUsuario);
                        }
                    }
                }
                else
                {
                    lastr_Resultado[1] = str_MensajeSalida;
                }
            }
            catch (Exception ex)
            {
                str_MensajeSalida = ex.ToString();
                lastr_Resultado[1] = str_MensajeSalida;
            }
            lastr_Resultado[0] = lstr_CodigoMensaje;
            return lastr_Resultado;
        }


        /// <summary>
        /// Confirma el registro de un usuario nuevo en el sistema
        /// </summary>
        /// <param name="str_IdUsuario">Identificador usuario</param>
        /// <param name="str_Clave">Contrasena de usuario</param>
        /// <param name="str_CodActivacion">Codigo de activacion</param>
        /// <param name="dat_FchModifica">Fecha de modificacion</param>
        /// <returns></returns>
        public string[] ufnConfirmarUsuario(string str_IdUsuario, string str_Clave, string str_CodActivacion, string dat_FchModifica, string str_IPMaquina)
        {
            string[] lastr_Resultado = new string[9];
            string lstr_CodResultado = "99";
            DataSet lds_Usuario = new DataSet();
            string lstr_SociedadUsr = String.Empty;
            string lstr_NomSociedad = String.Empty;
            string lstr_TipoIdUsuario = String.Empty;
            try
            {
                clsConfirmarUsuario ltc_ConfirmarUsuario = new clsConfirmarUsuario(str_IdUsuario, str_Clave, str_CodActivacion, dat_FchModifica, str_IPMaquina);
                lds_Usuario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(ltc_ConfirmarUsuario.Lstr_RespuestaSchema)));
                lds_Usuario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(ltc_ConfirmarUsuario.Lstr_RespuestaXML)));
                lstr_CodResultado = ltc_ConfirmarUsuario.Lstr_CodigoResultado;
                if (String.Equals(lstr_CodResultado, "00"))
                {
                    lstr_Nombre = lds_Usuario.Tables["Table"].Rows[0]["Nombre"].ToString();
                    lstr_CorreoUsuario = lds_Usuario.Tables["Table"].Rows[0]["Correo"].ToString();
                    lstr_IdSesionUsr = lds_Usuario.Tables["Table"].Rows[0]["IdSesionUsuario"].ToString();
                    lstr_Cedula = str_IdUsuario;
                    lstr_SociedadUsr = lds_Usuario.Tables["Table"].Rows[0]["IdSociedadGL"].ToString();
                    lstr_NomSociedad = lds_Usuario.Tables["Table"].Rows[0]["NomSociedad"].ToString();
                    lstr_TipoIdUsuario = lds_Usuario.Tables["Table"].Rows[0]["TipoIdUsuario"].ToString();
                    lastr_Resultado[1] = lstr_Nombre;
                    lastr_Resultado[2] = lstr_CorreoUsuario;
                    lastr_Resultado[3] = lstr_IdSesionUsr;
                    lastr_Resultado[4] = lstr_Cedula;
                    lastr_Resultado[5] = lstr_SociedadUsr.Trim();
                    lastr_Resultado[6] = lstr_NomSociedad.Trim();
                    lastr_Resultado[7] = lstr_TipoIdUsuario.Trim();
                    lboo_Activa = true;
                }
                lastr_Resultado[8] = ltc_ConfirmarUsuario.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                lastr_Resultado[0] = "99";
                lastr_Resultado[8] = "Error en logica "+ex.ToString();
            }
            lastr_Resultado[0] = lstr_CodResultado;
            lastr_Resultado[8] = "Error en logica ";
            return lastr_Resultado;
        }

        public string ufnCerrarSesionUsuario(string str_IdUsuario, string str_IdSesionUsuario)
        {
            string lstr_CodResultado = "99";
            try
            {
                clsCerrarSesionUsuario ltc_CerrarSesionUsuario = new clsCerrarSesionUsuario(str_IdUsuario, str_IdSesionUsuario);
                lstr_CodResultado = ltc_CerrarSesionUsuario.Lstr_CodigoResultado;
                if (String.Equals(lstr_CodResultado, "00"))
                {
                    lboo_Activa = true;
                }
            }
            catch (Exception ex)
            { }
            return lstr_CodResultado;
        }

        public string ufnCerrarSesionesActivas(string str_IdUsuario)
        {
            string lstr_CodResultado = "99";
            try
            {
                clsCerrarSesionesActivas ltc_CerrarSesionesActivas = new clsCerrarSesionesActivas(str_IdUsuario);
                lstr_CodResultado = ltc_CerrarSesionesActivas.Lstr_CodigoResultado;
                if (String.Equals(lstr_CodResultado, "00"))
                {
                    lboo_Activa = true;
                }
            }
            catch (Exception ex)
            { }
            return lstr_CodResultado;
        }

        /// <summary>
        /// Consulta de usuarios
        /// </summary>
        /// <param name="str_IdUsuario">Identificador de usuario</param>
        /// <returns>Dataset con usuario(s) consultados</returns>
        public DataSet ConsultarUsuarios(string str_IdUsuario, string str_TipoIdUsuario, string str_IdSociedadGL, string str_NomUsuario)
        {
            DataSet lds_Usuarios = new DataSet();
            try
            {
                clsConsultarUsuarios ltc_ConsultarUsuarios = new clsConsultarUsuarios(str_IdUsuario, str_TipoIdUsuario, str_IdSociedadGL, str_NomUsuario);
                if (String.Equals(ltc_ConsultarUsuarios.Lstr_CodigoResultado, "00"))
                {
                    lds_Usuarios.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(ltc_ConsultarUsuarios.Lstr_RespuestaSchema)));
                    lds_Usuarios.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(ltc_ConsultarUsuarios.Lstr_RespuestaXML)));
                }
            }
            catch
            {
            }
            return lds_Usuarios;
        }

        /// <summary>
        /// Verifica que si el usuario especificado posee permisos sobre determinado objeto
        /// </summary>
        /// <param name="str_IdUsuario">Identificador de usuario</param>
        /// <param name="str_IdObjeto">Identificador de objeto</param>
        /// <param name="str_Permiso">Permiso a consultar</param>
        /// <returns>Resultado de verificacion</returns>
        public bool UsuarioPoseePermiso(string str_IdUsuario, string str_IdObjeto, string str_Permiso)
        {
            bool lboo_PoseePermiso = false;
            DataSet lds_PermisosUsuario = new DataSet();
            try
            {
                lds_PermisosUsuario = ufnConsultarPermisosUsuarios(str_IdUsuario, str_IdObjeto);
                int lint_CantidadFilas = lds_PermisosUsuario.Tables["Table"].Rows.Count;
                for (int lint_ContFilas = 0; lint_ContFilas < lint_CantidadFilas; lint_ContFilas++)
                {
                    string lstr_ValorPermiso = lds_PermisosUsuario.Tables["Table"].Rows[lint_ContFilas][str_Permiso].ToString();
                    if (lstr_ValorPermiso == "true")
                    {
                        lboo_PoseePermiso = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            { }
            return lboo_PoseePermiso;
        }

        public string[] ufnActualizarUsuario(string str_IdUsuario, string str_TipoIdUsuario, string str_NomUsuario, string str_CorreoUsuario,
            string boo_Activo, string boo_Administrador, string boo_CtaHabilitada, string str_IdSociedadGL,
            string str_UsrModifica, string str_FchModifica)
        {
            string[] ResultadoActualizacion = new string[2];
            ResultadoActualizacion[0] = "99";
            ResultadoActualizacion[1] = "Error al realizar la actualizacion";
            try
            {
                clsActualizarUsuario ltc_ActualizarUsuario = new clsActualizarUsuario(str_IdUsuario, str_TipoIdUsuario, str_NomUsuario,
                    str_CorreoUsuario, boo_Activo, boo_Administrador, boo_CtaHabilitada, str_IdSociedadGL,
                    str_UsrModifica, str_FchModifica);
                ResultadoActualizacion[0] = ltc_ActualizarUsuario.Lstr_CodigoResultado;
                ResultadoActualizacion[1] = ltc_ActualizarUsuario.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            { }
            return ResultadoActualizacion;
        }

        public string[] ufnActualizarPerfilUsuario(string str_CedUsuario, string str_ClaveActual, string str_NuevaClave, string str_Usuario,
            string str_FchModifica)
        {
            string[] lstr_ResultadoActualizacion = new string[2];
            try
            {
                clsActualizarPerfilUsuario ltc_ActualizarPerfilUsuario = new clsActualizarPerfilUsuario(str_CedUsuario, str_ClaveActual,
                    str_NuevaClave, str_Usuario, str_FchModifica);
                string lstr_CodResultado = ltc_ActualizarPerfilUsuario.Lstr_CodigoResultado;
                string lstr_Mensaje = ltc_ActualizarPerfilUsuario.Lstr_MensajeRespuesta;
                lstr_ResultadoActualizacion[0] = lstr_CodResultado;
                lstr_ResultadoActualizacion[1] = lstr_Mensaje;
            }
            catch (Exception ex)
            {
                lstr_ResultadoActualizacion[0] = "99";
                lstr_ResultadoActualizacion[1] = "Error en lógica";
            }
            return lstr_ResultadoActualizacion;
        }

        public string ufnRecuperarContrasena(string[] str_DatosConn, string str_Cedula, string str_Correo)
        {
            string lstr_CodResultado = String.Empty;
            string lstr_PlantillaCorreo = String.Empty;
            try
            {
                clsRecuperarContrasena lrc_RecuperarContrasena = new clsRecuperarContrasena(str_Cedula, str_Correo);
                if (lrc_RecuperarContrasena.Lstr_CodigoResultado != "00")
                {
                    lstr_CodResultado = lrc_RecuperarContrasena.Lstr_CodigoResultado;
                }
                else
                {
                    DataSet lds_Usuario = new DataSet();
                    lds_Usuario.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(lrc_RecuperarContrasena.Lstr_RespuestaSchema)));
                    lds_Usuario.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(lrc_RecuperarContrasena.Lstr_RespuestaXML)));
                    lstr_CodResultado = lds_Usuario.Tables["Table"].Rows[0]["Clave"].ToString();
                    lstr_PlantillaCorreo = CargarPlantillaCorreo(gcls_Seguridad.DescifrarTextoAES(lstr_CodResultado).Replace(str_Cedula, ""), "Clave");
                    EnviarCorreo(str_DatosConn, str_Correo, lstr_PlantillaCorreo, "Recuperación de contraseña");
                }
            }
            catch (Exception ex)
            {
                // lstr_CodResultado = ex.Message;
            }
            return lstr_CodResultado;
        }
        public string CargarPlantillaCorreo(string codigoConfirmacion, string str_TipoPlantilla)
        {
            string plantillaCorreo = String.Empty;
            string ldir_Plantilla = String.Empty;
            try
            {
                if (str_TipoPlantilla == "Clave")
                {
                    ldir_Plantilla = ConfigurationManager.AppSettings["DireccionClave"];
                }
                else if (str_TipoPlantilla == "Firma")
                {
                    ldir_Plantilla = ConfigurationManager.AppSettings["DirRegistroFirma"];
                }
                else
                {
                    ldir_Plantilla = ConfigurationManager.AppSettings["DireccionPlantilla"];
                }
                plantillaCorreo = System.IO.File.ReadAllText(ldir_Plantilla);

                plantillaCorreo = plantillaCorreo.Replace("#Codigo#", String.Format("{0}", codigoConfirmacion));
            }
            catch (Exception ex)
            {
                //ManejadorMensajesHelper.RegistrarMensaje(String.Format("Se ha producido un error al intentar generar el mensaje a enviar por correo:\n {0}", ex.Message), ManejadorMensajesHelper.EnumTipoMensaje.Error);
                plantillaCorreo = ex.ToString();
            }

            return plantillaCorreo;
        }

        public void EnviarCorreo(string[] str_DatosConn, string str_CorreoDestino, string str_Mensaje, string str_Asunto)
        {
            string str_Resultado = String.Empty;
            //try
            //{
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient();
            client.Port = Convert.ToInt32(str_DatosConn[0]);
            client.Host = str_DatosConn[1];

            //client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(str_DatosConn[3], str_DatosConn[4]);

            MailMessage mm = new MailMessage();
            mm.From = new MailAddress(str_DatosConn[2]);
            mm.To.Add(str_CorreoDestino);
            mm.Subject = str_Asunto;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(str_Mensaje,
                 Encoding.UTF8, MediaTypeNames.Text.Html);
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mm.AlternateViews.Add(htmlView);
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.Normal;
            client.Send(mm);
            //}
            //catch (Exception ex)
            //{

            //}
        }

        public void EnviarCorreoAttach(string[] str_DatosConn, string str_CorreoDestino, string str_Mensaje, string str_Asunto, params clsMailAttachment[] attachments)
        {
            string str_Resultado = String.Empty;
            try
            {
                // Command line argument must the the SMTP host.

                SmtpClient client = new SmtpClient();
                client.Port = Convert.ToInt32(str_DatosConn[0]);
                client.Host = str_DatosConn[1];

                //client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new System.Net.NetworkCredential(str_DatosConn[3], str_DatosConn[4]);

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(str_DatosConn[2]);
                mm.To.Add(str_CorreoDestino);
                mm.Subject = str_Asunto;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(str_Mensaje,
                     Encoding.UTF8, MediaTypeNames.Text.Html);
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.AlternateViews.Add(htmlView);
                mm.IsBodyHtml = true;
                mm.Priority = MailPriority.Normal;
                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment("your attachment file");
                //mm.Attachments.Add(attachment);
                foreach (clsMailAttachment ma in attachments)
                {
                    mm.Attachments.Add(ma.File);
                }
                client.Send(mm);
            }
            catch (Exception ex)
            {
                gcls_Seguridad.SaveError(ex);
            }
        }

        public bool EnviarCorreoPC(int CorreoClientePort, string CorreoClienteHost, string CorreoNetworkCredentialUsuario, string CorreoNetworkCredentialPassWord, string str_CorreoFrom, string str_CorreoTo, string str_Mensaje, string str_Asunto)
        {
            string str_Resultado = String.Empty;
            try
            {
                // Command line argument must the the SMTP host.
                SmtpClient client = new SmtpClient();
                client.Port = CorreoClientePort; //25;
                client.Host = CorreoClienteHost; //"172.18.100.11";

                //client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                //client.Credentials = new System.Net.NetworkCredential("hacienda\\scan", "hacienda01*");
                client.Credentials = new System.Net.NetworkCredential(CorreoNetworkCredentialUsuario, CorreoNetworkCredentialPassWord);

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(str_CorreoFrom);
                mm.To.Add(str_CorreoTo);
                mm.Subject = str_Asunto;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(str_Mensaje,
                     Encoding.UTF8, MediaTypeNames.Text.Html);
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.AlternateViews.Add(htmlView);
                mm.IsBodyHtml = true;
                mm.Priority = MailPriority.Normal;
                client.Send(mm);

                return true;
            }
            catch (Exception ex)
            {

                throw;

            }
            finally
            {

            }

        }


        /// <summary>
        /// Obtiene la IP del usuario
        /// </summary>
        /// <returns>IP del usuario</returns>
        private string ObtenerIPUsuario()
        {
            string DireccionIP = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                DireccionIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                DireccionIP = HttpContext.Current.Request.UserHostAddress;
            }
            return DireccionIP;
        }

        public tUsuario()
        {

        }


    }
}