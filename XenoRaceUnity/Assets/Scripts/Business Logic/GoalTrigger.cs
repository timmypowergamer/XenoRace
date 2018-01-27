using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// When the player has entered the Goal trigger, we need to communicate that the player has won 
/// the game to anyone who cares. 
/// </summary>
public class GoalTrigger : MonoBehaviour
{
    /// <summary>
    /// When someone enters the trigger, we need to check if it's the player. If it is, then we need to tell 
    /// anyone who cares. (Action? Delegate? Messenger system? Singleton call?). 
    /// </summary>
    /// <param name="other">The collider that entered our trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GOAL TRIGGER has been entered by " + other.name); 
    }
}
