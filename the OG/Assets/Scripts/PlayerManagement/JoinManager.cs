using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JoinManager : MonoBehaviour
{
	public Image Player1RootReadyGameObject;
	public Image Player1TreeReadyGameObject;
	public Image Player2RootReadyGameObject;
	public Image Player2TreeReadyGameObject;

	public GameObject TextP1;
	public GameObject TextP2;

	private Image CurrentPlayer1Obj;
	private Image CurrentPlayer2Obj;

	public Color ReadyColor = Color.green;
	public Color NotReadyColor = Color.red;
	public List<PlayerContoller> PlayerInputs;

	private bool[] m_PlayersReady = {false, false};

	private void Awake()
	{
		EventManager.OnPlayerReady += OnPlayerReady;
		EventManager.OnPlayerSwitchType += OnPlayerSwitchType;
		CurrentPlayer1Obj = Player1TreeReadyGameObject;
		CurrentPlayer2Obj = Player2TreeReadyGameObject;
	}

	private void OnPlayerReady(int playerIndex)
	{
		if (PlayerInputs.Count > 1)
		{
			var typesAreEqual = PlayerInputs[0].Type == PlayerInputs[1].Type;
			var readyAmount = m_PlayersReady.Count(x => x);
			if (!typesAreEqual)
			{
				m_PlayersReady[playerIndex] = !m_PlayersReady[playerIndex];
			}
			else if (readyAmount == 0)
			{
				m_PlayersReady[playerIndex] = !m_PlayersReady[playerIndex];
			}
		}
		else
		{
			m_PlayersReady[playerIndex] = !m_PlayersReady[playerIndex];
		}

		CurrentPlayer1Obj.color = m_PlayersReady[0] ? ReadyColor : NotReadyColor;
		CurrentPlayer2Obj.color = m_PlayersReady[1] ? ReadyColor : NotReadyColor;
		if (m_PlayersReady.All(x => x) && 
		    PlayerInputs.Count(x => x.Type == PlayerContoller.CharacterType.Tree) == 1 &&
		    PlayerInputs.Count(x => x.Type == PlayerContoller.CharacterType.Root) == 1)
		{
			EventManager.StartGame();
			EnableInput();
		}
	}
	private void OnPlayerSwitchType(int playerIndex, PlayerContoller.CharacterType playerType)
	{
		m_PlayersReady[playerIndex] = false;
		if (playerIndex == 0)
		{
			CurrentPlayer1Obj.color = NotReadyColor;
			Player1RootReadyGameObject.gameObject.SetActive(playerType == PlayerContoller.CharacterType.Root);
			Player1TreeReadyGameObject.gameObject.SetActive(playerType == PlayerContoller.CharacterType.Tree);
			CurrentPlayer1Obj = playerType == PlayerContoller.CharacterType.Root
				? Player1RootReadyGameObject
				: Player1TreeReadyGameObject;
		}
		else if (playerIndex == 1)
		{
			CurrentPlayer2Obj.color = NotReadyColor;
			Player2RootReadyGameObject.gameObject.SetActive(playerType == PlayerContoller.CharacterType.Root);
			Player2TreeReadyGameObject.gameObject.SetActive(playerType == PlayerContoller.CharacterType.Tree);
			CurrentPlayer2Obj = playerType == PlayerContoller.CharacterType.Root
				? Player2RootReadyGameObject
				: Player2TreeReadyGameObject;
		}

	}

	private void EnableInput()
	{
		CurrentPlayer1Obj.gameObject.SetActive(false);
		CurrentPlayer2Obj.gameObject.SetActive(false);
		foreach (var playerInput in PlayerInputs)
		{
			playerInput.Playing = true;
		}
	}

	public void OnPlayerJoin(PlayerInput playerInput)
	{
		if (playerInput.playerIndex == 0)
		{
			CurrentPlayer1Obj.gameObject.SetActive(true);
			TextP1.SetActive(false);
		}
		else if (playerInput.playerIndex == 1)
		{
			CurrentPlayer2Obj.gameObject.SetActive(true);
			TextP2.SetActive(false);
		}
		Debug.Log($"id{playerInput.gameObject.name}{playerInput.playerIndex}");
		PlayerInputs.Add(playerInput.GetComponent<PlayerContoller>());
	}

	private void OnDestroy()
	{
		EventManager.OnPlayerReady -= OnPlayerReady;
		EventManager.OnPlayerSwitchType -= OnPlayerSwitchType;
	}
}