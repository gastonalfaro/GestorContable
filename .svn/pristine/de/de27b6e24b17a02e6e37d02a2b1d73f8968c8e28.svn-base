using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Compartidas
{
    [Serializable]
    public class clsArchivos
    {
        private int lint_Tamano = 0;
        private string lstr_Nombre = String.Empty;
        private string lstr_TipoContenido = String.Empty;
        private byte[] lbyt_Datos;


        public int Lint_Tamano
        {
            set 
            { 
                lint_Tamano = value;
            }
            get { return lint_Tamano; }
        }

        public string Lstr_Nombre
        {
            set { lstr_Nombre = value; }
            get { return lstr_Nombre; }
        }

        public string Lstr_TipoContenido
        {
            set { lstr_TipoContenido = value; }
            get { return lstr_TipoContenido; }
        }

        public byte[] Lbyt_Datos
        {
            set { lbyt_Datos = value; }
            get { return lbyt_Datos; }
        }


        public clsArchivos(int int_Tamano, string str_Nombre, string TipoContenido)
        {
            lint_Tamano = int_Tamano;
            lstr_Nombre = str_Nombre;
            lstr_TipoContenido = str_Nombre;
            lbyt_Datos = new byte[int_Tamano];
        }

        public clsArchivos()
        {

        }
        
    }

    [Serializable]
    public class clsListaArchivos
    {
        private clsArchivos l_Archivo;
        private List<clsArchivos> l_ListaArchivos;

        public clsArchivos L_Archivo
        {
            get
            {
                return l_Archivo;
            }
            set
            {
                l_Archivo = value;
            }
        }

        
        public void EliminarArchivo(string str_nombre, int int_tamano)
        {
            if (l_ListaArchivos != null)
            {
                l_ListaArchivos.RemoveAll(archivo => (archivo.Lstr_Nombre == str_nombre) && (archivo.Lint_Tamano == int_tamano));
            }
        }

        public List<clsArchivos> L_ListaArchivos
        {
            get
            {
                return l_ListaArchivos;
            }
            set
            {
                l_ListaArchivos = value;
            }
        }

        public clsListaArchivos(List<clsArchivos> listaArchivos)
        {
            l_ListaArchivos = listaArchivos;
        }

        public clsListaArchivos()
        {
            l_Archivo = new clsArchivos();
           l_ListaArchivos = new List<clsArchivos>();
        }
    }
}