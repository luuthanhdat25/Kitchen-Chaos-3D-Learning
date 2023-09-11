using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
        
    public event EventHandler OnStateChanged;
    
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }
    
    private State state;
    private float waitingToStartTimer = 1f;
    private float coundownToStartTimer = 3f;
    private float coundownToGamePlayTimer;
    [SerializeField] private float coundownToGamePlayTimerMax = 30f;

    private void Awake()
    {
        if(Instance != null) Debug.LogError("GameManager is already initialized");
        Instance = this;
        
        state = State.WaitingToStart;
    }

    private void Start() => coundownToGamePlayTimer = coundownToGamePlayTimerMax;

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer <= 0)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.CountdownToStart:
                coundownToStartTimer -= Time.deltaTime;
                if (coundownToStartTimer <= 0)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.GamePlaying:
                coundownToGamePlayTimer -= Time.deltaTime;
                if (coundownToGamePlayTimer <= 0)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying() => state == State.GamePlaying;
    
    public bool IsCountDownToStartIsActive() => state == State.CountdownToStart;
    
    public bool IsGameOver() => state == State.GameOver;

    public float GetCoundownToStartTimer() => coundownToStartTimer;
    
    public float GetCoundownToGamePlayTimerNormalized() => 1 - (coundownToGamePlayTimer / coundownToGamePlayTimerMax);
}

