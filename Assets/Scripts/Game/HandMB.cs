using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class HandMB : MonoBehaviour
{
    [SerializeField] private float timePerCard;
    [SerializeField] private GameObjectPool cardPool;
    [SerializeField] private Transform[] slotTrans;

    private bool isPlayer;

    private List<CardMB> cards = new List<CardMB>();

    public int NumCards { get { return cards.Count; } }

    private void Awake()
    {
        Assert.IsTrue(cardPool != null);
        Assert.IsTrue(slotTrans.Length == GameConstants.NUM_CARDS_PER_HAND);
    }

    public void Initialize(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }

    #region Interaction

    public IEnumerator AddCards(List<CardXML> list)
    {
        Assert.IsTrue(list.Count == GameConstants.NUM_CARDS_PER_HAND);
        for (int i = 0; i < list.Count; i++)
        {
            // Retrieve card from the pool
            CardMB card = cardPool.GetObject().GetComponent<CardMB>();
            Assert.IsTrue(card != null);
            // Set card view to match data
            card.transform.position = slotTrans[i].position;
            card.gameObject.SetActive(true);
            card.DrawCard(isPlayer);
            card.SetProperties(i, list[i]);
            cards.Add(card);
            // Yield until next card
            yield return new WaitForSeconds(timePerCard);
        }
    }

    public IEnumerator SelectCard(int index)
    {
        Assert.IsTrue(index >= 0 && index < cards.Count);
        cards[index].SelectCard();
        yield return new WaitForSeconds(1);
    }

    public IEnumerator RevealCard(int index)
    {
        Assert.IsTrue(index >= 0 && index < cards.Count);
        cards[index].RevealCard();
        yield return new WaitForSeconds(1);
    }

    public void ClearHand()
    {
        // Return each card to the pool
        foreach (CardMB card in cards)
        {
            card.ResetCard();
            cardPool.ReturnObject(card.gameObject);
        }
        cards.Clear();
    }

    public CardMB GetCardByIndex(int index)
    {
        Assert.IsTrue(index >= 0 && index < cards.Count);
        return cards[index];
    }

    #endregion

    #region Delegates

    public void SubscribeOnCardSelect(System.Action<int> function)
    {
        foreach (CardMB card in cards)
            card.OnCardSelect += function;
    }

    public void UnsubscribeOnCardSelect(System.Action<int> function)
    {
        foreach (CardMB card in cards)
            card.OnCardSelect -= function;
    }

    #endregion
}
