using System.Xml;
using System.Xml.Serialization;

public class CardXML
{
    [XmlAttribute("Name")]
    public string name;

    public int value;
    public ColorType color;
    public ActionType action;
}