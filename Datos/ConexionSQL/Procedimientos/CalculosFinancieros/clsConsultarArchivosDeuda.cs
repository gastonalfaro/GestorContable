using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros
{
    public class clsConsultarArchivosDeuda : clsProcedimientoAlmacenado
    {
        private Int64? lint_IdArchivoDeuda;
        private Int16? lint_Anno;
        private Int16? lint_Mes;
        private string lstr_Categoria;

        public Int64? Lint_IdArchivoDeuda
        {
            get { return lint_IdArchivoDeuda; }
            set { lint_IdArchivoDeuda = value; }
        }
        public Int16? Lint_Anno
        {
            get { return lint_Anno; }
            set { lint_Anno = value; }
        }
        public Int16? Lint_Mes
        {
            get { return lint_Mes; }
            set { lint_Mes = value; }
        }
        public string Lstr_Categoria
        {
            get { return lstr_Categoria; }
            set { lstr_Categoria = value; }
        }
        public clsConsultarArchivosDeuda(Int64? lint_IdArchivoDeuda, Int16? lint_Anno, Int16? lint_Mes, string lstr_Categoria)
        {
            this.lint_IdArchivoDeuda = lint_IdArchivoDeuda;
            this.lint_Anno = lint_Anno;
            this.lint_Mes = lint_Mes;
            this.lstr_Categoria = lstr_Categoria;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\CalculosFinancieros\\ConsultarArchivosDeuda.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}