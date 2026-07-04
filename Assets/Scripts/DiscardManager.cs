using UnityEngine;
using Project;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

public class DiscardManager : MonoBehaviour
{
    [SerializeField] public List<Card> discardCards = new List<Card>();

    public TextMeshProUGUI discardCount;
    public int discardCardsCount;

    void Awake()
    {
        UpdateDiscardCount();
    }

    private void UpdateDiscardCount()
    {
        discardCount.text = discardCards.Count.ToString();
        discardCardsCount = discardCards.Count;

    }

    public void AddToDiscard(Card card)
    {
        if (card != null)
        {
            discardCards.Add(card);
            UpdateDiscardCount();
        }
    }

    public Card PullFromDiscard()
    {
        if (discardCards.Count < 0)
        {
            return null;
        }

        Card cardToReturn = discardCards[discardCards.Count - 1];
        discardCards.RemoveAt(discardCards.Count - 1);

        UpdateDiscardCount();
        return cardToReturn;
        
    }

    public bool PullSeclectCardFromDiscard(Card card)
    {
        if (discardCards.Count <= 0 || !discardCards.Contains(card))
        {
            return false;
        }

        discardCards.Remove(card);
        UpdateDiscardCount();
        return true;
    }

    public List<Card> PullAllFromDiscard()
    {
        if (discardCards.Count <= 0)
        {
            return new List<Card>();
        }

        List<Card> cards = new List<Card>(discardCards);
        discardCards.Clear();

        UpdateDiscardCount();
        return cards;
    
    }

}
