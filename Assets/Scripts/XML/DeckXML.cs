using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class DeckXML
{
    [XmlAttribute("Name")]
    public string name;

    [XmlArray("Cards")]
    [XmlArrayItem("Card")]
    public List<CardXML> cards = new List<CardXML>();
}