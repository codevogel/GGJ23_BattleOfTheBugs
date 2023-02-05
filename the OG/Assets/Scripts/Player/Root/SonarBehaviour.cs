using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class SonarBehaviour : MonoBehaviour
{
	public GameObject Root;
	public VisualCueResource VisualCueResourceOnRoot;

	[SerializeField]
	private float m_Speed;
	[SerializeField]
	private int m_RayCount = 9;
	[SerializeField]
	private int m_DegreeOffset = 1;

	private Vector2 m_InputVector;

	public float ShootingCoolDown = 1.5f;

	private List<Ray2D> m_Rays = new List<Ray2D>();

	private float m_Time;
	private bool m_NoAim = true;

	private void Awake()
	{
		EventManager.OnPlayer2AimPerformed += AimTowards;
		EventManager.OnPlayer2AimCanceled+= FadePointer;
		EventManager.OnPlayer2Attack += SpawnSonar;
	}

	private void FixedUpdate()
	{
		var rads = Mathf.Atan2(m_InputVector.x, m_InputVector.y);
		var degrees = rads * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, -degrees));
		transform.position = new Vector3(Root.transform.position.x, Root.transform.position.y, Root.transform.position.z);
	}

	public void AimTowards(Vector2 input)
	{
		m_InputVector = input.normalized;
		StopCoroutine(FadeTo(0.0f, 0.1f));
		StartCoroutine(FadeTo(1.0f, 0.1f));
		m_NoAim = true;
	}

	private void FadePointer()
	{
		StopCoroutine(FadeTo(1.0f, 0.1f));
		StartCoroutine(FadeTo(0.0f, 0.1f));
		m_NoAim = true;

	}
	private IEnumerator FadeTo(float aValue, float aTime)
	{
		SpriteRenderer[] spriterenders = GetComponentsInChildren<SpriteRenderer>();
		foreach (SpriteRenderer sprite in spriterenders)
		{
			float alpha = sprite.material.color.a;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
				sprite.material.color = newColor;
				yield return null;
			}
			Color finaleColor = new Color(1, 1, 1, aValue);
			sprite.material.color = finaleColor;
		}
	}
	private void SpawnSonar()
	{
		if (m_Time > 0)
			return;

		if (m_InputVector == Vector2.zero.normalized)
			m_InputVector = Vector2.down.normalized;

		var rads = Mathf.Atan2(m_InputVector.x, m_InputVector.y);
		var degrees = rads * Mathf.Rad2Deg + 90;

		var sonarDir = DegreeToVector2(degrees);
		VisualCueResourceOnRoot.Play(new Vector3(sonarDir.x, -sonarDir.y, 0));
		
		var pos = new Vector2(transform.position.x, transform.position.y);

		// raycount has to be odd
		// ReSharper disable once PossibleLossOfFraction 
		m_Rays = new List<Ray2D>();
		degrees -= m_RayCount / 2;
		for (var i = 0; i < m_RayCount; i++)
		{
			var dirVec = DegreeToVector2(degrees + i);
			m_Rays.Add(new Ray2D(pos, new Vector2(-dirVec.x, dirVec.y)));
		}

		foreach (var ray in m_Rays)
		{
			var hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, LayerMask.GetMask("Obstacle"));
			Debug.Log($"hit:{hit}");
			if(hit.collider == null) continue;

			

			hit.collider.GetComponent<VisualCueResource>().Play(new Vector3(ray.direction.x, ray.direction.y, 0));
			if (hit.collider.TryGetComponent(out AudioCue audioCue))
			{
				audioCue.PlaySound();
			}
		}

		m_Time = ShootingCoolDown;
	}

	public Vector2 DegreeToVector2(float degree)
	{
		return new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad));
	}


	private void OnDestroy()
	{
		EventManager.OnPlayer2AimPerformed -= AimTowards;
		EventManager.OnPlayer2AimCanceled -= FadePointer;
		EventManager.OnPlayer2Attack -= SpawnSonar;
	}

	private void Update()
	{
		m_Time -= Time.deltaTime;
	}

	private void OnDrawGizmos()
	{
		foreach (var ray in m_Rays)
		{
			Gizmos.DrawRay(transform.position, new Vector3(ray.direction.x, ray.direction.y, 0));
		}
	}
}
