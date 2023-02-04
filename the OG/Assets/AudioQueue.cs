using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueue : MonoBehaviour
{
    public float timeBetweenSounds = 5f;
    public AudioClip waterDropSound;
    AudioSource audio;
    GameObject rootEnd;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = waterDropSound;

        rootEnd = GameObject.FindGameObjectWithTag("RootEnd");

        StartCoroutine(playSound());
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, rootEnd.transform.position);
        audio.volume = 1.0f - dist / 25f;
        
    }

    private IEnumerator playSound()
    {
        while (true)
        {
            audio.Play();
            yield return new WaitForSeconds(timeBetweenSounds);
        }
        
        
    }
}
