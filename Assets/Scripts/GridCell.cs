using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Vector2 gridIndex;
    public bool cellFull = false;
    public GameObject objectInCell;

    SpriteRenderer sprite;
    public Color highlightColor = Color.red;
    private Color originalColor;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    private void OnMouseOver()
    {
        sprite.color = highlightColor;
    }

    private void OnMouseExit()
    {
        sprite.color = originalColor;
    }

}
