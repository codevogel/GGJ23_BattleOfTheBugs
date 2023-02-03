using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

public class SunLightMovement : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    private UnityEvent moveSunLightEvent;
    public UnityEvent playerInSun;
    public float maxDistance = 20;
    public float timeToMoveMin = 5;
    public float timeToMoveMax = 10;
    public float movingSpeed = 1;
    private float time = 0;
    private float newPos;

    private void Start()
    {
        if (moveSunLightEvent == null)
            moveSunLightEvent = new UnityEvent();

        if (playerInSun == null)
            playerInSun = new UnityEvent();

        playerInSun.AddListener(GiveHealthPlayer);

        moveSunLightEvent.AddListener(MoveSunLigth);
    }

    //Change sunlight to random position
    private void MoveSunLigth()
    {
        float randomPos = Random.Range(-maxDistance, maxDistance);
        newPos = transform.position.x + randomPos;
    }

    //Pseudo code for giving healt player
    private void GiveHealthPlayer()
    {
        //give player health
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Probaly use playerscript here but no player yet so this is fine for pseudo code
        if (collision.gameObject.tag == "player")
            playerInSun.Invoke();
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(newPos, transform.position.y), movingSpeed * Time.deltaTime);
    }

    void Update()
    {
        time -= Time.deltaTime;
        if(time < 0)
        {
            time = Random.Range(timeToMoveMin, timeToMoveMax);
            moveSunLightEvent.Invoke();
        }
    }
}
