using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Snake : MonoBehaviour
{
    public static Snake Singleton;
    public float temporaryVelocityTime = 2f;
    public Movement Movement;
    public ScoreHandler ScoreHandler;

    public GameObject tail;
    public Transform leftWall, rightWall, upWall, downWall;
    private List<Transform> segments;
    private bool inDieProcess = false;

    public Action onSnakeEates, onSnakeDies;

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        // Due to execution order, score handler supposed to add action listeners after singleton is assigned.
        ScoreHandler.gameObject.SetActive(true);

        segments = new List<Transform>();
        segments.Add(transform);
        
        UpdateTail();
    }

    private async void UpdateTail()
    {
        if (!inDieProcess)
        {
            float tailDelay = (Movement.CurrentVelocity * -9.761f) + 308.15f;

            await Task.Delay((int)tailDelay);

            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
            }

            UpdateTail();
        }

    }

    public void DetectAction(Collider2D col)
    {
        switch (col.tag)
        {
            case "increase":
                Increase();
                break;
            case "decrease":
                Decrease();
                break;
            case "slowdown":
                SlowDown();
                break;
            case "speedup":
                SpeedUp();
                break;
            case "reverse":
                break;
            case "tail":
                SnakeDies(col);
                return;
            case "wall":
                WallLoop(col);
                return;
        }
        
        
        // eat and respawn edibles.
        Destroy(col.gameObject);
        onSnakeEates.Invoke();
    }

    private void Increase()
    {
        GameObject tail = Instantiate(this.tail);

        Vector3 tail_pos = segments[segments.Count - 1].position;
        
        tail.transform.position = tail_pos;
        segments.Add(tail.transform);
    }

    private void Decrease()
    {
        // avoid the possibility of first edible coming with the tag decrease.
        if (segments.Count > 1)
        {
            Transform lastTail = segments[segments.Count - 1];

            segments.Remove(lastTail);
            Destroy(lastTail.gameObject);
        }
    }

    private void SlowDown()
    {
        StartCoroutine(VelocityChange(Movement.CurrentVelocity * 0.66f));
    }

    private void SpeedUp()
    {
        StartCoroutine(VelocityChange(Movement.CurrentVelocity * 1.5f));
    }

    private void WallLoop(Collider2D wall)
    {
        if (wall.transform == leftWall)
        {
            StartCoroutine(TempDisableCollider(rightWall.GetComponent<Collider2D>()));
            transform.position = new Vector3(rightWall.position.x, transform.position.y, transform.position.z);
        }
        else if (wall.transform == rightWall)
        {
            StartCoroutine(TempDisableCollider(leftWall.GetComponent<Collider2D>()));
            transform.position = new Vector3(leftWall.position.x, transform.position.y, transform.position.z);
        }
        else if (wall.transform == upWall)
        {
            StartCoroutine(TempDisableCollider(downWall.GetComponent<Collider2D>()));
            transform.position = new Vector3(transform.position.x, downWall.transform.position.y, transform.position.z);
        }
        else if (wall.transform == downWall)
        {
            StartCoroutine(TempDisableCollider(upWall.GetComponent<Collider2D>()));
            transform.position = new Vector3(transform.position.x, upWall.transform.position.y, transform.position.z);
        }

        

    }

    private void SnakeDies(Collider2D col)
    {
        if (segments.Count < 7) return;
        
        if(col.transform == segments[1] || col.transform == segments[2]) return;

        Movement.ChangeSpeed(0);
        StartCoroutine(SnakeDieProcess());

    }

    private IEnumerator TempDisableCollider(Collider2D collider2D)
    {
        collider2D.enabled = false;
        yield return new WaitForSeconds(0.3f);
        collider2D.enabled = true;
    }

    private IEnumerator SnakeDieProcess()
    {
        inDieProcess = true;
        
        yield return null;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < segments.Count; j++)
            {
                segments[j].GetComponent<Renderer>().enabled = i % 2 == 1;
            }

            yield return new WaitForSeconds(0.3f);
        }

        for (int i = segments.Count-1; i > 0; i--)
        {
            Destroy(segments[i].gameObject);
        }
        
        segments.Clear();
        segments.Add(transform);
        
        onSnakeDies.Invoke();

        Movement.ChangeSpeed(Movement.InitialVelocity);
        inDieProcess = false;
        UpdateTail();
    }

    private IEnumerator VelocityChange(float adjustedSpeed)
    {
        float velocity = Movement.CurrentVelocity;
        Movement.ChangeSpeed(adjustedSpeed);

        yield return new WaitForSeconds(temporaryVelocityTime);
        Movement.ChangeSpeed(velocity);

    }
    
}
