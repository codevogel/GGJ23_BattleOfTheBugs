using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(LineRenderer))]
public class RootRenderer : MonoBehaviour
{
    public List<Vector3> points;
    public List<Light2D> lights;
    private LineRenderer _lineRenderer;

    [SerializeField]
    private float _deltaMin;

    private Vector2 FirstPoint { get => points[0];}
    private Vector3 LastPoint { get => points[points.Count - 1]; }

    [SerializeField]
    public Transform rootOrigin;


    private RootMotor _rootMotor;
    [SerializeField]
    private TreeMotor _treeMotor;

    [SerializeField]
    private Light2D _lightPrefab;

    public GameObject lightParent;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        lightParent = new GameObject();

        points = new List<Vector3>();
        lights = new List<Light2D>();
        AddPointAt(0, rootOrigin.position);

        _rootMotor = GetComponent<RootMotor>();

    }

    // Update is called once per frame
    void Update()
    {
        bool treeMoving = _treeMotor.Body.velocity.magnitude >= 0.1;
        _rootMotor.CanMove = !treeMoving;
        if (treeMoving)
        {
            CheckForRetract();
        }
        else
        {
            CheckForGrowth();
        }
        UpdateLineRenderer();
    }

    private void CheckForGrowth()
    {
        if (points.Count > 0)
        {
            if (Vector3.Distance(LastPoint, transform.position) < _deltaMin)
            {
                return;
            }
            else
            {
                AddPointAt(points.Count, transform.position);
                Debug.Log("Growing");
            }
        }
    }

    private bool CheckForRetract()
    {
        float delta = Vector2.Distance(FirstPoint, rootOrigin.position);
        if (delta <= _deltaMin)
        {
            Debug.Log("Not retracting");
            return false;
        }
        AddPointAt(0, rootOrigin.position);
        RemovePointAt(points.Count - 1);
        transform.position = LastPoint;
        return true;
    }



    public void UpdateLineRenderer()
    {
        _lineRenderer.positionCount = points.Count;
        _lineRenderer.SetPositions(points.ToArray());
    }

    public void AddPointAt(int index, Vector3 point)
    {
        points.Insert(index, point);
        Light2D light = Instantiate(_lightPrefab, point, Quaternion.identity, lightParent.transform);
        lights.Insert(index, light);
    }

    public void RemovePointAt(int index, bool withlights = true)
    {
        points.RemoveAt(index);

        if (withlights)
        {
	        Destroy(lights[index].gameObject);
			lights.RemoveAt(index);
        }
    }
}