using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CardMB : MonoBehaviour
{
    [SerializeField] private Text textRend;
    [SerializeField] private SpriteRenderer colorRend;
    [SerializeField] private SpriteRenderer actionRend;
    [SerializeField] private GameObject glowObj;

    public int Index { get; private set; }
    public int Value { get; private set; }
    public ColorType Color { get; private set; }
    public ActionType Action { get; private set; }

    public System.Action<int> OnCardSelect;

    private void Awake()
    {
        Assert.IsTrue(textRend != null);
        Assert.IsTrue(colorRend != null);
        Assert.IsTrue(actionRend != null);
    }

    private void OnMouseDown()
    {
        if (OnCardSelect != null)
        {
            OnCardSelect(Index);
        }
    }

    public void SetProperties(int index, CardXML card)
    {
        // Get the card properties from an XML object
        Index = index;
        Value = card.value;
        Color = card.color;
        Action = card.action;
        RefreshUI();
    }

    private void RefreshUI()
    {
        // Set the text and sprites of the card to match the current data
        textRend.text = Value.ToString();
        colorRend.sprite = CardManagerMB.Instance.GetColorSprite(Color);
        actionRend.sprite = CardManagerMB.Instance.GetActionSprite(Action);
    }

    public void ToggleGlow()
    {
        glowObj.ToggleActive();
    }
}