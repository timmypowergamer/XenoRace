using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/// <summary>
/// Only concerned with the number of seconds left, currently down to the millisecond. 
/// </summary>
public class TimerDisplay : MonoBehaviour {

    /// <summary>
    /// The field of text we want to display in. 
    /// </summary>
    [SerializeField]
    private Text displayText;

    // Our time measurement scratchpad variables. 
    private int minutes = 0;
    private int seconds = 0;
    private int milliseconds = 0;


    /// <summary>
    /// Update the displayed time every frame 
    /// </summary>
    private void Update()
    {
        // HACK: Just Squelching null issues 
        if (GameManager.Instance == null)
        {
            return; 
        }

        // Container for the time left 
        float temp = GameManager.Instance.TimeLeft;

        // Get minutes
        minutes = (int)Mathf.Floor(temp / 60.0f);
        temp -= (minutes * 60.0f);

        // Get Seconds 
        seconds = (int)Mathf.Floor(temp);
        temp -= seconds;

        // Get milliseconds 
        milliseconds = (int)Mathf.Floor(temp * 100);

        // I know there's a better way to do this with format strings, but I can't hack it to work - Alex M. 
        displayText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2") + ":" + milliseconds.ToString("D2");
    }
}
