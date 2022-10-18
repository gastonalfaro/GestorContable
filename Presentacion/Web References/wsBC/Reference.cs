﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace Presentacion.wsBC {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsBalanceComprobacionSoap", Namespace="http://tempuri.org/")]
    public partial class wsBalanceComprobacion : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback uwsBalanceComprobacionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsBalanceComprobacion() {
            this.Url = global::Presentacion.Properties.Settings.Default.Presentacion_wsBC_wsBalanceComprobacion;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event uwsBalanceComprobacionCompletedEventHandler uwsBalanceComprobacionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/uwsBalanceComprobacion", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet uwsBalanceComprobacion(ZINT_EST_CAB_BALANCE_CONSOL t_cabecera, ZINT_EST_POS_BALANCE_CONSOL[] t_posicion) {
            object[] results = this.Invoke("uwsBalanceComprobacion", new object[] {
                        t_cabecera,
                        t_posicion});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void uwsBalanceComprobacionAsync(ZINT_EST_CAB_BALANCE_CONSOL t_cabecera, ZINT_EST_POS_BALANCE_CONSOL[] t_posicion) {
            this.uwsBalanceComprobacionAsync(t_cabecera, t_posicion, null);
        }
        
        /// <remarks/>
        public void uwsBalanceComprobacionAsync(ZINT_EST_CAB_BALANCE_CONSOL t_cabecera, ZINT_EST_POS_BALANCE_CONSOL[] t_posicion, object userState) {
            if ((this.uwsBalanceComprobacionOperationCompleted == null)) {
                this.uwsBalanceComprobacionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnuwsBalanceComprobacionOperationCompleted);
            }
            this.InvokeAsync("uwsBalanceComprobacion", new object[] {
                        t_cabecera,
                        t_posicion}, this.uwsBalanceComprobacionOperationCompleted, userState);
        }
        
        private void OnuwsBalanceComprobacionOperationCompleted(object arg) {
            if ((this.uwsBalanceComprobacionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.uwsBalanceComprobacionCompleted(this, new uwsBalanceComprobacionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZINT_EST_CAB_BALANCE_CONSOL {
        
        private string lEDGERField;
        
        private string vISTAField;
        
        private string vERSIONField;
        
        private string eJERCICIOField;
        
        private string pERIODOField;
        
        private string uNID_CONSOLField;
        
        private string pLAN_POSField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LEDGER {
            get {
                return this.lEDGERField;
            }
            set {
                this.lEDGERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VISTA {
            get {
                return this.vISTAField;
            }
            set {
                this.vISTAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VERSION {
            get {
                return this.vERSIONField;
            }
            set {
                this.vERSIONField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string EJERCICIO {
            get {
                return this.eJERCICIOField;
            }
            set {
                this.eJERCICIOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PERIODO {
            get {
                return this.pERIODOField;
            }
            set {
                this.pERIODOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string UNID_CONSOL {
            get {
                return this.uNID_CONSOLField;
            }
            set {
                this.uNID_CONSOLField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PLAN_POS {
            get {
                return this.pLAN_POSField;
            }
            set {
                this.pLAN_POSField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3752.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZINT_EST_POS_BALANCE_CONSOL {
        
        private string pOSICIONField;
        
        private string sUBPOSICIONField;
        
        private string uNID_ASOCIADAField;
        
        private string mONEDAField;
        
        private string sIGNOField;
        
        private decimal vALOR_MLField;
        
        private decimal vALOR_MTField;
        
        private decimal vALOR_ACField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string POSICION {
            get {
                return this.pOSICIONField;
            }
            set {
                this.pOSICIONField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SUBPOSICION {
            get {
                return this.sUBPOSICIONField;
            }
            set {
                this.sUBPOSICIONField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string UNID_ASOCIADA {
            get {
                return this.uNID_ASOCIADAField;
            }
            set {
                this.uNID_ASOCIADAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MONEDA {
            get {
                return this.mONEDAField;
            }
            set {
                this.mONEDAField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SIGNO {
            get {
                return this.sIGNOField;
            }
            set {
                this.sIGNOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal VALOR_ML {
            get {
                return this.vALOR_MLField;
            }
            set {
                this.vALOR_MLField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal VALOR_MT {
            get {
                return this.vALOR_MTField;
            }
            set {
                this.vALOR_MTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal VALOR_AC {
            get {
                return this.vALOR_ACField;
            }
            set {
                this.vALOR_ACField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void uwsBalanceComprobacionCompletedEventHandler(object sender, uwsBalanceComprobacionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class uwsBalanceComprobacionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal uwsBalanceComprobacionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591