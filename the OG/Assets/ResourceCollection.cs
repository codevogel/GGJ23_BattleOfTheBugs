using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCollection : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D coll)
    {
		if(coll.gameObject.tag != "RootEnd") return;
        GameStateManager.LoadScene("WinScene");
    }
}
