using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
	public List<Transform> PlayerSpawns;

	public void OnPlayerJoin(PlayerInput playerInput)
	{
		playerInput.gameObject.transform.position = PlayerSpawns[playerInput.playerIndex].position;
	}
}
