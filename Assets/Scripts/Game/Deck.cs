using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

public class Deck
{
    private List<DeckComponent> components = new List<DeckComponent>();

    public Deck(string name)
    {
        // Load deck components from XML file
        DeckContainer container = DeckContainer.Load(Path.Combine(Application.dataPath, "XML/Decks.xml"));
        Dictionary<string, DeckXML> deckDict = DeckContainer.GetDictionary(container);
        Assert.IsTrue(deckDict.ContainsKey(name));
        components = deckDict[name].components.Clone();
    }

    public DeckComponent GetRandomComponent()
    {
        // Returns a component by weighted selection
        int cumulative = 0;
        int random = Random.Range(0, GetTotalWeight());
        foreach (DeckComponent component in components)
        {
            cumulative += component.weight;
            if (random < cumulative)
            {
                component.weight--;
                return component;
            }
                
        }
        return null;
    }

    public DeckComponent[] GetNumRandomComponents(int num)
    {
        // Returns multiple components by cumulative weighted selection
        DeckComponent[] array = new DeckComponent[num];
        for (int i = 0; i < array.Length; i++)
            array[i] = GetRandomComponent();
        return array;
    }

    private int GetTotalWeight()
    {
        // Returns the total weight of all components
        int total = 0;
        foreach (DeckComponent component in components)
            total += component.weight;
        return total;
    }

    public void ReturnComponent(DeckComponent component)
    {
        // Adds weight to the returned component
        if (components.Contains(component))
            component.weight++;
    }
}