using Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArcRenderer : MonoBehaviour
{
    public GameObject arrowPrefab; // The arrow head
    public GameObject dotPrefab; // The dots
    public int poolSize = 50; // the size of our dot pool
    private List<GameObject> dotPool = new List<GameObject>(); // The dot pool
    private GameObject arrowInstance;  //holds a referance to the arrow head

    public float spacing = 50; // the spacing of the dots
    public float arrowAngleAdjustment = 0; // the andlge correction for the arrowhead
    public int dotsToSkip = 1;  // the number of dots to skip to give the arrowhead space
    private Vector3 arrowDirection; // hodl the position the arrowhead needs to point from 

    public float baseScreenWidth = 1920f;
    [SerializeField] private float spacingScale;

    void Start()
    {
        arrowInstance = Instantiate(arrowPrefab, transform);
        arrowInstance.transform.localPosition = Vector3.zero;
        InitializeDotPool(poolSize);

        spacingScale = Screen.width / baseScreenWidth;
    }
    
    private void OnEnable()
    {
        spacingScale = Screen.width / baseScreenWidth;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 0;

        Vector3 startPos = transform.position; // the obj position
        Vector3 midPoint = CalculateMidPoint(startPos, mousePos);

        UpdateArc(startPos, midPoint, mousePos);
        PositionAndRotateArrow(mousePos);
    }

    private void InitializeDotPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject dot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity, transform);
            dot.SetActive(false);
            dotPool.Add(dot);
        }
    }

    private Vector3 CalculateMidPoint(Vector3 start, Vector3 end)
    {
        Vector3 midpoint = (start + end) / 2; // this finds the linear midpoint, we want an arc

        // Add the hight of the arc
        float arcHeight = Vector3.Distance(start, end) / 3f;
        midpoint.y += arcHeight;

        return midpoint;

    }

    private Vector3 QuadracticBeizerPoint(Vector3 start, Vector3 control, Vector3 end, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        
        Vector3 point = uu * start;
        point += 2 * u * t * control;
        point += tt * end;

        return point;
    }

    private void UpdateArc(Vector3 start, Vector3 mid, Vector3 end)
    {
        int numDots = Mathf.CeilToInt(Vector3.Distance(start, end) / (spacing * spacingScale));

        for (int i = 0; i < numDots && i < dotPool.Count; i++)
        {
            // Make a Bezier curve
            float t = i / (float)numDots;
            t = Math.Clamp(t, 0f, 1f); // Make sure t is between 0 and 1

            Vector3 position = QuadracticBeizerPoint(start, mid, end, t);

            // Check if the dot is not apart of the arrowhead
            if (i != numDots - dotsToSkip)
            {
                dotPool[i].transform.position = position;
                dotPool[i].SetActive(true);
            }
            // Set the last couple dots as the arrowhead
            if (i == numDots - (dotsToSkip + 1) && i - dotsToSkip + 1 >= 0)
            {
                arrowDirection = dotPool[i].transform.forward;
            }

        }
        // Turn off unused dots
        for (int i = numDots - dotsToSkip; i < dotPool.Count; i++)
        {
            if (i > 0)
            {
                dotPool[i].SetActive(false);
            }
        }
    }


    private void PositionAndRotateArrow(Vector3 position)
    {
        arrowInstance.transform.position = position;

        Vector3 direction = arrowDirection - position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle += arrowAngleAdjustment;
        arrowInstance.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
