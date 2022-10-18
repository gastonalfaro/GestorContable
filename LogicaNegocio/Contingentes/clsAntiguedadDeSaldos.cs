using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Contigentes;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.Contingentes
{
    public class clsAntiguedadDeSaldos
    {

        #region Parámetros

        private int? lint_IdAntiguedadSaldos;
        public int? Lint_IdAntiguedadSaldos
        {
            get { return lint_IdAntiguedadSaldos; }
            set { lint_IdAntiguedadSaldos = value; }
        }
        private int? lint_IdPrevisionIncobrables;
        public int? Lint_IdPrevisionIncobrables
        {
            get { return lint_IdPrevisionIncobrables; }
            set { lint_IdPrevisionIncobrables = value; }
        }

        private string lstr_IdResolucion;
        public string Lstr_IdResolucion
        {
            get { return lstr_IdResolucion; }
            set { lstr_IdResolucion = value; }
        }
        private string lstr_IdExpediente;
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        private string lstr_DescripcionVencimiento;
        public string Lstr_DescripcionVencimiento
        {
            get { return lstr_DescripcionVencimiento; }
            set { lstr_DescripcionVencimiento = value; }
        }
        private int? lint_DiasDeCuenta;
        public int? Lint_DiasDeCuenta
        {
            get { return lint_DiasDeCuenta; }
            set { lint_DiasDeCuenta = value; }
        }
        private int? lint_MesesDeCuenta;
        public int? Lint_MesesDeCuenta
        {
            get { return lint_MesesDeCuenta; }
            set { lint_MesesDeCuenta = value; }
        }
        private decimal? ldec_MontoIncobrable;
        public decimal? Ldec_MontoIncobrable
        {
            get { return ldec_MontoIncobrable; }
            set { ldec_MontoIncobrable = value; }
        }

        private decimal? ldec_DiferenciaAjustar;
        public decimal? Ldec_DiferenciaAjustar
        {
            get { return ldec_DiferenciaAjustar; }
            set { ldec_DiferenciaAjustar = value; }
        }
        private decimal? ldec_PorcentajeIncobrable;
        public decimal? Ldec_PorcentajeIncobrable
        {
            get { return ldec_PorcentajeIncobrable; }
            set { ldec_PorcentajeIncobrable = value; }
        }

        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        private string lstr_Usuario;
        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }

        private int? lint_TmpIdAntiguedadSaldos;
        public int? Lint_TmpIdAntiguedadSaldos
        {
            get { return lint_TmpIdAntiguedadSaldos; }
            set { lint_TmpIdAntiguedadSaldos = value; }
        }

        #endregion

        #region Obtención y asignación


        #endregion
        public clsAntiguedadDeSaldos() { }

        public Boolean CrearAntiguedadDeSaldos(int? lint_IdAntiguedadSaldos, int? lint_IdPrevisionIncobrables, string lstr_IdResolucion, string lstr_IdExpediente,
            string lstr_DescripcionVencimiento, int? lint_DiasDeCuenta, int? lint_MesesDeCuenta, decimal? ldec_MontoIncobrable, decimal? ldec_DiferenciaAjustar,
            decimal? ldec_PorcentajeIncobrable, string lstr_Estado, string lstr_Usuario,
            out string str_CodResultado, out string str_Mensaje, out int? int_TmpIdAntiguedadSaldos)
        {
            string[] resultado = new string[3];
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            int_TmpIdAntiguedadSaldos = 0;
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsCrearAntiguedadDeSaldos cr_Procedimiento = new clsCrearAntiguedadDeSaldos(lint_IdAntiguedadSaldos, lint_IdPrevisionIncobrables, lstr_IdResolucion, lstr_IdExpediente,
            lstr_DescripcionVencimiento, lint_DiasDeCuenta, lint_MesesDeCuenta, ldec_MontoIncobrable, ldec_DiferenciaAjustar,
            ldec_PorcentajeIncobrable, lstr_Estado, lstr_Usuario);

                resultado[0] = str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                resultado[1] = str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;
                int_TmpIdAntiguedadSaldos = cr_Procedimiento.Lint_TmpIdAntiguedadSaldos;
                resultado[2] = int_TmpIdAntiguedadSaldos.ToString();

                if (String.Equals(str_CodResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                    int_TmpIdAntiguedadSaldos = Convert.ToInt32(lds_TablasConsulta.Tables["Table"].Rows[0]["pTmpIdAntiguedadSaldos"]);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}