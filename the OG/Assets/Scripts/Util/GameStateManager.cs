using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{

    public static GameStateManager instance;

    private void Awake()
    {
	    if (instance != null && instance != this)
	    {
            GameObject.Destroy(this);
            return;
	    }
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
