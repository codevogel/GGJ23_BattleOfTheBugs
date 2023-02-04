using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float Health = 10f;
    public float MovementSpeed = 0.5f;
    public Transform target;
    private Vector3 targetPos;
    public int scale;

    private void Start()
    {

    }

    private void Awake()
    {
        
        
    }

    private void Update()
    {
        Vector3 newScale = transform.localScale;
        newScale.x = scale;
        transform.localScale = newScale;
        targetPos = target.transform.position;
        
        Vector3 newXpos = new Vector3(targetPos.x, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, newXpos, MovementSpeed * Time.deltaTime);
    }

}
