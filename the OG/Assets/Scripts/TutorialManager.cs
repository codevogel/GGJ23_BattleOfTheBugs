using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    public int currentEvent = 0;
    public bool enemyKilled = false;
    public bool inLight = false;

    public List<UnityEvent> events;
    public UnityEvent test = new UnityEvent();
    // Start is called before the first frame update
    private void Start()
    {
        EventManager.OnStartGame += OnStartGame;
        
    }

    private void Update()
    {
        if(enemyKilled && inLight)
        {
            NextEvent();
        }
    }
    private void OnStartGame()
    {
        startEvent();
    }
    public void startEvent()
    {
        events[currentEvent].Invoke();
    }

    public void NextEvent()
    {
        currentEvent++;
        events[currentEvent].Invoke();
        enemyKilled = false;
        inLight = false;
    }
}
