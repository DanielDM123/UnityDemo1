using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Test : MonoBehaviour
{
    int n = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(n);
        n++;
    }
}
