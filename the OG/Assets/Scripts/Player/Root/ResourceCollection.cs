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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Michael");
        if (collision.gameObject.tag != "RootEnd") return;
        GameStateManager.LoadScene("WinScene");
    }
}
