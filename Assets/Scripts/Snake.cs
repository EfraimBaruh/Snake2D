using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public void DetectAction(Collider2D col)
    {
        switch (col.tag)
        {
            case "increase":
                Debug.Log("increase");
                break;
            case "decrease":
                Debug.Log("decrease");
                break;
            case "slowdown":
                Debug.Log("slowdown");
                break;
            case "speedup":
                Debug.Log("slowdown");
                break;
        }
    }
    
}
