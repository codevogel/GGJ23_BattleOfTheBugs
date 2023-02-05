using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
	public Animator anim;
	public Animator anim2;
	public Animator anim3;
	public void StartAnim(int stage)
	{
		switch (stage)
		{
			case 0:
				anim.SetTrigger("1");
				break;
			case 1:
				anim.SetTrigger("2");
				break;
			case 2:
				anim.SetTrigger("3");
				break;
		}
	}

	public void OnEvolveAnimationEnded1()
	{
		transform.localScale = new Vector3(1.1f, 1.3f, 0);
	}
	public void OnEvolveAnimationEnded2()
	{
		transform.localScale = new Vector3(1.2f, 1.6f, 0);
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			EventManager.ResourceCollected();
		}
	}
}
