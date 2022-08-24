using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
        EdibleEffect();
        StartCoroutine(DemolishEdible());
    }

    private void EdibleEffect()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(0.4f, 0.3f))
            .Append(transform.DOScale(0.3f, 0.3f)).SetLoops(-1);
    }

    private IEnumerator DemolishEdible()
    {
        yield return new WaitForSeconds(_ediblelife);
        
        Destroy(gameObject);
    }
}
