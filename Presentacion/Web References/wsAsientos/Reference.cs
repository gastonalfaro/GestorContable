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

namespace Presentacion.wsAsientos {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServicioContableSoap", Namespace="http://tempuri.org/")]
    public partial class ServicioContable : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback EnviarAsientosOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ServicioContable() {
            this.Url = global::Presentacion.Properties.Settings.Default.Presentacion_wsAsientos_ServicioContable;
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
        public event EnviarAsientosCompletedEventHandler EnviarAsientosCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EnviarAsientos", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] EnviarAsientos(ZfiAsiento[] tabla_asientos, string str_Test) {
            object[] results = this.Invoke("EnviarAsientos", new object[] {
                        tabla_asientos,
                        str_Test});
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public void EnviarAsientosAsync(ZfiAsiento[] tabla_asientos, string str_Test) {
            this.EnviarAsientosAsync(tabla_asientos, str_Test, null);
        }
        
        /// <remarks/>
        public void EnviarAsientosAsync(ZfiAsiento[] tabla_asientos, string str_Test, object userState) {
            if ((this.EnviarAsientosOperationCompleted == null)) {
                this.EnviarAsientosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEnviarAsientosOperationCompleted);
            }
            this.InvokeAsync("EnviarAsientos", new object[] {
                        tabla_asientos,
                        str_Test}, this.EnviarAsientosOperationCompleted, userState);
        }
        
        private void OnEnviarAsientosOperationCompleted(object arg) {
            if ((this.EnviarAsientosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EnviarAsientosCompleted(this, new EnviarAsientosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:soap:functions:mc-style")]
    public partial class ZfiAsiento {
        
        private string mandtField;
        
        private string bldatField;
        
        private string blartField;
        
        private string bukrsField;
        
        private string budatField;
        
        private string waersField;
        
        private decimal kursfField;
        
        private string xblnrField;
        
        private string xref1HdField;
        
        private string xref2HdField;
        
        private string buzeiField;
        
        private string bktxtField;
        
        private string bschlField;
        
        private string hkontField;
        
        private string umskzField;
        
        private decimal wrbtrField;
        
        private decimal dmbe2Field;
        
        private string mwskzField;
        
        private string xmwstField;
        
        private string zfbdtField;
        
        private string zuonrField;
        
        private string sgtxtField;
        
        private string hbkidField;
        
        private string zlschField;
        
        private string kostlField;
        
        private string prctrField;
        
        private string aufnrField;
        
        private string projkField;
        
        private string fipexField;
        
        private string fistlField;
        
        private string measureField;
        
        private string geberField;
        
        private string werksField;
        
        private string valutField;
        
        private string kblnrField;
        
        private string kblposField;
        
        private string rcompField;
        
        private string xref2Field;
        
        private string xref3Field;
        
        private string fkberField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Mandt {
            get {
                return this.mandtField;
            }
            set {
                this.mandtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Bldat {
            get {
                return this.bldatField;
            }
            set {
                this.bldatField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Blart {
            get {
                return this.blartField;
            }
            set {
                this.blartField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Bukrs {
            get {
                return this.bukrsField;
            }
            set {
                this.bukrsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Budat {
            get {
                return this.budatField;
            }
            set {
                this.budatField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Waers {
            get {
                return this.waersField;
            }
            set {
                this.waersField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Kursf {
            get {
                return this.kursfField;
            }
            set {
                this.kursfField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Xblnr {
            get {
                return this.xblnrField;
            }
            set {
                this.xblnrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Xref1Hd {
            get {
                return this.xref1HdField;
            }
            set {
                this.xref1HdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Xref2Hd {
            get {
                return this.xref2HdField;
            }
            set {
                this.xref2HdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Buzei {
            get {
                return this.buzeiField;
            }
            set {
                this.buzeiField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Bktxt {
            get {
                return this.bktxtField;
            }
            set {
                this.bktxtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Bschl {
            get {
                return this.bschlField;
            }
            set {
                this.bschlField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Hkont {
            get {
                return this.hkontField;
            }
            set {
                this.hkontField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Umskz {
            get {
                return this.umskzField;
            }
            set {
                this.umskzField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Wrbtr {
            get {
                return this.wrbtrField;
            }
            set {
                this.wrbtrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal Dmbe2 {
            get {
                return this.dmbe2Field;
            }
            set {
                this.dmbe2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Mwskz {
            get {
                return this.mwskzField;
            }
            set {
                this.mwskzField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Xmwst {
            get {
                return this.xmwstField;
            }
            set {
                this.xmwstField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Zfbdt {
            get {
                return this.zfbdtField;
            }
            set {
                this.zfbdtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Zuonr {
            get {
                return this.zuonrField;
            }
            set {
                this.zuonrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Sgtxt {
            get {
                return this.sgtxtField;
            }
            set {
                this.sgtxtField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Hbkid {
            get {
                return this.hbkidField;
            }
            set {
                this.hbkidField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Zlsch {
            get {
                return this.zlschField;
            }
            set {
                this.zlschField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Kostl {
            get {
                return this.kostlField;
            }
            set {
                this.kostlField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Prctr {
            get {
                return this.prctrField;
            }
            set {
                this.prctrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Aufnr {
            get {
                return this.aufnrField;
            }
            set {
                this.aufnrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Projk {
            get {
                return this.projkField;
            }
            set {
                this.projkField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Fipex {
            get {
                return this.fipexField;
            }
            set {
                this.fipexField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Fistl {
            get {
                return this.fistlField;
            }
            set {
                this.fistlField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Measure {
            get {
                return this.measureField;
            }
            set {
                this.measureField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Geber {
            get {
                return this.geberField;
            }
            set {
                this.geberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Werks {
            get {
                return this.werksField;
            }
            set {
                this.werksField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Valut {
            get {
                return this.valutField;
            }
            set {
                this.valutField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Kblnr {
            get {
                return this.kblnrField;
            }
            set {
                this.kblnrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Kblpos {
            get {
                return this.kblposField;
            }
            set {
                this.kblposField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Rcomp {
            get {
                return this.rcompField;
            }
            set {
                this.rcompField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Xref2 {
            get {
                return this.xref2Field;
            }
            set {
                this.xref2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Xref3 {
            get {
                return this.xref3Field;
            }
            set {
                this.xref3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Fkber {
            get {
                return this.fkberField;
            }
            set {
                this.fkberField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    public delegate void EnviarAsientosCompletedEventHandler(object sender, EnviarAsientosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.3752.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EnviarAsientosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EnviarAsientosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591