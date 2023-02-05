using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (collision.gameObject.tag != "RootEnd") return;
        if (SceneManager.GetActiveScene().name == "Level1 Tut")
        {
            SceneManager.LoadScene("Level1");
        } else
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
