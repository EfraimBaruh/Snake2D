using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleLifeSpan : MonoBehaviour
{
    private float _ediblelife;

    public float EdibleLife
    {
        get { return _ediblelife; }
        set { _ediblelife = value; }
    }
    
    void Start()
    {
        StartCoroutine(DemolishEdible());
    }

    private IEnumerator DemolishEdible()
    {
        yield return new WaitForSeconds(_ediblelife);
        
        Destroy(gameObject);
    }
}
