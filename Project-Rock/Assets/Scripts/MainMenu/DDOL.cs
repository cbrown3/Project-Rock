﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;

        DontDestroyOnLoad(gameObject);

        if(SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        }
    }
}
