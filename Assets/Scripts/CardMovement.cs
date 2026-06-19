using UnityEngine;
using UnityEngine.EventSystems;
using Project;
using System.Collections;
using System.Collections.Generic;
using System;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform; // Current transform of the obj
    private Canvas canvas; // Parent canvas
    private Vector2 originalLocalPointerPosition; // mouse position
    private Vector3 originalPanelLocalPosition; // og location of the card
    private Vector3 originalScale;
    private int currentState = 0;
    private Quaternion originalRotation; // og rotation of the card
    private Vector3 originalPosition;  // og position of the 

    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private Vector3 cardPlay;
    [SerializeField] private Vector3 playPosition; 
    [SerializeField] private GameObject glowEffect; //  stores the highlight png?
    [SerializeField] private GameObject playArrow;
    [SerializeField] private float lerpFactor = 0.05f;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;


    }

    void Update()
    {
        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState(); // Check if mouse button is released
                if (!Input.GetMouseButton(0))
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();
                if (!Input.GetMouseButton(0))
                {
                    TransitionToState0();
                }
                break;

        }

    }

    // Set the card back to the original positions (the hand)
    private void TransitionToState0()
    {
        currentState = 0;
        rectTransform.localScale = originalScale; // Reset Scale
        rectTransform.localPosition = originalPosition; // Reset position
        rectTransform.localRotation = originalRotation; // Reset rotation 
        glowEffect.SetActive(false);
        playArrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalScale = rectTransform.localScale;
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;

            currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            TransitionToState0();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
            originalPanelLocalPosition = rectTransform.localPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
   {
        if (currentState == 2)
        {
            // Makes sure the the card is following the card on screen in the game window instead of just the coords of the screen
            // EP 5 @ 37:58   
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);

                if (rectTransform.localPosition.y > cardPlay.y)
                {
                    currentState = 3;
                    playArrow.SetActive(true);
                    rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFactor);
                }
            }
        }
    }

    private void HandleHoverState()
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDragState()
    {
        // Set the card's rotation to zero
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
}
