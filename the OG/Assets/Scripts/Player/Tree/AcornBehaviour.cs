using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornBehaviour : MonoBehaviour
{
    
    public float damage = 1;
    public float speed = 10;
    Rigidbody2D rigidbody2;
    public Vector2 moveVector;
    private EnemyBehaviour enemyBehaviour;

    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            Destroy(gameObject);

        if (collision.gameObject.tag == "Enemy")
        {
            enemyBehaviour = collision.gameObject.GetComponent<EnemyBehaviour>();
            enemyBehaviour.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rigidbody2.velocity = moveVector * Time.deltaTime * speed;
    }
}
