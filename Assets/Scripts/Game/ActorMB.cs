using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorMB : MonoBehaviour
{
    [SerializeField] private string deckName;
    [SerializeField] private HandMB hand;
    [SerializeField] private HealthBarMB healthBar;

    private int cardIndex;
    private Deck deck;

    public System.Action OnActorDeath;

    public bool HasDrawnCards { get; private set; }
    public bool HasSelectedCards { get; private set; }
    public bool HasRevealedCards { get; private set; }
    public bool HasAppliedEffects { get; private set; }
    public bool HasEndedTurn { get; private set; }

    protected abstract int GetSelectedIndex();

    private void Awake()
    {
        deck = new Deck(deckName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            DrawCards();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SelectCards();
        }
    }

    private void OnEnable()
    {
        healthBar.OnHealthZero += OnHealthZero;
    }

    private void OnDisable()
    {
        healthBar.OnHealthZero -= OnHealthZero;
    }

    public void DrawCards()
    {
        HasEndedTurn = false;
        StartCoroutine(DrawCardsCoroutine());
    }

    public IEnumerator DrawCardsCoroutine()
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
        StartCoroutine(SelectCardsCoroutine());
    }

    public IEnumerator SelectCardsCoroutine()
    {
        cardIndex = GetSelectedIndex();

        while (cardIndex < 0)
        {
            yield return null;
        }

        yield return hand.SelectCard(cardIndex);
        yield return new WaitForSeconds(2);
        HasSelectedCards = true;
    }

    public void RevealCards()
    {
        StartCoroutine(RevealCardsCoroutine());
    }

    public IEnumerator RevealCardsCoroutine()
    {
        // TODO: Reveal cards
        yield return new WaitForSeconds(2);
        HasRevealedCards = true;
    }

    public void ApplyEffects()
    {
        StartCoroutine(ApplyEffectsCoroutine());
    }

    public IEnumerator ApplyEffectsCoroutine()
    {
        // TODO: Apply effects
        yield return new WaitForSeconds(2);
        HasAppliedEffects = true;
    }

    public void EndTurn()
    {
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
        yield return new WaitForSeconds(2);
        HasEndedTurn = true;
    }

    private void OnHealthZero()
    {
        if (OnActorDeath != null)
        {
            OnActorDeath();
        }
    }

    private void OnCardSelect(int index)
    {
        cardIndex = index;
    }
}