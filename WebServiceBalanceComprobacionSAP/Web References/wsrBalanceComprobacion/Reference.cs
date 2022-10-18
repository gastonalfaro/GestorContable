﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.34209.
// 
#pragma warning disable 1591

namespace WebServiceBalanceComprobacionSAP.wsrBalanceComprobacion {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    // CODEGEN: The optional WSDL extension element 'Policy' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="Binding", Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZINT_RECIBE_BALANCE_COMPROBA : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback CallZINT_RECIBE_BALANCE_COMPROBAOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ZINT_RECIBE_BALANCE_COMPROBA() {
            this.Url = global::WebServiceBalanceComprobacionSAP.Properties.Settings.Default.WebServiceBalanceComprobacionSAP_wsrBalanceComprobacion_ZINT_RECIBE_BALANCE_COMPROBA;
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
        public event CallZINT_RECIBE_BALANCE_COMPROBACompletedEventHandler CallZINT_RECIBE_BALANCE_COMPROBACompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sap-com:document:sap:rfc:functions:ZINT_RECIBE_BALANCE_COMPROBA:ZINT_RECIBE_B" +
            "ALANCE_COMPROBARequest", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("ZINT_RECIBE_BALANCE_COMPROBAResponse", Namespace="urn:sap-com:document:sap:rfc:functions")]
        public ZINT_RECIBE_BALANCE_COMPROBAResponse CallZINT_RECIBE_BALANCE_COMPROBA([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")] ZINT_RECIBE_BALANCE_COMPROBA1 ZINT_RECIBE_BALANCE_COMPROBA) {
            object[] results = this.Invoke("CallZINT_RECIBE_BALANCE_COMPROBA", new object[] {
                        ZINT_RECIBE_BALANCE_COMPROBA});
            return ((ZINT_RECIBE_BALANCE_COMPROBAResponse)(results[0]));
        }
        
        /// <remarks/>
        public void CallZINT_RECIBE_BALANCE_COMPROBAAsync(ZINT_RECIBE_BALANCE_COMPROBA1 ZINT_RECIBE_BALANCE_COMPROBA) {
            this.CallZINT_RECIBE_BALANCE_COMPROBAAsync(ZINT_RECIBE_BALANCE_COMPROBA, null);
        }
        
        /// <remarks/>
        public void CallZINT_RECIBE_BALANCE_COMPROBAAsync(ZINT_RECIBE_BALANCE_COMPROBA1 ZINT_RECIBE_BALANCE_COMPROBA, object userState) {
            if ((this.CallZINT_RECIBE_BALANCE_COMPROBAOperationCompleted == null)) {
                this.CallZINT_RECIBE_BALANCE_COMPROBAOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCallZINT_RECIBE_BALANCE_COMPROBAOperationCompleted);
            }
            this.InvokeAsync("CallZINT_RECIBE_BALANCE_COMPROBA", new object[] {
                        ZINT_RECIBE_BALANCE_COMPROBA}, this.CallZINT_RECIBE_BALANCE_COMPROBAOperationCompleted, userState);
        }
        
        private void OnCallZINT_RECIBE_BALANCE_COMPROBAOperationCompleted(object arg) {
            if ((this.CallZINT_RECIBE_BALANCE_COMPROBACompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CallZINT_RECIBE_BALANCE_COMPROBACompleted(this, new CallZINT_RECIBE_BALANCE_COMPROBACompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZINT_RECIBE_BALANCE_COMPROBA1 {
        
        private ZINT_EST_POS_BALANCE_CONSOL[] iT_POSICIONESField;
        
        private ZINT_EST_CAB_BALANCE_CONSOL i_CABECERAField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZINT_EST_POS_BALANCE_CONSOL[] IT_POSICIONES {
            get {
                return this.iT_POSICIONESField;
            }
            set {
                this.iT_POSICIONESField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ZINT_EST_CAB_BALANCE_CONSOL I_CABECERA {
            get {
                return this.i_CABECERAField;
            }
            set {
                this.i_CABECERAField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZINT_EST_MESSAGE2 {
        
        private string tYPEField;
        
        private string idField;
        
        private string nUMBERField;
        
        private string mESSAGEField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TYPE {
            get {
                return this.tYPEField;
            }
            set {
                this.tYPEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NUMBER {
            get {
                return this.nUMBERField;
            }
            set {
                this.nUMBERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE {
            get {
                return this.mESSAGEField;
            }
            set {
                this.mESSAGEField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZINT_RECIBE_BALANCE_COMPROBAResponse {
        
        private ZINT_EST_MESSAGE2[] eT_MESSAGEField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZINT_EST_MESSAGE2[] ET_MESSAGE {
            get {
                return this.eT_MESSAGEField;
            }
            set {
                this.eT_MESSAGEField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void CallZINT_RECIBE_BALANCE_COMPROBACompletedEventHandler(object sender, CallZINT_RECIBE_BALANCE_COMPROBACompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CallZINT_RECIBE_BALANCE_COMPROBACompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CallZINT_RECIBE_BALANCE_COMPROBACompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ZINT_RECIBE_BALANCE_COMPROBAResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ZINT_RECIBE_BALANCE_COMPROBAResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591