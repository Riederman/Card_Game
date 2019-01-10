using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class DeckXML
{
    [XmlAttribute("Name")]
    public string name;

    [XmlElement("Component")]
    public List<DeckComponent> components = new List<DeckComponent>();
}