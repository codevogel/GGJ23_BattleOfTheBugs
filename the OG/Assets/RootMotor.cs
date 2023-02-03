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

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        AimTowards(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = _inputVector * Time.deltaTime * _motorSpeed;
    }

    public void AimTowards(Vector2 rawInput)
    {
        _inputVector = rawInput;
    }
}
