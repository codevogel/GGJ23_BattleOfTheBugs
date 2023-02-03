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

    private Rigidbody2D _rb2d;

    private float _rawInput;
    private bool _isBraking;

    private void Awake()
    {
	    EventManager.OnPlayer1MovePerformed += MoveInDirection;
	    EventManager.OnPlayer1MoveCanceled += StartBraking;
	}

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isBraking)
        {
            _rb2d.velocity *= (1 - _brakeFactor);
            return;
        }
        _rb2d.AddForce(new Vector2(_rawInput * _motorSpeed * Time.deltaTime, 0));
    }

    public void MoveInDirection(float axisRaw)
    {
	    StopBraking();
        _rawInput = axisRaw;
    }

    public void StopBraking()
    {
        _isBraking = false;
    }

    public void StartBraking()
    {
        _isBraking = true;
    }

    private void OnDestroy()
    {
	    EventManager.OnPlayer1MovePerformed -= MoveInDirection;
	    EventManager.OnPlayer1MoveCanceled -= StartBraking;
    }
}
