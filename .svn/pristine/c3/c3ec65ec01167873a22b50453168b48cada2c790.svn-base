using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsActualizarPermisosRolObjeto : clsProcedimientoAlmacenado
    {
        private string lint_IdRol;
        public string Lint_IdRol
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

        private string lboo_Consultar;
        public string Lboo_Consultar
        {
            get { return lboo_Consultar; }
            set { lboo_Consultar = value; }
        }

        private string lboo_Insertar;
        public string Lboo_Insertar
        {
            get { return lboo_Insertar; }
            set { lboo_Insertar = value; }
        }

        private string lboo_Borrar;
        public string Lboo_Borrar
        {
            get { return lboo_Borrar; }
            set { lboo_Borrar = value; }
        }

        private string lboo_Actualizar;
        public string Lboo_Actualizar
        {
            get { return lboo_Actualizar; }
            set { lboo_Actualizar = value; }
        }

        private string lboo_Exportar;
        public string Lboo_Exportar
        {
            get { return lboo_Exportar; }
            set { lboo_Exportar = value; }
        }

        private string lboo_Imprimir;
        public string Lboo_Imprimir
        {
            get { return lboo_Imprimir; }
            set { lboo_Imprimir = value; }
        }

        private string lstr_Usuario;
        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }

        private string lstr_FchModifica;
        public string Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }

        public clsActualizarPermisosRolObjeto(string int_IdRol, string str_IdObjeto, string boo_Consultar, string boo_Insertar,
            string boo_Borrar, string boo_Actualizar, string boo_Exportar, string boo_Imprimir, string str_Usuario, string str_FchModifica)
        {
            lint_IdRol = int_IdRol;
            lstr_IdObjeto = str_IdObjeto;
            lboo_Consultar = boo_Consultar;
            lboo_Insertar = boo_Insertar;
            lboo_Borrar = boo_Borrar;
            lboo_Actualizar = boo_Actualizar;
            lboo_Exportar = boo_Exportar;
            lboo_Imprimir = boo_Imprimir;
            lstr_Usuario = str_Usuario;
            lstr_FchModifica = str_FchModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ActualizarPermisosRolObjeto.config", this);
        }

    }
}