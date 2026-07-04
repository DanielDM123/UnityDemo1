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
    private DrawPileManager drawPileManager;

    public int startingHandSize = 6;
    public int maxHandSize = 12;
    public int currentHandSize;
    private bool startBattleRun = true;

    void Start()
    {
        // Load all assests from the Resource folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        // Put the loaded cards into the allCards list
        allCards.AddRange(cards);


    }

    void Awake()
    {
        if (drawPileManager == null)
        {
            drawPileManager = FindObjectOfType<DrawPileManager>();
        }

        if (handManager == null)
        {
            handManager = FindObjectOfType<HandManager>();
        }
    }

    void Update()
    {
        if (startBattleRun)
        {
            BattleSetup();
        }
    }


    public void BattleSetup()
    {
        handManager.BattleSetup(maxHandSize);
        Debug.Log("allCards size: " + allCards.Count);
        drawPileManager.MakeDrawPile(allCards);
        drawPileManager.BattleSetup(startingHandSize, maxHandSize);
        startBattleRun = false;
    }
}
