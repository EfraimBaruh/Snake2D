using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private List<String> detectables;
    public Events.CollisionEvent2D onTriggerEnter2D, onTriggerStay2D, onTriggerExit2D;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (CompareTag(col))
            onTriggerEnter2D.Invoke(col);
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (CompareTag(col))
            onTriggerStay2D.Invoke(col);
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        if (CompareTag(col))
            onTriggerExit2D.Invoke(col);
    }

    
    /// <summary>
    /// Compare collider tag with detectable ones.
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    private bool CompareTag(Collider2D col)
    {
        foreach (var tag in detectables)
        {
            if (col.CompareTag(tag))
                return true;
        }

        return false;
    }
}
