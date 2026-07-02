using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridCell : MonoBehaviour
{
    public Vector2 gridIndex;
    public bool cellFull = false;
    public GameObject objectInCell;

    SpriteRenderer sprite;
    public Color highlightColor = Color.cyan;
    public Color posColor = Color.green;
    public Color negColor = Color.red;
    private Color originalColor;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    void OnMouseEnter()
    {
        if (!GameManager.Instance.PlayingCard)
        {
            sprite.color = highlightColor;
        }
        else if (cellFull || gridIndex.x > 1)
        {
            sprite.color = negColor;
        }
        else
        {
            sprite.color = posColor;
        }
    }

    void OnMouseExit()
    {
        sprite.color = originalColor;
    }

    private void OnMouseOver()
    {
        //Debug.Log(gridIndex.x + " " + gridIndex.y);
        //Debug.Log(cellFull);
        //Debug.Log("===========================================");
    }



}
