using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public float Score = 0f;
    public float Timer = 0f;

    public bool EDITING = true;
    private CheckpointSystem CurrentTrack;//has list of checkpoints and the total and current checkpoint

	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(this);
	}
	// Update is called once per frame
	void Update ()
    {
        if (!EDITING)
            HandleRace();
        else
            HandleEditor();
        
	}
    void HandleRace()
    {
        if (CurrentTrack == null)
        {//need to find track checkpoints
            if (GameObject.Find("checkpoints") != null)
            {//found it
                CurrentTrack = GameObject.Find("checkpoints").GetComponent<CheckpointSystem>();
            }
            else
            {//didn't find it
                Debug.LogWarning("NO CHECKPOINT SYSTEM ON THIS TRACK?!?!?!");
            }
        }
        else
        {//Handle race related stuff
            Timer = Time.deltaTime;

            Score = CurrentTrack.CurrentCheckpoint;
        }
    }

    void HandleEditor()
    {



    }

    public void LoadNewTrack(string trackName)
    {
        CurrentTrack = null;
        UnityEngine.SceneManagement.SceneManager.LoadScene(trackName);
        Timer = 0f;
        Score = 0f;
    }
    public void RestartTrack()
    {
        if (!EDITING)
        {
            CurrentTrack = null;
            LoadNewTrack(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            Timer = 0f;
            Score = 0f;
        }
    }
}
