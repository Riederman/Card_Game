using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CardMB : MonoBehaviour
{
    [SerializeField] private Text textRend;
    [SerializeField] private SpriteRenderer colorRend;
    [SerializeField] private SpriteRenderer actionRend;

    private void Awake()
    {
        Assert.IsTrue(textRend != null);
        Assert.IsTrue(colorRend != null);
        Assert.IsTrue(actionRend != null);
    }

    public void SetView(CardXML card)
    {
        // Set the text and sprites of the card to match the XML data
        textRend.text = card.value.ToString();
        colorRend.sprite = CardManagerMB.Instance.GetColorSprite(card.color);
        actionRend.sprite = CardManagerMB.Instance.GetActionSprite(card.action);
    }
}