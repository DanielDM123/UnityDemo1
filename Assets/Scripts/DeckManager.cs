using Project;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    private HandManager handManager;

    private int currentIndex = 0;
    public int startingHandSize = 6;
    public int maxHandSize;
    public int currentHandSize;

    void Start()
    {
        // Load all assests from the Resource folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        // Put the loaded cards into the allCards list
        allCards.AddRange(cards);

        handManager = FindObjectOfType<HandManager>();
        maxHandSize = handManager.getMaxHandSize();
        for (int i = 0; i < startingHandSize; i++)
        {
            DrawCard(handManager);
            
        }

    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
        {
            return;
        }
        
        if (currentHandSize < maxHandSize)
        {
            Card nextCard = allCards[currentIndex];                  
            handManager.AddCardToHand(nextCard);
            currentIndex = (currentIndex + 1) % allCards.Count;
        }
        

    }
}
