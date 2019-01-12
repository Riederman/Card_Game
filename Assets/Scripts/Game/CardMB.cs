using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D), typeof(Animator))]
public class CardMB : MonoBehaviour
{
    [SerializeField] private Text textRend;
    [SerializeField] private SpriteRenderer colorRend;
    [SerializeField] private SpriteRenderer actionRend;
    [SerializeField] private GameObject glowObj;
    [SerializeField] private GameObject backObj;

    public int Index { get; private set; }
    public int Value { get; private set; }
    public ColorType Color { get; private set; }
    public ActionType Action { get; private set; }
    public IEffect Effect { get; private set; }

    public System.Action<int> OnCardSelect;

    private Animator animator;

    private void Awake()
    {
        Assert.IsTrue(textRend != null);
        Assert.IsTrue(colorRend != null);
        Assert.IsTrue(actionRend != null);
        animator = GetComponent<Animator>();
        Assert.IsTrue(animator != null);
    }

    private void OnMouseDown()
    {
        if (OnCardSelect != null)
            OnCardSelect(Index);
    }

    public void SetProperties(int index, CardXML card)
    {
        // Get the card properties from an XML object
        Index = index;
        Value = card.value;
        Color = card.color;
        Action = card.action;
        Effect = GetEffect(Action);
        RefreshUI();
    }

    private void RefreshUI()
    {
        // Set the text and sprites of the card to match the current data
        textRend.text = Value.ToString();
        colorRend.sprite = CardManagerMB.Instance.GetColorSprite(Color);
        actionRend.sprite = CardManagerMB.Instance.GetActionSprite(Action);
    }

    public void DrawCard(bool isPlayer)
    {
        backObj.SetActive(!isPlayer);
    }

    public void SelectCard()
    {
        animator.SetBool("Selected", true);
    }

    public void RevealCard()
    {
        backObj.SetActive(false);
        glowObj.SetActive(true);
    }

    public void ResetCard()
    {
        glowObj.SetActive(false);
        backObj.SetActive(false);
        animator.SetBool("Selected", false);
        gameObject.SetActive(false);
    }

    private IEffect GetEffect(ActionType action)
    {
        switch (action)
        {
            case ActionType.Attack:
                return new AttackEffect();
            case ActionType.Heal:
                return new HealEffect();
        }
        return null;
    }
}