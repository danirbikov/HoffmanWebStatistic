
// Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class SHOP_FLOOR_DATA
{

    private SHOP_FLOOR_DATAELT_SECTION eLT_SECTIONField;

    private string mSG_IDField;

    private System.DateTime mSG_DTField;

    private string pRODUCTIONORDERField;

    private ulong sAPCODEField;

    private string oRDERTYPEField;

    private string mODCODField;

    private ulong sAPSEQField;

    private string lINE_DESCRField;

    /// <remarks/>
    public SHOP_FLOOR_DATAELT_SECTION ELT_SECTION
    {
        get
        {
            return this.eLT_SECTIONField;
        }
        set
        {
            this.eLT_SECTIONField = value;
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
    public System.DateTime MSG_DT
    {
        get
        {
            return this.mSG_DTField;
        }
        set
        {
            this.mSG_DTField = value;
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
    public ulong SAPCODE
    {
        get
        {
            return this.sAPCODEField;
        }
        set
        {
            this.sAPCODEField = value;
        }
    }

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
    public string MODCOD
    {
        get
        {
            return this.mODCODField;
        }
        set
        {
            this.mODCODField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ulong SAPSEQ
    {
        get
        {
            return this.sAPSEQField;
        }
        set
        {
            this.sAPSEQField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string LINE_DESCR
    {
        get
        {
            return this.lINE_DESCRField;
        }
        set
        {
            this.lINE_DESCRField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class SHOP_FLOOR_DATAELT_SECTION
{

    private SHOP_FLOOR_DATAELT_SECTIONELT_DATA eLT_DATAField;

    /// <remarks/>
    public SHOP_FLOOR_DATAELT_SECTIONELT_DATA ELT_DATA
    {
        get
        {
            return this.eLT_DATAField;
        }
        set
        {
            this.eLT_DATAField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class SHOP_FLOOR_DATAELT_SECTIONELT_DATA
{

    private string oPCODEField;

    private string wORKPLACEField;

    private uint oPERATORField;

    private System.DateTime oPSTARTDATEField;

    private System.DateTime oPDATEField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string OPCODE
    {
        get
        {
            return this.oPCODEField;
        }
        set
        {
            this.oPCODEField = value;
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
    public uint OPERATOR
    {
        get
        {
            return this.oPERATORField;
        }
        set
        {
            this.oPERATORField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime OPSTARTDATE
    {
        get
        {
            return this.oPSTARTDATEField;
        }
        set
        {
            this.oPSTARTDATEField = value;
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

