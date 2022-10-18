using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros
{
    public class clsCrearArchivoDeuda : clsProcedimientoAlmacenado
    {
        private String lstr_IdModulo;
        private int lint_IdOpcionCategoria;
        private Int16 lint_Anno;
        private Int16 lint_Mes;
        private String lstr_Usuario;
        private int lint_TmpIdArchivoDeuda;
        
        public String Lstr_IdModulo
       {
           get { return lstr_IdModulo; }
           set { lstr_IdModulo = value; }
        }
        public int Lint_IdOpcionCategoria
        {
           get { return lint_IdOpcionCategoria; }
           set { lint_IdOpcionCategoria = value; }
        }
        public Int16 Lint_Anno
        {
            get { return lint_Anno; }
            set { lint_Anno = value; }
        }
        public Int16 Lint_Mes
        {
            get { return lint_Mes; }
            set { lint_Mes = value; }
        }
        public String Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }

        }

        public int Lint_TmpIdArchivoDeuda
        {
            get { return lint_TmpIdArchivoDeuda; }
            set { lint_TmpIdArchivoDeuda = value; }
        }
    


        public clsCrearArchivoDeuda(string lstr_IdModulo, Int16 lint_Mes, Int16 lint_Anno, int lint_IdOpcionCategoria, string lstr_Usuario)
        {
            this.lstr_IdModulo = lstr_IdModulo;
            this.Lint_Mes = lint_Mes;
            this.lint_Anno = lint_Anno;
            this.lint_IdOpcionCategoria = lint_IdOpcionCategoria;
            this.lstr_Usuario = lstr_Usuario;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\CalculosFinancieros\\CrearArchivoDeuda.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }

        }

    }
}