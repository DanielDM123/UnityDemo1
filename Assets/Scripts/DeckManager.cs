using UnityEngine;
using Project;
using System.Collections;
using System.Collections.Generic;
using System;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();

    private int currentIndex = 0;

    void Start()
    {
        // Load all assests from the Resource folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        // Put the loaded cards into the allCards list
        allCards.AddRange(cards);

        HandManager hand = FindObjectOfType<HandManager>();
        for (int i = 0; i < 6; i++)
        {
            DrawCard(hand);
        }

    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
        {
            return;
        }

        Card nextCard = allCards[currentIndex];
        bool addedCard = handManager.AddCardToHand(nextCard);
        if (addedCard)
        {
            currentIndex = (currentIndex + 1) % allCards.Count;
        }
        
    }
}
