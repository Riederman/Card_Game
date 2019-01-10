using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions;

public class CardManagerMB : Singleton<CardManagerMB>
{
    [SerializeField] private SpriteColorPair[] colorPairs;
    [SerializeField] private SpriteActionPair[] actionPairs;

    private Dictionary<ColorType, Sprite> colorDict = new Dictionary<ColorType, Sprite>();
    private Dictionary<ActionType, Sprite> actionDict = new Dictionary<ActionType, Sprite>();
    private Dictionary<string, CardXML> cardDict = new Dictionary<string, CardXML>();

    private void Awake()
    {
        // Populate dictionaries with data
        foreach (SpriteColorPair pair in colorPairs)
            colorDict.Add(pair.color, pair.sprite);
        foreach (SpriteActionPair pair in actionPairs)
            actionDict.Add(pair.action, pair.sprite);
        CardContainer container = CardContainer.Load(Path.Combine(Application.dataPath, "XML/Cards.xml"));
        cardDict = CardContainer.GetDictionary(container);
    }

    public CardXML GetCard(string name)
    {
        Assert.IsTrue(cardDict.ContainsKey(name));
        return cardDict[name];
    }

    public Sprite GetColorSprite(ColorType color)
    {
        Assert.IsTrue(colorDict.ContainsKey(color));
        return colorDict[color];
    }

    public Sprite GetActionSprite(ActionType action)
    {
        Assert.IsTrue(actionDict.ContainsKey(action));
        return actionDict[action];
    }
}