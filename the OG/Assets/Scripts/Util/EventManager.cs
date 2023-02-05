
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

	public static event Action<Vector2> OnPlayer1AimPerformed;

	public static void Player1AimPerformed(Vector2 value)
	{
		OnPlayer1AimPerformed?.Invoke(value);
	}

	public static event Action OnPlayer1AimCanceled;

	public static void Player1AimCanceled()
	{
		OnPlayer1AimCanceled?.Invoke();
	}

	public static event Action<Vector2> OnPlayer2AimPerformed;

	public static void Player2AimPerformed(Vector2 value)
	{
		OnPlayer2AimPerformed?.Invoke(value);
	}

	public static event Action OnPlayer2AimCanceled;

	public static void Player2AimCanceled()
	{
		OnPlayer2AimCanceled?.Invoke();
	}

	public static event Action OnPlayer1Attack;

	public static void Player1Attack()
	{
		OnPlayer1Attack?.Invoke();
	}

	public static event Action OnPlayer2Attack;

	public static void Player2Attack()
	{
		OnPlayer2Attack?.Invoke();
	}

	public static event Action<int> OnPlayerReady;

	public static void PlayerReady(int value)
	{
		OnPlayerReady?.Invoke(value);
	}

	public static event Action OnStartGame;

	public static void StartGame()
	{
		OnStartGame?.Invoke();
	}

	public static event Action<int, PlayerContoller.CharacterType> OnPlayerSwitchType;

	public static void PlayerSwitchType(int value1, PlayerContoller.CharacterType value2)
	{
		OnPlayerSwitchType?.Invoke(value1, value2);
	}
}
