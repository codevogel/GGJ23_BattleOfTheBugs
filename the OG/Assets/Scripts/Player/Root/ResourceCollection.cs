using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour
{

    [SerializeField]
    private List<Transform> randomSpawns;


    private void Awake()
    {
        transform.position = randomSpawns[Random.Range(0, randomSpawns.Count)].position;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
		if(coll.gameObject.tag != "RootEnd") return;
        GameStateManager.LoadScene("WinScene");
    }
}
