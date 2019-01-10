using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class HandMB : MonoBehaviour
{
    [SerializeField] private float timePerCard;
    [SerializeField] private GameObjectPool cardPool;
    [SerializeField] private Transform[] slotTrans;

    private List<CardMB> cards = new List<CardMB>();

    private void Awake()
    {
        Assert.IsTrue(cardPool != null);
        Assert.IsTrue(slotTrans.Length == GameConstants.NUM_CARDS_PER_HAND);
    }

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
            card.SetProperties(i, list[i]);
            cards.Add(card);
            // Yield until next card
            yield return new WaitForSeconds(timePerCard);
        }
    }

    public IEnumerator SelectCard(int index)
    {
        Assert.IsTrue(index >= 0 && index < cards.Count);
        yield return new WaitForSeconds(1);
        cards[index].ToggleGlow();
    }

    public void ClearHand()
    {
        // Return each card to the pool
        foreach (CardMB card in cards)
            cardPool.ReturnObject(card.gameObject);
        cards.Clear();
    }

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
}
