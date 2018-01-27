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

    /// <summary>
    /// Enforce our singleton-itude on awake. If we are the only game manager in the scene, then we *are* the 
    /// instance. Otherwise, we commit hara-kiri. 
    /// </summary>
    private void Awake()
    {
        if(instance == null)
        {
            instance = this; 
        } else
        {
            Destroy(this.gameObject); 
        }
    }

    /// <summary>
    /// Subscribe to any events that we care about on Start.
    /// </summary>
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {

	}

    void HandleRace()
    {

    }

    public void LoadNewTrack(string trackName)
    {

    }

    public void RestartTrack()
    {

    }
}
