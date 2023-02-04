using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject RightSpawner;
    public GameObject LeftSpawner;
    public GameObject EnemyPref;
    public float spawnDelay = 2f;
    private GameObject activeSpawn;
    public Transform leftTarget;
    public Transform rightTarget;
    private Transform target;
    private int scale;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(spawnEnemy());
    }


    private IEnumerator spawnEnemy()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            //pseudo random spawn location
            float y = Random.Range(0, 10);
            if(y < 5)
            {
                activeSpawn = LeftSpawner;
                target = leftTarget;
                scale = 1;
            } else
            {
                activeSpawn = RightSpawner;
                target = rightTarget;
                scale = -1;
            }
            GameObject enemy = Instantiate(EnemyPref, activeSpawn.transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyBehaviour>().target = target;
            enemy.GetComponent<EnemyBehaviour>().scale = scale;
            yield return new WaitForSeconds(spawnDelay);
        }
        
    }
}
