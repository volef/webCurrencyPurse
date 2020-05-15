namespace webTest.Serialization
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.gesmes.org/xml/2002-08-01")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.gesmes.org/xml/2002-08-01", IsNullable = false)]
    public class Envelope
    {
        private string _subjectField;

        private EnvelopeSender _senderField;

        private Cube _cubeField;

        /// <remarks/>
        public string Subject
        {
            get => _subjectField;
            set => _subjectField = value;
        }

        /// <remarks/>
        public EnvelopeSender Sender
        {
            get => _senderField;
            set => _senderField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
        public Cube Cube
        {
            get => _cubeField;
            set => _cubeField = value;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.gesmes.org/xml/2002-08-01")]
    public class EnvelopeSender
    {
        private string _nameField;

        /// <remarks/>
        public string Name
        {
            get => _nameField;
            set => _nameField = value;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref",
        IsNullable = false)]
    public class Cube
    {
        private CubeCube _cube1Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cube")]
        public CubeCube Cube1
        {
            get => _cube1Field;
            set => _cube1Field = value;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
    public class CubeCube
    {
        private CubeCubeCube[] _cubeField;

        private System.DateTime _timeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Cube")]
        public CubeCubeCube[] Cube
        {
            get => _cubeField;
            set => _cubeField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime Time
        {
            get => _timeField;
            set => _timeField = value;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true,
        Namespace = "http://www.ecb.int/vocabulary/2002-08-01/eurofxref")]
    public class CubeCubeCube
    {
        private string _currencyField;

        private decimal _rateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Currency
        {
            get => _currencyField;
            set => _currencyField = value;
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Rate
        {
            get => _rateField;
            set => _rateField = value;
        }
    }
}