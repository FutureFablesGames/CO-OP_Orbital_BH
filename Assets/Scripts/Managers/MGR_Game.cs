using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{ 
    Pregame,
    Playing,
    Paused,
    Postgame,
}


public class MGR_Game : MonoBehaviour
{    
    [Header("Game State")]
    public GameMode Mode;
    public GameState State = GameState.Pregame;
    public float TimeLimit;    
    private float time;

    private void Awake()
    {
        if (Manager.Game != null)
        {
            Debug.LogWarning("Replacing previous GameManager");
        }

        Manager.Game = this;
    }

    private void Start()
    {
        StartCoroutine(Pregame());
    }

    private void Update()
    {
        if (State != GameState.Playing) return;

        switch (Mode)
        {
            case GameMode.Timed:

                // Count down the time
                time -= Time.deltaTime;

                // Update the UI Time Display
                Manager.UI.UpdateTimeDisplay(time);

                // Check for Game Completion
                if (time <= 0.0f && State != GameState.Postgame)
                {
                    StartCoroutine(EndGame());
                }

                break;
        }
    }

    private IEnumerator Pregame()
    {
        State = GameState.Pregame;
        time = TimeLimit;

        // Prepare the UI
        Manager.UI.UpdateTimeDisplay(time);
        Manager.UI.UpdateResourcesDisplay("0");

        yield return new WaitForSeconds(3.0f);

        // Countdown: 3
        Manager.UI.SetCountdown("3");

        yield return new WaitForSeconds(1.0f);

        // Countdown: 2
        Manager.UI.SetCountdown("2");

        yield return new WaitForSeconds(1.0f);

        // Countdown: 1
        Manager.UI.SetCountdown("1");

        yield return new WaitForSeconds(1.0f);

        // GO!
        Manager.UI.SetCountdown("");
        Manager.UI.HideCountdown();
        State = GameState.Playing;
    }

    private IEnumerator EndGame()
    {
        // Announce Game Over
        Manager.UI.SetCountdown("Time Is Up!");
        State = GameState.Postgame;

        yield return new WaitForSeconds(3.0f);

        Manager.Scene.Load((int)Scenes.menu);
    }
}
