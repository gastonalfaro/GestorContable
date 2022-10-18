using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsModificarExpediente : clsProcedimientoAlmacenado
    {
        private string lstr_RutaArchivo = "..\\Contigentes\\Expedientes\\EliminarExpediente.config";


        public clsModificarExpediente()
        {
            EjecucionSP(lstr_RutaArchivo, this);
        }
    }
}