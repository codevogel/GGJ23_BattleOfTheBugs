using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RootCopy : MonoBehaviour
{
	public GameObject Root;
	public void CopyRoot()
	{
		var rr = Root.GetComponent<RootRenderer>();
		var rlr = Root.GetComponent<LineRenderer>();
		var newRoot = new GameObject("staticRoot");
		rr.lightParent.transform.SetParent(newRoot.transform);

		var lr = newRoot.AddComponent<LineRenderer>();
		lr.positionCount = rr.points.Count;
		lr.SetPositions(rr.points.ToArray());
		lr.sortingLayerID = rlr.sortingLayerID;
		lr.colorGradient = rlr.colorGradient;
		lr.materials = rlr.materials;
		lr.widthCurve = rlr.widthCurve;
		rr.lightParent = new GameObject();
		rr.lights = new List<Light2D>();
		while (rr.points.Count > 0)
		{
			rr.RemovePointAt(0, false);
		}

		rr.points = new List<Vector3>();
		rr.AddPointAt(0, rr.rootOrigin.position);
		rr.UpdateLineRenderer();
		//reset root.
		Root.transform.position = rr.rootOrigin.position;
	}

	LineRenderer CopyComponent(LineRenderer original, GameObject destination)
	{
		System.Type type = original.GetType();
		Component copy = destination.AddComponent(type);
		// Copied fields can be restricted with BindingFlags
		System.Reflection.FieldInfo[] fields = type.GetFields();
		foreach (System.Reflection.FieldInfo field in fields)
		{
			field.SetValue(copy, field.GetValue(original));
		}
		return (LineRenderer)copy;
	}
}
