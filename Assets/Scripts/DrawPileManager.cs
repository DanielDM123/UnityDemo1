using UnityEngine;
using Project;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using JetBrains.Annotations;

public class DrawPileManager : MonoBehaviour
{
    public List<Card> drawPile = new List<Card>();
    private HandManager handManager;
    private DiscardManager discardManager;

    private int currentIndex = 0;
    public int startingHandSize = 6;
    public int maxHandSize;
    public int currentHandSize;
    
    public TextMeshProUGUI drawPileCounter;

    void Start()
    {
        handManager = FindObjectOfType<HandManager>();

    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void MakeDrawPile(List<Card> cardsToAdd)
    {
        drawPile.AddRange(cardsToAdd);
        Utility.Shuffle(drawPile);
        UpdateDrawPileCount();
    }

    // Draw the cards into the players hand
    public void BattleSetup(int numberOfCardsToDraw, int setMaxHandSize)
    {
        maxHandSize = setMaxHandSize;

        for (int i = 0; i < numberOfCardsToDraw; i++)
        {
            DrawCard(handManager);
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (drawPile.Count == 0)
        {
            RefullDeckFromDiscard();
        }

        if (currentHandSize < maxHandSize)
        {
            Debug.Log("drawPile count: " + drawPile.Count);
            Debug.Log("currentIndex: " + currentIndex);
            Card nextCard = drawPile[currentIndex];
            handManager.AddCardToHand(nextCard);

            drawPile.RemoveAt(currentIndex);
            UpdateDrawPileCount();

            if (drawPile.Count > 0)
            {
                currentIndex %= drawPile.Count;
            }
           
        }


    }

    private void RefullDeckFromDiscard()
    {
        if (discardManager == null)
        {
            discardManager = FindObjectOfType<DiscardManager>();
        }

        if (discardManager != null && discardManager.discardCardsCount > 0)
        {
            drawPile = discardManager.PullAllFromDiscard();
            Utility.Shuffle(drawPile);
            currentIndex = 0;
        }

    }

    private void UpdateDrawPileCount()
    {
        drawPileCounter.text = drawPile.Count.ToString();
    }




}
