using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }
	public PlayerInputManager PlayerIM;
	public StateManager StageManagerScript;
	public GameObject Tree;
	public TutorialManager TutorialManagerScript;

	public CheatActions CheatActions;

	public bool rootDisabled = false;
	public bool treeDisabled = false;

	private void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		if (Instance != null && Instance != this)
		{
			Destroy(this);
			return;
		}
		Instance = this;

		PlayerIM = GetComponent<PlayerInputManager>();

		CheatActions = new CheatActions();
		CheatActions.Enable();
		CheatActions.actions.ExtraJoin.performed += ExtraJoinOnPerformed;
	}

	private void ExtraJoinOnPerformed(InputAction.CallbackContext ctx)
	{
		if (PlayerIM.playerCount >= PlayerIM.maxPlayerCount) return;
		GameObject.Instantiate(PlayerIM.playerPrefab, Vector3.zero, Quaternion.identity).
			GetComponent<PlayerInput>().DeactivateInput();
	}

	private void OnDestroy()
	{
		CheatActions.actions.ExtraJoin.performed -= ExtraJoinOnPerformed;
	}

	public void DisableRootInput()
	{
		rootDisabled = true;
	}
	public void EnableTreeInput()
    {
		treeDisabled = false;
    }
	public void EnableRootInput()
	{
		rootDisabled = false;
	}
	public void DisableTreeInput()
	{
		treeDisabled = true;
	}
}
