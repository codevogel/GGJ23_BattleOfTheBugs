
using System;
using UnityEngine;

public static class EventManager
{
	public static event Action<float> OnPlayer1MovePerformed;

	public static void Player1MovePerformed(float value)
	{
		OnPlayer1MovePerformed?.Invoke(value);
	}

	public static event Action OnPlayer1MoveCanceled;

	public static void Player1MoveCanceled()
	{
		OnPlayer1MoveCanceled?.Invoke();
	}

	public static event Action<Vector2> OnPlayer2MovePerformed;

	public static void Player2MovePerformed(Vector2 value)
	{
		OnPlayer2MovePerformed?.Invoke(value);
	}

	public static event Action OnPlayer2MoveCanceled;

	public static void Player2MoveCanceled()
	{
		OnPlayer2MoveCanceled?.Invoke();
	}

	public static event Action<Vector2> OnPlayerAimPerformed;

	public static void PlayerAimPerformed(Vector2 value)
	{
		OnPlayerAimPerformed?.Invoke(value);
	}

	public static event Action OnPlayerAimCanceled;

	public static void PlayerAimCanceled()
	{
		OnPlayerAimCanceled?.Invoke();
	}

	public static event Action OnPlayerAttack;

	public static void PlayerAttack()
	{
		OnPlayerAttack?.Invoke();
	}
}
