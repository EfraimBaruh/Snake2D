using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public Action MovementUpdate;
    
    [SerializeField] private float initialVelocity = 1f;
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;
    private float _currentVelocity;
    
    public float CurrentVelocity
    {
        get { return _currentVelocity; }
    }
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
        // start movement in random direction.
        _direction = Utils.GetRandomDirection2D();
        _currentVelocity = initialVelocity;
        MovementUpdate.Invoke();
    }

    private void Update()
    {
#if UNITY_EDITOR
        
        // TODO: tail movement will be added.
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Turn(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            Turn(Vector2.right);
        else if(Input.GetKeyDown(KeyCode.UpArrow))
            Turn(Vector2.up);
        else if(Input.GetKeyDown(KeyCode.DownArrow))
            Turn(Vector2.down);
#endif
    }


    private void Turn(Vector2 dir)
    {
        _direction = dir;
        MovementUpdate.Invoke();
    }
    public void ChangeSpeed(float velocity)
    {
        _currentVelocity = velocity;
        MovementUpdate.Invoke();
    }
    private void UpdateVelocity()
    {
        _rigidbody2D.velocity = _direction * _currentVelocity;

    }

    private void OnEnable()
    {
        MovementUpdate += UpdateVelocity;
    }
    
    private void OnDisable()
    {
        MovementUpdate -= UpdateVelocity;
    }
}
