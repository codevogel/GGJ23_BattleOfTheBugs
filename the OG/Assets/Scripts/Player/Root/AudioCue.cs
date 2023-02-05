using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour
{
	public AudioClip waterDropSound;
    private AudioSource m_Audio;
    private GameObject rootEnd;

    [SerializeField]
    private VisualCueResource m_visualCue;

    private void Awake()
    {
	    m_Audio = GetComponent<AudioSource>();
	    m_Audio.clip = waterDropSound;

        rootEnd = GameObject.FindGameObjectWithTag("RootEnd");

    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, rootEnd.transform.position);
        m_Audio.volume = 1.0f - dist / 25f;
    }

    public void PlaySound()
    {
	    m_Audio.Play();
    }

}
