using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JoinManager : MonoBehaviour
{
	public Image Player1ReadyGameObject;
	public Image Player2ReadyGameObject;
	public Color ReadyColor = Color.green;
	public Color NotReadyColor = Color.red;
	public List<PlayerInput> PlayerInputs;

	private bool[] m_PlayersReady = {false, false};

	private void Awake()
	{
		EventManager.OnPlayerReady += OnPlayerReady;
	}

	private void OnPlayerReady(int playerIndex)
	{
		m_PlayersReady[playerIndex] = !m_PlayersReady[playerIndex];
		Player1ReadyGameObject.color = m_PlayersReady[0] ? ReadyColor : NotReadyColor;
		Player2ReadyGameObject.color = m_PlayersReady[1] ? ReadyColor : NotReadyColor;
		if (m_PlayersReady.All(x => x))
		{
			EventManager.StartGame();
			EnableInput();
		}
	}

	private void EnableInput()
	{
		Player1ReadyGameObject.gameObject.SetActive(false);
		Player2ReadyGameObject.gameObject.SetActive(false);
		foreach (var playerInput in PlayerInputs)
		{
			playerInput.GetComponent<PlayerContoller>().Playing = true;
		}
	}

	public void OnPlayerJoin(PlayerInput playerInput)
	{
		Debug.Log($"id{playerInput.gameObject.name}{playerInput.playerIndex}");
		PlayerInputs.Add(playerInput);
	}

	private void OnDestroy()
	{
		EventManager.OnPlayerReady -= OnPlayerReady;
	}
}