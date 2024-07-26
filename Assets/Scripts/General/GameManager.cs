using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public bool isPaused { get; private set; }
    public bool isPlaying { get; private set; } = false;
    public UnityEvent OnWin;
    public UnityEvent OnLose;
    public UnityEvent OnPause;
    public UnityEvent OnResume;
    public UnityEvent OnStartGame;


    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
      
    }
    
    private void OnEnable()
    {
        OnPause.AddListener(onPause);
        OnResume.AddListener(onResume);
        OnWin.AddListener(onWin);
        OnLose.AddListener(onLose);
        OnStartGame.AddListener(onStartGame);
        isPlaying = false;
        Application.targetFrameRate = 60;
    }

    private void onStartGame()
    {
        isPlaying = true;
        Time.timeScale = 1;

    }

    private void onWin()
    {
        isPlaying = false;
        
    }

    private void onLose()
    {
        isPlaying = false;
       
    }

    void onPause()
    {
        isPaused = true;
        Time.timeScale = 0;
    }
    void onResume()
    {
        isPaused = false;
        Time.timeScale = 1;
    }


}
