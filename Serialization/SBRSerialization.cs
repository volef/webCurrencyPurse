using System.Xml.Serialization;

namespace webTest.Serialization
{
    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class ValCurs
    {
        private ValCursValute[] _valuteField;

        private string _dateField;

        private string _nameField;

        /// <remarks/>
        [XmlElementAttribute("Valute")]
        public ValCursValute[] Valute
        {
            get => _valuteField;
            set => _valuteField = value;
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string Date
        {
            get => _dateField;
            set => _dateField = value;
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string Name
        {
            get => _nameField;
            set => _nameField = value;
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class ValCursValute
    {
        private ushort _numCodeField;

        private string _charCodeField;

        private ushort _nominalField;

        private string _nameField;

        private string _valueField;

        private string _idField;

        /// <remarks/>
        public ushort NumCode
        {
            get => _numCodeField;
            set => _numCodeField = value;
        }

        /// <remarks/>
        public string CharCode
        {
            get => _charCodeField;
            set => _charCodeField = value;
        }

        /// <remarks/>
        public ushort Nominal
        {
            get => _nominalField;
            set => _nominalField = value;
        }

        /// <remarks/>
        public string Name
        {
            get => _nameField;
            set => _nameField = value;
        }

        /// <remarks/>
        public string Value
        {
            get => _valueField;
            set => _valueField = value;
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string Id
        {
            get => _idField;
            set => _idField = value;
        }
    }
}