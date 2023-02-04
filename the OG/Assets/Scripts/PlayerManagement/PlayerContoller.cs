using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerContoller : MonoBehaviour
{
	public enum CharacterType
	{
		Tree,
		Root
	}
	
	private bool m_IsSwitched = false;
	public CharacterType Type = CharacterType.Tree;

	public bool Playing = false;

	public int PlayerIndex => GetComponent<PlayerInput>().playerIndex + (m_IsSwitched ? 1 : 0);

	public float LeftStickDeadZone = 0.2f;

	private void Awake()
	{
		GameManager.Instance.CheatActions.actions.Switch.performed += SwitchOnPerformed;
		name = $"{name}{PlayerIndex}{Type}";
	}

	private void SwitchOnPerformed(InputAction.CallbackContext ctx)
	{
		m_IsSwitched = !m_IsSwitched;
	}

	public void OnPlayerMove(InputAction.CallbackContext ctx)
	{
		if(!Playing) return;
		var value = ctx.ReadValue<Vector2>();
		var type = Type;
		if (m_IsSwitched)
		{
			type = Type switch
			{
				CharacterType.Root => CharacterType.Tree,
				CharacterType.Tree => CharacterType.Root,
				_ => throw new ArgumentOutOfRangeException()
			};
		}
		switch (type)
		{
			case CharacterType.Tree:
				if (ctx.performed && value.x * value.x >= LeftStickDeadZone * LeftStickDeadZone)
				{
					float xDir = value.x > 0 ? 1 : -1;
					EventManager.Player1MovePerformed(xDir);
				}
				else if (ctx.canceled)
				{
					EventManager.Player1MoveCanceled();
				}
				break;
			case CharacterType.Root:
				if (ctx.performed)
				{
					EventManager.Player2MovePerformed(value);
				}
				else if (ctx.canceled)
				{
					EventManager.Player2MoveCanceled();
				}
				break;
			default:
				throw new IndexOutOfRangeException($"m_CharacterType:{Type} out of range!");
		}
	}
	public void OnPlayerAim(InputAction.CallbackContext ctx)
	{
		if (!Playing) return;
		if (Type == CharacterType.Root) return;
		var value = ctx.ReadValue<Vector2>();
		if (ctx.performed)
		{
			EventManager.PlayerAimPerformed(value);
		}
		else if (ctx.canceled)
		{
			EventManager.PlayerAimCanceled();
		}
	}
	public void OnPlayerAttack(InputAction.CallbackContext ctx)
	{
		if (!Playing) return;
		if (Type == CharacterType.Root) return;
		if (ctx.performed || ctx.canceled) return;
		EventManager.PlayerAttack();
	}

	public void OnPlayerReady(InputAction.CallbackContext ctx)
	{
		if (Playing) return;
		if (ctx.performed || ctx.canceled) return;
		EventManager.PlayerReady(PlayerIndex);
	}

	public void OnChangeType(InputAction.CallbackContext ctx)
	{
		if (Playing) return;
		if (ctx.performed || ctx.canceled) return;
		Type = Type switch
		{
			CharacterType.Root => CharacterType.Tree,
			CharacterType.Tree => CharacterType.Root,
			_ => throw new ArgumentOutOfRangeException()
		};
		EventManager.PlayerSwitchType(PlayerIndex, Type);
		name = $"{Type}{PlayerIndex}";
	}

	private void OnDestroy()
	{
		GameManager.Instance.CheatActions.actions.Switch.performed -= SwitchOnPerformed;
	}
}
