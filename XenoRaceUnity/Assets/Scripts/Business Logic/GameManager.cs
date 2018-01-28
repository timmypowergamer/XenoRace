using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Game Manager handles the win and lose conditions of the game. 
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Our singleton instance of the game manager. 
    /// </summary>
    private static GameManager instance; 

    /// <summary>
    /// Publically available reference to the game manager instance. 
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            return instance; 
        }
    }

    #region GameManager CoreVariables 

    /// <summary>
    /// How many seconds we start with for our track. (Should live in another data class?)
    /// </summary>
    [SerializeField] private int startingSeconds = 60;

    /// <summary>
    /// How much time is left in seconds. 
    /// </summary>
    private float timeLeft;

    /// <summary>
    /// Measures if the game is over. 
    /// </summary>
    private bool isGameOver = false;

    #endregion 

    /// <summary>
    /// The remaining time in seconds. 
    /// </summary>
    public float TimeLeft
    {
        get
        {
            return timeLeft; 
        }
    }

    /// <summary>
    /// Returns true if the game is over, false otherwise. 
    /// </summary>
    public bool IsGameOver
    {
        get
        {
            return isGameOver; 
        }
    }

    private string gameOverMsg = ""; 
    public string GameOverMessage
    {
        get
        {
            return gameOverMsg;
        }
    }

    /// <summary>
    /// Enforce our singleton-itude on awake. If we are the only game manager in the scene, then we *are* the 
    /// instance. Otherwise, we commit hara-kiri. 
    /// </summary>
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this); 
        } else
        {
            Destroy(this.gameObject); 
        }
    }

    /// <summary>
    /// Set up our variables when we start the race. 
    /// </summary>
    private void Start()
    {
        InitializeRace(); 
    }

    /// <summary>
    /// Track any game metrics we are about. 
    /// </summary>
    private void Update()
    {
        if(timeLeft > 0 && !isGameOver)
        {
            DecrementTimer();
        }
   
    }

    /// <summary>
    /// Decrement our timer. If we hit zero, then we need to call whoever cares. 
    /// </summary>
    private void DecrementTimer()
    {
        timeLeft -= Time.deltaTime;
        timeLeft = timeLeft < 0 ? 0 : timeLeft; // No negative times. 

        if(timeLeft == 0)
        {
            TimedOut(); 
        }
    }

    private void SetMessage(string goMessage)
    {
        gameOverMsg = goMessage; 
    }

    /// <summary>
    /// Function to call if the player has run out of time. 
    /// </summary>
    private void TimedOut()
    {
        isGameOver = true;
        SetMessage("You ran out of time!"); 
        CanvasManager.instance.Get<GameOverUIPanel>(UIPanelID.GameOver).Open();
        Core.Instance.EnablePlayerInput(false); 
    }

    /// <summary>
    /// Set the variables that we need for the race to start okay. 
    /// </summary>
    private void InitializeRace()
    {
        isGameOver = false;
        timeLeft = startingSeconds;
    }

    /// <summary>
    /// Function to call if the player has fallen off the track. 
    /// </summary>
    public void FellOffTrack()
    {
        isGameOver = true;
        SetMessage("You fell off the track!");
        CanvasManager.instance.Get<GameOverUIPanel>(UIPanelID.GameOver).Open();
        Core.Instance.EnablePlayerInput(false);
    }

    public void PlayerEnteredGoal()
    {
        isGameOver = true;
        SetMessage("You win! You beat the alien derby!"); 
        CanvasManager.instance.Get<GameOverUIPanel>(UIPanelID.GameOver).Open();
        Core.Instance.EnablePlayerInput(false);
    }

    /// <summary>
    /// Destroys the game manager - used for when we leave the actual "Game"
    /// </summary>
    public void Kill()
    {
        GameManager.instance = null;
        Destroy(this); 
    }
}
