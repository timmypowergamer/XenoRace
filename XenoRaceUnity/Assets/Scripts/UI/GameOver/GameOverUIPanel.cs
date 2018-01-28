using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameOverUIPanel : UIPanel
{
    /// <summary>
    /// Message set at runtime 
    /// </summary>
    [SerializeField]
    private Text message;


    /// <summary>
    /// When we open up, we need to set the message and highlight our first button. 
    /// </summary>
    /// <param name="transitionTrigger"></param>
    public override void Open(string transitionTrigger = "")
    {
        base.Open(transitionTrigger);


    }

    public void Build()
    {
        Debug.Log("BUILD PRESSED"); 
    }

    public void Retry()
    {
        Debug.Log("RETRY PRESSED"); 
    }

    public void Quit()
    {
        Debug.Log("QUIT PRESSED"); 
    }

}
