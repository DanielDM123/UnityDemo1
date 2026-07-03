using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] // typeof(gridCell)
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

    public GameObject[] backgrounds;
    private bool setbackground = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    void Update()
    {
        if (!setbackground)
        {
            SetBackground();
        }
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

    private void SetBackground()
    {
        if (gridIndex.x % 2 != 0)
        {
            backgrounds[0].SetActive(true);
        }

        if (gridIndex.y % 2 != 0)
        {
            backgrounds[1].SetActive(true);
        }
        setbackground = true;
    }



}
