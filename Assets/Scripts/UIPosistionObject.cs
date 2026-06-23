using UnityEngine;

public class UIPosistionObject : MonoBehaviour
{
    public RectTransform objectToPosition;

    public int widthDivider = 2;
    public int heightDivider = 2;
    public float widthMultiplier = 1f;
    public float heightMultiplier = 0f;

    public bool updatePosition = false; // used for debugging 

    void Start()
    {
        SetUIObjectPosition();
    }

    void Update()
    {
        if (updatePosition)
        {
            SetUIObjectPosition();
        }
    }

    public void SetUIObjectPosition()
    {
        if (objectToPosition != null && widthDivider != 0 && heightDivider !=0)
        {
            // Calculate the anchor position 
            float anchorX = widthMultiplier / widthDivider;
            float anchorY = heightMultiplier / heightDivider;

            // Set the anchor and pivot 
            objectToPosition.anchorMin = new Vector2 (anchorX, anchorY);
            objectToPosition.anchorMax = new Vector2(anchorX, anchorY);
            objectToPosition.pivot = new Vector2(0.5f, 0.5f);

            // Set the loacl position to zero to align with the anchor point
            objectToPosition.anchoredPosition = Vector2.zero;

        }
    }
}
