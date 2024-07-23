using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;

    private enum GameState{
        WaitingToStart,
        Playing,
        GameOver,
    }

    private GameState state;
    private float TargetRotation;
    private float TargetRotation2;
    private float WaitingToStartCountdown = 3f;
    private float PlayingCountdown = 10f;

    private void Awake()
    {
        Instance = this;
        state = GameState.WaitingToStart;
    }

    private void Update()
    {
        switch(state)
        {
            case GameState.WaitingToStart:
                WaitingToStartCountdown -= Time.deltaTime;
                Debug.Log(WaitingToStartCountdown);
                if(WaitingToStartCountdown <= 0)
                {
                    state = GameState.Playing;
                    Debug.Log(state);
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case GameState.Playing:
                PlayingCountdown -= Time.deltaTime;
                Debug.Log(PlayingCountdown);
                if(PlayingCountdown <= 0)
                {
                    state = GameState.GameOver;
                    Debug.Log(state);
                    OnStateChanged?.Invoke(this, new EventArgs());
                }
                break;
            case GameState.GameOver:
                break;
        }
    }

    public bool isWaitingToStart()
    {
        return state == GameState.WaitingToStart;
    }

    public bool isPlaying()
    {
        return state == GameState.Playing;
    }

    public bool isGameOver()
    {
        return state == GameState.GameOver;
    }
}
