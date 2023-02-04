using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour
{
    public float timeBetweenSounds = 5f;
    public AudioClip waterDropSound;
    private AudioSource m_Audio;
    private GameObject rootEnd;

    [SerializeField]
    private VisualCueResource m_visualCue;

    void Awake()
    {
	    m_Audio = GetComponent<AudioSource>();
	    m_Audio.clip = waterDropSound;

        rootEnd = GameObject.FindGameObjectWithTag("RootEnd");

        EventManager.OnStartGame += OnStartGame;

        
    }

    private void OnStartGame()
    {
        StartCoroutine(PlaySound());
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, rootEnd.transform.position);
        m_Audio.volume = 1.0f - dist / 25f;
        
    }

    private IEnumerator PlaySound()
    {
        while (true)
        {
	        m_Audio.Play();
            m_visualCue.play = true;
            yield return new WaitForSeconds(timeBetweenSounds);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        EventManager.OnStartGame -= OnStartGame;
    }
}
