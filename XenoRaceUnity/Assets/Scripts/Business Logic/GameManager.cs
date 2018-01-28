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
    /// How much time is left in seconds. 
    /// </summary>
    private float timeTaken;

    /// <summary>
    /// Measures if the game is over. 
    /// </summary>
    private bool isGameOver = false;

    /// <summary>
    /// Boolean to track if we are paused. 
    /// </summary>
    private bool isPaused = false; 

    #endregion 

    /// <summary>
    /// The remaining time in seconds. 
    /// </summary>
    public float TimeTaken
    {
        get
        {
            return timeTaken; 
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
        if(!isGameOver &&!isPaused)
        {
            IncrementTimer();
        }

        bool pauseInput = Input.GetButtonDown("Start"); 

        if(pauseInput && !isPaused)
        {
            isPaused = true;
            Debug.Log("PAUSED");
            SetMessage("PAUSE MENU");
            CanvasManager.instance.Get<GameOverUIPanel>(UIPanelID.GameOver).Open();
            Time.timeScale = 0; 

        } else if (pauseInput && isPaused)
        {
            isPaused = false;
            Debug.Log("UNPAUSED");
            CanvasManager.instance.Get<GameOverUIPanel>(UIPanelID.GameOver).Close();
            Time.timeScale = 1; 

        }
    }

    /// <summary>
    /// Increment our timer.
    /// </summary>
    private void IncrementTimer()
    {
        timeTaken += Time.deltaTime;
    }

    /// <summary>
    /// Set the GameOver Menu's message bar to something relevant to our context. 
    /// </summary>
    /// <param name="goMessage"></param>
    private void SetMessage(string goMessage)
    {
        gameOverMsg = goMessage; 
    }

    /// <summary>
    /// Set the variables that we need for the race to start okay. 
    /// </summary>
    private void InitializeRace()
    {
        isGameOver = false;
        isPaused = false;
        timeTaken = 0f;
        Time.timeScale = 1f; 
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

    /// <summary>
    /// If the player has entered the goal area, then he has won the game!
    /// </summary>
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

    public void BackToBuild()
    {
        LevelManager.Instance.GoToBuildFromRaceScene();
        Time.timeScale = 1f; 
        GameManager.Instance.Kill(); 
    }

    public void RetryLevel()
    {
        LevelManager.Instance.RestartRaceScene();
        GameManager.Instance.InitializeRace(); 
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }
}
