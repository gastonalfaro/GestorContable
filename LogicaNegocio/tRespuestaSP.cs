using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;

namespace LogicaNegocio
{
    public class tRespuestaSP
    {
        private DataSet ds_Resultado;
        private clsProcedimientoAlmacenado Procedimiento;

        public tRespuestaSP(clsProcedimientoAlmacenado Procedimiento)
        { Procedimiento.EjecucionSP("D:\\PwC\\Prop\\Ejemplo.config", Procedimiento); }
    }
}