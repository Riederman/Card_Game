using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[XmlRoot("CardCollection")]
public class CardContainer
{
    [XmlArray("Cards")]
    [XmlArrayItem("Card")]
    public List<CardXML> cards = new List<CardXML>();

    public static CardContainer Load(string path)
    {
        // Load data from an XML file to a container
        XmlSerializer serializer = new XmlSerializer(typeof(CardContainer));
        using (FileStream stream = new FileStream(path, FileMode.Open))
            return serializer.Deserialize(stream) as CardContainer;
    }

    public static Dictionary<string, CardXML> GetDictionary(CardContainer container)
    {
        // Add each card in the XML container to a dictionary
        Dictionary<string, CardXML> dictionary = new Dictionary<string, CardXML>();
        foreach (CardXML card in container.cards)
            dictionary.Add(card.name, card);
        return dictionary;
    }
}