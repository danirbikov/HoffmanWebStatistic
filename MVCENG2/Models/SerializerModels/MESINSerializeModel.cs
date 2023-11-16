namespace HoffmanWebstatistic.Models.SerializerModels
{

    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "MOM.Production")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "MOM.Production", IsNullable = false)]
    public partial class MES_VEHICLE_STATUS_DESCRIPTOR
    {

        private MES_VEHICLE_STATUS_DESCRIPTORPOSECTION pOSECTIONField;

        private MES_VEHICLE_STATUS_DESCRIPTORORDERSECTION oRDERSECTIONField;

        private MES_VEHICLE_STATUS_DESCRIPTORCHARACTERISTIC[] cHARACTERISTICSSECTIONField;

        private MES_VEHICLE_STATUS_DESCRIPTORMATERIAL[] mATERIALSSECTIONField;

        private MES_VEHICLE_STATUS_DESCRIPTORECUSECTION eCUSECTIONField;

        private MES_VEHICLE_STATUS_DESCRIPTORELT_DATA[] eLTSECTIONField;

        private string xSDVERField;

        private string mSG_IDField;

        private string pRODUCTIONORDERField;

        private System.DateTime oPDATEField;

        private string oPTYPEField;

        private string sENDERField;

        private string tARGETField;

        private string pHYSICAL_ADDRESSField;

        /// <remarks/>
        public MES_VEHICLE_STATUS_DESCRIPTORPOSECTION POSECTION
        {
            get
            {
                return this.pOSECTIONField;
            }
            set
            {
                this.pOSECTIONField = value;
            }
        }

        /// <remarks/>
        public MES_VEHICLE_STATUS_DESCRIPTORORDERSECTION ORDERSECTION
        {
            get
            {
                return this.oRDERSECTIONField;
            }
            set
            {
                this.oRDERSECTIONField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("CHARACTERISTIC", IsNullable = false)]
        public MES_VEHICLE_STATUS_DESCRIPTORCHARACTERISTIC[] CHARACTERISTICSSECTION
        {
            get
            {
                return this.cHARACTERISTICSSECTIONField;
            }
            set
            {
                this.cHARACTERISTICSSECTIONField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("MATERIAL", IsNullable = false)]
        public MES_VEHICLE_STATUS_DESCRIPTORMATERIAL[] MATERIALSSECTION
        {
            get
            {
                return this.mATERIALSSECTIONField;
            }
            set
            {
                this.mATERIALSSECTIONField = value;
            }
        }

        /// <remarks/>
        public MES_VEHICLE_STATUS_DESCRIPTORECUSECTION ECUSECTION
        {
            get
            {
                return this.eCUSECTIONField;
            }
            set
            {
                this.eCUSECTIONField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ELT_DATA", IsNullable = false)]
        public MES_VEHICLE_STATUS_DESCRIPTORELT_DATA[] ELTSECTION
        {
            get
            {
                return this.eLTSECTIONField;
            }
            set
            {
                this.eLTSECTIONField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string XSDVER
        {
            get
            {
                return this.xSDVERField;
            }
            set
            {
                this.xSDVERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MSG_ID
        {
            get
            {
                return this.mSG_IDField;
            }
            set
            {
                this.mSG_IDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PRODUCTIONORDER
        {
            get
            {
                return this.pRODUCTIONORDERField;
            }
            set
            {
                this.pRODUCTIONORDERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime OPDATE
        {
            get
            {
                return this.oPDATEField;
            }
            set
            {
                this.oPDATEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string OPTYPE
        {
            get
            {
                return this.oPTYPEField;
            }
            set
            {
                this.oPTYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SENDER
        {
            get
            {
                return this.sENDERField;
            }
            set
            {
                this.sENDERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TARGET
        {
            get
            {
                return this.tARGETField;
            }
            set
            {
                this.tARGETField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PHYSICAL_ADDRESS
        {
            get
            {
                return this.pHYSICAL_ADDRESSField;
            }
            set
            {
                this.pHYSICAL_ADDRESSField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "MOM.Production")]
    public partial class MES_VEHICLE_STATUS_DESCRIPTORPOSECTION
    {

        private string oRDERTYPEField;

        private ulong sEQNUMField;

        private string cURSTAField;

        private string lASTPOSITIONField;

        private string lAST_SAP_POSITIONField;

        private ushort mODELField;

        private string vINField;

        private string sERIALNUMBERField;

        private bool iSPRODUCEDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ORDERTYPE
        {
            get
            {
                return this.oRDERTYPEField;
            }
            set
            {
                this.oRDERTYPEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong SEQNUM
        {
            get
            {
                return this.sEQNUMField;
            }
            set
            {
                this.sEQNUMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CURSTA
        {
            get
            {
                return this.cURSTAField;
            }
            set
            {
                this.cURSTAField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LASTPOSITION
        {
            get
            {
                return this.lASTPOSITIONField;
            }
            set
            {
                this.lASTPOSITIONField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string LAST_SAP_POSITION
        {
            get
            {
                return this.lAST_SAP_POSITIONField;
            }
            set
            {
                this.lAST_SAP_POSITIONField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort MODEL
        {
            get
            {
                return this.mODELField;
            }
            set
            {
                this.mODELField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string VIN
        {
            get
            {
                return this.vINField;
            }
            set
            {
                this.vINField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SERIALNUMBER
        {
            get
            {
                return this.sERIALNUMBERField;
            }
            set
            {
                this.sERIALNUMBERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool ISPRODUCED
        {
            get
            {
                return this.iSPRODUCEDField;
            }
            set
            {
                this.iSPRODUCEDField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "MOM.Production")]
    public partial class MES_VEHICLE_STATUS_DESCRIPTORORDERSECTION
    {

        private MES_VEHICLE_STATUS_DESCRIPTORORDERSECTIONTRANSPORTATIONCODE[] tRANSPORTATIONCODESField;

        private ushort sALESORDERField;

        private ulong mATERIALField;

        private System.DateTime sCHEDULEDField;

        private System.DateTime rELEASEDField;

        private string pLANTField;

        private ushort sAP_PLANTField;

        private string aSSEMBLYLINEField;

        private string sAP_ASSEMBLYLINEField;

        private bool fLOWOUTField;

        private bool cONTAINMENTField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("TRANSPORTATIONCODE", IsNullable = false)]
        public MES_VEHICLE_STATUS_DESCRIPTORORDERSECTIONTRANSPORTATIONCODE[] TRANSPORTATIONCODES
        {
            get
            {
                return this.tRANSPORTATIONCODESField;
            }
            set
            {
                this.tRANSPORTATIONCODESField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort SALESORDER
        {
            get
            {
                return this.sALESORDERField;
            }
            set
            {
                this.sALESORDERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ulong MATERIAL
        {
            get
            {
                return this.mATERIALField;
            }
            set
            {
                this.mATERIALField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime SCHEDULED
        {
            get
            {
                return this.sCHEDULEDField;
            }
            set
            {
                this.sCHEDULEDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime RELEASED
        {
            get
            {
                return this.rELEASEDField;
            }
            set
            {
                this.rELEASEDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string PLANT
        {
            get
            {
                return this.pLANTField;
            }
            set
            {
                this.pLANTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort SAP_PLANT
        {
            get
            {
                return this.sAP_PLANTField;
            }
            set
            {
                this.sAP_PLANTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ASSEMBLYLINE
        {
            get
            {
                return this.aSSEMBLYLINEField;
            }
            set
            {
                this.aSSEMBLYLINEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SAP_ASSEMBLYLINE
        {
            get
            {
                return this.sAP_ASSEMBLYLINEField;
            }
            set
            {
                this.sAP_ASSEMBLYLINEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool FLOWOUT
        {
            get
            {
                return this.fLOWOUTField;
            }
            set
            {
                this.fLOWOUTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool CONTAINMENT
        {
            get
            {
                return this.cONTAINMENTField;
            }
            set
            {
                this.cONTAINMENTField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "MOM.Production")]
    public partial class MES_VEHICLE_STATUS_DESCRIPTORORDERSECTIONTRANSPORTATIONCODE
    {

        private ushort tRCODEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort TRCODE
        {
            get
            {
                return this.tRCODEField;
            }
            set
            {
                this.tRCODEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "MOM.Production")]
    public partial class MES_VEHICLE_STATUS_DESCRIPTORCHARACTERISTIC
    {

        private string cHAR_IDField;

        private string cHAR_DESCRField;

        private bool iSOPTField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CHAR_ID
        {
            get
            {
                return this.cHAR_IDField;
            }
            set
            {
                this.cHAR_IDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CHAR_DESCR
        {
            get
            {
                return this.cHAR_DESCRField;
            }
            set
            {
                this.cHAR_DESCRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool ISOPT
        {
            get
            {
                return this.iSOPTField;
            }
            set
            {
                this.iSOPTField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "MOM.Production")]
    public partial class MES_VEHICLE_STATUS_DESCRIPTORMATERIAL
    {

        private string dESCField;

        private uint gROUPField;

        private decimal qTYField;

        private string wORKPLACEField;

        private string sAP_WORKPLACEField;

        private bool cANBEBACKFLUSHEDBYERPField;

        private ulong valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DESC
        {
            get
            {
                return this.dESCField;
            }
            set
            {
                this.dESCField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint GROUP
        {
            get
            {
                return this.gROUPField;
            }
            set
            {
                this.gROUPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal QTY
        {
            get
            {
                return this.qTYField;
            }
            set
            {
                this.qTYField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string WORKPLACE
        {
            get
            {
                return this.wORKPLACEField;
            }
            set
            {
                this.wORKPLACEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SAP_WORKPLACE
        {
            get
            {
                return this.sAP_WORKPLACEField;
            }
            set
            {
                this.sAP_WORKPLACEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool CANBEBACKFLUSHEDBYERP
        {
            get
            {
                return this.cANBEBACKFLUSHEDBYERPField;
            }
            set
            {
                this.cANBEBACKFLUSHEDBYERPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public ulong Value
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "MOM.Production")]
    public partial class MES_VEHICLE_STATUS_DESCRIPTORECUSECTION
    {

        private MES_VEHICLE_STATUS_DESCRIPTORECUSECTIONSINGLE_ECU_DATA sINGLE_ECU_DATAField;

        /// <remarks/>
        public MES_VEHICLE_STATUS_DESCRIPTORECUSECTIONSINGLE_ECU_DATA SINGLE_ECU_DATA
        {
            get
            {
                return this.sINGLE_ECU_DATAField;
            }
            set
            {
                this.sINGLE_ECU_DATAField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "MOM.Production")]
    public partial class MES_VEHICLE_STATUS_DESCRIPTORECUSECTIONSINGLE_ECU_DATA
    {

        private MES_VEHICLE_STATUS_DESCRIPTORECUSECTIONSINGLE_ECU_DATAECU_CONFIG_ATTRIBUTE_DETAIL[] eCU_CONFIG_ATTRIBUTE_DETAILField;

        private string eCU_OP_CODEField;

        private string eCU_OP_WORKPLACEField;

        private string eCU_SAP_OP_WORKPLACEField;

        private byte eCU_HW_NUMBERField;

        private byte eCU_HW_VERSNUMBERField;

        private byte eCU_SW_NUMBERField;

        private byte eCU_SW_VERSNUMBERField;

        private string eCU_RESULTField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ECU_CONFIG_ATTRIBUTE_DETAIL")]
        public MES_VEHICLE_STATUS_DESCRIPTORECUSECTIONSINGLE_ECU_DATAECU_CONFIG_ATTRIBUTE_DETAIL[] ECU_CONFIG_ATTRIBUTE_DETAIL
        {
            get
            {
                return this.eCU_CONFIG_ATTRIBUTE_DETAILField;
            }
            set
            {
                this.eCU_CONFIG_ATTRIBUTE_DETAILField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ECU_OP_CODE
        {
            get
            {
                return this.eCU_OP_CODEField;
            }
            set
            {
                this.eCU_OP_CODEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ECU_OP_WORKPLACE
        {
            get
            {
                return this.eCU_OP_WORKPLACEField;
            }
            set
            {
                this.eCU_OP_WORKPLACEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ECU_SAP_OP_WORKPLACE
        {
            get
            {
                return this.eCU_SAP_OP_WORKPLACEField;
            }
            set
            {
                this.eCU_SAP_OP_WORKPLACEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ECU_HW_NUMBER
        {
            get
            {
                return this.eCU_HW_NUMBERField;
            }
            set
            {
                this.eCU_HW_NUMBERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ECU_HW_VERSNUMBER
        {
            get
            {
                return this.eCU_HW_VERSNUMBERField;
            }
            set
            {
                this.eCU_HW_VERSNUMBERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ECU_SW_NUMBER
        {
            get
            {
                return this.eCU_SW_NUMBERField;
            }
            set
            {
                this.eCU_SW_NUMBERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ECU_SW_VERSNUMBER
        {
            get
            {
                return this.eCU_SW_VERSNUMBERField;
            }
            set
            {
                this.eCU_SW_VERSNUMBERField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ECU_RESULT
        {
            get
            {
                return this.eCU_RESULTField;
            }
            set
            {
                this.eCU_RESULTField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "MOM.Production")]
    public partial class MES_VEHICLE_STATUS_DESCRIPTORECUSECTIONSINGLE_ECU_DATAECU_CONFIG_ATTRIBUTE_DETAIL
    {

        private string kEY_NAMEField;

        private string kEY_VALUEField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string KEY_NAME
        {
            get
            {
                return this.kEY_NAMEField;
            }
            set
            {
                this.kEY_NAMEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string KEY_VALUE
        {
            get
            {
                return this.kEY_VALUEField;
            }
            set
            {
                this.kEY_VALUEField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "MOM.Production")]
    public partial class MES_VEHICLE_STATUS_DESCRIPTORELT_DATA
    {

        private string eLT_OP_CODEField;

        private string eLT_OP_WORKPLACEField;

        private string eLT_OP_SAP_WORKPLACEField;

        private System.DateTime eLT_OP_START_DATEField;

        private System.DateTime eLT_OP_DATEField;

        private string eLT_RESULTField;

        private string eLT_TESTING_PROGRAMField;

        private string eLT_OPERATORField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ELT_OP_CODE
        {
            get
            {
                return this.eLT_OP_CODEField;
            }
            set
            {
                this.eLT_OP_CODEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ELT_OP_WORKPLACE
        {
            get
            {
                return this.eLT_OP_WORKPLACEField;
            }
            set
            {
                this.eLT_OP_WORKPLACEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ELT_OP_SAP_WORKPLACE
        {
            get
            {
                return this.eLT_OP_SAP_WORKPLACEField;
            }
            set
            {
                this.eLT_OP_SAP_WORKPLACEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime ELT_OP_START_DATE
        {
            get
            {
                return this.eLT_OP_START_DATEField;
            }
            set
            {
                this.eLT_OP_START_DATEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime ELT_OP_DATE
        {
            get
            {
                return this.eLT_OP_DATEField;
            }
            set
            {
                this.eLT_OP_DATEField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ELT_RESULT
        {
            get
            {
                return this.eLT_RESULTField;
            }
            set
            {
                this.eLT_RESULTField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ELT_TESTING_PROGRAM
        {
            get
            {
                return this.eLT_TESTING_PROGRAMField;
            }
            set
            {
                this.eLT_TESTING_PROGRAMField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ELT_OPERATOR
        {
            get
            {
                return this.eLT_OPERATORField;
            }
            set
            {
                this.eLT_OPERATORField = value;
            }
        }
    }

}