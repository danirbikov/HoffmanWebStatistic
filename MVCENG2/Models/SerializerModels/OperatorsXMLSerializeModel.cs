
// Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class OperatorsXMLSerializeModel
{

    private OperatorsXMLSerializeModelStand[] standField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("stand")]
    public OperatorsXMLSerializeModelStand[] stand
    {
        get
        {
            return this.standField;
        }
        set
        {
            this.standField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class OperatorsXMLSerializeModelStand
{

    private OperatorsXMLSerializeModelStandUser[] userField;

    private ulong numberField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("user")]
    public OperatorsXMLSerializeModelStandUser[] user
    {
        get
        {
            return this.userField;
        }
        set
        {
            this.userField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ulong number
    {
        get
        {
            return this.numberField;
        }
        set
        {
            this.numberField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class OperatorsXMLSerializeModelStandUser
{

    private string loginField;

    private ulong passwordField;

    private byte numberField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
    public string login
    {
        get
        {
            return this.loginField;
        }
        set
        {
            this.loginField = value;
        }
    }

    /// <remarks/>
    public ulong password
    {
        get
        {
            return this.passwordField;
        }
        set
        {
            this.passwordField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte number
    {
        get
        {
            return this.numberField;
        }
        set
        {
            this.numberField = value;
        }
    }
}

