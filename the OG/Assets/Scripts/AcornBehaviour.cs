using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornBehaviour : MonoBehaviour
{
    
    public float damage = 1;
    public float speed = 10;
    Rigidbody2D rigidbody2;
    public Vector2 moveVector;

    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidbody2.velocity = moveVector * Time.deltaTime * speed;
    }
}
