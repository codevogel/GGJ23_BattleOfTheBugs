using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField]
    private List<Obstacle> _obstaclePrefabs;
    public List<Obstacle> Obstacles = new List<Obstacle>();

	private Obstacle RandomObstacle { get => _obstaclePrefabs[Random.Range(0, _obstaclePrefabs.Count)]; }

    [SerializeField]
    private Vector2 bounds;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, bounds * 2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnRandomObstacles(1);
        }
    }

    public void SpawnRandomObstacles(int amountOfObstacles)
    {
        for (int i = 0; i < amountOfObstacles; i++)
        {
            Vector3 newPos = new Vector3(
                    transform.position.x + Random.Range(-bounds.x, bounds.x),
                    transform.position.y + Random.Range(-bounds.y, bounds.y),
                    transform.position.z);

            Obstacle randomObstacle = RandomObstacle;

            RaycastHit hit;
            int tries = 0;
            while (tries < 100 && Physics2D.OverlapCircleAll(newPos, randomObstacle.size, LayerMask.GetMask("Obstacle")).Length > 0)
            {
                newPos = new Vector3(
                    transform.position.x + Random.Range(-bounds.x, bounds.x),
                    transform.position.y + Random.Range(-bounds.y, bounds.y),
                    transform.position.z);
                tries++;
            }

            if (tries == 100)
            {
                //throw new NotImplementedException("Tried placing an obstacle 100 times but none of the locations were plausible locations.");
            }
            var ob = GameObject.Instantiate(
                RandomObstacle,
                newPos,
                Quaternion.identity,
                this.transform
                );
            Obstacles.Add(ob);
        }
    }

    public void DeleteAllObstacles()
    {
	    foreach (var ob in Obstacles)
	    {
		    Destroy(ob.gameObject);
	    }

	    Obstacles = new List<Obstacle>();
    }

}
