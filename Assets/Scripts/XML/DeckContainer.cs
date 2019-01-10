using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("Container")]
public class DeckContainer
{
    [XmlArray("Collection")]
    [XmlArrayItem("Deck")]
    public List<DeckXML> decks = new List<DeckXML>();

    public static DeckContainer Load(string path)
    {
        // Load data from an XML file to a container
        XmlSerializer serializer = new XmlSerializer(typeof(DeckContainer));
        using (FileStream stream = new FileStream(path, FileMode.Open))
            return serializer.Deserialize(stream) as DeckContainer;
    }

    public static Dictionary<string, DeckXML> GetDictionary(DeckContainer container)
    {
        // Add each deck in the XML container to a dictionary
        Dictionary<string, DeckXML> dictionary = new Dictionary<string, DeckXML>();
        foreach (DeckXML deck in container.decks)
            dictionary.Add(deck.name, deck);
        return dictionary;
    }
}