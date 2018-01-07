using System;
using System.Xml.Serialization;

namespace Task1
{
    public class Book
    {
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
        [XmlElement(ElementName = "isbn")]
        public string Isbn { get; set; }
        [XmlElement(ElementName = "author")]
        public string Author { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "genre")]
        public Genre Genre { get; set; }
        [XmlElement(ElementName = "publisher")]
        public string Publisher { get; set; }
        [XmlElement(ElementName = "publish_date", DataType = "date")]
        public DateTime PublishDate { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "registration_date", DataType = "date")]
        public DateTime RegistrationDate { get; set; }
    }
}
