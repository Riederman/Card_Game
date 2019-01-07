using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

public class CardManagerMB : Singleton<CardManagerMB>
{
    private Dictionary<string, CardXML> dictionary = new Dictionary<string, CardXML>();

    private void Awake()
    {
        CardContainer container = CardContainer.Load(Path.Combine(Application.dataPath, "XML/Decks.xml"));
        dictionary = CardContainer.GetDictionary(container);
    }

    public CardXML GetCard(string name)
    {
        Assert.IsTrue(dictionary.ContainsKey(name));
        return dictionary[name];
    }
}