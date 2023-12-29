using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    public event EventHandler Onstatechang;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameUnpause;
  
   private enum state
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver,
    }

    private state State;

    
    private float CountDownToStartTimer=3f;
    private float GamePlayingTimer;
    
    [SerializeField] private float GamePlayingTimerMax=300f;
    private bool isGamePause = false;



    private void Awake()
    {
        Instance = this;
        State = state.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseEventAction += Gameinput_OnPauseEventAction;
        GameInput.Instance.OnInteraction += Gameinput_OnInteraction;
    }

    private void Gameinput_OnInteraction(object sender, EventArgs e)
    {
        if (State == state.WaitingToStart)
        {
            State = state.CountDownToStart;
            Onstatechang?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Gameinput_OnPauseEventAction(object sender, EventArgs e)
    {
        gamepauseResume();
    }

    private void Update()
    {
        switch (State)
        {
           
            case state.CountDownToStart:
                CountDownToStartTimer -= Time.deltaTime;
                if (CountDownToStartTimer < 0f)
                {
                    State = state.GamePlaying;
                    GamePlayingTimer = GamePlayingTimerMax;
                    Onstatechang?.Invoke(this, EventArgs.Empty);

                }
                break; 
            case state.GamePlaying:
                GamePlayingTimer -= Time.deltaTime;
                if (GamePlayingTimer < 0f)
                {
                    State = state.GameOver;
                    Onstatechang?.Invoke(this, EventArgs.Empty);

                }
                break;
            case state.GameOver:
                break;
        }
     
    }

    public bool isGamePlaying()
    {
       return State == state.GamePlaying;
    }

    public bool isGameCountdownToStart()
    {
        return State == state.CountDownToStart;
    }

    public float GetGameCountdownTStartTimer()
    {
        return CountDownToStartTimer;
    }

    public bool IsGameOver()
    {
        return State == state.GameOver;
    }
    public float GetgamePlayingTimerNormalized()
    {
        return 1 - (GamePlayingTimer / GamePlayingTimerMax);
    }

    public void gamepauseResume()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpause?.Invoke(this, EventArgs.Empty);
        }
    
    }
}
