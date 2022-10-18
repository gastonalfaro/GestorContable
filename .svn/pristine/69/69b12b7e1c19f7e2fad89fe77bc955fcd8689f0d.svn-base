using Datos.ConexionSQL.Procedimientos.CalculosFinancieros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.CalculosFinancieros
{
    public class clsNotasCalculosFinancieros
    {
        private String lstr_IdModulo;
        private int lint_IdOpcionCategoria;
        private Int16 lint_Anno;
        private Int16 lint_Mes;
        private String lstr_Usuario;
        private string lint_IdArchivoDeuda;

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


        public string Lint_IdArchivoDeuda
        {
            get { return lint_IdArchivoDeuda; }
            set { lint_IdArchivoDeuda = value; }
        }

        public bool CrearArchivoDeuda(string lstr_IdModulo, Int16 lint_Mes, Int16 lint_Anno, int lint_IdOpcionCategoria, string lstr_Usuario,
                                                    out string str_CodResultado, out string str_Mensaje, out int int_TmpIdArchivoDuda)
        {
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            int_TmpIdArchivoDuda = 0;
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsCrearArchivoDeuda cr_Procedimiento = new clsCrearArchivoDeuda(lstr_IdModulo,
                                                                                                 lint_Mes,
                                                                                                 lint_Anno,
                                                                                                 lint_IdOpcionCategoria,
                                                                                                 lstr_Usuario);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;
                int_TmpIdArchivoDuda = cr_Procedimiento.Lint_TmpIdArchivoDeuda;

                if (String.Equals(str_CodResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                    //if (int_TmpIdArchivoDuda != 0)
                    //    int_TmpIdArchivoDuda = lint_IdFormulario;
                    //else
                    int_TmpIdArchivoDuda = Convert.ToInt32(lds_TablasConsulta.Tables["Table"].Rows[0]["pTmpIdArchivoDeuda"]);
                    return true;
                }
                else
                {
                    int_TmpIdArchivoDuda = -1;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataSet ConsultarArchivosDeuda(Int64? lint_IdArchivoDeuda, Int16? lint_Anno, Int16? lint_Mes, string lstr_Categoria)

        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarArchivosDeuda cr_Procedimiento = new clsConsultarArchivosDeuda(lint_IdArchivoDeuda, lint_Anno, lint_Mes, lstr_Categoria);
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            { }
            return lds_TablasConsulta;
        }
        public DataSet ConsultarCategoriasNotas()
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCategoriasNotas cr_Procedimiento = new clsConsultarCategoriasNotas();
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            { }
            return lds_TablasConsulta;
        }
    }
}