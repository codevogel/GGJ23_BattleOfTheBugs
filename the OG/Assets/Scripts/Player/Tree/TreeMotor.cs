using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMotor : MonoBehaviour
{

    [SerializeField]
    private float _motorSpeed;
    [SerializeField]
    [Range(.01f, 1)]
    private float _brakeFactor;

    private Rigidbody2D b_rb2d;
    public Rigidbody2D Body { get { return b_rb2d; } private set { b_rb2d = value; } }

    private float _rawInput;

    private void Awake()
    {
	    EventManager.OnPlayer1MovePerformed += MoveInDirection;
	    EventManager.OnPlayer1MoveCanceled += ClearRawInput;
	}

    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Body.velocity = new Vector2(_rawInput * _motorSpeed * Time.deltaTime, 0);
    }

    public void MoveInDirection(float axisRaw)
    {
        _rawInput = axisRaw;
    }

    private void OnDestroy()
    {
	    EventManager.OnPlayer1MovePerformed -= MoveInDirection;
	    EventManager.OnPlayer1MoveCanceled -= ClearRawInput;
    }

    private void ClearRawInput()
    {
        _rawInput = 0;
    }
}
