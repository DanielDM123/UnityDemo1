using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Project;

// This file connects the templates we created in CardData folder 
// onto the card we see in game
public class CardDisplay : MonoBehaviour
{
    // The card template from the CardData folder 
    public Card cardData;

    // The values we want the card in game to show
    public Image cardImage;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image[] typeImages;
    public Image damageImage;
    public Image displayImage;

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
        // Update the background of the card based on the first card type
        cardImage.color = cardColors[(int)cardData.cardType[0]];
        damageImage.color = typeColors[(int)cardData.damageType[0]];

        // Update the string & int values from the template
        nameText.text = cardData.cardName;
        //nameText.text = "asdf";
        healthText.text = cardData.health.ToString();
        damageText.text = $"{cardData.damageMin}-{cardData.damageMax}";
        displayImage.sprite = cardData.cardSpirte;


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
    }

}
