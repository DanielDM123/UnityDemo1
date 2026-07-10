using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Project;

// This file connects the templates we created in CardData folder 
// onto the card we see in game
public class CardDisplay : MonoBehaviour
{
    // === All Card Element ===

    // The card template from the CardData folder 
    public Card cardData;

    // The values we want the card in game to show
    public Image cardImage;
    public TMP_Text nameText;
    public Image[] typeImages;
    public Image displayImage;
    public GameObject characterElements;
    public GameObject spellElements;
    public GameObject characterCardLabel;
    public GameObject spellCardLabel;
    public TMP_Text descriptionText;


    // === Character Card Element ===
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image damageImage;

    // === Spell Card Element ===
    public GameObject[] spellTypelabels;
    public GameObject[] attributeTargetSymbols;
    public float attributeSymbolSpacing = 10f;
    public TMP_Text attributeChangeAmountText;



    private Color[] cardColors =
    {
        new Color(0.54f, 0f, 0f), // Fire
        new Color(0.51f, 0.34f, 0.17f), // Earth
        new Color(0f, 0f, 0.54f), // Water
        new Color(0.23f, 0.06f, 0.21f), // Dark
        new Color(0.65f, 0.61f, 0f), // Light
        new Color(0.63f, 0.82f, 0.82f) // Air
    };

    private Color[] typeColors =
    {
        Color.red, // Fire
        new Color(0.8f, 0.52f, 0.24f), // Earth
        Color.blue, // Water
        new Color(0.47f, 0f, 0.4f), // Dark
        Color.yellow, // Light
        new Color(0.83f, 0.94f, 0.94f) // Air
    };
    
    //void Start()
    //{
    //    UpdateCardDisplay();
    //    // Debug.Log(damageText);
    //}

    // Read from the card template and store them into the vars
    // so that we can display it
    public void UpdateCardDisplay()
    {
        // All card changes
        cardImage.color = cardColors[(int)cardData.cardType[0]];
        nameText.text = cardData.cardName;
        displayImage.sprite = cardData.cardSpirte;
        descriptionText.text = cardData.description;

        // Update the type images
        for (int i = 0; i < typeImages.Length; i++)
        {
            if (i < cardData.cardType.Count)
            {
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = typeColors[(int)cardData.cardType[i]];

                //Debug.Log("Index: " + i);
                //Debug.Log(cardData.cardType[i]);
                //Debug.Log((int)cardData.cardType[i]); // This will return the index of the type, not convert the string into an int
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }
        }

        // Specific card changes
        if (cardData is Character characterCard)
        {
            UpdateDisplayCharacterCard(characterCard);
        }

        if (cardData is Spell spellCard)
        {
            UpdateDisplaySpellCard(spellCard);
        }


    }

    private void UpdateDisplayCharacterCard(Character characterCard)
    {
        //spellElements.SetActive(false);
        //characterElements.SetActive(true);
        //characterCardLabel.SetActive(true);

        damageImage.color = typeColors[(int)characterCard.damageType[0]];
        healthText.text = characterCard.health.ToString();
        damageText.text = $"{characterCard.damageMin}-{characterCard.damageMax}";
    }


    private void UpdateDisplaySpellCard(Spell spellCard)
    {
        //spellElements.SetActive(true);
        //characterElements.SetActive(false);
        //spellCardLabel.SetActive(true);

        // Set correct spell type lable
        foreach (GameObject label in spellTypelabels)
        {
            label.SetActive(false);
        }
        //spellTypelabels[(int)spellCard.spellType].SetActive(true);

        // Reset and update attribute target symbols
        foreach (GameObject symbol in attributeTargetSymbols)
        {
            symbol.SetActive(false);
        }

        for (int i = 0; i < spellCard.attributeTarget.Count; i++)
        {
            GameObject currentSymbol = attributeTargetSymbols[(int)spellCard.attributeTarget[i]];
            currentSymbol.SetActive(true);
            float newYPosition = i * attributeSymbolSpacing;
            currentSymbol.transform.localPosition = new Vector3(0, newYPosition, 0);

        }

        attributeChangeAmountText.text = string.Join(", ", spellCard.attributeChangeAmount);

    }
}
