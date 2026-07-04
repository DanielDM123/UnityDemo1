using UnityEngine;
using Project;
using System.Collections;
using System.Collections.Generic;
using System;


public class HandManager : MonoBehaviour
{
    
    public GameObject cardPrefab; // Assigns card prefab in inspector
    public Transform handTransform; // Root of hand position
    public float fanSpread = -7.5f;
    public float cardSpacing = 100f;
    public float verticalSpacing = 100f;
    public int maxHandSize; // max size = Normal draw size * 2

    public List<GameObject> cardsInHand = new List<GameObject>();// Hold a list of card objs


    void Start()
    {

        
    }
    void Update()
    {
        // UpdateHandVisuals();
    }
    public void BattleSetup(int setMaxHandSize)
    {
        maxHandSize = setMaxHandSize;
    }


    public bool AddCardToHand(Card cardData)
    {
        // Check if we are at the max number of cards our hand can hold
        if (cardsInHand.Count >= maxHandSize)
        {
            // Skip function and don't add a new card to the hand
            Debug.Log("Reached Max Number of Cards in Hand!");
            UpdateHandVisuals();
            return false; 
        }

        // Craete a new card obj
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        // Add the data from the inputed card into the new object we just created
        newCard.GetComponent<CardDisplay>().cardData = cardData;
        newCard.GetComponent<CardDisplay>().UpdateCardDisplay();

        UpdateHandVisuals();
        return true;
    
    }


    public void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        // Divide by zero edge case
        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            // Find the rotation of each card and set it
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            // Find the x position of each card
            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
            // Find the y position of the arch of each card
            float normalizedPosition = (2f * i / (cardCount - 1) - 1f); // Normalize card position between -1 and 1
            float verictalOffset = verticalSpacing * (1 - normalizedPosition * normalizedPosition); // Makes a parabola (1 - x^2)
            
            // Set card position 
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verictalOffset, 0f);
        }
    }

    public void setMaxHandSize(int count) { maxHandSize = count; }
    public int getMaxHandSize() { return maxHandSize; }
}
