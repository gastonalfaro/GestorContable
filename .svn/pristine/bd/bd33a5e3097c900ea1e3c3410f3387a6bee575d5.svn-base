using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogicaNegocio.Mantenimiento;
using Datos.ConexionSQL.Procedimientos.Seguridad;
using System.Data;

namespace LogicaNegocio.Seguridad
{
    public class tObjeto
    {
        private int lint_IdObjeto;
        public int Lint_IdObjeto
        {
            get { return lint_IdObjeto; }
            set { lint_IdObjeto = value; }
        }

        private tModulo ltmod_Modulo;
        public tModulo Ltmod_Modulo
        {
            get { return ltmod_Modulo; }
            set { ltmod_Modulo = value; }
        }

        private string lstr_Tipo;
        public string Lstr_Tipo
        {
            get { return lstr_Tipo; }
            set { lstr_Tipo = value; }
        }

        private string lstr_Descripcion;
        public string Lstr_Descripcion
        {
            get { return lstr_Descripcion; }
            set { lstr_Descripcion = value; }
        }

        private tPermisosObjeto ltper_Permisos = new tPermisosObjeto();
        public tPermisosObjeto Ltper_Permisos
        {
            get { return ltper_Permisos; }
            set { ltper_Permisos = value; }
        }

        /// <summary>
        /// Creacion de objeto
        /// </summary>
        /// <param name="str_IdObjeto"></param>
        /// <param name="str_IdModulo"></param>
        /// <param name="str_TipoObjeto"></param>
        /// <param name="str_DescObjeto"></param>
        /// <param name="str_UsrCreacion"></param>
        /// <returns></returns>
        public string CrearObjeto(string str_IdObjeto, string str_IdModulo, string str_TipoObjeto,
            string str_DescObjeto, string str_UsrCreacion, out string str_MensajeSalida)
        {
            bool bool_ResultadoCreacion = false;
            string lstr_Codigo = "99";
            try
            {
                clsCrearObjeto cls_ProcCrearObjeto = new clsCrearObjeto(str_IdObjeto, str_IdModulo, str_TipoObjeto, str_DescObjeto, str_UsrCreacion);
                lstr_Codigo = cls_ProcCrearObjeto.Lstr_CodigoResultado;
                str_MensajeSalida = cls_ProcCrearObjeto.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";

            }
            return lstr_Codigo;
        }

        /// <summary>
        /// Consulta de objetos
        /// </summary>
        /// <param name="str_IdObjeto"></param>
        /// <param name="str_IdModulo"></param>
        /// <returns></returns>
        public DataSet ConsultarObjetos(string str_IdObjeto, string str_IdModulo, string str_DescObjeto, string str_TipoObjeto, out string str_MensajeSalida)
        {
            DataSet lds_TablasObjetos = new DataSet();
            try
            {
                clsConsultarObjetos cls_ProcConsultarObjetos = new clsConsultarObjetos(str_IdObjeto, str_IdModulo, str_DescObjeto, str_TipoObjeto);
                if (String.Equals(cls_ProcConsultarObjetos.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasObjetos.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcConsultarObjetos.Lstr_RespuestaSchema)));
                    lds_TablasObjetos.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ProcConsultarObjetos.Lstr_RespuestaXML)));
                }
                str_MensajeSalida = cls_ProcConsultarObjetos.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }

            return lds_TablasObjetos;
        }

        /// <summary>
        /// Actualizacion de objetos
        /// </summary>
        /// <param name="str_IdObjeto"></param>
        /// <param name="str_IdModulo"></param>
        /// <param name="str_TipoObjeto"></param>
        /// <param name="str_DescObjeto"></param>
        /// <param name="str_UsrModificacion"></param>
        /// <returns></returns>
        public bool ActualizarObjeto(string str_IdObjeto, string str_IdModulo, string str_Habilitado, string str_DescObjeto,
            string str_UsrModificacion, out string str_MensajeSalida)
        {
            bool bool_ResultadoActualizacion = false;
            try
            {
                clsActualizarObjeto cla_ActualizarObjeto = new clsActualizarObjeto(str_IdObjeto, str_IdModulo, str_Habilitado, str_DescObjeto, str_UsrModificacion);
                if (String.Equals(cla_ActualizarObjeto.Lstr_CodigoResultado, "00"))
                {
                    bool_ResultadoActualizacion = true;
                }
                str_MensajeSalida = cla_ActualizarObjeto.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }

            return bool_ResultadoActualizacion;
        }

        /// <summary>
        /// Funcion encargada de eliminar objetos del sistema
        /// </summary>
        /// <param name="str_IdObjeto">Identificador del objeto</param>
        /// <param name="str_IdModulo">Ientificador del modulo</param>
        /// <param name="str_FchModificacion">Fecha de consulta del objeto</param>
        /// <returns>True en cado de que la eliminacion sea exitosa</returns>
        public bool ufnEliminarObjeto(string str_IdObjeto, string str_IdModulo, string str_FchModificacion, out string str_MensajeSalida)
        {
            bool lboo_ResultadoEliminacion = false;
            try
            {
                clsEliminarObjeto cla_EliminarObjeto = new clsEliminarObjeto(str_IdObjeto, str_IdModulo, str_FchModificacion);
                if (String.Equals(cla_EliminarObjeto.Lstr_CodigoResultado, "00"))
                {
                    lboo_ResultadoEliminacion = true;
                }
                str_MensajeSalida = cla_EliminarObjeto.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                str_MensajeSalida = "No ha sido posible acceder a la base de datos";
            }
            return lboo_ResultadoEliminacion;
        }
        public tObjeto()
        { }


        public string IDObjeto { private set; get; }
        public string NombreObjeto { private set; get; }


        public tObjeto(string pIDObjeto, string pNombreObjeto)
        {
            this.IDObjeto = pIDObjeto;
            this.NombreObjeto = pNombreObjeto;
        }

    }
}