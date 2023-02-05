using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateManager : MonoBehaviour
{
	public List<UnityEvent<int>> ChangeStateEvents;

	public int CurrentStage = 0;

	private void Awake()
	{
		EventManager.OnResourceCollected += OnResourceCollected;
	}

	private void OnResourceCollected()
	{
		if (CurrentStage < ChangeStateEvents.Count)
		{
			ChangeStateEvents[CurrentStage]?.Invoke(CurrentStage);
		}
		else
		{
			GameStateManager.LoadScene("WinScene");
		}
		CurrentStage++;
	}

	private void OnDestroy()
	{
		EventManager.OnResourceCollected -= OnResourceCollected;
	}
}
