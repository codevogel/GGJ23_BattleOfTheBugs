using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualCueResource : MonoBehaviour
{
    public ParticleSystem visualCueResource;

    void Start()
    {
        visualCueResource = GetComponent<ParticleSystem>();

        var main = visualCueResource.main;
        main.startRotation3D = true;
    }

    public void Play(Vector3 dirVector)
    {
        var main = visualCueResource.main;
        dirVector.Normalize();


        main.startRotationZ = Mathf.Atan2(dirVector.x, dirVector.y);

        visualCueResource.Play();
    }
}
