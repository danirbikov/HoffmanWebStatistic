using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingerAPI.Models

{

    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
    public class Dashboard
    {

        private DashboardTestReport testReportField;

        /// <remarks/>
        public DashboardTestReport TestReport
        {
            get
            {
                return testReportField;
            }
            set
            {
                testReportField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
                return testHeaderField;
            }
            set
            {
                testHeaderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Result")]
        public DashboardTestReportResult[] Result
        {
            get
            {
                return resultField;
            }
            set
            {
                resultField = value;
            }
        }

        /// <remarks/>
        public DashboardTestReportTestEnd TestEnd
        {
            get
            {
                return testEndField;
            }
            set
            {
                testEndField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute()]
        public ulong productionNumber
        {
            get
            {
                return productionNumberField;
            }
            set
            {
                productionNumberField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class DashboardTestReportTestHeader
    {

        private ulong productionNumberField;

        private string vehicleIdentificationNumberField;

        private string testunitNameField;

        private string startOfTestField;

        private DateTime timeStampField;

        private DashboardTestReportTestHeaderResultTags resultTagsField;

        /// <remarks/>
        public ulong ProductionNumber
        {
            get
            {
                return productionNumberField;
            }
            set
            {
                productionNumberField = value;
            }
        }

        /// <remarks/>
        public string VehicleIdentificationNumber
        {
            get
            {
                return vehicleIdentificationNumberField;
            }
            set
            {
                vehicleIdentificationNumberField = value;
            }
        }

        /// <remarks/>
        public string TestunitName
        {
            get
            {
                return testunitNameField;
            }
            set
            {
                testunitNameField = value;
            }
        }

        /// <remarks/>
        public string StartOfTest
        {
            get
            {
                return startOfTestField;
            }
            set
            {
                startOfTestField = value;
            }
        }

        /// <remarks/>
        public DateTime TimeStamp
        {
            get
            {
                return timeStampField;
            }
            set
            {
                timeStampField = value;
            }
        }

        /// <remarks/>
        public DashboardTestReportTestHeaderResultTags ResultTags
        {
            get
            {
                return resultTagsField;
            }
            set
            {
                resultTagsField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
        [System.Xml.Serialization.XmlElement("ExecutionResultAdditionalData.VechicleDesignNumber")]
        public ulong ExecutionResultAdditionalDataVechicleDesignNumber
        {
            get
            {
                return executionResultAdditionalDataVechicleDesignNumberField;
            }
            set
            {
                executionResultAdditionalDataVechicleDesignNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ExecutionResultAdditionalData.SAP_Code")]
        public ulong ExecutionResultAdditionalDataSAP_Code
        {
            get
            {
                return executionResultAdditionalDataSAP_CodeField;
            }
            set
            {
                executionResultAdditionalDataSAP_CodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("TestUnit_Configuration.PrimaryLanguage")]
        public string TestUnit_ConfigurationPrimaryLanguage
        {
            get
            {
                return testUnit_ConfigurationPrimaryLanguageField;
            }
            set
            {
                testUnit_ConfigurationPrimaryLanguageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("MDA_Information.MDA_Number")]
        public byte MDA_InformationMDA_Number
        {
            get
            {
                return mDA_InformationMDA_NumberField;
            }
            set
            {
                mDA_InformationMDA_NumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("MDA_Information.MDA_Firmware_Version")]
        public string MDA_InformationMDA_Firmware_Version
        {
            get
            {
                return mDA_InformationMDA_Firmware_VersionField;
            }
            set
            {
                mDA_InformationMDA_Firmware_VersionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SHOP_FLOOR_DATA.SAPCODE")]
        public ulong SHOP_FLOOR_DATASAPCODE
        {
            get
            {
                return sHOP_FLOOR_DATASAPCODEField;
            }
            set
            {
                sHOP_FLOOR_DATASAPCODEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SHOP_FLOOR_DATA.ORDERTYPE")]
        public string SHOP_FLOOR_DATAORDERTYPE
        {
            get
            {
                return sHOP_FLOOR_DATAORDERTYPEField;
            }
            set
            {
                sHOP_FLOOR_DATAORDERTYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SHOP_FLOOR_DATA.MODCOD")]
        public object SHOP_FLOOR_DATAMODCOD
        {
            get
            {
                return sHOP_FLOOR_DATAMODCODField;
            }
            set
            {
                sHOP_FLOOR_DATAMODCODField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SHOP_FLOOR_DATA.SAPSEQ")]
        public ulong SHOP_FLOOR_DATASAPSEQ
        {
            get
            {
                return sHOP_FLOOR_DATASAPSEQField;
            }
            set
            {
                sHOP_FLOOR_DATASAPSEQField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("SHOP_FLOOR_DATA.LINE_DESCR")]
        public string SHOP_FLOOR_DATALINE_DESCR
        {
            get
            {
                return sHOP_FLOOR_DATALINE_DESCRField;
            }
            set
            {
                sHOP_FLOOR_DATALINE_DESCRField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
                return componentField;
            }
            set
            {
                componentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ComponentText")]
        public DashboardTestReportResultComponentText[] ComponentText
        {
            get
            {
                return componentTextField;
            }
            set
            {
                componentTextField = value;
            }
        }

        /// <remarks/>
        public string Item
        {
            get
            {
                return itemField;
            }
            set
            {
                itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ItemText")]
        public DashboardTestReportResultItemText[] ItemText
        {
            get
            {
                return itemTextField;
            }
            set
            {
                itemTextField = value;
            }
        }

        /// <remarks/>
        public string ResultType
        {
            get
            {
                return resultTypeField;
            }
            set
            {
                resultTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ResultText")]
        public DashboardTestReportResultResultText[] ResultText
        {
            get
            {
                return resultTextField;
            }
            set
            {
                resultTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("Error")]
        public DashboardTestReportResultError[] Error
        {
            get
            {
                return errorField;
            }
            set
            {
                errorField = value;
            }
        }

        /// <remarks/>
        public string ResultState
        {
            get
            {
                return resultStateField;
            }
            set
            {
                resultStateField = value;
            }
        }

        /// <remarks/>
        public DashboardTestReportResultType_Tolerance Type_Tolerance
        {
            get
            {
                return type_ToleranceField;
            }
            set
            {
                type_ToleranceField = value;
            }
        }

        /// <remarks/>
        public object Type_Execution
        {
            get
            {
                return type_ExecutionField;
            }
            set
            {
                type_ExecutionField = value;
            }
        }

        /// <remarks/>
        public DashboardTestReportResultType_State Type_State
        {
            get
            {
                return type_StateField;
            }
            set
            {
                type_StateField = value;
            }
        }

        /// <remarks/>
        public DashboardTestReportResultDetailTags DetailTags
        {
            get
            {
                return detailTagsField;
            }
            set
            {
                detailTagsField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class DashboardTestReportResultComponentText
    {

        private string langField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return langField;
            }
            set
            {
                langField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class DashboardTestReportResultItemText
    {

        private string langField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return langField;
            }
            set
            {
                langField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class DashboardTestReportResultResultText
    {

        private string langField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return langField;
            }
            set
            {
                langField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class DashboardTestReportResultError
    {

        private string langField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return langField;
            }
            set
            {
                langField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlText()]
        public string Value
        {
            get
            {
                return valueField;
            }
            set
            {
                valueField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
                return lowerLimitField;
            }
            set
            {
                lowerLimitField = value;
            }
        }

        /// <remarks/>
        public byte UpperLimit
        {
            get
            {
                return upperLimitField;
            }
            set
            {
                upperLimitField = value;
            }
        }

        /// <remarks/>
        public decimal ActualValue
        {
            get
            {
                return actualValueField;
            }
            set
            {
                actualValueField = value;
            }
        }

        /// <remarks/>
        public string PhysicalUnit
        {
            get
            {
                return physicalUnitField;
            }
            set
            {
                physicalUnitField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class DashboardTestReportResultType_State
    {

        private string actualStringField;

        private string referenceStringField;

        /// <remarks/>
        public string ActualString
        {
            get
            {
                return actualStringField;
            }
            set
            {
                actualStringField = value;
            }
        }

        /// <remarks/>
        public string ReferenceString
        {
            get
            {
                return referenceStringField;
            }
            set
            {
                referenceStringField = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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

        private DateTime eLT_SECTIONOPSTARTDATEField;

        private bool eLT_SECTIONOPSTARTDATEFieldSpecified;

        private DateTime eLT_SECTIONOPDATEField;

        private bool eLT_SECTIONOPDATEFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("TestInfo.PrintOnlyOnFull")]
        public bool TestInfoPrintOnlyOnFull
        {
            get
            {
                return testInfoPrintOnlyOnFullField;
            }
            set
            {
                testInfoPrintOnlyOnFullField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool TestInfoPrintOnlyOnFullSpecified
        {
            get
            {
                return testInfoPrintOnlyOnFullFieldSpecified;
            }
            set
            {
                testInfoPrintOnlyOnFullFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ReadInformation.SSHWNumber")]
        public string ReadInformationSSHWNumber
        {
            get
            {
                return readInformationSSHWNumberField;
            }
            set
            {
                readInformationSSHWNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ReadInformation.SSHWVersionNumber")]
        public string ReadInformationSSHWVersionNumber
        {
            get
            {
                return readInformationSSHWVersionNumberField;
            }
            set
            {
                readInformationSSHWVersionNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ReadInformation.SSSWNumber")]
        public string ReadInformationSSSWNumber
        {
            get
            {
                return readInformationSSSWNumberField;
            }
            set
            {
                readInformationSSSWNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ReadInformation.SSSWVersionNumber")]
        public uint ReadInformationSSSWVersionNumber
        {
            get
            {
                return readInformationSSSWVersionNumberField;
            }
            set
            {
                readInformationSSSWVersionNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ReadInformationSSSWVersionNumberSpecified
        {
            get
            {
                return readInformationSSSWVersionNumberFieldSpecified;
            }
            set
            {
                readInformationSSSWVersionNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ReadInformation.VMSWNumber")]
        public string ReadInformationVMSWNumber
        {
            get
            {
                return readInformationVMSWNumberField;
            }
            set
            {
                readInformationVMSWNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ReadInformation.VMSWVersionNumber")]
        public string ReadInformationVMSWVersionNumber
        {
            get
            {
                return readInformationVMSWVersionNumberField;
            }
            set
            {
                readInformationVMSWVersionNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("TestInfo.StartTime")]
        public string TestInfoStartTime
        {
            get
            {
                return testInfoStartTimeField;
            }
            set
            {
                testInfoStartTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("TestInfo.EndTime")]
        public string TestInfoEndTime
        {
            get
            {
                return testInfoEndTimeField;
            }
            set
            {
                testInfoEndTimeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ELT_SECTION.OPCODE")]
        public string ELT_SECTIONOPCODE
        {
            get
            {
                return eLT_SECTIONOPCODEField;
            }
            set
            {
                eLT_SECTIONOPCODEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ELT_SECTION.WORKPLACE")]
        public string ELT_SECTIONWORKPLACE
        {
            get
            {
                return eLT_SECTIONWORKPLACEField;
            }
            set
            {
                eLT_SECTIONWORKPLACEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ELT_SECTION.OPERATOR")]
        public uint ELT_SECTIONOPERATOR
        {
            get
            {
                return eLT_SECTIONOPERATORField;
            }
            set
            {
                eLT_SECTIONOPERATORField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ELT_SECTIONOPERATORSpecified
        {
            get
            {
                return eLT_SECTIONOPERATORFieldSpecified;
            }
            set
            {
                eLT_SECTIONOPERATORFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ELT_SECTION.OPSTARTDATE")]
        public DateTime ELT_SECTIONOPSTARTDATE
        {
            get
            {
                return eLT_SECTIONOPSTARTDATEField;
            }
            set
            {
                eLT_SECTIONOPSTARTDATEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ELT_SECTIONOPSTARTDATESpecified
        {
            get
            {
                return eLT_SECTIONOPSTARTDATEFieldSpecified;
            }
            set
            {
                eLT_SECTIONOPSTARTDATEFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElement("ELT_SECTION.OPDATE")]
        public DateTime ELT_SECTIONOPDATE
        {
            get
            {
                return eLT_SECTIONOPDATEField;
            }
            set
            {
                eLT_SECTIONOPDATEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnore()]
        public bool ELT_SECTIONOPDATESpecified
        {
            get
            {
                return eLT_SECTIONOPDATEFieldSpecified;
            }
            set
            {
                eLT_SECTIONOPDATEFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
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
                return testResultStateField;
            }
            set
            {
                testResultStateField = value;
            }
        }

        /// <remarks/>
        public ushort ResultCounter
        {
            get
            {
                return resultCounterField;
            }
            set
            {
                resultCounterField = value;
            }
        }

        /// <remarks/>
        public byte NotOkResults
        {
            get
            {
                return notOkResultsField;
            }
            set
            {
                notOkResultsField = value;
            }
        }

        /// <remarks/>
        public string EndOfReport
        {
            get
            {
                return endOfReportField;
            }
            set
            {
                endOfReportField = value;
            }
        }
    }


}
