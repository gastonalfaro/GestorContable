using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Datos.ConexionSQL.Procedimientos.Seguridad;

namespace LogicaNegocio.Seguridad
{
    public class tPermisosObjeto
    {
        private int lint_IdRol;
        public int Lint_IdRol
        {
            get { return lint_IdRol; }
            set { lint_IdRol = value; }
        }

        private string lstr_IdObjeto;
        public string Lstr_IdObjeto
        {
            get { return lstr_IdObjeto; }
            set { lstr_IdObjeto = value; }
        }

        private bool lboo_Consultar;
        public bool Lboo_Consultar
        {
            get { return lboo_Consultar; }
            set { lboo_Consultar = value; }
        }

        private bool lboo_Insertar;
        public bool Lboo_Insertar
        {
            get { return lboo_Insertar; }
            set { lboo_Insertar = value; }
        }

        private bool lboo_Borrar;
        public bool Lboo_Borrar
        {
            get { return lboo_Borrar; }
            set { lboo_Borrar = value; }
        }

        private bool lboo_Actualizar;
        public bool Lboo_Actualizar
        {
            get { return lboo_Actualizar; }
            set { lboo_Actualizar = value; }
        }

        private bool lboo_Exportar;
        public bool Lboo_Exportar
        {
            get { return lboo_Exportar; }
            set { lboo_Exportar = value; }
        }

        private bool lboo_Imprimir;
        public bool Lboo_Imprimir
        {
            get { return lboo_Imprimir; }
            set { lboo_Imprimir = value; }
        }

        public DataSet ConsultarRolesObjetos(string int_IdRol, string str_IdObjeto, string str_DescObjeto)
        {
            DataSet lds_RolesObjetos = new DataSet();
            try
            {
                clsConsultarRolesObjetos cru_PermisosUsuario = new clsConsultarRolesObjetos(int_IdRol, str_IdObjeto, str_DescObjeto);
                lds_RolesObjetos.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cru_PermisosUsuario.Lstr_RespuestaSchema)));
                lds_RolesObjetos.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cru_PermisosUsuario.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_RolesObjetos;
        }

        /// <summary>
        /// Creacion de un nuevo rol
        /// </summary>
        /// <param name="int_IdRol"></param>
        /// <param name="str_IdObjeto"></param>
        /// <param name="boo_Consultar"></param>
        /// <param name="boo_Insertar"></param>
        /// <param name="boo_Borrar"></param>
        /// <param name="boo_Actualizar"></param>
        /// <param name="boo_Exportar"></param>
        /// <param name="boo_Imprimir"></param>
        /// <param name="str_UsrCreacion"></param>
        /// <returns></returns>
        public bool CrearRolObjeto(string int_IdRol, string str_IdObjeto, string boo_Consultar, string boo_Insertar,
            string boo_Borrar, string boo_Actualizar, string boo_Exportar, string boo_Imprimir, string str_UsrCreacion)
        {
            bool bool_ResCreacion = false;
            try
            {
                clsCrearRolObjeto cls_ProcCrearRolObjeto = new clsCrearRolObjeto(int_IdRol, str_IdObjeto, boo_Consultar, boo_Insertar,
                    boo_Borrar, boo_Actualizar, boo_Exportar, boo_Imprimir, str_UsrCreacion);
                if (String.Equals(cls_ProcCrearRolObjeto.Lstr_CodigoResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool ActualizarRolObjeto(string int_IdRol, string str_IdObjeto, string boo_Consultar, string boo_Insertar,
           string boo_Borrar, string boo_Actualizar, string boo_Exportar, string boo_Imprimir,
            string str_Usuario, string str_FchModifica, out string str_MensajeProcedimiento)
        {
            bool bool_ResultadoActualizacion = false;
            try
            {
                clsActualizarPermisosRolObjeto ltc_ActualizarRolObjeto = new clsActualizarPermisosRolObjeto(int_IdRol, str_IdObjeto, boo_Consultar, boo_Insertar,
                    boo_Borrar, boo_Actualizar, boo_Exportar, boo_Imprimir, str_Usuario, str_FchModifica);
                string lstr_CodResultado = ltc_ActualizarRolObjeto.Lstr_CodigoResultado;
                if (String.Equals(lstr_CodResultado, "00"))
                {
                    bool_ResultadoActualizacion = true;
                }
                str_MensajeProcedimiento = ltc_ActualizarRolObjeto.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeProcedimiento = "Error al realizar el procedimiento";
            }
            return bool_ResultadoActualizacion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="int_IdRol">Identificador de rol</param>
        /// <param name="str_IdObjeto">Identificador de objeto</param>
        /// <param name="str_FchModifica">Fecha de consulta</param>
        /// <param name="str_MensajeSalida">Mensaje que se recibe de la base de datos</param>
        /// <returns>Resultado de eliminacion</returns>
        public bool EliminarRolObjeto(string int_IdRol, string str_IdObjeto, string str_FchModifica, out string str_MensajeSalida)
        {
            bool lboo_ResultadoEliminacion = false;
            try
            {
                clsEliminarRolObjeto cla_EliminarRolObjeto = new clsEliminarRolObjeto(int_IdRol, str_IdObjeto, str_FchModifica);
                if (String.Equals(cla_EliminarRolObjeto.Lstr_CodigoResultado, "00"))
                {
                    lboo_ResultadoEliminacion = true;
                }
                str_MensajeSalida = cla_EliminarRolObjeto.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }
            return lboo_ResultadoEliminacion;
        }
        public tPermisosObjeto()
        { }
    }
}