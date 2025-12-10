using System;
using System.Collections;
using System.Collections.Generic;
using PGGE.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameApp : Singleton<GameApp>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
