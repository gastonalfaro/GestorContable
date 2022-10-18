using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using System.Security.Principal;
using System.Net;

namespace Presentacion.Compartidas.VisorReportes
{
    public class ReportViewerCredentials : IReportServerCredentials
    {
        private string _userName;
        private string _password;
        private string _domain;

        public ReportViewerCredentials(string userName, string password, string domain)
        {
            _userName = userName;
            _password = password;
            _domain = domain;
        }

        public WindowsIdentity ImpersonationUser
        {
            get
            {
                //return null;
                return WindowsIdentity.GetCurrent();
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                return new NetworkCredential(_userName, _password, _domain);
            }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = _userName;
            password = _password;
            authority = _domain;
            // Not using form credentials  

            return false;
        }

    } 
}