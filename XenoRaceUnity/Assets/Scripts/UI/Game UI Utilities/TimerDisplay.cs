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
	private void Update ()
    {
        displayText.text = GameManager.Instance.TimeLeft.ToString(); 
	}
}
