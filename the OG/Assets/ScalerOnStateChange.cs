using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerOnStateChange : MonoBehaviour
{
	public void ScaleX(float newX)
	{
		transform.localScale = new Vector3(transform.localScale.x + newX, transform.localScale.y, 0);
	}
	public void ScaleY(float newY)
	{
		transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + newY, 0);
	}
}
