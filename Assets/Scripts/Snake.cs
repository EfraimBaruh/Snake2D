using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
    public static Snake Singleton;
    public float temporaryVelocityTime = 2f;
    public Movement Movement;
    public ScoreHandler ScoreHandler;

    public Action onSnakeEates, onSnakeDies;

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        // Due to execution order, score handler supposed to add action listeners after singleton is assigned.
        ScoreHandler.gameObject.SetActive(true);
    }

    public void DetectAction(Collider2D col)
    {
        switch (col.tag)
        {
            case "increase":
                Increase();
                Debug.Log("increase");
                break;
            case "decrease":
                Decrease();
                Debug.Log("decrease");
                break;
            case "slowdown":
                SlowDown();
                Debug.Log("slowdown");
                break;
            case "speedup":
                SpeedUp();
                Debug.Log("slowdown");
                break;
        }
        
        
        // eat and respawn edibles.
        Destroy(col.gameObject);
        onSnakeEates.Invoke();
    }

    private void Increase()
    {
        
    }

    private void Decrease()
    {
        
    }

    private void SlowDown()
    {
        StartCoroutine(VelocityChange(Movement.CurrentVelocity * 0.66f));
    }

    private void SpeedUp()
    {
        StartCoroutine(VelocityChange(Movement.CurrentVelocity * 1.5f));
    }

    private IEnumerator VelocityChange(float adjustedSpeed)
    {
        float velocity = Movement.CurrentVelocity;
        Movement.ChangeSpeed(adjustedSpeed);

        yield return new WaitForSeconds(temporaryVelocityTime);
        Movement.ChangeSpeed(velocity);

    }
    
}
