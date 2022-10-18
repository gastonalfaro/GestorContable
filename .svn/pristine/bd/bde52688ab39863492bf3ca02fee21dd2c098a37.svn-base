using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using System.Configuration;
using System.Reflection;

using System.Data;
using System.IO;
using System.Net;

using System.Globalization;

using Microsoft.Reporting.WebForms;

namespace Presentacion.Compartidas.VisorReportes
{
    public class ParametrosReporte
    {
        //public Microsoft.Reporting.WebForms.ReportParameter _oParam;
        public string _DireccionReporte;
        public List<ReportParameter> _oParametros = new List<ReportParameter>();
        public string _ServidorReportes;

        public ParametrosReporte(/*Microsoft.Reporting.WebForms.ReportParameter oParam, */string DireccionReporte, List<ReportParameter> oParametros, string ServidorReportes)
        {
            /*_oParam = oParam;*/
            _DireccionReporte = DireccionReporte;
            _oParametros = oParametros;
            _ServidorReportes = ServidorReportes;
        }
    }
}