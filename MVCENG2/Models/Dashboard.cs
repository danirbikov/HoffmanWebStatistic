using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCENG2.Models

{

    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class Dashboard
    {

        private DashboardTestReport testReportField;

        /// <remarks/>
        public DashboardTestReport TestReport
        {
            get
            {
                return this.testReportField;
            }
            set
            {
                this.testReportField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReport
    {

        private DashboardTestReportTestHeader testHeaderField;

        private DashboardTestReportResult[] resultField;

        private DashboardTestReportTestEnd testEndField;

        private ulong productionNumberField;

        /// <remarks/>
        public DashboardTestReportTestHeader TestHeader
        {
            get
            {
                return this.testHeaderField;
            }
            set
            {
                this.testHeaderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Result")]
        public DashboardTestReportResult[] Result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }

        /// <remarks/>
        public DashboardTestReportTestEnd TestEnd
        {
            get
            {
                return this.testEndField;
            }
            set
            {
                this.testEndField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong productionNumber
        {
            get
            {
                return this.productionNumberField;
            }
            set
            {
                this.productionNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportTestHeader
    {

        private ulong productionNumberField;

        private string vehicleIdentificationNumberField;

        private string testunitNameField;

        private string startOfTestField;

        private System.DateTime timeStampField;

        private DashboardTestReportTestHeaderResultTags resultTagsField;

        /// <remarks/>
        public ulong ProductionNumber
        {
            get
            {
                return this.productionNumberField;
            }
            set
            {
                this.productionNumberField = value;
            }
        }

        /// <remarks/>
        public string VehicleIdentificationNumber
        {
            get
            {
                return this.vehicleIdentificationNumberField;
            }
            set
            {
                this.vehicleIdentificationNumberField = value;
            }
        }

        /// <remarks/>
        public string TestunitName
        {
            get
            {
                return this.testunitNameField;
            }
            set
            {
                this.testunitNameField = value;
            }
        }

        /// <remarks/>
        public string StartOfTest
        {
            get
            {
                return this.startOfTestField;
            }
            set
            {
                this.startOfTestField = value;
            }
        }

        /// <remarks/>
        public System.DateTime TimeStamp
        {
            get
            {
                return this.timeStampField;
            }
            set
            {
                this.timeStampField = value;
            }
        }

        /// <remarks/>
        public DashboardTestReportTestHeaderResultTags ResultTags
        {
            get
            {
                return this.resultTagsField;
            }
            set
            {
                this.resultTagsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportTestHeaderResultTags
    {

        private ulong executionResultAdditionalDataVechicleDesignNumberField;

        private ulong executionResultAdditionalDataSAP_CodeField;

        private string testUnit_ConfigurationPrimaryLanguageField;

        private byte mDA_InformationMDA_NumberField;

        private string mDA_InformationMDA_Firmware_VersionField;

        private ulong sHOP_FLOOR_DATASAPCODEField;

        private string sHOP_FLOOR_DATAORDERTYPEField;

        private object sHOP_FLOOR_DATAMODCODField;

        private ulong sHOP_FLOOR_DATASAPSEQField;

        private string sHOP_FLOOR_DATALINE_DESCRField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ExecutionResultAdditionalData.VechicleDesignNumber")]
        public ulong ExecutionResultAdditionalDataVechicleDesignNumber
        {
            get
            {
                return this.executionResultAdditionalDataVechicleDesignNumberField;
            }
            set
            {
                this.executionResultAdditionalDataVechicleDesignNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ExecutionResultAdditionalData.SAP_Code")]
        public ulong ExecutionResultAdditionalDataSAP_Code
        {
            get
            {
                return this.executionResultAdditionalDataSAP_CodeField;
            }
            set
            {
                this.executionResultAdditionalDataSAP_CodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TestUnit_Configuration.PrimaryLanguage")]
        public string TestUnit_ConfigurationPrimaryLanguage
        {
            get
            {
                return this.testUnit_ConfigurationPrimaryLanguageField;
            }
            set
            {
                this.testUnit_ConfigurationPrimaryLanguageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MDA_Information.MDA_Number")]
        public byte MDA_InformationMDA_Number
        {
            get
            {
                return this.mDA_InformationMDA_NumberField;
            }
            set
            {
                this.mDA_InformationMDA_NumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MDA_Information.MDA_Firmware_Version")]
        public string MDA_InformationMDA_Firmware_Version
        {
            get
            {
                return this.mDA_InformationMDA_Firmware_VersionField;
            }
            set
            {
                this.mDA_InformationMDA_Firmware_VersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SHOP_FLOOR_DATA.SAPCODE")]
        public ulong SHOP_FLOOR_DATASAPCODE
        {
            get
            {
                return this.sHOP_FLOOR_DATASAPCODEField;
            }
            set
            {
                this.sHOP_FLOOR_DATASAPCODEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SHOP_FLOOR_DATA.ORDERTYPE")]
        public string SHOP_FLOOR_DATAORDERTYPE
        {
            get
            {
                return this.sHOP_FLOOR_DATAORDERTYPEField;
            }
            set
            {
                this.sHOP_FLOOR_DATAORDERTYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SHOP_FLOOR_DATA.MODCOD")]
        public object SHOP_FLOOR_DATAMODCOD
        {
            get
            {
                return this.sHOP_FLOOR_DATAMODCODField;
            }
            set
            {
                this.sHOP_FLOOR_DATAMODCODField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SHOP_FLOOR_DATA.SAPSEQ")]
        public ulong SHOP_FLOOR_DATASAPSEQ
        {
            get
            {
                return this.sHOP_FLOOR_DATASAPSEQField;
            }
            set
            {
                this.sHOP_FLOOR_DATASAPSEQField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SHOP_FLOOR_DATA.LINE_DESCR")]
        public string SHOP_FLOOR_DATALINE_DESCR
        {
            get
            {
                return this.sHOP_FLOOR_DATALINE_DESCRField;
            }
            set
            {
                this.sHOP_FLOOR_DATALINE_DESCRField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportResult
    {

        private string componentField;

        private DashboardTestReportResultComponentText[] componentTextField;

        private string itemField;

        private DashboardTestReportResultItemText[] itemTextField;

        private string resultTypeField;

        private DashboardTestReportResultResultText[] resultTextField;

        private DashboardTestReportResultError[] errorField;

        private string resultStateField;

        private DashboardTestReportResultType_Tolerance type_ToleranceField;

        private object type_ExecutionField;

        private DashboardTestReportResultType_State type_StateField;

        private DashboardTestReportResultDetailTags detailTagsField;

        /// <remarks/>
        public string Component
        {
            get
            {
                return this.componentField;
            }
            set
            {
                this.componentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ComponentText")]
        public DashboardTestReportResultComponentText[] ComponentText
        {
            get
            {
                return this.componentTextField;
            }
            set
            {
                this.componentTextField = value;
            }
        }

        /// <remarks/>
        public string Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemText")]
        public DashboardTestReportResultItemText[] ItemText
        {
            get
            {
                return this.itemTextField;
            }
            set
            {
                this.itemTextField = value;
            }
        }

        /// <remarks/>
        public string ResultType
        {
            get
            {
                return this.resultTypeField;
            }
            set
            {
                this.resultTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ResultText")]
        public DashboardTestReportResultResultText[] ResultText
        {
            get
            {
                return this.resultTextField;
            }
            set
            {
                this.resultTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Error")]
        public DashboardTestReportResultError[] Error
        {
            get
            {
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }

        /// <remarks/>
        public string ResultState
        {
            get
            {
                return this.resultStateField;
            }
            set
            {
                this.resultStateField = value;
            }
        }

        /// <remarks/>
        public DashboardTestReportResultType_Tolerance Type_Tolerance
        {
            get
            {
                return this.type_ToleranceField;
            }
            set
            {
                this.type_ToleranceField = value;
            }
        }

        /// <remarks/>
        public object Type_Execution
        {
            get
            {
                return this.type_ExecutionField;
            }
            set
            {
                this.type_ExecutionField = value;
            }
        }

        /// <remarks/>
        public DashboardTestReportResultType_State Type_State
        {
            get
            {
                return this.type_StateField;
            }
            set
            {
                this.type_StateField = value;
            }
        }

        /// <remarks/>
        public DashboardTestReportResultDetailTags DetailTags
        {
            get
            {
                return this.detailTagsField;
            }
            set
            {
                this.detailTagsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportResultComponentText
    {

        private string langField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportResultItemText
    {

        private string langField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportResultResultText
    {

        private string langField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportResultError
    {

        private string langField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportResultType_Tolerance
    {

        private decimal lowerLimitField;

        private byte upperLimitField;

        private decimal actualValueField;

        private string physicalUnitField;

        /// <remarks/>
        public decimal LowerLimit
        {
            get
            {
                return this.lowerLimitField;
            }
            set
            {
                this.lowerLimitField = value;
            }
        }

        /// <remarks/>
        public byte UpperLimit
        {
            get
            {
                return this.upperLimitField;
            }
            set
            {
                this.upperLimitField = value;
            }
        }

        /// <remarks/>
        public decimal ActualValue
        {
            get
            {
                return this.actualValueField;
            }
            set
            {
                this.actualValueField = value;
            }
        }

        /// <remarks/>
        public string PhysicalUnit
        {
            get
            {
                return this.physicalUnitField;
            }
            set
            {
                this.physicalUnitField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportResultType_State
    {

        private string actualStringField;

        private string referenceStringField;

        /// <remarks/>
        public string ActualString
        {
            get
            {
                return this.actualStringField;
            }
            set
            {
                this.actualStringField = value;
            }
        }

        /// <remarks/>
        public string ReferenceString
        {
            get
            {
                return this.referenceStringField;
            }
            set
            {
                this.referenceStringField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportResultDetailTags
    {

        private bool testInfoPrintOnlyOnFullField;

        private bool testInfoPrintOnlyOnFullFieldSpecified;

        private string readInformationSSHWNumberField;

        private string readInformationSSHWVersionNumberField;

        private string readInformationSSSWNumberField;

        private uint readInformationSSSWVersionNumberField;

        private bool readInformationSSSWVersionNumberFieldSpecified;

        private string readInformationVMSWNumberField;

        private string readInformationVMSWVersionNumberField;

        private string testInfoStartTimeField;

        private string testInfoEndTimeField;

        private string eLT_SECTIONOPCODEField;

        private string eLT_SECTIONWORKPLACEField;

        private uint eLT_SECTIONOPERATORField;

        private bool eLT_SECTIONOPERATORFieldSpecified;

        private System.DateTime eLT_SECTIONOPSTARTDATEField;

        private bool eLT_SECTIONOPSTARTDATEFieldSpecified;

        private System.DateTime eLT_SECTIONOPDATEField;

        private bool eLT_SECTIONOPDATEFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TestInfo.PrintOnlyOnFull")]
        public bool TestInfoPrintOnlyOnFull
        {
            get
            {
                return this.testInfoPrintOnlyOnFullField;
            }
            set
            {
                this.testInfoPrintOnlyOnFullField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TestInfoPrintOnlyOnFullSpecified
        {
            get
            {
                return this.testInfoPrintOnlyOnFullFieldSpecified;
            }
            set
            {
                this.testInfoPrintOnlyOnFullFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReadInformation.SSHWNumber")]
        public string ReadInformationSSHWNumber
        {
            get
            {
                return this.readInformationSSHWNumberField;
            }
            set
            {
                this.readInformationSSHWNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReadInformation.SSHWVersionNumber")]
        public string ReadInformationSSHWVersionNumber
        {
            get
            {
                return this.readInformationSSHWVersionNumberField;
            }
            set
            {
                this.readInformationSSHWVersionNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReadInformation.SSSWNumber")]
        public string ReadInformationSSSWNumber
        {
            get
            {
                return this.readInformationSSSWNumberField;
            }
            set
            {
                this.readInformationSSSWNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReadInformation.SSSWVersionNumber")]
        public uint ReadInformationSSSWVersionNumber
        {
            get
            {
                return this.readInformationSSSWVersionNumberField;
            }
            set
            {
                this.readInformationSSSWVersionNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReadInformationSSSWVersionNumberSpecified
        {
            get
            {
                return this.readInformationSSSWVersionNumberFieldSpecified;
            }
            set
            {
                this.readInformationSSSWVersionNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReadInformation.VMSWNumber")]
        public string ReadInformationVMSWNumber
        {
            get
            {
                return this.readInformationVMSWNumberField;
            }
            set
            {
                this.readInformationVMSWNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ReadInformation.VMSWVersionNumber")]
        public string ReadInformationVMSWVersionNumber
        {
            get
            {
                return this.readInformationVMSWVersionNumberField;
            }
            set
            {
                this.readInformationVMSWVersionNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TestInfo.StartTime")]
        public string TestInfoStartTime
        {
            get
            {
                return this.testInfoStartTimeField;
            }
            set
            {
                this.testInfoStartTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TestInfo.EndTime")]
        public string TestInfoEndTime
        {
            get
            {
                return this.testInfoEndTimeField;
            }
            set
            {
                this.testInfoEndTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ELT_SECTION.OPCODE")]
        public string ELT_SECTIONOPCODE
        {
            get
            {
                return this.eLT_SECTIONOPCODEField;
            }
            set
            {
                this.eLT_SECTIONOPCODEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ELT_SECTION.WORKPLACE")]
        public string ELT_SECTIONWORKPLACE
        {
            get
            {
                return this.eLT_SECTIONWORKPLACEField;
            }
            set
            {
                this.eLT_SECTIONWORKPLACEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ELT_SECTION.OPERATOR")]
        public uint ELT_SECTIONOPERATOR
        {
            get
            {
                return this.eLT_SECTIONOPERATORField;
            }
            set
            {
                this.eLT_SECTIONOPERATORField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ELT_SECTIONOPERATORSpecified
        {
            get
            {
                return this.eLT_SECTIONOPERATORFieldSpecified;
            }
            set
            {
                this.eLT_SECTIONOPERATORFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ELT_SECTION.OPSTARTDATE")]
        public System.DateTime ELT_SECTIONOPSTARTDATE
        {
            get
            {
                return this.eLT_SECTIONOPSTARTDATEField;
            }
            set
            {
                this.eLT_SECTIONOPSTARTDATEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ELT_SECTIONOPSTARTDATESpecified
        {
            get
            {
                return this.eLT_SECTIONOPSTARTDATEFieldSpecified;
            }
            set
            {
                this.eLT_SECTIONOPSTARTDATEFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ELT_SECTION.OPDATE")]
        public System.DateTime ELT_SECTIONOPDATE
        {
            get
            {
                return this.eLT_SECTIONOPDATEField;
            }
            set
            {
                this.eLT_SECTIONOPDATEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ELT_SECTIONOPDATESpecified
        {
            get
            {
                return this.eLT_SECTIONOPDATEFieldSpecified;
            }
            set
            {
                this.eLT_SECTIONOPDATEFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DashboardTestReportTestEnd
    {

        private string testResultStateField;

        private ushort resultCounterField;

        private byte notOkResultsField;

        private string endOfReportField;

        /// <remarks/>
        public string TestResultState
        {
            get
            {
                return this.testResultStateField;
            }
            set
            {
                this.testResultStateField = value;
            }
        }

        /// <remarks/>
        public ushort ResultCounter
        {
            get
            {
                return this.resultCounterField;
            }
            set
            {
                this.resultCounterField = value;
            }
        }

        /// <remarks/>
        public byte NotOkResults
        {
            get
            {
                return this.notOkResultsField;
            }
            set
            {
                this.notOkResultsField = value;
            }
        }

        /// <remarks/>
        public string EndOfReport
        {
            get
            {
                return this.endOfReportField;
            }
            set
            {
                this.endOfReportField = value;
            }
        }
    }


}
