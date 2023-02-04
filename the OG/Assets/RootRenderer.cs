using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class RootRenderer : MonoBehaviour
{
    private List<Vector3> points;
    private LineRenderer _lineRenderer;

    [SerializeField]
    private float _deltaMin;

    private Vector2 FirstPoint { get => points[0];}
    private Vector3 LastPoint { get => points[points.Count - 1]; }

    [SerializeField]
    private Transform rootOrigin;


    private RootMotor _rootMotor;
    [SerializeField]
    private TreeMotor _treeMotor;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        points = new List<Vector3>();
        points.Add(rootOrigin.position);
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
                points.Add(transform.position);
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
        points.Insert(0, rootOrigin.position);
        points.RemoveAt(points.Count - 1);
        transform.position = LastPoint;
        return true;
    }
    private void UpdateLineRenderer()
    {
        _lineRenderer.positionCount = points.Count;
        _lineRenderer.SetPositions(points.ToArray());
    }
}