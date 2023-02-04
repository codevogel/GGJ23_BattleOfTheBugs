using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float MovementSpeed = 2f;
    public Transform target;
    private Vector3 targetPos;
    public int scale;
    public float health = 1f;
    public float damage = 20f;
    Coroutine damageRoutine;


    private void Update()
    {
        Vector3 newScale = transform.GetChild(0).localScale;
        newScale.x = scale;
        transform.GetChild(0).localScale = newScale;
        targetPos = target.transform.position;
        
        Vector3 newXpos = new Vector3(targetPos.x, targetPos.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, newXpos, MovementSpeed * Time.deltaTime);

        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.transform.tag == "Player")
        {
            damageRoutine = StartCoroutine(DamageTree(damage));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            StopCoroutine(damageRoutine);
        }
    }

    private IEnumerator DamageTree(float damage)
    {
        while (true)
        {
            GameManager.Instance.gameObject.GetComponent<Energy>().DecreaseEnergy(damage);
            yield return new WaitForSeconds(1.5f);
        }
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
