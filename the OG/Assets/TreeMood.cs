using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMood : MonoBehaviour
{

    private GameObject _happy;
    private GameObject _sad;

    private void Start()
    {
        _happy = transform.Find("Happy").gameObject;
        _sad = transform.Find("Sadge").gameObject;
    }

    public void SwitchMoods(bool happy)
    {
        _happy.SetActive(happy);
        _sad.SetActive(!happy);
    }
}
