using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RootMotor : MonoBehaviour
{

    [SerializeField]
    private float _motorSpeed;

    private Vector2 _inputVector;

    private Rigidbody2D _rb2d;

    public bool CanMove { private get; set; }
    private void Awake()
    {
	    EventManager.OnPlayer2MovePerformed += AimTowards;
	    EventManager.OnPlayer2MoveCanceled += StopAimingTowards;
        _rb2d = GetComponent<Rigidbody2D>();    
    }
    private void FixedUpdate()
    {
        if (CanMove)
        {
            _rb2d.velocity = _inputVector * Time.deltaTime * _motorSpeed;
        }
        ClampY();
    }

    private void ClampY()
    {
        if (_rb2d.position.y > 0)
        {
            _rb2d.position = new Vector2(_rb2d.position.x, 0);
        }
    }

    private void AimTowards(Vector2 rawInput)
    {
        _inputVector = rawInput;
    }

    private void StopAimingTowards()
    {
	    _inputVector = Vector2.zero;
    }
}
