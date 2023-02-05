using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    [SerializeField]
    private AudioClip intro;
    [SerializeField]
    private AudioClip loop;

    private AudioSource source;

    private void Start()
    {
        if (source == null)
        {
            source = GetComponent<AudioSource>();
            source.clip = intro;
            source.loop = false;
            source.Play();
            StartCoroutine(PlayLoopAfterIntro());
            DontDestroyOnLoad(this.gameObject);
        }
        if (source != null && !source.isPlaying)
        {
            source = GetComponent<AudioSource>();
            source.clip = intro;
            source.loop = false;
            source.Play();
            StartCoroutine(PlayLoopAfterIntro());
            DontDestroyOnLoad(this.gameObject);
        }
    }


    private IEnumerator PlayLoopAfterIntro()
    {
        while(source.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        source.clip = loop;
        source.loop = true;
        source.Play();
    }
}
