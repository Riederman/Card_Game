using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorMB : MonoBehaviour
{
    [SerializeField] private string deckName;
    [SerializeField] private HealthBarMB healthBar;

    [SerializeField] protected HandMB hand;

    private int cardIndex;
    private Deck deck;

    public System.Action OnActorDeath;

    public ActorMB Opposite { get; set; }
    public ColorType CurrentColor { get; private set; }
    public bool HasDrawnCards { get; private set; }
    public bool HasSelectedCards { get; private set; }
    public bool HasRevealedCards { get; private set; }
    public bool HasAppliedEffects { get; private set; }
    public bool HasEndedTurn { get; private set; }

    protected abstract int GetSelectedIndex();
    protected abstract bool IsPlayer();

    #region Unity

    private void Awake()
    {
        deck = new Deck(deckName);
        hand.Initialize(IsPlayer());
    }

    private void OnEnable()
    {
        healthBar.OnHealthZero += OnHealthZero;
    }

    private void OnDisable()
    {
        healthBar.OnHealthZero -= OnHealthZero;
    }

    #endregion

    #region Turn

    public void DrawCards()
    {
        Debug.Log("Draw Cards " + name);
        HasEndedTurn = false;
        StartCoroutine(DrawCardsCoroutine());
    }

    private IEnumerator DrawCardsCoroutine()
    {
        List<CardXML> cards = new List<CardXML>();
        List<DeckComponent> components = new List<DeckComponent>();
        components.AddRange(deck.GetNumRandomComponents(GameConstants.NUM_CARDS_PER_HAND));
        foreach (DeckComponent component in components)
            cards.Add(CardManagerMB.Instance.GetCard(component.name));
        yield return hand.AddCards(cards);
        hand.SubscribeOnCardSelect(OnCardSelect);
        HasDrawnCards = true;
    }

    public void SelectCards()
    {
        Debug.Log("Select Cards " + name);
        StartCoroutine(SelectCardsCoroutine());
    }

    private IEnumerator SelectCardsCoroutine()
    {
        cardIndex = GetSelectedIndex();
        while (cardIndex < 0)
            yield return null;
        yield return hand.SelectCard(cardIndex);
        HasSelectedCards = true;
    }

    public void RevealCards()
    {
        Debug.Log("Reveal Cards " + name);
        StartCoroutine(RevealCardsCoroutine());
    }

    private IEnumerator RevealCardsCoroutine()
    {
        yield return hand.RevealCard(cardIndex);
        HasRevealedCards = true;
    }

    public void ApplyEffects()
    {
        Debug.Log("Apply Effects " + name);
        StartCoroutine(ApplyEffectsCoroutine());
    }

    private IEnumerator ApplyEffectsCoroutine()
    {
        CardMB card = hand.GetCardByIndex(cardIndex);
        EffectMessage message = new EffectMessage();
        message.target = card.Effect.CanTargetSelf ? this : Opposite;
        CurrentColor = card.Color;
        yield return new WaitForSeconds(1);
        float multiplier = GetColorMultiplier(CurrentColor, Opposite.CurrentColor);
        message.value = (int)(card.Value * multiplier);
        card.Effect.ApplyEffect(message);
        HasAppliedEffects = true;
    }

    public virtual void EndTurn()
    {
        healthBar.CheckOnHealthZero();
        deck.ReturnComponents();
        hand.UnsubscribeOnCardSelect(OnCardSelect);
        hand.ClearHand();
        HasDrawnCards = false;
        HasSelectedCards = false;
        HasRevealedCards = false;
        HasAppliedEffects = false;
        StartCoroutine(EndTurnCoroutine());
    }

    public IEnumerator EndTurnCoroutine()
    {
        yield return new WaitForSeconds(1);
        HasEndedTurn = true;
    }

    #endregion

    #region Interaction

    public void AddHealth(int value)
    {
        healthBar.AddHealth(value);
    }

    public void RemoveHealth(int value)
    {
        healthBar.RemoveHealth(value);
    }

    public float GetColorMultiplier(ColorType offensive, ColorType defensive)
    {
        if (offensive == ColorType.Red)
        {
            if (defensive == ColorType.Blue) return .5f;
            if (defensive == ColorType.Green || defensive == ColorType.Black) return 2;
        }
        if (offensive == ColorType.Green)
        {
            if (defensive == ColorType.Blue) return .5f;
            if (defensive == ColorType.Red || defensive == ColorType.Black) return 2;
        }
        if (offensive == ColorType.Blue)
        {
            if (defensive == ColorType.Green) return .5f;
            if (defensive == ColorType.Red || defensive == ColorType.Black) return 2;
        }
        if (offensive == ColorType.Black)
        {
            if (defensive == ColorType.Yellow) return .5f;
            if (defensive != ColorType.Black) return 2;
        }
        if (offensive == ColorType.Yellow && defensive == ColorType.Black) return 2;
        return 1;
    }

    #endregion

    #region Delegates

    private void OnHealthZero()
    {
        if (OnActorDeath != null)
            OnActorDeath();
    }

    private void OnCardSelect(int index)
    {
        cardIndex = index;
    }

    #endregion
}