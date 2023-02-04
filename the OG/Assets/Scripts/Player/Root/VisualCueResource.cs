using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCueResource : MonoBehaviour
{
    public ParticleSystem visualCueResource;
    
    public GameObject resource;
    public bool play = false;

    void Start()
    {
        visualCueResource = GetComponent<ParticleSystem>();

        var main = visualCueResource.main;
        main.startRotation3D = true;
    }

    void Update()
    {
        var main = visualCueResource.main;
        var dirVector = transform.position - resource.transform.position;
        dirVector.Normalize();


        main.startRotationZ = Mathf.Atan2(dirVector.x, dirVector.y);

        if (play)
        {
            visualCueResource.Play();
            play = false;
        }
    }
}
