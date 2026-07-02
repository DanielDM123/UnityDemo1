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
    private RectTransform canvasRectTransform;
    private Vector3 originalScale;
    private int currentState = 0;
    private Quaternion originalRotation; // og rotation of the card
    private Vector3 originalPosition;  // og position of the 

    private readonly int maxColumn = 2;

    private GridManager gridManager;

    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private Vector3 cardPlay;
    [SerializeField] private Vector3 playPosition; 
    [SerializeField] private GameObject glowEffect; //  stores the highlight png?
    [SerializeField] private GameObject playArrow;
    [SerializeField] private float lerpFactor = 0.05f;

    [SerializeField] private int cardPlayerDivider = 4;
    [SerializeField] private float cardPlayMultiplier = 1f;
    [SerializeField] private bool needUpdateCardPlayPosition = false; // for debugging
    [SerializeField] private int playPositionYDivider = 2;
    [SerializeField] private float playPositionYMultiplier = 1f;
    [SerializeField] private int playPositionXDivider = -5;
    [SerializeField] private float playPositionXMultiplier = 3f;
    [SerializeField] private bool needUpdatePlayPosition = false; // for debugging



    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;

        updateCardPlayPosition();
        updatePlayPosition();
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        
        // For debugging
        if (needUpdateCardPlayPosition)
        {
            updateCardPlayPosition();
        }
        if (needUpdatePlayPosition)
        {
            updatePlayPosition();
        }


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

                break;

        }

    }

    // Set the card back to the original positions (the hand)
    private void TransitionToState0()
    {
        currentState = 0;
        GameManager.Instance.PlayingCard = false;
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

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2)
        {
            if (Input.mousePosition.y > cardPlay.y)
            {
                currentState = 3;
                playArrow.SetActive(true);
                rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFactor);
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
        rectTransform.position = Vector3.Lerp(rectTransform.position,Input.mousePosition, lerpFactor);
    }

    private void HandlePlayState()
    {
        if (!GameManager.Instance.PlayingCard)
        {
            GameManager.Instance.PlayingCard = true;
        }

        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if (!Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if we let go on a gridcell object 
            if (hit.collider != null && hit.collider.GetComponent<GridCell>())
            {
                GridCell cell = hit.collider.GetComponent<GridCell>();
                Vector2 targetPos = cell.gridIndex;

                // Check to see if we can add the character to the cell 
                if (gridManager.AddObjectToGrid(GetComponent<CardDisplay>().cardData.prefab, targetPos))
                {
                    HandManager handManager = FindAnyObjectByType<HandManager>();
                    handManager.cardsInHand.Remove(gameObject);
                    handManager.UpdateHandVisuals();
                    Destroy(gameObject);
                }
            }
            TransitionToState0();
        }

        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }

    // Finds the threshold for the card play position depending on the aspect ratio of the screen
    private void updateCardPlayPosition()
    {
        if (cardPlayerDivider != 0 && canvasRectTransform != null)
        {
            float segment = cardPlayMultiplier / cardPlayerDivider;

            cardPlay.y = canvasRectTransform.rect.height * segment;


        }
    }


    private void updatePlayPosition()
    {
       
        if (canvasRectTransform != null && playPositionYDivider != 0 && playPositionXDivider != 0)
        {
            float segmentX = playPositionXMultiplier / playPositionXDivider;
            float segmentY = playPositionYMultiplier / playPositionYDivider;

            playPosition.x = canvasRectTransform.rect.height * segmentX;
            playPosition.y = canvasRectTransform.rect.height * segmentY;


        }
    }


}
