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

namespace Presentacion.wsBalanceComprobacion {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsBalanceComprobacionSAPSoap", Namespace="http://tempuri.org/")]
    public partial class wsBalanceComprobacionSAP : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback uwsBalanceComprobacionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsBalanceComprobacionSAP() {
            this.Url = global::Presentacion.Properties.Settings.Default.Presentacion_wsBalanceComprobacion_wsBalanceComprobacionSAP;
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
        public string[] uwsBalanceComprobacion(tBalanceComprobacionCabecera t_Cabecera, tBalanceComprobacionPosicion[] t_Posicion) {
            object[] results = this.Invoke("uwsBalanceComprobacion", new object[] {
                        t_Cabecera,
                        t_Posicion});
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public void uwsBalanceComprobacionAsync(tBalanceComprobacionCabecera t_Cabecera, tBalanceComprobacionPosicion[] t_Posicion) {
            this.uwsBalanceComprobacionAsync(t_Cabecera, t_Posicion, null);
        }
        
        /// <remarks/>
        public void uwsBalanceComprobacionAsync(tBalanceComprobacionCabecera t_Cabecera, tBalanceComprobacionPosicion[] t_Posicion, object userState) {
            if ((this.uwsBalanceComprobacionOperationCompleted == null)) {
                this.uwsBalanceComprobacionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnuwsBalanceComprobacionOperationCompleted);
            }
            this.InvokeAsync("uwsBalanceComprobacion", new object[] {
                        t_Cabecera,
                        t_Posicion}, this.uwsBalanceComprobacionOperationCompleted, userState);
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class tBalanceComprobacionCabecera {
        
        private string lstr_ledgerField;
        
        private string lstr_vistaField;
        
        private string lstr_versionField;
        
        private string lstr_ejercicioField;
        
        private string lstr_periodoField;
        
        private string lstr_unid_consolField;
        
        private string lstr_plan_posField;
        
        /// <remarks/>
        public string Lstr_ledger {
            get {
                return this.lstr_ledgerField;
            }
            set {
                this.lstr_ledgerField = value;
            }
        }
        
        /// <remarks/>
        public string Lstr_vista {
            get {
                return this.lstr_vistaField;
            }
            set {
                this.lstr_vistaField = value;
            }
        }
        
        /// <remarks/>
        public string Lstr_version {
            get {
                return this.lstr_versionField;
            }
            set {
                this.lstr_versionField = value;
            }
        }
        
        /// <remarks/>
        public string Lstr_ejercicio {
            get {
                return this.lstr_ejercicioField;
            }
            set {
                this.lstr_ejercicioField = value;
            }
        }
        
        /// <remarks/>
        public string Lstr_periodo {
            get {
                return this.lstr_periodoField;
            }
            set {
                this.lstr_periodoField = value;
            }
        }
        
        /// <remarks/>
        public string Lstr_unid_consol {
            get {
                return this.lstr_unid_consolField;
            }
            set {
                this.lstr_unid_consolField = value;
            }
        }
        
        /// <remarks/>
        public string Lstr_plan_pos {
            get {
                return this.lstr_plan_posField;
            }
            set {
                this.lstr_plan_posField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class tBalanceComprobacionPosicion {
        
        private string lstr_posicionField;
        
        private string lstr_subposicionField;
        
        private string lstr_unid_asociaField;
        
        private string lstr_monedaField;
        
        private string lstr_signoField;
        
        private decimal ldec_valor_mlField;
        
        private decimal ldec_valor_mtField;
        
        private decimal ldec_valor_acField;
        
        /// <remarks/>
        public string Lstr_posicion {
            get {
                return this.lstr_posicionField;
            }
            set {
                this.lstr_posicionField = value;
            }
        }
        
        /// <remarks/>
        public string Lstr_subposicion {
            get {
                return this.lstr_subposicionField;
            }
            set {
                this.lstr_subposicionField = value;
            }
        }
        
        /// <remarks/>
        public string Lstr_unid_asocia {
            get {
                return this.lstr_unid_asociaField;
            }
            set {
                this.lstr_unid_asociaField = value;
            }
        }
        
        /// <remarks/>
        public string Lstr_moneda {
            get {
                return this.lstr_monedaField;
            }
            set {
                this.lstr_monedaField = value;
            }
        }
        
        /// <remarks/>
        public string Lstr_signo {
            get {
                return this.lstr_signoField;
            }
            set {
                this.lstr_signoField = value;
            }
        }
        
        /// <remarks/>
        public decimal Ldec_valor_ml {
            get {
                return this.ldec_valor_mlField;
            }
            set {
                this.ldec_valor_mlField = value;
            }
        }
        
        /// <remarks/>
        public decimal Ldec_valor_mt {
            get {
                return this.ldec_valor_mtField;
            }
            set {
                this.ldec_valor_mtField = value;
            }
        }
        
        /// <remarks/>
        public decimal Ldec_valor_ac {
            get {
                return this.ldec_valor_acField;
            }
            set {
                this.ldec_valor_acField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    public delegate void uwsBalanceComprobacionCompletedEventHandler(object sender, uwsBalanceComprobacionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.34209")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class uwsBalanceComprobacionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal uwsBalanceComprobacionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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