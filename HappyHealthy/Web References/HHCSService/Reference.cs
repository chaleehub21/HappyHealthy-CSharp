﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace HappyHealthyCSharp.HHCSService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="HHCSServiceSoap", Namespace="http://tempuri.org/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(object[]))]
    public partial class HHCSService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback RegisterOperationCompleted;
        
        private System.Threading.SendOrPostCallback TestConnectionOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFoodDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback SynchonizeDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback ClassXMLGenerateTestOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public HHCSService() {
            this.Url = "http://172.20.97.219/HHCSService/HHCSService.asmx";
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
        public event RegisterCompletedEventHandler RegisterCompleted;
        
        /// <remarks/>
        public event TestConnectionCompletedEventHandler TestConnectionCompleted;
        
        /// <remarks/>
        public event GetFoodDataCompletedEventHandler GetFoodDataCompleted;
        
        /// <remarks/>
        public event GetDataCompletedEventHandler GetDataCompleted;
        
        /// <remarks/>
        public event SynchonizeDataCompletedEventHandler SynchonizeDataCompleted;
        
        /// <remarks/>
        public event ClassXMLGenerateTestCompletedEventHandler ClassXMLGenerateTestCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Register", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public object[] Register(string ud_email, string ud_pass) {
            object[] results = this.Invoke("Register", new object[] {
                        ud_email,
                        ud_pass});
            return ((object[])(results[0]));
        }
        
        /// <remarks/>
        public void RegisterAsync(string ud_email, string ud_pass) {
            this.RegisterAsync(ud_email, ud_pass, null);
        }
        
        /// <remarks/>
        public void RegisterAsync(string ud_email, string ud_pass, object userState) {
            if ((this.RegisterOperationCompleted == null)) {
                this.RegisterOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRegisterOperationCompleted);
            }
            this.InvokeAsync("Register", new object[] {
                        ud_email,
                        ud_pass}, this.RegisterOperationCompleted, userState);
        }
        
        private void OnRegisterOperationCompleted(object arg) {
            if ((this.RegisterCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RegisterCompleted(this, new RegisterCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/TestConnection", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool TestConnection() {
            object[] results = this.Invoke("TestConnection", new object[0]);
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void TestConnectionAsync() {
            this.TestConnectionAsync(null);
        }
        
        /// <remarks/>
        public void TestConnectionAsync(object userState) {
            if ((this.TestConnectionOperationCompleted == null)) {
                this.TestConnectionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestConnectionOperationCompleted);
            }
            this.InvokeAsync("TestConnection", new object[0], this.TestConnectionOperationCompleted, userState);
        }
        
        private void OnTestConnectionOperationCompleted(object arg) {
            if ((this.TestConnectionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TestConnectionCompleted(this, new TestConnectionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetFoodData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetFoodData() {
            object[] results = this.Invoke("GetFoodData", new object[0]);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetFoodDataAsync() {
            this.GetFoodDataAsync(null);
        }
        
        /// <remarks/>
        public void GetFoodDataAsync(object userState) {
            if ((this.GetFoodDataOperationCompleted == null)) {
                this.GetFoodDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFoodDataOperationCompleted);
            }
            this.InvokeAsync("GetFoodData", new object[0], this.GetFoodDataOperationCompleted, userState);
        }
        
        private void OnGetFoodDataOperationCompleted(object arg) {
            if ((this.GetFoodDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFoodDataCompleted(this, new GetFoodDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet GetData(string tableName, string email, string password) {
            object[] results = this.Invoke("GetData", new object[] {
                        tableName,
                        email,
                        password});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void GetDataAsync(string tableName, string email, string password) {
            this.GetDataAsync(tableName, email, password, null);
        }
        
        /// <remarks/>
        public void GetDataAsync(string tableName, string email, string password, object userState) {
            if ((this.GetDataOperationCompleted == null)) {
                this.GetDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDataOperationCompleted);
            }
            this.InvokeAsync("GetData", new object[] {
                        tableName,
                        email,
                        password}, this.GetDataOperationCompleted, userState);
        }
        
        private void OnGetDataOperationCompleted(object arg) {
            if ((this.GetDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDataCompleted(this, new GetDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SynchonizeData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] SynchonizeData(string email, string password, TEMP_DiabetesTABLE[] tempDiabetes, TEMP_KidneyTABLE[] tempKidney, TEMP_PressureTABLE[] tempPressure) {
            object[] results = this.Invoke("SynchonizeData", new object[] {
                        email,
                        password,
                        tempDiabetes,
                        tempKidney,
                        tempPressure});
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public void SynchonizeDataAsync(string email, string password, TEMP_DiabetesTABLE[] tempDiabetes, TEMP_KidneyTABLE[] tempKidney, TEMP_PressureTABLE[] tempPressure) {
            this.SynchonizeDataAsync(email, password, tempDiabetes, tempKidney, tempPressure, null);
        }
        
        /// <remarks/>
        public void SynchonizeDataAsync(string email, string password, TEMP_DiabetesTABLE[] tempDiabetes, TEMP_KidneyTABLE[] tempKidney, TEMP_PressureTABLE[] tempPressure, object userState) {
            if ((this.SynchonizeDataOperationCompleted == null)) {
                this.SynchonizeDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSynchonizeDataOperationCompleted);
            }
            this.InvokeAsync("SynchonizeData", new object[] {
                        email,
                        password,
                        tempDiabetes,
                        tempKidney,
                        tempPressure}, this.SynchonizeDataOperationCompleted, userState);
        }
        
        private void OnSynchonizeDataOperationCompleted(object arg) {
            if ((this.SynchonizeDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SynchonizeDataCompleted(this, new SynchonizeDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ClassXMLGenerateTest", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ClassXMLGenerateTest(TEMP_DiabetesTABLE data, TEMP_KidneyTABLE data2, TEMP_PressureTABLE data3) {
            object[] results = this.Invoke("ClassXMLGenerateTest", new object[] {
                        data,
                        data2,
                        data3});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ClassXMLGenerateTestAsync(TEMP_DiabetesTABLE data, TEMP_KidneyTABLE data2, TEMP_PressureTABLE data3) {
            this.ClassXMLGenerateTestAsync(data, data2, data3, null);
        }
        
        /// <remarks/>
        public void ClassXMLGenerateTestAsync(TEMP_DiabetesTABLE data, TEMP_KidneyTABLE data2, TEMP_PressureTABLE data3, object userState) {
            if ((this.ClassXMLGenerateTestOperationCompleted == null)) {
                this.ClassXMLGenerateTestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnClassXMLGenerateTestOperationCompleted);
            }
            this.InvokeAsync("ClassXMLGenerateTest", new object[] {
                        data,
                        data2,
                        data3}, this.ClassXMLGenerateTestOperationCompleted, userState);
        }
        
        private void OnClassXMLGenerateTestOperationCompleted(object arg) {
            if ((this.ClassXMLGenerateTestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ClassXMLGenerateTestCompleted(this, new ClassXMLGenerateTestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class TEMP_PressureTABLE {
        
        private int bp_id_pointerField;
        
        private System.DateTime bp_time_newField;
        
        private System.DateTime bp_time_oldField;
        
        private string bp_time_string_newField;
        
        private int bp_up_newField;
        
        private int bp_up_oldField;
        
        private int bp_lo_newField;
        
        private int bp_lo_oldField;
        
        private int bp_hr_newField;
        
        private int bp_hr_oldField;
        
        private int bp_up_lvl_newField;
        
        private int bp_up_lvl_oldField;
        
        private int bp_lo_lvl_newField;
        
        private int bp_lo_lvl_oldField;
        
        private int bp_hr_lvl_newField;
        
        private int bp_hr_lvl_oldField;
        
        private string modeField;
        
        /// <remarks/>
        public int bp_id_pointer {
            get {
                return this.bp_id_pointerField;
            }
            set {
                this.bp_id_pointerField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime bp_time_new {
            get {
                return this.bp_time_newField;
            }
            set {
                this.bp_time_newField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime bp_time_old {
            get {
                return this.bp_time_oldField;
            }
            set {
                this.bp_time_oldField = value;
            }
        }
        
        /// <remarks/>
        public string bp_time_string_new {
            get {
                return this.bp_time_string_newField;
            }
            set {
                this.bp_time_string_newField = value;
            }
        }
        
        /// <remarks/>
        public int bp_up_new {
            get {
                return this.bp_up_newField;
            }
            set {
                this.bp_up_newField = value;
            }
        }
        
        /// <remarks/>
        public int bp_up_old {
            get {
                return this.bp_up_oldField;
            }
            set {
                this.bp_up_oldField = value;
            }
        }
        
        /// <remarks/>
        public int bp_lo_new {
            get {
                return this.bp_lo_newField;
            }
            set {
                this.bp_lo_newField = value;
            }
        }
        
        /// <remarks/>
        public int bp_lo_old {
            get {
                return this.bp_lo_oldField;
            }
            set {
                this.bp_lo_oldField = value;
            }
        }
        
        /// <remarks/>
        public int bp_hr_new {
            get {
                return this.bp_hr_newField;
            }
            set {
                this.bp_hr_newField = value;
            }
        }
        
        /// <remarks/>
        public int bp_hr_old {
            get {
                return this.bp_hr_oldField;
            }
            set {
                this.bp_hr_oldField = value;
            }
        }
        
        /// <remarks/>
        public int bp_up_lvl_new {
            get {
                return this.bp_up_lvl_newField;
            }
            set {
                this.bp_up_lvl_newField = value;
            }
        }
        
        /// <remarks/>
        public int bp_up_lvl_old {
            get {
                return this.bp_up_lvl_oldField;
            }
            set {
                this.bp_up_lvl_oldField = value;
            }
        }
        
        /// <remarks/>
        public int bp_lo_lvl_new {
            get {
                return this.bp_lo_lvl_newField;
            }
            set {
                this.bp_lo_lvl_newField = value;
            }
        }
        
        /// <remarks/>
        public int bp_lo_lvl_old {
            get {
                return this.bp_lo_lvl_oldField;
            }
            set {
                this.bp_lo_lvl_oldField = value;
            }
        }
        
        /// <remarks/>
        public int bp_hr_lvl_new {
            get {
                return this.bp_hr_lvl_newField;
            }
            set {
                this.bp_hr_lvl_newField = value;
            }
        }
        
        /// <remarks/>
        public int bp_hr_lvl_old {
            get {
                return this.bp_hr_lvl_oldField;
            }
            set {
                this.bp_hr_lvl_oldField = value;
            }
        }
        
        /// <remarks/>
        public string mode {
            get {
                return this.modeField;
            }
            set {
                this.modeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class TEMP_KidneyTABLE {
        
        private int ckd_id_pointerField;
        
        private System.DateTime ckd_time_newField;
        
        private System.DateTime ckd_time_oldField;
        
        private string ckd_time_string_newField;
        
        private double ckd_gfr_newField;
        
        private double ckd_gfr_oldField;
        
        private int ckd_gfr_level_newField;
        
        private int ckd_gfr_level_oldField;
        
        private double ckd_creatinine_newField;
        
        private double ckd_creatinine_oldField;
        
        private double ckd_bun_newField;
        
        private double ckd_bun_oldField;
        
        private double ckd_sodium_newField;
        
        private double ckd_sodium_oldField;
        
        private double ckd_potassium_newField;
        
        private double ckd_potassium_oldField;
        
        private double ckd_albumin_blood_newField;
        
        private double ckd_albumin_blood_oldField;
        
        private double ckd_albumin_urine_newField;
        
        private double ckd_albumin_urine_oldField;
        
        private double ckd_phosphorus_blood_newField;
        
        private double ckd_phosphorus_blood_oldField;
        
        private string modeField;
        
        /// <remarks/>
        public int ckd_id_pointer {
            get {
                return this.ckd_id_pointerField;
            }
            set {
                this.ckd_id_pointerField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime ckd_time_new {
            get {
                return this.ckd_time_newField;
            }
            set {
                this.ckd_time_newField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime ckd_time_old {
            get {
                return this.ckd_time_oldField;
            }
            set {
                this.ckd_time_oldField = value;
            }
        }
        
        /// <remarks/>
        public string ckd_time_string_new {
            get {
                return this.ckd_time_string_newField;
            }
            set {
                this.ckd_time_string_newField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_gfr_new {
            get {
                return this.ckd_gfr_newField;
            }
            set {
                this.ckd_gfr_newField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_gfr_old {
            get {
                return this.ckd_gfr_oldField;
            }
            set {
                this.ckd_gfr_oldField = value;
            }
        }
        
        /// <remarks/>
        public int ckd_gfr_level_new {
            get {
                return this.ckd_gfr_level_newField;
            }
            set {
                this.ckd_gfr_level_newField = value;
            }
        }
        
        /// <remarks/>
        public int ckd_gfr_level_old {
            get {
                return this.ckd_gfr_level_oldField;
            }
            set {
                this.ckd_gfr_level_oldField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_creatinine_new {
            get {
                return this.ckd_creatinine_newField;
            }
            set {
                this.ckd_creatinine_newField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_creatinine_old {
            get {
                return this.ckd_creatinine_oldField;
            }
            set {
                this.ckd_creatinine_oldField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_bun_new {
            get {
                return this.ckd_bun_newField;
            }
            set {
                this.ckd_bun_newField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_bun_old {
            get {
                return this.ckd_bun_oldField;
            }
            set {
                this.ckd_bun_oldField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_sodium_new {
            get {
                return this.ckd_sodium_newField;
            }
            set {
                this.ckd_sodium_newField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_sodium_old {
            get {
                return this.ckd_sodium_oldField;
            }
            set {
                this.ckd_sodium_oldField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_potassium_new {
            get {
                return this.ckd_potassium_newField;
            }
            set {
                this.ckd_potassium_newField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_potassium_old {
            get {
                return this.ckd_potassium_oldField;
            }
            set {
                this.ckd_potassium_oldField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_albumin_blood_new {
            get {
                return this.ckd_albumin_blood_newField;
            }
            set {
                this.ckd_albumin_blood_newField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_albumin_blood_old {
            get {
                return this.ckd_albumin_blood_oldField;
            }
            set {
                this.ckd_albumin_blood_oldField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_albumin_urine_new {
            get {
                return this.ckd_albumin_urine_newField;
            }
            set {
                this.ckd_albumin_urine_newField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_albumin_urine_old {
            get {
                return this.ckd_albumin_urine_oldField;
            }
            set {
                this.ckd_albumin_urine_oldField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_phosphorus_blood_new {
            get {
                return this.ckd_phosphorus_blood_newField;
            }
            set {
                this.ckd_phosphorus_blood_newField = value;
            }
        }
        
        /// <remarks/>
        public double ckd_phosphorus_blood_old {
            get {
                return this.ckd_phosphorus_blood_oldField;
            }
            set {
                this.ckd_phosphorus_blood_oldField = value;
            }
        }
        
        /// <remarks/>
        public string mode {
            get {
                return this.modeField;
            }
            set {
                this.modeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2612.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class TEMP_DiabetesTABLE {
        
        private int fbs_id_pointerField;
        
        private System.DateTime fbs_time_newField;
        
        private string fbs_time_string_newField;
        
        private System.DateTime fbs_time_oldField;
        
        private int fbs_fbs_newField;
        
        private int fbs_fbs_oldField;
        
        private int fbs_fbs_lvl_newField;
        
        private int fbs_fbs_lvl_oldField;
        
        private string modeField;
        
        /// <remarks/>
        public int fbs_id_pointer {
            get {
                return this.fbs_id_pointerField;
            }
            set {
                this.fbs_id_pointerField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime fbs_time_new {
            get {
                return this.fbs_time_newField;
            }
            set {
                this.fbs_time_newField = value;
            }
        }
        
        /// <remarks/>
        public string fbs_time_string_new {
            get {
                return this.fbs_time_string_newField;
            }
            set {
                this.fbs_time_string_newField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime fbs_time_old {
            get {
                return this.fbs_time_oldField;
            }
            set {
                this.fbs_time_oldField = value;
            }
        }
        
        /// <remarks/>
        public int fbs_fbs_new {
            get {
                return this.fbs_fbs_newField;
            }
            set {
                this.fbs_fbs_newField = value;
            }
        }
        
        /// <remarks/>
        public int fbs_fbs_old {
            get {
                return this.fbs_fbs_oldField;
            }
            set {
                this.fbs_fbs_oldField = value;
            }
        }
        
        /// <remarks/>
        public int fbs_fbs_lvl_new {
            get {
                return this.fbs_fbs_lvl_newField;
            }
            set {
                this.fbs_fbs_lvl_newField = value;
            }
        }
        
        /// <remarks/>
        public int fbs_fbs_lvl_old {
            get {
                return this.fbs_fbs_lvl_oldField;
            }
            set {
                this.fbs_fbs_lvl_oldField = value;
            }
        }
        
        /// <remarks/>
        public string mode {
            get {
                return this.modeField;
            }
            set {
                this.modeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void RegisterCompletedEventHandler(object sender, RegisterCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RegisterCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RegisterCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public object[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((object[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void TestConnectionCompletedEventHandler(object sender, TestConnectionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TestConnectionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TestConnectionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void GetFoodDataCompletedEventHandler(object sender, GetFoodDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFoodDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFoodDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void GetDataCompletedEventHandler(object sender, GetDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void SynchonizeDataCompletedEventHandler(object sender, SynchonizeDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SynchonizeDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SynchonizeDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void ClassXMLGenerateTestCompletedEventHandler(object sender, ClassXMLGenerateTestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ClassXMLGenerateTestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ClassXMLGenerateTestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591