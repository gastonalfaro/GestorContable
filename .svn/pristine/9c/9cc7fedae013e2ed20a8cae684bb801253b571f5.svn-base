using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.Mantenimiento;
using System.Data;
using log4net;
using log4net.Config;

namespace LogicaNegocio.Mantenimiento
{
    public class clsPrevisionesIncobrables
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");

        private Nullable<int> lint_DiasMorosidad;
        public Nullable<int> Lint_DiasMorosidad
        {
            get { return lint_DiasMorosidad; }
            set { lint_DiasMorosidad = value; }
        }


        private Nullable<Decimal> ldec_PorcEstimacion;
        public Nullable<Decimal> Ldec_PorcEstimacion
        {
            get { return ldec_PorcEstimacion; }
            set { ldec_PorcEstimacion = value; }
        }


        private string lstr_Descripcion;
        public string Lstr_Descripcion
        {
            get { return lstr_Descripcion; }
            set { lstr_Descripcion = value; }
        }


        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        private DateTime ldt_FchModifica;
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }

        public DataSet ConsultarPrevisionesIncobrables(Nullable<int> int_DiasMorosidad, Nullable<Decimal> dec_PorcEstimacion, string str_Descripcion)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarPrevisionesIncobrables cr_Procedimiento = new clsConsultarPrevisionesIncobrables(int_DiasMorosidad, dec_PorcEstimacion, str_Descripcion);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch
            { }
            return lds_TablasConsulta;
        }

        public bool CrearPrevisionIncobrable(Int32 int_DiasMorosidad, Decimal dec_PorcEstimacion, string str_Descripcion, string str_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCrearPrevisionIncobrable cls_ProcCrearPrevisionIncobrable = new clsCrearPrevisionIncobrable(int_DiasMorosidad, dec_PorcEstimacion, str_Descripcion, str_UsrCreacion);
                str_CodResultado = cls_ProcCrearPrevisionIncobrable.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcCrearPrevisionIncobrable.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool ModificarPrevisionIncobrable(Int32 int_DiasMorosidad, Decimal dec_PorcEstimacion, string str_Descripcion, string str_UsrModifica, DateTime dt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarPrevisionIncobrable cls_ProcModificarPrevisionIncobrable = new clsModificarPrevisionIncobrable(int_DiasMorosidad, dec_PorcEstimacion, str_Descripcion, str_UsrModifica, dt_FchModifica);
                str_CodResultado = cls_ProcModificarPrevisionIncobrable.Lstr_CodigoResultado;
                str_Mensaje = cls_ProcModificarPrevisionIncobrable.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public clsPrevisionesIncobrables()
        { }
    }
}