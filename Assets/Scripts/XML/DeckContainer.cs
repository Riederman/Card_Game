using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("DeckCollection")]
public class DeckContainer
{
    [XmlArray("Decks")]
    [XmlArrayItem("Deck")]
    public List<DeckXML> decks = new List<DeckXML>();

    public void Save(string path)
    {
        // Save data from a container to an XML file
        XmlSerializer serializer = new XmlSerializer(typeof(DeckContainer));
        using (var stream = new FileStream(path, FileMode.Create))
            serializer.Serialize(stream, this);
    }

    public static DeckContainer Load(string path)
    {
        // Load data from an XML file to a container
        XmlSerializer serializer = new XmlSerializer(typeof(CardContainer));
        using (FileStream stream = new FileStream(path, FileMode.Open))
            return serializer.Deserialize(stream) as DeckContainer;
    }
}